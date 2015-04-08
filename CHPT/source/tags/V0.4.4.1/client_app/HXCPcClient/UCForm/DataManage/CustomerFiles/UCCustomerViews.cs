using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.CustomerFiles
{
    /// <summary>
    ///
    /// </summary>
    public partial class UCCustomerViews : UCBase
    {
        #region Constructor -- 构造函数
        public UCCustomerViews()
        {
            InitializeComponent();

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
        private void InitDataGridCellFormatting()   //初始化数据表格
        {
            try
            {
                #region 联系人信息
                dgvcontacts.CellFormatting += delegate(object sender, DataGridViewCellFormattingEventArgs args)
                {
                    UIAssistants.DgvCellDataConvert2DicData(dgvcontacts, args, "cont_post");
                    UIAssistants.DgvCellDataConvert2Datetime(dgvcontacts, args, "cont_birthday");
                    //UIAssistants.DgvCellDataConvert2Enum(dgvcontacts, args, "is_default", typeof(DataSources.EnumYesNo));
                };

                dgvcontacts.Rows.Add(3);
                dgvcontacts.AllowUserToAddRows = false;
                DataSources.BindComDataGridViewBoxColumnDataEnum(is_default, typeof(DataSources.EnumYesNo));

                dgvcontacts.CellEndEdit += delegate(object sender, DataGridViewCellEventArgs e)
                {
                    var value = CommonCtrl.IsNullToString(dgvcontacts.Rows[e.RowIndex].Cells["is_default"].Value);
                    if (!string.IsNullOrEmpty(value) && e.ColumnIndex == 6)
                    {
                        foreach (DataGridViewRow row in dgvcontacts.Rows)
                        {
                            if (dgvcontacts.Rows.IndexOf(row) == e.RowIndex) continue;
                            switch (value)
                            {
                                case "1":
                                    row.Cells["is_default"].Value = "0";
                                    break;
                                case "0":
                                    row.Cells["is_default"].Value = "0";
                                    break;
                            }
                        }
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
                if (String.IsNullOrEmpty(Id)) return;
                var dt = DBHelper.GetTable("查询客户档案", "tb_customer", "*", "cust_id='" + Id + "'", "", "");
                if (dt == null || dt.Rows.Count <= 0) return;
                var dr = dt.Rows[0];
                lbl_cust_code.Text = dr["cust_code"].ToString();
                lbl_legal_person.Text = dr["legal_person"].ToString();

                lbl_cust_name.Text = dr["cust_name"].ToString();
                lbl_cust_phone.Text = dr["cust_tel"].ToString();
                lbl_cust_short_name.Text = dr["cust_short_name"].ToString();
                lbl_cust_short_name_jp.Text = dr["cust_short_name"].ToString();
                lbl_cust_fax.Text = dr["cust_fax"].ToString();
                lbl_cust_address.Text = dr["cust_address"].ToString();
                lbl_cust_email.Text = dr["cust_email"].ToString();
                lbl_zip_code.Text = dr["zip_code"].ToString();
                lbl_cust_website.Text = dr["cust_website"].ToString();
                lbl_tax_num.Text = dr["tax_num"].ToString();
                lbl_cust_address_connect.Text = DBHelper.GetSingleValue("", "sys_area", "AREA_NAME", "AREA_CODE='" + dr["province"] + "'", "") + "-" + DBHelper.GetSingleValue("", "sys_area", "AREA_NAME", "AREA_CODE='" + dr["city"] + "'", "") + "-" + DBHelper.GetSingleValue("", "sys_area", "AREA_NAME", "AREA_CODE='" + dr["county"] + "'", "");
                lbl_cust_type_code.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_code", "dic_id='" + dr["cust_type"] + "'", "");
                lbl_cust_type_name.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["cust_type"] + "'", "");
                lbl_enterprise_nature.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["enterprise_nature"] + "'", "");
                lbl_credit_rating.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["credit_rating"] + "'", "");
                lbl_price_type.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["price_type"] + "'", "");
                lbl_open_bank.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["open_bank"] + "'", "");
                lbl_credit_line.Text = dr["credit_line"].ToString();
                lbl_credit_account_period.Text = dr["credit_account_period"].ToString();
                lbl_billing_name.Text = dr["billing_name"].ToString();
                lbl_billing_address.Text = dr["billing_address"].ToString();
                lbl_billing_account.Text = dr["billing_account"].ToString();
                lbl_is_member.Text = dr["is_member"].ToString() == "1" ? "是" : "否";
                lbl_member_number.Text = dr["member_number"].ToString();
                lbl_remarks.Text = dr["cust_remark"].ToString();
                lbl_open_bank.Text = dr["open_bank"].ToString();
                lbl_bank_account.Text = dr["bank_account"].ToString();
                lbl_cust_relation.Text = dr["cust_relation"].ToString();
                lbl_data_source.Text = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_name", "dic_id='" + dr["data_source"] + "'", "");
                lbl_sap_code.Text = dr["sap_code"].ToString();
                lbl_yt_customer_manager.Text = dr["yt_customer_manager"].ToString();
                lbl_status.Text = dr["status"].ToString() == "1" ? "启用" : "停用";
                lbl_create_by.Text = dr["create_by"].ToString();
                lbl_create_time.Text = Common.UtcLongToLocalDateTime(dr["create_time"]);
                lbl_update_by.Text = dr["update_by"].ToString();
                lbl_update_time.Text = Common.UtcLongToLocalDateTime(dr["update_time"]);
                BindContacts();
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

            try
            {
                //联系人数据                
                var dpt = DBHelper.GetTable("联系人数据", "(select tr.relation_object_id, tr.is_default tr_is_default, tb.*  from tb_contacts tb inner join tr_base_contacts tr on tb.cont_id = tr.cont_id) a", string.Format("*,{0} phone ,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")),
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
        #endregion

        #region Event -- 事件
        private void UCCustomerAddOrEdit_Load(object sender, EventArgs e)   //取数据赋值
        {
            try
            {
                InitDataGridCellFormatting();

                ucAttr.TableName = "tb_customer";
                ucAttr.TableNameKeyValue = "cust_id";
                BindControls();
            }
            catch (Exception ex)
            {
                UCCustomerManager.LogService4Customer.WriteLog(1, ex);
            }
        }
        #endregion
    }
}
