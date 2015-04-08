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
    /// 宇通客户
    /// cxz
    /// 2014-11-25
    /// </summary>
    public class YTCustomer
    {
        EnumerableRowCollection<DataRow> ecCustomer = null;

        public EnumerableRowCollection<DataRow> Customer
        {
            get
            {
                if (ecCustomer == null)
                {
                    DataTable dtDic = DBHelper.GetTable("获取客户ID", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode, "tb_customer", "cust_id,cust_crm_guid", "data_source='2'", "", "");
                    ecCustomer = dtDic.AsEnumerable();
                }
                return ecCustomer;
            }
        }

        /// <summary>
        /// 根据宇通CRMID，获取本地客户ID
        /// </summary>
        /// <param name="crmID">宇通crmID</param>
        /// <returns>客户ID</returns>
        public string GetLocalCustID(string crmID)
        {
            var drs = from t in Customer
                      where t.Field<string>("cust_crm_guid") == crmID
                      select new { id = t.Field<string>("cust_id") };
            string custID = string.Empty;
            if (drs.Count() > 0)
            {
                foreach (var id in drs)
                {
                    custID = id.id;
                    break;
                }
            }
            return custID;
        }
    }
}
