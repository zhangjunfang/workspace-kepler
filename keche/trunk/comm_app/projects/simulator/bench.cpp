#include "bench.h"
#include <comlog.h>

CBench::CBench()
{
	_last_access = time(0) ;
	_start       = false ;
}

CBench::~CBench()
{
	Stop() ;
}

// 初始化
bool CBench::Init( void )
{
	if ( ! _thread.init( 1, NULL, this ) ) {
		printf( "init bench failed\n" ) ;
		OUT_ERROR( NULL, 0, "bech" , "init bech failed" ) ;
		return false ;
	}
	return true ;
}

// 启动
bool CBench::Start( void )
{
	_start = true ;
	_thread.start() ;

	return true ;
}

// 停止
void CBench::Stop( void )
{
	if ( ! _start )
		return ;
	_start = false ;
	_thread.stop() ;
}

// 统计数据
void CBench::IncBench( EBenchType type , int n )
{
	share::Guard guard( _mutex ) ;
	{
		switch( type ) {
		case BENCH_ALL_USER:
			{
				_data.alluser_ = n ;
			}
			break ;
		case BENCH_ON_LINE:
			{
				_data.online_  = n ;
			}
			break ;
		case BENCH_OFF_LINE:
			{
				_data.offline_ = n ;
			}
			break ;
		case BENCH_CONNECT:
			{
				_data.connect_ += n ;
				_data.allconn_ += n ;
			}
			break ;
		case BENCH_DISCONN:
			{
				_data.disconn_ += n ;
				_data.alldis_  += n ;
			}
			break ;
		case BENCH_MSGSEND:
			{
				_data.msgsend_ += n ;
				_data.allsend_ += n ;
			}
			break ;
		case BENCH_MSGRECV:
			{
				_data.msgrecv_ += n ;
				_data.allrecv_ += n ;
			}
			break ;
		}
	}
}

// 运行线程对象
void CBench::run( void *param )
{
	const char *fmt = "---------------------------Bench-------------------------------\r\n"
			"all user: %d , online_user: %d, offline user: %d\r\n"
			"connect total : %d, disconnect total : %d \r\n"
			"connect time: %.2f , disconnect time: %.2f , \r\n"
			"msg send speed: %.2f , msg recv speed: %.2f\r\n"
			"total send : %d , total recv: %d , span time: %ld \r\n"
			"---------------------------------------------------------\r\n";

	char buf[1024] = {0} ;

	while( _start ) {
		time_t now = time(NULL) ;
		if ( now - _last_access < 5 ) {
			sleep( 1 ) ;
			continue ;
		}

		float span = (float) ( now - _last_access ) ;
		_last_access = now ;

		_mutex.lock() ;
		_data.spantime_ = now - _data.lasttime_ ;
		sprintf( buf, fmt , _data.alluser_ , _data.online_ , _data.offline_ ,
				_data.allconn_, _data.alldis_, (float) _data.connect_ / span , (float) _data.disconn_/span ,
				(float) _data.msgsend_ / span , (float) _data.msgrecv_ / span ,
				_data.allsend_ , _data.allrecv_ , _data.spantime_ ) ;
		_data.lasttime_ = now ;
		_data.msgsend_  = _data.msgrecv_  = 0 ;
		_data.connect_  = _data.disconn_  = 0 ;
		_mutex.unlock() ;

		printf( "%s\n" , buf);
		OUT_INFO( NULL, 0, "BENCH", buf ) ;
	}
}
