#include "msgclient.h"
#include "pccutil.h"
#include "pconvert.h"
#include <tools.h>

MsgClient::MsgClient( PConvert *convert )
	: _convert(convert)
{
	_last_handle_user_time = time(NULL) ;
}

MsgClient::~MsgClient( void )
{
	Stop() ;
}

// 加载MSG的用户文件
bool MsgClient::LoadMsgUser( const char *userfile )
{
	if ( userfile == NULL ) return false ;

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen( userfile, "r" );
	if (fp == NULL){
		OUT_ERROR( NULL, 0, NULL, "Load msg user file %s failed", userfile ) ;
		return false;
	}

	int count = 0 ;
	while (fgets(buf, sizeof(buf), fp)){
		unsigned int i = 0;
		while (i < sizeof(buf)){
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for (int i = 0, j = 0; i < (int)strlen(buf); ++ i ){
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n'){
				temp[j++] = buf[i];
			}
		}

		string line = temp;

		//1:10.1.99.115:8880:user_name:user_password:A3
		vector<string> vec_line ;
		if ( ! splitvector( line, vec_line, ":" , 7 ) ){
			continue ;
		}

		if ( vec_line[0] != MSG_USER_TAG ) {
			continue ;
		}

		User user ;
		user._user_id     =  vec_line[0] + vec_line[1] ;
		user._access_code = atoi( vec_line[1].c_str() ) ;
		user._ip          =  vec_line[2] ;
		user._port        =  atoi( vec_line[3].c_str() ) ;
		user._user_name   =  vec_line[4] ;
		user._user_pwd    =  vec_line[5] ;
		user._user_type   =  vec_line[6] ;
		user._user_state  = User::OFF_LINE ;
		user._socket_type = User::TcpConnClient ;
		user._connect_info.keep_alive = AlwaysReConn ;
		user._connect_info.timeval    = 30 ;

		// 添加到用户队列中
		_online_user.AddUser( user._user_id, user ) ;

		++ count ;
	}
	fclose(fp);
	fp = NULL;

	OUT_INFO( NULL, 0, NULL, "load msg user success %s, count %d" , userfile , count ) ;
	printf( "load msg user count %d success %s\n" , count, userfile ) ;

	return true ;
}

bool MsgClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	// 初始化转换环境对象
	_convert->initenv( pEnv ) ;

	char temp[256] = {0} ;
	// HTTP的URL的基地址的路径
	if ( pEnv->GetString( "base_picurl", temp ) ) {
		_picUrl = temp ;
	}

	// 加载基本订阅列表路径
	if ( pEnv->GetString( "base_dmddir" ,temp ) ) {
		_dmddir.SetString( temp ) ;
		OUT_INFO( NULL, 0 , NULL, "Load base dmddir %s", temp ) ;
	}

	if ( ! pEnv->GetString( "user_filepath" , temp ) ) {
		printf( "load user file failed\n" ) ;
		return false ;
	}

	int nvalue = 0 , send_thread = 1, recv_thread = 1, queue_size = 1000 ;
	// 发送线程
	if ( pEnv->GetInteger( "http_send_thread" , nvalue ) ) {
		send_thread = nvalue ;
	}
	// 接收线程
	if ( pEnv->GetInteger( "http_recv_thread" , nvalue ) ) {
		recv_thread = nvalue ;
	}
	// HTTP请求最大的队列长度
	if ( pEnv->GetInteger( "http_queue_size" , nvalue ) ) {
		queue_size = nvalue ;
	}

	// 初始化HTTP的服务
	if ( ! _httpcaller.Init( send_thread, recv_thread, queue_size ) ) {
		OUT_ERROR( NULL, 0, NULL, "init http caller failed" ) ;
		return false ;
	}
	_httpcaller.SetReponse( this ) ;

	// 设置分包对象
	setpackspliter( &_packspliter ) ;

	return LoadMsgUser( temp ) ;
}


void MsgClient::Stop( void )
{
	OUT_INFO("Msg",0,"MsgClient","stop");

	StopClient() ;

	_httpcaller.Stop() ;
}

bool MsgClient::Start( void )
{
	if ( ! _httpcaller.Start() ) {
		OUT_ERROR( NULL, 0, NULL, "start http caller failed" ) ;
		return false ;
	}
	return StartClient( "0.0.0.0", 0, 3 ) ;
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	if ( len < 4 ) return ;
	
	OUT_RECV3( sock->_szIp, sock->_port,  NULL, "on_data_arrived:[%d]%s", len, (const char*)data );

	const char *ptr = ( const char *)data ;
	if ( strncmp( ptr, "CAIT" , 4 ) == 0 ) {
		// 纷发处理数据
		HandleInnerData( sock, ptr, len ) ;
	} else {
		// 处理登陆相关
		HandleSession( sock, ptr, len ) ;
	}
}

void MsgClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	User user = _online_user.GetUserBySocket(sock);
	OUT_WARNING( sock->_szIp, sock->_port, user._user_id.c_str(), "Disconnection fd %d" , sock->_fd );
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
	while(1)
	{
		if ( ! Check() ) break ;

		HandleOfflineUsers() ;

		//这个地方，超时的同步有问题
		sleep(5);
	}
}

void MsgClient::NoopWork()
{
	while(1) {
		if ( !Check() ) break ;

		HandleOnlineUsers( 30 ) ;

		sleep(5) ;
	}
}

bool MsgClient::SendDataToUser( const string &area_code, const char *data, int len)
{
	if ( area_code == SEND_ALL )
	{
		vector<User>  users = _online_user.GetOnlineUsers() ;
		if ( users.empty() ) {
			return false ;
		}

		for ( size_t i = 0; i < users.size(); ++ i ) {
			// 群发数据
			SendData( users[i]._fd, data, len ) ;
		}

		return true ;
	}

	char buf[512] = {0};
	sprintf( buf, "%s%s", MSG_USER_TAG, area_code.c_str() ) ;

	User user = _online_user.GetUserByUserId( buf );
	if( user._user_id.empty() || user._user_state != User::ON_LINE )
	{
		OUT_ERROR( user._ip.c_str() , user._port , buf , "SendDataToUser %s failed" , data ) ;
		return false;
	}

	// 发送数据重新添加循环码的处理
	return SendData( user._fd, data, len ) ;
}

// 向MSG上传消息
bool MsgClient::HandleUpMsgData( const char *code, const char *data, int len )
{
	OUT_INFO( NULL, 0, "msg", "HandleUpMsgData %s", data ) ;

	if ( strstr( code, "_" ) != NULL ) {
		string userid ;
		// 取得MACID对应的接入数据
		if ( !_session.GetSession( code, userid , false ) ) {
			OUT_ERROR( NULL, 0, "Msg", "find macid %s userid failed, data: %s", code, data ) ;
			return false;
		}
		User user = _online_user.GetUserByUserId( userid ) ;
		if ( user._user_id.empty() || user._user_state != User::ON_LINE ) {
			OUT_ERROR( NULL, 0, userid.c_str() , "%s ,user not online, data: %s", code, data ) ;
			return false ;
		}
		// 发送数据
		return SendData( user._fd, data, len ) ;
	}
	// 根据MSG的码进行数据路由
	return SendDataToUser( code, data, len ) ;
}

// 加载订阅数据
void MsgClient::LoadSubscribe( User &user )
{
	if ( _dmddir.IsEmpty() )
		return ;

	char szbuf[1024] = {0};
	sprintf( szbuf, "%s/%d", _dmddir.GetBuffer(), user._access_code ) ;

	int   len = 0 ;
	char *ptr = ReadFile( szbuf, len ) ;
	if ( ptr == NULL ){
		OUT_ERROR( NULL, 0, NULL, "load subscribe file %s failed", szbuf ) ;
		return ;
	}

	// 移除掉换行回车空格等字符
	while( len > 0 ) {
		if ( ptr[len-1] == '\r' || ptr[len-1] == '\n' || ptr[len-1] == ' ' || ptr[len-1] == '\t' ){
			ptr[len-1] = 0 ;
			-- len ;
			continue ;
		}
		break ;
	}

	// DMD 0 {E005_13571198041} \r\n
	CQString sz ;
	sz.AppendBuffer( "DMD 0 {" ) ;
	sz.AppendBuffer( ptr , len ) ;
	sz.AppendBuffer( "} \r\n" ) ;
	FreeBuffer( ptr ) ;

	if ( ! SendData( user._fd, sz.GetBuffer(), sz.GetLength() ) ) {
		OUT_ERROR( NULL, 0, NULL, "Send Data : %s Failed", sz.GetBuffer() ) ;
	} else {
		OUT_PRINT( NULL, 0, NULL, "Send Sub: %s", sz.GetBuffer() ) ;
	}
}

