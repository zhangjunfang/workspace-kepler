using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm
{
    public delegate void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName);//定义委托 
    public delegate void deleteMenu(string strTag, string PUCName);
    [ComVisibleAttribute(true)]
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
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            this.Init();
        }
        public void SetContentMenuScrip(DataGridViewEx dgv)
        {
            if(dgv != null) dgv.ContextMenuStrip = cms_Function;
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
        /// 角色 控制默认页 按钮是否显示
        /// </summary>
        /// <param name="fun_id">默认页的Name</param>
        public void RoleButtonStstus(string fun_id)
        {
            if (HXCPcClient.GlobalStaticObj.gLoginDataSet != null
                && HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                dv.RowFilter = "fun_id='" + fun_id + "'";
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("button_add"))
                    {
                        this.btnAdd.Visible = dt.Rows[0]["button_add"].ToString() == "1" ? true : false;//新建
                    }
                    else
                    {
                        this.btnAdd.Visible = false;
                    }
                    if (dt.Columns.Contains("button_copy"))
                    {
                        this.btnCopy.Visible = dt.Rows[0]["button_copy"].ToString() == "1" ? true : false;//复制
                    }
                    else
                    {
                        this.btnCopy.Visible = false;
                    }
                    if (dt.Columns.Contains("button_edit"))
                    {
                        this.btnEdit.Visible = dt.Rows[0]["button_edit"].ToString() == "1" ? true : false;//编辑
                    }
                    else
                    {
                        this.btnEdit.Visible = false;
                    }
                    if (dt.Columns.Contains("button_delete"))
                    {
                        this.btnDelete.Visible = dt.Rows[0]["button_delete"].ToString() == "1" ? true : false;//删除
                    }
                    else
                    {
                        this.btnDelete.Visible = false;
                    }
                    if (dt.Columns.Contains("button_save"))
                    {
                        this.btnSave.Visible = dt.Rows[0]["button_save"].ToString() == "1" ? true : false;//保存
                    }
                    else
                    {
                        this.btnSave.Visible = false;
                    }
                    if (dt.Columns.Contains("button_balance"))
                    {
                        this.btnBalance.Visible = dt.Rows[0]["button_balance"].ToString() == "1" ? true : false;//结算
                    }
                    else
                    {
                        this.btnSubmit.Visible = false;
                    }
                    if (dt.Columns.Contains("button_submit"))
                    {
                        this.btnSubmit.Visible = dt.Rows[0]["button_submit"].ToString() == "1" ? true : false;//提交
                    }
                    else
                    {
                        this.btnSubmit.Visible = false;
                    }
                    if (dt.Columns.Contains("button_verify"))
                    {
                        this.btnVerify.Visible = dt.Rows[0]["button_verify"].ToString() == "1" ? true : false;//审核
                    }
                    else
                    {
                        this.btnVerify.Visible = false;
                    }
                    if (dt.Columns.Contains("button_cancel"))
                    {
                        this.btnCancel.Visible = dt.Rows[0]["button_cancel"].ToString() == "1" ? true : false;//取消
                    }
                    else
                    {
                        this.btnCancel.Visible = false;
                    }
                    if (dt.Columns.Contains("button_import"))
                    {
                        this.btnImport.Visible = dt.Rows[0]["button_import"].ToString() == "1" ? true : false;//导入
                    }
                    else
                    {
                        this.btnImport.Visible = false;
                    }
                    if (dt.Columns.Contains("button_export"))
                    {
                        this.btnExport.Visible = dt.Rows[0]["button_export"].ToString() == "1" ? true : false;//导出
                    }
                    else
                    {
                        this.btnExport.Visible = false;
                    }

                    if (dt.Rows[0]["button_print"].ToString() == "1")//打印预览设置
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

                    if (dt.Columns.Contains("button_status"))
                    {
                        this.btnStatus.Visible = dt.Rows[0]["button_status"].ToString() == "1" ? true : false;//启用
                    }
                    else
                    {
                        this.btnStatus.Visible = false;
                    }
                    if (dt.Columns.Contains("button_sync"))
                    {
                        this.btnSync.Visible = dt.Rows[0]["button_sync"].ToString() == "1" ? true : false;//同步
                    }
                    else
                    {
                        this.btnSync.Visible = false;
                    }
                    if (dt.Columns.Contains("button_confirm"))
                    {
                        this.btnConfirm.Visible = dt.Rows[0]["button_confirm"].ToString() == "1" ? true : false;//确认
                    }
                    else
                    {
                        this.btnConfirm.Visible = false;
                    }
                    if (dt.Columns.Contains("button_activate"))
                    {
                        this.btnActivation.Visible = dt.Rows[0]["button_activate"].ToString() == "1" ? true : false;//激活
                    }
                    else
                    {
                        this.btnActivation.Visible = false;
                    }
                    if (dt.Columns.Contains("button_commit"))
                    {
                        this.btnCommit.Visible = dt.Rows[0]["button_commit"].ToString() == "1" ? true : false;//上报厂家/总公司
                    }
                    else
                    {
                        this.btnCommit.Visible = false;
                    }

                    if (dt.Columns.Contains("button_revoke"))
                    {
                        this.btnRevoke.Visible = dt.Rows[0]["button_revoke"].ToString() == "1" ? true : false;//撤销
                    }
                    else
                    {
                        this.btnRevoke.Visible = false;
                    }
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
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            foreach (Control control in tlp_command.Controls)  //设置按钮图标
            {
                if (control is ButtonEx_sms)
                {
                    var cmdButton = control as ButtonEx_sms;
                    cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(cmdButton.Name.Replace("btn", "") + "_E", true);
                    ToolStripItem tsmi = null;
                    foreach (ToolStripItem item in cms_Function.Items)
                    {
                        if (item.Name == "tsmi" + cmdButton.Name.Replace("btn", ""))
                        {
                            tsmi = item;
                            break;
                        }
                    }
                    if (cmdButton.Enabled)
                    {
                        cmdButton.Icon =
                            ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                cmdButton.Name.Replace("btn", "") + "_E", true);
                    }
                    else
                    {
                        cmdButton.Icon =
                            ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                cmdButton.Name.Replace("btn", "") + "_D", true);
                    }
                    if (tsmi != null)
                    {
                        cmdButton.Tag = tsmi;
                        tsmi.Image = cmdButton.Icon;
                        tsmi.Text = cmdButton.Caption;
                    };
                    cmdButton.EnabledChanged += delegate(object sender, EventArgs args)
                    {
                        var cSender = sender as ButtonEx_sms;
                        if (cSender == null) return;
                        if (cSender.Enabled)
                        {
                            cSender.Icon =
                                ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                    cmdButton.Name.Replace("btn", "") + "_E", true);
                        }
                        else
                        {
                            cSender.Icon =
                                ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                    cmdButton.Name.Replace("btn", "") + "_D", true);
                        }
                        if (cSender != null && (cSender.Tag as ToolStripItem) != null)
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
                }
            }
        }

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

        /// <summary> 创建人：唐春奎 管理默认管理界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableManager()
        {
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
        
        }
        /// <summary> 创建人：唐春奎 管理操作（新增、编辑）界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableHandle()
        {
            btnImport.Visible = true;
            btnSave.Visible = true;
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
        }
        /// <summary> 创建人：唐春奎 管理默认详情界面工具栏上的按钮可见
        /// </summary>
        public void SetButtonVisiableView()
        {
            btnActivation.Visible = true;
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
