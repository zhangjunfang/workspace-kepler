using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.Skin;
using SYSModel;
using ServiceStationClient.ComponentUI;
using Model;
using HXC_FuncUtility;
namespace HXCServerWinForm.UCMainLayOut
{
    public delegate void MainFormAddLabels(DataRow fun, UCLeftMenu ucLeftMenu);//定义委托 
    public partial class UCLeftMenu : UserControl
        {
            #region 属性设置
            public static MainFormAddLabels MainFormAddLabel;//定义委托实例  
            /// <summary> 一级菜单id
            /// </summary>
            public string FunID;
            /// <summary> 主窗体内容区高度  
            /// </summary>
            public int panel_MainHeight = 0;
            /// <summary> 一级菜单名
            /// </summary>
            public string menuName = "";
            /// <summary> 父窗体
            /// </summary>
            public HXCServerMainForm FmMain;
            /// <summary> 当前选中的二级菜单
            /// </summary>
            string str_Curr_Item = "";
            /// <summary> 左侧宽度
            /// </summary>
            int LeftWidth = 236;
            /// <summary> 每个顶部panel 的高度
            /// </summary>
            int item_top_height = 25;
            /// <summary> 是否是加载 初始是true  load事件后为false
            /// </summary>
            bool IsLoad = true;
            /// <summary> 树图标列表
            /// </summary>
            ImageList treeImage = new ImageList();
            ///// <summary> 没有三级菜单的 选中图标
            ///// </summary>
            //Image iChecked = ServiceStationClient.Skin.Properties.Resources.page_last_enable;
            ///// <summary> 没有三级菜单的 未选中图标
            ///// </summary>
            //Image iNoChecked = ServiceStationClient.Skin.Properties.Resources.page_last_down;
            #endregion

            public UCLeftMenu()
            {
                InitializeComponent();
                //功能树图标  尺寸大小
                treeImage.ImageSize = new Size(12, 12);    
            }

            #region Load
            private void UCLeftMenu_Load(object sender, EventArgs e)
            {
                //lblMemuName.Text = menuName;
                DataTable dtFirstChilds = new DataTable();//第一个二级菜单下的子菜单
                DataTable dt = CommonCtrl.getDataChildsByPid(FunID);
                if (dt != null && dt.Rows.Count > 0)//result.Count() > 0)
                {
                    int next_top = 0;
                    int Pleft_panelHeight = panel_MainHeight; //panel的高度   
                    int Pleft_panel_count = dt.Rows.Count; //二级菜单个数
                    int Pleft_panel_item_height = Pleft_panelHeight - (Pleft_panel_count * item_top_height) + item_top_height; //二级菜单高度-展开的
                    int tvex_height = Pleft_panel_item_height - item_top_height; //菜单列表高度-展开的高度 三级菜单

                    str_Curr_Item = dt.Rows[0]["fun_id"].ToString();// result[0].fun_id;
                    int i = 0;
                    int pHeight = Pleft_panel_item_height;
                    int tHeight = tvex_height;

                    foreach (DataRow fun in dt.Rows)
                    {
                        DataTable dtChilds = CommonCtrl.getDataChildsByPid(fun["fun_id"].ToString());
                        LeftMenu(fun["fun_name"].ToString(), fun["fun_id"].ToString(), dtChilds, next_top, pHeight, tHeight, fun);
                        if (i == 0)
                        {
                            dtFirstChilds = dtChilds;
                            pHeight = item_top_height;
                            tHeight = 0;
                            next_top += Pleft_panel_item_height;
                        }
                        else
                        {
                            next_top += item_top_height;
                        }
                        i++;
                    }
                }
                if (IsLoad && dtFirstChilds.Rows.Count == 0)
                {
                    str_Curr_Item = "";
                }
                LeftMenuState(str_Curr_Item, "");
                IsLoad = false;
            }
            #endregion

