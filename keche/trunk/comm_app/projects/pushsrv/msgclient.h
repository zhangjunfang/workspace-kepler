/**********************************************
 * msgclient.h
 *
 *  Created on: 2011-07-28
 *    Author:   humingqing
 *    Comments: 实现与消息服务中心通信以及数据转换
 *********************************************/

#ifndef __MsgCLIENT_H__
#define __MsgCLIENT_H__

#include "interface.h"
#include <BaseClient.h>
#include <OnlineUser.h>
#include <time.h>
#include <Session.h>
#include <interpacker.h>
#include <set>
#include <string>
using namespace std ;

class MsgClient : public BaseClient , public IMsgClient
{
	class CSubscribeMgr
	{
	public:
		CSubscribeMgr() {}
		~CSubscribeMgr() {}
		// 添加MACID的数据
		bool AddSubId( const char *name )
		{
			share::Guard guard( _mutex ) ;
			set<string>::iterator it = _setid.find( name ) ;
			if ( it != _setid.end() ) {
				return false ;
			}
			_setid.insert( set<string>::value_type(name) ) ;

			return true ;
		}

		// 删除MACID的数据
		bool DelSubId( const char *name )
		{
			share::Guard guard( _mutex ) ;

			set<string>::iterator it = _setid.find( name ) ;
			if ( it == _setid.end() ) {
				return false ;
			}
			_setid.erase(it) ;

			return true ;
		}

		// 是否为空
		bool IsEmpty( void )
		{
			share::Guard guard( _mutex ) ;
			return _setid.empty() ;
		}

		// 取得所有有MACID
		set<string> & GetSubIds( void )
		{
			share::Guard guard( _mutex ) ;
			return _setid ;
		}

	private:
		// 需要处理同步问题
		share::Mutex    _mutex ;
		// 订阅的的数据
		set<string>  	_setid ;
	};
public:
	MsgClient( void ) ;
	virtual ~MsgClient() ;

	// 初始化
	virtual bool Init( ISystemEnv *pEnv );
	// 开始服务
	virtual bool Start( void );
	// 停止服务
	virtual void Stop();
	// 数据到来时处理
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 向MSG上传消息
	virtual void HandleMsgData( const char *macid, const char *data, int len ) ;
	// 添加用户处理
	virtual void AddUser( const char *ip, unsigned short port, const char *user, const char *pwd ) ;
	// 处理删除服务
	virtual void DelUser( const char *ip, unsigned short port ) ;
	// 添加订阅机制
	virtual void AddDemand( const char *name, int type ) ;
	// 取消定订阅
	virtual void DelDemand( const char *name, int type ) ;

public:
	// 断开连接处理
	virtual void on_dis_connection( socket_t *sock );
	//为服务端使用
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

	virtual void TimeWork();
	virtual void NoopWork();
	// 构建登陆信息数据
	virtual int  build_login_msg(User &user, char *buf, int buf_len);

protected:
	// 纷发内部数据
	void HandleInnerData( socket_t *sock, const char *data, int len ) ;
	// 纷发登陆处理
	void HandleSession( socket_t *sock, const char *data, int len ) ;
	// 处理离线用户
	void HandleOfflineUsers( void ) ;
	// 处理在线用户
	void HandleOnlineUsers(int timeval) ;
	// 通知所有用启
	void SendOnlineData( const char *data, int len ) ;
	// 纷发登陆用启的订阅
	void SendDemandData( socket_t *sock ) ;

private:
	// 环境指针
	ISystemEnv  *	_pEnv ;
	// 最后一次访问时间
	time_t		  	_last_handle_user_time ;
	// 在线用户处理
	OnlineUser   	_online_user;
	// 消息转发对象
	IMsgClient     *_pMsgClient ;
	// 会话管理对象
	CSessionMgr  	_session ;
	// 分包对象处理
	CInterSpliter   _packspliter ;
	// 订阅数据管理
	CSubscribeMgr   _macidsubmgr ;
	// 组订阅的管理
	CSubscribeMgr   _groupsubmgr ;
};

#endif /* LISTCLIENT_H_ */
