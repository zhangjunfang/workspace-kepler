using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Utility.Common;
using BLL;
using ServiceStationClient.ComponentUI;
using HXC_FuncUtility;
using SYSModel;

namespace HXCServerWinForm.UCForm
{
    public partial class UCCompanyEdit : UCBase
    {
        #region --成员变量
        private DataRow drRecord;
        /// <summary> 父窗体
        /// </summary>
        public UCCompany uc;
        private string parentName = string.Empty;
        #endregion

        #region --构造函数
        public UCCompanyEdit(DataRow dr, WindowStatus status, string parentName)
        {
            InitializeComponent();
            base.SaveEvent += new ClickHandler(UCCompanyEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCCompanyEdit_CancelEvent);
            base.DeleteEvent += new ClickHandler(UCCompanyEdit_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCCompanyEdit_StatusEvent);
            this.drRecord = dr;
            this.parentName = parentName;
        }
        #endregion

        #region --窗体初始化
        private void UCCompanyEdit_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(uc.Name);
            base.SetBtnStatus(WindowStatus.Edit);
            //省份
            CommonClass.CommonFuncCall.BindProviceComBox(this.cmbProvince, "省");
            //绑定状态 启用 停用 
            DataSources.BindComBoxDataEnum(this.cmbStatus, typeof(DataSources.EnumStatus), false);

            this.InitRecordData();
        }
        #endregion

        #region --按钮操作
        //保存
        void UCCompanyEdit_SaveEvent(object sender, EventArgs e)
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
            drRecord["com_code"] = this.tbCode.Caption.Trim();

            dicFileds.Add("com_name", this.tbName.Caption.Trim());//公司全名
            drRecord["com_name"] = this.tbName.Caption.Trim();

            dicFileds.Add("parent_id", cmbFather.SelectedValue.ToString());
            drRecord["parent_id"] = cmbFather.SelectedValue.ToString();

            dicFileds.Add("com_address", this.tbAddress.Caption.Trim());//详细地址
            drRecord["com_address"] = this.tbAddress.Caption.Trim();
            dicFileds.Add("zip_code", this.tbPostCode.Caption.Trim());//邮编
            drRecord["zip_code"] = this.tbPostCode.Caption.Trim();
            dicFileds.Add("legal_person", this.tbLegal_Person.Caption.Trim());//法人负责人
            drRecord["legal_person"] = this.tbLegal_Person.Caption.Trim();
            dicFileds.Add("com_contact", this.tbContract.Caption.Trim());//联系人
            drRecord["com_contact"] = this.tbContract.Caption.Trim();
            dicFileds.Add("com_tel", this.tbTelephone.Caption.Trim());
            drRecord["com_tel"] = this.tbTelephone.Caption.Trim();
            dicFileds.Add("com_email", this.tbEmail.Caption.Trim());//电子邮件
            drRecord["com_email"] = this.tbEmail.Caption.Trim();
            dicFileds.Add("com_fax", this.tbFax.Caption.Trim());//传真
            drRecord["com_fax"] = this.tbFax.Caption.Trim();
            dicFileds.Add("com_website", this.tbWeb.Caption.Trim());
            drRecord["com_website"] = this.tbWeb.Caption.Trim();
            dicFileds.Add("remark", this.tbRemark.Caption.Trim());
            drRecord["remark"] = this.tbRemark.Caption.Trim();

            dicFileds.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

            bool bln = DBHelper.Submit_AddOrEdit("修改公司档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "com_id", drRecord["com_id"].ToString(), dicFileds);
            if (bln)
            {
                MessageBoxEx.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                drRecord.AcceptChanges();
                deleteMenuByTag(this.Tag.ToString(), this.parentName);
                uc.BindData();
            }
            else
            {
                MessageBoxEx.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                drRecord.RejectChanges();
            }
        }

        //取消
        void UCCompanyEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }

        //删除
        void UCCompanyEdit_DeleteEvent(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion("即将删除该公司档案，是否继续？"))
            {
                return;
            }
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", "0");
            string[] ids = new string[1] { drRecord["com_id"].ToString() };
            bool flag = DBHelper.BatchUpdateDataByIn("删除公司档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", comField, "com_id", ids);
            if (flag)
            {
                this.drRecord.Table.Rows.Remove(drRecord);
                MessageBoxEx.Show("删除成功！");
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
                uc.BindData();
            }
        }

        //启动/停用
        void UCCompanyEdit_StatusEvent(object sender, EventArgs e)
        {
            Dictionary<string, string> comField = new Dictionary<string, string>();
            string status = string.Empty;
            string msg = string.Empty;
            if (drRecord["status"].ToString() == DataSources.EnumStatus.Start.ToString("d"))
            {
                status = DataSources.EnumStatus.Stop.ToString("d");
                msg = "停用公司档案";
            }
            else
            {
                status = DataSources.EnumStatus.Start.ToString("d");
                msg = "启用公司档案";
            }
            comField.Add("status", status);
            comField.Add("status", status);

            bool flag = DBHelper.Submit_AddOrEdit(msg, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "com_id", drRecord["com_id"].ToString(), comField);
            if (flag)
            {
                MessageBoxEx.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                drRecord["status"] = status;
                uc.BindData();
            }
        }
        #endregion

        #region --成员方法
        /// <summary>
        /// 初始化记录值
        /// </summary>
        private void InitRecordData()
        {
            tbCode.ReadOnly = true;
            //绑定公司档案 
            CommonClass.CommonFuncCall.BindCompanyComBox(this.cmbFather, "请选择", drRecord["com_id"].ToString());
            this.tbCode.Caption = drRecord["com_code"].ToString();
            this.tbName.Caption = drRecord["com_name"].ToString();
            //上级公司
            string strPID = drRecord["parent_id"].ToString();
            if (!string.IsNullOrEmpty(strPID) && !strPID.Equals("-1"))
            {
                this.cmbFather.SelectedValue = strPID;
            }

            this.cmbProvince.SelectedValue = drRecord["province"].ToString();
            this.cmbCity.SelectedValue = drRecord["city"].ToString();
            this.cmbTown.SelectedValue = drRecord["county"].ToString();
            this.tbAddress.Caption = drRecord["com_address"].ToString();
            this.tbPostCode.Caption = drRecord["zip_code"].ToString();
            this.tbLegal_Person.Caption = drRecord["legal_person"].ToString();
            //联系人
            this.tbContract.Caption = drRecord["com_contact"].ToString();
            //联系电话
            this.tbTelephone.Caption = drRecord["com_tel"].ToString();
            this.tbEmail.Caption = drRecord["com_email"].ToString();
            this.tbFax.Caption = drRecord["com_fax"].ToString();
            //网址
            this.tbWeb.Caption = drRecord["com_website"].ToString();
            this.tbRemark.Caption = drRecord["remark"].ToString();

            this.tbCreator.Caption = drRecord["create_by"].ToString();
            this.tbCreateTime.Caption = Common.UtcLongToLocalDateTime(drRecord["create_time"]);
            this.tbLastEdit.Caption = drRecord["update_by"].ToString();
            this.tbLastEdit_Time.Caption = Common.UtcLongToLocalDateTime(drRecord["update_time"].ToString());

            this.cmbStatus.SelectedValue = drRecord["status"].ToString();
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
