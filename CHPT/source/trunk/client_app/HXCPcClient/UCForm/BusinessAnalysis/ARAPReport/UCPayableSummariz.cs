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

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPayableSummariz : UCReport
    {
        public UCPayableSummariz()
            : base("v_payable_summariz", "应付账款汇总表")
        {
            InitializeComponent();
            colQiChu.DefaultCellStyle = styleMoney;
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

        private void UCPayableSummariz_Load(object sender, EventArgs e)
        {
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            base.listNegative = new List<string>();
            base.listNegative.Add("期初应付款");//期初应付款
            listNegative.Add("其中商业折扣");//其中商业折扣
            listNegative.Add("本期承收应付款");//本期承收应付款
            listNegative.Add("期末结存应付额");//期末结存应付额
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
            string filed = @"sup_code,sup_full_name,期初应付款,sum(本期发生) 本期增加应付款,SUM(其中商业折扣) 其中商业折扣,SUM(本期付款) 本期承收应付款,
SUM(其中现金折扣) 其中现金折扣,isnull(期初应付款,0)+sum(isnull(本期发生,0))-SUM(isnull(本期付款,0)) 期末结存应付额";
            string table = string.Format(@"(
select c.sup_code,c.sup_full_name,d.期初应付款,a.本期发生,a.其中商业折扣,a.本期付款,a.其中现金折扣,a.单据日期,c.sup_type
from v_payable_document a 
left join tb_supplier c on a.sup_id=c.sup_id
left join (
select sup_id,SUM(isnull(本期发生,0)-isnull(本期付款,0)) 期初应付款 from v_payable_document where 单据日期<{0}
group by sup_id
) d on a.sup_id=d.sup_id
where a.单据日期 between {0} and {1}
) a ", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            string groupBy = "group by sup_code,sup_full_name,期初应付款";
            string where = GetWhere();
            dt = DBHelper.GetTable("", table, filed, where, "", groupBy);
            //dt.DataTableToDate("order_date");
            dt.DataTableSum(null);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDetail();
        }

        void OpenDetail()
        {
            if (dgvReport.CurrentRow == null)
            {
                return;
            }
            string supName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupName.Name].Value);
            if (supName.Length == 0)
            {
                return;
            }
            UCPayableDetail detail = new UCPayableDetail();
            detail.supCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colSupCode.Name].Value);
            detail.supName = supName;
            detail.supType = CommonCtrl.IsNullToString(cboSup_type.SelectedValue);
            detail.startDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "应付账款明细表", "UCPayableDetail", this.Tag.ToString(), this.Name);
        }
    }
}
