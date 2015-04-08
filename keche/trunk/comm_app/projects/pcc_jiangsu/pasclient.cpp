#include "pasclient.h"
#include <mybase64.h>
#include "pccutil.h"
#include <crc16.h>
#include <comlog.h>
#include <tools.h>
#include "pconvert.h"

PasClient::PasClient(CStatInfo *stat)
	: _statinfo(stat)
{
}

PasClient::~PasClient()
{
	Stop() ;
}

bool PasClient::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	// serverid  , username, password , serverip , tcpport,  udpport

	char buf[1024] = {0};
	if ( ! _pEnv->GetString( "serverid", buf ) ) {
		printf( "serverid empty\n" ) ;
		OUT_ERROR( NULL, 0, NULL, "serverid empty" ) ;
		return false ;
	}
	_user.SetSrvId( buf ) ;

	PccUser tcpuser, udpuser ;
	if ( ! _pEnv->GetString( "username", buf ) ) {
		printf( "username empty\n") ;
		OUT_ERROR( NULL, 0, NULL, "username empty" ) ;
		return false ;
	}
	tcpuser._username = buf ;
	udpuser._username = buf ;

	if ( ! _pEnv->GetString( "password", buf ) ) {
		printf( "password empty\n") ;
		OUT_ERROR( NULL, 0, NULL, "password empty" ) ;
		return false ;
	}
	tcpuser._password = buf ;
	udpuser._password = buf ;

	if ( ! _pEnv->GetString( "serverip", buf ) ) {
		printf( "serverip empty\n") ;
		OUT_ERROR( NULL, 0, NULL, "serverip empty" ) ;
		return false ;
	}

	tcpuser._srv_ip = buf ;
	udpuser._srv_ip = buf ;
	_srvip			= buf ;

	if ( ! _pEnv->GetString( "tcpport", buf ) ) {
		printf( "tcpport empty\n") ;
		OUT_ERROR( NULL, 0, NULL, "tcpport empty" ) ;
		return false ;
	}
	tcpuser._srv_port = atoi( buf ) ;

	if ( ! _pEnv->GetString( "udpport", buf ) ) {
		printf( "udpport empty\n") ;
		OUT_ERROR( NULL, 0, NULL, "udpport empty" ) ;
		return false ;
	}
	udpuser._srv_port = atoi(buf) ;

	if ( ! _pEnv->GetString( "myudpsrvip" , buf ) ) {
		printf( "my udp server ip empty\n" ) ;
		OUT_ERROR( NULL, 0, NULL, "my udp server ip empty" ) ;
		return false ;
	}
	_ip = buf ;

	if ( ! _pEnv->GetString( "myudpport" , buf ) ) {
		printf( "my udp port ip empty\n" ) ;
		OUT_ERROR( NULL, 0, NULL, "my udp port ip empty" ) ;
		return false ;
	}
	_port = atoi(buf) ;

	_user.SetUser( tcpuser, true  ) ;
	_user.SetUser( udpuser, false ) ;
	// 设置分包对象对数据进行分包处理
	setpackspliter( &_packspliter ) ;

	// 加载车辆静态信息
	return _busloader.Init( pEnv ) ;
}

void PasClient::Stop( void )
{
	_busloader.Stop() ;
	StopClient() ;
}

bool PasClient::Start( void )
{
	// 启动车辆静态数据信息
	_busloader.Start() ;

	// 这里需要UDP做数据通道
	if ( ! StartUDP( "0.0.0.0", _port , 15 ) ) {
		printf( "start udp server failed" ) ;
		OUT_ERROR( NULL, 0, NULL, "start udp server failed" ) ;
		return false ;
	}
	// 这里需要TCP做控制通道
	return StartClient( "0.0.0.0", 0, 3 ) ;
}

// 初始化UDP的服务
bool PasClient::StartUDP( const char * ip, int port, int thread )
{
	_udp_handle.init( thread ) ;
	if( !_udp_handle.start_server( port, ip ) ){
		printf( "start ip %s port %d falied, %s:%d\n", ip, port , __FILE__, __LINE__ ) ;
		return false;
	}
	_udp_inited = true ;
	return true ;
}

