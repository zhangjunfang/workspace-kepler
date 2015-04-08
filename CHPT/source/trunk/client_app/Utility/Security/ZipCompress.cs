using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

/**
 *模块描述：执行Rar，需要在有rar.exe文件
 *创 建 人：sunyanan
 *创建时间：2014-10-24
 */

namespace Utility.Security
{
    public class ZipCompress
    {
        /// <summary> zip压缩（暂时仅支持单文件）
        /// </summary>
        /// <param name="filesPath">压缩文件目录</param>
        /// <param name="zipFilePath">zip文件路径</param>
        /// <param name="pwd">压缩密码</param>
        /// /// <returns>错误原因</returns>
        public static string CreateZipFile(string filesPath, string zipFilePath, string pwd)
        {
            if (!Directory.Exists(filesPath))
            {
                return string.Format("Cannot find directory '{0}'", filesPath);
            }

            try
            {
                string[] filenames = Directory.GetFiles(filesPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.Password = pwd;
                    s.SetLevel(9); // 压缩级别 0-9
                    byte[] buffer = new byte[4096]; //缓冲区大小
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }

            catch (Exception ex)
            {
                return string.Format("Exception during processing {0}", ex);
            }
            return "";
        }

        /// <summary> zip解压
        /// </summary>
        /// <param name="zipFilePath">zip文件路径</param>
        /// <param name="fileDir">解压目录</param>
        /// <param name="pwd">解压密码</param>
        /// <returns>错误原因</returns>
        public static string UnZipFile(string zipFilePath, string fileDir, string pwd, ref string errMsg)
        {
            if (!File.Exists(zipFilePath))
            {
                errMsg = string.Format("Cannot find file '{0}'", zipFilePath);
                return "";
            }
            string directoryName = string.Empty;
            try
            {
                FileStream fs = File.OpenRead(zipFilePath);
                using (ZipInputStream s = new ZipInputStream(fs))
                {
                    s.Password = pwd;
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        directoryName = Path.GetDirectoryName(theEntry.Name);
                        directoryName = fileDir + @"\" + directoryName;
                        string fileName = directoryName + "\\" + Path.GetFileName(theEntry.Name);

                        if (directoryName.Length > 0 && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = string.Format("Exception during processing {0}", ex);
                return "";
            }
            return directoryName;
        }

        /// <summary> zip解压
        /// </summary>
        /// <param name="zipFilePath">zip文件路径</param>
        /// <param name="fileDir">解压目录</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="pwd">解压密码</param>
        /// <param name="errMsg">错误原因</param>
        /// <returns>解压后路径</returns>
        public static string UnZipFile(string zipFilePath, string fileDir, string serviceType, string pwd, ref string errMsg)
        {
            if (!File.Exists(zipFilePath))
            {
                errMsg = string.Format("Cannot find file '{0}'", zipFilePath);
                return "";
            }
            string directoryName = string.Empty;
            try
            {
                FileStream fs = File.OpenRead(zipFilePath);
                using (ZipInputStream s = new ZipInputStream(fs))
                {
                    s.Password = pwd;
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        directoryName = Path.GetDirectoryName(theEntry.Name);
                        directoryName = fileDir + @"\" + directoryName + "\\" + serviceType;
                        string fileName = directoryName + "\\" + Path.GetFileName(theEntry.Name);

                        if (directoryName.Length > 0 && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = string.Format("Exception during processing {0}", ex);
                return "";
            }
            return directoryName;
        }
    }
}
