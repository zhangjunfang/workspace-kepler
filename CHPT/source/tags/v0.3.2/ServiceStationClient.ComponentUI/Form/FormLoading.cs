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
    public partial class FormLoading : Form
    {
        #region --成员变量
        public delegate void UiHandler(string msg);
        public static UiHandler uiHandler;

        private static FormLoading formLoading;
        private Control ctl;
        #endregion

        public FormLoading()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            InitializeComponent();
            this.Opacity = 0.7;
            labelMsg.Text = string.Empty;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 动画加载
        /// </summary>
        /// <param name="ctl"></param>
        public static void StartLoading(Control ctl)
        {
            if (formLoading != null)
            {
                EndLoading();
            }
            formLoading = new FormLoading();
            uiHandler -= new UiHandler(ShowText);
            uiHandler += new UiHandler(ShowText);           
            formLoading.Show();
            //formLoading.BringToFront();
            AdjustLoacation(ctl);           
        }       

        public static void ShowMsg(string text)
        {
            if (formLoading != null)
            {
                formLoading.Invoke(uiHandler, text);
            }
        }
                
        public static void EndLoading()
        {
            if (formLoading != null)
            {
                formLoading.Close();
                formLoading.Dispose();
                formLoading = null;
            }
        }

        private static void _SizeChanged(object sender, EventArgs args)
        {
            if (formLoading != null)
            {
                formLoading.Visible = ((Control)sender).Visible;
            }
        }

        private static void AdjustLoacation(Control ctl)
        {
            if (formLoading != null)
            {
                formLoading.Size = ctl.Size;
                formLoading.Location = ctl.PointToScreen(new Point(0, 0));
                formLoading.pbLoading.Location = new Point((formLoading.Width - formLoading.pbLoading.Width) / 2
                    , (formLoading.Height - formLoading.pbLoading.Height) / 2 - 10);
                formLoading.labelMsg.Location = new Point(
                    (formLoading.Width - formLoading.labelMsg.Width) / 2,
                    (formLoading.pbLoading.Location.X + formLoading.pbLoading.Height + 10));
            }
        }

        private static void ShowText(string msg)
        {
            if (formLoading != null)
            {
                formLoading.labelMsg.Text = msg;
                formLoading.labelMsg.Location = new Point(
                    (formLoading.Width - formLoading.labelMsg.Width) / 2,
                    (formLoading.pbLoading.Location.X + formLoading.pbLoading.Height + 10));
            }
        }

        #region --快捷切换
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && formLoading != null)
            {
                formLoading.Visible = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
