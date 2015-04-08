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
            //labelMsg.Text = string.Empty;
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
            formLoading.Size = ctl.Size;
            AdjustLoacation(ctl);           
        }

        /// <summary>
        /// 动画加载
        /// </summary>
        /// <param name="ctl">阴影覆盖控件</param>
        /// <param name="size"></param>
        public static void StartLoading(Control ctl,Size size)
        {
            if (formLoading != null)
            {
                EndLoading();
            }
            formLoading = new FormLoading();
            uiHandler -= new UiHandler(ShowText);
            uiHandler += new UiHandler(ShowText);
            formLoading.Show();
            if (size.Width > ctl.Width)
            {
                size = new Size(ctl.Width, size.Height);
            }
            if (size.Height > ctl.Height)
            {
                size = new Size(size.Width, ctl.Height);
            }
            formLoading.Size = size;
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
                Point startPoint = new Point((ctl.Width - formLoading.Width) / 2, (ctl.Height - formLoading.Height) / 2);
                formLoading.Location = ctl.PointToScreen(startPoint);
                formLoading.pbLoading.Location = new Point((formLoading.Width - formLoading.pbLoading.Width) / 2
                    , (formLoading.Height - formLoading.pbLoading.Height) / 2 - 10);
                formLoading.labelMsg.Location = new Point(
                    (formLoading.Width - formLoading.labelMsg.Width) / 2,
                    (formLoading.pbLoading.Location.Y + formLoading.pbLoading.Height + 15));
            }
        }      

        private static void ShowText(string msg)
        {
            if (formLoading != null)
            {
                formLoading.labelMsg.Text = msg;
                formLoading.labelMsg.Location = new Point(
                    (formLoading.Width - formLoading.labelMsg.Width) / 2,
                    (formLoading.pbLoading.Location.Y + formLoading.pbLoading.Height + 15));
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
