using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.SysManage.Company
{
    /// <summary>
    /// 公司档案 编辑复制新增
    /// 孙明生
    /// 修改人:杨天帅
    /// </summary>
    public partial class UCCompanyAddOrEdit : UCBase
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        Control errContrl;
        #endregion

        #region --成员变量
        private string parentName = string.Empty;
        private DataRow dr;
        //private WindowStatus winStatus;
        #endregion

        #region --构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="winStatus">窗体状态</param>
        /// <param name="dr">数据行</param>
        /// <param name="parentName">父窗体名称</param>
        public UCCompanyAddOrEdit(WindowStatus winStatus, DataRow dr, string parentName)
        {
            InitializeComponent();

            this.windowStatus = winStatus;
            this.dr = dr;
            this.parentName = parentName;

            base.SaveEvent += new ClickHandler(UCCompanyAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCCompanyAddOrEdit_CancelEvent);
            this.txtremark.InnerTextBox.Multiline = true;
            this.txtremark.Height *= 2;
            txtcom_name.InnerTextBox.TextChanged += new EventHandler(InnerTextBox_TextChanged);
            UCCompanyAddOrEdit_Load(null, null);
        }


        #endregion

        #region --窗体初始化
        private void UCCompanyAddOrEdit_Load(object sender, EventArgs e)
        {
            // base.RoleButtonStstus(uc.Name);//角色按钮权限-是否隐藏
            //base.SetBtnStatus(winStatus);
            SetBtnStatus();
            CommonCtrl.BindComboBoxByDictionarr(cborepair_qualification, "sys_repair_qualification", true);//维修资质
            CommonCtrl.BindComboBoxByDictionarr(cbounit_properties, "sys_enterprise_property", true);//单位性质
            CommonFuncCall.BindProviceComBox(cboprovince, "请选择");
            if (this.dr != null)
            {
                txtcom_code.Caption = this.dr["com_code"].ToString();//公司编码
                txtcom_name.Caption = this.dr["com_name"].ToString();//公司全名
                txtcom_short_name.Caption = this.dr["com_short_name"].ToString();//公司简称
                cborepair_qualification.SelectedValue = this.dr["repair_qualification"].ToString();//维修资质
                cbounit_properties.SelectedValue = this.dr["unit_properties"].ToString();//单位性质
                txtlegal_person.Caption = this.dr["legal_person"].ToString();//法人负责人
                txtcertificate_code.Caption = this.dr["certificate_code"].ToString();//组织机构代码
                txtzip_code.Caption = this.dr["zip_code"].ToString();//邮编
                cboprovince.SelectedValue = this.dr["province"].ToString();//省份
                cbocity.SelectedValue = this.dr["city"].ToString();//城市
                cbocounty.SelectedValue = this.dr["county"].ToString();//区县
                txtcom_address.Caption = this.dr["com_address"].ToString();//详细地址
                txtcom_tel.Caption = this.dr["com_tel"].ToString();//固话
                txtcom_phone.Caption = this.dr["com_phone"].ToString();//手机
                txtcom_email.Caption = this.dr["com_email"].ToString();//电子邮件
                txtcom_fax.Caption = this.dr["com_fax"].ToString();//传真
                txttax_account.Caption = this.dr["tax_account"].ToString();//税号
                txttax_qualification.Caption = this.dr["tax_qualification"].ToString();//纳税人资格
                ckbindepen_check.Checked = this.dr["indepen_check"].ToString() == "1" ? true : false;//独立核算  0 为否  1为是
                ckbindepen_legalperson.Checked = this.dr["indepen_legalperson"].ToString() == "1" ? true : false;//独立法人  0 为否  1为是
                ckbfinancial_indepen.Checked = this.dr["financial_indepen"].ToString() == "1" ? true : false;// 财务独立 0 为否  1为是  
                txtremark.Caption = this.dr["remark"].ToString();//备注
                if (!string.IsNullOrEmpty(this.dr["work_time"].ToString()))
                {
                    long time = 0;
                    if (long.TryParse(this.dr["work_time"].ToString(), out time))
                    {
                        dtpwork_time.Value = Common.UtcLongToLocalDateTime(time);//上班时间
                    }
                }
                txtser_car_num.Caption = this.dr["ser_car_num"].ToString();//服务车数
                chkis_repair_newenergy.Checked = this.dr["is_repair_newenergy"].ToString() == "1" ? true : false;// 是否维修新能源  0 为否  1为是
                chkis_repair_ng.Checked = this.dr["is_repair_ng"].ToString() == "1" ? true : false;// 是否维修NG车 0 为否  1为是 
                txtstaff_counts.Caption = this.dr["staff_counts"].ToString();//人员总数
                txtser_staff_counts.Caption = this.dr["ser_staff_counts"].ToString();//服务人员数
                txtmach_repair_staff_counts.Caption = this.dr["mach_repair_staff_counts"].ToString();//机器人数
                txtholder_electrician_counts.Caption = this.dr["holder_electrician_counts"].ToString();//持证电工数
                txttrench_counts.Caption = this.dr["trench_counts"].ToString();//地沟\举升机数
                txttwelve_trench_counts.Caption = this.dr["twelve_trench_counts"].ToString();//标准地沟\举升机数
                txtfour_location_counts.Caption = this.dr["four_location_counts"].ToString();//四轮定位仪数
                txtengine_test_counts.Caption = this.dr["engine_test_counts"].ToString();//发动机检测仪数

                txtfactory_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["factory_area"].ToString(), 2);//厂区占地面积
                txtparking_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["parking_area"].ToString(), 2);//停车区面积
                txtreception_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["reception_area"].ToString(), 2);//接待室面积
                txtcust_lounge_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["cust_lounge_area"].ToString(), 2);//客户休息室面积
                txtcust_toilet_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["cust_toilet_area"].ToString(), 2);//客户洗手间面积
                txtmeeting_room_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["meeting_room_area"].ToString(), 2);//会议室面积
                txttraining_room_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["training_room_area"].ToString(), 2);//培训室面积
                txtsettlement_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["settlement_area"].ToString(), 2);//结算区面积

                txtrepaired_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["repaired_area"].ToString(), 2);//待修区面积
                txtcheck_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["check_area"].ToString(), 2);//检查区面积
                txtrepair_workshop_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["repair_workshop_area"].ToString(), 2);//维修车间面积
                txtbig_repaired_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["big_repaired_area"].ToString(), 2);//总成大修面积
                txtparts_sales_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["parts_sales_area"].ToString(), 2);//配件销售面积
                txtparts_warehouse_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["parts_warehouse_area"].ToString(), 2);//配件仓库面积
                txtoldparts_warehouse_area.Caption = CommonUtility.DecimalRightZeroFill(this.dr["oldparts_warehouse_area"].ToString(), 2);//旧件仓库面积
                txtrepair_instructions.Caption = this.dr["repair_instructions"].ToString();//维修说明
            }
        }

        //设置按钮状态
        private void SetBtnStatus()
        {
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetSysManageAddBtn();
            }
            if (windowStatus == WindowStatus.Edit)
            {
                SetSysManageEditBtn();
            }
        }
        #endregion

        #region 取消事件
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCCompanyAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }
        #endregion

        #region 控件内容验证
        /// <summary>
        /// 控件内容验证
        /// </summary>
        /// <param name="msg">返回提示信息</param>
        /// <returns></returns>
        private bool Validator(ref string msg)
        {
            if (string.IsNullOrEmpty(txtcom_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_name, "公司全名不能为空!");

                errContrl = txtcom_name;
                return false;
            }
            if (cborepair_qualification.SelectedIndex == 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cborepair_qualification, "请选择维修资质!");
                errContrl = cborepair_qualification;
                return false;
            }
            if (cbounit_properties.SelectedIndex == 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cbounit_properties, "请选择单位性质!");

                errContrl = cbounit_properties;
                return false;
            }
            if (!string.IsNullOrEmpty(txtzip_code.Caption.Trim()) && !Utility.Common.Validator.IsPostCode(txtzip_code.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtzip_code, "邮政编码格式不正确!");

                return false;
            }
            if (!string.IsNullOrEmpty(txtcom_email.Caption.Trim()) && !Utility.Common.Validator.IsEmail(txtcom_email.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_email, "电子邮件格式不正确!");
                return false;
            }
            if (cboprovince.SelectedIndex == 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cboprovince, "请选择省份!");

                errContrl = cboprovince;
                return false;
            }
            if (cbocity.SelectedIndex == 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cbocity, "请选择城市!");
                errContrl = cbocity;
                return false;
            }
            if (cbocounty.Items.Count > 1 && cbocounty.SelectedIndex == 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cbocounty, "请选择区县!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtcom_tel.Caption.Trim()) && !Utility.Common.Validator.IsTel(txtcom_tel.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_tel, "固话格式不正确!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtcom_phone.Caption.Trim()) && !Utility.Common.Validator.IsMobile(txtcom_phone.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_phone, "手机格式不正确!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtcom_fax.Caption.Trim()) && !Utility.Common.Validator.IsTel(txtcom_fax.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_fax, "传真格式不正确!");
                return false;
            }


            if (!Utility.Common.Validator.IsInt(txtser_car_num.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtcom_fax, "服务车数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtstaff_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtstaff_counts, "人员总数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtser_staff_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtser_staff_counts, "服务人员数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtmach_repair_staff_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtmach_repair_staff_counts, "机器人数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtholder_electrician_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtholder_electrician_counts, "持证电工数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txttrench_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txttrench_counts, "地沟/举升机数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txttwelve_trench_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txttwelve_trench_counts, "标准地沟|举升机数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtfour_location_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtfour_location_counts, "四轮定位仪数格式不正确，请输入数字!");
                return false;
            }
            if (!Utility.Common.Validator.IsInt(txtengine_test_counts.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtengine_test_counts, "发动机检测仪数格式不正确，请输入数字!");
                return false;
            }
            return true;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCCompanyAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();

                #region 验证
                string msg = "";
                bool bln = Validator(ref msg);
                if (!bln)
                {
                    return;
                }
                string factory = Utility.Common.Validator.IsDecimal(txtfactory_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtfactory_area, "厂区占地面积格式不正确，例：10.15");
                    return;
                }
                string parking = Utility.Common.Validator.IsDecimal(txtparking_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtparking_area, "停车区面积格式不正确，例：10.15");
                    return;
                }
                string reception = Utility.Common.Validator.IsDecimal(txtreception_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtreception_area, "接待室面积格式不正确，例：10.15");
                    return;
                }
                string cust_lounge = Utility.Common.Validator.IsDecimal(txtcust_lounge_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtcust_lounge_area, "客户休息室面积格式不正确，例：10.15");
                    return;
                }
                string cust_toilet = Utility.Common.Validator.IsDecimal(txtcust_toilet_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtcust_toilet_area, "客户洗手间面积格式不正确，例：10.15");
                    return;
                }
                string meeting_room = Utility.Common.Validator.IsDecimal(txtmeeting_room_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtmeeting_room_area, "会议室面积格式不正确，例：10.15");
                    return;
                }
                string training_room = Utility.Common.Validator.IsDecimal(txttraining_room_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txttraining_room_area, "培训室面积格式不正确，例：10.15");
                    return;
                }
                string settlement = Utility.Common.Validator.IsDecimal(txtsettlement_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtsettlement_area, "结算区面积格式不正确，例：10.15");
                    return;
                }
                string repaired = Utility.Common.Validator.IsDecimal(txtrepaired_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtrepaired_area, "待修区面积格式不正确，例：10.15");
                    return;
                }
                string check = Utility.Common.Validator.IsDecimal(txtcheck_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtcheck_area, "检查区面积格式不正确，例：10.15");
                    return;
                }
                string repair_workshop = Utility.Common.Validator.IsDecimal(txtrepair_workshop_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtrepair_workshop_area, "维修车间面积格式不正确，例：10.15");
                    return;
                }
                string big_repaired = Utility.Common.Validator.IsDecimal(txtbig_repaired_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtbig_repaired_area, "总成大修面积格式不正确，例：10.15");
                    return;
                }
                string parts_sales = Utility.Common.Validator.IsDecimal(txtparts_sales_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtparts_sales_area, "配件销售面积格式不正确，例：10.15");
                    return;
                }
                string parts_warehouse = Utility.Common.Validator.IsDecimal(txtparts_warehouse_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtparts_warehouse_area, "配件仓库面积格式不正确，例：10.15");
                    return;
                }
                string oldparts_warehouse = Utility.Common.Validator.IsDecimal(txtoldparts_warehouse_area.Caption.Trim(), 10, 2, ref bln);
                if (!bln)
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtoldparts_warehouse_area, "旧件仓库面积格式不正确，例：10.15");
                    return;
                }
                #endregion

                string strCom_code = string.Empty;
                string keyName = string.Empty;
                string keyValue = string.Empty;
                string opName = "新增公司档案";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();

                #region
                dicFileds.Add("com_name", txtcom_name.Caption.Trim());//公司全名
                dicFileds.Add("com_short_name", txtcom_short_name.Caption.Trim());//公司简称
                dicFileds.Add("repair_qualification", cborepair_qualification.SelectedValue.ToString());//维修资质
                dicFileds.Add("unit_properties", cbounit_properties.SelectedValue.ToString());//单位性质
                dicFileds.Add("legal_person", txtlegal_person.Caption.Trim());//法人负责人
                dicFileds.Add("certificate_code", txtcertificate_code.Caption.Trim());//组织机构代码
                dicFileds.Add("zip_code", txtzip_code.Caption.Trim());//邮编
                dicFileds.Add("province", cboprovince.SelectedValue.ToString());//省份
                dicFileds.Add("city", cbocity.SelectedValue.ToString());//城市
                dicFileds.Add("county", cbocounty.SelectedValue.ToString());//区县
                dicFileds.Add("com_address", txtcom_address.Caption.Trim());//详细地址
                dicFileds.Add("com_tel", txtcom_tel.Caption.Trim());//固话
                dicFileds.Add("com_phone", txtcom_phone.Caption.Trim());//手机
                dicFileds.Add("com_email", txtcom_email.Caption.Trim());//电子邮件
                dicFileds.Add("com_fax", txtcom_fax.Caption.Trim());//传真
                dicFileds.Add("tax_account", txttax_account.Caption.Trim());//税号

                dicFileds.Add("tax_qualification", txttax_qualification.Caption.Trim());//纳税人资格

                dicFileds.Add("indepen_check", ckbindepen_check.Checked ? "1" : "0"); //独立核算  0 为否  1为是
                dicFileds.Add("indepen_legalperson", ckbindepen_legalperson.Checked ? "1" : "0"); //独立法人  0 为否  1为是
                dicFileds.Add("financial_indepen", ckbfinancial_indepen.Checked ? "1" : "0"); // 财务独立 0 为否  1为是   
                dicFileds.Add("is_total_company", chkis_total_company.Checked ? "1" : "0");//是否总公司
                dicFileds.Add("remark", txtremark.Caption.Trim());//备注
                #endregion

                #region 下面维修资质页签内容字段
                dicFileds.Add("work_time", Common.LocalDateTimeToUtcLong(dtpwork_time.Value).ToString());//上班时间
                dicFileds.Add("ser_car_num", txtser_car_num.Caption.Trim());//服务车数
                dicFileds.Add("is_repair_newenergy", chkis_repair_newenergy.Checked ? "1" : "0"); //是否维修新能源  0 为否  1为是
                dicFileds.Add("is_repair_ng", chkis_repair_ng.Checked ? "1" : "0"); // 是否维修NG车 0 为否  1为是   
                dicFileds.Add("staff_counts", txtstaff_counts.Caption.Trim());//人员总数
                dicFileds.Add("ser_staff_counts", txtser_staff_counts.Caption.Trim());//服务人员数
                dicFileds.Add("mach_repair_staff_counts", txtmach_repair_staff_counts.Caption.Trim());//机器人数
                dicFileds.Add("holder_electrician_counts", txtholder_electrician_counts.Caption.Trim());//持证电工数
                dicFileds.Add("trench_counts", txttrench_counts.Caption.Trim());//地沟\举升机数
                dicFileds.Add("twelve_trench_counts", txttwelve_trench_counts.Caption.Trim());//标准地沟\举升机数
                dicFileds.Add("four_location_counts", txtfour_location_counts.Caption.Trim());//四轮定位仪数
                dicFileds.Add("engine_test_counts", txtengine_test_counts.Caption.Trim());//发动机检测仪数

                dicFileds.Add("factory_area", factory);//厂区占地面积
                dicFileds.Add("parking_area", parking);//停车区面积
                dicFileds.Add("reception_area", reception);//接待室面积
                dicFileds.Add("cust_lounge_area", cust_lounge);//客户休息室面积
                dicFileds.Add("cust_toilet_area", cust_toilet);//客户洗手间面积
                dicFileds.Add("meeting_room_area", meeting_room);//会议室面积
                dicFileds.Add("training_room_area", training_room);//培训室面积
                dicFileds.Add("settlement_area", settlement);//结算区面积
                dicFileds.Add("repaired_area", repaired);//待修区面积
                dicFileds.Add("check_area", check);//检查区面积
                dicFileds.Add("repair_workshop_area", repair_workshop);//维修车间面积
                dicFileds.Add("big_repaired_area", big_repaired);//总成大修面积
                dicFileds.Add("parts_sales_area", parts_sales);//配件销售面积
                dicFileds.Add("parts_warehouse_area", parts_warehouse);//配件仓库面积
                dicFileds.Add("oldparts_warehouse_area", oldparts_warehouse);//旧件仓库面积

                dicFileds.Add("repair_instructions", txtrepair_instructions.Caption.Trim());//维修说明
                #endregion


                string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();

                dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
                dicFileds.Add("update_time", nowUtcTicks);

                if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
                {
                    strCom_code = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Company);
                    dicFileds.Add("com_code", strCom_code);//公司编码                   

                    dicFileds.Add("com_id", Guid.NewGuid().ToString());//新ID
                    dicFileds.Add("parent_id", "-1");//
                    dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                    dicFileds.Add("create_time", nowUtcTicks);

                    dicFileds.Add("status", SYSModel.DataSources.EnumStatus.Start.ToString("d"));//启用
                    dicFileds.Add("data_source", SYSModel.DataSources.EnumDataSources.SELFBUILD.ToString("d"));//来源 自建

                    dicFileds.Add("enable_flag", SYSModel.DataSources.EnumEnableFlag.USING.ToString("d"));//1为未删除状态
                }
                else if (windowStatus == WindowStatus.Edit)
                {
                    keyName = "com_id";
                    keyValue = this.dr[keyName].ToString();
                    opName = "更新公司档案";
                }
                bln = DBHelper.Submit_AddOrEdit(opName, "tb_company", keyName, keyValue, dicFileds);
                if (bln)
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                    MessageBoxEx.Show("保存成功!", "保存", MessageBoxButtons.OK, MessageBoxIcon.None);
                    MessageProcessor.UpdateComOrgInfo();
                    deleteMenuByTag(this.Tag.ToString(), this.parentName);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败！" + ex.Message, "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            if (errContrl == this.cboprovince)
            {
                errContrl = null;
                errorProvider1.Clear();
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

            if (errContrl == this.cbocity)
            {
                errContrl = null;
                errorProvider1.Clear();
            }
        }


        #endregion

        #region 选择事件
        private void cborepair_qualification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errContrl == this.cborepair_qualification)
            {
                errContrl = null;
                errorProvider1.Clear();
            }

        }

        private void cbounit_properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errContrl == this.cbounit_properties)
            {
                errContrl = null;
                errorProvider1.Clear();
            }
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (errContrl == this.txtcom_name)
            {
                errContrl = null;
                errorProvider1.Clear();
            }
        }
        #endregion


    }
}
