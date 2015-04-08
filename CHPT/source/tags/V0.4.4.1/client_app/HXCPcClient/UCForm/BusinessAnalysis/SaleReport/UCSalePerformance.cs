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
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSalePerformance : UCReport
    {
        public UCSalePerformance()
            : base("v_sale_performance_report", "销售业绩报表")
        {
            InitializeComponent();

            colBackMoney.DefaultCellStyle = styleMoney;
            colAmount.DefaultCellStyle = styleMoney;
            colReturnedMoney.DefaultCellStyle = styleMoney;
            colActual.DefaultCellStyle = styleMoney;
            colRate.DefaultCellStyle = styleMoney;
        }
        //窗体加载
        private void UCSalePerformance_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            base.listNegative = new List<string>();
            base.listNegative.Add("退货金额");
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
    }
}
