/*
 * filecache.cpp
 *
 *  Created on: 2011-12-21
 *      Author: humingqing
 */

#include "filecacheex.h"
#include <tools.h>
#include <assert.h>
#include <time.h>
#include <filequeue.h>

#define FILECACHE_FAILED   		-1  // 失败情况
#define FILECACHE_SUCCESS   	0	// 成功
#define FILECACHE_EMPTY  		1	// 为空的情况

CFileCacheEx::CFileCacheEx(IOHandler *handler):
	_inited(false), _datacache(handler), _arcosscache(handler)
{
	_handler = handler ;
}

CFileCacheEx::~CFileCacheEx()
{
	Stop() ;
}

// 初始化系统
bool CFileCacheEx::Init( ISystemEnv *pEnv )
{
	_pEnv = pEnv ;

	char temp[256] = {0};
	if ( ! pEnv->GetString( "base_filedir" , temp ) ) {
		printf( "load base_filedir failed\n" ) ;
		return false ;
	}

	char sz[512] = {0} ;
	getenvpath( temp, sz );

	char buf[1024] = {0};
	sprintf( buf, "%s/cache", sz ) ;
	// 初始化环境
	_datacache.Init( buf ) ;

	// 初始化跨域时缓存
	sprintf( buf, "%s/arcoss", sz ) ;
	_arcosscache.Init( buf ) ;

	_thread.init( 1, NULL, this ) ;

	_inited = true ;

	return true ;
}

// 启动系统
bool CFileCacheEx::Start( void )
{
	_thread.start() ;
	return true ;
}

// 停止系统
void CFileCacheEx::Stop( void )
{
	if ( ! _inited )
		return ;
	_inited = false ;
	_thread.stop() ;
}

// 保存数据
bool CFileCacheEx::WriteCache( int areacode, void *buf, int len )
{
	char szid[128] = {0} ;
	sprintf( szid, "%u", areacode ) ;
	return _datacache.Push( szid, buf, len ) ;
}

// 添加跨域信息
void CFileCacheEx::AddArcoss( int areacode, char color, char *vechile )
{
	_arcosscache.Push( areacode, color, vechile ) ;
}

// 删除跨域信息
void CFileCacheEx::DelArcoss( int areacode, char color, char *vechile )
{
	_arcosscache.Pop( areacode, color, vechile ) ;
}

// 修改跨域时间
void CFileCacheEx::ChgArcoss( int areacode, char color, char *vechile )
{
	_arcosscache.Change( areacode, color, vechile ) ;
}

// 在线用户
void CFileCacheEx::Online( int areacode )
{
	char szid[128] = {0} ;
	sprintf( szid, "%u", areacode ) ;

	_userlst.AddUser( areacode ) ;
	// 加载数据
	_datacache.Load( szid ) ;
	// 加载数据
	_arcosscache.Load( areacode ) ;
}

// 离线用户
void CFileCacheEx::Offline( int areacode )
{
	_userlst.DelUser( areacode ) ;
	// 处理断连时跨域
	_arcosscache.Offline( areacode ) ;
}

// 线程运行对象
void CFileCacheEx::run( void *param )
{
	while( _inited ){

		int uid = 0 ;
		if ( ! _userlst.PopUser(uid) ) {
			sleep(10) ;
			continue ;
		}

		char szid[128] = {0} ;
		sprintf( szid, "%u", uid ) ;
		// 弱出数据处理
		int ret = _datacache.Pop( szid ) ;
		// 处理数据
		if ( ret == FILECACHE_EMPTY ){
			sleep(1) ;  // 如果没有数据休息一会
		} else if ( ret == FILECACHE_FAILED ) {  // 发送数据失败用户不在线
			_userlst.DelUser( uid ) ;
			continue ;
		}
		// 处理补发跨域数据
		_arcosscache.Check( uid ) ;
	}
}

//----------------------------处理跨域数据补发-----------------------------------
CArcossCache::CArcossCache( IOHandler *handler ):_handler(handler)
{

}

CArcossCache::~CArcossCache()
{
	Clear() ;
}

// 保存文件路径
void CArcossCache::Init( const char *szroot )
{
	_basedir = szroot ;
}

// 上线加载数据
void CArcossCache::Load( int id )
{
	// 序列化
	unserialize( id ) ;
	serialize( id ) ;
}

// 添加新数据
void CArcossCache::Push( int id, char color, char *vechile )
{
	if ( AddNew(id, color, vechile) ) {
		// 序列化数据
		serialize( id ) ;
	}
}

// 添加新数据
bool CArcossCache::AddNew( int id, char color, char *vechile )
{
	share::Guard guard( _mutex ) ;

	return AddData(id, color, vechile, (uint64_t)time(NULL) , VECHILE_ON ) ;
}

