using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SYSModel;
using Newtonsoft.Json;
using System.Timers;
using System.Threading;
using Newtonsoft.Json.Converters;
using System.Windows.Forms;
namespace HXCFileTransferCache_Client
{
    public delegate void UpLoadFileHandler<UpLoadFileEventArgs>(object sender, UpLoadFileEventArgs e);
    public delegate void DownLoadFileHandler<DownLoadFileEventArgs>(object sender, DownLoadFileEventArgs e);
    public delegate void UpLoadFileReceiveHandler<UpLoadFileReceiveEventArgs>(object sender, UpLoadFileReceiveEventArgs e);

    public class FileTransferClient
    {
        public static event UpLoadFileHandler<UpLoadFileEventArgs> OnUpLoadFile;
        public static event UpLoadFileReceiveHandler<UpLoadFileReceiveEventArgs> OnUpLoadReceiveFile;
        public static event DownLoadFileHandler<DownLoadFileEventArgs> OnDownLoadFile;
        public static System.Timers.Timer upLoadTimer;
        static object mylock0 = new object();
        public static System.Timers.Timer downLoadTimer;
        static object mylock1 = new object();
        static object mylock2 = new object();
        static object mylock3 = new object();
        static object mylock4 = new object();
        private static List<string> _upLoadReceiveFileId = new List<string>();

        private static string _userId;
        private static string _PCClientCookieStr;

        public static string UserId
        {
            get
            {             
                    return _userId;          
            }
            set
            {                    _userId = value;              

            }
        }

        public static string PCClientCookieStr
        {
            get
            {
                return _PCClientCookieStr;
            }
            set
            {
                _PCClientCookieStr = value;
            }
        }


        public static List<string> UpLoadReceiveFileId
        {
            get { 
                lock(mylock2){
                    return _upLoadReceiveFileId;
                }            
            }
            set {
                lock (mylock2)
                {
                    _upLoadReceiveFileId = value;
                }  
            
            }
        }

        private static void AddIdList(string id)
        {
            lock (mylock2)
            {
                _upLoadReceiveFileId.Add(id);
            }
        }

        public static void RemoveIdList(string id)
        {
            lock (mylock2)
            {
                _upLoadReceiveFileId.Remove(id);
            }
        }

       private static bool _alreadyDisposed = false;
       ~FileTransferClient()
        {
           Dispose(true);
        }
        /// <summary>
       /// 切记：引用完类组件后，一定要调用此方法来释放！
        /// </summary>
        public static void Dispose()
        {
            if (upLoadTimer == null)
            {
              
            }
            else {
                upLoadTimer.Stop();
                upLoadTimer = null;
            }
            if (downLoadTimer == null)
            {

            }
            else
            {
                downLoadTimer.Stop();
                downLoadTimer = null;
            }       
            _upLoadReceiveFileId = null;
            DownLoadFilesBlocksQueue.Dispose();
            DownLoadFilesQueue.Dispose();
            FilesBlocksQueue.Dispose();
            FilesQueue.Dispose();
            GlobalStaticObj.Dispose();        
            Dispose(true);
        }

        public static void InitUpLoadTimer()
        {
            upLoadTimer = new System.Timers.Timer(1000);
            upLoadTimer.Elapsed += new ElapsedEventHandler(upLoadTimer_Elapsed);
            upLoadTimer.Start();
        }

        public static void InitDownLoadTimer()
        {
            downLoadTimer = new System.Timers.Timer(1000);
            downLoadTimer.Elapsed += new ElapsedEventHandler(downLoadTimer_Elapsed);
            downLoadTimer.Start();
        }

        static void upLoadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            if (WCFClientProxy.TestPCClientProxy())
            {
                lock (mylock0)
                {
                    string resultStr = GlobalStaticObj.Instance.proxyFile.GetUploadFileStatus(UpLoadReceiveFileId.ToArray<string>(), UserId, PCClientCookieStr);
                    if (string.IsNullOrEmpty(resultStr))
                    {
                        return;
                    }
                    else if (resultStr == "Session Error")
                    {
                        MessageBox.Show("会话认证异常");
                        return;
                    }
                    List<TransferReceiveFileObj> myList = JsonConvert.DeserializeObject<List<TransferReceiveFileObj>>(resultStr);
                    foreach (TransferReceiveFileObj obj in myList)
                    {
                        UpLoadFileReceiveMsg(new UpLoadFileReceiveEventArgs(obj));
                        if (obj == null)
                        { }
                        else
                        {
                            if (obj.WriteSize == obj.FileSize)
                            {
                                RemoveIdList(obj.guId);
                            }
                        }
                    }
                    if (UpLoadReceiveFileId.Count == 0)
                    {
                        if (upLoadTimer == null)
                        { }
                        else
                        {
                            upLoadTimer.Stop();
                            upLoadTimer = null;
                        }
                    }
                }
            }
        }

