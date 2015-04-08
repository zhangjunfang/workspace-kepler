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
            this.comCode = string.Empty;
            this.setbookName = string.Empty;
            this.setbookId = string.Empty;
            this.watchTime = string.Empty;
            this.userCode = string.Empty;
            this.onlineType = string.Empty;
            this.tbUserOnlineId = string.Empty;
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
            dt.Columns.Add("comCode");
            dt.Columns.Add("setbookName");
            dt.Columns.Add("setbookId");
            dt.Columns.Add("watchTime");
            dt.Columns.Add("userCode");
            dt.Columns.Add("onlineType");
            dt.Columns.Add("tbUserOnlineId"); 

            DataRow dr = dt.NewRow();
            dr["uOperId"] = this.uOperId;
            dr["comName"] = this.comName;
            dr["comCode"] = this.comCode;
            dr["setbookName"] = this.setbookName;
            dr["setbookId"] = this.setbookId;
            dr["watchTime"] = this.watchTime;
            dr["userCode"] = this.userCode;
            dr["onlineType"] = this.onlineType;
            dr["tbUserOnlineId"] = this.tbUserOnlineId; 
          
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
        /// 公司编码，关联字典表
        /// </summary>
        public string comCode { get; set; }
        /// <summary>
        /// 帐套
        /// </summary>
        public string setbookName { get; set; }
        /// <summary>
        /// 帐套编码，关联字典表
        /// </summary>
        public string setbookId { get; set; }
        /// <summary>
        /// 监控时间
        /// </summary>
        public string watchTime { get; set; }
        /// <summary>
        /// 人员编码 关联人员表
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// 在线行为类型，关联字典码表
        /// </summary>
        public string onlineType { get; set; }
        /// <summary>
        /// 关联tb_user_online表的id
        /// </summary>
        public string tbUserOnlineId { get; set; }
    }
}
