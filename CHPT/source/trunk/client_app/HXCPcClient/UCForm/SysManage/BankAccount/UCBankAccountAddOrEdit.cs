using System;
using System.Collections.Generic;
using System.Data;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.SysManage.BankAccount
{
    public partial class UCBankAccountAddOrEdit : UCBase
    {

        #region 变量属性

        string id = string.Empty;

        string parentName = string.Empty;

        UCBankAccountManage uc;

        DataRow dr;
        #endregion

        #region 构造函数初始化

        public UCBankAccountAddOrEdit(WindowStatus status, string id, UCBankAccountManage uc)
        {
            InitializeComponent();
            this.id = id;
            this.uc = uc;
            base.windowStatus = status;
            this.SaveEvent += new ClickHandler(UCBankAccountAddOrEdit_SaveEvent);
            this.CancelEvent += new ClickHandler(UCBankAccountAddOrEdit_CancelEvent);
            this.DeleteEvent += new ClickHandler(UCBankAccountAddOrEdit_DeleteEvent);
            this.StatusEvent += new ClickHandler(UCBankAccountAddOrEdit_StatusEvent);
        }


        private void UCBankAccountAddOrEdit_Load(object sender, EventArgs e)
        {
            //如果是编辑或者复制,则先绑定数据
            if ((windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy) && id.Length > 0)
            {
                BindData();

            }
            SetBtnStatus();
        }

        /// <summary>
        /// 设置页面按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (windowStatus == WindowStatus.Edit)
            {
                SetSysManageEditBtn();
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetSysManageAddBtn();
            }
        }


        #endregion

        #region 按钮事件

        void UCBankAccountAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }

        void UCBankAccountAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            Save();
        }

        private void txtBankAccount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            OnlyNum(e);
        }
        void UCBankAccountAddOrEdit_StatusEvent(object sender, EventArgs e)
        {

            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (int.Parse(dr["status"].ToString()) == (int)DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }

            if (StatusSql())
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                uc.BindData(id);
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (int.Parse(dr["status"].ToString()) == (int)DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        /// <summary>
        /// 执行启用停用
        /// </summary>
        /// <returns>是否成功</returns>
        private bool StatusSql()
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = string.Format("update tb_bank_account set status=@status where bank_account_id = '{0}'", id);
            string ids = string.Empty;
            if (int.Parse(dr["status"].ToString()) == (int)DataSources.EnumStatus.Start)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else if (int.Parse(dr["status"].ToString()) == (int)DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "银行账户", listSql);
        }

        void UCBankAccountAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }

        void DeleteData()
        {
            List<string> listField = new List<string>();
            listField.Add(id);
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

            if (DBHelper.BatchUpdateDataByIn("删除银行账户", "tb_bank_account", dicFileds, "bank_account_id", listField.ToArray()))
            {
                MessageBoxEx.Show("删除成功！");
                uc.BindData(id);
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }
        #endregion

        #region 校验保存

        void Save()
        {
            string bankName = txtBankName.Caption.Trim();
            if (bankName.Length == 0)
            {
                MessageBoxEx.Show("请输入银行名称！");
                txtBankName.Focus();
                return;
            }

            string bankAccount = txtBankAccount.Caption.Trim();
            if (bankAccount.Length == 0)
            {
                MessageBoxEx.Show("请输入银行账户！");
                txtBankAccount.Focus();
                return;
            }
            if (!Utility.Common.ValidateUtil.IsBankCard(bankAccount))
            {
                MessageBoxEx.Show("银行账户格式不正确！16-19位数字");
                txtBankAccount.Focus();
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
                dic.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.UtcNow).ToString());
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
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.UtcNow).ToString());
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

        #endregion

        #region 数据绑定

        void BindData()
        {
            string strWhere = string.Format("bank_account_id='{0}'", id);
            DataTable dt = DBHelper.GetTable("查询银行账户", "tb_bank_account", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            dr = dt.Rows[0];
            txtBankAccount.Caption = CommonCtrl.IsNullToString(dr["bank_account"]);
            txtBankName.Caption = CommonCtrl.IsNullToString(dr["bank_name"]);
            if (CommonCtrl.IsNullToString(dr["status"]) == "1")
            {
                rboEnable.Checked = true;
                btnStatus.Caption = "停用";
            }
            else
            {
                rboDisable.Checked = true;
                btnStatus.Caption = "启用";
            }
        }

        #endregion


    }
}
