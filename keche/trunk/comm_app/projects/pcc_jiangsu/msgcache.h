/**
 * Author: humingqing
 * Date:   2011-07-24
 * memo:   数据缓存对象
 */
#ifndef __MSG_CACHE_H__
#define __MSG_CACHE_H__

#include "interface.h"
#include <map>
#include <string>
#include <time.h>
#include <Mutex.h>

// 消息缓存模块
class MsgCache : public IMsgCache
{
	// 数据缓存结构体
	struct _msg_data
	{
		int   len ;   // 数据长度
		char *buf ;   // 数据的内容
		time_t ent ;  // 存放数据的时间
	};
	// 消息数据缓存
	typedef std::multimap<std::string,_msg_data*>  MapMsgData ;
	// 消息的时间索引
	typedef std::multimap<time_t,std::string> MapMsgIndex;
public:
	MsgCache() ;
	~MsgCache() ;

	// 添加数据
	bool AddData( const char *key, const char *buf, const int len ) ;
	// 取得数据
	char *GetData( const char *key, int &len ) ;
	// 释放数据
	void FreeData( char *data ) ;
	// 处理超时的数据
	void CheckData( int timeout ) ;
	// 移除数据
	bool Remove( const char *key ) ;

private:
	// 清除数据
	bool RemoveData( const char *key ) ;
	// 清空所有数据
	void ClearAll( void ) ;

private:
	// 处理多线程使用
	share::Mutex  _mutex ;
	// 数据缓存对象
	MapMsgData    _map_data ;
	// 时间索引
	MapMsgIndex   _map_index ;
};

#endif
