using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
using System.IO;
namespace HuiXiuCheWcfFileTransferService
{
    public class DownLoadFilesBlocksQueue 
    {
    private static DownLoadFilesBlocksQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    DownLoadFilesBlocksQueue()
    {
    }
    public static DownLoadFilesBlocksQueue Instance
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
                        _instance = new DownLoadFilesBlocksQueue();
                    }
                }
            }
            return _instance;
        }
    }
    private static ConcurrentBag<DownLoadFileDataObj> _serverDownLoadFileBlocksBag = null;
    //  private static ConcurrentQueue<DownLoadFileDataObj> _serverDownLoadFileBlocksQueue = null;
        private static readonly Object locker = new Object();
        private static readonly Object lockerPath = new Object();

         private static bool _alreadyDisposed = false;
         ~DownLoadFilesBlocksQueue()
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
              //_serverDownLoadFileBlocksQueue = null;
              _serverDownLoadFileBlocksBag = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public ConcurrentBag<DownLoadFileDataObj> ServerDownLoadFileBlocksBag
        {
            get
            {
                if (_serverDownLoadFileBlocksBag == null)
                {
                    lock (locker)
                    {
                        if (_serverDownLoadFileBlocksBag == null)
                        {
                            _serverDownLoadFileBlocksBag = new ConcurrentBag<DownLoadFileDataObj>();
                        }
                    }
                }
                return _serverDownLoadFileBlocksBag;
            }
        }

        //public ConcurrentQueue<DownLoadFileDataObj> ServerDownLoadFileBlocksQueue
        //{
        //    get
        //    {
        //        if (_serverDownLoadFileBlocksQueue == null)
        //        {
        //            lock (locker)
        //            {
        //                if (_serverDownLoadFileBlocksQueue == null)
        //                {
        //                    _serverDownLoadFileBlocksQueue = new ConcurrentQueue<DownLoadFileDataObj>();
        //                }
        //            }
        //        }
        //        return _serverDownLoadFileBlocksQueue;
        //    }
        //}
        public void Add(DownLoadFileDataObj item)
        {
            ServerDownLoadFileBlocksBag.Add(item);           
        }
        public bool Get(out DownLoadFileDataObj obj)
        {
            return ServerDownLoadFileBlocksBag.TryPeek(out obj);
        }
        public bool Remove(out DownLoadFileDataObj obj)
        {
            return ServerDownLoadFileBlocksBag.TryTake(out obj);
        }
    }
}