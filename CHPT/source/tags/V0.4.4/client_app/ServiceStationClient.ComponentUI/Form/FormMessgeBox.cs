using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    /// <summary>
    /// 数据加载动画窗体
    /// 创建者：杨天帅
    /// 创建时间：2014-12-4
    /// </summary>
    public partial class FormMessgeBox : Form
    {
        #region --成员变量
        private static FormMessgeBox formMsg;
        private static FormBG formBG;
        #endregion

        #region --构造函数
        public FormMessgeBox(string msg,Color color)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            this.labelMsg.Text = msg;
            this.labelMsg.ForeColor = color;
            this.panelTop.BackColor = color;
            this.panelYes.BackColor = color;
        }
        #endregion

        #region --显示内容
        /// <summary> 显示颜色 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        public static void ShowMsg(Control ctl, string msg,Color color)
        {
            if (formMsg != null)
            {
                formMsg.Close();
                formMsg = null;
            }
            if (formBG == null || formBG.IsDisposed)
            {
                formBG = new FormBG();
            }                       
            //formBG.BringToFront();

            formMsg = new FormMessgeBox(msg,color);            
            formBG.Show();
            //formBG.BringToFront();
            formBG.Size = ctl.Size;
            formBG.Location = ctl.PointToScreen(new Point(0, 0));
            formMsg.Show();
            formMsg.Location = new Point(formBG.Location.X + (formBG.Width - formMsg.Width) / 2,
                formBG.Location.Y + (formBG.Height - formMsg.Height) / 2);            
        }
        #endregion        

        #region --关闭按钮
        private void pbClose_Click(object sender, EventArgs e)
        {
            if (formMsg != null)
            {
                formMsg.Close();
            }
            if (formBG != null)
            {
                formBG.Close();
            }
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
            if (formMsg != null)
            {
                formMsg.Close();
            }
            if (formBG != null)
            {
                formBG.Close();
            }
            this.Close();       
        }       
        #endregion

        #region --取消按钮
        private void panelCancel_Click(object sender, EventArgs e)
        {
            if (formMsg != null)
            {
                formMsg.Close();
            }
            if (formBG != null)
            {
                formBG.Close();
            }
            this.Close();
        }       
        #endregion

        #region --快速登录
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                panelYes_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                if (formMsg != null)
                {
                    formMsg.Close();
                }
                if (formBG != null)
                {
                    formBG.Close();
                }
                this.Close();
                return true;
            }      
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
