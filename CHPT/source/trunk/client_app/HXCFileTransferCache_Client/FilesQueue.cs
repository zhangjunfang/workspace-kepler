using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using SYSModel;
namespace HXCFileTransferCache_Client
{
    public class FilesQueue 
    {
    private static FilesQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    FilesQueue()
    {
    }
    public static FilesQueue Instance
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
                        _instance = new FilesQueue();
                    }
                }
            }
            return _instance;
        }
    }


        private static ConcurrentDictionary<string, TransferSendFileObj> _pCClientFilesDic = null;
        private static readonly Object locker = new Object();
        private static bool _alreadyDisposed = false;
         ~FilesQueue()
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
              _pCClientFilesDic = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }
        private static ConcurrentDictionary<string, TransferSendFileObj> PCClientFilesDic
        {
            get
            {
                if (_pCClientFilesDic == null)
                {
                    lock (locker)
                    {
                        if (_pCClientFilesDic == null)
                        {
                            _pCClientFilesDic = new ConcurrentDictionary<string, TransferSendFileObj>();
                        }
                    }
                }
                return _pCClientFilesDic;
            }
        }

        public void Add(string guId, TransferSendFileObj obj)
        {
            TransferSendFileObj value = null;
            if (PCClientFilesDic.TryGetValue(guId, out value))
            {
                PCClientFilesDic.TryUpdate(guId, obj, value);
            }
            else
            {
                PCClientFilesDic.TryAdd(guId, obj);
            }
        }

        public bool Get(string guId, out TransferSendFileObj obj)
        {
            return PCClientFilesDic.TryGetValue(guId, out obj);
        }

        private static bool Remove(string guId, out TransferSendFileObj obj)
        {
            return PCClientFilesDic.TryRemove(guId, out obj);
        }
    }
}
