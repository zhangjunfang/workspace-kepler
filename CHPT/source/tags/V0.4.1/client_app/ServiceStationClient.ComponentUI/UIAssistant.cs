using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kord.LogService;
using Kord.LogService.TXT;

namespace ServiceStationClient.ComponentUI
{
    public static class UIAssistant
    {
        #region LogServer -- 日志服务
        private static LoggingService _uiLogService;

        public static LoggingService UILogService
        {
            get { return _uiLogService ?? (_uiLogService = CreateLogService("ComponentUI", "")); }
        }

        #region 日志服务信息 Add by kord
        private static readonly LogFormatter Logformartter = TXTLogFormatter.GetInstance();
        private static readonly LogFactory LogFactory = TXTLogFactory.GetInstance();
        private static readonly Dictionary<String, LoggingService> LoggingServicePool = new Dictionary<string, LoggingService>();
        private static LoggingService CreateLogService(String logName, String logFolder, Int32 logGrade = 1, Boolean isStart = true)
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
    }
}
