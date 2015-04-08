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

#include <map>
#include <string>

using std::map;
using std::string;

struct InnerMsg {
	string begin;
	string seqid;
	string macid;
	string order;
	map<string, string> param;
};

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
	//
	virtual void HandleData( const InnerMsg & msg ) = 0 ;
};

// 锐明拍照设备的消息转发服务
class IPhotoSvr {
public:
	virtual ~IPhotoSvr() {};

	virtual bool Init(ISystemEnv *pEnv) = 0;
	virtual bool Start(void) = 0;
	virtual void Stop(void) = 0;

	virtual void HandleData( const InnerMsg & msg ) = 0 ;
};

// 环境对象指针
class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得MSG的部件
	virtual IMsgClient *  GetMsgClient( void ) =  0;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
	//
	virtual IPhotoSvr *   GetPhotoSvr(void) = 0;
};

#endif
