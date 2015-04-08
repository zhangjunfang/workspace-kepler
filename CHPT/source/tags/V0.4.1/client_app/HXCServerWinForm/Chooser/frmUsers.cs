using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCServerWinForm.CommonClass;
using Utility.Common;
using BLL;
using HXC_FuncUtility;

namespace HXCServerWinForm.Chooser
{
    public partial class frmUsers : FormEx
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        #region 外部属性
        /// <summary> 人员id
        /// </summary>
        public string User_ID { get; set; }
        /// <summary> 人员编码
        /// </summary>
        public string User_Code { get; set; }
        /// <summary> 姓名
        /// </summary>
        public string User_Name { get; set; }
        #endregion

        private void frmUsers_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }
        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string where = " 1=1 ";
                if (!string.IsNullOrEmpty(txtuser_code.Caption.Trim()))
                {
                    where += string.Format(" and  user_code like '%{0}%'", txtuser_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtuser_name.Caption.Trim()))
                {
                    where += string.Format(" and  user_name like '%{0}%'", txtuser_name.Caption.Trim());
                }
                BindPageData(where);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-人员", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        /// <summary> 清除
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtuser_code.Caption = string.Empty;
            txtuser_name.Caption = string.Empty;
            txtuser_code.Focus();
        }
        /// <summary> 双击选择
        /// </summary>
        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    this.User_ID = dgvUsers.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
                    this.User_Code = dgvUsers.Rows[e.RowIndex].Cells["user_code"].Value.ToString();
                    this.User_Name = dgvUsers.Rows[e.RowIndex].Cells["user_name"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-人员", ex);
                    MessageBoxEx.ShowWarning("程序异常");
                }
            }
        }

        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            try
            {
                DataTable dt = DBHelper.GetTable("选择器人员列表", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_user", "*", where, "", "order by user_code");
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-人员", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion
    }
}
