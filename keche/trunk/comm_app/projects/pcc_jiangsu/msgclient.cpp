#include "msgclient.h"
#include "pccutil.h"
#include "pconvert.h"
#include <tools.h>

MsgClient::MsgClient( CStatInfo *stat )
	: _statinfo(stat)
{
	_last_handle_user_time = time(NULL) ;
	_convert			   = new PConvert ;
}

MsgClient::~MsgClient( void )
{
	Stop() ;

	if ( _convert != NULL ) {
		delete _convert ;
		_convert = NULL ;
	}
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

	char buf[1024] = {0} ;
	char temp[256] = {0} ;

	if ( ! pEnv->GetString( "carmap_file", temp ) ) {
		printf("load map failed\n") ;
		return false ;
	}
	getenvpath( temp, buf ) ;

	_carmapfile = buf ;
	// 加载信息
	_session.Load( buf ) ;

	if ( ! pEnv->GetString( "user_filepath" , temp ) ) {
		printf( "load user file failed\n" ) ;
		return false ;
	}

	// 设置分包对象
	setpackspliter( &_packspliter ) ;

	getenvpath( temp, buf ) ;

	return LoadMsgUser( buf ) ;
}


void MsgClient::Stop( void )
{
	OUT_INFO("Msg",0,"MsgClient","stop");

	StopClient() ;
}

bool MsgClient::Start( void )
{
	return StartClient( "0.0.0.0", 0, 3 ) ;
}

void MsgClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	if ( len < 4 ) return ;
	
	OUT_RECV( sock->_szIp, sock->_port,  NULL, "on_data_arrived:[%d]%s", len, (const char*)data );

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
	OUT_WARNING( sock->_szIp, sock->_port,  user._user_id.c_str(), "Disconnection fd %d" , sock->_fd );
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
		// 加载信息
		_session.Load( _carmapfile.c_str() ) ;
		// 设置车辆总数
		_statinfo->SetTotal( _session.GetCarTotal() ) ;
		// 如果变更就直接通知了
		LoadSubscribe( NULL , true ) ;

		sleep(5) ;
	}
}

