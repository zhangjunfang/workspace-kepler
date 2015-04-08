using System;
using System.Diagnostics;

namespace Kord.LogService
{
    /// <summary>
    /// 日志信息格式化
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 11:48:36</datetime>
    ///         <comment>create</comment>
    ///     </version>
    ///     <version number="1.1.1.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 11:48:36</datetime>
    ///         <comment>
    ///             日志格式化时,StackFrame将由日志服务给予而不是在格式化时获取
    ///         </comment>
    ///     </version>
    /// </versioning>
    public class LogFormatter
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 日志信息格式化
        /// </summary>
        protected LogFormatter()
        {
        }
        #endregion

        #region Field -- 字段
        protected static LogFormatter Instance;
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 日志记录时间格式化
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public virtual String FormatTime(DateTime time)
        {
            return null;
        }

        /// <summary>
        /// 日志记录格式化
        /// </summary>
        /// <param name="logrec">日志记录</param>
        public virtual void FormatLogRecord(LogRecord logrec)
        {

        }

        /// <summary>
        /// 日志文件名格式化
        /// </summary>
        /// <param name="logFileName">文件名</param>
        /// <returns></returns>
        public virtual String GetLogFileName(String logFileName)
        {
            return null;
        }
        /// <summary>
        /// 格式化日志记录
        /// </summary>
        /// <param name="logCfg">日志服务配置</param>
        /// <returns></returns>
        public virtual LogRecord FormatLogRecord(LogServiceConfig logCfg)
        {
            return null;
        }

        /// <summary>
        /// 格式化日志记录
        /// </summary>
        /// <param name="logHeader"></param>
        /// <param name="logBody"></param>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        public virtual LogRecord FormatLogRecord(LogHeader logHeader, LogBody logBody, StackFrame stackFrame)
        {
            return null;
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
