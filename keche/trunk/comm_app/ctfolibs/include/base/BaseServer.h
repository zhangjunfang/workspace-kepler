/**********************************************
 * BaseServer.h
 *
 *  Created on: 2010-6-24
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24 humingqing
 *    修改，主要去掉缓存队列，将线程对象使用自己写的线程池来处理数据
 *********************************************/

#ifndef BASESERVER_H_
#define BASESERVER_H_

#include <NetHandle.h>
#include <OnlineUser.h>
#include <BaseTools.h>
#include <Thread.h>

#define THREAD_NOMARL     1
#define THREAD_TIME	      2
#define THREAD_NOOP 	  3

class BaseServer : public CNetHandle , public share::Runnable
{
public:
	BaseServer( );
	~BaseServer();

	// 启动UDP的服务
	virtual bool StartUDP( int listen_port, const char * listen_ip , unsigned int nthread = THREAD_POOL_SIZE, unsigned int timeout = SOCKET_TIMEOUT ) ;
	// 启动TCP的服务
	virtual bool StartServer( int listen_port, const char* listen_ip,  unsigned int nthread = THREAD_POOL_SIZE, unsigned int timeout = SOCKET_TIMEOUT );
	// 停止TCP的服务
	virtual void StopServer( void ) ;
	virtual bool Check( void )  { return _initalized; } ;

	virtual void on_data_arrived( socket_t *sock , const void* data, int len){};
	virtual void on_dis_connection( socket_t *sock ){};
	virtual void on_new_connection( socket_t *sock , const char* ip, int port){};

protected:
	virtual void run( void *param ) ;

	virtual void TimeWork() = 0  ;
	virtual void NoopWork() = 0  ;

	bool SendData( socket_t *sock , const char* data, int len);
	void CloseSocket( socket_t *sock );

protected:
	string 				  _listen_ip;
	int 				  _listen_port;
	string 				  _udp_listen_ip ;
	int 				  _udp_listen_port ;

	share::ThreadManager  _noop_thread ;
	share::ThreadManager  _time_thread ;
	// TCP是否处理
	bool 				  _initalized  ;
	bool 				  _tcpinited   ;
	bool 				  _udpinited   ;
};


#endif /* BASESERVER_H_ */
