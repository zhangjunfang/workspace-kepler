using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Collections;
using HXCCommon.DotNetConfig;
using System.Threading;

namespace HXCCommon.DotNetCode
{
    /// <summary>
    ///  企业应用框架的日志类
    /// </summary>    
    public class LogHelper : IDisposable
    {
        private string LogFile;
        //日志文件写入流对象
        private static StreamWriter sw;
        string logIsWrite = ConfigHelper.GetAppSettings("LogIsWrite");
        /// <summary>
        /// 实例日志管理，以当前日期为文件名，如果文件不存在，则创建文件
        /// </summary>
        public LogHelper()
        {
            CreateLoggerFile(null);
        }
        /// <summary>
        /// 实例日志管理，如果文件不存在，则创建文件
        /// </summary>
        /// <param name="_log">创建txt文件名称</param>
        public LogHelper(string _log)
        {
            CreateLoggerFile(_log);
        }
        private void CreateLoggerFile(string fileName)
        {
            if (logIsWrite == "true")//是否写日志
            {
                Object _myLogPath;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = DateTimeHelper.GetToday();
                }
                _myLogPath = null;
                if (_myLogPath == null)
                {
                    LogFile = ConfigHelper.GetAppSettings("LogFilePath");
                    if (!System.IO.File.Exists(LogFile))
                    {
                        Directory.CreateDirectory(LogFile);
                    }
                }
                else
                {
                    LogFile = _myLogPath.ToString();
                }
                if (1 > LogFile.Length)
                {
                    Console.WriteLine("配置文件中没有指定日志文件目录！");
                    return;
                }
                if (false == Directory.Exists(LogFile))
                {
                    Console.WriteLine("配置文件中指定日志文件目录不存在！");
                    return;
                }
                if ((LogFile.Substring((LogFile.Length - 1), 1).Equals("/")) || (LogFile.Substring(LogFile.Length - 1, 1).Equals("\\")))
                {
                    LogFile = LogFile + fileName + ".log";
                }
                else
                {
                    LogFile = LogFile + "\\" + fileName + ".log";
                }
                try
                {
                    FileStream fs = new FileStream(LogFile, FileMode.OpenOrCreate);
                    fs.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        /// <summary>
        /// 向日志文件中写入日志
        /// </summary>
        /// <param name="messagestr"></param>
        private void writeInfos(String messagestr)
        {
            DateTime DateNow = new DateTime();
            string DateStr;
            string sWrite;
            try
            {
                FileOpen();
                DateNow = DateTime.Now;
                DateStr = "[" + DateNow.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                sWrite = DateStr + "\n" + messagestr;
                sw.WriteLine(sWrite);
                sw.Flush();
                sw.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //打开文件准备写入
        private void FileOpen()
        {
            sw = new StreamWriter(LogFile, true);
        }
        //关闭打开的日志文件
        private void FileClose()
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
        }
        /// <summary>
        /// 写入日志内容
        /// </summary>
        /// <param name="msg">日志消息</param>
        public void WriteLog(string msg)
        {
            if (msg != null)
            {
                writeInfos(msg.ToString());
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
        }

        #endregion
    }
}
