using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;
using HXCPcClient.CommonClass;
using Utility.Common;

namespace HXCPcClient.UCForm.FinancialManagement
{
    public class Financial
    {

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
        public static void DocumentAdvanceByBill(DataSources.EnumOrderType orderType, string id, List<SysSQLString> list)
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

        /// <summary>
        /// 检查单据未结算金额
        /// </summary>
        /// <param name="documentID">单据ID</param>
        /// <param name="documentType">单据类型</param>
        /// <param name="settlementMoney">本次结算金额</param>
        /// <returns></returns>
        public static bool CheckDocument(string documentID, string documentType, decimal settlementMoney)
        {
            decimal waiMoney = GetDocumentWaitMoney(documentID, documentType);
            if (waiMoney < settlementMoney)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取单据待结算金额
        /// </summary>
        /// <param name="documentID">单据ID</param>
        /// <param name="documentType">单据名称</param>
        /// <returns>待结算金额</returns>
        public static decimal GetDocumentWaitMoney(string documentID, string documentType)
        {
            DataTable dt = null;
            switch (documentType)
            {
                case "销售开单":
                    dt = DBHelper.GetTable("", "v_parts_sale_billing_receivable", "wait_money", string.Format("sale_billing_id='{0}'", documentID), "", "");
                    break;
                case "采购开单":
                    dt = DBHelper.GetTable("", "v_parts_purchase_billing_payment", "wait_money", string.Format("purchase_billing_id='{0}'", documentID), "", "");
                    break;
                case "维修结算单":
                    dt = DBHelper.GetTable("", "v_maintain_settlement_info_receivable", "wait_money", string.Format("settlement_id='{0}'", documentID), "", "");
                    break;
                case "三包结算单":
                    dt = DBHelper.GetTable("", "v_tb_maintain_three_guaranty_settlement_yt_receivable", "wait_money", string.Format("st_id='{0}'", documentID), "", "");
                    break;
            }
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["wait_money"] != null && dt.Rows[0]["wait_money"] != DBNull.Value)
            {
                decimal waitMoney = Convert.ToDecimal(dt.Rows[0]["wait_money"]);
                return waitMoney;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 重新计算单据的已结算,待结算金额
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <param name="id">单据ID</param>
        /// <param name="list"></param>
        public static void DocumentMoney(DataSources.EnumOrderType orderType, string id, List<SysSQLString> list)
        {
            SysSQLString sqlSettlement = new SysSQLString();
            sqlSettlement.cmdType = CommandType.Text;
            sqlSettlement.Param = new Dictionary<string, string>();
            sqlSettlement.Param.Add("order_id", id);
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                sqlSettlement.sqlString = @"update a set settled_money=b.money,wait_settled_money=billing_money-ISNULL(b.money,0) 
from tb_balance_documents a left join v_YingShou b on a.documents_id=b.documents_id where a.order_id=@order_id";
            }
            else
            {
                sqlSettlement.sqlString = @"update a set settled_money=b.money,wait_settled_money=billing_money-ISNULL(b.money,0) 
from tb_balance_documents a left join v_YingFu b on a.documents_id=b.documents_id where a.order_id=@order_id";
            }
            list.Add(sqlSettlement);
        }

        /// <summary>
        /// 往来核销重新计算单据的已结算,待结算金额
        /// </summary>
        /// <param name="enumAccount">单据类型</param>
        /// <param name="id">单据ID</param>
        /// <param name="list"></param>
        public static void VerificationDocumentMoney(DataSources.EnumAccountVerification enumAccount, string id, List<SysSQLString> list)
        {
            //不为预收转预收，预付转预付的情况，重新计算单据的已结算,待结算金额
            if (enumAccount != DataSources.EnumAccountVerification.YuShouToYuShou && enumAccount != DataSources.EnumAccountVerification.YuFuToYuFu)
            {
                SysSQLString sqlSettlement = new SysSQLString();
                sqlSettlement.cmdType = CommandType.Text;
                sqlSettlement.Param = new Dictionary<string, string>();
                sqlSettlement.Param.Add("order_id", id);
                string orderIndex = "1";
                //应收Sql
                string yingShouSql = @"update a set settled_money=b.money,wait_settled_money=a.money-ISNULL(b.money,0) 
from tb_verificationn_documents a left join v_YingShou b on a.order_id=b.documents_id
where a.account_verification_id=@order_id and order_index=@order_index";
                //应付Sql
                string yingFuSql = @"update a set settled_money=b.money,wait_settled_money=a.money-ISNULL(b.money,0) 
from tb_verificationn_documents a left join v_YingFu b on a.order_id=b.documents_id
where a.account_verification_id=@order_id and order_index=@order_index";

                //预收冲应收，应收转应收
                if (enumAccount == DataSources.EnumAccountVerification.YuShouToYingShou || enumAccount == DataSources.EnumAccountVerification.YingShouToYingShou)
                {
                    sqlSettlement.sqlString = yingShouSql;
                }
                //预付冲应付，应付转应付
                else if (enumAccount == DataSources.EnumAccountVerification.YuFuToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingFu)
                {
                    sqlSettlement.sqlString = yingFuSql;
                }
                //应收冲应付，应付冲应收
                if (enumAccount == DataSources.EnumAccountVerification.YingShouToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
                {
                    //应收已结算金额
                    sqlSettlement.sqlString = yingShouSql;
                    //应付已结算金额
                    SysSQLString sqlSettlement1 = new SysSQLString();
                    sqlSettlement1.cmdType = CommandType.Text;
                    sqlSettlement1.Param = new Dictionary<string, string>();
                    sqlSettlement1.Param.Add("order_id", id);
                    sqlSettlement1.sqlString = yingFuSql;
                    if (enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
                    {
                        orderIndex = "2";
                        sqlSettlement1.Param.Add("order_index", "1");
                    }
                    else
                    {
                        sqlSettlement1.Param.Add("order_index", "2");
                    }
                    list.Add(sqlSettlement1);
                }
                sqlSettlement.Param.Add("order_index", orderIndex);
                list.Add(sqlSettlement);
            }
        }
        /// <summary>
        /// 验证往来核销单据本次核销是否大于结算金额
        /// </summary>
        /// <param name="enumAccount">单据类型</param>
        /// <param name="id">单据ID</param>
        /// <returns></returns>
        public static bool VerificationCheckDocumentMoney(DataSources.EnumAccountVerification enumAccount, string id)
        {
            //不为预收转预收，预付转预付的情况，验证本次核销是否大于待结算金额
            if (enumAccount != DataSources.EnumAccountVerification.YuShouToYuShou && enumAccount != DataSources.EnumAccountVerification.YuFuToYuFu)
            {
                SQLObj sqlSettlement = new SQLObj();
                sqlSettlement.cmdType = CommandType.Text;
                sqlSettlement.Param = new Dictionary<string, ParamObj>();
                sqlSettlement.Param.Add("order_id", new ParamObj("order_id", id, SysDbType.VarChar, 40));
                string orderIndex = "1";
                //应收Sql
                string yingShouSql = @"select a.settled_money
from tb_verificationn_documents a left join v_YingShou b on a.order_id=b.documents_id
where a.account_verification_id=@order_id and order_index=@order_index and (a.money-ISNULL(b.money,0))<a.verification_money";
                //应付Sql
                string yingFuSql = @"select a.settled_money
from tb_verificationn_documents a left join v_YingFu b on a.order_id=b.documents_id
where a.account_verification_id=@order_id and order_index=@order_index and (a.money-ISNULL(b.money,0))<a.verification_money";

                //预收冲应收，应收转应收
                if (enumAccount == DataSources.EnumAccountVerification.YuShouToYingShou || enumAccount == DataSources.EnumAccountVerification.YingShouToYingShou)
                {
                    sqlSettlement.sqlString = yingShouSql;
                }
                //预付冲应付，应付转应付
                else if (enumAccount == DataSources.EnumAccountVerification.YuFuToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingFu)
                {
                    sqlSettlement.sqlString = yingFuSql;
                }
                //应收冲应付，应付冲应收
                if (enumAccount == DataSources.EnumAccountVerification.YingShouToYingFu || enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
                {
                    //应收已结算金额
                    sqlSettlement.sqlString = yingShouSql;
                    //应付已结算金额
                    SQLObj sqlSettlement1 = new SQLObj();
                    sqlSettlement1.cmdType = CommandType.Text;
                    sqlSettlement1.Param = new Dictionary<string, ParamObj>();
                    sqlSettlement1.Param.Add("order_id", new ParamObj("order_id", id, SysDbType.VarChar, 40));
                    sqlSettlement1.sqlString = yingFuSql;
                    if (enumAccount == DataSources.EnumAccountVerification.YingFuToYingShou)
                    {
                        orderIndex = "2";
                        sqlSettlement1.Param.Add("order_index", new ParamObj("order_index", "1", SysDbType.VarChar, 1));
                    }
                    else
                    {
                        sqlSettlement1.Param.Add("order_index", new ParamObj("order_index", "2", SysDbType.VarChar, 1));
                    }
                    DataSet ds1 = DBHelper.GetDataSet("验证往来核销单据", sqlSettlement1);
                    if (ds1 == null || ds1.Tables.Count == 0 || ds1.Tables[0].Rows.Count == 0)
                    {
                        return false;
                    }
                }
                sqlSettlement.Param.Add("order_index", new ParamObj("order_index", orderIndex, SysDbType.VarChar, 1));
                DataSet ds = DBHelper.GetDataSet("验证往来核销单据", sqlSettlement);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 验证应收应付单据本次结算是否大于结算金额
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <param name="id">单据ID</param>
        /// <returns></returns>
        public static bool CheckDocumentMoney(DataSources.EnumOrderType orderType, string id)
        {
            SQLObj sqlSettlement = new SQLObj();
            sqlSettlement.cmdType = CommandType.Text;
            sqlSettlement.Param = new Dictionary<string, ParamObj>();
            sqlSettlement.Param.Add("order_id", new ParamObj("order_id", id, SysDbType.VarChar, 40));
            if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            {
                sqlSettlement.sqlString = @"select a.order_id,a.settlement_money from tb_balance_documents a 
left join v_YingShou c on a.documents_id=c.documents_id  
where (a.billing_money- isnull(c.money,0))<a.settlement_money and a.order_id=@order_id";
            }
            else
            {
                sqlSettlement.sqlString = @"select a.order_id,a.settlement_money from tb_balance_documents a 
left join v_YingFu c on a.documents_id=c.documents_id  
where (a.billing_money- isnull(c.money,0))<a.settlement_money and a.order_id=@order_id";
            }
            DataSet ds = DBHelper.GetDataSet("验证应收应付单据", sqlSettlement);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckTotal(int strOrderType, string id)
        {
            decimal documentMoney = 0;//单据金额
            DataTable dtDocument = DBHelper.GetTable("", "tb_balance_documents", "sum(settlement_money) settlement_money,sum(paid_money) paid_money,count(1) document_count", string.Format("order_id='{0}'", id), "", "");
            //预收、预付
            if (strOrderType == 0)
            {
                //没有单据,则不需要验证
                if (dtDocument.Rows.Count == 0)
                {
                    return true;
                }
                if (Common.ConvertToDecimal(dtDocument.Rows[0]["document_count"]) == 0)
                {
                    return true;
                }
                documentMoney = Common.ConvertToDecimal(dtDocument.Rows[0]["settlement_money"]);//本次结算金额
            }
            else if (strOrderType == 1)
            {
                documentMoney = Common.ConvertToDecimal(dtDocument.Rows[0]["paid_money"]);//实收金额
            }
            //收付款明细
            DataTable dtDetail = DBHelper.GetTable("", "tb_payment_detail", "sum(money) money", string.Format("order_id='{0}'", id), "", "");
            if (dtDetail != null && dtDetail.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return Common.ConvertToDecimal(dtDetail.Rows[0]["money"]) == documentMoney;
            }
        }
    }
}
