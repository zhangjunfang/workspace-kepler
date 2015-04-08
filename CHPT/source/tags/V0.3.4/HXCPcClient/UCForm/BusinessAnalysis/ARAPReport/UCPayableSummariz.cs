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
            string filed = @"sup_code,sup_name,期初应付款,sum(allmoney) 本期增加应付款,SUM(whythe_discount) 其中商业折扣,SUM(money) 本期承收应付款,
SUM(deduction) 其中现金折扣,期初应付款+sum(allmoney)-SUM(money) 期末结存应付额";
            string table = string.Format(@"(
select a.sup_code,a.sup_name,d.期初应付款,a.allmoney,a.whythe_discount,e.money,e.deduction,a.order_date,c.sup_type
from tb_parts_purchase_billing a 
left join v_YingShou b on a.purchase_billing_id=b.documents_id
left join tb_supplier c on a.sup_id=c.sup_id
left join (
select sup_id,SUM(allmoney) 期初应付款 from tb_parts_purchase_billing where order_date<0
group by sup_id
) d on a.sup_id=d.sup_id
left join v_YingFu_detail e on a.sup_id=e.cust_id
where a.order_date between {0} and {1}
) a ", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            string groupBy = "group by sup_code,sup_name,期初应付款";
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
