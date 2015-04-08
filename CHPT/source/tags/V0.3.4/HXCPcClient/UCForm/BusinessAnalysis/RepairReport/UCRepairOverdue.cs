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
    public partial class UCRepairOverdue : UCReport
    {
        public UCRepairOverdue()
            : base("v_repair_overdue_report", "超期无服务车辆统计")
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
