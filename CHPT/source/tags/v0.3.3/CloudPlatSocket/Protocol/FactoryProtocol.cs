using System.Collections.Generic;
using CloudPlatSocket.Handler;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 车厂数据协议
    /// 消息主ID：Y|YD
    /// Y-客户端上传车厂数据协议（上行）
    /// YD-车厂下行数据（下行）
    /// </summary>
    public class FactoryProtocol : MessageProtocol, IProtocol
    {
        #region --成员变量
        /// <summary>
        /// 客户端上传车厂数据协议消息ID
        /// </summary>
        private const string id_y = "Y";
        /// <summary>
        /// 车厂下行数据消息应答
        /// </summary>
        private const string id_yd = "YD";
        /// <summary>
        /// 主消息ID集合
        /// </summary>
        public static List<string> ids = new List<string>() { id_y, id_yd };
        #endregion

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
        }      
        #endregion

        #region --构造函数
        public FactoryProtocol()
        {
            this.IsContolType = false;
            this.MessageId = id_y;            
        }
        public FactoryProtocol(string[] arrays)
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
        /// 获取消息集合,不包括校验码
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
        /// <summary>
        /// 校验是否有效
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsValid(string code)
        {
            return this.ValidCode == code;
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        public void Do(ref bool flag)
        {
            if (this.MessageId == id_yd)
            {
                FactoryHandler.Deal(this);              
            }
        }
        #endregion
    }
}
