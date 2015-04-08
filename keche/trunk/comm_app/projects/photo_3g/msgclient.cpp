#include "msgclient.h"
#include <tools.h>
#include <netutil.h>
#include <intercoder.h>

#include "tinyxml.h"
#include "httpquery.h"
#include "../tools/utils.h"

#define DOMAIN_POS "PHOTO3G_POS"
#define DOMAIN_MAP "PHOTO3G_MAP"

MsgClient::MsgClient(void)
{
	_pEnv = NULL;
	_httpUrl = "";
	_thread_num = 1;

	_timeQueue.init(3);
}

MsgClient::~MsgClient(void)
{
	Stop();
}

bool MsgClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	char buf[1024] = {0};

	if ( ! _pEnv->GetString( "msg_user_name", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_user_name empty" ) ;
		return false ;
	}
	_client_user._user_name = buf;

	if ( ! _pEnv->GetString( "msg_user_pwd", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_user_pwd empty" ) ;
		return false ;
	}
	_client_user._user_pwd  = buf;

	if ( ! _pEnv->GetString( "msg_connect_ip", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_connect_ip empty" ) ;
		return false ;
	}
	_client_user._ip = buf ;

	if ( ! _pEnv->GetString( "msg_connect_port", buf ) ) {
		OUT_ERROR( NULL, 0, NULL, "msg_connect_port empty" ) ;
		return false ;
	}
	_client_user._port = atoi(buf) ;

	int nvalue = 1;
	if (pEnv->GetInteger("msg_tcp_thread", nvalue)) {
		_thread_num = nvalue;
	}

	if( ! _pEnv->GetString("http_url", buf)) {
		OUT_ERROR( NULL, 0, NULL, "http_url empty" ) ;
		return false;
	}
	_httpUrl = buf;

	setpackspliter(&_packspliter);

	_client_user._user_state = User::OFF_LINE;

	return true;
}

void MsgClient::Stop(void)
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void)
{
	return StartClient(_client_user._ip.c_str(), _client_user._port, _thread_num);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	const char *ptr = (const char *) data;

	if (len < 4)
	{
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len );
		return;
	}

	//OUT_RECV( sock->_szIp, sock->_port,  NULL, "on_data_arrived:[%d]%s", len, ptr);

	if (strncmp(ptr, "CAIT", 4) == 0) {
		// 纷发处理数据
		HandleInnerData( sock, ptr, len);
	} else {
		// 处理登陆相关
		HandleSession( sock, ptr, len);
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	_client_user._user_state = User::OFF_LINE;
}

void MsgClient::TimeWork()
{
	while (Check()) {
		if(_client_user._user_state != User::ON_LINE || time(NULL) - _client_user._last_active_time > 30) {
			ConnectServer(_client_user, 3);
		} else {
			SendData(_client_user._fd, "NOOP \r\n", 7);
		}

		sleep(10);
	}
}

void MsgClient::NoopWork()
{
	IRedisCache *redis = _pEnv->GetRedisCache();

	while (Check()) {
		vector<string> keys;
		vector<string>::iterator it;
		string innerMsg;
		string oem, sim2g, sim3g;

		redis->HKeys("PHOTO3G_POS", keys);
		for(it = keys.begin(); it != keys.end(); ++it) {
			sim2g = *it;
			sim3g = "";
			oem = "";
			if( ! get3gby2g(sim2g, sim3g, oem)) {
				continue;
			}

			// 3g终端保持在线状态
			innerMsg = "CAITS 0_0 " + oem + "_" + sim3g + " 0 U_CONN {TYPE:1} \r\n";
			SendData(_client_user._fd, innerMsg.c_str(), innerMsg.length());
		}

		list<int> vals;
		_timeQueue.check(vals);

		sleep(30);
	}
}

int MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	sprintf(buf, "LOGI SAVE %s %s \r\n", user._user_name.c_str(), user._user_pwd.c_str());
	return (int) strlen(buf);
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len)
{
	string line(data, len - 2);

	vector<string> vec_temp;
	if (!splitvector(line, vec_temp, " ", 1))
	{
		return;
	}

	string head = vec_temp[0];
	if (head == "LACK")
	{
		/*
		 RESULT
		 >=0:权限值
		 -1:密码错误
		 -2:帐号已经登录
		 -3:帐号已经停用
		 -4:帐号不存在
		 -5:sql查询失败
		 -6:未登录数据库
		 */
		int ret = atoi(vec_temp[1].c_str());
		switch (ret)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			_client_user._user_state = User::ON_LINE;
			OUT_INFO(sock->_szIp, sock->_port, NULL, "LACK,login success!");
			break;
		case -1:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,password error!");
			break;
		case -2:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,the user has already login!");
			break;
		case -3:
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "LACK,user name is invalid!");
			break;
		default:
			OUT_ERROR( sock->_szIp, sock->_port, NULL, "unknow result" );
			break;
		}

		// 如果返回错误则直接处理
		if (ret < 0)
		{
			_tcp_handle.close_socket( sock );
		}
	}
	else if (head == "NOOP_ACK") {
		_client_user._last_active_time = time(NULL);
		OUT_INFO( sock->_szIp, sock->_port, _client_user._user_name.c_str(), "NOOP_ACK");
	} else {
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "except message:%s", (const char*)data);
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len)
{
	string   line;
	InnerMsg imsg;

	string strVal;
	string msgType;

	string oem, sim2g, sim3g;

	IPhotoSvr   *photo = _pEnv->GetPhotoSvr();
	IRedisCache *redis = _pEnv->GetRedisCache();

	line.assign(data, len);

	if( ! parseInnerMsg(line, imsg)) {
		return;
	}

	if (imsg.macid.length() != 16) {
		return; // 不是正确的macid
	}

	sim2g = getInnerMsgArg(imsg, "BASE_TEL");
	if ( ! sim2g.empty()) { // 发送给3g终端的消息
		if (_timeQueue.insert(imsg.seqid, 0) == false) {
			return; // 避免重复的消息
		}

		OUT_INFO(sock->_szIp, sock->_port, "RECV", "%s", line.c_str());

		char strTime[128];
		snprintf(strTime, 128, "%lu", time(NULL) + 1800);

		oem = imsg.macid.substr(0, 4);
		sim3g = imsg.macid.substr(5);

		// 在缓存中更新3g号码查2g号码的映射
		strVal = strTime + string(":") + oem + ":" + sim2g;
		redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());
		// 在缓存中更新2g号码查3g号码的映射
		strVal = strTime + string(":") + oem + ":" + sim3g;
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());

		photo->HandleData(imsg);

		return;
	}

	oem = sim3g = "";
	sim2g = imsg.macid.substr(5);
	if ( ! get3gby2g(sim2g, sim3g, oem)) {
		return; // 不是3g扩展的终端
	}

	msgType = getInnerMsgArg(imsg, "TYPE");
	if(imsg.order == "U_REPT" && msgType == "0") {
		// 在缓存中保存2g标准终端的最新位置
		strVal = ",20:" + getInnerMsgArg(imsg, "20");
		strVal += ",8:" + getInnerMsgArg(imsg, "8");
		strVal += ",1:" + getInnerMsgArg(imsg, "1");
		strVal += ",2:" + getInnerMsgArg(imsg, "2");
		strVal += ",6:" + getInnerMsgArg(imsg, "6");
		strVal += ",3:" + getInnerMsgArg(imsg, "3");
		strVal += ",4:" + getInnerMsgArg(imsg, "4");
		strVal += ",5:" + getInnerMsgArg(imsg, "5");
		redis->HSet(DOMAIN_POS, sim2g.c_str(), strVal.c_str());
	} else if(imsg.order == "U_REPT" && msgType == "5") {
		strVal = getInnerMsgArg(imsg, "18");
		if(strVal.compare(0, 2, "0/") == 0) {
			//　标准终端离线，删除缓存中GPS位置数据
			redis->HDel(DOMAIN_POS, sim2g.c_str());
		}
	}
}

