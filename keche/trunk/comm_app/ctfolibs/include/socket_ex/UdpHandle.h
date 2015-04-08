/***
 *  memo  : UDP服务器程序
 *  author: humingqing
 *  date:   2012-11-22
 *  description: UDP处理类，主要实现原理将UDP封装成内部的类似TCP的处理机制处理，
 *
 */
#ifndef __UDPHANDLE_H__
#define __UDPHANDLE_H__

#include <map>
#include <vector>
#include <string>
#include <set>
using namespace std;
#ifndef _UNIX
#include "EpollHandle.h"
#else
#ifdef _USE_POLL
#include "PollHandle.h"
#else
#include "KQueueHandle.h"
#endif
#endif
#include <Thread.h>
#include "TQueue.h"
#include "dataqueue.h"
#include "queuethread.h"
#include "protocol.h"

#define  UDP_CLIENT_CONN		1    // UDP客户端
#define  UDP_SERVER_CONN		2    // 服务器端
#define  UDP_UNKNOW_CONN		3
#define  UDP_THREAD_CONN   		1    // 连接线程
#define  UDP_THREAD_DATA		2	 // 数据处理线程

class CUdpHandle ;
// SOCK对象管理，这里设计使用读写锁，这样可以实现多个线程同时读的操作
class CUdpSockManager
{
public:
	//在系统中描述一个有效的socket。
	class udpconnect_t: public socket_t
	{
	public:
		udpconnect_t() :_read_buff(NULL) { beginsock(true, true); } ;
		virtual ~udpconnect_t() { endsock(true) ; }

		// 设置IP和端口
		void  init( int fd, const char *ip, unsigned short port , int ctype ) ;
		// 读取数据
		struct packet * split( const char *buf, int len, IPackSpliter *pack ) ;
		// 写数据
		int  write( const char *data, const int len ) ;
		// 是否为客户端连接
		bool close( void ) ;

	private:
		// 初始化系统数据
		void beginsock( bool bkfifo  , bool breset ) ;
		// 析构数据
		void endsock( bool bkfifo ) ;

	private:
		unsigned long  		 _size ;          // 缓存中的数据大小，单位字节。
		share::Mutex		 _mutex ;
		DataBuffer 		    *_read_buff ;     // 数据缓存
		int 				 _ctype ;		  // 连接类型
	};
public:
	CUdpSockManager()  ;
	virtual ~CUdpSockManager() ;

	// 分包处理
	socket_t * recv( int server_fd, CUdpHandle *handle , int &ret, int &err , IPackSpliter *pack ) ;
	// 签出数据
	socket_t * get( int sockfd, const char *ip, unsigned short port , int ctype , bool queue = true ) ;
	// 签入数据
	void  put( socket_t *sock ) ;
	// 关闭连接
	bool  close( socket_t *sock ) ;
	// 检测操时的连接对象
	int   check( int timeout, std::list<socket_t*> &lst ) ;

private:
	// 回收所有资源
	void clear( void ) ;

private:
	// 数据队列头
	TQueue<socket_t>		     	 _queue ;
	// 在线队列查找索引
	std::set<socket_t*>  			 _index ;
	// 在线队列管理
	TQueue<socket_t>				 _online ;
	// 数据同步操作锁
	share::Mutex         			 _mutex ;
	// 连接对象查找管理
	std::map<std::string, socket_t*> _mpsock;
	// 数据接收缓存区的大小
	char 		 _szbuf[READ_BUFFER_SIZE+1];
};

#ifndef _UNIX
class CUdpHandle : public CEpollHandle, public share::Runnable , public IQueueHandler
#else
#ifdef _USE_POLL
class CUdpHandle : public CPollHandle, public share::Runnable , public IQueueHandler
#else
class CUdpHandle : public CKQueueHandle , public share::Runnable ,public IQueueHandler
#endif
#endif
{
	friend class CUdpSockManager ;
	// 默认的分包对象
	class CDefaultSpliter: public IPackSpliter
	{
	public:
		CDefaultSpliter(){}
		~CDefaultSpliter(){}

		// 分包处理
		struct packet * get_kfifo_packet( DataBuffer *fifo ){
			// 取得新协议数据的分包
			return get_packet_from_kfifo(fifo) ;
		}

		// 释放数据包
		void free_kfifo_packet( struct packet *packet ){
			// 释放数据包
			free_packet( packet ) ;
		}
	};

public:
	CUdpHandle();
	virtual ~CUdpHandle();

	//初始化线程池，异步socket，使用前必须调用该函数
	bool 	init( unsigned int nthread = THREAD_POOL_SIZE , unsigned int timeout = SOCKET_TIMEOUT );
	// 停止线程，销毁数据
	bool 	uninit() ;

	bool 	start_server( int port , const char* ip = NULL  ) ;
	bool 	stop_server() ;

	//将应用层数据投递到fd缓冲区，等待线程池的调度发送。
	bool 	deliver_data( socket_t *sock , const void* data, int len ) ;
	// 断开连接，是否回调 on_disconnection由notify参数决定
	void    close_socket( socket_t *sock , bool notify = true ) ;
	// 设置分包对象
	void    setpackspliter( IPackSpliter *pack ) { _pack_spliter = pack ;};
	// 处理客户连接管理
	socket_t * connect_nonb(const char* ip, int port, int nsec = 5 ) ;
	// 交出数据回调接口
	void 	HandleQueue( void *packet ) ;

protected:

	// 处理事件函数
	void on_event( socket_t *sock, int events );
	// 线程运行对象
	void run( void *param )  ;
	// 写错误日志
	void write_errlog( socket_t *sock , int err , int nerr ) ;

protected:
	/*
	 *  用户自定义回调函数，此函数执行时，fd锁的状态为locked。所以
	 *  此函数中或此函数调用函数中 不能试图lock fd，否则死锁。
	 */
	virtual void on_data_arrived( socket_t *sock, void* data, int len){}
	//发送失败
	virtual void on_send_failed( socket_t *sock, void* data, int len){}
	/*
	 *  连接关闭时的回调函数。
	 */
	virtual void on_dis_connection( socket_t *sock ){};
	// 新的连接到来
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};

private:
	// 处是连接状检测线程
	void  process_check( void ) ;
	// 处理用户连接数据线程
	void  process_data( void ) ;

protected:
	//服务端口fd
	socket_t 			  * _server_fd ;
	// 是否已启动线程
	bool 					_udp_init ;
	// 设置分包对象
	IPackSpliter		 *  _pack_spliter ;
	// 网络事件监听线程
	share::ThreadManager    _thread_conn ;
	// 连接状态管理对象
	share::ThreadManager	_thread_check ;
	// 连接管理对象
	CUdpSockManager 	  * _socketmgr ;
	// 数据处理队列
	CDataQueue<CPacket>	   *_packqueue ;
	// 数据处理线程
	CQueueThread		  * _queuethread;
	// 默认分包对象
	CDefaultSpliter			_defaultpacker;
	// 连接超时回收时间
	unsigned int		    _socktimeout;
};

#endif
