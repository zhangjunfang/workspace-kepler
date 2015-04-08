using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
using System.IO;
using System.Windows.Forms;
namespace HXCFileTransferCache_Client
{
    public class DownLoadFilesBlocksQueue
    {
    private static DownLoadFilesBlocksQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    private static readonly object mylock3 = new object();
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

    private static ConcurrentBag<DownLoadFileDataObj> _pCClientDownLoadFileBlocksBag = null;
        //private static ConcurrentQueue<DownLoadFileDataObj> _pCClientDownLoadFileBlocksQueue = null;
        private static readonly Object locker = new Object();
        private static readonly Object lockerPath = new Object();
        private static int queueLength = 0;
        private static bool runFlag = false;
        private static bool _alreadyDisposed = false;
        ~DownLoadFilesBlocksQueue()
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
              _pCClientDownLoadFileBlocksBag = null;
              _savePath = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public ConcurrentBag<DownLoadFileDataObj> PCClientDownLoadFileBlocksBag
        {
            get
            {
                if (_pCClientDownLoadFileBlocksBag == null)
                {
                    lock (locker)
                    {
                        if (_pCClientDownLoadFileBlocksBag == null)
                        {
                            _pCClientDownLoadFileBlocksBag = new ConcurrentBag<DownLoadFileDataObj>();
                        }
                    }
                }
                return _pCClientDownLoadFileBlocksBag;
            }
        }

        //private static ConcurrentQueue<DownLoadFileDataObj> PCClientDownLoadFileBlocksQueue
        //{
        //    get
        //    {
        //        if (_pCClientDownLoadFileBlocksQueue == null)
        //        {
        //            lock (locker)
        //            {
        //                if (_pCClientDownLoadFileBlocksQueue == null)
        //                {
        //                    _pCClientDownLoadFileBlocksQueue = new ConcurrentQueue<DownLoadFileDataObj>();
        //                }
        //            }
        //        }
        //        return _pCClientDownLoadFileBlocksQueue;
        //    }
        //}

        private static string _savePath = null;
        private string SavePath
        {
            get
            {
                if (_savePath == null)
                {
                    lock (lockerPath)
                    {
                        if (_savePath == null)
                        {
                            _savePath = System.Configuration.ConfigurationManager.AppSettings["TempPath"].ToString();
                        }
                    }
                }
                return _savePath;
            }
        }

        public void Add(DownLoadFileDataObj item)
        {
            PCClientDownLoadFileBlocksBag.Add(item);
            Interlocked.Increment(ref queueLength);
            lock (mylock3)
            {
                if (item.Order_Num == 1)
                {
                    ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadWriteFileMethod);
                    Thread myThread = new Thread(ParStart);
                    myThread.IsBackground = true;
                    FileThreadPramObj o = new FileThreadPramObj();
                    o.guId = item.guId;
                    o.myThread = myThread;
                    myThread.Start(o);
                }
            }
        }

