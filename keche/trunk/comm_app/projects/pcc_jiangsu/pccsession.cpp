/*
 * pccsession.cpp
 *
 *  Created on: 2011-03-02
 *      Author: humingqing
 */

#include "pccsession.h"
#include <tools.h>

CPccSession::CPccSession()
{
}

CPccSession::~CPccSession()
{
}

// 加载数据
bool CPccSession::Load( const char *file )
{
	// area_code:ome_phone:color_num
	int   len = 0 ;
	char *ptr = ReadFile( file, len ) ;
	if ( ptr == NULL || len == 0 ) {
		return false ;
	}

	for( int i = 0; i < len; ++ i ){
		if ( ptr[i] != '\r' )
			continue ;
		ptr[i] = '\n' ;
	}

	vector<string> vec ;
	if ( ! splitvector( ptr , vec, "\n", 0 ) ) {
		FreeBuffer( ptr ) ;
		return false ;
	}

	// 处理用户数据,  4C54_15001088478:WZ:1:A1:18:苏E-Y8888
	for ( int i = 0; i < (int)vec.size(); ++ i ) {
		string &temp = vec[i] ;
		if ( temp.empty() )
			continue ;

		vector<string> vk ;
		if ( ! splitvector( temp, vk, ":" , 6 ) ){
			continue ;
		}

		_stCarInfo info ;
		info.macid       = vk[0] ;
		info.areacode    = vk[1] ;
		info.color       = vk[2] ;
		info.carmodel    = vk[3] ;
		info.vehicletype = vk[4] ;
		info.vehiclenum  = vk[5] ;

		_mgr.AddCarInfo( info ) ;
	}

	FreeBuffer( ptr ) ;
	return true ;
}

// 取得所有MAC数据
bool CPccSession::GetCarMacData( string &s )
{
	return _mgr.GetAllMacId( s ) ;
}

// 根据手机MAC取得车牌
bool CPccSession::GetCarInfo( const char *key, _stCarInfo &info )
{
	if ( !_mgr.GetCarInfo( key, info, true ) ) {
		// 如果会话中存在则直接处理
		return false ;
	}
	return true ;
}

// 根据车牌取得对应的手机MAC
bool CPccSession::GetCarMacId( const char *key, char *macid )
{
	_stCarInfo info ;
	if ( !_mgr.GetCarInfo( key, info, false ) ) {
		return false ;
	}
	// 取得数据
	sprintf( macid, "%s", info.macid.c_str() ) ;

	return true ;
}

CPccSession::CCarInfoMgr::CCarInfoMgr()
{
	_head = _tail = NULL ;
	_size = 0 ;
}

CPccSession::CCarInfoMgr::~CCarInfoMgr()
{
	ClearAll() ;
}

// 添加车辆信息
bool CPccSession::CCarInfoMgr::AddCarInfo( _stCarInfo &info )
{
	share::Guard guard( _mutex ) ;

	_stCarList *p = new _stCarList ;
	p->info = info ;
	p->next = p->pre = NULL ;

	if ( _head == NULL ) {
		_tail = _head = p ;
		_size = 1 ;
	} else {
		_tail->next = p ;
		p->pre      = _tail ;
		_tail       = p ;
		_size       = _size + 1 ;
	}

	_stCarList *temp = NULL ;

	CMapCarInfo::iterator it ;
	it = _phone2car.find( info.macid ) ;
	if ( it != _phone2car.end() ) {
		temp = it->second ;
		_phone2car.erase( it ) ;
	}

	it = _vehice2car.find( info.vehiclenum ) ;
	if ( it != _vehice2car.end() ) {
		_vehice2car.erase( it ) ;
	}
	if ( temp ) RemoveNode( temp ) ;

	_phone2car.insert( make_pair( info.macid, p ) ) ;
	_vehice2car.insert( make_pair(info.vehiclenum, p ) ) ;

	return true ;
}

// 取得车辆信息
bool CPccSession::CCarInfoMgr::GetCarInfo( const string &key, _stCarInfo &info , bool byphone )
{
	share::Guard guard( _mutex ) ;

	CMapCarInfo::iterator it ;
	if ( byphone ) {
		it = _phone2car.find( key ) ;
		if ( it == _phone2car.end() )
			return false ;
		info = (it->second)->info ;
	} else {
		it = _vehice2car.find( key ) ;
		if ( it == _vehice2car.end() )
			return false ;
		info = (it->second)->info ;
	}

	return true ;
}

// 移除车辆信息
void CPccSession::CCarInfoMgr::RemoveInfo( const string &key , bool byphone )
{
	share::Guard guard( _mutex ) ;

	_stCarList *temp = NULL ;
	CMapCarInfo::iterator it ;

	if ( byphone ) {
		it = _phone2car.find( key ) ;
		if ( it == _phone2car.end() ) {
			return ;
		}
		temp = it->second ;
		_phone2car.erase( it ) ;

		it = _vehice2car.find(  temp->info.vehiclenum ) ;
		if ( it != _vehice2car.end() ) {
			_vehice2car.erase( it ) ;
		}
	} else {
		it = _vehice2car.find( key ) ;
		if ( it == _vehice2car.end() ) {
			return ;
		}
		temp = it->second ;
		_vehice2car.erase( it ) ;

		it = _phone2car.find( temp->info.macid ) ;
		if ( it != _phone2car.end() ) {
			_phone2car.erase( it ) ;
		}
	}
	RemoveNode( temp ) ;
}

// 移除掉结点
void CPccSession::CCarInfoMgr::RemoveNode( _stCarList *p )
{
	if ( p == _head ) { // 如果为头节点
		_head = p->next   ;
		_head->pre = NULL ;
		if ( _head == NULL )
			_tail = NULL ;
	} else if ( _tail == p ){ // 如果为尾结点
		_tail = p->pre ;
		_tail->next = NULL ;
	} else { // 如果中间节点
		p->pre->next = p->next ;
		p->next->pre = p->pre  ;
	}
	_size = _size - 1 ;

	delete p ;
}

// 取得所有MACID
bool CPccSession::CCarInfoMgr::GetAllMacId( string &s )
{
	share::Guard guard( _mutex ) ;

	if ( _head == NULL )
		return false ;

	_stCarList *temp, *p = _head ;
	while( p != NULL ) {
		if ( ! s.empty() ){
			s += "," ;
		}
		s += p->info.macid ;
		p = p->next ;
	}
	return ( !s.empty() ) ;
}

// 清空所有数据
void CPccSession::CCarInfoMgr::ClearAll( void )
{
	share::Guard guard( _mutex ) ;

	if ( _head == NULL )
		return ;

	_stCarList *temp, *p = _head ;
	while( p != NULL ) {
		temp = p ;
		p = p->next ;
		delete temp ;
	}

	_head = _tail = NULL ;
	_size = 0 ;

	_phone2car.clear() ;
	_vehice2car.clear() ;
}



