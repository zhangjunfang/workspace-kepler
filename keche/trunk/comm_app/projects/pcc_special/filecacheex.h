/*
 * filecache.h
 *
 *  Created on: 2011-12-21
 *      Author: Administrator
 */

#ifndef __FILECACHEEX_H__
#define __FILECACHEEX_H__

#include "interface.h"
#include <Thread.h>
#include <filecache.h>

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

class IOHandler ;
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

#define MAX_CHECK_TIME   30  // 最长激活时间
// 文件CACHE设计
class CFileCacheEx : public share::Runnable
{
	// 在线用户队列
	class CListUser
	{
		struct _stUid
		{
			int 	areacode ;
			time_t  now ;
		};
	public:
		CListUser() {};
		~CListUser(){};

		// 添加用户
		void AddUser( int areacode ) {
			share::Guard guard( _mutex ) ;
			_stUid uid ;
			uid.areacode = areacode ;
			uid.now		 = time(NULL) ;
			_list.push_back( uid ) ;
		}
		// 删除用户
		void DelUser( int areacode ) {
			share::Guard guard( _mutex ) ;
			if ( _list.empty() )
				return ;

			list<_stUid>::iterator it ;
			for ( it = _list.begin(); it != _list.end(); ++ it ) {
				_stUid &uid = (*it) ;
				if ( uid.areacode != areacode ) {
					continue ;
				}
				_list.erase( it ) ;
				break ;
			}
		}
		// 处理用户
		bool PopUser( int &areacode ) {
			share::Guard guard( _mutex ) ;
			if ( _list.empty() )
				return false ;

			size_t cnt   = 0 ;
			size_t count = _list.size() ;
			time_t now   = time(NULL) ;

			areacode = -1 ;

			while( cnt < count ) {
				_stUid uid = _list.front() ;
				_list.pop_front() ;
				_list.push_back( uid ) ;

				if ( now - uid.now > MAX_CHECK_TIME ) {
					areacode = uid.areacode ;
					break ;
				}
				++ cnt ;
			}
			return ( areacode > 0 ) ;
		}

	private:
		// 用户队列锁
		share::Mutex _mutex;
		// 在线用户队列
		list<_stUid> _list ;
	};


public:
	CFileCacheEx(IOHandler *handler) ;
	~CFileCacheEx() ;

	// 初始化系统
	bool Init( ISystemEnv *pEnv ) ;
	// 启动系统
	bool Start( void ) ;
	// 停止系统
	void Stop( void ) ;
	// 保存数据
	bool WriteCache( int areacode, void *buf, int len ) ;
	// 添加跨域信息
	void AddArcoss( int areacode, char color, char *vechile ) ;
	// 删除跨域信息
	void DelArcoss( int areacode, char color, char *vechile ) ;
	// 修改跨域时间
	void ChgArcoss( int areacode, char color, char *vechile ) ;
	// 在线用户
	void Online( int areacode ) ;
	// 离线用户
	void Offline( int areacode ) ;

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
};

#endif /* FILECACHE_H_ */
