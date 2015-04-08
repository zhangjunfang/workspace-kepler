/**********************************************
 * systemenv.h
 *
 *  Created on: 2014-06-30
 *      Author: ycq
 *********************************************/

#ifndef _SYSTEMENV_H_
#define _SYSTEMENV_H_ 1

#include "interface.h"

class CCConfig ;
class CSystemEnv : public ISystemEnv {
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
	// 取得用户数据路径
	const char * GetUserPath( void ) { return NULL; }
	// 取得PAS对象
	IPasClient * GetPasClient( void ) { return _pas_client; }
	// 取得MSG Client 对象
	IMsgClient * GetMsgClient( void ) { return _msg_client; }
	// 取得RedisCache
	IRedisCache * GetRedisCache( void ) { return _rediscache; }
private:
	// 初始化日志系统
	bool InitLog( const char *logpath, const char *logname ) ;

private:
	// 配置文件类
	CCConfig		  *_config ;
	// 是否初始化
	bool 			   _initialed ;
	// 非国标监管平台客户端
	IPasClient 		 * _pas_client ;
	// msg客户端对象指针
	IMsgClient		 * _msg_client ;
	// redis客户端对象
	IRedisCache       *_rediscache;
};
#endif//_SYSTEMENV_H_
