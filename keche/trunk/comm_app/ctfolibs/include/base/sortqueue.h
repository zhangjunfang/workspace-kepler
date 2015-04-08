/*
 * sortqueue.h
 *
 *  Created on: 2012-12-27
 *      Author: humingqing
 *
 *  排序队列处理，主要实时按照时间排序插入数据，扩展原有的TQueue对象
 *  这里面使用   _next, _pre, _time 前后两个指针和时间排序对象
 *
 */

#ifndef __SORTQUEUE_H__
#define __SORTQUEUE_H__

#include <TQueue.h>

template<typename T>
class TSortQueue : public TQueue<T>
{
	typedef TQueue<T>  TBase;
public:
	TSortQueue() {
		TBase::_head = TBase::_tail = NULL;
		TBase::_size = 0 ;
	}
	~TSortQueue() { TBase::clear() ; }

	// 放入队列管理
	void insert( T *o )
	{
		o->_next = o->_pre = NULL ;
		// 如果没有元素就直接添加到头部
		if ( TBase::_head == NULL ) {
			TBase::_head = TBase::_tail = o ;
		} else {
			// 最后一个元素的超时时间大于当前元素的超时时间
			if ( TBase::_tail->_time > o->_time ) {
				// 直接判断头部也是否大于它
				if ( TBase::_head->_time > o->_time ) {
					TBase::_head->_pre = o ;
					o->_next    	   = TBase::_head ;
					TBase::_head       = o ;
				} else { //　否则肯定在中间的情况
					T *t = TBase::_tail->_pre ;
					// 倒过来遍历的数据
					while( t != NULL ) {
						if ( t->_time <= o->_time ){ // 在队列中间插入元素
							o->_pre  = t ;
							o->_next = t->_next ;
							if ( t->_next )
								t->_next->_pre = o ;
							t->_next = o ;
							break ;
						}
						t = t->_pre ;
					}
				}
			} else { //　这种情况直接放到队尾处理
				TBase::_tail->_next  =  o ;
				o->_pre      		 =  TBase::_tail ;
				TBase::_tail 		 =  o ;
			}
		}
		TBase::_size = TBase::_size + 1 ;
	}
};

#endif /* SORTQUEUE_H_ */
