using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Kord.LogService.TXT
{
    /// <summary>
    /// 文本日志服务
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 14:42:27</datetime>
    ///         <comment>create</comment>
    ///     </version>
    ///     <version number="1.1.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/16 15:10:00</datetime>
    ///         <comment>
    ///             添加参数为异常消息的日志写入方法
    ///         </comment>
    ///     </version>
    ///     <version number="1.1.1.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/17 10:12:00</datetime>
    ///         <comment>
    ///             修复获取StackFrame中的异常信息时所引错误导致异常信息无法正确获取到异常信息
    ///         </comment>
    ///     </version>
    ///     <version number="1.1.2.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/01/04 11:18:00</datetime>
    ///         <comment>
    ///             1.更改日志写入线程为后台线程(可能会出现当应用程序关闭时日志线程还有未写完的日志信息,则这些日志信息将不会继续写入日志文件)
    ///             2.更改默认的日志记录级别为3级
    ///             3.记录当前日志服务线程,并在日志停止(Stop)时终止当前日志记录线程
    ///         </comment>
    ///     </version>
    ///     <version number="1.1.3.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/01/07 19:49:00</datetime>
    ///         <comment>
    ///             添加参数带日志消息头的日志写入方法
    ///         </comment>
    ///     </version>
    /// </versioning>
    public class TXTLogService : LoggingService
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 文本日志服务
        /// </summary>
        /// <param name="logconfig"></param>
        public TXTLogService(LogServiceConfig logconfig) : base(logconfig)
        {
            ProcessIsBegin = true;
            var lpinterval = TXTLogFactory.GetLogPersistInterval(logconfig);

            LogPersistInterval = lpinterval == 0 ? DefaultPersistInterval : lpinterval;
        }
        #endregion

        #region Field -- 字段
        /// <summary>
        /// 日志线程是否已开启
        /// </summary>
        public bool ProcessIsBegin = false;
        /// <summary>
        /// 日志线程是否已关闭
        /// </summary>
        public bool ProcessIsClosed = false;
        /// <summary>
        /// 日志写入时间间隔
        /// </summary>
        public int LogPersistInterval = 0;
        //**********VERSION 1.1.2.0 START**********//
        private Thread _thread;  //日志线程
        private const Int32 DefaultLogGrade = 3; //默认日志级别
        //**********VERSION 1.1.2.0 END**********//

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 初始化日志服务头信息
        /// </summary>
        /// <returns></returns>
        public LogHeader PrepareLogHeader()
        {
            var logHeader = new LogHeader
            {
                LogInfo = LogConfig.LogInfo,
                LogName = LogConfig.LogName,
                LogSeperator = LogConfig.LogSeperator,
                LogType = LogConfig.LogType,
                LogVersion = LogConfig.LogVersion,
                LogGenTime = DateTime.Now,
                LogTraceEnable = LogConfig.LogTraceEnable
            };
            return logHeader;
        }

        /// <summary>
        /// 初始化日志体信息
        /// </summary>
        /// <param name="logMessage">日志体信息</param>
        /// <returns>初始完成的日志体信息结构</returns>
        public LogBody PrepareLogBody(String logMessage)
        {
            var logBody = new LogBody();
            TXTLogFactory.SetLogBodyString(logBody, logMessage);
            return logBody;
        }

        /// <summary>
        /// 初始化日志体信息
        /// </summary>
        /// <param name="msgHeader">日志信息头部</param>
        /// <param name="logMessage">日志体信息</param>
        /// <returns>初始完成的日志体信息结构</returns>
        /// <remarks>Version:1.1.3.0</remarks>
        public LogBody PrepareLogBody(String msgHeader, String logMessage)
        {
            var logBody = new LogBody();
            TXTLogFactory.SetLogBodyString(logBody, msgHeader, logMessage);
            return logBody;
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="logMessage">日志信息</param>
        public override void WriteLog(String logMessage)
        {
            if (DefaultLogGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            //**********VERSION 1.1.1.0 START**********//
            var stackFrames = new StackTrace(true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames[1];
            //**********VERSION 1.1.1.0 END**********//
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(logMessage), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="exception">异常信息</param>
        public override void WriteLog(Exception exception)
        {
            if (DefaultLogGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            //**********VERSION 1.1.1.0 START**********//
            var stackFrames = new StackTrace(exception, true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames.Last();
            //**********VERSION 1.1.1.0 END**********//
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(exception.Message), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="msgHeader">异常消息头信息</param>
        /// <param name="exception">异常信息</param>
        /// <remarks>Version:1.1.3.0</remarks>
        public override void WriteLog(String msgHeader, Exception exception)
        {
            if (DefaultLogGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            var stackFrames = new StackTrace(exception, true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames.Last();
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(exception.Message), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="theRecordGrade">日志级别</param>
        /// <param name="logMessage">日志信息</param>
        public override void WriteLog(Int32 theRecordGrade, string logMessage)
        {
            if (theRecordGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            //**********VERSION 1.1.1.0 START**********//
            var stackFrames = new StackTrace(true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames[1];
            //**********VERSION 1.1.1.0 END**********//
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(logMessage), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="theRecordGrade">日志级别</param>
        /// <param name="exception">异常信息</param>
        public override void WriteLog(Int32 theRecordGrade, Exception exception)
        {
            if (theRecordGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            //**********VERSION 1.1.1.0 START**********//
            var stackFrames = new StackTrace(exception, true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames.Last();
            //**********VERSION 1.1.1.0 END**********//
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(exception.Message), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="theRecordGrade">日志级别</param>
        /// <param name="msgHeader">日志消息头信息</param>
        /// <param name="exception">异常消息信息</param>
        /// <remarks>Version:1.1.3.0</remarks>
        public override void WriteLog(Int32 theRecordGrade, String msgHeader, Exception exception)
        {
            if (theRecordGrade < TXTLogFactory.GetLogGrade(LogConfig)) return;
            var stackFrames = new StackTrace(exception, true).GetFrames();
            StackFrame stackFrame = null;
            if (stackFrames != null) stackFrame = stackFrames.Last();
            var logrec = LogFormatter.FormatLogRecord(PrepareLogHeader(), PrepareLogBody(msgHeader, exception.Message), stackFrame);
            if (logrec != null)
            {
                LogRecordList.Add(logrec);
            }
        }

        /// <summary>
        /// 启动日志记录
        /// </summary>
        public override void Start()
        {
            //**********VERSION 1.1.2.0 START**********//
            if (_thread != null)
            {
                _thread.Start();
                return;
            }
            //**********VERSION 1.1.2.0 END**********//
            LogPersistenceService.SetPersistParam(LogFormatter.GetLogFileName(TXTLogFactory.GetLogFileName(LogConfig)));
            //**********VERSION 1.1.2.0 START**********//
            _thread = new Thread(Run) { IsBackground = true };
            _thread.Start();
            //new Thread(Run).Start();
            //**********VERSION 1.1.2.0 END**********//
        }

        /// <summary>
        /// 停止日志记录
        /// </summary>
        public override void Stop()
        {
            ProcessIsBegin = false;
            //**********VERSION 1.1.2.0 START**********/
            if (_thread != null) _thread.Join(1000);
            //**********VERSION 1.1.2.0 END**********//
            LogManager.UnregisterLogService(LogManager.GetLogName(LogConfig));
        }

        /// <summary>
        /// 执行日志记录
        /// </summary>
        /// <exception cref="GeneralException"></exception>
        protected void Run()
        {
            try
            {
                do
                {
                    ProcessIsClosed = false;
                    Thread.Sleep(LogPersistInterval);
                    if (LogRecordList.Count > 0)
                    {
                        LogPersistenceService.Save(LogRecordList);
                    }
                } while (ProcessIsBegin || LogRecordList.Count > 0);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.GetException(20002001, "TXTLogService", ex);
            }
            finally
            {
                ProcessIsBegin = false;
                ProcessIsClosed = true;
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
