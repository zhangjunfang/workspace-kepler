using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using SYSModel;
using System.IO;
using System.Timers;
namespace HuiXiuCheWcfFileTransferService
{
    public class FilesBlocksQueue 
    {
    //static Thread writeFileThread = null;
    //private static System.Timers.Timer checkFileTimer = new System.Timers.Timer();
    private static FilesBlocksQueue _instance = null;
    // Creates an syn object.
    private static readonly object SynObject = new object();
    private static readonly object SynWriteObject = new object();
    FilesBlocksQueue()
    {
        //checkFileTimer.Interval = 1000;
        //checkFileTimer.Enabled = true;
        //checkFileTimer.Stop();
        //checkFileTimer.Elapsed +=  new ElapsedEventHandler(OnTimedEvent);
    }

    //private static void OnTimedEvent(object source, ElapsedEventArgs e)
    //{
    //    if (queueLength > 0 && RunFlag)
    //    {
    //        ThreadPool.QueueUserWorkItem(new WaitCallback(NewThreadFunc));
    //    }
    //}


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



        //private static ConcurrentQueue<TransferFileDataObj> _serverFilesBlocksQueue = null;      

        private static ConcurrentBag<TransferFileDataObj> _serverFilesBlocksBag = null;

        private static readonly Object locker = new Object();
        private static readonly Object lockerPath = new Object();
        private static int queueLength = 0;
        private static bool _runFlag = true;
        private static readonly Object lockerRunFlag = new Object();
        private static bool RunFlag
        {
            get
            {
                lock (lockerRunFlag)
                {
                    return _runFlag;
                }
            }
            set
            {
                lock (lockerRunFlag)
                {
                    _runFlag = value;
                }

            }
        }
        //private static ConcurrentQueue<TransferFileDataObj> ServerFilesBlocksQueue
        //{
        //    get
        //    {
        //        if (_serverFilesBlocksQueue == null)
        //        {
        //            lock (locker)
        //            {
        //                if (_serverFilesBlocksQueue == null)
        //                {
        //                    _serverFilesBlocksQueue = new ConcurrentQueue<TransferFileDataObj>();
        //                }
        //            }
        //        }
        //        return _serverFilesBlocksQueue;
        //    }
        //}

        private static ConcurrentBag<TransferFileDataObj> ServerFilesBlocksBag
        {
            get
            {
                if (_serverFilesBlocksBag == null)
                {
                    lock (locker)
                    {
                        if (_serverFilesBlocksBag == null)
                        {
                            _serverFilesBlocksBag = new ConcurrentBag<TransferFileDataObj>();
                        }
                    }
                }
                return _serverFilesBlocksBag;
            }
        }

        private static bool _alreadyDisposed = false;
        ~FilesBlocksQueue()
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
              //_serverFilesBlocksQueue = null;
              _serverFilesBlocksBag = null;
              _savePath = null;
              _instance = null;
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        private static string _savePath = null;
        public string SavePath
        {
            get
            {
                if (_savePath == null)
                {
                    lock (lockerPath)
                    {
                        if (_savePath == null)
                        {
                            _savePath = System.Configuration.ConfigurationManager.AppSettings["savePath"].ToString();
                        }
                    }
                }
                return _savePath;
            }
        }

