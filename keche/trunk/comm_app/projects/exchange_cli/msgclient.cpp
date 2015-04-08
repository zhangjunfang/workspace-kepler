#include "msgclient.h"
#include <tools.h>
#include <netutil.h>
#include <intercoder.h>
#include <Base64.h>

#include "../tools/utils.h"

MsgClient::MsgClient(void)
{
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

	setpackspliter(&_packspliter);

	_client_user._fd = NULL;
	_client_user._user_state = User::OFF_LINE;
	_client_user._last_active_time = time(NULL);

	return true;
}

void MsgClient::Stop(void)
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void)
{
	return StartClient(_client_user._ip.c_str(), _client_user._port, 1);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	const char *ptr = (const char *) data;

	if (len < 4) {
		OUT_ERROR(sock->_szIp, sock->_port, NULL, "recv data error length: %d", len);
		return;
	}

	//OUT_RECV( sock->_szIp, sock->_port,  "RECV", "%.*s", len, ptr);

	if (strncmp(ptr, "CAIT", 4) == 0) {
		// 纷发处理数据
		HandleInnerData(sock, ptr, len - 2);
	} else {
		// 处理登陆相关
		HandleSession(sock, ptr, len - 2);
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	_client_user._user_state = User::OFF_LINE;
}

void MsgClient::TimeWork()
{
	while (Check()) {
		time_t now = time(NULL);
		const char *ip = _client_user._ip.c_str();
		unsigned int port = _client_user._port;

		User &user = _client_user;
		if (user._fd != NULL && user._user_state == User::ON_LINE && now - user._last_active_time < 180) {
			SendData(_client_user._fd, "NOOP \r\n", 7);
		} else if(!ConnectServer(_client_user, 10)) {
			OUT_INFO(ip, port, NULL, "connect server failure");
		}

		sleep(30);
	}
}

void MsgClient::NoopWork()
{
}

int MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	sprintf(buf, "LOGI SAVE %s %s \r\n", user._user_name.c_str(), user._user_pwd.c_str());
	return (int) strlen(buf);
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len)
{
	string line(data, len);

	vector<string> vec_temp;
	if (!splitvector(line, vec_temp, " ", 1))
	{
		return;
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
		int ret = atoi(vec_temp[1].c_str());
		switch (ret) {
		case 0:
		case 1:
		case 2:
		case 3:
			_client_user._user_state = User::ON_LINE;
			_client_user._last_active_time = time(NULL);
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
			OUT_ERROR(sock->_szIp, sock->_port, NULL, "unknow result");
			break;
		}

		// 如果返回错误则直接处理
		if (ret < 0) {
			_tcp_handle.close_socket(sock);
		}
	}
	else if (head == "NOOP_ACK") {
		_client_user._last_active_time = time(NULL);
		OUT_INFO( sock->_szIp, sock->_port, _client_user._user_name.c_str(), "NOOP_ACK");
	} else {
		OUT_WARNING( sock->_szIp, sock->_port, "except", "%.*s", len, (const char*)data);
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len)
{
	vector<string> fields;

	string seqid;
	string macid;
	string msgid;
	string detail;
	string oemcode;
	string phoneid;
	map<string, string> params;

	string line(data, len);

	fields.clear();
	if (Utils::splitStr(line, fields, ' ') != 7) {
		return; //内部协议格式不对
	}
	seqid = fields[1];
	macid = fields[2];
	msgid = fields[4];
	detail = fields[5];

	fields.clear();
	if(Utils::splitStr(macid, fields, '_') != 2) {
		return; //macid格式不对
	}
	oemcode = fields[0];
	phoneid = fields[1];


	if( ! _pEnv->GetProtParse()->parseInnerParam(detail, params)) {
		return; //参数解析失败
	}

	string type = params["TYPE"];
	vector<unsigned char> resbuf;

	//OUT_RECV( sock->_szIp, sock->_port,  "RECV", "%.*s", len, data);
	uint32_t exchSeqid;

	if(msgid == "U_REPT") {
		if(type == "0") {
			exchSeqid = _pEnv->GetProtParse()->buildExchLocation(phoneid, params, resbuf);
		} else if(type == "3") {
			exchSeqid = _pEnv->GetProtParse()->buildExchMMData(phoneid, params, resbuf);
		} else if(type == "5") {
			exchSeqid = _pEnv->GetProtParse()->buildExchStatus(phoneid, params, resbuf);
		} else if(type == "7") {
			exchSeqid = _pEnv->GetProtParse()->buildExchHistory(phoneid, params, resbuf);
		} else if(type == "9") {
			exchSeqid = _pEnv->GetProtParse()->buildExchTTData(phoneid, params, resbuf);
		} else if(type == "36") {
			exchSeqid = _pEnv->GetProtParse()->buildExchTermReg(phoneid, params, resbuf);
		} else if(type == "38") {
			exchSeqid = _pEnv->GetProtParse()->buildExchTermAuth(phoneid, params, resbuf);
		} else if(type == "39") {
			exchSeqid = _pEnv->GetProtParse()->buildExchMMEvent(phoneid, params, resbuf);
		} else if(type == "52") {
			exchSeqid = _pEnv->GetProtParse()->buildExchDriverEvent(phoneid, params, resbuf);
		} else if(type == "53") {
			exchSeqid = _pEnv->GetProtParse()->buildExchTermVersion(phoneid, params, resbuf);
		}
	}

	if(resbuf.empty()) {
		return; //消息被过滤
	}

	OUT_RECV( sock->_szIp, sock->_port,  "RECV", "%.*s", len, data);
	_pEnv->GetExchClient()->HandleData(exchSeqid, (char*)&resbuf[0], resbuf.size());
}

bool MsgClient::HandleData(const char *data, int len)
{
	if(_client_user._fd == NULL || _client_user._user_state != User::ON_LINE) {
		const char *ip = _client_user._ip.c_str();
		unsigned short port = _client_user._port;
		OUT_WARNING( ip, port, "offline", "%.*s", len, data);
		return false;
	}

	return SendData(_client_user._fd, data, len);
}
