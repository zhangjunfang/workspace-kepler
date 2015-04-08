/*
 * plugin.h
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 *  Memo: 插件模式来处理数据，这里主要针对货运和甩挂业务操作来进行处理，对于所有数据透传的数据可以通过插件来处理
 */

#ifndef __PLUGIN_H__
#define __PLUGIN_H__

#include <interface.h>
#include <iplugin.h>
#include <Mutex.h>
#include "waymgr.h"

class CPlugin :
	public IMsgHandler, public IPlugin
{
	class CPlugUserMgr
	{
		struct _UserInfo
		{
			unsigned int _id ;
			string 	 _userid ;
			string   _macid ;
		};
		typedef map<string,_UserInfo*> 			CMapMacId ;
		typedef map<unsigned int, _UserInfo*>   CMapUserId ;
	public:
		CPlugUserMgr() ;
		~CPlugUserMgr() ;
		// 添加到用户对象
		unsigned int AddUser( const char *userid, const char *macid ) ;
		// 取得用户对象
		bool GetUser( unsigned int id , string &userid, string &macid ) ;
		// 删除数据
		bool DelUser( unsigned int id ) ;

	private:
		// 清理所有对象
		void Clear( void ) ;

	private:
		// 用户操作锁
		share::Mutex _mutex ;
		// MACID对应的查找关系
		CMapMacId    _macids ;
		// 用户ID对应的查找关系
		CMapUserId   _userids;
		// 自动增长的ID号
		unsigned int _id ;
	};
public:
	CPlugin() ;
	~CPlugin() ;

	// 初始化
	bool Init( ISystemEnv * pEnv ) ;
	// 启动服务
	bool Start( void ) ;
	// 停止服务
	bool Stop( void ) ;
	// 处理数据
	bool Process( InterData &data , User &user ) ;

public:
	// 取得配置文件字符串形数据
	bool GetString( const char *key, char *buf ) ;
	// 取得配置文件整形的数据
	bool GetInteger( const char *key , int &value ) ;
	// 需要回调外部接发送的数据
	void OnDeliver( unsigned int fd, const char *data, int len , unsigned int cmd ) ;

private:
	// 环境对象
	ISystemEnv *  _pEnv ;
	// 用户会话管理对象
	CPlugUserMgr  _fdmgr ;
	// 是否开启插件模块
	bool 		  _enable ;
	// 通道管理对象
	CWayMgr		 *_waymgr ;
};


#endif /* PLUGIN_H_ */
