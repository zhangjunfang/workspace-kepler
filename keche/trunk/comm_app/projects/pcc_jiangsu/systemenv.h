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

class CStatInfo ;
class CCLog ;
class CCConfig ;
class CSystemEnv : public ISystemEnv
{
public:
	CSystemEnv() ;
	~CSystemEnv() ;

	// 初始化系统
	bool Init( const char *file , const char *logpath, const char *logname ) ;

	// 开始系统
	bool Start( void ) ;

	// 停止系统
	void Stop( void ) ;

	// 取得整形值
	bool GetInteger( const char *key , int &value ) ;
	// 取得字符串值
	bool GetString( const char *key , char *value ) ;
	// 取得缓存的序号
	void GetCacheKey( const char *macid , unsigned short data_type , char *buf ) ;
	// 取得PAS对象
	IPasClient * GetPasClient( void ) { return _pas_client; }
	// 取得MSG Client 对象
	IMsgClient * GetMsgClient( void ) { return _msg_client; }
	// 取得MsgCache对象
	IMsgCache  * GetMsgCache( void ) { return _msg_cache ; }
	// 取得对应的LOG对象
	CCLog * GetLogger() 		     { return _pErrLog; }

private:
	// 初始化日志系统
	bool InitLog( const char *logpath, const char *logname ) ;

private:
	// 配置文件类
	CCConfig		  *_config ;
	// 是否初始化
	bool 			   _initialed ;
	// 国标协议服务器
	IPasClient 		 * _pas_client ;
	// 实现监控平台转接协议
	IMsgClient		 * _msg_client ;
	// 数据缓存
	IMsgCache		 * _msg_cache ;
	// 苏汽接口服务
	IPasServer       * _pas_server ;
	// 错误日志记录
	CCLog   		 * _pErrLog ;
	// 统计信息
	CStatInfo 		 * _pStatInfo ;
};

#endif
