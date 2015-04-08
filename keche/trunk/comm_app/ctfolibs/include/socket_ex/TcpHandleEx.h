/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 下午 3:42:06
  * Description: 
  * Modification: 
  **/
#ifndef __TCPHANDLE_EX_H__
#define __TCPHANDLE_EX_H__

#include "TcpHandle.h"

class CTcpHandleEx : public CTcpHandle
{
public:
	CTcpHandleEx();
	~CTcpHandleEx();
public:
	void set_owner(void* owner);
	virtual void on_data_arrived( socket_t *sock, void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock, const char* ip, int port);
	virtual void on_send_failed( socket_t *sock, void* data, int len) ;
	// 检测IP是否有效
	virtual bool invalidate_ip( const char *ip ) ;
private:
	void* _owner;
};

#endif


