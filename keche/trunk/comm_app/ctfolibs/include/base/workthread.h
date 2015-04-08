/************************************************************************/
/* Author: humingqing                                                   */
/* Date:   2013-02-05													*/
/* Memo:   线程执行池对象												*/
/************************************************************************/
#ifndef __WORKTHREAD_H__
#define __WORKTHREAD_H__

#include <map>
#include <Monitor.h>
#include <Thread.h>
#include <sortqueue.h>

#define THREAD_REG_ERROR  -1  // 注册执行对象失败

class CWorkThread: public share::Runnable
{
	struct _ThreadUnit
	{
		int			 	 _id ;
		int			 	 _span ;
		share::Runnable *_pProc ;
		void        *	 _ptr ;
		bool		 	 _run ;
		time_t		 	 _time ;
		_ThreadUnit *	 _next ;
		_ThreadUnit *	 _pre ;

		_ThreadUnit() 
		{
			_next  = _pre = NULL ;
			_pProc = NULL ;
			_ptr   = NULL ;
			_run   = false ;
		}
	};
	typedef std::map<int,_ThreadUnit*> CMapUnit;

	// 序列产生对象
	class CSequeue
	{
	public:
		CSequeue():_id(0){}
		~CSequeue(){}

		// 产生序列
		int get_next_seq( void ) {
			int id = 0 ;

			_mutex.lock() ;
			_id = _id + 1 ;
			if ( _id < 0 ) {
				_id = 1 ;
			}
			id = _id;
			_mutex.unlock() ;

			return id ;
		}

	private:
		// 线程ID号对象
		int  		  _id ;
		// 操作同步锁
		share::Mutex  _mutex;
	};
public:
	CWorkThread() ;
	~CWorkThread() ;

	// 初始化线程
	bool Init( int nthread ) ;
	// 停止线程
	void Stop( void ) ;
	// 注册线程执行对象,当时间为零则默认执行一次
	int  Register( share::Runnable *pProc, void *ptr = NULL, int time = 0 ) ;
	// 撤销执行对象
	void UnRegister( int id ) ;

protected:
	// 检测是否有需要运行对象
	int  Check( void ) ;
	// 线程运行接口对象
	void run( void* param ) ;
	// 清除所有执行对象
	void Clear( void ) ;

private:
	// 信号管理对象
	share::Monitor			 _monitor ;
	// 线程管理对象
	share::ThreadManager	 _threadmgr ;
	// 是否初始化数据
	bool 					 _inited ;
	// 序列管理对象
	CSequeue				 _sequeue;
	// 同步操作对象
	share::Mutex			 _mutex;
	// 执行对象队列
	TSortQueue<_ThreadUnit>  _queue;
	// 查找索引
	CMapUnit				 _index;
};

#endif
