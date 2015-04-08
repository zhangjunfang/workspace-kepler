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
    public partial class UCPaperSummariz : UCReport
    {
        public UCPaperSummariz()
            : base("v_paper_summariz", "账面库存汇总表")
        {
            InitializeComponent();
        }
    }
}