            #region 添加菜单控件
            /// <summary>添加菜单控件
            /// </summary>
            /// <param name="LeftMemuName">菜单名称</param>
            /// <param name="STR_CS_LEFT_MEMU_NAME">fun_id</param>
            /// <param name="childList">二级菜单列表</param>
            /// <param name="next_top">下一个坐标top</param>
            /// <param name="pHeight">二级菜单高度-展开的</param>
            /// <param name="tHeight">菜单列表高度-展开的高度 三级菜单</param>
            /// <param name="SecondMenu">二级菜单DataRow</param>
            private void LeftMenu(string LeftMemuName, string STR_CS_LEFT_MEMU_NAME, DataTable childList, int next_top, int pHeight, int tHeight, DataRow SecondMenu)
            {

                PanelEx Pleft_panel_tiem = new PanelEx();       //二级菜单Panel                    
                Pleft_panel_tiem.Name = STR_CS_LEFT_MEMU_NAME;
                Pleft_panel_tiem.BackColor = Color.FromArgb(219, 236, 246);//.White;//.Silver;
                //Pleft_panel_tiem.Location = new Point(0, 0);
                Pleft_panel_tiem.Width = LeftWidth;
                Pleft_panel_tiem.Height = pHeight;
                Pleft_panel_tiem.BorderStyle = BorderStyle.FixedSingle;
                Pleft_panel_tiem.BorderWidth = 0;
                Pleft_panel_tiem.Top = next_top;

                Pleft_panel_tiem.Left = 0;
                //plecd.Anchor = AnchorStyles.Left;
                this.Controls.Add(Pleft_panel_tiem);

                PanelEx Pleft_panel_tiem_top = new PanelEx();
                Pleft_panel_tiem_top.Name = "top_" + STR_CS_LEFT_MEMU_NAME;
                Pleft_panel_tiem_top.BackColor = Color.FromArgb(233, 243, 253);// Color.LightSkyBlue;
                Pleft_panel_tiem_top.BorderWidth = 1;
                Pleft_panel_tiem_top.BorderColor = Color.FromArgb(190, 213, 243);
                Pleft_panel_tiem_top.Location = new Point(0, 0);
                Pleft_panel_tiem_top.Width = LeftWidth;
                Pleft_panel_tiem_top.Height = item_top_height;
                Pleft_panel_tiem_top.Tag = SecondMenu;
                Pleft_panel_tiem_top.BorderStyle = BorderStyle.FixedSingle;
                if (childList != null && childList.Rows.Count == 0)
                {
                    Pleft_panel_tiem_top.Click += new EventHandler(Pleft_panel_tiem_top_Click);
                }
                Pleft_panel_tiem.Controls.Add(Pleft_panel_tiem_top);

                var image = new PictureBox();
                image.Location = new Point(4,6);
                image.Image = SkinAssistant.GetResourcesImage(STR_CS_LEFT_MEMU_NAME, true);
                image.SizeMode = PictureBoxSizeMode.AutoSize;
                Pleft_panel_tiem_top.Controls.Add(image);

                Label lbl = new Label(); //菜单名称
                lbl.Name = "lbl_" + STR_CS_LEFT_MEMU_NAME;
                lbl.ForeColor = Color.FromArgb(20, 129, 194);
                lbl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.Cursor = Cursors.Default;
                lbl.BackColor = Color.Transparent;
                lbl.AutoSize = true;
                lbl.Left = 16;
                lbl.Top = 6;
                lbl.Text = LeftMemuName; //菜单名
                lbl.Tag = SecondMenu; ;
                if (childList != null && childList.Rows.Count == 0)
                {
                    lbl.Click += new EventHandler(lbl_Click);
                }
                Pleft_panel_tiem_top.Controls.Add(lbl);



                if (childList != null && childList.Rows.Count == 0)
                {
                    PictureBox pb = new PictureBox();
                    pb.Name = "pb_" + STR_CS_LEFT_MEMU_NAME;
                    pb.Width = 24;
                    pb.Height = 24;
                    pb.Left = LeftWidth - pb.Width - 2;
                    pb.Top = 3;
                    if (STR_CS_LEFT_MEMU_NAME == str_Curr_Item)
                    {
                        pb.Image = null;
                    }
                    else
                    {
                        pb.Image = null;
                    }
                    Pleft_panel_tiem_top.Controls.Add(pb);
                }
                else if (childList != null && childList.Rows.Count > 0)
                {
                    ServiceStationClient.ComponentUI.ImageButton ibtn = new ImageButton();  //展开折叠
                    ibtn.Name = "ibtn_" + STR_CS_LEFT_MEMU_NAME;
                    ibtn.Cursor = Cursors.Hand;
                    ibtn.Width = 24;
                    ibtn.Height = 24;
                    ibtn.Left = LeftWidth - ibtn.Width - 2;
                    ibtn.Top = 3;
                    if (STR_CS_LEFT_MEMU_NAME == str_Curr_Item)
                    {
                        ibtn.Tag = "1";
                        ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_delete;
                    }
                    else
                    {
                        ibtn.Tag = "0";
                        ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_add;
                    }
                    ibtn.Click += new EventHandler(ibtn_Click);
                    Pleft_panel_tiem_top.Controls.Add(ibtn);

                    TreeViewEx tvex = new TreeViewEx();
                    tvex.Name = "tv_" + STR_CS_LEFT_MEMU_NAME;
                    tvex.ForeColor = Color.FromArgb(20, 129, 194);
                    tvex.Indent = 8;
                    tvex.ItemHeight = 21;
                    tvex.Location = new Point(0, Pleft_panel_tiem_top.Height);
                    tvex.Width = LeftWidth;
                    tvex.Height = tHeight;
                    tvex.BorderStyle = BorderStyle.None;
                    tvex.Scrollable = true;
                    tvex.ShowLines = false;
                    tvex.Cursor = System.Windows.Forms.Cursors.Hand;
                    tvex.ImageList = treeImage;
                    Pleft_panel_tiem.Controls.Add(tvex);
                    if (childList != null && childList.Rows.Count > 0)
                    {
                        #region modify by kord
                        //foreach (DataRow fun in childList.Rows)
                        //{
                        //    TreeNode node = new TreeNode();
                        //    node.Tag = fun;
                        //    node.Text = fun["fun_name"].ToString();
                        //    node.Name = fun["fun_id"].ToString();
                        //    if (fun["fun_img"] != DBNull.Value && fun["fun_img"].ToString().Trim() != "")
                        //    {
                        //        treeImage.Images.Add(fun["fun_img"].ToString(), (Image)ServiceStationClient.Skin.Properties.Resources.ResourceManager.GetObject(fun["fun_img"].ToString()));
                        //        node.ImageKey = fun["fun_img"].ToString();
                        //        node.SelectedImageKey = fun["fun_img"].ToString();
                        //    }
                        //    tvex.Nodes.Add(node);
                        //}

                        foreach (DataRow fun in childList.Rows)
                        {
                            TreeNode node = new TreeNode();

                            node.Tag = fun;
                            node.Text = fun["fun_name"].ToString();
                            node.Name = fun["fun_id"].ToString();
                            if (fun["fun_img"] != DBNull.Value && fun["fun_img"].ToString().Trim() != "")
                            {
                                treeImage.Images.Add(fun["fun_img"].ToString(), ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(fun["fun_img"], true));
                                node.ImageKey = fun["fun_img"].ToString();
                                node.SelectedImageKey = fun["fun_img"].ToString();
                            }
                            tvex.Nodes.Add(node);
                        }
                        #endregion
                    }
                    //tvex.ItemDrag += new ItemDragEventHandler(tvex_ItemDrag);
                    tvex.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvex_NodeMouseClick);
                }
            }
            #endregion

