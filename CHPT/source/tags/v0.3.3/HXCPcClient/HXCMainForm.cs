using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Collections.Generic;
using System.Data;
using HXCPcClient.UCMainLayOut;
using HXCPcClient.CommonClass;
using System.Threading;
using HXCPcClient.FormLevelTwo;

namespace HXCPcClient
{
    /// <summary>
    /// 客户端主窗体
    /// </summary>
    public partial class HXCMainForm : FormExhyh
    {
        #region 属性
        /// <summary>
        /// 常用功能树图标
        /// </summary>
        ImageList treeImage = new ImageList();
        private Panel currentPanel = null;
        #endregion

        #region 初始化
        public HXCMainForm()
        {
            InitializeComponent();

            //this.panel_WinShortCutBtn.Visible = false;
            this.splitter_MenuNormalFunc.Enabled = false;
            this.notifyIcon.Visible = false;
            this.ibtnToLeft.Visible = false;          
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            this.WindowState = FormWindowState.Maximized;
            this.LoadEvent();

            Closed += (sender, args) => Environment.Exit(-1);
        }
        #endregion

        #region --加载自定义事件
        private void LoadEvent()
        {
            UCLeftMenu.MainFormAddLabel -= new MainFormAddLabels(this.MainFormAddLabel);
            UCLeftMenu.MainFormAddLabel += new MainFormAddLabels(this.MainFormAddLabel);

            HXCPcClient.UCForm.UCBase.addUserControls -= new UCForm.addUserControls(addUserControls);
            HXCPcClient.UCForm.UCBase.addUserControls += new UCForm.addUserControls(addUserControls);

            HXCPcClient.UCForm.UCBase.deleteMenu -= new UCForm.deleteMenu(deleteMenu);
            HXCPcClient.UCForm.UCBase.deleteMenu += new UCForm.deleteMenu(deleteMenu);

            HXCPcClient.UCForm.RepairBusiness.RepairDispatch.UCDispatchBase.addUserControls -= new UCForm.RepairBusiness.RepairDispatch.addUserControls(addUserControls);
            HXCPcClient.UCForm.RepairBusiness.RepairDispatch.UCDispatchBase.addUserControls += new UCForm.RepairBusiness.RepairDispatch.addUserControls(addUserControls);

            HXCPcClient.UCForm.RepairBusiness.RepairDispatch.UCDispatchBase.deleteMenu -= new UCForm.RepairBusiness.RepairDispatch.deleteMenu(deleteMenu);
            HXCPcClient.UCForm.RepairBusiness.RepairDispatch.UCDispatchBase.deleteMenu += new UCForm.RepairBusiness.RepairDispatch.deleteMenu(deleteMenu);
        }
        #endregion

        #region --窗体初始化
        private void HXCMainForm_Load(object sender, EventArgs e)
        {
            this.uiHandler -= new UiHandler(this.ShowCurrentTime);
            this.uiHandler += new UiHandler(this.ShowCurrentTime);
            //加载时间
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadDBTime));

            //加载图标
            this.LoadLogo();
            //加载报表设计器
            FastReport.Utils.Res.LoadLocale("Report\\Chinese (Simplified).frl");//设置报表设计器语言           
            //this.panel_Content.SetVisible(true, true, true, false);
            
