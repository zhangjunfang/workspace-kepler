#include "wasclient.h"
#include <netutil.h>
#include <tools.h>
#include <BaseTools.h>
#include <Base64.h>

#include <fstream>
using std::ifstream;

#include "utils.h"
#include "protocol.h"

WasClient::WasClient()
{
	_corp_data_path  = "";
	_thread_num 	 = 3;
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
	_protocol.init(szbuf);

	int nvalue = 0 ;
	if (pEnv->GetInteger("tcp_thread", nvalue)) {
		_thread_num = nvalue;
	}

	_gb808WaitReply.init(10);
	_innerWaitReply.init(30);
	_multiMediaData.init(60);

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
		HandleOnlineUsers();

		registerTerm();

		sleep(30) ;
	}
}

void WasClient::NoopWork()
{
	list<pair<string, Gb808WaitReply> > gb808WaitReply;
	list<pair<string, Gb808WaitReply> >::iterator itGb808;

	list<pair<string, MultiMediaData> > mediaData;
	list<pair<string, MultiMediaData> >::iterator itMedia;

	list<pair<string, InnerWaitReply> > innerWaitReply;
	list<pair<string, InnerWaitReply> >::iterator itInner;

	map<string, vector<unsigned char> > msg808Arr;
	string userid;
	User user;

	while( Check() ) {
		HandleOfflineUsers();

		// 多媒体ID超时检测
		mediaData.clear();
		_multiMediaData.check(mediaData);

		// 上行应答超时检测
		gb808WaitReply.clear();
		_gb808WaitReply.check(gb808WaitReply);
		for(itGb808 = gb808WaitReply.begin(); itGb808 != gb808WaitReply.end(); ++itGb808) {
			if(itGb808->second.number > 3) {
				continue;
			}

			user = _online_user.GetUserByUserId(itGb808->second.userid);
			if (user._user_id.empty() || user._user_state != User::ON_LINE
					|| user._fd == NULL || user._access_code != 1
					|| itGb808->second.login != user._login_time) {
				continue;
			}

			++itGb808->second.number;
			_gb808WaitReply.insert(itGb808->first, itGb808->second);

			vector<unsigned char> &msg = itGb808->second.msg808;
			OUT_HEX( user._ip.c_str(), user._port, user._user_name.c_str(), (const char*)&msg[0], msg.size() );
			SendData(user._fd, (char*) &msg[0], msg.size());
		}

		// 下行应答超时检测
		innerWaitReply.clear();
		_innerWaitReply.check(innerWaitReply);
		for(itInner = innerWaitReply.begin(); itInner != innerWaitReply.end(); ++itInner) {
			user = _online_user.GetUserByUserId(itInner->second.userid);
			if (user._user_id.empty() || user._user_state != User::ON_LINE
					|| user._fd == NULL || user._access_code != 1
					|| itGb808->second.login != user._login_time) {
				continue;
			}

			msg808Arr.clear();
			if(_protocol.buildGb808GeneralRsp(itInner->second.simpleMsg, msg808Arr, 1)) {
				sendDataToUser(user, msg808Arr);
			}
		}

		sleep(10) ;
	}
}

void WasClient::sendDataToUser(const User &user, const map<string, vector<unsigned char> > &msgs)
{
	map<string, vector<unsigned char> >::const_iterator it;

	for(it = msgs.begin(); it != msgs.end(); ++it) {
		const vector<unsigned char> &msg = it->second;

		OUT_HEX(user._ip.c_str(), user._port, user._user_name.c_str(), (char*)&msg[0], msg.size()) ;
		SendData(user._fd, (char*)&msg[0], msg.size());
	}
}