        //ThreadMethod如下:
        public void ThreadWriteFileMethod(object ParObject)
        {
            FileThreadPramObj o = (FileThreadPramObj)ParObject;
            DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
            DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
            fileObj0.Status = GetFileWriteStatus.StartWrite;
            DownLoadFilesQueue.Instance.Add(o.guId, fileObj0);
            HXCFileTransferCache_Client.FileTransferClient.DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObj0));
            FileStream fsWrite = null;
            long fileSize = 0;
            bool firstWrite = true;
            DownLoadFileOoj_Client fileObjAim = fileObj0;
            while (true)
            {
            start:
                if (!firstWrite)
                {
                    DownLoadFileOoj_Client fileObj1 = new DownLoadFileOoj_Client();
                    DownLoadFilesQueue.Instance.Get(o.guId, out fileObj1);
                    fileObjAim = fileObj1;

                    if ((System.DateTime.Now - fileObjAim.writeTime).TotalMinutes > 3)//写入等待超时3分钟
                    {
                        if (fsWrite != null)
                        {
                            fsWrite.Close();
                            fsWrite.Dispose();
                        }
                        //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                        fileObjAim.Status = GetFileWriteStatus.WriteWaitTimeOut;
                        DownLoadFilesQueue.Instance.Add(o.guId, fileObjAim);
                        HXCFileTransferCache_Client.FileTransferClient.DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObjAim));
                        //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                        Thread.Sleep(300);
                        o.myThread.Abort();
                        o.myThread = null;
                    }
                }
            for (int i = 0; i < PCClientDownLoadFileBlocksBag.Count; i++)
                {
                    DownLoadFileDataObj block = PCClientDownLoadFileBlocksBag.ElementAt<DownLoadFileDataObj>(i);
                    if (block.Order_Num != fileObjAim.NextDataBlockNum)
                    {
                        continue;
                    }
                    if (o.guId != block.guId)
                    {
                        continue;
                    }
                    if (!Directory.Exists(DownLoadFilesBlocksQueue.Instance.SavePath))//存放的默认文件夹是否存在
                    {
                        Directory.CreateDirectory(Instance.SavePath);//不存在则创建
                    }
                    string fileFullPath = Path.Combine(Application.StartupPath + "\\"+DownLoadFilesBlocksQueue.Instance.SavePath, fileObjAim.FileName);//合并路径生成文件存放路径 
                    if (fsWrite == null)
                    {
                        if (block.Order_Num == 1)
                        {
                            fsWrite = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write);
                            firstWrite = false;
                        }
                        else
                        {
                            fsWrite = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                        }
                    }                  
                    fsWrite.Write(block.FileData, 0, block.CanReadLength);
                    fileSize = fileSize + block.CanReadLength;    
                    fileObjAim.Status = GetFileWriteStatus.Writing;
                    fileObjAim.writeTime = System.DateTime.Now;
                    if (fileSize == fileObjAim.FileSize)//完整写入
                    {
                        fsWrite.Close();
                        fsWrite.Dispose();
                        //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                        fileObjAim.WriteSize = fileObjAim.WriteSize + block.CanReadLength;
                        fileObjAim.Status = GetFileWriteStatus.WriteEnd;
                        DownLoadFilesQueue.Instance.Add(o.guId, fileObjAim);
                        HXCFileTransferCache_Client.FileTransferClient.DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObjAim));
                        GlobalStaticObj.Instance.proxyFile.DeleteServerFileId(o.guId, FileTransferClient.UserId, FileTransferClient.PCClientCookieStr);
                        DownLoadFilesQueue.Instance.Remove(o.guId, out fileObjAim);
                        //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                        Thread.Sleep(300);
                        o.myThread.Abort();
                        o.myThread = null;
                    }
                    fileObjAim.WriteSize = fileObjAim.WriteSize + block.CanReadLength;
                    fileObjAim.NextDataBlockNum = fileObjAim.NextDataBlockNum + 1;
                    DownLoadFilesQueue.Instance.Add(o.guId, fileObjAim);
                    HXCFileTransferCache_Client.FileTransferClient.DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObjAim));
                    //ServerFilesBlocksQueue.TryDequeue(out block);
                    PCClientDownLoadFileBlocksBag.TryTake(out block);
                    //Thread.Sleep(50);
                }
                goto start;
            }
        }



        //private static void NewThreadFunc(object state)
        //{
        //    runFlag = true;
        //    DownLoadFileDataObj item = null;
        //    while (PCClientDownLoadFileBlocksQueue.TryPeek(out item))
        //    {
        //        ////发送文件数据块
        //        //if (FileTransferClient.ClientSendFileDataBlock(item))
        //        //{
        //        //    PCClientFilesBlocksQueue.TryDequeue(out item);
        //        //    Interlocked.Decrement(ref queueLength);
        //        //}
        //    }
        //    runFlag = false;
        //}

        //private static void findEndFlagFileBlock()
        //{
        //    foreach (DownLoadFileDataObj block in PCClientDownLoadFileBlocksQueue)
        //    {
        //        if (block.Order_Num == 0)
        //        {
        //            DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
        //            DownLoadFilesQueue.Instance.Get(block.guId, out fileObj0);
        //            Action<string, DownLoadFileOoj_Client, DownLoadFileDataObj> generateNewFile = WriteNewFile;
        //            generateNewFile.BeginInvoke(block.guId, fileObj0, block, null, null);
        //        }
        //        else
        //        {
        //            //if (block.EndFlag)
        //            //{
        //            DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
        //            DownLoadFilesQueue.Instance.Get(block.guId, out fileObj0);
        //            Action<string, DownLoadFileOoj_Client, DownLoadFileDataObj> generateRestFile = WriteRestFile;
        //            generateRestFile.BeginInvoke(block.guId, fileObj0, block, null, null);
        //            //}
        //        }
        //    }
        //}

        //private static void WriteNewFile(string guId, DownLoadFileOoj_Client fileObj0, DownLoadFileDataObj block)
        //{
        //    string tempPath = Application.StartupPath + "\\" + SavePath;
        //    if (!Directory.Exists(tempPath))//存放的默认文件夹是否存在
        //    {
        //        Directory.CreateDirectory(tempPath);//不存在则创建
        //    }
        //    string fileFullPath = Path.Combine(tempPath, fileObj0.FileName);//合并路径生成文件存放路径
        //    //创建文件流，读取流中的数据生成文件
        //    using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        int offset = 0;
        //        fs.Write(block.FileData, offset, block.FileData.Length);
        //        offset = offset + block.FileData.Length;
        //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.StartWrite;
        //        if (block.EndFlag)
        //        {
        //            fileObj0.DownLoadStatus = GetFileDownLoadStatus.WriteEnd;
        //        }
        //        fileObj0.WriteSize = offset;
        //        fileObj0.NextDataBlockNum = 1;
        //        int i = 0;
        //        while (!DownLoadFilesQueue.Instance.Add(guId, fileObj0))
        //        {
        //            i++;
        //            if (i > 2)
        //            {
        //                break;
        //            }
        //            continue;
        //        }
        //        DownLoadFileDataObj block0 = new DownLoadFileDataObj();
        //        i = 0;
        //        while (!PCClientDownLoadFileBlocksQueue.TryDequeue(out block0))
        //        {
        //            i++;
        //            if (i > 2)
        //            {
        //                break;
        //            }
        //            continue;
        //        }
        //    }

        //}

        //private static void WriteRestFile(string guId, DownLoadFileOoj_Client fileObj0, DownLoadFileDataObj block)
        //{
        //    if (fileObj0.NextDataBlockNum != block.Order_Num)
        //    {
        //        return;
        //    }
        //    string tempPath = Application.StartupPath + "\\" + SavePath;
        //    if (!Directory.Exists(tempPath))//存放的默认文件夹是否存在
        //    {
        //        Directory.CreateDirectory(tempPath);//不存在则创建
        //    }
        //    string fileFullPath = Path.Combine(tempPath, fileObj0.FileName);//合并路径生成文件存放路径
        //    //创建文件流，读取流中的数据生成文件
        //    using (FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.None))
        //    {
        //        fs.Write(block.FileData, fileObj0.WriteSize, block.FileData.Length);
        //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.Writeing;
        //        if (block.EndFlag)
        //        {
        //            fileObj0.DownLoadStatus = GetFileDownLoadStatus.WriteEnd;
        //        }
        //        fileObj0.WriteSize = fileObj0.WriteSize + block.FileData.Length; ;
        //        fileObj0.NextDataBlockNum = fileObj0.NextDataBlockNum + 1;
        //        int i = 0;
        //        while (!DownLoadFilesQueue.Instance.Add(guId, fileObj0))
        //        {
        //            i++;
        //            if (i > 2)
        //            {
        //                break;
        //            }
        //            continue;
        //        }
        //        DownLoadFileDataObj block0 = new DownLoadFileDataObj();
        //        i = 0;
        //        while (!PCClientDownLoadFileBlocksQueue.TryDequeue(out block0))
        //        {
        //            i++;
        //            if (i > 2)
        //            {
        //                break;
        //            }
        //            continue;
        //        }
        //    }
        //}

        //private static void WriteFile(string guId, TransferReceiveFileObj fileObj0)
        //{
        //    DownLoadFileDataObj[] arry = new DownLoadFileDataObj[fileObj0.DataBlockCount];
        //    foreach (DownLoadFileDataObj block in PCClientDownLoadFileBlocksQueue)
        //    {
        //        if (block.guId == guId)
        //        {
        //            arry[block.Order_Num] = block;
        //        }
        //    }

        //    if (!Directory.Exists(SavePath))//存放的默认文件夹是否存在
        //    {
        //        Directory.CreateDirectory(SavePath);//不存在则创建
        //    }

        //    string fileFullPath = Path.Combine(SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
        //    //创建文件流，读取流中的数据生成文件
        //    using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        int offset = 0;
        //        foreach (DownLoadFileDataObj byt in arry)
        //        {
        //            fs.Write(byt.FileData, offset, byt.FileData.Length);
        //            offset = offset + byt.FileData.Length;
        //        }
        //    }
        //    //清除文件队列和文件数据块队列中的数据
        //}
    }
}
