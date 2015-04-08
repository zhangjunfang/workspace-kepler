using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.Chooser
{
    public partial class frmBalanceDocuments : FormChooser
    {
        //DataSources.EnumOrderType orderType;//单据类型
        protected string custCode = string.Empty;//客户编码
        //protected string paymentType ;//收付款类型
        public string ids = string.Empty;//返回的ID集合
        public List<DataGridViewRow> listRows = new List<DataGridViewRow>();
        public frmBalanceDocuments( string custCode)
        {
            //dgvData.ReadOnly = false;
            InitializeComponent();
            //this.orderType = orderType;
            this.custCode = custCode;
            //this.paymentType = paymentType;
            dgvData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvData.Columns)
            {
                if (dgvc.Name == colCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }

        private void frmBalanceDocuments_Load(object sender, EventArgs e)
        {
            dtpBillsEndDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            BindData();
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected virtual void BindData()
        {
            //if (custCode.Length == 0 || paymentType.Length == 0)
            //{
            //    return;
            //}
            //dgvData.RowCount = 0;
            //DataTable dt;//数据
            //StringBuilder sbWhere = new StringBuilder();
            //if (orderType == DataSources.EnumOrderType.RECEIVABLE)
            //{
            //    #region 应收
            //    DataSources.EnumReceivableType enumType = (DataSources.EnumReceivableType)int.Parse(paymentType);
            //    if (enumType == DataSources.EnumReceivableType.RECEIVABLE)
            //    {
            //        sbWhere.AppendFormat("cust_id='{0}'", custCode);
            //        sbWhere.AppendFormat(" and wait_money>0");
            //        dt = DBHelper.GetTable("", "v_parts_sale_billing_receivable", "*", sbWhere.ToString(), "", "");
            //        if (dt == null || dt.Rows.Count == 0)
            //        {
            //            return;
            //        }
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
            //            dgvr.Cells[colID.Name].Value = dr["sale_billing_id"];//ID
            //            dgvr.Cells[colBillsName.Name].Value = "销售开单";//单据名称
            //            dgvr.Cells[colBillsType.Name].Value = dr["price_type"];//单据类型
            //            dgvr.Cells[colBillsCode.Name].Value = dr["sale_billing_id"];//单据编码
            //            dgvr.Cells[colTotalMoney.Name].Value = dr["current_collect"];//总金额
            //            dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
            //            dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
            //            string date = CommonCtrl.IsNullToString(dr["receivables_date"]);
            //            if (date.Length > 0)
            //            {
            //                dgvr.Cells[colReceivablesDate.Name].Value = Common.UtcLongToLocalDateTime(Int64.Parse(date));//收款日期
            //            }
            //            dgvr.Cells[colReceiptNO.Name].Value = dr["receipt_no"];//发票号
            //        }
            //    }
            //    else
            //    {
            //        colBalanceMoney.HeaderText = "已预收金额";
            //        colWaitMoney.HeaderText = "未预收金额";
            //        sbWhere.AppendFormat(" cust_code='{0}'", custCode);
            //        //sbWhere.Append(" and money<advance_money");
            //        dt = DBHelper.GetTable("", @"v_parts_sale_order_receivable", "*", sbWhere.ToString(), "", "");
            //        if (dt == null || dt.Rows.Count == 0)
            //        {
            //            return;
            //        }
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
            //            dgvr.Cells[colID.Name].Value = dr["sale_order_id"];//ID
            //            dgvr.Cells[colBillsName.Name].Value = "销售订单";//单据名称
            //            colBillsType.Visible = false;
            //            dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
            //            dgvr.Cells[colTotalMoney.Name].Value = dr["advance_money"];//总金额
            //            dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
            //            dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
            //            //dgvr.Cells[colReceivablesDate.Name].Value = dr["receivables_date"];//收款日期
            //            //dgvr.Cells[colReceiptNO.Name].Value = dr["receipt_no"];//发票号
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{
            //    #region 应付
            //    colReceivablesDate.HeaderText = "付款日期";
            //    lblDate.Text = "付款日期：";
            //    DataSources.EnumPaymentType enumType = (DataSources.EnumPaymentType)int.Parse(paymentType);
            //    if (enumType == DataSources.EnumPaymentType.PAYMENT)
            //    {
            //        colBalanceMoney.HeaderText = "已付款金额";
            //        colWaitMoney.HeaderText = "未付款金额";
            //        sbWhere.AppendFormat("sup_name='{0}'", custCode);
            //        sbWhere.Append(" and wait_money>0");
            //        dt = DBHelper.GetTable("", "v_parts_purchase_billing_payment", "*", sbWhere.ToString(), "", "");
            //        if (dt == null || dt.Rows.Count == 0)
            //        {
            //            return;
            //        }
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
            //            dgvr.Cells[colID.Name].Value = dr["purchase_billing_id"];//ID
            //            dgvr.Cells[colBillsName.Name].Value = "采购开单";//单据名称
            //            dgvr.Cells[colBillsType.Name].Value = dr["order_type"];//单据类型
            //            dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
            //            dgvr.Cells[colTotalMoney.Name].Value = dr["this_payment"];//总金额
            //            dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
            //            dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
            //            string date = CommonCtrl.IsNullToString(dr["payment_date"]);
            //            if (date.Length > 0)
            //            {
            //                dgvr.Cells[colReceivablesDate.Name].Value = Common.UtcLongToLocalDateTime(Int64.Parse(date));//付款日期
            //            }
            //            dgvr.Cells[colReceiptNO.Name].Value = dr["receipt_no"];//发票号
            //        }
            //    }
            //    else
            //    {
            //        colBalanceMoney.HeaderText = "已预付金额";
            //        colWaitMoney.HeaderText = "未预付金额";
            //        sbWhere.AppendFormat(" sup_code='{0}'", custCode);
            //        dt = DBHelper.GetTable("", "v_parts_purchase_order_payment", "*", sbWhere.ToString(), "", "");
            //        if (dt == null || dt.Rows.Count == 0)
            //        {
            //            return;
            //        }
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            DataGridViewRow dgvr = dgvData.Rows[dgvData.Rows.Add()];
            //            dgvr.Cells[colID.Name].Value = dr["order_id"];//ID
            //            dgvr.Cells[colBillsName.Name].Value = "采购订单";//单据名称
            //            colBillsType.Visible = false;
            //            dgvr.Cells[colBillsCode.Name].Value = dr["order_num"];//单据编码
            //            dgvr.Cells[colTotalMoney.Name].Value = dr["prepaid_money"];//总金额
            //            dgvr.Cells[colBalanceMoney.Name].Value = dr["money"];//已结算
            //            dgvr.Cells[colWaitMoney.Name].Value = dr["wait_money"];//未结算
            //        }
            //    }
            //    #endregion
            //}
        }
        //清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCheckNumber.Caption = string.Empty;
            txtOrderNum.Caption = string.Empty;
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //全选
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            AllCheckOrClear(true);
        }

        /// <summary>
        /// 全选或者全清
        /// </summary>
        /// <param name="isCheck"></param>
        void AllCheckOrClear(bool isCheck)
        {
            foreach (DataGridViewRow dgvr in dgvData.Rows)
            {
                dgvr.Cells[colCheck.Name].Value = isCheck;
            }
        }
        //全清
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            AllCheckOrClear(false);
        }
        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            dgvData.EndEdit();
            ids = string.Empty;
            listRows.Clear();
            foreach (DataGridViewRow dgvr in dgvData.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
                {
                    ids += string.Format("{0},", dgvr.Cells[colID.Name].Value);
                    listRows.Add(dgvr);
                }
            }
            if (ids.Length == 0)
            {
                MessageBoxEx.Show("请选择要导入的项！");
                return;
            }
            ids = ids.TrimEnd(',');
            if (!LockDocument(ids))
            {
                return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 锁定单据
        /// </summary>
        /// <param name="ids">要锁定单据的ID</param>
        /// <returns></returns>
        protected virtual bool LockDocument(string ids)
        {
            return true;
        }


        //public object CommonC { get; set; }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
