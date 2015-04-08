
using CloudPlatSocket.Protocol;

namespace CloudPlatSocket
{
    /// <summary>
    /// 协议转换类
    /// </summary>
    public class ProtocolHandler
    {
        /// <summary>
        /// 协议转换
        /// </summary>
        /// <param name="msgId">主消息ID</param>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static IProtocol GetProtocol(string msgId, string[] arrays)
        {
            if (msgId == LoginProtocol.id)
            {
                //登录协议
                return new LoginProtocol(arrays);
            }
            else if (ResultProtocol.ids.Contains(msgId))
            {
                //返回结果协议
                return new ResultProtocol(arrays);
            }
            else if (msgId == UploadDataProtocol.id)
            {
                //数据上传协议
                return new UploadDataProtocol(arrays);
            }
            else if (FactoryProtocol.ids.Contains(msgId))
            {
                //车厂数据协议
                return new FactoryProtocol(arrays);
            }
            else if (AnnounceProtocol.ids.Contains(msgId))
            {
                //公告协议
                return new AnnounceProtocol(arrays);
            }            
            else if (msgId == ControlProtocol.id)
            {
                //下行控制协议
                return new ControlProtocol(arrays);
            }
            return null;
        }

        public static IProtocol GetProtocol(MessageProtocol mp, string msgId)
        {
            if (msgId == LoginProtocol.id)
            {
                //登录协议
                return mp as LoginProtocol;
            }
            else if (ResultProtocol.ids.Contains(msgId))
            {
                //返回结果协议
                return mp as ResultProtocol;
            }
            else if (msgId == UploadDataProtocol.id)
            {
                //数据上传协议
                return mp as UploadDataProtocol;
            }
            else if (FactoryProtocol.ids.Contains(msgId))
            {
                //车厂数据协议
                return mp as FactoryProtocol;
            }
            else if (AnnounceProtocol.ids.Contains(msgId))
            {
                //公告协议
                return mp as AnnounceProtocol;
            }
            else if (msgId == ClientProtocol.id)
            {
                //车厂数据协议
                return mp as ClientProtocol;
            }
            else if (msgId == ControlProtocol.id)
            {
                //下行控制协议
                return mp as ControlProtocol;
            }
            return mp;
        }
    }
}
