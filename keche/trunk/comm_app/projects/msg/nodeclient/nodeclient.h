/*
 * nodeclient.h
 *
 *  Created on: 2011-11-10
 *      Author: humingqing
 */

#ifndef __NODECLIENT_H__
#define __NODECLIENT_H__

#include <interface.h>
#include <inodeface.h>
#include <BaseClient.h>
#include <nodeheader.h>
#include <msgbuilder.h>
#include <packspliter.h>

class CNodeClient : public BaseClient,
	public INodeClient, public IMsgNotify
{
public:
	CNodeClient() ;
	~CNodeClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始线程
	virtual bool Start( void ) ;
	// 停止线程
	virtual void Stop( void ) ;

	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};
	// 定时线程
	virtual void TimeWork();
	// 心跳线程
	virtual void NoopWork();
	// 构建登陆的消息
	virtual int build_login_msg( User &user, char *buf, int buf_len ) ;
	// 超或者删除通知数据对象
	virtual void NotifyMsgData( socket_t *sock, MsgData *p , ListFd &fds, unsigned int op ) ;

private:
	// 发送心跳链路测试
	void SendLinkTest( void ) ;
	// 重载发送数据函数
	bool SendDataEx( socket_t *sock, const char *data, int len ) ;

private:
	// 环境对象指针
	ISystemEnv   *_pEnv ;
	// 节点ID号
	unsigned int  _nodeid ;
	// 是否开启此模块
	bool 		  _enable ;
	// MSG服务器的IP
	string 		  _msg_ip ;
	// MSG服务器的端口
	int 		  _msg_port ;
	// 消息内存管理对象
	IAllocMsg	 *_pAlloc ;
	// 消息建设对象
	CMsgBuilder	 *_pBuilder ;
	// 异步等待队列
	IWaitGroup	 *_pWaitQueue;
	// 设置分包对象
	CPackSpliter  _pack_spliter;
};

#endif /* NODECLIENT_H_ */
