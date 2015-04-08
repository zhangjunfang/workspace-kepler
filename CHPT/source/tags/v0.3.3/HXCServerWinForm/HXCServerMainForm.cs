using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
using yuTongWebService;
using HXCServerWinForm.UCMainLayOut;
using HXC_FuncUtility;
using BLL;

namespace HXCServerWinForm
{
    public partial class HXCServerMainForm : FormExhyh
    {
        #region 属性
        /// <summary>常用功能树图标
        /// </summary>
        ImageList treeImage = new ImageList();
        private Panel currentPanel = null;
        #endregion

        /// <summary> 构造函数
        /// </summary>
        public HXCServerMainForm()
        {
            InitializeComponent();
            loadSkin();
            this.MinimumSize = new Size(1366, 660);
            this.MaximizedBounds = Screen.GetWorkingArea(this);
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary> Load加载
        /// </summary>
        private void HXCServerMainForm_Load(object sender, EventArgs e)
        {
            UCLeftMenu.MainFormAddLabel += new MainFormAddLabels(MainFormAddLabel);
            UCForm.UCBase.addUserControls += new UCForm.addUserControls(addUserControls);
            UCForm.UCBase.deleteMenu += new UCForm.deleteMenu(deleteMenu);
            lblMsg.Text = GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "  慧修车汽修汽配管理系统 -- 中交慧联版权所有";
            lblMsg.Location = new Point((panel_Bottom.Width - lblMsg.Width) / 2, (panel_Bottom.Height - lblMsg.Height) / 2);
            loadHome();// 加载首页  
            LoadUser_function();//加载常用功能
        }

        #region 事件

        #region 多页签
        #region 左侧菜单点击事件
        /// <summary>
        /// 左侧菜单点击事件
        /// </summary>
        /// <param name="fun">功能记录</param>
        /// <param name="ucLeftMenu">左侧菜单对象</param>
        public void MainFormAddLabel(DataRow fun, UCLeftMenu ucLeftMenu)
        {
            if (panel_Content.Controls.Find(fun["fun_id"].ToString(), true).Length == 0)
            {
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
                Image myImage = null;
                if (fun["fun_img"] != DBNull.Value && fun["fun_img"].ToString() != "")
                {
                    myImage = (Image)ServiceStationClient.Skin.Properties.Resources.ResourceManager.GetObject(fun["fun_img"].ToString());
                }
                childmemu(myImage, fun["fun_name"].ToString(), fun["fun_id"].ToString(), fun["parent_id"].ToString(), ucLeftMenu.Name, "");
            }
            else
            {
                panel_Content.Controls.Find(fun["fun_id"].ToString(), true)[0].BringToFront();
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
        /// <param name="thisUcTag">当前窗体Tag tag包括（三级 |一级| 二级 菜单的id）</param>
        public void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName)
        {
            if (thisUcTag != null)
            {
                string[] strArr = thisUcTag.Split('|');
                if (panel_Content.Controls.Find(menuId, true).Length == 0)
                {
                    userContol.Name = menuId;
                    userContol.Dock = DockStyle.Fill;
                    userContol.Focus();
                    userContol.Tag = menuId + "|" + strArr[1] + "|" + strArr[2];
                    panel_Content.Controls.Add(userContol);// panel_Main.Controls.Add(ucHome);
                    userContol.BringToFront();
                    Image myImage = null;
                    if (GlobalStaticObj.gLoginDataSet != null && GlobalStaticObj.gLoginDataSet.Tables[2] != null && GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
                    {
                        DataView dv = GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                        dv.RowFilter = "fun_id='" + strArr[0] + "'";
                        if (dv.Count > 0)
                        {
                            DataRowView drv = dv[0];
                            if (drv["fun_img"] != DBNull.Value && drv["fun_img"].ToString().Trim() != "")
                            {
                                myImage = (Image)ServiceStationClient.Skin.Properties.Resources.ResourceManager.GetObject(drv["fun_img"].ToString());
                            }
                        }
                    }
                    childmemu(myImage, memuName, menuId, strArr[2], strArr[1], PUCName);
                }
                else
                {
                    panel_Content.Controls.Find(menuId, true)[0].BringToFront();
                    childMemuState(menuId);
                }
                SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
            }
        }
        #endregion

        #region 添加多页签
        /// <summary>
        /// 添加多页签
        /// </summary>
        /// <param name="myImage">图标</param>
        /// <param name="ppid">三级级菜单名</param> 
        /// <param name="ppid">三级级菜单id</param>
        ///  <param name="ppid">所属二级菜单id</param>
        ///   <param name="ppid">所属一级菜单id</param>
        /// <param name="fun">FunctionEntity</param>
        ///  <param name="PUCName">父窗体Name</param>
        private void childmemu(Image myImage, string memuName, string funid, string pid, string ppid, string PUCName)// DataRow fun)
        {
            string STR_CS_MEMU_NAME = funid;//控件名
            //TagValue 当前选中的 三级菜单|一级菜单|二级菜单
            string TagValue = STR_CS_MEMU_NAME + "|" + ppid + "|" + pid + "|" + PUCName;
            var plecd = new Panel();       //二级菜单Panel                    
            plecd.Name = STR_CS_MEMU_NAME;
            plecd.Tag = TagValue;
            plecd.Width = 50;
            plecd.Height = 23;
            if (panelMemu.Controls.Count > 0)
            {
                var pnl = panelMemu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MEMU, true)[0] as Panel;
                plecd.Left = pnl.Left + pnl.Width + 1;
            }
            else
            {
                plecd.Left = 0;
            }
            plecd.Top = 0;
            plecd.Click += new EventHandler(plecd_Click);
            plecd.MouseEnter += plecd_MouseMove;
            plecd.MouseLeave += new EventHandler(plecd_MouseLeave);
            plecd.BorderStyle = BorderStyle.None;
            #region add by kord -- 为了突出当前选项卡背景色
            SetCurrentPnlBg(plecd);
            #endregion
            //GraphicsPath gp = new GraphicsPath();
            //int width = plecd.Width;
            //int height = plecd.Height;
            //gp.AddArc(-1, -1, 20, 20, 180, 90);
            //gp.AddArc(width - 20 - 1, -1, 20, 20, 270, 90);
            //gp.AddArc(width - 20 - 1, height - 20 - 1, 20, 20, 0, 90);
            //gp.AddArc(-1, height - 20 - 1, 20, 20, 90, 90);
            //gp.CloseAllFigures();
            //Region region = new Region(gp);
            //plecd.Region = region;
            //gp.Dispose();
            //region.Dispose();
            int PnlIndex = 0;
            if (panelMemu.Controls.Count > 0)
            {
                PnlIndex = panelMemu.Controls.GetChildIndex(panelMemu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MEMU, true)[0]);
            }
            panelMemu.Controls.Add(plecd);
            if (panelMemu.Controls.Count > 1)
            {
                panelMemu.Controls.SetChildIndex(plecd, PnlIndex + 1);
            }


            PictureBox pic = new PictureBox();    //二级菜单小图标
            pic.Cursor = Cursors.Default;
            pic.Left = 5;
            pic.Top = 8;
            pic.Width = 12;
            pic.Height = 12;
            pic.Tag = TagValue;
            if (myImage != null)
                pic.Image = myImage;
            pic.MouseEnter += pic_MouseMove;
            pic.MouseLeave += new EventHandler(pic_MouseLeave);
            pic.Click += new EventHandler(pic_Click);
            plecd.Controls.Add(pic);

            Label lbl = new Label(); //二级菜单名称
            lbl.Cursor = Cursors.Default;
            lbl.BackColor = Color.Transparent;
            lbl.AutoSize = true;
            lbl.Left = 22;
            lbl.Top = 8;
            lbl.Text = memuName; //菜单名
            lbl.Tag = TagValue;
            lbl.MouseEnter += lbl_MouseMove;
            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
            lbl.Click += new EventHandler(lbl_Click);
            plecd.Controls.Add(lbl);

            plecd.Width = 22 + lbl.Width + 12 + 5;

            var ibtn = new ImageButton { Name = "ibtn" + STR_CS_MEMU_NAME, Cursor = Cursors.Hand, Width = 8, Height = 8 };  //二级菜单关闭 x
            ibtn.Left = plecd.Width - ibtn.Width - 5;
            ibtn.Top = 5;
            ibtn.Tag = TagValue;
            ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.TabPage_Close_E;
            ibtn.MouseEnter += ibtn_MouseMove;
            ibtn.MouseLeave += new EventHandler(ibtn_MouseLeave);
            if (STR_CS_MEMU_NAME == SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE)
            {
                ibtn.Visible = false;
            }
            else
            {
                ibtn.Click += new EventHandler(ibtn_Click);
            }
            plecd.Controls.Add(ibtn);

            childMemuState(STR_CS_MEMU_NAME);

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
        /// <summary> 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        private void deleteMenu(string strTag)
        {
            int pnlIndex = 0;
            string[] strArr = strTag.Split('|');
            if (panelMemu.Controls.Count > 1)
            {
                var pnl = panelMemu.Controls.Find(strArr[0], true)[0] as Panel;
                pnlIndex = panelMemu.Controls.GetChildIndex(pnl);

            }
            panel_Content.Controls.Find(strArr[0], true)[0].Dispose();
            panelMemu.Controls.Find(strArr[0], true)[0].Dispose();
            if (strArr[0] == SYSModel.clsSysConfig.STR_CURR_MEMU)
            {
                if (pnlIndex > 0)
                {
                    var px = panelMemu.Controls[pnlIndex - 1] as Panel;
                    strArr = px.Tag.ToString().Split('|');
                    UCLeftMenu uc = panel_LeftMenu.Controls.Find(strArr[1], false)[0] as UCLeftMenu;
                    uc.BringToFront();
                    childMemuState(strArr[0]);
                    SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                    ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                    uc.LeftMenuState(strArr[2], strArr[0]);
                    SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                }
                else
                {
                    if (panelMemu.Controls.Count > 0)
                    {
                        pnlIndex = 0;
                        var px = panelMemu.Controls[pnlIndex] as Panel;
                        strArr = px.Tag.ToString().Split('|');
                        UCLeftMenu uc = panel_LeftMenu.Controls.Find(strArr[1], false)[0] as UCLeftMenu;
                        uc.BringToFront();
                        childMemuState(strArr[0]);
                        SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                        ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                        uc.LeftMenuState(strArr[2], strArr[0]);
                        SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                    }
                }
            }
            else
            {
                childMemuState(SYSModel.clsSysConfig.STR_CURR_MEMU);//(SYSModel.clsSysConfig.STR_CURR_MEMU);
            }
        }

        /// <summary> 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        /// <param name="PUCName">父窗体name</param>
        private void deleteMenu(string strTag, string PUCName)
        {
            int pnlIndex = 0;
            string[] strArr = strTag.Split('|');
            if (panelMemu.Controls.Count > 1)
            {
                var pnl = panelMemu.Controls.Find(strArr[0], true)[0] as Panel;
                pnlIndex = panelMemu.Controls.GetChildIndex(pnl);
            }
            Control[] colArr = panel_Content.Controls.Find(strArr[0], true);
            if (colArr.Length > 0)
            {
                colArr[0].Dispose();
            }
            panelMemu.Controls.Find(strArr[0], true)[0].Dispose();// panel_Main.Controls.Find(name, true)[0].Dispose();

            if (PUCName != "" && panelMemu.Controls.Find(PUCName, true).Length > 0)
            {
                string name = panelMemu.Controls.Find(PUCName, true)[0].Tag.ToString();
                string[] Arr = name.Split('|');
                if (Arr[1] != "")
                {
                    UCLeftMenu uc = (UCLeftMenu)panel_LeftMenu.Controls.Find(Arr[1], true)[0];
                    uc.BringToFront();
                    panel_Content.Controls.Find(Arr[0], true)[0].BringToFront();
                    SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                    ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                    uc.LeftMenuState(Arr[2], Arr[0]);
                    SYSModel.clsSysConfig.STR_CURR_TWOMEMU = Arr[2];
                }
                childMemuState(Arr[0]);
            }
            else
            {
                if (strArr[0] == SYSModel.clsSysConfig.STR_CURR_MEMU)
                {
                    if (pnlIndex > 0)
                    {
                        var px = panelMemu.Controls[pnlIndex - 1] as Panel;
                        strArr = px.Tag.ToString().Split('|');
                        UCLeftMenu uc = panel_LeftMenu.Controls.Find(strArr[1], false)[0] as UCLeftMenu;
                        uc.BringToFront();
                        childMemuState(strArr[0]);
                        SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                        ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                        uc.LeftMenuState(strArr[2], strArr[0]);
                        SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                    }
                    else
                    {
                        if (panelMemu.Controls.Count > 0)
                        {
                            pnlIndex = 0;
                            var px = panelMemu.Controls[pnlIndex] as Panel;
                            strArr = px.Tag.ToString().Split('|');
                            UCLeftMenu uc = panel_LeftMenu.Controls.Find(strArr[1], false)[0] as UCLeftMenu;
                            uc.BringToFront();
                            childMemuState(strArr[0]);
                            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                            ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态
                            uc.LeftMenuState(strArr[2], strArr[0]);
                            SYSModel.clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                        }
                    }
                }
                else
                {
                    (panel_LeftMenu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MAINMEMU, true)[0] as UCLeftMenu).LeftMenuState(SYSModel.clsSysConfig.STR_CURR_TWOMEMU, SYSModel.clsSysConfig.STR_CURR_MEMU);
                    childMemuState(SYSModel.clsSysConfig.STR_CURR_MEMU);//(SYSModel.clsSysConfig.STR_CURR_MEMU);
                }
            }
        }
        #endregion

        #region 标签菜单鼠标进入事件
        void ibtn_MouseMove(object sender, EventArgs e)
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

        void lbl_MouseMove(object sender, EventArgs e)
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

        void pic_MouseMove(object sender, EventArgs e)
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
        void plecd_MouseMove(object sender, EventArgs e)
        {
            (sender as Control).BackgroundImage = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("TabPage_L");
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
        /// 标签菜单单击事件
        /// </summary>
        void pic_Click(object sender, EventArgs e)
        {
            this.SetMenu(sender);
        }

        /// <summary>
        /// 标签菜单单击事件
        /// </summary>
        void plecd_Click(object sender, EventArgs e)
        {
            this.SetMenu(sender);

        }

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

                var name = ctl.Tag.ToString();
                var strArr = name.Split('|');
                if (strArr.Length <= 1 || strArr[1].Length <= 0)
                {
                    return;
                }
                var ctls = panel_LeftMenu.Controls.Find(strArr[1], false);
                if (ctls.Length > 0)
                {
                    var uc = (UCLeftMenu)ctls[0];
                    uc.BringToFront();
                    clsSysConfig.STR_CURR_MAINMEMU = uc.FunID;
                }
                clsSysConfig.STR_CURR_TWOMEMU = strArr[2];
                panel_Content.Controls.Find(strArr[0], false)[0].BringToFront();
                childMemuState(strArr[0]);
            }
            catch (Exception ex)
            {
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

            //if (sptMain.Panel2.Controls.Find(pname, true).Count() > 0)
            //{
            //    Control CurrControl = sptMain.Panel2.Controls.Find(pname, true)[0] as Control;
            //    SplitContainer spt = CurrControl.Parent.Parent as SplitContainer;

            //    if (sptMainTop.SplitterDistance == 20)
            //    {
            //        sptMainTop.SplitterDistance = 20;
            //        spt.SplitterDistance = 0;
            //    }
            //    else
            //    {
            //        sptMainTop.SplitterDistance = 260;
            //        spt.SplitterDistance = 260;
            //    }
            //}      
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
            if (panel_LeftMenu.Controls.Find(menuId, true).Length == 0)
            {
                UCMainLayOut.UCLeftMenu UCLeftMenu = new UCMainLayOut.UCLeftMenu(); //系统管理
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
                UCMainLayOut.UCLeftMenu UCLeftMenu = panel_LeftMenu.Controls.Find(menuId, true)[0] as UCMainLayOut.UCLeftMenu;
                UCLeftMenu.LeftMenuState(SYSModel.clsSysConfig.STR_CURR_TWOMEMU, SYSModel.clsSysConfig.STR_CURR_MEMU, SYSModel.clsSysConfig.STR_CURR_MEMU);
                UCLeftMenu.BringToFront();
            }
        }
        #endregion

        #region 常用功能
        /// <summary> 常用功能 点击事件
        /// </summary>
        private void tvUser_function_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DataRow dr = e.Node.Tag as DataRow;
            SYSModel.clsSysConfig.STR_CURR_TWOMEMU = dr["parent_id"].ToString();//二级菜单id
            string threeMenu = e.Node.Name;//三级菜单
            Control[] colsOne = panel_LeftMenu.Controls.Find(SYSModel.clsSysConfig.STR_CURR_MAINMEMU, true);
            if (colsOne.Length > 0)
            {
                UCLeftMenu uCLeftMenu = colsOne[0] as UCLeftMenu;
                Control[] colsTwo = panel_LeftMenu.Controls.Find(threeMenu, true);
                if (colsTwo.Length > 0)
                {
                    PanelEx pnlEx = colsTwo[0] as PanelEx;
                    pnlEx.Tag = dr;
                    uCLeftMenu.Pleft_panel_tiem_top_Click(pnlEx, null);
                }
            }
        }
        #endregion

