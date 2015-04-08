using System;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Collections.Generic;
using Utility.Common;
using HXCPcClient.CommonClass;
using System.Windows.Forms;

namespace HXCPcClient.FormLevelTwo
{
    public partial class FormPassword : FormEx
    {
        public FormPassword()
        {
            InitializeComponent();
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {
            this.txtpassword.Caption = GlobalStaticObj.PassWord;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            
            if (GlobalStaticObj.PassWord != txtpassword.Caption.Trim())
            {
                Utility.Common.Validator.SetError(errorProvider1, txtpassword, "旧密码输入不正确!");
                return;
            }
            if (!Utility.Common.ValidateUtil.IsPassword(txtpassword_new.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtpassword_new, "密码为6-20字母数字混合组成!");
                return;
            }
            if (txtpassword_new.Caption.Trim() != txtpassword_new_d.Caption.Trim())
            {
                Utility.Common.Validator.SetError(errorProvider1, txtpassword_new_d, "两次密码输入不一致!");
                return;
            }

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("password", this.txtpassword_new.Caption.Trim());
            dic.Add("update_by", GlobalStaticObj.UserID);
            dic.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());

            if (DBHelper.Submit_AddOrEdit("修改用户密码", "sys_user", "user_id", GlobalStaticObj.UserID, dic))
            {
                GlobalStaticObj.PassWord = txtpassword_new.Text.Trim();
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
