using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.RepairDispatch
{
    /// <summary>
    /// 维修管理-维修调度质检
    /// Author：JC
    /// AddTime：2014.11.11
    /// </summary>
    public partial class UCInspection : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 质检意见
        /// </summary>
        public string Content = string.Empty;//质检意见
        /// <summary>
        /// 单据质检状态
        /// </summary>
        public DataSources.EnumDispatchStatus DStatus;//单据质检状态
        /// <summary>
        /// 项目质检状态
        /// </summary>
        public DataSources.EnumProjectDisStatus PStatus;//项目质检状态
        #endregion

        public UCInspection()
        {
            InitializeComponent();
        }
        #region 确认按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            Content = !rtbContent.Text.Trim().Contains("质检意见：") ? rtbContent.Text.Trim() : rtbContent.Text.Trim().Replace("质检意见：", "");
            rtbContent.Text = Content;
            if (rtbContent.Text.Trim().Length > 200)
            {
                MessageBoxEx.Show("质检意见不能超过200个字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdbReturn.Checked && rtbContent.Text.Length == 0)
            {
                MessageBoxEx.Show("请填写质检意见！");
                rtbContent.Focus();
                return;
            }          
            if (rdbOK.Checked)
            {
                DStatus = DataSources.EnumDispatchStatus.HasPassed;
                PStatus = DataSources.EnumProjectDisStatus.InspectionCompleted;
            }
            else
            {
                DStatus = DataSources.EnumDispatchStatus.NotPassed;
                PStatus = DataSources.EnumProjectDisStatus.NotInspectionCompleted;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        private void rtbContent_TextChanged(object sender, EventArgs e)
        {
            if (rtbContent.Text.Trim().Length > 200)
            {
                MessageBoxEx.Show("质检意见不能超过200个字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