void WasClient::on_data_arrived(socket_t *sock, const void *data, int len)
{
	const char *ip    = sock->_szIp;
	unsigned int port = sock->_port;

	OUT_HEX( ip, port, "RECV", (char*)data, len ) ;

	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		return;
	}

	Gb808SimpleMsg simpleMsg;
	if( ! _protocol.prepareParseMsg(user._user_id, (const unsigned char*)data, (size_t)len, simpleMsg)) {
		return;
	}

	int i, n;
	char buffer[1024];

	string keyStr;
	string valStr;
	string keyMmd; //mmd = MultiMediaData

	string inner = "";
	Gb808GeneralRsp *genRsp;
	Gb808MultiMediaDataRsp *mmdRsp;
	Gb808TakePhoto *takePhoto;
	Gb808Text *text808;
	Gb808Call *call808;
	Gb808Reg_Rsp *reg_rsp;

	Gb808WaitReply gb808WaitReply;
	InnerWaitReply innerWaitReply;
	MultiMediaData multiMediaData;

	map<string, vector<unsigned char> > msg808Arr;

	CBase64 base64;

	switch(simpleMsg.msgid) {
	case 0x8001:
		if (simpleMsg.length < sizeof(Gb808GeneralRsp)) {
			return;
		}

		genRsp = (Gb808GeneralRsp*) &simpleMsg.body[0];

		if(genRsp->res == 0) {
			user._user_state = User::ON_LINE;
			user._access_code = 1;
		}
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		keyStr = simpleMsg.phone + "_" + Utils::int2str(ntohs(genRsp->seq), valStr);
		_gb808WaitReply.erase(keyStr, gb808WaitReply);

		break;
	case 0x8100:
		if (simpleMsg.length < 3) {
			return;
		}

		CloseSocket(user._fd);
		_online_user.DeleteUser(user._user_id); //注册对象只一次有效

		reg_rsp = (Gb808Reg_Rsp*)&simpleMsg.body[0];

		if(reg_rsp->res == 0) { //注册成功往redis保存鉴权码
			string authCode(reg_rsp->code, simpleMsg.length - 3);
			_pEnv->GetRedisCache()->HSet("KCTX.DIS808.PWD", user._user_id.c_str(), authCode.c_str());
		}
		break;
	case 0x8300:
		if (simpleMsg.length < sizeof(Gb808Text)) {
			return;
		}

		text808 = (Gb808Text*)&simpleMsg.body[0];

		base64.Encode((char*)text808->data, simpleMsg.length - sizeof(Gb808Text));
		keyStr = simpleMsg.phone + "_" + Utils::int2str(simpleMsg.seqid, valStr);
		inner = "CAITS " + keyStr + " " + user._user_name + " 0 D_SNDM {TYPE:1";
		inner += ",1:" + Utils::int2str(text808->flag, valStr);
		inner += ",2:" + string(base64.GetBuffer(), base64.GetLength()) + "} \r\n";

		_pEnv->GetMsgClient()->HandleMsgData(inner.c_str(), inner.length());

		innerWaitReply.userid = user._user_id;
		innerWaitReply.login = user._login_time;
		innerWaitReply.simpleMsg = simpleMsg;
		_innerWaitReply.insert(keyStr, innerWaitReply);
		break;
	case 0x8400:
		if (simpleMsg.length < sizeof(Gb808Call)) {
			return;
		}

		call808 = (Gb808Call*)&simpleMsg.body[0];

		keyStr = simpleMsg.phone + "_" + Utils::int2str(simpleMsg.seqid, valStr);
		inner = "CAITS " + keyStr + " " + user._user_name + " 0 D_CTLM {";
		switch(call808->flag == 0) {
		case 0:
			inner += "TYPE:16";
			break;
		case 1:
			inner += "TYPE:9";
			break;
		}
		inner += ",VALUE:" + string((char*)call808->data, simpleMsg.length - sizeof(Gb808Call)) + "} \r\n";

		_pEnv->GetMsgClient()->HandleMsgData(inner.c_str(), inner.length());

		innerWaitReply.userid = user._user_id;
		innerWaitReply.login = user._login_time;
		innerWaitReply.simpleMsg = simpleMsg;
		_innerWaitReply.insert(keyStr, innerWaitReply);
		break;
	case 0x8800:
		if(simpleMsg.length < sizeof(Gb808MultiMediaDataRsp)) {
			return;
		}

		mmdRsp = (Gb808MultiMediaDataRsp*)&simpleMsg.body[0];
		keyMmd = simpleMsg.phone + "_" + Utils::int2str(ntohl(mmdRsp->mmid), valStr);

		if(mmdRsp->size == 0) { // 应答成功
			if ( ! _multiMediaData.erase(keyMmd, multiMediaData)) {
				return;
			}

			for(i = 0; i < multiMediaData.count; ++i) {
				keyStr = simpleMsg.phone + "_" + Utils::int2str(multiMediaData.seqid + i, valStr);
				_gb808WaitReply.erase(keyStr, gb808WaitReply);
			}
		} else { // 需要补传
			if ( ! _multiMediaData.find(keyMmd, multiMediaData)) {
				return;
			}

			msg808Arr.clear();
			for(i = 0; i < mmdRsp->size; ++i) {
				n = ntohs(mmdRsp->index[i]) - 1;
				keyStr = simpleMsg.phone + "_" + Utils::int2str(multiMediaData.seqid + n, valStr);
				if( ! _gb808WaitReply.find(keyStr, gb808WaitReply)) {
					continue;
				}

				if(gb808WaitReply.login != user._login_time) {
					continue;
				}

				msg808Arr.insert(make_pair(keyStr, gb808WaitReply.msg808));
			}
			sendDataToUser(user, msg808Arr);
		}

		msg808Arr.clear();
		if(_protocol.buildGb808GeneralRsp(simpleMsg, msg808Arr, 0)) {
			sendDataToUser(user, msg808Arr);
		}
		break;
	case 0x8801:
		if (simpleMsg.length < sizeof(Gb808TakePhoto)) {
			return;
		}

		takePhoto = (Gb808TakePhoto*)&simpleMsg.body[0];
		takePhoto->command = ntohs(takePhoto->command);
		takePhoto->interval = ntohs(takePhoto->interval);
		takePhoto->ratio = takePhoto->ratio - 1;
		snprintf(buffer, 1024, " 0 D_CTLM {TYPE:10,VALUE:%u|%u|%u|%u|%u|%u|%u|%u|%u|%u} \r\n",
				takePhoto->channel, takePhoto->command, takePhoto->interval, takePhoto->save,
				takePhoto->ratio, takePhoto->quality, takePhoto->light, takePhoto->contrast,
				takePhoto->saturation, takePhoto->chroma);

		keyStr = simpleMsg.phone + "_" + Utils::int2str(simpleMsg.seqid, valStr);
		inner = "CAITS " + keyStr + " " + user._user_name + buffer;

		_pEnv->GetMsgClient()->HandleMsgData(inner.c_str(), inner.length());

		innerWaitReply.userid = user._user_id;
		innerWaitReply.login = user._login_time;
		innerWaitReply.simpleMsg = simpleMsg;
		_innerWaitReply.insert(keyStr, innerWaitReply);
		break;
	default:
		return;
	}
}

