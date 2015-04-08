/*
 * groupuser.h
 *
 *  Created on: 2011-11-2
 *      Author: humingqing
 *       这里实现使用到组的概念，也就算是简单访照linux操作系统用户权限管理机制，针对一系列用户进行分组处理，
 *       对于同一组的用户所有数据总和为MSG的消息总和，这里主要是针对存储分析等的分流操作，使用算法为简单的针对MAC_ID进行HASH分配方式来进行数据的均衡处理
 *       其中对于同一个用户下面可能存在多个连接情况，对于纷发数据同一用户连接上负载均衡处理，这里就需要采用简单的轮转算来处理
 *       这里设计为一个多级树状结构对象，因为用户下面可以分为组，而组下面可以为多个用户，而单个用户下面却可以是多个连接，一个多树不定叉的树将结构
 */

#ifndef __GROUPUSER_H__
#define __GROUPUSER_H__

#include <interface.h>
#include <assert.h>
#include <murhash.h>
#include <TQueue.h>

// 按位进行分布处理
#define MAX_TYPE_CMD    4			 // 类型最多个数
// 命令数据枚举
static unsigned int _gcmd[] = { PIPE_SEND_CMD, UWEB_SEND_CMD, SAVE_SEND_CMD, SEND_SEND_CMD };
// 定义一个Hash的种子
#define MUR_HASH_SEED  0xfffff
class CGroupUserMgr : public IGroupUserMgr
{
	// 用户扩展对象类
	struct UserEx
	{
		User _user;	  // 用户基本数据
		int _user_cmd;  // 用户连接类型
	};

	struct _ConnList;
	// 管理所有用户链表
	struct _UserList
	{
		UserEx _user;   	   // 当前用户值
		_UserList *_next, *_pre;  // 双向指针
		_ConnList *_conn;         // 指向所属的连接
	};

	struct _ConnHeader;
	// 分组的连接成员链表
	struct _ConnList
	{
		_UserList *_user;		 // 用户指针
		_ConnList *_next, *_pre; // 双向指针方便删除
		_ConnHeader *_header;		 // 指向连接头部
	};

	struct _HeaderList;
	// 连接对象链表头
	struct _ConnHeader
	{
		unsigned int _size;    // 当前连接个数
		_ConnList * _pcur;    // 当前使用连接
		std::string _group;   // 所属的组的名称
		std::string _userid;  // 所属连接组的用户
		_ConnList * _header;  // 分组成员头指针
		_ConnList * _tail;  // 分组成员的尾指针
		_HeaderList * _lsthead; // 链表头指针
	};

	// 连接头处理
	struct _HeaderList
	{
		_ConnHeader *_header;       // 头数指针
		_HeaderList *_next, *_pre; // 头尾指针处理
	};

	// 用户ID索引
	typedef std::map< std::string, _UserList* >   CMapIdIndex;
	// 当前用户的连接
	typedef std::map< std::string, _ConnHeader* > CMapConnGroup;

	// 用户对应的连接管理
	class CUserConnMgr
	{
		// 用户组的结构体
		typedef vector< _HeaderList* > VecGroup;
		// 用户组的链表结构
		struct _GroupList
		{
			_GroupList( )
			{
				_size = 0;
				_data = NULL;
				_next = NULL;
				_pre = NULL;
			}
			int _size;
			std::string _group;   // 所属的组的名称
			VecGroup _vec;	 // 组成员对象
			_HeaderList *_data;	 // 默认为空单节点情况
			_GroupList *_next;	 // 后一个节点指针
			_GroupList *_pre;	 // 前一个节点指针
		};
	public:
		CUserConnMgr( ) {} ;
		~CUserConnMgr( ) { Clear(); };

