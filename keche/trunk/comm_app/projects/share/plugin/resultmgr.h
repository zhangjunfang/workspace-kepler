/*
 * resultmgr.h
 *
 *  Created on: 2012-6-6
 *      Author: humingqing
 *  数据结果集缓存对象，主要通过查询条件构建的KEY来进行索引然后存储调用HTTP返回的数据
 */

#ifndef __RESULTMGR_H__
#define __RESULTMGR_H__

#include <map>
#include <vector>
#include <Ref.h>
#include <Mutex.h>
#include <time.h>
#include <TQueue.h>

class CResultMgr
{
	struct _Result
	{
		time_t				      _now ;
		unsigned int			  _id  ;
		std::vector<share::Ref *> _vec ;
		_Result 	     *_next, *_pre ;
	};
	typedef std::map<unsigned int,_Result* >  CMapResult ;
public:
	CResultMgr() ;
	~CResultMgr() ;
	// 添加结果
	bool AddResult( unsigned int msgid, share::Ref *obj ) ;
	// 添加结果
	template<typename T>
	bool AddResult( unsigned int msgid, std::vector<T *> &vec )
	{
		share::Guard guard( _mutex ) ;

		CMapResult::iterator it = _index.find( msgid ) ;
		if ( it != _index.end() ) {
			for ( int i = 0; i < (int) vec.size(); ++ i ) {
				vec[i]->AddRef() ;
				it->second->_vec.push_back( vec[i] ) ;
			}
			return true ;
		}

		_Result *p = new _Result ;
		p->_id  = msgid ;
		p->_now = time(NULL) ;

		for ( int i = 0; i < (int) vec.size(); ++ i ) {
			vec[i]->AddRef() ;
			p->_vec.push_back( (share::Ref *)vec[i] ) ;
		}
		_queue.push( p ) ;
		_index.insert( CMapResult::value_type( msgid, p ) ) ;

		return true ;
	}
	// 取得结果
	template<typename T>
	int  GetResult( unsigned int msgid, int offset, int count, std::vector<T*> &vec )
	{
		share::Guard guard( _mutex ) ;

		CMapResult::iterator it = _index.find( msgid ) ;
		if ( it == _index.end() )
			return 0 ;

		_Result *p = it->second ;

		int size = (int)p->_vec.size() ;
		if ( offset >= size )
			return 0 ;

		int index = 0 ;
		for ( int i = offset ; i < size; ++ i ) {
			p->_vec[i]->AddRef() ;
			vec.push_back( (T*)p->_vec[i] ) ;
			if ( ++index == count ) {
				break ;
			}
		}
		return index ;
	}
	// 清理数据
	void ClearResult( unsigned int msgid ) ;
	// 检测是否超时
	void Check( int timeout ) ;

private:
	// 清理对应的数据
	void Clear( _Result *p ) ;
	// 移除值
	void RemoveValue( _Result *p ) ;

private:
	// 数据结果集锁
	share::Mutex     _mutex ;
	// 数据结果对象
	CMapResult       _index ;
	// 时间索引
	TQueue<_Result>	 _queue ;
	// 最后一次检测时间
	time_t		     _lasttime ;
};

#endif /* DATACACHE_H_ */
