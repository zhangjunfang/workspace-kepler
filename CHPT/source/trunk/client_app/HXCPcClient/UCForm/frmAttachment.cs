using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm
{
    public partial class frmAttachment : FormEx
    {
        public frmAttachment()
        {
            InitializeComponent();
        }

        private void frmAttachment_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
