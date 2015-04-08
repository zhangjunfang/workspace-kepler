using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.BusinessAnalysis.RepairReport
{
    public partial class UCRepairVehicleNumber : UCReport
    {
        public UCRepairVehicleNumber()
            : base("v_repair_vehicle_number_report", "维修车辆台次统计")
        {
            InitializeComponent();
        }
    }
}
