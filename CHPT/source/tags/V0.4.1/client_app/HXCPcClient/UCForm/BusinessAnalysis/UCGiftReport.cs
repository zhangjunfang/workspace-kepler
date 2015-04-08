using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    public partial class UCGiftReport : UCReport
    {
        public UCGiftReport()
            : base("v_gift_report", "赠品汇总报表")
        {
            InitializeComponent();
        }

        private void UCGiftReport_Load(object sender, EventArgs e)
        {
            BindData();
        }
        //配件
        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //配件类型
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType partsType = new frmPartsType();
            if (partsType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = partsType.TypeName;
            }
        }
        //配件车型
        private void txtcVehicleModels_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels vehicleModels = new frmVehicleModels();
            if (vehicleModels.ShowDialog() == DialogResult.OK)
            {
                txtcVehicleModels.Text = vehicleModels.VMName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //绑定
        void BindData()
        {
 
        }
    }
}
