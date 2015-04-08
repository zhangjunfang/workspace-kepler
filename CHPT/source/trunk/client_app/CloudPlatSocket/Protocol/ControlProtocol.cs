using System;
using System.Collections.Generic;
using BLL;
using CloudPlatSocket.Handler;
using HXC_FuncUtility;

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
        /// <summary>
        /// 用户有效期
        /// </summary>
        public String Date { get; set; }
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
                if (SubMessageId == "CD2")
                {
                    this.controlType = arrays[5];
                }
                else if (SubMessageId == "CD3")
                {
                    this.Date = arrays[5];
                }
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
            if (SubMessageId == "CD2")
            {
                arrays[5] = controlType;
            }
            else if (SubMessageId == "CD3")
            {
                arrays[5] = Date;
            }
            return arrays;
        }
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="protocol"></param>
        public override void Do(ref bool flag)
        {
            if (this.MessageId == id)
            {
                ContolHandler.Deal(this);
            }
        }
        public override string ToString()
        {
            try
            {
                return String.Format("{0}${1}${2}${3}${4}${5}${6}", StationId,SerialNumber, MessageId, SubMessageId,TimeSpan,Date,ValidCode);
            }
            catch (Exception ex)
            {
                LogAssistant.LogService.WriteLog(ex);
                return "";
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
