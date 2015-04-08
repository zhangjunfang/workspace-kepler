/**********************************************
 * BaseClient.cpp
 *
 *  Created on: 2010-6-24
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24 humingqing
 *    修改，主要去掉缓存队列，将线程对象使用自己写的线程池来处理数据
 *********************************************/

#include "BaseClient.h"
#include "comlog.h"
#include "Base64.h"

BaseClient::BaseClient() : _initalized( false ) , _udp_inited( false ) , _tcp_inited( false ){

}

BaseClient::~BaseClient(){
	StopClient() ;
}

bool BaseClient::StartUDP( const char * connect_ip, int connect_port, int thread , unsigned int timeout )
{
	_udp_handle.init( thread , timeout ) ;

	_udp_user._ip   = connect_ip ;
	_udp_user._port = connect_port ;

	_udp_inited = true ;

	return true ;
}

bool BaseClient::StartClient(const char* connect_ip, int connect_port, int thread , unsigned int timeout )
{
	_tcp_handle.init( thread , timeout );

	_client_user._ip   = connect_ip;
	_client_user._port = connect_port;

	_initalized = true ;
	_tcp_inited = true ;

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

void BaseClient::StopClient( void )
{
	if ( ! _initalized )
		return ;

	_initalized = false ;

	_noop_thread.stop() ;
	_time_thread.stop() ;

	if ( _tcp_inited ) {
		_tcp_inited = false ;
		_tcp_handle.uninit() ;
	}
	if ( _udp_inited ) {
		_udp_inited = false ;
		_udp_handle.uninit() ;
	}
}

bool BaseClient::ConnectServer(User &user, unsigned int timeout)
{
//	FUNTRACE("bool PasServer::ConnectServer(const char* ip, int port)");
	if(time(0) - user._connect_info.last_reconnect_time < user._connect_info.timeval)
		return false;

	bool ret = false;
	if (user._fd)
		CloseSocket((socket_t*)user._fd);

	user._fd = _tcp_handle.connect_nonb(user._ip.c_str(), user._port, timeout);
	ret = (user._fd) ? true:false;

	if(ret )
	{
		char buffer[1024] = {0};
		int msg_len = build_login_msg( user, buffer, 1024 );
		assert( msg_len > 0 ) ;
		OUT_INFO(user._ip.c_str(),user._port,user._user_id.c_str(),"connect success,send login message:%s",buffer);
		SendData((socket_t*)user._fd,buffer,msg_len);
	}
	else
	{
		user._user_state = User::OFF_LINE;
	}
	user._last_active_time = time(0);
	user._login_time       = time(0);
	user._connect_info.last_reconnect_time = time(0);
	if(user._connect_info.keep_alive == ReConnTimes)
		user._connect_info.reconnect_times--;

	return ret;
}

void BaseClient::run( void *param )
{
	int n = 0 ;
	memcpy( &n , &param, sizeof(int) ) ;

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

void BaseClient::TimeWork()
{
}

void BaseClient::NoopWork()
{
}

void BaseClient::on_data_arrived( socket_t *sock , const void* data, int len)
{
	string send_buf = "Hello I've received your message\n";
	SendData( sock,send_buf.c_str(),send_buf.length());
}


bool BaseClient::SendData( socket_t *sock , const char* data, int len)
{
	if ( sock == NULL ) return false ;

	bool ret = false ;
	if ( check_udp_fd( sock ) ){
		ret = _udp_handle.deliver_data( sock, (const void*)data, len);
	} else {
		ret = _tcp_handle.deliver_data( sock, (const void*)data, len);
	}
	return ret;
}

bool BaseClient::CloseSocket( socket_t *sock )
{
	if ( sock == NULL ) return false ;

	if ( check_udp_fd( sock ) ) {
		_udp_handle.close_socket( sock ) ;
	} else {
		_tcp_handle.close_socket( sock );
	}
	return true;
}
