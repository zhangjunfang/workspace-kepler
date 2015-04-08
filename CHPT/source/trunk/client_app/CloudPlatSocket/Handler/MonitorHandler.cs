using CloudPlatSocket.Model;
using System.Data;
using HXC_FuncUtility;
using BLL;
using SYSModel;
using System.Text;
using System.Collections.Generic;

namespace CloudPlatSocket.Handler
{
    public class MonitorHandler
    {
        private static DataTable dtUser;

        /// <summary>
        /// 获取服务站状态
        /// </summary>
        /// <returns></returns>
        public static DataTable GetServerStatus()
        {
            ServerStatusModel ssm = new ServerStatusModel();
            ssm.ServerStatus = GlobalStaticObj_Server.ConnStatus ? "1" : "0";
            ssm.clientNums = LoginSessionInfo.Instance.dicLoginInfos.Count.ToString();
            ssm.ytCrmLinkedStatus = "1";
            ssm.csComId = GlobalStaticObj_Server.Instance.ComID;
            ssm.comId = GlobalStaticObj_Server.Instance.ComID;
            return ssm.GetDataTable();
        }
        /// <summary>
        /// 获取在线用户
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static List<UserOnlineModel> GetUser(string dbName)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("select");
            //sb.Append(" ");
            //sb.Append("a.user_id,a.org_id,a.land_name,a.user_name,a.create_time,a.is_operator,b.login_time,b.computer_ip,a.is_online,b.computer_mac,d.role_name");
            //sb.Append(" ");
            //sb.Append("from");
            //sb.Append(" ");
            //sb.Append("sys_user a");
            //sb.Append(" ");
            //sb.Append("left join sys_log_log b on a.user_id = b.user_id");
            //sb.Append(" ");
            //sb.Append("left join tr_user_role c on c.user_id = a.user_id");
            //sb.Append(" ");
            //sb.Append("left join sys_role d on d.role_id = c.role_id");
            //sb.Append(" ");
            //sb.Append("where");
            //sb.Append(" ");
            //sb.Append(string.Format("a.is_online = '{0}' and b.exit_time is NULL",DataSources.EnumYesNo.Yes.ToString("d")));
            var sbSql = "select user_id,org_id,land_name,user_name,create_time,login_time,computer_ip,is_online,computer_mac,role_name from v_onlineuser where is_online = '1'";
            List<UserOnlineModel> lists = new List<UserOnlineModel>();

            //当前在线用户
            DataTable dt = DataHelper.GetDataTable(dbName, sbSql);
            if (dt != null)
            {
                DataTable _dt = dt.Clone();

                string user_id = string.Empty;
                if (dtUser != null)
                {
                    foreach (DataRow dr in dtUser.Rows)
                    {
                        user_id = dr["user_id"].ToString();
                        DataRow[] drs = dt.Select("user_id='" + user_id + "'");
                        if (drs.Length == 0)
                        {
                            //离线用户
                            dr["is_online"] = DataSources.EnumYesNo.NO.ToString("d");
                            _dt.ImportRow(dr);
                        }
                    }
                }

                if (dtUser == null)
                {
                    dtUser = dt;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    user_id = dr["user_id"].ToString();
                    DataRow[] drs = dtUser.Select("user_id='" + user_id + "'");
                    if (drs.Length == 0)
                    {
                        //新上线用户
                        _dt.ImportRow(dr);
                    }
                }
                dtUser = dt;

                //组装数据
                foreach (DataRow dr in _dt.Rows)
                {
                    UserOnlineModel uom = new UserOnlineModel();
                    uom.tbUserOnlineId = dr["user_id"].ToString();
                    uom.comCode = GlobalStaticObj_Server.Instance.ComID;
                    uom.comName = GlobalStaticObj_Server.Instance.ComName;
                    //uom.setbookId = string.Empty;
                    uom.setbookName = dbName;
                    uom.clientAccount = dr["land_name"].ToString();
                    uom.realName = dr["user_name"].ToString();
                    uom.clientAccountOrgid = dr["org_id"].ToString();
                    uom.roleName = dr["role_name"].ToString();
                    //uom.isOperater = dr["is_operator"].ToString();
                    uom.isOperater = "1";
                    uom.regTime = dr["create_time"].ToString();
                    uom.loadTime = dr["login_time"].ToString();
                    uom.loadIdAddr = dr["computer_ip"].ToString();
                    uom.onlineStatus = dr["is_online"].ToString();
                    uom.clientMac = dr["computer_mac"].ToString();
                    lists.Add(uom);
                }
            }
            return lists;
        }

        public static List<BehaviorModel> GetBehavior(string dbName, string userId, string time)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select");
            sb.Append(" ");
            sb.Append("log.computer_ip, log.computer_mac, a.u_f_log_id,a.access_time,s.fun_name,b.user_code,a.user_id, b.land_name");
            sb.Append(" ");
            sb.Append("from");
            sb.Append(" ");
            sb.Append("tl_user_function_log a");
            sb.Append(" ");
            sb.Append("left join sys_user b on a.user_id = b.user_id left join sys_function s on a.fun_id = s.fun_id left join sys_log_log log on a.log_log_id = log.log_log_id");
            sb.Append(" ");      
            sb.Append("where");
            sb.Append(" ");
            sb.Append(string.Format("a.user_id = '{0}' and a.access_time>{1}", userId,time));

            List<BehaviorModel> lists = new List<BehaviorModel>();

            //当前在线用户
            DataTable dt = DataHelper.GetDataTable(dbName, sb.ToString());
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BehaviorModel bm = new BehaviorModel();
                    bm.uOperId = dr["u_f_log_id"].ToString();
                    bm.comName = GlobalStaticObj_Server.Instance.ComName;
                    bm.setbookName = dbName;
                    bm.watchTime = dr["access_time"].ToString();
                    bm.onlineType = dr["fun_name"].ToString();
                    bm.loadIdAddr = dr["computer_ip"].ToString();
                    bm.clientMac = dr["computer_mac"].ToString();
                    bm.clientAccount = dr["land_name"].ToString();
                    lists.Add(bm);
                }
            }
            return lists;
        }
    }
}