        static void downLoadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            lock (mylock1)
            {
                if (DownLoadFilesQueue.Instance.GetLength() < 1)
                {
                    downLoadTimer.Stop();
                }
            }
        }



        public static void StopSendThread()
        {
            FilesBlocksQueue.sendFileThread.Abort();
            FilesBlocksQueue.sendFileThread = null;
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
      
              OnUpLoadFile = null;  
              OnDownLoadFile = null; 
             //GC.SuppressFinalize(this);
          }
          _alreadyDisposed = true;
      }

        public static void UpLoadFileMsg(UpLoadFileEventArgs e)
        {
            if (e.SendFileObj != null)
            {
                e.Raise("FileTransferClient.UpLoadFileMsg", ref OnUpLoadFile);
            }
        }

        public static void UpLoadFileReceiveMsg(UpLoadFileReceiveEventArgs e)
        {
            if (e.UpLoadReceiveFileObj != null)
            {
                e.Raise("FileTransferClient.UpLoadFileReceiveMsg", ref OnUpLoadReceiveFile);
            }
        }

        public static void DownLoadFileMsg(DownLoadFileEventArgs e)
        {
            if (e.DownLoadFileObj != null)
            {
                e.Raise("FileTransferClient.DownLoadFileMsg", ref OnDownLoadFile);
            }
        }

       /// <summary>
       /// 上传文件
       /// </summary>
       /// <param name="filePath">文件路径</param>
       /// <param name="guId"></param>
       /// <param name="serverDir"></param>
        public static void ClientUpLoadFile(string filePath, string guId, string serverDir)
       {
           string fileName = Path.GetFileName(filePath);
           string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
           string fileType = Path.GetExtension(filePath);
           FileInfo fileInfo = new FileInfo(filePath);           
           using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
           {
               TransferSendFileObj fileObj = new TransferSendFileObj();
               fileObj.FileName = fileName;
               fileObj.FilePath = filePath;
               fileObj.FileSize = fileInfo.Length;
               fileObj.ReadSize = 0;
               fileObj.SendSize = 0;
               fileObj.Status = SendFileStatus.StartRead;
               fileObj.serverDir = serverDir;
               FilesQueue.Instance.Add(guId, fileObj);
               AddIdList(guId);
               TransferSendFileObj2 fileObj2 = new TransferSendFileObj2();
               fileObj2.BlockNum = 0;
               fileObj2.DataBlockCount = 0;
               fileObj2.FileName = fileObj.FileName;
               fileObj2.FilePath = fileObj.FilePath;
               fileObj2.FileSize = fileObj.FileSize;
               fileObj2.ReadSize = fileObj.ReadSize;
               fileObj2.SendSize = fileObj.SendSize;
               fileObj2.serverDir = fileObj.serverDir;
               fileObj2.Status = fileObj.Status;
               UpLoadFileMsg(new UpLoadFileEventArgs(guId,fileObj2));
               InitUpLoadTimer();
               SafeReadFile(fs, guId, fileName, filePath);
           }
           //byte[] data = new byte[fs.Length];
           //fs.Read(data, 0, data.Length);
       }


        public static bool ClientDownLoadFile(string path, string fileName, string guId, out string msg, out long FileSize)
        {
            if (WCFClientProxy.TestPCClientProxy())
            {
                if (GlobalStaticObj.Instance.proxyFile.DownLoadFile(path, fileName, guId, UserId, PCClientCookieStr, out msg, out FileSize))
                {
                    DownLoadFileOoj_Client fileObj = new DownLoadFileOoj_Client();
                    fileObj.FileName = fileName;
                    fileObj.guId = guId;
                    fileObj.FileSize = FileSize;
                    fileObj.path = path;
                    fileObj.NextDataBlockNum = 1;
                    fileObj.DownLoadStatus = GetFileDownLoadStatus.waiting;
                    DownLoadFilesQueue.Instance.Add(guId, fileObj);
                    DownLoadFileMsg(new DownLoadFileEventArgs(guId, fileObj));
                    lock (mylock3)
                    {
                        ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadDownLoadMethod);
                        Thread myThread = new Thread(ParStart);
                        myThread.IsBackground = true;
                        FileThreadPramObj o = new FileThreadPramObj();
                        o.guId = guId;
                        o.myThread = myThread;
                        myThread.Start(o);
                    }
                    return true;
                }
                return false;
            }
            else {
                msg = "无法连接服务端";
                FileSize = 0;
                return false;
            
            }
        }



       // public static bool GetFile(string path, string fileName, string guId, out string msg)
       //{
       //    if (GlobalStaticObj.Instance.proxyFile.DownLoadFile(path, fileName, guId, out msg))
       //    {
       //        DownLoadFileOoj_Client fileObj = new DownLoadFileOoj_Client();
       //        fileObj.FileName = fileName;
       //        fileObj.guId = guId;
       //        fileObj.DownLoadStatus = GetFileDownLoadStatus.waiting;
       //        DownLoadFilesQueue.Instance.Add(guId, fileObj);
       //        DownLoadFileMsg(new DownLoadFileEventArgs(guId,fileObj));
       //        loadData(path, fileName, guId);
       //        return true;
       //    }
       //    return false;
       //}

        private static void ThreadDownLoadMethod(object ParObject)
        {
            FileThreadPramObj o = (FileThreadPramObj)ParObject;
            DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
            DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
            fileObj0.DownLoadStatus = GetFileDownLoadStatus.StartDownLoad;
            fileObj0.DownLoadSize = 0;
            DownLoadFilesQueue.Instance.Add(o.guId, fileObj0);
            DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObj0));
            bool firstWrite = true;
            while (true)
            {
                if (!firstWrite)
                {
                    DownLoadFileOoj_Client fileObj1 = new DownLoadFileOoj_Client();
                    DownLoadFilesQueue.Instance.Get(o.guId, out fileObj1);
                    if ((System.DateTime.Now - fileObj1.getFileTime).TotalMinutes > 3)//写入等待超时3分钟
                    {
                        o.myThread.Abort();
                        o.myThread = null;
                    }
                }
                DownLoadFileDataObj obj = new DownLoadFileDataObj();
                if (GlobalStaticObj.Instance.proxyFile.GetSucDataBlock(o.guId,UserId, PCClientCookieStr))
                {
                    string resultStr = GlobalStaticObj.Instance.proxyFile.DownLoadFileDataBlock(o.guId,UserId, PCClientCookieStr);
                    if (!string.IsNullOrEmpty(resultStr))
                    {
                        DownLoadFileDataObj dataObj = JsonConvert.DeserializeObject<DownLoadFileDataObj>(resultStr);
                        if (dataObj != null)
                        {
                            DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
                            firstWrite = false;
                            DownLoadFilesBlocksQueue.Instance.Add(dataObj);
                            fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoading;
                            fileObj0.DownLoadSize = fileObj0.DownLoadSize + dataObj.CanReadLength;
                            fileObj0.getFileTime = System.DateTime.Now;
                            if (fileObj0.DownLoadSize == fileObj0.FileSize)
                            {
                                fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
                            }
                            DownLoadFilesQueue.Instance.Add(o.guId, fileObj0);
                            DownLoadFileMsg(new DownLoadFileEventArgs(o.guId, fileObj0));
                            if (fileObj0.DownLoadSize == fileObj0.FileSize)
                            {
                                Thread.Sleep(300);
                                o.myThread.Abort();
                                o.myThread = null;
                            }
                        }
                    }
                }
            }
        }


       //private static void loadData(string path, string fileName, string guId)
       //{
       //    DownLoadFileDataObj obj = new DownLoadFileDataObj();
       //    if (GlobalStaticObj.Instance.proxyFile.GetSucDataBlock(guId))
       //    {
       //        DownLoadFileOoj_Client fileObj = new DownLoadFileOoj_Client();
       //        fileObj.FileName = fileName;
       //        fileObj.guId = guId;
       //        fileObj.DownLoadStatus = GetFileDownLoadStatus.StartDownLoad;
       //        fileObj.DownLoadSize = 0;
       //        DownLoadFilesQueue.Instance.Add(guId, fileObj);
       //        DownLoadFileMsg(new DownLoadFileEventArgs(guId,fileObj));

       //        getFileDataBlocks(path, fileName, guId, out obj);
       //        if (!obj.EndFlag)
       //        {
       //            loadData(path, fileName, guId);
       //        }
       //        else
       //        {
       //        DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
       //        fileObj0.FileName = fileName;
       //        fileObj0.guId = guId;
       //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
       //        DownLoadFilesQueue.Instance.Add(guId, fileObj0);
       //        DownLoadFileMsg(new DownLoadFileEventArgs(guId,fileObj0));
       //            return;
       //        }
       //    }
       //    else {
       //        loadData(path, fileName, guId);
       //    }
       //}

       // private static void getFileDataBlocks(string path, string fileName, string guId, out DownLoadFileDataObj obj0)
       //{
       //  string resultStr = GlobalStaticObj.Instance.proxyFile.DownLoadFileDataBlock(path, fileName, guId);
       //  DownLoadFileDataObj obj = JsonConvert.DeserializeObject<DownLoadFileDataObj>(resultStr);
       //  if (!string.IsNullOrEmpty(resultStr))
       //  {
       //      obj0 = obj;
       //      DownLoadFilesBlocksQueue.Instance.Add(obj);  
       //      DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
       //        fileObj0.FileName = fileName;
       //        fileObj0.guId = guId;
       //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
       //        fileObj0.DownLoadSize =  fileObj0.DownLoadSize + obj.FileData.Length;
       //        DownLoadFilesQueue.Instance.Add(guId, fileObj0);
       //        DownLoadFileMsg(new DownLoadFileEventArgs(guId,fileObj0));
       //  }
       //  if(obj.EndFlag)
       //  {
       //      obj0 = obj;
       //      DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
       //      fileObj0.FileName = fileName;
       //      fileObj0.guId = guId;
       //      fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
       //      fileObj0.DownLoadSize = fileObj0.DownLoadSize + obj.FileData.Length;
       //      DownLoadFilesQueue.Instance.Add(guId, fileObj0);
       //      DownLoadFileMsg(new DownLoadFileEventArgs(guId, fileObj0));
       //      return; 
       //  }
       //  getFileDataBlocks(path,fileName,guId,out obj0);
       //}

        //private static void getFileDataBlocks(string guId)
        //{
        //    string resultStr = GlobalStaticObj.Instance.proxyFile.DownLoadFileDataBlock(guId);
        //    DownLoadFileDataObj obj = JsonConvert.DeserializeObject<DownLoadFileDataObj>(resultStr);
        //    if (!string.IsNullOrEmpty(resultStr))
        //    {
        //        DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
        //        DownLoadFilesQueue.Instance.Get(obj.guId, out fileObj0);
        //        DownLoadFilesBlocksQueue.Instance.Add(obj);              
        //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
        //        fileObj0.DownLoadSize = fileObj0.DownLoadSize + obj.FileData.Length;
        //        DownLoadFilesQueue.Instance.Add(guId, fileObj0);
        //        DownLoadFileMsg(new DownLoadFileEventArgs(guId, fileObj0));
        //    }
        //    if (obj.EndFlag)
        //    {
        //        obj0 = obj;
        //        DownLoadFileOoj_Client fileObj0 = new DownLoadFileOoj_Client();
        //        fileObj0.FileName = fileName;
        //        fileObj0.guId = guId;
        //        fileObj0.DownLoadStatus = GetFileDownLoadStatus.DownLoadEnd;
        //        fileObj0.DownLoadSize = fileObj0.DownLoadSize + obj.FileData.Length;
        //        DownLoadFilesQueue.Instance.Add(guId, fileObj0);
        //        DownLoadFileMsg(new DownLoadFileEventArgs(guId, fileObj0));
        //        return;
        //    }
        //    getFileDataBlocks(path, fileName, guId, out obj0);
        //}

       

       public static bool ClientSendFileDataBlock(TransferFileDataObj item)
       {
           bool result = false;
           if (WCFClientProxy.TestPCClientProxy())
           {
               TransferSendFileObj fileObj =  new TransferSendFileObj();
               FilesQueue.Instance.Get(item.guId, out fileObj);
               string resultStr = GlobalStaticObj.Instance.proxyFile.ReceiveFile(item.FileData,item.CanReadLength, item.FilePath, item.FileName, item.Order_Num, item.guId, fileObj.serverDir, fileObj.FileSize, item.EndFlag, fileObj.DataBlockCount,UserId, PCClientCookieStr);             
               bool.TryParse(resultStr, out result);
               if (FilesBlocksQueue.PCClientFilesBlocksQueue.IsEmpty)
               {
                   FilesBlocksQueue.RunFlag = true;                   
                   //Utility.Log.Log.writeLineToLog("发送线程终止：" + item.Order_Num.ToString() + " 结果：" + result.ToString(), "Send");
               }
               //Utility.Log.Log.writeLineToLog("发送文件块：" + item.Order_Num.ToString() + " 结果：" + result.ToString(), "Send");
           }
           return result;
       }

       public static void SafeReadFile(Stream stream, string guId, string FileName, string FilePath)
       {
           int bufferLength = 32768;
           //文件数据块编号
           int _order_Num = 0;
           //初始化一个32k的缓存
           //byte[] buffer = new byte[32768];
           byte[] buffer = new byte[bufferLength];
           int byteLength = 0;
           int read = 0;
           int block;
           //每次从流中读取缓存大小的数据，知道读取完所有的流为止
           TransferSendFileObj fileObj0 = new TransferSendFileObj();
           FilesQueue.Instance.Get(guId, out fileObj0);
           fileObj0.Status = SendFileStatus.Reading;
           FilesQueue.Instance.Add(guId, fileObj0);
           //Utility.Log.Log.writeLineToLog("开始读取文件","Read");
           while ((block = stream.Read(buffer, 0, buffer.Length)) > 0)
           {
               byteLength = byteLength + block;
               //重新设定读取位置
               read += block;
               _order_Num = _order_Num + 1;
               //检查是否到达了缓存的边界，检查是否还有可以读取的信息
               if (block == buffer.Length)
               {
                   //压入文件块传输缓存队列
                   TransferFileDataObj fileBlock = new TransferFileDataObj();
                   fileBlock.guId = guId;
                   Array.Copy(buffer, fileBlock.FileData, buffer.Length);
                   fileBlock.FilePath = FilePath;
                   fileBlock.Order_Num = _order_Num;
                   fileBlock.FileName = FileName;
                   fileBlock.CanReadLength = bufferLength;
                   FilesBlocksQueue.Instance.Add(fileBlock);
                   //Utility.Log.Log.writeLineToLog("读取文件块入队列：" + _order_Num.ToString(), "Read");
                   FilesQueue.Instance.Get(guId, out fileObj0);
                   fileObj0.ReadSize = fileObj0.ReadSize + block;
                   fileObj0.Status = SendFileStatus.Reading;
                                

                   // 尝试读取一个字节
                   int nextByte = stream.ReadByte();
                   // 读取失败则说明读取完成可以返回结果
                   if (nextByte == -1)
                   {
                       fileBlock.EndFlag = true;
                       //FilesQueue.Instance.Get(guId, out fileObj0);
                       fileObj0.Status = SendFileStatus.ReadEnd;
                       FilesQueue.Instance.Add(guId, fileObj0);
                       TransferSendFileObj2 fileObj2 = new TransferSendFileObj2();
                       fileObj2.BlockNum = _order_Num;
                       fileObj2.DataBlockCount = _order_Num;
                       fileObj2.FileName = fileObj0.FileName;
                       fileObj2.FilePath = fileObj0.FilePath;
                       fileObj2.FileSize = fileObj0.FileSize;
                       fileObj2.ReadSize = fileObj0.ReadSize;
                       fileObj2.SendSize = fileObj0.SendSize;
                       fileObj2.serverDir = fileObj0.serverDir;
                       fileObj2.Status = fileObj0.Status;
                       UpLoadFileMsg(new UpLoadFileEventArgs(guId, fileObj2));
                   }
                   else
                   {
                       fileBlock.EndFlag = false;
                       fileObj0.Status = SendFileStatus.Reading;
                       TransferSendFileObj2 fileObj2 = new TransferSendFileObj2();
                       fileObj2.BlockNum = _order_Num;
                       fileObj2.DataBlockCount = 0;
                       fileObj2.FileName = fileObj0.FileName;
                       fileObj2.FilePath = fileObj0.FilePath;
                       fileObj2.FileSize = fileObj0.FileSize;
                       fileObj2.ReadSize = fileObj0.ReadSize;
                       fileObj2.SendSize = fileObj0.SendSize;
                       fileObj2.serverDir = fileObj0.serverDir;
                       fileObj2.Status = fileObj0.Status;
                       UpLoadFileMsg(new UpLoadFileEventArgs(guId, fileObj2));
                   }
                   FilesQueue.Instance.Add(guId, fileObj0); 
                   stream.Seek(-1, SeekOrigin.Current);
                 
                   //清空缓存区
                   Array.Clear(buffer, 0, buffer.Length);
                   byteLength = 0;
               }              
           }
           //压入文件块传输缓存队列
           TransferFileDataObj fileBlockEnd = new TransferFileDataObj();
           fileBlockEnd.guId = guId;
           Array.Copy(buffer, fileBlockEnd.FileData, byteLength);
           fileBlockEnd.FilePath = FilePath;
           fileBlockEnd.CanReadLength = byteLength;
           fileBlockEnd.Order_Num = _order_Num;
           fileBlockEnd.FileName = FileName;
           fileBlockEnd.EndFlag = true;
           FilesBlocksQueue.Instance.Add(fileBlockEnd);
           //Utility.Log.Log.writeLineToLog("读取文件--尾块入队列：" + _order_Num.ToString(), "Read");
           //更新Files队列中，对应File的ReadSize    
           FilesQueue.Instance.Get(guId, out fileObj0);
           fileObj0.ReadSize = fileObj0.ReadSize + byteLength;
           fileObj0.Status = SendFileStatus.ReadEnd;
           fileObj0.DataBlockCount = fileBlockEnd.Order_Num;
           FilesQueue.Instance.Add(guId, fileObj0);
           TransferSendFileObj2 fileObj3 = new TransferSendFileObj2();
           fileObj3.BlockNum = _order_Num;
           fileObj3.DataBlockCount = 0;
           fileObj3.FileName = fileObj0.FileName;
           fileObj3.FilePath = fileObj0.FilePath;
           fileObj3.FileSize = fileObj0.FileSize;
           fileObj3.ReadSize = fileObj0.ReadSize;
           fileObj3.SendSize = fileObj0.SendSize;
           fileObj3.serverDir = fileObj0.serverDir;
           fileObj3.Status = fileObj0.Status;
           UpLoadFileMsg(new UpLoadFileEventArgs(guId, fileObj3));
           //清空缓存区
           Array.Clear(buffer, 0, buffer.Length);
       }

       public void SafeRead(Stream stream, byte[] data)
       {
           int offset = 0;
           int remaining = data.Length;
           //只要有剩余的字节就不停的读
           while (remaining > 0)
           {
               int read = stream.Read(data, offset, remaining);
               if (read <= 0)
               {
                   throw new EndOfStreamException("文件读取到" + read.ToString() + "失败！");
               }
               // 减少剩余的字节数
               remaining -= read;
               //增加偏移量
               offset += read;
           }
       }

       public byte[] ReadFully(Stream stream)
       {
           // 初始化一个32k的缓存
           byte[] buffer = new byte[32768];
           using (MemoryStream ms = new MemoryStream())
           { 
             //返回结果后会自动回收调用该对象的Dispose方法释放内存
             //不停的读取
               while (true)
               {
                   int read = stream.Read(buffer, 0, buffer.Length);
                   // 直到读取完最后的3M数据就可以返回结果了
                   if (read <= 0)
                       return ms.ToArray();
                   ms.Write(buffer, 0, read);
               }
           }
       }

       public byte[] Read2Buffer(Stream stream, int BufferLen)
       {
           // 如果指定的无效长度的缓冲区，则指定一个默认的长度作为缓存大小
           if (BufferLen < 1)
           {
               BufferLen = 0x8000;
           }
           // 初始化一个缓存区
           byte[] buffer = new byte[BufferLen];
           int read = 0;
           int block;
           // 每次从流中读取缓存大小的数据，知道读取完所有的流为止
           while ((block = stream.Read(buffer, read, buffer.Length - read)) > 0)
           {
               // 重新设定读取位置
               read += block;
               // 检查是否到达了缓存的边界，检查是否还有可以读取的信息
               if (read == buffer.Length)
               {
                   // 尝试读取一个字节
                   int nextByte = stream.ReadByte();
                   // 读取失败则说明读取完成可以返回结果
                   if (nextByte == -1)
                   {
                       return buffer;
                   }
                   // 调整数组大小准备继续读取
                   byte[] newBuf = new byte[buffer.Length * 2];
                   Array.Copy(buffer, newBuf, buffer.Length);
                   newBuf[read] = (byte)nextByte;
                   buffer = newBuf;// buffer是一个引用（指针），这里意在重新设定buffer指针指向一个更大的内存
                   read++;
               }
           }
           //如果缓存太大则使用ret来收缩前面while读取的buffer，然后直接返回
           byte[] ret = new byte[read];
           Array.Copy(buffer, ret, read);
           return ret;
       }
    }
}

