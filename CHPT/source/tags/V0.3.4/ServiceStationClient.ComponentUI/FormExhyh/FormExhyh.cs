using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class FormExhyh : Form
    {
        #region --UI交互
        public delegate void UiHandler(object para);
        public UiHandler uiHandler;
        #endregion

        public System.Windows.Forms.Panel panelHead;
        public System.Windows.Forms.Panel panelHeadBtn;
        public System.Windows.Forms.Panel panelHeadLeft;
        public System.Windows.Forms.Panel panel_WinShortCutBtn;
        public System.Windows.Forms.Panel panel_WinMinBtn;
        public System.Windows.Forms.Panel panel_WinMaxBtn;
        public System.Windows.Forms.Panel panel_WinCloseBtn;
                    
        private System.ComponentModel.IContainer components = null;
        public FlowLayoutPanel fLPanel_Btn;

        private string _windowTitle = string.Empty;
        public virtual string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }
        public FormExhyh()
        {
            InitializeComponent();
            this.loadWinBtn();

            this.panelHeadLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeadLeft_MouseDown);          
            this.panel_WinCloseBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_WinCloseBtn_MouseClick);
            this.panel_WinCloseBtn.MouseEnter += new System.EventHandler(this.panel_WinCloseBtn_MouseEnter);
            this.panel_WinCloseBtn.MouseLeave += new System.EventHandler(this.panel_WinCloseBtn_MouseLeave);
            this.panel_WinMaxBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_WinMaxBtn_MouseClick);
            this.panel_WinMaxBtn.MouseEnter += new System.EventHandler(this.panel_WinMaxBtn_MouseEnter);
            this.panel_WinMaxBtn.MouseLeave += new System.EventHandler(this.panel_WinMaxBtn_MouseLeave);
            this.panel_WinMinBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_WinMinBtn_MouseClick);
            this.panel_WinMinBtn.MouseEnter += new System.EventHandler(this.panel_WinMinBtn_MouseEnter);
            this.panel_WinMinBtn.MouseLeave += new System.EventHandler(this.panel_WinMinBtn_MouseLeave);           
            this.panel_WinShortCutBtn.MouseEnter += new System.EventHandler(this.panel_WinShortCutBtn_MouseEnter);
            this.panel_WinShortCutBtn.MouseLeave += new System.EventHandler(this.panel_WinShortCutBtn_MouseLeave);
            this.SetStyles();
        }

        protected virtual void RenderWinTitle()
        {
            if (WindowTitle == string.Empty)
            {
                return;
            }
            Label lblTitle = new Label();
            lblTitle.Text = WindowTitle;
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Font titleFont = new Font("微软雅黑", 12F);
            lblTitle.Font = titleFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(3, 3);
            lblTitle.AutoSize = true;
            panelHeadLeft.Controls.Add(lblTitle);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                //不设置，在XP中不支持，Win7中支持
                Version ver = System.Environment.OSVersion.Version;
                if (ver.Major > 5)
                {
                    if (!DesignMode)
                    {
                        if (MaximizeBox) { cp.Style |= (int)WindowStyle.WS_MAXIMIZEBOX; }
                        if (MinimizeBox) { cp.Style |= (int)WindowStyle.WS_MINIMIZEBOX; }


                        cp.ExStyle |= (int)WindowStyle.WS_CLIPCHILDREN;  //防止因窗体控件太多出现闪烁                       
                    }
                    cp.ClassStyle |= (int)ClassStyle.CS_DropSHADOW;  //实现窗体边框阴影效果
                }
                return cp;
            }
        }

        public virtual void loadWinBtn()
        {
            panel_WinShortCutBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.btnDown01;//菜单
            panel_WinMinBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMinimize01;//最小化
            panel_WinCloseBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnCloseWin01;//关闭
            if (this.WindowState == FormWindowState.Maximized)
            {
                //移入
                panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize03;//最大化
            }
            else
            {
                panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize01;//最大化
            }

        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        public const int FALSE = 0x0000;
        public const int TRUE = 0x0001;

        DateTime dt0 = new DateTime();
        DateTime dt1 = new DateTime();       
        int downCount = 0;


        public virtual void panelHeadLeft_MouseDown(object sender, MouseEventArgs e)
        {            
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

            if (downCount == 0)
            {
                dt0 = System.DateTime.Now;
                downCount = downCount + 1;
            }
            else if (downCount == 1)
            {
                dt1 = System.DateTime.Now;
                TimeSpan sT = (TimeSpan)(dt1 - dt0);
                if (sT.TotalMilliseconds < 1000)
                {
                    SendMessage(this.Handle, ServiceStationClient.ComponentUI.Win32API.WM_LBUTTONDBLCLK, HTCAPTION, 0);
                    dt0 = new DateTime();
                    dt1 = new DateTime();
                    downCount = 0;
                }
                else
                {
                    dt0 = new DateTime();
                    dt1 = new DateTime();
                    downCount = 0;
                }
            }
        }        

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //SetReion();
        }     

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //SetReion();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case ServiceStationClient.ComponentUI.Win32API.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            base.WndProc(ref m);
        }

        public void InitializeComponent()
        {
            this.panelHead = new System.Windows.Forms.Panel();
            this.panelHeadLeft = new System.Windows.Forms.Panel();
            this.panelHeadBtn = new System.Windows.Forms.Panel();
            this.fLPanel_Btn = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_WinCloseBtn = new System.Windows.Forms.Panel();
            this.panel_WinMaxBtn = new System.Windows.Forms.Panel();
            this.panel_WinMinBtn = new System.Windows.Forms.Panel();
            this.panel_WinShortCutBtn = new System.Windows.Forms.Panel();
            this.panelHead.SuspendLayout();
            this.panelHeadBtn.SuspendLayout();
            this.fLPanel_Btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.Controls.Add(this.panelHeadLeft);
            this.panelHead.Controls.Add(this.panelHeadBtn);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(747, 100);
            this.panelHead.TabIndex = 0;
            // 
            // panelHeadLeft
            // 
            this.panelHeadLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeadLeft.Location = new System.Drawing.Point(0, 0);
            this.panelHeadLeft.Name = "panelHeadLeft";
            this.panelHeadLeft.Size = new System.Drawing.Size(547, 100);
            this.panelHeadLeft.TabIndex = 1;
            // 
            // panelHeadBtn
            // 
            this.panelHeadBtn.Controls.Add(this.fLPanel_Btn);
            this.panelHeadBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHeadBtn.Location = new System.Drawing.Point(547, 0);
            this.panelHeadBtn.MaximumSize = new System.Drawing.Size(200, 0);
            this.panelHeadBtn.Name = "panelHeadBtn";
            this.panelHeadBtn.Size = new System.Drawing.Size(200, 100);
            this.panelHeadBtn.TabIndex = 0;
            // 
            // fLPanel_Btn
            // 
            this.fLPanel_Btn.Controls.Add(this.panel_WinCloseBtn);
            this.fLPanel_Btn.Controls.Add(this.panel_WinMaxBtn);
            this.fLPanel_Btn.Controls.Add(this.panel_WinMinBtn);
            this.fLPanel_Btn.Controls.Add(this.panel_WinShortCutBtn);
            this.fLPanel_Btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.fLPanel_Btn.Location = new System.Drawing.Point(0, 0);
            this.fLPanel_Btn.MinimumSize = new System.Drawing.Size(0, 30);
            this.fLPanel_Btn.Name = "fLPanel_Btn";
            this.fLPanel_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fLPanel_Btn.Size = new System.Drawing.Size(200, 30);
            this.fLPanel_Btn.TabIndex = 0;
            // 
            // panel_WinCloseBtn
            // 
            this.panel_WinCloseBtn.Location = new System.Drawing.Point(166, 1);
            this.panel_WinCloseBtn.Margin = new System.Windows.Forms.Padding(2, 1, 0, 0);
            this.panel_WinCloseBtn.Name = "panel_WinCloseBtn";
            this.panel_WinCloseBtn.Size = new System.Drawing.Size(32, 19);
            this.panel_WinCloseBtn.TabIndex = 7;
            // 
            // panel_WinMaxBtn
            // 
            this.panel_WinMaxBtn.Location = new System.Drawing.Point(134, 1);
            this.panel_WinMaxBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panel_WinMaxBtn.Name = "panel_WinMaxBtn";
            this.panel_WinMaxBtn.Size = new System.Drawing.Size(32, 19);
            this.panel_WinMaxBtn.TabIndex = 6;
            // 
            // panel_WinMinBtn
            // 
            this.panel_WinMinBtn.Location = new System.Drawing.Point(102, 1);
            this.panel_WinMinBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panel_WinMinBtn.Name = "panel_WinMinBtn";
            this.panel_WinMinBtn.Size = new System.Drawing.Size(32, 19);
            this.panel_WinMinBtn.TabIndex = 5;
            // 
            // panel_WinShortCutBtn
            // 
            this.panel_WinShortCutBtn.Location = new System.Drawing.Point(70, 1);
            this.panel_WinShortCutBtn.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panel_WinShortCutBtn.Name = "panel_WinShortCutBtn";
            this.panel_WinShortCutBtn.Size = new System.Drawing.Size(32, 19);
            this.panel_WinShortCutBtn.TabIndex = 4;
            this.panel_WinShortCutBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_WinShortCutBtn_MouseClick);
            // 
            // FormExhyh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 392);
            this.Controls.Add(this.panelHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormExhyh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABaseForm";
            this.panelHead.ResumeLayout(false);
            this.panelHeadBtn.ResumeLayout(false);
            this.fLPanel_Btn.ResumeLayout(false);
            this.ResumeLayout(false);

        }       

        #region 减少闪烁
        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            
            base.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion       

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        ////Show或Hide被调用时
        //protected override void OnVisibleChanged(EventArgs e)
        //{
        //    if (Visible)
        //    {
        //        //启用窗口淡入淡出
        //        if (!DesignMode)
        //        {
        //            //淡入特效
        //            FormWin32.AnimateWindow(this.Handle, 300, FormWin32.AW_BLEND | FormWin32.AW_ACTIVATE);
        //        }
        //        base.OnVisibleChanged(e);
        //    }
        //    else
        //    {
        //        base.OnVisibleChanged(e);
        //        FormWin32.AnimateWindow(this.Handle, 300, FormWin32.AW_BLEND | FormWin32.AW_HIDE);
        //    }
        //}

        //窗体关闭时
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);           
            //在Form_FormClosing中添加代码实现窗体的淡出
            FormWin32.AnimateWindow(this.Handle, 300, FormWin32.AW_BLEND | FormWin32.AW_HIDE);
        }

        //圆角
        private void SetReion()
        {
            using (GraphicsPath path =
                    GraphicsPathHelper.CreatePath(
                    new Rectangle(Point.Empty, base.Size), 6, RoundStyle.All, true))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                this.Region = region;
            }
        }

        protected virtual void panel_WinCloseBtn_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        protected virtual void panel_WinCloseBtn_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                //移入
                panel_WinCloseBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnCloseWin02;
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ctfo_exe.frmMain.panel_btnCloseWin_MouseEnter", ex.Message);
            }
        }

        protected virtual void panel_WinCloseBtn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                panel_WinCloseBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnCloseWin01;
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ctfo_exe.frmMain.panel_btnCloseWin_MouseLeave", ex.Message);
            }
        }

        protected virtual void panel_WinMaxBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize01;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize03;
            }
        }

        protected virtual void panel_WinMaxBtn_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    //移入
                    panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize04;//最大化
                }
                else
                {
                    panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize02;//最大化
                }
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ctfo_exe.frmMain.panel_btnCloseWin_MouseEnter", ex.Message);
            }
        }

        protected virtual void panel_WinMaxBtn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                //移入
                if (this.WindowState == FormWindowState.Maximized)
                {
                    //移入
                    panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize03;//最大化
                }
                else
                {
                    panel_WinMaxBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMaximize01;//最大化
                }
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ctfo_exe.frmMain.panel_btnCloseWin_MouseEnter", ex.Message);
            }
        }

        protected virtual void panel_WinMinBtn_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ServiceStationClient.Exe.frmMain.panel_btnMinimize_Click", ex.Message);
            }
        }

        protected virtual void panel_WinMinBtn_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                //移入
                panel_WinMinBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMinimize02;
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ctfo_exe.frmMain.panel_btnMinimize_MouseEnter", ex.Message);
            }
        }

        protected virtual void panel_WinMinBtn_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                //移出
                panel_WinMinBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._btnMinimize01;
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ServiceStationClient.Exe.frmMain.panel_btnMinimize_MouseLeave", ex.Message);
            }
        }       

        protected virtual void panel_WinShortCutBtn_MouseEnter(object sender, EventArgs e)
        {
            panel_WinShortCutBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.btnDown02;//菜单
        }

        protected virtual void panel_WinShortCutBtn_MouseLeave(object sender, EventArgs e)
        {
            panel_WinShortCutBtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.btnDown01;//菜单
        }

        protected virtual void panel_WinShortCutBtn_MouseClick(object sender, MouseEventArgs e)
        {

        }       
    }
}
