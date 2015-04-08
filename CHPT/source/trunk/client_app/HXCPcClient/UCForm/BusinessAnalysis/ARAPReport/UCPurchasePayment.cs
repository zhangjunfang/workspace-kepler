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
using HXCPcClient.CommonClass;
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis.ARAPReport
{
    public partial class UCPurchasePayment : UCReport
    {
        public UCPurchasePayment()
            : base("v_purchase_payment", "采购开单付款一览表")
        {
            InitializeComponent();
            Quick qSupp = new Quick();
            qSupp.BindSupplier(txtcSup_code);
            txtcSup_code.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcSup_code_DataBacked);
        }

        void txtcSup_code_DataBacked(DataRow dr)
        {
            txtSup_name.Caption = CommonCtrl.IsNullToString(dr["sup_full_name"]);
            txtcSup_code.Text = CommonCtrl.IsNullToString(dr["sup_code"]);
        }

        private void UCPurchasePayment_Load(object sender, EventArgs e)
        {
            //供应商类别
            CommonFuncCall.BindComBoxDataSource(cboSup_type, "sys_supplier_category", "全部");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            listNegative = new List<string>();
            listNegative.Add("sup_arrears");//供应商欠款
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
            string files = @"a.order_type,a.order_num,a.order_date,a.allmoney,isnull(a.balance_money,0) balance_money,a.allmoney-isnull(a.balance_money,0) waitmoney,a.receipt_type_name,a.payment_date,
a.this_payment,a.balance_way_name,a.balance_account_name,a.trans_way_name,a.whythe_discount,a.sup_arrears,
a.payment_limit,delivery_address,a.org_name,a.handle_name,a.operator_name,a.remark,a.sup_code,a.sup_name,a.purchase_billing_id";
            string table = @"tb_parts_purchase_billing a 
                             left join tb_supplier b on a.sup_id=b.sup_id";
            dt = DBHelper.GetTable("", table, files, strWhere, "", "order by a.sup_name");
            List<string> listDate = new List<string>();
            listDate.Add("order_date");
            listDate.Add("payment_date");
            dt.DataTableToDate(listDate);
            //List<string> listNot = new List<string>();
            //listNot.Add("create_time");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["order_type"] != null && dr["order_type"] != DBNull.Value)
                    {
                        dr["order_type"] = DataSources.GetDescription(typeof(DataSources.EnumPurchaseOrderType), dr["order_type"]);
                    }
                }
            }
            //按客户分组
            dt.DataTableGroup("sup_name", "order_type", "供应商名称：", "sup_code", "order_num", "供应商编码：", null);
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
            UCPurchaseBillView purchaseBillView = new UCPurchaseBillView(orderId, null);
            base.addUserControl(purchaseBillView, "采购开单-查看", "UCPurchaseBillView" + orderId + "", this.Tag.ToString(), this.Name);
        }
    }
}
