/**
 * author: humingqing
 * date:   2011-09-08
 */
#ifndef __WAITQUEUE_H__
#define __WAITQUEUE_H__

#include <map>
#include <time.h>
#include <Mutex.h>
#include <TQueue.h>

#define TIMEOUT_SPAN    30

class IAllocObj
{
public:
	virtual ~IAllocObj() {}
	// 数据开辟方法
	virtual void*  AllocObj( void )  = 0 ;
	// 数据回收方法
	virtual void FreeObj( void* ptr ) = 0 ;
};

class CWaitQueue
{
	// 队列数据
	struct _QueueData
	{
		// 索引ID号
		int     _id ;
		// 时间
		time_t  _time ;
		// 数据指针
		void*  	_ptr ;
		// 双向指针
		_QueueData *_next ;
		_QueueData *_pre ;
	};
	typedef std::map<unsigned int, _QueueData*>   CWaitMap ;
public:
	CWaitQueue( IAllocObj *pObj )
	{
		_last_check = time(NULL) ;
		_AllocObj 	= pObj ;
	}
	~CWaitQueue() { ClearQueue() ; }

	// 添加到队列中
	bool AddQueue( unsigned int key, void* data )
	{
		share::Guard guard( _mutex ) ;
		{
			if ( _index.find( key ) != _index.end() ) {
				return false ;
			}

			_QueueData *p = new _QueueData;
			if ( p == NULL ) {
				return false ;
			}
			p->_id   = key ;
			p->_time = time(NULL) ;
			p->_ptr  = data ;

			_queue.push( p ) ;
			_index[key] = p ;

			return true ;
		}
	}

	// 签出数据
	void*  CheckQueue( unsigned int key )
	{
		share::Guard guard( _mutex ) ;
		{
			CWaitMap::iterator it = _index.find( key ) ;
			if ( it == _index.end() ) {
				return NULL ;
			}
			_QueueData *p = it->second ;
			void* temp = (void*)( p->_ptr ) ;
			_index.erase( it ) ;
			delete _queue.erase( p ) ;

			return temp ;
		}
	}

	// 移除超时的数据
	void RemoveTimeOut( int timeout )
	{
		share::Guard guard( _mutex ) ;
		{
			time_t now = time(NULL) ;
			if ( now - _last_check < TIMEOUT_SPAN ) {
				return ;
			}
			_last_check = now ;

			if ( _queue.size() == 0 ) {
				return ;
			}

			time_t t = now - timeout ;

			_QueueData *tmp = NULL ;
			_QueueData *p   = _queue.begin() ;
			while( p != NULL ) {
				tmp = p ;
				p   = p->_next ;
				if ( tmp->_time < t ) {
					// 清除数据
					RemoveData( tmp->_id ) ;
					continue ;
				}
				break ;
			}
		}
	}

private:
	// 移除数据
	void RemoveData( unsigned int key )
	{
		CWaitMap::iterator it = _index.find( key ) ;
		if ( it == _index.end() ) {
			return ;
		}
		_QueueData *p = it->second ;
		_index.erase( it ) ;
		_AllocObj->FreeObj( p->_ptr ) ;
		delete _queue.erase( p ) ;
	}

	// 回收所有内存
	void ClearQueue( void )
	{
		share::Guard guard( _mutex ) ;
		{
			int size = 0 ;
			_QueueData *p = _queue.move( size ) ;
			if ( size == 0 ) {
				return ;
			}

			_QueueData *t = NULL ;
			while( p != NULL ) {
				t = p ;
				p = p->_next ;
				_AllocObj->FreeObj( t->_ptr ) ;
				delete t ;
			}
			_index.clear() ;
		}
	}

private:
	// 等待队列
	CWaitMap   		   _index ;
	// 超时索引
	TQueue<_QueueData> _queue ;
	// 资源锁
	share::Mutex  	   _mutex ;
	// 内存管理对象
	IAllocObj	     * _AllocObj ;
	// 最后一次检测
	time_t			   _last_check ;
};

#endif
