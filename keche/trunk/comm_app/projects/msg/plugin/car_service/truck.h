/*
 * truck.h
 *
 *  Created on: 2012-5-31
 *      Author: think
 */
#ifndef __TRUCK_H__
#define __TRUCK_H__

#include <iplugin.h>
#include "srvcaller.h"

namespace CarService{
	// 车后服务对象
	class CCarService:public IPlugWay
	{
	public:
		CCarService() ;
		~CCarService() ;

		// 需要初始化对象
		bool Init( IPlugin *plug , const char *url, int sendthread, int recvthread, int queuesize ) ;
		// 初始化插件通道
		bool Start( void ) ;
		// 停止插件通道
		bool Stop( void ) ;
		// 处理透传的数据
		bool Process( unsigned int fd, const char *data, int len , unsigned int cmd , const char *id ) ;

	private:
		// 回调对象
		IPlugin    * _pCaller ;
		// 业务调用
		CSrvCaller * _srvCaller ;
		// 解包工厂
		CPackFactory *_packfactory;
		// 车后服务数据解包对象
		CCarServiceUnPackMgr _unpacker;
	};
};
#endif /* TRUCK_H_ */
