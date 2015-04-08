/*
 * waymgr.h
 *
 *  Created on: 2012-5-31
 *      Author: think
 */

#ifndef __WAYMGR_H__
#define __WAYMGR_H__

#include <iplugin.h>
#include <map>
#include <vector>
#include <string>
#include <Mutex.h>
#include <Monitor.h>
#include <Thread.h>
#include <Ref.h>

// 插件管理对象
class CPlugWay : public share::Ref
{
public:
	CPlugWay( const char *path ) ;
	~CPlugWay() ;

	// 需要初始化对象
	bool Init( IPlugin *handler, const char *url, int sendthread, int recvthread, int queuesize ) ;
	// 初始化插件通道
	bool Start( void ) ;
	// 停止插件通道
	bool Stop( void ) ;
	// 处理透传的数据
	bool Process( unsigned int fd, const char *data, int len , unsigned int cmd , const char *id ) ;

private:
	// 加载通道
	bool LoadWay( const char *path ) ;
	// 释放通道
	void FreeWay( void ) ;

private:
	// 动态库加载句柄
	void	   *_hModule ;
	// 插件通道
	IPlugWay   *_pWay ;
};

// 动态插件的管理对象
class CWayMgr : public share::Runnable
{
	struct _PlugInfo
	{
		unsigned int 			  _id ;  // 默认的ID号
		std::vector<unsigned int> _vec;  // 调用的透传指令
		std::string 			  _path; // 动态库的路径
		std::string 			  _url;  // 调用HTTP服务地址
		int 					  _nsend; // 发送线程个数
		int 					  _nrecv; // 接收线程个数
		int 					  _nqueue;// 调用HTTP服务的队列大小

		_PlugInfo() {
			_id     = 0 ;
			_nsend  = _nrecv = 1 ;
			_nqueue = 1000 ;
		}
	};
	// 通道插件队列
	typedef std::map<std::string,_PlugInfo>     CMapN2Ids;
	typedef std::map<unsigned int,unsigned int> CPlugCmdMap ;
	typedef std::vector<CPlugWay*>			    CPlugWayVec ;
	typedef std::map<std::string,unsigned int>  CPlugNameMap ;
public:
	CWayMgr( IPlugin *handler ) ;
	~CWayMgr() ;

	// 初始化对象
	bool Init( void ) ;
	// 启动程序
	bool Start( void ) ;
	// 停止程序
	bool Stop( void ) ;
	// 根据命令字取得插件
	CPlugWay * CheckOut( unsigned int cmd , bool flag = false ) ;
	// 将插件签入
	void CheckIn( CPlugWay *plug ) ;

public:
	// 线程执行对象
	void run( void *param ) ;

private:
	// 停止所有通道
	void StopWay( void ) ;
	// 清理所有数据
	void Clear( void ) ;
	// 检测配置文件
	void CheckConf( void ) ;
	// 加载文件
	bool LoadFile( const char *file, CMapN2Ids &mpids ) ;

private:
	// 操作数据锁
	share::Mutex     	 _mutex ;
	// 信号对象
	share::Monitor		 _monitor ;
	// 线程管理对象
	share::ThreadManager _thread ;
	// 处理的数据的Handler
	IPlugin 			*_pHandler ;
	// 动态库的队列
	CPlugWayVec			 _wayvec ;
	// 插件名字到ID
	CPlugNameMap  	 	 _wayn2id ;
	// 命令字到ID
	CPlugCmdMap			 _wayc2id ;
	// 是否正常初始化
	bool 				 _inited ;
	// DLL加载配置文件路径
	std::string 		 _dllconf ;
	// DLL根目录
	std::string 		 _dllroot ;
};


#endif /* WAYMGR_H_ */
