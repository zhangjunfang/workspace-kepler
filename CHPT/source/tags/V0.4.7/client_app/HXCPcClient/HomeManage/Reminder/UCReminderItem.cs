using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HXCPcClient.HomeManage
{
    public partial class UCReminderItem : UserControl
    {
        #region --委托事件
        public delegate void ClickedComplate(DataTable dt);
        public ClickedComplate ClickedComplated;
        #endregion

        #region --成员变量
        private Color defaultColor = Color.FromArgb(115, 115, 115);
        private Color selectColor = Color.FromArgb(83, 186, 37);
        private DataTable dt;
        private bool loadFlag = true;
        #endregion

        #region --构造函数
        public UCReminderItem(string title)
        {
            InitializeComponent();            

            this.labelContent.Text = title;
            this.labelCount.Visible = false;
        }
        #endregion

        public void SetItems(DataTable _dt)
        {
            if (this.labelContent.Width > this.Width + 40)
            {
                this.labelContent.Text = this.labelContent.Text.Substring(0, 15) + "…";
            }

            dt = _dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                this.labelCount.Visible = true;
                this.labelCount.Text = dt.Rows.Count + "条";
                this.labelCount.Location = new Point(
                    this.labelContent.Location.X + this.labelContent.Width + 5, this.labelCount.Location.Y);                
            }
            else
            {
                this.Enabled = false;
            }
        }

        #region --事件
        //单击事件
        private void UCReminderItem_Click(object sender, EventArgs e)
        {
            if (this.dt != null && this.dt.Rows.Count > 0)
            {
                if(this.ClickedComplated!=null)
                {
                    this.ClickedComplated(dt);
                }                
            }          
        }
        private void UCReminderItem_MouseEnter(object sender, EventArgs e)
        {            
            this.labelContent.Font = new Font(this.labelContent.Font.FontFamily, this.labelContent.Font.Size, FontStyle.Bold);
            this.labelDot.ForeColor = this.selectColor;
            this.labelContent.ForeColor = this.selectColor;
            this.labelCount.ForeColor = this.selectColor;           
        }

        private void UCReminderItem_MouseLeave(object sender, EventArgs e)
        {
            this.labelContent.Font = new Font(this.labelContent.Font.FontFamily, this.labelContent.Font.Size, FontStyle.Regular);
            this.labelDot.ForeColor = this.defaultColor;
            this.labelContent.ForeColor = this.defaultColor;
            this.labelCount.ForeColor = Color.Red;            
        }
        #endregion

        #region --绘制下划线
        private void UCReminderItem_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                Pen pen = new Pen(new SolidBrush(Color.FromArgb(194, 216, 244)), 1);
                pen.DashPattern = new float[] { 5, 4 };
                g.DrawLine(pen, new Point(6, this.Height - 1), new Point(this.Width - 6, this.Height - 1));
            }
        }
        #endregion

        #region --窗体初始化
        private void UCReminderItem_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                this.loadFlag = false;
                foreach (Control trl in this.Controls)
                {
                    trl.MouseEnter -= new EventHandler(this.UCReminderItem_MouseEnter);
                    trl.MouseEnter += new EventHandler(this.UCReminderItem_MouseEnter);

                    trl.MouseLeave -= new EventHandler(this.UCReminderItem_MouseLeave);
                    trl.MouseLeave += new EventHandler(this.UCReminderItem_MouseLeave);
                }
            }
        }
        #endregion        
    }
}
