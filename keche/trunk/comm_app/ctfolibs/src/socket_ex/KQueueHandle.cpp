/*
 * KQueueHandle.cpp
 *
 *  Created on: 2012-3-24
 *      Author: humingqing
 */
#include "KQueueHandle.h"
#include <comlog.h>
#include <poll.h>
#include <UtilitySocket.h>

#ifdef _UNIX
#include <sys/types.h>
#include <sys/event.h>
#include <sys/time.h>
#endif

#ifdef _UNIX
CKQueueHandle::CKQueueHandle()
{
	_handle = -1;
}

CKQueueHandle::~CKQueueHandle()
{

}

bool CKQueueHandle::create(int max_socket_num /*= MAX_SOCKET_NUM*/)
{
    _handle = kqueue();
    if( _handle == -1 ){
        return false;
    }
    return true ;
}

bool CKQueueHandle::destroy()
{
	if ( _handle >= 0 ){
		close(_handle);
		return true ;
	}
	return false;
}

int CKQueueHandle::poll(int nval /*= 5000*/)
{
	struct kevent events[KQUEUE_EVENTS_NUM] ;

	timespec ts = {nval / 1000, (nval % 1000) * 1000000};
	int numevents = kevent(_handle, NULL, 0, events, KQUEUE_EVENTS_NUM, ( (nval>0)? &ts: NULL ) );

	if ( numevents > 0) {
		for(int j = 0; j < numevents; ++ j) {
			int mask = 0;
			struct kevent *e = &events[j];

			if (e->filter == EVFILT_READ) mask |= EVFILT_READ;
			if (e->filter == EVFILT_WRITE) mask |= EVFILT_WRITE;

			// 交出数据
			on_event( (socket_t*) e->udata , mask ) ;
		}
	}
	return numevents;
}

bool CKQueueHandle::add( socket_t *sock , unsigned int events)
{
	int err = 0 ;
	struct kevent ke;
	if (events & ReadableEvent) {
		EV_SET(&ke, sock->_fd, EVFILT_READ, EV_ADD, 0, 0, sock);
		if (kevent(_handle, &ke, 1, NULL, 0, NULL) == -1)
			++ err;
	}
	if (events & WritableEvent) {
		EV_SET(&ke, sock->_fd, EVFILT_WRITE, EV_ADD, 0, 0, sock);
		if (kevent(_handle, &ke, 1, NULL, 0, NULL) == -1)
			++ err;
	}
	sock->_events |= events ;

	return ( err != 2 ) ;
}

bool CKQueueHandle::del(socket_t *sock, unsigned int events)
{
	int err = 0 ;
	struct kevent ke;
	if (events & ReadableEvent) {
		EV_SET(&ke, sock->_fd, EVFILT_READ, EV_DELETE, 0, 0, sock);
		if ( kevent(_handle, &ke, 1, NULL, 0, NULL) == -1 )
			++ err ;
	}
	if (events & WritableEvent) {
		EV_SET(&ke, sock->_fd, EVFILT_WRITE, EV_DELETE, 0, 0, sock);
		if ( kevent(_handle, &ke, 1, NULL, 0, NULL) == -1 )
			++ err ;
	}
	sock->_events &= ~events;

	return ( err != 2 ) ;
}

bool CKQueueHandle::modify( socket_t *sock , unsigned int events)
{
	int err = 0 ;

	struct kevent ke;

	if(events & ReadableEvent) {
		EV_SET(&ke, sock->_fd, EVFILT_READ, EV_ADD , 0, 0, sock );
		sock->_events |= ((unsigned int)ReadableEvent);
	} else {
		EV_SET(&ke, sock->_fd, EVFILT_READ, EV_DELETE , 0, 0, sock );
		sock->_events &= ~((unsigned int)ReadableEvent);
	}
	if (kevent(_handle, &ke, 1, NULL, 0, NULL) == -1)
		++ err ;

	if ( events & WritableEvent ) {
		EV_SET(&ke, sock->_fd, EVFILT_WRITE, EV_ADD , 0, 0, sock );
		sock->_events |= ((unsigned int)WritableEvent) ;
	} else {
		EV_SET(&ke, sock->_fd, EVFILT_WRITE, EV_DELETE , 0, 0, sock );
		sock->_events &= ~((unsigned int)WritableEvent) ;
	}
	if (kevent(_handle, &ke, 1, NULL, 0, NULL) == -1)
		++ err ;

	return ( err != 2 ) ;
}

bool CKQueueHandle::is_read(int events)
{
	return events & EVFILT_READ;
}

bool CKQueueHandle::is_write(int events)
{
	return events & EVFILT_WRITE;
}

bool CKQueueHandle::is_excep(int events)
{
	return false;
}

#endif



