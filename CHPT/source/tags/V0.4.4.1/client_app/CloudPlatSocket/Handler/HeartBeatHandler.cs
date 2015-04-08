using HXC_FuncUtility;
using CloudPlatSocket.Protocol;

namespace CloudPlatSocket.Handler
{
    /// <summary>
    /// 心跳包
    /// </summary>
    class HeartBeatHandler
    {
        #region --成员变量
        /// <summary>
        /// 子消息ID
        /// </summary>
        public static string SubMessageId = "L2";        
        #endregion

        #region --成员方法
        /// <summary>
        /// 获取心跳包
        /// </summary>
        /// <returns></returns>
        private static MessageProtocol GetHeartBeat()
        {
            MessageProtocol mp = new MessageProtocol();
            mp.StationId = GlobalStaticObj_Server.Instance.StationID;
            mp.MessageId = LoginProtocol.id;
            //mp.SerialNumber = GlobalStaticObj_Server.Instance.SerialNumber.ToString();            
            mp.SubMessageId = SubMessageId;
            mp.TimeSpan = TimeHelper.GetTimeInMillis();
            return mp;
        }       
        #endregion

        public static void SendHeartBeat()
        {           
            //当发送信息队列为空时,发送心跳包
            ServiceAgent.AddSendQueue(GetHeartBeat());
            //SendMessage(HeartBeatHandler.GetHeartBeat());
        }        
    }
}
