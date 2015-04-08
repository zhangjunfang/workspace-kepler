#include "msgclient.h"
#include <tools.h>
#include <Base64.h>
#include "../tools/utils.h"

#include <fstream>
using std::ifstream;

#include <unistd.h>
#include <fcntl.h>

MsgClient::MsgClient()
{
	_seqid = 0;
	_dmdfile = "";
	_scppath = "";
	_threadnum = 1;
}

MsgClient::~MsgClient( void )
{
	Stop() ;
}

bool MsgClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char temp[256] = {0} ;
	int value;

	// 加载基本订阅列表路径
	if (!pEnv->GetString("dmd_file", temp)) {
		OUT_ERROR("MsgClient", 0, "INIT", "dmd_file is empty");
		return false;
	}
	_dmdfile.assign(temp);

	// msg客户端线程数
	if(_pEnv->GetInteger("msg_thread_num", value)) {
		_threadnum = value;
	}

	if (!_pEnv->GetString("scp_path", temp)) {
		OUT_ERROR("MsgClient", 0, "INIT", "scp_path is empty");
		return false;
	}
	_scppath = temp;

	if (!_pEnv->GetString("msg_connect_ip", temp)) {
		OUT_ERROR("MsgClient", 0, "INIT", "msg_connect_ip is empty");
		return false;
	}
	_client_user._ip = temp;

	if (!_pEnv->GetInteger("msg_connect_port", value)) {
		OUT_ERROR("MsgClient", 0, "INIT", "msg_connect_port is empty");
		return false;
	}
	_client_user._port = value;

	if (!_pEnv->GetString("msg_user_name", temp)) {
		OUT_ERROR("MsgClient", 0, "INIT", "msg_user_name is empty");
		return false;
	}
	_client_user._user_name = temp;

	if (!_pEnv->GetString("msg_user_pwd", temp)) {
		OUT_ERROR("MsgClient", 0, "INIT", "msg_user_pwd is empty");
		return false;
	}
	_client_user._user_pwd = temp;

	if (!_pEnv->GetString("pas_user_name", temp)) {
		OUT_ERROR("PasClient", 0, "INIT", "pas_user_name is empty");
		return false;
	}
	_pas_user_name = temp;

	_client_user._last_active_time = 0;
	_client_user._connect_info.timeval = 0;

	// 设置分包对象
	setpackspliter(&_packspliter);

	return true;
}


void MsgClient::Stop( void )
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void) {
	return StartClient(_client_user._ip.c_str(), _client_user._port, _threadnum);
}

