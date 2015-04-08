/*
 * datastat.h
 *
 *  Created on: 2012-9-25
 *      Author: humingqing
 *  Memo: 数据流量统计模块
 */

#ifndef __DATASTAT_H__
#define __DATASTAT_H__

#include <map>
#include <string>
#include <Mutex.h>

#define DF_KB  1024
#define DF_MB  1024*1024

class CStat
{
public:
	CStat( int span = 30 ) ;
	~CStat() {}
	// 简单流量统计
	void AddFlux( int n ) ;
	// 取得流量
	void GetFlux( float &count, float &speed ) ;
	// 是否很长时间没有发送数据
	bool Check( int timeout ) ;

private:
	// 同步锁
	share::Mutex    	 _mutex ;
	// 最后一次时间
	time_t   			 _last ;
	// 最后一次记数的时间
	time_t				 _atime ;
	// 数据量计数
	unsigned int 	 	 _count ;
	// 数据流量
	unsigned int 		 _len ;
	// 最后一次的流量
	float 				 _flux ;
	// 取得当前平均个数
	float 				 _nflux ;
	// 平均时间间隔
	unsigned int		 _span ;
};

// 数据流量统计对象
class CDataStat
{
	typedef std::map<int,CStat*> CMapStat ;
public:
	CDataStat() {};
	~CDataStat(){ Clear() ;};
	// 添加流速统计
	void AddFlux( int id, int len ) ;
	// 取得单个流速统计
	void GetFlux( int id, float &count, float &speed ) ;
	// 取得流速字符串
	const std::string GetFlux( void ) ;
	// 移除统计
	void Remove( int id ) ;

private:
	// 清理所有数据
	void Clear( void ) ;

private:
	// 处理同步操作的锁
	share::Mutex _mutex ;
	// 数据统计对象
	CMapStat     _mpstat ;
};

#endif /* DATASTAT_H_ */
