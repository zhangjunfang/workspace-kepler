using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm
{
    /// <summary>
    /// 审核公共窗体
    /// </summary>
    public partial class UCVerify : FormChooser
    {
        public string Content = string.Empty;//审核意见
        public DataSources.EnumAuditStatus auditStatus;//审核状态
        public UCVerify()
        {
            InitializeComponent();
        }

        #region 确认按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            Content = !rtbContent.Text.Trim().Contains("审核意见：") ? rtbContent.Text.Trim() : rtbContent.Text.Trim().Replace("审核意见：", "");
            rtbContent.Text = Content;
            if (rtbContent.Text.Trim().Length > 200)
            {
                MessageBoxEx.Show("审核意见不能超过200个字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdbReturn.Checked && rtbContent.Text.Length == 0)
            {
                MessageBoxEx.Show("请填写审核意见！");
                rtbContent.Focus();
                return;
            }           
            if (rdbOK.Checked)
            {
                auditStatus = DataSources.EnumAuditStatus.AUDIT;
            }
            else
            {
                auditStatus = DataSources.EnumAuditStatus.NOTAUDIT;
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
                MessageBoxEx.Show("审核意见不能超过200个字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