void MsgClient::HandleData( const InnerMsg & msg )
{
	const char *ip = _client_user._ip.c_str();
	unsigned short port = _client_user._port;

	string line;

	if( ! spellInnerMsg(msg, line)) {
		OUT_WARNING(ip, port, "SPELLERR", "macid:%s, seqid: %s", msg.macid.c_str(), msg.seqid.c_str());
		return;
	}

	if (_client_user._user_state != User::ON_LINE || _client_user._fd == NULL) {
		OUT_WARNING(ip, port, "OFFLINE", "%s", line.c_str());
		return;
	}

	OUT_INFO(ip, port, "SEND", "%s", line.c_str());

	SendData(_client_user._fd, line.c_str(), line.length());
}

bool MsgClient::parseInnerMsg(const string &str, InnerMsg &msg)
{
	string arg;
	string macid;
	vector<string> vec1;
	vector<string> vec2;
	vector<string> vec3;
	vector<string>::iterator ite;

	vec1.clear();
	Utils::splitStr(str, vec1, ' ');
	if (vec1.size() < 7) {
		// 内部消息的字段数是固定7个字段
		return false;
	}

	arg = vec1[5];
	if(arg.length() < 3) {
		// 指令参数的长度过短
		return false;
	}
	arg = arg.substr(1, arg.length() - 2);

	vec2.clear();
	Utils::splitStr(arg, vec2, ',');

	msg.begin = vec1[0];
	msg.seqid = vec1[1];
	msg.macid = vec1[2];
	msg.order = vec1[4];
	msg.param.clear();

	for(ite = vec2.begin(); ite != vec2.end(); ++ite) {
		vec3.clear();
		Utils::splitStr(*ite, vec3, ':');

		if(vec3.size() != 2) {
			continue;
		}

		msg.param.insert(make_pair(vec3[0], vec3[1]));
	}

	return true;
}

