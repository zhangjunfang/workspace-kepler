/*
 * icache.h
 *
 *  Created on: 2012-4-28
 *      Author: humingqing
 *  缓存处理
 */

#ifndef __ICACHE_H_
#define __ICACHE_H_

#include <vector>
#include <string>

class IContext ;

// 缓存对接处理接口
class IRedisObj
{
public:
	virtual ~IRedisObj() {}
	// 从缓存对象中取得接定的值
	virtual bool GetValue( const char *key , std::string &val ) = 0 ;
	// 设置缓存对象中指定的值
	virtual bool SetValue( const char *key, const char *val ) = 0 ;
	// 移除缓存对象中的值
	virtual bool Remove( const char *key ) = 0 ;
	// 取得缓存对象数组对象
	virtual int  GetList( const char *key, std::vector<std::string> &vec ) = 0 ;
	// 设置缓存对象数组值
	virtual int  SetList( const char *key, std::vector<std::string> &vec ) = 0 ;
	// 从队列中弹出值
	virtual bool PopValue( const char *key , std::string &val ) = 0 ;
	// 将数据放到队列尾部
	virtual bool PushValue( const char *key, const char *val ) = 0 ;
	// 模糊取得KEY的值
	virtual int  GetKeys( const char *key, std::vector<std::string> &vec ) = 0 ;
	// 删除数组中某个元素
	virtual bool LRem( const char *key , const char *val ) = 0 ;
	// 使用Hash SET集来处理数据
	virtual bool HSet( const char *areaid, const char *key, const char *val ) =  0 ;
	// 取得Hash SET集的数据
	virtual bool HGet( const char *areaid, const char *key, std::string &val ) = 0 ;
	// 取得Hash SET中的所有集合
	virtual int  HKeys( const char *areaid,  std::vector<std::string> &vec ) = 0 ;
	// 删除HASH中的某值
	virtual bool HDel( const char *areaid, const char *key ) = 0 ;
	// 设置Set集中的元素
	virtual bool SAdd( const char *key, const char *val ) = 0 ;
	// 删除SET集中的某个元素
	virtual bool SRem( const char *key , const char *val ) =  0 ;
	// 弹出SET集中某个元素
	virtual bool SPop( const char *key, std::string &val ) = 0 ;
	// 取得当前值SET集的所有成员
	virtual int  SMembers( const char *key, std::vector<std::string> &vec ) = 0 ;
};

// 缓存对象接口定义
class IRedisCache : public IRedisObj
{
public:
	virtual ~IRedisCache() {} ;
	// 初始化缓存对象
	virtual bool Init( IContext *pEnv ) = 0 ;
	// 开始缓存对象工作
	virtual bool Start( void ) = 0 ;
	// 停止缓存对象工作
	virtual bool Stop( void ) = 0 ;
	// 取得RedisObj操作对象
	virtual IRedisObj *GetObj( void ) =  0 ;
	// 放回取得RedisObj对象
	virtual void PutObj( IRedisObj *obj ) = 0 ;
};

#endif /* ICACHE_H_ */
