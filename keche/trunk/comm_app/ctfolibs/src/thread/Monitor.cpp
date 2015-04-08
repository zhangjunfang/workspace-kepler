/**
 * memo:   source from apache some edited
 * notice: 多线程使用 Monitor对象需要配合Synchronized来使用，如果不进行同步而存在多线程数据错乱情况
 * date:   2011/07/21
 * author: humingqing
 */

#include "Monitor.h"
#include "Util.h"
#include "Exception.h"

#include <assert.h>
#include <errno.h>

#include <iostream>

#include <pthread.h>

namespace share {

/**
 * Monitor implementation using the POSIX pthread library
 *
 * @version $Id:$
 */
class Monitor::Impl {

 public:

  Impl(): mutex_(NULL),
       condInitialized_(false),
       condnotifyend_(false){
    init(&ownedMutex_);
  }

  Impl(Mutex* mutex)
     : mutex_(NULL),
       condInitialized_(false),
       condnotifyend_(false){
    init(mutex);
  }

  Impl(Monitor* monitor)
     : mutex_(NULL),
       condInitialized_(false),
       condnotifyend_(false){
    init(&(monitor->mutex()));
  }

  ~Impl() { cleanup(); }

  Mutex& mutex() { return *mutex_; }
  void lock() { mutex().lock(); }
  void unlock() { mutex().unlock(); }

  void wait(int64_t timeout) const {
	// 如果已经收到停止的指令直接退出
	if ( condnotifyend_ )
		return ;

    assert(mutex_);
    pthread_mutex_t* mutexImpl =
      reinterpret_cast<pthread_mutex_t*>(mutex_->getUnderlyingImpl());
    assert(mutexImpl);

    // XXX Need to assert that caller owns mutex
    assert(timeout >= 0LL);
    if (timeout == 0LL) {
      int iret = pthread_cond_wait(&pthread_cond_, mutexImpl);
      assert(iret == 0);
    } else {
      struct timespec abstime;
      int64_t now = Util::currentTime();
#ifdef _USE_SECOND
      Util::toTimespec(abstime, now , timeout );
#else
      Util::toTimespec(abstime, now + timeout );
#endif
      int result = pthread_cond_timedwait(&pthread_cond_,
                                          mutexImpl,
                                          &abstime);
      if (result == ETIMEDOUT) {
        // pthread_cond_timedwait has been observed to return early on
        // various platforms, so comment out this assert.
#ifdef _XDEBUG
    	printf( "begin time %d, time out %d, current time %d\n" , now, timeout, Util::currentTime() ) ;
#endif
        // assert(Util::currentTime() >= (now + timeout));
        // throw TimedOutException();
      }
    }
  }

  void notify() {
    // XXX Need to assert that caller owns mutex
    int iret = pthread_cond_signal(&pthread_cond_);
    assert(iret == 0);
  }

  void notifyAll() {
    // XXX Need to assert that caller owns mutex
    int iret = pthread_cond_broadcast(&pthread_cond_);
    assert(iret == 0);
  }

  void notifyEnd() {
	// 设置结束标识
	condnotifyend_ = true ;
	// XXX Need to assert that caller owns mutex
	int iret = pthread_cond_broadcast(&pthread_cond_);
	assert(iret == 0);
  }

 private:

  void init(Mutex* mutex) {
    mutex_ = mutex;

    if (pthread_cond_init(&pthread_cond_, NULL) == 0) {
      condInitialized_ = true;
    }

    if (!condInitialized_) {
      cleanup();
      throw SystemResourceException();
    }
  }

  void cleanup() {
    if (condInitialized_) {
      condInitialized_ = false;
      int iret = pthread_cond_destroy(&pthread_cond_);
      assert(iret == 0);
    }
  }

  Mutex  ownedMutex_;
  Mutex* mutex_;

  mutable pthread_cond_t pthread_cond_;
  mutable bool condInitialized_;
  mutable bool condnotifyend_ ;
};

Monitor::Monitor() : impl_(new Monitor::Impl()) {}
Monitor::Monitor(Mutex* mutex) : impl_(new Monitor::Impl(mutex)) {}
Monitor::Monitor(Monitor* monitor) : impl_(new Monitor::Impl(monitor)) {}

Monitor::~Monitor() { delete impl_; }

Mutex& Monitor::mutex() const { return impl_->mutex(); }

void Monitor::lock() const { impl_->lock(); }

void Monitor::unlock() const { impl_->unlock(); }

void Monitor::wait(int64_t timeout) const { impl_->wait(timeout); }

void Monitor::notify() const { impl_->notify(); }

void Monitor::notifyAll() const { impl_->notifyAll(); }

void Monitor::notifyEnd() const { impl_->notifyEnd(); }

} // share