            #region 拖拽node到常用工能
            /// <summary> 当鼠标拖拽某节点时（两个控件的该事件都指向这个方法）
            /// </summary>
            void tvex_ItemDrag(object sender, ItemDragEventArgs e)
            {
                if (e.Item is TreeNode && e.Button == System.Windows.Forms.MouseButtons.Left &&
                   e.Item != null && sender is TreeViewEx)
                {
                    //TreeView trv = sender as TreeView;
                    //TreeNode node = e.Item as TreeNode;
                    //if (node.Parent != null && trv.Tag != null)
                    //{
                    //    int value = Convert.ToInt32(trv.Tag);
                    //    MoveTo = (MoveTreeView)value;
                    //    DragNode = node;
                    //    NodeSource = trv;
                    //    trv.DoDragDrop(node, DragDropEffects.Move);
                    //}
                    TreeViewEx tv = sender as TreeViewEx;
                    tv.DoDragDrop(e.Item, DragDropEffects.Copy);
                }

            }
            #endregion

            #region 左侧菜单状态
            /// <summary>
            /// 左侧菜单状态 折叠 展开 选中 未选
            /// </summary>
            /// <param name="pname">展开的功能名称name(二级菜单)</param>
            /// <param name="threeMenu">三级菜单</param>
            public void LeftMenuState(string pname, string threeMenu)
            {
                int Pleft_panelHeight = panel_MainHeight;//panel的高度
                if (this.Controls.Count > 0)
                {
                    int Pleft_panel_count = this.Controls.Count;// -1; //二级菜单个数
                    int Pleft_panel_item_height = Pleft_panelHeight - (Pleft_panel_count * item_top_height) + item_top_height; //二级菜单高度-展开的
                    int tvex_height = Pleft_panel_item_height - item_top_height; //菜单列表高度-展开的

                    int next_top = 0;
                    int i = 0;
                    foreach (PanelEx ctl in this.Controls)
                    {
                        PanelEx ctl_top = ctl.Controls.Find("top_" + ctl.Name, true)[0] as PanelEx;
                        ctl.Width = LeftWidth;
                        ctl_top.Width = LeftWidth;
                        //ctl_top.Height=item_top_height;
                        if (ctl.Controls.Find("ibtn_" + ctl.Name, true).Count() > 0)
                        {
                            ImageButton ibtn = ctl_top.Controls.Find("ibtn_" + ctl.Name, true)[0] as ImageButton;
                            TreeViewEx tvex = ctl.Controls.Find("tv_" + ctl.Name, true)[0] as TreeViewEx;

                            ibtn.Left = LeftWidth - ibtn.Width - 1;
                            tvex.Width = LeftWidth;

                            if (ctl.Name == pname)
                            {
                                ctl.Height = Pleft_panel_item_height;
                                tvex.Visible = true;
                                tvex.Height = tvex_height;
                                if (tvex.GetNodeCount(true) > 0)
                                {
                                    for (int j = 0; j < tvex.Nodes.Count; j++)
                                    {
                                        if (tvex.Nodes[j].Name == threeMenu)
                                        {
                                            tvex.Focus();
                                            //tvex.Nodes[j].BackColor = Color.FromArgb(51, 153, 255);
                                            tvex.SelectedNode = tvex.Nodes[j];//选中
                                            break;
                                        }
                                        //else
                                        //{
                                        //    //未选中
                                        //    tvex.Nodes[j].BackColor = Color.White;
                                        //}
                                    }
                                }
                                ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_delete;
                                ibtn.Tag = "1";
                            }
                            else
                            {
                                if (pname == "" && i == 0)
                                {
                                    ctl.Height = Pleft_panel_item_height;
                                    tvex.Visible = true;
                                    tvex.Height = tvex_height;
                                    ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_delete;
                                    ibtn.Tag = "1";
                                }
                                else
                                {
                                    ctl.Height = item_top_height;
                                    tvex.Visible = true;
                                    tvex.Height = 0;
                                    ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_add;
                                    ibtn.Tag = "0";
                                }
                            }
                        }
                        else
                        {
                            ctl.Height = item_top_height;
                            PictureBox pb = ctl_top.Controls.Find("pb_" + ctl.Name, true)[0] as PictureBox;
                            pb.Left = LeftWidth - pb.Width - 1;
                            if (ctl.Name == pname)
                            {
                                pb.Image = null;
                                //ctl_top.BackColor = Color.FromArgb(255, 255, 0, 0);
                            }
                            else if (ctl.Name == threeMenu)
                            {
                                pb.Image = null;
                                //ctl_top.BackColor = Color.FromArgb(255, 255, 0, 0);
                            }
                            else
                            {
                                pb.Image = null;
                                //ctl_top.BackColor = Color.FromArgb(233, 243, 253);
                            }
                            ctl_top.Refresh();
                        }
                        ctl.Top = next_top;
                        next_top += ctl.Height;

                        i++;
                    }
                }
            }

