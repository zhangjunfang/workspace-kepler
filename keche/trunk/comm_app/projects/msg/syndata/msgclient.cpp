#include "msgclient.h"
#include <tools.h>
#include <intercoder.h>
#include "picclient.h"

#define MSG_BACK_ID  "syndata_msg"

MsgClient::MsgClient():
	_filecache(this)
{
	_enable = false ;
	_picclient = new CPicClient;
}

MsgClient::~MsgClient( void )
{
	Stop() ;
	if ( _picclient != NULL ) {
		delete _picclient ;
		_picclient = NULL ;
	}
}

bool MsgClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	int nvalue = 0 ;
	if ( pEnv->GetInteger( "syn_data", nvalue ) ) {
		_enable = ( nvalue == 1 ) ;
	}
	if ( ! _enable ) return true ;

	char ip[128] = {0} ;
	// 同步中心服务器的IP
	if ( !pEnv->GetString( "syn_server", ip ) ) {
		printf( "remote syn_server center ip failed" ) ;
		OUT_ERROR( NULL, 0, NULL,"remote syn center ip failed" ) ;
		return false ;
	}
	// 同步中心服务器的端口
	if ( ! pEnv->GetInteger( "syn_port", nvalue ) ) {
		printf( "remote syn_port center port failed" ) ;
		OUT_ERROR( NULL, 0, NULL,"remote syn center port failed" ) ;
		return false ;
	}

	// 初始化图片服务对象
	if ( ! _picclient->Init( pEnv ) ) {
		OUT_ERROR( NULL, 0, NULL, "init pic client failed" ) ;
	}

	User user ;
	user._fd        			  = NULL ;
	user._ip					  = ip ;
	user._port					  = nvalue ;

	char szval[128] = {0} ;
	if ( ! pEnv->GetString( "syn_user", szval ) ) {
		printf( "remote syn_user failed\n" ) ;
		OUT_ERROR( NULL, 0, NULL, "remote syn user failed" ) ;
		return false ;
	}

	user._user_id   			  = szval ;
	user._user_name 		 	  = szval ;

	if ( ! pEnv->GetString( "syn_pwd", szval ) ) {
		printf( "remote syn_pwd failed\n" ) ;
		OUT_ERROR( NULL, 0, NULL, "remote syn_pwd failed" ) ;
		return false ;
	}

	user._user_pwd				  = szval ;
	user._user_type				  = "PIPE" ;
	user._user_state 			  = User::OFF_LINE ;
	user._socket_type			  = User::TcpConnClient ;
	user._connect_info.keep_alive = AlwaysReConn ;
	user._connect_info.timeval	  = 30 ;

	_online_user.AddUser( user._user_id, user ) ;

	setpackspliter( &_packspliter ) ;

	char szbuf[512] = {0};
	if ( ! pEnv->GetString( "base_filedir" , szbuf ) ) {
		printf( "load base_filedir failed\n" ) ;
		return false ;
	}

	nvalue = 0 ;
	pEnv->GetInteger( "sendcache_speed", nvalue ) ;

	char temp[1024] = {0} ;
	sprintf( temp, "%s/msgdata", szbuf ) ;

	return _filecache.Init( temp, nvalue ) ;
}


void MsgClient::Stop( void )
{
	if ( ! _enable ) return ;

	OUT_INFO("Msg",0,"MsgClient","stop");

	StopClient() ;

	_picclient->Stop() ;
}

bool MsgClient::Start( void )
{
	if ( ! _enable ) return true ;

	_picclient->Start() ;

	return StartClient( "0.0.0.0", 0, 3 ) ;
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
//	FUNTRACE("void ClientAccessServer::HandleInterData(int fd, const void *data, int len)");
	if ( len < 4 ) {
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len ) ;
		return ;
	}

    // 解密处理数据
	CInterCoder coder;
	coder.Decode( (const char *)data, len );

	OUT_RECV( sock->_szIp, sock->_port, NULL, "fd %d,on_data_arrived:[%d]%s", sock->_fd, coder.Length(), coder.Buffer() );

	const char *ptr = ( const char *) coder.Buffer() ;
	if ( strncmp( ptr, "CAIT" , 4 ) == 0  ) {
		// 纷发处理数据
		HandleInnerData( sock, ptr, coder.Length() ) ;
	} else {
		// 处理登陆相关
		HandleSession( sock, ptr, coder.Length() ) ;
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	User user = _online_user.GetUserBySocket(sock);
	OUT_WARNING( user._ip.c_str() , user._port, user._user_id.c_str(), "Disconnection fd %d" , sock->_fd );
	user._user_state = User::OFF_LINE ;
	_online_user.SetUser( user._user_id, user ) ;
}

