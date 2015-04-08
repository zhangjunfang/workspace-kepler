/*
 * usermgr.h
 *
 *  Created on: 2012-5-17
 *      Author: humingqing
 *  用户管理对象
 */

#ifndef __USERMGR_H__
#define __USERMGR_H__

#include <map>
#include <time.h>
#include <string>
#include <Mutex.h>
#include <TQueue.h>
#include <SocketHandle.h>

// 用户基本信息
struct _UserInfo
{
	enum State{OFF_LINE,WAITING_RESP,ON_LINE,DISABLED};
	_UserInfo()
	{
		_fd     = NULL ;
		_active = 0 ;
		_login  = 0 ;
		_state  = OFF_LINE ;
	}

	socket_t   *_fd ;
	std::string _key ;
	std::string _code ;
	std::string _ip ;
	int 		_port ;
	time_t  	_active;
	time_t  	_login ;
	State 		_state;
};

// 用户列表
struct _PairUser
{
	_UserInfo tcp , udp ;
	_PairUser *_next, *_pre ;
};

// 用户动作通知
class IPairNotify
{
public:
	virtual ~IPairNotify(){}
	// 关闭用户通知
	virtual void CloseUser( socket_t *sock ) = 0 ;
	// 通知用户上线
	virtual void NotifyUser( socket_t *sock, const char *key ) = 0 ;
};

// 用户管理对象
class CUserMgr
{
	typedef std::map<std::string,_PairUser *> CMapUser ;
	typedef std::map<socket_t*,_PairUser*> CMapFds ;
public:
	CUserMgr(IPairNotify *notify) ;
	~CUserMgr() ;
	// 添加用户
	bool AddUser( socket_t *sock ) ;
	// 注册
	bool OnRegister( socket_t *sock, const char *ckey, const char *ip, int port , socket_t *usock, std::string &key ) ;
	// 鉴权
	bool OnAuth( socket_t *sock, const char *akey, const char *code , std::string &key ) ;
	// 心跳处理
	bool OnLoop( socket_t *sock, const char *akey ) ;
	// 检测是否超时
	void Check( int timeout ) ;
	// 根据连接取得用户
	_PairUser * GetUser( socket_t *sock ) ;
	// 根据接入来取得用户
	_PairUser * GetUser( const char *key ) ;

private:
	// 产生KEY来处理
	const std::string GenKey( void ) ;
	// 添加新的用户
	void AddList( _PairUser *p ) ;
	// 删除用户数据
	void DelList( _PairUser *p , bool notify ) ;
	// 移除索引
	void RemoveIndex( _PairUser *p ) ;
	// 清理数据
	void Clear( void ) ;
	// 添加队列中
	bool AddMapFd( socket_t *sock, _PairUser *p ) ;
	// 取得数据
	_PairUser * GetMapFd( socket_t *sock ) ;
	// 添加接入码索引
	bool AddMapCode( const char *key, _PairUser *p ) ;

private:
	// 操作锁的处理
	share::Mutex 	  _mutex ;
	// 用户管理对队
	TQueue<_PairUser> _queue ;
	// 用户接入码索引
	CMapUser   		  _kuser;
	// TCP的FD索引
	CMapFds    		  _tcps ;
	// 用户通知
	IPairNotify *	  _notify;
};

#endif /* USERMGR_H_ */