            /// <summary> 左侧菜单状态 折叠 展开 选中 未选  点击一级菜单使用此函数
            /// </summary>
            /// <param name="pname">展开的功能名称name(二级菜单)</param>
            /// <param name="threeMenu">三级菜单</param>
            /// <param name="oldThreeMenu">原三级菜单</param>
            public void LeftMenuState(string pname, string threeMenu, string oldThreeMenu)
            {
                if (oldThreeMenu == "")
                {
                    LeftMenuState(pname, threeMenu);
                }
                else
                {
                    if (IsSameMenu(oldThreeMenu, SYSModel.clsSysConfig.STR_CURR_MAINMEMU))
                    {
                        LeftMenuState(pname, threeMenu);
                    }
                    else
                    {
                        int Pleft_panelHeight = panel_MainHeight;//panel的高度
                        if (this.Controls.Count > 0)
                        {
                            int Pleft_panel_count = this.Controls.Count;// -1; //二级菜单个数
                            int Pleft_panel_item_height = Pleft_panelHeight - (Pleft_panel_count * item_top_height) + item_top_height; //二级菜单高度-展开的
                            int tvex_height = Pleft_panel_item_height - item_top_height; //菜单列表高度-展开的

                            int next_top = 0;
                            int i = 0;
                            foreach (PanelEx ctl in this.Controls)
                            {
                                PanelEx ctl_top = ctl.Controls.Find("top_" + ctl.Name, true)[0] as PanelEx;
                                ctl.Width = LeftWidth;
                                ctl_top.Width = LeftWidth;
                                //ctl_top.Height = item_top_height;
                                if (ctl.Controls.Find("ibtn_" + ctl.Name, true).Count() > 0)
                                {
                                    ImageButton ibtn = ctl_top.Controls.Find("ibtn_" + ctl.Name, true)[0] as ImageButton;
                                    TreeViewEx tvex = ctl.Controls.Find("tv_" + ctl.Name, true)[0] as TreeViewEx;

                                    ibtn.Left = LeftWidth - ibtn.Width - 1;
                                    tvex.Width = LeftWidth;

                                    if (i == 0)
                                    {
                                        ctl.Height = Pleft_panel_item_height;
                                        tvex.Visible = true;
                                        tvex.Height = tvex_height;
                                        ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_delete;
                                        ibtn.Tag = "1";
                                    }
                                    else
                                    {
                                        ctl.Height = item_top_height;
                                        tvex.Visible = true;
                                        tvex.Height = 0;
                                        ibtn.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.ico_add;
                                        ibtn.Tag = "0";
                                    }
                                }
                                else
                                {
                                    //ctl_top.BackColor = Color.FromArgb(233, 243, 253);
                                    PictureBox pb = ctl_top.Controls.Find("pb_" + ctl.Name, true)[0] as PictureBox;
                                    pb.Left = LeftWidth - pb.Width - 1;
                                    ctl_top.Refresh();
                                }
                                ctl.Top = next_top;
                                next_top += ctl.Height;
                                i++;
                            }
                        }
                    }
                }
            }
            /// <summary> 判断原三级菜单是否在当前点击的一级菜单下
            /// </summary>
            /// <param name="oldThreeMenu">原三级菜单id</param>
            /// <param name="oneMenuId">当前一级菜单id</param>
            /// <returns></returns>
            private bool IsSameMenu(string oldThreeMenu, string oneMenuId)
            {
                bool bln = false;
                DataView dv = new DataView();
                dv = GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                dv.RowFilter = "fun_id='" + oldThreeMenu + "'";
                if (dv.ToTable().Rows.Count > 0)
                {
                    if (dv.ToTable().Rows[0]["parent_id"].ToString() == oneMenuId)
                    {
                        bln = true;
                    }
                    else
                    {
                        DataView dv2 = new DataView();
                        dv2 = GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                        dv2.RowFilter = "fun_id='" + dv.ToTable().Rows[0]["parent_id"].ToString() + "'";
                        if (dv2.ToTable().Rows[0]["parent_id"].ToString() == oneMenuId)
                        {
                            bln = true;
                        }
                    }
                }
                return bln;
            }
            #endregion