		// 添加用户连接处理
		bool AddUserConn( _UserList * p )
		{
			string group;
			string userid = p->_user._user._user_id;
			// 如果为分组的情况处理
			if ( ! p->_user._user._user_name.empty() ) {
				group = p->_user._user._user_type + p->_user._user._user_name;
			}
			// 如果多连接情况的处理
			size_t pos = userid.find( ":" );
			if ( pos != string::npos ) {
				userid = userid.substr( 0, pos );
			}

			_ConnHeader *header = NULL;
			CMapConnGroup::iterator it = _mapUserConn.find( userid );
			if ( it == _mapUserConn.end() ) {
				header = new _ConnHeader;
				header->_group = group;
				header->_userid = userid;
				header->_pcur = NULL;
				header->_size = 0;
				header->_tail = header->_header = NULL;
				// 添加到用户链上面
				_mapUserConn.insert( make_pair( userid, header ) );

				// 添加用户连接链上面
				AddHeader( header, p->_user._user_cmd );
			} else {
				header = it->second;
				// 如果组不一致用户名相同就不让它登陆了
				if ( header->_group != group ) {
					return false;
				}
			}

			_ConnList *conn = new _ConnList;
			conn->_user = p;
			conn->_header = header;
			p->_conn = conn;
			conn->_next = NULL;
			conn->_pre = NULL;
			header->_size = header->_size + 1;

			// 如果为空的情况
			if ( header->_header == NULL && header->_tail == NULL ) {
				header->_pcur = conn;
				header->_header = header->_tail = conn;
			} else { // 如果不为空头节点
				conn->_pre = header->_tail;
				header->_tail->_next = conn;
				conn->_next = NULL;
				header->_tail = conn;
			}

			return true;
		}

		// 删除当前连接处理
		void DelUserConn( _UserList *p )
		{
			if ( p == NULL )
				return;

			_ConnList *conn = p->_conn;
			_ConnHeader *header = conn->_header;

			unsigned int cmd = conn->_user->_user._user_cmd;

			// 如果只有一个元素的情况
			if ( conn == header->_header && conn == header->_tail ) {
				header->_header = header->_tail = NULL;
			} else if ( conn == header->_header ) {
				header->_header = conn->_next;
				header->_header->_pre = NULL;
			} else if ( conn == header->_tail ) {
				header->_tail = conn->_pre;
				header->_tail->_next = NULL;
			} else {
				conn->_pre->_next = conn->_next;
				conn->_next->_pre = conn->_pre;
			}
			header->_size = header->_size - 1;
			header->_pcur = header->_header;  // 断开连接直接指回头部连接发送
			// 删除连接管理
			delete conn;

			// 如果数据已为空了，就清空用户的数据了
			if ( header->_header == NULL && header->_size == 0 ) {
				// 移除当前用户会话
				RemoveConn( header->_userid );
				// 删除组的索引
				_HeaderList *phead = header->_lsthead ;
				if( phead != NULL ) {
					DelGroup( phead , cmd );
					delete _headqueue.erase( phead ) ;
				}
				// 移除组的对应关系
				delete header;
			}
		}

		// 取得可用发送用户
		int GetSendUsers( vector< User > &vec, unsigned int hash, unsigned int cmd )
		{
			// 没有可用的用户
			if ( _headqueue.size() == 0 ) {
				return 0;
			}

			int count = 0;
			for ( int i = 0 ; i < MAX_TYPE_CMD ; ++ i ) {
				if ( ! ( cmd & _gcmd[i] ) )
					continue;
				if ( _vecgroup[i].size() == 0 )
					continue;

				_GroupList *p = _vecgroup[i].begin() ;
				while ( p != NULL ) {
					if ( GetSendUsers( p, vec, hash, cmd ) ) {
						++ count;
					}
					p = p->_next;
				}
			}
			return count;
		}

		// 根据用户ID取得用户，可以为同应用的连接
		bool GetUserByUserId( const string &userid, User &user )
		{
			share::Guard guard( _mutex );

			if ( _mapUserConn.empty() )
				return false;

			CMapConnGroup::iterator it = _mapUserConn.find( userid );
			if ( it == _mapUserConn.end() )
				return false;

			_ConnHeader *header = it->second;
			if ( header->_size == 0 || header->_header == NULL )
				return false;

			if ( header->_pcur == NULL )
				header->_pcur = header->_header;
			_ConnList *conn = header->_pcur;
			//  如果连接个数大于1个的处理
			if ( header->_size > 1 ) {
				// 如果为尾部链就直接指到头部处理
				if ( header->_pcur == header->_tail ) {
					header->_pcur = header->_header;
				} else {
					header->_pcur = header->_pcur->_next;
				}
			}
			// 取得用户发送数据
			user = conn->_user->_user._user;

			return true;
		}

