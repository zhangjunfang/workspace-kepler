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

class ISystemEnv ;

// MAS数据传输对象
class IMasServer
{
public:
	virtual ~IMasServer() {}

	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	virtual void Stop( void )  = 0;
	// 纷发上传MAS数据
	virtual void HandleMasUpData( const char *data, int len ) = 0 ;
};

// PAS数据处理对象
class IPasServer
{
public:
	virtual ~IPasServer() {} ;
	// 初始化
	virtual bool Init(ISystemEnv *pEnv ) = 0 ;
	// 开始线程
	virtual bool Start( void ) = 0 ;
	// 停止处理
	virtual void Stop( void ) = 0 ;
	// 客户对内纷发数据
	virtual void HandleClientData( const char *data, const int len ) = 0 ;

};

class ISystemEnv
{
public:
	virtual ~ISystemEnv() {} ;
	// 初始化系统
	virtual bool Init( const char *file , const char *logpath , const char *runpath ) = 0 ;
	// 开始系统
	virtual bool Start( void ) = 0 ;
	// 停止系统
	virtual void Stop( void ) = 0 ;
	// 取得整形值
	virtual bool GetInteger( const char *key , int &value ) = 0 ;
	// 取得字符串值
	virtual bool GetString( const char *key , char *value ) = 0 ;
	// 取得当胶运行路径
	virtual const char * GetRunPath( void )  = 0 ;
	// 取得PAS的对象
	virtual IPasServer * GetPasServer( void ) = 0 ;
	// 取得监视对象
	virtual IMasServer * GetMasServer( void ) = 0 ;
};

#endif
