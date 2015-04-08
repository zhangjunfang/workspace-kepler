/*
 * whitelist.h
 *
 *  Created on: 2012-5-18
 *      Author: humingqing
 *  车辆数据的白名单处理
 */

#ifndef __WHITELIST_H__
#define __WHITELIST_H__

#include <set>
#include <map>
#include <string>
#include <Mutex.h>

class CWhiteList
{
	typedef std::set<std::string>  SetString ;
	struct _WhiteIds
	{
		SetString  _lst;
	};
	typedef std::map<int,_WhiteIds*>  CMacList;
public:
	CWhiteList() ;
	~CWhiteList() ;
	// 加载文件
	bool LoadList( const char *filename ) ;
	// 是否在白名单内
	bool OnWhite( int id, const char *key ) ;

private:
	// 清理数据
	void Clear() ;

private:
	// 处同步锁操作
	share::Mutex _mutex ;
	// 白名单列表
	CMacList 	 _macList ;
};


#endif /* WHITELIST_H_ */
