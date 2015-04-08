using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using BLL;
using Utility.Common;
using HXC_FuncUtility;
using SYSModel;

namespace HXCServerWinForm.UCForm
{
    public partial class UCCompanyViewOrAdd : UCBase
    {
        #region --成员变量
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCCompany uc;
        public delegate void RefreshData();
        public RefreshData refreshDataStart;
        private string parentName = string.Empty;
        private WindowStatus winStatus;
        #endregion

        #region --构造函数
        public UCCompanyViewOrAdd(WindowStatus winStatus, string parentName)
        {
            InitializeComponent();
            base.SaveEvent += new ClickHandler(UCCompanyViewOrAdd_SaveEvent);
            base.CancelEvent += new ClickHandler(UCCompanyViewOrAdd_CancelEvent);
            this.winStatus = winStatus;
            this.parentName = parentName;
        }


        #endregion

        private void UCCompanyViewOrAdd_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(uc.Name);
            base.SetBtnStatus(winStatus);
            //绑定公司档案 
            CommonClass.CommonFuncCall.BindCompanyComBox(this.cmbFather, "请选择", "");
            //绑定省份
            CommonClass.CommonFuncCall.BindProviceComBox(this.cmbProvince, "省");
        }

        #region --按钮操作
        //保存
        void UCCompanyViewOrAdd_SaveEvent(object sender, EventArgs e)
        {
            errProvider.Clear();
            if (this.tbCode.Caption.Trim().Length == 0)
            {
                Validator.SetError(errProvider, tbCode, "请录入公司编码");
                return;
            }
            if (this.tbName.Caption.Trim().Length == 0)
            {
                Validator.SetError(errProvider, tbName, "请录入公司名称");
                return;
            }

            //父公司
            if (cmbFather.SelectedIndex == 0)
            {
                Validator.SetError(errProvider, cmbFather, "请选择上级公司");
                return;
            }

            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            dicFileds.Add("com_code", this.tbCode.Caption.Trim());
            dicFileds.Add("com_name", this.tbName.Caption.Trim());//公司全名
            dicFileds.Add("com_address", this.tbAddress.Caption.Trim());//详细地址
            dicFileds.Add("zip_code", this.tbPostCode.Caption.Trim());//邮编
            dicFileds.Add("legal_person", this.tbLegal_Person.Caption.Trim());//法人负责人
            dicFileds.Add("com_contact", this.tbContract.Caption.Trim());//联系人
            dicFileds.Add("com_tel", this.tbTelephone.Caption.Trim());
            dicFileds.Add("com_email", this.tbEmail.Caption.Trim());//电子邮件
            dicFileds.Add("com_fax", this.tbFax.Caption.Trim());//传真
            dicFileds.Add("com_website", this.tbWeb.Caption.Trim());
            dicFileds.Add("remark", this.tbRemark.Caption.Trim());
            dicFileds.Add("com_id", Guid.NewGuid().ToString());
            dicFileds.Add("parent_id", cmbFather.SelectedValue.ToString());
            dicFileds.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
            dicFileds.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.USING.ToString("d"));
            dicFileds.Add("status", DataSources.EnumStatus.Start.ToString("d"));        

            bool bln = DBHelper.Submit_AddOrEdit("添加公司档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "", "", dicFileds);
            if (bln)
            {
                MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                deleteMenuByTag(this.Tag.ToString(), this.parentName);
                uc.BindData();
            }
            else
            {
                MessageBoxEx.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //取消
        void UCCompanyViewOrAdd_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }
        #endregion

        #region --所在地级联选择
        //选择省
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbProvince.SelectedValue.ToString()))
            {
                CommonClass.CommonFuncCall.BindCityComBox(this.cmbCity, this.cmbProvince.SelectedValue.ToString(), "市");
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, this.cmbCity.SelectedValue.ToString(), "县");
            }
            else
            {
                CommonClass.CommonFuncCall.BindCityComBox(this.cmbCity, "", "市");
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, "", "县");
            }
        }
        //选择市
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbCity.SelectedValue.ToString()))
            {
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, this.cmbCity.SelectedValue.ToString(), "市");
            }
            else
            {
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, "", "县");
            }
        }
        #endregion

    }
}
