/*
 * msgmgr.h
 *
 *  Created on: 2011-11-3
 *      Author: humingqing
 */

#ifndef __MSGMGR_H__
#define __MSGMGR_H__

#include "interface.h"
#include "usermgr.h"
#include <map>
#include <set>
#include <vector>
#include <string>
#include <time.h>
#include <Mutex.h>
#include <stdint.h>
#include <TQueue.h>
using namespace std ;

#define MAX_TIME_OUT	180  // 最大超时时间
#define RESULT_SUCCESS  0    // 返回成功
#define RESULT_FAILED   1	 // 返回失败

class CMsgBuilder ;
class CFdClient ;
// 分组的客户
typedef vector<CFdClient *> CVecClient ;

// 单一客户接入
class CFdClient
{
public:
	CFdClient(){
		_fd    = NULL;
		_id    = 0 ;
		_ncar  = 0  ;
		_port  = 0  ;
		_ip    = "" ;
		_last  = time(0) ;
		_next  = _pre = NULL ;
		_group = 0 ;
	}
	virtual ~CFdClient(){}
	// 添加的属的MSG
	void AddMsgClient( CFdClient *p ) {
		share::Guard guard( _mutex ) ;
		_vecfd.push_back( p ) ;
	}
	//　删除所有属的MSG
	void DelMsgClient( CFdClient *p ) {
		share::Guard guard( _mutex ) ;
		if ( _vecfd.empty() )
			return ;
		CVecClient::iterator it ;
		for ( it = _vecfd.begin(); it != _vecfd.end(); ++ it ) {
			if ( *it != p )
				continue ;
			_vecfd.erase( it ) ;
			break ;
		}
	}
	// 取得所有MSG的队列
	CVecClient & GetMsgClients( void ) {
		share::Guard guard( _mutex ) ;
		return _vecfd ;
	}
	// 清除所有MSG的数据
	void ClearMsgClients( void ) {
		share::Guard guard( _mutex ) ;
		_vecfd.clear() ;
	}

	socket_t    *_fd ;     // 对应的FD
	uint32_t	 _id ;     // 节点ID
	uint16_t     _group ;  // 所属当前分类ID
	uint32_t	 _ncar ;   // 当前接入的车辆数
	string 	     _ip   ;   // IP地址
	uint16_t     _port ;   // 端口
	time_t       _last ;   // 最后访问时间

	// 指向节点链表指针
	CFdClient *_next, *_pre ;
	// 所属的MSG
	CVecClient   _vecfd ;
	// 处理同步锁操作
	share::Mutex _mutex ;
};

// 组ID的列表
typedef std::vector<uint16_t>  CVecGroupIds ;

// 按类型分组的客户链
class CGroupMgr
{
	// 同一组下面所有客户
	class CGroupClient
	{
	public:
		CGroupClient(){};
		~CGroupClient(){};
		// 添加客户端
		void AddClient( CFdClient *p ) {
			_vecClient.push_back( p ) ;
		}

		// 移除客户端
		bool RemoveClient( CFdClient *p ) {
			if ( _vecClient.empty() ) {
				return false ;
			}
			// 查找删除对应数据
			CVecClient::iterator it ;
			for ( it = _vecClient.begin(); it != _vecClient.end(); ++ it )
			{
				if ( p != *it ) {
					continue ;
				}
				_vecClient.erase(it) ;
				return true ;
			}
			return false ;
		}

		// 取得当前组下面客户端
		int  GetSize( void ) {return _vecClient.size();}
		// 取得当前组下面所有客户端
		int  GetClients( CVecClient &vec ) {
			if ( _vecClient.empty() ) {
				return 0 ;
			}
			int size = (int) _vecClient.size() ;
			for ( int i = 0; i < size; ++ i ) {
				vec.push_back( _vecClient[i] ) ;
			}
			return size ;
		}

	private:
		// 当前分组下面客户
		CVecClient _vecClient;
	};

	// 定义客户向量链
	typedef map<uint16_t, CGroupClient *>  CMapClient ;
public:
	CGroupMgr() ;
	~CGroupMgr() ;

	// 添加到当前的客户组里面
	void AddGroup( CFdClient *p ) ;
	// 根FD移除对应的Client
	bool RemoveGroup( CFdClient *p ) ;
	// 取得当前组个数
	int  GetSize( void ) { return _mapClient.size(); }
	// 取得当前组的数据
	int  GetGroup( uint16_t group, CVecClient &vec )  ;
	// 取得当前组的所有子组名称
	int  GetGroupIDs( uint16_t group, CVecGroupIds &vec ) ;

private:
	// 客户端分组管理
	CMapClient   _mapClient ;
	// 处理同步操作锁
	share::Mutex _mutex ;
};

