using System;
using System.Collections.Generic;
using System.Data;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.BankAccount
{
    public partial class UCBankAccountAddOrEdit : UCBase
    {
        string id = string.Empty;
        UCBankAccountManage uc;
        public UCBankAccountAddOrEdit(WindowStatus status,string id,UCBankAccountManage uc)
        {
            InitializeComponent();
            this.id = id;
            this.uc = uc;
            base.windowStatus = status;
            this.SaveEvent += new ClickHandler(UCBankAccountAddOrEdit_SaveEvent);
        }

        void UCBankAccountAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            string bankAccount = txtBankAccount.Caption.Trim();
            if (bankAccount.Length == 0)
            {
                MessageBoxEx.Show("请输入银行账户！");
                txtBankAccount.Focus();
                return;
            }
            string bankName = txtBankName.Caption.Trim();
            if (bankName.Length == 0)
            {
                MessageBoxEx.Show("请输入银行名称！");
                txtBankName.Focus();
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("bank_name", bankName);
            dic.Add("bank_account", bankAccount);
            dic.Add("status", rboEnable.Checked ? "1" : "0");
            //判断窗体状态
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                dic.Add("bank_account_id", Guid.NewGuid().ToString());
                dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                dic.Add("create_by", GlobalStaticObj.UserID);
                dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                if (DBHelper.Submit_AddOrEdit("银行账户操作", "tb_bank_account", "", "", dic))
                {
                    MessageBoxEx.Show("保存成功!");
                    uc.BindData(id);
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!");
                }
            }
            else if (windowStatus == WindowStatus.Edit)
            {
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_time", DateTime.UtcNow.Ticks.ToString());
                if (DBHelper.Submit_AddOrEdit("银行账户操作", "tb_bank_account", "bank_account_id", id, dic))
                {
                    MessageBoxEx.Show("修改成功!");
                    uc.BindData(id);
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("修改失败!");
                }
            }
        }

        private void UCBankAccountAddOrEdit_Load(object sender, EventArgs e)
        {
            //如果是编辑或者复制,则先绑定数据
            if ((windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy) && id.Length > 0)
            {
                BindData();
            }
            SetBtnStatus(windowStatus);
        }
        void BindData()
        {
            string strWhere = string.Format("bank_account_id='{0}'", id);
            DataTable dt = DBHelper.GetTable("查询银行账户", "tb_bank_account", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            txtBankAccount.Caption = CommonCtrl.IsNullToString(dr["bank_account"]);
            txtBankName.Caption = CommonCtrl.IsNullToString(dr["bank_name"]);
            if (CommonCtrl.IsNullToString(dr["status"]) == "1")
            {
                rboEnable.Checked = true;
            }
            else
            {
                rboDisable.Checked = true;
            }
        }
    }
}
