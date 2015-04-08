using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 支撑系统上传控制指令类（上行）C
    /// </summary>
    public class CloudDataProtocol : MessageProtocol
    {
        #region --成员变量
        //消息ID
        public static string Id = "C";
        #endregion

        #region --成员属性
        /// <summary>
        /// 
        /// </summary>
        public string DataTicks { get; set; }
        #endregion

        #region --构造函数
        public CloudDataProtocol()
        {            
            this.MessageId = Id;            
        }
        public CloudDataProtocol(string[] arrays)
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
                this.DataTicks = arrays[5];          
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
            //要发送的服务站ID
            arrays[5] = this.StationId;    
            return arrays;
        }
        #endregion
    }
}