void PasClient::on_data_arrived( socket_t *sock, const void* data, int len)
{
	if ( len < 2 || data == NULL ){
		OUT_ERROR( sock->_szIp, sock->_port, "Recv", "recv fd %d data %s error, data len:%d", sock->_fd, (char *)data, len ) ;
		return ;
	}

	const char *ptr = ( const char *) data ;
	if ( (char)(*ptr) == '*' ) {  // 数据
		HandleRecvData( sock, (const char *)data, len ) ;
	} else { // 控制处理
		HandleCtrlData( sock, (const char *)data, len ) ;
	}
}

void PasClient::HandleRecvData( socket_t *sock, const char *data, int len )
{
	OUT_RECV( sock->_szIp, sock->_port, "PasClient", "UDP fd %d, Recv: %s", sock->_fd, data ) ;

	// 否则是别人的UDP连接过来的数据
	if ( strcmp( _srvip.c_str(), sock->_szIp ) != 0  ) {
		// 拆分数据
		const char *ptr = strstr( data, "|" ) ;
		if ( ptr == NULL ) {
			OUT_ERROR( sock->_szIp, sock->_port, "Recv", "PasClient UDP fd %d data error", sock->_fd ) ;
			return ;
		}

		char srvid[128] = {0};
		memcpy( srvid, data+1, ptr- data -1 ) ;
		// 暂时只处理数据通道的心跳
		if ( strncmp( ptr+1 , "NOOP", 4 ) == 0 ) {
			SendData( sock, data, len ) ;
			OUT_INFO( sock->_szIp, sock->_port, srvid, "PasClient UDP fd %d NOOP", sock->_fd ) ;
		} else {

			_statinfo->AddRecv( sock->_szIp ) ;

			vector<string> vec;
			if ( ! splitvector( data, vec, "|", 0 ) ) {
				OUT_ERROR( sock->_szIp, sock->_port, "Recv", "fd %d, split data error: %s", sock->_fd, data ) ;
				return ;
			}
			// 7
			CBase64Ex base64 ;
			base64.Decode( vec[7].c_str(), vec[7].length() ) ;

			string sdata = "*";
			sdata += GetSrvId() ;
			sdata += ptr ;
			// *0431|GPSPosInfo|1.0.4|XQ|4|P|17|mxJ5B4AqAX4|0|0|118.3315|33.7856|99.98|+93.9|308|1340187770|0|-1|nhs#
			if ( HandleData( sdata.c_str(), sdata.length() ) ) {
				_statinfo->AddSend( sock->_szIp ) ;
			} else {
				PCCMSG_LOG( _pEnv->GetLogger(), sock->_szIp, sock->_port, srvid, "ERROR", "发送车牌号%s到服务器失败", base64.GetBuffer() ) ;
			}
			OUT_PRINT( sock->_szIp, sock->_port, srvid, "%s,PasClient UDP fd %d, HandleData: %s" ,
					base64.GetBuffer(), sock->_fd, sdata.c_str() ) ;

			bool error = false ;
			char szmacid[512] = {0} ;
			if ( _pEnv->GetMsgClient()->GetCarMacId( base64.GetBuffer(), szmacid ) ) {
				CQString buf , msg ;
				if ( PConvert::buildintergps( vec, szmacid, buf , msg ) ) {
					// 转换回内部协议发送给MSG
					_pEnv->GetMsgClient()->HandleUpMsgData( SEND_ALL, buf.GetBuffer(), buf.GetLength() ) ;
					// 打印的数据
					OUT_INFO( sock->_szIp, sock->_port, srvid, "fd %d, PasServer %s, Send Data: %s",
							sock->_fd, base64.GetBuffer(),  buf.GetBuffer() ) ;
				} else {
					error = true ;
					OUT_ERROR( sock->_szIp, sock->_port,  srvid, "fd %d, %s, PasClient build inter gps macid %s",
							sock->_fd, base64.GetBuffer(), szmacid ) ;
					PCCMSG_LOG( _pEnv->GetLogger(), sock->_szIp, sock->_port, srvid, "ERROR", "车牌号%s,错误：%s",
							base64.GetBuffer() , msg.GetBuffer() ) ;
				}
			} else {
				error = true ;
				OUT_ERROR( sock->_szIp, sock->_port, srvid, "PasClient UDP fd %d, %s get macid failed",
						sock->_fd, base64.GetBuffer() ) ;
				//PCCMSG_LOG( _pEnv->GetLogger(), ip, port, srvid, "ERROR", "取得车牌号%s值对应关系失败", base64.GetBuffer() ) ;
			}
			_statinfo->AddVechile( sock->_szIp, base64.GetBuffer(), my_atoi(vec[4].c_str()) , error ) ;
		}
	} else {
		// 如果来自服务器的UDP的数据
		PccUser user ;
		if ( ! _user.GetUser( sock, user ) ) {
			PccUser &udpuser = _user.GetUser( false ) ;
			udpuser._fd  = sock ;
			udpuser.SetOnline() ;
		} else {
			_user.Update( PCC_USER_ACTVIE, user._tcp ) ;
			PccUser &tmpuser = _user.GetUser( false ) ;
			tmpuser.SetOnline();
		}

		vector<string> vec;
		if ( ! splitvector( data, vec, "|", 0 ) ) {
			OUT_ERROR( sock->_szIp, sock->_port, "Recv", "fd %d, split data error: %s", sock->_fd, data ) ;
			return ;
		}

		if ( vec.size() < 3 ){
			OUT_ERROR( sock->_szIp, sock->_port, "Recv", "fd %d, data error: %s", sock->_fd, data ) ;
			return ;
		}

		// 暂时只处理数据通道的心跳
		if ( vec[1] == "NOOP" ) {
			OUT_RECV( sock->_szIp, sock->_port, "UDP", "Recv fd %d data NOOP" , sock->_fd ) ;
		} /*else if ( vec[1] == "QryGPSPosInfo" || vec[1] == "QueryAllGPSData" ) {
			PccUser &udp = _user.GetUser(false) ;
			SendData( udp._fd, data, len ) ;
			OUT_PRINT( NULL, 0, "Send", "%s", data ) ;
		}*/
	}
}

