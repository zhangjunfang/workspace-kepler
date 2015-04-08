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
    public partial class UCActualDetail : UCReport
    {
        public UCActualDetail()
            : base("v_actual_detail", "实际库存明细表")
        {
            InitializeComponent();
        }
    }
}