void MsgClient::on_data_arrived(socket_t *sock, const void* data, int len) {
	if (len < 4) return;

	const char *ptr = (const char *) data;
	OUT_RECV( sock->_szIp, sock->_port, "RECV", "%.*s", len - 2, ptr);

	if (strncmp(ptr, "CAIT", 4) == 0) {
		// 纷发处理数据
		HandleInnerData(sock, ptr, len);
	} else {
		// 处理登陆相关
		HandleSession(sock, ptr, len);
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_WARNING(sock->_szIp, sock->_port, "Conn", "Disconnection fd %d", sock->_fd);

	_client_user._fd = NULL;
	_client_user._user_state = User::OFF_LINE;
	_client_user._login_time = 0;
}

void MsgClient::TimeWork()
{
	while (Check()) {
		time_t tv = time(NULL);
		User &user = _client_user;

		if(user._fd == NULL || user._user_state != User::ON_LINE || tv - user._last_active_time > 180) {
			ConnectServer(_client_user, 30);
		} else if(tv - user._last_active_time > 30) {
			string inner = "NOOP \r\n";

			SendData(user._fd, inner.c_str(), inner.length());
			OUT_SEND(user._ip.c_str(), user._port, "SEND", "%.*s", inner.length(), inner.c_str());
		}

		sleep(10);
	}
}

void MsgClient::NoopWork()
{
}

// 向MSG上传消息
bool MsgClient::HandleData(const char *data, int len )
{
	User &user = _client_user;
	vector<string> fields;
	string req(data, len);

	fields.clear();
	if(Utils::splitStr(req, fields, ',') < 4) {
		return false;
	}

	string seq = fields[1];
	string sim = fields[2];
	string cmd = fields[3];

	string seqid = getSeqid(sim);
	string macid = getMacid(sim);
	if(macid.empty()) {
		OUT_WARNING(user._ip.c_str(), user._port, "FAIL", "%s not found macid", sim.c_str());
		return false;
	}
	string inner = "CAITS " + seqid + " " + macid + " 0 ";

	if(cmd == "0") {
		inner += "D_CALL {TYPE:0,0:0,1:0} \r\n";
	} else if(cmd == "1" && fields.size() == 6) { //临时跟踪
		string times = fields[4];
		string count = fields[5];

		//次数为0表示永久,内部协议-1为永久
		count = (count == "0") ? "-1" : count;

		if(count == "1") { // 次数为1是位置查询
			inner += "D_CALL {TYPE:2} \r\n";
		} else {
			inner += "D_CALL {TYPE:0,0:" + times + ",1:" + count + "} \r\n";
		}
	} else if(cmd == "5" && fields.size() == 5) { //单向监听
		string callsim = fields[4];
		inner += "D_CTLM {TYPE:9,VALUE:" + callsim + "} \r\n";
	} else if(cmd == "7" && fields.size() == 5) { //文本信息
		string text = fields[4];
		inner += "D_SNDM {TYPE:1,1:0,2:" + text + "} \r\n";
	} else if(cmd == "8" && fields.size() == 10) { //拍摄照片
		string cmd = fields[4];   //拍摄标志
		size_t ids = atoi(fields[5].c_str());
		string times = fields[7]; //拍摄间隔
		string count = fields[8]; //图片个数

		if(cmd == "0") {
			count = "0";       //拍摄标志为0则停止，内部协议的图片个数为0是停止
		} else if(count == "0") {
			count = "0xFFF0";  //图片个数为0无限拍，内部协议的图片个数为FFF0是无限
		}

		if(ids & 1) {
			inner += "D_CTLM {TYPE:10,VALUE:1|" + count + "|" + times + "|0|0|5|127|65|65|127} \r\n";
		} else if(ids & 2) {
			inner += "D_CTLM {TYPE:10,VALUE:2|" + count + "|" + times + "|0|0|5|127|65|65|127} \r\n";
		} else if(ids & 4) {
			inner += "D_CTLM {TYPE:10,VALUE:3|" + count + "|" + times + "|0|0|5|127|65|65|127} \r\n";
		} else if(ids & 8) {
			inner += "D_CTLM {TYPE:10,VALUE:4|" + count + "|" + times + "|0|0|5|127|65|65|127} \r\n";
		}
	}

	if (user._user_state != User::ON_LINE || user._fd == NULL) {
		OUT_WARNING( user._ip.c_str(), user._port, "FAIL", "%.*s", inner.length(), inner.c_str());
		return false;
	}

	if (!SendData(user._fd, inner.c_str(), inner.length())) {
		OUT_WARNING( user._ip.c_str(), user._port, "FAIL", "%.*s", inner.length(), inner.c_str());
		return false;
	}

	OUT_SEND(user._ip.c_str(), user._port, "SEND", "%.*s", inner.length(), inner.c_str());

	share::Guard guare(_replymutex);
	_replycache.insert(make_pair(seqid, cmd + "," + seq));

	return true;
}

// 加载订阅数据
void MsgClient::LoadSubscribe()
{
	User &user = _client_user;

	if(_dmdfile.empty()) {
		OUT_ERROR(user._ip.c_str(), user._port, "LOAD", "dmd file is empty");
		return;
	}

	vector<string> fields;
	map<string, string> macidquery;

	ifstream ifs;
	string cmd = "DMD";
	string line;
	string inner = "";

	ifs.open(_dmdfile.c_str());
	while(getline(ifs, line)) {
		if(line.empty() || line[0] == '#') {
			continue;
		}

		if(inner.empty()) {
			inner.assign(cmd + " 0 {" + line);
			cmd = "ADD";
		} else {
			inner.append("," + line);
		}

		if(inner.length() > 10240) {
			inner.append("} \r\n");
			SendData(user._fd, inner.c_str(), inner.length());
			inner.clear();
		}

		fields.clear();
		if(Utils::splitStr(line, fields, '_') != 2) {
			continue;
		}
		macidquery.insert(make_pair(fields[1], line));
	}
	ifs.close();

	if (!inner.empty()) {
		inner.append("} \r\n");
		SendData(user._fd, inner.c_str(), inner.length());
	}

	if (!macidquery.empty()) {
		share::Guard guard(_macidmutex);
		_macidquery = macidquery;
	}
}

// 构建登陆处理
int MsgClient::build_login_msg(User &user, char *buf, int len)
{
	return snprintf(buf, len, "LOGI SAVE %s %s DM \r\n", user._user_name.c_str(), user._user_pwd.c_str());
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len )
{
	string line = string(data, len);

	vector<string> vec_temp ;
	if ( ! splitvector( line, vec_temp, " " , 1 ) ) {
		return ;
	}

	string head = vec_temp[0];
	if (head == "LACK") {
		/*
			RESULT
			>=0:权限值
			-1:密码错误
			-2:帐号已经登录
			-3:帐号已经停用
			-4:帐号不存在
		 */
		int ret = atoi( vec_temp[1].c_str() ) ;
		switch( ret ) {
		case 0:
		case 1:
		case 2:
		case 3:
			_client_user._fd = sock;
			_client_user._user_state = User::ON_LINE;
			_client_user._last_active_time = time(NULL);
			OUT_CONN( sock->_szIp, sock->_port, _client_user._user_name.c_str(), "Login success");
			// 登陆成功，如果为数据订制连接就直接需要处理发送订阅处理
			LoadSubscribe();
			break;
		case -1:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "LACK,password error!");
			break;
		case -2:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "LACK,the user has already login!");
			break;
		case -3:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "LACK,user name is invalid!");
			break;
		default:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "unknow result" );
			break;
		}

		// 如果返回错误则直接处理
		if ( ret < 0 ) {
			_tcp_handle.close_socket(sock);
		}
	}
	else if (head == "NOOP_ACK") {
		_client_user._last_active_time = time(NULL);
		OUT_INFO( sock->_szIp, sock->_port, _client_user._user_name.c_str(), "NOOP_ACK");
	} else {
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "except message:%s", (const char*)data);
	}
}

