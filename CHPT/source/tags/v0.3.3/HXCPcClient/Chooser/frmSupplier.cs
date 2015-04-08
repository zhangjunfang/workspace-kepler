using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;

namespace HXCPcClient.Chooser
{
    public partial class frmSupplier : FormChooser
    {
        public string supperID = string.Empty;
        public string supperCode = string.Empty;
        public string supperName = string.Empty;
        public frmSupplier()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContact.Caption = string.Empty;
            txtOther.Caption = string.Empty;
            txtSupperCode.Caption = string.Empty;
            txtSupperName.Caption = string.Empty;
            txtTelephone.Caption = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat(" enable_flag != 0 ");
            string supperCode = txtSupperCode.Caption.Trim();//供应商编码
            if (supperCode.Length > 0)
            {
                sbWhere.AppendFormat(" and sup_code like '%{0}%'", supperCode);
            }
            string supperName = txtSupperName.Caption.Trim();//供应商名称
            if (supperName.Length > 0)
            {
                sbWhere.AppendFormat(" and sup_full_name like '%{0}%'", supperName);
            }
            string tel=txtTelephone.Caption.Trim();
            if (tel.Length> 0)
            {
                sbWhere.AppendFormat(" and sup_tel like '%{0}%'", tel);
            }
            DataTable dt = DBHelper.GetTable("", "v_supplier_user", "*", sbWhere.ToString(), "", "");
            dgvSupper.DataSource = dt;
        }

        private void dgvSupper_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSupper();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedSupper();
        }

        void SelectedSupper()
        {
            if (dgvSupper.CurrentRow == null)
            {
                return;
            }
            supperID = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells["colSupperID"].Value);
            supperCode = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperCode.Name].Value);
            supperName = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperName.Name].Value);
            if (supperID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
