using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.BusinessAnalysis.StockReport
{
    public partial class UCAlarmDetail : UCReport
    {
        public UCAlarmDetail()
            : base("v_alarm_detail", "库存报警明细表")
        {
            InitializeComponent();
        }
    }
}
