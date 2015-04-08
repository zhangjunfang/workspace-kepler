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
using Utility.Common;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCReceivableSummariz : UCReport
    {
        public UCReceivableSummariz()
            : base("v_receivable_summariz", "应收账款汇总表")
        {
            InitializeComponent();
            colBenQi.DefaultCellStyle = styleMoney;
            colQiChu.DefaultCellStyle = styleMoney;
            colShangYe.DefaultCellStyle = styleMoney;
            colYingShou.DefaultCellStyle = styleMoney;
            colXianJin.DefaultCellStyle = styleMoney;
            colQiMo.DefaultCellStyle = styleMoney;
            colShouRu.DefaultCellStyle = styleMoney;
            colMaoLi.DefaultCellStyle = styleMoney;
            colLiLv.DefaultCellStyle = styleMoney;
            Quick qCust = new Quick();
            qCust.BindCustomer(txtcCust_code);
            txtcCust_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcCust_code_DataBacked);
        }

        void txtcCust_code_DataBacked(DataRow dr)
        {
            txtCust_name.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
            txtcCust_code.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
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
            string filed = @"cust_code,cust_name,期初应收款,sum(本期发生) 本期增加应收款,SUM(其中商业折扣) 其中商业折扣,SUM(本期收款) 本期承收应收款,
SUM(其中现金折扣) 其中现金折扣,isnull(期初应收款,0)+sum(isnull(本期发生,0))-SUM(isnull(本期收款,0)) 期末结存应收额";
            string table = string.Format(@"(
select c.cust_code,c.cust_name,d.期初应收款,a.本期发生,a.其中商业折扣,a.本期收款,a.其中现金折扣,c.cust_type,c.is_member
from v_receivable_document a 
left join tb_customer c on a.cust_id=c.cust_id
left join (
select cust_id,SUM(isnull(本期发生,0)-isnull(本期收款,0)) 期初应收款 from v_receivable_document where 单据日期<{0}
group by cust_id
) d on a.cust_id=d.cust_id
where 单据日期 between {0} and {1}
) a", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            string groupBy = "group by cust_code,cust_name,期初应收款";
            string where = GetWhere();
            dt = DBHelper.GetTable("获取应收账款汇总表", table, filed, where, "", groupBy);
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
