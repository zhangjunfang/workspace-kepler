using System.Collections.Generic;
using CloudPlatSocket.Handler;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 公告数据协议
    /// 主消息ID:S|SD
    /// S-支撑系统数据上传（上行）
    /// SD-支撑系统数据同步指令（下行）
    /// </summary>
    public class AnnounceProtocol : MessageProtocol, IProtocol
    {
        #region --成员变量
        /// <summary>
        /// 支撑系统数据上传消息ID
        /// </summary>
        private const string id_s = "S";
        /// <summary>
        /// 撑系统数据同步指令消息ID
        /// </summary>
        private const string id_sd = "SD";
        /// <summary>
        /// 主消息ID集合
        /// </summary>
        public static List<string> ids = new List<string>() { id_s, id_sd };
        #endregion

        #region --成员属性
        private string operation;
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                this.operation = value;
            }
        }
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
        public AnnounceProtocol()
        {
            this.IsContolType = false;           
            this.MessageId = id_s;
        }
        public AnnounceProtocol(string[] arrays)
        {
            if (arrays.Length > 6)
            {
                //服务站ID  Y可能为服务站sap
                this.StationId = arrays[0];
                //流水号
                this.SerialNumber = arrays[1];
                this.MessageId = arrays[2];
                this.SubMessageId = arrays[3];
                this.TimeSpan = arrays[4];
                this.operation = arrays[5];
                this.json = arrays[6];
                if (arrays.Length == 8)
                {
                    this.ValidCode = arrays[7];
                }                
            }
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <returns></returns>
        public override string[] GetStrings()
        {
            string[] arrays = new string[7];
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
            //操作类别
            arrays[5] = this.operation;
            //json对象
            arrays[6] = this.json;
            return arrays;
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
        /// <summary>
        /// 接收消息后，执行操作
        /// </summary>
        public new void Do(ref bool flag)
        {
            if (this.SubMessageId == id_sd)
            {
                AnnounceHandler.Deal(this);
            }
        }
        #endregion
    }
}
