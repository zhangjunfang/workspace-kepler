/**********************************************
 * systemenv.h
 *
 *  Created on: 2014-8-19
 *      Author: ycq
 *********************************************/
#ifndef __SYSTEMENV_H__
#define __SYSTEMENV_H__

#include "interface.h"

class CCConfig ;
class CSystemEnv : public ISystemEnv
{
public:
	CSystemEnv() ;
	~CSystemEnv() ;

	// 初始化系统
	bool Init( const char *file , const char *logpath , const char *userfile  , const char *logname ) ;

	// 开始系统
	bool Start( void ) ;

	// 停止系统
	void Stop( void ) ;

	// 取得整形值
	bool GetInteger( const char *key , int &value ) ;
	// 取得字符串值
	bool GetString( const char *key , char *value ) ;
	// 取得用户数据位置
	const char * GetUserPath( void )  { return _user_file_path.c_str(); }
	//
	IWasClient * GetWasClient(void) { return _wasclient;	}
	//
	IWasServer * GetWasServer(void) { return _wasserver; }
	//
	IRedisCache * GetRedisCache(void) { return _rediscache; }
private:
	// 初始化日志系统
	bool InitLog( const char *logpath , const char *logname ) ;

private:
	// 配置文件类
	CCConfig		  *_config ;
	// 用户文件路径
	std::string 	   _user_file_path ;
	// 是否初始化
	bool 			   _initialed ;
	// jt808客户端对象
	IWasClient       * _wasclient ;
	// jt808服务端对象
	IWasServer 		 * _wasserver ;
	// redis客户端对象
	IRedisCache      * _rediscache ;
};

#endif
