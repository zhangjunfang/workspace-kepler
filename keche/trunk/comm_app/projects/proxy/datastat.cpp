/*
 * datastat.cpp
 *
 *  Created on: 2012-9-25
 *      Author: humingqing
 *  流速统计
 */

#include "datastat.h"

CStat::CStat( int span )
{
	_last   = time(0) ;
	_count  = 0 ;
	_len    = 0 ;
	_flux   = 0.0f ;
	_nflux  = 0.0f ;
	_span   = span ;
}

// 简单流量统计
void CStat::AddFlux( int n ){
	share::Guard guard( _mutex ) ;

	// 累加流量
	_len   = _len   + n ;
	_count = _count + 1 ;

	_atime = time(0) ;
	// 计算某时间段的流量
	if ( _atime - _last >= _span ) {
		_flux   = (float)_len / (float)( _atime - _last ) ;
		_nflux  = (float)_count / (float)( _atime - _last ) ;
		_last   = _atime ;
		_len    = _count = 0 ;
	}
}

// 取得流量
void CStat::GetFlux( float &count, float &speed )
{
	share::Guard guard( _mutex ) ;
	count = _nflux ;
	speed = _flux ;
}

// 检测是否超时
bool CStat::Check( int timeout )
{
	share::Guard guard( _mutex ) ;

	time_t now = time(0) ;
	if ( now - _atime > timeout ) {
		_flux  = _nflux = 0.0f ;
		return true ;
	}
	return false ;
}

///////////////////////////////// 流速对象管理  ////////////////////////////////////////
// 添加流速统计
void CDataStat::AddFlux( int id, int len )
{
	_mutex.lock() ;
	CMapStat::iterator it = _mpstat.find( id ) ;
	if ( it != _mpstat.end() ) {
		it->second->AddFlux( len ) ;
	} else {
		CStat *stat = new CStat;
		stat->AddFlux( len ) ;
		_mpstat.insert( std::make_pair( id, stat ) ) ;
	}
	_mutex.unlock() ;
}

// 取得单个流速统计
void CDataStat::GetFlux( int id, float &count, float &speed )
{
	_mutex.lock() ;
	CMapStat::iterator it = _mpstat.find( id ) ;
	if ( it != _mpstat.end() ) {
		it->second->GetFlux( count, speed ) ;
	}
	_mutex.unlock() ;
}

// 移除统计
void CDataStat::Remove( int id )
{
	_mutex.lock() ;
	CMapStat::iterator it = _mpstat.find( id ) ;
	if ( it != _mpstat.end() ) {
		CStat *p = it->second ;
		_mpstat.erase( it ) ;
		delete p ;
	}
	_mutex.unlock() ;
}

// 取得流速字符串
const std::string  CDataStat::GetFlux( void )
{
	std::string s ;
	_mutex.lock() ;

	float count = 0.0f, speed = 0.0f ;
	char szbuf[512] = {0} ;

	CMapStat::iterator it ;
	for ( it = _mpstat.begin(); it != _mpstat.end(); ) {
		CStat *p = it->second ;
		if ( p->Check(180) ) {
			_mpstat.erase( it ++ ) ;
			delete p ;
			continue ;
		}

		p->GetFlux( count, speed ) ;
		sprintf( szbuf, "id:%u(%fkb,%f)", it->first, (float)speed/((float)DF_KB), count ) ;
		if ( ! s.empty() ) {
			s += "," ;
		}
		s += szbuf ;

		++ it ;
	}

	_mutex.unlock() ;
	return s ;
}

// 清理所有数据
void CDataStat::Clear( void )
{
	_mutex.lock() ;
	if( _mpstat.empty() ){
		_mutex.unlock() ;
		return ;
	}

	CMapStat::iterator it ;
	for ( it = _mpstat.begin(); it != _mpstat.end(); ++ it ) {
		delete it->second ;
	}
	_mpstat.clear() ;
	_mutex.unlock() ;
}


