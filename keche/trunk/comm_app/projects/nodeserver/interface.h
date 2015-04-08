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

#include <inodeface.h>
#include <SocketHandle.h>

class ISystemEnv ;
class INodeSrv
{
public:
	virtual ~INodeSrv() {} ;

	virtual bool Init( ISystemEnv *pEnv ) = 0 ;
	virtual bool Start( void ) = 0 ;
	// 重载STOP方法
	virtual void Stop( void ) = 0 ;
	// 发送数据处理
	virtual bool HandleData( socket_t *sock, const char *data, int len ) = 0 ;
	// 内部检测异常断连操作
	virtual void CloseClient( socket_t *sock ) = 0 ;
};


class ISystemEnv
{
public:
	virtual ~ISystemEnv() {} ;
	// 初始化系统
	virtual bool Init( const char *file , const char *logpath , const char *userfile  , const char *logname ) = 0 ;
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
	// 取得NodeSrv对象
	virtual INodeSrv * GetNodeSrv( void ) = 0 ;
	// 取得消息内存管理对象
	virtual IAllocMsg * GetAllocMsg( void ) = 0 ;
	// 取得消息队列分组对象
	virtual IWaitGroup * GetWaitGroup( void ) = 0 ;
};

#endif
