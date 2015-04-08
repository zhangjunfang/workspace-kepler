using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ServiceStationClient.ComponentUI;

namespace HXCServerWinForm.UCForm
{
    public delegate void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName);//定义委托 
    public delegate void deleteMenu(string strTag, string PUCName);
    [ComVisibleAttribute(true)]
    public partial class UCBase : UserControl
    {
        public WindowStatus windowStatus = WindowStatus.View;
        public static addUserControls addUserControls;//定义委托实例 
        public static deleteMenu deleteMenu;
        public UCBase()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            foreach (Control control in tlpOpBtn.Controls)  //设置按钮图标
            {
                if (control is ButtonEx_sms)
                {
                    var cmdButton = control as ButtonEx_sms;
                    cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(cmdButton.Name.Replace("btn", "") + "_E", true);
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
                    };
                }
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
            addUserControls(userContol, memuName, menuId, thisUcTag, PUCName);
        }
        /// <summary>
        /// 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        /// <param name="PUCName">父窗体name</param>
        public static void deleteMenuByTag(string strTag, string PUCName)
        {
            deleteMenu(strTag, PUCName);
        }

        /// <summary>
        /// 角色 控制默认页 按钮是否显示
        /// </summary>
        /// <param name="fun_id">默认页的Name</param>
        public void SetOpButtonVisible(string fun_id)
        {
            if (HXCServerWinForm.GlobalStaticObj.gLoginDataSet != null && HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                dv.RowFilter = "fun_id='" + fun_id + "'";
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                {
                    btnAdd.Visible = dt.Rows[0]["button_add"].ToString() == "1" ? true : false;
                    btnEdit.Visible = dt.Rows[0]["button_edit"].ToString() == "1" ? true : false;
                    btnDelete.Visible = dt.Rows[0]["button_delete"].ToString() == "1" ? true : false;
                    btnSave.Visible = dt.Rows[0]["button_save"].ToString() == "1" ? true : false;
                    btnCancel.Visible = dt.Rows[0]["button_cancel"].ToString() == "1" ? true : false;
                    btnStatus.Visible = dt.Rows[0]["button_status"].ToString() == "1" ? true : false;

                    btnshotoff.Visible = dt.Rows[0]["button_shotoff"].ToString() == "1" ? true : false;
                    btnSet.Visible = dt.Rows[0]["button_Set"].ToString() == "1" ? true : false;
                    btnDownLoad.Visible = dt.Rows[0]["button_download"].ToString() == "1" ? true : false;
                    btnrefresh.Visible = dt.Rows[0]["button_refresh"].ToString() == "1" ? true : false;
                    btnviewlist.Visible = dt.Rows[0]["button_viewlist"].ToString() == "1" ? true : false;
                    btnResetPwd.Visible = dt.Rows[0]["button_resetpwd"].ToString() == "1" ? true : false;

                    btnBackup.Visible = dt.Rows[0]["button_backup"].ToString() == "1" ? true : false;
                    btnRestore.Visible = dt.Rows[0]["button_restore"].ToString() == "1" ? true : false;
                    btnImport.Visible = dt.Rows[0]["button_import"].ToString() == "1" ? true : false;
                    btnExport.Visible = dt.Rows[0]["button_export"].ToString() == "1" ? true : false;
                    btnView.Visible = dt.Rows[0]["button_view"].ToString() == "1" ? true : false;
                    btnPrint.Visible = dt.Rows[0]["button_print"].ToString() == "1" ? true : false;
                    btnPreview.Visible = dt.Rows[0]["button_preview"].ToString() == "1" ? true : false;
                    btnSync.Visible = dt.Rows[0]["button_sync"].ToString() == "1" ? true : false;
                    btnOpRecord.Visible = dt.Rows[0]["button_oprecord"].ToString() == "1" ? true : false;
                }
            }
        }
        public delegate void ClickHandler(object sender, EventArgs e);

        #region 事件定义
        /// <summary> 新增事件
        /// </summary>
        public event ClickHandler AddEvent;

        /// <summary> 编辑事件
        /// </summary>
        public event ClickHandler EditEvent;

        /// <summary> 删除事件
        /// </summary>
        public event ClickHandler DeleteEvent;

        /// <summary> 保存事件
        /// </summary>
        public event ClickHandler SaveEvent;


        /// <summary> 取消事件
        /// </summary>
        public event ClickHandler CancelEvent;

        /// <summary> 浏览事件
        /// </summary>
        public event ClickHandler ViewEvent;

        /// <summary> 下载事件
        /// </summary>
        public event ClickHandler DownLoadEvent;

        /// <summary> 启停用事件
        /// </summary>
        public event ClickHandler StatusEvent;

        /// <summary> 备份事件
        /// </summary>
        public event ClickHandler BackupEvent;

        /// <summary> 还原事件
        /// </summary>
        public event ClickHandler RestoreEvent;

        /// <summary> 导入事件
        /// </summary>
        public event ClickHandler ImportEvent;

        /// <summary> 踢出事件
        /// </summary>
        public event ClickHandler ShotoffEvent;

        /// <summary> 重置密码事件
        /// </summary>
        public event ClickHandler ResetPwdEvent;

        /// <summary> 导出事件
        /// </summary>
        public event ClickHandler ExportEvent;

        /// <summary> 打印事件
        /// </summary>
        public event ClickHandler PrintEvent;

        /// <summary> 预览事件
        /// </summary>
        public event ClickHandler PreviewEvent;

        /// <summary> 同步事件
        /// </summary>
        public event ClickHandler SyncEvent;    //add by Kord

        /// <summary> 操作记录事件
        /// </summary>
        public event ClickHandler OpRecordEvent;
        #endregion

        #region 方法
        /// <summary> 根据窗体状态更改控件状态（新增/修改窗体-页签）
        /// </summary>
        /// <param name="status">窗体状态</param>
        public void SetBtnStatus(WindowStatus status)
        {
            windowStatus = status;
            Func<WindowStatus, bool> fc = delegate(WindowStatus ws)
            {
                //根据窗体状态，设置当前窗体的控件的状态
                switch (status)
                {
                    case WindowStatus.Add:
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = true;
                        btnCancel.Enabled = true;
                        btnView.Enabled = false;
                        btnStatus.Enabled = false;

                        btnDownLoad.Enabled = false;
                        btnshotoff.Enabled = false;
                        btnSet.Enabled = false;
                        btnrefresh.Enabled = false;
                        btnviewlist.Enabled = false;
                        btnResetPwd.Enabled = false;

                        btnBackup.Enabled = false;
                        btnRestore.Enabled = false;
                        btnImport.Enabled = false;
                        btnExport.Enabled = false;
                        btnPrint.Enabled = false;
                        btnPreview.Enabled = false;
                        btnSync.Enabled = false;
                        btnOpRecord.Enabled = false;
                        break;
                    case WindowStatus.Edit:
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = true;
                        btnCancel.Enabled = true;
                        btnView.Enabled = false;
                        btnStatus.Enabled = false;

                        btnshotoff.Enabled = false;
                        btnSet.Enabled = false;
                        btnrefresh.Enabled = false;
                        btnviewlist.Enabled = false;
                        btnResetPwd.Enabled = false;
                        btnSet.Enabled = false;

                        btnBackup.Enabled = false;
                        btnRestore.Enabled = false;
                        btnImport.Enabled = false;
                        btnExport.Enabled = false;
                        btnPrint.Enabled = false;
                        btnPreview.Enabled = false;
                        btnSync.Enabled = false;
                        btnOpRecord.Enabled = false;
                        break;
                    case WindowStatus.Save:
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                        btnView.Enabled = true;
                        btnStatus.Enabled = true;

                        btnshotoff.Enabled = true;
                        btnSet.Enabled = true;
                        btnDownLoad.Enabled = true;
                        btnrefresh.Enabled = true;
                        btnviewlist.Enabled = true;
                        btnResetPwd.Enabled = true;

                        btnBackup.Enabled = true;
                        btnRestore.Enabled = true;
                        btnImport.Enabled = true;
                        btnExport.Enabled = true;
                        btnPrint.Enabled = true;
                        btnPreview.Enabled = true;
                        btnSync.Enabled = true;
                        btnOpRecord.Enabled = true;
                        break;
                    case WindowStatus.Cancel:
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                        btnView.Enabled = true;
                        btnStatus.Enabled = true;

                        btnshotoff.Enabled = true;
                        btnSet.Enabled = true;
                        btnDownLoad.Enabled = true;
                        btnrefresh.Enabled = true;
                        btnviewlist.Enabled = true;
                        btnResetPwd.Enabled = true;

                        btnBackup.Enabled = true;
                        btnRestore.Enabled = true;
                        btnImport.Enabled = true;
                        btnExport.Enabled = true;
                        btnPrint.Enabled = true;
                        btnPreview.Enabled = true;
                        btnSync.Enabled = true;
                        btnOpRecord.Enabled = true;
                        break;
                    case WindowStatus.View:
                        btnAdd.Enabled = false;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;
                        btnView.Enabled = false;
                        btnStatus.Enabled = false;

                        btnshotoff.Enabled = false;
                        btnSet.Enabled = false;
                        btnDownLoad.Enabled = false;
                        btnrefresh.Enabled = false;
                        btnviewlist.Enabled = false;
                        btnResetPwd.Enabled = false;

                        btnBackup.Enabled = false;
                        btnRestore.Enabled = false;
                        btnImport.Enabled = false;
                        btnExport.Enabled = false;
                        btnPrint.Enabled = false;
                        btnPreview.Enabled = false;
                        btnSync.Enabled = false;
                        btnOpRecord.Enabled = false;
                        break;
                    case WindowStatus.Normal:
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                        btnView.Enabled = true;
                        btnStatus.Enabled = true;

                        btnshotoff.Enabled = true;
                        btnSet.Enabled = true;
                        btnDownLoad.Enabled = true;
                        btnrefresh.Enabled = true;
                        btnviewlist.Enabled = true;
                        btnResetPwd.Enabled = true;

                        btnBackup.Enabled = true;
                        btnRestore.Enabled = true;
                        btnImport.Enabled = true;
                        btnExport.Enabled = true;
                        btnPrint.Enabled = true;
                        btnPreview.Enabled = true;
                        btnSync.Enabled = true;
                        btnOpRecord.Enabled = true;
                        //设置隐藏
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        break;
                    default:
                        break;
                }
                return true;
            };
            this.BeginInvoke(fc, new object[] { status });
        }
        #endregion

        #region 按钮事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddEvent != null)
            {
                AddEvent(sender, e);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveEvent != null)
            {
                SaveEvent(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelEvent != null)
            {
                CancelEvent(sender, e);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (ViewEvent != null)
            {
                ViewEvent(sender, e);
            }
        }


        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (DownLoadEvent != null)
            {
                DownLoadEvent(sender, e);
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            //更新状态
            if (btnStatus.Caption == "启用")
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
            if (StatusEvent != null)
            {
                StatusEvent(sender, e);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (BackupEvent != null)
            {
                BackupEvent(sender, e);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (RestoreEvent != null)
            {
                RestoreEvent(sender, e);
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


        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(sender, e);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (PreviewEvent != null)
            {
                PreviewEvent(sender, e);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)  //add by Kord
        {
            if (SyncEvent != null)
            {
                SyncEvent(sender, e);
            }
        }

        private void btnOpRecord_Click(object sender, EventArgs e)
        {
            if (OpRecordEvent != null)
            {
                OpRecordEvent(sender, e);
            }
        }


        private void btnResetPwd_Click(object sender, EventArgs e)
        {
            if (ResetPwdEvent != null)
            {
                ResetPwdEvent(sender, e);
            }

        }

        private void btnshotoff_Click(object sender, EventArgs e)
        {
            if (ShotoffEvent != null)
            {
                ShotoffEvent(sender, e);
            }
        }
        #endregion

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
        View,
        /// <summary> 默认初始化
        /// </summary>
        Normal
    }
}
