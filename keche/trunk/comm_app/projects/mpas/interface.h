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
#include <icontext.h>
#include <icache.h>

class ISystemEnv ;
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
	// 向MSG下发消息
	virtual bool Deliver( const char *data, int len )  = 0 ;
	// 添加用户处理
	virtual void AddUser( const char *ip, unsigned short port, const char *user, const char *pwd ) = 0 ;
	// 处理删除服务
	virtual void DelUser( const char *ip, unsigned short port ) = 0 ;
	// 添加手机MAC
	virtual void AddMac2Car( const char *macid, const char *vechile ) = 0 ;
};

// 节点服务对象
class INodeClient
{
public:
	virtual ~INodeClient() {} ;
	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 启动节点客户
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
};

// Pas服务器接口
class IPasServer
{
public:
	virtual ~IPasServer() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 客户对内纷发数据
	virtual void HandlePasDown( const char *code , const char *data, const int len ) = 0 ;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得MSG的部件
	virtual IMsgClient *  GetMsgClient( void ) =  0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
	// 取得PAS的处理器
	virtual IPasServer *  GetPasServer( void ) = 0 ;
};

#endif
