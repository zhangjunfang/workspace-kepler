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
class IVechileClient
{
public:
	virtual ~IVechileClient() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
};

class ISystemEnv
{
public:
	virtual ~ISystemEnv() {} ;
	// 初始化系统
	virtual bool Init( const char *file , const char *logpath , const char *userfile ) = 0 ;

	// 开始系统
	virtual bool Start( void ) = 0 ;

	// 停止系统
	virtual void Stop( void ) = 0 ;

	// 取得整形值
	virtual bool GetInteger( const char *key , int &value ) = 0 ;
	// 取得字符串值
	virtual bool GetString( const char *key , char *value ) = 0 ;
	// 取得用户数据路径
	virtual const char * GetUserPath( void ) = 0 ;
	// 取得CAS对象
	virtual IVechileClient * GetVechileClient( void ) = 0 ;
};

#endif
