/*
 * fileserver.h
 *
 *  Created on: 2012-09-17
 *      Author: humingqing
 *  数据推送服务
 */

#ifndef __FILESERVER_H__
#define __FILESERVER_H__

#include <interface.h>
#include <Thread.h>
#include <idatapool.h>

class CSynServer : public ISynServer, public share::Runnable
{
public:
	CSynServer() ;
	~CSynServer() ;

	// 通过全局管理指针来处理
	virtual bool Init( ISystemEnv *pEnv ) ;
	// 开始启动服务器
	virtual bool Start( void ) ;
	// STOP方法
	virtual void Stop( void ) ;
	// 线程运行对象
	virtual void run( void *param )  ;

private:
	// 同步数据操作
	void SynData( void ) ;

private:
	// 环境对象指针
	ISystemEnv	 		*_pEnv ;
	// 线程执行对象
	share::ThreadManager _thpool;
	// 数据连接对象
	IDataPool 		    *_dbpool;
	// 数据连接串
	char 				 _szdb[1024];
	// 是否初始化了
	bool 				 _inited ;
	// 同步方式
	unsigned int 		 _synmode ;
	// 同步时间间隔
	int 		 		 _syntime ;
};


#endif /* PUSHSERVER_H_ */
