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
        public UCAcountSet()
        {
            InitializeComponent();
        }

        private void UCAcountSet_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvAccList);
            base.AddEvent += new ClickHandler(UCAcountSet_AddEvent);
            base.EditEvent += new ClickHandler(UCAcountSet_EditEvent);
            base.DeleteEvent += new ClickHandler(UCAcountSet_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCAcountSet_StatusEvent);
            base.ResetPwdEvent += new ClickHandler(UCAcountSet_ResetPwdEvent);
            base.BackupEvent += new ClickHandler(UCAcountSet_BackupEvent);
            base.RestoreEvent += new ClickHandler(UCAcountSet_RestoreEvent);
            base.OpRecordEvent += new ClickHandler(UCAcountSet_OpRecordEvent);
            dgvAccList.CellDoubleClick += new DataGridViewCellEventHandler(dgvAccList_CellDoubleClick);
            base.btnStatus.Enabled = false;
            BindCmb();
            btnSearch_Click(null, null);
        }


        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                where = string.Format("setbook_code!='000' and enable_flag='{0}' ", ((int)DataSources.EnumEnableFlag.USING));
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
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
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
            if (dgvAccList.Rows.Count >= 6)
            {
                MessageBoxEx.Show("对不起，系统只支持创建五个帐套", "系统提示");
                return;
            }
            try
            {
                frmAccountEdit frm = new frmAccountEdit();
                frm.OpType = 0;
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BindData(where);
                    dgvAccList.CurrentCell = dgvAccList.Rows[dgvAccList.RowCount - 1].Cells[0];
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
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
            try
            {
                string code = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                if (code == GlobalStaticObj_Server.CommAccCode)
                {
                    MessageBoxEx.Show("通用库不能编辑！");
                    return;
                }
                int currRowIndex = dgvAccList.CurrentRow.Index;
                frmAccountEdit frm = new frmAccountEdit();
                frm.OpType = 1;
                frm.id = dgvAccList.CurrentRow.Cells["id"].Value.ToString();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BindData(where);
                    dgvAccList.CurrentCell = dgvAccList.Rows[currRowIndex].Cells[0];
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 删除
        /// </summary>
        void UCAcountSet_DeleteEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择删除记录！");
                return;
            }
            if (MessageBoxEx.Show("将要删除选中帐套，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            try
            {
                string code = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("enable_flag", "0");
                bool flag = DBHelper.Submit_AddOrEdit("删除选中帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "setbook_code", code, dicField);
                if (flag)
                {
                    #region 分离被删除的数据库
                    string sqlStr = string.Format("if db_id('{0}') is not null exec sp_detach_db {0}", GlobalStaticObj_Server.DbPrefix + code);
                    DBHelper.ExtNonQuery("分离数据库", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlStr, CommandType.Text, null);
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
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 查看
        /// </summary>
        void dgvAccList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                return;
            }
            try
            {
                string code = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                if (code == GlobalStaticObj_Server.CommAccCode)
                {
                    MessageBoxEx.Show("当前账套是服务端使用，无相关参数信息，不能预览！");
                    return;
                }
                int currRowIndex = dgvAccList.CurrentRow.Index;
                frmAccountEdit frm = new frmAccountEdit();
                frm.OpType = 2;
                frm.id = dgvAccList.CurrentRow.Cells["id"].Value.ToString();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
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
            try
            {
                string accCode = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                string accName = dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString();
                string msg = string.Format("将要重置当前选中帐套{0}（{1})的超级管理员（admin）密码，是否继续？", accName, accCode);
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
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 启用停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCAcountSet_StatusEvent(object sender, EventArgs e)
        {
            if (dgvAccList.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择启停用记录！");
                return;
            }
            try
            {
                string code = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                string name = dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString();
                DataSources.EnumStatus enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvAccList.CurrentRow.Cells["status"].Value.ToString());
                if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中帐套" + name + "(" + code + ")，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    base.btnStatus.Caption = enumStatus == DataSources.EnumStatus.Start ? "停用" : "启用";
                    return; ;
                }
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                enumStatus = DataSources.EnumStatus.Start == enumStatus ? DataSources.EnumStatus.Stop : DataSources.EnumStatus.Start;
                dicField.Add("status", enumStatus.ToString("d"));
                bool flag = DBHelper.Submit_AddOrEdit((DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "setbook_code", code, dicField);
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
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
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
            try
            {
                string accCode = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                string msg = string.Format("将要备份当前选中帐套{0}（{1})，是否继续？", dgvAccList.CurrentRow.Cells["setbook_name"].Value.ToString(), accCode);
                DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string bak_filename = accCode + GlobalStaticObj_Server.Instance.CurrentDateTime.ToString("yyMMddHHmmss") + ".bak";
                    string errMsg = CommonUtility.BackupDb(accCode, bak_filename);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        MessageBoxEx.ShowWarning("系统正在努力尝试，请重新备份！");
                        return;
                    }
                    MessageBoxEx.ShowInformation("备份成功");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
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
            try
            {
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
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 操作记录
        /// </summary>
        void UCAcountSet_OpRecordEvent(object sender, EventArgs e)
        {
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            try
            {
                string fieldNmae = dgvAccList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("status"))
                {
                    DataSources.EnumStatus enumStatus = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumStatus, true);
                }
                if (fieldNmae.Equals("is_main_set_book"))
                {
                    DataSources.EnumYesNo enumStatus = (DataSources.EnumYesNo)Convert.ToInt16(e.Value.ToString());
                    e.Value = DataSources.GetDescription(enumStatus, true);
                }
                if (fieldNmae.Equals("create_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
            }

        }

        /// <summary> 单击事件
        /// </summary>
        private void dgvAccList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAccList.CurrentRow != null)
                {
                    string code = dgvAccList.CurrentRow.Cells["setbook_code"].Value.ToString();
                    if (code == GlobalStaticObj_Server.CommAccCode)
                    {

                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnStatus.Enabled = false;
                        base.btnResetPwd.Enabled = false;
                        return;
                    }
                    base.btnResetPwd.Enabled = true;
                    base.btnEdit.Enabled = true;
                    bool flag = false;
                    object obj = dgvAccList.CurrentRow.Cells["is_main_set_book"].Value;
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    {
                        return;
                    }

                    DataSources.EnumYesNo enumYesNo = (DataSources.EnumYesNo)Convert.ToInt32(obj.ToString());
                    flag = enumYesNo == DataSources.EnumYesNo.NO;

                    Func<bool, bool> fc = delegate(bool status)
                    {
                        base.btnStatus.Enabled = status;
                        base.btnDelete.Enabled = status;
                        return true;
                    };
                    this.BeginInvoke(fc, new object[] { flag });
                    object objStatus = dgvAccList.CurrentRow.Cells["status"].Value;
                    base.btnStatus.Caption = DataSources.EnumStatus.Start.ToString("d") == objStatus.ToString() ? "停用" : "启用";
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
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
                dgvAccList_CellClick(null, null);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion
    }
}
