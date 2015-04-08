using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    public partial class UCLoginPCSet : UserControl
    {
        public UCLoginPCSet()
        {
            InitializeComponent();
        }
        /// <summary> 查询条件
        /// </summary>
        string where = string.Empty;
        WindowStatus windowStatus = WindowStatus.View;

        /// <summary> 操作行索引
        /// </summary>
        private int editRowIndex = -1;

        private void UCLoginPCSet_Load(object sender, EventArgs e)
        {
            SetBtnStatus(windowStatus);
            DataGridViewEx.SetDataGridViewStyle(dgvPCList);
            this.dgvPCList.ReadOnly = false;

            this.BindCmb();
            BindPageData(where);
        }

        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtremark_name.Caption = string.Empty;
            txtpc_name.Caption = string.Empty;
            txtmac_address.Text = string.Empty;
            cmbis_allow_login.SelectedIndex = 0;
        }

        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            where = string.Format(" 1=1 ");
            if (!string.IsNullOrEmpty(txtremark_name.Caption.Trim()))
            {
                where += string.Format(" and  remark_name like '%{0}%'", txtremark_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtpc_name.Caption.Trim()))
            {
                where += string.Format(" and  pc_name like '%{0}%'", txtpc_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtmac_address.Caption))
            {
                where += string.Format(" and  mac_address = '{0}'", txtmac_address.Caption.ToString());
            }
            if (cmbis_allow_login.SelectedIndex > 0)
            {
                ListItem listItem = cmbis_allow_login.SelectedItem as ListItem;
                where += string.Format(" and  is_allow_login = '{0}'", listItem.Value);
            }
            page.PageIndex = 1;
            BindPageData(where);
        }

        /// <summary> 新增
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            editRowIndex = 0;
            if (dgvPCList.CurrentRow != null)
            {
                editRowIndex = dgvPCList.CurrentRow.Index;
            }
            dgvPCList.Rows.Insert(editRowIndex, 1);
            SetDefaultRowValue();
            CommonUtility.SetDgvEditCellBgColor(dgvPCList.Rows[editRowIndex], new string[] { "remark_name", "pc_name", "mac_address", "is_allow_login" }, true);
            SetBtnStatus(WindowStatus.Add);
        }
        /// <summary> 编辑
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPCList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！");
                return;
            }
            editRowIndex = dgvPCList.CurrentRow.Index;
            CommonUtility.SetDgvEditCellBgColor(dgvPCList.Rows[editRowIndex], new string[] { "remark_name", "pc_name", "mac_address", "is_allow_login" }, true);
            SetBtnStatus(WindowStatus.Edit);
        }

        /// <summary> 禁止
        /// </summary>
        private void btnForbid_Click(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvPCList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["login_pc_set_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择操作记录！");
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("is_allow_login", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量禁止登录电脑表", "sys_login_pc_set", dicField, "login_pc_set_id", listField.ToArray());
            if (flag)
            {
                BindPageData(where);
                if (dgvPCList.Rows.Count > 0)
                {
                    dgvPCList.CurrentCell = dgvPCList.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        /// <summary> 允许
        /// </summary>
        private void btnAllow_Click(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvPCList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["login_pc_set_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择操作的记录！");
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("is_allow_login", "1");
            bool flag = DBHelper.BatchUpdateDataByIn("批量允许登录电脑表", "sys_login_pc_set", dicField, "login_pc_set_id", listField.ToArray());
            if (flag)
            {
                BindPageData(where);
                if (dgvPCList.Rows.Count > 0)
                {
                    dgvPCList.CurrentCell = dgvPCList.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvPCList.EndEdit();
            if (editRowIndex < 0)
            {
                return;
            }
            if (!ValidateValue())
            {
                return;
            }
            bool flag = SaveRecord();
            if (flag)
            {
                CommonUtility.SetDgvEditCellBgColor(dgvPCList.Rows[editRowIndex], new string[] { "remark_name", "pc_name", "mac_address", "is_allow_login" }, false);
                dgvPCList.CurrentCell = dgvPCList.Rows[editRowIndex].Cells[0];
                SetBtnStatus(WindowStatus.Save);
                editRowIndex = -1;
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (editRowIndex < 0 || dgvPCList.Rows.Count == 0)
            {
                return;
            }
            BindPageData(where);
            if (dgvPCList.Rows.Count > 0)
            {
                dgvPCList.CurrentCell = dgvPCList.Rows[editRowIndex].Cells[0];
            }
            SetBtnStatus(WindowStatus.Cancel);
            editRowIndex = -1;
        }

        /// <summary> 页码改变事件
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData(where);
        }

        /// <summary> 控制批号单元格编辑
        /// </summary>
        private void dgvPCList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)//复选框列可编辑
            {
                return;
            }
            //判断是否编辑列
            if (e.RowIndex != editRowIndex)
            {
                e.Cancel = true;
            }
            string field = dgvPCList.Columns[e.ColumnIndex].DataPropertyName;
            if (field != "remark_name" && field != "pc_name" && field != "mac_address" && field != "is_allow_login")//可编辑字段
            {
                // 取消编辑 
                e.Cancel = true;
            }
        }
        #endregion

        #region 方法
        /// <summary> 保存验证
        /// </summary>
        /// <returns></returns>
        public bool ValidateValue()
        {
            DataGridViewRow row = dgvPCList.Rows[editRowIndex];
            object objRemarkName = row.Cells["remark_name"].Value;
            if (objRemarkName == null || string.IsNullOrEmpty(objRemarkName.ToString()))
            {
                MessageBoxEx.Show("备注名称不能为空！");
                dgvPCList.CurrentCell = row.Cells["remark_name"];
                return false;
            }
            object objPCName = row.Cells["pc_name"].Value;
            if (objPCName == null || string.IsNullOrEmpty(objPCName.ToString()))
            {
                MessageBoxEx.Show("计算机名不能为空！");
                dgvPCList.CurrentCell = row.Cells["pc_name"];
                return false;
            }
            object objMACAddress = row.Cells["mac_address"].Value;
            if (objMACAddress == null || string.IsNullOrEmpty(objMACAddress.ToString()))
            {
                MessageBoxEx.Show("MAC地址不能为空！");
                dgvPCList.CurrentCell = row.Cells["mac_address"];
                return false;
            }
            string isAllowLogin = row.Cells["is_allow_login"].Value.ToString();
            if (string.IsNullOrEmpty(isAllowLogin))
            {
                MessageBoxEx.Show("请选择是否允许登陆！");
                dgvPCList.CurrentCell = row.Cells["is_allow_login"];
                return false;
            }
            return true;
        }

        /// <summary> 绑定Combox
        /// </summary>
        private void BindCmb()
        {
            cmbis_allow_login.DataSource = DataSources.EnumToList(typeof(DataSources.EnumYesNo), true);
            is_allow_login.DisplayMember = "Text";
            is_allow_login.ValueMember = "Value";

            is_allow_login.DataSource = DataSources.EnumToList(typeof(DataSources.EnumYesNo), false);
            is_allow_login.DisplayMember = "Text";
            is_allow_login.ValueMember = "Value";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询登陆电脑设置", "sys_login_pc_set", "*", where, "", "update_time desc", page.PageIndex, page.PageSize, out recordCount);
            BindDgv(dt);
            page.RecordCount = recordCount;
        }

        /// <summary> 新增时设置默认数据
        /// </summary>
        private void SetDefaultRowValue()
        {
            DataGridViewRow row = dgvPCList.Rows[editRowIndex];
            row.Cells["login_pc_set_id"].Value = Guid.NewGuid().ToString();
            row.Cells["remark_name"].Value = "";
            row.Cells["pc_name"].Value = "";
            row.Cells["mac_address"].Value = "";
            row.Cells["is_allow_login"].Value = 1;
        }

        /// <summary> 添加登录电脑
        /// </summary>
        private bool SaveRecord()
        {
            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增登录电脑";
            DataGridViewRow row = dgvPCList.Rows[editRowIndex];
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            if (windowStatus == WindowStatus.Add)
            {
                dicFileds.Add("login_pc_set_id", row.Cells["login_pc_set_id"].Value.ToString());
                dicFileds.Add("create_by", GlobalStaticObj.UserID);
                dicFileds.Add("create_time", nowUtcTicks);
            }
            dicFileds.Add("remark_name", row.Cells["remark_name"].Value.ToString());
            dicFileds.Add("pc_name", row.Cells["pc_name"].Value.ToString());
            dicFileds.Add("mac_address", row.Cells["mac_address"].Value.ToString());
            dicFileds.Add("update_time", nowUtcTicks);
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            if (windowStatus == WindowStatus.Edit)
            {
                keyName = "login_pc_set_id";
                keyValue = row.Cells["login_pc_set_id"].Value.ToString();
                opName = "更新登录电脑";
            }
            return DBHelper.Submit_AddOrEdit(opName, "sys_login_pc_set", keyName, keyValue, dicFileds);
        }

        /// <summary> 绑定字典
        /// </summary>
        /// <param name="dt">字典数据表</param>
        private void BindDgv(DataTable dt)
        {
            dgvPCList.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int rowIndex = dgvPCList.Rows.Add(1);
                dgvPCList.Rows[rowIndex].Cells["login_pc_set_id"].Value = dr["login_pc_set_id"].ToString();
                dgvPCList.Rows[rowIndex].Cells["remark_name"].Value = dr["remark_name"].ToString();
                dgvPCList.Rows[rowIndex].Cells["pc_name"].Value = dr["pc_name"].ToString();
                dgvPCList.Rows[rowIndex].Cells["mac_address"].Value = dr["mac_address"].ToString();
                if (dr["is_allow_login"].ToString().Length > 0)
                {
                    dgvPCList.Rows[rowIndex].Cells["is_allow_login"].Value = Convert.ToInt32(dr["is_allow_login"].ToString());
                }
            }
        }

        /// <summary> 根据窗体状态更改控件状态（操作本页数据）
        /// </summary>
        /// <param name="status">窗体状态</param>                 
        public void SetBtnStatus(WindowStatus status)
        {
            windowStatus = status;
            //根据窗体状态，设置当前窗体的控件的状态
            switch (status)
            {
                case WindowStatus.Add:
                    btnAdd.Enabled = false;
                    btnForbid.Enabled = false;
                    btnAllow.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case WindowStatus.Edit:
                    btnAdd.Enabled = false;
                    btnForbid.Enabled = false;
                    btnAllow.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
                case WindowStatus.Save:
                    btnAdd.Enabled = true;
                    btnForbid.Enabled = true;
                    btnAllow.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                case WindowStatus.Cancel:
                    btnAdd.Enabled = true;
                    btnForbid.Enabled = true;
                    btnAllow.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                case WindowStatus.View:
                    btnAdd.Enabled = true;
                    btnForbid.Enabled = true;
                    btnAllow.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
