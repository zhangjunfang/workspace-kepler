
using CloudPlatSocket.Handler;
namespace CloudPlatSocket.Protocol
{
    /// <summary>
    /// 附件传输协议
    /// 主消息ID：F
    /// </summary>
    public class FileProtocol : MessageProtocol, IProtocol
    {
        #region --成员变量
        //消息ID
        public static string id = "F";
        #endregion

        #region --成员属性

        private string fileId;
        /// <summary>
        /// 附件ID
        /// </summary>
        public string FileId
        {
            get
            {
                return this.fileId;
            }
            set
            {
                this.fileId = value;
            }
        }

        private string fileType;
        /// <summary>
        /// 附件类型
        /// </summary>
        public string FileType
        {
            get
            {
                return this.fileType;
            }
            set
            {
                this.fileType = value;
            }
        }

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

        private string file;
        /// <summary>
        /// 文件内容
        /// </summary>
        public string File
        {
            get
            {
                return this.file;
            }
            set
            {
                this.file = value;
            }
        }        
        #endregion

        #region --构造函数
        public FileProtocol()
        {
            this.MessageId = id;
        }
        public FileProtocol(string[] arrays)
        {           
            if (arrays.Length > 9)
            {
                //服务站ID
                this.StationId = arrays[0];
                //流水号
                this.SerialNumber = arrays[1];
                this.MessageId = arrays[2];
                this.SubMessageId = arrays[3];
                this.TimeSpan = arrays[4];
                this.FileId = arrays[5];
                this.FileType = arrays[6];
                this.Operation = arrays[7];
                this.Json = arrays[8];
                this.File = arrays[9];
                if (arrays.Length == 11)
                {
                    this.ValidCode = arrays[10];
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
            string[] arrays = new string[10];
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
            //附件ID
            arrays[5] = this.FileId;
            //附件类别
            arrays[6] = this.FileType;
            //操作类别
            arrays[7] = this.Operation;
            //Json对象
            arrays[8] = this.Json;
            //Json对象
            arrays[9] = this.File;
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
        public new void Do(ref bool flag)
        {
            if (this.MessageId == id)
            {
                FileHandler.Deal(this);
            }
        }
        #endregion
    }
}