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
    public partial class UCRepairManage : UCReport
    {
        public UCRepairManage()
            : base("v_repair_manage_report", "维修经营情况统计")
        {
            InitializeComponent();
        }
    }
}
