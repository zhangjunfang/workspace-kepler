using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace BLL
{
    public class DbCommonUtil
    {
        /// <summary>
        /// 从字典码表中获取信息 字典码表编码的子集合 
        /// </summary>
        /// <param name="pDic_codeList">父级编码集合</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDictionariesByPDic_codes(string currAccDbName,ArrayList pDic_codeList)//(string pDic_codes)//"'"+code1+"','"+
        {
            string dic_code_in = "";
            foreach (string str in pDic_codeList)
            {
                dic_code_in += "'" + str + "',";
            }
            if (!string.IsNullOrEmpty(dic_code_in))
                dic_code_in = dic_code_in.Substring(0, dic_code_in.Length - 1);
            string sqlWhere = " parent_id in (select dic_id from sys_dictionaries where dic_code in (" + dic_code_in + ") and enable_flag=1) ";
            return DBHelper.GetTable("查询字典码表信息",currAccDbName, "sys_dictionaries", "dic_id,dic_name", sqlWhere, "", " order by create_time ");
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
    }
}
