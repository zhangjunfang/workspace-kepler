using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;

namespace HXCPcClient.UCForm.FinancialManagement
{
    /// <summary>
    /// 对前置单据的操作
    /// </summary>
    internal class Preposition
    {
        Dictionary<object, object> dicIDs = new Dictionary<object, object>();
        /// <summary>
        /// 新增ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="docName">单据名称</param>
        internal void AddID(object id, object docName)
        {
            if (id == null || id == DBNull.Value || docName == null || docName == DBNull.Value)
            {
                return;
            }
            dicIDs.Add(id, docName);
        }
        internal List<SysSQLString> GetSql(string statusName, DataSources.EnumImportStaus importStaus)
        {
            string strImportStatus = ((int)importStaus).ToString();
            List<SysSQLString> listSql = new List<SysSQLString>();
            if (dicIDs.Count == 0)
            {
                return listSql;
            }
            foreach (string key in dicIDs.Keys)
            {
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.Param = new Dictionary<string, string>();
                switch (dicIDs[key].ToString())
                {
                    case "销售开单":
                        sql.sqlString = string.Format("update tb_parts_sale_billing set {0}='{1}' where sale_billing_id='{2}';", statusName, strImportStatus, key);
                        break;
                    case "维修结算单":
                        sql.sqlString = string.Format("update tb_maintain_settlement_info set {0}='{1}' where settlement_id='{2}';", statusName, strImportStatus, key);
                        break;
                    case "三包结算单":
                        sql.sqlString = string.Format("update tb_maintain_three_guaranty_settlement set {0}='{1}' where st_id='{2}';", statusName, strImportStatus, key);
                        break;
                    case "销售订单":
                        sql.sqlString = string.Format("update tb_parts_sale_order set {0}='{1}' where sale_order_id='{2}';", statusName, strImportStatus, key);
                        break;
                    case "采购开单":
                        sql.sqlString = string.Format("update tb_parts_purchase_billing set {0}='{1}' where purchase_billing_id='{2}';", statusName, strImportStatus, key);
                        break;
                    case "采购订单":
                        sql.sqlString = string.Format("update tb_parts_purchase_order set {0}='{1}' where order_id='{2}';", statusName, strImportStatus, key);
                        break;
                }
                listSql.Add(sql);
            }
            return listSql;
        }
    }
}
