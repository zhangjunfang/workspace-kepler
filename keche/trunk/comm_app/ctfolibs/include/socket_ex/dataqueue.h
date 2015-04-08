/*
 * dataqueue.h
 *
 *  Created on: 2011-11-16
 *      Author: humingqing
 *  数据队列模板类，主要配合数据线程对象使用，实现通用的链表数据PUSH和POP操作，所以使用模板中必需有_next指针
 */

#ifndef __DATAQUEUE_H__
#define __DATAQUEUE_H__

#include <queuethread.h>

// 数据队列处理对象
template< typename T>
class CDataQueue : public IPackQueue
{
public:
	CDataQueue( int maxsize = -1 ):_max(maxsize){
		_head = _tail = NULL ;
		_size = 0 ;
	}
	~CDataQueue() {
		Clear() ;
	}

	// 存放数据
	bool Push( void *data ) {
		T *pack = ( T *)data ;
		// 如果大于队列的最大长度就直接报错误
		if ( _size > _max && _max > 0 ) {
			return false ;
		}

		// 处理链表，如果为头部为空
		if ( _head == NULL ) {
			_head = _tail = pack  ;
		} else {  // 如果中间节点
			_tail->_next = pack ;
			_tail        = pack ;
		}
		_tail->_next = NULL ;

		++ _size ;

		return true;
	}

	// 弹出数据
	void * Pop( void ) {
		// 如果为空则直接返回了
		if ( _size == 0 || _head == NULL ) {
			return NULL ;
		}

		// 从队列中取数据
		T *pack = _head ;
		if ( _head == _tail ) {
			_head = _tail = NULL ;
		} else {
			_head = _head->_next ;
		}
		-- _size ;
		return (void*) pack ;
	}

	// 释放数据
	void  Free( void *packet ) {
		if ( packet == NULL )
			return ;
		delete (T*)packet ;
	}
	// 取得队列的长度
	int Size( void ) { return _size; }

protected:
	// 清理数据没有回收的内存
	void Clear() {
		if ( _size == 0 || _head == NULL ) {
			return ;
		}

		// 处理接收未处理的数据
		T *pre, *p = _head ;
		while( p != NULL ) {
			pre = p ;
			p   = p->_next ;
			delete pre ;
		}
		_head = NULL ;
		_tail = NULL ;
		_size = 0 ;
	}

protected:
	// 队列的最大长度
	int 			 _max ;
	// 记录放入数据包的个数
	int 	 		 _size ;
	// 指向数据头的指针
	T 	   		   * _head ;
	// 指向数据尾的指针
	T 	  	       * _tail ;
};


#endif /* DATAQUEUE_H_ */
