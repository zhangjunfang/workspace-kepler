using System.Collections.Generic;
using SYSModel;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 服务消息应答协议
    /// 主消息ID：A|R
    /// 通用消息应答、链路检测（心跳包）
    /// </summary>
    public class ResultProtocol: MessageProtocol,IProtocol
    {
        #region --成员变量
        /// <summary>
        /// 链路检测应答消息ID
        /// </summary>
        private const string id_a = "A";
        /// <summary>
        /// 通用消息应答
        /// </summary>
        private const string id_r = "R";
        /// <summary>
        /// 主消息ID集合
        /// </summary>
        public static List<string> ids = new List<string>() { id_a, id_r };

        #endregion

        #region --成员属性

        private string result;
        /// <summary>
        /// 返回结果
        /// </summary>
        public string Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }       
        #endregion

        #region --构造函数
        public ResultProtocol()
        {            
            this.MessageId = id_r;
            this.IsContolType = false;
        }
        public ResultProtocol(string[] arrays)
        {           
            if (arrays.Length > 5)
            {
                //服务站ID
                this.StationId = arrays[0];
                //流水号
                this.SerialNumber = arrays[1];
                this.MessageId = arrays[2];
                this.SubMessageId = arrays[3];
                this.TimeSpan = arrays[4];
                this.result = arrays[5];
                this.Success = true;
                if (arrays.Length == 7)
                {
                    this.ValidCode = arrays[6];
                }              
            }
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 获取消息集合
        /// </summary>
        /// <returns></returns>
        public new string[] GetStrings()
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
            //返回结果
            arrays[5] = this.Result;            

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
        /// 收到消息具体操作
        /// </summary>
        /// <param name="flag"></param>
        public void Do(ref bool flag)
        {
            if (this.MessageId == id_r)
            {
                flag = this.result.Equals(DataSources.EnumResultType.Success.ToString("d"));                
            }
        }       
        #endregion
    }
}
