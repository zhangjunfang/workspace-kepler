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
    public class UserFunctionOPLogClass
    {
        private static UserFunctionOPLog _instance = null;
        // Creates an syn object.
        private static readonly object SynObject = new object();
        UserFunctionOPLogClass()
        {
        }
        public static UserFunctionOPLog Instance
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
                            _instance = new UserFunctionOPLog();
                        }
                    }
                }
                return _instance;
            }
        }



        private static ConcurrentQueue<UserFunctionOPLog> _userFunctionOPLogQueue = null;
        private static readonly Object locker = new Object();
        private static int queueLength = 0;
        private static bool runFlag = false;

        private static bool _alreadyDisposed = false;
        ~UserFunctionOPLogClass()
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
                _userFunctionOPLogQueue = null;
                _instance = null;
                //GC.SuppressFinalize(this);
            }
            _alreadyDisposed = true;
        }

        private ConcurrentQueue<UserFunctionOPLog> UserFunctionOPLogQueue
        {
            get
            {
                if (_userFunctionOPLogQueue == null)
                {
                    lock (locker)
                    {
                        if (_userFunctionOPLogQueue == null)
                        {
                            _userFunctionOPLogQueue = new ConcurrentQueue<UserFunctionOPLog>();
                            queueLength = 0;
                        }
                    }
                }
                return _userFunctionOPLogQueue;
            }
        }
        public void Add(UserFunctionOPLog item)
        {
            UserFunctionOPLogQueue.Enqueue(item);
            Interlocked.Increment(ref queueLength);
            lock (SynObject)
            {
                if (queueLength > 10 && !runFlag)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(NewThreadFunc));
                }
            }
        }


        private void NewThreadFunc(object state)
        {
            runFlag = true;
            UserFunctionOPLog item = null;
            while (UserFunctionOPLogQueue.TryDequeue(out item))
            {
                //将item送往用户操作日志表
                //访问数据库的操作写在这里
                BLL.OPLog.Add(item,GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.CommAccCode);
                Interlocked.Decrement(ref queueLength);
            }
            runFlag = false;
        }

    }
}
