#include "workthread.h"

CWorkThread::CWorkThread() 
	: _inited(false)
{

}

CWorkThread::~CWorkThread() 
{
	Stop() ;
	Clear() ;
}

// 初始化线程
bool CWorkThread::Init( int nthread ) 
{
	if ( ! _threadmgr.init( nthread, NULL, this ) ) {
		return false ;
	}
	_inited = true ;
	_threadmgr.start() ;
	return true ;
}

// 停止线程
void CWorkThread::Stop( void ) {
	if ( ! _inited )
		return ;
	_inited = false ;
	_monitor.notifyEnd() ;
	_threadmgr.stop() ;
}

// 注册线程执行对象,当时间为零则默认执行一次
int  CWorkThread::Register( share::Runnable *pProc, void *ptr , int t )
{
	if ( pProc == NULL )
		return THREAD_REG_ERROR ;

	int id = _sequeue.get_next_seq() ;

	_ThreadUnit *unit = new _ThreadUnit ;
	unit->_id    = id ;
	unit->_pProc = pProc ;
	unit->_span  = t ;
	unit->_ptr   = ptr ;
	unit->_time  = time(NULL) + t ;

	_mutex.lock() ;
	_queue.insert( unit ) ;
	_index[id] = unit ;
	_mutex.unlock() ;

	_monitor.notify() ;

	return id ;
}

// 撤销执行对象
void CWorkThread::UnRegister( int id ) 
{
	_mutex.lock() ;
	CMapUnit::iterator it = _index.find( id ) ;
	if ( it == _index.end() ) {
		_mutex.unlock() ;
		return ;
	}
	_ThreadUnit *p = it->second ;
	_index.erase( it ) ;
	_queue.erase( p ) ;
	_mutex.unlock() ;

	delete p ;
}

// 检测是否有需要运行对象
int CWorkThread::Check( void ) 
{
	_mutex.lock() ;

	if ( _queue.size() == 0 ) {
		_mutex.unlock() ;
		return 0 ;
	}

	time_t now = time(NULL) ;

	_ThreadUnit *t = NULL ;
	_ThreadUnit *p = _queue.begin() ;
	// 遍历所有执行体
	while( p != NULL ) {
		t = p ;
		p = _queue.next( p ) ;

		if ( now < t->_time )
			break ;
		
		t->_time = now + t->_span ;
		// 如果第一次没有执行完成，直接转到下一次执行
		if ( ! t->_run ) {
			t->_run  = true ;

			bool berase = false ;
			if ( t->_span == 0 ) {
				// 否则为执行一次对象
				_index.erase( t->_id ) ;
				_queue.erase( t ) ;
				berase = true ;
			}
			_mutex.unlock() ;

			t->_pProc->run( t->_ptr ) ;
			if ( berase ) {
				delete t ;
				// 如果执行一次就处理一次
				_mutex.lock() ;
				// 如果执行一次的情况就直接返回从头开始
				p = _queue.begin() ;

				continue; 
			}

			_mutex.lock() ;
			t->_run  = false ;
		} 

		t =_queue.erase( t ) ;
		// 如果为定时对象需要重复定时执行
		_queue.insert( t ) ;
	}
	// 如果执行对象为空
	if ( _queue.size() == 0 ) {
		_mutex.unlock() ;
		return 0 ;
	}

	// 计算下一次执行体的的时间
	int span = (int)( _queue.begin()->_time - time(NULL) ) ;
	
	_mutex.unlock() ;

	return ( span <= 0 ) ? -1 : span ;
}

// 线程运行接口对象
void CWorkThread::run( void* param )
{
	while( _inited ) {
		// 检测执行对象
		int time = Check();
		// 只有大于或者等于零时才进入等待状态
		if ( time >= 0 ) {
			// 等待
			_monitor.wait( time ) ;
		}
	}
}

// 清除所有执行对象
void CWorkThread::Clear( void ) 
{
	_mutex.lock() ;

	int size = 0;
	_ThreadUnit *p = _queue.move( size ) ;
	if ( size == 0 ) {
		_mutex.unlock() ;
		return ;
	}

	_ThreadUnit *t = NULL ;
	while( p != NULL ) {
		t = p ;
		p = p->_next ;
		delete t ;
	}
	_index.clear() ;

	_mutex.unlock() ;
}
