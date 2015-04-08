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
    public partial class UCRepairClientReconciliation : UCReport
    {
        public UCRepairClientReconciliation()
            : base("v_repair_client_reconciliation_report", "维修客户对账统计")
        {
            InitializeComponent();
        }

        private void txtcVehicleNO_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtcVehicleNO.Text = frmVehicle.strLicensePlate;
            }
        }

        private void txtcCustomerCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtcCustomerCode.Text = frmCInfo.strCustomerNo;
                txtCustomerName.Caption = frmCInfo.strCustomerName;
            }
        }

        private void UCRepairClientReconciliation_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }

        void BindData()
        {
            dt = DBHelper.GetTable("DBHelper", "v_repair_client_reconciliation_report", "*", GetWhere(), "", "order by 客户编码");
            dt.DataTableToDate("结算日期");
            dgvReport.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