	private:
		// 可用的发送的用户数据
		bool GetSendUsers( _GroupList *pcmd, vector< User > &vec, unsigned int hash, unsigned int cmd )
		{
			share::Guard guard( _mutex );

			_HeaderList *p = NULL;
			// 如果单节占
			if ( pcmd->_size == 0 ) {
				p = pcmd->_data;  // 直接取节点值
			} else {
				if ( pcmd->_size == 1 ) {  // 如果为组的单接入
					p = pcmd->_vec[0];
				} else { // 否则为组的多应用接入
					p = pcmd->_vec[hash % pcmd->_size];
				}
			}

			_ConnHeader * header = p->_header;
			if ( header->_size == 0 || header->_header == NULL )
				return false;

			if ( header->_pcur == NULL )
				header->_pcur = header->_header;
			_ConnList *conn = header->_pcur;
			// 如果用户当前为空直接返回
			if ( conn->_user == NULL )
				return false;

			// 如果为不在线用户就直接返回了
			if ( conn->_user->_user._user._user_state != User::ON_LINE )
				return false;

			// 如果连接数大于1才进行循环处理
			if ( header->_size > 1 ) {
				if ( header->_pcur == header->_tail ) {
					header->_pcur = header->_header;
				} else {
					header->_pcur = header->_pcur->_next;
				}
			}
			// 添加可发送的队列中
			vec.push_back( conn->_user->_user._user );
			// 如果成功返回结果
			return true;
		}

		// 移除用户连接处理
		void RemoveConn( const string &userid )
		{
			CMapConnGroup::iterator it = _mapUserConn.find( userid );
			if ( it == _mapUserConn.end() )
				return;
			_mapUserConn.erase( it );
		}

		// 添加数据
		void AddHeader( _ConnHeader *p, unsigned int cmd )
		{
			_HeaderList *temp = new _HeaderList;
			temp->_header = p;
			temp->_next = NULL;
			temp->_pre = NULL;
			p->_lsthead = temp;

			_headqueue.push( temp ) ;
			// 添加组的多级索引中
			AddGroup( temp, cmd );
		}

		// 移除头部数据
		void RemoveHeader( _ConnHeader *header )
		{
			if ( header->_header != NULL ) {
				_ConnList *pre, *p = header->_header;
				while ( p != NULL ) {
					pre = p;
					p = p->_next;
					delete pre;
				}
				header->_header = header->_tail = NULL;
			}
			delete header;
		}

		// 清理所有数据
		void Clear( void )
		{
			int size = 0 ;
			_HeaderList *p = _headqueue.move( size ) ;
			if ( size == 0 )
				return;

			_HeaderList *pre ;
			while ( p != NULL ) {
				pre = p;
				p = p->_next;
				RemoveHeader( pre->_header );
				delete pre;
			}

			_mapUserConn.clear();
		}

		// 查找所属组
		_GroupList * FindGroup( TQueue<_GroupList> &cmd, const string &group )
		{
			// 如果为空或者没有组的关系
			if ( cmd.size() == 0 || group.empty() ) {
				return NULL;
			}

			_GroupList *p = cmd.begin() ;
			// 查找所属组
			while ( p != NULL ) {
				if ( p->_group == group ) {
					return p;
				}
				p = p->_next;
			}

			return NULL;
		}

		// 添加到查找命令中
		void AddGroup( _HeaderList *p, unsigned int cmd )
		{
			// 添加到对应的组的关系中
			for ( int i = 0 ; i < MAX_TYPE_CMD ; ++ i ) {
				if ( ! ( cmd & _gcmd[i] ) )
					continue;

				string &group = p->_header->_group;
				// 查找所属组
				_GroupList *pcmd = FindGroup( _vecgroup[i], group );
				if ( pcmd == NULL ) { // 如果不存在或者单组
					pcmd = new _GroupList;
					pcmd->_group = group;
					pcmd->_next = NULL;
					_vecgroup[i].push( pcmd ) ;
				}
				// 如果组为空就是单节点模式
				if ( group.empty() )
					pcmd->_data = p;
				else { // 如果为以组的方式接入
					pcmd->_vec.push_back( p );
					++ pcmd->_size;
				}
			}
		}

