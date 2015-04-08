using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    public partial class UCDayToDayAccountReport : UCReport
    {
        public UCDayToDayAccountReport()
            : base("v_day_to_day_account_report","流水账报表")
        {
            InitializeComponent();
        }

        private void UCDayToDayAccountReport_Load(object sender, EventArgs e)
        {

        }
    }
}
