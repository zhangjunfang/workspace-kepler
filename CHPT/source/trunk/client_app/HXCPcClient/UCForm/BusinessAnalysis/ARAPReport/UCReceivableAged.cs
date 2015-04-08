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
    public partial class UCReceivableAged : UCReport
    {
        public UCReceivableAged()
            : base("v_receivable_aged", "应收账款账龄明细分析")
        {
            InitializeComponent();
            Quick qCust = new Quick();
            qCust.BindCustomer(txtcCust_code);
            txtcCust_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcCust_code_DataBacked);
        }

        void txtcCust_code_DataBacked(DataRow dr)
        {
            txtCust_name.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
            txtcCust_code.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
        }

        private void UCReceivableAged_Load(object sender, EventArgs e)
        {
            //客户类别
            CommonFuncCall.BindComBoxDataSource(cboCust_type, "sys_customer_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //是否会员
            DataSources.BindComBoxDataEnum(cboIsMember, typeof(DataSources.EnumYesNo), true);
            listNegative = new List<string>();
            listNegative.Add("单据金额");
            listNegative.Add("未结算金额");
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
            strWhere += " and a.allmoney>isnull(a.balance_money,0)";//只查未完全结算的
            string files = @"a.order_type,a.order_num,a.order_date,a.allmoney,isnull(a.balance_money,0) balance_money,a.allmoney-isnull(a.balance_money,0) waitmoney,a.cust_code,a.cust_name,a.sale_billing_id ";
            string table = @"tb_parts_sale_billing a 
                            left join tb_customer b on a.cust_id=b.cust_id";
            dt = DBHelper.GetTable("", table, files, strWhere, "", "order by a.cust_name");
            dt.DataTableToDate("order_date");
            if (dt != null)
            {
                dt.Columns.Add("aged");//账龄
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["order_type"] != null && dr["order_type"] != DBNull.Value)
                    {
                        dr["order_type"] = DataSources.GetDescription(typeof(DataSources.EnumSaleOrderType), dr["order_type"]);
                    }
                    if (dr["order_date"] != null && dr["order_date"] != DBNull.Value)
                    {
                        TimeSpan ts = DateTime.Now - Convert.ToDateTime(dr["order_date"]);
                        dr["aged"] = ts.Days + 1;
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
            string orderId = Convert.ToString(this.dgvReport.CurrentRow.Cells[colsale_billing_id.Name].Value);
            if (orderId.Length == 0)
            {
                return;
            }
            UCSaleBillView saleBillView = new UCSaleBillView(orderId, null);
            base.addUserControl(saleBillView, "销售开单-查看", "UCSaleBillView" + orderId + "", this.Tag.ToString(), this.Name);
        }
    }
}
