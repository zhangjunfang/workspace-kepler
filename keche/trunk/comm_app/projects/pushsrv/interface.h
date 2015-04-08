/**********************************************
 * interface.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象接口类定义，主要实现类与类之间交互的接口定义
 *********************************************/
#ifndef __INTERFACE_H__
#define __INTERFACE_H__

#include <inodeface.h>
#include <OnlineUser.h>
#include <icontext.h>
#include <icache.h>

class ISystemEnv ;
// 节点管理服务
class INodeClient
{
public:
	virtual ~INodeClient() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
};

#define DEMAND_MACID   0     // 通过MACID订阅  OME_PHONE  4C54_132343423
#define DEMAND_GROUP   1	 // 通过组进行订阅     group-> macid list

// MSG数据处理部件
class IMsgClient
{
public:
	virtual ~IMsgClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 向MSG上传消息
	virtual void HandleMsgData( const char *macid, const char *data, int len )  = 0 ;
	// 添加用户处理
	virtual void AddUser( const char *ip, unsigned short port, const char *user, const char *pwd ) = 0 ;
	// 处理删除服务
	virtual void DelUser( const char *ip, unsigned short port ) = 0 ;
	// 添加订阅机制
	virtual void AddDemand( const char *name, int type )  = 0 ;
	// 取消定订阅
	virtual void DelDemand( const char *name, int type )  = 0 ;
};

// 数据推送服务
class IPushServer
{
public:
	virtual ~IPushServer() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0 ;
	// 开始
	virtual bool Start( void ) = 0;
	// 停止
	virtual void Stop( void ) = 0;
	// 从MSG转发过来的数据
	virtual void HandleData( const char *data, int len ) = 0 ;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得MSG的部件
	virtual IMsgClient *  GetMsgClient( void ) =  0;
	// 取得推送服务对象
	virtual IPushServer * GetPushServer( void ) = 0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
};

#endif