        #region 基类窗体事件
        protected override void panel_WinShortCutBtn_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnClick(e);
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip_WinShortCutBtn.Show(panel_WinShortCutBtn, 20, 20);
            }
        }
        #endregion

        #region 窗体事件
        /// <summary> 退出，隐藏到托盘
        /// </summary>
        protected override void panel_WinCloseBtn_MouseClick(object sender, MouseEventArgs e)
        {
            base.OnClick(e);
            DialogResult dialogResult = MessageBoxEx.Show("系统即将隐藏到托盘，是否继续？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                this.notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloonTip(30, "注意：", "程序已最小化", ToolTipIcon.Info);
            }
        }

        /// <summary> 显示窗体
        /// </summary>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                    this.WindowState = FormWindowState.Maximized;
                }
            }
        }

        /// <summary> 窗体大小改变事件
        /// </summary>
        private void HXCServerMainForm_SizeChanged(object sender, EventArgs e)
        {
            panel_Body.Location = new Point(5, 72);
            panel_Body.Size = new Size(this.Width - 10, this.Height - 77);
            lblMsg.Location = new Point((panel_Bottom.Width - lblMsg.Width) / 2, (panel_Bottom.Height - lblMsg.Height) / 2);
        }

        /// <summary> 窗体关闭时
        /// </summary>
        private void HXCServerMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BLL.ServerUser.UserLoginOut(GlobalStaticObj_Server.Instance.UserID,GlobalStaticObj_Server.CommAccCode);
            Environment.Exit(-1);
        }
        #endregion

        #region 窗体拖拽
        /// <summary> Logo按下
        /// </summary>
        private void picLogo_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelHeadLeft_MouseDown(sender, e);
        }

        /// <summary> 标题按下事件
        /// </summary>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelHeadLeft_MouseDown(sender, e);
        }

        /// <summary> 系统名称按下
        /// </summary>
        private void lbl_SysName_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelHeadLeft_MouseDown(sender, e);
        }
        #endregion

        #region 功能菜单
        /// <summary> 系统维护
        /// </summary>
        private void ucMenu_SysMaintenanceClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenu.SysMaintenance_Tag, "系统维护");
        }

        /// <summary> 系统监控
        /// </summary>
        private void ucMenu_SysMonitorClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenu.SysMonitor_Tag, "系统监控");
        }

        /// <summary> 权限管理
        /// </summary>
        private void ucMenu_PermissionManageClick(object sender, EventArgs e)
        {
            AddLeftMenu(ucMenu.PermissionManage_Tag, "权限管理");
        }

        /// <summary> 用户信息
        /// </summary>
        private void tSMenuItem_UserInfo_Click(object sender, EventArgs e)
        {
            HXCServerWinForm.UCForm.Personnel.UCPersonnelView ucform = new UCForm.Personnel.UCPersonnelView();
            ucform.Dock = DockStyle.Fill; ucform.windowStatus = HXCServerWinForm.UCForm.WindowStatus.View;
            ucform.pnlOpt.Visible = false;
            ucform.id = GlobalStaticObj_Server.Instance.UserID;
            FormEx frm = new FormEx();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "用户信息-查看";
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ShowInTaskbar = false;
            if (frm.Controls.ContainsKey("pnlContainer"))
            {
                Control col = frm.Controls["pnlContainer"];
                frm.Size = ucform.Size;
                frm.Height += 30;
                frm.Width += 30;
                col.Controls.Add(ucform);
                frm.ShowDialog();
            }

        }

        /// <summary> 重置密码
        /// </summary>
        private void tSMenuItem_PWDReSet_Click(object sender, EventArgs e)
        {
            string msg = string.Format("将要重置当前用户{0}（{1})密码），\r\n是否继续？", GlobalStaticObj_Server.Instance.UserName, GlobalStaticObj_Server.Instance.LoginName);
            DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                dicFields.Add("password", "123456");
                bool flag = DBHelper.Submit_AddOrEdit("重置密码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_user", "land_name", GlobalStaticObj_Server.Instance.LoginName, dicFields);
                if (flag)
                {
                    MessageBoxEx.Show("重置成功", "系统提示");
                    return;
                }
                MessageBoxEx.Show("重置成功失败", "系统提示");
            }
        }

        /// <summary> 关于
        /// </summary>
        private void tSMenuItem_About_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new FormAbout();
            frmAbout = new FormAbout();
            frmAbout.ShowDialog();
        }

        /// <summary> 注销
        /// </summary>
        private void tSMenuItem_LogOut_Click(object sender, EventArgs e)
        {
            BLL.ServerUser.UserLoginOut(GlobalStaticObj_Server.Instance.UserID, GlobalStaticObj_Server.CommAccCode);
            Environment.Exit(-1);
        }

        /// <summary> 注册鉴权
        /// </summary>
        private void tSMenuItem_RegA_Click(object sender, EventArgs e)
        {
            UCForm.frmSoftReg regForm = new UCForm.frmSoftReg();
            regForm.StartPosition = FormStartPosition.CenterParent;
            Point ML = GlobalStaticObj.AppMainForm.Location;
            Point TL = new Point(ML.X + (GlobalStaticObj.AppMainForm.Width - regForm.Width) / 2, ML.Y + (GlobalStaticObj.AppMainForm.Height - regForm.Height) / 2);
            regForm.Location = TL;
            regForm.Show(GlobalStaticObj.AppMainForm);
        }
        #endregion

        /// <summary> 状态条更新
        /// </summary>
        private void tmrInfo_Tick(object sender, EventArgs e)
        {
            GlobalStaticObj_Server.Instance.CurrentDateTime = GlobalStaticObj_Server.Instance.CurrentDateTime.AddSeconds(1);
            lblMsg.Text = GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "  慧修车汽修汽配管理系统 -- 中交慧联版权所有";
        }
        #endregion

        #region 方法
        /// <summary> 加载皮肤
        /// </summary>
        private void loadSkin()
        {
            //splitContainerEx_Body.SplitterWidth = 5;
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

        /// <summary>加载常用功能
        /// </summary>
        private void LoadUser_function()
        {
            if (GlobalStaticObj.gLoginDataSet != null && GlobalStaticObj.gLoginDataSet.Tables[1] != null && GlobalStaticObj.gLoginDataSet.Tables[1].Rows.Count > 0)
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
                        treeImage.Images.Add(dr["fun_img"].ToString(), (Image)ServiceStationClient.Skin.Properties.Resources.ResourceManager.GetObject(dr["fun_img"].ToString()));
                        node.ImageKey = dr["fun_img"].ToString();
                        node.SelectedImageKey = dr["fun_img"].ToString();
                    }
                    tvUser_function.Nodes.Add(node);
                }
            }
        }

        /// <summary> 加载首页
        /// </summary>
        private void loadHome()
        {
            splitContainerEx_Body.SplitterWidth = 6;
            SYSModel.clsSysConfig.STR_CURR_MAINMEMU = "S_SysMaintenance";
            if (panel_LeftMenu.Controls.Find("S_SysMaintenance", true).Length == 0)
            {
                UCMainLayOut.UCLeftMenu UCLeftMenu = new UCMainLayOut.UCLeftMenu(); //系统管理
                UCLeftMenu.Name = "S_SysMaintenance";//EntityModel.clsSysConfig.STR_CS_MEMU_FUNCTIONMANAGE;
                // UCLeftMenu.dtFunction = dt;
                UCLeftMenu.FunID = "S_SysMaintenance";
                //UCLeftMenu.panel_MainHeight = pnlxLeftMenu.Height;
                UCLeftMenu.Dock = DockStyle.Fill;
                UCLeftMenu.Tag = "系统管理";
                UCLeftMenu.menuName = "系统管理";
                UCLeftMenu.FmMain = this;
                UCLeftMenu.Focus();
                panel_LeftMenu.Controls.Add(UCLeftMenu);
                UCLeftMenu.BringToFront();

                SYSModel.clsSysConfig.STR_CURR_MAINMEMU = UCLeftMenu.FunID;
                ucMenu.MenuButtonStatus();//三级菜单选中时 一级菜单的选中状态

                UCForm.UUCHomePage ucHome = new UCForm.UUCHomePage(); //首页
                // home.ID = "STR_" + STR_CS_MEMU_NAME;
                ucHome.ID = "STR_" + SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE;
                ucHome.Name = SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE;
                ucHome.Dock = DockStyle.Fill;
                ucHome.Focus();
                panel_Content.Controls.Add(ucHome);// panel_Main.Controls.Add(ucHome);
                ucHome.BringToFront();
                childmemu(null, "首页", SYSModel.clsSysConfig.STR_CS_MEMU_HOMEMANAGE, "", UCLeftMenu.Name, "");
            }
            else
            {
                (panel_LeftMenu.Controls.Find("CL_SystemManagement", true)[0] as UCMainLayOut.UCLeftMenu).BringToFront();
            }
        }

        #endregion

        #region 宇通接口定时器
        private void timer_YT_Tick(object sender, EventArgs e)
        {
            timer_YT.Interval = 12 * 60 * 60 * 1000;//12个小时
            string flag = timer_YT.Tag.ToString();
            if (flag == "1")
            {
                timer_YT.Tag = "0";
                //批量执行宇通增量接口
                //执行宇通增量接口时更细对应的时间
                YTInterface();
            }
            else
            {
                timer_YT.Tag = "1";
            }
        }

        /// <summary>执行宇通接口
        /// </summary>
        public void YTInterface()
        {
            SysConfig sysConfig = new SysConfig();
            string errMsg = string.Empty;
            string lastTime = sysConfig.GetLastTime("CustomerLastTime");//车辆客户最后更新时间
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadCustomer(lastTime);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
            lastTime = sysConfig.GetLastTime("ContactLastTime");//联系人最后更新时间
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadContact(lastTime, "01");
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
            lastTime = sysConfig.GetLastTime("UserLastTime");//用户最后更新时间
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadContact(lastTime, "02");
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Show(errMsg);
            }
            lastTime = sysConfig.GetLastTime("BusLastTime");//车辆最后更新时间
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadBus(lastTime);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
            lastTime = sysConfig.GetLastTime("PartLastTime");//配件最后更新时间
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadPart(lastTime);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
            lastTime = sysConfig.GetLastTime("ProdImprovement");//产品改进号
            errMsg = yuTongWebService.WebServ_YT_BasicData.LoadProdImprovement(lastTime);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
            }
            MessageBox.Show("增量更新完成");
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
            pnl.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion
    }
}
