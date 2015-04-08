/*
 * passerver.cpp
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  添加简单服务器通信框架
 */

#include "passerver.h"
#include <comlog.h>
#include <tools.h>
#include <protocol.h>
#include <arpa/inet.h>
#include "mybase64.h"
#include "pconvert.h"

PasServer::PasServer(CStatInfo *stat)
	: _user_mgr(this), _statinfo(stat)
{
	_thread_num  = 10 ;
	_last_check  = time(NULL) ;
	_xmlpath     = "./runing.xml" ;
}

PasServer::~PasServer( void )
{
	Stop() ;
}

bool PasServer::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char szip[128] = {0} ;
	if ( ! pEnv->GetString("pas_listen_ip" , szip) ){
		OUT_ERROR( NULL, 0, NULL, "get pas_listen_ip failed" ) ;
		return false ;
	}
	_listen_ip = szip ;

	int port = 0 ;
	if ( ! pEnv->GetInteger( "pas_listen_port", port ) ){
		OUT_ERROR( NULL, 0, NULL, "get pas_listen_port failed" ) ;
		return false ;
	}
	_listen_port = port ;

	int nvalue = 10 ;
	if ( pEnv->GetInteger( "pas_tcp_thread", nvalue )  ){
		_thread_num = nvalue ;
	}

	char szbuf[1024] = {0} ;
	if ( pEnv->GetString( "pcc_run_xml", szbuf) ) {
		_xmlpath = szbuf ;
	}

	// 设置数据分包对象
	setpackspliter( &_pack_spliter ) ;

	return true ;
}

bool PasServer::Start( void )
{
	/**
	if ( ! StartUDP( _listen_port , _listen_ip.c_str() , _thread_num ) ){
		OUT_ERROR( NULL, 0, "PasServer", "start udp failed" ) ;
		return false ;
	}*/
	if ( ! _udp_handle.init( _thread_num, _thread_num ) ) {
		OUT_ERROR( NULL, 0, "PasServer", "init udp client failed" ) ;
		return false ;
	}

	return StartServer( _listen_port , _listen_ip.c_str() , _thread_num ) ;
}

// 重载STOP方法
void PasServer::Stop( void )
{
	StopServer() ;
}

void PasServer::TimeWork()
{
	OUT_INFO( NULL, 0, NULL , "void PasServer::TimeWork()" ) ;
	//首先做的一件事就是加载用户信息列表
	while (1){
		if ( ! Check() ) break ;

		time_t now = time(NULL) ;
		if ( now - _last_check > 30 ) {
			_user_mgr.Check( 120 ) ;
			_last_check = now ;
		}
		// 检测统计数据
		_statinfo->Check() ;
		// 打铒数据
		_statinfo->Print( _xmlpath.c_str() ) ;

        sleep(10);
	}
}

void PasServer::NoopWork()
{
	OUT_INFO( NULL, 0, NULL , "void PasServer::NoopWork()" ) ;
}

void PasServer::on_data_arrived( socket_t *sock, const void *data, int len) //从客户端数据过来的数据
{
	if ( len < 3 ) {
		OUT_ERROR( sock->_szIp, sock->_port , "Data", "Recv fd %d, len %d data %s", sock->_fd, len, ( const char *)data ) ;
		return ;
	}

	const char *ptr = ( const char *) data ;
	if ( (char)(*ptr) == '*' ) {
		// 处理UDP的数据通道的数据
		HandleUDPData( sock, ptr, len ) ;
	} else {
		// 处理TCP的控制通道的数据
		HandleTcpData( sock, ptr, len ) ;
	}
}

