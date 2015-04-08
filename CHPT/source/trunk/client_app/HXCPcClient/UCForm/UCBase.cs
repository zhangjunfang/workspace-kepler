using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm
{
    public delegate void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName);//定义委托 
    public delegate void deleteMenu(string strTag, string PUCName);
    [ComVisible(true)]
    public partial class UCBase : UserControl
    {
        #region --UI交互
        public delegate void UiHandler(object para);
        public UiHandler uiHandler;
        #endregion

        public WindowStatus windowStatus = WindowStatus.View;
        public static addUserControls addUserControls;//定义委托实例 
        public static deleteMenu deleteMenu;
        public UCBase()
        {
            InitializeComponent();
            //btnInvalidOrActivation.Width = 0;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer, true);

            Load += delegate
            {
               Init();
            };
        }
        /// <summary>
        /// 添加标签 添加内容窗体
        /// </summary>
        /// <param name="userContol">新窗体</param>
        /// <param name="memuName">标签名 例： 车型档案-预览  或 车型档案</param>
        /// <param name="menuId">三级菜单id(新窗体+操作动词   例:新增 "UCBaseAdd"  如果是编辑或复制或预览 新窗体+操作动词+操作数据ID 例：UCBaseEdit123)</param>
        /// <param name="thisUcTag">当前窗体Tag tag包括（三级 |一级| 二级 菜单的id）</param>
        /// <param name="PUCName">当前窗体Name   this.Name）</param>
        public static void AddUserControl(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName)
        {
            if (addUserControls != null)
            {
                if (!thisUcTag.Contains("|"))
                {
                    thisUcTag = thisUcTag + "|" + "|";
                }
                addUserControls(userContol, memuName, menuId, thisUcTag, PUCName);
            }
        }
        /// <summary>
        /// 添加标签 添加内容窗体
        /// </summary>
        /// <param name="userContol">新窗体</param>
        /// <param name="memuName">标签名 例： 车型档案-预览  或 车型档案</param>
        /// <param name="menuId">三级菜单id(新窗体+操作动词   例:新增 "UCBaseAdd"  如果是编辑或复制或预览 新窗体+操作动词+操作数据ID 例：UCBaseEdit123)</param>
        /// <param name="thisUcTag">当前窗体Tag tag包括（三级 |一级| 二级 菜单的id）</param>
        /// <param name="PUCName">当前窗体Name   this.Name）</param>
        public void addUserControl(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName)
        {
            if (addUserControls != null)
            {
                addUserControls(userContol, memuName, menuId, thisUcTag, PUCName);
            }
        }
        /// <summary>
        /// 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        /// <param name="PUCName">父窗体name</param>
        public static void deleteMenuByTag(string strTag, string PUCName)
        {
            if (deleteMenu != null)
            {
                deleteMenu(strTag, PUCName);
            }
        }

        /// <summary>
        /// 关闭前操作
        /// </summary>
        /// <returns>true关闭当前标签，false阻止关闭当前标签</returns>
        public virtual bool CloseMenu()
        {
            return true;
        }
        /// <summary>
        /// 角色 控制默认页 按钮是否显示
        /// </summary>
        /// <param name="fun_id">默认页的Name</param>
        public void RoleButtonStstus(string fun_id)
        {
            DataRow dr = LocalCache.GetFunction(fun_id);
            if (dr != null)
            {
                if (dr.Table.Columns.Contains("button_add"))
                {
                    this.btnAdd.Visible = dr["button_add"].ToString() == "1" ? true : false;//新建
                }
                else
                {
                    this.btnAdd.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_copy"))
                {
                    this.btnCopy.Visible = dr["button_copy"].ToString() == "1" ? true : false;//复制
                }
                else
                {
                    this.btnCopy.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_edit"))
                {
                    this.btnEdit.Visible = dr["button_edit"].ToString() == "1" ? true : false;//编辑
                }
                else
                {
                    this.btnEdit.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_delete"))
                {
                    this.btnDelete.Visible = dr["button_delete"].ToString() == "1" ? true : false;//删除
                }
                else
                {
                    this.btnDelete.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_save"))
                {
                    this.btnSave.Visible = dr["button_save"].ToString() == "1" ? true : false;//保存
                }
                else
                {
                    this.btnSave.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_balance"))
                {
                    this.btnBalance.Visible = dr["button_balance"].ToString() == "1" ? true : false;//结算
                }
                else
                {
                    this.btnSubmit.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_submit"))
                {
                    this.btnSubmit.Visible = dr["button_submit"].ToString() == "1" ? true : false;//提交
                }
                else
                {
                    this.btnSubmit.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_verify"))
                {
                    this.btnVerify.Visible = dr["button_verify"].ToString() == "1" ? true : false;//审核
                }
                else
                {
                    this.btnVerify.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_cancel"))
                {
                    this.btnCancel.Visible = dr["button_cancel"].ToString() == "1" ? true : false;//取消
                }
                else
                {
                    this.btnCancel.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_import"))
                {
                    this.btnImport.Visible = dr["button_import"].ToString() == "1" ? true : false;//导入
                }
                else
                {
                    this.btnImport.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_export"))
                {
                    this.btnExport.Visible = dr["button_export"].ToString() == "1" ? true : false;//导出
                }
                else
                {
                    this.btnExport.Visible = false;
                }

                if (dr["button_print"].ToString() == "1")//打印预览设置
                {
                    btnPrint.Visible = true;
                    btnView.Visible = true;
                    btnSet.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnView.Visible = false;
                    btnSet.Visible = false;
                }

                if (dr.Table.Columns.Contains("button_status"))
                {
                    this.btnStatus.Visible = dr["button_status"].ToString() == "1" ? true : false;//启用
                }
                else
                {
                    this.btnStatus.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_sync"))
                {
                    this.btnSync.Visible = dr["button_sync"].ToString() == "1" ? true : false;//同步
                }
                else
                {
                    this.btnSync.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_confirm"))
                {
                    this.btnConfirm.Visible = dr["button_confirm"].ToString() == "1" ? true : false;//确认
                }
                else
                {
                    this.btnConfirm.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_activate"))
                {
                    this.btnActivation.Visible = dr["button_activate"].ToString() == "1" ? true : false;//激活
                }
                else
                {
                    this.btnActivation.Visible = false;
                }
                if (dr.Table.Columns.Contains("button_commit"))
                {
                    this.btnCommit.Visible = dr["button_commit"].ToString() == "1" ? true : false;//上报厂家/总公司
                }
                else
                {
                    this.btnCommit.Visible = false;
                }

                if (dr.Table.Columns.Contains("button_revoke"))
                {
                    this.btnRevoke.Visible = dr["button_revoke"].ToString() == "1" ? true : false;//撤销
                }
                else
                {
                    this.btnRevoke.Visible = false;
                }
            }
        }
        public delegate void ClickHandler(object sender, EventArgs e);

        #region 事件定义
        /// <summary> 
        /// 同步事件
        /// </summary>
        public event ClickHandler SyncEvent;    //add by Kord
        /// <summary> 
        /// 确认事件
        /// </summary>
        public event ClickHandler ConfirmEvent; //add by Kord
        /// <summary> 
        /// 作废/激活事件
        /// </summary>
        public event ClickHandler InvalidOrActivationEvent; //add by Kord
        /// <summary> 
        /// 上报厂家/总公司事件
        /// </summary>
        public event ClickHandler CommitEvent; //add by Kord
        /// <summary> 
        /// 撤销事件
        /// </summary>
        public event ClickHandler RevokeEvent; //add by Kord
        /// <summary> 新增事件
        /// </summary>
        public event ClickHandler AddEvent;

        /// <summary> 复制事件
        /// </summary>
        public event ClickHandler CopyEvent;


        /// <summary> 编辑事件
        /// </summary>
        public event ClickHandler EditEvent;

        /// <summary> 删除事件
        /// </summary>
        public event ClickHandler DeleteEvent;

        /// <summary> 状态更改事件
        /// </summary>
        public event ClickHandler StatusEvent;

        /// <summary> 审核事件
        /// </summary>
        public event ClickHandler VerifyEvent;

        /// <summary> 保存事件
        /// </summary>
        public event ClickHandler SaveEvent;
        /// <summary>
        /// 结算事件
        /// </summary>
        public event ClickHandler BalanceEvent;

        /// <summary> 提交事件
        /// </summary>
        public event ClickHandler SubmitEvent;

        /// <summary> 取消事件
        /// </summary>
        public event ClickHandler CancelEvent;

        /// <summary> 导入事件
        /// </summary>
        public event ClickHandler ImportEvent;

        /// <summary> 导出事件
        /// </summary>
        public event ClickHandler ExportEvent;

        /// <summary> 预览事件
        /// </summary>
        public event ClickHandler ViewEvent;

        /// <summary> 打印事件
        /// </summary>
        public event ClickHandler PrintEvent;

        /// <summary> 设置事件
        /// </summary>
        public event ClickHandler SetEvent;
        /// <summary>
        /// 更多操作事件
        /// </summary>
        public event ClickHandler MoreEvent;
        #endregion

        #region 方法
        /// <summary> 根据窗体状态更改控件状态（操作本页数据）
        /// syn
        /// </summary>
        /// <param name="status">窗体状态</param>
        public void SetBtnStatus(WindowStatus status)
        {
            this.windowStatus = status;
            //根据窗体状态，设置当前窗体的控件的状态
            switch (status)
            {
                case WindowStatus.Add:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnBalance.Enabled = false;
                    btnVerify.Enabled = false;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;

                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;

                    btnActivation.Enabled = false;

                    btnSync.Enabled = false;

                    btnConfirm.Enabled = false;
                    btnSubmit.Enabled = false;
                    btnCommit.Enabled = false;
                    btnRevoke.Enabled = false;

                    #region --不可见
                    btnAdd.Visible = false;
                    btnCopy.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnStatus.Visible = false;
                    btnBalance.Visible = false;
                    btnVerify.Visible = false;
                    btnImport.Visible = false;
                    btnExport.Visible = false;

                    btnView.Visible = false;
                    btnPrint.Visible = false;
                    btnSet.Visible = false;

                    btnActivation.Visible = false;

                    btnSync.Visible = false;

                    btnConfirm.Visible = false;
                    btnSubmit.Visible = false;
                    btnCommit.Visible = false;
                    btnRevoke.Visible = false;
                    #endregion

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case WindowStatus.Copy:
                case WindowStatus.Edit:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnBalance.Enabled = false;
                    btnVerify.Enabled = false;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;

                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;

                    btnSubmit.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnCommit.Enabled = false;
                    btnRevoke.Enabled = false;

                    btnActivation.Enabled = false;

                    btnSync.Enabled = false;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case WindowStatus.Save:
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnStatus.Enabled = true;
                    btnBalance.Enabled = true;
                    btnVerify.Enabled = true;
                    btnImport.Enabled = true;
                    btnExport.Enabled = true;

                    btnView.Enabled = true;
                    btnPrint.Enabled = true;
                    btnSet.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnConfirm.Enabled = true;
                    btnCommit.Enabled = true;
                    btnRevoke.Enabled = true;

                    btnActivation.Enabled = true;

                    btnSync.Enabled = true;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    break;
                case WindowStatus.Cancel:
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnStatus.Enabled = true;
                    btnBalance.Enabled = true;
                    btnVerify.Enabled = true;
                    btnImport.Enabled = true;
                    btnExport.Enabled = true;

                    btnView.Enabled = true;
                    btnPrint.Enabled = true;
                    btnSet.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnConfirm.Enabled = true;
                    btnCommit.Enabled = true;
                    btnRevoke.Enabled = true;

                    btnActivation.Enabled = true;

                    btnSync.Enabled = true;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                case WindowStatus.View:
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnStatus.Enabled = true;
                    btnBalance.Enabled = true;
                    btnVerify.Enabled = true;
                    btnImport.Enabled = true;
                    btnExport.Enabled = true;

                    btnView.Enabled = true;
                    btnPrint.Enabled = true;
                    btnSet.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnConfirm.Enabled = true;
                    btnCommit.Enabled = true;
                    btnRevoke.Enabled = true;

                    btnActivation.Enabled = true;

                    btnSync.Enabled = true;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                default:
                    break;
            }

        }

        /// <summary> 根据窗体状态更改控件状态（新增窗体）
        /// </summary>
        /// <param name="status">窗体状态</param>
        public void SetBtnStatus2(WindowStatus status)
        {
            windowStatus = status;

            //根据窗体状态，设置当前窗体的控件的状态
            switch (status)
            {
                case WindowStatus.Add:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;
                    break;
                case WindowStatus.Copy:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;
                    break;
                case WindowStatus.Edit:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;
                    break;
                case WindowStatus.Save:
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnStatus.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;
                    break;
                case WindowStatus.Cancel:
                    btnAdd.Enabled = true;
                    btnCopy.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnStatus.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnImport.Enabled = false;
                    btnExport.Enabled = false;
                    btnView.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSet.Enabled = false;
                    break;
                case WindowStatus.View:
                    btnAdd.Enabled = false;
                    btnCopy.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnStatus.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                default:
                    break;
            }
        }




        #region 设置按钮和右键菜单 add by kord
        public void ClearAllToolStripItem()
        {
            cms_Function.Items.Clear();
        }
        public void SetContentMenuScrip(Control dgv)
        {
            if (dgv == null) return;
            InitMenu();
            dgv.ContextMenuStrip = cms_Function;
        }
        public void AddToolStripItem(ButtonEx_sms button)
        {
            AddToolStripItem(button, cms_Function.Items.Count);
        }
        public void AddToolStripItem(ButtonEx_sms button, int index)
        {
            if (index < 0) index = 0;
            if (index > cms_Function.Items.Count) index = cms_Function.Items.Count;
            var tsmi = new ToolStripMenuItem
            {
                Name = "tsmi" + button.Name,
                Size = new System.Drawing.Size(165, 22),
            };
            CopyButtonInfo2MenuItem(button, tsmi);
            cms_Function.Items.Insert(index, tsmi);
        }
        public void AddToolStripItem(ButtonEx button)
        {
            AddToolStripItem(button, cms_Function.Items.Count);
        }
        public void AddToolStripItem(ButtonEx button, int index)
        {
            if (index < 0) index = 0;
            if (index > cms_Function.Items.Count) index = cms_Function.Items.Count;
            var tsmi = new ToolStripMenuItem
            {
                Name = "tsmi" + button.Name,
                Size = new System.Drawing.Size(165, 22),
            };
            CopyButtonInfo2MenuItem(button, tsmi);
            cms_Function.Items.Insert(index, tsmi);
        }
        private void CopyButtonInfo2MenuItem(ButtonEx cmdButton, ToolStripItem tsmi)
        {
            if (cmdButton == null || tsmi == null) return;
            cmdButton.Tag = tsmi;
            tsmi.Text = cmdButton.Caption;
            tsmi.Enabled = cmdButton.Enabled;
            tsmi.Visible = cmdButton.Visible;
            tsmi.Tag = cmdButton;
            tsmi.Click += TsmiOnClick4ButtonEx;
            cmdButton.EnabledChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx;
                if (cSender == null) return;
                if ((cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Enabled = cSender.Enabled;
                }
            };
            cmdButton.VisibleChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx;
                if (cSender != null && (cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Visible = cSender.Visible;
                }
            };
            cmdButton.TextChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx;
                if (cSender != null && (cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Text = cSender.Text;
                }
            };
        }
        private void CopyButtonInfo2MenuItem(ButtonEx_sms cmdButton, ToolStripItem tsmi)
        {
            if (cmdButton == null || tsmi == null) return;
            cmdButton.Tag = tsmi;
            tsmi.Image = cmdButton.Icon;
            tsmi.Text = cmdButton.Caption;
            tsmi.Enabled = cmdButton.Enabled;
            tsmi.Visible = cmdButton.Visible;
            tsmi.Tag = cmdButton;
            tsmi.Click += TsmiOnClick;
            cmdButton.EnabledChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx_sms;
                if (cSender == null) return;
                if (cSender.Enabled)
                {
                    var iconE = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                        cSender.Name.Replace("btn", "") + "_E", true);
                    var iconL = (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Edit) ? ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                        cSender.Name.Replace("btn", "") + "_L", true) : null;
                    cSender.Icon = iconL ?? iconE;
                }
                else
                {
                    cSender.Icon =
                        ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                            cSender.Name.Replace("btn", "") + "_D", true);
                }
                if ((cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Enabled = cSender.Enabled;
                }
            };
            cmdButton.VisibleChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx_sms;
                if (cSender != null && (cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Visible = cSender.Visible;
                }
            };
            cmdButton.TextChanged += delegate(object sender, EventArgs args)
            {
                var cSender = sender as ButtonEx_sms;
                if (cSender != null && (cSender.Tag as ToolStripItem) != null)
                {
                    (cSender.Tag as ToolStripItem).Text = cSender.Text;
                }
            };
        }

        private void TsmiOnClick(object sender, EventArgs eventArgs)
        {
            var cSender = sender as ToolStripItem;
            if (cSender != null && (cSender.Tag as ButtonEx_sms) != null)
            {
                (cSender.Tag as ButtonEx_sms).PerformClick();
            }
        }
        private void TsmiOnClick4ButtonEx(object sender, EventArgs eventArgs)
        {
            var cSender = sender as ToolStripItem;
            if (cSender != null && (cSender.Tag as ButtonEx) != null)
            {
                (cSender.Tag as ButtonEx).PerformClick();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitMenu()
        {
            foreach (var cmdButton in tlp_command.Controls.OfType<ButtonEx_sms>().Select(control => control))
            {
                var tsmi = cms_Function.Items.Cast<ToolStripItem>().FirstOrDefault(item => item.Name == "tsmi" + cmdButton.Name.Replace("btn", ""));
                CopyButtonInfo2MenuItem(cmdButton, tsmi);
            }
        }

        private void Init()
        {
            foreach (var cmdButton in tlp_command.Controls.OfType<ButtonEx_sms>().Select(control => control))
            {
                cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(cmdButton.Name.Replace("btn", "") + "_E", true);
                var textCap = cmdButton.Caption;
                switch (textCap)
                {
                    case "激活":
                        cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Activation_" + (cmdButton.Enabled ? "E" : "D"), true);
                        break;
                    case "作废":
                        cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Invalid_" + (cmdButton.Enabled ? "E" : "D"), true);
                        break;
                }
                cmdButton.TextChanged += delegate(object sender, EventArgs args) //临时使用,不建议使用
                {
                    var btn = sender as ButtonEx_sms;
                    if (btn == null) return;
                    var text = btn.Caption;
                    switch (text)
                    {
                        case "激活":
                            btn.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Activation_" + (btn.Enabled ? "E" : "D"), true);
                            break;
                        case "作废":
                            btn.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Invalid_" + (btn.Enabled ? "E" : "D"), true);
                            break;
                    }
                };
                cmdButton.EnabledChanged += delegate(object sender, EventArgs args)
                {
                    var cSender = sender as ButtonEx_sms;
                    if (cSender == null) return;
                    if (cSender.Enabled)
                    {
                        var iconE = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                            cSender.Name.Replace("btn", "") + "_E", true);
                        var iconL = (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Edit) ? ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                            cSender.Name.Replace("btn", "") + "_L", true) : null;
                        cSender.Icon = iconL ?? iconE;
                    }
                    else
                    {
                        cSender.Icon =
                            ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                cSender.Name.Replace("btn", "") + "_D", true);
                    }
                    switch (cSender.Caption)
                    {
                        case "激活":
                            cSender.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Activation_" + (cSender.Enabled ? "E" : "D"), true);
                            break;
                        case "作废":
                            cSender.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("Invalid_" + (cSender.Enabled ? "E" : "D"), true);
                            break;
                    }
                };
            }
        }
        #endregion

        /// <summary>
        /// 清除查询面板
        /// </summary>
        /// <param name="pnlSearch">查询面板</param>
        public void ClearSearch(Panel pnlSearch)
        {
            foreach (Control ctr in pnlSearch.Controls)
            {
                if (ctr is TextBoxEx)
                {
                    TextBoxEx txt = (TextBoxEx)ctr;
                    txt.Caption = string.Empty;
                }
                else if (ctr is ComboBoxEx)
                {
                    ComboBoxEx cbo = (ComboBoxEx)ctr;
                    if (cbo.Items.Count > 0)
                    {
                        cbo.SelectedIndex = 0;
                    }
                    else
                    {
                        cbo.SelectedItem = null;
                    }
                }
                else if (ctr is TextChooser)
                {
                    TextChooser txtc = (TextChooser)ctr;
                    txtc.Text = string.Empty;
                    txtc.Tag = null;
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cb = (CheckBox)ctr;
                    cb.Checked = false;
                }
                else if (ctr is DateInterval)
                {
                    DateInterval di = (DateInterval)ctr;
                    di.Empty();
                }
            }
        }

        /// <summary> 设置容器内空间是否可用
        /// </summary>
        /// <param name="cnl">容器空间</param>
        /// <param name="isCanUse">是否可用</param>
        public void SetControlEnable(Control pControl, bool isCanUse)
        {
            foreach (Control cnl in pControl.Controls)
            {
                if (cnl is TextBox)
                {
                    TextBox txt = cnl as TextBox;
                    txt.ReadOnly = !isCanUse;
                }
                else if (cnl is CheckBox || cnl is DateTimePicker || cnl is ComboBox || cnl is NumericUpDown || cnl is RadioButton || cnl is Button)
                {
                    cnl.Enabled = isCanUse;
                }
                else if (cnl is DataGridView)
                {
                    DataGridView dgv = cnl as DataGridView;
                    dgv.ReadOnly = !isCanUse;
                }
                else if (cnl is ServiceStationClient.ComponentUI.TextBox.TextChooser)
                {
                    ServiceStationClient.ComponentUI.TextBox.TextChooser tc = (ServiceStationClient.ComponentUI.TextBox.TextChooser)cnl;
                    tc.Enabled = isCanUse;
                }
                else if (cnl is ServiceStationClient.ComponentUI.DateTimePickerEx)
                {
                    ServiceStationClient.ComponentUI.DateTimePickerEx dtp = (ServiceStationClient.ComponentUI.DateTimePickerEx)cnl;
                    dtp.Enabled = isCanUse;
                }
                else if (cnl is ServiceStationClient.ComponentUI.DateTimePickerEx_sms)
                {
                    ServiceStationClient.ComponentUI.DateTimePickerEx_sms dtp = (ServiceStationClient.ComponentUI.DateTimePickerEx_sms)cnl;
                    dtp.Enabled = isCanUse;
                }
                else if (cnl.HasChildren)
                {
                    SetControlEnable(cnl, isCanUse);
                }
            }
        }
        #endregion

        #region 按钮事件
        private void btnSync_Click(object sender, EventArgs e)  //add by Kord
        {
            if (SyncEvent != null)
            {
                SyncEvent(sender, e);
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)   //add by Kord
        {
            if (ConfirmEvent != null)
            {
                ConfirmEvent(sender, e);
            }
        }
        /// <summary>
        /// 作废事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvalidOrActivation_Click(object sender, EventArgs e)  //add by Kord
        {
            //更新状态
            //if (btnInvalidOrActivation.Caption == "作废")
            //{
            //    btnInvalidOrActivation.Caption = "激活";
            //}
            //else
            //{
            //    btnInvalidOrActivation.Caption = "作废";
            //}
            if (InvalidOrActivationEvent != null)
            {
                InvalidOrActivationEvent(sender, e);
            }
        }
        private void btnCommit_Click(object sender, EventArgs e)  //add by Kord
        {
            if (CommitEvent != null)
            {
                CommitEvent(sender, e);
            }
        }
        private void btnRevoke_Click(object sender, EventArgs e)   //add by Kord
        {
            if (RevokeEvent != null)
            {
                RevokeEvent(sender, e);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddEvent != null)
            {
                AddEvent(sender, e);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (CopyEvent != null)
            {
                CopyEvent(sender, e);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (EditEvent != null)
            {
                EditEvent(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
            {
                DeleteEvent(sender, e);
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (StatusEvent != null)
            {
                StatusEvent(sender, e);
            }
            //更新状态
            if (btnStatus.Caption == "启用")
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (VerifyEvent != null)
            {
                VerifyEvent(sender, e);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(sender, e);
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            if (BalanceEvent != null)
            {
                BalanceEvent(sender, e);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (SubmitEvent != null)
            {
                SubmitEvent(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelEvent != null)
            {
                CancelEvent(sender, e);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (ImportEvent != null)
            {
                ImportEvent(sender, e);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ExportEvent != null)
            {
                ExportEvent(sender, e);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (ViewEvent != null)
            {
                ViewEvent(sender, e);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(sender, e);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (SetEvent != null)
            {
                SetEvent(sender, e);
            }
        }
        #endregion

        #region 设置系统管理页面状态
        /// <summary>
        /// 设置系统管理新增页面按钮状态
        /// </summary>
        public void SetSysManageAddBtn()
        {

            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = false;//编辑按钮
            btnDelete.Visible = false;//删除按钮
            btnStatus.Visible = false;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = true;//保存按钮
            btnCancel.Visible = true;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = false;//预览按钮
            btnPrint.Visible = false;//打印按钮
            btnSet.Visible = false;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮

        }

        /// <summary>
        /// 设置系统管理编辑页面按钮状态
        /// </summary>
        public void SetSysManageEditBtn()
        {
            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = false;//编辑按钮
            btnDelete.Visible = true;//删除按钮
            btnStatus.Visible = true;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = true;//保存按钮
            btnCancel.Visible = true;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = false;//预览按钮
            btnPrint.Visible = false;//打印按钮
            btnSet.Visible = false;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮

        }

        /// <summary>
        /// 设置系统管理浏览页面中的按钮初始状态
        /// </summary>
        public void SetSysManageViewBtn()
        {
            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = true;//编辑按钮
            btnDelete.Visible = true;//删除按钮
            btnStatus.Visible = true;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = false;//保存按钮
            btnCancel.Visible = false;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = true;//预览按钮
            btnPrint.Visible = true;//打印按钮
            btnSet.Visible = true;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮

        }
        #endregion

        #region 数据管理页面按钮控制
        /// <summary>
        /// 设置数据管理浏览页面中的按钮初始状态
        /// </summary>
        public void SetDataViewBtn()
        {
            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = true;//编辑按钮
            btnDelete.Visible = true;//删除按钮
            btnStatus.Visible = true;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = false;//保存按钮
            btnCancel.Visible = false;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = false;//预览按钮
            btnPrint.Visible = false;//打印按钮
            btnSet.Visible = false;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮



        }

        /// <summary>
        /// 设置数据管理新增页面中的按钮初始状态
        /// </summary>
        public void SetDataAddBtn()
        {

            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = false;//编辑按钮
            btnDelete.Visible = false;//删除按钮
            btnStatus.Visible = false;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = true;//保存按钮
            btnCancel.Visible = true;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = false;//预览按钮
            btnPrint.Visible = false;//打印按钮
            btnSet.Visible = false;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮

        }

        /// <summary>
        /// 设置数据管理编辑页面中的按钮初始状态
        /// </summary>
        public void SetDataEditBtn()
        {

            btnAdd.Visible = false;//新增按钮
            btnCopy.Visible = false;//复制按钮
            btnEdit.Visible = false;//编辑按钮
            btnDelete.Visible = true;//删除按钮
            btnStatus.Visible = true;//启用/停用按钮
            btnSubmit.Visible = false;//提交按钮
            btnSave.Visible = true;//保存按钮
            btnCancel.Visible = true;//取消按钮
            btnImport.Visible = false;//导入按钮
            btnExport.Visible = false;//导出按钮
            btnView.Visible = false;//预览按钮
            btnPrint.Visible = false;//打印按钮
            btnSet.Visible = false;//设置按钮
            btnBalance.Visible = false;//结算按钮
            btnVerify.Visible = false;//审核按钮
            btnConfirm.Visible = false;//确认按钮
            btnCommit.Visible = false;//上报厂家/总公司按钮
            btnRevoke.Visible = false;//撤销按钮
            btnActivation.Visible = false;//作废按钮
            btnSync.Visible = false;//同步按钮

        }
        #endregion


        private void btnMore_Click(object sender, EventArgs e)
        {
            if (MoreEvent != null)
            {
                MoreEvent(sender, e);
            }
        }

        /// <summary> 创建人：唐春奎 循环窗体中所有的控件设置所有按钮全部不可用 
        /// </summary>
        public void SetBaseButtonStatus(Control.ControlCollection Controls)
        {
            foreach (Control p_Controls in Controls)
            {
                if (p_Controls.GetType().Name == "ButtonEx_sms")
                {
                    p_Controls.Visible = false;
                }
                else if (p_Controls.HasChildren)
                {
                    SetBaseButtonStatus(p_Controls.Controls);
                }
            }
        }
        /// <summary>创建人：唐春奎 设置所有按钮全部不可用  
        /// </summary>
        public void SetBaseButtonStatus()
        {
            SetBaseButtonStatus(pnlOpt.Controls);
        }
        /// <summary> 设置工具栏按钮，当在可见的情况下，设置为不可用
        /// </summary>
        public void SetBaeButtonEnableFalse(Control.ControlCollection Controls)
        {
            foreach (Control p_Controls in Controls)
            {
                if (p_Controls.GetType().Name == "ButtonEx_sms")
                {
                    if (p_Controls.Visible)
                    {
                        p_Controls.Enabled = false;
                    }
                }
                else if (p_Controls.HasChildren)
                {
                    SetBaeButtonEnableFalse(p_Controls.Controls);
                }
            }
        }
        /// <summary> 设置工具栏按钮，当在可见的情况下
        /// </summary>
        public void SetBaeButtonEnable()
        {
            SetBaeButtonEnableFalse(pnlOpt.Controls);
        }

        /// <summary> 创建人：唐春奎 管理默认管理界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableManager()
        {
            //新增、复制、编辑、删除、提交、审核、导出、设置、预览、打印、操作、撤销、其他
            btnAdd.Visible = true;
            btnCopy.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnVerify.Visible = true;
            btnImport.Visible = true;
            btnExport.Visible = true;
            btnActivation.Visible = true;
            btnSubmit.Visible = true;
            btnSet.Visible = true;
            btnView.Visible = true;
            btnPrint.Visible = true;
            btnCancel.Visible = true;
            btnSet.Visible = true;
            btnRevoke.Visible = true;
        }
        /// <summary> 创建人：唐春奎 管理业务查询界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableManagerSearch()
        {
            //预览、打印、设置、导出
            btnView.Visible = true;
            btnPrint.Visible = true;
            btnSet.Visible = true;
            btnExport.Visible = true;
        }
        /// <summary> 创建人：唐春奎 管理操作（新增、复制）界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableHandleAddCopy()
        {
            //提交、保存、取消、导入、导出、设置、预览、打印、过程、其他
            btnSubmit.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnImport.Visible = true;
            btnExport.Visible = true;
            btnSet.Visible = true;
            btnView.Visible = true;
            btnPrint.Visible = true;
        }
        /// <summary> 创建人：唐春奎 管理操作（新增、复制）界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableHandleEdit()
        {
            //提交、保存、取消、导入、导出、设置、预览、打印、操作、过程、其他
            btnSubmit.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnImport.Visible = true;
            btnExport.Visible = true;
            btnSet.Visible = true;
            btnView.Visible = true;
            btnPrint.Visible = true;
        }
        /// <summary> 创建人：唐春奎 管理默认详情界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableView()
        {
            //复制、编辑、删除、废/活、提交、审核、导出、设置、预览、打印、操作、撤销、更多操作、过程、其他
            btnCopy.Visible = true;
            btnCopy.Enabled = false;
            btnEdit.Visible = true;
            btnEdit.Enabled = false;
            btnDelete.Visible = true;
            btnDelete.Enabled = false;
            btnActivation.Visible = true;
            btnSubmit.Visible = true;
            btnSubmit.Enabled = true;
            //btnVerify.Visible = true;
            btnExport.Visible = true;
            btnSet.Visible = true;
            btnView.Visible = true;
            btnPrint.Visible = true;
            //btnRevoke.Visible = true;
        }

        /// <summary>
        /// 限制控件只能输入数字,删除,小数点
        /// </summary>
        /// <param name="e">键盘事件数据</param>
        public void OnlyNum(KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
                return;
            }
        }

    }

    /// <summary>
    /// 窗体状态
    /// </summary>
    public enum WindowStatus
    {
        /// <summary> 新增
        /// </summary>
        Add,
        /// <summary> 复制
        /// </summary>
        Copy,
        /// <summary> 编辑
        /// </summary>
        Edit,
        /// <summary> 审核
        /// </summary>
        Verify,
        /// <summary> 保存
        /// </summary>
        Save,
        /// <summary> 提交
        /// </summary>
        Submit,
        /// <summary> 取消
        /// </summary>
        Cancel,
        /// <summary> 预览
        /// </summary>
        View
    }
}
