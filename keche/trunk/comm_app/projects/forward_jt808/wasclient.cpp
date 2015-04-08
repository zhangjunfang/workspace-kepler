#include "wasclient.h"
#include <netutil.h>
#include <tools.h>
#include <BaseTools.h>

#include <fstream>
using std::ifstream;

#include "../tools/utils.h"
#include "jt808prot.h"

WasClient::WasClient()
{
	_hostfile = "";
	_thread_num = 3;
	_max_timeout = 180;
}

WasClient::~WasClient()
{
	Stop();
}

bool WasClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv ;

	char szbuf[512] = {0};

	if ( ! pEnv->GetString("media_path", szbuf)) {
		OUT_ERROR( NULL, 0, NULL, "get media_path  failure" ) ;
		return false;
	}
	_hostfile = szbuf;

	int nvalue = 0 ;
	if (pEnv->GetInteger("client_thread", nvalue)) {
		_thread_num = nvalue;
	}

	if ( pEnv->GetInteger( "was_user_time" , nvalue ) ) {
		_max_timeout = nvalue ;
	}

	loadHost();

	setpackspliter( &_spliter808 ) ;

	return true;
}

bool WasClient::Start()
{
	return StartClient( "" , 0 , _thread_num ) ;
}

void WasClient::Stop()
{
	StopClient() ;
}

void WasClient::TimeWork()
{
	while ( Check() ) {
		registerTerm();

		sleep(60) ;
	}
}

void WasClient::NoopWork()
{
	while( Check() ) {
		HandleOnlineUsers();

		HandleOfflineUsers();

		sleep(10) ;
	}
}

void WasClient::on_data_arrived(socket_t *sock, const void *data, int len)
{
	const char *ptr = (const char*)data;
	const char *ip    = sock->_szIp;
	unsigned int port = sock->_port;
	OUT_HEX(ip, port, "RECV", ptr, len);

	const size_t min_len = sizeof(JTHeader) + sizeof(JTFooter);
	const size_t max_len = (min_len + sizeof(MsgPart) + 0x3ff) * 2;
	if(len < (int)min_len || len > (int)max_len) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	vector<unsigned char> dstBuf;
	unsigned char *dstPtr;
	size_t dstLen;

	dstBuf.resize(len);
	dstPtr = &dstBuf[0];
	dstLen = Utils::deCode808((uint8_t*) data, len, dstPtr);

	JTHeader *header = (JTHeader*)dstPtr;
	uint16_t msgid = htons(header->msgid);
	uint16_t flags = htons(header->length);

	uint8_t ispack = flags & 0x2000; //第13位是否分包标识
	uint16_t length = flags & 0x3ff; //第0 ~ 9位消息体长度
	if(ispack == 1 && dstLen != length + min_len + sizeof(MsgPart)) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	} else if(ispack == 0 && dstLen != length + min_len) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	string simid;
	if(!bcd2sim(header->simid, simid)) {
		OUT_WARNING(ip, port, "RECV", "invalid message, drop it! length %d", len) ;
		return;
	}

	string keyStr;
	string valStr;

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}
	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);

	if(msgid == 0x8001) {
		if (dstLen != sizeof(CommonResp)) {
			return;
		}

		CommonResp *rsp = (CommonResp*)dstPtr;
		uint16_t rmsgid = htons(rsp->resp_msg_id);
		uint8_t result = rsp->result;

		if(rmsgid == 0x0102 && result == 0) {
			user._user_state = User::ON_LINE;
			_online_user.SetUser(user._user_id, user);
			return;
		}
	} else if(msgid == 0x8100) {
		CloseSocket(user._fd);

		TermRegisterResp *rsp = (TermRegisterResp*)dstPtr;
		if(rsp->result == 0) { //注册成功往redis保存鉴权码
			string akey((char*)rsp->akey, length - 3);
			_pEnv->GetRedisCache()->HSet("KCTX.FORWARD808.AKEY", user._user_id.c_str(), akey.c_str());
		}
	}

	_pEnv->GetWasServer()->HandleData(simid, dstPtr, dstLen);
}

void WasClient::on_dis_connection(socket_t *sock)
{
	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}

	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp , sock->_port , user._user_id.c_str(), "ENV on_dis_connection fd %d", sock->_fd ) ;

	user._fd = NULL;
	user._user_state  = User::OFF_LINE ;
	_online_user.SetUser(user._user_id, user);
}

int WasClient::build_login_msg( User &user, char *buf,int buf_len )
{
	vector<uint8_t> msg;

	if(!buildTerm0102(user._user_id, msg) || (int)msg.size() > buf_len) {
		memcpy(buf, "\x7e\x00\x7e", 3);
		return 3;
	}

	memcpy(buf, &msg[0], msg.size());

	return msg.size();
}

