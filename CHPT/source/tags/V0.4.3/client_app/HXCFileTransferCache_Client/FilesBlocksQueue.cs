using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
namespace HXCFileTransferCache_Client
{
    public class FilesBlocksQueue 
    {
    public static Thread sendFileThread = null;
    private static FilesBlocksQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    FilesBlocksQueue()
    {
    }
    public static FilesBlocksQueue Instance
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
                        _instance = new FilesBlocksQueue();
                    }
                }
            }
            return _instance;
        }
    }


        private static ConcurrentQueue<TransferFileDataObj> _pCClientFilesBlocksQueue = null;
        private static readonly Object locker = new Object();
        private static int queueLength = 0;
        private static bool _runFlag = true;
        private static readonly Object lockerRunFlag = new Object();
        public static bool RunFlag
        {
            get
            {
                lock (lockerRunFlag)
                    {
                        return _runFlag;
                    }
            }
            set {
                lock (lockerRunFlag)
                {
                    _runFlag = value;
                }
            
            }
        }

        private static bool _alreadyDisposed = false;
        ~FilesBlocksQueue()
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
              if (sendFileThread == null)
              {

              }
              else
              {
                  sendFileThread.Abort();
                  sendFileThread = null;
              }
              _pCClientFilesBlocksQueue = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public static ConcurrentQueue<TransferFileDataObj> PCClientFilesBlocksQueue
        {
            get
            {
                if (_pCClientFilesBlocksQueue == null)
                {
                    lock (locker)
                    {
                        if (_pCClientFilesBlocksQueue == null)
                        {
                            _pCClientFilesBlocksQueue = new ConcurrentQueue<TransferFileDataObj>();
                        }
                    }
                }
                return _pCClientFilesBlocksQueue;
            }
        }

        public void Add(TransferFileDataObj item)
        {
            PCClientFilesBlocksQueue.Enqueue(item);
            Interlocked.Increment(ref queueLength);
            //if (RunFlag)
            //{
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(NewThreadFunc));
            //}

            //if (RunFlag)
            //{
            lock (lockerRunFlag)
            {
                if (sendFileThread == null)
                {
                    sendFileThread = new Thread(new ThreadStart(NewThreadFunc));
                    sendFileThread.IsBackground = true;
                    sendFileThread.Start();
                }
            }
            //}
            //if (RunFlag)
            //{
            //    ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadMethod);
            //    Thread myThread = new Thread(ParStart);
            //    myThread.IsBackground = true;
            //    object o = myThread;
            //    myThread.Start(o);
            //}
        }

        public void ThreadMethod(object ParObject)
        {
            while (true)
            {
                NewThreadFunc((Thread)ParObject);
            }
        }

        private static void NewThreadFunc()
        {
            //RunFlag = false;
            while (true)
            {
                TransferFileDataObj item = null;
                if (PCClientFilesBlocksQueue.TryPeek(out item))
                {
                    if (item != null)
                    {
                        if (FileTransferClient.ClientSendFileDataBlock(item))
                        {
                            TransferFileDataObj block = null;
                            if (PCClientFilesBlocksQueue.TryDequeue(out block))
                            {
                                Interlocked.Decrement(ref queueLength);
                                TransferSendFileObj fileObj = new TransferSendFileObj();
                                FilesQueue.Instance.Get(block.guId, out fileObj);
                                if (fileObj != null && block != null)
                                {

                                    fileObj.SendSize = fileObj.SendSize + block.CanReadLength;
                                    if (item.Order_Num == 1)
                                    {
                                        fileObj.Status = SendFileStatus.StartSend;
                                    }
                                    else
                                    {
                                        fileObj.Status = SendFileStatus.Sending;
                                    }
                                    if (fileObj.FileSize == fileObj.SendSize)
                                    {
                                        fileObj.Status = SendFileStatus.SendEnd;
                                    }
                                    FilesQueue.Instance.Add(block.guId, fileObj);
                                    TransferSendFileObj2 fileObj2 = new TransferSendFileObj2();
                                    fileObj2.BlockNum = item.Order_Num;
                                    fileObj2.DataBlockCount = fileObj.DataBlockCount;
                                    fileObj2.FileName = fileObj.FileName;
                                    fileObj2.FilePath = fileObj.FilePath;
                                    fileObj2.FileSize = fileObj.FileSize;
                                    fileObj2.ReadSize = fileObj.ReadSize;
                                    fileObj2.SendSize = fileObj.SendSize;
                                    fileObj2.serverDir = fileObj.serverDir;
                                    fileObj2.Status = fileObj.Status;
                                    FileTransferClient.UpLoadFileMsg(new UpLoadFileEventArgs(block.guId, fileObj2));
                                }
                            }
                        }
                    }
                }
            }        
        }

        private static void NewThreadFunc(Thread ParObject)
        {
            RunFlag = false;
            while (!RunFlag)
            {              
                TransferFileDataObj item = null;
                if (PCClientFilesBlocksQueue.TryPeek(out item))
                {
                    if (item != null)
                    {
                        if (FileTransferClient.ClientSendFileDataBlock(item))
                        {
                            TransferFileDataObj block = null;
                            if (PCClientFilesBlocksQueue.TryDequeue(out block))
                            {
                                Interlocked.Decrement(ref queueLength);
                                TransferSendFileObj fileObj = new TransferSendFileObj();
                                FilesQueue.Instance.Get(block.guId, out fileObj);
                                if (fileObj != null && block != null)
                                {
                                    fileObj.SendSize = fileObj.SendSize + block.FileData.Length;
                                    if (item.Order_Num == 1)
                                    {
                                        fileObj.Status = SendFileStatus.StartSend;
                                    }
                                    else {
                                        fileObj.Status = SendFileStatus.Sending;
                                    }
                                    if (fileObj.FileSize == fileObj.SendSize)
                                    {
                                        fileObj.Status = SendFileStatus.SendEnd;
                                    }
                                    FilesQueue.Instance.Add(block.guId, fileObj);
                                    TransferSendFileObj2 fileObj2 = new TransferSendFileObj2();
                                    fileObj2.BlockNum = item.Order_Num;
                                    fileObj2.DataBlockCount = fileObj.DataBlockCount;
                                    fileObj2.FileName = fileObj.FileName;
                                    fileObj2.FilePath = fileObj.FilePath;
                                    fileObj2.FileSize = fileObj.FileSize;
                                    fileObj2.ReadSize = fileObj.ReadSize;
                                    fileObj2.SendSize = fileObj.SendSize;
                                    fileObj2.serverDir = fileObj.serverDir;
                                    fileObj2.Status = fileObj.Status;
                                    FileTransferClient.UpLoadFileMsg(new UpLoadFileEventArgs(block.guId, fileObj2));
                                }
                            }
                        }
                    }
                }
            }
            //RunFlag = true;
            ////Utility.Log.Log.writeLineToLog("发送线程终止000：" , "Send");
            ////Thread.Sleep(3000);
            //ParObject.Abort();
            //ParObject = null;
        }

        //private static void NewThreadFunc(object state)
        //private static void NewThreadFunc()
        //{
        //    //RunFlag = false;
        //    //while (!RunFlag)
        //    //{
        //    //    TransferFileDataObj item = null;
        //    //    if (PCClientFilesBlocksQueue.TryPeek(out item))
        //    //    {
        //    //        if (item != null)
        //    //        {
        //    //            //发送文件数据块
        //    //            if (FileTransferClient.ClientSendFileDataBlock(item))
        //    //            {
        //    //                if (PCClientFilesBlocksQueue.TryDequeue(out item))
        //    //                {
        //    //                    Interlocked.Decrement(ref queueLength);
        //    //                    TransferSendFileObj fileObj = new TransferSendFileObj();
        //    //                    fileObj.SendSize = fileObj.SendSize + item.FileData.Length;
        //    //                    FilesQueue.Instance.Add(item.guId, fileObj);
        //    //                    FileTransferClient.UpLoadFileMsg(new UpLoadFileEventArgs(item.guId, fileObj));
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //    else {
        //    //        RunFlag = true;
        //    //    }
        //    //}
        //    //RunFlag = true;

        //    RunFlag = false;
        //    while (!RunFlag)
        //    {
        //        //TransferFileDataObj item = null;
        //        //if (PCClientFilesBlocksQueue.TryPeek(out item))
        //        //{
        //        //    if (item != null)
        //        //    {
        //        //        //发送文件数据块
        //        //        if (FileTransferClient.ClientSendFileDataBlock(item))
        //        //        {
        //        //            if (PCClientFilesBlocksQueue.TryDequeue(out item))
        //        //            {
        //        //                Interlocked.Decrement(ref queueLength);
        //        //                TransferSendFileObj fileObj = new TransferSendFileObj();
        //        //                fileObj.SendSize = fileObj.SendSize + item.FileData.Length;
        //        //                FilesQueue.Instance.Add(item.guId, fileObj);
        //        //                FileTransferClient.UpLoadFileMsg(new UpLoadFileEventArgs(item.guId, fileObj));
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        TransferFileDataObj item = null;
        //        foreach (TransferFileDataObj block in PCClientFilesBlocksQueue)
        //        {
        //            item = block;
        //            if (FileTransferClient.ClientSendFileDataBlock(item))
        //            {
        //                if (PCClientFilesBlocksQueue.TryDequeue(out item))
        //                {
        //                    Interlocked.Decrement(ref queueLength);
        //                    TransferSendFileObj fileObj = new TransferSendFileObj();
        //                    fileObj.SendSize = fileObj.SendSize + item.FileData.Length;
        //                    FilesQueue.Instance.Add(item.guId, fileObj);
        //                    FileTransferClient.UpLoadFileMsg(new UpLoadFileEventArgs(item.guId, fileObj));
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
