/***********************************************************************
 ** Copyright (c)2009,北京千方科技集团有限公司
 ** All rights reserved.
 ** 
 ** File name  : DataPool.h
 ** Author     : lizp (lizp.net@gmail.com)
 ** Date       : 2010-1-3 下午 05:02:25
 ** Comments   : 中转站，存放ClassType信息
 **
 ** 2011-09-14 humingqing 修改缓存池对象，添加时间索引来处理超时数据，
 ** 利用MAP自动排序功能，这样可以减少MAP的遍历次数，毕竟MAP的遍历的效率是很低的
 ***********************************************************************/

#ifndef _DATA_POOL_H_
#define _DATA_POOL_H_

#include <list>
#include <string>
#include <errno.h>
#include <comlog.h>
#include <tools.h>
#include <Monitor.h>
#include <map>

#define max_data_pool_num 10*10000

// 数据队列
template<class ClassType>
class CDataPool
{
/**
 * <humingqing 2012.07.10>
 * 这里使用自已记数的原则处理， 主要是在性能测试中发现list。size()方法效率很低，没有通过计数变量来处理效率高
 * 多线程使用 getData时，有可能取得数据元素为空的情况，这里需要判断的取出的元素是否有效，主要原因是因为锁的作用不连续而引起的
 */
protected:
	share::Monitor  _monitor ;
	share::Mutex    _mutex ;
	list<ClassType> _datapool ;
	unsigned int    _maxsize ;
	unsigned int    _cursize ;

public:
	CDataPool( void ):_maxsize(max_data_pool_num)
	{
		_cursize = 0 ;
	}
	CDataPool( int size ): _maxsize(size)
	{
		_cursize = 0 ;
	}
	~CDataPool(){}

	/**
	 *  获取数据池中的数据。阻塞式函数,为什么还要传递引用进去，如果是真真的话，没法进行初始化，不能通过返回值来判定是否真的去的数据成功。
	 */
	ClassType getData(ClassType &element, bool block = true )
	{
		// 如果用阻塞类型，没有数据就直接阻塞了
		if ( block && empty() ) {
			_monitor.lock() ;
			_monitor.wait() ;
			_monitor.unlock() ;
		}

		_mutex.lock() ;
		//有可能出现空值的情况，当缓冲池满了以后---->会触发sem_post的时件。导致最后sempost的事件多于semwait
		if ( _cursize > 0 )
		{
			element = (ClassType)_datapool.front();
			_datapool.pop_front();
			-- _cursize ;
		}
		_mutex.unlock() ;

		return element;
	}

	/**
	 *  向数据池中添加数据。
	 */
	bool addData(ClassType& rd,int flag = 1)
	{
		_mutex.lock() ;

		if ( _cursize > _maxsize )
		{
			ClassType element = _datapool.front();
			_datapool.pop_front();
			if(flag) delete element;
			-- _cursize ;
		}
		_datapool.push_back(rd);
		++ _cursize ;

		_mutex.unlock() ;

		_monitor.notify() ;

		return true;
	}

	/**
	 * 数据对象是否为空
	 */
	bool empty()
	{
		share::Guard g(_mutex) ;
		{
			return ( _cursize == 0 ) ;
		}
	}
	// 取得池中最大值
	int get_max_size(){ return _maxsize; }
	// 未处理锁操作，对于同步数据是会有点影响
	int get_cur_size(){ return _cursize; }

	// 防止出现阻塞
	void notifyend() {
		// 析构处理时需要发送一个信号过去
		_monitor.notifyEnd() ;
	}
};
#pragma pack(1)

typedef struct _CacheData
{
	string user_id; //登录用户的ID，不是发送消息中的mac_id;
	string str_send_msg;
	string timeout_msg;

	string seq;
	string mac_id;
	string command;
	string company_id;

	char *send_data;
	int send_data_len;

	time_t send_time;

	_CacheData()
	{
		send_data 		= NULL;
		send_data_len   = 0;
		send_time 		= time(0);
	}
} CacheData;
#pragma pack()

// 缓存池
class CacheDataPool
{
public:
	CacheDataPool(){}
	~CacheDataPool(){}

	// 添加缓存数据
	void add( const string &req, const CacheData &data)
	{
		share::Guard g( _mutex ) ;
		{
			_cache[req] = data;
			// 添加时间索引
			_index.insert( pair<time_t,string>(data.send_time, req) ) ;
		}
	}

	// 签出缓存数据
	CacheData checkData( const string &req)
	{
		share::Guard g( _mutex ) ;
		{
			CacheData d;
			map<string, CacheData>::iterator p = _cache.find(req);
			if (p != _cache.end())
			{
				d = (CacheData) p->second;
				_cache.erase(req);
			}
			// 清除索引
			RemoveIndex( d.send_time, req ) ;

			return d;
		}
	}

	// 处理超时数据
	bool timeoutData( int seconds , list<CacheData> &lst )
	{
		share::Guard g( _mutex ) ;
		{
			if ( _index.empty() )
				return false ;

			time_t now = time(0) - seconds ;

			map<string,CacheData>::iterator itx ;
			multimap<time_t,string>::iterator it ;
			for ( it = _index.begin(); it != _index.end(); ){
				if ( it->first > now ) {
					break ;
				}
				// 查找数据
				itx = _cache.find( it->second ) ;
				if ( itx == _cache.end() ) {
					_index.erase( it ++ ) ;
					continue ;
				}
				// 添加超时队列中
				lst.push_back( itx->second ) ;

				_cache.erase( itx ) ;
				_index.erase( it ++ ) ;
			}

			return ( ! lst.empty() ) ;
		}
	}

private:
	// 移除索引
	void RemoveIndex( time_t t , const string &seq )
	{
		multimap<time_t,string>::iterator it = _index.find( t ) ;
		if ( it == _index.end() ){
			return ;
		}

		for ( ; it != _index.end(); ++ it ){
			if ( it->first != t ) {
				break ;
			}
			if ( it->second == seq ){
				_index.erase( it ) ;
				break ;
			}
		}
	}

protected:
	share::Mutex           	_mutex ;
	// 缓存MAP
	map<string, CacheData> 	_cache ;
	// 缓存索引
	multimap<time_t,string> _index ;
};

#endif

