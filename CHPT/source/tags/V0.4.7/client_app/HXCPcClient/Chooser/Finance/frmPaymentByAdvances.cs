using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 预付导入
    /// </summary>
    public class frmPaymentByAdvances:frmBalanceDocuments
    {
        public frmPaymentByAdvances(string custCode) :
            base( custCode)
        {
            lblDateBill.Text = "付款日期从：";
            lblDateBill.Visible = false;
            dtiBillDate.Visible = false;
            lblCheckNum.Visible = false;
            txtCheckNumber.Visible = false;
            colReceivablesDate.Visible = false;
            colReceiptNO.Visible = false;
            colBalanceMoney.HeaderText = "已预付金额";
            colWaitMoney.HeaderText = "未预付金额";
            colReceivablesDate.HeaderText = "付款日期";
            colBillsType.Visible = false;
        }
        public frmPaymentByAdvances()
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
            sbWhere.AppendFormat(" wait_money>0 and sup_id='{0}'", custCode);
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
            dt = DBHelper.GetTable("", "v_parts_purchase_order_payment", "*", sbWhere.ToString(), "", "order by order_date");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells[colID.Name].Value = dr["order_id"];//ID
                dgvr.Cells[colBillsName.Name].Value = "采购订单";//单据名称
                dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
                dgvr.Cells[colTotalMoney.Name].Value = dr["prepaid_money"];//总金额
                dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
                dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
                dgvr.Cells[colOrderDate.Name].Value = Common.UtcLongToLocalDateTime(dr["order_date"], "yyyy-MM-dd");//单据日期
            }
        }

        protected override bool LockDocument(string ids)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("is_occupy_finance", ((int)DataSources.EnumImportStaus.OCCUPY).ToString());
            return DBHelper.BatchUpdateDataByIn("锁定采购订单", "tb_parts_purchase_order", dic, "order_id", ids.Split(','));
        }
    }
}
