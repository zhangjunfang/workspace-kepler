
namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 客户端平台登录消息协议
    /// 主消息ID：L
    /// </summary>
    public class LoginProtocol : MessageProtocol,IProtocol
    {
        #region --成员变量
        //消息ID
        public static string id = "L";
        #endregion

        #region --成员属性

        private string userId;
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        private string password;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        private string permissionCode;
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string PermissionCode
        {
            get
            {
                return this.permissionCode;
            }
            set
            {
                this.permissionCode = value;
            }
        }       
        #endregion

        #region --构造函数
        public LoginProtocol()
        {            
            this.MessageId = id;            
        }
        public LoginProtocol(string[] arrays)
        {           
            if (arrays.Length > 7)
            {
                //服务站ID
                this.StationId = arrays[0];
                //流水号
                this.SerialNumber = arrays[1];
                this.MessageId = arrays[2];
                this.SubMessageId = arrays[3];
                this.TimeSpan = arrays[4];
                this.userId = arrays[5];
                this.password = arrays[6];
                this.permissionCode = arrays[7];
                if (arrays.Length == 9)
                {
                    this.ValidCode = arrays[8];
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
            string[] arrays = new string[8];
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
            //登录账号
            arrays[5] = this.UserId;
            //登录密码
            arrays[6] = this.Password;
            //鉴权码
            arrays[7] = this.PermissionCode;             

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
        #endregion
    }
}
