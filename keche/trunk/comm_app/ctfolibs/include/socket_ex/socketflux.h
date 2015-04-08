/*
 * socketflux.h
 *
 *  Created on: 2012-1-12
 *      Author: Administrator
 */

#ifndef __SOCKETFLUX_H__
#define __SOCKETFLUX_H__

#include <time.h>
#include <Mutex.h>

#define FLUX_KB	 1024.0f

class CSocketFlux
{
public:
	CSocketFlux( int span = 5 ){
		_last  = time(0) ;
		_recv  = _send = 0 ;
		_rflux = _wflux = 0.0f ;
		_span  = 5 ;
	}
	~CSocketFlux(){
	}

	// 简单流量统计
	void AddFlux( int rn , int wn){
		share::Guard guard( _mutex ) ;

		// 累加流量
		_recv  = _recv + rn ;
		_send  = _send + wn ;

		time_t now = time(0) ;
		unsigned int nspan = now - _last ;
		// 计算某时间段的流量
		if ( nspan >= _span ) {
			_rflux   = (float)_recv / (float)nspan ;
			_wflux   = (float)_send / (float)nspan ;
			_last    = now ;
			_recv    = _send   =   0 ;
			printf( "recv flux: %f, send flux: %f\n" , _rflux, _wflux ) ;
		}
	}

	// 取得流量
	float GetRFlux( void ) {
		share::Guard guard( _mutex ) ;
		return _rflux ;
	}

	// 取得读数据流量
	float GetWFlux( void ) {
		share::Guard guard( _mutex ) ;
		return _wflux ;
	}

private:
	// 同步锁
	share::Mutex    	 _mutex ;
	// 最后一次时间
	time_t   			 _last ;
	// 数据量计数
	unsigned int 	 	 _recv ;
	// 发送数据量
	unsigned int 		 _send ;
	// 最后一次的流量
	float 				 _rflux ;
	// 写数据的流量
	float 				 _wflux ;
	// 平均时间间隔
	unsigned int		 _span ;
};


#endif /* SOCKETFLUX_H_ */
