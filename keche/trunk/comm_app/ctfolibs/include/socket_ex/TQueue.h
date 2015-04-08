/*
 * tqueue.h
 *
 *  Created on: 2012-11-24
 *      Author: humingqing
 *  通用队列操作锁处理
 */

#ifndef __TQUEUE_H__
#define __TQUEUE_H__

template<typename T>
class TQueue
{
public:
	TQueue()
	{
		_head = _tail = NULL;
		_size = 0 ;
	}

	~TQueue() { clear() ;}

	// 放入队列管理
	void push( T *o )
	{
		o->_next = o->_pre = NULL ;

		if ( _head == NULL ) {
			_head = _tail = o ;
		} else {
			_tail->_next = o ;
			o->_pre      = _tail  ;
			_tail        = o ;
		}
		_size = _size + 1 ;
	}

	// 移出队列
	T * pop( void )
	{
		if ( _size == 0 )
			return NULL ;

		T *p = _head ;
		if ( _head == _tail ) {
			_head = _tail = NULL ;
		} else {
			_head = _head->_next ;
			_head->_pre = NULL ;
		}
		_size = _size -1 ;

		p->_next = p->_pre = NULL ;

		return p ;
	}

	// 全部移走
	T * move( int &size )
	{
		T *p = _head ;
		size = _size;
		_head = _tail = NULL ;
		_size = 0 ;
		return p ;
	}

	// 从队列中移走
	T * erase( T *o )
	{
		if ( o == _head ) {
			if ( _head == _tail ) {
				_head = _tail = NULL ;
			} else {
				_head = o->_next ;
				_head->_pre = NULL ;
			}
		} else if ( o == _tail ) {
			_tail = o->_pre ;
			_tail->_next = NULL ;
		} else {
			o->_pre->_next = o->_next ;
			o->_next->_pre = o->_pre  ;
		}
		_size = _size - 1 ;
		return o ;
	}

	// 队头的指针
	T * begin( void ) { return _head; }
	// 队尾指针
	T * end( void ) { return _tail; }
	// 取得下一个节点
	T * next( T *cur ) { return cur->_next ; }
	// 取得前一个节点
	T * pre( T *cur ) { return cur->_pre ; }
	// 取得队列长度
	int size( void ) { return _size; }

protected:
	// 清除队列的所有数据
	void clear( void )
	{
		if ( _size == 0 ) {
			return ;
		}

		T *t = NULL ;
		T *p = _head ;
		while( p != NULL ) {
			t = p ;
			p = p->_next ;
			delete t ;
		}
		_head = _tail = NULL ;
		_size = 0 ;
	}

protected:
	// 数据队列头
	T 	*_head ;
	// 连接数据队尾
	T 	*_tail ;
	// 连接中个数
	int  _size ;
};


#endif /* TQUEUE_H_ */
