using System;
using System.Collections.Generic;

namespace Kord.LogService
{
    /// <summary>
    /// LogService
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 11:51:32</datetime>
    ///         <comment>create</comment>
    ///     </version>
    ///     <version number="1.1.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/16 5:05:00</datetime>
    ///         <comment>
    ///             添加参数为异常的日志写入方法
    ///         </comment>
    ///     </version>
    ///     <version number="1.2.1.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/01/07 19:47:00</datetime>
    ///         <comment>
    ///             添加参数带日志消息头的日志写入方法
    ///         </comment>
    ///     </version>
    /// </versioning>
    public class LoggingService
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 创建日志服务
        /// </summary>
        /// <param name="logCfg">日志服务配置文件</param>
        protected LoggingService(LogServiceConfig logCfg)
        {
            DefaultPersistInterval = 300;
            LogRecordList = new List<LogRecord>();
            LogConfig = logCfg;
        }
        #endregion

        #region Field -- 字段
        protected ILogPersistenceService LogPersistenceService; //日志写入间隔
        protected LogFormatter LogFormatter;    //日志信息格式
        protected int DefaultPersistInterval;   //默认日志写入间隔
        protected List<LogRecord> LogRecordList;    //日志信息列表
        protected LogServiceConfig LogConfig;   //日志服务信息
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="logMessage">日志消息信息</param>
        public virtual void WriteLog(String logMessage)
        {
            var list = new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">异常消息信息</param>
        /// <remarks>Version:1.1.0.0</remarks>
        public virtual void WriteLog(Exception exception)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgHeader">日志消息头信息</param>
        /// <param name="exception">异常消息信息</param>
        /// <remarks>Version:1.2.1.0</remarks>
        public virtual void WriteLog(String msgHeader, Exception exception)
        {

        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="logGrade">日志级别</param>
        /// <param name="logMessage">日志信息信息</param>
        public virtual void WriteLog(Int32 logGrade, String logMessage)
        {
        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="logGrade">日志级别</param>
        /// <param name="exception">异常消息信息</param>
        /// <remarks>Version:1.1.0.0</remarks>
        public virtual void WriteLog(Int32 logGrade, Exception exception)
        {
        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="logGrade">日志级别</param>
        /// <param name="logHeader">日志消息头信息</param>
        /// <param name="exception">异常消息信息</param>
        /// <remarks>Version:1.2.1.0</remarks>
        public virtual void WriteLog(Int32 logGrade,String logHeader, Exception exception)
        {
        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="logObject"></param>
        public virtual void WriteLog(Object logObject)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lps"></param>
        public void SetLogPersistenceService(ILogPersistenceService lps)
        {
            if (lps != null)
            {
                LogPersistenceService = lps;
            }
        }

        /// <summary>
        /// 设置日志信息格式
        /// </summary>
        /// <param name="logf"></param>
        public virtual void SetLogFormatter(LogFormatter logf)
        {
            if (logf != null)
                LogFormatter = logf;
        }

        /// <summary>
        /// 启动日志服务
        /// </summary>
        public virtual void Start()
        {
        }

        /// <summary>
        /// 停止日志服务
        /// </summary>
        public virtual void Stop()
        {
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
