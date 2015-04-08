/*
 * rediscache.h
 *
 *  Created on: 2012-4-18
 *      Author: humingqing
 */

#ifndef __REDISCACHE_H__
#define __REDISCACHE_H__

#include <icontext.h>
#include <icache.h>
#include <string>
#include <vector>

class RedisPool;
class RedisCache :
	public IRedisCache
{
public:
	RedisCache() ;
	~RedisCache() ;
	// 初始化缓存对象
	bool Init( IContext *pEnv ) ;
	// 开始缓存对象工作
	bool Start( void ) ;
	// 停止缓存对象工作
	bool Stop( void ) ;
	// 取得RedisObj操作对象
	IRedisObj *GetObj( void ) ;
	// 放回取得RedisObj对象
	void PutObj( IRedisObj *obj ) ;

	//=============== 针对操作频率不高的缓存操作  ========================
	// 从缓存对象中取得接定的值
	bool GetValue( const char *key , std::string &val ) ;
	// 设置缓存对象中指定的值
	bool SetValue( const char *key, const char *val ) ;
	// 移除缓存对象中的值
	bool Remove( const char *key ) ;
	// 取得缓存对象数组对象
	int GetList( const char *key, std::vector<std::string> &vec ) ;
	// 设置缓存对象数组值
	int SetList( const char *key, std::vector<std::string> &vec ) ;
	// 从队列中弹出值
	bool PopValue( const char *key , std::string &val ) ;
	// 将数据放到队列尾部
	bool PushValue( const char *key, const char *val ) ;
	// 模糊取得KEY的值
	int  GetKeys( const char *key, std::vector<std::string> &vec ) ;
	// 删除数组中某个元素
	bool LRem( const char *key , const char *val ) ;
	// 使用Hash SET集来处理数据
	bool HSet( const char *areaid, const char *key, const char *val ) ;
	// 取得Hash SET集的数据
	bool HGet( const char *areaid, const char *key, std::string &val ) ;
	// 取得Hash SET中的所有集合
	int  HKeys( const char *areaid,  std::vector<std::string> &vec ) ;
	// 删除HASH中的某值
	bool HDel( const char *areaid, const char *key ) ;
	// 设置Set集中的元素
	bool SAdd( const char *key, const char *val ) ;
	// 删除SET集中的某个元素
	bool SRem( const char *key , const char *val ) ;
	// 弹出SET集中某个元素
	bool SPop( const char *key, std::string &val ) ;
	// 取得当前值SET集的所有成员
	int  SMembers( const char *key, std::vector<std::string> &vec ) ;

private:
	// 缓存对象池
	RedisPool  *_pool ;
};


#endif /* REDISCACHE_H_ */
