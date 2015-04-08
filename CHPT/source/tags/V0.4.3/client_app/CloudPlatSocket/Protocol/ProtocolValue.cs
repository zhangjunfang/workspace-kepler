
namespace CloudPlatSocket.Protocol
{
    public enum OperateType
    {
        NONE=0,
        /// <summary>
        /// 可增、修、删
        /// </summary>
        AUD=1,
        /// <summary>
        /// 仅可增
        /// </summary>
        A=2,
        /// <summary>
        /// 可增、修
        /// </summary>
        AU=3,      
        /// <summary>
        /// 新增、删除
        /// </summary>
        AD=4,
        /// <summary>
        /// 仅仅可删除
        /// </summary>
        D=5,
        /// <summary>
        /// 关联关系
        /// </summary>
        PK=6
    }
    public class ProtocolValue
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 关联表
        /// </summary>
        public string PreTableName { get; set; }
        /// <summary>
        /// 关联表主键
        /// </summary>
        public string PreKey { get; set; }
        /// <summary>
        /// 上传表外键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 可执行操作
        /// </summary>
        public OperateType Operate { get; set; }
        
    }
}
