using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using System.Data;
using Utility.Common;
namespace yuTongWebService
{
    /// <summary>
    /// 宇通最后更新时间
    /// </summary>
    public class SysConfig
    {
        /// <summary>
        /// 跟据名称，获取最后同步时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetLastTime(string name)
        {
            string sql=string.Format("select key_value from sys_config where key_name='{0}'",name);
            string lastTime = DBHelper.GetSingleValue("获取最后同步时间", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sql);
            if (string.IsNullOrEmpty(lastTime))
            {
                lastTime = "1900-01-01";
            }
            return lastTime;
        }

        /// <summary>
        /// 更新最后同步时间
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public void UpdateLastTime(string name)
        {
            List<SysSQLString> listSql=new List<SysSQLString> ();
            SysSQLString sqlUpdate=new SysSQLString ();
            sqlUpdate.cmdType=CommandType.Text;
            sqlUpdate.Param=new Dictionary<string,string> ();
            string lastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sqlUpdate.sqlString = string.Format("update sys_config set key_value='{0}',update_time='{1}' where key_name='{2}'", lastTime, Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString(), name);
            listSql.Add(sqlUpdate);
            try
            {
                DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("更新最后同步时间", GlobalStaticObj_Server.CommAccCode, listSql);
            }
            catch (Exception e)
            {
                GlobalStaticObj_Server.YTInterfaceLogService.WriteLog("更新最后同步时间", e);
            }
        }
    }
}
