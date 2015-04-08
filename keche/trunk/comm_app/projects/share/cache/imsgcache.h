/*
 * imsgcache.h
 *
 *  Created on: 2012-12-12
 *      Author: humingqing
 */

#ifndef __IMSGCACHE_H_
#define __IMSGCACHE_H_

// 消息缓存对象
class IMsgCache
{
public:
	virtual ~IMsgCache() {} ;
	// 添加数据
	virtual bool AddData( const char *key, const char *buf, const int len ) = 0 ;
	// 取得数据
	virtual char *GetData( const char *key, int &len , bool erase=true ) = 0  ;
	// 释放数据
	virtual void FreeData( char *data ) = 0 ;
	// 处理超时的数据
	virtual void CheckData( int timeout ) = 0 ;
	// 移除数据
	virtual bool Remove( const char *key ) = 0 ;
};


#endif /* IMSGCACHE_H_ */
