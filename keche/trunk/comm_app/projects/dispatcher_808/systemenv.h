/**********************************************
 * systemenv.h
 *
 *  Created on: 2011-07-24
 *      Author: humingqing
 *       Email: qshuihu@gmail.com
 *    Comments: 环境对象类，主要管理需要交互的对象，对象与对象交互之间的中界，
 *    这样，任何两个对象之间的交互都可以通过对象管理部件来实现直接交互，使各部件之间透明化，
 *    也使得结构合理化，对于每一个部件实现，都必需实现Init Start Stop三个功能主要实现系统
 *    之间的统一规范化处理。
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
	// 取得MSG的数据对象
	IMsgClient *  GetMsgClient( void )  { return _msgclient; }
	//
	IWasClient *  GetWasClient( void )  { return _wasclient; }
	//
	IUserMgr   *  GetUserMgr(void) {return _userMgr; }
	// 取得RedisCache
	IRedisCache * GetRedisCache( void ) { return _rediscache; }
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
	// MSGClient对象
	IMsgClient 		 * _msgclient ;
	//
	IWasClient       * _wasclient ;
	//
	IUserMgr         * _userMgr;
	//
	IRedisCache      * _rediscache ;
};

#endif
