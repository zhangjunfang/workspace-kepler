/**
 * date:   2011/07/21
 * author: humingqing
 * memo:   线程管理对象，部分参考apache线程池实现
 */

#include "Thread.h"
#include "Exception.h"
#include <assert.h>
#include <pthread.h>

#include <iostream>

namespace share{

/**
 * 线程数据初始化
 */
Thread::Thread( Runnable *runner , void *param,  int policy , int priority , int stackSize , bool detached ) :  _runner(runner)
{
	_param     = NULL ;
	_param     = param ;
	_pthread   = (pthread_t) -1 ;
	_policy    = policy   ;
	_priority  = priority ;
	_detached  = detached ;
	_stackSize = stackSize ;
	_selfRef   = this ;
	_state     = uninitialized ;
}

Thread::~Thread(void)
{
	if(!_detached) {
	  try {
		join();
	  } catch(...) {
		// We're really hosed.
	  }
	}
}
/**
* Starts the thread. Does platform specific thread creation and
* configuration then invokes the run method of the Runnable object bound
* to this thread.
*/
void Thread::start( void )
{
	if (_state != uninitialized) {
	  return;
	}

	pthread_attr_t thread_attr;
	if (pthread_attr_init(&thread_attr) != 0) {
		throw SystemResourceException("pthread_attr_init failed");
	}

	if(pthread_attr_setdetachstate(&thread_attr,
								   _detached ?
								   PTHREAD_CREATE_DETACHED :
								   PTHREAD_CREATE_JOINABLE) != 0) {
		throw SystemResourceException("pthread_attr_setdetachstate failed");
	}

	// Set thread stack size
	if (pthread_attr_setstacksize(&thread_attr, MB * _stackSize ) != 0) {
	  throw SystemResourceException("pthread_attr_setstacksize failed");
	}

	// Set thread policy
	if (pthread_attr_setschedpolicy(&thread_attr, _policy) != 0) {
	  throw SystemResourceException("pthread_attr_setschedpolicy failed");
	}

	struct sched_param sched_param;
	sched_param.sched_priority = _priority;

	// Set thread priority
	if (pthread_attr_setschedparam(&thread_attr, &sched_param) != 0) {
	  throw SystemResourceException("pthread_attr_setschedparam failed");
	}

	// Create reference
	_state = starting;

	if (pthread_create(&_pthread, &thread_attr, ThreadMain, (void*)_selfRef) != 0) {
	  throw SystemResourceException("pthread_create failed");
	}
}

/**
* Join this thread. Current thread blocks until this target thread
* completes.
*/
void Thread::join()
{
	if (!_detached && _state != uninitialized) {
	  void* ignore;
	  _detached = pthread_join(_pthread, &ignore) == 0;
	}
}

/**
 * 线程运行对象
 */
void * Thread::ThreadMain( void *param )
{
	Thread *thread = ( Thread *) param ;

	if (thread == NULL) {
		return (void*)0;
	}

	if (thread->_state != starting) {
		return (void*)0;
	}

	// 进行线程添加引用
	thread->AddRef() ;
	thread->_state = starting;
	thread->runable()->run( thread->_param ) ;

	if (thread->_state != stopping && thread->_state != stopped) {
		thread->_state = stopping;
	}
	// 线程结束减少引用
	thread->Release() ;

	return (void*)0;
}

//===================================== 线程管理对象 =========================================//

ThreadManager::~ThreadManager()
{
	if ( _thread_lst.empty() ) {
		return ;
	}

	Thread *p = NULL ;
	ThreadList::iterator it ;
	for ( it = _thread_lst.begin(); it != _thread_lst.end(); ++ it )
	{
		p = *it ; // printf( "get ref %d\n" , p->GetRef() ) ;
		if ( p != NULL ){
			p->Release() ;
			p = NULL ;
		}
	}
	_thread_lst.clear() ;
}

/**
 *  初始化线程对象
 */
bool ThreadManager::init( unsigned int nthread, void *param, Runnable *runner )
{
	if ( nthread == 0 )
		return false ;

	for ( unsigned int i = 0; i < nthread; ++ i ){
		Thread *p = new Thread( runner, param ) ;
		assert( p != NULL ) ;
		p->AddRef() ;
		_thread_lst.push_back( p ) ;
	}
	return true ;
}

/**
 *  开始运行线程
 */
void ThreadManager::start( void )
{
	Thread *p = NULL ;
	ThreadList::iterator it ;
	for ( it = _thread_lst.begin(); it != _thread_lst.end(); ++ it )
	{
		p = *it ;
		assert( p != NULL ) ;
		p->start() ;
	}
	_thread_state = true ;
}

/**
 *  停止线程
 */
void ThreadManager::stop( void )
{
	if ( ! _thread_state )
		return ;

	_thread_state = false ;

	Thread *p = NULL ;
	ThreadList::iterator it ;
	for ( it = _thread_lst.begin(); it != _thread_lst.end(); ++ it )
	{
		p = *it ;
		assert( p != NULL ) ;
		p->join() ;
	}
}

}
