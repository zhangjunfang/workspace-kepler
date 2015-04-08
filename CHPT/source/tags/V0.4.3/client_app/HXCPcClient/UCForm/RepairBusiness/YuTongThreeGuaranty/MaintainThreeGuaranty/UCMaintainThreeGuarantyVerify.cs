using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCApproveStatusInfo         
    /// Author:       Kord
    /// Date:         2014/11/5 19:04:08
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	UCApproveStatusInfo
    ///***************************************************************************//
    public partial class UCMaintainThreeGuarantyVerify : FormEx
    {
        #region Constructor -- 构造函数
        /// <summary>
        /// 服务单审核详情
        /// </summary>
        /// <param name="tgId">服务单id</param>
        public UCMaintainThreeGuarantyVerify(String tgId)
        {
            InitializeComponent();

            _tgId = tgId;

            InitEventHandle();
        }
        #endregion

        #region Field -- 字段
        private readonly String _tgId = String.Empty;
        public UCMaintainThreeGuarantyManager UcForm;
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        private void InitEventHandle()
        {
            btn_ok.Click += delegate
            {
                if (rdo_false.Checked && String.IsNullOrEmpty(txt_information.Text))
                {
                    MessageBoxEx.Show("请填写审核意见!", "操作提示");
                    txt_information.Focus();
                    return;
                }
                SaveData();
            };
            btn_cancel.Click += delegate
            {
                DialogResult = DialogResult.Cancel;
            };
        }
        private void SaveData()
        {
            var status = rdo_ture.Checked
                ? DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHTG
                : DbDic2Enum.SYS_SERVICE_INFO_STATUS_SHWTG;
            var dicFields = new Dictionary<string, string>();
            dicFields.Add("verify_info", txt_information.Text);
            dicFields.Add("info_status", status);

            var result = DBHelper.Submit_AddOrEdit("三包服务单审核详情", "tb_maintain_three_guaranty", "tg_id", _tgId, dicFields);
            if (!result)
            {
                MessageBoxEx.Show("审核服务单出错!", "操作提示");
            }
            else
            {
                //if (rdo_ture.Checked)
                //{
                //    var resultStr = UcForm.Submit2Company(_tgId);
                //    MessageBoxEx.Show(String.IsNullOrEmpty(resultStr) ? "三包服务单上报厂家成功!" : resultStr, "操作提示");
                //}
                DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}


