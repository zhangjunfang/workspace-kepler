/*
 * msgqueue.cpp
 *
 *  Created on: 2012-6-1
 *      Author: humingqing
 *  消息等待队列
 */

#include "msgqueue.h"
#include <comlog.h>

CMsgQueue::CMsgQueue( IMsgCaller *caller ) : _caller(caller)
{

}

CMsgQueue::~CMsgQueue()
{
	Clear() ;
}

// 添加对象
bool CMsgQueue::AddObj( unsigned int seq, unsigned int fd, unsigned int cmd, const char *id, IPacket *msg )
{
	share::Guard guard( _mutex ) ;

	_MsgObj *obj = new _MsgObj ;
	obj->_seq = seq ;
	obj->_fd  = fd ;
	obj->_cmd = cmd ;
	obj->_id  = id ;
	obj->_msg = msg ;
	obj->_now = time(NULL) ;

	msg->AddRef() ;

	_queue.push( obj ) ;
	_index.insert( make_pair(seq, obj ) ) ;

	return true ;
}

// 取得对应对象
CMsgQueue::_MsgObj * CMsgQueue::GetObj( unsigned int seq )
{
	share::Guard guard( _mutex ) ;
	CMapObj::iterator it = _index.find( seq ) ;
	if ( it == _index.end() )
		return NULL ;

	_MsgObj *obj = it->second ;
	_index.erase( it ) ;

	return _queue.erase( obj ) ;
}

// 释放对象
void CMsgQueue::FreeObj( CMsgQueue::_MsgObj *obj )
{
	if ( obj == NULL )
		return ;

	obj->_msg->Release() ;
	delete obj ;
}

// 删除对象
bool CMsgQueue::Remove( unsigned int seq )
{
	_MsgObj *obj = GetObj( seq ) ;
	if ( obj == NULL )
		return false ;

	FreeObj( obj ) ;

	return true ;
}

// 移除对应的值
void CMsgQueue::RemoveValue( unsigned int seq , bool callback )
{
	CMapObj::iterator it = _index.find( seq ) ;
	if ( it == _index.end() )
		return ;
	_MsgObj *obj = it->second ;
	_index.erase( it ) ;
	_queue.erase( obj ) ;

	// 如果需要回调处理
	if ( callback ) {
		// 提交外部处理
		_caller->OnTimeOut( obj->_seq, obj->_fd, obj->_cmd, obj->_id.c_str(), obj->_msg ) ;
	}
	FreeObj( obj ) ;
}


//  检测超时的对象
void CMsgQueue::Check( int timeout )
{
	share::Guard guard( _mutex ) ;

	if ( _queue.size() == 0 )
		return ;

	time_t now = time(NULL) ;

	_MsgObj *t = NULL ;
	_MsgObj *p = _queue.begin() ;
	while( p != NULL ) {
		t = p ;
		p = p->_next ;
		if ( now - t->_now < timeout )
			break ;
		// 移除对象
		RemoveValue( t->_seq, true ) ;
	}
}

// 清除数据
void CMsgQueue::Clear( void )
{
	share::Guard guard( _mutex ) ;

	int size = 0 ;
	_MsgObj *p = _queue.move(size) ;
	if ( size == 0 )
		return ;

	while( p != NULL ) {
		p = p->_next ;
		FreeObj( p->_pre ) ;
	}
	_index.clear();
}



