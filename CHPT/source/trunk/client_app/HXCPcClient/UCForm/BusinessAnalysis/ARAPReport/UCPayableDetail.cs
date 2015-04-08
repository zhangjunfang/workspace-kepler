using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.FinancialManagement.Receivable;
using SYSModel;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling;
using HXCPcClient.UCForm.FinancialManagement.AccountVerification;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPayableDetail : UCReport
    {
        public string supCode = string.Empty;
        public string supName = string.Empty;
        public string supType = string.Empty;
        public string startDate = DateTime.Now.ToString("yyyy-MM-01");
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string company = string.Empty;
        public string orgID = string.Empty;
        public UCPayableDetail()
            : base("v_payable_detail", "应付账款明细表")
        {
            InitializeComponent();
            colBenQi.DefaultCellStyle = styleMoney;
            colShangYe.DefaultCellStyle = styleMoney;
            colYingFu.DefaultCellStyle = styleMoney;
            colXianJin.DefaultCellStyle = styleMoney;
            colQiMo.DefaultCellStyle = styleMoney;
            Quick qSupp = new Quick();
            qSupp.BindSupplier(txtcSup_code);
            txtcSup_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcSup_code_DataBacked);
        }

        void txtcSup_code_DataBacked(DataRow dr)
        {
            txtSup_name.Caption = CommonCtrl.IsNullToString(dr["sup_full_name"]);
            txtcSup_code.Text = CommonCtrl.IsNullToString(dr["sup_code"]);
        }

        private void UCPayableDetail_Load(object sender, EventArgs e)
        {
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            base.listNegative = new List<string>();
            listNegative.Add("其中商业折扣");//其中商业折扣
            listNegative.Add("本期付款");//本期付款
            listNegative.Add("其中现金折扣");//其中现金折扣
            listNegative.Add("期末应付");//期末应付

            txtcSup_code.Text = supCode;
            txtSup_name.Caption = supName;
            cboSup_type.SelectedValue = supType;
            dicreate_time.StartDate = startDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = company;
            cboorg_id.SelectedValue = orgID;

            BindData();

            //双击查看明细,要手动调用权限
            if (this.Name != "CL_BusinessAnalysis_ARAP_PayableDet")
            {
                base.RoleButtonStstus("CL_BusinessAnalysis_ARAP_PayableDet");
            }
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }

        private void txtcSup_code_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier frmSup = new frmSupplier();
            if (frmSup.ShowDialog() == DialogResult.OK)
            {
                txtcSup_code.Text = frmSup.supperCode;
                txtSup_name.Caption = frmSup.supperName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            string filed = "a.*,c.sup_code,c.sup_full_name,d.期初应付款,0.0 期末应付,a.备注";
            string table = string.Format(@"v_payable_document a 
left join tb_supplier c on a.sup_id=c.sup_id
left join (
select sup_id,SUM(isnull(本期发生,0)-isnull(本期付款,0)) 期初应付款 from v_payable_document where 单据日期<{0}
group by sup_id
) d on a.sup_id=d.sup_id", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by c.sup_full_name,单据日期");
            dt.DataTableToDate("单据日期");
            List<string> listNot = new List<string>();
            listNot.Add("期末应付");
            //按客户分组
            dt.DataTableGroup("sup_full_name", "业务类别", "供应商名称：", "sup_code", "单据日期", "供应商编码：", listNot, "期初应付款", "期初应付", "期末应付", "期末应付", "本期发生", "本期付款");
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }

        void OpenDocument()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[colpurchase_billing_id.Name].Value);
            if (orderId.Length == 0)
            {
                return;
            }
            string type = dgvReport.CurrentRow.Cells[colType.Name].Value.ToString();
            switch (type)
            {
                case "采购开单":
                    UCPurchaseBillView ucPurchaseView = new UCPurchaseBillView(orderId, null);
                    base.addUserControl(ucPurchaseView, "采购开单-查看", "UCPurchaseBillView" + orderId + "", this.Tag.ToString(), this.Name);
                    break;
                case "采购收款":
                    UCReceivableAdd ucPayable = new UCReceivableAdd(WindowStatus.View, orderId, null, DataSources.EnumOrderType.PAYMENT);
                    base.addUserControl(ucPayable, "应付账款-预览", "UCReceivableAdd" + orderId, this.Tag.ToString(), this.Name);
                    break;
                case "往来核销":
                    UCAccountVerificationAdd ucAccountVerificationView = new UCAccountVerificationAdd(WindowStatus.View, orderId, null);
                    base.addUserControl(ucAccountVerificationView, "往来核销-预览", "UCAccountVerificationAdd" + orderId, this.Tag.ToString(), this.Name);
                    break;
            }
        }
    }
}
