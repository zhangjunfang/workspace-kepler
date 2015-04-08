using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudPlatSocket.Protocol
{
    public class ErrorProtocol : MessageProtocol, IProtocol
    {
        #region --成员变量
        /// <summary>
        /// 上传错误消息ID
        /// </summary>
        private const string id = "W";       
        #endregion
    }
}
