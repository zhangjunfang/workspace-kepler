using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LogService.TXT
{
    /// <summary>
    /// TXTLogFormatter
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 14:42:11</datetime>
    ///         <comment>create</comment>
    ///     </version>
    ///     <version number="1.1.1.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/17 10:12:00</datetime>
    ///         <comment>
    ///             日志格式化时,StackFrame将由日志服务给予而不是在格式化时获取
    ///             日志源信息将不再输出日志源文件的完全路径而是只输出日志源文件的名称及扩展名
    ///         </comment>
    ///     </version>
    /// </versioning>
    public class TXTLogFormatter : LogFormatter
    {
        #region Constructor -- 构造函数
        private TXTLogFormatter()
        {
        }
        #endregion

        #region Field -- 字段
        /// <summary>
        /// 日志文件类型扩展名
        /// </summary>
        public readonly String Filetype = ".txt";
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 获取日志格式
        /// </summary>
        /// <returns></returns>
        public static LogFormatter GetInstance()
        {
            return Instance ?? (Instance = new TXTLogFormatter());
        }

        /// <summary>
        /// 获取日志时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public override string FormatTime(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss:fff");
        }

        /// <summary>
        /// 获取日志文件名称
        /// </summary>
        /// <param name="logFileName"></param>
        /// <returns></returns>
        public override string GetLogFileName(string logFileName)
        {
            return logFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logHeader"></param>
        /// <param name="logBody"></param>
        /// <param name="stackfram"></param>
        /// <returns></returns>
        public override LogRecord FormatLogRecord(LogHeader logHeader, LogBody logBody, StackFrame stackfram)
        {
            var lr = new LogRecord();
            var sb = new StringBuilder();
            var seperator = TXTLogFactory.GetLogSeperator(logHeader);
            sb.Append(FormatTime(TXTLogFactory.GetLogGenTime(logHeader)));
            sb.Append(seperator);
            sb.Append(logHeader.LogName);
            sb.Append(seperator);
            sb.Append(logHeader.LogInfo);
            sb.Append(seperator);
            sb.Append(logHeader.LogType);
            sb.Append(seperator);
            sb.Append(logHeader.LogVersion);
            //2014-12-09 20:33:36:495|AdminService|WAP.WCF.HELPER|TEXT|1.0||||2014-12-09 20:33:36:495	999920
            sb.Append(seperator);
            if (logHeader.LogTraceEnable)
            {
                var className = Path.GetFileName(LogManager.GetClassName(stackfram));   //VERSION:1.1.1.0
                var methodName = LogManager.GetMethodName(stackfram);
                var lineNoumber = LogManager.GetLineNumber(stackfram);
                var colNumber = LogManager.GetColumnNumber(stackfram);

                sb.Append(className);
                sb.Append(seperator);
                sb.Append(methodName);
                sb.Append(seperator);
                sb.Append(lineNoumber);
                sb.Append(seperator);
                sb.Append(colNumber);
                sb.Append(seperator);
            }
            sb.Append(TXTLogFactory.GetLogBodyString(logBody));
            lr.LogMessage = sb.ToString();
            return lr;
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
