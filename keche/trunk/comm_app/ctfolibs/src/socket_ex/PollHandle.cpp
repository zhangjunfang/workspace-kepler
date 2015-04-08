/*
 * PollHandle.cpp
 *
 *  Created on: 2012-7-1
 *      Author: humingqing
 */

#include "PollHandle.h"
#include <UtilitySocket.h>
#include <comlog.h>

#ifdef _USE_POLL
#include <unistd.h>
#include <fcntl.h>
#include <sys/resource.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <errno.h>
#include <sys/socket.h>
#include <poll.h>
#include <string.h>
#include <TQueue.h>

// =============================== pollarray_t =========================================
// POLL管理对象
class pollarray_t
{
	struct pollfd_t
	{
		pollfd 	  _pfd ;
		socket_t *_sock ;
		pollfd_t *_next ;
		pollfd_t *_pre  ;
	};
	typedef std::map<socket_t*,pollfd_t*> mapfd_t ;
public:
	pollarray_t() {}
	~pollarray_t() { _mparray.clear() ;}
	// 添加POLL
	bool addpoll( socket_t *sock, unsigned int events ) {
		if ( sock->_fd <= 0 ) return false ;

		pollfd_t *p = new pollfd_t ;
		/*|POLLWRBAND|POLLWRNORM |POLLRDNORM|POLLRDBAND|POLLPRI|POLLOUT*/
		p->_pfd.fd 	  = sock->_fd ;
		p->_pfd.events  = POLLERR | POLLHUP ; // 暂时不支持写事件，因为这样就不会出阻塞的Poll操作
		if ( events & WritableEvent )
			p->_pfd.events |= POLLOUT ;
		if ( events & ReadableEvent )
			p->_pfd.events |= POLLIN ;

		p->_pfd.revents = 0 ;
		p->_sock = sock ;
		p->_next = p->_pre = NULL ;

		_mparray[p->_sock]  = p ;

		_pollqueue.push( p ) ;

		return true ;
	}
	// 修改POLL
	bool editpoll( socket_t *sock, unsigned int events ) {
		if ( sock->_fd <= 0 ) return false ;

		pollfd_t *p = findfd( sock, false ) ;
		if ( p == NULL )
			return false ;

		p->_pfd.events = POLLERR | POLLHUP ;
		if ( events & WritableEvent )
			p->_pfd.events |= POLLOUT ;
		if ( events & ReadableEvent )
			p->_pfd.events |= POLLIN ;
		p->_sock = sock ;

		return true ;
	}
	// 删除POLL
	bool delpoll( socket_t *sock, unsigned int events ) {
		if ( sock->_fd <=0 ) return false ;

		pollfd_t *p = findfd( sock, true ) ;
		if ( p == NULL ) {
			return false ;
		}
		// 移除结点
		delete _pollqueue.erase( p ) ;
		return true;
	}
	// 取得所有POLL
	int  pollarray( pollset_t &fdarray , vecsocket_t &vec ) {
		int size = _pollqueue.size() ;
		if ( size == 0 ) {
			return 0 ;
		}

		pollfd_t *p = _pollqueue.begin() ;
		while( p != NULL) {
			fdarray.push_back( p->_pfd ) ;
			vec.push_back( p->_sock ) ;
			p = _pollqueue.next( p ) ;
		}
		return size ;
	}

private:
	// 查找FD资源
	pollfd_t * findfd( socket_t *sock, bool erase ) {
		mapfd_t::iterator it = _mparray.find( sock ) ;
		if ( it == _mparray.end() ) {
			return NULL;
		}
		pollfd_t *p = it->second ;
		if ( erase )
			_mparray.erase( it ) ;
		return  p ;
	}

private:
	// 针对查找的索引
	mapfd_t 		 _mparray   ;
	// 数据链表指针
	TQueue<pollfd_t> _pollqueue ;
};

//===========================PollHandle==================================

CPollHandle::CPollHandle()
{
	_pollarray = new pollarray_t ;
}

CPollHandle::~CPollHandle()
{
	delete _pollarray ;
}

//创建
bool CPollHandle::create( int max_socket_num )
{
	return true ;
}

bool CPollHandle::destroy()
{
	return true ;
}

bool CPollHandle::add( socket_t *sock , unsigned int events)
{
	if ( !_pollarray->addpoll( sock, events ) ) {
		return false ;
	}
	sock->_events |= events ;

	return true ;
}

bool CPollHandle::del( socket_t *sock , unsigned int events)
{
	if ( ! _pollarray->delpoll( sock, events ) ) {
		return false ;
	}
	sock->_events &=~events ;

	return true ;
}

bool CPollHandle::modify( socket_t *sock, unsigned int events)
{
	if( !_pollarray->editpoll( sock, events ) ) {
		return false ;
	}
	sock->_events = events ;
	return true ;
}

int CPollHandle::pollarray( pollset_t &fdarray , vecsocket_t &vec )
{
	return _pollarray->pollarray( fdarray , vec ) ;
}

int CPollHandle::poll( int timeout )
{
	// 这里用到多线程使用和回调处理，为了避免死锁只好先拷一份出来处理
	pollset_t fdarray ;
	vecsocket_t vec;
	int fdcount = pollarray( fdarray , vec ) ;
	if ( fdcount == 0 ) {
		return -1 ;
	}

	int result  = ::poll( &fdarray[0] , fdcount , timeout ) ;
	if ( result > 0 ) {
		for (int i=0 ; i< fdcount; ++ i )
		{
			int event = 0 , revent = fdarray[i].revents ;
			// 检测POLL中的读事件
			if ( revent & ( POLLIN | POLLERR | POLLHUP )  ){
				event |= ReadableEvent ;
			}

			// 检测POLL中的写事件
			if ( revent & POLLOUT ) {
				event |= WritableEvent ;
			}
			if ( event != 0 )
				on_event( vec[i] , event );
		}
	}
	return result ;
}

bool CPollHandle::is_read(int events)
{
	return events & ReadableEvent ;
}

bool CPollHandle::is_write(int events)
{
	return events & WritableEvent ;
}

bool CPollHandle::is_excep(int events)
{
	return false ;
}

#endif
