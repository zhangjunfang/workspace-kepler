/*
 * stat.h
 *
 *  Created on: 2011-11-16
 *      Author: humingqing
 */

#ifndef __STATFLUX_H_
#define __STATFLUX_H_

#include <time.h>
#include <Mutex.h>

#define FLUX_KB	 1024.0f

class CStatFlux
{
public:
	CStatFlux( int span = 5 ){
		_last   = time(0) ;
		_count  = 0 ;
		_flux   = 0.0f ;
		_span   = 5 ;
	}
	~CStatFlux(){
	}

	// 简单流量统计
	void AddFlux( int n ){
		share::Guard guard( _mutex ) ;

		// 累加流量
		_count  = _count + n ;

		time_t now = time(0) ;
		// 计算某时间段的流量
		if ( now - _last >= _span ) {
			_flux   = (float)_count / (float)( now - _last ) ;
			_last   = now ;
			_count  = 0 ;
		}
	}

	// 取得流量
	float GetFlux( void ) {
		share::Guard guard( _mutex ) ;
		return _flux ;
	}
private:
	// 同步锁
	share::Mutex    	 _mutex ;
	// 最后一次时间
	time_t   			 _last ;
	// 数据量计数
	unsigned int 	 	 _count ;
	// 最后一次的流量
	float 				 _flux ;
	// 平均时间间隔
	unsigned int		 _span ;
};

#endif /* STAT_H_ */
