using System;

namespace Kord.LogService
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 11:53:32</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class LogFactory
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 日志工厂
        /// </summary>
        protected LogFactory()
        {
        }
        #endregion

        #region Field -- 字段
        protected static LogFactory Instance;   //日志工厂
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 默认日志信息格式
        /// </summary>
        /// <returns></returns>
        public virtual LogFormatter DefaultLogFormatter()
        {
            return null;
        }

        /// <summary>
        /// 默认日志持久化服务
        /// </summary>
        /// <returns></returns>
        public virtual ILogPersistenceService DefaultLGS()
        {
            return null;
        }

        /// <summary>
        /// 创建日志信息格式
        /// </summary>
        /// <param name="logFormatName"></param>
        /// <returns></returns>
        public virtual LogFormatter CreateLogFormatter(String logFormatName)
        {
            return null;
        }
        /// <summary>
        /// 根据日志配置创建日志服务
        /// </summary>
        /// <param name="logCfg">日志配置</param>
        /// <returns></returns>
        public virtual LoggingService CreateLogService(LogServiceConfig logCfg)
        {
            return null;
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
