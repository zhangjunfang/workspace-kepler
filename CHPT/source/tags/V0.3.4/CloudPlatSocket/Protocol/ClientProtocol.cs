using CloudPlatSocket.Handler;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 在线客户端信息上传协议
    /// 主消息ID：T
    /// </summary>
    public class ClientProtocol : MessageProtocol, IProtocol
    {
        /// <summary>
        /// 主消息ID
        /// </summary>
        public const string id = "T";       

        #region --成员属性
        private string json;
        /// <summary>
        /// Json对象
        /// </summary>
        public string Json
        {
            get
            {
                return this.json;
            }
            set
            {
                this.json = value;
            }
        }       
        #endregion

        #region --构造函数
        public ClientProtocol()
        {            
            this.IsContolType = false;
            this.MessageId = id;            
        }
        public ClientProtocol(string[] arrays)
        {
            if (arrays.Length > 5)
            {
                //服务站ID  Y可能为服务站sap
                this.StationId = arrays[0];
                //流水号
                this.SerialNumber = arrays[1];
                this.MessageId = arrays[2];
                this.SubMessageId = arrays[3];
                this.TimeSpan = arrays[4];
                this.json = arrays[5];
                if (arrays.Length == 7)
                {
                    this.ValidCode = arrays[6];
                }               
                this.IsContolType = false;
            }
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <returns></returns>
        public string[] GetStrings()
        {
            string[] arrays = new string[6];
            //服务站编号
            arrays[0] = this.StationId;
            //消息流水号
            arrays[1] = this.SerialNumber;
            //消息主Id
            arrays[2] = this.MessageId;
            //子消息Id
            arrays[3] = this.SubMessageId;
            //时间戳
            arrays[4] = this.TimeSpan;
            //json对象
            arrays[5] = this.json;

            return arrays;
        }        
        /// <summary> 写错误日志
        /// </summary>
        public void ErrorLog()
        {
            ContolHandler.WriteErrorLog(this);
        }
        #endregion       
    }
}
