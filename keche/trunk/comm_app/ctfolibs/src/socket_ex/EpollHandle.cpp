#include "EpollHandle.h"
#include <unistd.h>
#include <fcntl.h>
#include <sys/resource.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <errno.h>
#include <string.h>
#include "UtilitySocket.h"
#include <comlog.h>

#ifndef _UNIX

#ifdef _EPOLL_ET
#define EPOLLMODE   EPOLLET
#else
#define EPOLLMODE   0
#endif

CEpollHandle::CEpollHandle()
{
	_handle = -1;
}

CEpollHandle::~CEpollHandle()
{

}

bool CEpollHandle::create(int max_socket_num /*= MAX_SOCKET_NUM*/)   
{  
    _handle = epoll_create(max_socket_num); 
    if( _handle == -1 ){
        return false;   
    }
    return true ;
}

bool CEpollHandle::destroy()
{
	if ( _handle >= 0 ){
		close(_handle);
		return true ;
	}
	return false;
}

int CEpollHandle::poll(int timeout /*= 5000*/)
{
	epoll_event events[EPOLL_EVENTS_NUM];

	int fd_num = epoll_wait(_handle, events, EPOLL_EVENTS_NUM, timeout);
	for( int i = 0 ; i < fd_num; ++ i ) {
		// 交出数据
		on_event( (socket_t *)events[i].data.ptr, events[i].events ) ;
	}

	return fd_num ;
}

bool CEpollHandle::add(socket_t *sock, unsigned int events)
{   
	epoll_event e_event;  

	e_event.data.fd  = sock->_fd ;
	e_event.data.ptr = sock ;
	e_event.events   = EPOLLMODE;
	if ( events & ReadableEvent )
		e_event.events |= EPOLLIN;
	if ( events & WritableEvent )
		e_event.events |= EPOLLOUT;
	sock->_events = events ;
	
	int r = ctl(EPOLL_CTL_ADD, sock->_fd , e_event);

	return r == 0;
}  

bool CEpollHandle::del(socket_t *sock, unsigned int events)
{   
	epoll_event e_event;  
	 
	e_event.data.fd  = sock->_fd;
	e_event.events   = 0;
	e_event.data.ptr = sock ;
	if ( events & ReadableEvent )
		e_event.events |= EPOLLIN;
	if ( events & WritableEvent )
		e_event.events |= EPOLLOUT;
	sock->_events = events ;

	return ctl(EPOLL_CTL_DEL, sock->_fd , e_event) == 0;
}   

bool CEpollHandle::modify(socket_t *sock, unsigned int events)
{   
	epoll_event e_event;  
	  
	e_event.data.fd  = sock->_fd ;
	e_event.data.ptr = sock ;
	e_event.events   = EPOLLMODE;
	if ( events & ReadableEvent ) {
		e_event.events |= EPOLLIN;
	}
	if ( events & WritableEvent ) {
		e_event.events |= EPOLLOUT;
	}
	sock->_events = events ;

	return ctl(EPOLL_CTL_MOD, sock->_fd , e_event) == 0;
}

int CEpollHandle::ctl(int op, int fd, struct epoll_event &event)   
{   
	int ret = epoll_ctl(_handle, op, fd, &event);   
	if( ret != 0 ){
        // OUT_ERROR(NULL, 0, NULL, "epoll_ctl failed , ret is %d, fd %d , errno %d, %s", ret, fd, errno , strerror(errno) );
	}  

	return ret;   
}  

bool CEpollHandle::is_read(int events)
{
	return events & EPOLLIN;
}

bool CEpollHandle::is_write(int events)
{
	return events & EPOLLOUT;
}

bool CEpollHandle::is_excep(int events)
{
	return false;
}

#endif
