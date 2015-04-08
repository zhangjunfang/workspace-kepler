/**********************************************
 * interface.h
 *
 *  Created on: 2014-06-30
 *      Author: ycq
 *********************************************/

#ifndef _INTERFACE_H_
#define _INTERFACE_H_ 1

#include <icache.h>
#include <icontext.h>

class ISystemEnv;

// 非国标监管协议接入
class IPasClient {
public:
	virtual ~IPasClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0 ;
	// 停止处理
	virtual void Stop( void ) = 0 ;
	// 客户对内纷发数据
	virtual bool HandleData( const char *data, const int len ) = 0 ;
};

// 实现协议转换部件
class IMsgClient {
public:
	virtual ~IMsgClient() {}
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0;
	// 开始
	virtual bool Start(void) = 0;
	// 停止
	virtual void Stop(void) = 0;
	// 向MSG上传消息
	virtual bool HandleData(const char *data, int len) = 0;
};

class ISystemEnv: public IContext {
public:
	virtual ~ISystemEnv() {}
	// 取得PAS client对象
	virtual IPasClient * GetPasClient(void) = 0;
	// 取得MSG client对象
	virtual IMsgClient * GetMsgClient(void) = 0;
	// 取得Redis Cache对象
	virtual IRedisCache * GetRedisCache(void) = 0;
};
#endif//_INTERFACE_H_
