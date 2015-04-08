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
    public partial class UCRepairLaborHour : UCReport
    {
        public UCRepairLaborHour()
            : base("v_repair_labor_hour_report", "维修人员工时统计")
        {
            InitializeComponent();
        }
    }
}
