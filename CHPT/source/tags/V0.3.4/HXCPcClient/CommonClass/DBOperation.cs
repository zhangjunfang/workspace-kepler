using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SYSModel;
using Model;
using Utility.Common;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 数据库公共操作
    /// </summary>
    public class DBOperation
    {
        /// <summary>
        /// 获取往来余额
        /// </summary>
        /// <param name="custID">往来单位单位ID</param>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public static decimal WaiLaiYuE(string custID, DataSources.EnumOrderType orderType)
        {
            SYSModel.SQLObj sql = new SYSModel.SQLObj();
            sql.cmdType = CommandType.StoredProcedure;
            if (orderType == DataSources.EnumOrderType.PAYMENT)
            {
                sql.sqlString = "p_yufu_yu_e";
            }
            else
            {
                sql.sqlString = "p_yushou_yu_e";
            }
            sql.Param = new Dictionary<string, ParamObj>();
            sql.Param.Add("cust_code", new ParamObj("cust_code", custID, SysDbType.VarChar, 40));
            DataSet ds = DBHelper.GetDataSet("查询往来余额", sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows[0][0] == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
            }
        }

        /// <summary>
        /// 添加应收应付
        /// </summary>
        /// <param name="bill">应收应付单</param>
        /// <param name="documents">结算单据</param>
        /// <param name="detail">应收应付明细</param>
        /// <returns></returns>
        public static bool AddBillReceivable(tb_bill_receivable bill, tb_balance_documents documents, tb_payment_detail detail)
        {
            bool isAdd = false;
            List<SysSQLString> listSql = new List<SysSQLString>();
            bill.payable_single_id = Guid.NewGuid().ToString();
            documents.order_id = bill.payable_single_id;
            detail.order_id = bill.payable_single_id;
            AddReceivableSqlString(listSql, bill);
            AddDetailSqlString(listSql, detail);
            AddBalanceDocumentsSqlString(listSql, documents);
            try
            {
                isAdd = DBHelper.BatchExeSQLStringMultiByTrans("新增应收应付", listSql);
            }
            catch (Exception ex)
            {
                isAdd = false;
            }
            return isAdd;
        }

        /// <summary>
        /// 添加收付款sql
        /// </summary>
        /// <param name="list"></param>
        static void AddReceivableSqlString(List<SysSQLString> list, tb_bill_receivable bill)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("payable_single_id", bill.payable_single_id);
            sql.Param.Add("order_num", bill.order_num);//单号
            sql.Param.Add("order_date", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//时间
            sql.Param.Add("order_status", ((int)DataSources.EnumAuditStatus.SUBMIT).ToString());//单据状态
            sql.Param.Add("order_type", bill.order_type.ToString());//单据类型
            sql.Param.Add("cust_id", bill.cust_id);
            //sql.Param.Add("cust_code", bill.cust_code);//往来单位
            //sql.Param.Add("cust_name", bill.cust_name);//
            sql.Param.Add("payment_type", bill.payment_type.ToString());//收付款类型
            sql.Param.Add("payment_money", bill.payment_money.ToString());
            sql.Param.Add("dealings_balance", bill.dealings_balance==null ?null :bill.dealings_balance.ToString());
            //sql.Param.Add("bank_of_deposit", bill.bank_of_deposit);//开户银行
            //sql.Param.Add("bank_account", bill.bank_account);//银行账户
            sql.Param.Add("org_id", bill.org_id);//部门
            sql.Param.Add("handle", GlobalStaticObj.UserID);//经办人
            sql.Param.Add("operator", GlobalStaticObj.UserID);//操作人
            //sql.Param.Add("remark", bill.remark);
            sql.Param.Add("create_by", GlobalStaticObj.UserID);
            sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            sql.Param.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
//            sql.sqlString = @"INSERT INTO [tb_bill_receivable]
//           (payable_single_id,order_num ,order_date,order_status,order_type,cust_id,cust_code,cust_name,payment_type,payment_money,dealings_balance
//,bank_of_deposit,bank_account,org_id,handle,operator,create_by,create_time,status,enable_flag,remark)
//     VALUES
//           (@payable_single_id,@order_num,@order_date,@order_status,@order_type,@cust_id,@cust_code,@cust_name,@payment_type,@payment_money,@dealings_balance
//,@bank_of_deposit,@bank_account,@org_id,@handle,@operator,@create_by,@create_time,@status,@enable_flag,@remark);";
            if (bill.order_type == 0)
            {
                sql.sqlString = @"INSERT INTO [tb_bill_receivable]
           (payable_single_id,order_num ,order_date,order_status,order_type,cust_id,cust_code,cust_name,payment_type,payment_money,dealings_balance
,bank_of_deposit,bank_account,org_id,handle,operator,create_by,create_time,status,enable_flag)
select @payable_single_id,@order_num,@order_date,@order_status,@order_type,@cust_id,cust_code,cust_name,@payment_type,@payment_money,@dealings_balance,
bank_account_person,bank_account,@org_id,@handle,@operator,@create_by,@create_time,@status,@enable_flag from tb_customer
where cust_id=@cust_id";
            }
            else
            {
                sql.sqlString = @"INSERT INTO [tb_bill_receivable]
           (payable_single_id,order_num ,order_date,order_status,order_type,cust_id,cust_code,cust_name,payment_type,payment_money,dealings_balance
,bank_of_deposit,bank_account,org_id,handle,operator,create_by,create_time,status,enable_flag)
select @payable_single_id,@order_num,@order_date,@order_status,@order_type,@cust_id,sup_code,sup_short_name,@payment_type,@payment_money,@dealings_balance,
null,null,@org_id,@handle,@operator,@create_by,@create_time,@status,@enable_flag 
from tb_supplier where sup_id =@cust_id";
            }
            list.Add(sql);
        }

        /// <summary>
        /// 添加收付款明细sql
        /// </summary>
        /// <param name="list"></param>
        static void AddDetailSqlString(List<SysSQLString> list, tb_payment_detail detail)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("order_id", detail.order_id);
            sql.Param.Add("money", detail.money.ToString());//金额
            sql.Param.Add("balance_way", detail.balance_way);//结算方式
            //sql.Param.Add("payment_account", detail.payment_account);//付款账户
            //sql.Param.Add("bank_of_deposit", detail.bank_of_deposit);//开户银行
            //sql.Param.Add("bank_account", detail.bank_account);//银行账户
            sql.Param.Add("check_number", detail.check_number);//票号
            //sql.Param.Add("remark", detail.remark);//备注
            sql.Param.Add("create_by", GlobalStaticObj.UserID);
            sql.Param.Add("create_time",Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
//            sql.sqlString = @"INSERT INTO [tb_payment_detail](payment_detail_id,order_id,money
//,balance_way,payment_account,bank_of_deposit,bank_account,check_number,remark,create_by,create_time)
//     VALUES
//(@payment_detail_id,@order_id,@money,@balance_way,@payment_account,@bank_of_deposit,@bank_account,@check_number,@remark
//,@create_by,@create_time)";
            sql.Param.Add("payment_detail_id", Guid.NewGuid().ToString());
            sql.sqlString = @"INSERT INTO [tb_payment_detail](payment_detail_id,order_id,money
,balance_way,payment_account,bank_of_deposit,bank_account,check_number,create_by,create_time)
select @payment_detail_id,@order_id,@money,a.balance_way_id,a.default_account,b.bank_name,b.cashier_account,
@check_number,@create_by,@create_time from tb_balance_way a
left join v_cashier_account b on a.default_account=b.cashier_account
where a.balance_way_id=@balance_way";
            list.Add(sql);
        }

        /// <summary>
        /// 添加结算单据sql
        /// </summary>
        /// <param name="list"></param>
        static void AddBalanceDocumentsSqlString(List<SysSQLString> list, tb_balance_documents documents)
        {
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            sql.Param.Add("order_id", documents.order_id);
            sql.Param.Add("documents_name", documents.documents_name);//单据名称
            sql.Param.Add("documents_id", documents.documents_id);//单据ID
            sql.Param.Add("documents_num", documents.documents_num);//单据号
            sql.Param.Add("documents_date", documents.documents_date.ToString());
            sql.Param.Add("billing_money", documents.billing_money == null ? null : documents.billing_money.ToString());//开单金额
            sql.Param.Add("settled_money", "0");//已结算金额
            sql.Param.Add("wait_settled_money", documents.billing_money == null ? null : documents.billing_money.ToString());//待结算金额
            sql.Param.Add("settlement_money", documents.billing_money == null ? null : documents.billing_money.ToString());//本次结算
            sql.Param.Add("gathering", "1");//收款
            sql.Param.Add("paid_money", documents.billing_money == null ? null : documents.billing_money.ToString());//实收金额
            sql.Param.Add("deposit_rate", "100");//折扣率
            sql.Param.Add("deduction", null);//折扣金额
            sql.Param.Add("remark", documents.remark);//备注
            sql.Param.Add("create_by", GlobalStaticObj.UserID);
            sql.Param.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            sql.sqlString = @"INSERT INTO [tb_balance_documents]
           (balance_documents_id
           ,order_id
           ,documents_name
           ,documents_id
           ,documents_num
           ,documents_date
           ,billing_money
           ,settled_money
           ,wait_settled_money
           ,settlement_money
           ,gathering
           ,paid_money
           ,deposit_rate
           ,deduction
           ,remark
           ,create_by
           ,create_time)
     VALUES
           (@balance_documents_id
           ,@order_id
           ,@documents_name
           ,@documents_id
           ,@documents_num
           ,@documents_date
           ,@billing_money
           ,@settled_money
           ,@wait_settled_money
           ,@settlement_money
           ,@gathering
           ,@paid_money
           ,@deposit_rate
           ,@deduction
           ,@remark
           ,@create_by
           ,@create_time)";
            sql.Param.Add("balance_documents_id", Guid.NewGuid().ToString());
            list.Add(sql);
        }
    }
}
