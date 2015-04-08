using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 用户行为监控
    /// </summary>
    public class BehaviorModel
    {
        public static string table = "tb_user_behavior_monitor";

        public BehaviorModel()
        {
            this.uOperId = string.Empty;
            this.comName = string.Empty;
            this.setbookName = string.Empty;
            this.watchTime = string.Empty;
            this.onlineType = string.Empty;

            this.orgName = string.Empty;
            this.clientAccount = string.Empty;
            this.clientMac = string.Empty;
            this.loadIdAddr = string.Empty;
            this.roleName = string.Empty;
        }
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable(table);
            dt.Columns.Add("uOperId");
            dt.Columns.Add("comName");
            dt.Columns.Add("setbookName");
            dt.Columns.Add("watchTime");
            dt.Columns.Add("onlineType");

            dt.Columns.Add("orgName");
            dt.Columns.Add("clientAccount");
            dt.Columns.Add("clientMac");
            dt.Columns.Add("loadIdAddr");
            dt.Columns.Add("roleName");

            DataRow dr = dt.NewRow();
            dr["uOperId"] = this.uOperId;
            dr["comName"] = this.comName;
            dr["setbookName"] = this.setbookName;
            dr["watchTime"] = this.watchTime;
            dr["onlineType"] = this.onlineType;

            dr["orgName"] = orgName;
            dr["clientAccount"] = clientAccount;
            dr["clientMac"] = clientMac;
            dr["loadIdAddr"] = loadIdAddr;
            dr["roleName"] = roleName;

            dt.Rows.Add(dr);
            return dt;
        }
        /// <summary>
        /// 用户操作记录表id
        /// </summary>
        public string uOperId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string comName { get; set; }
        /// <summary>
        /// 帐套
        /// </summary>
        public string setbookName { get; set; }
        /// <summary>
        /// 监控时间
        /// </summary>
        public string watchTime { get; set; }
        /// <summary>
        /// 在线行为类型，关联字典码表
        /// </summary>
        public string onlineType { get; set; }
        #region 2015.02.05 为了对应云平台而移除的字段(与马驰和薛辉讨论决定的)
        ///// <summary>
        ///// 关联tb_user_online表的id
        ///// </summary>
        //public string tbUserOnlineId { get; set; }
        ///// <summary>
        ///// 人员编码 关联人员表
        ///// </summary>
        //public string userCode { get; set; }
        ///// <summary>
        ///// 帐套编码，关联字典表
        ///// </summary>
        //public string setbookId { get; set; }
        ///// <summary>
        ///// 公司编码，关联字典表
        ///// </summary>
        //public string comCode { get; set; }
        #endregion
        #region 2015.02.05 为了对应云平台而增加的字段(与马驰和薛辉讨论决定的)
        /// <summary>
        /// 用户登录账号
        /// </summary>
        public string clientAccount { get; set; }
        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string roleName { get; set; }
        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string loadIdAddr { get; set; }
        /// <summary>
        /// mac地址
        /// </summary>
        public string clientMac { get; set; }
        #endregion
    }
}