void PasClient::HandleCtrlData( socket_t *sock, const char *data, int len )
{
	PccUser user ;
	if ( ! _user.GetUser( sock, user ) ) {
		if ( ! check_udp_fd(sock) ) {
			OUT_ERROR( sock->_szIp, sock->_port, "Recv", "get user failed, recv fd %d data %s", sock->_fd, (char *)data ) ;
			return ;
		}
		// 如果为UDP就直接收了
		PccUser &tmp = _user.GetUser(false) ;
		user = tmp ;
	}
	_user.Update( PCC_USER_ACTVIE, user._tcp ) ;

	vector<string> vecline ;
	// 拆分数据
	if ( ! splitvector( data, vecline, "\r\n", 0 ) ){
		OUT_ERROR( sock->_szIp, sock->_port, "Recv", "fd %d, data error: %s", sock->_fd, data ) ;
		return ;
	}

	OUT_RECV( sock->_szIp, sock->_port, user._username.c_str(), "fd %d, Recv: %s", sock->_fd, data ) ;
	// 处理所有拆分后的数据
	for ( int i = 0; i < vecline.size(); ++ i ) {
		string &line = vecline[i] ;
		if ( line.empty() )
			continue ;

		vector<string> veck ;
		if ( ! splitvector( line, veck, " ", 0 ) )
			continue ;

		if ( veck.size() < 3 ) continue ;

		if ( veck[0] != "SZE" || veck[2].empty() )
			continue ;

		if ( veck[1] == "L" )
		{  // 控制通道连接
			const char *lkey = veck[2].c_str() ;
			if ( strcmp( lkey, "-1") == 0 || strcmp( lkey, "-2") == 0  ) {
				if ( strcmp( lkey, "-1") == 0 ) {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "invalid connected fd %d" , sock->_fd ) ;
				} else if ( strcmp( lkey, "-2") == 0 ) {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "in reconnect time fd %d", sock->_fd ) ;
				} else {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "unkown error fd %d", sock->_fd ) ;
				}
				CloseSocket( sock ) ;
			} else {
				char buf[1024] = {0};  // 连接成功需要发送用户鉴权
				sprintf( buf, "SZ A %s|%s|%s\r\n", veck[2].c_str(), user._username.c_str(), user._password.c_str() ) ;
				SendData( sock, buf, strlen(buf) ) ;
				OUT_SEND( sock->_szIp, sock->_port, user._username.c_str(), "Send %s", buf ) ;
			}
		}
		else if ( veck[1] == "A" || veck[1] == "P" )
		{ // 用户鉴权, 或者数据通道注册
			const char *ckey = veck[2].c_str() ;
			if ( strcmp( ckey, "-1") == 0 || strcmp( ckey, "-2") == 0 ) {
				if (  strcmp( ckey, "-1") == 0 ) {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "%s fd %d failed",
							( veck[1] == "A" ) ? "auth" : "register data way",sock->_fd ) ;
				} else if ( strcmp( ckey, "-2") == 0 ) {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "lkey %s invalid fd %d",
							user._srv_key.c_str(), sock->_fd ) ;
				} else {
					OUT_ERROR( sock->_szIp, sock->_port, user._username.c_str(), "unkown error fd %d", sock->_fd ) ;
				}
				CloseSocket( sock ) ;
				continue ;
			}

			PccUser &tmp = _user.GetUser( ( veck[1] == "A" ) ) ;
			tmp._srv_key = veck[2] ;  // 如果鉴权成功，保存鉴权码
			tmp.Update( PCC_USER_ACTVIE ) ;
			if ( veck[1] == "A" ) {
				tmp.SetOnline() ;
			}
			OUT_INFO( sock->_szIp, sock->_port, tmp._username.c_str(), "Login success, fd %d, login fd %d tcp %d, srv key %s" ,
					sock->_fd , tmp._fd ? tmp._fd->_fd : -1, tmp._tcp , veck[2].c_str() ) ;
		}
		else if ( veck[1] == "N" )
		{ // 控制通道握手维护
			OUT_INFO( sock->_szIp, sock->_port, user._username.c_str(), "NOOP" ) ;
		}
	}
}

