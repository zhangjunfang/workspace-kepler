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
    /// 维修管理-维修调度停工原因
    /// Author：JC
    /// AddTime：2014.11.11
    /// </summary>
    public partial class UCStopReason : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 停工原因
        /// </summary>
        public string Content = string.Empty;//停工原因
        /// <summary>
        /// 单据质检状态
        /// </summary>
        public DataSources.EnumDispatchStatus DStatus;//单据质检状态
        /// <summary>
        /// 项目质检状态
        /// </summary>
        public DataSources.EnumProjectDisStatus PStatus;//项目质检状态
        #endregion
        public UCStopReason()
        {
            InitializeComponent();
        }

        #region 确认按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rtbContent.Text.Length == 0)
            {
                MessageBoxEx.Show("请填写停工原因！");
                rtbContent.Focus();
                return;
            }
            Content = !rtbContent.Text.Trim().Contains("停工原因：") ? rtbContent.Text.Trim() : rtbContent.Text.Trim().Replace("停工原因：", "");
            DStatus = DataSources.EnumDispatchStatus.StopWork;
            PStatus = DataSources.EnumProjectDisStatus.Pause;
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
    }
}
