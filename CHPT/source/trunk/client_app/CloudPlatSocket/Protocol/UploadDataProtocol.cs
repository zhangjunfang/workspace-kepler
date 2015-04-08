using System;
using Utility.Log;
using CloudPlatSocket.Handler;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 数据上传协议
    /// 主消息ID：U
    /// 客户端数据上传
    /// </summary>
    public class UploadDataProtocol : MessageProtocol, IProtocol
    {
        //消息ID
        public static string id = "U";

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
        public UploadDataProtocol()
        {           
            this.MessageId = id;
        }
        public UploadDataProtocol(string[] arrays)
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

        public override string ToString()
        {
            try
            {
                return String.Format("{0}${1}\n{2}", Operation, Json, base.ToString());
            }
            catch (Exception ex)
            {
                LogAssistant.LogService.WriteLog(ex);
                return "";
            }
        }

        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <returns></returns>
        public string[] GetStrings()
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
        /// <summary> 写错误日志
        /// </summary>
        public new void ErrorLog()
        {
            UploadDataHandler.WriteErrorLog(this);           
        }
        #endregion
    }
}
