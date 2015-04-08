using CloudPlatSocket.Handler;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 控制指令协议
    /// 主消息ID：CD   
    /// CD-控制指令下行
    /// </summary>
    public class ControlProtocol : MessageProtocol, IProtocol
    {
        #region --成员变量
        /// <summary>
        ///主消息ID
        /// </summary>
        public const string id = "CD";      
        #endregion

        #region --成员属性
        private string controlType;
        /// <summary>
        /// 控制类型
        /// </summary>
        public string ControlType
        {
            get
            {
                return this.controlType;
            }
        }        
        #endregion

        #region --构造函数
        public ControlProtocol()
        {            
            this.IsContolType = true;
            this.MessageId = id;
        }
        public ControlProtocol(string[] arrays)
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
                this.controlType = arrays[5];
                if (arrays.Length == 7)
                {
                    this.ValidCode = arrays[6];
                }               
                this.IsContolType = true;
            }
        }
        #endregion

        #region --成员方法        
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="protocol"></param>
        public new void Do(ref bool flag)
        {
            if (this.MessageId == id)
            {
                ContolHandler.Deal(this);
            }
        }
        /// <summary>
        /// 校验是否有效
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public new bool IsValid(string code)
        {
            return this.ValidCode == code;
        }     
        #endregion
    }
}
