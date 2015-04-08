using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using BLL;
using Utility.Common;
using HXCServerWinForm.CommonClass;
using SYSModel;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm
{
    public partial class UCOrganizeAddOrEdit : UCBase
    {
        #region --成员变量
        public DataRow drRecord;
        public UCOrganize uc;
        #endregion

        #region --构造函数
        public UCOrganizeAddOrEdit()
        {
            InitializeComponent();
            this.SaveEvent += new ClickHandler(UCOrganizeAddOrEdit_SaveEvent);
            this.CancelEvent += new ClickHandler(UCOrganizeAddOrEdit_CancelEvent);
            this.DeleteEvent += new ClickHandler(UCOrganizeAddOrEdit_DeleteEvent);
            this.StatusEvent += new ClickHandler(UCOrganizeAddOrEdit_StatusEvent);
        }
        #endregion

        #region --窗体初始化
        private void UCOrganizeAdd_Load(object sender, EventArgs e)
        {
            //加载公司
            CommonFuncCall.BindCompanyComBox(this.cmbCompany, "请选择", "");
            base.SetOpButtonVisible(uc.Name);
            base.SetBtnStatus(windowStatus);
           
            string org_code = "";
            tbCode.ReadOnly = true;
            if (drRecord != null)
            {
                org_code = drRecord["org_code"].ToString();
                tbCode.Caption = drRecord["org_code"].ToString();
            }
            else
            {
                tbCode.Caption = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Organization);
            }
            if (windowStatus != WindowStatus.Add)
            {
                this.InitRecordData();
            }
            else
            {
                lbfilter.Visible = false;
                lbcreate_by.Visible = false;
                tbCreator.Visible = false;
                lbcreate_time.Visible = false;
                tbCreateTime.Visible = false;
                lbupdate_by.Visible = false;
                tbLastEdit.Visible = false;
                lbupdate_time.Visible = false;
                tbLastEdit_Time.Visible = false;
            }
            if (windowStatus == WindowStatus.View)
            {
               
                Common.SetControlForLable(this, true);
            }
        }
        #endregion

        #region --按钮操作
        /// <summary> 保存
        /// </summary>
        void UCOrganizeAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbCompany.SelectedIndex == 0)
                {
                    Validator.SetError(errProvider, cmbCompany, "请选择所属公司");
                    return;
                }
                if (tbCode.Caption.Length == 0)
                {
                    Validator.SetError(errProvider, tbCode, "请填写组织编码");
                    return;
                }
                if (tbName.Caption.Length == 0)
                {
                    Validator.SetError(errProvider, tbName, "请填写组织名称");
                    return;
                }

                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                dicFileds.Add("com_id", this.cmbCompany.SelectedValue.ToString());
                string parentID = "";
                if (cmbOrganize.SelectedIndex > 0)
                {
                    parentID = cmbOrganize.SelectedValue.ToString();
                }
                else
                {
                    parentID = cmbCompany.SelectedValue.ToString();
                }
                dicFileds.Add("parent_id", parentID);
                dicFileds.Add("org_code", this.tbCode.Caption.Trim());
                dicFileds.Add("org_name", this.tbName.Caption.Trim());//公司全名
                dicFileds.Add("contact_name", this.tbContract.Caption.Trim());//联系人
                dicFileds.Add("contact_telephone", this.tbTelephone.Caption.Trim());
                dicFileds.Add("remark", this.tbRemark.Caption.Trim());
                dicFileds.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                string pkName = "";
                string pkValue = "";
                if (windowStatus == WindowStatus.Add)
                {
                    dicFileds.Add("org_id", Guid.NewGuid().ToString());
                    dicFileds.Add("enable_flag", "1");//1为未删除状态
                    dicFileds.Add("status", DataSources.EnumStatus.Start.ToString("d"));//启用
                    dicFileds.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    dicFileds.Add("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                }
                else
                {
                    pkName = "org_id";
                    pkValue = drRecord["org_id"].ToString();
                }

                bool bln = DBHelper.Submit_AddOrEdit("添加组织档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", pkName, pkValue, dicFileds);
                if (bln)
                {
                    MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    uc.BindData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("组织管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 取消
        /// </summary>
        void UCOrganizeAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("组织管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 启停用
        /// </summary>
        void UCOrganizeAddOrEdit_StatusEvent(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> comField = new Dictionary<string, string>();
                string status = string.Empty;
                string msg = string.Empty;
                if (drRecord["status"].ToString() == DataSources.EnumStatus.Start.ToString("d"))
                {
                    status = DataSources.EnumStatus.Stop.ToString("d");
                    msg = "停用组织档案";
                }
                else
                {
                    status = DataSources.EnumStatus.Start.ToString("d");
                    msg = "启用组织档案";
                }
                comField.Add("status", status);

                bool flag = DBHelper.Submit_AddOrEdit(msg, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", "org_id", drRecord["org_id"].ToString(), comField);
                if (flag)
                {
                    MessageBoxEx.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    drRecord["status"] = status;
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("组织管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 删除
        /// </summary>
        void UCOrganizeAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", "0");
                string[] ids = new string[1] { drRecord["org_id"].ToString() };
                bool flag = DBHelper.BatchUpdateDataByIn("删除组织档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", comField, "org_id", ids);
                if (flag)
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("组织管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        #endregion

        #region --成员方法
        /// <summary> 初始化记录值
        /// </summary>
        private void InitRecordData()
        {
            this.cmbCompany.SelectedValue = drRecord["com_id"].ToString();
            this.tbCode.Caption = drRecord["org_code"].ToString();
            this.tbName.Caption = drRecord["org_name"].ToString();
            this.cmbOrganize.SelectedValue = drRecord["parent_id"].ToString();
            //联系人
            this.tbContract.Caption = drRecord["contact_name"].ToString();
            //联系电话
            this.tbTelephone.Caption = drRecord["contact_telephone"].ToString();
            this.tbRemark.Caption = drRecord["remark"].ToString();
            this.tbCreator.Caption = drRecord["create_name"].ToString();
            this.tbCreator.Tag = drRecord["create_by"].ToString();
            this.tbCreateTime.Caption = Common.UtcLongToLocalDateTime(drRecord["create_time"]);
            this.tbLastEdit.Tag = drRecord["update_by"].ToString();
            this.tbLastEdit.Caption = drRecord["update_name"].ToString();
            this.tbLastEdit_Time.Caption = Common.UtcLongToLocalDateTime(drRecord["update_time"].ToString());
        }
        #endregion

        #region --选择公司组织联动
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbCompany.SelectedValue != null
                    && this.cmbCompany.SelectedValue.ToString().Length > 0)
                {
                    //加载组织
                    CommonFuncCall.BindOrganizeComBox(this.cmbOrganize, this.cmbCompany.SelectedValue.ToString(), "请选择");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("组织管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion
    }
}
