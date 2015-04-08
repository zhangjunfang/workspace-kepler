using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LogService;
using LogService.TXT;

namespace HXC.UI.Library
{
    /// <summary>
    /// LibraryAssistant
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/19 15:57:38</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class LibraryAssistant
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        internal static LoggingService UILogService = CreateLogService("UILibrary", "UILibrary");
        #endregion

        #region Method -- 方法
        #region 日志服务信息 Add by kord
        internal static readonly LogFormatter Logformartter = TXTLogFormatter.GetInstance();
        internal static readonly LogFactory LogFactory = TXTLogFactory.GetInstance();
        internal static readonly Dictionary<String, LoggingService> LoggingServicePool = new Dictionary<string, LoggingService>();
        /// <summary>
        /// 创建文本日志服务
        /// </summary>
        /// <param name="logName">日志名称(根据此名称生成日志文件名称)</param>
        /// <param name="logFolder">日志文件夹名称(生成的日志文件放在此文件夹内)</param>
        /// <param name="logGrade">日志级别(开发默认为1,正式发版为3,可忽略此参数)</param>
        /// <param name="isStart">是否启动日志服务(默认为启动,可忽略此参数)</param>
        /// <returns></returns>
        internal static LoggingService CreateLogService(String logName, String logFolder, Int32 logGrade = 1, Boolean isStart = true)
        {
            var logKey = String.Format("{0}+{1}", logName, logFolder);
            if (LoggingServicePool.ContainsKey(logKey))
            {
                return LoggingServicePool[logKey];
            }
            if (LogManager.GetLogFormatter("TEXTLogFormatter") == null) LogManager.RegisterLogFormatter("TEXTLogFormatter", Logformartter);
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
            LoggingServicePool.Add(logKey, logService);
            if (isStart) logService.Start();
            return logService;
        }
        #endregion
        #endregion

        #region Event -- 事件

        #endregion
    }
}
