/**********************************************
 * wasserver.h
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/
#ifndef __WASSERVER_H__
#define __WASSERVER_H__

#include <std.h>
#include "interface.h"
#include <BaseServer.h>
#include <protocol.h>
#include <interpacker.h>

class WasServer: public BaseServer, public IWasServer {
	// 环境对象
	ISystemEnv		    *_pEnv ;
	// 在线用户
	OnlineUser 			 _online_user;
	// 线程数
	unsigned int 		 _thread_num ;
	// 链接空闲时间，秒
	unsigned int       _max_timeout;
	// 分包对象
	C808Spliter		 	 _pack_spliter ;
public:
	WasServer();
	~WasServer();
	// 初始化系统
	bool Init(ISystemEnv *pEnv);
	// 开始启动系统
	bool Start( void ) ;
	// 停止系统
	void Stop( void ) ;
	// 下行数据处理函数
	bool HandleData(const string &sim, const uint8_t *ptr, size_t len);
	// 判断终端是否在线
	bool ChkTerminal(const string &sim);
protected:
	virtual void on_data_arrived( socket_t *sock , const void *data, int len );
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock , const char* ip, int port );

	virtual void TimeWork();
	virtual void NoopWork();

	// 发送7E编码的数据
	bool Send7ECodeData( socket_t *sock , const char *data, int len ) ;
	// 发送响应数据
	bool SendResponse( socket_t *sock, const char *id , const char *data, int len ) ;
};

#endif//__WASSERVER_H__
