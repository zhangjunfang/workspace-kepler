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
            string Str_Where = " enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ";           
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

        #region 单元格单击事件
        private void gvWorkList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                strWhours_id = gvWorkList.Rows[e.RowIndex].Cells["whours_id"].Value != null ? gvWorkList.Rows[e.RowIndex].Cells["whours_id"].Value.ToString() : "";
                strRepairType= gvWorkList.Rows[e.RowIndex].Cells["repair_type"].Value!=null?gvWorkList.Rows[e.RowIndex].Cells["repair_type"].Value.ToString():"";
                strProjectNum = gvWorkList.Rows[e.RowIndex].Cells["project_num"].Value!=null?gvWorkList.Rows[e.RowIndex].Cells["project_num"].Value.ToString():"0";
                strProjectName = gvWorkList.Rows[e.RowIndex].Cells["project_name"].Value!=null? gvWorkList.Rows[e.RowIndex].Cells["project_name"].Value.ToString():"";
                strRemark = gvWorkList.Rows[e.RowIndex].Cells["remark"].Value!=null?gvWorkList.Rows[e.RowIndex].Cells["remark"].Value.ToString():"";
                strQuotaPrice = gvWorkList.Rows[e.RowIndex].Cells["quota_price"].Value!=null?gvWorkList.Rows[e.RowIndex].Cells["quota_price"].Value.ToString():"0";
                strWhoursNum = gvWorkList.Rows[e.RowIndex].Cells["whours_num_a"].Value!=null?gvWorkList.Rows[e.RowIndex].Cells["whours_num_a"].Value.ToString():"0";
                strWhoursType = gvWorkList.Rows[e.RowIndex].Cells["whours_type"].Value != null ? gvWorkList.Rows[e.RowIndex].Cells["whours_type"].Value.ToString() : "";
                strWhoursChange = gvWorkList.Rows[e.RowIndex].Cells[drtxt_whours_change.Name].Value != null ? gvWorkList.Rows[e.RowIndex].Cells[drtxt_whours_change.Name].Value.ToString() : "";
                this.DialogResult = DialogResult.OK;
            }
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
                string strTypeId=e.Value.ToString();
                e.Value = LocalCache.GetDictNameById(strTypeId);
            }
            if (fieldNmae.Equals("whours_type"))
            {
                string strWType = e.Value.ToString()=="1"?"工时":"定额";
                e.Value = strWType;
            }
        }       
        #endregion
    }
}
