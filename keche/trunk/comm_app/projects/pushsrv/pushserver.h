/*
 * pushserver.h
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  数据推送服务
 */

#ifndef __PUSHSERVER_H__
#define __PUSHSERVER_H__

#include <interface.h>
#include <BaseServer.h>
#include <interpacker.h>
#include "subscribe.h"
#include "proto_convert.h"
#include <set>
#include <vector>
#include <string>
//用户在三分钟的时间内没有数据上传，就认为是超时了。
#define SOCK_TIME_OUT  (1 * 30)

class PushServer :
	public BaseServer , public IPushServer
{
public:
	PushServer() ;
	~PushServer() ;
	//////////////////////////ICasClientServer//////////////////////////////
	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	////////////////////////////////////////////////////////////
	virtual void on_data_arrived( socket_t *sock, const void *data, int len);

	virtual void on_new_connection( socket_t *sock, const char* ip, int port);

	virtual void on_dis_connection( socket_t *sock );
	// 从MSG转发过来的数据
	virtual void HandleData( const char *data, int len ) ;

	virtual void TimeWork();

	virtual void NoopWork();

	// 从MSG 发过来的数据进行转发
	void DispathMsgData(const string &mac_id, const char *data, int len);

	void NotifyMsgData(const string &mac_id, const char *data, int len);

	void SendDataToUser(const string &user_id, char *data, int len);

private:
	// 发送加密数据
	bool SendDataEx( socket_t *sock, const char *data, int len ) ;
	// 加载订阅关系
	void LoadSubscribe( const char *key, std::vector<std::string> &vec, std::set<std::string> &kset ) ;

private:
	// 环境对象指针
	ISystemEnv			  *_pEnv ;
	// 处理线程数
	unsigned  int		   _thread_num ;
	// 数据分包对象
	CInterSpliter		   _pack_spliter ;

	//PushUserHandler        _push_user_handler;

	Subscribe              _subs_info;

	CInterProtoParse       _inter_parse;

	CNewProtoParse         _new_parse;

	ConvertProto           _proto_convert;
	// 在线用户处理
	OnlineUser   		   _online_user;
};


#endif /* PUSHSERVER_H_ */