void WasClient::on_dis_connection(socket_t *sock)
{
	User user = _online_user.GetUserBySocket(sock);
	if(user._user_id.empty()) {
		OUT_WARNING(sock->_szIp, sock->_port, "Conn", "get user fail, fd %d", sock->_fd);
		return;
	}

	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp, sock->_port, "Conn", "Recv disconnect fd %d", sock->_fd ) ;

	user._fd = NULL;
	user._user_state  = User::OFF_LINE ;
	user._access_code = 0 ;

	_online_user.SetUser(user._user_id, user);
}

int WasClient::build_login_msg( User &user, char *buf,int buf_len )
{
	map<string, vector<unsigned char> > msg808Arr;
	map<string, vector<unsigned char> >::iterator msg808Ite;

	if(user._user_pwd.empty()) {
		// 不需要鉴权，发送心跳包，避免TCP_DEFER_ACCEPT
		_protocol.buildGb808HeartBeat(user._user_name, msg808Arr);
	} else {
		// 发送鉴权消息
		_protocol.buildGb808Login(user._user_name, user._user_pwd, msg808Arr);
	}

	int buflen = 0;

	msg808Ite = msg808Arr.begin();
	if(msg808Ite != msg808Arr.end()) {
		vector<unsigned char> &msg = msg808Ite->second;
		buflen = msg.size();
		if(buflen > buf_len) {
			return 0;
		}
		memcpy(buf, (char*)&msg[0], buflen);
	}

	return buflen;
}

