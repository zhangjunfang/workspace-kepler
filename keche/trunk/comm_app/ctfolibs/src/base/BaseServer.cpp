/**********************************************
 * BaseServer.cpp
 *
 *  Created on: 2010-6-24
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24 humingqing
 *    修改，主要去掉缓存队列，将线程对象使用自己写的线程池来处理数据
 *********************************************/

#include "BaseServer.h"
#include "comlog.h"
#include "Base64.h"

BaseServer::BaseServer(): _initalized(false) , _tcpinited ( false ) , _udpinited( false )
{
	_udp_listen_port = 0 ;
}

BaseServer::~BaseServer()
{
	StopServer() ;
}

bool BaseServer::StartServer(int port, const char* ip, unsigned int nthread, unsigned int timeout )
{
	_listen_ip    = ip;
	_listen_port  = port;

	_tcp_handle.init( nthread , timeout );
	if( !_tcp_handle.start_server( port, ip ) )
	{
		return false;
	}

	_initalized = true ;
	_tcpinited  = true ;

	if ( !_noop_thread.init( 1, (void*) THREAD_NOOP , this ) )
	{
		printf( "start noop thread failed, %s:%d", __FILE__, __LINE__ ) ;
		_initalized = false ;
		return false ;
	}
	_noop_thread.start() ;

	if ( !_time_thread.init( 1, (void*) THREAD_TIME , this ) )
	{
		printf( "time noop thread failed, %s:%d", __FILE__, __LINE__ ) ;
		_initalized = false ;
		_noop_thread.stop() ;
		return false ;
	}
	_time_thread.start() ;

	return true;
}

bool BaseServer::StartUDP( int port, const char * ip , unsigned int nthread , unsigned int timeout )
{
	_udp_listen_ip    = ip;
	_udp_listen_port  = port;

	_udp_handle.init( nthread , timeout ) ;
	if( !_udp_handle.start_server( port, ip ) ){
		printf( "start ip %s port %d falied, %s:%d\n", ip, port , __FILE__, __LINE__ ) ;
		return false;
	}

	_udpinited  = true ;

	return true ;
}


void BaseServer::StopServer( void )
{
	if ( ! _initalized )
		return ;

	_initalized = false ;

	_noop_thread.stop() ;
	_time_thread.stop() ;

	if ( _tcpinited ){
		_tcpinited = false ;
		_tcp_handle.uninit() ;
	}
	if ( _udpinited ) {
		_udpinited = false ;
		_udp_handle.uninit() ;
	}
}


void BaseServer::run( void *param )
{
	int n = 0 ;
	memcpy( &n , &param, sizeof(int) ) ;

	OUT_INFO( NULL, 0, NULL, "BaseServer::run %d", n ) ;

	switch( n )
	{
	case THREAD_TIME:
		TimeWork() ;
		break ;
	case THREAD_NOOP:
		NoopWork() ;
		break ;
	}
}

bool BaseServer::SendData( socket_t *sock , const char *data, int len)
{
	if ( sock == NULL ) return false ;

	bool ret = false ;
	if ( check_udp_fd(sock) ) {
		ret = _udp_handle.deliver_data( sock, ( const void *) data, len ) ;
	} else {
		ret = _tcp_handle.deliver_data( sock, (const void*)data, len ) ;
	}
	return ret;
}

void BaseServer::CloseSocket( socket_t *sock )
{
	if ( sock == NULL ) return ;

	if ( check_udp_fd( sock ) ) {
		_udp_handle.close_socket( sock ) ;
	} else {
		_tcp_handle.close_socket( sock );
	}
}