void WasClient::HandleOfflineUsers()
{
	vector<User> users = _online_user.GetOfflineUsers(3*60);

	for(int i = 0; i < (int)users.size(); i++) {
		User &user = users[i];

		if(!_pEnv->GetWasServer()->ChkTerminal(user._user_name)) {
			continue;
		}

		ConnectServer(user, 1);
		_online_user.AddUser( user._user_id, user );
	}
}

bool WasClient::bcd2sim(uint8_t *bcd, string &sim)
{
    const char *TAB = "0123456789abcdef";
    const size_t BCDLEN = 6;
    char buf[BCDLEN * 2 + 1];
    size_t i;
    size_t begin = string::npos;
    size_t end = 0;

    for(i = 0; i < BCDLEN; ++i) {
        buf[end] = TAB[bcd[i] >> 4];
        if(begin == string::npos && buf[end] != '0') {
            begin = end;
        }
        ++end;

        buf[end] = TAB[bcd[i] & 0xf];
        if(begin == string::npos && buf[end] != '0') {
            begin = end;
        }
        ++end;
    }
    buf[end] = 0;

    if(begin == string::npos) {
        return false;
    }
    sim = buf + begin;

    return true;
}

uint8_t WasClient::get_check_sum(uint8_t *buf, size_t len)
{
	if(buf == NULL || len < 1)
		return 0;
	unsigned char check_sum = 0;
	for (int i = 0; i < (int) len; i++) {
		check_sum ^= buf[i];
	}
	return check_sum;
}

bool WasClient::buildTerm0100(const TermInfo &term, vector<uint8_t> &msg)
{
	TermRegister *reg;

	if(term.plate.length() > 128) {
		return false;
	}

	msg.resize(sizeof(TermRegister) + term.plate.length() + sizeof(JTFooter));
	memset(&msg[0], 0, msg.size());
	reg = (TermRegister*)&msg[0];

	reg->header.delim = 0x7e;
	reg->header.msgid = htons(0x0100);
	reg->header.length = htons(msg.size() - sizeof(JTHeader) - sizeof(JTFooter));
	Utils::hex2array(term.sim, reg->header.simid);
	reg->header.seqid = htons(0);

	term.termMfrs.copy(reg->corp_id, 5);
	term.termType.copy(reg->termtype, 20);
	term.termID.copy(reg->termid, 7);
	reg->carcolor = atoi(term.color.c_str());
	term.plate.copy(reg->carplate, term.plate.length());

	JTFooter *footer = (JTFooter*)(&msg[0] + sizeof(TermRegister) + term.plate.length());
	footer->check_sum = get_check_sum(&msg[1], msg.size() - 3);
	footer->delim = 0x7e;

	return true;
}

bool WasClient::buildTerm0102(const string &userid, vector<uint8_t> &msg)
{
	size_t pos = userid.find('_');
	if(pos == string::npos) {
		return false;
	}
	string sim = userid.substr(0, pos);

	string akey;
	_pEnv->GetRedisCache()->HGet("KCTX.FORWARD808.AKEY", userid.c_str(), akey);
	if (akey.empty()) {
		return false; //redis 中不存在鉴权码
	}

	JTHeader *header;
	JTFooter *footer;
	char *akeyptr;

	msg.resize(sizeof(JTHeader) + akey.length() + sizeof(JTFooter));
	header = (JTHeader*)&msg[0];
	footer = (JTFooter*)&msg[sizeof(JTHeader) + akey.length()];
	akeyptr = (char*)&msg[sizeof(JTHeader)];

	header->delim = 0x7e;
	header->msgid = htons(0x0102);
	header->length = htons(akey.length());
	Utils::hex2array(sim, header->simid);
	header->seqid = htons(0);
	akey.copy(akeyptr, akey.length());
	footer->check_sum = get_check_sum(&msg[1], msg.size() - 3);
	footer->delim = 0x7e;

	return false;
}

