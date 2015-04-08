/*
 * queuemgr.cpp
 *
 *  Created on: 2012-10-22
 *      Author: humingqing
 *
 *  memo: 主要实现的功能
 *   1. 异步发送的数据等待队列，当接收到响应后清空对象,当等待响应超时时，需要重新发送
 *   2. 重新发送每一次等待超时时间，与次的时间为乘2的等待关系，也就是重发次数越多等待时间越长
 *   3. 如果终端处理能力有限，保证当需要发送数据直接入队列等待发送会顺序发送
 *   4. 当顺序发送时，平台收到终端上一个下发的响应后，自动触发下一个等待需要发送的消息
 *   5. 如果等待发送数据超时，平台会自动下发消息，如果有多个超时下发数据，则只发送单个数据，直到收到上一个应答或者再次超时再下发
 */

#include "queuemgr.h"
#include <assert.h>

CQueueMgr::CQueue::CQueue( IQCaller *pCaller , int ent )
{
	_pCaller = pCaller ;
	_send 	 = 0 ;
	_next    = _pre  = NULL ;
	_maxent  = ent ;
}

CQueueMgr::CQueue::~CQueue()
{
	int size = 0 ;
	_QData *p = _queue.move(size) ;
	if ( size == 0 )
		return ;

	while( p->_next != NULL ) {
		p = p->_next ;
		_pCaller->Destroy( p->_pre->_ptr ) ;
		delete p->_pre ;
	}
	if ( p != NULL ) {
		_pCaller->Destroy( p->_ptr ) ;
		delete p ;
	}

	_index.clear() ;
	_send = 0 ;
}

// 添加数据
bool CQueueMgr::CQueue::Add( unsigned int seq, void *data, int timeout, bool send )
{
	// 先移除原有的数据
	Remove( seq ) ;

	_QData *p = new _QData ;
	assert( p != NULL ) ;

	p->_seq   = seq ;
	p->_ntime = timeout ;
	p->_time  = time(NULL) + timeout ;
	p->_ptr   = data ;

	if ( send ) {
		p->_ent = -1 ;
	} else {
		p->_ent = _maxent ;
	}

	Add( p ) ;

	_index.insert( std::make_pair(seq, p ) ) ;

	return true ;
}

// 添加到节点中
void CQueueMgr::CQueue::Add( _QData *p )
{
	_queue.insert( p ) ;

	if ( p->_ent < 0 )
		_send = _send + 1 ;

	// printf( "add seq id %d, size %d, send %d \n", p->_seq , _size, _send ) ;
}

// 删除数据
void CQueueMgr::CQueue::Remove( unsigned int seq )
{
	_QIndex::iterator it = _index.find( seq ) ;
	if ( it == _index.end() )
		return ;

	_QData *p = it->second ;
	_index.erase( it ) ;

	Remove( p ) ;

	_pCaller->Destroy( p->_ptr ) ;
	delete p ;
}

// 移除所有的数据节点
void CQueueMgr::CQueue::Remove( _QData *p )
{
	// printf( "remove seq id %d\n", p->_seq ) ;
	if ( p->_ent < 0 )
		_send = _send - 1 ;

	_queue.erase( p ) ;
}

// 触发入队列的数据发送一次
void CQueueMgr::CQueue::Send( void )
{
	if ( _send == 0 )
		return ;

	_QData *p = _queue.begin() ;
	while( p != NULL ) {
		if ( p->_ent < 0 )
			break ;
		p = _queue.next(p) ;
	}

	if ( p != NULL ) {
		Remove( p ) ;
		p->_ent  = _maxent ;
		p->_time = time(NULL) + p->_ntime ;
		Add( p ) ;

		_pCaller->OnReSend( p->_ptr ) ;

		// printf( "send seq %d\n", p->_seq ) ;
	}
}

