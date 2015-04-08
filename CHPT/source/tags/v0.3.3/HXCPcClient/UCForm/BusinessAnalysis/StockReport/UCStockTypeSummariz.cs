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
    public partial class UCStockTypeSummariz : UCReport
    {
        public UCStockTypeSummariz()
            : base("v_stock_type_summariz", "库存分类汇总表")
        {
            InitializeComponent();
        }
    }
}
