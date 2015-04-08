using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using BLL;
using HXC_FuncUtility;
using Utility.Common;
using SYSModel;

namespace HXCServerWinForm.UCForm.AcountSet
{
    public partial class frmAccountEdit : FormEx
    {
        #region 属性
        /// <summary>当前步骤
        /// </summary>
        public int currStep = 0;
        /// <summary> 0-新增 1-编辑 2-查看
        /// </summary>
        public int OpType = 0;
        /// <summary> 帐套id
        /// </summary>
        public string id = "";
        #endregion

        public frmAccountEdit()
        {
            InitializeComponent();
        }

        private void frmAccountEdit_Load(object sender, EventArgs e)
        {
            if (OpType == 2)
            {
                Common.SetControlEnable(pnlContainer, false);
            }
            CommonClass.CommonFuncCall.BindProviceComBox(ddlprovince, "请选择");
            BindCmx();
            BindData();
            ToStep(true);
        }

        #region 事件
        /// <summary> 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = HandleStep();
                if (flag)
                {
                    MessageBoxEx.Show("操作成功", "系统提示");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 上一步
        /// </summary>
        private void btnPrevStep_Click(object sender, EventArgs e)
        {
            try
            {
                ToStep(false);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 下一步
        /// </summary>
        private void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                errProvider.Clear();
                bool flag = HandleStep();
                if (flag)
                {
                    ToStep(true);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 创建帐套
        /// </summary>
        private void btnCreateAcc_Click(object sender, EventArgs e)
        {
            try
            {
                List<SysSQLString> list = new List<SysSQLString>();
                SysSQLString sysSQLString;
                StringBuilder strSql;
                #region 帐套信息
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_setbook(");
                strSql.Append("id,setbook_name,is_main_set_book,setbook_code,com_name,organization_code,legal_person,opening_bank,bank_account,province,city,county,postal_address,zip_code,company_web_site,email,contact,contact_telephone,status,create_by,create_time,enable_flag");
                strSql.Append(") values (");
                strSql.Append("@id,@setbook_name,@is_main_set_book,@setbook_code,@com_name,@organization_code,@legal_person,@opening_bank,@bank_account,@province,@city,@county,@postal_address,@zip_code,@company_web_site,@email,@contact,@contact_telephone,@status,@create_by,@create_time,@enable_flag) ");
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetAccParas();
                list.Add(sysSQLString);
                #endregion
                #region 维修参数
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_repair_param(");
                strSql.Append("r_param_id,appointment_audit,r_reception_audit,r_schedul_quality_ins,r_settlement_audit,rescue_audit,r_return_audit,requisition_audit,material_return_audit,repail_flow1,single_editors_same_person,single_audit_same_person,single_disabled_same_person,single_delete_same_person,repair_reception_import_pre,repair_return_import_pre,requisition_import_pre,material_return_import_pre,three_service_import_pre_yt,old_pieces_storage_import_pre,allow_material_larger_parts_r,requisition_auto_outbound,create_by,create_time,book_id,repail_flow2,repail_flow3,time_standards");
                strSql.Append(") values (");
                strSql.Append("@r_param_id,@appointment_audit,@r_reception_audit,@r_schedul_quality_ins,@r_settlement_audit,@rescue_audit,@r_return_audit,@requisition_audit,@material_return_audit,@repail_flow1,@single_editors_same_person,@single_audit_same_person,@single_disabled_same_person,@single_delete_same_person,@repair_reception_import_pre,@repair_return_import_pre,@requisition_import_pre,@material_return_import_pre,@three_service_import_pre_yt,@old_pieces_storage_import_pre,@allow_material_larger_parts_r,@requisition_auto_outbound,@create_by,@create_time,@book_id,@repail_flow2,@repail_flow3,@time_standards) ");
                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetRepairParas();
                list.Add(sysSQLString);
                #endregion
                #region 采购参数
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_purchase_param(");
                strSql.Append("purchase_param_id,purchase_plan_audit,purchase_order_audit,purchase_open_audit,purchase_order_audit_yt,purchase_open_outin,single_editors_same_person,single_audit_same_person,single_disabled_same_person,single_delete_same_person,purchase_order_import_pre,purchase_open_import_pre,create_by,create_time,book_id");
                strSql.Append(") values (");
                strSql.Append("@purchase_param_id,@purchase_plan_audit,@purchase_order_audit,@purchase_open_audit,@purchase_order_audit_yt,@purchase_open_outin,@single_editors_same_person,@single_audit_same_person,@single_disabled_same_person,@single_delete_same_person,@purchase_order_import_pre,@purchase_open_import_pre,@create_by,@create_time,@book_id) ");

                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetPurchaseParas();
                list.Add(sysSQLString);
                #endregion
                #region 销售参数
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_sale_param(");
                strSql.Append("sale_param_id,sales_plan_audit,sales_order_audit,sales_open_audit,sales_open_outin,sales_order_line_credit,sales_open_line_credit,single_editors_same_person,single_audit_same_person,single_disabled_same_person,single_delete_same_person,create_by,create_time,book_id");
                strSql.Append(") values (");
                strSql.Append("@sale_param_id,@sales_plan_audit,@sales_order_audit,@sales_open_audit,@sales_open_outin,@sales_order_line_credit,@sales_open_line_credit,@single_editors_same_person,@single_audit_same_person,@single_disabled_same_person,@single_delete_same_person,@create_by,@create_time,@book_id) ");

                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetSaleParas();
                list.Add(sysSQLString);
                #endregion
                #region 库存参数
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_stock_param(");
                strSql.Append("stock_param_id,storage_manage,making_audit_one_person,allow_zero_lib_stock,allow_zero_lib_junction,single_editors_one_person,single_audit_one_person,single_disabled_one_person,single_delete_one_person,monthly_average_method,moving_average_method,fifo_method,counts,counts_zero,price,price_zero,warehous_single_reference,create_by,create_time,book_id,batch_manage");
                strSql.Append(") values (");
                strSql.Append("@stock_param_id,@storage_manage,@making_audit_one_person,@allow_zero_lib_stock,@allow_zero_lib_junction,@single_editors_one_person,@single_audit_one_person,@single_disabled_one_person,@single_delete_one_person,@monthly_average_method,@moving_average_method,@fifo_method,@counts,@counts_zero,@price,@price_zero,@warehous_single_reference,@create_by,@create_time,@book_id,@batch_manage) ");

                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetStockParas();
                list.Add(sysSQLString);
                #endregion
                #region 财务参数
                sysSQLString = new SysSQLString();
                sysSQLString.cmdType = CommandType.Text;
                strSql = new StringBuilder();
                strSql.Append("insert into sys_financial_ser_param(");
                strSql.Append("financial_ser_param_id,tax_rate,currency,counts,counts_zero,price,price_zero,create_by,create_time,book_id");
                strSql.Append(") values (");
                strSql.Append("@financial_ser_param_id,@tax_rate,@currency,@counts,@counts_zero,@price,@price_zero,@create_by,@create_time,@book_id) ");

                sysSQLString.sqlString = strSql.ToString();
                sysSQLString.Param = GetFinancParas();
                list.Add(sysSQLString);
                #endregion

                #region 创建帐套
                try
                {
                    string newDbName = GlobalStaticObj_Server.DbPrefix + txtsetbook_code.Caption.Trim();
                    Dictionary<string, string> dicParam = new Dictionary<string, string>();
                    dicParam.Add("newDbName", newDbName);//新建数据库名(不带扩展名)
                    dicParam.Add("dbDataDirPath", GlobalStaticObj_Server.Instance.DbServerInstallDir);//服务端数据库安装路径(带\)
                    dicParam.Add("soureDbName", GlobalStaticObj_Server.Instance.DbTemplateBakFileName);//原数据库名(不带扩展名)
                    dicParam.Add("soureBackupFilePATH", GlobalStaticObj_Server.Instance.DbServerBackDir + GlobalStaticObj_Server.Instance.DbTemplateBakFileName + ".bak");//服务端备份文件路径(带文件名扩展名)           
                    DBHelper.ExtNonQuery("创建帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sp_createdbbybak", CommandType.StoredProcedure, dicParam);
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("帐套创建失败" + ex.Message, "系统提示");
                    OpType = 0;
                    return;
                }
                #endregion

                bool flag = DBHelper.BatchExeSQLStringMultiByTrans("创建帐套", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, list);
                if (!flag)
                {
                    MessageBoxEx.Show("帐套创建失败", "系统提示");
                    return;
                }

                MessageBoxEx.Show("帐套创建成功", "系统提示");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OpType == 2)
            {
                this.Close();
                return;
            }
            string msg = "取消将会放弃保存用户信息，是否继续？";
            if (OpType == 0)
            {
                msg = "取消将会放弃创建帐套，是否继续？";
            }
            DialogResult result = MessageBoxEx.Show(msg, "系统提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #region 城市联动
        /// <summary> 选择省份
        /// </summary>
        private void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlprovince.SelectedValue.ToString()))
                {
                    CommonClass.CommonFuncCall.BindCityComBox(ddlcity, ddlprovince.SelectedValue.ToString(), "请选择");
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
                }
                else
                {
                    CommonClass.CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        /// <summary> 选择城市
        /// </summary>
        private void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlcity.SelectedValue.ToString()))
                {
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
                }
                else
                {
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("帐套设置", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion
        #endregion

        #region 方法
        /// <summary> 下拉框初始化绑定
        /// </summary>
        public void BindCmx()
        {
            //绑定货币类型
            cmbcurrency.DataSource = DataSources.EnumToListByValueString(typeof(DataSources.EnumCurrencyType), true, "请选择");
            cmbcurrency.ValueMember = "value";
            cmbcurrency.DisplayMember = "text";
        }

        /// <summary> 绑定数据
        /// </summary>
        private void BindData()
        {
            if (OpType == 0)
            {
                //生成新的帐套代码
                txtsetbook_code.Caption = "";
                string newCode = "";
                string maxCode = DBHelper.GetSingleValue("获取最大帐套代码", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "select max(setbook_code) from sys_setbook");
                newCode = string.IsNullOrEmpty(maxCode) ? "001" : (Convert.ToInt32(maxCode) + 1).ToString().PadLeft(3, '0');
                txtsetbook_code.Caption = newCode;
                return;
            }

            DataTable dtAcc = DBHelper.GetTable("获取帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "*", "id='" + id + "'", "", "");
            if (dtAcc.Rows.Count > 0)
            {
                #region 帐套信息
                DataRow drAcc = dtAcc.Rows[0];
                txtsetbook_name.Caption = drAcc["setbook_name"].ToString();
                txtsetbook_code.Caption = drAcc["setbook_code"].ToString();
                txtcom_name.Caption = drAcc["com_name"].ToString();
                txtorganization_code.Caption = drAcc["organization_code"].ToString();
                txtlegal_person.Caption = drAcc["legal_person"].ToString();
                txtopening_bank.Caption = drAcc["opening_bank"].ToString();
                txtbank_account.Caption = drAcc["bank_account"].ToString();
                ddlprovince.SelectedValue = drAcc["province"].ToString();
                ddlcity.SelectedValue = drAcc["city"].ToString();
                ddlcounty.SelectedValue = drAcc["county"].ToString();
                txtpostal_address.Caption = drAcc["postal_address"].ToString();
                txtzip_code.Caption = drAcc["zip_code"].ToString();
                txtcompany_web_site.Caption = drAcc["company_web_site"].ToString();
                txtemail.Caption = drAcc["email"].ToString();
                txtcontact.Caption = drAcc["contact"].ToString();
                txtcontact_telephone.Caption = drAcc["contact_telephone"].ToString();
                #endregion

                string where = "book_id='" + id + "'";

                #region 维修信息
                DataTable dtRepair = DBHelper.GetTable("获取维修参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_repair_param", "*", where, "", "");
                if (dtRepair.Rows.Count > 0)
                {
                    DataRow drRepair = dtRepair.Rows[0];
                    rbrepail_flow1.Checked = drRepair["repail_flow1"].ToString() == "1";
                    cbsingle_editors_same_person2.Checked = drRepair["single_editors_same_person"].ToString() == "1";
                    cbsingle_audit_same_person2.Checked = drRepair["single_audit_same_person"].ToString() == "1";
                    cbsingle_disabled_same_person2.Checked = drRepair["single_disabled_same_person"].ToString() == "1";
                    cbsingle_delete_same_person2.Checked = drRepair["single_delete_same_person"].ToString() == "1";
                    cbrepair_reception_import_pre.Checked = drRepair["repair_reception_import_pre"].ToString() == "1";
                    cbrepair_return_import_pre.Checked = drRepair["repair_return_import_pre"].ToString() == "1";
                    cbrequisition_import_pre.Checked = drRepair["requisition_import_pre"].ToString() == "1";
                    cbmaterial_return_import_pre.Checked = drRepair["material_return_import_pre"].ToString() == "1";
                    cbthree_service_import_pre_yt.Checked = drRepair["three_service_import_pre_yt"].ToString() == "1";
                    cbappointment_audit.Checked = drRepair["appointment_audit"].ToString() == "1";
                    cbold_pieces_storage_import_pre.Checked = drRepair["old_pieces_storage_import_pre"].ToString() == "1";
                    cballow_material_larger_parts_r.Checked = drRepair["allow_material_larger_parts_r"].ToString() == "1";
                    cbrequisition_auto_outbound.Checked = drRepair["requisition_auto_outbound"].ToString() == "1";
                    rbrepail_flow2.Checked = drRepair["repail_flow2"].ToString() == "1";
                    cbr_reception_audit.Checked = drRepair["r_reception_audit"].ToString() == "1";
                    rbrepail_flow3.Checked = drRepair["repail_flow3"].ToString() == "1";
                    cbr_schedul_quality_ins.Checked = drRepair["r_schedul_quality_ins"].ToString() == "1";
                    cbr_settlement_audit.Checked = drRepair["r_settlement_audit"].ToString() == "1";
                    cbrescue_audit.Checked = drRepair["rescue_audit"].ToString() == "1";
                    cbr_return_audit.Checked = drRepair["r_return_audit"].ToString() == "1";
                    cbrequisition_audit.Checked = drRepair["requisition_audit"].ToString() == "1";
                    cbmaterial_return_audit.Checked = drRepair["material_return_audit"].ToString() == "1";
                    txtTimeStandtards.Value = 0;
                    if (!string.IsNullOrEmpty(drRepair["time_standards"].ToString()))
                    {
                        txtTimeStandtards.Value = Convert.ToDecimal(drRepair["time_standards"].ToString());
                    }
                }

                #endregion

                #region 采购信息
                DataTable dtPurchase = DBHelper.GetTable("获取采购参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_purchase_param", "*", where, "", "");
                if (dtPurchase.Rows.Count > 0)
                {
                    DataRow drPurchase = dtPurchase.Rows[0];
                    cbsingle_delete_same_person3.Checked = drPurchase["single_delete_same_person"].ToString() == "1";
                    cbpurchase_order_import_pre.Checked = drPurchase["purchase_order_import_pre"].ToString() == "1";
                    cbpurchase_open_import_pre.Checked = drPurchase["purchase_open_import_pre"].ToString() == "1";
                    cbpurchase_plan_audit.Checked = drPurchase["purchase_plan_audit"].ToString() == "1";
                    cbpurchase_order_audit.Checked = drPurchase["purchase_order_audit"].ToString() == "1";
                    cbpurchase_open_audit.Checked = drPurchase["purchase_open_audit"].ToString() == "1";
                    cbpurchase_order_audit_yt.Checked = drPurchase["purchase_order_audit_yt"].ToString() == "1";
                    cbpurchase_open_outin.Checked = drPurchase["purchase_open_outin"].ToString() == "1";
                    cbsingle_editors_same_person3.Checked = drPurchase["single_editors_same_person"].ToString() == "1";
                    cbsingle_audit_same_person3.Checked = drPurchase["single_audit_same_person"].ToString() == "1";
                    cbsingle_disabled_same_person3.Checked = drPurchase["single_disabled_same_person"].ToString() == "1";
                }
                #endregion

                #region 销售信息
                DataTable dtSale = DBHelper.GetTable("获取销售参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_sale_param", "*", where, "", "");
                if (dtSale.Rows.Count > 0)
                {
                    DataRow drSale = dtSale.Rows[0];
                    cbsingle_disabled_same_person4.Checked = drSale["single_disabled_same_person"].ToString() == "1";
                    cbsingle_delete_same_person4.Checked = drSale["single_delete_same_person"].ToString() == "1";
                    cbsales_plan_audit.Checked = drSale["sales_plan_audit"].ToString() == "1";
                    cbsales_order_audit.Checked = drSale["sales_order_audit"].ToString() == "1";
                    cbsales_open_audit.Checked = drSale["sales_open_audit"].ToString() == "1";
                    cbsales_open_outin.Checked = drSale["sales_open_outin"].ToString() == "1";
                    cbsales_order_line_credit.Checked = drSale["sales_order_line_credit"].ToString() == "1";
                    cbsales_open_line_credit.Checked = drSale["sales_open_line_credit"].ToString() == "1";
                    cbsingle_editors_same_person4.Checked = drSale["single_editors_same_person"].ToString() == "1";
                    cbsingle_audit_same_person4.Checked = drSale["single_audit_same_person"].ToString() == "1";
                }
                #endregion

                #region 库存信息
                DataTable dtStock = DBHelper.GetTable("获取库存参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_stock_param", "*", where, "", "");
                if (dtStock.Rows.Count > 0)
                {
                    DataRow drStock = dtStock.Rows[0];
                    rbmonthly_average_method.Checked = drStock["monthly_average_method"].ToString() == "1";
                    rbmoving_average_method.Checked = drStock["moving_average_method"].ToString() == "1";
                    rbfifo_method.Checked = drStock["fifo_method"].ToString() == "1";
                    numcounts5.Value = Convert.ToDecimal(drStock["counts"]);
                    cbcounts_zero5.Checked = drStock["counts_zero"].ToString() == "1";
                    numprice5.Value = Convert.ToDecimal(drStock["price"]);
                    cbprice_zero5.Checked = drStock["price_zero"].ToString() == "1";
                    cbwarehous_single_reference.Checked = drStock["warehous_single_reference"].ToString() == "1";
                    cbstorage_manage.Checked = drStock["storage_manage"].ToString() == "1";
                    cbbatch_manage.Checked = drStock["batch_manage"].ToString() == "1";
                    cbmaking_audit_one_person.Checked = drStock["making_audit_one_person"].ToString() == "1";
                    cballow_zero_lib_stock.Checked = drStock["allow_zero_lib_stock"].ToString() == "1";
                    cballow_zero_lib_junction.Checked = drStock["allow_zero_lib_junction"].ToString() == "1";
                    cbsingle_editors_one_person5.Checked = drStock["single_editors_one_person"].ToString() == "1";
                    cbsingle_audit_one_person5.Checked = drStock["single_audit_one_person"].ToString() == "1";
                    cbsingle_disabled_one_person5.Checked = drStock["single_disabled_one_person"].ToString() == "1";
                    cbsingle_delete_one_person5.Checked = drStock["single_delete_one_person"].ToString() == "1";
                }
                #endregion

                #region 财务信息
                DataTable dtFinan = DBHelper.GetTable("获取财务参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_financial_ser_param", "*", where, "", "");
                if (dtFinan.Rows.Count > 0)
                {
                    DataRow drFinan = dtFinan.Rows[0];
                    numtax_rate.Value = Convert.ToDecimal(drFinan["tax_rate"]) * 100;
                    cmbcurrency.SelectedValue = drFinan["currency"].ToString();
                    numcounts6.Value = Convert.ToDecimal(drFinan["counts"]);
                    cbcounts_zero6.Checked = drFinan["counts_zero"].ToString() == "1";
                    numprice6.Value = Convert.ToDecimal(drFinan["price"]);
                    cbprice_zero6.Checked = drFinan["price_zero"].ToString() == "1";
                }
                #endregion
            }

        }

        /// <summary> 跳转到步骤
        /// </summary>
        /// <param name="toDirection">正向反向</param>
        public void ToStep(bool toDirection)
        {
            if (toDirection)
            {
                currStep += 1;
            }
            else
            {
                currStep -= 1;
            }
            string frmTitle = string.Empty;
            if (OpType == 0)
            {
                frmTitle = "新增";
            }
            else if (OpType == 0)
            {
                frmTitle = "编辑";
            }
            else if (OpType == 0)
            {
                frmTitle = "查看";
            }
            this.Text = string.Format(frmTitle + "帐套引导（{0}/6）", currStep);
            this.Invalidate(TextRect);
            btnPrevStep.Visible = currStep != 1;
            btnNextStep.Visible = currStep != 6;
            if (currStep == 6)
            {
                btnCreateAcc.Visible = OpType == 0;
                if (OpType == 0 || OpType == 2)
                {
                    btnSave.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnSave.Caption = "完成";
                }
            }
            else
            {
                btnCreateAcc.Visible = false;
                btnSave.Visible = OpType == 1;
                btnSave.Caption = "保存";
            }

            #region 跳转
            if (currStep == 1)
            {
                pnlStep1.BringToFront();
            }
            if (currStep == 2)
            {
                pnlStep2.BringToFront();
            }
            if (currStep == 3)
            {
                pnlStep3.BringToFront();
            }
            if (currStep == 4)
            {
                pnlStep4.BringToFront();
            }
            if (currStep == 5)
            {
                pnlStep5.BringToFront();
            }
            if (currStep == 6)
            {
                pnlStep6.BringToFront();
            }
            #endregion
        }

        /// <summary> 每步处理
        /// </summary>
        public bool HandleStep()
        {
            if (currStep == 1)
            {
                #region 帐套信息
                if (string.IsNullOrEmpty(txtsetbook_name.Caption.Trim()))
                {
                    Validator.SetError(errProvider, txtsetbook_name, "请录入帐套名称");
                    return false;
                }
                if (string.IsNullOrEmpty(txtcom_name.Caption.Trim()))
                {
                    Validator.SetError(errProvider, txtcom_name, "请录入公司名称");
                    return false;
                }
                if (string.IsNullOrEmpty(txtcontact.Caption.Trim()))
                {
                    Validator.SetError(errProvider, txtcontact, "请录入联系人");
                    return false;
                }
                if (string.IsNullOrEmpty(txtcontact_telephone.Caption.Trim()))
                {
                    Validator.SetError(errProvider, txtcontact_telephone, "请录入联系人电话号码或手机号");
                    return false;
                }
                else
                {
                    if (Common.ValidateStr(txtcontact_telephone.Caption.Trim(), @"[\d-]+") != 1)
                    {
                        Validator.SetError(errProvider, txtcontact_telephone, "请录入正确的电话号码或手机号");
                        return false;
                    }

                }
                if (!string.IsNullOrEmpty(txtemail.Caption))
                {
                    if (Common.ValidateStr(txtemail.Caption.Trim(), @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") != 1)
                    {
                        Validator.SetError(errProvider, txtemail, "请录入正确的电子邮箱");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtzip_code.Caption))
                {
                    if (Common.ValidateStr(txtzip_code.Caption.Trim(), @"[1-9]\d{5}(?!\d)") != 1)
                    {
                        Validator.SetError(errProvider, txtzip_code, "请录入正确的邮政编码");
                        return false;
                    }
                }

                if (OpType == 1)
                {
                    bool flag = SaveAccInfo();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            else if (currStep == 2)
            {
                #region 维修业务
                if (OpType == 1)
                {
                    bool flag = SaveRepairParam();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            else if (currStep == 3)
            {
                #region 采购业务
                if (OpType == 1)
                {
                    bool flag = SavePurchaseParam();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            else if (currStep == 4)
            {
                #region 销售业务
                if (OpType == 1)
                {
                    bool flag = SaveSaleParam();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            else if (currStep == 5)
            {
                #region 库存业务
                if (OpType == 1)
                {
                    bool flag = SaveStockParam();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            else if (currStep == 6)
            {
                #region 财务业务
                if (OpType == 1)
                {
                    bool flag = SavefinancParam();
                    if (!flag)
                    {
                        MessageBoxEx.Show("保存失败", "系统提示");
                        return false;
                    }
                }
                #endregion
            }
            return true;
        }

        /// <summary> 保存帐套信息
        /// </summary>
        /// <param name="isAdd">新增 or 编辑</param>
        /// <returns>是否成功</returns>
        public bool SaveAccInfo()
        {
            Dictionary<string, string> dicFields = GetAccParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "id", id, dicFields);
            return flag;
        }

        /// <summary> 获取帐套信息参数集合
        /// </summary>
        public Dictionary<string, string> GetAccParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("setbook_name", txtsetbook_name.Caption.Trim());
            dicFields.Add("com_name", txtcom_name.Caption.Trim());
            dicFields.Add("organization_code", txtorganization_code.Caption.Trim());
            dicFields.Add("contact", txtcontact.Caption.Trim());
            dicFields.Add("contact_telephone", txtcontact_telephone.Caption.Trim());
            dicFields.Add("legal_person", txtlegal_person.Caption.Trim());
            dicFields.Add("email", txtemail.Caption.Trim());
            dicFields.Add("opening_bank", txtopening_bank.Caption.Trim());
            dicFields.Add("bank_account", txtbank_account.Caption.Trim());
            dicFields.Add("province", ddlprovince.SelectedValue.ToString());
            dicFields.Add("city", ddlcity.SelectedValue.ToString());
            dicFields.Add("county", ddlcounty.SelectedValue.ToString());
            dicFields.Add("postal_address", txtpostal_address.Caption.Trim());
            dicFields.Add("zip_code", txtzip_code.Caption.Trim());
            dicFields.Add("company_web_site", txtcompany_web_site.Caption.Trim());
            if (OpType == 0)
            {
                id = Guid.NewGuid().ToString();
                dicFields.Add("id", id);
                dicFields.Add("setbook_code", txtsetbook_code.Caption.Trim());
                dicFields.Add("is_main_set_book", ((int)DataSources.EnumYesNo.NO).ToString());
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
                dicFields.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                dicFields.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }

        /// <summary> 保存维修参数
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SaveRepairParam()
        {
            Dictionary<string, string> dicFields = GetRepairParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存维修参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_repair_param", "book_id", id, dicFields);
            return flag;
        }

        /// <summary> 获取维修参数集合
        /// </summary>
        public Dictionary<string, string> GetRepairParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("repail_flow1", rbrepail_flow1.Checked ? "1" : "0");
            dicFields.Add("single_editors_same_person", cbsingle_editors_same_person2.Checked ? "1" : "0");
            dicFields.Add("single_audit_same_person", cbsingle_audit_same_person2.Checked ? "1" : "0");
            dicFields.Add("single_disabled_same_person", cbsingle_disabled_same_person2.Checked ? "1" : "0");
            dicFields.Add("single_delete_same_person", cbsingle_delete_same_person2.Checked ? "1" : "0");
            dicFields.Add("repair_reception_import_pre", cbrepair_reception_import_pre.Checked ? "1" : "0");
            dicFields.Add("repair_return_import_pre", cbrepair_return_import_pre.Checked ? "1" : "0");
            dicFields.Add("requisition_import_pre", cbrequisition_import_pre.Checked ? "1" : "0");
            dicFields.Add("material_return_import_pre", cbmaterial_return_import_pre.Checked ? "1" : "0");
            dicFields.Add("three_service_import_pre_yt", cbthree_service_import_pre_yt.Checked ? "1" : "0");
            dicFields.Add("appointment_audit", cbappointment_audit.Checked ? "1" : "0");
            dicFields.Add("old_pieces_storage_import_pre", cbold_pieces_storage_import_pre.Checked ? "1" : "0");
            dicFields.Add("allow_material_larger_parts_r", cballow_material_larger_parts_r.Checked ? "1" : "0");
            dicFields.Add("requisition_auto_outbound", cbrequisition_auto_outbound.Checked ? "1" : "0");
            dicFields.Add("repail_flow2", rbrepail_flow2.Checked ? "1" : "0");
            dicFields.Add("r_reception_audit", cbr_reception_audit.Checked ? "1" : "0");
            dicFields.Add("repail_flow3", rbrepail_flow3.Checked ? "1" : "0");
            dicFields.Add("r_schedul_quality_ins", cbr_schedul_quality_ins.Checked ? "1" : "0");
            dicFields.Add("r_settlement_audit", cbr_settlement_audit.Checked ? "1" : "0");
            dicFields.Add("rescue_audit", cbrescue_audit.Checked ? "1" : "0");
            dicFields.Add("r_return_audit", cbr_return_audit.Checked ? "1" : "0");
            dicFields.Add("requisition_audit", cbrequisition_audit.Checked ? "1" : "0");
            dicFields.Add("material_return_audit", cbmaterial_return_audit.Checked ? "1" : "0");
            dicFields.Add("time_standards", txtTimeStandtards.Value.ToString());
            if (OpType == 0)
            {
                dicFields.Add("r_param_id", Guid.NewGuid().ToString());
                dicFields.Add("book_id", id);
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }

        /// <summary> 保存采购参数
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SavePurchaseParam()
        {
            Dictionary<string, string> dicFields = GetPurchaseParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存采购参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_purchase_param", "book_id", id, dicFields);
            return flag;
        }

        /// <summary> 获取采购参数集合
        /// </summary>
        public Dictionary<string, string> GetPurchaseParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("single_delete_same_person", cbsingle_delete_same_person3.Checked ? "1" : "0");
            dicFields.Add("purchase_order_import_pre", cbpurchase_order_import_pre.Checked ? "1" : "0");
            dicFields.Add("purchase_open_import_pre", cbpurchase_open_import_pre.Checked ? "1" : "0");
            dicFields.Add("purchase_plan_audit", cbpurchase_plan_audit.Checked ? "1" : "0");
            dicFields.Add("purchase_order_audit", cbpurchase_order_audit.Checked ? "1" : "0");
            dicFields.Add("purchase_open_audit", cbpurchase_open_audit.Checked ? "1" : "0");
            dicFields.Add("purchase_order_audit_yt", cbpurchase_order_audit_yt.Checked ? "1" : "0");
            dicFields.Add("purchase_open_outin", cbpurchase_open_outin.Checked ? "1" : "0");
            dicFields.Add("single_editors_same_person", cbsingle_editors_same_person3.Checked ? "1" : "0");
            dicFields.Add("single_audit_same_person", cbsingle_audit_same_person3.Checked ? "1" : "0");
            dicFields.Add("single_disabled_same_person", cbsingle_disabled_same_person3.Checked ? "1" : "0");
            if (OpType == 0)
            {
                dicFields.Add("purchase_param_id", Guid.NewGuid().ToString());
                dicFields.Add("book_id", id);
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }

        /// <summary> 保存销售参数
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SaveSaleParam()
        {
            Dictionary<string, string> dicFields = GetSaleParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存销售参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_sale_param", "book_id", id, dicFields);
            return flag;
        }

        /// <summary> 获取销售参数集合
        /// </summary>
        public Dictionary<string, string> GetSaleParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("single_disabled_same_person", cbsingle_disabled_same_person4.Checked ? "1" : "0");
            dicFields.Add("single_delete_same_person", cbsingle_delete_same_person4.Checked ? "1" : "0");
            dicFields.Add("sales_plan_audit", cbsales_plan_audit.Checked ? "1" : "0");
            dicFields.Add("sales_order_audit", cbsales_order_audit.Checked ? "1" : "0");
            dicFields.Add("sales_open_audit", cbsales_open_audit.Checked ? "1" : "0");
            dicFields.Add("sales_open_outin", cbsales_open_outin.Checked ? "1" : "0");
            dicFields.Add("sales_order_line_credit", cbsales_order_line_credit.Checked ? "1" : "0");
            dicFields.Add("sales_open_line_credit", cbsales_open_line_credit.Checked ? "1" : "0");
            dicFields.Add("single_editors_same_person", cbsingle_editors_same_person4.Checked ? "1" : "0");
            dicFields.Add("single_audit_same_person", cbsingle_audit_same_person4.Checked ? "1" : "0");
            if (OpType == 0)
            {
                dicFields.Add("sale_param_id", Guid.NewGuid().ToString());
                dicFields.Add("book_id", id);
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }

        /// <summary> 保存库存参数
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SaveStockParam()
        {
            Dictionary<string, string> dicFields = GetStockParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存库存参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_stock_param", "book_id", id, dicFields);
            return flag;
        }

        /// <summary> 获取库存参数集合
        /// </summary>
        public Dictionary<string, string> GetStockParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("monthly_average_method", rbmonthly_average_method.Checked ? "1" : "0");
            dicFields.Add("moving_average_method", rbmoving_average_method.Checked ? "1" : "0");
            dicFields.Add("fifo_method", rbfifo_method.Checked ? "1" : "0");
            dicFields.Add("counts", numcounts5.Value.ToString());
            dicFields.Add("counts_zero", cbcounts_zero5.Checked ? "1" : "0");
            dicFields.Add("price", numprice5.Value.ToString());
            dicFields.Add("price_zero", cbprice_zero5.Checked ? "1" : "0");
            dicFields.Add("warehous_single_reference", cbwarehous_single_reference.Checked ? "1" : "0");
            dicFields.Add("storage_manage", cbstorage_manage.Checked ? "1" : "0");
            dicFields.Add("batch_manage", cbbatch_manage.Checked ? "1" : "0");
            dicFields.Add("making_audit_one_person", cbmaking_audit_one_person.Checked ? "1" : "0");
            dicFields.Add("allow_zero_lib_stock", cballow_zero_lib_stock.Checked ? "1" : "0");
            dicFields.Add("allow_zero_lib_junction", cballow_zero_lib_junction.Checked ? "1" : "0");
            dicFields.Add("single_editors_one_person", cbsingle_editors_one_person5.Checked ? "1" : "0");
            dicFields.Add("single_audit_one_person", cbsingle_audit_one_person5.Checked ? "1" : "0");
            dicFields.Add("single_disabled_one_person", cbsingle_disabled_one_person5.Checked ? "1" : "0");
            dicFields.Add("single_delete_one_person", cbsingle_delete_one_person5.Checked ? "1" : "0");
            if (OpType == 0)
            {
                dicFields.Add("stock_param_id", Guid.NewGuid().ToString());
                dicFields.Add("book_id", id);
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }

        /// <summary> 保存财务参数
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SavefinancParam()
        {
            Dictionary<string, string> dicFields = GetFinancParas();
            bool flag = DBHelper.Submit_AddOrEdit("保存财务参数", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_financial_ser_param", "book_id", id, dicFields);
            return flag;
        }

        /// <summary> 获取财务参数集合
        /// </summary>
        public Dictionary<string, string> GetFinancParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("tax_rate", (numtax_rate.Value / 100).ToString());
            dicFields.Add("currency", cmbcurrency.SelectedValue.ToString());
            dicFields.Add("counts", numcounts6.Value.ToString());
            dicFields.Add("counts_zero", cbcounts_zero6.Checked ? "1" : "0");
            dicFields.Add("price", numprice6.Value.ToString());
            dicFields.Add("price_zero", cbprice_zero6.Checked ? "1" : "0");

            if (OpType == 0)
            {
                dicFields.Add("financial_ser_param_id", Guid.NewGuid().ToString());
                dicFields.Add("book_id", id);
                dicFields.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }
        #endregion
    }
}
