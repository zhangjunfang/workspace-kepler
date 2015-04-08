/**
 * Author: humingqing
 * Date:   2011-07-24
 * memo:   数据缓存对象
 */
#ifndef __MSG_CACHE_H__
#define __MSG_CACHE_H__

#include "imsgcache.h"
#include <map>
#include <string>
#include <time.h>
#include <Mutex.h>
#include <TQueue.h>

// 消息缓存模块
class MsgCache : public IMsgCache
{
	// 数据缓存结构体
	struct _msg_data
	{
		std::string key ;   // 对象的KEY
		int   		len ;   // 数据长度
		char *		buf ;   // 数据的内容
		time_t 		ent ;   // 存放数据的时间
		_msg_data *_next ;  // 后一级指针
		_msg_data *_pre ;	// 前一个指针
	};
	// 消息数据缓存
	typedef std::multimap<std::string,_msg_data*>  MapMsgData ;
public:
	MsgCache() ;
	~MsgCache() ;

	// 添加数据
	bool AddData( const char *key, const char *buf, const int len ) ;
	// 取得数据
	char *GetData( const char *key, int &len, bool erase=true ) ;
	// 释放数据
	void FreeData( char *data ) ;
	// 处理超时的数据
	void CheckData( int timeout ) ;
	// 移除数据
	bool Remove( const char *key ) ;

private:
	// 清空所有数据
	void ClearAll( void ) ;

private:
	// 处理多线程使用
	share::Mutex      _mutex ;
	// 数据缓存对象
	MapMsgData    	  _index ;
	// 时间索引
	TQueue<_msg_data> _queue;
};

#endif
