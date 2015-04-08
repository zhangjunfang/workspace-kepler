/*
 * statinfo.cpp
 *
 *  Created on: 2012-7-27
 *      Author: humingqing
 */

#include "statinfo.h"
#include <tools.h>

// 添加双向链的节点宏
#define ADD_NODE( head, tail, p )   { p->_pre  = p->_next = NULL ; \
	if ( head == NULL ) {  head = tail = p ; } else { tail->_next = p ; p->_pre = tail  ; tail   = p ; } }

// 删除双向链的节点宏
#define DEL_NODE( head, tail, p )  { \
	if ( p == head ) { if ( head == tail ) { head = tail = NULL ; } else { head = p->_next ; head->_pre = NULL ; } \
	} else if ( p == tail ) { tail = p->_pre ; tail->_next = NULL ; } else { p->_pre->_next = p->_next ; p->_next->_pre = p->_pre  ;  } }


CStatInfo::CStatInfo()
{
	_head = _tail = NULL ;
	_size = 0 ;
	_lasttime = time(NULL) ;
}

CStatInfo::~CStatInfo()
{
	Clear() ;
}

// 设置车辆总数
void CStatInfo::SetTotal( unsigned int total )
{
	_ntotal = total ;
}

// 设置的客户的IP和端口
void CStatInfo::SetClient( const char *ip, unsigned short tcpport, unsigned short udpport )
{
	share::Guard g( _mutex ) ;
	// 设置客户端的IP和端口
	AddClient( ip, tcpport, udpport ) ;
}

// 添加客户端
CStatInfo::_ClientInfo *CStatInfo::AddClient( const char *ip, unsigned short tcpport, unsigned short udpport )
{
	_ClientInfo *p = NULL ;
	CMapClient::iterator it = _mpClient.find( ip ) ;
	if ( it == _mpClient.end() ) {
		p = new _ClientInfo ;
		p->_ip     = ip ;
		p->_errcnt = 0 ;
		p->_tcp    = tcpport ;
		p->_udp    = udpport ;
		p->_login  = time(NULL) ;
		p->_time   = time(NULL) ;
		p->_tail   = p->_head = NULL ;
		p->_size   = 0 ;
		p->_send   = 0 ;
		p->_recv   = 0 ;

		ADD_NODE( _head, _tail, p ) ;

		++ _size ;

		_mpClient.insert( CMapClient::value_type(ip, p ) ) ;

	} else {
		p = it->second ;
		if ( tcpport > 0 ) {
			p->_tcp = tcpport ;
		}
		if ( udpport > 0 ) {
			p->_udp = udpport ;
		}
		p->_time = time(NULL) ;
	}
	return p ;
}

// 是否出错
void CStatInfo::AddVechile( const char *ip, const char *carnum, unsigned char color, bool error )
{
	if ( carnum == NULL )
		return ;

	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( ip, 0, 0 ) ;
	if ( p == NULL )
		return ;

	_CarInfo *info = NULL ;

	CMapCar::iterator it = p->_mpcar.find( carnum ) ;
	if ( it == p->_mpcar.end() ) {
		info = new _CarInfo ;
		info->_time     = time(NULL) ;
		info->_carnum   = carnum ;
		info->_carcolor = color ;
		info->_errcnt   = 0 ;
		if ( error ) {
			++ info->_errcnt ;
			++ p->_errcnt ;
		}
		ADD_NODE( p->_head, p->_tail, info ) ;
		p->_mpcar.insert( CMapCar::value_type( carnum, info ) ) ;
		++ p->_size ;

	} else {
		info = it->second ;
		info->_time = time(NULL) ;
		info->_errcnt = info->_errcnt + ( error ) ? 1 : 0  ;
	}
}

// 接收到的个数
void CStatInfo::AddRecv( const char *ip )
{
	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( ip, 0, 0 ) ;
	if ( p == NULL )
		return ;

	++ p->_recv ;
}

// 发送出去的个数
void CStatInfo::AddSend( const char *ip )
{
	share::Guard g( _mutex ) ;

	_ClientInfo *p = AddClient( ip, 0, 0 ) ;
	if ( p == NULL )
		return ;

	++ p->_send ;
}

// 检测是否超时
void CStatInfo::Check( void )
{
	share::Guard g( _mutex ) ;

	time_t now = time(NULL) ;
	if ( now - _lasttime < 30 ) {
		return ;
	}
	_lasttime = now ;

	if ( _head == NULL )
		return ;

	int count = 0 ;
	_ClientInfo *tmp = NULL ;
	_ClientInfo *p   = _head ;
	while( p != NULL ) {
		tmp = p ;
		p = p->_next ;

		// 如果6分钟内没有数据就直接超时
		if ( now - tmp->_time > 360 ) {
			CMapClient::iterator it = _mpClient.find( tmp->_ip ) ;
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
	}
	// 记录当前在线的总的车辆数
	_nonline = count ;
}

void CStatInfo::Print( const char *xml )
{
	std::string s = "<TransmitData>\n" ;

	_mutex.lock() ;

	char szbuf[1024] = {0} ;
	sprintf( szbuf, "<ServerStatus status=\"1\" vehiclecount=\"%d\" online=\"%d\">\n",  _ntotal, _nonline ) ;
	s += szbuf ;

	_ClientInfo *p   = _head ;
	while( p != NULL ) {
		sprintf( szbuf,
					"<ConnectionOrg name=\"%s\" connstatus=\"1\" lastconnect=\"%u\" lastbreak=\"%u\" getmessage=\"%u\" sendmessage=\"%u\"/>\n",
					p->_ip.c_str(), p->_login, p->_time, p->_recv, p->_send ) ;
		s += szbuf ;
		p = p->_next ;
	}

	s +="</ServerStatus>\n</TransmitData>\n" ;

	_mutex.unlock() ;

	// 如果写文件失败
	if ( ! WriteFile( xml, s.c_str(), s.length() ) ) {
		printf( "%s\n", s.c_str() ) ;
	}
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
			CMapCar::iterator it = p->_mpcar.find( tmp->_carnum) ;
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



