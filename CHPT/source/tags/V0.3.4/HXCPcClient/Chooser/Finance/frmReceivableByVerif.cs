using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXCPcClient.CommonClass;
using System.Windows.Forms;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 应收导入--往来核销
    /// </summary>
    public class frmReceivableByVerification:frmBalanceDocuments
    {
        public frmReceivableByVerification(string custCode)
            : base(custCode)
        {
 
        }
        protected override void BindData()
        {
            if (custCode.Length == 0)
            {
                return;
            }
            dgvData.Rows.Clear();
            DataTable dt;//数据
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("cust_id='{0}'", custCode);
            sbWhere.AppendFormat(" and wait_money>0");
            sbWhere.AppendFormat(" and is_lock='{0}'", (int)DataSources.EnumImportStaus.OPEN);
            dt = DBHelper.GetTable("", "v_receivable", "*", sbWhere.ToString(), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells[colID.Name].Value = dr["document_id"];//ID
                dgvr.Cells[colBillsName.Name].Value = dr["document_name"];//单据名称
                dgvr.Cells[colBillsType.Name].Value = dr["document_type"];//单据类型
                dgvr.Cells[colBillsCode.Name].Value = dr["document_num"];//单据编码
                dgvr.Cells[colTotalMoney.Name].Value = dr["YingShou"];//总金额
                dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
                dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
                string date = CommonCtrl.IsNullToString(dr["Date"]);
                if (date.Length > 0)
                {
                    dgvr.Cells[colReceivablesDate.Name].Value = Common.UtcLongToLocalDateTime(Int64.Parse(date));//收款日期
                }
                dgvr.Cells[colReceiptNO.Name].Value = dr["receipt_no"];//发票号
            }
        }

        protected override bool LockDocument(string ids)
        {
            ids = string.Format("'{0}'", ids);
            ids = ids.Replace(",", "','");
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString saleBilling = new SysSQLString();//销售开单
            saleBilling.cmdType = CommandType.Text;
            saleBilling.Param = new Dictionary<string, string>();
            saleBilling.sqlString =string.Format( "update tb_parts_sale_billing set is_lock=@is_lock where sale_billing_id in ({0})",ids);
            saleBilling.Param.Add("is_lock", ((int)DataSources.EnumImportStaus.LOCK).ToString());
            //saleBilling.Param.Add("id", ids);
            listSql.Add(saleBilling);
            SysSQLString maintainSql = new SysSQLString();//维修结算单
            maintainSql.cmdType = CommandType.Text;
            maintainSql.sqlString =string.Format( "update tb_maintain_settlement_info set is_lock=@is_lock where settlement_id in ({0})",ids);
            maintainSql.Param = new Dictionary<string, string>();
            maintainSql.Param.Add("is_lock", ((int)DataSources.EnumImportStaus.LOCK).ToString());
            //maintainSql.Param.Add("id", ids);
            listSql.Add(maintainSql);
            SysSQLString threeSql = new SysSQLString();//三包服务单
            threeSql.cmdType = CommandType.Text;
            threeSql.sqlString = string.Format("update tb_maintain_three_guaranty_settlement set is_lock=@is_lock where st_id in ({0})", ids);
            threeSql.Param = new Dictionary<string, string>();
            threeSql.Param.Add("is_lock", ((int)DataSources.EnumImportStaus.LOCK).ToString());
            //threeSql.Param.Add("id", ids);
            listSql.Add(threeSql);
            return DBHelper.BatchExeSQLStringMultiByTrans("锁定应收单据", listSql);
        }
    }
}
