/*
 * queuethread.cpp
 *
 *  Created on: 2012-6-19
 *      Author: humingqing
 */

#include "queuethread.h"

CQueueThread::CQueueThread( IPackQueue *queue, IQueueHandler *handler )
	:_queue(queue), _handler(handler), _inited(false)
{

}

CQueueThread::~CQueueThread()
{
	Stop() ;
}

//  初始化
bool CQueueThread::Init( int thread )
{
	if ( !_threadmgr.init( thread, NULL, this ) ) {
		return false ;
	}
	_inited = true ;
	_threadmgr.start() ;
	return true ;
}

// 停止
void CQueueThread::Stop( void )
{
	if ( ! _inited )
		return ;
	_inited = false ;
	_monitor.notifyEnd() ;
	_threadmgr.stop() ;
}

// 存放数据
bool CQueueThread::Push( void *packet )
{
	_mutex.lock() ;
	if ( ! _queue->Push( packet ) ) {
		_mutex.unlock() ;
		return false ;
	}
	_mutex.unlock() ;
	_monitor.notify() ;

	return true ;
}

// 处理数据
void CQueueThread::Process( void )
{
	void *p = NULL ;
	// 从数据队列中取数据
	_mutex.lock() ;
	p = _queue->Pop() ;
	_mutex.unlock() ;
	// 如果数据为空则续
	if ( p == NULL ) {
		_monitor.lock() ;
		_monitor.wait() ;
		_monitor.unlock() ;
		return ;
	}
	// 回调数据处理对象
	_handler->HandleQueue( p ) ;
	// 释放数据
	_queue->Free( p ) ;
}

// 线程运行接口对象
void CQueueThread::run( void *param )
{
	while( _inited ) {
		Process() ;
	}
}




