/**
 * author: humingqing
 * date:   2011-09-09
 * memo:   会话管理对象，这里是简单处理会话超时添加和移除，在操作会话时，
 * 	可以设制回调对象接收处理通知，当新增和删除时都是会通知外界处理，
 * 	不过通知是在加锁下面进行，所以在外界的回调对象中不能再调用SESSION对象的方法，否则会进入死锁的状态
 * 	<2012-05-04> 对于超时检测，这里实现了一个简单时间排序链表，时间最早就放到队列最前面，这样检测超时只需要检测前面元素是否超时，如果没有就不需要再继续检查
 */
#ifndef __SESSION_H__
#define __SESSION_H__

#include <time.h>
#include <map>
#include <string>
#include <Mutex.h>
#include <TQueue.h>
using namespace std ;

#define SESSION_CHECK_SPAN  30

// 会话变化通知对象
#define SESSION_ADDED    0x01  // 添加会话
#define SESSION_REMOVE   0x02  // 删除会话

class ISessionNotify
{
public:
	// 会话新增删除通知对象
	virtual ~ISessionNotify() {}
	// 添加数据通知
	virtual void NotifyChange( const char *key, const char *val , const int op ) = 0 ;
};

class CSessionMgr
{
protected:
	struct _Session
	{
		time_t    _time ;
		string    _key ;
		string    _value ;
		_Session *_next, *_pre ;
	};
public:
	CSessionMgr( bool timeout = false  ) ;
	~CSessionMgr() ;
	// 添加会话
	void AddSession( const string &key, const string &val ) ;
	// 取得会话是否更新时间
	bool GetSession( const string &key, string &val , bool update = true ) ;
	// 移除会话
	void RemoveSession( const string &key ) ;
	// 检测超时处理
	void CheckTimeOut( int timeout ) ;
	// 设置变回调对象
	void SetNotify( ISessionNotify *pNotify ) { _notfiy = pNotify ; }
	// 取得当前会话数
	int  GetSize( void ) ;
	// 回收内存
	void RecycleAll( void ) ;

private:
	// 移除数据
	void RemoveValue( _Session *p , bool clean ) ;
	// 添加节点数据
	void AddValue( _Session *p ) ;
	// 移除索引
	void RemoveIndex( const string &key ) ;

protected:
	share::Mutex  	   _mutex ;
	// 会话管理对象
	typedef map<string,_Session*>  CMapSession;
	CMapSession   	   _mapSession ;
	// 最后一次检测时间
	time_t		  	   _last_check ;
	// 是否需要超时处理
	bool 		  	   _btimeout ;
	// 会话通知对象
	ISessionNotify *   _notfiy ;
	// 队列管理对象
	TQueue<_Session>   _queue ;
};

#endif
