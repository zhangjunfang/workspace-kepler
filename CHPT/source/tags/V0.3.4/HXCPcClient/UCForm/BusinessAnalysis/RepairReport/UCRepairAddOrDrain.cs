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
    public partial class UCRepairAddOrDrain : UCReport
    {
        public UCRepairAddOrDrain()
            : base("v_repair_add_or_drain_report", "新增与流失客户统计")
        {
            InitializeComponent();
        }

    }
}
