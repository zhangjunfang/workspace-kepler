using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCServerWinForm.CommonClass;
using SYSModel;
using Utility.Common;
using BLL;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm.Personnel
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
        #endregion

        #region 初始化
        public UCPersonnelView()
        {
            InitializeComponent();
            base.CancelEvent += new ClickHandler(UCPersonnelView_CancelEvent);
        }
        #endregion

        #region 取消事件
        void UCPersonnelView_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region Load
        private void UCPersonnelView_Load(object sender, EventArgs e)
        {
            //base.RoleButtonStstus(uc.Name);//角色按钮权限-是否隐藏
            base.SetBtnStatus(wStatus);
            DataSources.BindComBoxDataEnum(cbostatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbodata_sources, typeof(DataSources.EnumDataSources), true);//数据来源 自建 宇通

            //CommonCtrl.BindComboBoxByDictionarr(cbostatus, "sys_data_status", true);//绑定状态 启用 停用
            //CommonCtrl.BindComboBoxByDictionarr(cbodata_sources, "sys_data_source", true);//数据来源 自建 宇通

            DataSources.BindComBoxDataEnum(cbois_operator, typeof(DataSources.EnumYesNo), false);//是否操作员
            CommonFuncCall.BindComBoxDataSource(cbonation, "sys_nation", "请选择");//民族
            CommonFuncCall.BindComBoxDataSource(cbosex, "sys_sex", "请选择");//性别
            CommonFuncCall.BindComBoxDataSource(cboidcard_type, "sys_certificates_type", "请选择");//证件类型
            CommonFuncCall.BindComBoxDataSource(cboeducation, "sys_education", "请选择");//学历
            CommonFuncCall.BindComBoxDataSource(cboposition, "sys_post", "请选择");//岗位
            CommonFuncCall.BindComBoxDataSource(cbolevel, "sys_personnel_level", "请选择");//级别
            DataTable dt = DBHelper.GetTable("查询人员信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_User", "*", "user_id='" + id + "'", "", "");
            if (dt.Rows.Count <= 0)
            {
                MessageBoxEx.Show("查询人员失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lbluser_code.Text = dt.Rows[0]["user_code"].ToString();
            lbluser_name.Text = dt.Rows[0]["user_name"].ToString();
            lblland_name.Text = dt.Rows[0]["land_name"].ToString();
            lblremark.Text = dt.Rows[0]["remark"].ToString();
            lbluser_phone.Text = dt.Rows[0]["user_phone"].ToString();
            lbluser_telephone.Text = dt.Rows[0]["user_telephone"].ToString();
            txtcom_name.Caption = dt.Rows[0]["com_name"].ToString();
            txcorg_name.Text = dt.Rows[0]["org_name"].ToString();
            lbluser_fax.Text = dt.Rows[0]["user_fax"].ToString();
            cbosex.SelectedValue = dt.Rows[0]["sex"].ToString();
            cbois_operator.SelectedValue = dt.Rows[0]["is_operator"].ToString();
            cboidcard_type.SelectedValue = dt.Rows[0]["idcard_type"].ToString();
            lbluser_email.Text = dt.Rows[0]["user_email"].ToString();
            lblidcard_num.Text = dt.Rows[0]["idcard_num"].ToString();
            lbluser_address.Text = dt.Rows[0]["user_address"].ToString();
            lblcreate_by.Text = dt.Rows[0]["create_Username"].ToString();
            lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["create_time"].ToString())).ToString();
            lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();
            lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["update_time"].ToString())).ToString();
            cbostatus.SelectedValue = dt.Rows[0]["status"].ToString();
            cbodata_sources.SelectedValue = dt.Rows[0]["data_sources"].ToString();

            cbonation.SelectedValue = dt.Rows[0]["nation"].ToString();
            lblgraduate_institutions.Text = dt.Rows[0]["graduate_institutions"].ToString();
            lbltechnical_expertise.Text = dt.Rows[0]["technical_expertise"].ToString();
            lbluser_height.Text = dt.Rows[0]["user_height"].ToString();
            lblnative_place.Text = dt.Rows[0]["native_place"].ToString();
            lblspecialty.Text = dt.Rows[0]["specialty"].ToString();
            lblentry_date.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["entry_date"].ToString())).ToString();
            lbluser_weight.Text = dt.Rows[0]["user_weight"].ToString();
            lblregister_address.Text = dt.Rows[0]["register_address"].ToString();
            lblgraduate_date.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["graduate_date"].ToString())).ToString();
            lblwage.Text = dt.Rows[0]["wage"].ToString();
            lblbirthday.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["birthday"].ToString())).ToString();
            cboeducation.SelectedValue = dt.Rows[0]["education"].ToString();
            cboposition.SelectedValue = dt.Rows[0]["position"].ToString();
            lblpolitical_status.Text = dt.Rows[0]["political_status"].ToString();
            cbolevel.SelectedValue = dt.Rows[0]["level"].ToString();


            DataTable dt_pic = DBHelper.GetTable("查询用户图片", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "attachment_info", "*", "relation_object_id='" + id + "' and att_name='用户图片' and att_type='图片'", "", "");
            if (dt_pic != null && dt_pic.Rows.Count > 0)
            {
                string photoPath = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_path"]);
                string photoID = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_id"]);
                picuser.BackgroundImage = FileOperation.DownLoadImage(photoPath);
            }
            BindAData("sys_user", id);
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
