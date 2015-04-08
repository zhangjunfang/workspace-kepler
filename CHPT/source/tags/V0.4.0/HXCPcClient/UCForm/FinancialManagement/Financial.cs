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

        /// <summary>
        /// 计算往来核销单据已结算金额
        /// </summary>
        /// <param name="enumAccount">往来核销单据类型</param>
        /// <param name="id">往来核销ID</param>
        /// <param name="list">sql列表</param>
        public static void DocumentSettlementByVerification(DataSources.EnumAccountVerification enumAccount, string id, List<SysSQLString> list)
        {
            //不为预收转预收，预付转预付的情况，计算已结算金额
            if (enumAccount != DataSources.EnumAccountVerification.YuShouToYuShou && enumAccount != DataSources.EnumAccountVerification.YuFuToYuFu)
            {
                SysSQLString sqlSettlement = new SysSQLString();
                sqlSettlement.cmdType = CommandType.StoredProcedure;
                sqlSettlement.Param = new Dictionary<string, string>();
                sqlSettlement.Param.Add("order_id", id);
                sqlSettlement.Param.Add("type", "2");

                //预收冲应收，应收转应收
                if (enumAccount == DataSources.EnumAccountVerification.YuShouToYingShou || enumAccount == DataSources.EnumAccountVerification.YingShouToYingShou)
                {
                    sqlSettlement.sqlString = "p_yingshou_jiesuan";
                }
                //预付冲应付，应付转应付
                else if (enumAccount == DataSources.EnumAccountVerification.YuFuToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingFu)
                {
                    sqlSettlement.sqlString = "p_yingfu_jiesuan";
                }
                //应收冲应付，应付冲应收
                if (enumAccount == DataSources.EnumAccountVerification.YingShouToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
                {
                    //应收已结算金额
                    sqlSettlement.sqlString = "p_yingshou_jiesuan";
                    //应付已结算金额
                    SysSQLString sqlSettlement1 = new SysSQLString();
                    sqlSettlement1.cmdType = CommandType.StoredProcedure;
                    sqlSettlement1.Param = new Dictionary<string, string>();
                    sqlSettlement1.Param.Add("order_id", id);
                    sqlSettlement1.Param.Add("type", "2");
                    sqlSettlement1.sqlString = "p_yingfu_jiesuan";
                    list.Add(sqlSettlement1);
                }
                list.Add(sqlSettlement);
            }
        }

        /// <summary>
        /// 计算应收应付单据已结算金额
        /// </summary>
        /// <param name="orderType">应收应付单据类型</param>
        /// <param name="id">应收应付ID</param>
        /// <param name="list">sql列表</param>
        public static void DocumentSettlementByBill(DataSources.EnumOrderType orderType, string id, List<SysSQLString> list)
        {
            SysSQLString sqlSettlement = new SysSQLString();
            sqlSettlement.cmdType = CommandType.StoredProcedure;
            sqlSettlement.Param = new Dictionary<string, string>();
            sqlSettlement.Param.Add("order_id", id);
            sqlSettlement.Param.Add("type", "1");
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                sqlSettlement.sqlString = "p_yingshou_jiesuan";
            }
            else
            {
                sqlSettlement.sqlString = "p_yingfu_jiesuan";
            }
            list.Add(sqlSettlement);
        }
        /// <summary>
        /// 计算应收应付单据已预收付金额
        /// </summary>
        /// <param name="orderType">应收应付单据类型</param>
        /// <param name="id">应收应付ID</param>
        /// <param name="list">sql列表</param>
        public static void DocumentAdvanceByBill(DataSources.EnumOrderType orderType,string id, List<SysSQLString> list)
        {
            SysSQLString sqlSettlement = new SysSQLString();
            sqlSettlement.cmdType = CommandType.StoredProcedure;
            sqlSettlement.Param = new Dictionary<string, string>();
            sqlSettlement.Param.Add("order_id", id);
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                sqlSettlement.sqlString = "p_yushou_jiesuan";
            }
            else
            {
                sqlSettlement.sqlString = "p_yufu_jiesuan";
            }
            list.Add(sqlSettlement);
        }
    }
}
