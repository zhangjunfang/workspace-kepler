using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;

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

        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDocument();
        }

        void OpenDocument()
        {
 
        }
    }
}
