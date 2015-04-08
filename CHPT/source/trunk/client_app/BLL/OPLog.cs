using System;
using System.Data;
using System.Collections.Generic;
using Model;
using DALFactory;
using IDAL;
using SYSModel;
using Utility.Common;
using HXC_FuncUtility;

namespace BLL
{
    public class OPLog
    {
        /// <summary> 添加文件日志
        /// </summary>
        /// <param name="item"></param>
        /// <param name="currAccDbName"></param>
        /// <returns></returns>
        public static bool Add(UserFileOPLog item, string currAccDbName)
        {
            Dictionary<string, string> DicParam = new Dictionary<string, string>();
            DicParam.Add("ClientUserID", item.userOP.UserID);
            DicParam.Add("OPName", item.userOP.OPName);
            DicParam.Add("LogID", System.Guid.NewGuid().ToString());
            DicParam.Add("FileName", item.FileName.Replace('\'', '"'));
            DicParam.Add("FilePath", item.FilePath.Replace('\'', '"'));
            DicParam.Add("sTimeTicks", item.sTimeTicks.ToString());
            DicParam.Add("eTimeTicks", item.eTimeTicks.ToString());
            DicParam.Add("exeResult", item.exeResult ? "1" : "0");
            return DBHelper.Submit_AddLog("添加文件日志", currAccDbName, "tl_ClientUserFile", "", "", DicParam);
        }

        /// <summary> 添加日志
        /// </summary>
        /// <param name="item"></param>
        /// <param name="currAccDbName"></param>
        /// <returns></returns>
        public static bool Add(UserFunctionOPLog item, string currAccDbName)
        {
            Dictionary<string, string> DicParam = new Dictionary<string, string>();
            DicParam.Add("user_id", item.userOP.UserID);
            DicParam.Add("u_f_log_id", System.Guid.NewGuid().ToString());
            DicParam.Add("com_id", item.com_id);
            DicParam.Add("setbook_id", item.acc_code);
            DicParam.Add("access_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            DicParam.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            DicParam.Add("fun_id", item.fun_id.ToString());
            return DBHelper.Submit_AddLog("添加用户菜单日志", GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.CommAccCode, "tl_user_function_log", "", "", DicParam);
        }
    }
}