void MsgClient::parseParam(const string &param, map<string, string> &detail)
{
	vector<string> item;
	vector<string> pair;
	vector<string>::iterator it;

	item.clear();
	Utils::splitStr(param, item, ',');
	for(it = item.begin(); it != item.end(); ++it) {
		pair.clear();
		if(Utils::splitStr(*it, pair, ':') != 2) {
			continue;
		}

		detail.insert(make_pair(pair[0], pair[1]));
	}
}

string MsgClient::queryParam(const string & key, const map<string, string> &detail)
{
	map<string, string>::const_iterator it;

	if((it = detail.find(key)) != detail.end()) {
		return it->second;
	}

	return "";
}

string MsgClient::leftAlign(const string src, size_t n, char c)
{
    string dst = string(n, c) + src;

    return dst.substr(dst.length() - n);
}

bool MsgClient::converUrpt(const string &macid, const string &seqid, const string &param, DataBuffer &buf)
{
	char tmpbuf[BUFSIZ + 1];
	vector<string> fields;
	map<string, string> detail; //消息体各个参数的key:value

	fields.clear();
	if(Utils::splitStr(macid, fields, '_') != 2) {
		return false;
	}

	string sim = fields[1]; //手机号码
	string block = "";      //监管协议

	parseParam(param, detail);
	string type = queryParam("TYPE", detail);
	if(type == "0") {
		block = "3"; //3号消息，车辆定位汇报
	} else {
		return false;
	}

	string lon = queryParam("1", detail);
	string lat = queryParam("2", detail);
	string speed = queryParam("3", detail);
	string gpsdt = queryParam("4", detail);
	string direct = queryParam("5", detail);
	string status = queryParam("8", detail);
	string termid = "14411" + leftAlign(_pas_user_name, 4, '0') + sim;

	snprintf(tmpbuf, BUFSIZ, "%.6f", atoi(lon.c_str()) / 600000.0);
	lon = tmpbuf;
	snprintf(tmpbuf, BUFSIZ, "%.6f", atoi(lat.c_str()) / 600000.0);
	lat = tmpbuf;
	snprintf(tmpbuf, BUFSIZ, "%.2f", atoi(speed.c_str()) / 10.0);
	speed = tmpbuf;
	if(gpsdt.length() == 15) {
		gpsdt = gpsdt.substr(0, 8) + gpsdt.substr(9);
	} else {
		gpsdt = "00000000000000";
	}

	size_t statint = atoi(status.c_str());
	string statstr = "0";
	if(statint & 1) {
		statstr = "20000";
	}

	block += "," + termid +"," + gpsdt +",1," + lon + "," + lat + "," + speed;
	block += "," + direct + "," + statstr;

	unsigned short msglen = htons(block.length());
	buf.writeBlock(&msglen, sizeof(short));
	buf.writeBlock(block.c_str(), block.length());

	return true;
}

