using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using HXC_FuncUtility;
using BLL;

namespace HXCServerWinForm.UCForm
{
    public partial class frmPersionSet : FormEx
    {
        public frmPersionSet()
        {
            InitializeComponent();
        }

        private void frmPersionSet_Load(object sender, EventArgs e)
        {
            txtloginid.Caption = GlobalStaticObj_Server.Instance.LoginName;
            txtusername.Caption = GlobalStaticObj_Server.Instance.UserName;
        }

        #region 事件
        /// <summary> 提交
        /// </summary>
        private void btnSummit_Click(object sender, EventArgs e)
        {
            errProvider.Clear();
            try
            {
                if (string.IsNullOrEmpty(txtusername.Caption))
                {
                    Validator.SetError(errProvider, txtusername, "请录入姓名");
                    return;
                }
                if (string.IsNullOrEmpty(txtoldpwd.Caption))
                {
                    Validator.SetError(errProvider, txtoldpwd, "请录入旧密码");
                    return;
                }
                //Utility.Security.Secret.MD5(
                if (txtoldpwd.Caption.Trim() != GlobalStaticObj_Server.Instance.PassWord)
                {
                    Validator.SetError(errProvider, txtoldpwd, "旧密码验证错误，请录入正确的旧密码");
                    return;
                }
                if (string.IsNullOrEmpty(txtnewpwd.Caption))
                {
                    Validator.SetError(errProvider, txtnewpwd, "请录入新密码");
                    return;
                }
                if (string.IsNullOrEmpty(txtnewpwd_again.Caption))
                {
                    Validator.SetError(errProvider, txtnewpwd_again, "请再次录入新密码");
                    return;
                }
                if (txtnewpwd.Caption.Trim() != txtnewpwd_again.Caption.Trim())
                {
                    Validator.SetError(errProvider, txtnewpwd_again, "两次录入新密码不一致");
                    return;
                }
                //string md5Pwd = Utility.Security.Secret.MD5(txtnewpwd.Caption.Trim());
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                dicFileds.Add("land_name", txtloginid.Caption.Trim());
                dicFileds.Add("password", txtnewpwd.Caption.Trim());
                bool flag = DBHelper.Submit_AddOrEdit("修改用户名和密码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_user", "user_id", GlobalStaticObj_Server.Instance.UserID, dicFileds);
                if (flag)
                {
                    GlobalStaticObj_Server.Instance.PassWord = txtnewpwd.Caption.Trim();
                    MessageBoxEx.ShowInformation("修改成功");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBoxEx.ShowWarning("修改失败");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("个人设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("个人设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion
    }
}
