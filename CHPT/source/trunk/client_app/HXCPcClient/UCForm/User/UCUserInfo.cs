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
    public partial class UCUserInfo : UserControl
    {
        public UCUserInfo()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);   //   禁止擦除背景. 
            SetStyle(ControlStyles.DoubleBuffer, true);   
        }

        private void UCUserInfo_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                Button btn = new Button();
                btn.Text = "按钮" + i.ToString();
                this.flowLayoutPanel1.Controls.Add(btn);
            }
        }
    }
}