// 结束跨域处理
void CArcossCache::Pop( int id, char color, char *vechile )
{
	if ( Remove(id, color, vechile) ) {
		// 序列化数据
		serialize( id ) ;
	}
}

// 移除数据
bool CArcossCache::Remove( int id, char color, char *vechile )
{
	share::Guard gurad( _mutex ) ;

	ArcossData *p = RemoveIndex( id, color, vechile ) ;
	if ( p == NULL ) {
		return false ;
	}

	CMapArcoss::iterator it = _mapArcoss.find( id ) ;
	if ( it == _mapArcoss.end() ) {
		return false ;
	}

	ArcossHeader *header = it->second ;
	if ( header->_tail == p && header->_head == p ) { // 只有一个元素
		_mapArcoss.erase( it ) ;
		deletefile( id ) ;
		delete header ;
	} else { // 有多个元素的情况
		if ( p == header->_head ){ // 位于头部数据
			header->_head = p->_next ;
			if ( p->_next != NULL ) {
				p->_next->_pre = NULL ;
			}
		} else if ( p == header->_tail ){ // 位于尾部的情况
			header->_tail = p->_pre ;
			if ( p->_pre != NULL ) {
				p->_pre->_next = NULL ;
			}
		} else { // 位于中间的情况
			p->_pre->_next = p->_next ;
			p->_next->_pre = p->_pre ;
		}
		header->_size = header->_size - 1 ;
	}
	delete p ;

	return true ;
}

// 修改数据时间
void CArcossCache::Change( int id, char color, char *vechile )
{
	// 如果修改成功需要序列化
	if ( ChangeEx( id, color, vechile ) ) {
		serialize( id ) ;
	}
}

// 修改数据
bool CArcossCache::ChangeEx( int id, char color, char *vechile )
{
	share::Guard guard( _mutex ) ;

	char carnum[VECHILE_LEN+1] = {0};
	safe_memncpy( carnum, vechile, VECHILE_LEN ) ;

	char key[512] = {0};
	sprintf( key, "%d_%d_%s", id, color, carnum ) ;

	CMapIndex::iterator itx = _mapIndex.find( key ) ;
	if ( itx == _mapIndex.end() ) {
		return false ;
	}

	ArcossData *p = itx->second ;
	p->time = time(NULL) ;

	return true ;
}

// 断连接情况
void CArcossCache::Offline( int id )
{
	share::Guard guard( _mutex ) ;
	if ( _mapArcoss.empty() )
		return ;

	CMapArcoss::iterator it = _mapArcoss.find( id ) ;
	if ( it == _mapArcoss.end() ) {
		return ;
	}

	ArcossHeader *header = it->second ;
	if ( header->_size > 0 ) {
		ArcossData *p = header->_head ;
		while( p != NULL ) {
			p->state = VECHILE_OFF ;
			p = p->_next ;
		}
	}
}

// 检测数据
bool CArcossCache::Check( int id )
{
	if ( ! CheckEx( id ) ) {
		return false;
	}
	serialize( id ) ;
	return true ;
}

bool CArcossCache::CheckEx( int id )
{
	share::Guard guard( _mutex ) ;
	if ( _mapArcoss.empty() )
		return false;

	CMapArcoss::iterator it = _mapArcoss.find( id ) ;
	if ( it == _mapArcoss.end() )
		return false;

	ArcossHeader *header = it->second ;
	if ( header->_size == 0 ) {
		_mapArcoss.erase( it ) ;
		delete header ;
		deletefile( id ) ;
		return false ;
	}

	char szid[128] = {0} ;
	sprintf( szid, "%u", id ) ;

	int count = 0 ;

	ArcossData *p = header->_head ;
	while( p != NULL ) {
		if ( p->state != VECHILE_OFF ) {
			p = p->_next ;
			continue ;
		}
		ArcossData *temp = p ;
		p = p->_next ;

		// 向外发送数据处理结果,这里回调外界不会引起死锁
		if ( _handler->HandleQueue( szid , temp, sizeof(ArcossData) , DATA_ARCOSSDAT ) == IOHANDLE_FAILED ){
			// 如果发送数据失败就直接返回了
			break ;
		}

		// 移除数据
		RemoveIndex( id, temp->color, temp->vechile ) ;

		// 只有一个元素
		if ( temp == header->_head && temp == header->_tail ){
			_mapArcoss.erase( it ) ;
			deletefile( id ) ;
			delete header ;
			delete temp ;
			break ;
		}

		// 有多个元素的情况
		if ( temp == header->_head ){ // 位于头部数据
			header->_head = temp->_next ;
			if ( temp->_next != NULL ) {
				temp->_next->_pre = NULL ;
			}
		} else if ( temp == header->_tail ){ // 位于尾部的情况
			header->_tail = temp->_pre ;
			if ( temp->_pre != NULL ) {
				temp->_pre->_next = NULL ;
			}
		} else { // 位于中间的情况
			temp->_pre->_next = temp->_next ;
			temp->_next->_pre = temp->_pre ;
		}
		header->_size = header->_size - 1 ;
		delete temp ;

		++ count ;
	}
	return ( count > 0 ) ;
}

