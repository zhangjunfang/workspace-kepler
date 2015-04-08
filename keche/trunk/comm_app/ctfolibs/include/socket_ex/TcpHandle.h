/**
 * memo:   重新设计了TCP数据处理对象,使用新的线程对象整体想法，模块化，单一化设计,
 * 	对于底层数据分包，可以继承IPackSpliter接口的两个方法，就可以实现自己数据的分包
 * date:   2011/07/21
 * author: humingqing
 */

#ifndef __TCP_HANDLE_H__
#define __TCP_HANDLE_H__

#include <list>
#include <set>
#include <stdlib.h>
#ifndef _UNIX
#include "EpollHandle.h"
#else
#ifdef _USE_POLL
#include "PollHandle.h"
#else
#include "KQueueHandle.h"
#endif
#endif
#include <comlog.h>
#include <Thread.h>
#include <Monitor.h>
#include "TQueue.h"
#include "dataqueue.h"
#include "queuethread.h"
#include "protocol.h"

// SOCK对象管理，主要管理连接状态
class CSockManager
{
public:
	// 数据结构体
	struct _node
	{
		char  *  _dptr;
		int  	 _len;
		_node *  _next;
	};

	class nodequeue
	{
	public:
		nodequeue();
		~nodequeue();

		// 添加到头部
		void addhead( _node *p ) ;
		// 添加节点数
		bool addtail( const char *buf, int size ) ;
		// 弹出数据
		_node *popnode( void ) ;
		// 移到新链上
		void moveto( nodequeue *node ) ;
		// 重置
		void reset( void ) ;
		// 清理数据
		void clear( void ) ;
		// 取得头部数据
		_node * getnodes( void ) { return _head ; }
		// 取得数据长度
		int  size( void )  { return _size ;}
		// 添加重新处理次数
		void addref( void ) { ++ _dref ;}

	private:
		// 头节点
		_node  *		_head ;
		// 尾节点
		_node  *		_tail ;
		// 长度
		unsigned int    _size ;
		// 数据引用
		unsigned int    _dref ;
	};

	//在系统中描述一个有效的socket。
	class connect_t: public socket_t
	{
	public:
		connect_t( CSocketHandle *eventer )
			: _read_buff(NULL) , _eventer(eventer) { beginsock(); }
		virtual ~connect_t() { endsock() ; }
		// 设置IP和端口以及FD
		void init( unsigned int fd, const char *szip , unsigned short port ) ;
		// 发送数据
		int  write( int &err ) ;
		// 读取数据
		struct packet *read( int &ret , int &nerr , IPackSpliter *pack ) ;
		// 添加需要发送的数据
		bool deliver( const char *buf, int len ) ;
		// 是否已经释放连接
		bool close( void ) ;
		// 是否超时没用任保操作
		bool check( void ) ;

	public:
		// 取得链接数据
		_node * readlist( void ) ;

	private:
		// 初始化系统数据
		void beginsock( void ) ;
		// 析构数据
		void endsock( void ) ;

	private :
		unsigned char  		_status;    	// 1:表示在任务队列中，0:表示不在。
		nodequeue 		   *_inqueue ;      // 输入数据的Queue
		nodequeue		   *_outqueue ;		// 输出数据的Queue
		share::Mutex  		_mutex;	    	// 数据处理锁
		DataBuffer 		   *_read_buff;     // 数据缓存
		CSocketHandle      *_eventer;  // SOCK事件处理对象
	};

public:
	CSockManager( CSocketHandle *eventer) :_eventer(eventer)  {}
	~CSockManager() { clear(); }
	// 签出数据
	socket_t * get( int sockfd, const char *ip, unsigned short port , bool queue = true ) ;
	// 签入数据
	void  put( socket_t *sock ) ;
	// 关闭连接
	bool  close( socket_t *sock ) ;
	// 取得所有回收连接对象
	socket_t * recyle( void ) ;
	// 检测超时连接对象
	int check( int timeout, std::list<socket_t*> &lst ) ;

private:
	// 回收所有资源
	void clear( void ) ;

private:
	// 数据队列头
	TQueue<socket_t>     _queue ;
	// 在线队列管理
	std::set<socket_t*>  _index ;
	// 在线队列管理
	TQueue<socket_t>	 _online ;
	// 回收队列中
	TQueue<socket_t>	 _recyle;
	// 数据同步操作锁
	share::Mutex         _mutex ;
	// SOCK事件处理对象
	CSocketHandle     *  _eventer;
} ;

#define  TCP_THREAD_CONN   		1    // 处理事件线程
#define  TCP_THREAD_DATA   		2    // 处理数据线程
#define  TCP_MAX_THREAD        128   // 最大线程个数

#ifndef _UNIX
class CTcpHandle : public CEpollHandle, public share::Runnable ,public IQueueHandler
#else
#ifdef _USE_POLL
class CTcpHandle : public CPollHandle, public share::Runnable ,public IQueueHandler
#else
class CTcpHandle : public CKQueueHandle , public share::Runnable ,public IQueueHandler
#endif
#endif
{
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
	CTcpHandle() ;
	virtual ~CTcpHandle() ;

	//初始化线程池，异步socket，使用前必须调用该函数
	bool 	init( unsigned int nthread = THREAD_POOL_SIZE , unsigned int timeout = SOCKET_TIMEOUT );
	// 停止线程，销毁数据
	bool 	uninit() ;

	bool 	start_server( int port , const char* ip = NULL ) ;
	bool 	stop_server() ;

	//将应用层数据投递到fd缓冲区，等待线程池的调度发送。
	bool 	deliver_data( socket_t *sock , const void* data, int len ) ;
	// 断开连接，是否回调 on_disconnection由notify参数决定
	void    close_socket( socket_t *sock , bool notify = true ) ;
	// 设置分包对象
	void    setpackspliter( IPackSpliter *pack ) { _pack_spliter = pack; }
	// 线程运行对象
	void 	run( void *param )  ;
	// 交出数据回调接口
	void 	HandleQueue( void *packet ) ;
	//返回fd
	socket_t * connect_nonb( const char* ip, int port, int nsec = 5 );

protected:
	// 处理事件函数
	void on_event( socket_t *sock , int events );
	//不调用on_disconnection
	bool close_fd( socket_t *sock ) ;

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
	virtual void on_new_connection( socket_t *sock , const char* ip, int port){};
	// 检测IP是否有效
	virtual bool invalidate_ip( const char *ip ) = 0 ;

private:
	// 写入错误日志
	void  write_errlog( socket_t *sock , int err , int nerr ) ;
	// 读数据
	void  read_data( socket_t *sock ) ;
	// 写数据
	bool  write_data( socket_t *sock ) ;
	// 处是连接状检测线程
	void  process_check( void ) ;
	// 处理用户连接数据线程
	void  process_data( void ) ;

private:
	//服务端口fd
	socket_t 			* _server_fd ;
	// 是否停止服务
	bool 				  _tcp_init ;
	// 设置分包对象
	IPackSpliter		 *_pack_spliter ;
	// 处理事件线程
	share::ThreadManager  _thread_conn ;
	// 连接处理检测线程
	share::ThreadManager  _thread_check;
	// 数据处理队列
	CDataQueue<CPacket>	* _packqueue ;
	// SOCKET管理对象
	CSockManager		* _socketmgr ;
	// 数据处理线程
	CQueueThread		* _queuethread;
	// 默认的分包对象
	CDefaultSpliter		  _defaultspliter;
	// 默认连接超时时间
	unsigned int 		  _socktimeout;
};

#endif
