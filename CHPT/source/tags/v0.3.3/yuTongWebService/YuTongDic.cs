using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL;
using HXC_FuncUtility;

namespace yuTongWebService
{
    /// <summary>
    /// 宇通字典码表转换
    /// cxz
    /// 2014-11-12
    /// </summary>
    public class YuTongDic
    {
        EnumerableRowCollection<DataRow> ecDic = null;

        public EnumerableRowCollection<DataRow> Dic
        {
            get
            {
                if (ecDic == null)
                {
                    DataTable dtDic = DBHelper.GetTable("获取字典编码",GlobalStaticObj_Server.DbPrefix+ GlobalStaticObj_Server.Instance.MainAccCode, "sys_dictionaries", "dic_code,dic_id", "data_source='2'", "", "");
                    ecDic = dtDic.AsEnumerable();
                }
                return ecDic;
            }
        }

        /// <summary> 根据宇通编码获取本地字典ID
        /// 本地存储的是字典id，字段名称+"_"+宇通编码转成本地字典编码
        /// 根据本地字典编码去字典表查找字典id
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">宇通编码</param>
        /// <returns>返回本地字典ID</returns>
        public string GetLocalDicID(string fieldName, string value)
        {
            var drs = from t in Dic
                      where t.Field<string>("dic_code") == string.Format("{0}_{1}", fieldName, value)
                      select new { id = t.Field<string>("dic_id") };
            string dicID = string.Empty;
            if (drs.Count() > 0)
            {
                foreach (var id in drs)
                {
                    dicID = id.id;
                    break;
                }
            }
            return dicID;
        }


        /// <summary> 根据本地字典ID获取宇通编码
        /// 本地存储的是字典id，获取到编码后去除字段名称_转成宇通编码
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">本地字典id</param>
        /// <returns>返回宇通编码</returns>
        public string GetYTDicCode(string fieldName, string value)
        {
            var drs = from t in Dic
                      where t.Field<string>("dic_id") == value
                      select new { code = t.Field<string>("dic_code") };
            string dicCode = string.Empty;
            if (drs.Count() > 0)
            {
                foreach (var id in drs)
                {
                    dicCode = id.code.Replace(fieldName + "_", "");
                    break;
                }
            }
            return dicCode;
        }
    }
}
