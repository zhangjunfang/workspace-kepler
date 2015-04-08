/*
 * KQueueHandle.h
 *
 *  Created on: 2012-3-24
 *      Author: humingqing
 */

#ifndef __KQUEUEHANDLE_H__
#define __KQUEUEHANDLE_H__

#include "SocketHandle.h"

#define KQUEUE_EVENTS_NUM	1024

#ifdef _UNIX
class CKQueueHandle : public CSocketHandle
{
public:
	CKQueueHandle();
	virtual ~CKQueueHandle();
public:
	//´´½¨
    bool create(int max_socket_num = MAX_SOCKET_NUM);
	bool destroy();

    bool add( socket_t *sock , unsigned int events);
	bool del( socket_t *sock , unsigned int events);
	bool modify( socket_t *sock, unsigned int events);
	int  poll(int timeout = 5000);

	//·µ»Øfd
    virtual void on_event( socket_t *sock ,int events){}

	virtual bool is_read(int events);
	virtual bool is_write(int events);
	virtual bool is_excep(int events);

private:
    int 	_handle;
};
#endif

#endif /* KQUEUEHANDLE_H_ */
