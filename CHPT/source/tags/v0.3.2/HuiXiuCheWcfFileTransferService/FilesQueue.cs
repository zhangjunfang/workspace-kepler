using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using SYSModel;

namespace HuiXiuCheWcfFileTransferService
{

    /// <summary>
    /// 切记：引用完类组件后，一定要调用此方法来释放！
    /// </summary>
    public class HuiXiuCheWcfFileTransferServiceDispose
    {
        /// <summary>
        /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            FilesQueue.Dispose();
            FilesBlocksQueue.Dispose();
            DownLoadFilesQueue.Dispose();
            DownLoadFilesBlocksQueue.Dispose();
        }
    }

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

        private static ConcurrentDictionary<string, TransferReceiveFileObj> _serverFilesDic = null;
        private static readonly Object locker = new Object();
        private static ConcurrentDictionary<string, TransferReceiveFileObj> ServerFilesDic
        {
            get
            {
                if (_serverFilesDic == null)
                {
                    lock (locker)
                    {
                        if (_serverFilesDic == null)
                        {
                            _serverFilesDic = new ConcurrentDictionary<string, TransferReceiveFileObj>();
                        }
                    }
                }
                return _serverFilesDic;
            }
        }

          private static bool _alreadyDisposed = false;
          ~FilesQueue()
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
              _serverFilesDic = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public static bool Add(string guId, TransferReceiveFileObj obj)
        {
            TransferReceiveFileObj value = null;
            if (ServerFilesDic.TryGetValue(guId, out value))
            {
               return ServerFilesDic.TryUpdate(guId, obj, value);
            }
            else
            {
                return ServerFilesDic.TryAdd(guId, obj);
            }
        }

        public static bool Get(string guId, out TransferReceiveFileObj obj)
        {
            return ServerFilesDic.TryGetValue(guId, out obj);
        }

        public static bool Remove(string guId, out TransferReceiveFileObj obj)
        {
            return ServerFilesDic.TryRemove(guId, out obj);
        }
    }
}