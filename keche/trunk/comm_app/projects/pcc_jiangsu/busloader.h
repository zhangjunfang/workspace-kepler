/*
 * businfoloader.h
 *
 *  Created on: 2012-3-19
 *      Author: think
 */

#ifndef __BUSLOADER_H__
#define __BUSLOADER_H__

#include <string>
#include <vector>
#include <Thread.h>

// 系统环境对象
class ISystemEnv ;
// 加载车辆静态信息对象
class BusLoader : public share::Runnable
{
public:
	BusLoader() ;
	~BusLoader() ;

	// 初始化系统
	bool Init( ISystemEnv *pEnv ) ;
	// 启动线程
	bool Start( void ) ;
	// 停止线程
	void Stop( void ) ;
	// 线程执行对象
	void run( void *param ) ;

private:
	// 车辆静态文件
	void loadbusfile( void ) ;

private:
	// 线程执行对象
	share::ThreadManager  _thread ;
	// 系统环境对象
	ISystemEnv *		  _pEnv ;
	// 是否初始化处理
	bool 				  _inited ;
	// 需要读取的静态数据文件
	std::string			  _sbusfile;
	// 文件号索引ID值
	int 				  _index ;
};


#endif /* BUSINFOLOADER_H_ */
