using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace testWinForm
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: 这行代码将数据加载到表“hxcDataSet.tb_bill_receivable”中。您可以根据需要移动或删除它。
                this.tb_bill_receivableTableAdapter.Fill(this.hxcDataSet.tb_bill_receivable);

            }
            catch (Exception s)
            {
 
            }
            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.Refresh();
            //MemoryStream ms = new MemoryStream();
            //XmlSerializer serializer=new XmlSerializer(typeof())
            //serializer.Serialize(ms, CreateReport());
            //this.reportViewer1.Reset();
            //this.reportViewer1.LocalReport.LoadReportDefinition(ms);
            //this.reportViewer1.LocalReport.
            //this.reportViewer1.LocalReport.DataSources.Add(tb_bill_receivableBindingSource);
            //this.reportViewer1.RefreshReport();
        }
    }
}
