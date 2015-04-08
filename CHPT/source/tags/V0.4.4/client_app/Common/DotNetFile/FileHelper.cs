using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using HXCCommon.DotNetConfig;
using HXCCommon.DotNetCode;
using System.Web;

namespace HXCCommon.DotNetFile
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 创建系统异常日志
        /// </summary>
        protected static LogHelper Logger = new LogHelper("文件帮助类");

        public static string GetDownloadFileUrl(byte[] val)
        {
            //MemoryStream ms = new MemoryStream((byte[])val);//把照片读到MemoryStream里     
            //Bitmap imageBlob = new System.Drawing.Bitmap(ms, true);//用流创建Image     
            //string temPicPath = System.Web.HttpContext.Current.Server.MapPath("/") + "tempPic\\";
            //if (!Directory.Exists(temPicPath))
            //    Directory.CreateDirectory(temPicPath);
            //string file0 = "/tempPic/" + ht["EXPERTOID"] + ht["FILEEXTNAME"];
            //imageBlob.Save(temPicPath + ht["EXPERTOID"] + ht["FILEEXTNAME"]);
            //PHOTO.Src = file0;//输出图片
            return "";
        }



        /// <summary>
        /// 将流读取到缓冲区中
        /// </summary>
        /// <param name="stream">原始流</param>
        public static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                //创建缓冲区
                byte[] buffer = new byte[stream.Length];

                //读取流
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

                //返回流
                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭流
                stream.Close();
            }
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool IsExistFile(string filename)
        {
            return System.IO.File.Exists(filename);
        }
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool CopyFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool CopyFile(string sourceFileName, string destFileName)
        {
            return CopyFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        /// 获得指定目录下的文件列表
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string[] GetDirectoryFileList(string path, string searchPattern)
        {
            if (!Directory.Exists(path))
                return new string[0];
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = dirInfo.GetFiles(searchPattern);
            string[] result = new string[fileInfos.Length];
            for (int i = 0; i < fileInfos.Length; i++)
            {
                result[i] = fileInfos[i].Name;
            }
            return result;
        }
        /// <summary>
        /// 获取指定目录的所有文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetDirectoryFileList(string path)
        {
            return GetDirectoryFileList(path, "*.*");
        }
        /// <summary>
        /// 文件转字节流
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static byte[] FileToBytes(string path)
        {
            FileStream fs = System.IO.File.OpenRead(path);
            MemoryStream ms = new MemoryStream();
            int bdata;
            while ((bdata = fs.ReadByte()) != -1)
            {
                ms.WriteByte(((byte)bdata));
            }
            byte[] data = (byte[])ms.ToArray();
            fs.Close();
            ms.Close();
            return data;
        }
        /// <summary>
        /// 把流生成文件
        /// </summary>
        /// <param name="buffer">文件流</param>
        /// <param name="fileName">要生成的文件名【路】</param>
        public static bool UpLoadFile(byte[] buffer, string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建文件
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        //写入二进制流
                        fs.Write(buffer, 0, buffer.Length);

                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 图片转为base64编码
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns></returns>
        public static string ImgToBase64String(string filePath)
        {
            try
            {
                Bitmap bmp = new Bitmap(filePath);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception e)
            {
                Logger.WriteLog("图片转为base64编码出现异常\r\n" + e.Message + "\r\n");
            }
            return "";
        }
        /// <summary>
        /// base64编码的文本转为图片
        /// </summary>
        /// <param name="photo">base64编码</param>
        /// <param name="filePath">保存绝对路径</param>
        /// <returns></returns>
        public static bool Base64ToImage(string photo, string filePath)
        {
            try
            {
                string dirpath = ConfigHelper.GetAppSettings("SaveFile") + "\\" + DateTime.Now.ToString("yyyyMMdd");//文件保存路径
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                byte[] arr = Convert.FromBase64String(photo);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(filePath);
                ms.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteLog("base64编码的文本转为图片出现异常\r\n" + e.Message + "\r\n base64编码" + photo + "\r\n");
                return false;
            }
        }
        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="Size">初始文件大小</param>
        /// <returns></returns>
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";
            return m_strSize;
        }
        /// <summary>
        /// 根据文件名，得到文件的大小，单位分别是GB/MB/KB
        /// </summary>
        /// <param name="FileFullPath">文件名</param>
        /// <returns>返回文件大小</returns>
        public static string GetFileSize(string fileName)
        {
            if (System.IO.File.Exists(fileName) == true)
            {
                FileInfo fileinfo = new FileInfo(fileName);
                long fl = fileinfo.Length;
                if (fl > 1024 * 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                else if (fl > 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                else
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / 1024, 2)) + " KB";
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDictSize(string path)
        {
            if (!System.IO.Directory.Exists(path))
                return "0";
            string[] fs = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            //获取该文件夹中所有的文件名	
            long ll = 0;
            foreach (string f in fs)
            {
                dynamic fa = System.IO.File.GetAttributes(f);
                System.IO.FileInfo fi = new System.IO.FileInfo(f);
                ll += fi.Length;
            }
            return CountSize(ll);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        /// <summary>
        /// 删除非空文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
                dir.Delete(true);
            }
        }
        /// <summary>
        /// 文件转换文件流
        /// </summary>
        /// <param name="filePath">保存绝对路径</param>
        /// <returns></returns>
        public byte[] DownloadFileByte(string filePath)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[fs.Length];
            return bytes;
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void GetDownload(string filePath)
        {
            string Name = Path.GetFileName(filePath);
            string FullFileName = HttpContext.Current.Server.MapPath(filePath);
            FileInfo DownloadFile = new FileInfo(FullFileName);
            if (DownloadFile.Exists)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + Name);
                HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                HttpContext.Current.Response.WriteFile(DownloadFile.FullName);

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// //如果不存在就创建file文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}

