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
        }

        private void UCPayableDetail_Load(object sender, EventArgs e)
        {
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            base.listNegative = new List<string>();
            listNegative.Add("其中商业折扣");//其中商业折扣
            listNegative.Add("本期收款");//本期收款
            listNegative.Add("其中现金折扣");//其中现金折扣
            listNegative.Add("期末应收");//期末应收

            txtcSup_code.Text = supCode;
            txtSup_name.Caption = supName;
            cboSup_type.SelectedValue = supType;
            dicreate_time.StartDate = startDate;
            dicreate_time.EndDate = endDate;
            cboCompany.SelectedValue = company;
            cboorg_id.SelectedValue = orgID;

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
            string filed = "'采购开单' 业务类别,a.order_date,a.order_num,a.allmoney,a.whythe_discount,b.money 本期付款,f.deduction 其中现金折扣,a.sup_code,a.sup_name,d.期初应付款,e.本期增加应付款,isnull(d.期初应付款,0)+isnull(e.本期增加应付款,0)-isnull(f.本期付款,0) 期末应付,a.purchase_billing_id";
            string table = string.Format(@"tb_parts_purchase_billing a 
left join v_YingShou b on a.purchase_billing_id=b.documents_id
left join tb_customer c on a.sup_id=c.cust_id
left join (
select sup_id,SUM(allmoney) 期初应付款 from tb_parts_purchase_billing where order_date<{0}
group by sup_id
) d on a.sup_id=d.sup_id
left join (
select sup_id,SUM(allmoney) 本期增加应付款 from tb_parts_purchase_billing where order_date between {0} and {1}
group by sup_id
)e on a.sup_id=e.sup_id
left join (
select cust_id,SUM(money) 本期付款,SUM(deduction) deduction from v_YingFu_detail where order_date between {0} and {1}
group by cust_id
) f on a.sup_id=f.cust_id", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.StartDate).Date), Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dicreate_time.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            dt = DBHelper.GetTable("", table, filed, strWhere, "", "order by a.sup_name");
            dt.DataTableToDate("order_date");
            List<string> listNot = new List<string>();
            listNot.Add("期末应付");
            //按客户分组
            dt.DataTableGroup("sup_name", "业务类别", "供应商名称：", "sup_code", "order_date", "供应商编码：", listNot, "期初应付款", "期初应付", "期末应付", "期末应付", "allmoney", "本期付款");
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
            UCPurchaseBillView ucPurchaseView = new UCPurchaseBillView(orderId, null);
            base.addUserControl(ucPurchaseView, "采购开单-查看", "UCPurchaseBillView" + orderId + "", this.Tag.ToString(), this.Name);
        }
    }
}
