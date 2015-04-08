using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling;
using HXCPcClient.UCForm.FinancialManagement.Receivable;
using HXCPcClient.UCForm.FinancialManagement.AccountVerification;
using SYSModel;
using Utility.Common;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPayableChecking : UCReport
    {
        public UCPayableChecking()
            : base("v_payable_checking", "应付款对账单")
        {
            InitializeComponent();
        }

        private void UCPayableChecking_Load(object sender, EventArgs e)
        {
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            listNegative = new List<string>();
            listNegative.Add("应付金额");
            listNegative.Add("期末余额");
            colJieSuan.DefaultCellStyle = styleMoney;
            colYingFu.DefaultCellStyle = styleMoney;
            colQiMo.DefaultCellStyle = styleMoney;

            BindData();
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
            string filed = "业务类别 单据类型,单据日期,单据编号,isnull(本期发生,0)-isnull(本期付款,0) 应付金额,b.sup_code,b.sup_full_name,c.期初应付款,0 期末余额,a.ID,a.已结算金额,a.备注";
            string table = string.Format(@"v_payable_document a 
left join tb_supplier b on a.sup_id=b.sup_id
left join (
select sup_id,SUM(isnull(本期发生,0)-isnull(本期付款,0)) 期初应付款 from v_payable_document where 单据日期<{0}
group by sup_id
) c on a.sup_id=c.sup_id", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by b.sup_full_name,单据日期");
            dt.DataTableToDate("单据日期");
            List<string> listNot = new List<string>();
            listNot.Add("期末余额");
            //按客户分组
            dt.DataTableGroup("sup_full_name", "单据类型", "供应商名称：", "sup_code", "单据日期", "供应商编码：", listNot, "期初应付款", "期初应付", "期末余额", null, "应付金额", null);
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
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[colID.Name].Value);
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