// 构建登陆处理
int MsgClient::build_login_msg( User &user, char *buf, int buf_len )
{
	string stype = "SAVE" , sext = "\r\n" ;
	if ( user._user_type == "DMDATA" ) {
		stype = "SAVE" ; sext = "DM \r\n" ;
	} else {
		stype = user._user_type ;
	}
	sprintf( buf, "LOGI %s %s %s %s",
			stype.c_str() , user._user_name.c_str(), user._user_pwd.c_str() , sext.c_str() ) ;

	return strlen(buf) ;
}

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
					OUT_WARNING(sock->_szIp, sock->_port, NULL,"Can't find the syn_user");
					return;
				}
				user._user_state 		= User::ON_LINE ;
				user._last_active_time  = time(NULL) ;
				// 重新处理用户状态
				_online_user.SetUser( user._user_id, user ) ;

				OUT_CONN( sock->_szIp, sock->_port, user._user_name.c_str(), "Login success, fd %d access code %d" , sock->_fd, user._access_code ) ;
				// 登陆成功，如果为数据订制连接就直接需要处理发送订阅处理
				if ( user._user_type == "DMDATA" ) LoadSubscribe( user ) ;
			}
			break ;
		case -1:
			{
				OUT_ERROR( sock->_szIp, sock->_port,  NULL , "LACK,password error!");
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
				OUT_ERROR( sock->_szIp, sock->_port,  NULL,  "unknow result" ) ;
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
	user._last_active_time = time(NULL) ;
	_online_user.SetUser( user._user_id, user ) ;

	string line( data, len ) ;
	vector<string>  vec ;
	if ( ! splitvector( line, vec, " " , 6 )  ){
		OUT_ERROR( sock->_szIp, sock->_port, user._user_name.c_str() , "fd %d data error: %s", sock->_fd, data ) ;
		return ;
	}

	DataBuffer buf ;

	string head  = vec[0] ;
	string seqid = vec[1] ;
	string macid = vec[2] ;
	string code  = vec[3] ;  // 通信码，对于点名数据的区分
	string cmd   = vec[4] ;
	string val 	 = vec[5] ;

	if ( head == "CAITS" ) {
		if( cmd == "U_REPT" ){
			// 上报类消息处理
			_convert->convert_urept( macid , val , buf , ( code == "201") ) ;
		} else if( cmd == "D_CTLM" ) {
			// ToDo: 控制类消息处理

		} else if( cmd == "D_SNDM" ) {
			// ToDo : 消息发送的处理
		} else {
			OUT_WARNING( sock->_szIp, sock->_port, user._user_name.c_str() , "except message:%s", (const char*)data ) ;
		}
	} else {
		// 处理通应应答消息
		_convert->convert_comm( seqid, macid, val, buf ) ;
	}

	if( buf.getLength() > 0 ) {
		// 添加用户会话中
		_session.AddSession( macid, user._user_id ) ;
		// 发送指定的地区用户
		_pEnv->GetPasClient()->HandleData( buf.getBuffer(), buf.getLength() ) ;
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
			if(user._fd != NULL )
			{
				OUT_WARNING( user._ip.c_str() , user._port , user._user_name.c_str() , "fd %d close socket", user._fd->_fd );
				CloseSocket(user._fd);
			}
		}
		else if(user._socket_type == User::TcpConnClient)
		{
			if( user._fd != NULL )
			{
				OUT_INFO( user._ip.c_str() , user._port , user._user_name.c_str() ,"conn fd %d close socket", user._fd->_fd );
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
	if( now - _last_handle_user_time < timeval){
		return;
	}
	_last_handle_user_time = now ;

	vector<User> vec_users = _online_user.GetOnlineUsers();
	for(int i = 0; i < (int)vec_users.size(); i++)
	{
		User &user = vec_users[i] ;
		if( user._socket_type == User::TcpConnClient || user._fd != NULL )
		{
			string loop = "NOOP \r\n" ;
			SendData( user._fd, loop.c_str(), loop.length() ) ;
			OUT_SEND( user._ip.c_str(), user._port, user._user_id.c_str(),"NOOP");
		}
	}
}

//////////////////////////////////////////// 处理通过HTTP下载图片  /////////////////////////////////////////////////////
// 通过HTTP服务来取图片
void MsgClient::LoadUrlPic( unsigned int seq, const char *path )
{
	char url[1024] = {0};
	sprintf( url, "%s/%s", (const char *)_picUrl, path ) ;

	// 发送获取照片的请求
	if ( ! _httpcaller.Request( seq, url ) ) {
		// 记录发送请求错误
		OUT_ERROR( NULL, 0, NULL, "request url %s seq id %d failed", url, seq ) ;
	}
}

// 处理HTTP的响应回调处理
void MsgClient::ProcessResp( unsigned int seqid, const char *data, const int len , const int err )
{
	// 处理数据
	if ( data == NULL || err != HTTP_CALL_SUCCESS || len == 0 ) {
		OUT_ERROR( NULL, 0, "Pic" , "pic seqid %u, error %d" , seqid, err ) ;
		return ;
	}
	_convert->sendpicture( seqid, data, len ) ;
}

