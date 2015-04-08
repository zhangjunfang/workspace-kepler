using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXC_FuncUtility;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using BLL;
using HXCServerWinForm.UCForm.AcountSet;

namespace HXCServerWinForm.UCForm.AutoBackupSet
{
    public partial class UCAutoBackupSet : UCBase
    {
        private string where = string.Empty;
        List<string> listStart = new List<string>();
        List<string> listStop = new List<string>();

        public UCAutoBackupSet()
        {
            InitializeComponent();
        }

        private void UCAutoBackupSet_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvtasklist);
            dgvtasklist.ReadOnly = false;
            base.AddEvent += new ClickHandler(UCAutoBackupSet_AddEvent);
            base.EditEvent += new ClickHandler(UCAutoBackupSet_EditEvent);
            base.DeleteEvent += new ClickHandler(UCAutoBackupSet_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCAutoBackupSet_StatusEvent);
            base.btnStatus.Enabled = false;
            BindCmb();
            btnSearch_Click(null, null);
        }

        #region 事件

        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            where = string.Format("enable_flag='{0}' ", ((int)DataSources.EnumEnableFlag.USING));
            if (!string.IsNullOrEmpty(txtsetbook_code.Caption.Trim()))
            {
                where += string.Format(" and  setbook_code like '%{0}%'", txtsetbook_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtsetbook_name.Caption.Trim()))
            {
                where += string.Format(" and  setbook_name like '%{0}%'", txtsetbook_name.Caption.Trim());
            }
            if (cmstatus.SelectedIndex > 0)
            {
                where += string.Format(" and  status = '{0}'", cmstatus.SelectedValue);
            }
            if (!string.IsNullOrEmpty(dtpstart.Value))
            {
                long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpstart.Value));
                where += " and create_time>=" + startTicks.ToString();
            }
            if (!string.IsNullOrEmpty(dtpend.Value))
            {
                long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpend.Value).AddDays(1));
                where += " and create_time<" + endTicks.ToString();
            }
            BindData(where);
        }

        /// <summary> 清除查询条件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtsetbook_code.Caption = string.Empty;
            txtsetbook_name.Caption = string.Empty;
            cmstatus.SelectedIndex = 0;
            dtpstart.Value = "";
            dtpend.Value = "";
        }

        /// <summary> 新增
        /// </summary>
        void UCAutoBackupSet_AddEvent(object sender, EventArgs e)
        {
            frmAutoBackupSetEdit frm = new frmAutoBackupSetEdit();
            frm.isAdd = true;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BindData(where);
                if (dgvAccList.RowCount > 0)
                {
                    dgvtasklist.CurrentCell = dgvtasklist.Rows[dgvtasklist.RowCount - 1].Cells[0];
                }
            }
        }

        /// <summary> 编辑
        /// </summary>
        void UCAutoBackupSet_EditEvent(object sender, EventArgs e)
        {
            if (dgvtasklist.CurrentRow == null)
            {
                return;
            }
            int currRowIndex = dgvtasklist.CurrentRow.Index;
            frmAutoBackupSetEdit frm = new frmAutoBackupSetEdit();
            frm.isAdd = false;
            frm.auto_backup_set_id = dgvtasklist.CurrentRow.Cells["auto_backup_set_id"].Value.ToString();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BindData(where);
                dgvtasklist.CurrentCell = dgvtasklist.Rows[currRowIndex].Cells[0];
            }
        }

        /// <summary> 删除
        /// </summary>
        void UCAutoBackupSet_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvtasklist.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["auto_backup_set_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！");
                return;
            }
            if (MessageBoxEx.Show("将要删除选中任务，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除选中任务", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_auto_backup_set", dicField, "auto_backup_set_id", listField.ToArray());
            if (flag)
            {
                BindData(where);
                if (dgvtasklist.Rows.Count > 0)
                {
                    dgvtasklist.CurrentCell = dgvtasklist.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 启用停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCAutoBackupSet_StatusEvent(object sender, EventArgs e)
        {
            if (listStart.Count == 0 && listStop.Count == 0)
            {
                MessageBoxEx.Show("请选择" + base.btnStatus.Caption + "任务！");
                return;
            }
            DataSources.EnumStatus enumStatus = listStart.Count > 0 ? DataSources.EnumStatus.Start : DataSources.EnumStatus.Stop;
            string[] arrField; ;
            if (listStart.Count > 0)
            {
                arrField = new string[listStart.Count];
                listStart.CopyTo(arrField);
            }
            else
            {
                arrField = new string[listStop.Count];
                listStop.CopyTo(arrField);
            }

            if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中任务，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("status", enumStatus == DataSources.EnumStatus.Start ? Convert.ToInt16(DataSources.EnumStatus.Stop).ToString() : Convert.ToInt16(DataSources.EnumStatus.Start).ToString());
            bool flag = DBHelper.BatchUpdateDataByIn("批量" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中任务", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_auto_backup_set", dicField, "auto_backup_set_id", arrField);
            if (flag)
            {
                BindData(where);
                if (dgvtasklist.Rows.Count > 0)
                {
                    dgvtasklist.CurrentCell = dgvtasklist.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！", "系统提示");
            }
            else
            {
                MessageBoxEx.Show("操作失败！", "系统提示");
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvtasklist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string fieldNmae = dgvtasklist.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae == "setbook_code")
            {
                string book_id = dgvtasklist.Rows[e.RowIndex].Cells["book_id"].Value.ToString();
                if (book_id == "000")
                {
                    dgvtasklist.Rows[e.RowIndex].Cells["setbook_code"].Value = "---";
                    dgvtasklist.Rows[e.RowIndex].Cells["setbook_name"].Value = "所有账套";
                }
            }           
            if (fieldNmae.Equals("auto_backup_date"))
            {
                DataSources.EnumAutoBackupType enumType = (DataSources.EnumAutoBackupType)Convert.ToInt32(dgvtasklist.Rows[e.RowIndex].Cells["auto_backup_type"].Value.ToString());
                int intervalValue = Convert.ToInt32(dgvtasklist.Rows[e.RowIndex].Cells["auto_backup_interval"].Value.ToString());
                string value = "";
                if (enumType == DataSources.EnumAutoBackupType.EveryWeek)
                {
                    value = "每周" + DataSources.GetDescription((DataSources.EnumWeek)intervalValue, false);
                }
                else if (enumType == DataSources.EnumAutoBackupType.EveryMonth)
                {
                    value = "每月" + intervalValue + "号";
                }
                else if (enumType == DataSources.EnumAutoBackupType.EveryDay)
                {
                    value = "每" + intervalValue + "天";
                }
                e.Value = value;
            }

            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            } 

            if (fieldNmae.Equals("status"))
            {
                DataSources.EnumStatus enumStatus = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumStatus, true);
            }
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("auto_backup_starttime"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }

        /// <summary> 控制单元格编辑
        /// </summary>
        private void dgvtasklist_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)//复选框列可编辑
            {
                e.Cancel = true;
            }
        }

        /// <summary> 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvtasklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataSources.EnumStatus enumState = (DataSources.EnumStatus)Convert.ToInt32(dgvtasklist.Rows[e.RowIndex].Cells["status"].Value.ToString());
                string code = dgvtasklist.Rows[e.RowIndex].Cells["auto_backup_set_id"].Value.ToString();
                bool flag = Convert.ToBoolean(dgvtasklist.Rows[e.RowIndex].Cells[0].EditedFormattedValue);
                if (flag)//选中
                {
                    if (enumState == DataSources.EnumStatus.Start)
                    {
                        if (!listStart.Contains(code))
                        {
                            listStart.Add(code);
                        }
                    }
                    else
                    {
                        if (!listStop.Contains(code))
                        {
                            listStop.Add(code);
                        }
                    }
                }
                else
                {
                    if (enumState == DataSources.EnumStatus.Start)
                    {
                        if (listStart.Contains(code))
                        {
                            listStart.Remove(code);
                        }
                    }
                    else
                    {
                        if (listStop.Contains(code))
                        {
                            listStop.Remove(code);
                        }
                    }
                }
                SetBtnStatus(false);
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定下拉框
        /// </summary>
        public void BindCmb()
        {
            List<ListItem> list = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);
            cmstatus.DataSource = list;
            cmstatus.ValueMember = "value";
            cmstatus.DisplayMember = "text";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindData(string where)
        {
            DataTable dt = DBHelper.GetTable("分页查询任务信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_sys_auto_backup_set", "*", where, "", "order by create_time");
            dgvtasklist.DataSource = dt;
            SetBtnStatus(true);
        }

        /// <summary> 设置按钮状态
        /// </summary>
        public void SetBtnStatus(bool isInit)
        {
            if (isInit)
            {
                listStart = new List<string>();
                listStop = new List<string>();
            }
            bool isCanUse = (listStart.Count == 0 && listStop.Count > 0) || (listStart.Count > 0 && listStop.Count == 0);
            base.btnStatus.Enabled = isCanUse;
            base.btnStatus.Caption = listStart.Count > 0 ? "停用" : "启用";
        }
        #endregion

    }
}