        public void Add(TransferFileDataObj item)
        {
            //ServerFilesBlocksQueue.Enqueue(item);
            ServerFilesBlocksBag.Add(item);
            Interlocked.Increment(ref queueLength);
            //if (RunFlag)
            //{
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(NewThreadFunc));
            //}
            lock (lockerRunFlag)
            {
                if (item.Order_Num == 1)
                {
                    ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadMethod);
                    Thread myThread = new Thread(ParStart);
                    myThread.IsBackground = true;
                    FileThreadPramObj o = new FileThreadPramObj();
                    o.guId=item.guId;
                    o.myThread = myThread;
                    myThread.Start(o);
                }
            }
            //if (RunFlag)
            //{
            //    writeFileThread = new Thread(new ThreadStart(NewThreadFunc));
            //    writeFileThread.IsBackground = true;
            //    writeFileThread.Start();
            //}
        }

        //ThreadMethod如下:
        public void ThreadMethod(object ParObject)
        {
                FileThreadPramObj o = (FileThreadPramObj)ParObject;
                TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
                FilesQueue.Get(o.guId, out fileObj0);
                //WriteFile2(o.guId, fileObj0, o.myThread);

                FileStream fsWrite = null;
                long fileSize = 0;
                bool firstWrite = true;
                TransferReceiveFileObj fileObjAim = fileObj0;
                while (true)
                {
                start:
                    if (!firstWrite)
                    {
                        TransferReceiveFileObj fileObj1 = new TransferReceiveFileObj();
                        FilesQueue.Get(o.guId, out fileObj1);
                        fileObjAim = fileObj1;

                        if ((System.DateTime.Now - fileObjAim.writeTime).TotalMinutes > 3)//写入等待超时3分钟
                        {
                            if (fsWrite != null)
                            {
                                fsWrite.Close();
                                fsWrite.Dispose();
                            }
                            //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                            fileObjAim.Status = ReceiveFileStatus.WriteWaitTimeOut;
                            FilesQueue.Add(o.guId, fileObjAim);
                            //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                            Thread.Sleep(300);
                            o.myThread.Abort();
                            o.myThread = null;
                        }
                    }


                    for (int i = 0; i < ServerFilesBlocksBag.Count; i++)
                    {
                        TransferFileDataObj block = ServerFilesBlocksBag.ElementAt<TransferFileDataObj>(i);
                        if (block.Order_Num != fileObjAim.NextDataBlockNum)
                        {
                            continue;
                        }
                        if (o.guId != block.guId)
                        {
                            continue;
                        }

                        if (!Directory.Exists(Instance.SavePath + fileObjAim.serverDir))//存放的默认文件夹是否存在
                        {
                            Directory.CreateDirectory(Instance.SavePath + fileObjAim.serverDir);//不存在则创建
                        }
                        string fileFullPath = Path.Combine(Instance.SavePath + fileObjAim.serverDir, fileObjAim.FileName);//合并路径生成文件存放路径
                        //创建文件流，读取流中的数据生成文件
                        //lock (SynWriteObject)
                        //{
                        //fsWriteAppend.Lock(
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
                        //}
                        //else {
                        //    fsWrite.Write(block.FileData, 0, block.CanReadLength);
                        //}
                        //else { 

                        //}
                        // Utility.Log.Log.writeLineToLog("写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                        //fsWrite.Seek((block.Order_Num - 1) * 32768, SeekOrigin.Begin);
                        //if (block.Order_Num == 1)
                        //{
                        //    fsWrite.Seek(0, SeekOrigin.Begin);
                        //}
                        //else {
                        //    fsWrite.Seek(0, SeekOrigin.End);
                        //} 
                        fsWrite.Write(block.FileData, 0, block.CanReadLength);
                        fileSize = fileSize + block.CanReadLength;
                        //}
                        fileObjAim.Status = ReceiveFileStatus.Writing;
                        fileObjAim.writeTime = System.DateTime.Now;
                        if (fileSize == fileObjAim.FileSize)//完整写入
                        {
                            fsWrite.Close();
                            fsWrite.Dispose();
                            //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                            fileObjAim.WriteSize = fileObjAim.WriteSize + block.CanReadLength;
                            fileObjAim.Status = ReceiveFileStatus.WriteEnd;
                            //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                            Thread.Sleep(300);
                            o.myThread.Abort();
                            o.myThread = null;
                        }
                        fileObjAim.WriteSize = fileObjAim.WriteSize + block.CanReadLength;
                        fileObjAim.NextDataBlockNum = fileObjAim.NextDataBlockNum + 1;
                        FilesQueue.Add(o.guId, fileObjAim);
                        //ServerFilesBlocksQueue.TryDequeue(out block);
                        ServerFilesBlocksBag.TryTake(out block);
                        //Thread.Sleep(50);
                    }
                    goto start;
                }
        }

        public bool Get(out TransferFileDataObj obj)
        {
          //return ServerFilesBlocksQueue.TryPeek(out obj);
            return ServerFilesBlocksBag.TryPeek(out obj);
        }

        public bool Remove(out TransferFileDataObj obj)
        {
            //return ServerFilesBlocksQueue.TryDequeue(out obj);
            return ServerFilesBlocksBag.TryTake(out obj);
        }


        private static void NewThreadFunc()
        {
            ////RunFlag = false;
            //TransferFileDataObj item = null;
            //while (!RunFlag)
            //{
            //    if (ServerFilesBlocksQueue.TryPeek(out item))
            //    {
            //        findEndFlagFileBlock();
            //    }
            //    else
            //    {
            //        RunFlag = true;
            //    }
            //}
            //RunFlag = true;
            ////checkFileTimer.Start();

            RunFlag = false;
            //TransferFileDataObj item = null;
            while (true)
            {
                //if (ServerFilesBlocksQueue.TryPeek(out item))
                //{
                    findEndFlagFileBlock();
                //}              
            }
          
    
        }

        private static void findEndFlagFileBlock()
        {
            foreach (TransferFileDataObj block in ServerFilesBlocksBag)
            //foreach (TransferFileDataObj block in ServerFilesBlocksQueue)
            {
                if (block.Order_Num == 1)
                {
                    TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
                    FilesQueue.Get(block.guId, out fileObj0);
                    //Action<string, TransferReceiveFileObj,TransferFileDataObj> generateNewFile = WriteNewFile;
                    //generateNewFile.BeginInvoke(block.guId, fileObj0,block, null, null);
                    WriteNewFile(block.guId, fileObj0, block);
                }
                else
                {
                    //if (block.EndFlag)
                    //{
                    TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
                    FilesQueue.Get(block.guId, out fileObj0);
                    //Action<string, TransferReceiveFileObj,TransferFileDataObj> generateRestFile = WriteRestFile;
                    //generateRestFile.BeginInvoke(block.guId, fileObj0,block, null, null);
                    //}
                    if (block.Order_Num == fileObj0.NextDataBlockNum)
                    {
                        WriteRestFile(block.guId, fileObj0, block); 
                    }                   
                }
            }
        }

        private void findEndFlagFileBlock(Thread myThread)
        {
            //TransferFileDataObj block = new TransferFileDataObj();
            //if(ServerFilesBlocksQueue.TryPeek(out block)){
            //foreach (TransferFileDataObj block in ServerFilesBlocksQueue)
            //{

            //foreach (TransferFileDataObj block in ServerFilesBlocksBag)
            //{
            //    if (block.Order_Num == 1)
            //    {
            //        TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
            //        FilesQueue.Get(block.guId, out fileObj0);
            //        WriteNewFile(block.guId, fileObj0, block,myThread);
            //    }
            //    else
            //    {
            //        TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
            //        FilesQueue.Get(block.guId, out fileObj0);
            //        if (block.Order_Num == fileObj0.NextDataBlockNum)
            //        {
            //            WriteRestFile(block.guId, fileObj0, block,myThread);
            //        }
            //    }
            ////}
            //} 

            foreach (TransferFileDataObj block in ServerFilesBlocksBag)
            {
                if (block.Order_Num == 1)
                {
                    TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
                    FilesQueue.Get(block.guId, out fileObj0);
                    WriteFile2(block.guId, fileObj0, myThread);
                }              
            }            
         
        }


        private static void WriteNewFile(string guId, TransferReceiveFileObj fileObj0,TransferFileDataObj block)
        {
            if (!Directory.Exists(Path.Combine(Instance.SavePath + fileObj0.serverDir)))//存放的默认文件夹是否存在
            {
                Directory.CreateDirectory(Path.Combine(Instance.SavePath + fileObj0.serverDir));//不存在则创建
            }
            string fileFullPath = Path.Combine(Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
            //创建文件流，读取流中的数据生成文件
            int offset = 0;
            //lock (SynWriteObject)
            //{
            FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write);               
                //Utility.Log.Log.writeLineToLog("开始写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                fs.Write(block.FileData, 0, block.CanReadLength);
                fs.Close();
                fs.Dispose();
            //}
                offset = offset + block.FileData.Length;
                fileObj0.Status = ReceiveFileStatus.StartWrite;
                if (block.EndFlag)
                {
                    //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                    fileObj0.Status = ReceiveFileStatus.WriteEnd;                   
                }
                fileObj0.WriteSize = offset;
                fileObj0.NextDataBlockNum = 2;             
                FilesQueue.Add(guId, fileObj0);
                //ServerFilesBlocksQueue.TryDequeue(out block);
                ServerFilesBlocksBag.TryTake(out block); 
        }
        //FileStream fsWriteNew = null;
        private void WriteNewFile(string guId, TransferReceiveFileObj fileObj0, TransferFileDataObj block, Thread myThread)
        {
            if (!Directory.Exists(Path.Combine(Instance.SavePath + fileObj0.serverDir)))//存放的默认文件夹是否存在
            {
                Directory.CreateDirectory(Path.Combine(Instance.SavePath + fileObj0.serverDir));//不存在则创建
            }
            string fileFullPath = Path.Combine(Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
            //创建文件流，读取流中的数据生成文件
            int offset = 0;
            //lock (SynWriteObject)
            //{
            //if (fsWriteNew == null)
            //{
                FileStream fsWriteNew = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write);
            //}
                //Utility.Log.Log.writeLineToLog("开始写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                fsWriteNew.Seek(0, SeekOrigin.Begin);
                fsWriteNew.Write(block.FileData, 0, block.CanReadLength);
                fsWriteNew.Close();
                fsWriteNew.Dispose();
            //}
            offset = offset + block.FileData.Length;
            fileObj0.Status = ReceiveFileStatus.StartWrite;
            if (block.EndFlag)
            {
                //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                fileObj0.Status = ReceiveFileStatus.WriteEnd;
                //Utility.Log.Log.writeLineToLog("写入线程终止--New点：" + block.Order_Num.ToString(), "Write" + guId);
                Thread.Sleep(300);
                myThread.Abort();
                myThread = null;
            }
            fileObj0.WriteSize = offset;
            fileObj0.NextDataBlockNum = 2;
            FilesQueue.Add(guId, fileObj0);
            //ServerFilesBlocksQueue.TryDequeue(out block);
            ServerFilesBlocksBag.TryTake(out block); 
        }


        private static void WriteRestFile(string guId, TransferReceiveFileObj fileObj0,TransferFileDataObj block)
        {
            if (block.Order_Num != fileObj0.NextDataBlockNum)
            {
                return;
            }
            if (!Directory.Exists(Instance.SavePath))//存放的默认文件夹是否存在
            {
                Directory.CreateDirectory(Instance.SavePath);//不存在则创建
            }
            string fileFullPath = Path.Combine(Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
            //创建文件流，读取流中的数据生成文件
            //lock (SynWriteObject)
            //{
            FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                //Utility.Log.Log.writeLineToLog("写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                fs.Write(block.FileData, 0, block.CanReadLength);
                fs.Close();
                fs.Dispose();
            //}
                fileObj0.Status = ReceiveFileStatus.Writing;
                if (block.EndFlag)
                {
                    //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                    fileObj0.Status = ReceiveFileStatus.WriteEnd;               
                }
                fileObj0.WriteSize = fileObj0.WriteSize + block.FileData.Length; ;
                fileObj0.NextDataBlockNum = fileObj0.NextDataBlockNum+1;
                FilesQueue.Add(guId, fileObj0);
                //ServerFilesBlocksQueue.TryDequeue(out block);
                ServerFilesBlocksBag.TryTake(out block); 
        }
        //FileStream fsWriteAppend = null;
        private void WriteRestFile(string guId, TransferReceiveFileObj fileObj0, TransferFileDataObj block0, Thread myThread)
        {
            FileStream fsWriteAppend = null;
            for (int i=0;i<ServerFilesBlocksBag.Count;i++)
            {
                TransferFileDataObj block = ServerFilesBlocksBag.ElementAt <TransferFileDataObj>(i);
                if (block0.Order_Num != fileObj0.NextDataBlockNum)
                {
                    return;
                }
                if (guId != block.guId)
                {
                    return;
                }

                if (!Directory.Exists(Instance.SavePath))//存放的默认文件夹是否存在
                {
                    Directory.CreateDirectory(Instance.SavePath);//不存在则创建
                }
                string fileFullPath = Path.Combine(Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
                //创建文件流，读取流中的数据生成文件
                //lock (SynWriteObject)
                //{
                //fsWriteAppend.Lock(
                if (fsWriteAppend == null)
                {
                fsWriteAppend = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                }
                //else { 

                //}
                // Utility.Log.Log.writeLineToLog("写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                fsWriteAppend.Seek((block.Order_Num - 1) * 32768, SeekOrigin.Begin);
                fsWriteAppend.Write(block.FileData, 0, block.CanReadLength);
                fsWriteAppend.Close();
                fsWriteAppend.Dispose();
                //}
                fileObj0.Status = ReceiveFileStatus.Writing;
                if (block.EndFlag)
                {
                    //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                    fileObj0.Status = ReceiveFileStatus.WriteEnd;
                    //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                    Thread.Sleep(300);
                    myThread.Abort();
                    myThread = null;
                }
                fileObj0.WriteSize = fileObj0.WriteSize + block.FileData.Length; ;
                fileObj0.NextDataBlockNum = fileObj0.NextDataBlockNum + 1;
                FilesQueue.Add(guId, fileObj0);
                //ServerFilesBlocksQueue.TryDequeue(out block);
                ServerFilesBlocksBag.TryTake(out block);
                //Thread.Sleep(50);
            }
        }

        private void WriteFile2(string guId, TransferReceiveFileObj fileObj0, Thread myThread)
        {
            FileStream fsWrite = null;
            long fileSize = 0;
            bool firstWrite = true;
            TransferReceiveFileObj fileObjAim = fileObj0;
            while (true)
            {
            start:
                if (!firstWrite)
                {
                    TransferReceiveFileObj fileObj1 = new TransferReceiveFileObj();
                    FilesQueue.Get(guId, out fileObj1);
                    fileObjAim = fileObj1;

                    if ((System.DateTime.Now - fileObjAim.writeTime).TotalMinutes > 3)//写入等待超时3分钟
                    {
                        if (fsWrite != null)
                        {
                            fsWrite.Close();
                            fsWrite.Dispose();
                        }
                        //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                        fileObjAim.Status = ReceiveFileStatus.WriteWaitTimeOut;
                        FilesQueue.Add(guId, fileObjAim);
                        //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                        Thread.Sleep(300);
                        myThread.Abort();
                        myThread = null;
                    }                    
                }


                for (int i = 0; i < ServerFilesBlocksBag.Count; i++)
                {
                    TransferFileDataObj block = ServerFilesBlocksBag.ElementAt<TransferFileDataObj>(i);
                    if (block.Order_Num != fileObjAim.NextDataBlockNum)
                    {
                        return;
                    }
                    if (guId != block.guId)
                    {
                        return;
                    }

                    if (!Directory.Exists(Instance.SavePath))//存放的默认文件夹是否存在
                    {
                        Directory.CreateDirectory(Instance.SavePath);//不存在则创建
                    }
                    string fileFullPath = Path.Combine(Instance.SavePath + fileObjAim.serverDir, fileObjAim.FileName);//合并路径生成文件存放路径
                    //创建文件流，读取流中的数据生成文件
                    //lock (SynWriteObject)
                    //{
                    //fsWriteAppend.Lock(
                    if (fsWrite == null)
                    {
                        if (block.Order_Num == 1)
                        {
                            fsWrite = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write);
                            firstWrite = false;
                        }
                        else {
                            fsWrite = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                        }
                    }
                    //else { 

                    //}
                    // Utility.Log.Log.writeLineToLog("写入文件：" + block.Order_Num.ToString(), "Write" + guId);
                    //fsWrite.Seek((block.Order_Num - 1) * 32768, SeekOrigin.Begin);
                    //if (block.Order_Num == 1)
                    //{
                    //    fsWrite.Seek(0, SeekOrigin.Begin);
                    //}
                    //else {
                    //    fsWrite.Seek(0, SeekOrigin.End);
                    //}
                    fsWrite.Write(block.FileData, 0, block.CanReadLength);
                    fileSize = fileSize + block.CanReadLength;
                    //}
                    fileObjAim.Status = ReceiveFileStatus.Writing;
                    fileObjAim.writeTime = System.DateTime.Now;
                    if (fileSize == fileObjAim.FileSize)//完整写入
                    {
                        fsWrite.Close();
                        fsWrite.Dispose();
                        //Utility.Log.Log.writeLineToLog("写入文件--尾块：" + block.Order_Num.ToString(), "Write" + guId);
                        fileObjAim.Status = ReceiveFileStatus.WriteEnd;
                        //Utility.Log.Log.writeLineToLog("写入线程终止--Rest点：" + block.Order_Num.ToString(), "Write" + guId);
                        Thread.Sleep(300);
                        myThread.Abort();
                        myThread = null;
                    }
                    fileObjAim.WriteSize = fileObjAim.WriteSize + block.FileData.Length; ;
                    fileObjAim.NextDataBlockNum = fileObjAim.NextDataBlockNum + 1;
                    FilesQueue.Add(guId, fileObjAim);
                    //ServerFilesBlocksQueue.TryDequeue(out block);
                    ServerFilesBlocksBag.TryTake(out block);
                    //Thread.Sleep(50);
                   
                }
                goto start;
            }
         
        }


        //private static void WriteFile(string guId, TransferReceiveFileObj fileObj0)
        //{
        //    TransferFileDataObj[] arry = new TransferFileDataObj[fileObj0.DataBlockCount];
        //    foreach (TransferFileDataObj block in ServerFilesBlocksQueue)
        //    {
        //        if (block.guId == guId)
        //        {
        //            arry[block.Order_Num] = block;
        //        }
        //    }

        //    if (!Directory.Exists(Instance.SavePath))//存放的默认文件夹是否存在
        //    {
        //        Directory.CreateDirectory(Instance.SavePath);//不存在则创建
        //    }

        //    string fileFullPath = Path.Combine(Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径
        //    //创建文件流，读取流中的数据生成文件
        //    using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        int offset = 0;
        //        foreach (TransferFileDataObj byt in arry)
        //        {
        //            fs.Write(byt.FileData, offset, byt.FileData.Length);
        //            offset = offset + byt.FileData.Length;
        //        }                
        //    }
        //    //清除文件队列和文件数据块队列中的数据
        //}
    }
}