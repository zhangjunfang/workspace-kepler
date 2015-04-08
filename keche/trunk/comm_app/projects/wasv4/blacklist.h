/*
 * blacklist.h
 *
 *  Created on: 2012-7-23
 *      Author: humingqing
 *  memo: 前置机黑名单的处理
 */

#ifndef __BLACKLIST_H__
#define __BLACKLIST_H__

#include <Mutex.h>
#include <set>
#include <string>

// 黑名单使用加载双缓存处理
#define BACK_BLACKNUM   2

// 黑名单管理对象
class CBlackList
{
	typedef std::set<std::string>  SetString;
public:
	CBlackList() ;
	~CBlackList() ;

	// 加载黑名单用户
	bool LoadBlack( const char *filename ) ;
	// 判断手机号是否在黑名单中
	bool OnBlack( const char *phone ) ;
private:
	// 当前使用的索引
	int			 	_index ;
	// 双数据区切换管理
	SetString    	_setBlack[BACK_BLACKNUM] ;
	// 数据管理锁对象
	share::Mutex    _mutex ;
	// 最大数据
	int 			_size ;
};
#endif /* BLACKLIST_H_ */
