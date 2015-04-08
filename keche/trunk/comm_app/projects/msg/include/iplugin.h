/*
 * iplugin.h
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 *  透传的数据插件接口
 */

#ifndef __IPLUGIN_H__
#define __IPLUGIN_H__

#include <SocketHandle.h>

#define MSG_PLUG_IN   	0x0100   // 输入的数据
#define MSG_PLUG_OUT  	0x0200   // 输出的数据

// 插件对象需要支持的接口
class IPlugin
{
public:
	virtual ~IPlugin() {} ;
	// 取得配置文件字符串形数据
	virtual bool GetString( const char *key, char *buf ) = 0 ;
	// 取得配置文件整形的数据
	virtual bool GetInteger( const char *key , int &value ) = 0 ;
	// 需要回调外部接发送的数据
	virtual void OnDeliver( unsigned int id, const char *data, int len , unsigned int cmd ) =  0 ;
};

// 插件执行通道
class IPlugWay
{
public:
	virtual ~IPlugWay() {} ;
	// 需要初始化对象
	virtual bool Init( IPlugin *plug , const char *url, int sendthread, int recvthread, int queuesize ) = 0 ;
	// 初始化插件通道
	virtual bool Start( void ) = 0 ;
	// 停止插件通道
	virtual bool Stop( void ) = 0 ;
	// 处理透传的数据
	virtual bool Process( unsigned int id , const char *data, int len , unsigned int cmd , const char *mid ) = 0 ;
};


#endif /* IPLUGIN_H_ */
