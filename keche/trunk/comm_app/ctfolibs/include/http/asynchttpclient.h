/**
 * author: humingqing
 * date:   2011-09-07
 * memo:   异步HTTP请求处理类，主要实现异步调用HTTP请求返回对应请求值，这里需要先获HTTP的请求的序号，
 *  	先存放等待队列中，然后再调用，异步操作需要先入队列，然后再操作，因为异步操作中可能出现响应先回的情况
 */

#ifndef  __ASYNC_HTTP_CLIENT_H__
#define  __ASYNC_HTTP_CLIENT_H__

#include <map>
#include <vector>
#include <time.h>
#include "NetHandle.h"
#include "qstring.h"
#include "httpclient.h"
#include <protocol.h>
#include <Thread.h>
#include <Monitor.h>
#include <allocmgr.h>

// 处理异步的HTTP数据
class IHttpCallbacker
{
public:
	virtual ~IHttpCallbacker() {}
	// 异步回调处理
	virtual void ProcHTTPResponse( unsigned int seq_id , const int err , const CHttpResponse& resp ) = 0 ;
};

// 异步的HTTP的处理对象
class CAsyncHttpClient : public CNetHandle, public share::Runnable , public IPackSpliter
{
	// 请求处理
	struct _REQ_DATA
	{
		// 连接的FD值
		socket_t  	*_fd ;
		// 序号ID
		unsigned int _seq ;
		// 最后一次访的时间
		time_t   	 _time ;
		// 服务器IP
		CQString  	 _ip ;
		// 服务器的端口
		unsigned int _port ;
		// 需要发送数据
		CQString  	 _senddata ;
		// 引用计数处理
		int 		 _ref ;

		_REQ_DATA   *_next ;
		_REQ_DATA   *_pre ;
	};

	// 等待处理
	class CWaitListReq
	{
	public:
		CWaitListReq() ;
		~CWaitListReq() ;
		// 存放等待处理队列中
		void PushReq( _REQ_DATA *data ) ;
		// 返回需要处理的请求
		_REQ_DATA *PopReq ( void ) ;
		// 开辟空间
		_REQ_DATA *AllocReq( void ) ;
		// 回收对象
		void FreeReq( _REQ_DATA *req ) ;
		// 取得当前元素个数
		int GetQueueSize() ;

	private:
		// 等待处理请求
		typedef std::list<_REQ_DATA*>	CListReq ;
		CListReq				_list_req ;
		// 记录当前请求个数
		int 					_size ;
		// 等待的个数
		int 					_waitsize;
		// 锁处理
		share::Mutex  			_mutex ;
		share::Monitor  		_monitor ;
		// 内存管理对象
		TAllocMgr<_REQ_DATA>    _allocmgr ;
	};

public:
	CAsyncHttpClient();
	~CAsyncHttpClient();

public:
	// 开始线程
	bool Start( unsigned int nsend = 1 , unsigned int nrecv = 1 ) ;
	// 停止线程
	void Stop( void ) ;
	// 发送HTTP的请求，返回对应的请求序号
	int  HttpRequest( CHttpRequest& request , unsigned int seq_id ) ;
	// 设置数据处理回调对象
	void SetDataProcessor( IHttpCallbacker* p ){ _pCallbacker = p ; } ;
	// 取得请求序号
	unsigned int GetSequeue( void ) ;
	// 设置HTTP请求队列中最大长度
	void SetQueueSize( int size ){ _maxsize = size; };

public:
	// 运行线程
	virtual void run( void *param ) ;
	virtual void on_data_arrived( socket_t *fd, const void* data, int len);
	virtual void on_dis_connection( socket_t *fd ) ;
	virtual void on_new_connection( socket_t *fd, const char* ip, int port){};
	// 分包处理
	virtual struct packet * get_kfifo_packet( DataBuffer *fifo ) ;
	// 释放数据包
	virtual void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}

private:
	// 处理等待队列中的请求
	void ProcessWaitReq( void ) ;

private:
	// 数据回调对象
	IHttpCallbacker* 		_pCallbacker ;
	// 处理数据线程
	share::ThreadManager  	_check_thread ;
	// 索号处理
	unsigned int 			_seq_id ;
	// 序号锁
	share::Mutex 			_mutex_seq ;
	// 是否初始化
	bool 					_initalized ;
	// 等待处理数据
	CWaitListReq			_list_req ;
	// 最大的HTTP的队列长度
	int 					_maxsize ;
	// 引用计数锁
	share::Mutex		    _mutex_ref ;
};



#endif
