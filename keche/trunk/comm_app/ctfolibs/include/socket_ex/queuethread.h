/*
 * queuethread.h
 *
 *  Created on: 2012-6-19
 *      Author: humingqing
 *  数据处理队列线程对象
 */

#ifndef __QUEUETHREAD_H__
#define __QUEUETHREAD_H__

#include <Monitor.h>
#include <Thread.h>

// 数据包队列
class IPackQueue
{
public:
	virtual ~IPackQueue() {}
	// 存放数据
	virtual bool Push( void *packet ) = 0 ;
	// 弹出数据
	virtual void * Pop( void ) = 0 ;
	// 释放数据
	virtual void  Free( void *packet ) = 0 ;
};

// 数据传送接口
class IQueueHandler
{
public:
	virtual ~IQueueHandler() {}
	// 交出数据回调接口
	virtual void HandleQueue( void *packet ) = 0 ;
};

// 线程数据处理队列
class CQueueThread : public share::Runnable
{
public:
	CQueueThread( IPackQueue *queue, IQueueHandler *handler ) ;
	~CQueueThread() ;
	//  初始化
	bool Init( int thread ) ;
	// 停止
	void Stop( void ) ;
	// 存放数据
	bool Push( void *packet ) ;

public:
	// 线程运行接口对象
	void run( void *param ) ;
	// 处理数据
	void Process( void ) ;

private:
	// 数据存放队列
	IPackQueue 			 *_queue ;
	// 数据回调接口
	IQueueHandler   	 *_handler ;
	// 数据同步锁操作
	share::Mutex		  _mutex ;
	// 信号管理对象
	share::Monitor		  _monitor ;
	// 线程管理对象
	share::ThreadManager  _threadmgr ;
	// 是否初始化数据
	bool 				  _inited ;
};

#endif /* QUEUETHREAD_H_ */
