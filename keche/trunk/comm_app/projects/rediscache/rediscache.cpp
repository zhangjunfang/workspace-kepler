/*
 * rediscache.cpp
 *
 *  Created on: 2012-4-18
 *      Author: humingqing
 */

#include "rediscache.h"
#include "redispool.h"

RedisCache::RedisCache()
{
	_pool = new RedisPool;
}

RedisCache::~RedisCache()
{
	if ( _pool != NULL ) {
		delete _pool ;
		_pool = NULL ;
	}
}

// 初始化缓存对象
bool RedisCache::Init( IContext *pEnv )
{
	return _pool->Init( pEnv ) ;
}

// 开始缓存对象工作
bool RedisCache::Start( void )
{
	return _pool->Start() ;
}

// 停止缓存对象工作
bool RedisCache::Stop( void )
{
	return _pool->Stop() ;
}

// 取得RedisObj操作对象
IRedisObj *RedisCache::GetObj( void )
{
	return _pool->CheckOut() ;
}

// 放回取得RedisObj对象
void RedisCache::PutObj( IRedisObj *obj )
{
	_pool->CheckIn( (RedisObj*)obj ) ;
}

// 从缓存对象中取得接定的值
bool RedisCache::GetValue( const char *key , std::string &val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->GetValue( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 设置缓存对象中指定的值
bool RedisCache::SetValue( const char *key, const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->SetValue( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 移除缓存对象中的值
bool RedisCache::Remove( const char *key )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->Remove( key ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 取得缓存对象数组对象
int RedisCache::GetList( const char *key, std::vector<std::string> &vec )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	int count = obj->GetList( key, vec ) ;
	_pool->CheckIn( obj ) ;

	return count;
}

// 设置缓存对象数组值
int RedisCache::SetList( const char *key, std::vector<std::string> &vec )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return -1;
	}
	int count = obj->SetList( key, vec ) ;
	_pool->CheckIn( obj ) ;

	return count ;
}

// 从队列中弹出值
bool RedisCache::PopValue( const char *key , std::string &val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->PopValue( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 将数据放到队列尾部
bool RedisCache::PushValue( const char *key, const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->PushValue( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 模糊取得KEY的值
int RedisCache::GetKeys( const char *key, std::vector<std::string> &vec )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL )
		return -1 ;

	int count = obj->GetKeys( key, vec ) ;
	_pool->CheckIn( obj ) ;

	return count ;
}

// 删除数组中某个元素
bool RedisCache::LRem( const char *key , const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->LRem( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 使用Hash SET集来处理数据
bool RedisCache::HSet( const char *areaid, const char *key, const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->HSet( areaid, key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 取得Hash SET集的数据
bool RedisCache::HGet( const char *areaid, const char *key, std::string &val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->HGet( areaid, key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 取得Hash SET中的所有集合
int RedisCache::HKeys( const char *areaid,  std::vector<std::string> &vec )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL )
		return -1 ;

	int count = obj->HKeys( areaid, vec ) ;
	_pool->CheckIn( obj ) ;

	return count ;
}

// 删除HASH中的某值
bool RedisCache::HDel( const char *areaid, const char *key )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->HDel( areaid, key ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 设置Set集中的元素
bool RedisCache::SAdd( const char *key, const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->SAdd( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 删除SET集中的某个元素
bool RedisCache::SRem( const char *key , const char *val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->SRem( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 弹出SET集中某个元素
bool RedisCache::SPop( const char *key, std::string &val )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL ) {
		return false ;
	}
	bool find = obj->SPop( key, val ) ;
	_pool->CheckIn( obj ) ;

	return find ;
}

// 取得当前值SET集的所有成员
int RedisCache::SMembers( const char *key, std::vector<std::string> &vec )
{
	RedisObj *obj = _pool->CheckOut() ;
	if ( obj == NULL )
		return -1 ;

	int count = obj->SMembers( key, vec ) ;
	_pool->CheckIn( obj ) ;

	return count ;
}

