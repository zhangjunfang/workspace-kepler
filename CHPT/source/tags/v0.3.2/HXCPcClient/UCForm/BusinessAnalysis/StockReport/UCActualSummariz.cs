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
    public partial class UCActualSummariz : UCReport
    {
        public UCActualSummariz()
            : base("v_actual_summariz", "实际库存汇总表")
        {
            InitializeComponent();
        }
    }
}
