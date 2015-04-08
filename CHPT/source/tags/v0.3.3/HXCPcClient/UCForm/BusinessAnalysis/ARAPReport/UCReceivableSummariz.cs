using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableSummariz : UCReport
    {
        public UCReceivableSummariz()
            : base("v_receivable_summariz", "应收账款汇总表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCReceivableSummariz_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            base.listNegative = new List<string>();
            base.listNegative.Add("期初应收款");//期初应收款
            listNegative.Add("其中商业折扣");//其中商业折扣
            listNegative.Add("本期承收应收款");//本期承收应收款
            listNegative.Add("期末结存应收额");//期末结存应收额
            listNegative.Add("本期销售收入");//本期销售收入
            BindData();
        }

        //客户选择
        private void txtcCust_code_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCust_code.Text = frmCust.strCustomerNo;
                txtCust_name.Caption = frmCust.strCustomerName;
            }
        }

        //选择公司，绑定公司所有部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }

        void BindData()
        {
            string filed = @"客户编码,客户名称,sum(销售收入) 销售收入,sum(退货金额) 退货金额,sum(销售收入)+sum(退货金额) 销售净额,
                            sum(回款金额) 回款金额,sum(实际回款) 实际回款,case when sum(销售收入)+sum(退货金额)<>0 then sum(回款金额)/(sum(销售收入)+sum(退货金额)) else 0 end 回款比率";
            string groupBy = "group by 客户编码,客户名称";
            string where = GetWhere();
            dt = DBHelper.GetTable("", "v_sale_performance_report", filed, where, "", groupBy);
            //dt.DataTableToDate("order_date");
            dt.DataTableSum(null);
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
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
            string custName = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colCustName.Name].Value);
            if (custName.Length == 0)
            {
                return;
            }
            UCReceivableDetail detail = new UCReceivableDetail();
            detail.custCode = CommonCtrl.IsNullToString(dgvReport.CurrentRow.Cells[colCustCode.Name].Value);
            detail.custName = custName;
            detail.custType = CommonCtrl.IsNullToString(cboCust_type.SelectedValue);
            detail.is_member = CommonCtrl.IsNullToString(cboIsMember.SelectedValue);
            detail.startDate = dicreate_time.StartDate;
            detail.endDate = dicreate_time.EndDate;
            detail.company = CommonCtrl.IsNullToString(cboCompany.SelectedValue);
            detail.orgID = CommonCtrl.IsNullToString(cboorg_id.SelectedValue);
            base.addUserControl(detail, "应收账款明细表", "UCReceivableDetail", this.Tag.ToString(), this.Name);
        }
    }
}
