/**
 * date:   2011/07/21
 * author: humingqing
 * memo:   source from apache some edited
 */

#ifndef __SHARE_EXCEPTION_H__
#define __SHARE_EXCEPTION_H__

#include <string>
#include <exception>

namespace share {

class TException : public std::exception {
 public:
  TException() {}

  TException(const std::string& message) :
    message_(message) {}

  virtual ~TException() throw() {}

  virtual const char* what() const throw() {
    if (message_.empty()) {
      return "Default TException.";
    } else {
      return message_.c_str();
    }
  }

 protected:
  std::string message_;

};

class NoSuchTaskException : public TException {};

class UncancellableTaskException : public TException {};

class InvalidArgumentException : public TException {};

class IllegalStateException : public TException {};

class TimedOutException : public TException {
public:
  TimedOutException():TException("TimedOutException"){};
  TimedOutException(const std::string& message ) :
    TException(message) {}
};

class TooManyPendingTasksException : public TException {
public:
  TooManyPendingTasksException():TException("TooManyPendingTasksException"){};
  TooManyPendingTasksException(const std::string& message ) :
    TException(message) {}
};

class SystemResourceException : public TException {
public:
    SystemResourceException() {}

    SystemResourceException(const std::string& message) :
        TException(message) {}
};

}

#endif
