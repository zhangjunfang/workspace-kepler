using System.Text;
using System;

namespace CloudPlatSocket
{
    public class ProtocolTranslator
    {
        /// <summary>
        /// 起始标志
        /// </summary>
        private const char _StartFlag = '[';
        /// <summary>
        /// 起始标志
        /// </summary>
        public static char StartFlag
        {
            get
            {
                return _StartFlag;
            }
        }

        /// <summary>
        /// 结束标志
        /// </summary>
        private const char _EndFlag = ']';
        /// <summary>
        /// 结束标志
        /// </summary>
        public static char EndFlag
        {
            get
            {
                return _EndFlag;
            }
        }
        /// <summary>
        /// 分隔符
        /// </summary>
        private const char separator = '$';
        private bool isFixedLen = true; 
        /// <summary>
        /// 是否为固定长度
        /// </summary>
        public bool IsFixedLen
        {
            set
            {
                this.isFixedLen = value;
            }
            get
            {
                return this.isFixedLen;
            }
        }
        private static int protocolLen = 100;
        /// <summary>
        /// 协议长度
        /// </summary>
        public static int ProtocolLength
        {
            get
            {
                return protocolLen;
            }
        }       

        #region 格式化消息
        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_messageProtocol">消息协议</param>
        /// <returns></returns>
        public static string SerilizeMessage(IProtocol _messageProtocol)
        {
            string message = string.Empty;            
            string[] arrays = _messageProtocol.GetStrings();
            if (arrays != null)
            {
                for (int i = 0; i < arrays.Length; i++)
                {
                    message += arrays[i];
                    if (i < arrays.Length - 1)
                    {
                        //添加分割符
                        message += separator;
                    }
                }
                //添加校验
                message += separator + GetValidCode(message);
                message = _StartFlag + message + _EndFlag;
            }
            return message;
        }
        #endregion

        #region 接收解析消息
        /// <summary>
        /// 接收解析消息
        /// </summary>
        /// <param name="_receivedMessage"></param>
        /// <returns></returns>
        public static MessageProtocol DeserilizeMessage(string _receivedMessage)
        {
            if (_receivedMessage.Length == 0)
            {
                return null;
            }
            _receivedMessage = _receivedMessage.Replace(_StartFlag.ToString(), "").Replace(_EndFlag.ToString(), "");
            string[] arrays = _receivedMessage.Split(separator);
            MessageProtocol messageProtocol = new MessageProtocol(arrays);

            #region --获取校验码
            string headMessage = string.Empty;
            headMessage = _receivedMessage.Substring(0, _receivedMessage.LastIndexOf(separator));
            headMessage = GetValidCode(headMessage);
            #endregion

            messageProtocol.Success = messageProtocol.IsValid(headMessage) ? true : false;
            if (!messageProtocol.Success)
            {
                messageProtocol.IsValid(headMessage);
            }
            return messageProtocol;
        }
        #endregion 
       
        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string GetValidCode(string message)
        {            
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            message = ByteToHex(bytes);
            int num = 0;
            for (int i = 0; i < message.Length; i = i + 2)
            {
                //按照Service端进行异或运算（汉字编码，双字节）
                num ^= Convert.ToInt16(message.Substring(i, 2),16);
            }
            return num.ToString();
        }
        /// <summary>
        /// 字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="comByte"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] comByte)
        {
            string returnStr = "";
            if (comByte != null)
            {
                for (int i = 0; i < comByte.Length; i++)
                {
                    returnStr += comByte[i].ToString("X2");
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="comByte"></param>
        /// <returns></returns>
        public static string _ByteToHex(byte[] comByte)
        {
            string returnStr = "";
            if (comByte != null)
            {
                for (int i = 0; i < comByte.Length; i++)
                {
                    returnStr += comByte[i].ToString("X");
                }
            }
            return returnStr;
        }
    }
}
