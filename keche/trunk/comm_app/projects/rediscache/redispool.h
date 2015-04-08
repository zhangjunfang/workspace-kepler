/*
 * redispool.h
 *
 *  Created on: 2012-4-18
 *      Author: humingqing
 *
 *  Redis缓存对象连接池，主要实现对于大量请求并发时，对于一次开辟的连接可以复用的原则，
 *  如果一定时间内没有使用由定时线程回收，对于Redis读写操作，通过主从来实现读写分离来处理，如果没有从就直接从主对象读取
 */

#ifndef __REDISPOOL_H__
#define __REDISPOOL_H__

#include <icache.h>
#include <icontext.h>
#include <vector>
#include <string>
#include <Mutex.h>
#include <Thread.h>
#include <time.h>
#include <TQueue.h>

using std::string;

// 这里定义对象存活时间为60秒
#define OBJ_LIVE_TIME  120

struct redisContext ;
// Redis对象
class RedisObj : public IRedisObj
{
	typedef std::vector<std::string> VecString ;
public:
	// 缓存对象头尾指会
	RedisObj *_next ;
	RedisObj *_pre  ;

public:
	RedisObj() ;
	~RedisObj() ;
	// 初始化对象
	bool InitObj( const char *masterip, unsigned short masterport, const char *slaverip=NULL, unsigned short slaverport= 0) ;
	// 取得数据对象
	bool GetValue( const char *key , std::string &val ) ;
	// 设置缓存对象中指定的值
	bool SetValue( const char *key, const char *val ) ;
	// 移除缓存对象中的值
	bool Remove( const char *key ) ;
	// 取得缓存对象数组对象
	int GetList( const char *key, VecString &vec ) ;
	// 设置缓存对象数组值
	int SetList( const char *key, VecString &vec ) ;
	// 从队列中弹出值
	bool PopValue( const char *key , std::string &val ) ;
	// 将数据放到队列尾部
	bool PushValue( const char *key, const char *val ) ;
	// 模糊取得所有KEY的值
	int  GetKeys( const char *key, VecString &vec ) ;
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
	// 释放所有对象
	void FreeObj( void ) ;
	// 检测是否存活状态
	bool Ping( void ) ;
	// 设置密码
	void auth(const char *password);
	// 设置数据库编号
	void select(int number);
	// 检测对象状态
	bool stauts();

private:
	// 连接服务器
	redisContext * Connect( const char *ip, int port ) ;
	// 取得数组类型的值
	int  GetArray( const char *cmd, VecString &vec ) ;
	// 取得字符串型的值
	bool GetString( const char *cmd, std::string &val ) ;
	// 执行命行类型的值
	bool Execute( const char *cmd ) ;

private:
	// 主缓存对象
	redisContext *_ctmaster ;
	// 从缓存对象
	redisContext *_ctslaver ;
	// 最后一次检测时间
	time_t		  _lastcheck;
	// 密码
	string        _password;
	// 数据库编号
	int           _number;
};

// 双池切换处理
#define BACK_POOL_SIZE  2
// Redis连接池对象
class RedisPool : public share::Runnable
{
	typedef TQueue<RedisObj>   RedisObjList;
public:
	RedisPool() ;
	~RedisPool() ;

	// 初始化环境对象
	bool Init( IContext *pEnv ) ;
	// 开始线程
	bool Start( void ) ;
	// 停止对象
	bool Stop( void ) ;

	// 从连接池中签出对象
	RedisObj * CheckOut( void ) ;
	// 设置连接池对象
	void CheckIn(RedisObj *obj) ;

public:
	// 线程执行对象
	void run( void *param ) ;

private:
	// 解析地址是否正常
	bool PaserAddress( char *val ) ;
	// 清理所有对象
	void Clear( void ) ;

private:
	// 线程管理对象
	share::ThreadManager  _threadpool;
	// 对象池的锁操作
	share::Mutex		  _mutex ;
	// 对象池的操作
	RedisObjList		  _objpool[BACK_POOL_SIZE] ;
	// 当前使用池的序号
	int 				  _index ;
	// 初始化处理
	bool 				  _inited ;
	// 主缓存的IP地址
	std::string 		  _masterip ;
	// 主缓的端口
	unsigned short        _masterport ;
	// 从缓存的IP地址
	std::string			  _slaverip ;
	// 从缓存的端口
	unsigned short   	  _slaverport ;
	// 密码
	string                _password;
	// 编号
	int                   _number;
};


#endif /* REDISPOOL_H_ */