void WasClient::registerTerm()
{
	string keyStr;
	string valStr;
	vector<string> fields;

	string userid;
	User user;

	vector<string> allsim;
	vector<string>::iterator itVs;
	map<string, HostInfo>::iterator itMsh;

	TermInfo term;
	vector<uint8_t> msg;

	allsim.clear();
	_pEnv->GetRedisCache()->HKeys("KCTX.FORWARD808.INFO", allsim);
	for(itVs = allsim.begin(); itVs != allsim.end(); ++itVs) {
		const string &sim = *itVs;

		msg.clear();
		for(itMsh = _hostInfo.begin(); itMsh != _hostInfo.end(); ++itMsh) {
			const string &code = itMsh->first;
			const HostInfo &host = itMsh->second;

			keyStr = sim + "_" + code;
			valStr.clear();
			_pEnv->GetRedisCache()->HGet("KCTX.FORWARD808.AKEY", keyStr.c_str(), valStr);
			if(!valStr.empty()) {
				continue; //redis 中已存在鉴权码
			}

			if(msg.empty()) { //同个终端注册消息只需要构建一次
				valStr.clear();
				_pEnv->GetRedisCache()->HGet("KCTX.FORWARD808.INFO", sim.c_str(), valStr);
				fields.clear();
				if (Utils::splitStr(valStr, fields, ',') != 5) {
					break; //redis中的注册信息格式不对
				}
				term.sim = sim;
				term.color = fields[0];
				term.plate = fields[1];
				term.termMfrs = fields[2];
				term.termType = fields[3];
				term.termID = fields[4];

				if(!buildTerm0100(term, msg)) {
					break;
				}
			}

			user._user_id = keyStr;
			user._user_state = User::OFF_LINE;
			user._ip = host.ip;
			user._port = host.port;
			user._last_active_time = time(NULL);
			user._connect_info.last_reconnect_time = 0;
			user._connect_info.timeval = 0;
			user._fd = _tcp_handle.connect_nonb(user._ip.c_str(), user._port, 1);
			if(user._fd == NULL) {
				continue; //连接对接平台前置机失败。
			}

			Send7ECodeData(user._fd, &msg[0], msg.size());

			_online_user.AddUser(user._user_id, user);
		}
	}
}

void WasClient::loadHost()
{
	ifstream ifs;
	string line;
	vector<string> fields;

	string key;
	HostInfo val;

	ifs.open(_hostfile.c_str());
	while(getline(ifs, line)) {
		if(line.empty() || line[0] == '#') {
			continue;
		}

		fields.clear();
		if(Utils::splitStr(line, fields, ':') != 5) {
			continue;
		}

		if(fields[0] != "WASCLIENT") {
			continue;
		}

		//WASCLIENT:编号:ip地址:端口:是否开鉴权
		key = fields[1];
		val.ip = fields[2];
		val.port = atoi(fields[3].c_str());
		_hostInfo.insert(make_pair(key, val));
	}
}

bool WasClient::Send7ECodeData(socket_t *sock, const uint8_t *ptr, size_t len)
{
	vector<unsigned char> msgBuf;
	unsigned char *msgPtr;
	size_t msgLen;

	msgBuf.resize(len * 2);
	msgPtr = &msgBuf[0];
	msgLen = Utils::enCode808(ptr, len, msgPtr);

	OUT_HEX( sock->_szIp, sock->_port, "SEND" , (char*)msgPtr, msgLen );

	return SendData( sock, (char*)msgPtr, msgLen) ;
}

bool WasClient::HandleData(const string &sim, const uint8_t *ptr, size_t len)
{
	string userid;
	map<string, HostInfo>::iterator it;

	for (it = _hostInfo.begin(); it != _hostInfo.end(); ++it) {
		const string &key = it->first;

		userid = sim + "_" + key;
		User user = _online_user.GetUserByUserId(userid);
		if (user._user_id.empty()) {
			continue;
		}

		if(user._fd == NULL || user._user_state != User::ON_LINE) {
			continue;
		}

		Send7ECodeData(user._fd, ptr, len);
	}

	return true;
}

bool WasClient::AddTerminal(const string &sim)
{
	string userid;
	map<string, HostInfo>::iterator it;

	for (it = _hostInfo.begin(); it != _hostInfo.end(); ++it) {
		const string &key = it->first;
		const HostInfo &val = it->second;

		userid = sim + "_" + key;
		User user = _online_user.GetUserByUserId(userid);
		if (!user._user_id.empty()) {
			continue;
		}

		user._user_id = userid;
		user._user_name = sim;
		user._fd = NULL;
		user._user_state = User::OFF_LINE;
		user._ip = val.ip;
		user._port = val.port;
		user._connect_info.last_reconnect_time = 0;
		user._connect_info.timeval = 0;

		ConnectServer(user, 1);
		_online_user.AddUser(userid, user);
	}

	return true;
}

bool WasClient::DelTerminal(const string &sim)
{
	string userid;
	map<string, HostInfo>::iterator it;

	for (it = _hostInfo.begin(); it != _hostInfo.end(); ++it) {
		const string &key = it->first;

		userid = sim + "_" + key;
		User user = _online_user.DeleteUser(userid);
		if (user._user_id.empty()) {
			continue;
		}

		if(user._fd != NULL) {
			CloseSocket(user._fd);
		}
	}

	return true;
}
