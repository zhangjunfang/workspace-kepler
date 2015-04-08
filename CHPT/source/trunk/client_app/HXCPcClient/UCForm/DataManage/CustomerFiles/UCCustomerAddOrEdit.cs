using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Model;
using SYSModel;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.CustomerFiles
{
    /// <summary>
    /// 客户档案 新增 编辑
    /// 孙明生
    /// </summary>
    public partial class UCCustomerAddOrEdit : UCBase
    {
        #region Constructor -- 构造函数
        public UCCustomerAddOrEdit()
        {
            InitializeComponent();
            SaveEvent += UCCustomerAddOrEdit_SaveEvent;
            SetFuncationButtonVisible();
        }
        #endregion

        #region Property -- 属性
        /// <summary>
        /// 角色id
        /// </summary>
        public string Id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCCustomerManager UCCustomerManager { get; set; }
        #endregion

        #region Method -- 方法
        public override bool CloseMenu()
        {
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return false;
            }
            return true;
        }
        private void InitDataGridCellFormatting()   //初始化数据表格
        {
            try
            {
                dgvVehicle.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
                {
                    try
                    {
                        UIAssistants.DgvCellDataConvert2DicData(dgvVehicle, args, "brand");
                        UIAssistants.DgvCellDataConvert2Table(dgvVehicle, args, "model", "tb_vehicle_models", "vm_id", "vm_name");
                    }
                    catch (Exception ex)
                    {
                        UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                    }
                };

                #region 车辆信息
                dgvVehicle.ReadOnly = false;
                dgvVehicle.Rows.Add(3);
                dgvVehicle.AllowUserToAddRows = true;
                tb_vehicle_license_plate.ReadOnly = true;
                tb_vehicle_remark.ReadOnly = true;
                tb_vehicle_v_brand.ReadOnly = true;
                tb_vehicle_v_id.ReadOnly = true;
                tb_vehicle_v_model.ReadOnly = true;
                tb_vehicle_vin.ReadOnly = true;

                dgvVehicle.CellClick += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    try
                    {
                        if (args.ColumnIndex < 0) return;
                        //if (dgvVehicle.Columns[args.ColumnIndex] != tb_vehicle_license_plate) return;
                        var chooser = new frmVehicleGrade();
                        var result = chooser.ShowDialog();
                        if (result != DialogResult.OK) return;
                        var strId = chooser.strLicensePlate;
                        var dpt = DBHelper.GetTable("查询车辆档案信息", "tb_vehicle", "*", " license_plate='" + strId + "'", "", "");
                        if (dpt.Rows.Count <= 0) return;
                        var dpr = dpt.Rows[0];
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_license_plate"].Value = CommonCtrl.IsNullToString(dpr["license_plate"]);
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_v_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand"]);
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_v_id"].Value = CommonCtrl.IsNullToString(dpr["v_id"]);
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_v_model"].Value = CommonCtrl.IsNullToString(dpr["v_model"]);
                        dgvVehicle.Rows[args.RowIndex].Cells["tb_vehicle_vin"].Value = CommonCtrl.IsNullToString(dpr["vin"]);
                    }
                    catch (Exception ex)
                    {
                        UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                    }
                };
                #endregion

                #region 联系人信息
                dgvcontacts.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
                {
                    try
                    {
                        //UIAssistants.DgvCellDataConvert2DicData(dgvcontacts, args, "cont_post");
                        UIAssistants.DgvCellDataConvert2Datetime(dgvcontacts, args, "cont_birthday");
                    }
                    catch (Exception ex)
                    {
                        UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                    }
                };

                dgvcontacts.ReadOnly = false;
                dgvcontacts.Rows.Add(3);
                dgvcontacts.AllowUserToAddRows = true;
                cont_name.ReadOnly = true;
                cont_post.ReadOnly = true;
                cont_phone.ReadOnly = true;
                cont_tel.ReadOnly = true;
                cont_birthday.ReadOnly = true;
                cont_email.ReadOnly = true;
                is_default.ReadOnly = false;
                remark.ReadOnly = true;

                dgvcontacts.CellContentClick += delegate(object sender, DataGridViewCellEventArgs e)
                {
                    try
                    {
                        if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                        {
                            //获取控件的值
                            for (int i = 0; i < dgvcontacts.Rows.Count; i++)
                            {
                                dgvcontacts.Rows[i].Cells["is_default"].Value = "0";
                            }
                            dgvcontacts.Rows[e.RowIndex].Cells["is_default"].Value = "1";
                        }
                    }
                    catch (Exception ex)
                    {
                        UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                    }
                };

                dgvcontacts.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
                {
                    try
                    {
                        if (args.ColumnIndex < 0 || args.RowIndex < 0) return;
                        if (dgvcontacts.Columns[args.ColumnIndex] != cont_name) return;
                        var frmPart = new frmContacts();
                        var result = frmPart.ShowDialog();
                        if (result != DialogResult.OK) return;
                        var strId = frmPart.contID;
                        var dpt = DBHelper.GetTable("查询联系人信息", "v_contacts", string.Format("*,{0} phone ,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), " cont_id='" + strId + "'", "", "");
                        if (dpt.Rows.Count <= 0) return;
                        var dpr = dpt.Rows[0];
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_id"].Value = CommonCtrl.IsNullToString(dpr["cont_id"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_name"].Value = CommonCtrl.IsNullToString(dpr["cont_name"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_post"].Value = CommonCtrl.IsNullToString(dpr["cont_post_name"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_phone"].Value = CommonCtrl.IsNullToString(dpr["phone"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_tel"].Value = CommonCtrl.IsNullToString(dpr["tel"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_birthday"].Value = CommonCtrl.IsNullToString(dpr["cont_birthday"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["cont_email"].Value = CommonCtrl.IsNullToString(dpr["cont_email"]);
                        dgvcontacts.Rows[args.RowIndex].Cells["is_default"].Value = "0";
                        dgvcontacts.Rows[args.RowIndex].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    }
                    catch (Exception ex)
                    {
                        UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                    }
                };
                #endregion
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
        }
        private void BindControls() //绑定控件值
        {
            try
            {
                if ((windowStatus != WindowStatus.Edit && windowStatus != WindowStatus.Copy) || Id == "") return;
                var dt = DBHelper.GetTable("查询客户档案", "tb_customer", "*", "cust_id='" + Id + "'", "", "");
                if (dt == null || dt.Rows.Count <= 0) return;
                var dr = dt.Rows[0];
                txtcust_code.Caption = dr["cust_code"].ToString();
                txtcust_name.Caption = dr["cust_name"].ToString();
                txtcust_tel.Caption = dr["cust_tel"].ToString();
                txtcust_short_name.Caption = dr["cust_short_name"].ToString();
                txtcust_fax.Caption = dr["cust_fax"].ToString();
                txtcust_address.Caption = dr["cust_address"].ToString();
                txtcust_email.Caption = dr["cust_email"].ToString();
                txtzip_code.Caption = dr["zip_code"].ToString();
                txtcust_website.Caption = dr["cust_website"].ToString();
                txttax_num.Caption = dr["tax_num"].ToString();
                cboprovince.SelectedValue = dr["province"].ToString();
                cbocity.SelectedValue = dr["city"].ToString();
                cbocounty.SelectedValue = dr["county"].ToString();
                cbocust_type.SelectedValue = dr["cust_type"].ToString();
                cboenterprise_nature.SelectedValue = dr["enterprise_nature"].ToString();
                cbocredit_rating.SelectedValue = dr["credit_rating"].ToString();
                cboprice_type.SelectedValue = dr["price_type"].ToString();

                cboopen_bank.SelectedValue = dr["open_bank"].ToString();//开户银行 码表

                txtcredit_line.Caption = dr["credit_line"].ToString();
                txtcredit_account_period.Caption = dr["credit_account_period"].ToString();
                txtbilling_name.Caption = dr["billing_name"].ToString();
                txtbilling_address.Caption = dr["billing_address"].ToString();
                txtbilling_account.Caption = dr["billing_account"].ToString();
                txtbank_account.Caption = dr["bank_account"].ToString();
                txtbank_account_person.Caption = dr["bank_account_person"].ToString();
                txtcust_remark.Caption = dr["cust_remark"].ToString();


                if (dr["is_member"].ToString() == "1")
                {
                    rdbis_member_y.Checked = true;
                    rdbis_member_n.Checked = false;
                }
                else
                {
                    rdbis_member_y.Checked = false;
                    rdbis_member_n.Checked = true;
                }
                txtmember_number.Caption = dr["member_number"].ToString();
                cbomember_class.SelectedValue = dr["member_class"].ToString();
                if (dr["member_period_validity"] != DBNull.Value)
                {
                    var lDate = Convert.ToInt64(dr["member_period_validity"]);
                    dtpmember_period_validity.Value = Common.UtcLongToLocalDateTime(lDate).ToString();
                }
                BindContacts();
                BindVehicle();
                ucAttr.BindAttachment();
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
        }
        private void BindContacts() //绑定联系人
        {
            #region 联系人数据

            //联系人数据                
            try
            {
                var dpt = DBHelper.GetTable("联系人数据", "(select tr.relation_object_id, tr.is_default tr_is_default, tb.*  from tb_contacts tb inner join tr_base_contacts tr on tb.cont_id = tr.cont_id) a", string.Format("*,{0}  phone,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")),
                        string.Format(" a.relation_object_id='{0}'", Id), "", "");
                if (dpt != null && dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgvcontacts.Rows.Count)
                    {
                        dgvcontacts.Rows.Add(dpt.Rows.Count + 1 - dgvcontacts.Rows.Count);
                    }
                    for (var i = 0; i < dpt.Rows.Count; i++)
                    {
                        var dpr = dpt.Rows[i];
                        dgvcontacts.Rows[i].Cells["cont_id"].Value = CommonCtrl.IsNullToString(dpr["cont_id"]);
                        dgvcontacts.Rows[i].Cells["cont_name"].Value = CommonCtrl.IsNullToString(dpr["cont_name"]);
                        dgvcontacts.Rows[i].Cells["cont_post"].Value = CommonCtrl.IsNullToString(dpr["cont_post"]);
                        dgvcontacts.Rows[i].Cells["cont_phone"].Value = CommonCtrl.IsNullToString(dpr["phone"]);
                        dgvcontacts.Rows[i].Cells["cont_tel"].Value = CommonCtrl.IsNullToString(dpr["tel"]);
                        dgvcontacts.Rows[i].Cells["cont_birthday"].Value = CommonCtrl.IsNullToString(dpr["cont_birthday"]);
                        dgvcontacts.Rows[i].Cells["cont_email"].Value = CommonCtrl.IsNullToString(dpr["cont_email"]);
                        dgvcontacts.Rows[i].Cells["is_default"].Value = CommonCtrl.IsNullToString(dpr["tr_is_default"]);
                        dgvcontacts.Rows[i].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    }
                }
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
            #endregion
        }
        private void BindVehicle()  //车辆信息
        {
            #region 车辆信息

            try
            {
                //车辆信息                
                var dpt = DBHelper.GetTable("车辆信息", "tb_vehicle", "*",
                    string.Format(" cust_id='{0}'", Id), "", "");
                if (dpt != null && dpt.Rows.Count > 0)
                {
                    if (dpt.Rows.Count > dgvVehicle.Rows.Count)
                    {
                        dgvVehicle.Rows.Add(dpt.Rows.Count + 1 - dgvVehicle.Rows.Count);
                    }
                    for (var i = 0; i < dpt.Rows.Count; i++)
                    {
                        var dpr = dpt.Rows[i];

                        dgvVehicle.Rows[i].Cells["tb_vehicle_license_plate"].Value = CommonCtrl.IsNullToString(dpr["license_plate"]);
                        dgvVehicle.Rows[i].Cells["tb_vehicle_remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvVehicle.Rows[i].Cells["tb_vehicle_v_brand"].Value = CommonCtrl.IsNullToString(dpr["v_brand"]);
                        dgvVehicle.Rows[i].Cells["tb_vehicle_v_id"].Value = CommonCtrl.IsNullToString(dpr["v_id"]);
                        dgvVehicle.Rows[i].Cells["tb_vehicle_v_model"].Value = CommonCtrl.IsNullToString(dpr["v_model"]);
                        dgvVehicle.Rows[i].Cells["tb_vehicle_vin"].Value = CommonCtrl.IsNullToString(dpr["vin"]);
                    }
                }
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
            #endregion
        }
        private Boolean CheckControlValue()
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtcust_name.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcust_name, "客户名称不能为空!");
                    return false;
                }
                if (cbocust_type.SelectedValue.ToString() == "")
                {
                    Validator.SetError(errorProvider1, cbocust_type, "请选择客户类别!");
                    return false;
                }
                if (cboenterprise_nature.SelectedValue.ToString() == "")
                {
                    Validator.SetError(errorProvider1, cboenterprise_nature, "请选择企业性质!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtcust_tel.Caption.Trim()) && !Validator.IsTel(txtcust_tel.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcust_tel, "电话格式不正确!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtcust_fax.Caption.Trim()) && !Validator.IsTel(txtcust_fax.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcust_fax, "传真格式不正确!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtcust_email.Caption.Trim()) && !Validator.IsEmail(txtcust_email.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcust_email, "邮箱格式不正确!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtzip_code.Caption.Trim()) && !Validator.IsPostCode(txtzip_code.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtzip_code, "邮码格式不正确!");
                    return false;
                }
                if (cbocredit_rating.SelectedValue.ToString() == "")
                {
                    Validator.SetError(errorProvider1, cbocredit_rating, "请选择信用等级!");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtcredit_line.Caption.Trim()) && !Validator.IsInt(txtcredit_line.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcredit_line, "信用额度格式不正确，请输入数字,如10000元");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtcredit_account_period.Caption.Trim()) && !Validator.IsInt(txtcredit_account_period.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtcredit_account_period, "信用账期格式不正确，请输入数字,如12个月");
                    return false;
                }
                if (!string.IsNullOrEmpty(txtbilling_account.Caption.Trim()) && !Validator.IsInt(txtbilling_account.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtbilling_account, "开票账号格式不正确!");
                    return false;
                }
                //if (!string.IsNullOrEmpty(txtbilling_account.Caption.Trim()) && !Validator.IsInt(txtbilling_account.Caption.Trim()))
                //{
                //    Validator.SetError(errorProvider1, txtbilling_account, "开票账号格式不正确!");
                //    return false;
                //}
                if (txtbank_account.Caption.Trim().Length > 0 && !ValidateUtil.IsBankCard(txtbank_account.Caption.Trim()))
                {
                    Validator.SetError(errorProvider1, txtbank_account, "银行账号格式有误！");
                    return false;
                }
                if (rdbis_member_y.Checked)
                {
                    if (cbomember_class.SelectedValue.ToString() == "")
                    {
                        Validator.SetError(errorProvider1, cbomember_class, "请选择会员等级!");
                        return false;
                    }
                    if (string.IsNullOrEmpty(dtpmember_period_validity.Value.Trim()))
                    {
                        Validator.SetError(errorProvider1, dtpmember_period_validity, "有效期不能为空!");
                        return false;
                    }
                    if (!Validator.IsDateTime(dtpmember_period_validity.Value.Trim()))
                    {
                        Validator.SetError(errorProvider1, dtpmember_period_validity, "有效期格式不正确!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置功能按钮可见性
        /// </summary>
        private void SetFuncationButtonVisible()
        {
            try
            {
                var btnCols = new ObservableCollection<ButtonEx_sms>()
            {
                btnSave, btnImport, btnExport, btnSet, btnView, btnPrint
            };
                UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
        }
        private Tuple<SysSQLString, String> BuildCustomerSqlString()    //生成客户档案SQL
        {
            try
            {
                var sssCust = new SysSQLString { Param = new Dictionary<string, string>() };
                switch (windowStatus)
                {
                    case WindowStatus.Copy:
                    case WindowStatus.Add:
                        {
                            sssCust.sqlString =
                                "insert into tb_customer (cust_id,cust_code,cust_name,cust_short_name,cust_quick_code,cust_type,legal_person,province,city,county,cust_address,zip_code,cust_tel,cust_phone,cust_fax,cust_email,cust_website,enterprise_nature,tax_num,credit_rating,credit_line,credit_account_period,price_type,open_bank,bank_account,bank_account_person,billing_name,billing_address,billing_account,cust_remark,is_member,member_number,member_class,member_period_validity,accessories_discount,workhours_discount,status,enable_flag,yt_sap_code,yt_customer_manager,data_source,country,cust_relation,indepen_legalperson,market_segment,institution_code,com_constitution,registered_capital,vehicle_structure,agency,sap_code,business_scope,ent_qualification,cust_crm_guid,create_by,create_time) values (@cust_id,@cust_code,@cust_name,@cust_short_name,@cust_quick_code,@cust_type,@legal_person,@province,@city,@county,@cust_address,@zip_code,@cust_tel,@cust_phone,@cust_fax,@cust_email,@cust_website,@enterprise_nature,@tax_num,@credit_rating,@credit_line,@credit_account_period,@price_type,@open_bank,@bank_account,@bank_account_person,@billing_name,@billing_address,@billing_account,@cust_remark,@is_member,@member_number,@member_class,@member_period_validity,@accessories_discount,@workhours_discount,@status,@enable_flag,@yt_sap_code,@yt_customer_manager,@data_source,@country,@cust_relation,@indepen_legalperson,@market_segment,@institution_code,@com_constitution,@registered_capital,@vehicle_structure,@agency,@sap_code,@business_scope,@ent_qualification,@cust_crm_guid,@create_by,@create_time)";

                            var guid = Guid.NewGuid().ToString();
                            Id = guid;
                            sssCust.Param.Add("cust_id", guid);
                            sssCust.Param.Add("cust_code", CommonUtility.GetNewNo(DataSources.EnumProjectType.Customer));
                            sssCust.Param.Add("create_by", GlobalStaticObj.UserID);
                            sssCust.Param.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                            sssCust.Param.Add("is_member", rdbis_member_y.Checked ? "1" : "0");
                            sssCust.Param.Add("member_number", rdbis_member_y.Checked ? CommonUtility.GetNewNo(DataSources.EnumProjectType.CustomerSer_member) : "");
                            sssCust.Param.Add("member_class", CommonCtrl.IsNullToString(cbomember_class.SelectedValue));
                            var time = rdbis_member_y.Checked ? Convert.ToDateTime(dtpmember_period_validity.Value) : DateTime.Now;
                            sssCust.Param.Add("member_period_validity", rdbis_member_y.Checked ? Common.LocalDateTimeToUtcLong(time).ToString() : "");
                        }
                        break;
                    case WindowStatus.Edit:
                        sssCust.sqlString =
                            "update tb_customer set cust_id = @cust_id, cust_code = @cust_code, cust_name = @cust_name, cust_short_name = @cust_short_name, cust_quick_code = @cust_quick_code, cust_type = @cust_type, legal_person = @legal_person, province = @province, city = @city, county = @county, cust_address = @cust_address, zip_code = @zip_code, cust_tel = @cust_tel, cust_phone = @cust_phone, cust_fax = @cust_fax, cust_email = @cust_email, cust_website = @cust_website, enterprise_nature = @enterprise_nature, tax_num = @tax_num, credit_rating = @credit_rating, credit_line = @credit_line, credit_account_period = @credit_account_period, price_type = @price_type, open_bank = @open_bank, bank_account = @bank_account, bank_account_person = @bank_account_person, billing_name = @billing_name, billing_address = @billing_address, billing_account = @billing_account, cust_remark = @cust_remark, is_member = @is_member, member_number = @member_number, member_class = @member_class, member_period_validity = @member_period_validity, accessories_discount = @accessories_discount, workhours_discount = @workhours_discount, status = @status, enable_flag = @enable_flag, yt_sap_code = @yt_sap_code, yt_customer_manager = @yt_customer_manager, data_source = @data_source, country = @country, cust_relation = @cust_relation, indepen_legalperson = @indepen_legalperson, market_segment = @market_segment, institution_code = @institution_code, com_constitution = @com_constitution, registered_capital = @registered_capital, vehicle_structure = @vehicle_structure, agency = @agency, sap_code = @sap_code, business_scope = @business_scope, ent_qualification = @ent_qualification, cust_crm_guid = @cust_crm_guid, update_by = @update_by, update_time = @update_time where cust_id = '" + Id + "'";
                        sssCust.Param.Add("cust_id", Id);
                        sssCust.Param.Add("cust_code", txtcust_code.Caption.Trim());
                        sssCust.Param.Add("update_by", GlobalStaticObj.UserID);
                        sssCust.Param.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                        sssCust.Param.Add("is_member", rdbis_member_y.Checked ? "1" : "0");
                        sssCust.Param.Add("member_number", rdbis_member_y.Checked ? txtmember_number.Caption : "");
                        sssCust.Param.Add("member_class", rdbis_member_y.Checked ? CommonCtrl.IsNullToString(cbomember_class.SelectedValue) : "");
                        var time1 = rdbis_member_y.Checked ? Convert.ToDateTime(dtpmember_period_validity.Value) : DateTime.Now;
                        sssCust.Param.Add("member_period_validity", Common.LocalDateTimeToUtcLong(time1).ToString());
                        break;
                }
                sssCust.Param.Add("cust_name", txtcust_name.Caption.Trim());
                sssCust.Param.Add("cust_short_name", txtcust_short_name.Caption.Trim());
                sssCust.Param.Add("cust_quick_code", "");
                sssCust.Param.Add("cust_type", CommonCtrl.IsNullToString(cbocust_type.SelectedValue));
                sssCust.Param.Add("legal_person", "");
                sssCust.Param.Add("province", CommonCtrl.IsNullToString(cboprovince.SelectedValue));
                sssCust.Param.Add("city", CommonCtrl.IsNullToString(cbocity.SelectedValue));
                sssCust.Param.Add("county", CommonCtrl.IsNullToString(cbocounty.SelectedValue));
                sssCust.Param.Add("cust_address", txtcust_address.Caption.Trim());
                sssCust.Param.Add("zip_code", txtzip_code.Caption.Trim());
                sssCust.Param.Add("cust_tel", txtcust_tel.Caption.Trim());
                sssCust.Param.Add("cust_phone", "");
                sssCust.Param.Add("cust_fax", txtcust_fax.Caption.Trim());
                sssCust.Param.Add("cust_email", txtcust_email.Caption.Trim());
                sssCust.Param.Add("cust_website", txtcust_website.Caption.Trim());
                sssCust.Param.Add("enterprise_nature", CommonCtrl.IsNullToString(cboenterprise_nature.SelectedValue));
                sssCust.Param.Add("tax_num", txttax_num.Caption.Trim());
                sssCust.Param.Add("credit_rating", CommonCtrl.IsNullToString(cbocredit_rating.SelectedValue));
                sssCust.Param.Add("credit_line", txtcredit_line.Caption.Trim());
                sssCust.Param.Add("credit_account_period", txtcredit_account_period.Caption.Trim());
                sssCust.Param.Add("price_type", CommonCtrl.IsNullToString(cboprice_type.SelectedValue));
                sssCust.Param.Add("open_bank", CommonCtrl.IsNullToString(cboopen_bank.SelectedValue));
                sssCust.Param.Add("bank_account", txtbank_account.Caption.Trim());
                sssCust.Param.Add("bank_account_person", txtbank_account_person.Caption.Trim());
                sssCust.Param.Add("billing_name", txtbilling_name.Caption.Trim());
                sssCust.Param.Add("billing_address", txtbilling_address.Caption.Trim());
                sssCust.Param.Add("billing_account", txtbilling_account.Caption.Trim());
                sssCust.Param.Add("cust_remark", txtcust_remark.Caption.Trim());
                sssCust.Param.Add("accessories_discount", "");
                sssCust.Param.Add("workhours_discount", "");
                sssCust.Param.Add("status", "1");
                sssCust.Param.Add("enable_flag", "1");
                sssCust.Param.Add("yt_sap_code", "");
                sssCust.Param.Add("sap_code", "");
                sssCust.Param.Add("yt_customer_manager", "");
                sssCust.Param.Add("data_source", ((Int32)DataSources.EnumDataSources.SELFBUILD).ToString());
                sssCust.Param.Add("country", CommonCtrl.IsNullToString(cbocounty.SelectedValue));
                sssCust.Param.Add("cust_relation", "");
                sssCust.Param.Add("indepen_legalperson", "");
                sssCust.Param.Add("market_segment", "");
                sssCust.Param.Add("institution_code", "");
                sssCust.Param.Add("com_constitution", "");
                sssCust.Param.Add("registered_capital", "");
                sssCust.Param.Add("vehicle_structure", "");
                sssCust.Param.Add("agency", "");
                sssCust.Param.Add("business_scope", "");
                sssCust.Param.Add("ent_qualification", "");
                sssCust.Param.Add("cust_crm_guid", "");

                var rtVaule = new Tuple<SysSQLString, String>(sssCust, Id);
                return rtVaule;
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1,ex);
                return null;
            }
        }
        private IEnumerable<SysSQLString> BuildContactRelation(String custId)   //生成联系人与客户关系SQL
        {
            try
            {
                var sysSqlStrList = new List<SysSQLString>();
                if (String.IsNullOrEmpty(custId)) return sysSqlStrList;
                var sysSqlStrDelete = new SysSQLString
                {
                    sqlString = "delete from tr_base_contacts where relation_object_id = @relation_object_id",   //删除与客户档案相关的数据
                    Param = new Dictionary<string, string>()
                };
                sysSqlStrDelete.Param.Add("relation_object_id", custId);
                sysSqlStrList.Add(sysSqlStrDelete);
                foreach (DataGridViewRow row in dgvcontacts.Rows)
                {
                    if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(row.Cells["cont_id"].Value))) continue;
                    var sysSqlStrInsert = new SysSQLString
                    {
                        sqlString = "insert into tr_base_contacts (id ,cont_id ,relation_object ,relation_object_id ,is_default ) values (@id,@cont_id,@relation_object,@relation_object_id,@is_default)",
                        Param = new Dictionary<string, string>()
                    };
                    sysSqlStrInsert.Param.Add("id", Guid.NewGuid().ToString());
                    sysSqlStrInsert.Param.Add("cont_id", row.Cells["cont_id"].Value.ToString());
                    sysSqlStrInsert.Param.Add("relation_object", "客户档案");
                    sysSqlStrInsert.Param.Add("relation_object_id", custId);
                    sysSqlStrInsert.Param.Add("is_default", row.Cells["is_default"].Value.ToString());
                    sysSqlStrList.Add(sysSqlStrInsert);
                }
                return sysSqlStrList;
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                return null;
            }
        }
        private IEnumerable<SysSQLString> BuildVehicleRelation(String custId)   //生成车辆与客户关系SQL
        {
            try
            {
                var sysSqlStrList = new List<SysSQLString>();
                if (String.IsNullOrEmpty(custId)) return sysSqlStrList;
                foreach (DataGridViewRow row in dgvVehicle.Rows)
                {
                    if (String.IsNullOrEmpty(CommonCtrl.IsNullToString(row.Cells["tb_vehicle_v_id"].Value))) continue;
                    var sysSqlStrUpdate = new SysSQLString
                    {
                        sqlString = "update tb_vehicle set cust_id = @cust_id, update_time = @update_time, update_by = @update_by where v_id = @v_id"
                    };
                    sysSqlStrUpdate.Param = new Dictionary<string, string>();
                    sysSqlStrUpdate.Param.Add("cust_id", custId);
                    sysSqlStrUpdate.Param.Add("update_by", GlobalStaticObj.UserID);
                    sysSqlStrUpdate.Param.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                    sysSqlStrUpdate.Param.Add("v_id", row.Cells["tb_vehicle_v_id"].Value.ToString());
                    sysSqlStrList.Add(sysSqlStrUpdate);
                }
                return sysSqlStrList;
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                return null;
            }
        }
        private IEnumerable<SysSQLString> BuildVipMemberSqlString(String custId)    //生成会员信息SQL
        {
            try
            {
                var sysSqlStrList = new List<SysSQLString>();
                if (String.IsNullOrEmpty(custId)) return sysSqlStrList;
                var sssCust = new SysSQLString { Param = new Dictionary<string, string>() };
                switch (windowStatus)
                {
                    case WindowStatus.Copy:
                    case WindowStatus.Add:
                        {
                            sssCust.sqlString =
                                "insert into tb_CustomerSer_member (vip_id,corp_id,vip_code,member_grade,remark,validity_time,integral,status,enable_flag,create_by,create_time,update_by,update_time) values (@vip_id,@corp_id,@vip_code,@member_grade,@remark,@validity_time,@integral,@status,@enable_flag,@create_by,@create_time,@update_by,@update_time)";
                            sssCust.Param.Add("vip_id", Guid.NewGuid().ToString());
                            sssCust.Param.Add("vip_code", CommonUtility.GetNewNo(DataSources.EnumProjectType.CustomerSer_member));
                            sssCust.Param.Add("corp_id", custId);
                            sssCust.Param.Add("member_grade", CommonCtrl.IsNullToString(cbomember_class.SelectedValue));
                            sssCust.Param.Add("status", "1");
                            sssCust.Param.Add("remark", "");
                            sssCust.Param.Add("validity_time", String.IsNullOrEmpty(dtpmember_period_validity.Value) ? String.Empty : Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpmember_period_validity.Value)).ToString());
                            sssCust.Param.Add("integral", "0");
                            sssCust.Param.Add("enable_flag", "1");
                            sssCust.Param.Add("create_by", GlobalStaticObj.UserID);
                            sssCust.Param.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                            sssCust.Param.Add("update_by", GlobalStaticObj.UserID);
                            sssCust.Param.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                        }
                        break;
                    case WindowStatus.Edit:
                        sssCust.sqlString =
                                "update tb_CustomerSer_member set member_grade = @member_grade,remark = @remark,validity_time = @validity_time,update_by = @update_by,update_time = @update_time where vip_code = @vip_code";
                        sssCust.Param.Add("vip_code", txtmember_number.Caption);
                        sssCust.Param.Add("member_grade", CommonCtrl.IsNullToString(cbomember_class.SelectedValue));
                        sssCust.Param.Add("remark", "");
                        sssCust.Param.Add("validity_time", String.IsNullOrEmpty(dtpmember_period_validity.Value) ? String.Empty : Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpmember_period_validity.Value)).ToString());
                        sssCust.Param.Add("update_by", GlobalStaticObj.UserID);
                        sssCust.Param.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
                        break;
                }
                sysSqlStrList.Add(sssCust);
                return sysSqlStrList;
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                return null;
            }
        }
        #endregion

        #region Event -- 事件
        private void UCCustomerAddOrEdit_Load(object sender, EventArgs e)   //取数据赋值
        {
            try
            {
                CommonFuncCall.BindProviceComBox(cboprovince, "省");  //省份
                CommonFuncCall.BindComBoxDataSource(cbocust_type, "sys_customer_category", "请选择");//客户类别 码表           
                CommonFuncCall.BindComBoxDataSource(cboenterprise_nature, "sys_enterprise_property", "请选择");//企业性质 码表
                CommonFuncCall.BindComBoxDataSource(cboprice_type, "sys_customer_price_type", "请选择");//价格类型 码表
                CommonFuncCall.BindComBoxDataSource(cboopen_bank, "sys_deposit_bank", "请选择");//开户银行 码表
                CommonFuncCall.BindComBoxDataSource(cbomember_class, "sys_member_grade", "请选择");//会员等级 码表
                CommonFuncCall.BindComBoxDataSource(cbocredit_rating, "sys_credit_rating", "请选择");//信用等级 码表

                InitDataGridCellFormatting();

                ucAttr.TableName = "tb_customer";
                ucAttr.TableNameKeyValue = Id;
                BindControls();
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
        }
        void UCCustomerAddOrEdit_SaveEvent(object sender, EventArgs e)  //数据保存
        {
            try
            {
                if (!CheckControlValue()) return;
                var sysSqlStrList = new List<SysSQLString>();

                var custSql = BuildCustomerSqlString();
                sysSqlStrList.Add(custSql.Item1);
                sysSqlStrList.AddRange(BuildContactRelation(custSql.Item2));
                sysSqlStrList.AddRange(BuildVehicleRelation(custSql.Item2));
                sysSqlStrList.AddRange(BuildVipMemberSqlString(custSql.Item2));

                ucAttr.GetAttachmentSql(sysSqlStrList);   //保存附件时失败...目前保留此代码
                var opName = windowStatus == WindowStatus.Edit ? "更新客户档案" : "新增客户档案";
                var result = DBHelper.BatchExeSQLStringMultiByTrans(opName, sysSqlStrList);
                if (result)
                {
                    var customer = new tb_customer();
                    customer.cust_id = custSql.Item1.Param["cust_id"];
                    //customer.cust_code = custSql.Item1.Param["cust_code"];  //客户编码会将宇通那边的客户编码覆盖...现在上传将取消此字段
                    customer.cust_name = custSql.Item1.Param["cust_name"];
                    customer.cust_short_name = custSql.Item1.Param["cust_short_name"];
                    customer.cust_quick_code = custSql.Item1.Param["cust_quick_code"];
                    customer.cust_type = custSql.Item1.Param["cust_type"];
                    customer.legal_person = custSql.Item1.Param["legal_person"];
                    customer.enterprise_nature = custSql.Item1.Param["enterprise_nature"];
                    customer.cust_tel = custSql.Item1.Param["cust_tel"];
                    customer.cust_fax = custSql.Item1.Param["cust_fax"];
                    customer.cust_email = custSql.Item1.Param["cust_email"];
                    customer.cust_phone = custSql.Item1.Param["cust_phone"];
                    customer.cust_website = custSql.Item1.Param["cust_website"];
                    customer.province = custSql.Item1.Param["province"];
                    customer.city = custSql.Item1.Param["city"];
                    customer.county = custSql.Item1.Param["county"];
                    customer.cust_address = custSql.Item1.Param["cust_address"];
                    customer.zip_code = custSql.Item1.Param["zip_code"];
                    customer.tax_num = custSql.Item1.Param["tax_num"];
                    customer.indepen_legalperson = custSql.Item1.Param["indepen_legalperson"];
                    customer.credit_rating = custSql.Item1.Param["credit_rating"];
                    var creditLine = 0;
                    customer.credit_line = Int32.TryParse(custSql.Item1.Param["credit_line"], out creditLine) ? creditLine : 0;
                    customer.credit_account_period = String.IsNullOrEmpty(custSql.Item1.Param["credit_account_period"]) ? 0 : Convert.ToInt32(custSql.Item1.Param["credit_account_period"]);
                    customer.price_type = custSql.Item1.Param["price_type"];
                    customer.billing_name = custSql.Item1.Param["billing_name"];
                    customer.billing_address = custSql.Item1.Param["billing_address"];
                    customer.billing_account = custSql.Item1.Param["billing_account"];
                    customer.open_bank = custSql.Item1.Param["open_bank"];
                    customer.bank_account = custSql.Item1.Param["bank_account"];
                    customer.bank_account_person = custSql.Item1.Param["bank_account_person"];
                    customer.cust_remark = custSql.Item1.Param["cust_remark"];
                    customer.is_member = custSql.Item1.Param["is_member"];
                    customer.member_number = custSql.Item1.Param["member_number"];
                    customer.member_class = custSql.Item1.Param["member_class"];
                    if (rdbis_member_y.Checked) customer.member_period_validity = Convert.ToInt64(custSql.Item1.Param["member_period_validity"]);
                    customer.status = "0";
                    customer.enable_flag = custSql.Item1.Param["enable_flag"];
                    customer.data_source = custSql.Item1.Param["data_source"];
                    customer.cust_crm_guid = custSql.Item1.Param["cust_crm_guid"];

                    customer.accessories_discount = 0;
                    customer.workhours_discount = 0;
                    customer.country = custSql.Item1.Param["country"];
                    customer.indepen_legalperson = custSql.Item1.Param["indepen_legalperson"];
                    customer.market_segment = custSql.Item1.Param["market_segment"];
                    customer.institution_code = custSql.Item1.Param["institution_code"];
                    customer.com_constitution = custSql.Item1.Param["com_constitution"];
                    customer.registered_capital = custSql.Item1.Param["registered_capital"];
                    customer.vehicle_structure = custSql.Item1.Param["vehicle_structure"];
                    customer.agency = custSql.Item1.Param["agency"];
                    customer.sap_code = custSql.Item1.Param["sap_code"];
                    customer.business_scope = custSql.Item1.Param["business_scope"];
                    customer.ent_qualification = custSql.Item1.Param["ent_qualification"];


                    if (windowStatus == WindowStatus.Edit)
                    {
                        customer.update_by = custSql.Item1.Param["update_by"];
                        customer.update_time = Convert.ToInt64(custSql.Item1.Param["update_time"]);
                    }
                    else
                    {
                        customer.create_time = Convert.ToInt64(custSql.Item1.Param["create_time"]);
                        customer.create_by = custSql.Item1.Param["create_by"];
                    }
                    new Action(delegate { var flag = DBHelper.WebServHandler(opName, EnumWebServFunName.UpLoadCustomer, customer);
                        var contactSql = BuildContactRelation(custSql.Item2).ToArray();
                        if (String.IsNullOrEmpty(flag))
                        {
                            foreach (var sysSqlString in contactSql)
                            {
                                if (!sysSqlString.Param.ContainsKey("cont_id")) continue;
                                var contId = sysSqlString.Param["cont_id"];
                                var dt1 = DBHelper.GetTable("获取客户CRMID", "tb_customer", "*", String.Format("cust_id = '{0}'", customer.cust_id), "", "");
                                var dt = DBHelper.GetTable("根据客户档案获取联系信息", "v_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), " cont_id = '" + contId + "'", "", "");
                                if (dt.DefaultView != null && dt.DefaultView.Count > 0)
                                {
                                    var cont = new tb_contacts_ex
                                    {
                                        cont_id = CommonCtrl.IsNullToString(dt.DefaultView[0]["cont_id"]),
                                        cont_name = CommonCtrl.IsNullToString(dt.DefaultView[0]["cont_name"]),
                                        cont_post = CommonCtrl.IsNullToString(dt.DefaultView[0]["cont_post_name"]),
                                        cont_phone = CommonCtrl.IsNullToString(dt.DefaultView[0]["phone"]),
                                        nation = CommonCtrl.IsNullToString(dt.DefaultView[0]["nation_name"]),
                                        parent_customer = CommonCtrl.IsNullToString(dt1.DefaultView[0]["cust_crm_guid"]),
                                        //alter by 杨超逸
                                        //2014.12.26
                                        //宇通借口男女性别上传数据修改联系人时,性别传1（男）和2（女）
                                        //sex = CommonCtrl.IsNullToString(dt.DefaultView[0]["sex"]),
                                        sex = UpSex(dt.DefaultView[0]["sex"]),
                                        //alter end
                                        status = CommonCtrl.IsNullToString(dt.DefaultView[0]["status"]),
                                        cont_post_remark = CommonCtrl.IsNullToString(dt.DefaultView[0]["post_remark"]),
                                        cont_crm_guid = CommonCtrl.IsNullToString(dt.DefaultView[0]["cont_crm_guid"]),
                                        contact_type = "01" //标识类型为联系人
                                    };
                                    var flag4Cont = DBHelper.WebServHandler(opName, EnumWebServFunName.UpLoadCcontact, cont);
                                    if (String.IsNullOrEmpty(flag4Cont))
                                    {
                                        //do something
                                    }
                                }

                            }
                        } }).BeginInvoke(null,null);
                    MessageBoxEx.Show("保存成功!", "保存");
                    UCCustomerManager.BindPageData();
                    deleteMenuByTag(Tag.ToString(), UCCustomerManager.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "保存");
                }
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
                MessageBoxEx.Show("保存失败！" + ex.Message, "提示");
            }

        }

        #region 省市
        private void cboprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboprovince.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(cbocity, cboprovince.SelectedValue.ToString(), "市");
                CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "区/县");
            }
            else
            {
                CommonFuncCall.BindCityComBox(cbocity, "", "市");
                CommonFuncCall.BindCountryComBox(cbocounty, "", "区/县");
            }
        }
        private void cbocity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbocity.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCountryComBox(cbocounty, "", "区/县");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(cbocounty, cbocity.SelectedValue.ToString(), "区/县");
            }
        }

        #endregion

        #region 是否会员
        /// <summary>
        /// 是否会员选种植改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbis_member_y_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbis_member_y.Checked)
            {
                cbomember_class.Enabled = true;
                dtpmember_period_validity.Enabled = true;
            }
            else
            {
                cbomember_class.Enabled = false;
                dtpmember_period_validity.Enabled = false;
            }
        }
        #endregion

        #region 上传联系人性别转换方法
        //上传到宇通接口的性别转化 联系人时 ，性别传1（男）和2（女）
        private string UpSex(object sexvalue)
        {
            if (CommonCtrl.IsNullToString(sexvalue) == "1")
            {
                return "1";
            }
            if (sexvalue.ToString() == "0")
                return "2";
            else
                return "1";


        }
        #endregion

        #endregion
    }
}