// 移除索引
ArcossData *CArcossCache::RemoveIndex( int id, char color , char *vechile )
{
	char carnum[VECHILE_LEN+1] = {0};
	safe_memncpy( carnum, vechile, VECHILE_LEN ) ;

	char key[512] = {0};
	sprintf( key, "%d_%d_%s", id, color, carnum ) ;

	CMapIndex::iterator itx = _mapIndex.find( key ) ;
	if ( itx == _mapIndex.end() ) {
		return NULL ;
	}
	ArcossData *p = itx->second ;
	_mapIndex.erase( itx ) ;

	return p ;
}

// 删除文件
void CArcossCache::deletefile( int id )
{
	char key[128] = {0};
	sprintf( key, "%d", id ) ;

	CFileQueue file( (char*)_basedir.c_str(), key ) ;
	file.remove() ;
}

// 序列化数据
void CArcossCache::serialize( int id )
{
	share::Guard guard( _mutex ) ;

	if ( _mapArcoss.empty() ) return ;

	CMapArcoss::iterator it = _mapArcoss.find( id ) ;
	if ( it == _mapArcoss.end() ) {
		return ;
	}

	char key[128] = {0};
	sprintf( key, "%d", id ) ;

	CFileQueue file( (char*)_basedir.c_str(), key ) ;
	file.remove() ;

	ArcossHeader *header = it->second ;
	if ( header->_size > 0 ) {
		ArcossData *p = header->_head ;
		while( p != NULL ) {
			file.push( p, sizeof(ArcossData) ) ;
			p = p->_next ;
		}
	}
}

// 反序列化数据
void CArcossCache::unserialize( int id )
{
	share::Guard guard( _mutex ) ;

	char key[128] = {0};
	sprintf( key, "%d", id ) ;

	CFileQueue file( (char*)_basedir.c_str(), key ) ;

	queue_item *item = file.pop() ;
	while( item != NULL ) {
		ArcossData *p = ( ArcossData *) item->data ;
		// 添加到链表中
		AddData( id, p->color, p->vechile, p->time , VECHILE_OFF ) ;
		free( item ) ;
		item = file.pop() ;
	}
}

// 添加新数据
bool CArcossCache::AddData( int id, char color, char *vechile , uint64_t time , char state )
{
	char carnum[VECHILE_LEN+1] = {0};
	safe_memncpy( carnum, vechile, VECHILE_LEN ) ;

	char key[512] = {0};
	sprintf( key, "%d_%d_%s", id, color, carnum ) ;

	CMapIndex::iterator itx = _mapIndex.find( key ) ;
	if ( itx != _mapIndex.end() ) {
		return false;
	}

	ArcossData *p = new ArcossData ;
	p->areacode = id ;
	p->color    = color ;
	safe_memncpy( p->vechile, vechile, VECHILE_LEN ) ;
	p->time     = time ;
	p->state    = state ;
	p->_next 	= NULL ;
	p->_pre  	= NULL ;

	CMapArcoss::iterator it = _mapArcoss.find( id ) ;
	if ( it == _mapArcoss.end() ) {
		ArcossHeader *header = new ArcossHeader ;
		header->_size = 1 ;
		header->_head = header->_tail = p ;
		_mapArcoss.insert( make_pair( id, header ) ) ;
	} else {
		ArcossHeader *header = it->second ;
		header->_tail->_next = p ;
		p->_pre				 = header->_tail ;
		header->_tail		 = p ;
		header->_size        = header->_size + 1 ;
	}
	_mapIndex.insert( make_pair(key, p) ) ;

	return true ;
}

// 清理数据
void CArcossCache::Clear( void )
{
	share::Guard guard( _mutex ) ;
	if ( _mapArcoss.empty() )
		return ;

	CMapArcoss::iterator it ;
	for ( it = _mapArcoss.begin(); it != _mapArcoss.end(); ++ it ) {
		ArcossHeader *header = it->second ;
		if ( header->_size > 0 ) {
			ArcossData *p = header->_head ;
			while( p->_next != NULL ) {
				p = p->_next ;
				delete p->_pre ;
			}
			delete p ;
		}
		delete header ;
	}
	_mapArcoss.clear() ;
	_mapIndex.clear() ;
}


