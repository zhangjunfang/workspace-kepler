using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.Chooser;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;
using HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuarantySettlement;
using HXCPcClient.UCForm.RepairBusiness.RepairBalance;
using HXCPcClient.UCForm.FinancialManagement.Receivable;
using HXCPcClient.UCForm.FinancialManagement.AccountVerification;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableChecking : UCReport
    {
        public UCReceivableChecking()
            : base("v_receivable_checking", "应收款对账单")
        {
            InitializeComponent();
        }

        private void UCReceivableChecking_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);

            listNegative = new List<string>();
            listNegative.Add("应收金额");
            listNegative.Add("期末余额");
            colQiMo.DefaultCellStyle = styleMoney;
            colYingShou.DefaultCellStyle = styleMoney;
            colJieSuan.DefaultCellStyle = styleMoney;

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

        private void txtcCust_code_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCust_code.Text = frmCust.strCustomerNo;
                txtCust_name.Caption = frmCust.strCustomerName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            string strWhere = GetWhere();
            string filed = "业务类别 单据类型,单据日期,单据编号,isnull(本期发生,0)-isnull(本期收款,0) 应收金额,c.期初应收款,b.cust_code,b.cust_name,0 期末余额,a.ID,a.已结算金额,a.备注 ";
            string table = string.Format(@" v_receivable_document a 
left join tb_customer b on a.cust_id=b.cust_id
left join (
select cust_id,sum(ISNULL(本期发生,0)-ISNULL(本期收款,0)) 期初应收款 from v_receivable_document where 单据日期<{0}
group by cust_id
) c on a.cust_id=c.cust_id", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by cust_name,单据日期");
            dt.DataTableToDate("单据日期");
            List<string> listNot = new List<string>();
            listNot.Add("期末余额");
            //按客户分组
            dt.DataTableGroup("cust_name", "单据类型", "客户名称：", "cust_code", "单据日期", "客户编码：", listNot, "期初应收款", "期初应收", "期末余额", null, "应收金额", null);
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
                case "销售开单":
                    UCSaleBillView ucSaleView = new UCSaleBillView(orderId, null);
                    base.addUserControl(ucSaleView, "销售开单-查看", "UCSaleBillView" + orderId + "", this.Tag.ToString(), this.Name);
                    break;
                case "三包结算单":
                    UCThreeGuarantySettlementView ucView = new UCThreeGuarantySettlementView();
                    ucView.SettlementId = orderId;
                    base.addUserControl(ucView, "三包结算单-查看", "UCThreeGuarantySettlementView" + orderId, this.Tag.ToString(), this.Name);
                    break;
                case "维修结算单":
                    UCRepairBalanceView ucRepairView = new UCRepairBalanceView(orderId);
                    base.addUserControl(ucRepairView, "维修结算单-查看", "UCRepairBalanceView" + orderId, this.Tag.ToString(), this.Name);
                    break;
                case "销售收款":
                    UCReceivableAdd ucPayable = new UCReceivableAdd(WindowStatus.View, orderId, null, DataSources.EnumOrderType.RECEIVABLE);
                    base.addUserControl(ucPayable, "应收账款-预览", "UCReceivableAdd" + orderId, this.Tag.ToString(), this.Name);
                    break;
                case "往来核销":
                    UCAccountVerificationAdd ucAccountVerificationView = new UCAccountVerificationAdd(WindowStatus.View, orderId, null);
                    base.addUserControl(ucAccountVerificationView, "往来核销-预览", "UCAccountVerificationAdd" + orderId, this.Tag.ToString(), this.Name);
                    break;
            }
        }
    }
}
