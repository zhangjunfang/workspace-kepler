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
    public partial class UCRepairEfficiency : UCReport
    {
        public UCRepairEfficiency()
            : base("v_repair_efficiency_report", "技师工作效率和工位利用率")
        {
            InitializeComponent();
        }
    }
}
