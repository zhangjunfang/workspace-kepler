using System;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.Data;
using HXCPcClient.CommonClass;
using System.Threading;
using System.Collections.Generic;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    /// <summary>
    /// 公司相关信息
    /// 孙明生
    /// </summary>
    public partial class UCComInfo : UCBase
    {       
        #region --构造函数
        public UCComInfo()
        {
            InitializeComponent();     
        }
        #endregion

        #region --窗体初始化
        private void UCComInfo_Load(object sender, EventArgs e)
        {
            //base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏

            base.btnEdit.Enabled = false;
            btnStatus.Visible = false;
            btnDelete.Visible = false;
            btnVerify.Visible = false;
            //this.btnSave.Enabled = false;
            //this.btnCancel.Enabled = false;

            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is TextBoxEx)
                {
                    ((TextBoxEx)ctl).ReadOnly = true;
                }
            }

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.EditEvent += new ClickHandler(UCComInfo_EditEvent);
            base.SaveEvent += new ClickHandler(UCComInfo_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #region --按钮事件

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        void UCComInfo_SaveEvent(object sender, EventArgs e)
        {
            if (this.tbCompanyName.ReadOnly)
            {
                return;
            }

            Dictionary<string, string> dicFileds = new Dictionary<string, string>();

            dicFileds.Add("setbook_name", this.tbDbName.Caption.Trim());
            dicFileds.Add("com_name", this.tbCompanyName.Caption.Trim());
            dicFileds.Add("organization_code", this.tbCode.Caption.Trim());
            dicFileds.Add("legal_person", this.tb_legal_person.Caption.Trim());
            dicFileds.Add("opening_bank", this.tb_opening_bank.Caption.Trim());
            dicFileds.Add("bank_account", this.tb_bank_account.Caption.Trim());
            dicFileds.Add("postal_address", this.tb_postal_address.Caption.Trim());
            dicFileds.Add("zip_code", this.tb_zip_code.Caption.Trim());
            dicFileds.Add("company_web_site", this.tb_company_web_site.Caption.Trim());
            dicFileds.Add("contact", this.tb_contact.Caption.Trim());
            dicFileds.Add("contact_telephone", this.tb_contact_telephone.Caption.Trim());
            dicFileds.Add("email", this.tb_email.Caption.Trim());

            bool flag = DBHelper.Submit_AddOrEdit("修改账套信息", GlobalStaticObj.CommAccCode, "sys_setbook", "id", this.tbDbCode.Caption, dicFileds);
            if (flag)
            {
                MessageBoxEx.Show("保存成功!", "保存", MessageBoxButtons.OK, MessageBoxIcon.None);
                foreach (Control ctl in this.panelEx1.Controls)
                {
                    if (ctl is TextBoxEx)
                    {
                        ((TextBoxEx)ctl).ReadOnly = true;
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        void UCComInfo_EditEvent(object sender, EventArgs e)
        {
            foreach (Control ctl in this.panelEx1.Controls)
            {
                if (ctl is TextBoxEx && ctl.Name != this.tbDbCode.Name)
                {
                    ((TextBoxEx)ctl).ReadOnly = false;
                }
            }
        }

        
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #endregion

        #region --显示数据

        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _ShowData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询账套信息", GlobalStaticObj.CommAccCode, "sys_setbook", "*", string.Format("id='{0}'", GlobalStaticObj.CurrAccID), "", "");
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowData(object obj)
        {
            DataTable dt = obj as DataTable;
            if (dt.Columns.Contains("id"))
            {
                this.tbDbCode.Caption = dt.Rows[0]["id"].ToString();
            }
            if (dt.Columns.Contains("setbook_name"))
            {
                this.tbDbName.Caption = dt.Rows[0]["setbook_name"].ToString();
            }
            if (dt.Columns.Contains("com_name"))
            {
                this.tbCompanyName.Caption = dt.Rows[0]["com_name"].ToString();
            }
            if (dt.Columns.Contains("organization_code"))
            {
                this.tbCode.Caption = dt.Rows[0]["organization_code"].ToString();
            }
            if (dt.Columns.Contains("legal_person"))
            {
                this.tb_legal_person.Caption = dt.Rows[0]["legal_person"].ToString();
            }
            if (dt.Columns.Contains("opening_bank"))
            {
                this.tb_opening_bank.Caption = dt.Rows[0]["opening_bank"].ToString();
            }
            if (dt.Columns.Contains("bank_account"))
            {
                this.tb_bank_account.Caption = dt.Rows[0]["bank_account"].ToString();
            }
            if (dt.Columns.Contains("postal_address"))
            {
                this.tb_postal_address.Caption = dt.Rows[0]["postal_address"].ToString();
            }
            if (dt.Columns.Contains("zip_code"))
            {
                this.tb_zip_code.Caption = dt.Rows[0]["zip_code"].ToString();
            }
            if (dt.Columns.Contains("company_web_site"))
            {
                this.tb_company_web_site.Caption = dt.Rows[0]["company_web_site"].ToString();
            }
            
            if (dt.Columns.Contains("contact"))
            {
                this.tb_contact.Caption = dt.Rows[0]["contact"].ToString();
            }
            if (dt.Columns.Contains("contact_telephone"))
            {
                this.tb_contact_telephone.Caption = dt.Rows[0]["contact_telephone"].ToString();
            }
            if (dt.Columns.Contains("email"))
            {
                this.tb_email.Caption = dt.Rows[0]["email"].ToString();
            }           
        }

        #endregion
    }
}
