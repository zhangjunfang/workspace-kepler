using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;

namespace HXCPcClient.UCForm.FinancialManagement
{
    public class Financial
    {
        /// <summary>
        /// 销售开单
        /// </summary>
        /// <param name="statusName"></param>
        /// <param name="importStatus"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static SysSQLString GetSaleBilling(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_parts_sale_billing set {0}='{1}' where sale_billing_id in ({2});", statusName, importStatus, ids);
            return sql;
        }
        public static SysSQLString GetSaleOrder(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_parts_sale_order set {0}='{1}' where sale_order_id in ({2});", statusName, importStatus, ids);
            return sql;
        }
        /// <summary>
        /// 维修结算单
        /// </summary>
        /// <param name="statusName"></param>
        /// <param name="importStatus"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static SysSQLString GetMaintainSettlement(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_maintain_settlement_info set {0}='{1}' where settlement_id in ({2});", statusName, importStatus, ids);
            return sql;
        }
        /// <summary>
        /// 三包结算单
        /// </summary>
        /// <param name="statusName"></param>
        /// <param name="importStatus"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static SysSQLString GetMaintainThree(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_maintain_three_guaranty_settlement set {0}='{1}' where st_id in ({2});", statusName, importStatus, ids);
            return sql;
        }
        /// <summary>
        /// 采购开单
        /// </summary>
        /// <param name="statusName"></param>
        /// <param name="importStatus"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static SysSQLString GetPurchaseBilling(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_parts_purchase_billing set {0}='{1}' where purchase_billing_id in ({2});", statusName, importStatus, ids);
            return sql;
        }

        public static SysSQLString GetPurchaseOrder(string statusName, string importStatus, string ids)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.sqlString = string.Format("update tb_parts_purchase_order set {0}='{1}' where order_id in ({2});", statusName, importStatus, ids);
            return sql;
        }
    }
}
