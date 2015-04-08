using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using HuiXiuCheWcfFileTransferContract;
using SYSModel;
using System.IO;
using Newtonsoft.Json.Converters;
using System.Threading;
using System.Collections.Concurrent;
namespace HuiXiuCheWcfFileTransferService
{
    
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HXCWCFFileTransferService : IHXCWCFFileTransferService
    {
        #region
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        #endregion
        static object mylock0 = new object();
        public string TestConnect()
        {
            return "1";
        }

        private static string _savePath = null;
        public static string SavePath
        {
            get
            {
                if (_savePath == null)
                {                   
                 _savePath = System.Configuration.ConfigurationManager.AppSettings["savePath"].ToString();                     
                }
                return _savePath;
            }
        }

        private static ConcurrentDictionary<string, Thread> guidFileDic = new ConcurrentDictionary<string, Thread>();

        //返回的是成功删除的文件List<DeleteServerFileObj>,会话异常则返回"Session Error";
        public string DeleteServerFiles(string FilesJsonStr, string UserID, string PCClientCookieStr)
        {
            if (SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                List<DeleteServerFileObj> myList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DeleteServerFileObj>>(FilesJsonStr);
                List<DeleteServerFileObj> List0 = new List<DeleteServerFileObj>();
                foreach (DeleteServerFileObj obj in myList)
                {
                    string fileFullPath = Path.Combine(SavePath + obj.ServerDir, obj.fileName);
                    //创建文件流，读取流中的数据生成文件
                    if (File.Exists(fileFullPath))
                    {
                        File.Delete(fileFullPath);
                        List0.Add(obj);
                    }
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(List0);
            }
            else
            {
                return "Session Error";
            }        
        }


        //删除下载文件队列对应的文件Id,会话异常则返回"Session Error";
        public string DeleteServerFileId(string DownFileId, string UserID, string PCClientCookieStr)
        {
            if (SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {   
            DownLoadFileOoj_Server fileObj = new DownLoadFileOoj_Server();
            bool bResult = DownLoadFilesQueue.Instance.Remove(DownFileId, out fileObj);
            return Newtonsoft.Json.JsonConvert.SerializeObject(bResult);
            }
            else
            {
                return "Session Error";
            }        
        }



        public string ReceiveFile2(byte[] fileDataArry, int CanReadLength, string fileName, string guId, string serverDir, bool EndFlag)
        {

            //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadMethod);
            //Thread myThread = new Thread(ParStart);
            //myThread.IsBackground = true;
            //FileThreadObj thObj = new FileThreadObj();
            //thObj.CanReadLength = CanReadLength;
            //thObj.fileDataArry = fileDataArry;
            //thObj.fileName = fileName;
            //thObj.guId = guId;
            //thObj.serverDir = serverDir;
            //thObj.EndFlag = EndFlag;
            //thObj.myThread = myThread;
            //object o = thObj;
            //myThread.Start(o);           

            string result = string.Empty;
            result = "false";
            if (!Directory.Exists(Path.Combine(SavePath + serverDir)))
            {
                Directory.CreateDirectory(Path.Combine(SavePath + serverDir));
            }
            string fileFullPath = Path.Combine(SavePath + serverDir, fileName);
            //创建文件流，读取流中的数据生成文件
            if (!File.Exists(fileFullPath))
            {           
                        FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                        fs.Write(fileDataArry, 0, CanReadLength);
                        fs.Close();
                        fs.Dispose();
                        result = "true";    
            }
            else
            {              
                        FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.None);
                        fs.Write(fileDataArry, 0, CanReadLength);
                        fs.Close();
                        fs.Dispose();
                        result = "true";          
            }
            if (EndFlag)
            {

            }
            return result;
        }

        //private static bool occupyFlag = true;
        //public string ReceiveFile2(byte[] fileDataArry,int CanReadLength, string fileName, string guId, string serverDir, bool EndFlag)
        //{

        //    //ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadMethod);
        //    //Thread myThread = new Thread(ParStart);
        //    //myThread.IsBackground = true;
        //    //FileThreadObj thObj = new FileThreadObj();
        //    //thObj.CanReadLength = CanReadLength;
        //    //thObj.fileDataArry = fileDataArry;
        //    //thObj.fileName = fileName;
        //    //thObj.guId = guId;
        //    //thObj.serverDir = serverDir;
        //    //thObj.EndFlag = EndFlag;
        //    //thObj.myThread = myThread;
        //    //object o = thObj;
        //    //myThread.Start(o);           
            
        //    string result = string.Empty;
        //    result = "false";
        //    if (!Directory.Exists(Path.Combine(SavePath + serverDir)))
        //    {
        //        Directory.CreateDirectory(Path.Combine(SavePath + serverDir));
        //    }
        //    string fileFullPath = Path.Combine(SavePath + serverDir, fileName);
        //    //创建文件流，读取流中的数据生成文件
        //    if (!File.Exists(fileFullPath))
        //    {
        //        while (occupyFlag)
        //        {
        //            try
        //            {
        //                FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //                fs.Write(fileDataArry, 0, CanReadLength);
        //                fs.Close();
        //                fs.Dispose();
        //                result = "true";
        //                occupyFlag = false;
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        occupyFlag = true;
        //    }
        //    else {
        //        //while (guidFileDic[guId])
        //        //{
        //        //    continue;
        //        //}
        //        //guidFileDic.TryUpdate(guId, true, false); 
        //        while (occupyFlag)
        //        {
        //            try
        //            {
        //                FileStream fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write, FileShare.None);
        //                fs.Write(fileDataArry, 0, CanReadLength);
        //                fs.Close();
        //                fs.Dispose();
        //                result = "true";
        //                occupyFlag = false;
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    if (EndFlag)
        //    {

        //    }
        //    return result;
        //}


        public string GetUploadFileStatus(string[] fileGuId,string UserID, string PCClientCookieStr)
        {
            if (SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                string result = string.Empty;
                List<TransferReceiveFileObj> myList = new List<TransferReceiveFileObj>();
                TransferReceiveFileObj obj = null;
                foreach (string id in fileGuId)
                {
                    FilesQueue.Get(id, out obj);
                    myList.Add(obj);
                }               
                return Newtonsoft.Json.JsonConvert.SerializeObject(myList);
            }
            else
            {
                return "Session Error";
            }
        }


        public string ReceiveFile(byte[] fileDataArry, int CanReadLength, string path, string fileName, int Order_Num, string guId, string serverDir, long fileSize, bool EndFlag, int DataBlockCount, string UserID, string PCClientCookieStr)
        {
            if (!SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                return "Session Error";
            }
            string result = string.Empty;
            result = "false";
            TransferReceiveFileObj fileObj0 = new TransferReceiveFileObj();
            if (!FilesQueue.Get(guId, out fileObj0))
            {
                fileObj0 = new TransferReceiveFileObj();
                fileObj0.guId = guId;
                fileObj0.FileName = fileName;
                fileObj0.FilePath = path;
                fileObj0.FileSize = fileSize;
                fileObj0.ReceiveSize = 0;
                fileObj0.Status = ReceiveFileStatus.StartReceive;
                fileObj0.DataBlockCount = DataBlockCount;
                fileObj0.serverDir = serverDir;
                fileObj0.NextDataBlockNum = 1;
                FilesQueue.Add(guId, fileObj0);
            }
            //int len = fileDataArry.Length;
            TransferFileDataObj fileBlock = new TransferFileDataObj();
            fileBlock.guId = guId;
            Array.Copy(fileDataArry, fileBlock.FileData, CanReadLength);
            fileBlock.CanReadLength = CanReadLength;
            fileBlock.FilePath = path;
            fileBlock.Order_Num = Order_Num;
            FilesBlocksQueue.Instance.Add(fileBlock);
            if (!EndFlag)
            {
                //Utility.Log.Log.writeLineToLog("接收文件--入队列：" + Order_Num.ToString(), "Receive");
            }
            FilesQueue.Get(guId, out fileObj0);
            if (EndFlag)
            {
                fileObj0.Status = ReceiveFileStatus.ReceiveEnd;
                //Utility.Log.Log.writeLineToLog("接收文件--尾块入队列：" + Order_Num.ToString(), "Receive");
            }
            else {
                if (fileObj0.Status == ReceiveFileStatus.StartReceive)
                {
                    fileObj0.Status = ReceiveFileStatus.Receiving;
                }
            }
            fileObj0.ReceiveSize = fileObj0.ReceiveSize + CanReadLength;
            FilesQueue.Add(guId, fileObj0);
            result = "true";
            return result;
        }

        public bool DownLoadFile(string path, string fileName, string guId, string UserID, string PCClientCookieStr, out string msg, out long FileSize)
        {
            if (!SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                msg = "Session Error";
                FileSize = 0;
                return false;
            }
            bool result = false;
            try
            {
                string fileFullPath = Path.Combine(FilesBlocksQueue.Instance.SavePath + path, fileName);//合并路径生成文件存放路径 
                if (!File.Exists(fileFullPath))
                {
                    msg = "File not find";
                    FileSize = 0;
                    return result;
                }
                FileInfo fileInfo = new FileInfo(fileFullPath);
                FileSize = fileInfo.Length;
                    DownLoadFileOoj_Server fileObj = new DownLoadFileOoj_Server();
                    fileObj.FileName = fileName;
                    fileObj.serverDir = path;
                    fileObj.FileSize = fileInfo.Length;
                    fileObj.ReadSize = 0;
                    fileObj.SendSize = 0;
                    fileObj.ReadStatus = GetFileReadStatus.StartRead;
                    fileObj.SendStatus = GetFileSendStatus.waiting;
                    fileObj.guId = guId;
                    DownLoadFilesQueue.Instance.Add(guId, fileObj);
                    msg = "File have find, reading...";
                    lock (mylock0)
                    {
                        ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadMethod);
                        Thread myThread = new Thread(ParStart);
                        myThread.IsBackground = true;
                        FileThreadPramObj o = new FileThreadPramObj();
                        o.guId = guId;
                        o.myThread = myThread;
                        myThread.Start(o);
                        result = true;
                    }
                    //Action<Stream, string, string, string> readFile = SafeReadFile;
                    //readFile.BeginInvoke(fs, guId, fileName, path, null, null);
                    //result = true;
               
            }
            catch (Exception ex)
            {
                msg = "error";
                result = false;
                throw ex;               
            }
            return result;
        }


        //ThreadMethod如下:
        public void ThreadMethod(object ParObject)
        {
              FileThreadPramObj o = (FileThreadPramObj)ParObject;
              DownLoadFileOoj_Server fileObj0 = new DownLoadFileOoj_Server();
              DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
                //文件数据块编号
               int bufferLength = 32768;
               int _order_Num = 0;
                //初始化一个32k的缓存
                byte[] buffer = new byte[bufferLength];
                int read = 0;
                int block;
                int byteLength = 0;
                //每次从流中读取缓存大小的数据，知道读取完所有的流为止
                fileObj0.ReadStatus = GetFileReadStatus.StartRead;
                DownLoadFilesQueue.Instance.Add(o.guId, fileObj0);
                string fileFullPath = Path.Combine(FilesBlocksQueue.Instance.SavePath + fileObj0.serverDir, fileObj0.FileName);//合并路径生成文件存放路径 
                using (FileStream stream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                while ((block = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
               byteLength = byteLength + block;
               //重新设定读取位置
               read += block;
               _order_Num = _order_Num + 1;       
                    //检查是否到达了缓存的边界，检查是否还有可以读取的信息
                    if (block == buffer.Length)
                    {
                        DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
                        //压入文件块传输缓存队列
                        DownLoadFileDataObj fileBlock = new DownLoadFileDataObj();
                        fileBlock.guId = o.guId;
                        Array.Copy(buffer, fileBlock.FileData, buffer.Length);
                        fileBlock.serverDir = fileObj0.serverDir;
                        fileBlock.Order_Num = _order_Num;
                        fileBlock.FileName = fileObj0.FileName;
                        fileBlock.CanReadLength = bufferLength;                    
                        fileObj0.ReadSize = fileObj0.ReadSize + block;
                        fileObj0.ReadStatus = GetFileReadStatus.Reading;
                        // 尝试读取一个字节
                        int nextByte = stream.ReadByte();
                        // 读取失败则说明读取完成可以返回结果
                        if (nextByte == -1)
                        {
                            fileBlock.EndFlag = true;
                        }
                        else
                        {
                            fileBlock.EndFlag = false;
                        }
                   DownLoadFilesBlocksQueue.Instance.Add(fileBlock);
                   //Utility.Log.Log.writeLineToLog("发送文件读取文件块：" + fileBlock.Order_Num.ToString(), "DownLoadRead");
                   DownLoadFilesQueue.Instance.Add(o.guId, fileObj0); 
                   stream.Seek(-1, SeekOrigin.Current);                 
                   //清空缓存区
                   Array.Clear(buffer, 0, buffer.Length);
                   byteLength = 0;
                    }
                }
                //压入文件块传输缓存队列
                DownLoadFileDataObj fileBlockEnd = new DownLoadFileDataObj();
                fileBlockEnd.guId = o.guId;
                Array.Copy(buffer, fileBlockEnd.FileData, byteLength);
                fileBlockEnd.serverDir = fileObj0.serverDir;
                fileBlockEnd.FileName = fileObj0.FileName;
                fileBlockEnd.Order_Num = _order_Num;
                fileBlockEnd.EndFlag = true;
                fileBlockEnd.CanReadLength = byteLength;
                DownLoadFilesBlocksQueue.Instance.Add(fileBlockEnd);
                //Utility.Log.Log.writeLineToLog("发送文件读取文件块：" + fileBlockEnd.Order_Num.ToString(), "DownLoadRead");
                //更新Files队列中，对应File的ReadSize    
                DownLoadFilesQueue.Instance.Get(o.guId, out fileObj0);
                fileObj0.ReadSize = fileObj0.ReadSize + byteLength;
                fileObj0.ReadStatus = GetFileReadStatus.ReadEnd;
                DownLoadFilesQueue.Instance.Add(o.guId, fileObj0);
                //清空缓存区
                Array.Clear(buffer, 0, buffer.Length);
            }
        }


        //public string DownLoadFileDataBlock(string path, string fileName, string guId)
        //{
        //    string objStr = string.Empty;
        //    DownLoadFileDataObj dataBlock0 = new DownLoadFileDataObj();
        //    foreach (DownLoadFileDataObj obj in DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksQueue)
        //    {
        //        if (obj.guId == guId)
        //        {
        //            dataBlock0 = obj;
        //        }
        //    }
        //    objStr = Newtonsoft.Json.JsonConvert.SerializeObject(dataBlock0);
        //    return objStr;        
        //}

        public string DownLoadFileDataBlock(string guId, string UserID, string PCClientCookieStr)
        {
            if (!SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                return "Session Error";
            }

            string objStr = string.Empty;
            DownLoadFileDataObj dataBlock0 = null;
            //foreach (DownLoadFileDataObj obj in DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksQueue)
            //{
            //    if (obj.guId == guId)
            //    {
            //        dataBlock0 = obj;
            //    }
            //}

            //for (int i = 0; i < DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksBag.Count; i++)
            //{
            DownLoadFileDataObj block = null;
            //DownLoadFileDataObj block = DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksBag.ElementAt<DownLoadFileDataObj>(i);
              if(DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksBag.TryPeek(out block))
              {
                if (block.guId == guId)
                 {
                     dataBlock0 = new DownLoadFileDataObj();
                     dataBlock0.CanReadLength = block.CanReadLength;
                     dataBlock0.EndFlag = block.EndFlag;
                     dataBlock0.FileData = block.FileData;
                     dataBlock0.FileName = block.FileName;
                     dataBlock0.guId = block.guId;
                     dataBlock0.Order_Num = block.Order_Num;
                     dataBlock0.serverDir = block.serverDir;
                     DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksBag.TryTake(out block);                  
                 }
              }
            //}
            if (dataBlock0 == null)
            {
                objStr = string.Empty;
            }
            else
            {
                objStr = Newtonsoft.Json.JsonConvert.SerializeObject(dataBlock0);
            }
            return objStr;
        }

        public bool GetSucDataBlock(string guId, string UserID, string PCClientCookieStr)
        {
            if (!SessionCookie.CheckPCClientCookieStr(UserID, PCClientCookieStr))
            {
                return false;
            }
            foreach (DownLoadFileDataObj obj in DownLoadFilesBlocksQueue.Instance.ServerDownLoadFileBlocksBag)
            {
                if (obj.guId == guId)
                {
                    return true;
                }
            }
            return false;
        }

        public void SafeReadFile(Stream stream, string guId, string FileName,string path)
        {
            //文件数据块编号
            int _order_Num = 0;
            //初始化一个32k的缓存
            byte[] buffer = new byte[32768];
            int read = 0;
            int block;
            //每次从流中读取缓存大小的数据，知道读取完所有的流为止
            DownLoadFileOoj_Server fileObj0 = new DownLoadFileOoj_Server();
            DownLoadFilesQueue.Instance.Get(guId, out fileObj0);
            fileObj0.ReadStatus = GetFileReadStatus.Reading;
            DownLoadFilesQueue.Instance.Add(guId, fileObj0);
            while ((block = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                //重新设定读取位置
                read += block;
                //检查是否到达了缓存的边界，检查是否还有可以读取的信息
                if (block == buffer.Length)
                {
                    //压入文件块传输缓存队列
                    DownLoadFileDataObj fileBlock = new DownLoadFileDataObj();
                    fileBlock.guId = guId;
                    Array.Copy(buffer, fileBlock.FileData, buffer.Length);
                    fileBlock.serverDir = path;
                    fileBlock.Order_Num = _order_Num;
                    DownLoadFilesBlocksQueue.Instance.Add(fileBlock);
                    DownLoadFilesQueue.Instance.Get(guId, out fileObj0);
                    fileObj0.ReadSize = fileObj0.ReadSize + block;
                    fileObj0.ReadStatus = GetFileReadStatus.Reading;
                    DownLoadFilesQueue.Instance.Add(guId, fileObj0);

                    // 尝试读取一个字节
                    int nextByte = stream.ReadByte();
                    // 读取失败则说明读取完成可以返回结果
                    if (nextByte == -1)
                    {
                        fileBlock.EndFlag = true;
                    }
                    else
                    {
                        fileBlock.EndFlag = false;
                    }

                    stream.Seek(-1, SeekOrigin.Current);
                    _order_Num = _order_Num + 1;
                    //清空缓存区
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            //压入文件块传输缓存队列
            DownLoadFileDataObj fileBlockEnd = new DownLoadFileDataObj();
            fileBlockEnd.guId = guId;
            Array.Copy(buffer, fileBlockEnd.FileData, buffer.Length);
            fileBlockEnd.serverDir = path;
            fileBlockEnd.Order_Num = _order_Num;
            fileBlockEnd.EndFlag = true;
            DownLoadFilesBlocksQueue.Instance.Add(fileBlockEnd);
            //更新Files队列中，对应File的ReadSize    
            DownLoadFilesQueue.Instance.Get(guId, out fileObj0);
            fileObj0.ReadSize = fileObj0.ReadSize + fileBlockEnd.FileData.Length;
            fileObj0.ReadStatus =  GetFileReadStatus.ReadEnd;
            DownLoadFilesQueue.Instance.Add(guId, fileObj0);
            //清空缓存区
            Array.Clear(buffer, 0, buffer.Length);
        }

    }
   
}
