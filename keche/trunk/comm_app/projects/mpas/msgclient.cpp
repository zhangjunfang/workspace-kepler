#include "msgclient.h"
#include <tools.h>
#include <netutil.h>
#include <intercoder.h>

MsgClient::MsgClient( PConvert *convert ) : _convert( convert )
{
	_pMsgClient = NULL;
	_last_handle_user_time = time(NULL);
}

MsgClient::~MsgClient(void)
{
	Stop();
}

// 添加用户处理
void MsgClient::AddUser(const char *ip, unsigned short port, const char *username, const char *pwd)
{
	char szid[256] = {0};
	sprintf(szid, "%lu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if (!user._user_id.empty())
		return;

	user._user_id = szid;
	user._ip = ip;
	user._port = port;
	user._user_name = username;
	user._user_pwd = pwd;
	user._user_type = "UPIPE";
	user._user_state = User::OFF_LINE;
	user._socket_type = User::TcpConnClient;
	user._connect_info.keep_alive = AlwaysReConn;
	user._connect_info.timeval = 30;

	// 添加到用户队列中
	_online_user.AddUser(user._user_id, user);
}

// 处理删除服务
void MsgClient::DelUser(const char *ip, unsigned short port)
{
	char szid[256] = {0};
	sprintf(szid, "%lu", netutil::strToAddr(ip, port));

	User user = _online_user.GetUserByUserId(szid);
	if (user._user_id.empty())
		return;

	CloseSocket(user._fd);

	_online_user.DeleteUser(szid);
}

bool MsgClient::Init(ISystemEnv *pEnv)
{
	_pEnv = pEnv;

	setpackspliter(&_packspliter);

	return true;
}

void MsgClient::Stop(void)
{
	OUT_INFO("Msg", 0, "MsgClient", "stop");

	StopClient();
}

bool MsgClient::Start(void)
{
	return StartClient("0.0.0.0", 0, 1);
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	if (len < 4){
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len );
		return;
	}

	const char *ip 		= sock->_szIp ;
	unsigned short port = sock->_port ;

	OUT_RECV( ip, port, NULL, "fd %d, on_data_arrived:[%d]%s", sock->_fd, len, (const char*)data);

	CInterCoder coder;
	// 数据解密处理
	if (!coder.Decode((const char *) data, len)) {
		OUT_ERROR( ip, port, "Decode", "fd %d, MsgClient recv error data:%d,%s", sock->_fd , len, ( const char *)data );
		return;
	}

	const char *ptr = (const char *) coder.Buffer();
	if (strncmp(ptr, "CAIT", 4) == 0)
	{
		// 纷发处理数据
		HandleInnerData( sock , ptr, coder.Length());
	}
	else
	{
		// 处理登陆相关
		HandleSession( sock , ptr, coder.Length());
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	User user = _online_user.GetUserBySocket(sock);
	OUT_WARNING( user._ip.c_str(), user._port, user._user_id.c_str(), "Disconnection");
	user._user_state = User::OFF_LINE;
	_online_user.SetUser(user._user_id, user);
}

void MsgClient::TimeWork()
{
	/*
	 * 1.将超时的连接去掉；
	 * 2.定时发送NOOP消息
	 * 3.Reload配置文件中的新的连接。
	 * 4.
	 */
	while (1)
	{
		if (!Check())
			break;

		HandleOfflineUsers();
		HandleOnlineUsers(30);

		// 处理超时的对象
		// _session.CheckTimeOut(120);
		//这个地方，超时的同步有问题
		sleep(5);
	}
}

void MsgClient::NoopWork()
{

}

// 向MSG上传消息
bool MsgClient::Deliver( const char *data, int len )
{
	// 如果没有开启节点服务就直接发送了
	vector<User> vec = _online_user.GetOnlineUsers() ;
	if ( vec.empty() ) {
		OUT_ERROR( NULL, 0, "msg", "HandleUpMsgData %s", data ) ;
		return false ;
	}

	int send = 0 ;
	for ( int i = 0; i < (int) vec.size(); ++ i ) {
		User &user = vec[i] ;
		if ( user._fd == NULL )
			continue ;

		if ( SendData( user._fd, data, len ) ) {
			++ send ;
		}
	}

	if ( send > 0 ) {
		OUT_PRINT( NULL , 0, "msg", "HandleUpMsgData %s", data );
	}

	return true ;
}

int MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	sprintf(buf, "LOGI UPIPE %s %s \r\n", user._user_name.c_str(), user._user_pwd.c_str());
	return (int) strlen(buf);
}

