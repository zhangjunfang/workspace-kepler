/*
 * waitgroup.cpp
 *
 *  Created on: 2011-11-8
 *      Author: humingqing
 */
#include "waitgroup.h"
#include <comlog.h>

CWaitGroup::CWaitGroup(IAllocMsg *pAlloc) :
	_pAlloc( pAlloc ),_inited(false)
{
	_notify = NULL ;
}

CWaitGroup::~CWaitGroup()
{
	Stop() ;
	ClearAll() ;
}

// 初始化
bool CWaitGroup::Init( void )
{
	// 初始化线程对象
	if ( ! _thread.init( 1, this, this ) ) {
		OUT_ERROR( NULL, 0, "WaitGroup" , "start thread failed" ) ;
		return false ;
	}
	_inited = true ;
	return true ;
}

// 开始线程
bool CWaitGroup::Start( void )
{
	_thread.start() ;
	_inited = true ;
	return true ;
}

// 停止
void CWaitGroup::Stop( void )
{
	if ( ! _inited )
		return  ;

	_inited = false ;
	_thread.stop() ;
}

// 添加组的数据
bool CWaitGroup::AddGroup( socket_t *sock, unsigned int seq, MsgData *msg )
{
	share::Guard guard( _mutex ) ;
	{
		CMapQueue::iterator it = _index.find( seq ) ;
		if ( it != _index.end() )
			return false ;

		Queue *p = new Queue ;
		p->seq   = seq ;
		p->now   = time(NULL) ;
		p->msg   = msg ;
		p->sock  = sock ;

		_queue.push( p ) ;
		_index.insert( make_pair( seq, p ) ) ;

		return true ;
	}
}

// 添加到对应的组里面
bool CWaitGroup::AddQueue( unsigned int seq, socket_t *sock )
{
	share::Guard guard( _mutex ) ;
	{
		CMapQueue::iterator it = _index.find( seq ) ;
		if ( it == _index.end() )
			return false ;

		Queue *p = it->second ;
		p->fdlst.push_back( sock ) ;

		return true ;
	}
}

// 删除对应的值
bool CWaitGroup::DelQueue( unsigned int seq, socket_t *sock , bool bclear )
{
	bool bfind = false ;
	Queue *p = NULL ;
	{
		share::Guard guard( _mutex ) ;
		{
			CMapQueue::iterator it = _index.find( seq ) ;
			if ( it == _index.end() )
				return false ;

			p = it->second ;
			if ( ! p->fdlst.empty() ) {
				ListFd::iterator itx  ;
				for ( itx = p->fdlst.begin(); itx != p->fdlst.end(); ++ itx ) {
					if ( *itx != sock )
						continue ;
					p->fdlst.erase( itx ) ;
					bfind = true ;
					break ;
				}
			}
			// 是否为空
			if ( p->fdlst.empty() && bclear ) {
				// 移除索引处理
				_index.erase( it ) ;
				// 如果全部处理成功则直接移除
				p = _queue.erase( p ) ;
			}
		}
	}

	// 放到外面回调防止死锁操作
	if ( p->fdlst.empty() && bclear ) {
		// ToDo: NotifyMsg
		if ( _notify ) {
			// 回调成功处理
			_notify->NotifyMsgData( p->sock , p->msg , p->fdlst , MSG_COMPLETE ) ;
		}
		_pAlloc->FreeMsg( p->msg ) ;

		delete p ;
	}

	return bfind ;
}

// 取得没有响应的FD
int  CWaitGroup::GetCount( unsigned int seq )
{
	share::Guard guard( _mutex ) ;
	{
		CMapQueue::iterator it = _index.find( seq ) ;
		if ( it == _index.end() )
			return 0 ;

		Queue *p = it->second ;
		// 返回对应的数据
		return p->fdlst.size() ;
	}
}

// 取得对应的FD的LIST值
bool CWaitGroup::GetList( unsigned int seq, ListFd &fds )
{
	share::Guard guard( _mutex ) ;
	{
		CMapQueue::iterator it = _index.find( seq ) ;
		if ( it == _index.end() )
			return false ;

		Queue *p = it->second ;
		if ( p->fdlst.empty() )
			return false ;

		fds = p->fdlst ;
		// 返回对应的数据
		return true ;
	}
}

void CWaitGroup::run( void *param )
{
	while( _inited ) {
		// 检测超时请求数据
		CheckTime() ;
		sleep(5) ;
	}
}

// 检测超时
void CWaitGroup::CheckTime( void )
{
	if ( _queue.size() == 0 ) {
		return  ;
	}

	Queue *t,*p = NULL ;
	std::list<Queue*> lst ;
	time_t last = time(NULL) - MAX_WAITGROUP_TIMEOUT ;
	{
		// 加索操作超时数据
		share::Guard guard( _mutex ) ;
		{
			// 遍历所有索引数据
			p = _queue.begin() ;
			while( p != NULL ) {
				t = p ;
				p = p->_next ;
				if ( t->now > last )
					break ;

				RemoveMsg( t->seq ) ;

				t = _queue.erase( t ) ;
				if ( t != NULL ) {
					lst.push_back( t ) ;
				}
			}
		}
	}

	// 放到外面来回调处理防止死锁
	if ( ! lst.empty() ) {
		std::list<Queue*>::iterator itx ;
		for ( itx = lst.begin(); itx != lst.end(); ++ itx ) {
			p = ( *itx ) ;
			if ( _notify ) {
				// 回调超时处理
				_notify->NotifyMsgData( p->sock , p->msg , p->fdlst , MSG_TIMEOUT ) ;
			}
			_pAlloc->FreeMsg( p->msg ) ;

			delete p ;
		}
		lst.clear() ;
	}
}

CWaitGroup::Queue* CWaitGroup::RemoveMsg( unsigned int seq )
{
	CMapQueue::iterator it = _index.find( seq ) ;
	if ( it == _index.end() )
		return NULL;

	Queue *p = it->second ;
	_index.erase( it ) ;

	return p ;
}

// 清除所有数据
void CWaitGroup::ClearAll( void )
{
	share::Guard guard( _mutex ) ;
	{
		int size = 0 ;
		Queue *p = _queue.move(size) ;
		if ( size == 0 )
			return ;

		while( p != NULL ) {
			p = p->_next ;
			// 回收所有内存
			_pAlloc->FreeMsg( p->_pre->msg ) ;

			delete p->_pre ;
		}
		_index.clear() ;
	}
}
