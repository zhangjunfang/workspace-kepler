/*
 * filecache.h
 *
 *  Created on: 2011-12-21
 *      Author: humingqing
 *  数据缓存对象
 */

#ifndef __FILECACHEEX_H__
#define __FILECACHEEX_H__

#include "interface.h"
#include <Mutex.h>
#include <Thread.h>
#include <filecache.h>

#define DATA_FILECACHE   	0  // 数据缓存
#define DATA_ARCOSSDAT	 	1  // 跨域控制

#define VECHILE_LEN   21
#define VECHILE_OFF   0
#define VECHILE_ON    1

struct ArcossData
{
	int     areacode ;
	char    vechile[VECHILE_LEN] ;
	char    color ;
	uint64_t time  ;
	char    state ; // 是否断连状态
	ArcossData *_next ;
	ArcossData *_pre  ;
};

// 跨域补发请求处理
class CArcossCache
{
	struct ArcossHeader
	{
		int 		_size ;
		ArcossData *_head ;
		ArcossData *_tail ;
	};
	// 数据存放结构单元
	typedef map<int,ArcossHeader *>  CMapArcoss;
	// 索引存放的MAP
	typedef map<string,ArcossData *> CMapIndex ;
public:
	CArcossCache( IOHandler *handler ) ;
	~CArcossCache() ;
	// 保存文件路径
	void Init( const char *szroot ) ;
	// 上线加载数据
	void Load( int id ) ;
	// 添加新数据
	void Push( int id, char color, char *vechile ) ;
	// 结束跨域处理
	void Pop( int id, char color, char *vechile ) ;
	// 修改数据时间
	void Change( int id, char color, char *vechile ) ;
	// 断连接情况
	void Offline( int id ) ;
	// 检测数据
	bool Check( int id ) ;

private:
	// 序列化数据
	void serialize( int id ) ;
	// 添加新数据
	bool AddNew( int id, char color, char *vechile ) ;
	// 移除数据
	bool Remove( int id, char color, char *vechile ) ;
	// 修改操作
	bool ChangeEx( int id, char color, char *vechile ) ;
	// 反序列化数据
	void unserialize( int id ) ;
	// 删除文件
	void deletefile( int id ) ;
	// 添加新数据
	bool AddData( int id, char color, char *vechile , uint64_t time , char state ) ;
	// 移除索引
	ArcossData *RemoveIndex( int id, char color , char *vechile ) ;
	// 清理数据
	void Clear( void ) ;
	// 检测是需要通知
	bool CheckEx( int id ) ;

private:
	// 数据处理句柄
	IOHandler  *  _handler ;
	// 数据映射
	CMapArcoss    _mapArcoss;
	// 数据索引
	CMapIndex     _mapIndex ;
	// 同步处理锁操作
	share::Mutex  _mutex ;
	// 基础的根目录
	string 		  _basedir ;
};

#define MAX_CHECK_TIME   	30    // 最长激活时间
#define USER_ERR_NOMARL      0    // 正常状态
#define USER_ERR_EMPTY		-1    // 是否为空
#define USER_ERR_OVERFLUX   -2    // 超时流速状态

// 文件CACHE设计
class CFileCacheEx : public share::Runnable
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
			int	      id ; 	   // 当前数据别名ID
			time_t    now ;    // 最后一次操作时间
			CFluxCtrl flux ;   // 每个数据库一个流速控制
			int 	  state ;
		};
		typedef list<_stUid>  ListUid ;
	public:
		CListUser() {};
		~CListUser(){};

		// 添加用户
		void AddUser( const int id, unsigned int speed = 1000 )
		{
			set< int >::iterator it = _setid.find( id );
			if ( it != _setid.end() )
				return;

			_stUid uid;
			uid.id 	= id;
			uid.now = time( NULL );
			uid.flux.SetSpeed( speed );
			uid.state  = USER_ERR_NOMARL;
			_list.push_back( uid );

			_setid.insert( set< int >::value_type( id ) );
		}

		// 删除用户
		void DelUser( const int id )
		{
			_setid.erase( id );

			if ( _list.empty() )
				return;

			list< _stUid >::iterator it;
			for ( it = _list.begin(); it != _list.end() ; ++ it ) {
				_stUid &uid = ( * it );
				if ( uid.id != id ) {
					continue;
				}
				_list.erase( it );
				break;
			}
		}

		// 删除SET的索引
		void DelSet( const int id ){ _setid.erase( id ) ; }
		// 返回当前用户列表
		ListUid  &ListUser( void ){ return _list ;}

	private:
		// 在线用户队列
		ListUid 	 _list ;
		// 用户ID的索引
		set<int>  	 _setid ;
	};

public:
	CFileCacheEx(IOHandler *handler) ;
	~CFileCacheEx() ;

	// 初始化系统
	bool Init( ISystemEnv *pEnv , const char *name ) ;
	// 启动系统
	bool Start( void ) ;
	// 停止系统
	void Stop( void ) ;
	// 保存数据
	bool WriteCache( int id, void *buf, int len ) ;
	// 添加跨域信息
	void AddArcoss( int id, char color, char *vechile ) ;
	// 删除跨域信息
	void DelArcoss( int id, char color, char *vechile ) ;
	// 修改跨域时间
	void ChgArcoss( int id, char color, char *vechile ) ;
	// 在线用户
	void Online( int id ) ;
	// 离线用户
	void Offline( int id ) ;

public:
	// 线程运行对象
	virtual void run( void *param ) ;

private:
	// 环境对象指针
	ISystemEnv 			*_pEnv ;
	// 处理线程对象
	share::ThreadManager _thread;
	// 在线用户队列
	CListUser			 _userlst;
	// 处理数据的IO对象
	IOHandler			*_handler ;
	// 是否启动线程
	bool 				 _inited ;
	// 数据缓存对象
	CDataCache			 _datacache ;
	// 跨域缓存对象
	CArcossCache		 _arcosscache ;
	// 发送数据速度
	int					 _sendspeed;
	// 用户处理同步锁操作
	share::Mutex	     _mutex ;
};

#endif /* FILECACHE_H_ */
