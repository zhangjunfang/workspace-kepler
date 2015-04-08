#ifndef __SOCKETHANDLE_H__
#define __SOCKETHANDLE_H__

#include <errno.h>
#include <databuffer.h>

#ifdef _XDEBUG
#define FUNTRACE(fmt, ...)  printf( fmt , ##__VA_ARGS__ )
#define LOGDEBUG( ip, port, fmt , ... )   \
	OUT_INFO( ip, port, NULL, fmt, ## __VA_ARGS__ )
#else
#define FUNTRACE(fmt, ...)
#define LOGDEBUG( ip, port, fmt , ... )
#endif

#define ERRLOG( ip, port, fmt , ... )  \
	OUT_ERROR( ip, port, NULL, fmt, ## __VA_ARGS__ )

#define MAX_SOCKET_NUM 1024
#define MAX_EVENTS_NUM 1024

#ifdef _MAC_OS
#define MAX_FD_OFFSET			12000    // MAC OS最多为
#else
#define MAX_FD_OFFSET 			0xfffff   // 最大连接数不能超过65535*16
#endif
#define MIN_FD_OFFSET  			64
#define SOCKET_CONTINUE     	1   	// 继续处理
#define SOCKET_SUCCESS 	 		0  		// 处理数据成功
#define SOCKET_FAILED			-1  	// 处理数据错误
#define SOCKET_DISCONN 			-2  	// 接收断连的请求
#define SOCKET_RECVERR   		-3  	// 接收数据错误
#define SOCKET_SENDERR			-4  	// 发送数据错误
#define SOCKET_BUFFER		    -5  	// TCP发送缓存区满

#define THREAD_POOL_SIZE         2  	// 默认线程个数
#ifndef READ_BUFFER_SIZE
#define READ_BUFFER_SIZE 		4096	// 缓存数据大小
#endif
#define UDP_FD_MASK				0xffff    // UDP掩码
#define UDP_FD_OFSSET			0x10000   // UDP开始位置
#define MAX_SOCKET_BUF			640000    // 最大发送和接收缓存区的大小
#define MIN_RECV_THREAD			8		  // 接收线程处理
#define SOCKET_TIMEOUT		    180       // 默认连接三分钟没数据超时
#define SOCKET_CHECK			30		  // 30秒检测连接

struct packet ;
//struct kfifo ;
// 回底层调分包对象
class IPackSpliter
{
public:
	virtual ~IPackSpliter() {}
	// 分包处理
	virtual struct packet * get_kfifo_packet( DataBuffer *fifo )  = 0 ;
	// 释放数据包
	virtual void free_kfifo_packet( struct packet *packet ) = 0 ;
};

enum SocketType{
	FD_TCP = 1 ,   // TCP连接方式
	FD_UDP = 2 ,   // UDP连接方式
};

enum EventType {
	ReadableEvent = 1,    //!< data available to read
	WritableEvent = 2,    //!< connected/data can be written without blocking
	Exception     = 4     //!< uh oh
};

// 连接对象
class socket_t
{
public:
	socket_t() {
		_type   = FD_TCP ;
		_fd     = -1 ;
		_events = 0 ;
		_last   = 0 ;
		_ptr    = NULL ;
		_next   = NULL ;
		_pre    = NULL ;
	}

	virtual ~socket_t() {}

public:
	// 连接类型
	unsigned char 		_type ;
	// 网连接SOCKET
	int 		  		_fd ;
	// 事件对象
	unsigned int  		_events ;
	// 接入IP地址
	char		   		_szIp[32];      // 服务器的IP
	// 接入的端口
	unsigned short		_port ;          // 服务器的端口
	// 最后一次接收数据时间
	time_t			    _last ;
	// 允许用户扩展数据指针
	void *				_ptr ;
	// 指向下一个数据指针
	socket_t  	 	   *_next ;
	// 前驱节点
	socket_t		   *_pre ;
};

class CSocketHandle
{
public:
	CSocketHandle(){};
	virtual ~CSocketHandle(){};

public:	
	//创建
    virtual bool create(int max_socket_num = MAX_SOCKET_NUM) = 0 ;
	virtual bool destroy() { return false; } ;
    virtual bool add(socket_t *fd, unsigned int events) = 0 ;
	virtual bool del(socket_t *fd, unsigned int events) = 0 ;
	virtual bool modify(socket_t *fd, unsigned int events) = 0 ;
	virtual int  poll(int timeout = 5) = 0 ;
	
	virtual bool is_read(int events){return false;};
	virtual bool is_write(int events){return false;};
	virtual bool is_excep(int events){return false;};
	
    virtual void on_event( socket_t *fd, int events ){};
};

#endif
