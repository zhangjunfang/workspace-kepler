/*
 * statinfo.cpp
 *
 *  Created on: 2012-7-27
 *      Author: humingqing
 *  Memo: 统计当前用户所有车辆的数据以及当前在线用户个数以及发送的数据流量
 */

#include "statinfo.h"
#include <tools.h>
#include <comlog.h>

// 添加双向链的节点宏
#define ADD_NODE( head, tail, p )   { p->_pre  = p->_next = NULL ; \
	if ( head == NULL ) {  head = tail = p ; } else { tail->_next = p ; p->_pre = tail  ; tail   = p ; } }

// 删除双向链的节点宏
#define DEL_NODE( head, tail, p )  { \
	if ( p == head ) { if ( head == tail ) { head = tail = NULL ; } else { head = p->_next ; head->_pre = NULL ; } \
	} else if ( p == tail ) { tail = p->_pre ; tail->_next = NULL ; } else { p->_pre->_next = p->_next ; p->_next->_pre = p->_pre  ;  } }


CStatInfo::CStatInfo( const char *name ) : _name(name)
{
	_head = _tail = NULL ;
	_size = 0 ;
	_lasttime = time(NULL) ;
	_ncar = 0 ;
}

CStatInfo::~CStatInfo()
{
	Clear() ;
}

// 添加客户端
CStatInfo::_ClientInfo *CStatInfo::AddClient( unsigned int id )
{
	_ClientInfo *p = NULL ;

	time_t now = time(NULL) ;

	CMapClient::iterator it = _mpClient.find( id ) ;
	if ( it == _mpClient.end() ) {
		p = new _ClientInfo ;
		p->_id     = id ;
		p->_errcnt = 0 ;
		p->_time   = now ;
		p->_tail   = p->_head = NULL ;
		p->_size   = 0 ;
		p->_send   = 0 ;
		p->_recv   = 0 ;

		ADD_NODE( _head, _tail, p ) ;

		++ _size ;

		_mpClient.insert( CMapClient::value_type(id, p ) ) ;

	} else {
		p = it->second ;
		p->_time = now ;
	}
	return p ;
}

// 是否出错
void CStatInfo::AddVechile( unsigned int id, const char *macid, int flag )
{
	if ( macid == NULL )
		return ;

	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( id ) ;
	if ( p == NULL )
		return ;

	_CarInfo *info = NULL ;

	CMapCar::iterator it = p->_mpcar.find( macid ) ;
	if ( it == p->_mpcar.end() ) {
		info = new _CarInfo ;
		info->_time     = time(NULL) ;
		info->_macid    = macid ;
		info->_errcnt   = 0 ;
		if ( flag & STAT_ERROR ) {
			++ info->_errcnt ;
			++ p->_errcnt ;
		}
		ADD_NODE( p->_head, p->_tail, info ) ;
		p->_mpcar.insert( CMapCar::value_type( macid, info ) ) ;
		++ p->_size ;

	} else {
		info = it->second ;
		info->_time = time(NULL) ;
		if ( flag & STAT_ERROR ) {
			info->_errcnt = info->_errcnt + 1 ;
		}
	}
	// 一次处理统计业务
	if ( flag & STAT_RECV ) ++ p->_recv ;
	if ( flag & STAT_SEND ) ++ p->_send ;
}

// 接收到的个数
void CStatInfo::AddRecv( unsigned int id )
{
	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( id ) ;
	if ( p == NULL )
		return ;

	++ p->_recv ;
}

// 发送出去的个数
void CStatInfo::AddSend( unsigned int id )
{
	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( id ) ;
	if ( p == NULL )
		return ;

	++ p->_send ;
}

// 检测是否超时
void CStatInfo::Check( void )
{
	share::Guard g( _mutex ) ;

	time_t ntime = _lasttime ;
	time_t now = time(NULL) ;
	if ( now - _lasttime < 30 ) {
		return ;
	}
	_lasttime = now ;

	if ( _head == NULL )
		return ;

	std::string s ;
	char szbuf[512] = {0} ;

	int count = 0 ;

	_ClientInfo *tmp = NULL ;
	_ClientInfo *p   = _head ;
	while( p != NULL ) {
		tmp = p ;
		p = p->_next ;

		// 如果6分钟内没有数据就直接超时
		if ( now - tmp->_time > 180 ) {
			CMapClient::iterator it = _mpClient.find( tmp->_id ) ;
			if ( it != _mpClient.end() ) {
				DEL_NODE( _head, _tail, tmp ) ;
				DelClient( tmp ) ;
				-- _size ;
				_mpClient.erase( it ) ;
			}
		} else {
			// 检测客户端的数据
			count += CheckClient( tmp , now ) ;
		}

		sprintf( szbuf, "%d(%d/%d/%d)",
				tmp->_id, tmp->_size, tmp->_send, tmp->_recv ) ;
		tmp->_recv  = 0 ;
		tmp->_send  = 0 ;

		if ( ! s.empty() ) {
			s += "," ;
		}
		s += szbuf ;
	}
	// 记录当前在线的总的车辆数
	_ncar = count ;
	// 打印统计日志
	OUT_RUNNING( NULL, 0, "ONLINE", "%s client: %d, total car: %d, time span: %d, %s",
			_name.c_str(), _size, _ncar, now-ntime, s.c_str() ) ;
}

// 检测客户端数据
int CStatInfo::CheckClient( _ClientInfo *p , time_t now )
{
	if ( p->_head == NULL )
		return 0;

	_CarInfo *tmp = NULL ;
	_CarInfo *info = p->_head ;
	while( info != NULL ) {
		tmp = info ;
		info = info->_next ;
		if ( now - tmp->_time > 360 ) {
			CMapCar::iterator it = p->_mpcar.find( tmp->_macid ) ;
			if ( it != p->_mpcar.end() )
				p->_mpcar.erase( it ) ;

			DEL_NODE( p->_head, p->_tail, tmp ) ;
			if ( tmp->_errcnt > 0 ) {
				-- p->_errcnt ;
			}
			delete tmp ;
			-- p->_size ;
		}
	}
	return p->_size ;
}

// 释放客户端数据
void CStatInfo::DelClient( _ClientInfo *p )
{
	if ( p->_size > 0 ) {
		CMapCar::iterator itx ;
		for ( itx = p->_mpcar.begin(); itx != p->_mpcar.end(); ++ itx ) {
			delete itx->second ;
		}
		p->_mpcar.clear() ;
		p->_size = 0 ;
		p->_head = p->_tail = NULL ;
	}
	delete p ;
}

// 清除所有数据
void CStatInfo::Clear( void )
{
	share::Guard g( _mutex ) ;

	if ( _head == NULL )
		return ;

	CMapClient::iterator it ;
	for ( it = _mpClient.begin(); it != _mpClient.end(); ++ it ) {
		DelClient( it->second ) ;
	}
	_mpClient.clear() ;
	_head = _tail = NULL ;
	_size = 0 ;
}