bool MsgClient::spellInnerMsg(const InnerMsg &msg, string &str)
{
	map<string, string>::const_iterator ite;
	string args;

	for(ite = msg.param.begin(); ite != msg.param.end(); ++ite) {
		if(args.empty()) {
			args = ite->first + ":" + ite->second;
		} else {
			args += "," + ite->first + ":" + ite->second;
		}
	}

	IRedisCache *redis = _pEnv->GetRedisCache();

	string type = getInnerMsgArg(msg, "TYPE");

	// 如果是图片，需要在缓存中获取关联终端的GPS位置数据
	if(msg.order == "U_REPT" && type == "3" && msg.macid.length() == 16) {
		string hVal = "";
		string baseTel = msg.macid.substr(5);

		if ( ! redis->HGet(DOMAIN_POS, baseTel.c_str(), hVal)) {
			return false;
		}

		args += hVal;  // 在3g图片消息中添加关联的2g终端的位置数据
	}

	str = msg.begin + " " + msg.seqid + " " + msg.macid + " 0 " + msg.order + " {" + args + "} \r\n";

	return true;
}

bool MsgClient::get3gby2g(const string &sim2g, string &sim3g, string &oem) {
	const char *chrVal;
	string strVal;
	vector<string> fields;

	IRedisCache *redis = _pEnv->GetRedisCache();
	time_t curTime = time(NULL);

	if (redis->HGet(DOMAIN_MAP, sim2g.c_str(), strVal)
			&& Utils::splitStr(strVal, fields, ':') == 3
			&& atoi(fields[0].c_str()) > curTime) {
		oem = fields[1];
		sim3g = fields[2];

		if(oem.length() != 4 || sim3g.length() != 11) {
			return false; // 长度不正确
		}
		return true;
	}

	char strTime[128];
	snprintf(strTime, 128, "%lu", curTime + 1800); // 30分钟超时更新
	strVal = strTime + string(":0:0"); // 获取号码失败时使用的默认值

	HttpQuery hq;
	string dat = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"\
			"<Request service=\"vehicleInforService\" "\
			"method=\"get3gBy2g\" id=\"0\"><Param><Item  "\
			"sim2=\"" + sim2g + "\" /></Param></Request>";

	if ( ! hq.post(_httpUrl, dat)) {
		OUT_WARNING(NULL, 0, "get3gBy2g", "http query %s fail",	sim3g.c_str());
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}

	string strXml((char*) hq.data(), hq.size());
	TiXmlDocument doc;
	TiXmlElement *root;
	TiXmlElement *node;

	doc.Parse(strXml.c_str(), 0, TIXML_ENCODING_UTF8);
	if ((root = doc.RootElement()) == NULL) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}

	if ((node = root->FirstChildElement("Result")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}

	if ((node = node->FirstChildElement("Item")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}

	if ((chrVal = node->Attribute("oemcode")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}
	oem = chrVal;

	if ((chrVal = node->Attribute("commaddr")) == NULL) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}
	sim3g = chrVal;

	if (oem.length() != 4 || sim3g.length() != 11) {
		redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
		return false;
	}

	// 在缓存中更新2g号码查3g号码的映射
	strVal = strTime + string(":") + oem + ":" + sim3g;
	redis->HSet(DOMAIN_MAP, sim2g.c_str(), strVal.c_str());
	// 在缓存中更新3g号码查2g号码的映射
	strVal = strTime + string(":") + oem + ":" + sim2g;
	redis->HSet(DOMAIN_MAP, sim3g.c_str(), strVal.c_str());

	return true;
}

string MsgClient::getInnerMsgArg(const InnerMsg &msg, const string &key)
{
	map<string, string>::const_iterator ite;

	ite = msg.param.find(key);
	if(ite == msg.param.end()) {
		return "";
	}

	return ite->second;
}
