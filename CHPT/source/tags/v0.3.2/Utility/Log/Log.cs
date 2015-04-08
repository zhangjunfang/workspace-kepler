using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Kord.LogService;
using Kord.LogService.TXT;

namespace Utility.Log
{
    public class Log
    {
        private static readonly Object lockerRunFlag = new Object();
        private static readonly Object lockerServiceLog = new Object();

        public static void writeCloudLog(string str)
        {
            lock (lockerServiceLog)
            {
                string path = Application.StartupPath + "\\Log\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path += "\\Cloud\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                DateTime now = DateTime.Now;
                string logName = string.Format(@"{0}-{1}-{2}.log", now.Year, now.Month, now.Day);
                string logpath = path + logName;
                if (!File.Exists(logpath))
                {
                    FileStream fs = new FileStream(logpath, FileMode.Create);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(string.Format("----------------------{0}--------------------------\r\n",
                        now.ToString("yyyy-MM-dd HH:mm:ss")));
                    sw.Write(str);
                    sw.Write("\r\n");
                    sw.Write("----------------------footer--------------------------\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(logpath, FileMode.Append);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(string.Format("----------------------{0}--------------------------\r\n",
                        now.ToString("yyyy-MM-dd HH:mm:ss")));
                    sw.Write(str);
                    sw.Write("\r\n----------------------footer--------------------------\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }

        /// <summary> 记录错误日志 
        /// </summary>
        /// <param name="str"></param>
        public static void writeLog(string str)
        {
            lock (lockerRunFlag)
            {
                string path = Application.StartupPath + "\\Log\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                DateTime now = DateTime.Now;
                string logName = string.Format(@"fatal_{0}-{1}-{2}.log", now.Year, now.Month, now.Day);
                string logpath = path + logName;
                if (!File.Exists(logpath))
                {
                    FileStream fs = new FileStream(logpath, FileMode.Create);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(string.Format("\r\n----------------------{0}--------------------------\r\n",
                        now.ToString("yyyy-MM-dd HH:mm:ss")));
                    sw.Write(str);
                    sw.Write("\r\n");
                    sw.Write("\r\n----------------------footer--------------------------\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(logpath, FileMode.Append);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(string.Format("\r\n----------------------{0}--------------------------\r\n",
                        now.ToString("yyyy-MM-dd HH:mm:ss")));
                    sw.Write(str);
                    sw.Write("\r\n");
                    sw.Write("\r\n----------------------footer--------------------------\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
            //System.IO.File.AppendAllText(logpath, string.Format("\r\n----------------------{0}--------------------------\r\n", now.ToString("yyyy-MM-dd HH:mm:ss")));
            //System.IO.File.AppendAllText(logpath, str);
            //System.IO.File.AppendAllText(logpath, "\r\n");
            //System.IO.File.AppendAllText(logpath, "\r\n----------------------footer--------------------------\r\n");
        }

        /// <summary> 记录日志 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        public static void writeLineToLog(string str, string type)
        {
            str = string.Format("时间：{0}\r\n{1}", DateTime.Now, str);
            lock (lockerRunFlag)
            {
                string path = Application.StartupPath + "\\Log\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                DateTime now = DateTime.Now;
                string logName = string.Format(@"log_" + type + "{0}-{1}-{2}.log", now.Year, now.Month, now.Day);
                string logpath = path + logName;
                if (!File.Exists(logpath))
                {
                    FileStream fs = new FileStream(logpath, FileMode.Create);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(str);
                    sw.Write("\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    sw.Dispose();
                    fs.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(logpath, FileMode.Append);
                    //实例化一个StreamWriter-->与fs相关联
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(str);
                    sw.Write("\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    sw.Dispose();
                    fs.Dispose();
                }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 记录错误记录
        /// </summary>
        /// <param name="e">异常实例</param>
        /// <param name="type">错误类型</param>
        public static void writeLineToLog(Exception e, string type)
        {
            writeLineToLog(string.Format("错误源：{0}\r\n错误消息：{1}\r\n调用堆栈：{2}", e.Source, e.Message, e.StackTrace), type);
        }

        #region Add by kord
        static readonly LogFormatter Logformartter = TXTLogFormatter.GetInstance();
        static readonly LogFactory LogFactory = TXTLogFactory.GetInstance();
        public static LoggingService CreateLogService(String logName, String logFolder, Int32 logGrade = 1, Boolean isStart = true)
        {
            if(LogManager.GetLogFormatter("TEXTLogFormatter") == null) LogManager.RegisterLogFormatter("TEXTLogFormatter", Logformartter);
            if (LogManager.GetLogFactory("TEXTLogFactory") == null) LogManager.RegisterLogFactory("TEXTLogFactory", LogFactory);

            var logCfg = new LogServiceConfig
            {
                LogFactoryName = "TEXTLogFactory",
                LogName = logName,
                LogSeperator = "|",
                LogFilePath = Application.StartupPath + "\\Log\\",
                LogSubFolder = logFolder,
                LogFileNameFormatter = logName + "|yyyyMMdd|M*5|_",
                LogType = @"TEXT",
                LogVersion = "1.0",
                LogFormatter = "TEXTLogFormatter",
                LogInfo = "",
                LogEnable = true,
                LogTraceEnable = true,
                LogGrade = logGrade,
                LogPersistInterval = 500,
                LogDescription = ""
            };

            var logService = LogManager.CreateLogService(logCfg);
            if(isStart) logService.Start();
            return logService;
        }
        #endregion
    }
}
