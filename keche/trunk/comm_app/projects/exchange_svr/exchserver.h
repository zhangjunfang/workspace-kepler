/*
 * exchserver.h
 *
 *  Created on: 2014-5-23
 *      Author: ycq
 *  消息转发服务
 */

#ifndef _PHOTOSVR_H_
#define _PHOTOSVR_H_ 1

#include "interface.h"
#include "exchspliter.h"
#include <BaseServer.h>

#include <set>
using std::set;
#include <vector>
using std::vector;
#include <string>
using std::string;

class ExchServer : public BaseServer , public IExchServer {
	// 环境对象指针
	ISystemEnv			    *_pEnv ;
	// 处理线程数
	unsigned  int		     _thread_num ;
	// 数据分包对象
	CExchSpliter   		     _pack_spliter;
	// 在线用户处理
	OnlineUser   		     _online_user;
private:
	size_t enCode(const uint8_t *src, size_t len, uint8_t *dst);
	size_t deCode(const uint8_t *src, size_t len, uint8_t *dst);
public:
	ExchServer() ;
	~ExchServer() ;

	// 服务初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	////////////////////////////////////////////////////////////
	virtual void on_data_arrived( socket_t *sock, const void *data, int len);
	virtual void on_new_connection( socket_t *sock, const char* ip, int port);
	virtual void on_dis_connection( socket_t *sock );


	virtual void TimeWork();
	virtual void NoopWork();

	virtual void HandleData(const char *data, int len) ;
};

#endif//_PHOTOSVR_H_
