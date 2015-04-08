using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SYSModel;
using HuiXiuCheWcfFileContract;
using System.IO;
using HXCLog;
namespace HuiXiuCheWcfFileService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HXCWCFFileService : IHXCWCFFileService
    {
        private static readonly Object locker = new Object();
        private static string _savePath = null;
        private static string SavePath
        {
            get
            {
                if (_savePath == null)
                {
                    lock (locker)
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

        private static void writeUserFileOpLog(UserFileOPLog uOpLog)
        {
            UserOPFileLogClass.Instance.Add(uOpLog);
        }

        public Stream TestConnect()
        {
            string testString = "1";
            byte[] buffer = new byte[1];
            buffer = Encoding.Default.GetBytes(testString);
            MemoryStream stream = new MemoryStream(buffer);    
            return stream;
        }

        /// <summary> 上传文件 
        /// </summary>
        /// <param name="request">1上传成功，0上传失败</param>
        public UploadFileResponse UploadFile(FileTransferMessage request)
        {

            //if (!HXCSession.SessionCookie.CheckPCClientCookieStr(request.UserID, request.CookieStr))
            //{
            //    return;
            //}
            UploadFileResponse response = new UploadFileResponse();
            response.Flag = false;
            UserFileOPLog uOpLog = new UserFileOPLog();
            UserIDOP uOp = new UserIDOP();
            uOp.UserID = request.UserID;
            uOp.OPName = "UploadFile";
            uOpLog.userOP = uOp;
            uOpLog.FileName = request.FileName;
            if (!Directory.Exists(SavePath))//存放的默认文件夹是否存在
            {
                Directory.CreateDirectory(SavePath);//不存在则创建
            }
            string path = SavePath;
            if (!string.IsNullOrEmpty(request.SavePath))
            {
                path = Path.Combine(SavePath, request.SavePath);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            uOpLog.sTimeTicks = DateTime.UtcNow.Ticks;
            string fileName = request.FileName;//文件名
            string fileFullPath = Path.Combine(path, fileName);//合并路径生成文件存放路径
            uOpLog.FilePath = fileFullPath;
            Stream sourceStream = request.FileData;
            if (sourceStream == null) { return response; }
            if (!sourceStream.CanRead) { return response; }
            //创建文件流，读取流中的数据生成文件
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    const int bufferLength = 4096;
                    byte[] myBuffer = new byte[bufferLength];//数据缓冲区
                    int count;
                    while ((count = sourceStream.Read(myBuffer, 0, bufferLength)) > 0)
                    {
                        fs.Write(myBuffer, 0, count);
                    }
                    fs.Close();
                    sourceStream.Close();
                    uOpLog.eTimeTicks = DateTime.UtcNow.Ticks;
                    uOpLog.exeResult = true;
                    response.Flag = true;
                    return response;
                }
                catch (Exception ex)
                {
                    uOpLog.eTimeTicks = DateTime.UtcNow.Ticks;
                    uOpLog.exeResult = false;
                    return response;
                    throw ex;
                }
                finally {
                    writeUserFileOpLog(uOpLog);
                }
            }
        }
        /// <summary> 下载文件
        /// </summary>
        /// <param name="fileName">下载文件名称</param>
        /// <returns></returns>
        public Stream DownLoadFile(string fileName, string savePath, string userID, string cookieStr)
        {
            //if (!HXCSession.SessionCookie.CheckPCClientCookieStr(userID,cookieStr))
            //{
            //    return null;
            //}
            //DownFileResponse response = new DownFileResponse();
            //response.FileData = null;
            UserFileOPLog uOpLog = new UserFileOPLog();
            UserIDOP uOp = new UserIDOP();
            uOp.UserID = userID;
            uOp.OPName = "DownLoadFile";
            uOpLog.userOP = uOp;
            uOpLog.FileName = fileName;
            string fileFullPath = Path.Combine(SavePath, fileName);//服务器文件路径
            if (!string.IsNullOrEmpty(savePath))
            {
                fileFullPath = Path.Combine(SavePath, savePath, fileName);
            }
            uOpLog.FilePath = fileFullPath;
            if (!File.Exists(fileFullPath))//判断文件是否存在
            {
                return null;
            }
            FileStream myStream = null;
            try
            {
                uOpLog.sTimeTicks = DateTime.UtcNow.Ticks;
                myStream = File.OpenRead(fileFullPath);
               
            }
            catch (Exception ex) {
                uOpLog.eTimeTicks = DateTime.UtcNow.Ticks;
                uOpLog.exeResult = false;               
                return null;
                throw ex;
            }
            finally
            {
                uOpLog.eTimeTicks = DateTime.UtcNow.Ticks;
                uOpLog.exeResult = true;
                writeUserFileOpLog(uOpLog);
            }
            return myStream;
        }
    }
}
