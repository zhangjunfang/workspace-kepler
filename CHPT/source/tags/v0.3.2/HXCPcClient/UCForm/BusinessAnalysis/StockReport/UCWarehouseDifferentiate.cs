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
    public partial class UCWarehouseDifferentiate : UCReport
    {
        public UCWarehouseDifferentiate()
            : base("v_warehouse_differentiate", "分仓库数量金额表")
        {
            InitializeComponent();
        }
    }
}
