using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using HXC_FuncUtility;
using Utility.Net;

namespace BLL
{
    public class DbCommonUtil
    {
        /// <summary>
        /// 从字典码表中获取信息 字典码表编码的子集合 
        /// </summary>
        /// <param name="pDic_codeList">父级编码集合</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDictionariesByPDic_codes(string currAccDbName, ArrayList pDic_codeList)//(string pDic_codes)//"'"+code1+"','"+
        {
            string dic_code_in = "";
            foreach (string str in pDic_codeList)
            {
                dic_code_in += "'" + str + "',";
            }
            if (!string.IsNullOrEmpty(dic_code_in))
                dic_code_in = dic_code_in.Substring(0, dic_code_in.Length - 1);
            string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code in (" + dic_code_in + ") and enable_flag=1) ";
            return DBHelper.GetTable("查询字典码表信息", currAccDbName, "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
        }

        /// <summary>
        /// 检索码表Table中是否有dic_code这条记录 有即返回名称
        /// </summary>
        /// <param name="dt">码表Table</param>
        /// <param name="dic_id">码表id</param>
        /// <returns>码表名称</returns>
        public static string RetrievalDic_name(DataTable dt, string dic_id)
        {
            string dic_name = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["dic_id"].ToString() == dic_id)
                    {
                        dic_name = dr["dic_name"].ToString();
                        break;
                    }
                }
            }
            return dic_name;
        }

        /// <summary> 升级数据库
        /// </summary>
        /// <param name="currSoftVersion">当前软件版本</param>
        /// <param name="currDbVersion">当前数据库版本</param>
        /// <returns></returns>
        public static List<string> GetUpdateDbVersion(string currSoftVersion, string currDbVersion)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Hashtable ht = new Hashtable();
            ht.Add("currSoftVersion", currSoftVersion);
            ht.Add("currDbVersion", currDbVersion);
            string result = HttpRequest.DoGet("http://119.57.151.34:1800/sspapp/version/findVersionDb.do", ht);
            List<string> listVersion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result);
            return listVersion;
        }

        /// <summary> 升级数据库
        /// </summary>
        /// <param name="currSoftVersion">当前软件版本</param>
        /// <param name="currDbVersion">当前数据库版本</param>
        /// <returns></returns>
        public static bool UpdateDb(List<string> listVersion)
        {
            bool flag = false;
            foreach (string version in listVersion)
            {
                flag = ExcuteScript(version, true);
                if (!flag)
                {
                    return false;
                }
                flag = ExcuteScript(version, false);
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary> 
        /// </summary>
        /// <param name="dirName">脚本目录名称</param>
        /// <param name="isServDb">是否通用库</param>
        /// <returns></returns>
        public static bool ExcuteScript(string dirName, bool isServDb)
        {
            string sqlDir = System.Windows.Forms.Application.StartupPath + @"\sqlscript\" + (isServDb ? "S_" : "C_") + dirName;
            if (!Directory.Exists(sqlDir))
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("数据库升级", sqlDir+"数据库脚本丢失");
                return false;
            }
            DataTable dt = DBHelper.GetTable("获取账套代码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "setbook_code", "enable_flag=1 and setbook_code!='000'", "", "");

            string[] fileArr = Directory.GetFiles(sqlDir);
            foreach (string filePath in fileArr)
            {
                string sqlStr = File.ReadAllText(filePath);
                if (isServDb)
                {
                    try
                    {
                        DBHelper.ExtNonQuery("执行sql脚本", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlStr, CommandType.Text, null);
                    }
                    catch (Exception ex)
                    {
                        GlobalStaticObj_Server.GlobalLogService.WriteLog("HXC_000数据库升级" + filePath, ex);
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        DBHelper.ExtNonQuery("执行sql脚本", "HXC", sqlStr, CommandType.Text, null);
                    }
                    catch (Exception ex)
                    {
                        GlobalStaticObj_Server.GlobalLogService.WriteLog("HXC数据库升级" + filePath, ex);
                        return false;
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        string accCode = dr["setbook_code"].ToString();
                        try
                        {
                            DBHelper.ExtNonQuery("执行sql脚本", GlobalStaticObj_Server.DbPrefix + accCode, sqlStr, CommandType.Text, null);
                        }
                        catch (Exception ex)
                        {
                            GlobalStaticObj_Server.GlobalLogService.WriteLog("HXC_" + accCode + "数据库升级" + filePath, ex);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
