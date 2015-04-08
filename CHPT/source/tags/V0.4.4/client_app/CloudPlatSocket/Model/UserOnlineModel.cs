using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class UserOnlineModel
    {
        public static string table = "tb_user_online";

        public UserOnlineModel()
        {
            this.tbUserOnlineId = string.Empty;
            this.comCode = string.Empty;
            this.comName = string.Empty;
            this.setbookId = string.Empty;
            this.setbookName = string.Empty;
            this.clientAccount = string.Empty;
            this.realName = string.Empty;            
            this.clientAccountOrgid = string.Empty;
            this.roleName = string.Empty;
            this.isOperater = string.Empty;
            this.regTime = string.Empty;
            this.loadTime = string.Empty;
            this.loadIdAddr = string.Empty;
            this.onlineStatus = string.Empty;
            this.clientMac = string.Empty; 
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable(table);
            dt.Columns.Add("tbUserOnlineId");
            dt.Columns.Add("comCode");
            dt.Columns.Add("comName");
            dt.Columns.Add("setbookId");
            dt.Columns.Add("setbookName");
            dt.Columns.Add("clientAccount");
            dt.Columns.Add("realName");
            dt.Columns.Add("clientAccountOrgid");
            dt.Columns.Add("roleName");
            dt.Columns.Add("isOperater");
            dt.Columns.Add("regTime");
            dt.Columns.Add("loadTime");
            dt.Columns.Add("loadIdAddr");
            dt.Columns.Add("onlineStatus");
            dt.Columns.Add("clientMac");       

            DataRow dr = dt.NewRow();
            dr["tbUserOnlineId"] = this.tbUserOnlineId;
            dr["comCode"] = this.comCode;
            dr["comName"] = this.comName;
            dr["setbookId"] = this.setbookId;
            dr["setbookName"] = this.setbookName;
            dr["clientAccount"] = this.clientAccount;
            dr["realName"] = this.realName;
            dr["clientAccountOrgid"] = this.clientAccountOrgid;
            dr["roleName"] = this.roleName;
            dr["isOperater"] = this.isOperater;
            dr["regTime"] = this.regTime;
            dr["loadTime"] = this.loadTime;
            dr["loadIdAddr"] = this.loadIdAddr;
            dr["onlineStatus"] = this.onlineStatus;
            dr["clientMac"] = this.clientMac;

            dt.Rows.Add(dr);
            return dt;
        }
        /// <summary>
        /// 用户在线ID
        /// </summary>
        public string tbUserOnlineId { get; set; }
        /// <summary>
        /// 公司编码，关联公司表
        /// </summary>
        public string comCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string comName { get; set; }
        /// <summary>
        /// 帐套编码
        /// </summary>
        public string setbookId { get; set; }
        /// <summary>
        /// 帐套名称
        /// </summary>
        public string setbookName { get; set; }
        /// <summary>
        /// CS的客户端账号
        /// </summary>
        public string clientAccount { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string realName { get; set; }       
        /// <summary>
        /// CS的客户端登陆人所属组织
        /// </summary>
        public string clientAccountOrgid { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
        /// <summary>
        /// 是否操作员1是0不是
        /// </summary>
        public string isOperater { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public string regTime { get; set; }
        /// <summary>
        /// 登陆时间(指CS端C登陆到S的时间)
        /// </summary>
        public string loadTime { get; set; }
        /// <summary>
        /// 登陆IP地址
        /// </summary>
        public string loadIdAddr { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>
        public string onlineStatus { get; set; }
        /// <summary>
        /// cs端的客户端的mac地址
        /// </summary>
        public string clientMac { get; set; }       
    }
}
