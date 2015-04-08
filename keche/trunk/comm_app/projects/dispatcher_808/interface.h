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

#include <icontext.h>
#include <icache.h>

class ISystemEnv ;

#include <set>
using std::set;
#include <string>
using std::string;

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
	// 下行数据处理函数
	virtual bool HandleMsgData(const char *data, int len) = 0;
};

// 数据推送服务
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
	virtual bool HandleMsgData(const string userid, const char *data, int len) = 0;
};

// 用户管理
class IUserMgr {
public:
	virtual ~IUserMgr() {};

	// 初始化
	virtual bool Init(ISystemEnv *pEnv) = 0;
	// 开始
	virtual bool Start(void) = 0;
	// 停止
	virtual void Stop(void) = 0;

	// 查询分发路由
	virtual set<string> getRoute(const string &macid) = 0;
	// 查询通道信息
	virtual string getCorpInfo(const string &corpid) = 0;
	// 确认分发路由
	virtual bool chkRoute(const string &userid) = 0;
	// 获取所有路由
	virtual set<string> getAllRoute() = 0;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得MSG的部件
	virtual IMsgClient *  GetMsgClient( void ) =  0;
	//
	virtual IWasClient *  GetWasClient( void ) =  0;
	//
	virtual IUserMgr   *  GetUserMgr(void) = 0;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
};

#endif
