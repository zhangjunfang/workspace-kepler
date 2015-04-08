using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.Settlement
{
    public partial class UCSettlementAddOrEdit : UCBase
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --成员变量
        string id = string.Empty;
        string parentName = string.Empty;
        #endregion

        #region --构造函数
        public UCSettlementAddOrEdit(WindowStatus status,string id,UCSettlementManage uc)
        {
            InitializeComponent();
            this.windowStatus = status;
            this.id = id;           
            this.SaveEvent += new ClickHandler(UCSettlementAddOrEdit_SaveEvent);
        }
        #endregion

        void UCSettlementAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            Save();
        }
        void Save()
        {           
            if (this.cmbJSFS.SelectedValue==null
                ||this.cmbJSFS.SelectedValue.ToString().Length == 0)
            {
                MessageBoxEx.Show("请输入结算方式！");               
                return;
            }
            string defaultAccount = CommonCtrl.IsNullToString(cboDefaultAccount.SelectedValue);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("default_account", defaultAccount.Length == 0 ? null : defaultAccount);
            dic.Add("balance_way_name", this.cmbJSFS.Text);
            dic.Add("status", rboEnable.Checked ? "1" : "0");
            //判断窗体状态
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                dic.Add("balance_way_id", Guid.NewGuid().ToString());
                dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
                dic.Add("create_by", GlobalStaticObj.UserID);
                dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                if (DBHelper.Submit_AddOrEdit("结算方式操作", "tb_balance_way", "", "", dic))
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                    MessageBoxEx.Show("保存成功!");                   
                    deleteMenuByTag(this.Tag.ToString(), this.parentName);
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
                if (DBHelper.Submit_AddOrEdit("结算方式操作", "tb_balance_way", "balance_way_id", id, dic))
                {
                    MessageBoxEx.Show("修改成功!");
                    //uc.BindData(id);
                    deleteMenuByTag(this.Tag.ToString(), this.parentName);
                }
                else
                {
                    MessageBoxEx.Show("修改失败!");
                }
            }
        }

        void BindData()
        {
            string strWhere = string.Format("balance_way_id='{0}'", id);
            DataTable dt = DBHelper.GetTable("查询结算方式", "tb_balance_way", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            this.cmbJSFS.SelectedValue = CommonCtrl.IsNullToString(dr["balance_way_id"]);
            cboDefaultAccount.SelectedValue = CommonCtrl.IsNullToString(dr["default_account"]);
            if (CommonCtrl.IsNullToString(dr["status"]) == "1")
            {
                rboEnable.Checked = true;
            }
            else
            {
                rboDisable.Checked = true;
            }
        }

        #region --窗体初始化
        private void UCSettlementAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus(windowStatus);

            CommonCtrl.CmbBindDict(this.cmbJSFS, "sys_closing_way");  
          
            BindDefaultAccount();
            //如果是编辑或者复制,则先绑定数据
            if ((windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy) && id.Length > 0)
            {
                BindData();
            }
            
        }
        #endregion

        void BindDefaultAccount()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);
            sbWhere.AppendFormat(" and status='{0}'", (int)DataSources.EnumStatus.Start);
            DataTable dt = DBHelper.GetTable("", "tb_bank_account", "bank_account_id,bank_name", sbWhere.ToString(), "", "");
            cboDefaultAccount.ValueMember = "bank_account_id";
            cboDefaultAccount.DisplayMember = "bank_name";
            cboDefaultAccount.DataSource = dt;
        }
    }
}
