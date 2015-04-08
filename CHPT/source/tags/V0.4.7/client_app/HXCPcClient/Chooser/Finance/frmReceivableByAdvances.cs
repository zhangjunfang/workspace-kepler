using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HXCPcClient.CommonClass;
using System.Windows.Forms;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 预收导入
    /// </summary>
    public class frmReceivableByAdvances:frmBalanceDocuments
    {
        public frmReceivableByAdvances(string custCode) :
            base( custCode)
        {
            colBillsType.Visible = false;
            lblDateBill.Visible = false;
            dtiBillDate.Visible = false;
            lblCheckNum.Visible = false;
            txtCheckNumber.Visible = false;
            colReceivablesDate.Visible = false;
            colReceiptNO.Visible = false;
        }

        public frmReceivableByAdvances()
            : base()
        { }

        protected override void BindData()
        {
            if (custCode.Length == 0)
            {
                return;
            }
            dgvData.RowCount = 0;
            DataTable dt;//数据
            StringBuilder sbWhere = new StringBuilder();
            colBalanceMoney.HeaderText = "已预收金额";
            colWaitMoney.HeaderText = "未预收金额";
            sbWhere.AppendFormat(" wait_money>0 and cust_id='{0}'", custCode);
            //sbWhere.Append(" and money<advance_money");
            sbWhere.AppendFormat(" and isnull(is_occupy_finance,0)='{0}'", (int)DataSources.EnumImportStaus.OPEN);
            if (!string.IsNullOrEmpty(dtiDate.StartDate))
            {
                sbWhere.AppendFormat(" and order_date>{0}", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtiDate.StartDate).Date));
            }
            if (!string.IsNullOrEmpty(dtiDate.EndDate))
            {
                sbWhere.AppendFormat(" and order_date <{0}", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtiDate.EndDate).Date.AddDays(1)));
            }
            string orderNum = txtOrderNum.Caption.Trim();//单据编号
            if (orderNum.Length > 0)
            {
                sbWhere.AppendFormat(" and order_num like '%{0}%'", orderNum);
            }
            dt = DBHelper.GetTable("", @"v_parts_sale_order_receivable", "*", sbWhere.ToString(), "", "order by order_date");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells[colID.Name].Value = dr["sale_order_id"];//ID
                dgvr.Cells[colBillsName.Name].Value = "销售订单";//单据名称
                dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
                dgvr.Cells[colTotalMoney.Name].Value = dr["advance_money"];//总金额
                dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
                dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
                //dgvr.Cells[colReceivablesDate.Name].Value = dr["receivables_date"];//收款日期
                //dgvr.Cells[colReceiptNO.Name].Value = dr["receipt_no"];//发票号
                dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["order_date"], "yyyy-MM-dd");//单据日期
            }
        }

        protected override bool LockDocument(string ids)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("is_occupy_finance", ((int)DataSources.EnumImportStaus.OCCUPY).ToString());
            return DBHelper.BatchUpdateDataByIn("锁定销售订单", "tb_parts_sale_order", dic, "sale_order_id", ids.Split(','));
        }
    }
}
