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
            DtArea = DBHelper.GetTable("获取地区数据", "sys_area", "*", string.Format("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d")), "", "order by AREA_CODE asc");            
        }

        private static void LoadAnnounce()
        {
            DtAnnounce = DBHelper.GetTable("获取公告数据", "sys_announcement"
                , "top 20 *", string.Format("enable_flag='{0}' and status='{1}'", DataSources.EnumEnableFlag.USING.ToString("d"), DataSources.EnumAuditStatus.AUDIT.ToString("d")), "", "order by create_time desc");

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
            DataTable dt = DBHelper.GetTable("获取提醒设置", "sys_reminding_set", "*", string.Format("user_id='{0}'", GlobalStaticObj.UserID), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            string project=string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                project = dr["projec"].ToString();
                if (project == DataSources.EnumReminderType.YYDZ.ToString())
                {
                    sb.Append(string.Format("select * from tb_maintain_reservation where document_status='{0}' and enable_flag='{1}' and maintain_time>'{2}' and maintain_time<'{3}' order by update_time desc"
                         , DataSources.EnumAuditStatus.AUDIT.ToString("d"), DataSources.EnumEnableFlag.USING.ToString("d"), Common.LocalDateTimeToUtcLong(DateTime.Now), Common.LocalDateTimeToUtcLong(DateTime.Now.AddMonths(1))));
                    sb.Append(";");
                }
                else if (project == DataSources.EnumReminderType.DPG.ToString())
                {
                    sb.Append(string.Format("select * from tb_maintain_info where info_status='{0}' and enable_flag='{1}' and update_time>'{2}' and update_time<'{3}' order by update_time desc"
                            , DataSources.EnumDispatchStatus.NotStartWork.ToString("d"), DataSources.EnumEnableFlag.USING.ToString("d")));
                    sb.Append(";");
                }
                else if (project == DataSources.EnumReminderType.SBFWDBH.ToString())
                {
                    //三包服务驳回提醒
                    sb.Append(string.Format("select * from tb_maintain_three_guaranty where info_status='{0}' and enable_flag='{1}' and update_time>'{2}' and update_time<'{3}'"
                             , DataSources.EnumDispatchStatus.NotStartWork.ToString("d")
                             , DataSources.EnumEnableFlag.USING.ToString("d")
                             , Common.LocalDateTimeToUtcLong(DateTime.Now)
                             , Common.LocalDateTimeToUtcLong(DateTime.Now.AddDays(1))));
                    sb.Append(";");
                }
            }
            if (sb.ToString().Length == 0)
            {
                return;
            }

            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = sb.ToString();

            DsReminder = DBHelper.GetDataSet("获取提醒", sql);

            if (ReminderComplated != null && DsReminder != null)
            {                
                ReminderComplated(DsReminder);
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

            DataSet ds = DBHelper.GetDataSet("获取预加载信息", sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                DtDict = ds.Tables[0];
                DtDept = ds.Tables[1];
                DtUser = ds.Tables[2];
                DtArea = ds.Tables[3];
            }
        }
        #endregion

        #region 公用方法
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
            ThreadPool.QueueUserWorkItem(new WaitCallback(_Update), CacheList.Announce);
            ThreadPool.QueueUserWorkItem(new WaitCallback(_Update), CacheList.Reminder);
            ThreadPool.QueueUserWorkItem(new WaitCallback(_LoadPreData));         
        }
        
        public static void _Update(object state)
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