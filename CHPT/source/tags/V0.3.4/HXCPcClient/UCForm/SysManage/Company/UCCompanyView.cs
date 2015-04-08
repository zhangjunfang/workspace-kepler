using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using System.Collections;

namespace HXCPcClient.UCForm.SysManage.Company
{
    /// <summary>
    /// 公司档案 预览
    /// 孙明生
    /// </summary>
    public partial class UCCompanyView : UCBase
    {
        /// <summary>
        /// 公司id
        /// </summary>
        public string id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCCompanyManager uc;

        public WindowStatus wStatus;
        DataTable dtdic;
        public UCCompanyView(string _id, UCCompanyManager _uc, WindowStatus _wStatus)
        {
            id = _id;
            uc = _uc;
            wStatus = _wStatus;
            InitializeComponent();
            base.CancelEvent += new ClickHandler(UCCompanyView_CancelEvent);
            UCCompanyView_Load(null, null);
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCCompanyView_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name); 
        }

        #region Load
        private void UCCompanyView_Load(object sender, EventArgs e)
        {
           // base.RoleButtonStstus(uc.Name);//角色按钮权限-是否隐藏
            base.SetBtnStatus(wStatus);
            //CommonCtrl.BindComboBoxByDictionarr(cbostatus, "sys_data_status", true);//绑定状态 启用 停用
            //CommonCtrl.BindComboBoxByDictionarr(cbodata_sources, "sys_data_source", true);//数据来源 自建 宇通

            DataSources.BindComBoxDataEnum(cbostatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbodata_sources, typeof(DataSources.EnumDataSources), true);//数据来源 自建 宇通

            CommonCtrl.BindComboBoxByDictionarr(cborepair_qualification, "sys_repair_qualification", true);//维修资质
            CommonCtrl.BindComboBoxByDictionarr(cbounit_properties, "sys_enterprise_property", true);//单位性质
            CommonFuncCall.BindProviceComBox(cboprovince, "请选择");
            string strSql = "select c.*,(select USER_NAME from sys_user where user_id =c.create_by )as create_Username , "
                            + "(select USER_NAME from sys_user where user_id =c.update_by ) as update_username  from tb_company c where c.com_id='" + id + "'";
            SQLObj sqlobj = new SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询公司", sqlobj);
            //DBHelper.GetTable("查询公司", "tb_company", "*", "com_id='" + id + "'", "", "");
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                MessageBoxEx.Show("查询公司失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = ds.Tables[0];
            lblcom_code.Text = dt.Rows[0]["com_code"].ToString();//公司编码
            lblcom_name.Text = dt.Rows[0]["com_name"].ToString();//公司全名
            lblcom_short_name.Text = dt.Rows[0]["com_short_name"].ToString();//公司简称
            cborepair_qualification.SelectedValue = dt.Rows[0]["repair_qualification"].ToString();//维修资质
            cbounit_properties.SelectedValue = dt.Rows[0]["unit_properties"].ToString();//单位性质
            lbllegal_person.Text = dt.Rows[0]["legal_person"].ToString();//法人负责人
            lblcertificate_code.Text = dt.Rows[0]["certificate_code"].ToString();//组织机构代码
            lblzip_code.Text = dt.Rows[0]["zip_code"].ToString();//邮编
            cboprovince.SelectedValue = dt.Rows[0]["province"].ToString();//省份
            cbocity.SelectedValue = dt.Rows[0]["city"].ToString();//城市
            cbocounty.SelectedValue = dt.Rows[0]["county"].ToString();//区县
            lblcom_address.Text = dt.Rows[0]["com_address"].ToString();//详细地址
            lblcom_tel.Text = dt.Rows[0]["com_tel"].ToString();//固话
            lblcom_phone.Text = dt.Rows[0]["com_phone"].ToString();//手机
            lblcom_email.Text = dt.Rows[0]["com_email"].ToString();//电子邮件
            lblcom_fax.Text = dt.Rows[0]["com_fax"].ToString();//传真
            lbltax_account.Text = dt.Rows[0]["tax_account"].ToString();//税号
            lbltax_qualification.Text = dt.Rows[0]["tax_qualification"].ToString();//纳税人资格
            ckbindepen_check.Checked = dt.Rows[0]["indepen_check"].ToString() == "1" ? true : false;//独立核算  0 为否  1为是
            ckbindepen_legalperson.Checked = dt.Rows[0]["indepen_legalperson"].ToString() == "1" ? true : false;//独立法人  0 为否  1为是
            ckbfinancial_indepen.Checked = dt.Rows[0]["financial_indepen"].ToString() == "1" ? true : false;// 财务独立 0 为否  1为是  

            lblremark.Text = dt.Rows[0]["remark"].ToString();
            lblcreate_by.Text = dt.Rows[0]["create_Username"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["create_time"].ToString()))
            {
                lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["create_time"].ToString())).ToString();
            }

            lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["update_time"].ToString()))
            {
                lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["update_time"].ToString())).ToString();
            }

            cbostatus.SelectedValue = dt.Rows[0]["status"].ToString();
            cbodata_sources.SelectedValue = dt.Rows[0]["data_source"].ToString();

            long time=0;
            if (!string.IsNullOrEmpty(dt.Rows[0]["work_time"].ToString())
                    && long.TryParse(dt.Rows[0]["work_time"].ToString(), out time))
            {
                lblwork_time.Text = Common.UtcLongToLocalDateTime(time).ToString();//上班时间
            }            

            lblser_car_num.Text = dt.Rows[0]["ser_car_num"].ToString();//服务车数
            chkis_repair_newenergy.Checked = dt.Rows[0]["is_repair_newenergy"].ToString() == "1" ? true : false;// 是否维修新能源  0 为否  1为是
            chkis_repair_ng.Checked = dt.Rows[0]["is_repair_ng"].ToString() == "1" ? true : false;// 是否维修NG车 0 为否  1为是 
            lblstaff_counts.Text = dt.Rows[0]["staff_counts"].ToString();//人员总数
            lblser_staff_counts.Text = dt.Rows[0]["ser_staff_counts"].ToString();//服务人员数
            lblmach_repair_staff_counts.Text = dt.Rows[0]["mach_repair_staff_counts"].ToString();//机器人数
            lblholder_electrician_counts.Text = dt.Rows[0]["holder_electrician_counts"].ToString();//持证电工数
            lbltrench_counts.Text = dt.Rows[0]["trench_counts"].ToString();//地沟\举升机数
            lbltwelve_trench_counts.Text = dt.Rows[0]["twelve_trench_counts"].ToString();//标准地沟\举升机数
            lblfour_location_counts.Text = dt.Rows[0]["four_location_counts"].ToString();//四轮定位仪数
            lblengine_test_counts.Text = dt.Rows[0]["engine_test_counts"].ToString();//发动机检测仪数

            lblfactory_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["factory_area"].ToString(), 2);//厂区占地面积
            lblparking_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["parking_area"].ToString(), 2);//停车区面积
            lblreception_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["reception_area"].ToString(), 2);//接待室面积
            lblcust_lounge_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["cust_lounge_area"].ToString(), 2);//客户休息室面积
            lblcust_toilet_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["cust_toilet_area"].ToString(), 2);//客户洗手间面积
            lblmeeting_room_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["meeting_room_area"].ToString(), 2);//会议室面积
            lbltraining_room_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["training_room_area"].ToString(), 2);//培训室面积
            lblsettlement_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["settlement_area"].ToString(), 2);//结算区面积

            lblrepaired_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["repaired_area"].ToString(), 2);//待修区面积
            lblcheck_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["check_area"].ToString(), 2);//检查区面积
            lblrepair_workshop_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["repair_workshop_area"].ToString(), 2);//维修车间面积
            lblbig_repaired_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["big_repaired_area"].ToString(), 2);//总成大修面积
            lblparts_sales_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["parts_sales_area"].ToString(), 2);//配件销售面积
            lblparts_warehouse_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["parts_warehouse_area"].ToString(), 2);//配件仓库面积
            lbloldparts_warehouse_area.Text = CommonUtility.DecimalRightZeroFill(dt.Rows[0]["oldparts_warehouse_area"].ToString(), 2);//旧件仓库面积
            lblrepair_instructions.Text = dt.Rows[0]["repair_instructions"].ToString();//维修说明

            ArrayList al = new ArrayList();
            al.Add("sys_post");//岗位
            dtdic = CommonFuncCall.GetDictionariesByPDic_codes(al);

            DataTable dt_user = DBHelper.GetTable("查询公司人员信息", "v_User", "*", "com_id='" + id + "'", "", "order by create_time");
            dgvUser.DataSource = dt_user.DefaultView;
        }
        #endregion

        #region 省市 选择
        private void cboprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboprovince.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(cbocity, cboprovince.SelectedValue.ToString(), "请选择");
                CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCityComBox(cbocity, "", "请选择");
                CommonFuncCall.BindCountryComBox(cbocounty, "", "请选择");
            }
        }

        private void cbocity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbocity.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(cbocounty, "", "请选择");
            }
        }
         #endregion

        #region 格式化数据
        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvUser.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("position"))
            {
                e.Value = CommonFuncCall.RetrievalDic_name(dtdic, e.Value.ToString());
            }
        }
        #endregion

    }
}
