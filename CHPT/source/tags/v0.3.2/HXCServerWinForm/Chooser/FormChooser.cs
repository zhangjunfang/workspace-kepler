using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStationClient.ComponentUI;
using System.Windows.Forms;

namespace HXCServerWinForm.Chooser
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
    }
}
