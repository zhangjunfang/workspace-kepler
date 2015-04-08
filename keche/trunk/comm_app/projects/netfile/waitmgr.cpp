/*
 * waitobjmgr.cpp
 *
 *  Created on: 2012-9-18
 *      Author: humingqing
 */

#include "waitmgr.h"

CWaitObjMgr::CWaitObjMgr()
{
}

CWaitObjMgr::~CWaitObjMgr()
{
	Clear() ;
}

// 开辟序号对象
_waitobj * CWaitObjMgr::AllocObj( unsigned int seq , DataBuffer *out )
{
	_waitobj *obj = new _waitobj ;
	obj->_result  = 2 ;  // 这里默认为超时
	obj->_outbuf  = out ;

	_mutex.lock() ;
	_mapobj.insert( std::make_pair(seq, obj ) ) ;
	_mutex.unlock() ;

	return obj ;
}

// 更新对象数据
void CWaitObjMgr::ChangeObj( unsigned int seq, unsigned char result, void *data, int len )
{
	_mutex.lock() ;

	CMapObj::iterator it = _mapobj.find( seq ) ;
	if ( it == _mapobj.end() ) {
		_mutex.unlock() ;
		return ;
	}
	it->second->_result = result ;
	if ( data != NULL && len > 0 ) {
		it->second->_outbuf->writeBlock( data, len ) ;
	}
	it->second->_monitor.notify() ;

	_mutex.unlock() ;
}

// 移除对象
void CWaitObjMgr::RemoveObj( unsigned int seq )
{
	_mutex.lock() ;
	CMapObj::iterator it = _mapobj.find( seq ) ;
	if ( it == _mapobj.end() ) {
		_mutex.unlock() ;
		return ;
	}
	_waitobj *obj = it->second ;
	_mapobj.erase( it ) ;
	delete obj ;
	_mutex.unlock() ;
}

// 通知所有对象处理
void CWaitObjMgr::NotfiyAll( void )
{
	_mutex.lock() ;

	if( _mapobj.empty() ){
		_mutex.unlock() ;
		return ;
	}

	CMapObj::iterator it ;
	for ( it = _mapobj.begin(); it != _mapobj.end(); ++ it ){
		it->second->_monitor.notify() ;
	}

	_mutex.unlock() ;
}

// 清理所有对象
void CWaitObjMgr::Clear( void )
{
	_mutex.lock() ;

	if( _mapobj.empty() ){
		_mutex.unlock() ;
		return ;
	}

	CMapObj::iterator it ;
	for ( it = _mapobj.begin(); it != _mapobj.end(); ++ it ){
		delete it->second ;
	}
	_mapobj.clear() ;

	_mutex.unlock() ;
}
