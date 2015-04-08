using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace LogService
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 14:12:22</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class LogManager
    {
        #region Constructor -- 构造函数
        static LogManager()
        {
            
        }
        #endregion

        #region Field -- 字段
        static readonly Dictionary<String, LoggingService> LogServiceDict = new Dictionary<string, LoggingService>();
        static readonly Dictionary<string, LogFactory> LogFactoryDict = new Dictionary<string, LogFactory>();
        static readonly Dictionary<string, LogFormatter> LogFormatterDict = new Dictionary<string, LogFormatter>();
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        #region Factory
        /// <summary>
        /// 注册日志工厂
        /// </summary>
        /// <param name="logFactoryName">工厂名称</param>
        /// <param name="logFactory">实例</param>
        public static void RegisterLogFactory(String logFactoryName, LogFactory logFactory)
        {
            if (!LogFactoryDict.ContainsKey(logFactoryName))
                LogFactoryDict.Add(logFactoryName, logFactory);
        }
        /// <summary>
        /// 移除日志工厂
        /// </summary>
        /// <param name="logFactoryName">工厂名称</param>
        public static void UnRegisterLogFactory(String logFactoryName)
        {
            if (LogFactoryDict.ContainsKey(logFactoryName))
                LogFactoryDict.Remove(logFactoryName);
        }
        /// <summary>
        /// 根据日志工厂名称获取日志工厂
        /// </summary>
        /// <param name="logFactoryName"></param>
        /// <returns></returns>
        public static LogFactory GetLogFactory(String logFactoryName)
        {
            return LogFactoryDict.ContainsKey(logFactoryName) ? LogFactoryDict[logFactoryName] : null;
        }
        /// <summary>
        /// 获取日志工厂名称
        /// </summary>
        /// <param name="logCfg"></param>
        /// <returns></returns>
        public static string GetLogFactoryName(LogServiceConfig logCfg)
        {
            return logCfg.LogFactoryName;
        }
        #endregion

        #region Service
        /// <summary>
        /// 注册日志服务
        /// </summary>
        /// <param name="logServiceName">名称</param>
        /// <param name="logService">日志服务名称</param>
        public static void RegisterLogService(String logServiceName, LoggingService logService)
        {
            if (!LogServiceDict.ContainsKey(logServiceName))
                LogServiceDict.Add(logServiceName, logService);
        }
        /// <summary>
        /// 移除日志服务
        /// </summary>
        /// <param name="logServiceName">日志服务名称</param>
        public static void UnregisterLogService(String logServiceName)
        {
            if (LogServiceDict.ContainsKey(logServiceName))
                LogServiceDict.Remove(logServiceName);
        }
        /// <summary>
        /// 根据日志配置文件创建日志服务
        /// </summary>
        /// <param name="logCfg"></param>
        /// <returns></returns>
        public static LoggingService CreateLogService(LogServiceConfig logCfg)
        {
            LoggingService logService;
            var logName = GetLogName(logCfg);
            var logFactoryName = GetLogFactoryName(logCfg);
            if (LogServiceDict.TryGetValue(logName, out logService))
            {
                return logService;
            }
            var logFactory = GetLogFactory(logFactoryName);
            return logFactory != null ? GetLogFactory(logFactoryName).CreateLogService(logCfg) : null;
        }
        /// <summary>
        /// 根据日志服务名称获取日志服务
        /// </summary>
        /// <param name="logName">日志服务名称</param>
        /// <returns>日志服务</returns>
        public static LoggingService GetLogService(string logName)
        {
            return LogServiceDict.ContainsKey(logName) ? LogServiceDict[logName] : null;
        }
        /// <summary>
        /// 获取日志名称
        /// </summary>
        /// <param name="logCfg">日志配置信息</param>
        /// <returns></returns>
        public static string GetLogName(LogServiceConfig logCfg)
        {
            return logCfg.LogName;
        }
        #endregion

        #region Formatter
        /// <summary>
        /// 注册日志信息格式
        /// </summary>
        /// <param name="logFormatName">日志格式名称</param>
        /// <param name="logFormatter">日志信息格式</param>
        public static void RegisterLogFormatter(String logFormatName, LogFormatter logFormatter)
        {
            if(!LogFormatterDict.ContainsKey(logFormatName))
                LogFormatterDict.Add(logFormatName, logFormatter);
        }
        /// <summary>
        /// 移除日志信息格式
        /// </summary>
        /// <param name="logFormatName">日志格式名称</param>
        public static void UnRegisterLogFormatter(String logFormatName)
        {
            if (LogFormatterDict.ContainsKey(logFormatName))
                LogFormatterDict.Remove(logFormatName);
        }
        /// <summary>
        /// 根据日志信息格式名称获取日志信息格式
        /// </summary>
        /// <param name="logFormatName">日志信息格式名称</param>
        /// <returns>日志信息格式</returns>
        public static LogFormatter GetLogFormatter(String logFormatName)
        {
            return LogFormatterDict.ContainsKey(logFormatName) ? LogFormatterDict[logFormatName] : null;
        }
        /// <summary>
        /// 根据日志参数获取日志格式信息
        /// </summary>
        /// <param name="logCfg"></param>
        /// <returns></returns>
        public static LogFormatter GetLogFormatter(LogServiceConfig logCfg)
        {
            return GetLogFormatter(logCfg.LogFormatter);
        }
        /// <summary>
        /// 获取日志格式名称
        /// </summary>
        /// <param name="logCfg">日志配置信息</param>
        /// <returns></returns>
        public static string GetLogFormatterName(LogServiceConfig logCfg)
        {
            return logCfg.LogFormatter;
        }

        #region StackTrace 信息追踪
        /// <summary>
        /// 获取追踪信息所在文件
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static String GetClassName(StackFrame frame)
        {
            const String unknow = "NULL";
            if (frame == null) return unknow;
            var className = frame.GetFileName();
            return String.IsNullOrEmpty(className)
                ? unknow
                : className;
        }
        /// <summary>
        /// 获取追踪信息所在方法
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static String GetMethodName(StackFrame frame)
        {
            const String unknow = "NULL";
            if (frame == null) return unknow;
            var methodName = frame.GetMethod().Name;
            return String.IsNullOrEmpty(methodName)
                ? unknow
                : methodName;
        }
        /// <summary>
        /// 获取追踪信息所在行
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static String GetLineNumber(StackFrame frame)
        {
            const String unknow = "NULL";
            if (frame == null) return unknow;
            var lineNumber = frame.GetFileLineNumber();
            return String.IsNullOrEmpty(lineNumber.ToString(CultureInfo.InvariantCulture))
                ? unknow
                : lineNumber.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 获取追踪信息所在列
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static String GetColumnNumber(StackFrame frame)
        {
            const String unknow = "NULL";
            if (frame == null) return unknow;
            var columnNumber = frame.GetFileColumnNumber();
            return String.IsNullOrEmpty(columnNumber.ToString(CultureInfo.InvariantCulture))
                ? unknow
                : columnNumber.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #endregion
        #endregion

        #region Event -- 事件

        #endregion
    }
}