bool WasClient::addActiveUser(const string &userid, const string &macid)
{
	string simid;
	string corpid;

	string line;
	vector<string> fields;

	string addr;
	int port;
	int authFlag;
	string authCode;

	fields.clear();
	if(Utils::splitStr(userid, fields, '_') != 2) {
		return false;
	}
	simid = fields[0];
	corpid = fields[1];

	line = _pEnv->GetUserMgr()->getCorpInfo(corpid);
	if(line.empty()) {
		return false;
	}

	fields.clear();
	if(Utils::splitStr(line, fields, ':') != 3) {
		return false;
	}
	addr = fields[0];
	port = atoi(fields[1].c_str());
	authFlag = atoi(fields[2].c_str());

	authCode = "";
	if(authFlag != 0) {
		if( ! _pEnv->GetRedisCache()->HGet("KCTX.DIS808.PWD", userid.c_str(), authCode) || authCode.empty()) {
			OUT_WARNING(addr.c_str(), port, userid.c_str(), "not found auth code");
			return false;
		}
	}

	User user;
	user._user_id = userid;
	user._fd = NULL;
	user._ip = addr;
	user._port = port;
	user._user_name = macid;
	user._user_pwd = authCode;
	user._user_state = User::OFF_LINE;
	user._last_active_time = time(NULL);
	_online_user.AddUser(userid, user);

	return true;
}

bool WasClient::HandleMsgData(const string userid, const char *data, int len)
{
	vector<string>      fields;
	map<string, string> params;
	map<string, string>::iterator paramIte;

	map<string, vector<unsigned char> > msg808Arr;
	map<string, vector<unsigned char> >::iterator msg808Ite;

	string line(data, len);

	fields.clear();
	if(Utils::splitStr(line, fields, ' ') != 7) {
		return false;
	}

	if( ! _protocol.parseInnerParam(fields[5], params)) {
		return false;
	}

	string type = params["TYPE"];
	string stat = params["18"];

	if (type.compare("5") == 0 && stat.compare(0, 2, "0/") == 0) {
		// 如果收到离线包，删除用户对象，等下次上线再添加
		User user = _online_user.GetUserByUserId(userid);
		if (user._user_id.empty()) {
			return false;
		}

		_online_user.DeleteUser(userid);
		if(user._fd != NULL) {
			CloseSocket(user._fd);
		}

		OUT_PRINT( NULL, 0, "STATUS", "delete userid: %s" , userid.c_str() ) ;

		return true;
	}

	User user = _online_user.GetUserByUserId(userid);
	if(user._user_id.empty()) {
		OUT_WARNING(NULL, 0, "STATUS", "userid: %s now active", userid.c_str());
		return addActiveUser(userid, fields[2]);
	}

	if ( user._fd == NULL || user._user_state != User::ON_LINE) {
		OUT_PRINT( user._ip.c_str(), user._port, user._user_name.c_str(), "%s no ready" , user._user_id.c_str() ) ;
		return false;
	}

	if(fields[0] == "CAITR") {
		string replyRet;
		InnerWaitReply innerWaitReply;

		if( ! _innerWaitReply.erase(fields[1], innerWaitReply)) {
			return false;
		}

		replyRet = params["RET"];
		if(replyRet.empty()) {
			return false;
		}

		msg808Arr.clear();
		if (_protocol.buildGb808GeneralRsp(innerWaitReply.simpleMsg, msg808Arr, atoi(replyRet.c_str()))) {
			sendDataToUser(user, msg808Arr);
		}

		return true;
	}

	if(fields[0] != "CAITS" || fields[4] != "U_REPT") {
		return false;
	}

	string key;

	Gb808WaitReply gb808WaitReply;
	MultiMediaData multiMediaData;

	switch(atoi(type.c_str())) {
	case 0:
	case 1:
		_protocol.buildGb808Location(user._user_name, params, msg808Arr);
		break;
	case 3:
		if( ! _protocol.buildGb808MultiMediaData(user._user_name, params, msg808Arr, multiMediaData)) {
			return false;
		}
		_multiMediaData.insert(user._user_name + "_" + params["120"], multiMediaData);
		break;
	case 39:
		_protocol.buildGb808MultiMediaEvent(user._user_name, params, msg808Arr);
		break;
	}

	for(msg808Ite = msg808Arr.begin(); msg808Ite != msg808Arr.end(); ++msg808Ite) {
		vector<unsigned char> &msg = msg808Ite->second;

		OUT_HEX( user._ip.c_str(), user._port, user._user_name.c_str(), (const char *)&msg[0], msg.size() ) ;
		SendData(user._fd, (char*) &msg[0], msg.size());

		gb808WaitReply.userid = user._user_id;
		gb808WaitReply.login = user._login_time;
		gb808WaitReply.number = 1;
		gb808WaitReply.msg808 = msg;
		_gb808WaitReply.insert(msg808Ite->first, gb808WaitReply);
	}

	return true ;
}

