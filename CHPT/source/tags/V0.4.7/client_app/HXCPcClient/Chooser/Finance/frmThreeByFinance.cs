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
    /// 三包服务单
    /// </summary>
    public class frmThreeByFinance : frmBalanceDocuments
    {
        public frmThreeByFinance(string custCode)
            : base(custCode)
        {
            lblDateBill.Visible = false;
            dtiBillDate.Visible = false;
            colBillsType.Visible = false;
            colReceivablesDate.Visible = false;
            colReceiptNO.Visible = false;
            lblDate.Visible = false;
            dtiDate.Visible = false;
            lblCheckNum.Location = new System.Drawing.Point(44, 27);
            lblCheckNum.Text = "财务凭证号：";
            txtCheckNumber.Location = new System.Drawing.Point(120,22);
        }
        public frmThreeByFinance()
            : base()
        {

        }

        protected override void BindData()
        {
            if (custCode.Length == 0)
            {
                return;
            }
            dgvData.RowCount = 0;
            DataTable dt;//数据
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("cust_id='{0}'", custCode);
            sbWhere.AppendFormat(" and wait_money>0");
            sbWhere.AppendFormat(" and isnull(is_occupy_finance,0)='{0}'", (int)DataSources.EnumImportStaus.OPEN);
            string orderNum = txtOrderNum.Caption.Trim();//单据编号
            if (orderNum.Length > 0)
            {
                sbWhere.AppendFormat(" and settlement_no like '%{0}%'", orderNum);
            }
            string finance_voucher_no = txtCheckNumber.Caption.Trim();//财务凭证号
            if (finance_voucher_no.Length > 0)
            {
                sbWhere.AppendFormat(" and finance_voucher_no like '%{0}%'", finance_voucher_no);
            }
            dt = DBHelper.GetTable("", "v_tb_maintain_three_guaranty_settlement_yt_receivable", "*", sbWhere.ToString(), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
                dgvr.Cells["colID"].Value = dr["st_id"];//ID
                dgvr.Cells["colBillsName"].Value = "三包结算单";//单据名称
                //dgvr.Cells["colBillsType"].Value = dr["price_type"];//单据类型
                dgvr.Cells["colBillsCode"].Value = dr["settlement_no"];//单据编码
                dgvr.Cells["colTotalMoney"].Value = dr["sum_cost"];//总金额
                dgvr.Cells["colBalanceMoney"].Value = dr["money"];//已结算
                dgvr.Cells["colWaitMoney"].Value = dr["wait_money"];//未结算
                string date = CommonCtrl.IsNullToString(dr["clearing_time"]);
                if (date.Length > 0)
                {
                    dgvr.Cells["colReceivablesDate"].Value = Common.UtcLongToLocalDateTime(Int64.Parse(date));//收款日期
                }
                //dgvr.Cells["colReceiptNO"].Value = dr["receipt_no"];//发票号
            }
        }

        protected override bool LockDocument(string ids)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("is_occupy_finance", ((int)DataSources.EnumImportStaus.OCCUPY).ToString());
            return DBHelper.BatchUpdateDataByIn("锁定三包单", "tb_maintain_three_guaranty_settlement", dic, "st_id", ids.Split(','));
        }
    }
}
