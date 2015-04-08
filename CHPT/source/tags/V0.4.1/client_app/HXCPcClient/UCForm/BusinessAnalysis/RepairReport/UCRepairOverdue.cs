using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairOverdue : UCReport
    {
        public UCRepairOverdue()
            : base("v_repair_overdue_report", "超期无服务车辆统计")
        {
            InitializeComponent();
        }

        private void UCRepairOverdue_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindCompany(cboCompany, "全部");
        }

        private void txtvehicle_models_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmVM = new frmVehicleModels();
            if (frmVM.ShowDialog() == DialogResult.OK)
            {
                txtvehicle_models.Text = frmVM.VMName;
            }
        }

        private void txtclicense_plate_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVG = new frmVehicleGrade();
            if (frmVG.ShowDialog() == DialogResult.OK)
            {
                txtclicense_plate.Text = frmVG.strLicensePlate;
            }
        }

    }
}
