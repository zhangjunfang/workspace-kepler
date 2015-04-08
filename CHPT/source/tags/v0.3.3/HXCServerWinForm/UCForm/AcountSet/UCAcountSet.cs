using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCServerWinForm.CommonClass;
using HXCServerWinForm.Chooser;

namespace HXCServerWinForm.UCForm.AcountSet
{
    public partial class UCAcountSet : UCBase
    {
        private string where = string.Empty;
        List<string> listStart = new List<string>();
        List<string> listStop = new List<string>();
        public UCAcountSet()
        {
            InitializeComponent();
        }

        private void UCAcountSet_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvAccList);
            dgvAccList.ReadOnly = false;
            base.AddEvent += new ClickHandler(UCAcountSet_AddEvent);
            base.EditEvent += new ClickHandler(UCAcountSet_EditEvent);
            base.DeleteEvent += new ClickHandler(UCAcountSet_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCAcountSet_StatusEvent);
            base.ResetPwdEvent += new ClickHandler(UCAcountSet_ResetPwdEvent);
            base.BackupEvent += new ClickHandler(UCAcountSet_BackupEvent);
            base.RestoreEvent += new ClickHandler(UCAcountSet_RestoreEvent);
            base.OpRecordEvent += new ClickHandler(UCAcountSet_OpRecordEvent);
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
        void UCAcountSet_AddEvent(object sender, EventArgs e)
        {
            if (dgvAccList.Rows.Count >= 5)
            {
                MessageBoxEx.Show("对不起，系统只支持创建五个帐套", "系统提示");
                return;
            }
            frmAccountEdit frm = new frmAccountEdit();
            frm.isAdd = true;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BindData(where);
                dgvAccList.CurrentCell = dgvAccList.Rows[dgvAccList.RowCount - 1].Cells[0];
            }
        }

        /// <summary> 编辑
        /// </summary>
        void UCAcountSet_EditEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                return;
            }
            int currRowIndex = dgvAccList.CurrentRow.Index;
            frmAccountEdit frm = new frmAccountEdit();
            frm.isAdd = false;
            frm.id = dgvAccList.CurrentRow.Cells["id"].Value.ToString();
            frm.isAdd = false;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BindData(where);
                dgvAccList.CurrentCell = dgvAccList.Rows[currRowIndex].Cells[0];
            }
        }

        /// <summary> 删除
        /// </summary>
        void UCAcountSet_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvAccList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["setbook_code"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！");
                return;
            }
            if (MessageBoxEx.Show("将要删除选中帐套，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除选中帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", dicField, "setbook_code", listField.ToArray());
            if (flag)
            {
                #region 分离被删除的数据库
                foreach (string str in listField)
                {
                    string sqlStr = string.Format("if db_id('{0}') is not null exec sp_detach_db {0}", GlobalStaticObj_Server.DbPrefix + str);
                    DBHelper.ExtNonQuery("分离数据库", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlStr, CommandType.Text, null);
                }
                #endregion

                BindData(where);
                if (dgvAccList.Rows.Count > 0)
                {
                    dgvAccList.CurrentCell = dgvAccList.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 重置密码
        /// </summary>
        void UCAcountSet_ResetPwdEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择记录！");
                return;
            }
            string accCode = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
            string accName=dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString();
            string msg = string.Format("将要重置当前选中帐套{0}（{1})的超级管理员（admin）密码，是否继续？",accName , accCode);
            DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();
                dicFields.Add("password", "123456");
                bool flag = DBHelper.Submit_AddOrEdit("重置密码", GlobalStaticObj_Server.DbPrefix + accCode, "sys_user", "land_name", "admin", dicFields);
                if (flag)
                {
                    msg = string.Format("账套代码:{0} 账套名称:{1} 用户名:admin \r\n密码重置成功,新密码为:123456", accCode, accName);
                    MessageBoxEx.Show(msg, "重置账套密码");
                    return;
                }
                MessageBoxEx.Show("重置成功失败", "系统提示");
            }
        }

        /// <summary> 启用停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCAcountSet_StatusEvent(object sender, EventArgs e)
        {
            if (listStart.Count == 0 && listStop.Count == 0)
            {
                MessageBoxEx.Show("请选择" + base.btnStatus.Caption + "帐套！");
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

            if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中帐套，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("status", enumStatus == DataSources.EnumStatus.Start ? Convert.ToInt16(DataSources.EnumStatus.Stop).ToString() : Convert.ToInt16(DataSources.EnumStatus.Start).ToString());
            bool flag = DBHelper.BatchUpdateDataByIn("批量" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", dicField, "setbook_code", arrField);
            if (flag)
            {
                BindData(where);
                if (dgvAccList.Rows.Count > 0)
                {
                    dgvAccList.CurrentCell = dgvAccList.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 备份
        /// </summary>
        void UCAcountSet_BackupEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择记录！");
                return;
            }
            string accCode = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
            string msg = string.Format("将要备份当前选中帐套{0}（{1})，是否继续？", dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString(), accCode);
            DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string bak_filename = accCode + GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyMMddHHmmss") + ".bak";
                string errMsg = CommonUtility.BackupDb(accCode, bak_filename);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    MessageBoxEx.ShowWarning(errMsg);
                    return;
                }
                MessageBoxEx.ShowInformation("备份成功");
            }
        }

        /// <summary> 还原
        /// </summary>
        void UCAcountSet_RestoreEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择记录！");
                return;
            }
            string accCode = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
            string msg = string.Format("将要还原当前选中帐套{0}（{1})，还原前请备份，是否继续？", dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString(), accCode);
            DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                frmBackupRecord frm = new frmBackupRecord();
                frm.IsSelected = true;
                frm.Acc_Code = accCode;
                frm.Acc_Name = dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string errMsg = CommonUtility.RestoreDb(accCode, frm.FileName);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        MessageBoxEx.ShowWarning(errMsg);
                        return;
                    }
                    MessageBoxEx.ShowInformation("还原成功");
                }
            }
        }

        /// <summary> 操作记录
        /// </summary>
        void UCAcountSet_OpRecordEvent(object sender, EventArgs e)
        {
        }

        /// <summary> 双击新增/复制/编辑帐套内容
        /// </summary>
        private void dgvDicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //string dic_id = dgvAccList.Rows[e.RowIndex].Cells["dic_id"].Value.ToString();
                //string dic_code = dgvAccList.Rows[e.RowIndex].Cells["dic_code"].Value.ToString();
                //string dic_name = dgvAccList.Rows[e.RowIndex].Cells["dic_name"].Value.ToString();
                //UCDictionaryAddOrEdit uc = new UCDictionaryAddOrEdit();
                //uc.ParentId = dic_id;
                //uc.ParentCode = dic_code;
                //uc.ParentName = dic_name;
                //base.addUserControl(uc, "字典码表-新增/复制/编辑", "UCDictionaryManagerOperator" + dic_code, this.Tag.ToString(), this.Name);

            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvAccList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("status"))
            {
                DataSources.EnumStatus enumStatus = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumStatus, true);
            }
            if (fieldNmae.Equals("create_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }

        }

        /// <summary> 控制单元格编辑
        /// </summary>
        private void dgvAccList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)//复选框列可编辑
            {
                e.Cancel = true;
            }
        }

        /// <summary> 单击事件
        /// </summary>
        private void dgvAccList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAccList.CurrentRow != null)
            {
                object obj = dgvAccList.CurrentRow.Cells["is_main_set_book"].Value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                {
                    return;
                }

                DataSources.EnumYesNo enumDataSources = (DataSources.EnumYesNo)Convert.ToInt32(obj.ToString());
                bool flag = enumDataSources == DataSources.EnumYesNo.NO;
                Func<bool, bool> fc = delegate(bool status)
                {
                    base.btnStatus.Enabled = status;
                    base.btnDelete.Enabled = status;
                    return true;
                };
                this.BeginInvoke(fc, new object[] { flag });
            }
        }

        /// <summary> 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAccList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataSources.EnumStatus enumState = (DataSources.EnumStatus)Convert.ToInt32(dgvAccList.Rows[e.RowIndex].Cells["status"].Value.ToString());
                string code = dgvAccList.Rows[e.RowIndex].Cells["setbook_code"].Value.ToString();
                bool flag = Convert.ToBoolean(dgvAccList.Rows[e.RowIndex].Cells[0].EditedFormattedValue);
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
            try
            {
                DataTable dt = DBHelper.GetTable("分页查询帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "*", where, "", "order by setbook_code");
                dgvAccList.DataSource = dt;
                SetBtnStatus(true);
                dgvAccList_CellClick(null, null);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog(ex.Message, "帐套设置-帐套信息查询失败");
                MessageBoxEx.ShowWarning("查询失败");
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
