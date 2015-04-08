using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HXCPcClient.LoginForms
{
    public partial class FormMsg : Form
    {
        public FormMsg()
        {
            InitializeComponent();
        }

        #region --关闭按钮
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_d;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_n;
        }
        #endregion

        #region --确认按钮
        private void panelYes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelYes_MouseEnter(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(23, 146, 219);
        }

        private void panelYes_MouseLeave(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(20, 129, 194);
        }
        #endregion        
    }
}