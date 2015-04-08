#ifndef __ALLOCMGR_H__
#define __ALLOCMGR_H__

#include <stdio.h>
#include <TQueue.h>
#include <Mutex.h>

// 队列开辟内存管理
template<typename T>
class TAllocMgr
{
public:
	TAllocMgr() {}
	~TAllocMgr() {}
	// 取得对象
	T * alloc( void ) {
		T *p = NULL ;

		_mutex.lock();
		if ( _queue.size() == 0 ) {
			_mutex.unlock() ;
			return NULL ;
		}
		p = _queue.begin() ;
		_queue.erase( p ) ;
		_mutex.unlock() ;

		return p ;
	}

	// 放入对象
	void free( T *obj ) {
		if ( obj == NULL )
			return ;

		_mutex.lock() ;
		_queue.push( obj ) ;
		_mutex.unlock() ;
	}

	// 内存队列长度
	int size( void ) {
		int nsize = 0 ;
		_mutex.lock() ;
		nsize = _queue.size() ;
		_mutex.unlock() ;
		return nsize ;
	}

private:
	// 同步操作锁
	share::Mutex	 _mutex;
	// 存放数据队列
	TQueue<T>		 _queue;
};

#endif
