using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.UCForm.CustomerService.Feedback
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCFeedbackAddOrEdit         
    /// Author:       Kord
    /// Date:         2014/10/22 17:55:09
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// Function: 
    /// 	客户服务-信息反馈-新增/编辑
    ///***************************************************************************//
    public partial class UCFeedbackAddOrEdit : UCBase
    {
        #region Constructor -- 构造函数
        public UCFeedbackAddOrEdit()
        {
            InitializeComponent();

            Load += UCCallBackAddOrEdit_Load;
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCFeedbackManager UCFeedbakcManager { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public WindowStatus OpType { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public String CustId { get; set; }
        /// <summary>
        /// 反馈人Id
        /// </summary>
        public String ContId { get; set; }
        /// <summary>
        /// 经办人Id
        /// </summary>
        public String HandleId { get; set; }
        /// <summary>
        /// 反馈信息Id
        /// </summary>
        public String FeedbackId { get; set; }
        #endregion

        #region Method -- 方法
        //初始化
        private void Init()
        {
            #region 设置功能按钮可见性
            UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>()
            {
                btnSave, btnCancel, btnSet, btnView, btnPrint
            });
            #endregion

            #region 初始化数据绑定
            CommonCtrl.BindComboBoxByDictionarr(cbo_cb_Callback_type, "sys_callback_type", false);    //绑定反馈类型
            CommonCtrl.BindComboBoxByDictionarr(cbo_cb_Callback_mode, "sys_callback_mode", false);    //绑定反馈方式
            CommonCtrl.BindComboBoxByDictionarr(cbo_cust_type, "sys_customer_category", false);    //客户类别
            CommonCtrl.BindComboBoxByDictionarr(cbo_member_class, "sys_member_grade", false);    //绑定会员等级
            CommonFuncCall.BindProviceComBox(cbo_province, "请选择");  //绑定省份
            CommonFuncCall.BindCityComBox(cbo_city, "", "请选择");   //绑定城市
            CommonFuncCall.BindCountryComBox(cbo_county, "", "请选择");    //绑定县/区
            cbo_province.SelectedIndexChanged += ddlprovince_SelectedIndexChanged;
            cbo_city.SelectedIndexChanged += ddlcity_SelectedIndexChanged;
            CommonCtrl.CmbBindDict(cbo_cust_type, "sys_customer_category", false);  //客户类别
            #endregion

            lbl_cb_create_by.Text = GlobalStaticObj.UserName;
            lbl_cb_create_by.Tag = GlobalStaticObj.UserID;
            if (OpType == WindowStatus.Edit || OpType == WindowStatus.View)
            {
                SetFeedbackInfo();
                SetCustInfo();
                SetContInfo();
            }
            if (OpType == WindowStatus.View) palQTop.Enabled = false;

            #region 注册功能按钮事件
            #region 选择客户信息
            txt_cust_code.ChooserClick += delegate
            {
                var frmCustomer = new frmCustomerInfo();
                var result = frmCustomer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CustId = frmCustomer.strCustomerId;
                    txt_cust_code.Tag = CustId;
                    txt_cust_code.Text = frmCustomer.strCustomerNo;
                }
                SetCustInfo();
            };

            #endregion

            #region 选择反馈人
            txt_cb_Callback_by.ChooserClick += delegate
            {
                var frmContacts = new frmContacts();
                var result = frmContacts.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ContId = frmContacts.contID;
                    txt_cb_Callback_by.Tag = ContId;
                    txt_cb_Callback_by.Text = frmContacts.contName;
                    txt_cb_Callback_by_phone.Caption = frmContacts.contPhone;
                }
                SetContInfo();
            };
            #endregion

            #region 选择经办人
            txt_handle_name.ChooserClick += delegate
            {
                var chooser = new frmUsers();
                var result = chooser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txt_handle_name.Text = chooser.User_Name;
                    txt_handle_name.Tag = chooser.User_ID;
                    txt_cb_handle_org.Caption = chooser.OrgName;
                }
            };
            #endregion

            #region 保存数据
            SaveEvent += delegate
            {
                var check = CheckValue();
                if (!check) return;
                var dicFileds = new Dictionary<String, String>();
                if (OpType == WindowStatus.Add)
                {
                    dicFileds.Add("create_by", GlobalStaticObj.UserID);  //创建人
                    dicFileds.Add("create_time", DBHelper.GetCurrentTime().Ticks.ToString());    //创建时间
                    dicFileds.Add("update_by", GlobalStaticObj.UserID);  //最后编辑人
                    dicFileds.Add("update_time", DBHelper.GetCurrentTime().Ticks.ToString());    //最后编辑时间
                    dicFileds.Add("Feedback_id", Guid.NewGuid().ToString());  //客户ID

                }
                else if (OpType == WindowStatus.Edit)
                {
                    dicFileds.Add("update_by", GlobalStaticObj.UserID);  //最后编辑人
                    dicFileds.Add("update_time", DBHelper.GetCurrentTime().Ticks.ToString());    //最后编辑时间
                }
                dicFileds.Add("corp_id", txt_cust_code.Tag.ToString());  //客户ID
                dicFileds.Add("Feedback_time", DBHelper.GetCurrentTime().Ticks.ToString());   //反馈时间
                dicFileds.Add("Feedback_type", cbo_cb_Callback_type.SelectedValue.ToString()); //反馈类型
                dicFileds.Add("Feedback_mode", cbo_cb_Callback_mode.SelectedValue.ToString()); //反馈方式
                dicFileds.Add("title",txt_cb_title.Caption);    //反馈标题
                dicFileds.Add("record", rtx_cb_record.Text);    //反馈内容
                dicFileds.Add("Feedback_by", txt_cb_Callback_by.Tag.ToString());  //反馈人员名称
                dicFileds.Add("Feedback_phone", txt_cb_Callback_by_phone.Caption);   //被反馈人电话
                //dicFileds.Add("status", "1");   //数据状态
                dicFileds.Add("handle_name", txt_handle_name.Tag.ToString());    //经办人
                dicFileds.Add("handle_org", txt_cb_handle_org.Caption);    //经办人部门

                var result = false;
                try
                {
                    result = DBHelper.Submit_AddOrEdit("保存客户反馈", "tb_CustomerSer_Feedback", "Feedback_id", FeedbackId, dicFileds);
                }
                catch (Exception ex)
                {
                    result = false;
                }
                MessageBoxEx.Show(result ? "保存成功!" : "保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                if (result)
                {
                    UCFeedbakcManager.BindPageData();
                    deleteMenuByTag(Tag.ToString(), UCFeedbakcManager.Name);
                }
            };
            #endregion
            #endregion
        }
        private Boolean CheckValue()    //必填字段非空验证
        {
            if (String.IsNullOrEmpty(txt_cust_code.Text.Trim()))
            {
                MessageBoxEx.Show("客户编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txt_cust_code.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txt_cb_title.Caption.Trim()))
            {
                MessageBoxEx.Show("反馈标题不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txt_cb_title.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cbo_cb_Callback_mode.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("反馈形式不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                cbo_cb_Callback_mode.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cbo_cb_Callback_type.Text.Trim()))
            {
                MessageBoxEx.Show("反馈类型不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                cbo_cb_Callback_type.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txt_handle_name.Text.Trim()))
            {
                MessageBoxEx.Show("经办人不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txt_handle_name.Focus();
                return false;
            }
            //if (String.IsNullOrEmpty(txt_cb_handle_org.Caption.Trim()))
            //{
            //    MessageBoxEx.Show("部门不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            //    txt_cb_handle_org.Focus();
            //    return false;
            //}
            return true;
        }
        private void SetCustInfo()  //设置客户信息
        {
            if (String.IsNullOrEmpty(CustId)) return;
            var custInfo = DBHelper.GetTable("查询客户信息", "tb_customer", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cust_phone")), "cust_id = '" + CustId + "'", "",
                "");
            if (custInfo != null && custInfo.DefaultView.Count != 0)
            {
                txt_cust_code.Text = custInfo.DefaultView[0]["cust_code"].ToString();
                txt_cust_code.Tag = CustId;
                txt_cust_name.Text = custInfo.DefaultView[0]["cust_name"].ToString();
                txt_legal_person.Caption = custInfo.DefaultView[0]["legal_person"].ToString();
                cbo_cust_type.SelectedValue = custInfo.DefaultView[0]["cust_type"].ToString();
                txt_cust_short_name.Caption = custInfo.DefaultView[0]["cust_short_name"].ToString();
                txt_cust_quick_code.Caption = custInfo.DefaultView[0]["cust_quick_code"].ToString();
                cbo_province.Text = custInfo.DefaultView[0]["province"].ToString();
                cbo_city.Text = custInfo.DefaultView[0]["city"].ToString();
                txt_cust_fax.Caption = custInfo.DefaultView[0]["cust_fax"].ToString();
                cbo_county.Text = custInfo.DefaultView[0]["county"].ToString();
                txt_cust_address.Caption = custInfo.DefaultView[0]["cust_address"].ToString();
                txt_cust_phone.Caption = custInfo.DefaultView[0]["phone"].ToString();
                txt_cust_email.Caption = custInfo.DefaultView[0]["cust_email"].ToString();
                chk_is_member.Checked = custInfo.DefaultView[0]["is_member"].ToString() == "1";
                txt_member_number.Caption = custInfo.DefaultView[0]["member_number"].ToString();
                cbo_member_class.Text = custInfo.DefaultView[0]["member_class"].ToString();

                if (custInfo.DefaultView[0]["member_period_validity"] != DBNull.Value)
                {
                    var ticks = (long)custInfo.DefaultView[0]["member_period_validity"];
                    dtp_member_period_validity.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
        }
        private void SetContInfo() //设置反馈人信息
        {
            var custInfo = DBHelper.GetTable("查询联系人信息", "tb_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), "cont_id = '" + ContId + "'", "", "");
            if (custInfo != null && custInfo.DefaultView.Count != 0)
            {
                txt_cb_Callback_by.Text = custInfo.DefaultView[0]["cont_name"].ToString();
                txt_cb_Callback_by.Tag = ContId;
                txt_cb_Callback_by_phone.Caption = custInfo.DefaultView[0]["phone"].ToString();
            }
        }
        private void SetFeedbackInfo()  //设置反馈信息
        {
            var feedbackInfo = DBHelper.GetTable("查询反馈信息", "tb_CustomerSer_Feedback", "*", "Feedback_id = '" + FeedbackId + "'", "", "");
            if (feedbackInfo != null && feedbackInfo.DefaultView.Count != 0)
            {
                CustId = feedbackInfo.DefaultView[0]["corp_id"].ToString();
                ContId = feedbackInfo.DefaultView[0]["Feedback_by"].ToString();
                txt_cb_title.Caption = feedbackInfo.DefaultView[0]["title"].ToString();
                rtx_cb_record.Text = feedbackInfo.DefaultView[0]["record"].ToString();
                HandleId = feedbackInfo.DefaultView[0]["handle_name"].ToString();
                txt_cb_handle_org.Caption = CommonCtrl.IsNullToString(feedbackInfo.DefaultView[0]["handle_org"]);
                SetHandleInfo();
            }
        }
        private void SetHandleInfo() //设置经办人信息
        {
            var custInfo = DBHelper.GetTable("查询经办人信息", "sys_user", "*", "user_id = '" + HandleId + "'", "", "");
            if (custInfo != null && custInfo.DefaultView.Count != 0)
            {
                txt_handle_name.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["user_name"]);
                txt_handle_name.Tag = ContId;
            }
        }
        #endregion

        #region Event -- 事件
        private void UCCallBackAddOrEdit_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbo_province.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(cbo_city, cbo_province.SelectedValue.ToString(), "请选择");
                CommonFuncCall.BindCountryComBox(cbo_county, cbo_city.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCityComBox(cbo_city, "", "请选择");
                CommonFuncCall.BindCountryComBox(cbo_county, "", "请选择");
            }
        }

        private void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cbo_city.SelectedValue)))
            {
                CommonFuncCall.BindCountryComBox(cbo_county, cbo_city.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(cbo_county, "", "请选择");
            }
        }
        #endregion
    }
}
