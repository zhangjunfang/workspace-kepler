using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStationClient.ComponentUI;
using System.Windows.Forms;

namespace HXCPcClient.Chooser
{
    public class FormChooser : FormEx
    {
    
        public FormChooser()
        {
            this.CanResize = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormChooser
            // 
            this.ClientSize = new System.Drawing.Size(684, 402);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormChooser";
            this.ResumeLayout(false);

        }
    }
}
