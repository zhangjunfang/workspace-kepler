using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 服务端状态
    public class ServerStatusModel
    {
        public static string table = "tb_com_info";

        public ServerStatusModel()
        {
            this.ServerStatus = string.Empty;
            this.clientNums = string.Empty;
            this.ytCrmLinkedStatus = string.Empty;
            this.csComId = string.Empty;
            this.comId = string.Empty;
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable(table);
            dt.Columns.Add("serviceStatus");
            dt.Columns.Add("clientNums");
            dt.Columns.Add("ytCrmLinkedStatus");
            dt.Columns.Add("csComId");
            dt.Columns.Add("comId");

            DataRow dr = dt.NewRow();
            dr["serviceStatus"] = this.ServerStatus;
            dr["clientNums"] = this.clientNums;
            dr["ytCrmLinkedStatus"] = this.ytCrmLinkedStatus;
            dr["csComId"] = this.csComId;
            dr["comId"] = this.comId;
            dt.Rows.Add(dr);
            return dt;
        }
        /// <summary>
        /// 服务端在线状态0离线；1在线
        /// </summary>
        public string ServerStatus { get; set; }
        /// <summary>
        /// 客户端在线数
        /// </summary>
        public string clientNums { get; set; }
        /// <summary>
        /// 宇通CRM系统链路0异常1正常
        /// </summary>
        public string ytCrmLinkedStatus { get; set; }
        /// <summary>
        /// 服务端公司Id，服务站Id
        /// </summary>
        public string csComId { get; set; }
        /// <summary>
        /// 公司档案ID
        /// </summary>
        public string comId { get; set; }
    }
}