// MSG服务器
class CMsgServer : public CFdClient
{
	// 处理MSG状态
	enum MSG_STATE{ MSG_OFFLINE = 0, MSG_WAITUSER , MSG_ONLINE };
public:
	CMsgServer() { _state = MSG_OFFLINE; };
	~CMsgServer(){};

	// 添加新的客户链接
	void AddClient( CFdClient *p ) {
		p->AddMsgClient( this ) ;

		share::Guard guard( _mutex ) ;
		_vecfd.push_back( p ) ;
	}
	// 移除对应的Client
	bool RemoveClient( CFdClient *p ) {
		_mutex.lock() ;
		if ( _vecfd.empty() ) {
			_mutex.unlock() ;
			return false ;
		}

		// 查找删除对应数据
		CVecClient::iterator it ;
		for ( it = _vecfd.begin(); it != _vecfd.end(); ++ it ) {
			if ( p != *it ) {
				continue ;
			}
			_vecfd.erase(it) ;
			_mutex.unlock() ;
			// 删对应的MSG关系
			p->DelMsgClient( this ) ;

			return true ;
		}
		_mutex.unlock() ;

		return false ;
	}
	// 是否为空
	int  GetSize( void ) {
		share::Guard guard( _mutex ) ;
		return _vecfd.size();
	}
	// 取得当前组下MSG下面的客户链接数
	int  GetClients( uint16_t group, CVecClient &vec ) {
		share::Guard guard( _mutex ) ;
		if ( _vecfd.empty() ) {
			return 0 ;
		}
		// 取得当前MSG下面组的客户链接个数
		CVecClient::iterator it ;
		for( it = _vecfd.begin(); it != _vecfd.end(); ++ it ) {
			CFdClient *p = (*it) ;
			if ( p->_group != group ) {
				continue ;
			}
			vec.push_back( p ) ;
		}
		return (int) vec.size() ;
	}

	// 取得当前组的前置机个数
	int GetGroupSize( uint16_t group ) {
		share::Guard guard( _mutex ) ;
		if ( _vecfd.empty() )
			return 0 ;

		int count = 0 ;
		CVecClient::iterator it ;
		for ( it = _vecfd.begin(); it != _vecfd.end(); ++ it ) {
			CFdClient *p = ( *it ) ;
			if ( p->_group != group ) {
				continue ;
			}
			++ count ;
		}
		return count ;
	}

	// 取得所有MSG下面的客户连接
	int GetClients( CVecClient &vec ) {
		share::Guard guard( _mutex ) ;
		if ( _vecfd.empty() ) {
			return 0 ;
		}
		vec = _vecfd ;
		return (int)vec.size() ;
	}
	// 取得MSG的状态
	bool IsOnline(){ return ( _state == MSG_ONLINE ) ; }
	// 是否等待用户应答状态
	bool IsWaitUser() { return ( _state == MSG_WAITUSER ); }
	// 设置MSG的状态
	void SetOnline( void )   { _state = MSG_ONLINE ; }
	// 设置MSG为等待用户状态
	void SetWaitUser( void ) { _state = MSG_WAITUSER ; }

private:
	// MSG服务器的状态
	MSG_STATE 	 _state ;
};

// MSG管理对象
class CMsgManager
{
	// 定义服务器的列表
	typedef map<uint64_t, CMsgServer*>  CMapMsgSrv ;
public:
	CMsgManager(ISystemEnv *pEnv, CMsgBuilder *builder ) ;
	~CMsgManager() ;

	// 添加MSG服务器
	bool AddMsgServer( CMsgServer *p ) ;
	// 添加客户端
	int  AddClient( uint64_t serverid , CFdClient *p ) ;
	// 移除对应的客户端分为三类前置机，MSG以及存储
	bool RemoveClient( CFdClient *p ) ;
	// 纷发已添加的用户名
	bool DispatherUsers( socket_t *sock , UserInfo *p , int count ) ;
	// 取得所有的MSG列表
	bool GetMsgList( CVecClient &vec ) ;

private:
	// 移除的为MSG节点
	bool RemoveMsgNode( CFdClient *p ) ;
	// 查找MSG
	CMsgServer * FindMsgNode( CFdClient *pmsg , bool erase ) ;

private:
	// 对应的MSG服务器的列表
	CMapMsgSrv    _mapSrv ;
	// 环境对象指针
	ISystemEnv   *_pEnv ;
	// 消息构建对象
	CMsgBuilder	 *_builder ;
	// MSG队列锁
	share::Mutex  _msgmutex ;
};

