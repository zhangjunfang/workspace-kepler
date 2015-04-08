#include "Session.h"

CSessionMgr::CSessionMgr( bool timeout ) : _btimeout(timeout)
{
	_last_check   = time(NULL) ;
	_notfiy       = NULL ;
}

CSessionMgr::~CSessionMgr()
{
	RecycleAll() ;
}

// 添加会话
void CSessionMgr::AddSession( const string &key, const string &val )
{
	share::Guard guard( _mutex ) ;
	{
		_Session *p = NULL ;
		CMapSession::iterator it = _mapSession.find( key ) ;
		if ( it != _mapSession.end() ) {
			p = it->second ;
			p->_time  = time(NULL) ;
			p->_key   = key ;
			p->_value = val ;

			// 从已有队列头中移走
			if ( _btimeout ) {
				RemoveValue( p, false ) ;
			}
		} else {
			p = new _Session ;
			p->_time  = time(NULL) ;
			p->_key   = key ;
			p->_value = val ;
			// 添加到MAP中处理
			_mapSession.insert( pair<string,_Session*>( key, p ) ) ;
			// 通知为添加数据
			if( _notfiy ) {
				// 通知外部添加新数据
				_notfiy->NotifyChange( key.c_str(), val.c_str(), SESSION_ADDED ) ;
			}
		}
		// 如果需要处理超时，添加到会话对队中
		if ( _btimeout ) AddValue( p ) ;
	}
}

// 取得会话
bool CSessionMgr::GetSession( const string &key, string &val , bool update )
{
	share::Guard guard( _mutex ) ;
	{
		CMapSession::iterator it = _mapSession.find( key ) ;
		if ( it == _mapSession.end() ) {
			return false ;
		}
		_Session *p = it->second ;
		val = p->_value ;

		// 更新最后一次使用时间
		if ( _btimeout && update ) {
			p = _queue.erase( p ) ;
			p->_time = time(NULL) ;
			_queue.push( p ) ;
		}

		return true ;
	}
}

// 移除会话
void CSessionMgr::RemoveSession( const string &key )
{
	share::Guard guard( _mutex ) ;
	{
		CMapSession::iterator it = _mapSession.find(key) ;
		if ( it == _mapSession.end() ) {
			return ;
		}
		_Session *p = it->second ;
		_mapSession.erase( it ) ;
		// 通知为添加数据
		if( _notfiy ) {
			// 通知外部添加新数据
			_notfiy->NotifyChange( key.c_str(), p->_value.c_str() , SESSION_REMOVE ) ;
		}
		// 是否有超时处理
		if ( _btimeout ) {
			// 如果有超时直接移走
			RemoveValue( p , true ) ;
		} else {
			delete p ;
		}
	}
}

// 检测超时处理
void CSessionMgr::CheckTimeOut( int timeout )
{
	share::Guard guard( _mutex ) ;
	{
		if ( ! _btimeout ) {
			return  ;
		}

		time_t now = time(NULL) ;
		if ( now - _last_check < SESSION_CHECK_SPAN ) {
			return ;
		}
		_last_check = now ;

		// 计算超时
		time_t t = now - timeout ;

		_Session *tmp,*p = _queue.begin() ;
		// 只需要检测头部的数据就可以了
		while( p != NULL ) {
			// 如果操时时间大于当前最小时间就跳出
			if ( p->_time > t )
				break ;

			tmp = p ;
			// 直接指向下一个元素
			p   = _queue.next( p ) ;
			// 移除MAP索引
			RemoveIndex( tmp->_key ) ;
			// 移除链表中节点
			RemoveValue( tmp , true ) ;
		}
	}
}

// 取得当前会话数
int CSessionMgr::GetSize( void )
{
	share::Guard guard( _mutex ) ;
	return _queue.size() ;
}

// 添加节点数据
void CSessionMgr::AddValue( _Session *p )
{
	_queue.push( p ) ;
}

void CSessionMgr::RemoveIndex( const string &key )
{
	CMapSession::iterator it = _mapSession.find( key ) ;
	if ( it == _mapSession.end() ) {
		return ;
	}

	_Session *p = it->second ;
	_mapSession.erase( it ) ;

	// 通知为添加数据
	if( _notfiy ) {
		// 通知外部添加新数据
		_notfiy->NotifyChange( p->_key.c_str() , p->_value.c_str() , SESSION_REMOVE ) ;
	}
}

// 移除数据
void CSessionMgr::RemoveValue( _Session *p , bool clean )
{
	p = _queue.erase( p ) ;
	if ( clean ) delete p ;
}

// 回收内存
void CSessionMgr::RecycleAll( void )
{
	share::Guard guard( _mutex ) ;
	{
		if ( ! _btimeout ) {
			CMapSession::iterator it ;
			for ( it = _mapSession.begin(); it != _mapSession.end(); ++ it ) {
				delete it->second ;
			}
		} else {
			int size = 0 ;
			_Session *p = _queue.move( size ) ;
			if ( size > 0 ) {
				_Session *tmp ;
				while ( p != NULL ) {
					tmp = p ;
					p   = p->_next ;
					delete tmp ;
				}
			}
		}
		_mapSession.clear() ;
	}
}
