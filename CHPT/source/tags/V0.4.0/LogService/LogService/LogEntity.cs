using System;

namespace Kord.LogService
{
    /// <summary>
    /// 日志头信息
    /// </summary>
    public class LogHeader
    {
        #region Constructor -- 构造函数

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 日志产生时间
        /// </summary>
        public DateTime LogGenTime { get; set; }
        /// <summary>
        /// 日志名称
        /// </summary>
        public String LogName { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public String LogInfo { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public String LogType { get; set; }
        /// <summary>
        /// 日志版本
        /// </summary>
        public String LogVersion { get; set; }
        /// <summary>
        /// 日志分隔符
        /// </summary>
        public String LogSeperator { get; set; }
        /// <summary>
        /// 是否记录日志源信息
        /// </summary>
        public Boolean LogTraceEnable { get; set; }
        #endregion

    }
    /// <summary>
    /// 日志体
    /// </summary>
    public class LogBody
    {
        #region Constructor -- 构造函数

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 日志体消息
        /// </summary>
        public String LogBodyString { get; set; }
        /// <summary>
        /// 日志体对象
        /// </summary>
        public Object LogBodyObject { get; set; }
        #endregion
    }
    /// <summary>
    /// 日志消息配置
    /// </summary>
    public class LogMessage
    {
        #region Constructor -- 构造函数

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 消息Id
        /// </summary>
        public String MessageId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 消息级别
        /// </summary>
        public Int32 MessageGrade { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public String MessageName { get; set; }
        /// <summary>
        /// 消息源模块
        /// </summary>
        public String MessageModuleName { get; set; }
        /// <summary>
        /// 消息源类名
        /// </summary>
        public String MessageClassName { get; set; }
        /// <summary>
        /// 消息源方法名
        /// </summary>
        public String MessageMethodName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public String MessageBody { get; set; }
        #endregion
    }
    /// <summary>
    /// 日志服务信息配置
    /// </summary>
    public class LogServiceConfig
    {
        #region Property -- 属性
        /// <summary>
        /// 日志ID
        /// </summary>
        public String LogID { get; set; }
        /// <summary>
        /// 日志工厂名称
        /// </summary>
        public String LogFactoryName { get; set; }
        /// <summary>
        /// 日志名称
        /// </summary>
        public String LogName { get; set; }
        /// <summary>
        /// 日志分隔符
        /// </summary>
        public String LogSeperator { get; set; }
        /// <summary>
        /// 日志文件路径,末尾包含'\'
        /// </summary>
        public String LogFilePath { get; set; }
        /// <summary>
        /// 日志文件子文件夹,开头不包含'\'
        /// </summary>
        public String LogSubFolder { get; set; }
        /// <summary>
        /// 日志文件名称
        /// </summary>
        /// <remarks>
        /// 格式: 文件名|日期格式|分割单位*分割大小|连接符
        /// 示例: SystemLog|yyyyMMdd|M*5|_
        /// </remarks>
        public String LogFileNameFormatter { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public String LogType { get; set; }
        /// <summary>
        /// 日志版本
        /// </summary>
        public String LogVersion { get; set; }
        /// <summary>
        /// 日志格式
        /// </summary>
        public String LogFormatter { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public String LogInfo { get; set; }
        /// <summary>
        /// 是否启用日志
        /// </summary>
        public Boolean LogEnable { get; set; }
        /// <summary>
        /// 是否记录日志源信息
        /// </summary>
        public Boolean LogTraceEnable { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        /// <remarks>
        /// 1   DEBUGE  调试用信息,实际运行中不推荐输出
        /// 2   WARN    程序Bug信息,实际运行中不推荐输出
        /// 3   INFO    记录信息,程序运行信息记录
        /// 4   ERROR   错误,程序运行时错误
        /// 5   FATAL   致命错误,程序运行时严重错误
        /// </remarks>
        public Int32 LogGrade { get; set; }
        /// <summary>
        /// 日志记录保存间隔
        /// </summary>
        public Int32 LogPersistInterval { get; set; }
        /// <summary>
        /// 日志备注信息
        /// </summary>
        public String LogDescription { get; set; }
        #endregion
    }
}
