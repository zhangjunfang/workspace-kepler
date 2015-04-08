
namespace CloudPlatSocket
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.11.3
    /// Function:socket message protocol
    /// UpdateTime:2014.11.3
    /// </summary>
    public class MessageProtocol : IProtocol
    {
        #region --成员变量
        private IProtocol protocol;
        /// <summary>
        /// 子消息ID
        /// </summary>
        public static string RecSubMessageId = "A2";
        #endregion
        
        #region --成员属性
        private string stationId;
        /// <summary>
        /// 服务站编号
        /// </summary>
        public string StationId
        {
            set
            {
                this.stationId = value;
            }
            get
            {
                return this.stationId;
            }
        }

        //private string dbCode;
        ///// <summary> 获取或设置帐套 
        ///// </summary>
        //public string DbCode
        //{
        //    get
        //    {
        //        return this.dbCode;
        //    }
        //    set
        //    {
        //        this.dbCode = value;
        //    }
        //}

        private string serialNumber;
        /// <summary>
        /// 消息流水号
        /// </summary>
        public string SerialNumber
        {
            set
            {
                this.serialNumber = value;
            }
            get
            {
                return this.serialNumber;
            }
        }

        public int sendCount;
        /// <summary> 发送次数
        /// </summary>
        public int SendCount
        {
            get
            {
                return this.sendCount;
            }
            set
            {
                this.sendCount = value;
            }
        }
        private string messageId;
        /// <summary>
        /// 主消息Id
        /// </summary>
        public string MessageId
        {
            set
            {
                this.messageId = value;
                if (this.protocol == null)
                {
                    this.protocol = ProtocolHandler.GetProtocol(this, this.messageId);
                }
            }
            get
            {
                return this.messageId;
            }
        }

        private string subMessageId;
        /// <summary>
        /// 子消息Id
        /// </summary>
        public string SubMessageId
        {
            set
            {
                this.subMessageId = value;
            }
            get
            {
                return this.subMessageId;
            }
        }

        private string timeSpan;
        /// <summary> 时间戳
        /// </summary>
        public string TimeSpan
        {
            set
            {
                this.timeSpan = value;
            }
            get
            {
                return this.timeSpan;
            }
        }
        private string validCode;
        /// <summary>
        /// 校验码
        /// </summary>
        public string ValidCode
        {
            get
            {
                return this.validCode;
            }
            set
            {
                this.validCode = value;
            }
        }
        #endregion

        #region --扩展属性       
        private bool success;
        /// <summary>
        /// 连接是否成功
        /// </summary>
        public bool Success
        {
            get
            {
                return success;
            }
            set
            {
                this.success = value;
            }
        }
        /// <summary>
        /// 是否为控制指令
        /// </summary>
        public bool IsContolType { get; set; }
        public bool SerialNumberLock { get; set; }
        #endregion

        #region --构造函数
        public MessageProtocol()
        {
            //this.subMessageId = SubMessageIdRec;
        }
        /// <summary>
        /// 消息协议
        /// </summary>
        /// <param name="arrays"></param>
        public MessageProtocol(string[] arrays)
        {
            this.success = true;
            this.stationId = arrays[0];
            this.serialNumber = arrays[1];
            this.messageId = arrays[2];
            this.subMessageId = arrays[3];
            this.timeSpan = arrays[4];
            this.validCode = arrays[arrays.Length - 1];
            this.success = true;
            if (this.subMessageId != RecSubMessageId)
            {
                //消息分发
                this.protocol = ProtocolHandler.GetProtocol(this.messageId, arrays);
            }
        }
        public IProtocol GetRealProtocol()
        {
            return this.protocol;
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <returns></returns>
        public string[] GetStrings()
        {
            if (this.protocol != null)
            {
                return this.protocol.GetStrings();
            }
            string[] arrays = new string[5];
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
            return arrays;
        }
        /// <summary>
        /// 通过校验验证，验证消息是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid(string code)
        {
            if (this.protocol != null)
            {
                return this.protocol.IsValid(code);
            }
            return true;
        }
        /// <summary>
        /// 数据获取是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return this.success;
        }
        public void Do(ref bool flag)
        {
            if (this.protocol != null)
            {
                this.protocol.Do(ref flag);
            }
        }       
        /// <summary>
        /// 写错误日志
        /// </summary>
        public void ErrorLog()
        {
            if (this.protocol != null)
            {
                this.protocol.ErrorLog();
            }
        }        
        #endregion
    }
}