/*
 * filecache.h
 *
 *  Created on: 2012-4-25
 *      Author: humingqing
 *
 *  实现数据在发送过程，未成功发送到MSG或者与MSG连接异常时的数据进行保存，当恢复正常时将数据发送回MSG
 */

#ifndef __FILECACHE_H__
#define __FILECACHE_H__

#include <map>
#include <set>
#include <list>
#include <Mutex.h>
#include <string>
#include <time.h>
using namespace std ;

#define IOHANDLE_FAILED  	-1   // 发数数据失败
#define IOHANDLE_SUCCESS  	0	 // 成功
#define IOHANDLE_ERRDATA 	1	 // 无效数据

// 处理数据回调对象
class IOHandler
{
public:
	// 处理IO数据
	virtual ~IOHandler() {} ;
	// 处理数据的回调方法
	virtual int HandleQueue( const char *sid , void *buf, int len , int msgid = 0 ) = 0 ;
};

class CFileQueue ;
// 数据缓存对象
class CDataCache
{
	// 文件队列映射
	typedef map<string, CFileQueue*>  CMapQueue ;
public:
	CDataCache( IOHandler *handler ) ;
	~CDataCache() ;

	// 设置当前目录
	void Init( const char *szroot ) ;
	// 加载用户数据
	void Load( const string &sid ) ;
	// 添加新数据
	bool Push( const string &sid , void *buf, int len ) ;
	// 弹出数据
	int  Pop( const string &sid ) ;

private:
	// 添加新的数据对象
	CFileQueue * AddNewQueue( const string &sid ) ;
	// 清理所有数据
	void Clear( void ) ;

private:
	// 数据处理句柄
	IOHandler  * _handler ;
	// 文件映射队列
	CMapQueue	 _queue ;
	// 文件锁处理
	share::Mutex _mutex ;
	// 基地址
	string 		_basedir ;
};

#define MAX_CHECK_TIME      30   // 最长激活时间
#define USER_ERR_NOMARL      0   // 正常状态
#define USER_ERR_EMPTY		-1   // 是否为空
#define USER_ERR_OVERFLUX   -2   // 超时流速状态

// 文件CACHE设计
class CFileCache
{
	// 在线用户队列
	class CListUser
	{
		// 流速控制
		class CFluxCtrl
		{
		public:
			CFluxCtrl() {
				_maxsend  = 0 ;
				_curcount = 0 ;
				_curtime  = time(NULL) ;
			};
			~CFluxCtrl() {}

			// 检测是否超出流速
			bool OverSpeed( void ) {
				if ( _maxsend <= 0 )
					return false ;

				time_t now = time(NULL) ;
				if ( _curtime != now ) {
					_curcount = 0 ;
					_curtime  = now ;
				}
				++ _curcount ;
				return ( _curcount > _maxsend ) ;
			}

			// 设置发送速度
			void SetSpeed( unsigned int speed ){
				_maxsend = speed ;
			}

		private:
			// 最大发送速度
			unsigned int _maxsend  ;
			// 当前发送个数
			unsigned int _curcount ;
			// 当前时间
			time_t		 _curtime  ;
		};

	public:
		struct _stUid
		{
			string	  userid ; // 当前数据别名ID
			time_t    now ;    // 最后一次操作时间
			CFluxCtrl flux ;   // 每个数据库一个流速控制
			int 	  state ;
		};
		typedef list<_stUid>  ListUid ;
	public:
		CListUser() {};
		~CListUser(){};

		// 添加用户
		void AddUser( const string &userid, unsigned int speed )
		{
			set< string >::iterator it = _setid.find( userid );
			if ( it != _setid.end() )
				return;

			_stUid uid;
			uid.userid = userid;
			uid.now    = time( NULL );
			uid.flux.SetSpeed( speed );
			uid.state  = USER_ERR_NOMARL;
			_list.push_back( uid );

			_setid.insert( set< string >::value_type( userid ) );
		}

		// 删除用户
		void DelUser( const string &userid )
		{
			_setid.erase( userid );

			if ( _list.empty() )
				return;

			list< _stUid >::iterator it;
			for ( it = _list.begin(); it != _list.end() ; ++ it ) {
				_stUid &uid = ( * it );
				if ( uid.userid != userid ) {
					continue;
				}
				_list.erase( it );
				break;
			}
		}

		// 删除SET的索引
		void DelSet( const string &userid ){ _setid.erase( userid ) ; }
		// 返回当前用户列表
		ListUid  &ListUser( void ){ return _list ;}

	private:
		// 在线用户队列
		ListUid 	 _list ;
		// 用户ID的索引
		set<string>  _setid ;
	};

public:
	CFileCache(IOHandler *handler) ;
	~CFileCache() ;

	// 初始化系统
	bool Init( const char *dir, int maxsend = 0 ) ;
	// 保存数据
	bool WriteCache( const char *sid, void *buf, int len ) ;
	// 在线用户
	void Online( const char *sid ) ;
	// 离线用户
	void Offline( const char *sid ) ;
	// 线程运行对象
	bool Check( void ) ;

private:
	// 在线用户队列
	CListUser			 _userlst;
	// 处理数据的IO对象
	IOHandler			*_handler ;
	// 数据缓存对象
	CDataCache			 _datacache ;
	// 每个用户最大流速
	unsigned int 		 _maxsend;
	// 同步操作用户锁
	share::Mutex 		 _mutex ;
};

#endif /* FILECACHE_H_ */
