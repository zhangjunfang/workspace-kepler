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
using Utility.Common;
using SYSModel;
using HXCPcClient.UCForm;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 工时档案选择器
    /// Author：JC
    /// AddTime：2014.10.11
    /// </summary>
    public partial class frmWorkHours : FormChooser
    {
        #region 外部属性设置
        /// <summary>
        /// id
        /// </summary>
        public string strWhours_id = string.Empty;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string strProjectNum = string.Empty;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string strProjectName = string.Empty;
        /// <summary>
        /// 维修项目类别
        /// </summary>
        public string strRepairType = string.Empty;
        /// <summary>
        /// 工时数量
        /// </summary>
        public string strWhoursNum = string.Empty;
        /// <summary>
        /// 工时调整
        /// </summary>
        public string strWhoursChange = string.Empty;
        /// <summary>
        /// 工时单价
        /// </summary>
        public string strQuotaPrice = string.Empty;
        /// <summary>
        /// 工时类型
        /// </summary>
        public string strWhoursType = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark = string.Empty;
        #endregion
        public frmWorkHours()
        {
            InitializeComponent();
            this.AcceptButton = btnSearch;
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
        }

        #region 分页事件
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvWorkList();
        }
        #endregion

        #region 组合查询条件
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " status='1' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ";
            if (!string.IsNullOrEmpty(txtProNo.Caption.Trim()))
            {
                Str_Where += string.Format(" and  project_num like '%{0}%'", txtProNo.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtProName.Caption.Trim()))
            {
                Str_Where += string.Format(" and  project_name like '%{0}%'", txtProName.Caption.Trim());
            }
            return Str_Where;
        }
        #endregion

        #region 加载工时档案列表信息
        /// <summary>
        /// 加载工时档案列表信息
        /// </summary>
        public void BindgvWorkList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvWork_dt = DBHelper.GetTableByPage("查询工时列表信息", "v_workhours_users", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvWorkList.DataSource = gvWork_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        #endregion

        #region 查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvWorkList();
        }
        #endregion

        #region 清空事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProName.Caption = string.Empty;
            txtProNo.Caption = string.Empty;
        }
        #endregion

        #region  重写datagridview中的时间等
        private void gvWorkList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = gvWorkList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("repair_type"))
            {
                string strTypeId = e.Value.ToString();
                e.Value = LocalCache.GetDictNameById(strTypeId);
            }
            if (fieldNmae.Equals("whours_type"))
            {
                string strWType = e.Value.ToString() == "1" ? "工时" : "定额";
                e.Value = strWType;
            }
        }
        #endregion

        #region 关闭事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 确认事件
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            strWhours_id = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_id"].Value);
            strRepairType =CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["repair_type"].Value);
            strProjectNum =CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["project_num"].Value);
            strProjectName =CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["project_name"].Value);
            strRemark = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["remark"].Value);
            strQuotaPrice =CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["quota_price"].Value);
            strWhoursNum = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_num_a"].Value);
            strWhoursType = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_type"].Value);
            if (strWhoursType == "1")
            {
                DataTable dt = DBHelper.GetTable("查询维修业务参数-工时标准", GlobalStaticObj.CommAccCode, "sys_repair_param", "time_standards", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
                strQuotaPrice = CommonCtrl.IsNullToString(dt.Rows[0]["time_standards"]);
            }
            strWhoursChange = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells[drtxt_whours_change.Name].Value);
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region 单元格双击事件
        private void gvWorkList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                strWhours_id = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["whours_id"].Value);
                strRepairType =CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["repair_type"].Value);
                strProjectNum = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["project_num"].Value);
                strProjectName =CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["project_name"].Value);
                strRemark = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["remark"].Value);
                strQuotaPrice =CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["quota_price"].Value);
                strWhoursNum = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["whours_num_a"].Value);
                strWhoursType = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells["whours_type"].Value);
                if (strWhoursType == "1")
                {
                    DataTable dt = DBHelper.GetTable("查询维修业务参数-工时标准", GlobalStaticObj.CommAccCode, "sys_repair_param", "time_standards", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
                    strQuotaPrice = CommonCtrl.IsNullToString(dt.Rows[0]["time_standards"]);
                }
                strWhoursChange = CommonCtrl.IsNullToString(gvWorkList.Rows[e.RowIndex].Cells[drtxt_whours_change.Name].Value);
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region 判断是否选择了一条数据
        /// <summary>
        /// 判断是否选择了一条数据
        /// </summary>
        /// <returns></returns>
        private bool IsCheck()
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvWorkList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["whours_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择工时档案记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能选择1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 选择行
        private void gvWorkList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in gvWorkList.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            gvWorkList.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion

        #region 回车事件
        private void gvWorkList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                strWhours_id = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_id"].Value);
                strRepairType = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["repair_type"].Value);
                strProjectNum = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["project_num"].Value);
                strProjectName = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["project_name"].Value);
                strRemark = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["remark"].Value);
                strQuotaPrice = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["quota_price"].Value);
                strWhoursNum = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_num_a"].Value);
                strWhoursType = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells["whours_type"].Value);
                strWhoursChange = CommonCtrl.IsNullToString(gvWorkList.CurrentRow.Cells[drtxt_whours_change.Name].Value);
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion
    }
}
