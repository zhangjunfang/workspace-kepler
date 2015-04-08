/**
 * memo:   source from apache some edited
 * notice: 多线程使用 Monitor对象需要配合Synchronized来使用，如果不进行同步而存在多线程数据错乱情况
 * date:   2011/07/21
 * author: humingqing
 */

#ifndef _SHARE_MONITOR_H_
#define _SHARE_MONITOR_H_

#include <Mutex.h>

namespace share {

/**
 * A monitor is a combination mutex and condition-event.  Waiting and
 * notifying condition events requires that the caller own the mutex.  Mutex
 * lock and unlock operations can be performed independently of condition
 * events.  This is more or less analogous to java.lang.Object multi-thread
 * operations.
 *
 * Note the Monitor can create a new, internal mutex; alternatively, a
 * separate Mutex can be passed in and the Monitor will re-use it without
 * taking ownership.  It's the user's responsibility to make sure that the
 * Mutex is not deallocated before the Monitor.
 *
 * Note that all methods are const.  Monitors implement logical constness, not
 * bit constness.  This allows const methods to call monitor methods without
 * needing to cast away constness or change to non-const signatures.
 *
 * @version $Id:$
 */
class Monitor {
 public:
  /** Creates a new mutex, and takes ownership of it. */
  Monitor();

  /** Uses the provided mutex without taking ownership. */
  explicit Monitor(Mutex* mutex);

  /** Uses the mutex inside the provided Monitor without taking ownership. */
  explicit Monitor(Monitor* monitor);

  /** Deallocates the mutex only if we own it. */
  virtual ~Monitor();

  Mutex& mutex() const;

  virtual void lock() const;

  virtual void unlock() const;

  virtual void wait(int64_t timeout=0LL) const;

  virtual void notify() const;

  virtual void notifyAll() const;

  virtual void notifyEnd() const ;

 private:

  class Impl;

  Impl* impl_;
};

class Synchronized {
 public:
 Synchronized(const Monitor* monitor) : g(monitor->mutex()) { }
 Synchronized(const Monitor& monitor) : g(monitor.mutex()) { }

 private:
  Guard g;
};


} // share

#endif // #ifndef _THRIFT_CONCURRENCY_MONITOR_H_
