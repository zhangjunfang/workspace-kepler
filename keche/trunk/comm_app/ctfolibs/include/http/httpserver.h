/*
 * httpserver.h
 *
 *  Created on: 2012-2-29
 *      Author: humingqing
 *
 *  HTTP服务器框架，实现简单的HTTP数据处理，暂时不解析HTTP发送过来的数据，需要由外部服务器进行解析处理
 */

#ifndef __HTTPSERVER_H__
#define __HTTPSERVER_H__

#include <map>
#include "httpresp.h"
#include "NetHandle.h"
#include <protocol.h>

/**
 * HTTP服务器错误码
 */
#define HTTPSVR_ERR_SUCCESS								0
#define HTTPSVR_ERR_FAILED								-1
#define HTTPSVR_ERR_ALREADYSTART						-2
#define HTTPSVR_ERR_RECVPORTBINDFAILED					-3
#define HTTPSVR_ERR_LISTENFAILED						-4
#define HTTPSVR_ERR_STARTTHREADPOOLFAILED				-5
#define HTTPSVR_ERR_PARAMERROR							-6
#define HTTPSVR_ERR_PROCING								-7
#define HTTPSVR_ERR_SAVECONNFAILED						-8

/**
 * 业务容器对象接口
 */

#define SERVERCONTAINER_ERR_SUCCESS							0
#define SERVERCONTAINER_ERR_PROCING							1
#define SERVERCONTAINER_ERR_FAILED							-1
#define SERVERCONTAINER_ERR_DATAILLEGAL						-2

class IHttpResponse ;
class IHTTPServerContainer
{
public:
	virtual ~IHTTPServerContainer(){}
	/**
	 * 调用容器对象处理HTTP	请求
	 * @param conn_id const time_t 连接ID，在异步处理时有用
	 */
	virtual int ProcRequest( const char* data , const int size , IHttpResponse* resp ) = 0 ;
};

class CHttpServer : public CNetHandle,public IPackSpliter
{
public:
	CHttpServer(IHTTPServerContainer* pContainer);
	~CHttpServer();

	/**
	 * 启动服务器,设置服务器端口
	 */
	int Start( unsigned int thread, unsigned short port ) ;

	/**
	 * 停止服务器
	 */
	void Stop( void ) ;

public:
	// 数据到来
	virtual void on_data_arrived( socket_t *sock, const void* data, int len);
	// 是否断开连接
	virtual void on_dis_connection( socket_t *sock ){};
	// 新连接到来
	virtual void on_new_connection( socket_t *sock, const char* ip, int port){};
	// 分包处理
	virtual struct packet * get_kfifo_packet( DataBuffer *fifo ) ;
	// 释放数据包
	virtual void free_kfifo_packet( struct packet *packet ) {
		free_packet( packet ) ;
	}

private:
	// 是否已启动服务
	bool 				  _initalized ;
	// 处理数据容器
	IHTTPServerContainer *_pContainer ;
};



#endif /* HTTPSERVER_H_ */
