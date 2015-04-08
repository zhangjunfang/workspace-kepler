using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
using HXC_FuncUtility;
namespace HXCLog
{
    /// <summary>
    /// 切记：引用完类组件后，一定要调用此方法来释放！
    /// </summary>
    public class HXCLogDispose
    {
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            UserOPLogClass.Dispose();
            UserOPFileLogClass.Dispose();
        }
    }

    public class UserOPLogClass
    {

    private static UserOPLogClass _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    UserOPLogClass()
    {
    }
    public static UserOPLogClass Instance
    {
        get
        {
            // Double-Checked Locking
            if (null == _instance)
            {
                lock (SynObject)
                {
                    if (null == _instance)
                    {
                        _instance = new UserOPLogClass();
                    }
                }
            }
            return _instance;
        }
    }



       private static ConcurrentQueue<UserOPLog>  _userOPLogQueue = null;
       private static readonly Object locker = new Object();
       private static int queueLength = 0;
       private static bool runFlag = false;

           private static bool _alreadyDisposed = false;
           ~UserOPLogClass()
        {
           Dispose(true);
        }
        /// <summary>
       /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
       /// 清理所有正在使用的资源。
       /// </summary>
       /// <param name="isDisposing">如果应释放托管资源，为 true；否则为 false</param>
        protected static void Dispose(bool isDisposing)
      {
          if (_alreadyDisposed)
              return;
          if (isDisposing)
          {
              _userOPLogQueue = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

       private ConcurrentQueue<UserOPLog> UserOPLogQueue
        {
            get
            {
                if (_userOPLogQueue == null)
                {
                    lock (locker)
                    {
                        if (_userOPLogQueue == null)
                        {
                            _userOPLogQueue = new ConcurrentQueue<UserOPLog>();
                            queueLength = 0;
                        }
                    }
                }
                return _userOPLogQueue;
            }
        }  
    }
}