void MsgClient::HandleSession( socket_t *sock, const char *data, int len)
{
	const char *ip 		= sock->_szIp ;
	unsigned short port = sock->_port ;

	char *ptr = strstr( data, " " ) ;
	if ( ptr == NULL ) {
		OUT_ERROR( ip, port, NULL, "split session data failed" ) ;
		return ;
	}

	// 如果为登陆消息
	if ( strncmp( data, "LACK", 4 ) == 0 )
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
		 */// LACK 0 0 0 %d \r\n
		int ret = atoi(ptr+1);
		switch (ret)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			{
				User user = _online_user.GetUserBySocket(sock);
				if (user._user_id.empty())
				{
					OUT_WARNING(ip, port, NULL, "Can't find the syn_user");
					return;
				}
				user._user_state = User::ON_LINE;
				user._last_active_time = time(NULL);
				// 重新处理用户状态
				_online_user.SetUser(user._user_id, user);

				OUT_CONN( ip , port, user._user_name.c_str(), "Login success, fd %d access code %d" , sock->_fd, user._access_code );
			}
			break;
		case -1:
			{
				OUT_ERROR(ip, port, NULL, "LACK,password error!");
			}
			break;
		case -2:
			{
				OUT_ERROR(ip, port, NULL, "LACK,the user has already login!");
			}
			break;
		case -3:
			{
				OUT_ERROR(ip, port, NULL, "LACK,user name is invalid!");
			}
			break;
		default:
			{
				OUT_ERROR( ip, port, NULL, "unknow result" );
			}
			break;
		}

		// 如果返回错误则直接处理
		if (ret < 0)
		{
			_tcp_handle.close_socket(sock);
		}
	}
	else if ( strncmp( data, "NOOP_ACK" , 8 ) == 0 )
	{
		User user = _online_user.GetUserBySocket(sock);
		user._last_active_time = time(NULL);
		_online_user.SetUser(user._user_id, user);

		OUT_INFO( ip, port, user._user_name.c_str(), "NOOP_ACK");
	}
	else
	{
		OUT_WARNING( ip, port, NULL, "except message:%s", (const char*)data);
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len)
{
	const char *ip 		= sock->_szIp;
	unsigned short port = sock->_port;

	User user = _online_user.GetUserBySocket( sock ) ;
	if ( user._user_id.empty() ) {
		OUT_ERROR( ip, port, NULL, "get fd %d user failed", sock->_fd ) ;
		return ;
	}

	// CAITS 0_0 MACID 0 U_REPT {TYPE:5,18:上下线状态/前置机服务器ID/管道控制服务ID/消息服务器id}
	string line(data, len);
	vector<string> vec;
	if ( !splitvector( line, vec, " ", 6 ) ) {
		OUT_ERROR( ip, port, user._user_name.c_str() , "fd %d data error: %s", sock->_fd, data );
		return;
	}

	string macid = vec[2] ;
	if ( macid.empty() ){
		OUT_ERROR( ip, port, user._user_name.c_str(), "fd %d macid empty", sock->_fd ) ;
		return ;
	}

	string vechile ;
	if ( ! _macid2carnum.GetSession( macid, vechile ) ) {
		OUT_ERROR( ip, port, user._user_name.c_str(), "fd %d get vechile num by mac id %s failed", macid.c_str() ) ;
		return ;
	}

	string acode ;
	DataBuffer buf ;
	// 转换控制指令
	if (vec[4] == "D_CTLM") {
		// 转换控制指令
		_convert->convert_ctrl( vec[1], macid , vec[5] , vechile, buf , acode ) ;
	} else if ( vec[4] == "D_SNDM" ) {
        OUT_INFO( ip, port, user._user_name.c_str(), "D_SNDM"); //xifengming
		_convert->convert_sndm( vec[1] , macid, vec[5], vechile, buf, acode ) ;
	}
	if ( ! acode.empty() && buf.getLength() > 0 ) {
		// 下发给PAS对应的接入商
		_pEnv->GetPasServer()->HandlePasDown( acode.c_str(), buf.getBuffer(), buf.getLength() ) ;
	}

	user._last_active_time = time(NULL);
	_online_user.SetUser(user._user_id, user);
}

void MsgClient::HandleOfflineUsers()
{
	vector<User> vec_users = _online_user.GetOfflineUsers(3 * 60);
	for (int i = 0; i < (int) vec_users.size(); i++)
	{
		User &user = vec_users[i];
		if (user._socket_type == User::TcpClient)
		{
			if ( user._fd != NULL )
			{
				OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(), "fd %d close socket", user._fd->_fd );
				CloseSocket(user._fd);
			}
		}
		else if (user._socket_type == User::TcpConnClient)
		{
			if ( user._fd != NULL )
			{
				OUT_WARNING( user._ip.c_str(), user._port, user._user_name.c_str(), "fd %d close socket", user._fd->_fd );
				user.show();
				CloseSocket(user._fd);
				user._fd = NULL;
			}
			if (ConnectServer(user, 10))
			{
				// 添加列表中。
				_online_user.AddUser(user._user_id, user);
			}
			else if (user._connect_info.keep_alive == AlwaysReConn)
			{
				// 添加用户
				_online_user.AddUser(user._user_id, user);
			}
		}
	}
}

void MsgClient::HandleOnlineUsers(int timeval)
{
	time_t now = time(NULL);
	if (now - _last_handle_user_time < timeval)
	{
		return;
	}
	_last_handle_user_time = now;

	vector<User> vec_users = _online_user.GetOnlineUsers();
	for (int i = 0; i < (int) vec_users.size(); i++)
	{
		User &user = vec_users[i];
		if (user._socket_type == User::TcpConnClient)
		{
			string loop = "NOOP \r\n";
			SendData(user._fd, loop.c_str(), loop.length());
			OUT_SEND(vec_users[i]._ip.c_str(), vec_users[i]._port, vec_users[i]._user_id.c_str(),
					"NOOP");
		}
	}
}

// 添加手机MAC
void MsgClient::AddMac2Car( const char *macid, const char *vechile )
{
	_macid2carnum.AddSession( macid, vechile ) ;
}

