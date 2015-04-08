using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using HuiXiuCheWcfFileContract;
using System.Windows.Forms;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileOperation
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public static bool UploadFile(string savePath, string fileName)
        {
            FileTransferMessage fileMessage = new FileTransferMessage();
            fileMessage.CookieStr = GlobalStaticObj.CookieStr;
            fileMessage.UserID = GlobalStaticObj.UserID;
            //fileMessage.SavePath = savePath;
            fileMessage.FileName = fileName;
            FileStream fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            fileMessage.FileData = fileStream;
            try
            {
                GlobalStaticObj.proxyFile.UploadFile(fileMessage);
                fileStream.Close();
                fileStream.Dispose();
                return true;
            }
            catch (Exception e)
            {
                fileStream.Close();
                fileStream.Dispose();
                GlobalStaticObj.GlobalLogService.WriteLog("【附件上传】" + e);  //Modify by kord -- Utility.Log.Log.writeLineToLog(e, "附件上传");
                return false;
            }
        }

        /// <summary>
        /// 上传附件到指定路径
        /// </summary>
        /// <param name="path">指定服务端路径</param>
        /// <param name="filePath">本地文件路径</param>
        /// <returns></returns>
        public static bool UploadFileByPath(string path, string filePath)
        {
            FileTransferMessage fileMessage = new FileTransferMessage();
            fileMessage.CookieStr = GlobalStaticObj.CookieStr;
            fileMessage.UserID = GlobalStaticObj.UserID;
            fileMessage.SavePath = path;
            fileMessage.FileName = Path.GetFileName(filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            fileMessage.FileData = fileStream;
            try
            {
                GlobalStaticObj.proxyFile.UploadFile(fileMessage);
                fileStream.Close();
                fileStream.Dispose();
                return true;
            }
            catch (Exception e)
            {
                fileStream.Close();
                fileStream.Dispose();
                GlobalStaticObj.GlobalLogService.WriteLog("【附件上传】" + e);  //Modify by kord -- Utility.Log.Log.writeLineToLog(e, "附件上传");
                return false;
            }
        }

        public static bool UploadFile(Stream stream, string fileName)
        {
            FileTransferMessage fileMessage = new FileTransferMessage();
            fileMessage.CookieStr = GlobalStaticObj.CookieStr;
            fileMessage.UserID = GlobalStaticObj.UserID;
            //fileMessage.SavePath = filePath;
            fileMessage.FileName = fileName;
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            fileMessage.FileData = stream;
            try
            {
                return GlobalStaticObj.proxyFile.UploadFile(fileMessage).Flag;
                //GlobalStaticObj.proxyFile.UploadFile1(stream, fileName);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>文件流</returns>
        public static Stream DownLoadFileByStream(string fileName, string path)
        {
            if (fileName.Length == 0)
            {
                return null;
            }
            if (!WCFClientProxy.TestFileProxy())
            {
                return null;
            }
            try
            {
                Stream stream = GlobalStaticObj.proxyFile.DownLoadFile(fileName, path, GlobalStaticObj.UserID, GlobalStaticObj.CookieStr);
                return stream;
            }
            catch (Exception te)
            {
                return null;
            }
        }

        public static Stream DownLoadFile(string fileName)
        {
            return DownLoadFileByStream(fileName, "");
        }

        /// <summary>
        /// 下载文件字节
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>文件字节</returns>
        public static byte[] DownLoadByte(string fileName)
        {
            Stream stream = DownLoadFileByStream(fileName, "");
            if (stream == null)
            {
                return null;
            }
            List<byte> listByte = new List<byte>();
            try
            {
                MemoryStream ms = new MemoryStream();
                const int bufferLength = 4096;
                byte[] myBuffer = new byte[bufferLength];//数据缓冲区
                int count;
                while ((count = stream.Read(myBuffer, 0, bufferLength)) > 0)
                {
                    ms.Write(myBuffer, 0, count);
                }
                stream.Close();
                stream.Dispose();
                if (ms.Length == 0)
                {
                    return null;
                }
                byte[] bytes = ms.ToArray();
                ms.Close();
                ms.Dispose();
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                stream.Close();
                stream.Dispose();
                return null;
            }           
        }

        /// <summary>
        /// 下载文件转成16进制
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>已转成16进制</returns>
        public static string DownLoadString16(string fileName)
        {
            byte[] bytes = DownLoadByte(fileName);
            if (bytes == null)
            {
                return string.Empty;
            }
            return Convert.ToBase64String(bytes);
            //StringBuilder sbByte = new StringBuilder();
            //foreach (byte b in bytes)
            //{
            //    sbByte.Append(b.ToString("X2"));
            //}
            //return sbByte.ToString();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="path"></param>
        /// <seealso cref="String">String类型的信息</seealso>
        /// <returns>下载后的文件路径</returns>
        public static string DownLoadFileByFile(string fileName, string path)
        {
            Stream stream = DownLoadFileByStream(fileName, path);
            if (stream == null)
            {
                return null;
            }
            string fullPath = Path.Combine(Application.StartupPath, path);
            fullPath = Path.Combine(fullPath, fileName);
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                try
                {
                    const int bufferLength = 4096;
                    byte[] myBuffer = new byte[bufferLength];//数据缓冲区
                    int count;
                    while ((count = stream.Read(myBuffer, 0, bufferLength)) > 0)
                    {
                        fs.Write(myBuffer, 0, count);
                    }
                    if (fs.Length == 0)
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            stream.Close();
            stream.Dispose();
            return fullPath;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static Image DownLoadImage(string fileName, string path)
        {
            Stream stream = DownLoadFileByStream(fileName, path);
            if (stream == null)
            {
                return null;
            }
            Image img = new Bitmap(stream);
            stream.Close();
            stream.Dispose();
            return img;
        }

        public static Image DownLoadImage(string fileName)
        {
            return DownLoadImage(fileName, "");
        }
    }
}
