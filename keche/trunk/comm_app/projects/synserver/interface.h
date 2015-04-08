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

#include <icache.h>
#include <icontext.h>

class ISystemEnv ;

class ISynServer
{
public:
	virtual ~ISynServer() {} ;
	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 开始启动服务器
	virtual bool Start( void ) = 0 ;
	// STOP方法
	virtual void Stop( void ) = 0 ;
};

class ISystemEnv: public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得图片服务器
	virtual ISynServer* GetSynServer( void ) = 0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
};

#endif
