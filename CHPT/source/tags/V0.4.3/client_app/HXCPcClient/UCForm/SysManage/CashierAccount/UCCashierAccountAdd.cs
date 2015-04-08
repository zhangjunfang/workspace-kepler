using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.CashierAccount
{
    public partial class UCCashierAccountAdd : UCBase
    {
        string id = string.Empty;
        UCCashierAccManage uc;
        public UCCashierAccountAdd(WindowStatus status, string id, UCCashierAccManage uc)
        {
            InitializeComponent();
            this.windowStatus = status;
            this.id = id;
            this.uc = uc;
            this.SaveEvent += new ClickHandler(UCCashierAccountAdd_SaveEvent);
        }

        void UCCashierAccountAdd_SaveEvent(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            string accountName = txtAccountName.Caption.Trim();
            if (accountName.Length == 0)
            {
                MessageBoxEx.Show("请输入账户名称！");
                txtBankAccount.Focus();
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string accountType = CommonCtrl.IsNullToString(cboAccountType.SelectedValue);//账户类型
            string bankID = CommonCtrl.IsNullToString(cboBank.SelectedValue);//银行名称
            dic.Add("account_name", accountName);
            dic.Add("account_type", accountType.Length == 0 ? null : accountType);
            dic.Add("bank_id", bankID.Length == 0 ? null : bankID);
            dic.Add("status", rboEnable.Checked ? "1" : "0");
            //判断窗体状态
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                dic.Add("cashier_account", Guid.NewGuid().ToString());
                dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                dic.Add("create_by", GlobalStaticObj.UserID);
                dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                if (DBHelper.Submit_AddOrEdit("出纳账户操作", "tb_cashier_account", "", "", dic))
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
                if (DBHelper.Submit_AddOrEdit("出纳账户操作", "tb_cashier_account", "cashier_account", id, dic))
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
        private void UCCashierAccountAdd_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus(windowStatus);

            this.BindBank();
            this.BindAccountType();
            //如果是编辑或者复制,则先绑定数据
            if ((windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy) && this.id.Length > 0)
            {
                this.BindData();
            }            
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            string strWhere = string.Format("cashier_account='{0}'", id);
            DataTable dt = DBHelper.GetTable("查询出纳账户", "v_cashier_account", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            txtAccountName.Caption = CommonCtrl.IsNullToString(dr["account_name"]);
            string accountType = CommonCtrl.IsNullToString(dr["account_type"]);
            if (accountType.Length == 0)
            {
                cboAccountType.SelectedItem = null;
            }
            else
            {
                cboAccountType.SelectedValue = Convert.ToInt32(accountType);
            }
            txtBankAccount.Caption = CommonCtrl.IsNullToString(dr["bank_account"]);
            cboBank.SelectedValue = dr["bank_id"];
            if (CommonCtrl.IsNullToString(dr["status"]) == "1")
            {
                rboEnable.Checked = true;
            }
            else
            {
                rboDisable.Checked = true;
            }
        }
        /// <summary>
        /// 绑定账户类型
        /// </summary>
        void BindAccountType()
        {
            List<ListItem> list = DataSources.EnumToList(typeof(DataSources.EnumAccountType), true);
            list.RemoveAt(0);
            cboAccountType.ValueMember = "Value";
            cboAccountType.DisplayMember = "Text";
            cboAccountType.DataSource = list;
        }
        /// <summary>
        /// 绑定银行账户
        /// </summary>
        void BindBank()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);
            sbWhere.AppendFormat(" and status='{0}'", (int)DataSources.EnumStatus.Start);
            DataTable dt = DBHelper.GetTable("", "tb_bank_account", "bank_account_id,bank_name,bank_account", sbWhere.ToString(), "", "");
            cboBank.ValueMember = "bank_account_id";
            cboBank.DisplayMember = "bank_name";
            cboBank.DataSource = dt;
        }

        private void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBank.SelectedItem == null)
            {
                return;
            }
            DataRowView drv = (DataRowView)cboBank.SelectedItem;
            txtBankAccount.Caption = CommonCtrl.IsNullToString(drv["bank_account"]);
        }
    }
}
