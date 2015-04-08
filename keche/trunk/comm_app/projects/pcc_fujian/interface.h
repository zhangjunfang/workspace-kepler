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
#include <imsgcache.h>

#define SEND_ALL   "SEND_ALL"

class ISystemEnv ;

// 实现标准国标协议接入
class IPasClient
{
public:
	virtual ~IPasClient() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0 ;
	// 停止处理
	virtual void Stop( void ) = 0 ;
	// 客户对内纷发数据
	virtual void HandleData( const char *data, const int len ) = 0 ;
};

// 实现协议转换部件
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
	virtual bool HandleUpMsgData( const char *code, const char *data, int len )  = 0 ;
	// 通过HTTP服务来取图片
	virtual void LoadUrlPic( unsigned int seq, const char *path ) = 0 ;
};

class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;
	// 取得PAS的对象
	virtual IPasClient * GetPasClient( void ) = 0 ;
	// 取得MSG Client对象
	virtual IMsgClient * GetMsgClient( void ) =  0 ;
	// 取得MsgCache对象
	virtual IMsgCache  * GetMsgCache( void ) = 0 ;
};

#endif
