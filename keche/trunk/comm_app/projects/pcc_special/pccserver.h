/*
 * pccserver.h
 *
 *  Created on: 2011-11-28
 *      Author: humingqing
 *      主要实现PAS的下行数据以及内部协议数据的处理
 */

#ifndef __PCCSERVER_H__
#define __PCCSERVER_H__

#include "interface.h"
#include <BaseServer.h>
#include <OnlineUser.h>
#include <ProtoParse.h>

#define COMPANY_TYPE 	"PIPE"
#define WEB_TYPE 		"WEB"
#define STORAGE_TYPE 	"SAVE"
#define PROXY_TYPE		"PROXY"
#define ROUTE_TYPE      "ROUTE"
#define MAX_TIMEOUT  180

class CPccServer : public BaseServer, public IPccServer
{
public:
	CPccServer() ;
	~CPccServer() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始线程
	virtual bool Start( void ) ;
	// 停止处理
	virtual void Stop( void ) ;
	// 从链路主动发起断开主从链的请求
	virtual void Close( int accesscode , unsigned short msgid, int reason ) ;
	// 给PCC路由更新路由配置
	virtual void updateAreaids(const string &areaids);
protected:
	// 实现服务必需的接口
	virtual void on_data_arrived( socket_t *sock, const void* data, int len );
	virtual void on_dis_connection( socket_t *sock );
	virtual void on_new_connection( socket_t *sock, const char* ip, int port ){};

	// 心跳线程和定时线程
	virtual void TimeWork() ;
	virtual void NoopWork() ;

private:
	// 处理809协议的数据
	void HandleOutData( socket_t *sock, const char *data, int len ) ;
	// 处理内部下发数据
	void HandleInnerData( socket_t *sock, const char *data, int len ) ;
	// 处理超时连接
	void HandleOfflineUsers() ;
	// 发送809协议数据
	bool SendCrcData( socket_t *sock, const char* data, int len ) ;
	// 是否需要处理加解密
	bool EncryptData( unsigned char *data, unsigned int len , bool encode ) ;
	// 更新PCC当前信息
	void UpdatePcc( void ) ;

private:
	// 环境对象指针
	ISystemEnv		   *_pEnv ;
	// 在线用户队列
	OnlineUser 			_online_user;
	// 处理线程
	unsigned int 		_thread_num ;
	// 809 协议解析
	ProtoParse			_proto_parse;
	//所有监管平台的省代码
	string              _areaids;
	//发送路由配置加锁
	share::Mutex        _mutex ;
};

#endif /* PCCSERVER_H_ */
