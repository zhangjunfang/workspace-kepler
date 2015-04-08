/*
 * queuemgr.h
 *
 *  Created on: 2012-10-22
 *      Author: humingqing
 *
 *	memo:
 *  数据发送重发等待队列，如果数据到达后收到通用应答后清空，如果没有收到通用应答时超时需要重发
 *  其中，实现了可以将数据延后发送的机制，先将数据存放到数据队列中，如果数据不想立即发送，那么就需要等着超时或者响应来触发
 *
 *   1. 异步发送的数据等待队列，当接收到响应后清空对象,当等待响应超时时，需要重新发送
 *   2. 重新发送每一次等待超时时间，与次的时间为乘2的等待关系，也就是重发次数越多等待时间越长
 *   3. 如果终端处理能力有限，保证当需要发送数据直接入队列等待发送会顺序发送
 *   4. 当顺序发送时，平台收到终端上一个下发的响应后，自动触发下一个等待需要发送的消息
 *   5. 如果等待发送数据超时，平台会自动下发消息，如果有多个超时下发数据，则只发送单个数据，直到收到上一个应答或者再次超时再下发
 */

#ifndef __QUEUEMGR_H_
#define __QUEUEMGR_H_

#include <map>
#include <string>
#include <list>
#include <Thread.h>
#include <Monitor.h>
#include <sortqueue.h>

#define QUEUE_MAXRESEND   2   // 最大重发次数
#define QUEUE_SENDTIME    5   // 队列发送时间控制

// 数据回调对象
class IQCaller
{
public:
	virtual ~IQCaller() {}
	// 调用超时重发数据
	virtual bool OnReSend( void *data ) = 0 ;
	// 调用超时后重发次数据删除数据
	virtual void Destroy( void *data ) = 0 ;
};

// 数据队列管理对象
class CQueueMgr: public share::Runnable
{
	class CQueue
	{
		struct _QData
		{
			time_t  _time ;  // 最后操作的时间
			int     _ntime;  // 相对超时时间
			int     _ent ;   // 重发次数
			int     _seq ;   // 序号ID
			void *  _ptr ;   // 发送的数据内容
			_QData *_pre ;   // 前驱节点
			_QData *_next ;  // 后续节点
		};
		typedef std::map<int, _QData*> _QIndex;
	public:
		CQueue( IQCaller *pCaller , int ent ) ;
		~CQueue() ;
		// 添加数据
		bool Add( unsigned int seq, void *data, int timeout, bool send ) ;
		// 删除数据
		void Remove( unsigned int seq ) ;
		// 触发入队列的数据发送一次
		void Send( void ) ;
		// 检测超时的数据
		bool Check( int &nexttime ) ;

	private:
		// 添加到节点中
		void Add( _QData *p ) ;
		// 移除节点
		void Remove( _QData *p ) ;

	private:
		// 使用排序链表来完成
		TSortQueue<_QData> _queue ;
		// 数据队列索引
		_QIndex 		   _index ;
		// 数据处理回调对象
		IQCaller   *	   _pCaller;
		// 需要发送的个数
		int 			   _send ;
		// 最大重发次数
		int 	    	   _maxent ;

	public:
		// 数据队列的ID号
		std::string _id ;
		// 链表后一个元素接针
		CQueue    * _next ;
		// 链表前一个元素指针
		CQueue    * _pre ;
	};
	typedef std::map<std::string,CQueue*>  CMapQueue;

public:
	CQueueMgr( IQCaller *pCaller , int time = QUEUE_SENDTIME, int ent = QUEUE_MAXRESEND ) ;
	virtual ~CQueueMgr() ;
	// 添加到等待发送队列中，是否为延后由线程对象来发送,主要触发可以通过接收到响应后触发处理
	bool Add( const char *id, unsigned int seq, void *data , bool send = false ) ;
	// 删除ID号对象
	void Del( const char *id ) ;
	// 移除对象,是否检测需要触发消息发送
	void Remove( const char *id, unsigned int seq , bool check = true ) ;

protected:
	// 实现线程对象运行检测接口
	virtual void run( void *param ) ;
	// 清空的所有数据
	void Clear( void ) ;

private:
	// 同步等待对象
	share::Monitor       _monitor ;
	// 操作对象锁
	share::Mutex		 _mutex ;
	// 等待队列线程管理对象
	CMapQueue  			 _index ;
	// 线程执行对象
	share::ThreadManager _thread;
	// 是否初始化
	bool 				 _inited ;
	// 数据回调对象
	IQCaller		    *_pCaller ;
	// 使用队列模板
	TQueue<CQueue>		 _queue ;
	// 发送时间间隔
	int 				 _maxspan ;
	// 发送次数处理
	int 				 _maxent ;
};


#endif /* MYQUEUE_H_ */
