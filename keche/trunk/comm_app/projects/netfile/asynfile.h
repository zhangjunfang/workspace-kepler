/*
 * fileclient.h
 *
 *  Created on: 2012-9-18
 *      Author: humingqing
 *
 *  Memo: 文件读写客户端程序
 */

#ifndef __FILECLIENT_H_
#define __FILECLIENT_H_

#include <inetfile.h>
#include <NetHandle.h>
#include <protocol.h>
#include <interpacker.h>

class CWaitObjMgr ;
class CAsynFile : public CNetHandle, public INetFile
{
public:
	CAsynFile() ;
	~CAsynFile() ;

	// 打开连接
	int open( const char *ip, int port , const char *user, const char *pwd ) ;
	// 写入文件数据
	int write( const char *path , const char *data, int len ) ;
	// 读取文件数据
	int read( const char *path, DataBuffer &duf ) ;
	// 关闭连接
	void close( void ) ;

protected:
	// 发送数据
	bool mysend( void *data, int len ) ;
	// 构建数据头部
	void buildheader( DataBuffer &buf, unsigned int seq, unsigned short cmd, unsigned int len ) ;
	// 处理数据到来事件
	void on_data_arrived( socket_t *sock, const void* data, int len ) ;
	// 断开连接
	void on_dis_connection( socket_t *sock ) ;
	// 新连接到来
	void on_new_connection( socket_t *sock, const char* ip, int port){}

private:
	// 连接的FD值
	socket_t	   *_sock ;
	// 数据包拆包对象
	CBigSpliter  	_packspliter ;
	// 是否连接成功
	bool 			_connected ;
	// 等待数据对象
	CWaitObjMgr    *_waitmgr ;
};

#endif /* FILECLIENT_H_ */
