/*
 * datasaver.h
 *
 *  Created on: 2012-5-30
 *      Author: humingqing
 *  数据存储分析的对象
 */

#ifndef __DATASAVER_H__
#define __DATASAVER_H__

#include <interface.h>
#include <Thread.h>

class IDataPool ;
class Inter2SaveConvert ;

#define DB_THREAD      3 

class CDataSaver : public IMsgHandler,  public share::Runnable
{
public:
	CDataSaver() ;
	~CDataSaver() ;

	// 初始化
	bool Init( ISystemEnv * pEnv ) ;
	// 启动服务
	bool Start( void ) ;
	// 停止服务
	bool Stop( void ) ;
	// 处理数据
	bool Process( InterData &data , User &user ) ;
	// 线程执行对象
	virtual void run( void *param );
	
private:
	// 环境对象指针
	ISystemEnv 		  *_pEnv ;
	// 转换数据对象
	Inter2SaveConvert *_inter_save_convert;
	// 数据库连接池
	IDataPool 		  *_save_pool;
	// 是否开启存储部件
	bool 			   	  _enable_save ;
	//处理缓冲池中数据的线程。
	share::ThreadManager  _db_thread ;
};


#endif /* DATASAVE_H_ */
