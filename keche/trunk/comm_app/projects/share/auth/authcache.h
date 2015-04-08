/*
 * authcache.h
 *
 *  Created on: 2012-5-14
 *      Author: humingqing
 *
 *  memo: 前置机鉴权缓存，主要对前置机数据进行持久化处理，这样同样构建本地缓存
 */

#ifndef __AUTHCACHE_H__
#define __AUTHCACHE_H__

#include <map>
#include <string>
#include <qstring.h>
#include <Mutex.h>
#include <TQueue.h>

#define AUTH_ERR_SUCCESS 	0   // 返回成功
#define AUTH_ERR_FAILED 	-1  // 返回失败
#define AUTH_ERR_TIMEOUT    -2  // 返回超时

class CAuthCache
{
	struct _stAuth
	{
		char phone[12];
		char oem[8] ;
		char authcode[20] ;
		time_t time ;
		_stAuth *_next ;
		_stAuth *_pre ;
	};
	typedef std::map<std::string,_stAuth *>  CMapAuth ;
public:
	CAuthCache() ;
	~CAuthCache() ;
	// 加载鉴权注册文件
	bool LoadFile( const char *filename ) ;
	// 处理车机鉴权
	int  TermAuth( const char *phone, const char *auth, CQString &ome , time_t n = 0 ) ;
	// 添加授权信息
	bool AddAuth( const char *oem, const char *phone, const char *authcode ) ;
	// 移除数据
	void Remove( const char *phone ) ;
	// 从缓存中取得数据
	bool GetCache( const char *phone, CQString &ome ) ;
	// 定时缓存序列化处理
	void Check( int timeout ) ;

private:
	// 清理数据
	void Clear( void ) ;
	// 序列化对象
	bool Serialize( void ) ;
	// 反序列化对象
	bool UnSerialize( void ) ;

private:
	// 同步处理锁操作
	share::Mutex    _mutex ;
	// MAP映射处理
	CMapAuth 	    _mpAuth ;
	// 序列化的文件名
	CQString        _filename ;
	// 鉴权数据队列
	TQueue<_stAuth> _queue ;
	// 最后一次保存的时间
	time_t 		    _last ;
	// 变更数据的个数
	int 			_change ;
};


#endif /* AUTHCACHE_H_ */
