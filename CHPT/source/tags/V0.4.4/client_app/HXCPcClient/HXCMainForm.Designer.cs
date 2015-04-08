using ServiceStationClient.ComponentUI.Panel;
namespace HXCPcClient
{
    partial class HXCMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HXCMainForm));
            this.panel_LoGo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbl_SysName = new System.Windows.Forms.Label();
            this.contextMenuStrip_WinShortCutBtn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMenuItem_UserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_PWDReSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.splitContainerEx_Body = new ServiceStationClient.ComponentUI.SplitContainerEx();
            this.splitter_MenuNormalFunc = new System.Windows.Forms.Splitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pnl_nf = new System.Windows.Forms.Panel();
            this.labelExt1 = new System.Windows.Forms.LabelExt();
            this.panel_LeftMenu = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.panel_NormalFunc = new System.Windows.Forms.Panel();
            this.tabControlEx1 = new ServiceStationClient.ComponentUI.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvUser_function = new ServiceStationClient.ComponentUI.TreeViewEx();
            this.panel_Content = new ServiceStationClient.ComponentUI.Panel.PanelExtend();
            this.panel_TabBtnArea = new System.Windows.Forms.Panel();
            this.pnlxMainTop = new ServiceStationClient.ComponentUI.PanelEx();
            this.panelMemu = new ServiceStationClient.ComponentUI.PanelEx();
            this.ibtnToRight = new ServiceStationClient.ComponentUI.ImageButton();
            this.ibtnToLeft = new ServiceStationClient.ComponentUI.ImageButton();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.tlpMessage = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lvlVoiceSwitch = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lblComUserLand = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblLock = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.tmrInfo = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.contextShortMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭其他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucMenuBar1 = new HXCPcClient.UCMainLayOut.UCMenuBar();
            this.panelHead.SuspendLayout();
            this.panelHeadLeft.SuspendLayout();
            this.panel_LoGo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.contextMenuStrip_WinShortCutBtn.SuspendLayout();
            this.panel_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx_Body)).BeginInit();
            this.splitContainerEx_Body.Panel1.SuspendLayout();
            this.splitContainerEx_Body.Panel2.SuspendLayout();
            this.splitContainerEx_Body.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.pnl_nf.SuspendLayout();
            this.panel_NormalFunc.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_TabBtnArea.SuspendLayout();
            this.pnlxMainTop.SuspendLayout();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            this.panel_Bottom.SuspendLayout();
            this.tlpMessage.SuspendLayout();
            this.contextShortMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panelHead.Size = new System.Drawing.Size(1300, 70);
            this.panelHead.DoubleClick += new System.EventHandler(this.panelHeadLeft_DoubleClick);
            this.panelHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.panelHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // panelHeadBtn
            // 
            this.panelHeadBtn.Location = new System.Drawing.Point(1151, 0);
            this.panelHeadBtn.Size = new System.Drawing.Size(149, 70);
            // 
            // panelHeadLeft
            // 
            this.panelHeadLeft.Controls.Add(this.ucMenuBar1);
            this.panelHeadLeft.Controls.Add(this.panel_LoGo);
            this.panelHeadLeft.Size = new System.Drawing.Size(1151, 70);
            // 
            // panel_LoGo
            // 
            this.panel_LoGo.BackColor = System.Drawing.Color.Transparent;
            this.panel_LoGo.Controls.Add(this.picLogo);
            this.panel_LoGo.Controls.Add(this.lbl_SysName);
            this.panel_LoGo.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel_LoGo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_LoGo.Location = new System.Drawing.Point(0, 0);
            this.panel_LoGo.Name = "panel_LoGo";
            this.panel_LoGo.Size = new System.Drawing.Size(373, 70);
            this.panel_LoGo.TabIndex = 0;
            this.panel_LoGo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.panel_LoGo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.picLogo.Location = new System.Drawing.Point(6, 4);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(210, 63);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.picLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // lbl_SysName
            // 
            this.lbl_SysName.AutoSize = true;
            this.lbl_SysName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_SysName.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_SysName.ForeColor = System.Drawing.Color.LightBlue;
            this.lbl_SysName.Location = new System.Drawing.Point(213, 22);
            this.lbl_SysName.Name = "lbl_SysName";
            this.lbl_SysName.Size = new System.Drawing.Size(156, 25);
            this.lbl_SysName.TabIndex = 1;
            this.lbl_SysName.Text = "汽修汽配管理系统";
            this.lbl_SysName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_SysName_MouseDown);
            // 
            // contextMenuStrip_WinShortCutBtn
            // 
            this.contextMenuStrip_WinShortCutBtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMenuItem_UserInfo,
            this.tSMenuItem_PWDReSet,
            this.tSMenuItem_Help,
            this.tSMenuItem_About});
            this.contextMenuStrip_WinShortCutBtn.Name = "contextMenuStrip_WinShortCutBtn";
            this.contextMenuStrip_WinShortCutBtn.ShowImageMargin = false;
            this.contextMenuStrip_WinShortCutBtn.Size = new System.Drawing.Size(100, 92);
            // 
            // tSMenuItem_UserInfo
            // 
            this.tSMenuItem_UserInfo.Name = "tSMenuItem_UserInfo";
            this.tSMenuItem_UserInfo.Size = new System.Drawing.Size(99, 22);
            this.tSMenuItem_UserInfo.Text = "用户信息";
            this.tSMenuItem_UserInfo.Click += new System.EventHandler(this.tSMenuItem_UserInfo_Click);
            // 
            // tSMenuItem_PWDReSet
            // 
            this.tSMenuItem_PWDReSet.Name = "tSMenuItem_PWDReSet";
            this.tSMenuItem_PWDReSet.Size = new System.Drawing.Size(99, 22);
            this.tSMenuItem_PWDReSet.Text = "重置密码";
            this.tSMenuItem_PWDReSet.Click += new System.EventHandler(this.tSMenuItem_PWDReSet_Click);
            // 
            // tSMenuItem_Help
            // 
            this.tSMenuItem_Help.Name = "tSMenuItem_Help";
            this.tSMenuItem_Help.Size = new System.Drawing.Size(99, 22);
            this.tSMenuItem_Help.Text = "帮助";
            // 
            // tSMenuItem_About
            // 
            this.tSMenuItem_About.Name = "tSMenuItem_About";
            this.tSMenuItem_About.Size = new System.Drawing.Size(99, 22);
            this.tSMenuItem_About.Text = "关于";
            this.tSMenuItem_About.Click += new System.EventHandler(this.tSMenuItem_About_Click);
            // 
            // panel_Body
            // 
            this.panel_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panel_Body.Controls.Add(this.splitContainerEx_Body);
            this.panel_Body.Controls.Add(this.panel_Bottom);
            this.panel_Body.Location = new System.Drawing.Point(0, 72);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(1299, 714);
            this.panel_Body.TabIndex = 2;
            // 
            // splitContainerEx_Body
            // 
            this.splitContainerEx_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerEx_Body.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerEx_Body.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx_Body.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerEx_Body.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.splitContainerEx_Body.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.splitContainerEx_Body.Location = new System.Drawing.Point(1, 0);
            this.splitContainerEx_Body.Name = "splitContainerEx_Body";
            // 
            // splitContainerEx_Body.Panel1
            // 
            this.splitContainerEx_Body.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerEx_Body.Panel1.Controls.Add(this.splitter_MenuNormalFunc);
            this.splitContainerEx_Body.Panel1.Controls.Add(this.panelLeft);
            this.splitContainerEx_Body.Panel1.SizeChanged += new System.EventHandler(this.splitContainerEx_Body_Panel1_SizeChanged);
            this.splitContainerEx_Body.Panel1MinSize = 200;
            // 
            // splitContainerEx_Body.Panel2
            // 
            this.splitContainerEx_Body.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainerEx_Body.Panel2.Controls.Add(this.panel_Content);
            this.splitContainerEx_Body.Panel2.Controls.Add(this.panel_TabBtnArea);
            this.splitContainerEx_Body.Panel2.Controls.Add(this.panelMenu);
            this.splitContainerEx_Body.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.splitContainerEx_Body.Panel2MinSize = 500;
            this.splitContainerEx_Body.Size = new System.Drawing.Size(1300, 689);
            this.splitContainerEx_Body.SplitterDistance = 213;
            this.splitContainerEx_Body.SplitterWidth = 6;
            this.splitContainerEx_Body.TabIndex = 1;
            // 
            // splitter_MenuNormalFunc
            // 
            this.splitter_MenuNormalFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.splitter_MenuNormalFunc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter_MenuNormalFunc.Location = new System.Drawing.Point(0, 686);
            this.splitter_MenuNormalFunc.Name = "splitter_MenuNormalFunc";
            this.splitter_MenuNormalFunc.Size = new System.Drawing.Size(213, 3);
            this.splitter_MenuNormalFunc.TabIndex = 1;
            this.splitter_MenuNormalFunc.TabStop = false;
            this.splitter_MenuNormalFunc.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_MenuNormalFunc_SplitterMoved);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.pnl_nf);
            this.panelLeft.Controls.Add(this.panel_LeftMenu);
            this.panelLeft.Controls.Add(this.panel_NormalFunc);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(213, 689);
            this.panelLeft.TabIndex = 4;
            // 
            // pnl_nf
            // 
            this.pnl_nf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_nf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_nf.BackgroundImage")));
            this.pnl_nf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_nf.Controls.Add(this.labelExt1);
            this.pnl_nf.Location = new System.Drawing.Point(5, 461);
            this.pnl_nf.Name = "pnl_nf";
            this.pnl_nf.Size = new System.Drawing.Size(79, 22);
            this.pnl_nf.TabIndex = 5;
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
            this.panel_LeftMenu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panel_LeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.panel_LeftMenu.Name = "panel_LeftMenu";
            this.panel_LeftMenu.Padding = new System.Windows.Forms.Padding(1);
            this.panel_LeftMenu.ShowBorder = true;
            this.panel_LeftMenu.Size = new System.Drawing.Size(213, 459);
            this.panel_LeftMenu.TabIndex = 2;
            // 
            // panel_NormalFunc
            // 
            this.panel_NormalFunc.Controls.Add(this.tabControlEx1);
            this.panel_NormalFunc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_NormalFunc.Location = new System.Drawing.Point(0, 459);
            this.panel_NormalFunc.Margin = new System.Windows.Forms.Padding(0);
            this.panel_NormalFunc.MaximumSize = new System.Drawing.Size(0, 230);
            this.panel_NormalFunc.MinimumSize = new System.Drawing.Size(0, 230);
            this.panel_NormalFunc.Name = "panel_NormalFunc";
            this.panel_NormalFunc.Size = new System.Drawing.Size(213, 230);
            this.panel_NormalFunc.TabIndex = 0;
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(229)))), ((int)(((byte)(243)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Location = new System.Drawing.Point(0, 0);
            this.tabControlEx1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.Padding = new System.Drawing.Point(0, 0);
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(213, 226);
            this.tabControlEx1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvUser_function);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(205, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常用功能";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvUser_function
            // 
            this.tvUser_function.AllowDrop = true;
            this.tvUser_function.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvUser_function.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.tvUser_function.Size = new System.Drawing.Size(205, 196);
            this.tvUser_function.TabIndex = 0;
            this.tvUser_function.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvUser_function_ItemDrag);
            this.tvUser_function.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUser_function_NodeMouseClick);
            this.tvUser_function.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvUser_function_DragDrop);
            this.tvUser_function.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvUser_function_DragEnter);
            this.tvUser_function.DragOver += new System.Windows.Forms.DragEventHandler(this.tvUser_function_DragOver);
            // 
            // panel_Content
            // 
            this.panel_Content.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.panel_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Content.Location = new System.Drawing.Point(7, 25);
            this.panel_Content.Name = "panel_Content";
            this.panel_Content.Padding = new System.Windows.Forms.Padding(1, 0, 3, 1);
            this.panel_Content.ShowBorder = true;
            this.panel_Content.Size = new System.Drawing.Size(1074, 661);
            this.panel_Content.TabIndex = 2;
            // 
            // panel_TabBtnArea
            // 
            this.panel_TabBtnArea.Controls.Add(this.pnlxMainTop);
            this.panel_TabBtnArea.Controls.Add(this.ibtnToRight);
            this.panel_TabBtnArea.Controls.Add(this.ibtnToLeft);
            this.panel_TabBtnArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_TabBtnArea.Location = new System.Drawing.Point(7, 0);
            this.panel_TabBtnArea.MaximumSize = new System.Drawing.Size(0, 25);
            this.panel_TabBtnArea.MinimumSize = new System.Drawing.Size(0, 25);
            this.panel_TabBtnArea.Name = "panel_TabBtnArea";
            this.panel_TabBtnArea.Size = new System.Drawing.Size(1074, 25);
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
            this.pnlxMainTop.Location = new System.Drawing.Point(30, 0);
            this.pnlxMainTop.Name = "pnlxMainTop";
            this.pnlxMainTop.Size = new System.Drawing.Size(1014, 25);
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
            this.ibtnToRight.Location = new System.Drawing.Point(1044, 0);
            this.ibtnToRight.Name = "ibtnToRight";
            this.ibtnToRight.Size = new System.Drawing.Size(30, 25);
            this.ibtnToRight.TabIndex = 12;
            this.ibtnToRight.Visible = false;
            this.ibtnToRight.Click += new System.EventHandler(this.ibtnToRight_Click);
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
            this.ibtnToLeft.Size = new System.Drawing.Size(30, 25);
            this.ibtnToLeft.TabIndex = 11;
            this.ibtnToLeft.Visible = false;
            this.ibtnToLeft.Click += new System.EventHandler(this.ibtnToLeft_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.panelMenu.Controls.Add(this.pbLeft);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(7, 686);
            this.panelMenu.TabIndex = 3;
            this.panelMenu.Visible = false;
            this.panelMenu.Click += new System.EventHandler(this.panelMenu_Click);
            this.panelMenu.MouseEnter += new System.EventHandler(this.panelMenu_Click);
            // 
            // pbLeft
            // 
            this.pbLeft.BackColor = System.Drawing.Color.Transparent;
            this.pbLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbLeft.BackgroundImage")));
            this.pbLeft.Location = new System.Drawing.Point(0, 303);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(7, 81);
            this.pbLeft.TabIndex = 4;
            this.pbLeft.TabStop = false;
            this.pbLeft.Click += new System.EventHandler(this.panelMenu_Click);
            this.pbLeft.MouseEnter += new System.EventHandler(this.panelMenu_Click);
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(129)))), ((int)(((byte)(194)))));
            this.panel_Bottom.Controls.Add(this.tlpMessage);
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 691);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(1299, 23);
            this.panel_Bottom.TabIndex = 0;
            // 
            // tlpMessage
            // 
            this.tlpMessage.AutoSize = true;
            this.tlpMessage.ColumnCount = 15;
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMessage.Controls.Add(this.label9, 14, 0);
            this.tlpMessage.Controls.Add(this.lbl7, 13, 0);
            this.tlpMessage.Controls.Add(this.labelVersion, 12, 0);
            this.tlpMessage.Controls.Add(this.lbl6, 11, 0);
            this.tlpMessage.Controls.Add(this.lblConnectionStatus, 10, 0);
            this.tlpMessage.Controls.Add(this.lbl5, 9, 0);
            this.tlpMessage.Controls.Add(this.lvlVoiceSwitch, 8, 0);
            this.tlpMessage.Controls.Add(this.lbl4, 7, 0);
            this.tlpMessage.Controls.Add(this.lblcount, 6, 0);
            this.tlpMessage.Controls.Add(this.lbl3, 5, 0);
            this.tlpMessage.Controls.Add(this.lblComUserLand, 4, 0);
            this.tlpMessage.Controls.Add(this.lbl2, 3, 0);
            this.tlpMessage.Controls.Add(this.lblLock, 2, 0);
            this.tlpMessage.Controls.Add(this.lbl1, 1, 0);
            this.tlpMessage.Controls.Add(this.lblDateTime, 0, 0);
            this.tlpMessage.Location = new System.Drawing.Point(179, 5);
            this.tlpMessage.Name = "tlpMessage";
            this.tlpMessage.RowCount = 1;
            this.tlpMessage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMessage.Size = new System.Drawing.Size(711, 14);
            this.tlpMessage.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(674, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "帮助";
            // 
            // lbl7
            // 
            this.lbl7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl7.AutoSize = true;
            this.lbl7.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl7.ForeColor = System.Drawing.Color.White;
            this.lbl7.Location = new System.Drawing.Point(649, 0);
            this.lbl7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(14, 14);
            this.lbl7.TabIndex = 15;
            this.lbl7.Text = "|";
            this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelVersion.ForeColor = System.Drawing.Color.White;
            this.labelVersion.Location = new System.Drawing.Point(585, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(53, 12);
            this.labelVersion.TabIndex = 14;
            this.labelVersion.Text = "版本信息";
            // 
            // lbl6
            // 
            this.lbl6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl6.AutoSize = true;
            this.lbl6.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl6.ForeColor = System.Drawing.Color.White;
            this.lbl6.Location = new System.Drawing.Point(560, 0);
            this.lbl6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(14, 14);
            this.lbl6.TabIndex = 13;
            this.lbl6.Text = "|";
            this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.White;
            this.lblConnectionStatus.Location = new System.Drawing.Point(496, 0);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(53, 12);
            this.lblConnectionStatus.TabIndex = 12;
            this.lblConnectionStatus.Text = "连接状态";
            // 
            // lbl5
            // 
            this.lbl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl5.ForeColor = System.Drawing.Color.White;
            this.lbl5.Location = new System.Drawing.Point(471, 0);
            this.lbl5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(14, 14);
            this.lbl5.TabIndex = 11;
            this.lbl5.Text = "|";
            this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvlVoiceSwitch
            // 
            this.lvlVoiceSwitch.AutoSize = true;
            this.lvlVoiceSwitch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvlVoiceSwitch.ForeColor = System.Drawing.Color.White;
            this.lvlVoiceSwitch.Location = new System.Drawing.Point(407, 0);
            this.lvlVoiceSwitch.Name = "lvlVoiceSwitch";
            this.lvlVoiceSwitch.Size = new System.Drawing.Size(53, 12);
            this.lvlVoiceSwitch.TabIndex = 10;
            this.lvlVoiceSwitch.Text = "声音开关";
            // 
            // lbl4
            // 
            this.lbl4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl4.ForeColor = System.Drawing.Color.White;
            this.lbl4.Location = new System.Drawing.Point(382, 0);
            this.lbl4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(14, 14);
            this.lbl4.TabIndex = 9;
            this.lbl4.Text = "|";
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblcount.ForeColor = System.Drawing.Color.White;
            this.lblcount.Location = new System.Drawing.Point(324, 0);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(47, 12);
            this.lblcount.TabIndex = 8;
            this.lblcount.Text = "在线0人";
            // 
            // lbl3
            // 
            this.lbl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl3.ForeColor = System.Drawing.Color.White;
            this.lbl3.Location = new System.Drawing.Point(299, 0);
            this.lbl3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(14, 14);
            this.lbl3.TabIndex = 7;
            this.lbl3.Text = "|";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblComUserLand
            // 
            this.lblComUserLand.AutoSize = true;
            this.lblComUserLand.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblComUserLand.ForeColor = System.Drawing.Color.White;
            this.lblComUserLand.Location = new System.Drawing.Point(181, 0);
            this.lblComUserLand.Name = "lblComUserLand";
            this.lblComUserLand.Size = new System.Drawing.Size(107, 12);
            this.lblComUserLand.TabIndex = 6;
            this.lblComUserLand.Text = "公司-用户名(账号)";
            // 
            // lbl2
            // 
            this.lbl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl2.ForeColor = System.Drawing.Color.White;
            this.lbl2.Location = new System.Drawing.Point(156, 0);
            this.lbl2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(14, 14);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "|";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLock
            // 
            this.lblLock.AutoSize = true;
            this.lblLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLock.ForeColor = System.Drawing.Color.White;
            this.lblLock.Location = new System.Drawing.Point(92, 0);
            this.lblLock.Name = "lblLock";
            this.lblLock.Size = new System.Drawing.Size(53, 12);
            this.lblLock.TabIndex = 4;
            this.lblLock.Text = "锁定离开";
            this.lblLock.Click += new System.EventHandler(this.lblLock_Click);
            // 
            // lbl1
            // 
            this.lbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("宋体", 10F);
            this.lbl1.ForeColor = System.Drawing.Color.White;
            this.lbl1.Location = new System.Drawing.Point(67, 0);
            this.lbl1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(14, 14);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "|";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(3, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(53, 12);
            this.lblDateTime.TabIndex = 2;
            this.lblDateTime.Text = "DateTime";
            // 
            // tmrInfo
            // 
            this.tmrInfo.Enabled = true;
            this.tmrInfo.Interval = 1000;
            this.tmrInfo.Tick += new System.EventHandler(this.tmrInfo_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Interval = 60000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
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
            this.关闭ToolStripMenuItem.Image = global::HXCPcClient.Properties.Resources.close;
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 关闭其他ToolStripMenuItem
            // 
            this.关闭其他ToolStripMenuItem.Image = global::HXCPcClient.Properties.Resources.close_other;
            this.关闭其他ToolStripMenuItem.Name = "关闭其他ToolStripMenuItem";
            this.关闭其他ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭其他ToolStripMenuItem.Text = "关闭其他";
            this.关闭其他ToolStripMenuItem.Click += new System.EventHandler(this.关闭其他ToolStripMenuItem_Click);
            // 
            // 关闭全部ToolStripMenuItem
            // 
            this.关闭全部ToolStripMenuItem.Image = global::HXCPcClient.Properties.Resources.close_all;
            this.关闭全部ToolStripMenuItem.Name = "关闭全部ToolStripMenuItem";
            this.关闭全部ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭全部ToolStripMenuItem.Text = "关闭全部";
            this.关闭全部ToolStripMenuItem.Click += new System.EventHandler(this.关闭全部ToolStripMenuItem_Click);
            // 
            // ucMenuBar1
            // 
            this.ucMenuBar1.AccessoriesBusiness_Tag = "CL_AccessoriesBusiness";
            this.ucMenuBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMenuBar1.BackColor = System.Drawing.Color.Transparent;
            this.ucMenuBar1.BusinessAnalysis_Tag = "CL_BusinessAnalysis";
            this.ucMenuBar1.CustomerService_Tag = "CL_CustomerService";
            this.ucMenuBar1.DataManagement_Tag = "CL_DataManagement";
            this.ucMenuBar1.FinancialManagement_Tag = "CL_FinancialManagement";
            this.ucMenuBar1.Location = new System.Drawing.Point(628, 5);
            this.ucMenuBar1.Name = "ucMenuBar1";
            this.ucMenuBar1.RepairBusiness_Tag = "CL_RepairBusiness";
            this.ucMenuBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucMenuBar1.Size = new System.Drawing.Size(520, 64);
            this.ucMenuBar1.SystemManagement_Tag = "CL_SystemManagement";
            this.ucMenuBar1.TabIndex = 0;
            this.ucMenuBar1.SystemManagementClick += new System.EventHandler(this.ucMenuBar1_SystemManagementClick);
            this.ucMenuBar1.DataManagementClick += new System.EventHandler(this.ucMenuBar1_DataManagementClick);
            this.ucMenuBar1.BusinessAnalysisClick += new System.EventHandler(this.ucMenuBar1_BusinessAnalysisClick);
            this.ucMenuBar1.CustomerServiceClick += new System.EventHandler(this.ucMenuBar1_CustomerServiceClick);
            this.ucMenuBar1.FinancialManagementClick += new System.EventHandler(this.ucMenuBar1_FinancialManagementClick);
            this.ucMenuBar1.AccessoriesBusinessClick += new System.EventHandler(this.ucMenuBar1_AccessoriesBusinessClick);
            this.ucMenuBar1.RepairBusinessClick += new System.EventHandler(this.ucMenuBar1_RepairBusinessClick);
            this.ucMenuBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseDown);
            this.ucMenuBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHead_MouseMove);
            // 
            // HXCMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1300, 786);
            this.Controls.Add(this.panel_Body);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 650);
            this.Name = "HXCMainForm";
            this.Text = "慧修车系统客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HXCMainForm_FormClosing);
            this.Load += new System.EventHandler(this.HXCMainForm_Load);
            this.SizeChanged += new System.EventHandler(this.HXCMainForm_SizeChanged);
            this.Controls.SetChildIndex(this.panel_Body, 0);
            this.Controls.SetChildIndex(this.panelHead, 0);
            this.panelHead.ResumeLayout(false);
            this.panelHeadLeft.ResumeLayout(false);
            this.panel_LoGo.ResumeLayout(false);
            this.panel_LoGo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.contextMenuStrip_WinShortCutBtn.ResumeLayout(false);
            this.panel_Body.ResumeLayout(false);
            this.splitContainerEx_Body.Panel1.ResumeLayout(false);
            this.splitContainerEx_Body.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx_Body)).EndInit();
            this.splitContainerEx_Body.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.pnl_nf.ResumeLayout(false);
            this.panel_NormalFunc.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel_TabBtnArea.ResumeLayout(false);
            this.pnlxMainTop.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            this.panel_Bottom.ResumeLayout(false);
            this.panel_Bottom.PerformLayout();
            this.tlpMessage.ResumeLayout(false);
            this.tlpMessage.PerformLayout();
            this.contextShortMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_LoGo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WinShortCutBtn;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_UserInfo;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_PWDReSet;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem_About;
        private System.Windows.Forms.Panel panel_Body;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Timer tmrInfo;
        private ServiceStationClient.ComponentUI.SplitContainerEx splitContainerEx_Body;
        private PanelExtend panel_LeftMenu;
        private System.Windows.Forms.Splitter splitter_MenuNormalFunc;
        private System.Windows.Forms.Panel panel_NormalFunc;
        private System.Windows.Forms.Panel panel_TabBtnArea;
        private PanelExtend panel_Content;
        private ServiceStationClient.ComponentUI.PanelEx pnlxMainTop;
        public ServiceStationClient.ComponentUI.PanelEx panelMemu;
        private ServiceStationClient.ComponentUI.ImageButton ibtnToRight;
        private System.Windows.Forms.Label lbl_SysName;
        private ServiceStationClient.ComponentUI.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private ServiceStationClient.ComponentUI.TreeViewEx tvUser_function;
        private UCMainLayOut.UCMenuBar ucMenuBar1;
        private System.Windows.Forms.TableLayoutPanel tlpMessage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lvlVoiceSwitch;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lblComUserLand;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lblLock;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private ServiceStationClient.ComponentUI.ImageButton ibtnToLeft;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ContextMenuStrip contextShortMenu;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭其他ToolStripMenuItem;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.ToolStripMenuItem 关闭全部ToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_nf;
        private System.Windows.Forms.LabelExt labelExt1;
    }
}