void MsgClient::TimeWork()
{
	/*
	 * 1.将超时的连接去掉；
	 * 2.定时发送NOOP消息
	 * 3.Reload配置文件中的新的连接。
	 * 4.
	 */
	while(1){

		if ( ! Check() ) break ;
		HandleOfflineUsers() ;
		HandleOnlineUsers( 30 ) ;

		//这个地方，超时的同步有问题
		sleep(5);
	}
}

void MsgClient::NoopWork()
{
	while(1){
		if ( ! Check() ) break ;
		// 检测是否有缓存数据要写出
		if ( ! _filecache.Check() ) {
			sleep(1) ;
		}
	}
}

// 构建登陆信息数据
int  MsgClient::build_login_msg(User &user, char *buf, int buf_len)
{
	char szbuf[1024] = {0} ;
	sprintf( szbuf, "LOGI %s %s %s \r\n",
			user._user_type.c_str(), user._user_name.c_str(), user._user_pwd.c_str() ) ;

	// 加密处理数据
	CInterCoder coder;
	coder.Encode( szbuf, strlen(szbuf) );

	memcpy( buf, coder.Buffer(), coder.Length() ) ;

	return coder.Length() ;
}

// 纷发登陆用户会话数据
void MsgClient::HandleSession( socket_t *sock, const char *data, int len )
{
	string line = data;

	vector<string> vec_temp ;
	if ( ! splitvector( line, vec_temp, " " , 1 ) ) {
		return ;
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
		int ret = atoi( vec_temp[1].c_str() ) ;
		switch( ret )
		{
		case 0:
		case 1:
		case 2:
		case 3:
			{
				User user = _online_user.GetUserBySocket( sock ) ;
				if( user._user_id.empty() )
				{
					OUT_WARNING( sock->_szIp, sock->_port, NULL,"Can't find the syn_user");
					return;
				}
				user._user_state 		= User::ON_LINE ;
				user._last_active_time  = time(NULL) ;
				// 重新处理用户状态
				_online_user.SetUser( user._user_id, user ) ;

				OUT_CONN( sock->_szIp, sock->_port, user._user_name.c_str(), "Login success, fd %d access code %d" , sock->_fd, user._access_code ) ;
				// 用户登陆设置为在线状态
				_filecache.Online( MSG_BACK_ID ) ;
			}
			break ;
		case -1:
			{
				OUT_ERROR( sock->_szIp, sock->_port, NULL , "LACK,password error!");
			}
			break ;
		case -2:
			{
				OUT_ERROR( sock->_szIp, sock->_port, NULL ,"LACK,the user has already login!");
			}
			break ;
		case -3:
			{
				OUT_ERROR( sock->_szIp, sock->_port, NULL, "LACK,user name is invalid!");
			}
			break ;
		default:
			{
				OUT_ERROR( sock->_szIp, sock->_port, NULL,  "unknow result" ) ;
			}
			break;
		}

		// 如果返回错误则直接处理
		if ( ret < 0 )
		{
			_tcp_handle.close_socket(sock);
		}
	}
	else if (head == "NOOP_ACK")
	{
		User user = _online_user.GetUserBySocket( sock ) ;
		user._last_active_time  = time(NULL) ;
		_online_user.SetUser( user._user_id, user ) ;

		OUT_INFO( sock->_szIp, sock->_port, user._user_name.c_str() , "NOOP_ACK");
	}
	else
	{
		OUT_WARNING( sock->_szIp, sock->_port, NULL, "except message:%s", (const char*)data );
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len )
{
	User user = _online_user.GetUserBySocket( sock ) ;
	if ( user._user_id.empty()  ) {
		OUT_ERROR( sock->_szIp, sock->_port, "CAIS" , "find fd %d user failed, data %s", sock->_fd, data ) ;
		return ;
	}

	// ToDo: 下发的MSG
	_pEnv->GetMsgClientServer()->DeliverEx( NULL, data, len ) ;

	user._last_active_time = time(NULL) ;
	_online_user.SetUser( user._user_id, user ) ;
}

// 补发数据到MSG
void MsgClient::HandleData( const char *data, int len , bool pic )
{
	if ( ! _enable ) return ;

	// 如果为图片直接添加图片处理对象队列中
	if ( pic ) {
		_picclient->AddMedia( data, len ) ;
		return ;
	}
	// 加密处理数据
	CInterCoder coder;
	coder.Encode( data, len );

	vector<User> vec = _online_user.GetOnlineUsers() ;
	if ( vec.empty() ) {
		_filecache.WriteCache( MSG_BACK_ID, (void*)coder.Buffer(), coder.Length() ) ;
		return ;
	}

	bool send = false ;
	int nsize = vec.size();
	for ( int i = 0; i < nsize; ++ i ) {
		// 对数据进解密处理
		if ( ! SendData( vec[i]._fd, coder.Buffer(), coder.Length() ) ) {
			continue ;
		}
		send = true ;
	}
	if ( ! send ) {
		_filecache.WriteCache( MSG_BACK_ID, (void*)coder.Buffer(), coder.Length() ) ;
	}
}

void MsgClient::HandleOfflineUsers()
{
	vector<User> vec_users = _online_user.GetOfflineUsers(3*60);
	for(int i = 0; i < (int)vec_users.size(); i++)
	{
		User &user = vec_users[i];
		if(user._socket_type == User::TcpClient)
		{
			if( user._fd != NULL ){
				OUT_WARNING( user._ip.c_str() , user._port , user._user_name.c_str() , "HandleOffline Users close socket fd %d", user._fd->_fd );
				CloseSocket(user._fd);
			}
		}
		else if(user._socket_type == User::TcpConnClient)
		{
			if(user._fd != NULL)
			{
				OUT_WARNING( user._ip.c_str() , user._port , user._user_name.c_str() ,"TcpConnClient close socket fd %d", user._fd->_fd );
				user.show();
				CloseSocket(user._fd);
				user._fd = NULL;
			}
			if ( ConnectServer(user, 10) ) {
				// 添加列表中。
				_online_user.AddUser( user._user_id, user ) ;
			} else if ( user._connect_info.keep_alive == AlwaysReConn ) {
				// 添加用户
				_online_user.AddUser( user._user_id, user ) ;
			}
		}
	}
}

void MsgClient::HandleOnlineUsers(int timeval)
{
	time_t now = time(NULL) ;

	static time_t last_handle_user_time = 0;
	if( now - last_handle_user_time < timeval){
		return;
	}
	last_handle_user_time = now ;

	vector<User> vec_users = _online_user.GetOnlineUsers();
	if ( vec_users.size() == 0 )
		return ;

	char szbuf[128] = {"NOOP \r\n"} ;
	// 加密处理数据
	CInterCoder coder;
	coder.Encode( szbuf, strlen(szbuf) );

	int nsize = vec_users.size() ;
	for(int i = 0; i < nsize; ++ i ){
		User &user = vec_users[i] ;
		if( user._socket_type == User::TcpConnClient && user._fd != NULL )
		{
			SendData( user._fd, coder.Buffer(), coder.Length() ) ;
			OUT_SEND(vec_users[i]._ip.c_str(),vec_users[i]._port,vec_users[i]._user_id.c_str(),"NOOP");
		}
	}
	// 用户登陆设置为在线状态
	_filecache.Online( MSG_BACK_ID ) ;
}

// 缓存数据回调接口
int MsgClient::HandleQueue( const char *sid, void *buf, int len , int msgid )
{
	// 先判断一下是否有在线用户
	vector< User > vec = _online_user.GetOnlineUsers();
	if ( vec.empty() ) {
		return IOHANDLE_FAILED;
	}

	// 根据路由来进行轮转发送
	int nsize = vec.size();
	for ( int i = 0; i < nsize; ++ i ) {
		// 对数据进解密处理
		SendData( vec[i]._fd, (const char *)buf, len ) ;
	}
	return IOHANDLE_SUCCESS;
}
