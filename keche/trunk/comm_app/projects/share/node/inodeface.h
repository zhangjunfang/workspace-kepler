/*
 * inodeface.h
 *
 *  Created on: 2011-11-10
 *      Author: humingqing
 */

#ifndef __INODEFACE_H__
#define __INODEFACE_H__

#include <list>
#include <databuffer.h>
#include <SocketHandle.h>
// FD的列表
typedef std::list<socket_t*> ListFd ;
// 构建一个消息数据
struct MsgData
{
	unsigned short cmd ;
	unsigned int   seq ;
	DataBuffer     buf ;
};


#define MSG_COMPLETE   0   // 响应数据完成
#define MSG_TIMEOUT    1   // 响应数据超时

// 数据通知对象
class IMsgNotify
{
public:
	virtual ~IMsgNotify() {} ;
	// 超或者删除通知数据对象
	virtual void NotifyMsgData( socket_t *sock , MsgData *p , ListFd &fd_list, unsigned int op ) = 0 ;
};

// MSG内存管理对象
class IAllocMsg
{
public:
	// MSG 内存开辟和回收对象
	virtual ~IAllocMsg() {}
	// 开辟内存
	virtual MsgData * AllocMsg() = 0 ;
	// 回收内存
	virtual void FreeMsg( MsgData *p ) = 0 ;
};

// 等待队列对象的接口
class IWaitGroup
{
public:
	virtual ~IWaitGroup() {} ;
	// 初始化
	virtual bool Init( void ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0  ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 添加组的数据
	virtual bool AddGroup( socket_t *sock, unsigned int seq, MsgData *msg ) = 0 ;
	// 添加到对应的组里面
	virtual bool AddQueue( unsigned int seq, socket_t *sock ) = 0 ;
	// 删除对应的值
	virtual bool DelQueue( unsigned int seq, socket_t *sock , bool bclear ) = 0 ;
	// 取得没有响应的FD
	virtual int  GetCount( unsigned int seq ) = 0 ;
	// 取得对应的FD的LIST值
	virtual bool GetList( unsigned int seq, ListFd &fds ) = 0 ;
	// 设置数据回收对象
	virtual void SetNotify( IMsgNotify *pNotify ) = 0 ;
};

#endif /* INODEFACE_H_ */
