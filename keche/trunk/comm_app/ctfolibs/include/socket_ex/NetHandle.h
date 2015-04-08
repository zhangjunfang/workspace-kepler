/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 下午 3:41:41
  * Description: 
  * Modification: 
  **/

#ifndef __NETHANDLE_H__
#define __NETHANDLE_H__

#include <TcpHandleEx.h>
#include <UdpHandleEx.h>

enum { PROTOCOL_TCP = 0, PROTOCOL_UDP = 1, PROTOCOL_TCP_AND_UDP = 2 };

class CNetHandle 
{
public:
	CNetHandle();
	virtual ~CNetHandle();

public:
	virtual void on_data_arrived( socket_t *sock, const void* data, int len) = 0 ;
	//此函数中不允许调用closesocket!!
	virtual void on_dis_connection( socket_t *sock ) = 0 ;
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port) = 0 ;
	// 发送失败数据处理
	virtual void on_send_failed( socket_t *sock, void* data, int len) {} ;
	// 检测是否有效的合法IP
	virtual bool check_ip( const char *ip ) { return true ; }
	// 根据sock标志位来进行检测是否为UDP
	virtual bool check_udp_fd( socket_t *sock )
	{
		if (sock != NULL)
			return ( sock->_type == FD_UDP ) ;
		return false ;
	}
public:
	// 设制数据分包对象
	void setpackspliter( IPackSpliter *pack ) ;

protected:
	CUdpHandleEx	_udp_handle;
	CTcpHandleEx	_tcp_handle;
};

#endif