		// 删除查找数据
		void DelGroup( _HeaderList *p, unsigned int cmd )
		{
			// 遍历所有可用组的类型
			for ( int i = 0 ; i < MAX_TYPE_CMD ; ++ i ) {
				if ( ! ( cmd & _gcmd[i] ) )
					continue;
				if ( _vecgroup[i].size() == 0 )
					continue;

				// 查找相关的组
				_GroupList *pcmd = FindGroup( _vecgroup[i], p->_header->_group );
				if ( pcmd == NULL ) {  // 如果没有组的单点情况
					pcmd = _vecgroup[i].begin() ;
					while ( pcmd != NULL ) {
						if ( pcmd->_data == p ) {
							delete _vecgroup[i].erase(pcmd) ;
							break;
						}
						pcmd = pcmd->_next;
					}
				} else {  // 如果查找到组的情况
					VecGroup::iterator it;
					for ( it = pcmd->_vec.begin(); it != pcmd->_vec.end() ; ++ it ) {
						if ( * it != p )
							continue;
						pcmd->_vec.erase( it );
						-- pcmd->_size;
						break;
					}
					// 如果元素个数为空则需要清理节点头
					if ( pcmd->_size == 0 ) {
						delete _vecgroup[i].erase( pcmd ) ;
					}
				}
			}
		}

	private:
		// 用户连接管理
		CMapConnGroup 		_mapUserConn;
		// 用户头尾指针
		TQueue<_HeaderList> _headqueue;
		// 多个线程同时读引起空指针
		share::Mutex 		_mutex;
		// 按组的类型分布HASH的组表
		TQueue<_GroupList>  _vecgroup[MAX_TYPE_CMD];
	};

public:
	// 初始化数据
	CGroupUserMgr( ){
		_pusermgr = new CUserConnMgr;
	}
	// 回收所有内存
	~CGroupUserMgr( ) {
		delete _pusermgr;
	}

	//0 success; -1,此用户用已经存在,且不论其是否在线。
	bool AddUser( const std::string &user_id, const User &user )
	{
		share::RWGuard guard( _rwmutex, true );
		// 查找用户是否存在了
		if ( FindIdIndex( user_id, false ) != NULL )
			return false;

		_UserList * p = new _UserList;
		p->_user._user = user ;
		p->_user._user_cmd = GetCmdByUserType( user._user_type ); // 取得对应命令类型
		p->_next = p->_pre = NULL;

		// 如果添加到用户组失败就直接返回了
		if ( ! _pusermgr->AddUserConn( p ) ) {
			delete p;
			return false;
		}
		_userqueue.push( p ) ;

		p->_user._user._fd->_ptr = p ;
		// 添加索引操作
		_mapIdIndex.insert( make_pair( user_id, p ) );

		return true;
	}

	// 取得用户通过FD
	bool GetUserBySocket( socket_t *sock, User &user )
	{
		// 取得用户通过用户的fd
		share::RWGuard guard( _rwmutex, false );
		_UserList *p = FindFdIndex( sock, false );
		if ( p == NULL )
			return false;
		// 返回用户信息
		user = p->_user._user;
		return true;
	}

	// 通过用户ID来取得用户
	bool GetUserByUserId( const std::string &user_id, User &user, bool bgroup = false )
	{
		// 取得用户通过用户ID
		share::RWGuard guard( _rwmutex, false );
		// 查找用户
		_UserList *p = FindIdIndex( user_id, false );
		if ( p == NULL ) {
			if ( ! bgroup || user_id.empty() )
				return false;

			// 如果有组处理，需要处理当前组
			size_t pos = user_id.find( ":" );
			if ( pos == string::npos ) {
				return false;
			}
			// 取得当前所属的用户
			string guserid = user_id.substr( 0, pos );
			// get user from user list
			return _pusermgr->GetUserByUserId( guserid, user );
		}
		// 返回找着当前用户
		user = p->_user._user;
		return true;
	}

