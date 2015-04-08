/*
 * iplist.h
 *
 *  Created on: 2012-8-3
 *      Author: humingqing
 *  ip 禁用列表，直接从IP段来禁用
 */

#ifndef __IPLIST_H__
#define __IPLIST_H__

#include <list>
#include <Mutex.h>
// 黑名单使用加载双缓存处理
#define BACK_IPNUM   2

class CIpList
{
	struct IpInfo
	{
		int  _len ;
		char _szip[128] ;
	};
	typedef std::list<IpInfo>  IpList;
public:
	CIpList() ;
	~CIpList() ;

	// 加载IP的黑名单
	bool LoadIps( const char *filename ) ;
	// 检测IP是否在黑名单中
	bool Check( const char *ip ) ;

private:
	// 当前使用的索引
	int		 _index ;
	// 当前元素的个数
	int      _size ;
	// IP的地址列表
	IpList   _ips[BACK_IPNUM] ;
	// 同步操作锁
	share::Mutex _mutex ;
};


#endif /* IPLIST_H_ */
