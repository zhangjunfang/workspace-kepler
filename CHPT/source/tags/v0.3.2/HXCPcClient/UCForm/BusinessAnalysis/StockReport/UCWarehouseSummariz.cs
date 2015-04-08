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
    public partial class UCWarehouseSummariz : UCReport
    {
        public UCWarehouseSummariz()
            : base("v_warehouse_summariz", "仓库开单汇总表")
        {
            InitializeComponent();
        }
    }
}
