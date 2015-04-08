/**
  * Name: 
  * Copyright: 
  * Author: lizp.net@gmail.com
  * Date: 2009-11-3 обнГ 3:42:18
  * Description: 
  * Modification: 
  **/
#ifndef __UDPHANDLE_EX_H__
#define __UDPHANDLE_EX_H__

#include "UdpHandle.h"

class CUdpHandleEx : public CUdpHandle
{
public:
	CUdpHandleEx();
	~CUdpHandleEx();
public:
	void set_owner(void* owner);
	virtual void on_data_arrived( socket_t *sock, void* data, int len);
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock , const char* ip, int port);
private:
	void* _owner;

};

#endif





