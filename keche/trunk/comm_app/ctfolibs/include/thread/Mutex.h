/**
 * date:   2011/07/21
 * author: humingqing
 * memo:   source from apache some edited
 */

#ifndef __SHARE_MUTEX_H__
#define __SHARE_MUTEX_H__

#include <Share.h>

namespace share {

#ifndef SHARE_NO_CONTENTION_PROFILING

/**
 * Determines if the Share Mutex and RWMutex classes will attempt to
 * profile their blocking acquire methods. If this value is set to non-zero,
 * Thrift will attempt to invoke the callback once every profilingSampleRate
 * times.  However, as the sampling is not synchronized the rate is not
 * guranateed, and could be subject to big bursts and swings.  Please ensure
 * your sampling callback is as performant as your application requires.
 *
 * The callback will get called with the wait time taken to lock the mutex in
 * usec and a (void*) that uniquely identifies the Mutex (or RWMutex)
 * being locked.
 *
 * The enableMutexProfiling() function is unsynchronized; calling this function
 * while profiling is already enabled may result in race conditions.  On
 * architectures where a pointer assignment is atomic, this is safe but there
 * is no guarantee threads will agree on a single callback within any
 * particular time period.
 */
typedef void (*MutexWaitCallback)(const void* id, int64_t waitTimeMicros);
void enableMutexProfiling(int32_t profilingSampleRate,
                          MutexWaitCallback callback);

#endif

/**
 * A simple mutex class
 *
 * @version $Id:$
 */
class Mutex {
 public:
  typedef void (*Initializer)(void*);

  Mutex(Initializer init = DEFAULT_INITIALIZER);
  virtual ~Mutex() ;
  virtual void lock() const;
  virtual bool trylock() const;
  virtual bool timedlock(int64_t milliseconds) const;
  virtual void unlock() const;

  void* getUnderlyingImpl() const;

  static void DEFAULT_INITIALIZER(void*);
  static void ADAPTIVE_INITIALIZER(void*);
  static void RECURSIVE_INITIALIZER(void*);

 private:

  class impl;
  impl *impl_;
};

class RWMutex {
public:
  RWMutex();
  virtual ~RWMutex() ;

  // these get the lock and block until it is done successfully
  virtual void acquireRead() const;
  virtual void acquireWrite() const;

  // these attempt to get the lock, returning false immediately if they fail
  virtual bool attemptRead() const;
  virtual bool attemptWrite() const;

  // this releases both read and write locks
  virtual void release() const;

private:

  class  impl;
  impl  *impl_;
};

class Guard {
 public:
  Guard(const Mutex& value, int64_t timeout = 0) : mutex_(&value) {
    if (timeout == 0) {
      value.lock();
    } else if (timeout < 0) {
      if (!value.trylock()) {
        mutex_ = NULL;
      }
    } else {
      if (!value.timedlock(timeout)) {
        mutex_ = NULL;
      }
    }
  }
  ~Guard() {
    if (mutex_) {
      mutex_->unlock();
    }
  }

  operator bool() const {
    return (mutex_ != NULL);
  }

 private:
  const Mutex* mutex_;
};

// Can be used as second argument to RWGuard to make code more readable
// as to whether we're doing acquireRead() or acquireWrite().
enum RWGuardType {
  RW_READ = 0,
  RW_WRITE = 1,
};


class RWGuard {
  public:
    RWGuard(const RWMutex& value, bool write = false)
         : rw_mutex_(value) {
      if (write) {
        rw_mutex_.acquireWrite();
      } else {
        rw_mutex_.acquireRead();
      }
    }

    RWGuard(const RWMutex& value, RWGuardType type)
         : rw_mutex_(value) {
      if (type == RW_WRITE) {
        rw_mutex_.acquireWrite();
      } else {
        rw_mutex_.acquireRead();
      }
    }
    ~RWGuard() {
      rw_mutex_.release();
    }
  private:
    const RWMutex& rw_mutex_;
};


// A little hack to prevent someone from trying to do "Guard(m);"
// Such a use is invalid because the temporary Guard object is
// destoryed at the end of the line, releasing the lock.
// Sorry for polluting the global namespace, but I think it's worth it.
#define Guard(m) incorrect_use_of_Guard(m)
#define RWGuard(m) incorrect_use_of_RWGuard(m)


} // apache::thrift::concurrency

#endif // #ifndef _THRIFT_CONCURRENCY_MUTEX_H_