	// 删除用户通过FD
	void DeleteUser( socket_t *sock )
	{
		// 删除组的关系
		share::RWGuard guard( _rwmutex, true );
		_UserList *p = FindFdIndex( sock, true );
		if ( p == NULL )
			return;

		FindIdIndex( p->_user._user._user_id, true );
		RemoveUserList( p );
	}

	// 删除用户
	void DeleteUser( const std::string &user_id )
	{
		// 删除组关系
		share::RWGuard guard( _rwmutex, true );
		_UserList *p = FindIdIndex( user_id, true );
		if ( p == NULL )
			return;

		FindFdIndex( p->_user._user._fd, true );
		RemoveUserList( p );
	}

	// 取得在线用户
	bool GetOnlineUsers( std::vector< User > &vec )
	{
		// 取得在线用户
		share::RWGuard guard( _rwmutex, false );
		if (  _userqueue.size() == 0 )
			return false;

		_UserList *p = _userqueue.begin() ;
		while ( p != NULL ) {
			// 遍历所有当前用户
			if ( p->_user._user._user_state == User::ON_LINE
					&& p->_user._user._fd != NULL ) {
				vec.push_back( p->_user._user );
			}
			p = p->_next;
		}

		return true;
	}

	// 更改用户的状态
	bool SetUser( const std::string &user_id, User &user )
	{
		share::RWGuard guard( _rwmutex, true );
		_UserList *p = FindIdIndex( user_id, false );
		if ( p == NULL )
			return false;
		p->_user._user = user;
		p->_user._user_cmd = GetCmdByUserType( user._user_type );
		return true;
	}

	// 检测是否需要发送的数据
	bool GetSendGroup( std::vector< User > &vec, unsigned int hash, unsigned int cmd )
	{
		share::RWGuard guard( _rwmutex, false );
		// 取得可以发送数据的用户
		return ( _pusermgr->GetSendUsers( vec, hash, cmd ) > 0 );
	}

	// 取得Hash值
	unsigned int GetHash( const char *key, int len )
	{
		// 产生Hash数据
		return mur_mur_hash2( ( void* ) key, len, MUR_HASH_SEED );
	}

	// 当前所有连接数
	int GetSize( void )
	{
		share::RWGuard guard( _rwmutex, false );
		return _userqueue.size() ;
	}

private:
	// 移除用户数据
	void RemoveUserList( _UserList * p )
	{
		// 从用户队列中移除
		p = _userqueue.erase( p ) ;
		// 移除用户
		_pusermgr->DelUserConn( p );

		//
		if ( p->_user._user._fd != NULL ) {
			p->_user._user._fd->_ptr = NULL ;
		}

		delete p;
	}

	// 移除FD索引
	_UserList * FindFdIndex( socket_t *sock, bool erase )
	{
		if ( sock->_ptr == NULL )
			return NULL ;

		_UserList *p = (_UserList *) sock->_ptr ;
		if ( erase )
			sock->_ptr = NULL ;
		if ( p->_user._user._fd != sock )
			return NULL ;

		return p;
	}

	// 移除用户ID的索引
	_UserList * FindIdIndex( const string &userid, bool erase )
	{
		CMapIdIndex::iterator it = _mapIdIndex.find( userid );
		if ( it == _mapIdIndex.end() )
			return NULL;
		_UserList *p = it->second;
		if ( erase )
			_mapIdIndex.erase( it );
		return p;
	}

	// 通过用户类型来取得对应命令
	unsigned int GetCmdByUserType( const string &msg_type )
	{
		if ( msg_type == COMPANY_TYPE ) {
			return PIPE_SEND_CMD;
		} else if ( msg_type == WEB_TYPE ) {
			return UWEB_SEND_CMD;
		} else if ( msg_type == STORAGE_TYPE ) {
			return SAVE_SEND_CMD;
		} else if ( msg_type == SEND_TYPE ) {
			return SEND_SEND_CMD;
		}
		return 0x00000000;
	}

private:
	share::RWMutex    _rwmutex;
	TQueue<_UserList> _userqueue;
	CMapIdIndex 	  _mapIdIndex; // 用户ID的索引
	CUserConnMgr *    _pusermgr;   // 用户数据管理
};

#endif /* GROUPUSER_H_ */
