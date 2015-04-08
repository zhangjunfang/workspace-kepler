using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairAddOrDrain : UCReport
    {
        public UCRepairAddOrDrain()
            : base("v_repair_add_or_drain_report", "新增与流失客户统计")
        {
            InitializeComponent();

            Quick quick = new Quick();
            quick.BindCustomer(txtcCustCode);
            txtcCustCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcCustCode_DataBacked);
        }

        void txtcCustCode_DataBacked(DataRow dr)
        {
            txtCustName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
            txtcCustCode.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
        }
        //客户选择
        private void txtcCustCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCust = new frmCustomerInfo();
            if (frmCust.ShowDialog() == DialogResult.OK)
            {
                txtcCustCode.Text = frmCust.strCustomerNo;
                txtCustName.Caption = frmCust.strCustomerName;
            }
        }
        //车牌号选择
        private void txtlicense_plate_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicleGrad = new frmVehicleGrade();
            if (frmVehicleGrad.ShowDialog() == DialogResult.OK)
            {
                txtlicense_plate.Text = frmVehicleGrad.strLicensePlate;
            }
        }

        private void UCRepairAddOrDrain_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
        }

    }
}
