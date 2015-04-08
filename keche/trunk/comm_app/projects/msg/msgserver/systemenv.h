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

#include <map>
#include "interface.h"
#ifdef _GPS_STAT
#include <gpsstat.h>
#endif

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
	// 取得Msg对象
	IMsgClientServer * GetMsgClientServer( void ){ return _msg_server; } ;
	// 取得用户管理对象
	IGroupUserMgr * GetUserMgr( void ) { return _usermgr ; }
	// 取得发布服务
	IPublisher  * GetPublisher( void ) { return _publisher; }
	// 取得RedisCache
	IRedisCache * GetRedisCache( void ) { return _rediscache; }
	// 取得对应处理器的对象
	IMsgHandler * GetMsgHandler( unsigned int id ) { return _msghandler[id]; }
	// 取得中心同步msgclient
	IMsgClient  * GetMsgClient( void ) { return _msgclient; }

#ifdef _GPS_STAT
	// 这里处理GPS的数据统计计数器
	void AddGpsCount( unsigned int count )  { _gpsstat.Add(count); 			}
	void SetGpsState( bool enable ) 		{ _gpsstat.SetEnable(enable) ;  }
	bool GetGpsState( void ) 				{ return _gpsstat.Enable(); 	}
	int  GetGpsCount( void )				{ return _gpsstat.Size(); 		}
#endif

private:
	// 初始化日志系统
	bool InitLog( const char *logpath , const char *logname ) ;

private:
	// 配置文件类
	CCConfig		  *_config ;
	// 用户文件路径
	std::string 	   _user_file_path ;
	// Msg服务器和客户端
	IMsgClientServer * _msg_server ;
	// 节点管理客户端
	INodeClient		 * _node_client ;
	// 用户管理对象
	IGroupUserMgr    * _usermgr ;
	// 是否初始化
	bool 			   _initialed ;
	// 发布服务模块
	IPublisher		 * _publisher ;
	// RedisCache
	IRedisCache      * _rediscache ;
#ifdef _GPS_STAT
	// 统计位置数据上报
	CGpsStat 		   _gpsstat ;
#endif
	// 处理部件对象
	IMsgHandler      * _msghandler[MAX_MSGHANDLE] ;
	// 同步中心服务器
	IMsgClient       * _msgclient ;
};

#endif
