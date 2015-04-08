/*
 * usermgr.cpp
 *
 *  Created on: 2012-5-17
 *      Author: humingqing
 *
 *  处理用户会话
 */
#include "usermgr.h"
#include <comlog.h>

CUserMgr::CUserMgr(IPairNotify *notify)
	:_notify(notify)
{
}

CUserMgr::~CUserMgr()
{
	Clear() ;
}

// 添加用户
bool CUserMgr::AddUser( socket_t *fd )
{
	share::Guard guard( _mutex ) ;

	_PairUser *p = new _PairUser;
	p->tcp._login = time(NULL) ;
	p->tcp._fd    = fd ;
	p->_next = p->_pre = NULL ;
	p->tcp._active = time(NULL) ;

	if ( ! AddMapFd( fd, p ) ) {
		return false ;
	}
	AddList( p ) ;
	// 添加到TCP的索引
	return true ;
}

// 注册
bool CUserMgr::OnRegister( socket_t *fd, const char *ckey, const char *ip, int port , socket_t *usock, std::string &key )
{
	share::Guard guard( _mutex ) ;

	_PairUser *p = GetMapFd( fd ) ;
	if ( p == NULL || ckey == NULL )
		return false ;
	if ( strcmp( p->tcp._key.c_str(), ckey ) != 0 )
		return false ;

	key = GenKey() ;
	p->udp._fd  = usock ;
	p->udp._key = key ;
	p->udp._login = time(NULL) ;
	p->tcp._active = time(NULL) ;

	return true ;
}

// 鉴权
bool CUserMgr::OnAuth( socket_t *fd, const char *akey, const char *code , std::string &key )
{
	share::Guard guard( _mutex ) ;

	_PairUser *p = GetMapFd( fd ) ;
	if ( p == NULL || akey == NULL || code == NULL )
		return false ;
	if ( p->tcp._key.empty() )
		return false ;

	if ( strcmp( p->tcp._key.c_str(), akey ) != 0 )
		return false ;
	p->tcp._code = code ;
	p->udp._code = code ;
	key = GenKey() ;
	p->tcp._key  = key ;
	p->tcp._active = time(NULL) ;

	// 添加到用户鉴权处理
	return AddMapCode( code, p ) ;
}

// 心跳处理
bool CUserMgr::OnLoop( socket_t *fd, const char *akey )
{
	share::Guard guard( _mutex ) ;

	_PairUser *p = GetMapFd( fd ) ;
	if ( p == NULL || akey == NULL )
		return false ;

	const char *key = p->tcp._key.c_str() ;
	if ( strcmp( akey , key ) != 0 )
		return false ;

	p->tcp._active = time(NULL) ;

	return true ;
}

// 检测是否超时
void CUserMgr::Check( int timeout )
{
	time_t now = time(NULL) ;

	int    count = 0 ;
	string suser ;

	_mutex.lock() ;
	_PairUser *tmp, *p = _queue.begin() ;
	while( p != NULL ) {
		tmp = p ;
		p = _queue.next( p ) ;

		// 处理超时的连接
		if ( now - tmp->tcp._active > timeout ){
			// 删除数据
			DelList( tmp , true ) ;
		} else {
			if ( ! suser.empty() ) {
				suser += "," ;
			}
			suser += tmp->tcp._code ;
			++ count ;
		}
	}
	_mutex.unlock() ;

	OUT_WARNING( NULL, 0, "ONLINE", "online user count %d, users: %s" , count,  suser.c_str() ) ;
}

// 根据连接取得用户
_PairUser * CUserMgr::GetUser( socket_t *fd )
{
	share::Guard guard( _mutex ) ;

	_PairUser *p = GetMapFd( fd ) ;
	if ( p == NULL )
		return NULL;

	return p ;
}

// 根据接入来取得用户
_PairUser * CUserMgr::GetUser( const char *key )
{
	share::Guard guard( _mutex ) ;

	CMapUser::iterator it = _kuser.find( key ) ;
	if ( it == _kuser.end() )
		return NULL ;

	return (it->second) ;
}

//===========================================================================
// 添加队列中
bool CUserMgr::AddMapFd( socket_t * fd, _PairUser *p )
{
	CMapFds::iterator it  = _tcps.find( fd ) ;
	if ( it != _tcps.end() ) {
		DelList( it->second , false ) ;
	}
	_tcps.insert( std::make_pair(fd, p ) ) ;
	return true ;
}

// 取得数据
_PairUser * CUserMgr::GetMapFd( socket_t *fd )
{
	CMapFds::iterator it  = _tcps.find( fd ) ;
	if ( it == _tcps.end() )
		return NULL ;

	return it->second ;
}

// 添加接入码索引
bool CUserMgr::AddMapCode( const char *key, _PairUser *p )
{
	CMapUser::iterator it = _kuser.find( key ) ;
	if ( it != _kuser.end() ){
		it->second = p ;
	}
	_kuser.insert( std::make_pair(key,p) ) ;
	return true ;
}

// 产生KEY来处理
const std::string CUserMgr::GenKey( void )
{
	time_t now = time(NULL) ;
	char buf[128] = {0};
	sprintf( buf , "%llu", now ) ;
	return buf ;
}

// 添加新的用户
void CUserMgr::AddList( _PairUser *p )
{
	_queue.push( p ) ;

	p->tcp._key = GenKey() ;

	_notify->NotifyUser( p->tcp._fd , p->tcp._key.c_str() )  ;
}

// 删除用户数据
void CUserMgr::DelList( _PairUser *p , bool notify )
{
	p = _queue.erase( p ) ;

	// 移除索引
	RemoveIndex( p ) ;

	if ( notify )
		_notify->CloseUser( p->tcp._fd ) ;

	delete p ;
}

// 移除索引
void CUserMgr::RemoveIndex( _PairUser *p )
{
	// 如果接入码不空就直接移除
	if ( ! p->tcp._code.empty() ) {
		CMapUser::iterator it = _kuser.find( p->tcp._code ) ;
		if ( it != _kuser.end() ){
			_kuser.erase( it ) ;
		}
	}

	CMapFds::iterator itx ;
	// 移除TCP的索引
	itx = _tcps.find( p->tcp._fd ) ;
	if ( itx != _tcps.end() ) {
		_tcps.erase( itx ) ;
	}
}

// 清理所有数据
void CUserMgr::Clear( void )
{
	share::Guard guard( _mutex ) ;

	_kuser.clear() ;
	_tcps.clear() ;
}




