/*
 * inetfile.h
 *
 *  Created on: 2012-9-18
 *      Author: humingqing
 *  Memo: 网络文件读写接口对象
 */

#ifndef __INETFILE_H__
#define __INETFILE_H__

#include <databuffer.h>

#define FILE_RET_NOCONN	   -1	 // 没有连接状态
#define FILE_RET_SUCCESS   	0  	 // 成功返回结果
#define FILE_RET_FAILED	   	1  	 // 返回失败的结果
#define FILE_RET_TIMEOUT   	2  	 // 超时返回处理
#define FILE_RET_SENDERR    3    // 发送数据失败
#define FILE_RET_READERR    4    // 读取数据错误
#define FILE_MAX_WAITTIME   10   // 最长等待的时间

// 网络文件管理接口
class INetFile
{
public:
	virtual ~INetFile() {}
	// 打开连接
	virtual int open( const char *ip, int port , const char *user, const char *pwd ) = 0 ;
	// 写入文件数据
	virtual int write( const char *path , const char *data, int len ) = 0 ;
	// 读取文件数据
	virtual int read( const char *path, DataBuffer &duf ) = 0 ;
	// 关闭连接
	virtual void close( void ) = 0 ;
};

// 网络文件管理对象
class NetFileMgr
{
public:
	enum NET_MODE{ SYN_MODE = 0, ASYN_MODE = 1 } ;
	// 取得文件读取对萌
	static INetFile * getfileobj( NET_MODE mode ) ;
	// 释放文件对象
	static void release( INetFile *p ) ;
} ;

#endif /* INETFILE_H_ */
