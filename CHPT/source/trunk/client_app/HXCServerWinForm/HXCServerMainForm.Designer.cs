namespace HXCServerWinForm
{
    partial class HXCServerMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HXCServerMainForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.splitContainerEx_Body = new ServiceStationClient.ComponentUI.SplitContainerEx();
            this.pnl_nf = new System.Windows.Forms.Panel();
            this.labelExt1 = new System.Windows.Forms.LabelExt();
            this.panel_LeftMenu = new System.Windows.Forms.Panel();
            this.splitter_MenuNormalFunc = new System.Windows.Forms.Splitter();
            this.panel_NormalFunc = new System.Windows.Forms.Panel();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvUser_function = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.panel_Content = new System.Windows.Forms.Panel();
            this.splitter_TabContent = new System.Windows.Forms.Splitter();
            this.panel_TabBtnArea = new System.Windows.Forms.Panel();
            this.pnlxMainTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.panelMemu = new ServiceStationClient.ComponentUI.PanelEx();
            this.ibtnToRight = new ServiceStationClient.ComponentUI.ImageButton();
            this.ibtnToLeft = new ServiceStationClient.ComponentUI.ImageButton();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.panel_LoGo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbl_SysName = new System.Windows.Forms.Label();
            this.fLPanel_Buss = new System.Windows.Forms.FlowLayoutPanel();
            this.ucMenu = new HXCServerWinForm.UCMainLayOut.UCMenuBar();
            this.timer_YT = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip_WinShortCutBtn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMenuItem_UserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_PWDReSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_RegA = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_Initialize = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_Softupdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_LogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrInfo = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_sys = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextShortMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭其他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelHead.SuspendLayout();
            this.panelHeadLeft.SuspendLayout();
            this.panel_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx_Body)).BeginInit();
            this.splitContainerEx_Body.Panel1.SuspendLayout();
            this.splitContainerEx_Body.Panel2.SuspendLayout();
            this.splitContainerEx_Body.SuspendLayout();
            this.pnl_nf.SuspendLayout();
            this.panel_NormalFunc.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_TabBtnArea.SuspendLayout();
            this.pnlxMainTop.SuspendLayout();
            this.panel_Bottom.SuspendLayout();
            this.panel_LoGo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.fLPanel_Buss.SuspendLayout();
            this.contextMenuStrip_WinShortCutBtn.SuspendLayout();
            this.contextMenuStrip_sys.SuspendLayout();
            this.contextShortMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelHead.Size = new System.Drawing.Size(900, 70);
            // 
            // panelHeadBtn
            // 
            this.panelHeadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelHeadBtn.Location = new System.Drawing.Point(751, 0);
            this.panelHeadBtn.Size = new System.Drawing.Size(149, 70);
            // 
            // panelHeadLeft
            // 
            this.panelHeadLeft.Controls.Add(this.fLPanel_Buss);
            this.panelHeadLeft.Controls.Add(this.panel_LoGo);
            this.panelHeadLeft.Controls.Add(this.lblTitle);
            this.panelHeadLeft.Size = new System.Drawing.Size(751, 70);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitle.Location = new System.Drawing.Point(5, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(302, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "慧修车汽修汽配系统 -- 服务器端";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // panel_Body
            // 
            this.panel_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_Body.Controls.Add(this.splitContainerEx_Body);
            this.panel_Body.Controls.Add(this.panel_Bottom);
            this.panel_Body.Location = new System.Drawing.Point(0, 72);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(900, 429);
            this.panel_Body.TabIndex = 1;
            // 
            // splitContainerEx_Body
            // 
            this.splitContainerEx_Body.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerEx_Body.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx_Body.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerEx_Body.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.splitContainerEx_Body.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.splitContainerEx_Body.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx_Body.Name = "splitContainerEx_Body";
            // 
            // splitContainerEx_Body.Panel1
            // 
            this.splitContainerEx_Body.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerEx_Body.Panel1.Controls.Add(this.pnl_nf);
            this.splitContainerEx_Body.Panel1.Controls.Add(this.panel_LeftMenu);
            this.splitContainerEx_Body.Panel1.Controls.Add(this.splitter_MenuNormalFunc);
            this.splitContainerEx_Body.Panel1.Controls.Add(this.panel_NormalFunc);
            this.splitContainerEx_Body.Panel1MinSize = 10;
            // 
            // splitContainerEx_Body.Panel2
            // 
            this.splitContainerEx_Body.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerEx_Body.Panel2.Controls.Add(this.panel_Content);
            this.splitContainerEx_Body.Panel2.Controls.Add(this.splitter_TabContent);
            this.splitContainerEx_Body.Panel2.Controls.Add(this.panel_TabBtnArea);
            this.splitContainerEx_Body.Size = new System.Drawing.Size(900, 406);
            this.splitContainerEx_Body.SplitterDistance = 180;
            this.splitContainerEx_Body.TabIndex = 2;
            // 
            // pnl_nf
            // 
            this.pnl_nf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_nf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_nf.BackgroundImage")));
            this.pnl_nf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_nf.Controls.Add(this.labelExt1);
            this.pnl_nf.Location = new System.Drawing.Point(2, 178);
            this.pnl_nf.Name = "pnl_nf";
            this.pnl_nf.Size = new System.Drawing.Size(79, 22);
            this.pnl_nf.TabIndex = 4;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            this.labelExt1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelExt1.Location = new System.Drawing.Point(11, 3);
            this.labelExt1.Margin = new System.Windows.Forms.Padding(0);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(56, 16);
            this.labelExt1.TabIndex = 0;
            this.labelExt1.Text = "常用功能";
            // 
            // panel_LeftMenu
            // 
            this.panel_LeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.panel_LeftMenu.Name = "panel_LeftMenu";
            this.panel_LeftMenu.Size = new System.Drawing.Size(180, 173);
            this.panel_LeftMenu.TabIndex = 2;
            // 
            // splitter_MenuNormalFunc
            // 
            this.splitter_MenuNormalFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.splitter_MenuNormalFunc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter_MenuNormalFunc.Location = new System.Drawing.Point(0, 173);
            this.splitter_MenuNormalFunc.Name = "splitter_MenuNormalFunc";
            this.splitter_MenuNormalFunc.Size = new System.Drawing.Size(180, 3);
            this.splitter_MenuNormalFunc.TabIndex = 1;
            this.splitter_MenuNormalFunc.TabStop = false;
            // 
            // panel_NormalFunc
            // 
            this.panel_NormalFunc.Controls.Add(this.tabControlEx1);
            this.panel_NormalFunc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_NormalFunc.Location = new System.Drawing.Point(0, 176);
            this.panel_NormalFunc.MaximumSize = new System.Drawing.Size(0, 230);
            this.panel_NormalFunc.MinimumSize = new System.Drawing.Size(0, 230);
            this.panel_NormalFunc.Name = "panel_NormalFunc";
            this.panel_NormalFunc.Size = new System.Drawing.Size(180, 230);
            this.panel_NormalFunc.TabIndex = 0;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(0, 0);
            this.tabControlEx1.Margin = new System.Windows.Forms.Padding(1);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.Padding = new System.Drawing.Point(0, 0);
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(180, 230);
            this.tabControlEx1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvUser_function);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(172, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常用功能";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvUser_function
            // 
            this.tvUser_function.AllowDrop = true;
            this.tvUser_function.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvUser_function.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUser_function.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvUser_function.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.tvUser_function.IgnoreAutoCheck = false;
            this.tvUser_function.Indent = 5;
            this.tvUser_function.ItemHeight = 21;
            this.tvUser_function.Location = new System.Drawing.Point(0, 0);
            this.tvUser_function.Margin = new System.Windows.Forms.Padding(0);
            this.tvUser_function.Name = "tvUser_function";
            this.tvUser_function.ShowLines = false;
            this.tvUser_function.Size = new System.Drawing.Size(172, 200);
            this.tvUser_function.TabIndex = 0;
            this.tvUser_function.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUser_function_NodeMouseClick);
            // 
            // panel_Content
            // 
            this.panel_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Content.Location = new System.Drawing.Point(0, 28);
            this.panel_Content.Name = "panel_Content";
            this.panel_Content.Size = new System.Drawing.Size(716, 378);
            this.panel_Content.TabIndex = 2;
            // 
            // splitter_TabContent
            // 
            this.splitter_TabContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.splitter_TabContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter_TabContent.Location = new System.Drawing.Point(0, 25);
            this.splitter_TabContent.MinSize = 3;
            this.splitter_TabContent.Name = "splitter_TabContent";
            this.splitter_TabContent.Size = new System.Drawing.Size(716, 3);
            this.splitter_TabContent.TabIndex = 1;
            this.splitter_TabContent.TabStop = false;
            // 
            // panel_TabBtnArea
            // 
            this.panel_TabBtnArea.Controls.Add(this.pnlxMainTop);
            this.panel_TabBtnArea.Controls.Add(this.ibtnToRight);
            this.panel_TabBtnArea.Controls.Add(this.ibtnToLeft);
            this.panel_TabBtnArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_TabBtnArea.Location = new System.Drawing.Point(0, 0);
            this.panel_TabBtnArea.MaximumSize = new System.Drawing.Size(0, 25);
            this.panel_TabBtnArea.MinimumSize = new System.Drawing.Size(0, 25);
            this.panel_TabBtnArea.Name = "panel_TabBtnArea";
            this.panel_TabBtnArea.Size = new System.Drawing.Size(716, 25);
            this.panel_TabBtnArea.TabIndex = 0;
            // 
            // pnlxMainTop
            // 
            this.pnlxMainTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.pnlxMainTop.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlxMainTop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(213)))), ((int)(((byte)(243)))));
            this.pnlxMainTop.BorderWidth = 1;
            this.pnlxMainTop.Controls.Add(this.panelMemu);
            this.pnlxMainTop.Curvature = 0;
            this.pnlxMainTop.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.pnlxMainTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlxMainTop.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.pnlxMainTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pnlxMainTop.Location = new System.Drawing.Point(20, 0);
            this.pnlxMainTop.Name = "pnlxMainTop";
            this.pnlxMainTop.Size = new System.Drawing.Size(670, 25);
            this.pnlxMainTop.TabIndex = 13;
            // 
            // panelMemu
            // 
            this.panelMemu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(213)))), ((int)(((byte)(243)))));
            this.panelMemu.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelMemu.BorderColor = System.Drawing.Color.Transparent;
            this.panelMemu.BorderWidth = 0;
            this.panelMemu.Curvature = 0;
            this.panelMemu.CurveMode = ((ServiceStationClient.ComponentUI.CornerCurveMode)((((ServiceStationClient.ComponentUI.CornerCurveMode.TopLeft | ServiceStationClient.ComponentUI.CornerCurveMode.TopRight) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomLeft) 
            | ServiceStationClient.ComponentUI.CornerCurveMode.BottomRight)));
            this.panelMemu.GradientMode = ServiceStationClient.ComponentUI.LinearGradientMode.None;
            this.panelMemu.Location = new System.Drawing.Point(0, 1);
            this.panelMemu.Margin = new System.Windows.Forms.Padding(0);
            this.panelMemu.Name = "panelMemu";
            this.panelMemu.Size = new System.Drawing.Size(23, 23);
            this.panelMemu.TabIndex = 9;
            // 
            // ibtnToRight
            // 
            this.ibtnToRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ibtnToRight.BackgroundImage")));
            this.ibtnToRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnToRight.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnToRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtnToRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.ibtnToRight.ImageDown = null;
            this.ibtnToRight.ImageHover = null;
            this.ibtnToRight.ImageNormal = null;
            this.ibtnToRight.IngoreLeave = false;
            this.ibtnToRight.Location = new System.Drawing.Point(690, 0);
            this.ibtnToRight.Name = "ibtnToRight";
            this.ibtnToRight.Size = new System.Drawing.Size(26, 25);
            this.ibtnToRight.TabIndex = 12;
            this.ibtnToRight.Visible = false;
            // 
            // ibtnToLeft
            // 
            this.ibtnToLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ibtnToLeft.BackgroundImage")));
            this.ibtnToLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ibtnToLeft.ButtonStatus = ServiceStationClient.ComponentUI.ImageButton.Status.Normal;
            this.ibtnToLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ibtnToLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.ibtnToLeft.ImageDown = null;
            this.ibtnToLeft.ImageHover = null;
            this.ibtnToLeft.ImageNormal = null;
            this.ibtnToLeft.IngoreLeave = false;
            this.ibtnToLeft.Location = new System.Drawing.Point(0, 0);
            this.ibtnToLeft.Name = "ibtnToLeft";
            this.ibtnToLeft.Size = new System.Drawing.Size(20, 25);
            this.ibtnToLeft.TabIndex = 11;
            this.ibtnToLeft.Visible = false;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panel_Bottom.Controls.Add(this.lblMsg);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 406);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(900, 23);
            this.panel_Bottom.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 10F);
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(259, 4);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 14);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_LoGo
            // 
            this.panel_LoGo.BackColor = System.Drawing.Color.Transparent;
            this.panel_LoGo.Controls.Add(this.picLogo);
            this.panel_LoGo.Controls.Add(this.lbl_SysName);
            this.panel_LoGo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_LoGo.Location = new System.Drawing.Point(0, 0);
            this.panel_LoGo.Name = "panel_LoGo";
            this.panel_LoGo.Size = new System.Drawing.Size(455, 70);
            this.panel_LoGo.TabIndex = 1;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Location = new System.Drawing.Point(5, 4);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(210, 63);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picLogo_MouseDown);
            // 
            // lbl_SysName
            // 
            this.lbl_SysName.AutoSize = true;
            this.lbl_SysName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_SysName.ForeColor = System.Drawing.Color.LightBlue;
            this.lbl_SysName.Location = new System.Drawing.Point(214, 32);
            this.lbl_SysName.Name = "lbl_SysName";
            this.lbl_SysName.Size = new System.Drawing.Size(230, 24);
            this.lbl_SysName.TabIndex = 2;
            this.lbl_SysName.Text = "汽修汽配管理系统服务端";
            this.lbl_SysName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_SysName_MouseDown);
            // 
            // fLPanel_Buss
            // 
            this.fLPanel_Buss.BackColor = System.Drawing.Color.Transparent;
            this.fLPanel_Buss.Controls.Add(this.ucMenu);
            this.fLPanel_Buss.Dock = System.Windows.Forms.DockStyle.Right;
            this.fLPanel_Buss.Location = new System.Drawing.Point(451, 0);
            this.fLPanel_Buss.Name = "fLPanel_Buss";
            this.fLPanel_Buss.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fLPanel_Buss.Size = new System.Drawing.Size(300, 70);
            this.fLPanel_Buss.TabIndex = 2;
            // 
            // ucMenu
            // 
            this.ucMenu.BackColor = System.Drawing.Color.Transparent;
            this.ucMenu.Location = new System.Drawing.Point(0, 3);
            this.ucMenu.Name = "ucMenu";
            this.ucMenu.PermissionManage_Tag = "S_PermissionManage";
            this.ucMenu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucMenu.Size = new System.Drawing.Size(297, 69);
            this.ucMenu.SysMaintenance_Tag = "S_SysMaintenance";
            this.ucMenu.SysMonitor_Tag = "S_SysMonitor";
            this.ucMenu.TabIndex = 0;
            this.ucMenu.SysMonitorClick += new System.EventHandler(this.ucMenu_SysMonitorClick);
            this.ucMenu.SysMaintenanceClick += new System.EventHandler(this.ucMenu_SysMaintenanceClick);
            this.ucMenu.PermisssionManageClick += new System.EventHandler(this.ucMenu_PermissionManageClick);
            // 
            // timer_YT
            // 
            this.timer_YT.Enabled = true;
            this.timer_YT.Interval = 1000;
            this.timer_YT.Tag = "0";
            this.timer_YT.Tick += new System.EventHandler(this.timer_YT_Tick);
            // 
            // contextMenuStrip_WinShortCutBtn
            // 
            this.contextMenuStrip_WinShortCutBtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMenuItem_UserInfo,
            this.tSMenuItem_PWDReSet,
            this.tSMenuItem_RegA,
            this.tSMenuItem_Initialize,
            this.tSMenuItem_Help,
            this.tSMenuItem_Softupdate,
            this.tSMenuItem_About,
            this.tSMenuItem_LogOut});
            this.contextMenuStrip_WinShortCutBtn.Name = "contextMenuStrip_WinShortCutBtn";
            this.contextMenuStrip_WinShortCutBtn.ShowImageMargin = false;
            this.contextMenuStrip_WinShortCutBtn.Size = new System.Drawing.Size(112, 180);
            // 
            // tSMenuItem_UserInfo
            // 
            this.tSMenuItem_UserInfo.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_UserInfo.Name = "tSMenuItem_UserInfo";
            this.tSMenuItem_UserInfo.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_UserInfo.Text = "用户信息";
            this.tSMenuItem_UserInfo.Click += new System.EventHandler(this.tSMenuItem_UserInfo_Click);
            // 
            // tSMenuItem_PWDReSet
            // 
            this.tSMenuItem_PWDReSet.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_PWDReSet.Name = "tSMenuItem_PWDReSet";
            this.tSMenuItem_PWDReSet.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_PWDReSet.Text = "修改密码";
            this.tSMenuItem_PWDReSet.Click += new System.EventHandler(this.tSMenuItem_PWDReSet_Click);
            // 
            // tSMenuItem_RegA
            // 
            this.tSMenuItem_RegA.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_RegA.Name = "tSMenuItem_RegA";
            this.tSMenuItem_RegA.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_RegA.Text = "注册鉴权";
            this.tSMenuItem_RegA.Click += new System.EventHandler(this.tSMenuItem_RegA_Click);
            // 
            // tSMenuItem_Initialize
            // 
            this.tSMenuItem_Initialize.Name = "tSMenuItem_Initialize";
            this.tSMenuItem_Initialize.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_Initialize.Text = "数据初始化";
            this.tSMenuItem_Initialize.Click += new System.EventHandler(this.tSMenuItem_Initialize_Click);
            // 
            // tSMenuItem_Help
            // 
            this.tSMenuItem_Help.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_Help.Name = "tSMenuItem_Help";
            this.tSMenuItem_Help.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_Help.Text = "帮助";
            this.tSMenuItem_Help.Click += new System.EventHandler(this.tSMenuItem_Help_Click);
            // 
            // tSMenuItem_Softupdate
            // 
            this.tSMenuItem_Softupdate.Name = "tSMenuItem_Softupdate";
            this.tSMenuItem_Softupdate.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_Softupdate.Text = "软件升级";
            this.tSMenuItem_Softupdate.Click += new System.EventHandler(this.tSMenuItem_Softupdate_Click);
            // 
            // tSMenuItem_About
            // 
            this.tSMenuItem_About.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_About.Name = "tSMenuItem_About";
            this.tSMenuItem_About.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_About.Text = "关于";
            this.tSMenuItem_About.Click += new System.EventHandler(this.tSMenuItem_About_Click);
            // 
            // tSMenuItem_LogOut
            // 
            this.tSMenuItem_LogOut.BackColor = System.Drawing.Color.Transparent;
            this.tSMenuItem_LogOut.Name = "tSMenuItem_LogOut";
            this.tSMenuItem_LogOut.Size = new System.Drawing.Size(111, 22);
            this.tSMenuItem_LogOut.Text = "退出";
            this.tSMenuItem_LogOut.Click += new System.EventHandler(this.tSMenuItem_LogOut_Click);
            // 
            // tmrInfo
            // 
            this.tmrInfo.Enabled = true;
            this.tmrInfo.Interval = 1000;
            this.tmrInfo.Tick += new System.EventHandler(this.tmrInfo_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_sys;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip_sys
            // 
            this.contextMenuStrip_sys.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMenuItem_exit});
            this.contextMenuStrip_sys.Name = "contextMenuStrip_WinShortCutBtn";
            this.contextMenuStrip_sys.ShowImageMargin = false;
            this.contextMenuStrip_sys.Size = new System.Drawing.Size(76, 26);
            // 
            // tSMenuItem_exit
            // 
            this.tSMenuItem_exit.Name = "tSMenuItem_exit";
            this.tSMenuItem_exit.Size = new System.Drawing.Size(75, 22);
            this.tSMenuItem_exit.Text = "退出";
            this.tSMenuItem_exit.Click += new System.EventHandler(this.tSMenuItem_LogOut_Click);
            // 
            // contextShortMenu
            // 
            this.contextShortMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem,
            this.关闭其他ToolStripMenuItem,
            this.关闭全部ToolStripMenuItem});
            this.contextShortMenu.Name = "contextShortMenu";
            this.contextShortMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Image = global::HXCServerWinForm.Properties.Resources.close;
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 关闭其他ToolStripMenuItem
            // 
            this.关闭其他ToolStripMenuItem.Image = global::HXCServerWinForm.Properties.Resources.close_other;
            this.关闭其他ToolStripMenuItem.Name = "关闭其他ToolStripMenuItem";
            this.关闭其他ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭其他ToolStripMenuItem.Text = "关闭其他";
            this.关闭其他ToolStripMenuItem.Click += new System.EventHandler(this.关闭其他ToolStripMenuItem_Click);
            // 
            // 关闭全部ToolStripMenuItem
            // 
            this.关闭全部ToolStripMenuItem.Image = global::HXCServerWinForm.Properties.Resources.close_all;
            this.关闭全部ToolStripMenuItem.Name = "关闭全部ToolStripMenuItem";
            this.关闭全部ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭全部ToolStripMenuItem.Text = "关闭全部";
            this.关闭全部ToolStripMenuItem.Click += new System.EventHandler(this.关闭全部ToolStripMenuItem_Click);
            // 
            // HXCServerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.panel_Body);
            this.DoubleBuffered = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HXCServerMainForm";
            this.Text = "慧修车系统服务器端主窗体";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HXCServerMainForm_FormClosing);
            this.Load += new System.EventHandler(this.HXCServerMainForm_Load);
            this.SizeChanged += new System.EventHandler(this.HXCServerMainForm_SizeChanged);
            this.Controls.SetChildIndex(this.panel_Body, 0);
            this.Controls.SetChildIndex(this.panelHead, 0);
            this.panelHead.ResumeLayout(false);
            this.panelHeadLeft.ResumeLayout(false);
            this.panelHeadLeft.PerformLayout();
            this.panel_Body.ResumeLayout(false);
            this.splitContainerEx_Body.Panel1.ResumeLayout(false);
            this.splitContainerEx_Body.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx_Body)).EndInit();
            this.splitContainerEx_Body.ResumeLayout(false);
            this.pnl_nf.ResumeLayout(false);
            this.panel_NormalFunc.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel_TabBtnArea.ResumeLayout(false);
            this.pnlxMainTop.ResumeLayout(false);
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            this.panel_LoGo.ResumeLayout(false);
            this.panel_LoGo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.fLPanel_Buss.ResumeLayout(false);
            this.contextMenuStrip_WinShortCutBtn.ResumeLayout(false);
            this.contextMenuStrip_sys.ResumeLayout(false);
            this.contextShortMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel_Body;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Panel panel_LoGo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.FlowLayoutPanel fLPanel_Buss;  
        private ServiceStationClient.ComponentUI.SplitContainerEx splitContainerEx_Body;
        private System.Windows.Forms.Panel panel_LeftMenu;
        private System.Windows.Forms.Splitter splitter_MenuNormalFunc;
        private System.Windows.Forms.Panel panel_NormalFunc;
        private System.Windows.Forms.Panel panel_Content;
        private System.Windows.Forms.Splitter splitter_TabContent;
        private System.Windows.Forms.Panel panel_TabBtnArea;
        private ServiceStationClient.ComponentUI.PanelEx pnlxMainTop;
        public ServiceStationClient.ComponentUI.PanelEx panelMemu;
        private ServiceStationClient.ComponentUI.ImageButton ibtnToRight;
        private ServiceStationClient.ComponentUI.ImageButton ibtnToLeft;
        private UCMainLayOut.UCMenuBar ucMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WinShortCutBtn;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_UserInfo;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_PWDReSet;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_About;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_LogOut;
        private System.Windows.Forms.Label lbl_SysName;
        private System.Windows.Forms.Timer timer_YT;
        private System.Windows.Forms.Timer tmrInfo;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.TreeViewEx tvUser_function;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_RegA;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_sys;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_exit;
        private System.Windows.Forms.Panel pnl_nf;
        private System.Windows.Forms.LabelExt labelExt1;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_Initialize;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_Softupdate;
        private System.Windows.Forms.ContextMenuStrip contextShortMenu;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭其他ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭全部ToolStripMenuItem;
    }
}

