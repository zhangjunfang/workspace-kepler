using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.User
{
    public partial class UCUserSetPWD : UserControl
    {
        public UCUserSetPWD()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //   禁止擦除背景. 
            SetStyle(ControlStyles.DoubleBuffer, true);   
        }

        private void UCUserSetPWD_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Text = "Button" + i.ToString();
                this.flowLayoutPanel1.Controls.Add(btn);
            }
        }
    }
}
