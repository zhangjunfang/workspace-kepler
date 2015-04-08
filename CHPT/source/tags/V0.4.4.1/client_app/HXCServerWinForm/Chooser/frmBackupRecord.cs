using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using System.IO;
using System.Diagnostics;

namespace HXCServerWinForm.Chooser
{
    public partial class frmBackupRecord : FormEx
    {
        #region 外部属性
        /// <summary> 是否选择器
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary> 是否自动备份
        /// </summary>
        public bool IsAutoBackupType = false;

        /// <summary> >帐套编码
        /// </summary
        public string Acc_Code { get; set; }
        /// <summary> >帐套名称
        /// </summary
        public string Acc_Name { get; set; }
        /// <summary> 文件名
        /// </summary>
        public string FileName { get; set; }
        #endregion

        public frmBackupRecord()
        {
            InitializeComponent();
        }
        private void frmBackupRecord_Load(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                is_success.Visible = false;
                bak_failmsg.Visible = false;
                txtacccode.ReadOnly = true;
                txtacccode.Caption = this.Acc_Code;
                txtaccname.Caption = this.Acc_Name;
                txtaccname.ReadOnly = true;
            }
            else
            {
                txtFileDir.Caption = GlobalStaticObj_Server.Instance.DbServerBackDir;
            }
            BindCmb();
            if (IsAutoBackupType)
            {
                cmmethod.SelectedIndex = 2;
                cmmethod.Enabled = false;
            }
            BindPageData();
        }

        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindPageData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 清除查询条件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsSelected)
                {
                    txtacccode.Caption = string.Empty;
                    txtaccname.Caption = string.Empty;
                }
                dtpstart.Value = string.Empty;
                dtpend.Value = string.Empty;
                cmmethod.SelectedIndex = 0;
                txtacccode.Focus();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvBakList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            try
            {
                string fieldNmae = dgvBakList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("bak_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
                if (fieldNmae.Equals("bak_method"))
                {
                    DataSources.EnumBackupMethod enumStatus = (DataSources.EnumBackupMethod)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumStatus, true);
                }
                if (fieldNmae.Equals("is_success"))
                {
                    DataSources.EnumYesNo enumStatus = (DataSources.EnumYesNo)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumStatus, true);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
            }

        }
        /// <summary> 选择备份文件
        /// </summary>
        private void dgvBakList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && IsSelected)
                {
                    this.FileName = dgvBakList.Rows[e.RowIndex].Cells["bak_filename"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileDir.Caption.Trim()))
            {
                return;
            }
            try
            {
                if (GlobalStaticObj_Server.Instance.DbIP == GlobalStaticObj_Server.Instance.LoginIP)
                {
                    if (Directory.Exists(txtFileDir.Caption.Trim()))
                    {
                        System.Diagnostics.Process.Start("Explorer.exe", txtFileDir.Caption.Trim());
                    }
                }
                else
                {
                    Process.Start("mstsc.exe");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 页跳转
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPageData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定下拉框
        /// </summary>
        public void BindCmb()
        {
            List<ListItem> list = DataSources.EnumToList(typeof(DataSources.EnumBackupMethod), true);
            cmmethod.DataSource = list;
            cmmethod.ValueMember = "value";
            cmmethod.DisplayMember = "text";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData()
        {
            string where = "1=1 ";
            #region 查询条件
            if (IsSelected)
            {
                where += string.Format(" and  is_success = '{0}'", (int)DataSources.EnumYesNo.Yes);
            }
            if (!string.IsNullOrEmpty(txtacccode.Caption.Trim()))
            {
                where += string.Format(" and  bak_acccode like '%{0}%'", txtacccode.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtaccname.Caption.Trim()))
            {
                where += string.Format(" and  bak_accname like '%{0}%'", txtaccname.Caption.Trim());
            }

            if (cmmethod.SelectedIndex > 0)
            {
                where += string.Format(" and  bak_method = '{0}'", cmmethod.SelectedValue);
            }
            if (!string.IsNullOrEmpty(dtpstart.Value))
            {
                long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpstart.Value));
                where += " and bak_time>=" + startTicks.ToString();
            }
            if (!string.IsNullOrEmpty(dtpend.Value))
            {
                long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpend.Value).AddDays(1));
                where += " and bak_time<" + endTicks.ToString();
            }
            #endregion

            try
            {
                int recordCount = 0;
                DataTable dt = DBHelper.GetTableByPage("选择器查询备份记录表", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_backup_record", "*", where, "", "order by bak_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvBakList.DataSource = dt;
                page.RecordCount = recordCount;
                page.SetBtnState();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-备份记录", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }

        }
        #endregion

    }
}
