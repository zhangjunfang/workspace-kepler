#include "msgclient.h"
#include <tools.h>

#include <string>
#include <vector>
#include <fstream>
using std::string;
using std::vector;
using std::ifstream;

MsgClient::MsgClient( const char *strtype ):
	_session( true ), _dataroute(false)
{
	_pMsgClient = NULL ;
	_last_handle_user_time = time(NULL) ;
	_strclient = strtype ;
}

MsgClient::~MsgClient( void )
{
	Stop() ;
}

// 加载MSG的用户文件
bool MsgClient::LoadMsgUser( const char *userfile )
{
	if ( userfile == NULL ) return false ;

	int  len  = 0 ;
	char *ptr = ReadFile(userfile, len) ;
	if ( ptr == NULL ) {
		OUT_ERROR( NULL, 0, NULL, "Read msg user failed, %s", userfile ) ;
		return false ;
	}

	int i = 0 ;
	// 将分割符处理
	for( i = 0; i < len; ++ i ) {
		if ( ptr[i] != '\r' ) {
			continue ;
		}
		ptr[i] = '\n' ;
	}

	vector<string> vec ;
	if ( ! splitvector( ptr, vec, "\n", 0 ) ) {
		FreeBuffer( ptr ) ;
		OUT_ERROR( NULL, 0, NULL, "Split msg data failed" ) ;
		return false ;
	}

	int count = 0 ;
	for( i = 0; i < (int)vec.size(); ++ i )
	{
		string &line = vec[i] ;
		//1:10.1.99.115:8880:user_name:user_password:A3
		vector<string> vec_line ;
		if ( ! splitvector( line, vec_line, ":" , 7 ) ){
			continue ;
		}

		if ( vec_line[0] != _strclient ) {
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
	FreeBuffer( ptr ) ;

	OUT_INFO( NULL, 0, NULL, "load msg user success %s, count %d" , userfile , count ) ;
	printf( "load msg user count %d success %s\n" , count, userfile ) ;

	return true ;
}

bool MsgClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	// 取得用户文件路径
	char user_file[256] = {0} ;
	if ( ! pEnv->GetString( "user_filepath" , user_file ) ) {
		printf( "load user file failed\n" ) ;
		return false ;
	}

	int nvalue = 0 ;
	// 是否仅只是数据路由
	if ( pEnv->GetInteger( "dataroute", nvalue ) ) {
		_dataroute = ( nvalue == 1 ) ;
	}

	char temp[512] = {0} ;
	// 加载基本订阅列表路径
	if ( pEnv->GetString( "base_dmddir" ,temp ) ) {
		_dmddir.SetString( temp ) ;
		OUT_INFO( NULL, 0 , NULL, "Load base dmddir %s", temp ) ;
	}

	setpackspliter( &_packspliter ) ;

	return LoadMsgUser( user_file ) ;
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
//	FUNTRACE("void ClientAccessServer::HandleInterData(int fd, const void *data, int len)");
	if ( len < 4 ) {
		OUT_ERROR( sock->_szIp, sock->_port, NULL, "recv data error length: %d", len ) ;
		return ;
	}

    const char *ip 	    =  sock->_szIp;
    unsigned short port =  sock->_port;

	OUT_RECV( ip, port, NULL, "on_data_arrived:[%d]%s", len, (const char*)data );

	const char *ptr = ( const char *) data ;
	if ( strncmp( ptr, "CAIT" , 4 ) == 0  ) {
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
	User user = _online_user.GetUserBySocket( sock );
	OUT_WARNING( sock->_szIp , sock->_port, user._user_id.c_str(), "Disconnection fd %d" , sock->_fd );
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
		HandleOnlineUsers( 30 ) ;

		// 处理超时的对象
		_session.CheckTimeOut( 120 ) ;
		//这个地方，超时的同步有问题
		sleep(5);
	}
}

void MsgClient::NoopWork()
{

}

// 向MSG上传消息
void MsgClient::HandleMsgData( const char *macid, const char *data, int len )
{
	OUT_INFO( NULL, 0, "msg", "HandleUpMsgData %s", data ) ;
	if ( _strclient == MSG_SAVE_CLIENT ) {
		if ( _dataroute ) {
			// 如果只是数据理由就只允许数据接过来不允许数据发送过去
			return ;
		}

		string val ;
		if ( ! _session.GetSession(macid,val) ) {
			OUT_ERROR( NULL, 0, macid, "Get Session Failed, Data %s" , data ) ;
			return ;
		}
		User user = _online_user.GetUserByUserId( val ) ;
		if ( user._user_id.empty() || user._fd == NULL ) {
			OUT_ERROR( NULL, 0, macid, "Get User empty , User id: %s, data %s" , val.c_str() , data ) ;
			return ;
		}
		// 发送数据
		SendData( user._fd, data, len ) ;
		return ;
	}

	// 广播模式
	vector<User> vec = _online_user.GetOnlineUsers() ;

	int count = vec.size() ;
	//  直接群发处理
	for ( int i = 0; i < count; ++ i ) {
		User &user = vec[i] ;
		if ( user._fd == NULL || user._user_state != User::ON_LINE )
			continue ;
		SendData( user._fd, data ,len ) ;
	}
}

// 构建登陆信息数据
int  MsgClient::build_login_msg(User &user, char *buf, int buf_len)
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

// 纷发登陆用户会话数据
void MsgClient::HandleSession( socket_t *sock, const char *data, int len )
{
	string line = data;

	vector<string> vec_temp ;
	if ( ! splitvector( line, vec_temp, " " , 1 ) ) {
		return ;
	}

	const char *ip 	    = sock->_szIp ;
	unsigned short port = sock->_port ;

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
				if( user._user_id.empty() ) {
					OUT_WARNING(ip,port,NULL,"Can't find the syn_user");
					return;
				}
				user._user_state 		= User::ON_LINE ;
				user._last_active_time  = time(NULL) ;
				// 重新处理用户状态
				_online_user.SetUser( user._user_id, user ) ;

				OUT_CONN( ip , port, user._user_name.c_str(), "Login success, fd %d access code %d" , sock->_port, user._access_code ) ;
				// 登陆成功，如果为数据订制连接就直接需要处理发送订阅处理
				if ( user._user_type == "DMDATA" ) LoadSubscribe( user ) ;
			}
			break ;
		case -1:
			{
				OUT_ERROR(ip, port, NULL , "LACK,password error!");
			}
			break ;
		case -2:
			{
				OUT_ERROR(ip, port, NULL ,"LACK,the user has already login!");
			}
			break ;
		case -3:
			{
				OUT_ERROR(ip, port, NULL, "LACK,user name is invalid!");
			}
			break ;
		default:
			{
				OUT_ERROR( ip, port, NULL,  "unknow result" ) ;
			}
			break;
		}

		// 如果返回错误则直接处理
		if ( ret < 0 ) {
			_tcp_handle.close_socket( sock );
		}
	}
	else if (head == "NOOP_ACK")
	{
		User user = _online_user.GetUserBySocket( sock ) ;
		user._last_active_time  = time(NULL) ;
		_online_user.SetUser( user._user_id, user ) ;

		OUT_INFO( ip, port, user._user_name.c_str() , "NOOP_ACK");
	}
	else
	{
		OUT_WARNING( ip , port , NULL, "except message:%s", (const char*)data );
	}
}

