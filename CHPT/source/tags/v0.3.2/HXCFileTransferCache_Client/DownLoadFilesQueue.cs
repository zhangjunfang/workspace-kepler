using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using SYSModel;

namespace HXCFileTransferCache_Client
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

        private static ConcurrentDictionary<string, DownLoadFileOoj_Client> _pCClientDownLoadFilesDic = null;
        private static readonly Object locker = new Object();
        private static bool _alreadyDisposed = false;
        ~DownLoadFilesQueue()
        {
           Dispose(true);
        }
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
              _pCClientDownLoadFilesDic = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public static ConcurrentDictionary<string, DownLoadFileOoj_Client> PCClientDownLoadFilesDic
        {
            get
            {
                if (_pCClientDownLoadFilesDic == null)
                {
                    lock (locker)
                    {
                        if (_pCClientDownLoadFilesDic == null)
                        {
                            _pCClientDownLoadFilesDic = new ConcurrentDictionary<string, DownLoadFileOoj_Client>();
                        }
                    }
                }
                return _pCClientDownLoadFilesDic;
            }
        }

        public bool Add(string guId, DownLoadFileOoj_Client obj)
        {
            DownLoadFileOoj_Client value = null;
            if (PCClientDownLoadFilesDic.TryGetValue(guId, out value))
            {
              return  PCClientDownLoadFilesDic.TryUpdate(guId, obj, value);
            }
            else
            {
              return  PCClientDownLoadFilesDic.TryAdd(guId, obj);
            }
        }

        public bool Get(string guId, out DownLoadFileOoj_Client obj)
        {
            return PCClientDownLoadFilesDic.TryGetValue(guId, out obj);
        }

        public bool Remove(string guId, out DownLoadFileOoj_Client obj)
        {
            return PCClientDownLoadFilesDic.TryRemove(guId, out obj);
        }

        public int GetLength()
        {
            return PCClientDownLoadFilesDic.Count;
        }

       

    }
}
