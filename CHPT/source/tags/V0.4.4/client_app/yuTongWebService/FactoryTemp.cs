using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using HXC_FuncUtility;
using SYSModel;

namespace yuTongWebService
{
    /// <summary>
    /// 
    /// </summary>
    public class FactoryTemp
    {
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="billNumber">单据号</param>
        /// <param name="billType">单据类型</param>
        /// <param name="operate">操作类型</param>
        public static void DeleteFactory(string billNumber, DataSources.EnumBillType billType, DataSources.EnumOperateObj operate)
        {
            string sql = "delete tb_factory_temp where billNumber=@billNumber and billType=@billType and opType=@opType";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("billNumber", billNumber);
            dic.Add("billType", billType.ToString());
            dic.Add("opType", operate.ToString("d"));
            DBHelper.ExtNonQuery("删除云平台缓存", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sql, System.Data.CommandType.Text, dic);
        }
    }
}
