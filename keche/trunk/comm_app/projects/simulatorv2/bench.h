/**
 * author: humingqing
 * date:   2011-09-19
 * memo:   实时统计在线情况对象
 */
#ifndef __BENCH_H__
#define __BENCH_H__

#include <time.h>
#include <Mutex.h>
#include <Thread.h>

enum EBenchType
{
	BENCH_ALL_USER = 0 ,
	BENCH_ON_LINE  ,
	BENCH_OFF_LINE ,
	BENCH_CONNECT  ,
	BENCH_DISCONN  ,
	BENCH_MSGSEND  ,
	BENCH_MSGRECV
};

class CBench : public share::Runnable
{
	struct _BenchData
	{
		unsigned int alluser_ ;  // 最大用户数
		unsigned int online_  ;  // 当前在线用户数
		unsigned int offline_ ;  // 离线用户数
		unsigned int connect_ ;  // 连接次数
		unsigned int disconn_ ;  // 断连次数
		unsigned int allconn_ ;  // 总连接次数
		unsigned int alldis_  ;  // 总断连次数
		unsigned int msgsend_ ;  // 发送消息数
		unsigned int msgrecv_ ;  // 接收消息数
		unsigned int allsend_ ;  // 所有发送消息数
		unsigned int allrecv_ ;  // 所有接收消息数
		time_t 		 spantime_;  // 经过时间
		time_t		 lasttime_ ; // 最后一次时间

		_BenchData() {
			alluser_  =  online_ = offline_ = 0 ;
			connect_  = disconn_ = 0 ;
			msgsend_  = msgrecv_ = 0 ;
			allsend_  = allrecv_ = 0 ;
			spantime_ =  0 ;
			lasttime_ = time(NULL) ;
		}
	};
public:
	CBench() ;
	~CBench() ;

	// 初始化
	bool Init( void ) ;
	// 启动
	bool Start( void ) ;
	// 停止
	void Stop( void ) ;
	// 统计数据
	void IncBench( EBenchType type , int n = 1 ) ;

public:
	// 运行线程对象
	virtual void run( void *param ) ;

private:
	// 统计数据锁
	share::Mutex 		 _mutex ;
	// 线程对象
	share::ThreadManager _thread ;
	// 统计数据
	_BenchData		     _data ;
	// 最后一次访问的时间
	time_t			     _last_access ;
	// 是否启动线程
	bool 			 	 _start ;
};

#endif
