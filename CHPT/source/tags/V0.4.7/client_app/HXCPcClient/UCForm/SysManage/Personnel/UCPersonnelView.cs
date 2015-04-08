using System;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using System.Collections.Generic;

namespace HXCPcClient.UCForm.SysManage.Personnel
{
    /// <summary>
    /// 人员管理 预览
    /// 孙明生
    /// </summary>
    public partial class UCPersonnelView : UCBase
    {

        #region 属性
        /// <summary>
        /// 人员id
        /// </summary>
        public string id = "";

        /// <summary>
        /// 父窗体
        /// </summary>
        public UCPersonnelManager uc;

        public WindowStatus wStatus;

        DataRow userinfo;
        #endregion

        #region 构造和载入函数
        public UCPersonnelView()
        {
            InitializeComponent();
            SubscribeEvent();
        }

        private void UCPersonnelView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(wStatus);
            BindCboData();
            BindData();
            BindPhoto();
            BindAData("sys_user", id);
            SetSysManageViewBtn();
            SetBtnStatus();
        }
        #endregion

        #region 事件订阅和处理
        private void SubscribeEvent()
        {
            CancelEvent += new ClickHandler(UCPersonnelView_CancelEvent);
            EditEvent += new ClickHandler(UCWareHouseView_EditEvent);
            DeleteEvent += new ClickHandler(UCWareHouseView_DeleteEvent);
            StatusEvent += new ClickHandler(UCWareHouseView_StatusEvent);
        }

