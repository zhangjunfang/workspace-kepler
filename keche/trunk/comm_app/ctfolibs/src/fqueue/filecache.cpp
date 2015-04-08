/*
 * filecache.cpp
 *
 *  Created on: 2012-4-25
 *      Author: humingqing
 */

#include "filecache.h"
#include <tools.h>
#include <assert.h>
#include <time.h>
#include <filequeue.h>

#define FILECACHE_FAILED   		-1  // 失败情况
#define FILECACHE_SUCCESS   	0	// 成功
#define FILECACHE_EMPTY  		1	// 为空的情况

CFileCache::CFileCache(IOHandler *handler)
	: _datacache(handler) , _maxsend(0)
{
	_handler = handler ;
}

CFileCache::~CFileCache()
{
}

// 初始化系统
bool CFileCache::Init( const char *dir, int maxsend )
{
	// 最大发送速度限制
	_maxsend = maxsend ;
	// 初始化环境
	_datacache.Init( dir ) ;

	return true ;
}

// 保存数据
bool CFileCache::WriteCache( const char *szid , void *buf, int len )
{
	return _datacache.Push( szid , buf, len ) ;
}

// 在线用户
void CFileCache::Online( const char *szid )
{
	_mutex.lock() ;
	_userlst.AddUser( szid , _maxsend ) ;
	_mutex.unlock() ;

	// 加载数据
	_datacache.Load( szid ) ;
}

// 离线用户
void CFileCache::Offline( const char *szid )
{
	_mutex.lock() ;
	_userlst.DelUser( szid ) ;
	_mutex.unlock() ;
}

// 线程运行对象
bool CFileCache::Check( void )
{
	_mutex.lock() ;
	CListUser::ListUid &lst = _userlst.ListUser() ;
	if ( lst.empty() ) {
		_mutex.unlock() ;
		return false ;
	}

	time_t now = time(NULL) ;
	// 是否成功
	bool success = false ;
	CListUser::ListUid::iterator it ;
	// 需要遍歷所有當前可用的用戶
	for ( it = lst.begin(); it != lst.end(); ) {
		CListUser::_stUid &uid = ( *it ) ;
		// 如果超时出流速
		if ( uid.state == USER_ERR_OVERFLUX && uid.now == now ) {
			++ it ;
			continue ;
		}

		// 状态是否没有取出数据
		if ( uid.state == USER_ERR_EMPTY && now - uid.now < MAX_CHECK_TIME ) {
			++ it ;
			continue ;
		}

		// 将状态切回正常
		uid.state = USER_ERR_NOMARL ;
		// 记录当前时间
		uid.now   = now ;
		// 如果超出流速就暂时停一会儿再继续发
		if ( uid.flux.OverSpeed() ) {
			uid.state = USER_ERR_OVERFLUX ;
			++ it ;
			continue ;
		}

		// 弱出数据处理
		int ret = _datacache.Pop( uid.userid ) ;
		// 处理数据
		if ( ret == FILECACHE_EMPTY ){
			uid.state = USER_ERR_EMPTY ;
			++ it ;
			continue ;  // 如果没有数据休息一会
		} else if ( ret == FILECACHE_FAILED ) {  // 发送数据失败用户不在线
			// 删除索引
			_userlst.DelSet( uid.userid ) ;
			lst.erase( it++ ) ;
			continue ;
		}
		success = true ;
		++ it ;
	}
	_mutex.unlock() ;

	return success ;
}

//-------------------------CDataCache 数据缓存对象--------------------------------

CDataCache::CDataCache( IOHandler *handler )
{
	_handler = handler ;
}

CDataCache::~CDataCache()
{
	Clear() ;
}

void CDataCache::Init( const char *szroot )
{
	_basedir = szroot ;
}

// 加载用户数据
void CDataCache::Load( const string &sid )
{
	// 添加已存在用户队列中
	_mutex.lock() ;
	AddNewQueue( sid ) ;
	_mutex.unlock() ;
}

// 添加新数据
bool CDataCache::Push( const string &sid, void *buf, int len )
{
	int ret = 0 ;

	_mutex.lock() ;
	CFileQueue *fqueue = AddNewQueue( sid ) ;
	assert( fqueue != NULL ) ;
	ret = fqueue->push( buf, len ) ;
	_mutex.unlock() ;

	return ( ret == 0 ) ;
}

// 弹出数据
int CDataCache::Pop( const string &sid )
{
	queue_item *item = NULL ;
	{
		share::Guard guard( _mutex ) ;
		{
			if ( _queue.empty() )
				return FILECACHE_EMPTY ;

			CMapQueue::iterator it ;
			it = _queue.find( sid ) ;
			if ( it == _queue.end() ) {
				return true ;
			}

			CFileQueue *fqueue = it->second ;
			item = fqueue->pop() ;
			if ( item == NULL ) {
				// 如果文件没有数据就可以删除了
				fqueue->remove() ;
				_queue.erase( it ) ;
				delete fqueue ;
			}
		}
	}
	// 移到锁外面来处理数据，因为外界回调发送失败会引起死锁
	if ( item != NULL ) {
		// 向外递交数据
		if ( _handler->HandleQueue( sid.c_str() , &item->data[0] , item->len ) == IOHANDLE_FAILED ) {
			// 重新放入队列中
			Push( sid, &item->data[0] , item->len ) ;
			free( item ) ;
			return FILECACHE_FAILED ;
		}
		free( item ) ;
	}
	return FILECACHE_SUCCESS ;
}

// 添加新的数据对象
CFileQueue * CDataCache::AddNewQueue( const string &sid )
{
	CMapQueue::iterator it = _queue.find( sid ) ;
	if ( it != _queue.end() ) {
		return it->second ;
	}

	CFileQueue *fqueue = new CFileQueue( (char*)_basedir.c_str(), (char*)sid.c_str() ) ;
	assert( fqueue != NULL ) ;
	_queue.insert( make_pair(sid, fqueue) ) ;
	return fqueue ;
}

// 清理所有数据
void CDataCache::Clear( void )
{
	share::Guard guard( _mutex ) ;
	if ( _queue.empty() )
		return ;

	CMapQueue::iterator it ;
	for ( it = _queue.begin(); it != _queue.end(); ++ it ) {
		delete it->second ;
	}
	_queue.clear() ;
}
