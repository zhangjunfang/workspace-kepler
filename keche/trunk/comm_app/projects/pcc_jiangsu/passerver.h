/*
 * passerver.h
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  数据推送服务
 */

#ifndef __PASSERVER_H__
#define __PASSERVER_H__

#include <interface.h>
#include <BaseServer.h>
#include "mypacker.h"
#include "usermgr.h"
#include "statinfo.h"

//用户在三分钟的时间内没有数据上传，就认为是超时了。
#define SOCK_TIME_OUT  (3 * 10)

class PasServer :
	public BaseServer , public IPasServer, public IPairNotify
{
public:
	PasServer(CStatInfo *stat) ;
	~PasServer() ;
	//////////////////////////ICasClientServer//////////////////////////////
	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	////////////////////////////////////////////////////////////
	virtual void on_data_arrived( socket_t *sock, const void *data, int len);
	// 接收到断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	// 接收到新连接到来处理
	virtual void on_new_connection( socket_t *sock, const char* ip, int port) ;
	// 从MSG转发过来的数据
	virtual void HandleData( const char *data, int len ) ;
	// 定时线程
	virtual void TimeWork();
	// 心跳处理
	virtual void NoopWork();
	// 关闭用户通知
	virtual void CloseUser( socket_t *sock ) ;
	// 通知用户上线
	virtual void NotifyUser( socket_t *sock , const char *key ) ;

private:
	// 处理TCP的数据
	void HandleTcpData( socket_t *sock, const char *data, int len ) ;
	// 处理UDP的数据
	void HandleUDPData( socket_t *sock, const char *data, int len ) ;
	// 连接UDP的服务器
	socket_t * ConnectUDP( const char *ip, int port ) ;

private:
	// 环境对象指针
	ISystemEnv			  *_pEnv ;
	// 处理线程数
	unsigned  int		   _thread_num ;
	// 数据分包对象
	CMyPackSpliter		   _pack_spliter ;
	// 用户管理对象
	CUserMgr			   _user_mgr ;
	// 最后一次检测
	time_t				   _last_check ;
	// 统计模块
	CStatInfo			  *_statinfo ;
	// 保存的XML文件路径
	string 				   _xmlpath ;
};


#endif /* PUSHSERVER_H_ */