void WasClient::HandleOnlineUsers()
{
	vector<User> users = _online_user.GetOnlineUsers();

	map<string, vector<unsigned char> > msg808Arr;

	for(int i = 0; i < (int)users.size(); ++i) {
		User &user = users[i] ;

		if(user._user_name.empty()) {
			continue;
		}

		if( ! _pEnv->GetUserMgr()->chkRoute(user._user_id)) {
			// 手机号码在路由表中已删除
			_online_user.DeleteUser(user._user_id);
			CloseSocket(user._fd);

			continue;
		}

		msg808Arr.clear();
		_protocol.buildGb808HeartBeat(user._user_name, msg808Arr);

		sendDataToUser(user, msg808Arr);
	}
}

void WasClient::HandleOfflineUsers()
{
	vector<User> users = _online_user.GetOfflineUsers(3*60);

	for(int i = 0; i < (int)users.size(); i++) {
		User &user = users[i];

		if(user._user_name.empty()) {
			continue;
		}

		if( ! _pEnv->GetUserMgr()->chkRoute(user._user_id)) {
			// 手机号码在路由表中已删除
			continue;
		}

		ConnectServer(user, 3);
		_online_user.AddUser( user._user_id, user );
	}
}

void WasClient::registerTerm()
{
	set<string> userids;
	set<string>::iterator itSs;

	string valueStr;
	vector<string> fields;

	map<string, string> params;
	map<string, vector<unsigned char> > msg808Arr;
	map<string, vector<unsigned char> >::iterator msg808Ite;

	string num;
	string corp;

	string addr;
	string port;
	string flag;

	User user;

	userids = _pEnv->GetUserMgr()->getAllRoute();
	for(itSs = userids.begin(); itSs != userids.end(); ++itSs) {
		const string &userid = *itSs;

		valueStr.clear();
		_pEnv->GetRedisCache()->HGet("KCTX.DIS808.PWD", userid.c_str(), valueStr);
		if( ! valueStr.empty()) { //redis 中已存在鉴权码
			continue;
		}

		fields.clear();
		if(Utils::splitStr(userid, fields, '_') != 2) {
			continue;
		}
		num = fields[0];   //终端手机号码
		corp = fields[1];  //对接平台编码

		valueStr = _pEnv->GetUserMgr()->getCorpInfo(corp);
		fields.clear();
		if(Utils::splitStr(valueStr, fields, ':') != 3) {
			continue;
		}
		addr = fields[0];  //对接平台地址
		port = fields[1];  //对接平台端口
		flag = fields[2];  //注册鉴权标识

		if(flag != "1") { //对接平台不需要注册鉴权。
			continue;
		}

		valueStr.clear();
		_pEnv->GetRedisCache()->HGet("KCTX.SECURE", num.c_str(), valueStr);
		if(valueStr.empty()) { //redis 中不存在当前终端配置数据
			continue;
		}

		fields.clear();
		if(Utils::splitStr(valueStr, fields, ',') != 10) {//redis 中不存在当前终端配置数据
			continue;
		}

		params.clear();
		params["44"]  = fields[2];  //终端id
		params["202"] = fields[3];  //车牌颜色
		params["104"] = fields[4];  //车牌号码

		msg808Arr.clear();
		if( ! _protocol.buildGb808Register(num, params, msg808Arr)) {
			continue;
		}

		user._user_id = userid;
		user._ip = addr;
		user._port = atoi(port.c_str());
		user._fd = _tcp_handle.connect_nonb(user._ip.c_str(), user._port, 3);
		if(user._fd == NULL) { //连接对接平台前置机失败。
			continue;
		}
		user._last_active_time = time(NULL);

		_online_user.AddUser(user._user_id, user);

		for(msg808Ite = msg808Arr.begin(); msg808Ite != msg808Arr.end(); ++msg808Ite) {
			vector<unsigned char> &msg = msg808Ite->second;

			OUT_HEX( user._ip.c_str(), user._port, user._user_id.c_str(), (const char *)&msg[0], msg.size() ) ;
			SendData(user._fd, (char*) &msg[0], msg.size());
		}
	}
}
