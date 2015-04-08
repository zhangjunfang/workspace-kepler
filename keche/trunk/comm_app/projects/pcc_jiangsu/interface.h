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

#define SEND_ALL   "SEND_ALL"

class ISystemEnv ;
// 消息缓存对象
class IMsgCache
{
public:
	virtual ~IMsgCache() {} ;
	// 添加数据
	virtual bool AddData( const char *key, const char *buf, const int len ) = 0 ;
	// 取得数据
	virtual char *GetData( const char *key, int &len ) = 0  ;
	// 释放数据
	virtual void FreeData( char *data ) = 0 ;
	// 处理超时的数据
	virtual void CheckData( int timeout ) = 0 ;
	// 移除数据
	virtual bool Remove( const char *key ) = 0 ;
};

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
	virtual bool HandleData( const char *data, const int len ) = 0 ;
	// 取得PAS对应服务ID
	virtual const char * GetSrvId( void )  = 0 ;
	// 当前用户是否在线
	virtual bool IsOnline( void ) = 0 ;
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
	virtual void HandleUpMsgData( const char *code, const char *data, int len )  = 0 ;
	// 取得手机号
	virtual bool GetCarMacId( const char *key, char *macid ) = 0 ;
};

// 实现协议转换部件
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
};

// PCC其它日志处理
#define  PCCMSG_LOG( logger, ip, port, userid, key, fmt, ... )   \
	logger->print_net_msg( 1 , __FILE__, __LINE__, __FUNCTION__, key , ip, port, userid, fmt ,  ## __VA_ARGS__ )

class CCLog ;
class ISystemEnv
{
public:
	virtual ~ISystemEnv() {} ;
	// 初始化系统
	virtual bool Init( const char *file , const char *logpath, const char *logname ) = 0 ;

	// 开始系统
	virtual bool Start( void ) = 0 ;

	// 停止系统
	virtual void Stop( void ) = 0 ;

	// 取得整形值
	virtual bool GetInteger( const char *key , int &value ) = 0 ;
	// 取得字符串值
	virtual bool GetString( const char *key , char *value ) = 0 ;
	// 取得Cache的KEY值
	virtual void GetCacheKey( const char *macid , unsigned short data_type , char *buf ) = 0 ;
	// 取得PAS的对象
	virtual IPasClient * GetPasClient( void ) = 0 ;
	// 取得MSG Client对象
	virtual IMsgClient * GetMsgClient( void ) =  0 ;
	// 取得MsgCache对象
	virtual IMsgCache  * GetMsgCache( void ) = 0 ;
	// 取得对应的LOG对象
	virtual CCLog *      GetLogger() = 0 ;
};

#endif
