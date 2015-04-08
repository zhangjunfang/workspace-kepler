using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace HXCCommon.DotNetFile
{
    /// <summary>
    /// FTP文件操作
    /// </summary>
    public class FTPHelper
    {
        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="fileUpload">上传控件</param>
        /// <param name="ftpServerIP">上传文件服务器IP</param>
        /// <param name="ftpUserID">服务器用户名</param>
        /// <param name="ftpPassword">服务器密码</param>
        /// <returns></returns>
        public string Upload(FileUpload fileUpload, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            string filename = fileUpload.FileName;
            string sRet = "上传成功！";
            FileInfo fileInf = new FileInfo(fileUpload.PostedFile.FileName);
            string uri = "ftp://" + ftpServerIP + "/" + filename;
            FtpWebRequest reqFTP;
            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = false;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            // 缓冲大小设置为2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return sRet;
        }
        /// <summary>
        /// FTP下载文件
        /// </summary>
        /// <param name="userId">ftp用户名</param>
        /// <param name="pwd">ftp密码</param>
        /// <param name="ftpPath">ftp文件路径</param>
        /// <param name="filePath">下载保存路径</param>
        /// <param name="fileName">ftp文件名</param>
        /// <returns></returns>
        public string Download(string userId, string pwd, string ftpPath, string filePath, string fileName)
        {
            string sRet = "下载成功！";
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + fileName, FileMode.Create);
                // 根据uri创建FtpWebRequest对象   
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpPath + fileName));
                // 指定执行什么命令  
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                // 指定数据传输类型  
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                // ftp用户名和密码  
                reqFTP.Credentials = new NetworkCredential(userId, pwd);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                // 把下载的文件写入流
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                // 缓冲大小设置为2kb  
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                // 每次读文件流的2kb  
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    // 把内容从文件流写入   
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                //关闭两个流和ftp连接
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            //返回下载结果（是否下载成功）
            return sRet;
        }
        /// <summary>
        /// FTP删除文件
        /// </summary>
        /// <param name="ftpPath">ftp文件路径</param>
        /// <param name="userId">ftp用户名</param>
        /// <param name="pwd">ftp密码</param>
        /// <param name="fileName">ftp文件名</param>
        /// <returns></returns>
        public string DeleteFile(string ftpPath, string userId, string pwd, string fileName)
        {
            string sRet = "删除成功！";
            FtpWebResponse Respose = null;
            FtpWebRequest reqFTP = null;
            Stream localfile = null;
            Stream stream = null;
            try
            {
                //根据uri创建FtpWebRequest对象  
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format(@"{0}{1}", ftpPath, fileName)));
                //提供账号密码的验证  
                reqFTP.Credentials = new NetworkCredential(userId, pwd);
                //默认为true是上传完后不会关闭FTP连接  
                reqFTP.KeepAlive = false;
                //执行删除操作
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                Respose = (FtpWebResponse)reqFTP.GetResponse();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            finally
            {
                //关闭连接跟流
                if (Respose != null)
                    Respose.Close();
                if (localfile != null)
                    localfile.Close();
                if (stream != null)
                    stream.Close();
            }
            //返回执行状态
            return sRet;
        }
    }
}