void PasClient::on_dis_connection( socket_t *sock )
{
	//专门处理底层的链路突然断开的情况，不处理超时和正常流程下的断开情况。
	OUT_INFO( sock->_szIp, sock->_port, "Conn", "Recv disconnect fd %d", sock->_fd ) ;
	_user.DisConnect( sock ) ;
}

void PasClient::TimeWork()
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

		// 检测用户状态
		CheckUserState( _user ) ;

		// 十分钟超时检测
		_pEnv->GetMsgCache()->CheckData( 600 ) ;

		// PCCMSG_LOG( _pEnv->GetLogger(), "127.0.0.1", 8888 , "0431", "ERROR", "取得车牌号%s值对应关系失败", "test" ) ;

		sleep(3) ;
	}
}


// 重连控制连接还是数据连接
void PasClient::ConnectServer( UserSession &user, bool tcp )
{
	// 更新最后一次登陆的时间
	user.Update( PCC_USER_LOGIN, tcp ) ;

	PccUser &pccuser = user.GetUser( tcp ) ;
	// 如果TCP连接
	if ( tcp ) {
		pccuser._fd = _tcp_handle.connect_nonb( pccuser._srv_ip.c_str(), pccuser._srv_port, 10 ) ;
		if ( pccuser._fd > 0 ) {
			pccuser.SetWaitResp() ;
		}
	} /**else { // 数据通道
		pccuser._fd = _udp_handle.connect_nonb( pccuser._srv_ip.c_str(), pccuser._srv_port, 10 ) ;
		if ( pccuser._fd == -1 )
			return ;
		pccuser.SetWaitResp() ;
	}*/

	PccUser &udpuser = user.GetUser( false ) ;
	udpuser.SetOffline() ;
	if(udpuser._fd != NULL) {
		CloseSocket(udpuser._fd);
		udpuser._fd = NULL;
	}

	// 通过TCP通道发送注册数据通道信息
	if ( ! tcp ) {
		PccUser &tcpuser = user.GetUser(true) ;

		char buf[1024] = {0};
		sprintf( buf, "SZ P %s|%s|%d\r\n", user.GetKey(true), _ip.c_str(), _port ) ;
		// 发送数据通道连接请求
		SendData( tcpuser._fd, buf, strlen(buf) ) ;

		//OUT_SEND( pccuser._srv_ip.c_str(), pccuser._srv_port, pccuser._username.c_str(), "fd %d, Tcp fd %d Send %s",
		//		pccuser._fd->_fd, tcpuser._fd->_fd, buf ) ;
	}
}

// 检测用户状态
void PasClient::CheckUserState( UserSession &user )
{
	// 如果当前TCP用户不在线
	if ( user.IsOffline(true) ) {
		// 如果当前用户还没有超时就允许重新连接
		if ( !user.Check( 30, PCC_USER_LOGIN , true ) )
			return ;
		ConnectServer( user, true ) ;
	} else if ( user.IsOffline(false) && user.IsOnline(true) ){
		// 如果当前TCP用户鉴权码为空也不能登陆数据链路
		if ( ! user.IsKey(true) )
			return ;
		if ( ! user.Check( 30, PCC_USER_LOGIN, false ) )
			return ;

		ConnectServer( user, false ) ;
	}
}

