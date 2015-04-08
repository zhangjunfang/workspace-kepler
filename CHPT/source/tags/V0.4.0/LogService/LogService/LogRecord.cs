using System;

namespace Kord.LogService
{
    /// <summary>
    /// 日志信息
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 10:34:03</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    [Serializable]
    public class LogRecord
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 日志信息类型
        /// </summary>
        public Type RecordType { get; set; }
        /// <summary>
        /// 日志消息
        /// </summary>
        public String LogMessage { get; set; }
        /// <summary>
        /// 日志对象
        /// </summary>
        public Object LogObject { get; set; }
        #endregion

        #region Method -- 方法

        #endregion

        #region Event -- 事件

        #endregion
    }
}