        void UCWareHouseView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCWareHouseView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCWareHouseView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        void UCPersonnelView_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (userinfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
                MessageBoxEx.Show(btnStatus.Caption + "成功！");
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (userinfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        private bool StatusSql()
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            if (userinfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "人员", "sys_user", "user_id", userinfo["user_id"].ToString(), status);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit()
        {

            UCPersonnelAddOrEdit editFrm = new UCPersonnelAddOrEdit(userinfo, uc.Name);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(editFrm, "人员档案-编辑", "UCSupplierEdit" + userinfo["user_id"].ToString(), this.Tag.ToString(), this.Name);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dictionary<string, string> updateField = new Dictionary<string, string>();
                updateField.Add("enable_flag", "0");
                if (DBHelper.Submit_AddOrEdit("删除人员", "sys_user", "user_id", userinfo["user_id"].ToString(), updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (userinfo["status"] == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

        #region 数据绑定回显
        /// <summary>
        /// 绑定图片
        /// </summary>
        private void BindPhoto()
        {
            DataTable dt_pic = DBHelper.GetTable("查询用户图片", "attachment_info", "*", "relation_object_id='" + id + "' and att_name='用户图片' and att_type='图片'", "", "");
            if (dt_pic != null && dt_pic.Rows.Count > 0)
            {
                string photoPath = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_path"]);
                string photoID = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_id"]);
                picuser.BackgroundImage = FileOperation.DownLoadImage(photoPath);
            }
        }

        /// <summary>
        /// 绑定下拉框数据
        /// </summary>
        private void BindCboData()
        {
            DataSources.BindComBoxDataEnum(cbostatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbodata_sources, typeof(DataSources.EnumDataSources), true);//数据来源 自建 宇通

            CommonFuncCall.BindComBoxDataSource(cbojkzk, "sys_health", "请选择");//健康状况

            DataSources.BindComBoxDataEnum(cbois_operator, typeof(DataSources.EnumYesNo), false);//是否操作员
            CommonFuncCall.BindComBoxDataSource(cbonation, "sys_nation", "请选择");//民族
            CommonFuncCall.BindComBoxDataSource(cbosex, "sys_sex", "请选择");//性别
            CommonFuncCall.BindComBoxDataSource(cboidcard_type, "sys_certificates_type", "请选择");//证件类型
            CommonFuncCall.BindComBoxDataSource(cboeducation, "sys_education", "请选择");//学历
            CommonFuncCall.BindComBoxDataSource(cboposition, "sys_post", "请选择");//岗位
            CommonFuncCall.BindComBoxDataSource(cbolevel, "sys_personnel_level", "请选择");//级别
        }

        /// <summary>
        /// 绑定回显数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = DBHelper.GetTable("查询人员信息", "v_User", "*", "user_id='" + id + "'", "", "");
            if (dt.Rows.Count <= 0)
            {
                MessageBoxEx.Show("查询人员失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            userinfo = dt.Rows[0];
            lbluser_code.Text = userinfo["user_code"].ToString();
            lbluser_name.Text = userinfo["user_name"].ToString();
            lblland_name.Text = userinfo["land_name"].ToString();
            lblremark.Text = userinfo["remark"].ToString();
            lbluser_phone.Text = userinfo["user_phone"].ToString();
            lbluser_telephone.Text = userinfo["user_telephone"].ToString();
            txtcom_name.Caption = userinfo["com_name"].ToString();
            txcorg_name.Text = userinfo["org_name"].ToString();
            lbluser_fax.Text = userinfo["user_fax"].ToString();
            if (string.IsNullOrEmpty(userinfo["sex"].ToString()))
            { cbosex.Visible = false; }
            else
            { cbosex.SelectedValue = userinfo["sex"].ToString(); }

            cbois_operator.SelectedValue = userinfo["is_operator"].ToString();

           
            lbluser_email.Text = userinfo["user_email"].ToString();
            lblidcard_num.Text = userinfo["idcard_num"].ToString();
            lbluser_address.Text = userinfo["user_address"].ToString();
            lblcreate_by.Text = userinfo["create_Username"].ToString();
            lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(userinfo["create_time"].ToString())).ToString();
            lblupdate_by.Text = userinfo["update_username"].ToString();
            lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(userinfo["update_time"].ToString())).ToString();
            cbostatus.SelectedValue = userinfo["status"].ToString();
            cbodata_sources.SelectedValue = userinfo["data_sources"].ToString();


            lblgraduate_institutions.Text = userinfo["graduate_institutions"].ToString();
            lbltechnical_expertise.Text = userinfo["technical_expertise"].ToString();
            lbluser_height.Text = userinfo["user_height"].ToString();
            lblnative_place.Text = userinfo["native_place"].ToString();
            lblspecialty.Text = userinfo["specialty"].ToString();
            lblentry_date.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(userinfo["entry_date"].ToString())).ToString();
            lbluser_weight.Text = userinfo["user_weight"].ToString();
            lblregister_address.Text = userinfo["register_address"].ToString();
            lblgraduate_date.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(userinfo["graduate_date"].ToString())).ToString();
            lblwage.Text = userinfo["wage"].ToString();
            lblbirthday.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(userinfo["birthday"].ToString())).ToString();
            lblpolitical_status.Text = userinfo["political_status"].ToString();

            cboeducation.SelectedValue = userinfo["education"].ToString();
            if (cboeducation.SelectedValue.ToString() == string.Empty)
                cboeducation.Visible = false;
            cbonation.SelectedValue = userinfo["nation"].ToString();
            if (cbonation.SelectedValue.ToString() == string.Empty)
                cbonation.Visible = false;
            cboposition.SelectedValue = userinfo["position"].ToString();
            if (cboposition.SelectedValue.ToString() == string.Empty)
                cboposition.Visible = false;
            cbolevel.SelectedValue = userinfo["level"].ToString();
            if (cbolevel.SelectedValue.ToString() == string.Empty)
                cbolevel.Visible = false;
            cbojkzk.SelectedValue = userinfo["health"].ToString();
            if (cbojkzk.SelectedValue.ToString() == string.Empty)
                cbojkzk.Visible = false;
            cboidcard_type.SelectedValue = userinfo["idcard_type"].ToString();
            if (cboidcard_type.SelectedValue.ToString() == string.Empty)
                cboidcard_type.Visible = false;

        }
        #endregion

        #region 加载附件信息
        /// <summary>
        /// 加载附件信息
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strOrderId"></param>
        private void BindAData(string strTableName, string strOrderId)
        {
            //附件信息数据
            ucAttr.TableName = strTableName;
            ucAttr.TableNameKeyValue = strOrderId;
            ucAttr.BindAttachment();
        }
        #endregion

    }
}
