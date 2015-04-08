using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using SYSModel;
namespace HuiXiuCheWcfFileTransferService
{
    public class DownLoadFilesQueue 
    {
    private static DownLoadFilesQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    DownLoadFilesQueue()
    {
    }
    public static DownLoadFilesQueue Instance
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
                        _instance = new DownLoadFilesQueue();
                    }
                }
            }
            return _instance;
        }
    }

        private static ConcurrentDictionary<string, DownLoadFileOoj_Server> _serverDownLoadFilesDic = null;
        private static readonly Object locker = new Object();

         private static bool _alreadyDisposed = false;
         ~DownLoadFilesQueue()
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
              _serverDownLoadFilesDic = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        private static ConcurrentDictionary<string, DownLoadFileOoj_Server> ServerDownLoadFilesDic
        {
            get
            {
                if (_serverDownLoadFilesDic == null)
                {
                    lock (locker)
                    {
                        if (_serverDownLoadFilesDic == null)
                        {
                            _serverDownLoadFilesDic = new ConcurrentDictionary<string, DownLoadFileOoj_Server>();
                        }
                    }
                }
                return _serverDownLoadFilesDic;
            }
        }

        public bool Add(string guId, DownLoadFileOoj_Server obj)
        {
            DownLoadFileOoj_Server value = null;
            if (ServerDownLoadFilesDic.TryGetValue(guId, out value))
            {
                return ServerDownLoadFilesDic.TryUpdate(guId, obj, value);
            }
            else
            {
                return ServerDownLoadFilesDic.TryAdd(guId, obj);
            }
        }

        public bool Get(string guId, out DownLoadFileOoj_Server obj)
        {
            return ServerDownLoadFilesDic.TryGetValue(guId, out obj);
        }

        public bool Remove(string guId, out DownLoadFileOoj_Server obj)
        {
            return ServerDownLoadFilesDic.TryRemove(guId, out obj);
        }
    }
}