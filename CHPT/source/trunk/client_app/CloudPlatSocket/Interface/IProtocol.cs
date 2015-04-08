
namespace CloudPlatSocket
{
    public interface IProtocol
    {        
        /// <summary>
        /// 获取消息集合,不包括校验码
        /// </summary>
        /// <returns></returns>
        string[] GetStrings();        
        /// <summary>
        /// 校验是否有效
        /// </summary>
        /// <param name="code">校验码</param>
        /// <returns></returns>
        bool IsValid(string code);
        /// <summary>
        /// 消息操作
        /// </summary>
        /// <param name="flag"></param>
        void Do(ref bool flag);       
        /// <summary>
        /// 写错误日志
        /// </summary>
        void ErrorLog();       
    }
}