// 检测超时的数据
bool CQueueMgr::CQueue::Check( int &nexttime )
{
	if ( _queue.size() == 0 )
		return false;

	time_t now = time(NULL) ;

	_QData *t,*p  = _queue.begin() ;

	int nsend = 0 ;
	while ( p != NULL ) {
		if ( p->_time > now )
			break ;
		t = p ;
		p = _queue.next(p) ;

		// 先从前面移走的数据
		Remove( t ) ;

		// 如果为等待发送的数据，超时发送按顺序单个发送
		if ( t->_ent < 0 ) {
			if ( nsend == 0 ) {// 调用发送接口发送数据
				_pCaller->OnReSend( t->_ptr ) ;
				t->_ent = _maxent ;
				nsend = nsend + 1 ;
			}
		} else { // 否则为等待响应发送的数据
			// 调用发送接口发送数据
			_pCaller->OnReSend( t->_ptr ) ;

			t->_ntime = t->_ntime * 2 ; // 每一次等待时间随次数减少而增加的
			t->_ent   = t->_ent - 1 ;
		}
		t->_time  = now + t->_ntime ;

		// 如果发送次为零就不再处理
		if ( t->_ent == 0 ) {  // 移除超时的元素，不重发也不再等待
			_index.erase( t->_seq ) ;
			_pCaller->Destroy( t->_ptr ) ;
			delete t ;
		} else {
			Add( t ) ; // 按时间序列添加队列中
		}
	}
	if ( _queue.size() == 0 )
		return false ;

	// 取得最近发送需要等待的时间
	nexttime = _queue.begin()->_time - now ;

	return true ;
}

CQueueMgr::CQueueMgr( IQCaller *pCaller , int time, int ent )
{
	_maxspan = time ;
	_maxent  = ent ;

	_pCaller = pCaller ;
	if ( ! _thread.init( 1, NULL, this ) ) {
		printf( "init queue thread failed\n" ) ;
		return ;
	}
	_inited = true ;
	_thread.start() ;
}

CQueueMgr::~CQueueMgr()
{
	Clear() ;

	if ( ! _inited )
		return ;

	_inited = false ;
	_monitor.notifyEnd() ;
	_thread.stop() ;
}

// 添加到等待发送队列中，是否为延后由线程对象来发送,主要触
bool CQueueMgr::Add( const char *id, unsigned int seq, void *data , bool send )
{
	_mutex.lock() ;

	CQueue *p = NULL ;
	CMapQueue::iterator it = _index.find( id ) ;
	if ( it == _index.end() ) {
		p = new CQueue(_pCaller, _maxent ) ;
		p->_id = id ;
		_queue.push( p ) ;
		_index.insert( std::make_pair(id, p ) ) ;
	} else {
		p = it->second ;
	}
	bool success = p->Add( seq, data, _maxspan , send ) ;

	_mutex.unlock() ;

	_monitor.notify() ;

	return success ;
}

// 删除ID号对象
void CQueueMgr::Del( const char *id )
{
	_mutex.lock() ;
	CMapQueue::iterator it = _index.find( id ) ;
	if ( it == _index.end() ) {
		_mutex.unlock() ;
		return ;
	}
	CQueue *p = it->second ;
	_index.erase( it ) ;
	delete _queue.erase( p ) ;
	_mutex.unlock() ;
}

// 移除对象
void CQueueMgr::Remove( const char *id, unsigned int seq , bool check )
{
	_mutex.lock() ;
	CMapQueue::iterator it = _index.find( id ) ;
	if ( it == _index.end() ) {
		_mutex.unlock() ;
		return ;
	}
	// 收到响应应答后清空等待数据
	it->second->Remove( seq ) ;
	if ( check ) { // 发送等待需要发送的数据
		it->second->Send() ;
	}
	_mutex.unlock() ;

	_monitor.notify() ;
}

// 实现线程对象运行检测接口
void CQueueMgr::run( void *param )
{
	while( _inited ) {

		int ntime = 30 ;
		_mutex.lock() ;
		if( _queue.size() > 0 ) {
			int nval = 0 ;
			CQueue *t, *p = _queue.begin() ;
			while( p != NULL ){
				t = p ;
				p = _queue.next( p ) ;

				// 检测数据是否需要发送
				if ( ! t->Check( nval ) ) {
					// 移除数据
					_queue.erase( t ) ;
					// 删队索引元素
					_index.erase( t->_id ) ;
					delete t ;
					continue ;
				}

				// 计算出队列元素最近需要处理消息时间
				if ( nval < ntime ) {
					ntime = nval ;
				}
			}
		}
		_mutex.unlock() ;

		// printf( "run time over %d, size %d\n", ntime , _size ) ;

		ntime = ( ntime <= 0 ) ? 1 : ntime ;
		_monitor.lock() ;
		_monitor.wait(ntime) ;
		_monitor.unlock() ;
	}
}

// 清空所有数据
void CQueueMgr::Clear( void )
{
	_mutex.lock() ;

	int size = 0 ;
	CQueue *p = _queue.move( size ) ;
	if ( size == 0 ) {
		_mutex.unlock() ;
		return ;
	}

	while( p != NULL ) {
		p = p->_next ;
		delete p->_pre ;
	}
	_index.clear() ;
	_mutex.unlock() ;
}

