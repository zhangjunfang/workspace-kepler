/*
 * nodesrv.h
 *
 *  Created on: 2011-11-4
 *      Author: humingqing
 */

#ifndef __NODESRV_H__
#define __NODESRV_H__

#include "nodemgr.h"
#include <BaseServer.h>
#include <packspliter.h>

class CNodeSrv : public BaseServer , public INodeSrv
{
public:
	CNodeSrv() ;
	~CNodeSrv() ;

	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	// 处理数据到来
	virtual void on_data_arrived( socket_t *sock, const void *data, int len);
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	// 新连接到来
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};
	// 时间线程
	virtual void TimeWork();
	// 心跳线程
	virtual void NoopWork();
	// 发送数据处理
	virtual bool HandleData( socket_t *sock, const char *data, int len ) ;
	// 内部检测异常的断连操作
	virtual void CloseClient( socket_t *sock ) ;

private:
	// 环境对象指针
	ISystemEnv    *_pEnv ;
	// 工作线程
	unsigned int   _thread_num ;
	// 节点管理对象
	CNodeMgr	  *_pNodeMgr ;
	// 分包对象
	CPackSpliter   _pack_spliter ;
};


#endif /* NODESRV_H_ */
