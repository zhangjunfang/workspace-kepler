using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairMaterial : UCReport
    {
        public UCRepairMaterial()
            : base("v_repair_material_report", "维修用料综合统计")
        {
            InitializeComponent();
        }

        private void UCRepairMaterial_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            CommonFuncCall.BindWarehouse(cboWarehouse, "全部");
            DataSources.BindComBoxDataEnum(cboThreeWarranty, typeof(DataSources.EnumYesNo), true);
            BindData();
        }

        void BindData()
        {
            dt = DBHelper.GetTable("", "v_repair_material_report", "*", GetWhere(), "", "");
            dgvReport.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType partsType = new frmPartsType();
            if (partsType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = partsType.TypeName;
            }
        }

        private void txtPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }

        private void txtcPickingPeople_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo customerInfo = new frmCustomerInfo();
            if (customerInfo.ShowDialog() == DialogResult.OK)
            {
                txtcPickingPeople.Text = customerInfo.strCustomerName;
            }
        }

    }
}