// 处理TCP的数据
void PasServer::HandleTcpData( socket_t *sock, const char *data, int len )
{
	OUT_RECV( sock->_szIp, sock->_port, "PasServer", "TCP fd %d, Recv: %s", sock->_fd, data ) ;

	_PairUser *user = _user_mgr.GetUser( sock ) ;
	if ( user == NULL ) {
		OUT_ERROR( sock->_szIp, sock->_port, "Error", "fd %d, recv data %s not find user", sock->_fd, data ) ;
		return ;
	}

	// 处理所有拆分后的数据
	string line( data, len - 2 ) ;
	vector<string> veck ;
	if ( ! splitvector( line, veck, " ", 0 ) ){
		OUT_ERROR( sock->_szIp, sock->_port, "Error", "fd %d, split data %s error", sock->_fd, data ) ;
		return ;
	}
	if ( veck.size() < 3 ) {
		OUT_ERROR( sock->_szIp, sock->_port,  "Error", "fd %d, data format %s error", sock->_fd, data ) ;
		return ;
	}
	if ( veck[0] != "SZ" || veck[2].empty() ){
		OUT_ERROR( sock->_szIp, sock->_port, "Error", "fd %d, param %s error", sock->_fd, data ) ;
		return ;
	}

	string val = veck[2] ;
	if ( veck[1] == "A" )
	{  // 控制通道连接
		vector<string> vk ;
		if ( ! splitvector( val, vk, "|", 3 ) ){
			OUT_ERROR( sock->_szIp, sock->_port, "Error", "fd %d split param failed" , sock->_fd ) ;
			return ;
		}

		string ckey ;

		char szbuf[256] = {0};
		if ( ! _user_mgr.OnAuth( sock, vk[0].c_str(), vk[2].c_str() , ckey ) ) {
			sprintf( szbuf, "SZE A -2\r\n" ) ;
			OUT_ERROR( sock->_szIp, sock->_port, "Error", "fd %d auth lkey failed", sock->_fd ) ;
		} else {
			sprintf( szbuf, "SZE A %s\r\n" , ckey.c_str() ) ;
			OUT_INFO( sock->_szIp, sock->_port, "Auth", "fd %d auth key %s success", sock->_fd , ckey.c_str() ) ;
		}
		SendData( sock, szbuf, strlen(szbuf) ) ;
	}
	else if ( veck[1] == "P" )
	{ // 用户鉴权, 或者数据通道注册
		vector<string> vk ;
		if ( ! splitvector( val, vk, "|", 3 ) ){
			OUT_ERROR( sock->_szIp, sock->_port,  "Error", "fd %d split param failed" , sock->_fd ) ;
			return ;
		}

		if( vk[0].empty() || vk[1].empty() || vk[2].empty() ){
			return ;
		}

		string pkey ;
		char szbuf[256] = {0};

		const char *szip = vk[1].c_str() ;
		int udpport = atoi( vk[2].c_str() ) ;

		// 记录在用户
		_statinfo->SetClient( sock->_szIp, sock->_port,  udpport ) ;

		socket_t *nfd = ConnectUDP( szip , udpport ) ;
		if ( nfd == NULL ) {
			OUT_ERROR( sock->_szIp, sock->_port,  "Error", "connect udp server ip %s port %d failed", szip, udpport ) ;
			return ;
		}

		const char *ckey = vk[0].c_str() ;
		if ( ! _user_mgr.OnRegister( sock, ckey, szip , udpport , nfd, pkey ) ) {
			sprintf( szbuf, "SZE P -1\r\n" ) ;
			OUT_ERROR( sock->_szIp, sock->_port,  "Error", "fd %d register pkey failed", sock->_fd ) ;
			CloseSocket( nfd ) ;
			nfd = NULL;
		} else {
			sprintf( szbuf, "SZE P %s\r\n" , pkey.c_str() ) ;
			OUT_INFO( sock->_szIp, sock->_port, "Auth", "fd %d register key %s success", sock->_fd , pkey.c_str() ) ;
		}
		SendData( sock, szbuf, strlen(szbuf) ) ;

		// 发送一个心跳过去
		if ( nfd != NULL ) {
			sprintf( szbuf, "*%s|NOOP|%s#", user->udp._code.c_str(), user->udp._key.c_str() ) ;
			SendData( nfd , szbuf, strlen(szbuf) ) ;
		}
	}
	else if ( veck[1] == "N" )
	{ // 控制通道握手维护
		if ( _user_mgr.OnLoop( sock, val.c_str() ) ) {
			char buf[128] = {0};
			sprintf( buf, "SZE N %s\r\n", val.c_str() ) ;
			SendData( sock, buf, strlen(buf) ) ;
			OUT_INFO(sock->_szIp, sock->_port,  user->tcp._code.c_str() , "NOOP" ) ;
		} else {
			OUT_ERROR( sock->_szIp, sock->_port, user->tcp._code.c_str() , "fd %d , check NOOP error" , sock->_fd ) ;
		}
	}
}

