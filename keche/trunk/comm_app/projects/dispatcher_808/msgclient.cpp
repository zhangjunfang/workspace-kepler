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
	return StartClient(_client_user._ip.c_str(), _client_user._port, 1);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	const char *ptr = (const char *) data;

	if (len < 4)
	{
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len );
		return;
	}

	//OUT_RECV( sock->_szIp, sock->_port,  "RECV", "%.*s", len, ptr);

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
		if(_client_user._user_state != User::ON_LINE || time(NULL) - _client_user._last_active_time > 60) {
			_client_user._user_state = User::OFF_LINE;
			if(ConnectServer(_client_user, 3)) {
				//_client_user._user_state = User::ON_LINE;
			}
		} else {
			SendData(_client_user._fd, "NOOP \r\n", 7);
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
	vector<string>      fields;
	vector<string>      fields1;

	string line(data, len);

	if (Utils::splitStr(line, fields, ' ') != 7) {
		return;
	}

	if(Utils::splitStr(fields[2], fields1, '_') != 2) {
		return; // macid格式不对
	}

	set<string> corps;
	set<string>::iterator it;
	string userid;

	corps = _pEnv->GetUserMgr()->getRoute(fields1[1]);
	if( ! corps.empty()) {
		OUT_RECV( sock->_szIp, sock->_port,  "RECV", "%.*s", len, data);
	}

	for(it = corps.begin(); it != corps.end(); ++it) {
		userid = fields1[1] + "_" + *it;
		_pEnv->GetWasClient()->HandleMsgData(userid, data, len);
	}
}

bool MsgClient::HandleMsgData(const char *data, int len)
{
	if(_client_user._fd == NULL || _client_user._user_state != User::ON_LINE) {
		const char *ip = _client_user._ip.c_str();
		unsigned short port = _client_user._port;
		OUT_WARNING( ip, port, "offline", "HandleMsgData: %.*s", len, data);
		return false;
	}

	return SendData(_client_user._fd, data, len);
}
