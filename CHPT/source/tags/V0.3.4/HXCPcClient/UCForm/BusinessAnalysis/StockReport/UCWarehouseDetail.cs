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
    public partial class UCWarehouseDetail : UCReport
    {
        public UCWarehouseDetail()
            : base("v_warehouse_detail", "仓库开单明细表")
        {
            InitializeComponent();
        }
    }
}