// 处理UDP的数据
void PasServer::HandleUDPData( socket_t *sock, const char *data, int len )
{
	OUT_RECV( sock->_szIp, sock->_port, "PasServer", "UDP fd %d, Recv: %s", sock->_fd, data ) ;

	const char *ptr = strstr( data, "|" ) ;
	if ( ptr == NULL ) {
		OUT_ERROR( sock->_szIp, sock->_port,  "Recv", "fd %d data error", sock->_fd ) ;
		return ;
	}

	char srvid[128] = {0};
	memcpy( srvid, data+1, ptr- data -1 ) ;

	_PairUser *user = _user_mgr.GetUser( srvid ) ;
	if ( user == NULL ){
		OUT_ERROR( sock->_szIp, sock->_port,  srvid, "fd %d user not login", sock->_fd ) ;
		return ;
	}
	user->udp._active = time(NULL) ;
	user->udp._fd     = sock ;

	// 暂时只处理数据通道的心跳
	if ( strncmp( ptr+1 , "NOOP", 4 ) == 0 ) {
		char *begin = strstr( ptr+1, "|" ) ;
		if ( begin == NULL ) {
			OUT_ERROR( sock->_szIp, sock->_port,  srvid, "fd %d data error", sock->_fd ) ;
			return ;
		}
		char *end = strstr( begin+1, "#" ) ;
		if ( end == NULL || end <= begin+1 ) {
			OUT_ERROR( sock->_szIp, sock->_port, srvid, "fd %d data error", sock->_fd ) ;
			return ;
		}

		char pkey[128] = {0};
		memcpy( pkey, begin+1, end - begin-1 ) ;
		if ( strcmp( pkey, user->udp._key.c_str()) != 0 ) {
			OUT_ERROR( sock->_szIp, sock->_port, srvid, "fd %d data error", sock->_fd ) ;
		} else {
			SendData( sock, data, len ) ;
			OUT_INFO( sock->_szIp, sock->_port,  srvid, "fd %d NOOP", sock->_fd ) ;
		}
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
		sdata += _pEnv->GetPasClient()->GetSrvId() ;
		sdata += ptr ;
		if ( _pEnv->GetPasClient()->HandleData( sdata.c_str(), sdata.length() ) ){
			_statinfo->AddSend( sock->_szIp ) ;
		} else {
			PCCMSG_LOG( _pEnv->GetLogger(), sock->_szIp, sock->_port, srvid, "ERROR", "发送车牌号%s到服务器失败", base64.GetBuffer() ) ;
		}

		OUT_PRINT( sock->_szIp, sock->_port, srvid, "%s,UDP fd %d, HandleData: %s" , base64.GetBuffer(), sock->_fd, sdata.c_str() ) ;

		bool error = false ;
		char szmacid[512] = {0} ;
		if ( _pEnv->GetMsgClient()->GetCarMacId( base64.GetBuffer(), szmacid ) ) {
			CQString buf , msg ;
			if ( PConvert::buildintergps( vec, szmacid, buf , msg ) ) {
				// 转换回内部协议发送给MSG
				_pEnv->GetMsgClient()->HandleUpMsgData( SEND_ALL, buf.GetBuffer(), buf.GetLength() ) ;
				// 打印的数据
				OUT_INFO( sock->_szIp, sock->_port, srvid, "fd %d, PasServer %s, Send Data: %s", sock->_fd,  base64.GetBuffer(),  buf.GetBuffer() ) ;
			} else {
				error = true ;
				OUT_ERROR( sock->_szIp, sock->_port, srvid, "fd %d, %s, PasServer build inter gps macid %s", sock->_fd, base64.GetBuffer(), szmacid ) ;
				PCCMSG_LOG( _pEnv->GetLogger(), sock->_szIp, sock->_port, srvid, "ERROR", "车牌号%s,错误：%s", base64.GetBuffer() , msg.GetBuffer() ) ;
			}
		} else {
			error = true ;
			OUT_ERROR( sock->_szIp, sock->_port, srvid, "PasServer UDP fd %d, %s get macid failed", sock->_fd, base64.GetBuffer() ) ;
			//PCCMSG_LOG( _pEnv->GetLogger(), ip, port, srvid, "ERROR", "取得车牌号%s值对应关系失败", base64.GetBuffer() ) ;
		}
		_statinfo->AddVechile( sock->_szIp, base64.GetBuffer(), my_atoi(vec[4].c_str()) , error ) ;
	}
}

void PasServer::on_dis_connection( socket_t *sock )
{
	// Nothing do
}

// 接收到新连接到来处理
void PasServer::on_new_connection( socket_t *sock, const char* ip, int port)
{
	if ( check_udp_fd( sock ) )
		return ;
	// 添加到用户队列
	_user_mgr.AddUser( sock ) ;
}

// 从MSG转发过来的数据
void PasServer::HandleData( const char *data, int len )
{

}

// 关闭用户通知
void PasServer::CloseUser( socket_t *sock )
{
	// 关闭连接处理
	CloseSocket( sock ) ;
}

// 通知用户上线
void PasServer::NotifyUser( socket_t *sock , const char *key )
{
	char szbuf[128] = {0} ;
	sprintf( szbuf, "SZE L %s\r\n", key ) ;
	SendData( sock, szbuf, strlen(szbuf) ) ;
}

// 连接UDP的服务器
socket_t *PasServer::ConnectUDP( const char *ip, int port )
{
	socket_t *fd = _udp_handle.connect_nonb( ip , port , 10 ) ;
	if ( fd == NULL ) {
		OUT_ERROR( ip, port, "Conn", "connect remote udp server failed\n" ) ;
		return NULL;
	}
	return fd ;
}


