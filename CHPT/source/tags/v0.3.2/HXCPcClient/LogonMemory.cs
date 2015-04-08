using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient
{
    public partial class LogonMemory : UserControl
    {
        public delegate void ItemClickedHandler(object sender);
        public event ItemClickedHandler ItemClicked;      
        private int height;
        public string UserID { get; set; }
        public string RoleID { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public Color SelectedColor { get; set; }
        public LogonMemory(string userId)
        {
            InitializeComponent();
            this.pbImg.Visible = false;
            this.labelUserId.Location = new Point(this.pbImg.Location.X, this.labelUserId.Location.Y);
            this.labelUserId.Text = userId;
            this.UserID = userId;
            foreach (Control tr in this.Controls)
            {
                tr.MouseEnter += new EventHandler(LogonMemory_MouseEnter);
                tr.MouseLeave += new EventHandler(LogonMemory_MouseLeave);
            }
        }
        public LogonMemory(Image img, string userId)
        {
            InitializeComponent();
            this.pbImg.BackgroundImage = img;
            this.labelUserId.Text = userId;
            this.UserID = userId;
            foreach (Control tr in this.Controls)
            {
                tr.MouseEnter += new EventHandler(LogonMemory_MouseEnter);
                tr.MouseLeave += new EventHandler(LogonMemory_MouseLeave);
            }
        }

        public void SetSize(Font font, int height)
        {
            this.height = height;
            this.labelUserId.Font = font;
            this.Height = height;
            this.pbImg.Location = new Point(this.pbImg.Location.X, (this.Height - this.pbImg.Height) / 2);
            this.labelUserId.Location = new Point(this.labelUserId.Location.X, (this.Height - this.labelUserId.Height) / 2 - 1);
        }

        private void LogonMemory_MouseEnter(object sender, EventArgs e)
        {            
            this.BackColor = this.SelectedColor;
            this.labelUserId.ForeColor = Color.White;
            this.Parent.Focus();            
        }

        private void LogonMemory_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.labelUserId.ForeColor = Color.Black;            
        }

        private void LogonMemory_Click(object sender, EventArgs e)
        {
            if (this.ItemClicked != null)
            {
                this.ItemClicked(this);
            }
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {

        }

        private void LogonMemory_KeyDown(object sender, KeyEventArgs e)
        {

        }                       
    }
}