bool MsgClient::converResp(const string &macid, const string &seqid, const string &param, DataBuffer &buf)
{
	string rSim;
	string rSeq;
	string rCmd;
	string rRet;

	vector<string> fields;

	fields.clear();
	if(Utils::splitStr(macid, fields, '_') != 2) {
		return false;
	}
	rSim = fields[1];

	size_t pos = param.find("RET:");
	if(pos != string::npos) {
		rRet = param.substr(pos + 4, 1);
	}

	if(rRet != "0") {
		rRet = "1"; //监管平台只有1表示错误
	}

	share::Guard guare(_replymutex);
	map<string, string>::iterator it = _replycache.find(seqid);
	if(it == _replycache.end()) {
		return false;
	}
	string replydata = it->second;
	_replycache.erase(it);

	fields.clear();
	if(Utils::splitStr(replydata, fields, ',') != 2) {
		return false;
	}
	rCmd = fields[0];
	rSeq = fields[1];

	string block = "(CONTROLRESP," + rSeq + "," + rSim + "," + rRet + "," + rCmd + ")";

	buf.writeBlock(block.c_str(), block.length() + 1);

	return true;
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len )
{
	string line( data, len ) ;
	vector<string>  vec ;
	if ( ! splitvector( line, vec, " " , 6 )  ){
		OUT_ERROR( sock->_szIp, sock->_port, _client_user._user_name.c_str() , "%.*s", len, data ) ;
		return ;
	}

	DataBuffer buf ;

	string head  = vec[0] ;
	string seqid = vec[1] ;
	string macid = vec[2] ;
	string code  = vec[3] ;  // 通信码，对于点名数据的区分
	string cmd   = vec[4] ;
	string value = vec[5] ;

	if(value.length() < 2) {
		return; //消息体不符合最小长度
	}

	//移除消息体中的{}
	string param = value.substr(1, value.length() - 2);

	if ( head == "CAITS" && cmd == "U_REPT" ) {
		//终端主动上传
		converUrpt(macid, seqid, param, buf);
	}

	if( buf.getLength() > 0 ) { //发送指定的地区用户
		_pEnv->GetPasClient()->HandleData( buf.getBuffer(), buf.getLength() ) ;
	}
}

string MsgClient::getSeqid(const string &sim)
{
	char buf[1024];

	snprintf(buf, 1024, "%s_%u", sim.c_str(), __sync_fetch_and_add(&_seqid, 1));

	return buf;
}

string MsgClient::getMacid(const string &sim)
{
	map<string, string>::iterator it;

	share::Guard guard(_macidmutex);
	if((it = _macidquery.find(sim)) != _macidquery.end()) {
		return it->second;
	}

	return "";
}
