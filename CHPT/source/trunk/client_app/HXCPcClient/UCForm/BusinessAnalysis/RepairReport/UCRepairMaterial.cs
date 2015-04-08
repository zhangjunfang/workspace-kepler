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

            Quick quick = new Quick();
            quick.BindParts(txtPartsCode);
            txtPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtPartsCode_DataBacked);
        }

        void txtPartsCode_DataBacked(DataRow dr)
        {
            txtPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
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
            string fileds = "配件编码,配件名称,单位,sum(数量) 数量,sum(金额),规格,图号,产地,车型,品牌,是否进口,配件类别,仓库";
            string groupBy = "group by 配件编码,配件名称,单位,规格,图号,产地,车型,品牌,是否进口,配件类别,仓库";
            dt = DBHelper.GetTable("", "v_repair_material_report", fileds, GetWhere(), "", groupBy);
            dgvReport.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //配件类型选择
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType partsType = new frmPartsType();
            if (partsType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = partsType.TypeName;
            }
        }
        //配件编码选择
        private void txtPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //领料人选择
        private void txtcPickingPeople_ChooserClick(object sender, EventArgs e)
        {
            frmUsers users = new frmUsers();
            if (users.ShowDialog() == DialogResult.OK)
            {
                txtcPickingPeople.Text = users.User_Name;
            }
        }

    }
}
