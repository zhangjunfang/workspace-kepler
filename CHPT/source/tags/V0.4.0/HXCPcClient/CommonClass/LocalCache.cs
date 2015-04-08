using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 本地缓存类
    /// </summary>
    public class LocalCache
    {
        #region --UI交互
        public delegate void AnnounceComplate(DataTable dt);
        public static AnnounceComplate AnnounceComplated;
        //提醒
        public delegate void ReminderComplate(DataSet ds);
        public static ReminderComplate ReminderComplated;
        #endregion  
        
        #region --缓存
        /// <summary> 码表
        /// </summary>
        public static DataTable DtDict { get; set; }
        /// <summary> 部门表
        /// </summary>
        public static DataTable DtDept { get; set; }
        /// <summary> 用户表
        /// </summary>
        public static DataTable DtUser { get; set; }
        /// <summary> 地区表
        /// </summary>
        public static DataTable DtArea { get; set; }

        /// <summary> 公告 
        /// </summary>
        public static DataTable DtAnnounce { get; set; }
        /// <summary> 提醒 
        /// </summary>
        public static DataSet DsReminder { get; set; }
        #endregion

        #region --私有方法
        /// <summary> 加载提醒 
        /// </summary>
        private static void LoadDict()
        {
            DtDict = DBHelper.GetTable("获取码表数据", "sys_dictionaries", "*", string.Format("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d")), "", "");
        }
        /// <summary> 加载部门 
        /// </summary>
        private static void LoadDept()
        {
            DtDept = DBHelper.GetTable("获取部门数据", "tb_organization", "*", string.Format("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d")), "", "");
        }
        /// <summary> 加载用户
        /// </summary>sys_user
        private static void LoadUser()
        {
            DtUser = DBHelper.GetTable("获取用户数据", "sys_user", "*", string.Format("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d")), "", "");
        }
        private static void LoadArea()
        {
            DtArea = DBHelper.GetTable("获取地区数据", "sys_area", "*", "", "", "order by AREA_CODE asc");            
        }

        private static void LoadAnnounce()
        {
            try
            {
                DtAnnounce = DBHelper.GetTable("获取公告数据", "sys_announcement"
                    , "top 20 *", string.Format("enable_flag='{0}' and status='{1}'", DataSources.EnumEnableFlag.USING.ToString("d"),
                    DataSources.EnumAuditStatus.AUDIT.ToString("d")), "", "order by create_time desc");
            }
            catch
            {
                DtAnnounce = null;
            }

            if (AnnounceComplated != null
                   && DtAnnounce != null)
            {
                AnnounceComplated(DtAnnounce);
            }
        }
        /// <summary> 加载提醒 
        /// </summary>
        private static void LoadReminder()
        {
            //判断是否具有提醒功能
            string where = string.Format("user_id='{0}'", GlobalStaticObj.UserID);
            where = string.Empty;
            DataTable dt = DBHelper.GetTable("获取提醒设置", "sys_reminding_set", "*", where, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataTable newDt = dt.DefaultView.ToTable(true, "projec", "happen_time");
            StringBuilder sb = new StringBuilder();
            string project=string.Empty;
            double second = 0;
            List<string> tableNames = new List<string>();
            foreach (DataRow dr in newDt.Rows)
            {
                project = dr["projec"].ToString();
                if (string.IsNullOrEmpty(dr["happen_time"].ToString()))
                {
                    second = Double.Parse(dr["happen_time"].ToString());
                }
                else
                {
                    second = 0;
                }
                if (project == DataSources.EnumReminderType.YYDZ.ToString())
                {
                    tableNames.Add(project);
                    if (second > 0)
                    {
                        sb.Append(string.Format("select * from tb_maintain_reservation " + project + " where document_status='{0}' and enable_flag='{1}' and maintain_time>'{2}' and maintain_time<'{3}' order by update_time desc"
                         , DataSources.EnumAuditStatus.SUBMIT.ToString("d"), DataSources.EnumEnableFlag.USING.ToString("d"), Common.LocalDateTimeToUtcLong(DateTime.Now), Common.LocalDateTimeToUtcLong(DateTime.Now.AddSeconds(second))));
                    }
                    else
                    {
                        sb.Append(string.Format("select * from tb_maintain_reservation " + project + " where document_status='{0}' and enable_flag='{1}' and maintain_time>'{2}' order by update_time desc"
                         , DataSources.EnumAuditStatus.SUBMIT.ToString("d"), DataSources.EnumEnableFlag.USING.ToString("d"), Common.LocalDateTimeToUtcLong(DateTime.Now)));
                    }                    
                    sb.Append(";");
                }
                else if (project == DataSources.EnumReminderType.DPG.ToString())
                {
                    tableNames.Add(project);
                    sb.Append(string.Format("select a.*,b.fitting_sum,b.other_item_sum,b.privilege_cost,b.should_sum,b.received_sum,b.debt_cost from tb_maintain_info a left join tb_maintain_settlement_info b on a.maintain_id=b.maintain_id where a.info_status='{0}' and a.enable_flag='{1}' order by a.update_time desc"
                            , DataSources.EnumDispatchStatus.NotStartWork.ToString("d"), DataSources.EnumEnableFlag.USING.ToString("d")));
                    sb.Append(";");
                }
                else if (project == DataSources.EnumReminderType.SBFWDBH.ToString())
                {
                    tableNames.Add(project);
                    //三包服务驳回提醒
                    sb.Append(string.Format("select * from tb_maintain_three_guaranty where info_status='{0}' and enable_flag='{1}' order by update_time desc"
                             , DataSources.EnumDispatchStatus.NotStartWork.ToString("d")
                             , DataSources.EnumEnableFlag.USING.ToString("d")));
                    sb.Append(";");
                }
            }           

            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = sb.ToString();

            try
            {
                DsReminder = DBHelper.GetDataSet("获取提醒", sql);
            }
            catch
            {

            }

            if (DsReminder != null)
            {
                for (int i = 0; i < DsReminder.Tables.Count; i++)
                {
                    DsReminder.Tables[i].TableName = tableNames[i];
                }
                if (ReminderComplated != null)
                {
                    ReminderComplated(DsReminder);
                }
            }
        }
        private static void _LoadPreData(object obj)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select * from sys_dictionaries");
            sb.Append(";");
            sb.Append("select * from tb_organization");
            sb.Append(";");
            sb.Append("select * from sys_user");
            sb.Append(";");
            sb.Append("select * from sys_area");

            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = sb.ToString();

            try
            {
                DataSet ds = DBHelper.GetDataSet("获取预加载信息", sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DtDict = ds.Tables[0];
                    DtDept = ds.Tables[1];
                    DtUser = ds.Tables[2];
                    DtArea = ds.Tables[3];
                }
            }
            catch
            {

            }
        }
        #endregion

        #region --公用方法
        /// <summary> 获取码表名称
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public static string GetDictNameById(string dictId)
        {            
            if (LocalCache.DtDict == null)
            {
                LocalCache.Update(CacheList.Dict);
            }
            if (LocalCache.DtDict != null
                && LocalCache.DtDict.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtDict.Select("dic_id = '" + dictId + "'");
                if (drs.Length > 0)
                {
                    return drs[0]["dic_name"].ToString();
                }                
            }
            return string.Empty;
        }
        /// <summary> 获取部门名称
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public static string GetDeptNameById(string orgId)
        {
            if (LocalCache.DtDept == null)
            {
                LocalCache.Update(CacheList.Org);
            }
            if (LocalCache.DtDept != null
                && LocalCache.DtDept.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtDept.Select("org_id = '" + orgId + "'");
                if (drs.Length > 0)
                {
                    return drs[0]["org_name"].ToString();
                }
            }
            return string.Empty;
        }
        /// <summary> 获取用户名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserById(string userId)
        {
            if (LocalCache.DtUser == null)
            {
                LocalCache.Update(CacheList.User);
            }
            if (LocalCache.DtUser != null
                && LocalCache.DtUser.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtUser.Select("user_id = '" + userId + "'");
                if (drs.Length > 0)
                {
                    return drs[0]["user_name"].ToString();
                }
            }
            return string.Empty;
        }
        public static void PreLoad()
        {
            //_LoadPreData(null);
            _Update(CacheList.Dict);
            _Update(CacheList.Area);
            _Update(CacheList.Org);
            _Update(CacheList.User);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(_LoadPreData));
            _Update(CacheList.Announce);
            _Update(CacheList.Reminder);
        }

        public static void _Update(CacheList cache)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(_UpdateThread), cache);
        }
        private static void _UpdateThread(object state)
        {
            Update((CacheList)state);
        }
        public static void Update(CacheList cache)
        {            
            if (cache == CacheList.Menu || cache == CacheList.All)
            {
                return;
            }
            if (cache == CacheList.Dict || cache == CacheList.All)
            {
                LoadDict();
                if (cache == CacheList.Dict)
                {
                    return;
                }
            }
            if (cache == CacheList.Org || cache == CacheList.All)
            {
                LoadDept();
                if (cache == CacheList.Org)
                {
                    return;
                }
            }
            if (cache == CacheList.User || cache == CacheList.All)
            {
                LoadUser();
                if (cache == CacheList.User)
                {
                    return;
                }
            }
            if (cache == CacheList.Area || cache == CacheList.All)
            {
                LoadArea();
                if (cache == CacheList.Area)
                {
                    return;
                }
            }
            if (cache == CacheList.Announce || cache == CacheList.All)
            {
                LoadAnnounce();
                if (cache == CacheList.Announce)
                {
                    return;
                }
            }
            if (cache == CacheList.Reminder || cache == CacheList.All)
            {
                LoadReminder();
                if (cache == CacheList.Reminder)
                {
                    return;
                }
            }
        }        

        public static bool HasFunction(string fun_id)
        {
            if (GlobalStaticObj.gLoginDataSet != null
                && GlobalStaticObj.gLoginDataSet.Tables.Count > 1)
            {
                //建议菜单放在缓存里面
                if (GlobalStaticObj.gLoginDataSet.Tables[2].Select("fun_id='" + fun_id + "'").Length > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static DataRow GetFunction(string fun_id)
        {
            if (GlobalStaticObj.gLoginDataSet != null
                && GlobalStaticObj.gLoginDataSet.Tables.Count > 1)
            {
                //建议菜单放在缓存里面
                DataRow[] drs = GlobalStaticObj.gLoginDataSet.Tables[2].Select("fun_id='" + fun_id + "'");
                if (drs.Length > 0)
                {
                    return drs[0];
                }
            }
            return null;
        }

        public static DataTable GetParentFunction(string parent_id)
        {
            if (GlobalStaticObj.gLoginDataSet != null
                && GlobalStaticObj.gLoginDataSet.Tables.Count > 1)
            {
                DataTable dt = GlobalStaticObj.gLoginDataSet.Tables[2].Clone();
                //建议菜单放在缓存里面
                DataRow[] drs = GlobalStaticObj.gLoginDataSet.Tables[2].Select("parent_id='" + parent_id + "'");
                foreach (DataRow dr in drs)
                {
                    dt.ImportRow(dr);
                }
                return dt;
            }
            return null;
        }
       
        #endregion
    }

    /// <summary>
    /// 缓存列表
    /// </summary>
    public enum CacheList
    {        
        /// <summary>
        /// 全部信息
        /// </summary>
        All,
        /// <summary> 码表字典 
        /// </summary>
        Dict,
        /// <summary> 部门字典 
        /// </summary>
        Org,
        /// <summary> 用户字典 
        /// </summary>
        User,
        /// <summary> 地区
        /// </summary>
        Area,
        /// <summary> 菜单
        /// </summary>
        Menu,
        /// <summary> 公告 
        /// </summary>
        Announce,
        /// <summary> 提醒 
        /// </summary>
        Reminder        
    }
}