void PasClient::NoopWork()
{
	while(1) {
		if ( !Check() ) break ;

		CheckUserLoop( _user ) ;

		sleep(5) ;
	}
}

// 检测用户心跳
void PasClient::CheckUserLoop( UserSession &user )
{
	// 控制通道的链路维护
	PccUser &tcpuser = user.GetUser( true ) ;
	if ( tcpuser.IsOnline() ) {
		if ( tcpuser.Check( 30, PCC_USER_LOOP ) && user.IsKey(true) ) {
			char buf[1024] = {0};
			sprintf( buf, "SZ N %s\r\n", tcpuser._srv_key.c_str() ) ;
			SendData( tcpuser._fd, buf, strlen(buf) ) ;
			tcpuser.Update( PCC_USER_LOOP ) ;

			OUT_SEND( tcpuser._srv_ip.c_str(), tcpuser._srv_port, tcpuser._username.c_str(), "fd %d, Send TCP NOOP: %s",
					tcpuser._fd->_fd, buf ) ;
		}
		// 如果超时需要处理重连操作
		if ( tcpuser.Check( 180, PCC_USER_ACTVIE ) ) {
			tcpuser.SetOffline() ;
			user.GetUser(false).SetOffline() ;
			OUT_ERROR( tcpuser._srv_ip.c_str(), tcpuser._srv_port, tcpuser._username.c_str(), "tcp  actvie time timeout") ;
		}
	} else if ( ! tcpuser.IsOffline() ) {
		if ( tcpuser.Check( 120, PCC_USER_LOGIN ) ) {
			tcpuser.SetOffline() ;
			OUT_PRINT( tcpuser._srv_ip.c_str(), tcpuser._srv_port, tcpuser._username.c_str(), "connect server tcp timeout") ;
		}
	}

	// 数据通道链路维护
	PccUser &udpuser = user.GetUser(false) ;
	if ( udpuser.IsOnline() ) {
		if ( udpuser.Check( 30, PCC_USER_LOOP ) && user.IsKey(false) ) {
			char buf[1024] = {0};
			sprintf( buf, "*%s|NOOP|%s#", user.GetSrvId(), udpuser._srv_key.c_str() ) ;
			SendData( udpuser._fd, buf, strlen(buf) ) ;
			// _udp_handle.deliver_data( udpuser._fd, udpuser._srv_ip.c_str(), udpuser._srv_port, buf, strlen(buf) ) ;
			udpuser.Update( PCC_USER_LOOP ) ;

			OUT_SEND( udpuser._srv_ip.c_str(), udpuser._srv_port, udpuser._username.c_str(), "fd %d, Send UDP NOOP: %s",
					udpuser._fd->_fd, buf ) ;
		}
		// 如果UDP心跳没有应答就直接处理重连了
		if ( udpuser.Check(180, PCC_USER_ACTVIE) ) {
			udpuser.SetOffline() ;
			OUT_ERROR( udpuser._srv_ip.c_str(), udpuser._srv_port, udpuser._username.c_str(), "udp active time timeout" ) ;
			tcpuser.SetOffline() ;
		}
	} else {
		if ( udpuser.Check( 120, PCC_USER_LOGIN) ) {
			udpuser.SetOffline() ;
			OUT_PRINT( udpuser._srv_ip.c_str(), udpuser._srv_port, udpuser._username.c_str(), "connect server udp timeout") ;
			tcpuser.SetOffline() ;
		}
	}
}

bool PasClient::HandleData( const char *data, int len )
{
	PccUser &user = _user.GetUser( false ) ;
	// 发送数据到监管平台,这里是通过数据通道来发送数据
	if ( ! _user.IsOnline(false) ) {
		OUT_ERROR( user._srv_ip.c_str(),  user._srv_port, user._username.c_str(), "User data way is not exist, data: %s" , data ) ;
		// _user.SetState( false, true ) ;
		return false;
	}
	if ( ! SendData( user._fd, data, len ) ){
		OUT_ERROR( user._srv_ip.c_str(),  user._srv_port, user._username.c_str(), "Send Data Failed,%s" , data ) ;
		return false ;
	}
	OUT_PRINT( user._srv_ip.c_str(),  user._srv_port, user._username.c_str(), "fd %d, Send Data: %s" , user._fd->_fd, data ) ;
	return true ;
}
