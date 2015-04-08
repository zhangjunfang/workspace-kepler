/**********************************************
 * interface.h
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/
#ifndef __INTERFACE_H__
#define __INTERFACE_H__

#include <icontext.h>
#include <icache.h>

class ISystemEnv ;

#include <string>
using std::string;

// 部标808客户端
class IWasClient {
public:
	virtual ~IWasClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0 ;
	// 开始
	virtual bool Start( void ) = 0;
	// 停止
	virtual void Stop( void ) = 0;
	// 上行数据处理函数
	virtual bool HandleData(const string &sim, const uint8_t *ptr, size_t len) = 0;
	// 新增模拟终端
	virtual bool AddTerminal(const string &sim) = 0;
	// 删除模拟终端
	virtual bool DelTerminal(const string &sim) = 0;
};

// 部标808服务端
class IWasServer {
public:
	virtual ~IWasServer() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0 ;
	// 开始
	virtual bool Start( void ) = 0;
	// 停止
	virtual void Stop( void ) = 0;
	// 下行数据处理函数
	virtual bool HandleData(const string &sim, const uint8_t *ptr, size_t len) = 0;
	// 判断终端是否在线
	virtual bool ChkTerminal(const string &sim) = 0;
};

// 环境对象指针
class ISystemEnv: public IContext {
public:
	virtual ~ISystemEnv() {}

	virtual IWasClient* GetWasClient(void) = 0;
	virtual IWasServer* GetWasServer(void) = 0;
	virtual IRedisCache* GetRedisCache(void) = 0;
};

#endif