// 加载订阅数据
void MsgClient::LoadSubscribe( socket_t *sock , bool notify )
{
	if ( ! notify && _submacids.empty() ) {
		_session.GetCarMacData( _submacids ) ;
	} else {
		string s ;
		_session.GetCarMacData( s ) ;
		if ( s == _submacids && notify ) {
			return ;
		}
		_submacids = s ;
	}

	if ( _submacids.empty() ) return ;

	// DMD 0 {E005_13571198041} \r\n
	CQString sz ;
	sz.AppendBuffer( "DMD 0 {" ) ;
	sz.AppendBuffer( _submacids.c_str() , _submacids.length() ) ;
	sz.AppendBuffer( "} \r\n" ) ;

	// 数据变更通知所有MSG处理
	if ( notify ) {
		vector<User> vec = _online_user.GetOnlineUsers() ;
		for ( int i = 0; i < vec.size(); ++ i ) {
			User &user = vec[i] ;
			if ( user._user_type != MSG_SUB_TYPE ) {
				continue ;
			}
			SendData( user._fd , sz.GetBuffer(), sz.GetLength() ) ;
		}
	} else {
		// 处理单一订阅
		SendData( sock, sz.GetBuffer(), sz.GetLength() ) ;
	}
	if ( sock != NULL )
	OUT_PRINT( sock->_szIp, sock->_port, "DMD", "Send %s" , sz.GetBuffer() ) ;
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
void MsgClient::HandleUpMsgData( const char *code, const char *data, int len )
{
	OUT_INFO( NULL, 0, "msg", "HandleUpMsgData %s", data ) ;
	SendDataToUser( code, data, len ) ;
}

// 构建登陆处理
int MsgClient::build_login_msg( User &user, char *buf,int buf_len )
{
	string stype = "SAVE" , sext = "\r\n" ;
	if ( user._user_type == MSG_SUB_TYPE ) {
		stype = "SAVE" ; sext = "DM \r\n" ;
	} else {
		stype = user._user_type ;
	}
	sprintf( buf, "LOGI %s %s %s %s",
			stype.c_str() , user._user_name.c_str(), user._user_pwd.c_str() , sext.c_str() ) ;

	return (int)strlen(buf) ;
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
					OUT_WARNING( sock->_szIp, sock->_port, NULL,"Can't find the syn_user");
					return;
				}
				user._user_state 		= User::ON_LINE ;
				user._last_active_time  = time(NULL) ;
				// 重新处理用户状态
				_online_user.SetUser( user._user_id, user ) ;

				OUT_CONN( sock->_szIp, sock->_port, user._user_name.c_str(), "Login success, fd %d access code %d" , sock->_fd, user._access_code ) ;

				// 如果为订阅用户就直接处理
				if ( user._user_type == MSG_SUB_TYPE ) {
					LoadSubscribe( sock, false ) ;
				}
				// 记录在用户
				_statinfo->SetClient( sock->_szIp, sock->_port,  0 ) ;
			}
			break ;
		case -1:
			{
				OUT_ERROR( sock->_szIp, sock->_port,  NULL , "LACK,password error!");
			}
			break ;
		case -2:
			{
				OUT_ERROR( sock->_szIp, sock->_port,  NULL ,"LACK,the user has already login!");
			}
			break ;
		case -3:
			{
				OUT_ERROR( sock->_szIp, sock->_port,  NULL, "LACK,user name is invalid!");
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
		OUT_ERROR( sock->_szIp, sock->_port,  "CAIS" , "find fd %d user failed, data %s", sock->_fd, data ) ;
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

	string macid = vec[2] ;
	string cmd   = vec[4] ;
	string head  = vec[0] ;
	string seqid = vec[1] ;
	string val 	 = vec[5] ;

	bool gps = false ;
	if ( cmd == "U_REPT" ) {
		if ( strncmp( val.c_str(), "{TYPE:0" , 7 ) == 0 || strncmp( val.c_str(), "{TYPE:1" , 7 )  == 0 ) {
			_statinfo->AddRecv( sock->_szIp ) ; gps = true ;
		}
	}

	_stCarInfo info ;
	if ( ! _session.GetCarInfo( macid.c_str() , info )  ){
		OUT_ERROR( sock->_szIp, sock->_port, user._user_name.c_str() , "fd %d get car info by %s failed" , sock->_fd, macid.c_str() ) ;
		return ;
	}
	OUT_INFO( sock->_szIp, sock->_port, user._user_name.c_str() , "get color %s number %s , fd %d" ,
			info.color.c_str() , info.vehiclenum.c_str() ,  sock->_fd ) ;

	CQString buf ;
	// 添加到车列表
	_statinfo->AddVechile( sock->_szIp, info.vehiclenum.c_str(), my_atoi( info.color.c_str() ) , false ) ;

	if ( head == "CAITS" ) {
		if( cmd == "U_REPT" ){
			// 上报类消息处理
			_convert->convert_urept( info, val, buf ) ;
		} else if( cmd == "D_CTLM" ) {
			// ToDo: 控制类消息处理
			// _convert->convert_dctlm( seqid, macid, car_num, color, val, msg_len ) ;
		} else if( cmd == "D_SNDM" ) {
			// ToDo : 消息发送的处理
		} else {
			OUT_WARNING( sock->_szIp, sock->_port, user._user_name.c_str() , "except message:%s", (const char*)data ) ;
		}
	} else {
		// 处理通应应答消息
	}

	if( ! buf.IsEmpty() ) {
		// 发送指定的地区用户
		_pEnv->GetPasClient()->HandleData( buf.GetBuffer(), buf.GetLength() ) ;

		_statinfo->AddSend( sock->_szIp ) ;
	} else if (gps){
		PCCMSG_LOG( _pEnv->GetLogger(), sock->_szIp, sock->_port, user._user_name.c_str(), "ERROR", "%s,转换成GPS数据出错,%s",
				info.vehiclenum.c_str(), val.c_str() ) ;
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
			if( user._fd != NULL )
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
			OUT_SEND(user._ip.c_str(),user._port,user._user_id.c_str(),"NOOP");
		}
	}
}

