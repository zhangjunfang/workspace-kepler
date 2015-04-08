using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.CustomerService.Member
{
    /// <summary>
    /// UCMemberViewDetail
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/18 15:14:57</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class UCMemberViewDetail : UCBase
    {
        #region Constructor -- 构造函数
        public UCMemberViewDetail()
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
        public Member.UCMemberManager UCMemberManager { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public WindowStatus OpType { get; set; }
        /// <summary>
        /// 父窗体选中的回访记录信息
        /// </summary>
        public DataRowView SelectedRow { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public String VipId { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public String CustId { get; set; }
        #endregion

        #region Method -- 方法
        //初始化
        private void Init()
        {

            #region 初始化下拉框数据绑定

            #endregion

            #region 初始化界面控件值
            if (OpType == WindowStatus.View)
            {
                UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>
                {
                    btnExport,btnSet, btnView,btnPrint
                });
                SetCustInfo();
                if (!String.IsNullOrEmpty(VipId))
                    SetMemberInfo();
                palQTop.Enabled = false;
            }
            #endregion

            #region 注册功能按钮事件

            #endregion
        }
        private void SetCustInfo()  //设置客户信息
        {
            var custInfo = DBHelper.GetTable("查询客户信息", "tb_customer", "*", "cust_id = '" + CustId + "'", "",
                "");
            if (custInfo != null && custInfo.DefaultView.Count != 0)
            {
                txt_cust_name.Tag = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["cust_id"]);
                txt_cust_name.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["cust_name"]);
                txt_legal_person.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["legal_person"]);
                txt_cust_phone.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["cust_phone"]);
                txt_cust_address.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["cust_address"]);
                txt_cust_tel.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["cust_tel"].ToString());
            }
        }
        private void SetMemberInfo() //设置会员信息
        {
            if (!String.IsNullOrEmpty(VipId))
            {
                var custInfo = DBHelper.GetTable("查询会员信息", "tb_CustomerSer_member", "*", "vip_id = '" + VipId + "'", "", "");
                if (custInfo != null && custInfo.DefaultView.Count != 0)
                {
                    txt_vip_code.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["vip_code"]);
                    cbo_member_grade.Text = LocalCache.GetDictNameById(CommonCtrl.IsNullToString(custInfo.DefaultView[0]["member_grade"]));
                    dtp_validity_time.Text = Common.UtcLongToLocalDateTime(CommonCtrl.IsNullToString(custInfo.DefaultView[0]["validity_time"]));
                    txt_integral.Text = CommonCtrl.IsNullToString(custInfo.DefaultView[0]["integral"]);
                }
            }
        }
        #endregion

        #region Event -- 事件
        private void UCCallBackAddOrEdit_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion
    }
}
