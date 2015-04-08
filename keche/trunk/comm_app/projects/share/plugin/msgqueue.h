/*
 * msgqueue.h
 *
 *  Created on: 2012-6-1
 *      Author: humingqing
 *  消息队列处理
 */

#ifndef __MSGQUEUE_H__
#define __MSGQUEUE_H__

#include <map>
#include <string>
#include <Thread.h>
#include <Monitor.h>
#include <time.h>
#include "msgpack.h"
#include <TQueue.h>

// 消息回调对象
class IMsgCaller
{
public:
	virtual ~IMsgCaller(){}
	// 消息超时处理
	virtual void OnTimeOut( unsigned int seq, unsigned int fd, unsigned int cmd, const char *id, IPacket *msg ) = 0 ;
};

// 消息队列
class CMsgQueue
{
public:
	struct _MsgObj
	{
		unsigned int _fd ;
		unsigned int _cmd;
		unsigned int _seq;
		std::string	 _id;
		IPacket     *_msg;
		time_t		 _now;
		_MsgObj     *_next;
		_MsgObj     *_pre ;
	};

	typedef std::map<unsigned int,_MsgObj *>  CMapObj ;
public:
	CMsgQueue( IMsgCaller *caller ) ;
	~CMsgQueue() ;
	// 添加对象
	bool AddObj( unsigned int seq, unsigned int fd, unsigned int cmd, const char *id, IPacket *msg ) ;
	// 删除对象
	bool Remove( unsigned int seq ) ;
	// 取得对应对象
	_MsgObj * GetObj( unsigned int seq ) ;
	// 释放对象
	void  FreeObj( _MsgObj *obj ) ;
	//  检测超时的对象
	void Check( int timeout ) ;

private:
	// 移除对应的值
	void RemoveValue( unsigned int seq , bool callback ) ;
	// 清除数据
	void Clear( void ) ;

private:
	// 同步操作锁
	share::Mutex 		 _mutex ;
	// 等待数据响应队列
	CMapObj	 			 _index ;
	// 时间索引队列
	TQueue<_MsgObj> 	 _queue ;
	// 消息回调对象
	IMsgCaller		   * _caller ;
};


#endif /* MSGQUEUE_H_ */