// 定义最大重发次数
#define MAX_RESEND_TIME   5

// 针对Client的管理
class CFdClientMgr
{
	// 重发次数的引用计数对象
	class CSeqRef{
		typedef map<unsigned int, unsigned int> CMapRef ;
	public:
		CSeqRef(){}
		~CSeqRef(){}
		// 添加发送次数引用
		unsigned int AddRef( unsigned int seq ) {
			share::Guard guard( _mutex ) ;
			CMapRef::iterator it = _ref.find( seq ) ;

			unsigned int count = 1 ;
			if ( it == _ref.end() ) {
				_ref.insert( make_pair( seq, count ) ) ;
			} else {
				count = it->second + 1 ;
				it->second = count ;
			}
			return count ;
		}

		// 删除记数的引用
		void DelRef( unsigned int seq ) {
			share::Guard guard( _mutex ) ;
			CMapRef::iterator it = _ref.find( seq ) ;
			if ( it == _ref.end() )
				return ;
			_ref.erase( it ) ;
		}

	private:
		CMapRef  	 _ref ;
		share::Mutex _mutex ;
	};
public:
	CFdClientMgr( ISystemEnv *pEnv, CMsgBuilder *pBuilder ) ;
	~CFdClientMgr() ;
	// 登陆服务器平台
	bool LoginClient( socket_t *sock, unsigned int seq, NodeLoginReq *req ) ;
	// 注销登陆平台
	bool LogoutClient( socket_t *sock, unsigned int seq ) ;
	// 链路测试
	bool LinkTest( socket_t *sock, unsigned int seq , NodeLinkTestReq *req ) ;
	// 请求分配用户名
	bool UserName( socket_t *sock, unsigned int seq ) ;
	// 请求分配MSG列表
	bool GetMsgList( socket_t *sock, unsigned int seq ) ;
	// 处理申请用户名的结果
	bool ProcUserName( socket_t *sock, unsigned int seq , MsgData *p ) ; // 这个序号来自申请用户名的序号
	// 重新补发用户列表数据
	bool DispatchUser( socket_t *sock ) ;
	// 重新补发未收到的数据
	bool ResendMsg( socket_t *sock, MsgData *p , ListFd &fds ) ;
	// 检测当前FD是否存在
	bool CheckFdClient( socket_t *sock ) ;
	// 移除客户端
	bool RemoveFdClient( socket_t *sock ) ;
private:
	// 添加客户端
	bool AddClient( CFdClient *p ) ;
	// 根据FD取得对应CLIENT
	CFdClient * GetFdClient( socket_t *sock ) ;
	// 移除客户端
	bool RemoveClient( CFdClient *p ) ;
	// 通知添加MSG处理
	void NotifyAddMsg( CFdClient *p ) ;

private:
	// 锁操作
	share::RWMutex   	_rwmutex ;
	// 连接对象队列
	TQueue<CFdClient>   _fdqueue;
	// 组的管理
	CGroupMgr 		   *_pGroupMgr ;
	// MSG服务器管理
	CMsgManager		   *_pMsgMgr ;
	// 消息构构对象
	CMsgBuilder		   *_pBuilder ;
	// 用户管理对象
	CUserMgr		   *_pUserMgr ;
	// 重计数的引用次数
	CSeqRef				_seqref ;
	// 环境对象指针
	ISystemEnv 	       *_pEnv ;
};

// 节点管理对象管理所有结点情况
class CNodeMgr : public IMsgNotify
{
public:
	CNodeMgr(ISystemEnv *pEnv) ;
	~CNodeMgr() ;
	// 处理所有数据
	void Process( socket_t *sock, const char *data, int len ) ;
	// 断连事件
	void Close( socket_t *sock ) ;
	// 处理结果回调对象
	void NotifyMsgData( socket_t *sock, MsgData *p , ListFd &fds, unsigned int op ) ;

private:
	// 处理数据服务器
	ISystemEnv *   _pEnv ;
	// 连接管理
	CFdClientMgr * _clientmgr ;
	// 消息构建对象
	CMsgBuilder	 * _builder ;
};

#endif /* MSGLIST_H_ */
