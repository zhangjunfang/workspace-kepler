/**********************************************
 * BaseClient.h
 *
 *  Created on: 2010-6-24
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments: 2011-07-24 humingqing
 *    修改，主要去掉缓存队列，将线程对象使用自己写的线程池来处理数据
 *********************************************/

#ifndef BASECLIENT_H_
#define BASECLIENT_H_

#include <NetHandle.h>
#include <OnlineUser.h>
#include <BaseTools.h>
#include <Thread.h>

#define THREAD_NOMARL     1
#define THREAD_TIME	      2
#define THREAD_NOOP 	  3

class BaseClient : public CNetHandle, public share::Runnable
{
public:
	BaseClient();
	virtual ~BaseClient();

	// 初始化UDP的服务
	virtual bool StartUDP( const char * connect_ip, int connect_port, int thread , unsigned int timeout = SOCKET_TIMEOUT ) ;
	// 开始线程
	virtual bool StartClient(const char* connect_ip, int connect_port, int thread , unsigned int timeout = SOCKET_TIMEOUT );
	// 停止线程
	virtual void StopClient( void ) ;
	// 是否开始线程
	virtual bool Check( void ) { return _initalized ; }

	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	virtual void on_dis_connection( socket_t *sock ){};
	virtual void on_new_connection( socket_t *sock, const char* ip, int port ){};

protected:
	virtual void run( void *param )  ;

	virtual void TimeWork() = 0 ;
	virtual void NoopWork() = 0 ;
	// 使用此方法就必需实现build_login_msg连接服务器
	virtual bool ConnectServer(User &user, unsigned int timeout);
	// 构建登陆处理
	virtual int build_login_msg( User &user, char *buf, int buf_len ) { return -1; };

	bool SendData( socket_t *sock, const char* data, int len);
	bool CloseSocket( socket_t *sock );

protected:
	User 			 	  	_client_user;
	User					_udp_user ;
	share::ThreadManager  	_noop_thread ;
	share::ThreadManager  	_time_thread ;
	bool 				  	_initalized  ;
	bool 					_udp_inited ;
	bool 					_tcp_inited ;
};

#endif /* BASECLIENT_H_ */
