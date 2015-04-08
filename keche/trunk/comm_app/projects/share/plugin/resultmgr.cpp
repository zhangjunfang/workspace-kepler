/*
 * resultmgr.cpp
 *
 *  Created on: 2012-6-6
 *      Author: humingqing
 *  数据结果集对象
 */

#include "resultmgr.h"

CResultMgr::CResultMgr()
{

}

CResultMgr::~CResultMgr()
{
	ClearResult(0) ;
}

// 添加结果
bool CResultMgr::AddResult( unsigned int msgid, share::Ref *obj )
{
	share::Guard guard( _mutex );

	CMapResult::iterator it = _index.find(msgid);

	if ( it != _index.end() ) {
		obj->AddRef() ;
		it->second->_vec.push_back( (share::Ref*) obj ) ;
		return true ;
	}

	_Result *p = new _Result ;
	p->_id  = msgid ;
	p->_now = time(NULL) ;

	obj->AddRef() ;
	p->_vec.push_back( (share::Ref*) obj ) ;

	_queue.push( p ) ;
	_index.insert( CMapResult::value_type( msgid, p ) ) ;

	return true ;
}

// 清理数据
void CResultMgr::ClearResult( unsigned int msgid )
{
	share::Guard guard( _mutex ) ;
	if ( _queue.size() == 0 )
		return ;

	if ( msgid ==  0 ) {
		_Result *t = NULL ;
		_Result *p = _queue.begin() ;
		while( p != NULL ) {
			t = p ;
			p = p->_next ;
			Clear( t ) ;
		}
		_index.clear() ;
	} else {
		CMapResult::iterator it = _index.find( msgid ) ;
		if ( it != _index.end() ) {
			Clear( it->second ) ;
			_index.erase( it ) ;
		}
	}
}

// 检测是否超时
void CResultMgr::Check( int timeout )
{
	time_t now = time(NULL) ;
	if ( now - _lasttime < 180 ) {
		return ;
	}
	_lasttime = now ;

	share::Guard guard( _mutex ) ;

	_Result *t = NULL ;
	_Result *p = _queue.begin() ;
	while( p != NULL ) {
		t = p ;
		p = p->_next ;

		if ( now - t->_now < timeout ){
			break ;
		}
		RemoveValue( t ) ;
	}
}

// 移除值
void CResultMgr::RemoveValue( _Result *p )
{
	CMapResult::iterator it = _index.find( p->_id ) ;
	if ( it == _index.end() )
		return ;
	_index.erase( it ) ;
	Clear( p ) ;
}

// 清理对应的数据
void CResultMgr::Clear( _Result *p )
{
	if ( p->_vec.empty() )
		return ;

	for ( int i = 0; i < (int)p->_vec.size(); ++ i ) {
		p->_vec[i]->Release() ;
	}

	p->_vec.clear();
	delete  _queue.erase(p) ;
}


