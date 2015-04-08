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

#include <qstring.h>
#include <icontext.h>
#include <icache.h>

class ISystemEnv ;
#define TIMEOUT_LIVE			180

class IClient
{
public:
	virtual ~IClient() {}
	// 初始化
	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	// 开始
	virtual bool Start( void ) = 0 ;
	// 停止
	virtual void Stop( void ) = 0 ;
	// 向下发送数据
	virtual void HandleUpData( const char *data, const int len ) = 0 ;
};

class IServer
{
public:
	virtual ~IServer() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
	// 处理was下发的数据
	virtual void HandleDownData( const char *userid, const char *data, int len , unsigned int seq = 0 , bool send = true ) = 0 ;
	// 取得在线车辆数
	virtual int  GetOnlineSize( void ) = 0 ;
	// 发送TTS语音播报
	virtual void SendTTSMessage( const char *userid, const char *msg, int len ) = 0 ;
};

class ISystemEnv : public IContext
{
public:
	virtual ~ISystemEnv() {} ;

	virtual unsigned short GetSequeue( const char *key , int len = 1 ) = 0 ;
	// 重置用户序列
	virtual void ResetSequeue( const char *key ) = 0  ;
	// 取得Msg处理对象
	virtual IClient    * GetMsgClient( void ) = 0 ;
	// 取得CAS对象
	virtual IServer    * GetClientServer( void ) = 0 ;
	// 取得RedisCache
	virtual IRedisCache * GetRedisCache( void ) = 0 ;
#ifdef _GPS_STAT
	// 这里处理GPS的数据统计计数器
	virtual void AddGpsCount( unsigned int count ) = 0 ;
	virtual void SetGpsState( bool enable ) = 0 ;
	virtual bool GetGpsState( void ) = 0 ;
	virtual int  GetGpsCount( void ) = 0 ;
#endif
};

#endif
