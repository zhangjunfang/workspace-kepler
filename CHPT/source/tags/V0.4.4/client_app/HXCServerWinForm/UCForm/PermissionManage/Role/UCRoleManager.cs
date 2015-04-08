using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCServerWinForm.CommonClass;
using Utility.Common;
using SYSModel;
using System.Collections;
using BLL;
using HXC_FuncUtility;
using System.IO;
using Utility.CommonForm;

namespace HXCServerWinForm.UCForm.Role
{
    /// <summary>
    /// 角色管理
    /// 孙明生
    /// </summary>
    public partial class UCRoleManager : UCBase
    {
        #region 属性
        private string where = string.Format(" enable_flag='1' ");//enable_flag 1未删除

        List<string> listStart = new List<string>();
        List<string> listStop = new List<string>();
        #endregion

        #region 初始化
        public UCRoleManager()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCRoleManager_AddEvent);
            base.EditEvent += new ClickHandler(UCRoleManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRoleManager_DeleteEvent);
            base.ExportEvent += new ClickHandler(UCRoleManager_ExportEvent);
            base.StatusEvent += new ClickHandler(UCRoleManager_StatusEvent);
        }

        #endregion

        #region Load
        private void UCRoleManager_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//角色按钮权限-是否隐藏
            base.SetBtnStatus(WindowStatus.Normal);
            dgvRole.ReadOnly = false;
            DataSources.BindComBoxDataEnum(ddlstate, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataGridViewEx.SetDataGridViewStyle(dgvRole);
            btnQuery_Click(null, null);
        }
        #endregion

        #region 事件
        /// <summary> 转向添加
        /// </summary> 
        void UCRoleManager_AddEvent(object sender, EventArgs e)
        {
            try
            {
                UCRoleAddOrEdit UCRoleAddOrEdit = new UCRoleAddOrEdit();
                UCRoleAddOrEdit.uc = this;
                UCRoleAddOrEdit.windowStatus = WindowStatus.Add;
                base.addUserControl(UCRoleAddOrEdit, "角色管理-新增", "RoleAdd", this.Tag.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        /// <summary> 去编辑
        /// </summary> 
        void UCRoleManager_EditEvent(object sender, EventArgs e)
        {
            if (dgvRole.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (dgvRole.CurrentRow.Cells["role_code"].Value.ToString() == "system")
                    return;
                UCRoleAddOrEdit RoleEdit = new UCRoleAddOrEdit();
                RoleEdit.uc = this;
                RoleEdit.windowStatus = WindowStatus.Edit;
                RoleEdit.id = dgvRole.CurrentRow.Cells["role_id"].Value.ToString();  //参数 角色管理ID
                base.addUserControl(RoleEdit, "角色管理-编辑", "RoleEdit" + RoleEdit.id, this.Tag.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        /// <summary> 删除
        /// </summary> 
        void UCRoleManager_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRole.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["role_id"].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool flag = false;
                foreach (string str in listField)
                {
                    //删除关联信息
                    string keyName = "role_id";
                    string keyValue = str;
                    string opName = "删除用户角色关系";
                    DBHelper.DeleteDataByID(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_user_role", keyName, keyValue);
                    opName = "删除菜单角色关系";
                    DBHelper.DeleteDataByID(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_role_function", keyName, keyValue);

                }
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", "0");
                flag = DBHelper.BatchUpdateDataByIn("批量删除角色", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_role", comField, "role_id", listField.ToArray());
                if (flag)
                {
                    BindData();
                    if (dgvRole.Rows.Count > 0)
                    {
                        dgvRole.CurrentCell = dgvRole.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        void UCRoleManager_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string fileName = "角色" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvRole);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 查询
        /// </summary> 
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 清除
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtrole_code.Caption = string.Empty;
            txtrole_name.Caption = string.Empty;
            ddlstate.SelectedIndex = 0;
            dtpstart.Value = string.Empty;
            dtpend.Value = string.Empty;
        }

        #region dgv事件
        private void dgvRole_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 0)
                {
                    e.Cancel = true;
                }
                if (dgvRole.Rows[e.RowIndex].Cells["role_code"].Value.ToString() == "system")
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        /// <summary>
        /// 双击时间 去浏览页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string id = dgvRole.Rows[e.RowIndex].Cells["role_id"].Value.ToString();
                    UCRoleAddOrEdit uc = new UCRoleAddOrEdit();
                    uc.uc = this;
                    uc.windowStatus = WindowStatus.View;
                    uc.id = id;
                    base.addUserControl(uc, "角色管理-查看", "RoleView" + id, this.Tag.ToString(), this.Name);
                }
                catch (Exception ex)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                    MessageBoxEx.ShowWarning("程序异常");
                }
            }
        }

        private void dgvRole_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            try
            {
                string fieldNmae = dgvRole.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
                if (fieldNmae.Equals("enable_flag"))
                {
                    DataSources.EnumEnableFlag enumEnableFlag = (DataSources.EnumEnableFlag)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumEnableFlag, true);
                }
                if (fieldNmae.Equals("state"))
                {
                    SYSModel.DataSources.EnumStatus EnumStatus = (SYSModel.DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(EnumStatus, true);
                }
                if (fieldNmae.Equals("data_sources"))
                {
                    SYSModel.DataSources.EnumDataSources EnumDataSources = (SYSModel.DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(EnumDataSources, true);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion

        #endregion

        #region 方法
        /// <summary> 查询绑定数据
        /// </summary>
        public void BindData()
        {
            try
            {
                where = string.Format(" enable_flag='1' ");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtrole_code.Caption.Trim()))//角色编码
                {
                    where += string.Format(" and  role_code like '%{0}%'", txtrole_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtrole_name.Caption.Trim()))//角色名
                {
                    where += string.Format(" and  role_name like '%{0}%'", txtrole_name.Caption.Trim());
                }
                string state = CommonCtrl.IsNullToString(ddlstate.SelectedValue);
                if (!string.IsNullOrEmpty(state)) //状态
                {
                    where += string.Format(" and  state = '{0}'", state);
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

                DataTable dt = DBHelper.GetTable("查询角色管理", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_role", "*", where, "", "order by role_id");
                dgvRole.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary> 保存后更新列表
        /// </summary>
        /// <param name="strRole_id">选中行的 角色ID</param>
        public void SaveAfter(string strRole_id)
        {
            BindData();
            foreach (DataGridViewRow dr in dgvRole.Rows)
            {
                object com_id = dr.Cells["role_id"].EditedFormattedValue;
                if (com_id != null && (string)com_id == strRole_id)
                {
                    dr.Selected = true;
                }
            }
        }
        #endregion

        #region --启用停用
        /// <summary> 启动或者停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCRoleManager_StatusEvent(object sender, EventArgs e)
        {
            try
            {
                if (listStart.Count == 0 && listStop.Count == 0)
                {
                    MessageBoxEx.Show("请选择" + base.btnStatus.Caption + "记录！");
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

                if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中记录，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("state", enumStatus == DataSources.EnumStatus.Start ? Convert.ToInt16(DataSources.EnumStatus.Stop).ToString() : Convert.ToInt16(DataSources.EnumStatus.Start).ToString());
                bool flag = DBHelper.BatchUpdateDataByIn("批量" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中记录", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_role", dicField, "role_id", arrField);
                if (flag)
                {
                    listStart = new List<string>();
                    listStart = new List<string>();
                    BindData();
                    if (dgvRole.Rows.Count > 0)
                    {
                        dgvRole.CurrentCell = dgvRole.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("操作成功！", "系统提示");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！", "系统提示");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 单元格单击事件
        /// </summary>
        private void dgvRole_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvRole.EndEdit();
                listStart = new List<string>();
                listStop = new List<string>();
                foreach (DataGridViewRow row in dgvRole.Rows)
                {
                    bool flag = Convert.ToBoolean(row.Cells["colCheck"].Value);

                    if (flag)//选中
                    {
                        object obj = row.Cells["state"].Value;
                        if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                        {
                            return;
                        }
                        DataSources.EnumStatus enumState = (DataSources.EnumStatus)Convert.ToInt32(obj.ToString());
                        string id = row.Cells["role_id"].Value.ToString();
                        if (enumState == DataSources.EnumStatus.Start)
                        {
                            if (!listStart.Contains(id))
                            {
                                listStart.Add(id);
                            }
                        }
                        else
                        {
                            if (!listStop.Contains(id))
                            {
                                listStop.Add(id);
                            }
                        }
                    }
                }
                SetBtnStatus(false);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
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
