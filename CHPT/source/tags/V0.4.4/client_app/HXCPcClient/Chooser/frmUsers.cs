using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.Chooser
{
    public partial class frmUsers : FormEx
    {
        public frmUsers()
        {
            InitializeComponent();

            DataSources.BindComBoxDataEnum(cbo_data_source, typeof(DataSources.EnumDataSources), true);
        }
        #region 外部属性
        public String AppendWhere = String.Empty;   //add by kord
        /// <summary> 人员id
        /// </summary>
        public string User_ID { get; set; }
        /// <summary> 人员编码
        /// </summary>
        public string User_Code { get; set; }
        /// <summary> 姓名
        /// </summary>
        public string User_Name { get; set; }
        public string CrmId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public String OrgId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public String OrgName { get; set; }
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
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(txtuser_code.Caption.Trim()))
            {
                where += string.Format(" and  user_code like '%{0}%'", txtuser_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtuser_name.Caption.Trim()))
            {
                where += string.Format(" and  user_name like '%{0}%'", txtuser_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txt_com_code.Caption.Trim()))
            {
                where += string.Format(" and com_code like '%{0}%'", txt_com_code.Caption.Trim());
            }
            if (cbo_data_source.SelectedValue != null && !String.IsNullOrEmpty(cbo_data_source.SelectedValue.ToString()))
            {
                where += String.Format(" and data_sources like '%{0}%'", cbo_data_source.SelectedValue);
            }
            BindPageData(where);
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
                this.User_ID = dgvUsers.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
                this.User_Code = dgvUsers.Rows[e.RowIndex].Cells["user_code"].Value.ToString();
                this.User_Name = dgvUsers.Rows[e.RowIndex].Cells["user_name"].Value.ToString();
                this.OrgId = dgvUsers.Rows[e.RowIndex].Cells["drtxt_org_id"].Value.ToString();
                this.OrgName = dgvUsers.Rows[e.RowIndex].Cells["drtxt_org_name"].Value.ToString();
                this.CrmId = dgvUsers.Rows[e.RowIndex].Cells[drtxt_crm_id.Name].Value.ToString();

                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            DataTable dt = DBHelper.GetTable("选择器人员列表", "v_user", "*", where + AppendWhere, "", "order by user_code");
            dgvUsers.DataSource = dt;
        }
        #endregion
    }
}