            this.loadHome();// 加载首页           
            this.LoadUser_function();//加载常用功能   

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._GetOnline));
        }

        #endregion

        #region --加载当前时间
        /// <summary> 加载时间 
        /// </summary>
        private void _LoadDBTime(object obj)
        {
            GlobalStaticObj.CurrentDateTime = HXCPcClient.CommonClass.DBHelper.GetCurrentTime();
            this.Invoke(this.uiHandler, "");
        }
        private void ShowCurrentTime(object obj)
        {
            lblDateTime.Text = GlobalStaticObj.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "  " + GlobalStaticObj.CurrentDateTime.ToString("dddd");
            lblComUserLand.Text = GlobalStaticObj.CurrUserCom_Name + "-" + GlobalStaticObj.UserName + "(" + GlobalStaticObj.LandName + ")";
        }
        #endregion

        #region 加载常用功能
        /// <summary>
        /// 加载常用功能
        /// </summary>
        private void LoadUser_function()
        {
            if (GlobalStaticObj.gLoginDataSet != null
                && GlobalStaticObj.gLoginDataSet.Tables[1] != null
                && GlobalStaticObj.gLoginDataSet.Tables[1].Rows.Count > 0)
            {
                treeImage.ImageSize = new Size(12, 12);
                tvUser_function.ImageList = treeImage;

                foreach (DataRow dr in GlobalStaticObj.gLoginDataSet.Tables[1].Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = dr;
                    node.Name = dr["fun_id"].ToString();
                    node.Text = dr["fun_name"].ToString();
                    if (dr["fun_img"] != DBNull.Value && dr["fun_img"].ToString() != "")
                    {
                        treeImage.Images.Add(dr["fun_img"].ToString(),
                            ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(dr["fun_img"], true));
                        node.ImageKey = dr["fun_img"].ToString();
                        node.SelectedImageKey = dr["fun_img"].ToString();
                    }
                    tvUser_function.Nodes.Add(node);
                }
            }
        }
        #endregion

        #region 加载图标
        private void LoadLogo()
        {
            this.panel_WinShortCutBtn.ContextMenuStrip = this.contextMenuStrip_WinShortCutBtn;
            try
            {
                if (string.IsNullOrEmpty(SYSModel.clsSysConfig.SYSTEM_LOGO_PATH))
                {
                    picLogo.Image = ServiceStationClient.Skin.Properties.Resources.logo;
                }
                else
                {
                    picLogo.Load(SYSModel.clsSysConfig.SYSTEM_LOGO_PATH);
                }
            }
            catch (Exception ex)
            {
                //log.AddErrorLog("ServiceStationClient.Exe.frmMain.loadSkin", ex.Message);
            }
        }
        #endregion

        #region 时间自动走的事件
        private void tmrInfo_Tick(object sender, EventArgs e)
        {
            GlobalStaticObj.CurrentDateTime = GlobalStaticObj.CurrentDateTime.AddSeconds(1);
            lblDateTime.Text = GlobalStaticObj.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                + "  " + GlobalStaticObj.CurrentDateTime.ToString("dddd");
        }
        #endregion

        #region 多页签 —————————

        #region 加载首页
        /// <summary>
        /// 加载首页
        /// </summary>
        private void loadHome()
        {
            splitContainerEx_Body.SplitterWidth = 6;

            UUCHomeManage ucHome = new UUCHomeManage(); //首页               
            ucHome.ID = "STR_" + SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE;
            ucHome.Name = SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE;
            ucHome.Dock = DockStyle.Fill;            
            panel_Content.Controls.Add(ucHome);// panel_Main.Controls.Add(ucHome);
            ucHome.BringToFront();            

            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = "CL_RepairBusiness";
            childmemu(null, "首页", SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE, "", SYSModel.clsSysConfig.STR_CURR_MAINMEMU, "");

            if (!this.panel_LeftMenu.Controls.ContainsKey(SYSModel.clsSysConfig.STR_CURR_MAINMEMU))
            {
                HXCPcClient.UCMainLayOut.UCLeftMenu UCLeftMenu = new HXCPcClient.UCMainLayOut.UCLeftMenu(); //系统管理
                UCLeftMenu.Name = SYSModel.clsSysConfig.STR_CURR_MAINMEMU;
                UCLeftMenu.FunID = SYSModel.clsSysConfig.STR_CURR_MAINMEMU;
                UCLeftMenu.Dock = DockStyle.Fill;                
                UCLeftMenu.menuName = "维修业务";
                UCLeftMenu.Tag = UCLeftMenu.menuName;
                UCLeftMenu.FmMain = this;
                UCLeftMenu.Focus();
                panel_LeftMenu.Controls.Add(UCLeftMenu);
                UCLeftMenu.BringToFront();

                SYSModel.clsSysConfig.STR_CURR_MAINMEMU = UCLeftMenu.FunID;
                ucMenuBar1.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态               
            }
            else
            {
                (panel_LeftMenu.Controls.Find("CL_RepairBusiness", false)[0] as HXCPcClient.UCMainLayOut.UCLeftMenu).BringToFront();
            }
        }
        #endregion

        #region 左侧菜单点击事件
        /// <summary>
        /// 左侧菜单点击事件
        /// </summary>
        /// <param name="fun">功能记录</param>
        /// <param name="ucLeftMenu">左侧菜单对象</param>
        public void MainFormAddLabel(DataRow fun, UCLeftMenu ucLeftMenu)
        {           
            if (!this.panel_Content.Controls.ContainsKey(fun["fun_id"].ToString()))
            {
                //添加窗体
                UserControl uc = CommonCtrl.TraverseForm(fun["fun_uri"].ToString());
                if (uc == null)
                {
                    MessageBoxEx.Show(fun["fun_name"].ToString() + " 不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                uc.Name = fun["fun_id"].ToString();
                uc.Dock = DockStyle.Fill;
                uc.Focus();
                uc.Tag = fun["fun_id"].ToString() + "|" + ucLeftMenu.Name + "|" + fun["parent_id"].ToString();//三级 |一级| 二级 菜单的id
                panel_Content.Controls.Add(uc);
                uc.BringToFront();

                //添加页签
                Image myImage = null;
                if (fun["fun_img"] != DBNull.Value && fun["fun_img"].ToString().Length > 0)
                {
                    myImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(fun["fun_img"]);
                }
                childmemu(myImage, fun["fun_name"].ToString(), fun["fun_id"].ToString(), fun["parent_id"].ToString(), ucLeftMenu.Name, "");
            }
            else
            {
                panel_Content.Controls[fun["fun_id"].ToString()].BringToFront();
                childMemuState(fun["fun_id"].ToString());
            }
            SYSModel.clsSysConfig.STR_CURR_TWOMEMU = fun["parent_id"].ToString();
        }
        #endregion

        #region 添加标签 添加内容窗体  子窗体内事件触发
        /// <summary>
        /// 添加标签 添加内容窗体
        /// </summary>
        /// <param name="userContol">新窗体</param>
        /// <param name="memuName">标签名</param>
        /// <param name="menuId">三级菜单id</param>
        /// <param name="thisUcTag">当前窗体Tag tag包括（三级|一级| 二级 菜单的id）</param>
        /// ///  <param name="PUCName">父窗体Name</param>
        public void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName)
        {
            try
            {
                if (thisUcTag != null)
                {
                    string[] strArr = thisUcTag.Split('|');
                    if (!panel_Content.Controls.ContainsKey(menuId))
                    {
                        Image myImage = null;
                        if (GlobalStaticObj.gLoginDataSet != null
                            && GlobalStaticObj.gLoginDataSet.Tables.Count > 2
                            && GlobalStaticObj.gLoginDataSet.Tables[2] != null
                            && GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
                        {
                            DataView dv = GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                            dv.RowFilter = "fun_id='" + strArr[0] + "'";
                            if (dv.Count > 0)
                            {
                                DataRowView drv = dv[0];
                                if (drv["fun_img"] != DBNull.Value && drv["fun_img"].ToString().Length > 0)
                                {
                                    myImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(drv["fun_img"]);
                                }
                            }
                        }

                        userContol.Name = menuId;
                        userContol.Dock = DockStyle.Fill;
                        userContol.Focus();
                        panel_Content.Controls.Add(userContol);// panel_Main.Controls.Add(ucHome);                   
                        userContol.BringToFront();
                        userContol.Tag = menuId + "|" + strArr[1] + "|" + strArr[2] + "|" + PUCName;

                        childmemu(myImage, memuName, menuId, strArr[2], strArr[1], PUCName);
                    }
                    else
                    {
                        this.panel_Content.Controls.Find(menuId, false)[0].BringToFront();
                        childMemuState(menuId);
                    }
                    SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                }
                else
                {
                    userContol.Dock = DockStyle.Fill;
                    userContol.Focus();
                    panel_Content.Controls.Add(userContol);// panel_Main.Controls.Add(ucHome);                   
                    userContol.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
            
        }
        #endregion

        #region 添加多页签
        /// <summary>
        /// 添加多页签
        /// </summary>
        /// <param name="myImage">图标</param>
        /// <param name="memuName">三级级菜单名</param> 
        /// <param name="ppid">三级级菜单id</param>
        /// <param name="ppid">所属二级菜单id</param>
        /// <param name="ppid">所属一级菜单id</param>
        /// <param name="fun">FunctionEntity</param>
        /// <param name="PUCName">父窗体Name</param>
        /// <remarks>
        /// 代码请勿更改,更改前请告知 Kord
        /// </remarks>
        private void childmemu(Image myImage, string memuName, string funid, string pid, string ppid, string PUCName)
        {
            try
            {
                var STR_CS_MEMU_NAME = funid;//控件名
                //TagValue 当前选中的 三级菜单|一级菜单|二级菜单
                var TagValue = STR_CS_MEMU_NAME + "|" + ppid + "|" + pid + "|" + PUCName;
                var plecd = new Panel { Name = STR_CS_MEMU_NAME, Tag = TagValue, Width = 50, Height = 23 };       //二级菜单Panel                    
                Control ctl = null;
                if (panelMemu.Controls.Count > 0)
                {
                    var ctrls = panelMemu.Controls.Find(clsSysConfig.STR_CURR_MEMU, false);
                    if (ctrls.Any()) ctl = ctrls[0];
                    if (ctl != null) plecd.Left = ctl.Left + ctl.Width + 1;
                }
                else
                {
                    plecd.Left = 0;
                }
                plecd.Top = 0;
                plecd.Anchor = AnchorStyles.Left;
                plecd.Click += lbl_Click;
                plecd.MouseMove += plecd_MouseEnter;
                plecd.MouseLeave += plecd_MouseLeave;
                plecd.BorderStyle = BorderStyle.None;
                #region add by kord -- 为了突出当前选项卡背景色
                SetCurrentPnlBg(plecd);
                #endregion
                plecd.BackgroundImageLayout = ImageLayout.Stretch;
                var PnlIndex = 0;
                if (panelMemu.Controls.Count > 0)
                {
                    if (ctl != null) PnlIndex = panelMemu.Controls.GetChildIndex(ctl);
                }
                panelMemu.Controls.Add(plecd);
                if (panelMemu.Controls.Count > 1)
                {
                    panelMemu.Controls.SetChildIndex(plecd, PnlIndex + 1);
                }

                var pic = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.CenterImage,  //二级菜单小图标
                    Cursor = Cursors.Default,
                    BackColor = Color.Transparent,
                    Left = 5,
                    Top = 7,
                    Width = 12,
                    Height = 12,
                    Tag = TagValue
                };
                if (myImage != null)
                {
                    pic.Image = myImage;
                }
                pic.MouseMove += lbl_MouseEnter;
                pic.MouseLeave += pic_MouseLeave;
                pic.Click += lbl_Click;
                plecd.Controls.Add(pic);

                var lbl = new Label
                {
                    Cursor = Cursors.Default,
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Left = 22,
                    Top = 8,
                    Text = memuName,    //二级菜单名称
                    Tag = TagValue
                };
                lbl.MouseEnter += lbl_MouseEnter;
                lbl.MouseLeave += lbl_MouseLeave;
                lbl.Click += lbl_Click;
                plecd.Controls.Add(lbl);

                plecd.Width = 22 + lbl.Width + 12 + 5;

                var ibtn = new ImageButton { Name = "ibtn" + STR_CS_MEMU_NAME, Cursor = Cursors.Hand, Width = 8, Height = 8 };  //二级菜单关闭 x
                ibtn.Left = plecd.Width - ibtn.Width - 5;
                ibtn.Top = 5;
                ibtn.Tag = TagValue;
                ibtn.BackColor = Color.Transparent;
                ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.TabPage_Close_E;
                ibtn.MouseEnter += ibtn_MouseEnter;
                ibtn.MouseLeave += ibtn_MouseLeave;
                if (STR_CS_MEMU_NAME == clsSysConfig.STR_CS_MEMU_HOMEMANAGE)
                {
                    ibtn.Visible = false;
                }
                else
                {
                    ibtn.Click += ibtn_Click;
                }
                plecd.Controls.Add(ibtn);

                childMemuState(STR_CS_MEMU_NAME);
            }
            catch (Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 菜单关闭按钮事件
        /// <summary>
        /// 菜单关闭按钮事件
        /// </summary>
        void ibtn_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            if (ctl.Tag == null)
            {
                return;
            }
            string strTag = ctl.Tag.ToString();
            string[] arrays = strTag.Split('|');
            if (arrays.Length > 3)
            {
                this.deleteMenu(strTag, arrays[3]);
            }
        }
        #endregion

        #region 删除标签 及其 内容

        /// <summary>
        /// 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        /// <param name="PUCName">父窗体name</param>
        private void deleteMenu(string strTag, string PUCName)
        {
            string[] strArr = strTag.Split('|');

            int pnlIndex = 0;

            Control pnl = this.panelMemu.Controls.Find(strArr[0], false)[0] as Control;
            pnlIndex = this.panelMemu.Controls.GetChildIndex(pnl);
            
            pnl.Dispose();
            //panel_Content.Controls.Find(strArr[0], false)[0].Dispose();
            panel_Content.Controls.RemoveByKey(strArr[0]);
            if (PUCName.Length > 0)
            {
                Control[] ctls = this.panelMemu.Controls.Find(PUCName, false);
                if (ctls.Length > 0)
                {
                    string name = ctls[0].Tag.ToString();
                    string[] Arr = name.Split('|');
                    if (Arr[1].Length > 0)
                    {
                        this.panel_Content.Controls.Find(Arr[0], false)[0].BringToFront();
                        ctls = this.panel_LeftMenu.Controls.Find(Arr[1], false);
                        if (ctls.Length > 0)
                        {
                            UCLeftMenu uc = (UCLeftMenu)ctls[0];
                            uc.BringToFront();
                            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                            ucMenuBar1.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                            uc.LeftMenuState(Arr[2], Arr[0]);
                        }
                    }
                    SYSModel.clsSysConfig.STR_CURR_TWOMEMU = Arr[2];
                    childMemuState(Arr[0]);
                }
            }
            else
            {
                if (strArr[0] == SYSModel.clsSysConfig.STR_CURR_MEMU)
                {
                    if (pnlIndex > 0)
                    {
                        pnlIndex--;
                    }
                    else
                    {
                        pnlIndex = 0;
                    }
                    if (panelMemu.Controls.Count > 0)
                    {
                        Panel px = panelMemu.Controls[pnlIndex] as Panel;
                        strArr = px.Tag.ToString().Split('|');
                        if (strArr[1].Length > 0)
                        {
                            Control[] ctls = panel_LeftMenu.Controls.Find(strArr[1], false);
                            if (ctls.Length > 0)
                            {
                                UCLeftMenu uc = ctls[0] as UCLeftMenu;
                                uc.BringToFront();
                                SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                                ucMenuBar1.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                                uc.LeftMenuState(strArr[2], strArr[0]);
                            }
                        }
                        childMemuState(strArr[0]);
                        SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                    }
                }
                else
                {
                    (panel_LeftMenu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MAINMEMU, false)[0] as UCLeftMenu)
                                .LeftMenuState(SYSModel.clsSysConfig.STR_CURR_TWOMEMU, SYSModel.clsSysConfig.STR_CURR_MEMU);
                    childMemuState(SYSModel.clsSysConfig.STR_CURR_MEMU);
                }
            }
        }
        #endregion

        #region 标签菜单鼠标进入事件
        void ibtn_MouseEnter(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            string[] strArr = name.Split('|');
            (sender as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_Close_L");
            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {
                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L");
                //(ctls[0] as Control).Refresh();
            }
        }

        void lbl_MouseEnter(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            string[] strArr = name.Split('|');
            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {

                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L");
                //(ctls[0] as Control).Refresh();
            }            
        }

        void pic_MouseEnter(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            string[] strArr = name.Split('|');
            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {
                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L");
                //(ctls[0] as Control).Refresh();
            }
        }

        /// <summary>
        /// 标签菜单鼠标进入事件
        /// </summary>
        void plecd_MouseEnter(object sender, EventArgs e)
        {
            (sender as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L");
            //(sender as Control).Refresh();
        }
        #endregion

        #region 菜单鼠标离开事件
        void ibtn_MouseLeave(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            (sender as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_Close_E");
            string[] strArr = name.Split('|');
            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {
                if (ctls[0] == currentPanel) return; //add by kord -- 如果事件源为当前选项卡则不更改选项卡背景
                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_E");
                //(ctls[0] as Control).Refresh();               
            }            
        }

        void lbl_MouseLeave(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            string[] strArr = name.Split('|');

            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {
                if (ctls[0] == currentPanel) return; //add by kord -- 如果事件源为当前选项卡则不更改选项卡背景
                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_E");
                //(ctls[0] as Control).Refresh();
            }
        }

        void pic_MouseLeave(object sender, EventArgs e)
        {
            string name = (sender as Control).Tag.ToString();
            string[] strArr = name.Split('|');

            Control[] ctls = this.panelMemu.Controls.Find(strArr[0], false);
            if (ctls.Length > 0)
            {
                if (ctls[0] == currentPanel) return; //add by kord -- 如果事件源为当前选项卡则不更改选项卡背景
                (ctls[0] as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_E");
                //(ctls[0] as Control).Refresh();
            }          
        }
        /// <summary>
        /// 菜单鼠标离开事件
        /// </summary>
        void plecd_MouseLeave(object sender, EventArgs e)
        {
            if (sender == currentPanel) return; //add by kord -- 如果事件源为当前选项卡则不更改选项卡背景
            (sender as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_E");
            //(sender as Control).Refresh();
        }
        #endregion

        #region 标签菜单单击事件
        /// <summary>
        /// 标签菜单单击事件
        /// </summary>
        void lbl_Click(object sender, EventArgs e)
        {
            this.SetMenu(sender);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///     modify by kord @2014.12.19 10.08
        ///     捕获异常信息,并记录对象的"路径"信息
        /// </remarks>
        /// <param name="obj"></param>
        private void SetMenu(object obj)
        {
            try
            {
                if (!(obj is Control))
                {
                    return;
                }
                var ctl = obj as Control;
                if (ctl.Tag == null)
                {
                    return;
                }

                GlobalStaticObj.GlobalLogService.WriteLog(1, ctl.Tag.ToString());
                var name = ctl.Tag.ToString();
                var strArr = name.Split('|');
                if (strArr.Length <= 1 || strArr[1].Length <= 0)
                {
                    const string msg = "无法获取菜单对应的\"路径\"信息!";
                    GlobalStaticObj.GlobalLogService.WriteLog(msg);
                    MessageBoxEx.Show(msg);
                    return;
                }
                var ctls = panel_LeftMenu.Controls.Find(strArr[1], false);
                if (ctls.Length > 0)
                {
                    var uc = (UCLeftMenu)ctls[0];
                    uc.BringToFront();
                    clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                    GlobalStaticObj.GlobalLogService.WriteLog(1, uc.FunID);
                    ucMenuBar1.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                }
                clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                panel_Content.Controls.Find(strArr[0], false)[0].BringToFront();
                childMemuState(strArr[0]);
            }
            catch (Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 三级菜单状态
        /// <summary>
        /// 三级菜单状态
        /// </summary>
        /// <param name="pname">选中的功能名称name</param>
        private void childMemuState(string pname)
        {
            SYSModel.clsSysConfig.STR_CURR_MEMU = pname;
            if (panelMemu.Controls.Count > 0)
            {
                //页签更新
                this.panelMemuStatus();

                //double w = panelMemu.Width;
                int ileft = 1;
                int panelMemuWidth = 0;
                for (int i = 0; i < panelMemu.Controls.Count; i++)
                {
                    Control ctl = panelMemu.Controls[i];
                    if (ctl.Name == pname)
                    {
                        //ctl.BackColor = Color.FromArgb(241, 246, 252);  //选中的当前页签颜色 //remove by kord
                        SetCurrentPnlBg(ctl);   //add by kord -- 设置页签背景图片
                    }
                    else
                    {
                        ctl.BackColor = Color.FromArgb(223, 238, 255); //其它页签颜色
                    }
                    ctl.Location = new Point(ileft, 0);
                    ileft = ileft + ctl.Width + 1;
                    panelMemuWidth += ctl.Width + 1;
                    //ctl.Refresh();
                }
                panelMemu.Width = panelMemuWidth;
            }
            panelMemu.Refresh();
        }
        #endregion

        #region 标签左右按钮
        //←左
        private void ibtnToLeft_Click(object sender, EventArgs e)
        {
            panelMemu.Left = panelMemu.Left - 80;
            panelMemuStatus();
        }

        //右
        private void ibtnToRight_Click(object sender, EventArgs e)
        {
            panelMemu.Left = panelMemu.Left + 80;
            panelMemuStatus();
        }
        #endregion

        #region 多页签状态
        /// <summary>
        /// 多页签状态
        /// </summary>
        private void panelMemuStatus()
        {
            if (panelMemu.Width <= pnlxMainTop.Width)
            {
                panelMemu.Left = 0;
            }
            if (panelMemu.Left < 0)
            {
                if (panelMemu.Left + panelMemu.Width >= pnlxMainTop.Width)
                {
                    ibtnToLeft.Visible = true;
                    ibtnToRight.Visible = true;
                }
                else
                {
                    ibtnToLeft.Visible = false;
                    ibtnToRight.Visible = true;
                }
            }
            else
            {
                if (panelMemu.Width > pnlxMainTop.Width)
                {
                    ibtnToLeft.Visible = true;
                    ibtnToRight.Visible = false;
                }
                else
                {
                    ibtnToLeft.Visible = false;
                    ibtnToRight.Visible = false;
                }
            }
        }
        #endregion

        #endregion

        #region 添加左侧菜单
        /// <summary>
        /// 添加左侧菜单
        /// </summary>
        /// <param name="menuId">一级菜单的Tag 菜单id</param>
        /// <param name="munuName">菜单名</param>
        private void AddLeftMenu(string menuId, string munuName)
        {
            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = menuId;
            if (!this.panel_LeftMenu.Controls.ContainsKey(menuId))
            {
                UCLeftMenu UCLeftMenu = new UCLeftMenu(); //系统管理
                UCLeftMenu.Name = menuId;//SYSModel.clsSysConfig.STR_CS_MEMU_FUNCTIONMANAGE;
                // UCLeftMenu.dtFunction = dt;
                UCLeftMenu.FunID = menuId;
                //UCLeftMenu.panel_MainHeight = pnlxLeftMenu.Height;
                UCLeftMenu.Dock = DockStyle.Fill;
                UCLeftMenu.Tag = munuName;
                UCLeftMenu.menuName = munuName;
                UCLeftMenu.FmMain = this;
                UCLeftMenu.Focus();
                panel_LeftMenu.Controls.Add(UCLeftMenu);
                UCLeftMenu.BringToFront();
            }
            else
            {
                UCLeftMenu UCLeftMenu = panel_LeftMenu.Controls.Find(menuId, false)[0] as UCLeftMenu;
                UCLeftMenu.LeftMenuState(SYSModel.clsSysConfig.STR_CURR_TWOMEMU, SYSModel.clsSysConfig.STR_CURR_MEMU, SYSModel.clsSysConfig.STR_CURR_MEMU);
                UCLeftMenu.BringToFront();
            }
        }
        #endregion

        #region 顶部一级菜单事件
        /// <summary>
        /// 系统管理
        /// </summary>
        private void ucMenuBar1_SystemManagementClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.SystemManagement_Tag, "系统管理");
        }
        /// <summary>
        /// 数据管理
        /// </summary>
        private void ucMenuBar1_DataManagementClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.DataManagement_Tag, "数据管理");
        }

        private void ucMenuBar1_RepairBusinessClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.RepairBusiness_Tag, "维修管理");
        }

        private void ucMenuBar1_FinancialManagementClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.FinancialManagement_Tag, "账务管理");
        }
        /// <summary>
        /// 配件业务
        /// </summary>
        private void ucMenuBar1_AccessoriesBusinessClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.AccessoriesBusiness_Tag, "配件业务");
        }

        private void ucMenuBar1_CustomerServiceClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.CustomerService_Tag, "客户服务");
        }

        private void ucMenuBar1_BusinessAnalysisClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenuBar1.BusinessAnalysis_Tag, "经营分析");
        }
        #endregion

        #region 菜单拖动
        #region 菜单拖动 拖入
        /// <summary>
        /// 刚拖进时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvUser_function_DragEnter(object sender, DragEventArgs e)
        {
            //设置拖拽类型(这里是复制拖拽)
            e.Effect = DragDropEffects.Copy;
        }
        /// <summary>
        /// 拖拽完成时发生的事件（两个控件的该事件都指向这个方法）
        /// </summary>
        private void tvUser_function_DragDrop(object sender, DragEventArgs e)
        {
            if (sender != null && sender is TreeViewEx && e.AllowedEffect == DragDropEffects.Copy)
            {
                //获取值
                TreeNode item = (TreeNode)e.Data.GetData(e.Data.GetFormats()[0]);
                TreeNode node = (TreeNode)item.Clone();
                foreach (TreeNode nd in this.tvUser_function.Nodes)
                {
                    if (node.Name == nd.Name)
                    {
                        MessageBoxEx.Show("菜单已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //添加关联表信息
                string keyName = string.Empty;
                string keyValue = string.Empty;
                string opName = "新增常用功能";
                Dictionary<string, string> Fileds = new Dictionary<string, string>();
                string strUser_fun_id = Guid.NewGuid().ToString();
                Fileds.Add("user_fun_id", strUser_fun_id);//新ID
                Fileds.Add("fun_id", node.Name);
                Fileds.Add("user_id", GlobalStaticObj.UserID);
                if (DBHelper.Submit_AddOrEdit(opName, "tr_user_function", keyName, keyValue, Fileds))
                {
                    DataTable dt = DBHelper.GetTable("查询常用功能", "sys_function f, tr_user_function uf",
                         " f.fun_id,f.fun_name,f.fun_img,f.fun_uri,f.parent_id,(select parent_id from sys_function where fun_id=f.parent_id) as firstid ",
                         "f.fun_id=uf.fun_id and uf.user_fun_id = '" + strUser_fun_id + "'", "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["fun_img"] != DBNull.Value && dt.Rows[0]["fun_img"].ToString() != "")
                        {
                            treeImage.Images.Add(dt.Rows[0]["fun_img"].ToString(), (Image)ServiceStationClient.Skin.Properties.Resources.ResourceManager.GetObject(dt.Rows[0]["fun_img"].ToString()));
                            node.ImageKey = dt.Rows[0]["fun_img"].ToString();
                            node.SelectedImageKey = dt.Rows[0]["fun_img"].ToString();
                        }
                        node.Tag = dt.Rows[0];
                        (sender as TreeViewEx).Nodes.Add(node);
                    }
                }
            }
        }
        #endregion
        #region 菜单拖动 拖出
        /// <summary>
        /// 开始拖动
        /// </summary>
        private void tvUser_function_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode && e.Button == System.Windows.Forms.MouseButtons.Left &&
              e.Item != null && sender is TreeViewEx)
            {
                (sender as TreeViewEx).DoDragDrop(e.Item, DragDropEffects.None);
            }
        }
        /// <summary>
        /// 移动到边界外
        /// </summary>
        private void tvUser_function_DragOver(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == DragDropEffects.None)
            {
                //获取值
                TreeNode item = (TreeNode)e.Data.GetData(e.Data.GetFormats()[0]);
                //删除关联关系
                if (DBHelper.BatchDeleteDataByWhere("删除常用功能", "tr_user_function", "fun_id='" + item.Name + "' and user_id='" + GlobalStaticObj.UserID + "'"))
                {
                    (sender as TreeViewEx).Nodes.Remove(item);
                }
            }
        }
        #endregion
        #endregion

        #region 常用功能 点击事件
        private void tvUser_function_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DataRow dr = e.Node.Tag as DataRow;
            HXCPcClient.UCMainLayOut.UCLeftMenu UCLeftMenu;
            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = dr["firstid"].ToString();//一级菜单id
            SYSModel.clsSysConfig.STR_CURR_TWOMEMU = dr["parent_id"].ToString();//二级菜单id
            string threeMenu = e.Node.Name;//三级菜单
            Control[] ctls = this.panel_LeftMenu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MAINMEMU, false);

            if (ctls.Length == 0)
            {
                UCLeftMenu = new HXCPcClient.UCMainLayOut.UCLeftMenu();
                UCLeftMenu.Name = SYSModel.clsSysConfig.STR_CURR_MAINMEMU;
                UCLeftMenu.FunID = SYSModel.clsSysConfig.STR_CURR_MAINMEMU;
                UCLeftMenu.Dock = DockStyle.Fill;
                UCLeftMenu.Tag = "";
                UCLeftMenu.menuName = "";
                UCLeftMenu.FmMain = this;
                UCLeftMenu.Focus();
                panel_LeftMenu.Controls.Add(UCLeftMenu);
            }
            else
            {
                UCLeftMenu = ctls[0] as HXCPcClient.UCMainLayOut.UCLeftMenu;
            }
            UCLeftMenu.LeftMenuState(SYSModel.clsSysConfig.STR_CURR_TWOMEMU, e.Node.Name);
            UCLeftMenu.BringToFront();
            this.MenuAdd(dr);
            ucMenuBar1.MenuButtonStatus();
        }
        #endregion

        #region 常用功能 添加菜单
        /// <summary>
        /// 常用功能 添加菜单  
        /// </summary>
        /// <param name="dr">DataRow一行数据 包括（fun_id,fun_name,fun_img,fun_uri,parent_id, firstid）firstid为一级菜单ID</param>
        private void MenuAdd(DataRow dr)
        {
            string menuId = dr["fun_id"].ToString();
            string url = dr["fun_uri"].ToString();
            if (!this.panel_Content.Controls.ContainsKey(menuId))
            {
                UserControl uc = CommonCtrl.TraverseForm(url);
                if (uc == null)
                {
                    MessageBoxEx.Show(menuId + " 不存在!", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                uc.Name = menuId;
                uc.Dock = DockStyle.Fill;
                uc.Focus();
                uc.Tag = menuId + "|" + dr["firstid"].ToString() + "|" + dr["parent_id"].ToString();//三级 |一级| 二级 菜单的id
                panel_Content.Controls.Add(uc);
                uc.BringToFront();
                Image myImage = null;
                if (dr["fun_img"] != DBNull.Value && dr["fun_img"].ToString().Trim().Length > 0)
                {
                    myImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(dr["fun_img"]);
                }
                childmemu(myImage, dr["fun_name"].ToString(), 
                    dr["fun_id"].ToString(), dr["parent_id"].ToString(), dr["firstid"].ToString(), "");
            }
            else
            {
                this.panel_Content.Controls[menuId].BringToFront();
                childMemuState(menuId);
            }
        }
        #endregion

        #region 退出
        protected override void panel_WinCloseBtn_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnClick(e);
            DialogResult dialogResult = MessageBoxEx.Show("确定要退出本系统吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {                   
                this.Close();
            }
        }
        #endregion

        #region 更多功能

        protected override void panel_WinShortCutBtn_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnClick(e);
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip_WinShortCutBtn.Show(panel_WinShortCutBtn, 20, 20);
            }
        }

        private void tSMenuItem_About_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void HXCMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBHelper.LoginOutput("系统登出", GlobalStaticObj.UserID);
        }

        private void tSMenuItem_UserInfo_Click(object sender, EventArgs e)
        {
            RespFunStruct respO = new RespFunStruct();
            ReqeFunStruct UserO = opUserParam();
            FunctionReturnBoolDelegateHandler funcCall = new FunctionReturnBoolDelegateHandler(Call);
            OpCommand.ShowLoadForm(GlobalStaticObj.AppMainForm, OpCommand.LForm);
            funcCall.BeginInvoke("新建用户", UserO, out respO, CallBack, funcCall);
        }

        private void CallBack(IAsyncResult ar)
        {
            RespFunStruct result = new RespFunStruct();
            FunctionReturnBoolDelegateHandler andl = (FunctionReturnBoolDelegateHandler)ar.AsyncState;
            andl.EndInvoke(out result, ar);
            bool flag = false;
            string str = bool.TryParse(result.ReturnObject, out flag) ? "新建用户成功" : "新建用户失败";
            OpCommand.closeForm(OpCommand.myFormClose, OpCommand.LForm);
            MessageBox.Show(str);
        }

        private bool Call(string opName, ReqeFunStruct request0, out RespFunStruct result)
        {
            return CommonFuncCall.TryCallWCFFunc(opName, request0, out result);
        }

        private ReqeFunStruct opUserParam()
        {
            ReqeFunStruct newUserO = new ReqeFunStruct();
            newUserO.SubSysName = SubSysName.CommonBaseFuncCall;
            newUserO.FunName = ComFunCallEnum.Submit_AddOrEdit;
            Submit_AddOrEdit addUser0 = new Submit_AddOrEdit();
            addUser0.TableName = "[sys_user]";
            Dictionary<string, string> DicParam = new Dictionary<string, string>();
            DicParam.Add("user_name", "123");
            DicParam.Add("password", "456");
            DicParam.Add("create_by", GlobalStaticObj.UserID);
            DicParam.Add("create_time", DateTime.UtcNow.Ticks.ToString());
            DicParam.Add("user_id", System.Guid.NewGuid().ToString());
            addUser0.DicParam = DicParam;
            newUserO.FunObject = addUser0;
            return newUserO;
        }
        private void lbl_SysName_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelHeadLeft_MouseDown(sender, e);
        }
        #endregion

        #region --鼠标左键拖动移动
        Point downPoint;
        private void panelHead_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }
        private void panelHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion

        #region --锁定离开
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            this.ShowInTaskbar = true;
            this.Visible = true;
        }

        private void lblLock_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = true;
            this.ShowInTaskbar = false;
            this.Visible = false;
        }
        #endregion

        #region --左侧隐藏显示改变大小
        private void splitContainerEx_Body_Panel1_SizeChanged(object sender, EventArgs e)
        {
            panelMemuStatus();
        }
        #endregion

        #region --上下移动
        private void splitter_MenuNormalFunc_SplitterMoved(object sender, SplitterEventArgs e)
        {
            int oldY = this.panel_LeftMenu.Location.Y + this.panel_LeftMenu.Height;
            this.panel_NormalFunc.Location = new Point(this.panel_NormalFunc.Location.X,
                this.panel_NormalFunc.Location.Y + e.SplitY - oldY);
            this.panel_NormalFunc.Height = this.panel_NormalFunc.Height + oldY - e.SplitY;
        }
        #endregion

        #region --数据刷新
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //return; //停用数据刷新
            this.timerRefresh.Enabled = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(LocalCache._Update), CacheList.Announce);
            ThreadPool.QueueUserWorkItem(new WaitCallback(LocalCache._Update), CacheList.Reminder);
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._GetOnline));
            this.timerRefresh.Enabled = true;
        }
        #endregion

        #region --获取在线用户
        private void _GetOnline(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询在线用户信息", "sys_user", "*", "is_online = '1'", "", "");
            if (dt != null)
            {
                this._ShowOnline(dt.Rows.Count);
            }
        }
        private delegate void ShowOnline(int count);
        private void _ShowOnline(int count)
        {
            if (this.lblcount.InvokeRequired)
            {
                ShowOnline so = new ShowOnline(this._ShowOnline);
                this.Invoke(so, count);
            }
            else
            {
                this.lblcount.Text = "在线 " + count + " 人";
            }
        }
        #endregion

        #region --快捷键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                timerRefresh_Tick(null, null);
                return true;
            }
            if (keyData == Keys.F2)
            {
                if (this.Visible)
                {
                    this.notifyIcon.Visible = true;
                    this.ShowInTaskbar = false;
                    this.Visible = false;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region --自动锁屏
        private void LockWindows(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询在线用户信息", "sys_user", "*", "", "", "");
            if (dt != null)
            {
                this._ShowOnline(dt.Rows.Count);
            }
        }
        #endregion

        #region add by kord -- 突出当前选项卡背景色
        private void SetCurrentPnlBg(Object ctrl)
        {
            if (currentPanel != null)
            {
                currentPanel.BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_E");
            }
            var pnl = ctrl as Panel;
            if (pnl == null) return;
            currentPanel = pnl;
            pnl.BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L"); 
        }
        #endregion
    }
}
