using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using HXC_FuncUtility;

namespace HXCServerWinForm.CommonClass
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
        public static bool UploadFile(string filePath, string fileName)
        {
            string serverFileDir = GlobalStaticObj_Server.Instance.FilePath;
            string targetFilePath = Path.Combine(serverFileDir, fileName);//合并路径生成文件存放路径
            File.Copy(filePath, targetFilePath);
            return true;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public static bool UploadFile(Stream stream, string fileName)
        {
            string serverFileDir = GlobalStaticObj_Server.Instance.FilePath;
            string targetFilePath = Path.Combine(serverFileDir, fileName);//合并路径生成文件存放路径
            using (FileStream fs = File.Create(targetFilePath))
            {
                int bufferSize = 2048;
                byte[] bytes = new byte[bufferSize];
                int length = stream.Read(bytes, 0, bufferSize);
                while (length > 0)
                {
                    fs.Write(bytes, 0, length);
                    length = stream.Read(bytes, 0, bufferSize);
                }
                stream.Dispose();
                stream.Close();
            }

            return true;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream DownLoadFile(string fileName)
        {
            if (fileName.Length == 0)
            {
                return null;
            }
            string serverFileDir = GlobalStaticObj_Server.Instance.FilePath;
            string targetFilePath = Path.Combine(serverFileDir, fileName);//合并路径生成文件存放路径
            return File.OpenRead(targetFilePath);
        }

        public static string DownLoadFile(string fileName, string path)
        {
            string serverFileDir = GlobalStaticObj_Server.Instance.FilePath;
            string targetFilePath = Path.Combine(serverFileDir, fileName);//合并路径生成文件存放路径
            string fullPath = Path.Combine(Application.StartupPath, path);
            fullPath = Path.Combine(fullPath, fileName);
            File.Copy(targetFilePath, fullPath);
            return fullPath;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static Image DownLoadImage(string fileName)
        {
            string serverFileDir = GlobalStaticObj_Server.Instance.FilePath;
            string targetFilePath = Path.Combine(serverFileDir, fileName);//合并路径生成文件存放路径
            if (!File.Exists(targetFilePath))
            {
                return null;
            }
            Stream stream = File.OpenRead(targetFilePath);
            if (stream == null)
            {
                return null;
            }
            Image img = new Bitmap(stream);
            return img;
        }
    }
}
