using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using ClientBLL;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.CustomerService.Member
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     UCCallBackAddOrEdit         
    /// Author:       Kord
    /// Date:         2014/10/22 17:55:09
    /// Machine Name: KORD-PC
    ///***************************************************************************//
    /// Function: 
    /// 	UCCallBackAddOrEdit
    ///***************************************************************************//
    public partial class UCMemberAddOrEdit : UCBase
    {
        #region Constructor -- 构造函数
        public UCMemberAddOrEdit()
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
            CommonCtrl.BindComboBoxByDictionarr(cbo_member_grade, "sys_member_grade", false);    //绑定会员等级
            #endregion

            #region 初始化界面控件值

            if (windowStatus == WindowStatus.Add)
            {
                UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>
                {
                    btnSave, btnExport,btnSet, btnView,btnPrint
                });
                txt_vip_code.Caption = CommonUtility.GetNewNo(DataSources.EnumProjectType.CustomerSer_member);
                dtp_validity_time.Value = DBHelper.GetCurrentTime().AddDays(1);
            }
            if (windowStatus == WindowStatus.Edit)
            {
                UIAssistants.SetUCBaseFuncationVisible(this, new ObservableCollection<ButtonEx_sms>
                {
                    btnExport,btnSet, btnView,btnPrint
                });
                rtx_remark.Text = SelectedRow["remark"].ToString();
                SetCustInfo();
                if (!String.IsNullOrEmpty(VipId))
                    SetMemberInfo();
            }
            if (windowStatus == WindowStatus.View)
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
            #region 选择客户信息
            txt_cust_name.ChooserClick += delegate
            {
                var frmCustomer = new frmCustomerInfo();
                var result = frmCustomer.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CustId = frmCustomer.strCustomerId;
                }
                SetCustInfo();
            };
            #endregion

            #region 保存数据
            SaveEvent += delegate
            {
                var check = CheckValue();
                if (!check) return;
                var dicFileds = new Dictionary<String, String>();
                var dicField4Cust = new Dictionary<String, String>();

                if (windowStatus == WindowStatus.Add)
                {
                    dicFileds.Add("vip_id", Guid.NewGuid().ToString());
                }
                dicFileds.Add("corp_id", CustId);
                dicFileds.Add("vip_code", txt_vip_code.Caption);
                dicFileds.Add("member_grade", cbo_member_grade.SelectedValue.ToString());
                dicFileds.Add("remark", rtx_remark.Text);
                dicFileds.Add("validity_time", dtp_validity_time.Value.Ticks.ToString());
                if (windowStatus == WindowStatus.Add)
                {
                    dicFileds.Add("create_by", GlobalStaticObj.CurrUserCom_Code);
                    dicFileds.Add("create_time", DBHelper.GetCurrentTime().Ticks.ToString());
                    dicFileds.Add("update_by", GlobalStaticObj.CurrUserCom_Code);
                    dicFileds.Add("update_time", DBHelper.GetCurrentTime().Ticks.ToString());
                }
                else if (windowStatus == WindowStatus.Edit)
                {
                    dicFileds.Add("update_by", GlobalStaticObj.CurrUserCom_Code);
                    dicFileds.Add("update_time", DBHelper.GetCurrentTime().Ticks.ToString());
                }
                dicFileds.Add("status", "1");

                CustId = txt_cust_name.Tag.ToString();
                dicField4Cust.Add("is_member", "1");
                dicField4Cust.Add("member_number", txt_vip_code.Caption);
                dicField4Cust.Add("member_class", cbo_member_grade.SelectedValue.ToString());
                dicField4Cust.Add("member_period_validity", dtp_validity_time.Value.Ticks.ToString());
                dicField4Cust.Add("update_by", GlobalStaticObj.CurrUserCom_Code);
                dicField4Cust.Add("update_time", DBHelper.GetCurrentTime().Ticks.ToString());

                Boolean result, result4Cust;
                try
                {
                    result = DBHelper.Submit_AddOrEdit("保存会员信息", "tb_CustomerSer_Member", "vip_id", VipId, dicFileds);
                    result4Cust = DBHelper.Submit_AddOrEdit("更新客户档案", "tb_customer", "cust_id", CustId, dicField4Cust);
                }
                catch (Exception ex)
                {
                    result = false;
                    result4Cust = false;
                }
                MessageBoxEx.Show(result && result4Cust ? "保存成功!" : "保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                if (result && result4Cust)
                {
                    UCMemberManager.BindPageData();
                    deleteMenuByTag(Tag.ToString(), UCMemberManager.Name);
                }
            };
            #endregion
            #endregion
        }
        private Boolean CheckValue()    //必填字段非空验证
        {
            if (dtp_validity_time.Value < DBHelper.GetCurrentTime().AddDays(1))
            {
                MessageBoxEx.Show("会员有效期为无效日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                dtp_validity_time.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txt_vip_code.Caption.Trim()))
            {
                MessageBoxEx.Show("会员编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txt_vip_code.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(cbo_member_grade.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("会员等级不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                cbo_member_grade.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txt_cust_name.Text.Trim()))
            {
                MessageBoxEx.Show("客户信息不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txt_cust_name.Focus();
                return false;
            }
            return true;
        }
        private void SetCustInfo()  //设置客户信息
        {
            var custInfo = DBHelper.GetTable("查询客户信息", "tb_customer", "*", "cust_id = '" + CustId + "'", "",
                "");
            if (custInfo != null && custInfo.DefaultView.Count != 0)
            {
                txt_cust_name.Tag = custInfo.DefaultView[0]["cust_id"].ToString();
                txt_cust_name.Text = custInfo.DefaultView[0]["cust_name"].ToString();
                txt_legal_person.Caption = custInfo.DefaultView[0]["legal_person"].ToString();
                //txt_cust_job.Caption = custInfo.DefaultView[0]["cust_quick_code"].ToString(); //unknow
                txt_cust_phone.Caption = custInfo.DefaultView[0]["cust_phone"].ToString();
                txt_cust_address.Caption = custInfo.DefaultView[0]["cust_address"].ToString();
                txt_cust_tel.Caption = custInfo.DefaultView[0]["cust_tel"].ToString();
            }
            else
            {
                CustId = "";
            }
        }
        private void SetMemberInfo() //设置会员信息
        {
            if (SelectedRow != null)
            {
                txt_vip_code.Caption = SelectedRow["vip_code"].ToString();
            
                var custInfo = DBHelper.GetTable("查询会员信息", "tb_CustomerSer_member", "*", "vip_id = '" + SelectedRow["vip_id"] + "'", "", "");
                if (custInfo != null && custInfo.DefaultView.Count != 0)
                {
                    cbo_member_grade.SelectedValue = custInfo.DefaultView[0]["member_grade"].ToString();
                    dtp_validity_time.Value = Convert.ToDateTime(Common.UtcLongToLocalDateTime(SelectedRow["validity_time"].ToString()));
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
