/*
 * waitgroup.h
 *
 *  Created on: 2011-11-8
 *      Author: humingqing
 */

#ifndef __WAITGROUP_H__
#define __WAITGROUP_H__

#include <inodeface.h>
#include <map>
#include <msgbuilder.h>
#include <Thread.h>
#include <TQueue.h>

#define MAX_WAITGROUP_TIMEOUT  30  // 等待响应最长超时时间

class CAllocMsg : public IAllocMsg
{
public:
	CAllocMsg(){}
	~CAllocMsg(){}

	// 开辟内存
	MsgData * AllocMsg(){
		return new MsgData;
	}
	// 回收内存
	void FreeMsg( MsgData *p ){
		if ( p == NULL )
			return ;
		delete p ;
	}
};

class CWaitGroup : public IWaitGroup, public share::Runnable
{
	// 队列数据
	struct Queue
	{
		int        seq ;   // 序号ID号
		socket_t  *sock ;  // which fd cope
		time_t     now ;   // current time
		MsgData *  msg ;   // msg data
		ListFd     fdlst ; // notify fd
		Queue 	  *_next;
		Queue     *_pre ;
	};
	// 序号对应组处理
	typedef std::map<uint32_t,Queue*> CMapQueue ;
public:
	CWaitGroup(IAllocMsg *pAlloc) ;
	~CWaitGroup() ;

	// 初始化
	bool Init( void ) ;
	// 开始线程
	bool Start( void ) ;
	// 停止
	void Stop( void ) ;

	// 添加组的数据
	bool AddGroup( socket_t *sock, unsigned int seq, MsgData *msg ) ;
	// 添加到对应的组里面
	bool AddQueue( unsigned int seq, socket_t *sock ) ;
	// 删除对应的值
	bool DelQueue( unsigned int seq, socket_t *sock , bool bclear ) ;
	// 取得没有响应的FD
	int  GetCount( unsigned int seq ) ;
	// 取得对应的FD的LIST值
	bool GetList( unsigned int seq, ListFd &fds ) ;
	// 设置回调对象
	void SetNotify( IMsgNotify *pNotify ) { _notify = pNotify ; }
	// 线程运行对象
	void run( void *param ) ;

private:
	// 检测超时
	void CheckTime( void ) ;
	// 清除所有数据
	void ClearAll( void ) ;
	// 移除超时数据
	Queue* RemoveMsg( unsigned int seq ) ;

private:
	IAllocMsg		    *_pAlloc ;
	// 处理加锁操作
	share::Mutex  		 _mutex ;
	// 处理SEQ操作
	CMapQueue     		 _index ;
	// 时间索引
	TQueue<Queue>	  	 _queue ;
	// 线程对象
	share::ThreadManager _thread ;
	// 是否初始化线程
	bool 				 _inited ;
	// 通知回调对象
	IMsgNotify 			*_notify ;
};

#endif /* GROUPWAIT_H_ */