void MsgClient::HandleInnerData( socket_t *sock, const char *data, int len )
{
	const char *ip 		= sock->_szIp ;
	unsigned short port = sock->_port ;

	User user = _online_user.GetUserBySocket( sock ) ;
	if ( user._user_id.empty()  ) {
		OUT_ERROR( ip, port, "CAIS" , "find fd %d user failed, data %s", sock->_fd, data ) ;
		return ;
	}

	string line( data, len ) ;
	vector<string>  vec ;
	if ( ! splitvector( line, vec, " " , 6 )  ){
		OUT_ERROR( ip, port, user._user_name.c_str() , "fd %d data error: %s", sock->_fd, data ) ;
		return ;
	}

	string macid     = vec[2] ;
	// if store type need save session
	if ( _strclient == MSG_SAVE_CLIENT ) {
		_session.AddSession( macid, user._user_id ) ;
	}

	// add end split string "\r\n"
	string sdata = line + "\r\n" ;

	// 转发数据
	_pMsgClient->HandleMsgData( macid.c_str(), sdata.c_str(), sdata.length() ) ;

	user._last_active_time = time(NULL) ;
	_online_user.SetUser( user._user_id, user ) ;
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
				OUT_INFO( user._ip.c_str() , user._port , user._user_name.c_str() , "HandleOffline Users close socket fd %d", user._fd->_fd );
				CloseSocket(user._fd);
			}
		}
		else if(user._socket_type == User::TcpConnClient)
		{
			if(user._fd != NULL )
			{
				OUT_INFO( user._ip.c_str() , user._port , user._user_name.c_str() ,"TcpConnClient close socket fd %d", user._fd->_fd );
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
		if( user._socket_type == User::TcpConnClient)
		{
			string loop = "NOOP \r\n" ;
			SendData( user._fd, loop.c_str(), loop.length() ) ;
			OUT_SEND(vec_users[i]._ip.c_str(),vec_users[i]._port,vec_users[i]._user_id.c_str(),"NOOP");
		}
	}
}

// 加载订阅数据
void MsgClient::LoadSubscribe( User &user )
{
	if ( _dmddir.IsEmpty() )
		return ;

	char szbuf[1024] = {0};
	sprintf( szbuf, "%s/%d", _dmddir.GetBuffer(), user._access_code ) ;

    string line;
    ifstream ifs;

	size_t prev;
	size_t next;
	size_t size;
	string value;
	string inner;
	string order;

	inner = "";
	order = "DMD";
	ifs.open(szbuf);
	while (getline(ifs, line)) {
		if ((size = line.size()) > 0 && line[size - 1] == '\r') {
			line.erase(size - 1);
		}

		if (line.empty() || line[0] == '#') {
			continue;
		}

		prev = 0;
		size = line.size();
		while (prev < size) {
			if ((next = line.find_first_of(", ", prev)) == string::npos) {
				next = size;
			}
			value = line.substr(prev, next - prev);
			prev = next + 1;

			if (value.empty()) {
				continue;
			}

			if (inner.empty()) {
				inner.assign(order + " 0 {" + value);
			} else {
				inner.append("," + value);
			}

			if (inner.size() > 4096) {
				inner.append("} \r\n");
				SendData(user._fd, inner.c_str(), inner.length());
				OUT_SEND(user._ip.c_str(), user._port, "SEND", "%s", line.c_str());
				inner = "";
				order = "ADD";
			}
		}
	}
	ifs.close();

	if (!inner.empty()) {
		inner.append("} \r\n");
		SendData(user._fd, inner.c_str(), inner.length());
		OUT_SEND(user._ip.c_str(), user._port, "SEND", "%s", line.c_str());
	}
}

