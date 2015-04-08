
using System;

namespace LogService.TXT
{
    /// <summary>
    /// TXTLogFactory
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 14:41:58</datetime>
    ///         <comment>create</comment>
    ///     </version>
    ///     <version number="1.1.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/01/07 20:02:00</datetime>
    ///         <comment>
    ///             添加可以解析日志头信息的方法
    ///         </comment>
    ///     </version>
    /// </versioning>
    public class TXTLogFactory : LogFactory
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 
        /// </summary>
        protected TXTLogFactory()
        {
        }  
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        #region Create & Get
        /// <summary>
        /// 获取日志工厂对象
        /// </summary>
        /// <returns></returns>
        public static LogFactory GetInstance()
        {
            return Instance ?? (Instance = new TXTLogFactory());
        }

        /// <summary>
        /// 创建日志信息格式
        /// </summary>
        /// <param name="logFormatName"></param>
        /// <returns></returns>
        public override LogFormatter CreateLogFormatter(string logFormatName)
        {
            return LogManager.GetLogFormatter(logFormatName);
        }
        /// <summary>
        /// 创建日志服务
        /// </summary>
        /// <param name="logConfig"></param>
        /// <returns></returns>
        public override LoggingService CreateLogService(LogServiceConfig logConfig)
        {
            LoggingService logService = new TXTLogService(logConfig);
            logService.SetLogPersistenceService(new StreamFileLPS(logConfig));
            logService.SetLogFormatter(LogManager.GetLogFormatter(logConfig));
            LogManager.RegisterLogService(LogManager.GetLogName(logConfig), logService);
            return logService;
        }

        /// <summary>
        /// 默认日志信息格式
        /// </summary>
        /// <returns></returns>
        public override LogFormatter DefaultLogFormatter()
        {
            return LogManager.GetLogFormatter("TEXT");
        }
        #endregion

        #region Service
        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <param name="logConfig"></param>
        /// <returns></returns>
        public static string GetLogFilePath(LogServiceConfig logConfig)
        {
            return logConfig.LogFilePath;
        }

        /// <summary>
        /// 获取日志文件子路径
        /// </summary>
        /// <param name="logConfig"></param>
        /// <returns></returns>
        public static string GetLogSubFolder(LogServiceConfig logConfig)
        {
            return logConfig.LogSubFolder;
        }

        /// <summary>
        /// 根据日志参数获取日志文件名
        /// </summary>
        /// <param name="logConfig"></param>
        /// <returns></returns>
        public static string GetLogFileName(LogServiceConfig logConfig)
        {
            return logConfig.LogFileNameFormatter;
        }

        /// <summary>
        /// 获取日志写入间隔时间
        /// </summary>
        /// <param name="logconfig"></param>
        /// <returns></returns>
        public static int GetLogPersistInterval(LogServiceConfig logconfig)
        {
            return logconfig.LogPersistInterval;
        }

        /// <summary>
        /// 获取日志服务级别
        /// </summary>
        /// <param name="logconfig"></param>
        /// <returns></returns>
        public static Int32 GetLogGrade(LogServiceConfig logconfig)
        {
            return logconfig.LogGrade;
        }
        #endregion

        #region Header
        /// <summary>
        /// 获取日志分隔符
        /// </summary>
        /// <returns></returns>
        public static string GetLogSeperator(LogHeader logHeader)
        {
            return logHeader.LogSeperator;
        }

        /// <summary>
        /// 获取日志产生日期
        /// </summary>
        /// <param name="logHeader"></param>
        /// <returns></returns>
        public static DateTime GetLogGenTime(LogHeader logHeader)
        {
            return logHeader.LogGenTime;
        }
        #endregion

        #region Body
        /// <summary>
        /// 获取日志体字符串
        /// </summary>
        /// <param name="logBody"></param>
        /// <returns></returns>
        public static string GetLogBodyString(LogBody logBody)
        {
            return logBody.LogBodyString;
        }

        /// <summary>
        /// 设置日志体字符串
        /// </summary>
        /// <param name="logBody"></param>
        /// <param name="message"></param>
        public static void SetLogBodyString(LogBody logBody, String message)
        {
            logBody.LogBodyString = message;
        }

        /// <summary>
        /// 设置日志体字符串
        /// </summary>
        /// <param name="logBody"></param>
        /// <param name="msgHeader"></param>
        /// <param name="message"></param>
        /// <remarks>Version:1.1.0.0</remarks>
        public static void SetLogBodyString(LogBody logBody, String msgHeader, String message)
        {
            logBody.LogBodyString = msgHeader + message;
        }
        #endregion
        #endregion

        #region Event -- 事件

        #endregion
    }
}
