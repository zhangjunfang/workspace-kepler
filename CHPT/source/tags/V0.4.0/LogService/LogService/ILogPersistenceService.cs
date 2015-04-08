using System;
using System.Collections.Generic;

namespace Kord.LogService
{
    /// <summary>
    /// 日志持久化服务
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/8 10:39:03</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface ILogPersistenceService
    {
        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="logRecordList"></param>
        void Save(List<LogRecord> logRecordList);
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="setting"></param>
        void SetPersistParam(String setting);
        /// <summary>
        /// 读取
        /// </summary>
        void Load();
        #endregion

        #region Event -- 事件

        #endregion
    }
}