            #region 事件
            /// <summary> 菜单文字事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void lbl_Click(object sender, EventArgs e)
            {
                DataRow fun = (DataRow)(sender as Label).Tag;
                MainFormAddLabel(fun, this);

                Label lbl = sender as Label;
                str_Curr_Item = lbl.Name.Substring(4, lbl.Name.Length - 4);
                LeftMenuState(str_Curr_Item, str_Curr_Item);
                Action<string, string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
                OpLog.BeginInvoke((sender as Label).Tag.ToString(), GlobalStaticObj_Server.CommAccCode, null, null);
            }
            /// <summary> 二级菜单事件
            /// </summary>
            public void Pleft_panel_tiem_top_Click(object sender, EventArgs e)
            {
                DataRow fun = (DataRow)(sender as PanelEx).Tag;
                MainFormAddLabel(fun, this);

                PanelEx pnl = sender as PanelEx;
                str_Curr_Item = pnl.Name.Substring(4, pnl.Name.Length - 4);
                LeftMenuState(str_Curr_Item, str_Curr_Item);
                Action<string, string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
                OpLog.BeginInvoke((sender as PanelEx).Tag.ToString(), GlobalStaticObj_Server.CommAccCode, null, null);
            }
            /// <summary>  tree 三级菜单事件
            /// </summary>
            private void tvex_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
            {
                DataRow fun = (DataRow)e.Node.Tag;
                MainFormAddLabel(fun, this);
                foreach (PanelEx ctl in this.Controls)
                {
                    if (ctl.Controls.Find("ibtn_" + ctl.Name, true).Count() == 0)
                    {
                        PanelEx ctl_top = ctl.Controls.Find("top_" + ctl.Name, true)[0] as PanelEx;
                        //ctl_top.BackColor = Color.FromArgb(233, 243, 253);// 未选中
                        PictureBox pb = ctl_top.Controls.Find("pb_" + ctl.Name, true)[0] as PictureBox;
                        pb.Image = null;
                        ctl_top.Refresh();
                    }
                }
                Action<string, string> OpLog = HXCServerWinForm.CommonClass.CommonFuncCall.LogFunctionCall;
                OpLog.BeginInvoke(e.Node.Tag.ToString(), "000", null, null);
            }
            /// <summary> 展开/折叠
            /// </summary>
            void ibtn_Click(object sender, EventArgs e)
            {
                ImageButton ibtn = sender as ImageButton;
                str_Curr_Item = ibtn.Name.Substring(5, ibtn.Name.Length - 5);
                if (ibtn.Tag.ToString() == "0")//展开
                {
                    LeftMenuState(str_Curr_Item, SYSModel.clsSysConfig.STR_CURR_MEMU);
                }
                else//折叠
                {
                    LeftMenuState("", SYSModel.clsSysConfig.STR_CURR_MEMU);
                }
            }

            /// <summary> 控件大小改变事件
            /// </summary>
            private void UCLeftMenu_SizeChanged(object sender, EventArgs e)
            {
                panel_MainHeight = this.Height;
                LeftWidth = this.Width - 2;
                LeftMenuState(str_Curr_Item, SYSModel.clsSysConfig.STR_CURR_MEMU);
            }
            #endregion

        }
    }
