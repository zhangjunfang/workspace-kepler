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
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCSaleGathering : UCReport
    {
        public UCSaleGathering()
            : base("v_sale_gathering", "销售开单收款一览表")
        {
            InitializeComponent();
        }

        private void UCSaleGathering_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            listNegative = new List<string>();
            listNegative.Add("cust_arrears");//客户欠款
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
            string files = @"a.order_type,a.order_num,a.order_date,a.allmoney,isnull(c.money,0),a.allmoney-isnull(c.money,0) waitmoney,a.receipt_type_name,a.receivables_date,
a.current_collect,a.balance_way_name,a.balance_account_name,a.trans_way_name,a.whythe_discount,a.cust_arrears,
a.receivables_limit,delivery_address,a.org_name,a.handle_name,a.operator_name,a.remark,a.cust_code,a.cust_name,a.sale_billing_id";
            string table = @"tb_parts_sale_billing a 
                             left join tb_customer b on a.cust_id=b.cust_id
                             left join v_YingShou c on a.sale_billing_id=c.documents_id";
            dt = DBHelper.GetTable("", table, files, strWhere, "", "order by cust_name");
            List<string> listDate = new List<string>();
            listDate.Add("order_date");
            listDate.Add("receivables_date");
            dt.DataTableToDate(listDate);
            //List<string> listNot = new List<string>();
            //listNot.Add("create_time");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["order_type"] != null && dr["order_type"] != DBNull.Value)
                    {
                        dr["order_type"] = DataSources.GetDescription(typeof(DataSources.EnumSaleOrderType), dr["order_type"]);
                    }
                }
            }
            //按客户分组
            dt.DataTableGroup("cust_name", "order_type", "客户名称：", "cust_code", "order_num", "客户编码：", null);
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
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[col_sale_billing_id.Name].Value);
            if (orderId.Length == 0)
            {
                return;
            }
            UCSaleBillView saleBillView = new UCSaleBillView(orderId, null);
            base.addUserControl(saleBillView, "销售开单-查看", "UCSaleBillView" + orderId + "", this.Tag.ToString(), this.Name);
        }
    }
}
