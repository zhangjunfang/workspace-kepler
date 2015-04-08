/**
 * memo:   线程管理对象，部分参考apache线程池实现，其中，apache对线程对象管理使用boost中智能指针，
 *         这里使用自己的引用计数对象来进行引用管理，对于线程对象使用需要继承 Runnable接口实现run接口，
 *         其中，param是线程个性化参数，主要针对同一个对象使用Runnable来实现多组线程池完成不同功能情况就可以通过个性化参数来区分
 *
 * date:   2011/07/21
 * author: humingqing
 */

#ifndef __SHARE_THREAD_H__
#define __SHARE_THREAD_H__

#include <Share.h>
#include <Ref.h>
#include <list>
#include <Mutex.h>
#include <pthread.h>

/**
 * 线程运行对象接口
 *
 * @version $Id:$
 */

namespace share {

/**
 * 线程执行对象
 */
class Runnable {
public:
  virtual ~Runnable() {} ;
  /**
   * 线程运行对象
   */
  virtual void run( void *param ) = 0 ;
};

/**
 * 线程处理对象
 */
class Thread : public Ref
{
	 /**
	   * POSIX Thread scheduler policies
	   */
	  enum POLICY {
	    OTHER,
	    FIFO,
	    ROUND_ROBIN
	  };

	  /**
	   * POSIX Thread scheduler relative priorities,
	   *
	   * Absolute priority is determined by scheduler policy and OS. This
	   * enumeration specifies relative priorities such that one can specify a
	   * priority withing a giving scheduler policy without knowing the absolute
	   * value of the priority.
	   */
	  enum PRIORITY {
	    LOWEST = 0,
	    LOWER = 1,
	    LOW = 2,
	    NORMAL = 3,
	    HIGH = 4,
	    HIGHER = 5,
	    HIGHEST = 6,
	    INCREMENT = 7,
	    DECREMENT = 8
	  };

	  enum STATE {
	    uninitialized,
	    starting,
	    started,
	    stopping,
	    stopped
	  };

	  static const int MB = 1024 * 1024;
public:
	  /**
	   * 线程构造对象
	   */
	  Thread( Runnable *runner , void *param = NULL ,  int policy = FIFO , int priority = NORMAL , int stackSize = 8 , bool detached = false ) ;

	  virtual ~Thread() ;

	  /**
	   * Starts the thread. Does platform specific thread creation and
	   * configuration then invokes the run method of the Runnable object bound
	   * to this thread.
	   */
	  virtual void start( void ) ;

	  /**
	   * Join this thread. Current thread blocks until this target thread
	   * completes.
	   */
	  virtual void join() ;

	  /**
	   * Gets the thread's platform-specific ID
	   */
	  virtual pthread_t getId() { return _pthread ; } ;

public:
	  /**
	   * 执行线程的主函数
	   */
	  static void * ThreadMain( void *param ) ;

	  /**
	   * 取得运行对象
	   */
	  Runnable * runable( void )  { return _runner ; }

private:
	  /**
	   *  线程运行对象
	   */
	  Runnable *_runner ;

	  /**
	   *  线程ID
	   */
	  pthread_t _pthread ;

	  /**
	   *  带过的参数
	   */
	  void * 	_param ;

	  /**
	   * 线程状态
	   */
	  STATE 	_state;

	  /**
	   * POSIX Thread scheduler policies
	   */
	  int 		_policy;

	  /**
	   *  线程优先级
	   */
	  int 		_priority;

	  /**
	   * 栈空间大小
	   */
	  int 		_stackSize;

	  /**
	   * 类型
	   */
	  bool 		_detached;

	  /**
	   * 线程对象
	   */
	  Thread   *_selfRef ;
};

/**
 * 线程管理对象
 */
class ThreadManager
{
public:
	ThreadManager():_thread_state(false) {}

	virtual ~ThreadManager() ;
	/**
	 *  初始化线程对象
	 */
	virtual bool init( unsigned int nthread, void *param, Runnable *runner )  ;

	/**
	 *  开始运行线程
	 */
	virtual void start( void ) ;

	/**
	 *  停止线程
	 */
	virtual void stop( void ) ;

private:
	/**
	 * 线程对象列表
	 */
	typedef std::list<Thread*>  ThreadList ;

	/**
	 * 线程存放对象
	 */
	ThreadList	 _thread_lst ;

	/**
	 * 线程池的状态
	 */
	bool 		 _thread_state ;
};

}
#endif
