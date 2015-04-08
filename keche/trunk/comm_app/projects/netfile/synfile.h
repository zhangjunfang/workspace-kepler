/*
 * netfile.h
 *
 *  Created on: 2012-9-18
 *      Author: humingqing
 *  Memo: 网络文件对象
 */

#ifndef __NETFILE_H__
#define __NETFILE_H__

#include <inetfile.h>
#include <Mutex.h>

class CSynFile : public INetFile
{
public:
	CSynFile() ;
	~CSynFile() ;

	// 打开连接
	int open( const char *ip, int port , const char *user, const char *pwd ) ;
	// 写入文件数据
	int write( const char *path , const char *data, int len ) ;
	// 读取文件数据
	int read( const char *path, DataBuffer &duf ) ;
	// 关闭连接
	void close( void ) ;

private:
	// 构建数据头部
	void buildheader( DataBuffer &buf, unsigned short cmd, unsigned int len ) ;
	// 连接服务器
	int  myconnect( const char *ip, unsigned short port ) ;
	// 关闭连接
	void myclose( void ) ;
	// 读取数据
	int  myread( DataBuffer *buf ) ;
	// 写数据
	bool mywrite( const char *buf, int len ) ;

private:
	// 连接的FD值
	int 		    _fd ;
	// 序号管理对象
	unsigned int    _seq ;
	// 同步操作锁
	share::Mutex	_mutex ;
};



#endif /* NETFILE_H_ */
