using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using SYSModel;
using System.Reflection;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling
{
    public partial class UCSaleBillAddOrEdit : UCBase
    {
        List<partunitprice> list_partunitprice = new List<partunitprice>();
        List<lastunitclass> list_lastunitprice = new List<lastunitclass>();
        private string oldorder_num = string.Empty;
        #region 变量
        WindowStatus status;
        UCSaleBillManang uc;
        string ImportType = "销售订单";//1: 导入的是销售订单，2、3:导入的是销售开单
        int rowIndex = -1;
        int oldindex = -1;
        string sale_billing_id;
        /// <summary>
        /// 通过选择器得到的客户id
        /// </summary>
        string cust_id = string.Empty;
        /// <summary>
        /// 通过选择器得到的客户编码
        /// </summary>
        string cust_code = string.Empty;
        /// <summary>
        /// 结算单位
        /// </summary>
        string balance_unit = string.Empty;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        DataTable dt_parts_brand = null;
        tb_parts_sale_billing tb_salebill_Model = new tb_parts_sale_billing();
        #endregion

        #region 窗体初始化
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="status"></param>
        /// <param name="sale_billing_id"></param>
        /// <param name="uc"></param>
        public UCSaleBillAddOrEdit(WindowStatus status, string sale_billing_id, UCSaleBillManang uc)
        {
            InitializeComponent();
            this.uc = uc;
            this.status = status;
            this.sale_billing_id = sale_billing_id;
            this.Resize += new EventHandler(UCSaleBillAddOrEdit_Resize);
            base.SaveEvent += new ClickHandler(UCSaleBillAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCSaleBillAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCSaleBillAddOrEdit_ImportEvent);
            ddlorder_type.SelectedValueChanged += new EventHandler(ddlorder_type_SelectedValueChanged);
            gvPurchaseList.CellDoubleClick += new DataGridViewCellEventHandler(gvPurchaseList_CellDoubleClick);

            txtreceivables_limit.KeyPress += new KeyPressEventHandler(txtreceivables_limit_KeyPress);
            txtreceivables_limit.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtreceivables_limit_UserControlValueChanged);

            txtcurrent_collect.KeyPress += new KeyPressEventHandler(txtcurrent_collect_KeyPress);
            txtcurrent_collect.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtcurrent_collect_UserControlValueChanged);

            txtcust_arrears.KeyPress += new KeyPressEventHandler(txtcust_arrears_KeyPress);
            txtcust_arrears.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtcust_arrears_UserControlValueChanged);

            txtwhythe_discount.KeyPress += new KeyPressEventHandler(txtwhythe_discount_KeyPress);
            txtwhythe_discount.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtwhythe_discount_UserControlValueChanged);

            txtbalance_money.KeyPress += new KeyPressEventHandler(txtbalance_money_KeyPress);
            txtbalance_money.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtbalance_money_UserControlValueChanged);

            txtreceipt_no.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtreceipt_no_UserControlValueChanged);
            txtcheck_number.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtcheck_number_UserControlValueChanged);

            business_count.ValueType = typeof(decimal);
            business_count.MaxInputLength = 9;
            original_price.ValueType = typeof(decimal);
            original_price.MaxInputLength = 9;
            discount.ValueType = typeof(decimal);
            discount.MaxInputLength = 3;
            business_price.ValueType = typeof(decimal);
            business_price.MaxInputLength = 9;
            tax_rate.ValueType = typeof(decimal);
            tax_rate.MaxInputLength = 3;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSaleBillAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            { base.SetButtonVisiableHandleAddCopy(); }
            else if (status == WindowStatus.Edit)
            { base.SetButtonVisiableHandleEdit(); }

            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "wh_id", "unit_id", "original_price", "business_price", "business_count", "discount", "tax_rate", "is_gift", "make_date", "arrival_date", "remark" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);

            CommonFuncCall.BindWarehouse(wh_id);
            CommonFuncCall.BindUnit(unit_id);
            ddtorder_date.Value = DateTime.Now;
            ddtreceivables_date.Value = DateTime.Now;

            //单据类型
            CommonFuncCall.BindSaleOrderType(ddlorder_type, false, "请选择");
            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_way, "sys_trans_mode", "请选择");
            //发票类型
            CommonFuncCall.BindComBoxDataSource(ddlreceipt_type, "sys_receipt_type", "请选择");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlbalance_way, "请选择");
            //结算账户
            //CommonFuncCall.BindAccount(ddlbalance_account, "", "请选择");
            CommonFuncCall.BindAllAccount(ddlbalance_account, "请选择");

            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");

            dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");

            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(sale_billing_id);
                GetAccessories(sale_billing_id);
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
            }
            gvPurchaseList.Rows.Add(1);
            Choosefrm.CusNameChoose(txtcust_name, Choosefrm.delDataBack = Balance_UnitName_DataBack);
            Choosefrm.CusNameChoose(txtbalance_unit, Choosefrm.delDataBack = null);
        }
        #endregion

        #region 控件事件
        /// <summary> 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleBillAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("保存");
        }
        /// <summary> 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleBillAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("提交");
        }
        /// <summary> 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleBillAddOrEdit_ImportEvent(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cust_id))
            {
                if (ddlorder_type.SelectedValue.ToString() == "1")
                { ImportSaleOrderInfo(); }
                else
                { ImportSaleBillInfo(); }
            }
            else
            { MessageBoxEx.Show("请选择客户名称!"); }
        }
        /// <summary> 选择结算方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlbalance_way_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            //{
            //    string sql_where = " enable_flag='1' and balance_way_id='" + ddlbalance_way.SelectedValue.ToString() + "'";
            //    DataTable dt = DBHelper.GetTable("查询结算方式", "tb_balance_way", "default_account", sql_where, "", "");
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        CommonFuncCall.BindAccount(ddlbalance_account, dt.Rows[0]["default_account"].ToString(), "请选择");
            //    }
            //    else
            //    { CommonFuncCall.BindAccount(ddlbalance_account, "", "请选择"); }
            //}
            //else
            //{ CommonFuncCall.BindAccount(ddlbalance_account, "", "请选择"); }

            if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            {
                string sql_where = " enable_flag='1' and balance_way_id='" + ddlbalance_way.SelectedValue.ToString() + "'";
                DataTable dt = DBHelper.GetTable("查询结算方式", "tb_balance_way", "default_account", sql_where, "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (var item in ddlbalance_account.Items)
                    {
                        if (((ListItem)item).Value.ToString() == dt.Rows[0]["default_account"].ToString())
                        {
                            ddlbalance_account.SelectedValue = dt.Rows[0]["default_account"].ToString();
                            return;
                        }
                    }
                    ddlbalance_account.SelectedValue = "";
                }
                else
                { ddlbalance_account.SelectedValue = ""; }
            }
            else
            { ddlbalance_account.SelectedValue = ""; }
        }
        /// <summary> 选择客户编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcust_name_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo chooseSupplier = new frmCustomerInfo();
            chooseSupplier.ShowDialog();
            string customerid=chooseSupplier.strCustomerId;
            if (!string.IsNullOrEmpty(customerid))
            {
                cust_code = chooseSupplier.strCustomerNo;
                txtcust_name.Text = chooseSupplier.strCustomerName;
                txtbalance_unit.Text = chooseSupplier.strCustomerName;

                DataTable dt = DBHelper.GetTable("查询客户档案信息", "tb_customer", "*", " enable_flag != 0 and cust_id='" + customerid + "'", "", "");
                if (dt.Rows.Count > 0)
                {
                    txtfax.Caption = dt.Rows[0]["cust_fax"].ToString();
                    txtcontacts_tel.Caption = dt.Rows[0]["cust_tel"].ToString();
                }
                string TableName = string.Format(@"(
                                                    select cont_name from tb_contacts where cont_id=
                                                    (select cont_id from tr_base_contacts where relation_object_id='{0}' and is_default='1')
                                                    ) tb_conts", customerid);
                DataTable dt_conts = DBHelper.GetTable("查询客户默认的联系人信息", TableName, "*", "", "", "");
                if (dt_conts != null && dt_conts.Rows.Count > 0)
                {
                    txtcontacts.Caption = dt_conts.Rows[0]["cont_name"].ToString();
                }
                else
                { txtcontacts.Caption = string.Empty; }

                //当客户id不为空，同时变更客户id时，需清空配件信息列表
                if (cust_id != string.Empty && cust_id != chooseSupplier.strCustomerId)
                {
                    if (gvPurchaseList.Rows.Count > 0)
                    {
                        int PurchaseListRows = gvPurchaseList.Rows.Count;
                        for (int i = 0; i < PurchaseListRows; i++)
                        {
                            if (PurchaseListRows > 1)
                            {
                                DeleteListInfoEidtImportStatus(i);
                                i = -1;
                                PurchaseListRows = gvPurchaseList.Rows.Count;
                            }
                        }
                    }
                }
                cust_id = chooseSupplier.strCustomerId;
            }
        }
        /// <summary> 选择结算单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbalance_unit_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo chooseSupplier = new frmCustomerInfo();
            chooseSupplier.ShowDialog();
            if (!string.IsNullOrEmpty(chooseSupplier.strCustomerId))
            {
                balance_unit = chooseSupplier.strCustomerId;
                txtbalance_unit.Text = chooseSupplier.strCustomerName;
            }
        }
        /// <summary> 添加配件信息到列表的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleBillAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmParts frm = new frmParts();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string PartsCode = frm.PartsCode;
                    if (gvPurchaseList.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                        {
                            if (dr.Cells["parts_code"].Value == null)
                            {
                                continue;
                            }
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            if (dr.Cells["parts_code"].Value.ToString() == PartsCode && relation_order == "")
                            {
                                MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
                                return;
                            }
                        }
                    }
                    DataTable dt = GetAccessoriesByCode(PartsCode);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return;
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        //int rowsindex = gvPurchaseList.Rows.Add();
                        int rowsindex = gvPurchaseList.Rows.Count - 1;
                        DataGridViewRow dgvr = gvPurchaseList.Rows[rowsindex];
                        GetGridViewRowByDr(dgvr, dr, "",rowsindex, "Add");
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvPurchaseList.Rows.Add(1);
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldindex = -1; }
        }
        /// <summary> 编辑列表信息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleBillEdit_Click(object sender, EventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 删除列表信息的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleBillDelete_Click(object sender, EventArgs e)
        {
            #region 原来的单个删除的方法
            //if (oldindex > -1)
            //{
            //    try
            //    {
            //        if (gvPurchaseList.Rows[oldindex].Cells["parts_code"].Value != null)
            //        {
            //            DeleteListInfoEidtImportStatus(oldindex);
            //        }
            //    }
            //    catch (Exception ex)
            //    { }
            //    finally
            //    { oldindex = -1; }
            //} 
            #endregion

            try
            {
                List<DataGridViewRow> list_dr = GetSelectedRecord();
                if (list_dr.Count > 0)
                {
                    if (MessageBoxEx.Show("确认要删除选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    { return; }
                    foreach (DataGridViewRow dr in list_dr)
                    {
                        if (dr.Cells["parts_code"].Value != null)
                        {
                            #region 当删除成功时，将销售计划单状态释放为正常
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    if (ImportOrderType == "销售订单")
                                    {
                                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_order", "is_occupy", " order_num='" + relation_order + "'", "", "");
                                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                                        {
                                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                                            if (is_occupy == "1")
                                            {
                                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                                DBHelper.Submit_AddOrEdit("修改销售订单的导入状态为正常", "tb_parts_sale_order", "order_num", relation_order, dicValue);
                                            }
                                        }
                                    }
                                    else if (ImportOrderType == "销售开单")
                                    {
                                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_billing", "is_occupy", " order_num='" + relation_order + "'", "", "");
                                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                                        {
                                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                                            if (is_occupy == "1")
                                            {
                                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                                DBHelper.Submit_AddOrEdit("修改销售开单的导入状态为正常", "tb_parts_sale_billing", "order_num", relation_order, dicValue);
                                            }
                                        }
                                    }
                                }
                            }
                            gvPurchaseList.Rows.Remove(dr);
                            #endregion
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("请勾选要删除的配件!");
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldindex = -1; }
        }
        /// <summary> 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), "请选择");
        }
        /// <summary> 单据类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddlorder_type_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                SetDataGridViewColumnName(ddlorder_type.SelectedValue.ToString());
            }
        }
        /// <summary> 获取鼠标停留行的行索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                oldindex = e.RowIndex;
                gvPurchaseList.Rows[oldindex].Selected = true;
            }
        }
        /// <summary> 单元格内容格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dt_parts_brand.Rows.Count > 0)
            {
                if (e.Value == null || e.Value.ToString().Length == 0)
                {
                    return;
                }
                string fieldNmae = gvPurchaseList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("parts_brand"))
                {
                    for (int i = 0; i < dt_parts_brand.Rows.Count; i++)
                    {
                        if (dt_parts_brand.Rows[i]["dic_id"].ToString() == e.Value.ToString())
                        {
                            gvPurchaseList.Rows[e.RowIndex].Cells["parts_brand_name"].Value = dt_parts_brand.Rows[i]["dic_name"].ToString();
                            break;
                        }
                    }
                }
                if (fieldNmae.Equals("business_count"))
                {
                    string num = gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Value.ToString();
                    num = string.IsNullOrEmpty(num) ? "0" : num;
                    if (Convert.ToDecimal(num) < 0)
                    { gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Style.ForeColor = Color.Red; }
                    else
                    { gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Style.ForeColor = Color.Black; }
                }
            }
        }
        /// <summary> 赠品时单价、金额默认变成0 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string ColumnName = gvPurchaseList.CurrentCell.OwningColumn.Name;
                    if (ColumnName == "is_gift")
                    {
                        //获取控件的值
                        for (int i = 0; i < gvPurchaseList.Rows.Count; i++)
                        {
                            object ischeck = gvPurchaseList.Rows[i].Cells["is_gift"].EditedFormattedValue;
                            if (ischeck != null && (bool)ischeck)
                            {
                                int NewrowIndex = e.RowIndex;
                                //原始单价
                                string original_price = "0";
                                gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value = original_price;
                                //业务单价
                                GetBusiness_priceByOriginal_price(original_price, NewrowIndex);
                                //货款
                                GetPayment(NewrowIndex);
                                //税款
                                GetTax(NewrowIndex);
                                //税价合计
                                GetValorem_Together(NewrowIndex);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
            finally
            {
                rowIndex = -1;
            }
        }
        /// <summary> 双击列表，弹出配件选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvPurchaseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 统计数量、金额的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleBillAddOrEdit_Resize(object sender, EventArgs e)
        {
            string[] total = { business_count.Name, tax.Name, payment.Name, valorem_together.Name };
            ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            if (string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请选择单据类型!");
                return false;
            }
            if (string.IsNullOrEmpty(cust_id))
            {
                MessageBoxEx.Show("请选择客户名称!"); return false;
            }
            if (string.IsNullOrEmpty(ddlreceipt_type.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请选择发票类型!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtcurrent_collect.Caption.Trim()))
            {
                if (Convert.ToDecimal(txtcurrent_collect.Caption.Trim()) != 0)
                {
                    if (string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
                    {
                        MessageBoxEx.Show("本次现收不为0的情况下,请选择结算方式!");
                        return false;
                    }
                }
            }
            if (!txtcontacts_tel.Verify(true))
            { return false; }
            if (!txtfax.Verify(true))
            { return false; }
            //当销售开单时，业务数量必须全部大于0，否则验证不通过
            if (ddlorder_type.SelectedValue.ToString() == "1")
            {
                if (!VerifBusinessCount("1"))
                {
                    MessageBoxEx.Show("销售开单时,业务数量必须全部大于0 !");
                    return false;
                }
            }
            //当销售退货时，业务数量必须全部小于0，否则验证不通过
            else if (ddlorder_type.SelectedValue.ToString() == "2")
            {
                if (!VerifBusinessCount("2"))
                {
                    MessageBoxEx.Show("销售退货时,业务数量必须全部小于0 !");
                    return false;
                }
            }
            //当销售换货时，业务数量必须同时含有大于0和小于0的信息，否则验证不通过
            else if (ddlorder_type.SelectedValue.ToString() == "3")
            {
                if (!VerifBusinessCount("3"))
                {
                    MessageBoxEx.Show("销售换货时,业务数量必须同时存在大于0和小于0的配件!");
                    return false;
                }
            }

            //foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            //{
            //     if (dr.Cells["parts_code"].Value == null)
            //     {
            //         continue;
            //     }
            //     string wh_id = CommonCtrl.IsNullToString(dr.Cells["wh_id"].Value);//配件仓库
            //    if (wh_id.Length == 0)
            //    {
            //        MessageBoxEx.Show("请选择配件仓库!");
            //        gvPurchaseList.CurrentCell = dr.Cells["wh_id"];
            //        return false;
            //    }
            //    string SelectUnit = CommonCtrl.IsNullToString(dr.Cells["unit_id"].Value);//配件单位
            //    if (SelectUnit.Length == 0)
            //    {
            //        MessageBoxEx.Show("请选择配件单位!");
            //        gvPurchaseList.CurrentCell = dr.Cells["unit_id"];
            //        return false;
            //    }
            //}
            return true;
        }
        /// <summary>
        /// 加载销售计划信息和配件信息
        /// </summary>
        /// <param name="sale_billing_id"></param>
        private void LoadInfo(string sale_billing_id)
        {
            if (!string.IsNullOrEmpty(sale_billing_id))
            {
                //1.查看一条销售开单信息
                DataTable dt = DBHelper.GetTable("查看一条销售开单信息", "tb_parts_sale_billing", "*", " sale_billing_id='" + sale_billing_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_salebill_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_salebill_Model, "");
                    cust_id = tb_salebill_Model.cust_id;
                    cust_code = tb_salebill_Model.cust_code;
                    txtcust_name.Text = tb_salebill_Model.cust_name;
                    balance_unit = tb_salebill_Model.balance_unit;
                    txtbalance_unit.Text = tb_salebill_Model.balance_unit_name;
                    oldorder_num = tb_salebill_Model.order_num;
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = "";
                        txtbalance_money.Caption = "0";
                    }
                    lblcreate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.create_time.ToString())).ToString();
                    if (tb_salebill_Model.update_time > 0)
                    { lblupdate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary> 
        /// 根据销售开单号获取配件信息
        /// </summary>
        /// <param name="sale_billing_id"></param>
        private void GetAccessories(string sale_billing_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_sale = DBHelper.GetTable("查询销售开单配件表信息", "tb_parts_sale_billing_p", "*", " sale_billing_id='" + sale_billing_id + "'", "", "");
            if (dt_parts_sale != null && dt_parts_sale.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_parts_sale.Rows)
                {
                    string default_price = "0";
                    string default_unit_id = string.Empty;
                    string data_source = string.IsNullOrEmpty(dr["car_factory_code"].ToString()) ? "1" : "2";
                    DataGridViewComboBoxCell dgcombox = null;
                    int CurrentRowIndenx = gvPurchaseList.Rows.Add();
                    DataGridViewRow dgvr = gvPurchaseList.Rows[CurrentRowIndenx];

                    dgvr.Cells["wh_id"].Value = dr["wh_id"] == null ? "" : dr["wh_id"].ToString();
                    dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];
                    dgvr.Cells["model"].Value = dr["model"];
                    dgvr.Cells["parts_brand"].Value = dr["parts_brand"];
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];
                    BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), CurrentRowIndenx, ref dgcombox, ref default_unit_id, ref default_price);
                    dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                    dgvr.Cells["business_count"].Value = dr["business_count"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["tax_rate"].Value = dr["tax_rate"];
                    dgvr.Cells["tax"].Value = dr["tax"];
                    dgvr.Cells["payment"].Value = dr["payment"];
                    dgvr.Cells["valorem_together"].Value = dr["valorem_together"];//税价合计
                    dgvr.Cells["is_gift"].Value = dr["is_gift"];

                    if (Convert.ToInt64(dr["make_date"].ToString() == "" ? "0" : dr["make_date"].ToString()) > 0)
                    {
                        dgvr.Cells["make_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["make_date"]));//生产日期
                    }
                    if (Convert.ToInt64(dr["arrival_date"].ToString() == "" ? "0" : dr["arrival_date"].ToString()) > 0)
                    {
                        dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"]));//到期日期
                    }
                    dgvr.Cells["return_count"].Value = dr["return_count"];//退货数量
                    dgvr.Cells["library_count"].Value = dr["library_count"];//出库数量
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//车厂配件编码
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["auxiliary_count"].Value = dr["auxiliary_count"];//辅助数量

                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                    if (status == WindowStatus.Copy)
                    {
                        dgvr.Cells["relation_order"].Value = string.Empty;
                        dgvr.Cells["ImportOrderType"].Value = string.Empty;
                    }
                    else
                    {
                        dgvr.Cells["relation_order"].Value = dr["relation_order"];//引用单号
                        dgvr.Cells["ImportOrderType"].Value = dr["ImportOrderType"];
                    }
                }
            }
        }
        /// <summary>
        /// 在编辑和添加时对主数据和销售开单配件表中的配件信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="sale_billing_id"></param>
        private void DealAccessories(List<SysSQLString> listSql, string sale_billing_id)
        {
            string PartsCodes = GetListPartsCodes();
            DataTable dt_CarType = CommonFuncCall.GetCarType(PartsCodes);
            DataTable dt_PartsType = CommonFuncCall.GetPartsType(PartsCodes);

            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql1 = "delete from tb_parts_sale_billing_p where sale_billing_id=@sale_billing_id;";
            dic.Add("sale_billing_id", sale_billing_id);
            sysStringSql.sqlString = sql1;
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);
            if (gvPurchaseList.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        continue;
                    }
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    dic.Add("sale_billing_parts_id", Guid.NewGuid().ToString());
                    dic.Add("sale_billing_id", sale_billing_id);
                    if (dr.Cells["wh_id"].Value == null)
                    {
                        dic.Add("wh_id", string.Empty);
                        dic.Add("wh_name", string.Empty);
                    }
                    else
                    {
                        dic.Add("wh_id", dr.Cells["wh_id"].Value.ToString());
                        dic.Add("wh_name", dr.Cells["wh_id"].EditedFormattedValue.ToString());
                    }
                    string parts_code = string.Empty;
                    if (dr.Cells["parts_code"].Value == null)
                    { parts_code = string.Empty; }
                    else
                    { parts_code = dr.Cells["parts_code"].Value.ToString(); }

                    dic.Add("parts_id", dr.Cells["parts_id"].Value == null ? "" : dr.Cells["parts_id"].Value.ToString());
                    dic.Add("parts_code", parts_code);
                    dic.Add("parts_name", dr.Cells["parts_name"].Value == null ? "" : dr.Cells["parts_name"].Value.ToString());
                    dic.Add("model", dr.Cells["model"].Value == null ? "" : dr.Cells["model"].Value.ToString());
                    dic.Add("drawing_num", dr.Cells["drawing_num"].Value == null ? "" : dr.Cells["drawing_num"].Value.ToString());
                    if (dr.Cells["unit_id"].Value == null)
                    {
                        dic.Add("unit_id", string.Empty);
                        dic.Add("unit_name", string.Empty);
                    }
                    else
                    {
                        dic.Add("unit_id", dr.Cells["unit_id"].Value.ToString());
                        dic.Add("unit_name", dr.Cells["unit_id"].EditedFormattedValue.ToString());
                    }
                    dic.Add("parts_brand", dr.Cells["parts_brand"].Value == null ? "" : dr.Cells["parts_brand"].Value.ToString());
                    dic.Add("parts_brand_name", dr.Cells["parts_brand_name"].Value == null ? "" : dr.Cells["parts_brand_name"].Value.ToString());
                    dic.Add("business_count", dr.Cells["business_count"].Value == null ? "0" : dr.Cells["business_count"].Value.ToString() == "" ? "0" : dr.Cells["business_count"].Value.ToString());
                    dic.Add("original_price", dr.Cells["original_price"].Value == null ? "0" : dr.Cells["original_price"].Value.ToString() == "" ? "0" : dr.Cells["original_price"].Value.ToString());
                    dic.Add("discount", dr.Cells["discount"].Value == null ? "0" : dr.Cells["discount"].Value.ToString() == "" ? "0" : dr.Cells["discount"].Value.ToString());
                    dic.Add("business_price", dr.Cells["business_price"].Value == null ? "0" : dr.Cells["business_price"].Value.ToString() == "" ? "0" : dr.Cells["business_price"].Value.ToString());
                    dic.Add("tax_rate", dr.Cells["tax_rate"].Value == null ? "0" : dr.Cells["tax_rate"].Value.ToString() == "" ? "0" : dr.Cells["tax_rate"].Value.ToString());
                    dic.Add("tax", dr.Cells["tax"].Value == null ? "0" : dr.Cells["tax"].Value.ToString() == "" ? "0" : dr.Cells["tax"].Value.ToString());
                    dic.Add("payment", dr.Cells["payment"].Value == null ? "0" : dr.Cells["payment"].Value.ToString() == "" ? "0" : dr.Cells["payment"].Value.ToString());
                    dic.Add("valorem_together", dr.Cells["valorem_together"].Value == null ? "0" : dr.Cells["valorem_together"].Value.ToString() == "" ? "0" : dr.Cells["valorem_together"].Value.ToString());//价税合计
                    dic.Add("auxiliary_count", dr.Cells["auxiliary_count"].Value == null ? "0" : dr.Cells["auxiliary_count"].Value.ToString() == "" ? "0" : dr.Cells["auxiliary_count"].Value.ToString());//辅助数量
                    dic.Add("relation_order", dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString());//引用单号
                    dic.Add("is_gift", dr.Cells["is_gift"].Value == null ? "1" : dr.Cells["is_gift"].Value.ToString());
                    if (dr.Cells["make_date"].Value != null)
                    {
                        dic.Add("make_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(Convert.ToDateTime(dr.Cells["make_date"].Value.ToString()).ToShortDateString() + " 23:59:59")).ToString());
                    }
                    else
                    {
                        dic.Add("make_date", "0");
                    }
                    if (dr.Cells["arrival_date"].Value != null)
                    {
                        dic.Add("arrival_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(Convert.ToDateTime(dr.Cells["arrival_date"].Value.ToString()).ToShortDateString() + " 23:59:59")).ToString());
                    }
                    else
                    {
                        dic.Add("arrival_date", "0");
                    }

                    dic.Add("remark", dr.Cells["remark"].Value == null ? "" : dr.Cells["remark"].Value.ToString());
                    dic.Add("return_count", dr.Cells["return_count"].Value == null ? "0" : dr.Cells["return_count"].Value.ToString() == "" ? "0" : dr.Cells["return_count"].Value.ToString());//退货业务数量
                    dic.Add("library_count", dr.Cells["library_count"].Value == null ? "0" : dr.Cells["library_count"].Value.ToString() == "" ? "0" : dr.Cells["library_count"].Value.ToString());//入库数量
                    dic.Add("car_factory_code", dr.Cells["car_factory_code"].Value == null ? "" : dr.Cells["car_factory_code"].Value.ToString());//车厂配件编码

                    dic.Add("create_by", dr.Cells["create_by"].Value == null ? "" : dr.Cells["create_by"].Value.ToString());
                    dic.Add("create_name", dr.Cells["create_name"].Value == null ? "" : dr.Cells["create_name"].Value.ToString());
                    dic.Add("create_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["create_time"].Value == null ? DateTime.Now.ToString() : dr.Cells["create_time"].Value.ToString())).ToString());
                    dic.Add("update_by", GlobalStaticObj.UserID);
                    dic.Add("update_name", GlobalStaticObj.UserName);
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                    dic.Add("ImportOrderType", dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString());
                    dic.Add("finish_count", "0");

                    string vm_name = string.Empty;
                    if (dt_CarType != null && dt_CarType.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(parts_code))
                        {
                            DataRow[] dr_cartype = dt_CarType.Select(" ser_parts_code='" + parts_code + "'");
                            if (dr_cartype.Length > 0)
                            {
                                foreach (DataRow item in dr_cartype)
                                {
                                    vm_name += "" + item["vm_name"].ToString() + ",";
                                }
                            }
                        }
                    }
                    vm_name = vm_name.Trim(',');
                    string parts_type_id = string.Empty;
                    string parts_type_name = string.Empty;
                    if (dt_PartsType != null && dt_PartsType.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(parts_code))
                        {
                            DataRow[] dr_partstype = dt_PartsType.Select(" ser_parts_code='" + parts_code + "'");
                            if (dr_partstype.Length > 0)
                            {
                                parts_type_id = dr_partstype[0]["dic_id"].ToString();
                                parts_type_name = dr_partstype[0]["dic_name"].ToString();
                            }
                        }
                    }
                    dic.Add("vm_name", vm_name);
                    dic.Add("parts_type_id", parts_type_id);
                    dic.Add("parts_type_name", parts_type_name);

                    string sql2 = string.Format(@"Insert Into tb_parts_sale_billing_p(sale_billing_parts_id,sale_billing_id,wh_id,wh_name,
                    parts_id,parts_code,parts_name,model,drawing_num,unit_id,unit_name,parts_brand,parts_brand_name,business_count,original_price,discount,business_price,
                    tax_rate,tax,payment,valorem_together,auxiliary_count,relation_order,is_gift,make_date,arrival_date,remark,
                    return_count,library_count,car_factory_code,create_by,create_name,create_time,update_by,update_name,
                    update_time,ImportOrderType,finish_count,vm_name,parts_type_id,parts_type_name) values(@sale_billing_parts_id,@sale_billing_id,@wh_id,@wh_name,
                    @parts_id,@parts_code,@parts_name,@model,@drawing_num,@unit_id,@unit_name,@parts_brand,@parts_brand_name,@business_count,@original_price,@discount,@business_price,
                    @tax_rate,@tax,@payment,@valorem_together,@auxiliary_count,@relation_order,@is_gift,@make_date,@arrival_date,@remark,
                    @return_count,@library_count,@car_factory_code,@create_by,@create_name,@create_time,@update_by,@update_name,@update_time,
                    @ImportOrderType,@finish_count,@vm_name,@parts_type_id,@parts_type_name);");
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
        }
        /// <summary> 批量删除配件信息成功时，将oldorderstatus计划单状态释放为正常，并清空列表
        /// </summary>
        /// <param name="rowindex"></param>
        void DeleteListInfoEidtImportStatus(int rowindex)
        {
            #region 当删除成功时，将销售计划单状态释放为正常
            DataGridViewRow dr = gvPurchaseList.Rows[rowindex];
            if (dr.Cells["parts_code"].Value == null)
            {
                return;
            }
            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
            string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
            if (!string.IsNullOrEmpty(relation_order))
            {
                if (!CheckPre_order_code(relation_order))
                {
                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                    if (ImportOrderType == "销售订单")
                    {
                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_order", "is_occupy", " order_num='" + relation_order + "'", "", "");
                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                        {
                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                            if (is_occupy == "1")
                            {
                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                DBHelper.Submit_AddOrEdit("修改销售订单的导入状态为正常", "tb_parts_sale_order", "order_num", relation_order, dicValue);
                            }
                        }
                    }
                    else if (ImportOrderType == "销售开单")
                    {
                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_billing", "is_occupy", " order_num='" + relation_order + "'", "", "");
                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                        {
                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                            if (is_occupy == "1")
                            {
                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                DBHelper.Submit_AddOrEdit("修改销售开单的导入状态为正常", "tb_parts_sale_billing", "order_num", relation_order, dicValue);
                            }
                        }
                    }
                }
            }
            gvPurchaseList.Rows.Remove(gvPurchaseList.Rows[rowindex]);
            #endregion
        }
        /// <summary> 根据ID获取配件信息
        /// </summary>
        /// <param name="PartsID"></param>
        /// <returns></returns>
        private DataTable GetAccessoriesByCode(string PartsCode)
        {
            //有问题，需要修改
            DataTable dt = DBHelper.GetTable("", "tb_parts", "*", string.Format("ser_parts_code='{0}'", PartsCode), "", "");
            return dt;
        }
        /// <summary> 导入销售订单配件信息(销售收货)
        /// </summary>
        void ImportSaleOrderInfo()
        {
            try
            {
                UCChooseSaleOrder frm = new UCChooseSaleOrder(cust_id,"");
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<PartsInfoClassBySaleOrder> List_PartInfo = frm.List_PartInfo;
                    DataTable dt_SaleOrder = frm.dt_SaleOrder;
                    if (List_PartInfo.Count > 0)
                    {
                        if (ddlorder_type.SelectedValue.ToString() == "1")
                        {
                            ImportType = "销售订单";
                        }
                        else if (ddlorder_type.SelectedValue.ToString() == "2" || ddlorder_type.SelectedValue.ToString() == "3")
                        {
                            ImportType = "销售开单";
                        }
                        for (int i = 0; i < List_PartInfo.Count; i++)
                        {
                            bool IsExist = false;//判断要导入的信息是否已经在列表中存在，默认是不存在
                            string sale_order_id = List_PartInfo[i].sale_order_id;
                            string ordercode = string.Empty;
                            string partscode = List_PartInfo[i].parts_code;
                            #region 获取引用单号
                            if (dt_SaleOrder != null && dt_SaleOrder.Rows.Count > 0)
                            {
                                for (int a = 0; a < dt_SaleOrder.Rows.Count; a++)
                                {
                                    if (dt_SaleOrder.Rows[a]["sale_order_id"].ToString() == sale_order_id)
                                    {
                                        ordercode = dt_SaleOrder.Rows[a]["order_num"].ToString();
                                        ordercode = string.IsNullOrEmpty(ordercode) ? sale_order_id : ordercode;
                                        break;
                                    }
                                }
                            }
                            #endregion
                            #region 判断配件信息是否已经存在于列表
                            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                            {
                                if (dr.Cells["parts_code"].Value == null)
                                {
                                    continue;
                                }
                                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                                if (dr.Cells["parts_code"].Value.ToString() == partscode && relation_order == ordercode)
                                {
                                    IsExist = true;
                                    break;
                                }
                            }
                            #endregion
                            #region 当不存在时，添加到配件列表中
                            if (!IsExist)
                            {
                                DataTable dt = DBHelper.GetTable("查询销售订单配件表信息", "tb_parts_sale_order_p", "*", " sale_order_id='" + sale_order_id + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
                                if (dt == null || dt.Rows.Count == 0)
                                {
                                    break;
                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //int rowsindex = gvPurchaseList.Rows.Add();
                                    int rowsindex = gvPurchaseList.Rows.Count - 1;
                                    DataGridViewRow dgvr = gvPurchaseList.Rows[rowsindex];
                                    GetGridViewRowByDrImport(dgvr, dr, ordercode, "销售订单",rowsindex, ImportType);
                                }
                                gvPurchaseList.Rows.Add(1);
                            }
                            #endregion
                            #region 当添加成功时，将销售订单状态设置成占用，使前置单据不可以编辑、删除
                            DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_order", "is_occupy", " order_num='" + ordercode + "'", "", "");
                            if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                            {
                                string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                                if (is_occupy == "0")
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    dicValue.Add("is_occupy", "1");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                    DBHelper.Submit_AddOrEdit("修改销售订单的导入状态为占用", "tb_parts_sale_order", "order_num", ordercode, dicValue);
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 导入销售开单配件信息(销售换货、销售退货)
        /// </summary>
        void ImportSaleBillInfo()
        {
            UCChooseSaleBill frm = new UCChooseSaleBill("1", cust_id);//销售收货
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<PartsInfoClassBySaleBill> List_PartInfo = frm.List_PartInfo;
                DataTable dt_SaleBill = frm.dt_SaleBill;
                if (List_PartInfo.Count > 0)
                {
                    if (ddlorder_type.SelectedValue.ToString() == "1")
                    {
                        ImportType = "销售订单";
                    }
                    else if (ddlorder_type.SelectedValue.ToString() == "2" || ddlorder_type.SelectedValue.ToString() == "3")
                    {
                        ImportType = "销售开单";
                    }
                    for (int i = 0; i < List_PartInfo.Count; i++)
                    {
                        bool IsExist = false;//判断要导入的信息是否已经在列表中存在，默认是不存在
                        string billID = List_PartInfo[i].sale_billing_id;
                        string billcode = string.Empty;
                        string partscode = List_PartInfo[i].parts_code;
                        #region 获取引用单号
                        if (dt_SaleBill != null && dt_SaleBill.Rows.Count > 0)
                        {
                            for (int a = 0; a < dt_SaleBill.Rows.Count; a++)
                            {
                                if (dt_SaleBill.Rows[a]["sale_billing_id"].ToString() == billID)
                                {
                                    billcode = dt_SaleBill.Rows[a]["order_num"].ToString();
                                    billcode = string.IsNullOrEmpty(billcode) ? billID : billcode;
                                    break;
                                }
                            }
                        }
                        #endregion
                        #region 判断配件信息是否已经存在于列表
                        foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                        {
                            if (dr.Cells["parts_code"].Value == null)
                            {
                                continue;
                            }
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            if (dr.Cells["parts_code"].Value.ToString() == partscode && relation_order == billcode)
                            {
                                IsExist = true;
                                break;
                            }
                        }
                        #endregion
                        #region 当不存在时，添加到配件列表中
                        if (!IsExist)
                        {
                            DataTable dt = DBHelper.GetTable("查询销售开单配件表信息", "tb_parts_sale_billing_p", "*", " sale_billing_id='" + billID + "' and parts_code='" + partscode + "' and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                break;
                            }
                            foreach (DataRow dr in dt.Rows)
                            {
                                //int rowsindex = gvPurchaseList.Rows.Add();
                                int rowsindex = gvPurchaseList.Rows.Count - 1;
                                DataGridViewRow dgvr = gvPurchaseList.Rows[rowsindex];
                                GetGridViewRowByDrImport(dgvr, dr, billcode, "销售开单",rowsindex, ImportType);
                            }
                            gvPurchaseList.Rows.Add(1);
                        }
                        #endregion
                        #region 当添加成功时，将销售开单中的，退货/换货状态设置成占用，使前置单据不可以编辑、删除
                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_billing", "is_occupy", " order_num='" + billcode + "'", "", "");
                         if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                         {
                             string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                             if (is_occupy == "0")
                             {
                                 Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                 dicValue.Add("is_occupy", "1");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                 DBHelper.Submit_AddOrEdit("修改销售开单的导入状态为占用", "tb_parts_sale_billing", "order_num", billcode, dicValue);
                             }
                         }
                        #endregion
                    }
                }
            }
        }
        /// <summary>批量修改销售订单导入状态 
        /// </summary>
        /// <param name="status">保存时：0 释放为正常，提交时：2 锁定</param>
        void ImportSaleOrderStatus(string status)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                if (!string.IsNullOrEmpty(relation_order))
                {
                    listField.Add(relation_order);
                }
            }
            Dictionary<string, string> Field = new Dictionary<string, string>();
            Field.Add("is_occupy", status);//单据导入状态，0正常，1占用，2锁定
            DBHelper.BatchUpdateDataByIn("批量修改销售订单导入状态", "tb_parts_sale_order", Field, "order_num", listField.ToArray());
        }
        /// <summary>批量修改销售开单导入状态 
        /// </summary>
        /// <param name="status">保存时：0 释放为正常，提交时：2 锁定</param>
        void ImportSaleBillStatus(string status)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                if (!string.IsNullOrEmpty(relation_order))
                {
                    listField.Add(relation_order);
                }
            }
            Dictionary<string, string> Field = new Dictionary<string, string>();
            Field.Add("is_occupy", status);//单据导入状态，0正常，1占用，2锁定
            DBHelper.BatchUpdateDataByIn("批量修改销售开单导入状态", "tb_parts_sale_billing", Field, "order_num", listField.ToArray());
        }
        /// <summary> 单个添加的方法
        /// </summary>
        /// <param name="dgvr"></param>
        /// <param name="dr"></param>
        /// <param name="relation_order"></param>
        /// <param name="HandleType"></param>
        void GetGridViewRowByDr(DataGridViewRow dgvr, DataRow dr, string relation_order, int rowIndex, string HandleType)
        {
            try
            {
                string default_price = "0";
                string default_unit_id = string.Empty;
                string data_source = dr["data_source"].ToString();
                DataGridViewComboBoxCell dgcombox = null;
                dgvr.Cells["wh_id"].Value = "";//仓库(默认请选择)
                dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                dgvr.Cells["parts_code"].Value = dr["ser_parts_code"];//配件编码
                dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                BindDataGridViewComboBoxCell(data_source, dr["ser_parts_code"].ToString(), rowIndex, ref dgcombox, ref default_unit_id, ref default_price);
                dgvr.Cells["unit_id"].Value = default_unit_id;//单位
                //dgvr.Cells["unit_id"].Value = dr["default_unit"];//单位

                dgvr.Cells["original_price"].Value = default_price;//原始单价
                dgvr.Cells["discount"].Value = string.IsNullOrEmpty(txtwhythe_discount.Caption.Trim()) ? "100" : txtwhythe_discount.Caption.Trim();;//折扣
                dgvr.Cells["business_price"].Value = default_price;//业务单价

                dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌
                dgvr.Cells["model"].Value = dr["model"];//规格型号
                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码
                dgvr.Cells["relation_order"].Value = relation_order;

                if (HandleType == "Add")
                {
                    dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                    dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                    dgvr.Cells["create_time"].Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 批量导入的方式
        /// </summary>
        /// <param name="dgvr"></param>
        /// <param name="dr"></param>
        /// <param name="relation_order"></param>
        /// <param name="HandleType"></param>
        void GetGridViewRowByDrImport(DataGridViewRow dgvr, DataRow dr, string relation_order, string OrderTypeName, int rowIndex, string ImportOrderType)
        {
            try
            {
                string default_price = "0";
                string default_unit_id = string.Empty;
                string data_source = string.IsNullOrEmpty(dr["car_factory_code"].ToString()) ? "1" : "2";
                DataGridViewComboBoxCell dgcombox = null;
                if (OrderTypeName == "销售开单")
                {
                    dgvr.Cells["wh_id"].Value = dr["wh_id"] == null ? "" : dr["wh_id"].ToString();//仓库
                }
                else if (OrderTypeName == "销售订单")
                {
                    dgvr.Cells["wh_id"].Value = "";
                }
                dgvr.Cells["original_price"].Value = dr["original_price"];//原始单价
                dgvr.Cells["discount"].Value = dr["discount"];//折扣%
                dgvr.Cells["business_price"].Value = dr["business_price"];//业务单价
                dgvr.Cells["tax_rate"].Value = dr["tax_rate"];//税率
                dgvr.Cells["tax"].Value = dr["tax"];//税款
                dgvr.Cells["payment"].Value = dr["payment"];//货款
                dgvr.Cells["valorem_together"].Value = dr["valorem_together"];//税价合计
                dgvr.Cells["is_gift"].Value = dr["is_gift"];
                //if (Convert.ToInt64(dr["arrival_date"]) > 0)
                //{
                //    dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"]));//到期日期
                //}
                dgvr.Cells["remark"].Value = dr["remark"];

                dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                dgvr.Cells["parts_code"].Value = dr["parts_code"];//配件编码
                dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), rowIndex, ref dgcombox, ref default_unit_id, ref default_price);
                dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌

                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码

                dgvr.Cells["relation_order"].Value = relation_order;
                dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                dgvr.Cells["create_time"].Value = DateTime.Now;
                dgvr.Cells["ImportOrderType"].Value = ImportOrderType;

                //dgvr.Cells["business_count"].Value = dr["business_count"];//业务数量
                //如果选择的是退货，自动变负数
                if (ddlorder_type.SelectedValue.ToString() == "2")
                {
                    decimal ret = decimal.Parse(dr["business_count"].ToString() == "" ? "0" : dr["business_count"].ToString()) - decimal.Parse(dr["finish_count"].ToString() == "" ? "0" : dr["finish_count"].ToString());//剩余的 业务数量
                    if (ret > 0)
                    { dgvr.Cells["business_count"].Value = -ret; }
                    else
                    { dgvr.Cells["business_count"].Value = ret; }
                    //货款
                    GetPaymentByBusiness_counts(dgvr.Cells["business_count"].Value.ToString(), rowIndex);
                    //税款
                    GetTax(rowIndex);
                    //税价合计
                    GetValorem_Together(rowIndex);
                }
                else
                {
                    dgvr.Cells["business_count"].Value = decimal.Parse(dr["business_count"].ToString() == "" ? "0" : dr["business_count"].ToString()) - decimal.Parse(dr["finish_count"].ToString() == "" ? "0" : dr["finish_count"].ToString());//剩余的 业务数量
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 数据保存/提交 方法
        /// </summary>
        /// <param name="HandleTypeName">保存/提交</param>
        private void SaveEventOrSubmitEventFunction(string HandleTypeName)
        {
            try
            {
                gvPurchaseList.EndEdit();
                if (CheckDataInfo())
                {
                    string SucessMess = "保存";
                    string opName = "销售开单操作";
                    if (HandleTypeName == "提交")
                    {
                        SucessMess = "提交";
                        #region 当操作类型为提交时，先要验证各个要导入的配件数量是否大于配件可导入的剩余数量，如果大于，弹出提示，不可以导入
                        DataTable dt_surplus = GetSurplusCount();
                        if (dt_surplus != null && dt_surplus.Rows.Count > 0)
                        {
                            if (gvPurchaseList.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow gvdr in gvPurchaseList.Rows)
                                {
                                    if (gvdr.Cells["relation_order"].Value == null || gvdr.Cells["relation_order"].Value.ToString() == "")
                                    {
                                        break;
                                    }
                                    if (gvdr.Cells["parts_code"].Value == null || gvdr.Cells["parts_code"].Value.ToString() == "")
                                    {
                                        break;
                                    }
                                    string order_num = gvdr.Cells["relation_order"].Value == null ? "" : gvdr.Cells["relation_order"].Value.ToString();
                                    string parts_code = gvdr.Cells["parts_code"].Value == null ? "" : gvdr.Cells["parts_code"].Value.ToString();
                                    int business_count = int.Parse(gvdr.Cells["business_count"].Value == null ? "" : gvdr.Cells["business_count"].Value.ToString());
                                    int SurplusCount = 10000000;//设置默认剩余数量为1000000
                                    DataRow[] dr = dt_surplus.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                                    if (dr != null && dr.Count() > 0)
                                    {
                                        SurplusCount = int.Parse(dr[0]["SurplusCount"].ToString()) < 0 ? 0 : int.Parse(dr[0]["SurplusCount"].ToString());
                                    }
                                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                                    {
                                        //当执行过程为添加或复制时，直接判断剩余可导入的配件数量
                                        SurplusCount = int.Parse(dr[0]["SurplusCount"].ToString()) < 0 ? 0 : int.Parse(dr[0]["SurplusCount"].ToString());
                                        if (business_count > SurplusCount)
                                        {
                                            MessageBoxEx.Show("引用单号为:" + order_num + ",配件编码为:" + parts_code + "的业务数量超出剩余数量，请修改!");
                                            return;
                                        }
                                    }
                                    else if (status == WindowStatus.Edit)
                                    {
                                        //当执行过程为编辑时，要把本单据中原先已经存在的配件导入数量考虑在内，不可以直接判断剩余可导入的配件数量
                                        //公式为: 本次输入的数量<=本单据中原先保存的数量+剩余数量
                                        DataTable dt_old = GetThisBusinessCount(sale_billing_id, order_num, parts_code);
                                        if (dt_old != null && dt_old.Rows.Count > 0)
                                        {
                                            DataRow[] dr_old = dt_old.Select(" relation_order='" + order_num + "' and parts_code='" + parts_code + "'");
                                            if (dr_old != null && dr_old.Count() > 0)
                                            {
                                                int oldcount = int.Parse(dr_old[0]["business_count"].ToString()) < 0 ? 0 : int.Parse(dr_old[0]["business_count"].ToString());
                                                if (business_count > oldcount + SurplusCount)
                                                {
                                                    MessageBoxEx.Show("引用单号为:" + order_num + ",配件编码为:" + parts_code + "的业务数量超出剩余数量，请修改!");
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        if (lblorder_num.Text == "")
                        {
                            lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.SaleOpenOrder);//获取销售开单编号
                            if (oldorder_num == lblorder_num.Text)
                            {
                                MessageBoxEx.Show("复制的单据生成的编号和原单据编号重复了，原单号是:" + oldorder_num + "，新单号是:" + lblorder_num.Text + "");
                            }
                        }
                        lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                    }

                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        sale_billing_id = Guid.NewGuid().ToString();
                        AddSaleOrderSqlString(listSql, sale_billing_id, HandleTypeName);
                        opName = "新增销售开单";
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditSaleOrderSqlString(listSql, sale_billing_id, tb_salebill_Model, HandleTypeName);
                        opName = "修改销售开单";
                    }
                    DealAccessories(listSql, sale_billing_id);
                    GetPre_Order_Code(listSql, sale_billing_id, lblorder_num.Text);
                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("" + SucessMess + "成功！");

                        if (HandleTypeName == "保存")
                        {
                            if (ImportType == "销售订单")
                            { ImportSaleOrderStatus("0"); }
                            else if (ImportType == "销售开单")
                            { ImportSaleBillStatus("0"); }
                        }
                        else if (HandleTypeName == "提交")
                        {
                            SetOrderStatus(sale_billing_id);
                        }
                        uc.BindgvPurchaseOrderList();
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                    else
                    {
                        MessageBoxEx.Show("" + SucessMess + "失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        /// <summary> 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="sale_billing_id"></param>
        private void AddSaleOrderSqlString(List<SysSQLString> listSql, string sale_billing_id, string HandleType)
        {
            decimal allmoney = 0;
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            ddtreceivables_date.Value = Convert.ToDateTime(ddtreceivables_date.Value.ToShortDateString() + " 23:59:59");
            tb_parts_sale_billing model = new tb_parts_sale_billing();
            CommonFuncCall.SetModelObjectValue(this, model);
            GetAllMoney(ref allmoney);

            model.sale_billing_id = sale_billing_id;
            model.cust_id = cust_id;
            model.cust_code = cust_code;
            model.cust_name = txtcust_name.Text;
            //单据类型
            if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                model.order_type_name = ddlorder_type.SelectedItem.ToString();
            }
            //发票类型
            if (!string.IsNullOrEmpty(ddlreceipt_type.SelectedValue.ToString()))
            {
                model.receipt_type_name = ddlreceipt_type.SelectedItem.ToString();
            }
            //运输方式
            if (!string.IsNullOrEmpty(ddltrans_way.SelectedValue.ToString()))
            {
                model.trans_way_name = ddltrans_way.SelectedItem.ToString();
            }
            //结算方式
            if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            {
                model.balance_way_name = ddlbalance_way.SelectedItem.ToString();
            }
            //结算账户
            if (!string.IsNullOrEmpty(ddlbalance_account.SelectedValue.ToString()))
            {
                model.balance_account_name = ddlbalance_account.SelectedItem.ToString();
            }
            //部门
            if (!string.IsNullOrEmpty(ddlorg_id.SelectedValue.ToString()))
            {
                model.org_id = ddlorg_id.SelectedValue.ToString();
                model.org_name = ddlorg_id.SelectedItem.ToString();
            }
            //经办人
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                model.handle = ddlhandle.SelectedValue.ToString();
                model.handle_name = ddlhandle.SelectedItem.ToString();
            }
            model.balance_unit = balance_unit;
            model.balance_unit_name = txtbalance_unit.Text.Trim();
            model.create_by = GlobalStaticObj.UserID;
            model.create_name = GlobalStaticObj.UserName;
            model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
            model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称
            model.allmoney = allmoney;
            model.is_occupy = "0";
            model.is_lock = "0";
            model.enable_flag = "1";
            if (HandleType == "保存")
            {
                model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
            }
            else if (HandleType == "提交")
            {
                model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
            }
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Insert Into tb_parts_sale_billing( ");
                StringBuilder sp = new StringBuilder();
                StringBuilder sb_prame = new StringBuilder();
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    sb_prame.Append("," + name);
                    sp.Append(",@" + name);
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
                sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")").Append(";");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary> 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="sale_billing_id"></param>
        /// <param name="model"></param>
        private void EditSaleOrderSqlString(List<SysSQLString> listSql, string sale_billing_id, tb_parts_sale_billing model, string HandleType)
        {
            decimal allmoney = 0;
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            ddtreceivables_date.Value = Convert.ToDateTime(ddtreceivables_date.Value.ToShortDateString() + " 23:59:59");
            CommonFuncCall.SetModelObjectValue(this, model);
            GetAllMoney(ref allmoney);

            model.cust_id = cust_id;
            model.cust_code = cust_code;
            model.cust_name = txtcust_name.Text;
            //单据类型
            if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                model.order_type_name = ddlorder_type.SelectedItem.ToString();
            }
            //发票类型
            if (!string.IsNullOrEmpty(ddlreceipt_type.SelectedValue.ToString()))
            {
                model.receipt_type_name = ddlreceipt_type.SelectedItem.ToString();
            }
            //运输方式
            if (!string.IsNullOrEmpty(ddltrans_way.SelectedValue.ToString()))
            {
                model.trans_way_name = ddltrans_way.SelectedItem.ToString();
            }
            //结算方式
            if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            {
                model.balance_way_name = ddlbalance_way.SelectedItem.ToString();
            }
            //结算账户
            if (!string.IsNullOrEmpty(ddlbalance_account.SelectedValue.ToString()))
            {
                model.balance_account_name = ddlbalance_account.SelectedItem.ToString();
            }
            //部门
            if (!string.IsNullOrEmpty(ddlorg_id.SelectedValue.ToString()))
            {
                model.org_id = ddlorg_id.SelectedValue.ToString();
                model.org_name = ddlorg_id.SelectedItem.ToString();
            }
            //经办人
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                model.handle = ddlhandle.SelectedValue.ToString();
                model.handle_name = ddlhandle.SelectedItem.ToString();
            }
            model.balance_unit = balance_unit;
            model.balance_unit_name = txtbalance_unit.Text.Trim();
            model.update_by = GlobalStaticObj.UserID;
            model.update_name = GlobalStaticObj.UserName;
            model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.allmoney = allmoney;
            model.enable_flag = "1";
            if (HandleType == "保存")
            {
                model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
            }
            else if (HandleType == "提交")
            {
                model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
            }
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Update tb_parts_sale_billing Set ");
                bool isFirstValue = true;
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    else
                    {
                        sb.Append("," + name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(" where sale_billing_id='" + sale_billing_id + "';");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary> 获取配件列表中所有配件的编号
        /// </summary>
        /// <returns></returns>
        string GetListPartsCodes()
        {
            string Parts_Codes = string.Empty;
            if (gvPurchaseList.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value != null)
                    {
                        Parts_Codes += "'" + dr.Cells["parts_code"].Value.ToString() + "',";
                    }
                }
            }
            return Parts_Codes.Trim(',');
        }
        /// <summary> 获取总金额
        /// </summary>
        /// <param name="valorem_together">总金额</param>
        void GetAllMoney(ref decimal valorem_together)
        {
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                valorem_together = valorem_together + Convert.ToDecimal(dr.Cells["valorem_together"].Value == null ? "0" : dr.Cells["valorem_together"].Value.ToString() == "" ? "0" : dr.Cells["valorem_together"].Value.ToString());
            }
        }
        /// <summary> 更改整单折扣时，修改列表中的折扣
        /// </summary>
        void SetListDiscount()
        {
            try
            {
                string alldiscount = "100";
                alldiscount = string.IsNullOrEmpty(txtwhythe_discount.Caption.Trim()) ? "100" : txtwhythe_discount.Caption.Trim();
                if (gvPurchaseList.Rows.Count > 0)
                {
                    rowIndex = 0;
                    foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                    {
                        if (dr.Cells["parts_code"].Value == null)
                        {
                            continue;
                        }
                        dr.Cells["discount"].Value = alldiscount;
                        //业务单价
                        GetBusiness_priceByDiscount(alldiscount, rowIndex);
                        //货款
                        GetPayment(rowIndex);
                        //税款
                        GetTax(rowIndex);
                        //税价合计
                        GetValorem_Together(rowIndex);
                        rowIndex = rowIndex + 1;
                    }
                }
            }
            catch (Exception ex)
            { }
            finally { rowIndex = -1; }
        }
         /// <summary> 编辑配件列表中的配件信息
        /// </summary>
        void EditPartsInfo()
        {
            if (oldindex > -1)
            {
                try
                {

                    frmParts frm = new frmParts();
                    DialogResult result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string PartsCode = frm.PartsCode;
                        if (gvPurchaseList.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                            {
                                if (dr.Cells["parts_code"].Value == null)
                                {
                                    continue;
                                }
                                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                                if (dr.Cells["parts_code"].Value.ToString() == PartsCode && relation_order == "")
                                {
                                    MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
                                    return;
                                }
                            }
                        }
                        DataGridViewRow dgvr = gvPurchaseList.Rows[oldindex];
                        DataTable dt = GetAccessoriesByCode(PartsCode);
                        foreach (DataRow dr in dt.Rows)
                        {
                            GetGridViewRowByDr(dgvr, dr, "", oldindex, "Edit");
                        }
                        if (gvPurchaseList.Rows.Count - 1 == oldindex)
                        {
                            gvPurchaseList.Rows.Add(1);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { oldindex = -1; }
            }
        }
        /// <summary> 获取gvPurchasePlanList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<DataGridViewRow> GetSelectedRecord()
        {
            List<DataGridViewRow> listField = new List<DataGridViewRow>();
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr);
                }
            }
            return listField;
        }
        #endregion

        #region 限制文本框输入内容的事件
        /// <summary> 收款期限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtreceivables_limit_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 收款期限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtreceivables_limit_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 3650);
        }
        /// <summary> 本次现收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcurrent_collect_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 本次现收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcurrent_collect_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 1000000000);
        }
        /// <summary> 客户欠款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcust_arrears_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 客户欠款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcust_arrears_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 1000000000);
        }
        /// <summary> 整单折扣-下按键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtwhythe_discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary>整单折扣-内容改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtwhythe_discount_UserControlValueChanged(object sender, EventArgs e)
        {
            string discount = string.IsNullOrEmpty(txtwhythe_discount.Caption.Trim()) ? "100" : txtwhythe_discount.Caption.Trim();
            if (Convert.ToDecimal(discount) < 1000)
            { SetListDiscount(); }
            else
            {
                txtwhythe_discount.Caption = txtwhythe_discount.Caption.Trim().Substring(0, txtwhythe_discount.Caption.Trim().Length - 1);
            }
        }
        /// <summary> 已结算金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtbalance_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 已结算金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtbalance_money_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 1000000000);
        }
        /// <summary> 发票号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtreceipt_no_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedLength(sender, 20);
        }
        /// <summary> 支票号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcheck_number_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedLength(sender, 18);
        }
        #endregion

        #region 自动计算金额的功能
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.White;
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    DataGridViewTextBoxEditingControl dc = e.Control as DataGridViewTextBoxEditingControl;
                    dc.TextChanged -= dc_TextChanged;
                    dc.TextChanged += dc_TextChanged;
                }
                DataGridView dgv = sender as DataGridView;
                //判断相应的列
                if (dgv.CurrentCell.GetType().Name == "DataGridViewComboBoxCell" && dgv.CurrentCell.RowIndex != -1)
                {
                    //给这个DataGridViewComboBoxCell加上下拉事件
                    (e.Control as ComboBox).SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ColumnName = ((System.Windows.Forms.DataGridViewTextBoxEditingControl)(sender)).EditingControlDataGridView.CurrentCell.OwningColumn.Name;
                int NewrowIndex = ((System.Windows.Forms.DataGridViewTextBoxEditingControl)(sender)).EditingControlDataGridView.CurrentRow.Index;
                DataGridViewTextBoxEditingControl c = sender as DataGridViewTextBoxEditingControl;
                c.SelectionStart = c.Text.Trim().Length;
                if (ColumnName == "original_price" || ColumnName == "discount" || ColumnName == "business_price" || ColumnName == "business_count" || ColumnName == "tax_rate")
                {
                    c.Text = string.IsNullOrEmpty(c.Text) ? "0" : c.Text;
                }
                //原始单价
                if (ColumnName == "original_price")
                {
                    //业务单价
                    GetBusiness_priceByOriginal_price(c.Text, NewrowIndex);
                    //货款
                    GetPayment(NewrowIndex);
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                }
                //折扣
                else if (ColumnName == "discount")
                {
                    //业务单价
                    GetBusiness_priceByDiscount(c.Text, NewrowIndex);
                    //货款
                    GetPayment(NewrowIndex);
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                }
                //业务单价
                else if (ColumnName == "business_price")
                {
                    //当修改业务单价时，自动更改折扣
                    string original_price = gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value == null ? "1" : gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value.ToString();
                    original_price = string.IsNullOrEmpty(original_price) ? "1" : original_price;
                    original_price = Convert.ToDecimal(original_price) == 0 ? "1" : original_price;
                    gvPurchaseList.Rows[NewrowIndex].Cells["discount"].Value = Convert.ToDecimal(c.Text) / Convert.ToDecimal(original_price) * 100;

                    //货款
                    GetPaymentByBusiness_price(c.Text, NewrowIndex);
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                }
                //业务数量
                else if (ColumnName == "business_count")
                {
                    //当销售开单时，业务数量必须全部大于0，否则验证不通过
                    if (ddlorder_type.SelectedValue.ToString() == "1")
                    {
                        c.Text = Convert.ToDecimal(c.Text) < 0 ? (-Convert.ToDecimal(c.Text)).ToString() : Convert.ToDecimal(c.Text).ToString();
                    }
                    //当销售退货时，业务数量必须全部小于0，否则验证不通过
                    else if (ddlorder_type.SelectedValue.ToString() == "2")
                    {
                        c.Text = Convert.ToDecimal(c.Text) > 0 ? (-Convert.ToDecimal(c.Text)).ToString() : Convert.ToDecimal(c.Text).ToString();
                    }
                    gvPurchaseList.Rows[NewrowIndex].Cells["business_count"].Value = c.Text;
                    if (Convert.ToDecimal(c.Text) < 0)
                    { c.ForeColor = Color.Red; }
                    else
                    { c.ForeColor = Color.Black; }
                    //货款
                    GetPaymentByBusiness_counts(c.Text, NewrowIndex);
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                }
                //税率
                else if (ColumnName == "tax_rate")
                {
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>修改原始单价后, 获取业务单价
        /// </summary>
        void GetBusiness_priceByOriginal_price(string original_price, int NewrowIndex)
        {
            //折扣
            string discount = gvPurchaseList.Rows[NewrowIndex].Cells["discount"].Value == null ? "100" : gvPurchaseList.Rows[NewrowIndex].Cells["discount"].Value.ToString();
            discount = string.IsNullOrEmpty(discount) ? "100" : discount;
            //业务单价
            gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value = Convert.ToDecimal(original_price) * (Convert.ToDecimal(discount) / 100);
        }
        /// <summary>修改折扣后获取业务单价
        /// </summary>
        /// 
        void GetBusiness_priceByDiscount(string discount, int NewrowIndex)
        {
            //原始单价
            string original_price = gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value.ToString();
            original_price = string.IsNullOrEmpty(original_price) ? "0" : original_price;
            //业务单价
            gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value = (Convert.ToDecimal(discount) / 100) * Convert.ToDecimal(original_price);
        }

        /// <summary>修改业务单价后, 获取货款
        /// </summary>
        void GetPaymentByBusiness_price(string business_price, int NewrowIndex)
        {
            //业务数量
            string business_count = gvPurchaseList.Rows[NewrowIndex].Cells["business_count"].Value == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["business_count"].Value.ToString();
            business_count = string.IsNullOrEmpty(business_count) ? "0" : business_count;
            //货款
            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_price) * Convert.ToDecimal(business_count);
        }
        /// <summary>修改业务数量后, 获取货款
        /// </summary>
        void GetPaymentByBusiness_counts(string business_count, int NewrowIndex)
        {
            //业务单价
            string business_price = gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;
            //货款
            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_count) * Convert.ToDecimal(business_price);
        }
        /// <summary> 获取货款
        /// </summary>
        void GetPayment(int NewrowIndex)
        {
            //业务数量
            string business_count = gvPurchaseList.Rows[NewrowIndex].Cells["business_count"].Value == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["business_count"].Value.ToString();
            business_count = string.IsNullOrEmpty(business_count) ? "0" : business_count;
            //业务单价
            string business_price = gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;

            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_count) * Convert.ToDecimal(business_price);
        }

        /// <summary> 修改税率后，获取税款 
        /// </summary>
        void GetTaxByTax_rate(string tax_rate, int NewrowIndex)
        {
            //货款
            string payment = gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value.ToString();
            payment = string.IsNullOrEmpty(payment) ? "0" : payment;
            //税款
            gvPurchaseList.Rows[NewrowIndex].Cells["tax"].Value = Convert.ToDecimal(tax_rate) / 100 * Convert.ToDecimal(payment);
        }
        /// <summary> 获取税款 
        /// </summary>
        void GetTax(int NewrowIndex)
        {
            //货款
            string payment = gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value.ToString();
            payment = string.IsNullOrEmpty(payment) ? "0" : payment;
            //税率
            string tax_rate = gvPurchaseList.Rows[NewrowIndex].Cells["tax_rate"].EditedFormattedValue == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["tax_rate"].EditedFormattedValue.ToString();
            tax_rate = string.IsNullOrEmpty(tax_rate) ? "0" : tax_rate;

            gvPurchaseList.Rows[NewrowIndex].Cells["tax"].Value = Convert.ToDecimal(payment) * Convert.ToDecimal(tax_rate) / 100;
        }
        /// <summary> 获取税价合计 
        /// </summary>
        void GetValorem_Together(int NewrowIndex)
        {
            //税价合计
            gvPurchaseList.Rows[NewrowIndex].Cells["valorem_together"].Value = Convert.ToDecimal(gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value) + Convert.ToDecimal(gvPurchaseList.Rows[NewrowIndex].Cells["tax"].Value);
        }
        #endregion

        #region 配件的单位下拉框事件和处理
        /// <summary> 下拉框事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            ////这里比较重要
            combox.Leave -= new EventHandler(combox_Leave);
            combox.Leave += new EventHandler(combox_Leave);
            try
            {
                //在这里就可以做值是否改变判断
                if (combox.SelectedValue != null)
                {
                    if (combox.SelectedValue.ToString() != "")
                    {
                        if (list_partunitprice.Count > 0)
                        {
                            List<partunitprice> newunitprice = list_partunitprice.FindAll(p => p.unitid == combox.SelectedValue.ToString());
                            if (newunitprice.Count > 0)
                            {
                                decimal oldprice = 0;
                                decimal oldrate = 1;
                                decimal original_price = 0;
                                lastunitclass lastprice_model = new lastunitclass();
                                lastprice_model.unitid = combox.SelectedValue.ToString();
                                lastprice_model.price = newunitprice[0].price;
                                lastprice_model.rate = newunitprice[0].rate;
                                if (list_lastunitprice.Count > 0)
                                {
                                    List<lastunitclass> newListunit = list_lastunitprice.FindAll(p => p.unitid == combox.SelectedValue.ToString());
                                    if (newListunit.Count > 0)
                                    {
                                        oldprice = newListunit[0].price;
                                        oldrate = newListunit[0].rate == 0 ? 1 : newListunit[0].rate;
                                        list_lastunitprice.Remove(newListunit[0]);
                                        original_price = newunitprice[0].rate / oldrate * oldprice;
                                        list_lastunitprice.Add(lastprice_model);
                                    }
                                    else
                                    {
                                        original_price = newunitprice[0].price;
                                        list_lastunitprice.Add(lastprice_model);
                                    }
                                }
                                else
                                {
                                    original_price = newunitprice[0].price;
                                    list_lastunitprice.Add(lastprice_model);
                                }
                                Set_Original_price(original_price, gvPurchaseList.CurrentRow.Index);
                            }
                        }
                    }
                    else
                    {
                        Set_Original_price(0, gvPurchaseList.CurrentRow.Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary> 离开combox时，把事件删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void combox_Leave(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            //做完处理，须撤销动态事件
            combox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
        }
        /// <summary> 选择单位后设置原始单价
        /// </summary>
        /// <param name="price"></param>
        /// <param name="rowsindex"></param>
        void Set_Original_price(decimal original_price, int rowsindex)
        {
            gvPurchaseList.Rows[rowsindex].Cells["original_price"].Value = original_price.ToString();
            //业务折扣
            string discount = gvPurchaseList.Rows[rowsindex].Cells["discount"].Value == null ? "100" : gvPurchaseList.Rows[rowsindex].Cells["discount"].Value.ToString();
            discount = string.IsNullOrEmpty(discount) ? "100" : discount;
            //业务单价
            gvPurchaseList.Rows[rowsindex].Cells["business_price"].Value = original_price * (Convert.ToDecimal(discount) / 100);
            string business_price = gvPurchaseList.Rows[rowsindex].Cells["business_price"].Value == null ? "" : gvPurchaseList.Rows[rowsindex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;
            //业务数量
            string business_count = gvPurchaseList.Rows[rowsindex].Cells["business_count"].Value == null ? "0" : gvPurchaseList.Rows[rowsindex].Cells["business_count"].Value.ToString();
            business_count = string.IsNullOrEmpty(business_count) ? "0" : business_count;
            //货款
            gvPurchaseList.Rows[rowsindex].Cells["payment"].Value = Convert.ToDecimal(business_count) * Convert.ToDecimal(business_price);
            string payment = gvPurchaseList.Rows[rowsindex].Cells["payment"].Value == null ? "" : gvPurchaseList.Rows[rowsindex].Cells["payment"].Value.ToString();
            payment = string.IsNullOrEmpty(payment) ? "0" : payment;
            //税率
            string tax_rate = gvPurchaseList.Rows[rowsindex].Cells["tax_rate"].Value == null ? "0" : gvPurchaseList.Rows[rowsindex].Cells["tax_rate"].Value.ToString();
            tax_rate = string.IsNullOrEmpty(tax_rate) ? "0" : tax_rate;
            //税款
            gvPurchaseList.Rows[rowsindex].Cells["tax"].Value = Convert.ToDecimal(payment) * Convert.ToDecimal(tax_rate) / 100;
            //税价合计
            gvPurchaseList.Rows[rowsindex].Cells["valorem_together"].Value = Convert.ToDecimal(gvPurchaseList.Rows[rowsindex].Cells["payment"].Value) + Convert.ToDecimal(gvPurchaseList.Rows[rowsindex].Cells["tax"].Value);
        }
        /// <summary> 为每一行的配件信息绑定下拉框信息
        /// </summary>
        /// <param name="parts_code"></param>
        /// <param name="rowsindex"></param>
        /// <param name="dgcombox"></param>
        public void BindDataGridViewComboBoxCell(string data_source, string parts_code, int rowsindex, ref DataGridViewComboBoxCell dgcombox, ref string default_unit_id, ref string default_price)
        {
            dgcombox = (DataGridViewComboBoxCell)gvPurchaseList.Rows[rowsindex].Cells["unit_id"];

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "请选择"));
            string wherestr = string.Format(@" parts_id=(select parts_id from tb_parts where ser_parts_code='{0}') and enable_flag=1", parts_code);
            DataTable dt = DBHelper.GetTable("", "tb_parts_price", "*", wherestr, "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(new ListItem(dt.Rows[i]["pp_id"].ToString(), dt.Rows[i]["unit"].ToString()));

                    partunitprice model = new partunitprice();
                    model.parts_id = dt.Rows[i]["parts_id"].ToString();
                    model.unitid = dt.Rows[i]["pp_id"].ToString();
                    model.unitname = dt.Rows[i]["unit"].ToString();
                    model.rate = Convert.ToDecimal(dt.Rows[i]["rate"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["rate"].ToString()) ? "0" : dt.Rows[i]["rate"].ToString());
                    //model.price = Convert.ToDecimal(dt.Rows[i]["into_price_two"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["into_price_two"].ToString()) ? "0" : dt.Rows[i]["into_price_two"].ToString());

                    //根据配件数据来源，判断给什么单价(oldorderstatus业务时：自建的配件信息用参考进价，宇通的配件用销售2A价)
                    if (data_source == "1")//自建配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_out_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_out_price"].ToString()) ? "0" : dt.Rows[i]["ref_out_price"].ToString());
                    }
                    else if (data_source == "2")//宇通配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_out_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_out_price"].ToString()) ? "0" : dt.Rows[i]["ref_out_price"].ToString());
                    }
                    //oldorderstatus业务中，判断单位是否是oldorderstatus单位
                    if (dt.Rows[i]["is_sale"].ToString() == "1")
                    {
                        default_unit_id = dt.Rows[i]["pp_id"].ToString();
                        default_price = model.price.ToString();
                    }

                    if (list_partunitprice.Count > 0)
                    {
                        if (!list_partunitprice.Exists(p => p.unitid == dt.Rows[i]["pp_id"].ToString()))
                        {
                            list_partunitprice.Add(model);
                        }
                    }
                    else
                    {
                        list_partunitprice.Add(model);
                    }
                }
            }
            dgcombox.DataSource = list;
            dgcombox.ValueMember = "Value";
            dgcombox.DisplayMember = "Text";
        }
        #endregion

        #region 提交单据时，判断配件完成数量、各个前置单据的导入状态、前后置单据关联信息保存到中间表的功能
        /// <summary> 当删除配件信息时，检索一下该配件的引用单号下是否还存在其他配件信息在列表中，如果存在不需要解锁前置单据，如果不存在，需要解锁前置单据
        /// </summary>
        /// <param name="relation_order_code">引用单号</param>
        /// <returns>返回值 false：不存在，true:存在</returns>
        bool CheckPre_order_code(string relation_order_code)
        {
            bool isExisit = false;
            int relation_count = 0;
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                if (!string.IsNullOrEmpty(relation_order))
                {
                    if (relation_order == relation_order_code)
                    {
                        relation_count = relation_count + 1;
                        if (relation_count > 1)
                        {
                            isExisit = true;
                            break;
                        }
                    }
                }
            }
            return isExisit;
        }
        /// <summary> 获取当前配件列表中存在的引用单号,
        /// 并生成执行的sql
        /// </summary>
        /// <returns></returns>
        void GetPre_Order_Code(List<SysSQLString> listSql, string post_order_id, string post_order_code)
        {
            List<string> list = new List<string>();

            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql1 = "delete from tr_order_relation where post_order_id=@post_order_id and post_order_code=@post_order_code;";
            dic.Add("post_order_id", post_order_id);
            dic.Add("post_order_code", post_order_code);
            sysStringSql.sqlString = sql1;
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);

            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                if (!string.IsNullOrEmpty(relation_order))
                {
                    if (!list.Contains(relation_order))
                    {
                        list.Add(relation_order);

                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        dic.Add("order_relation_id", Guid.NewGuid().ToString());
                        dic.Add("pre_order_id", string.Empty);
                        dic.Add("pre_order_code", relation_order);
                        dic.Add("post_order_id", post_order_id);
                        dic.Add("post_order_code", post_order_code);
                        string sql2 = string.Format(@"Insert Into tr_order_relation(order_relation_id,pre_order_id,pre_order_code,
                                                      post_order_id,post_order_code)  values(@order_relation_id,@pre_order_id,
                                                      @pre_order_code,@post_order_id,@post_order_code);");
                        sysStringSql.sqlString = sql2;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                }
            }
        }
        /// <summary> 获取各个前置单据中配件业务数量在后置单据中的已完成的数量
        /// </summary>
        DataTable GetFinishCount()
        {
            DataTable dt = null;
            try
            {
                string files = string.Empty;
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        continue;
                    }
                    string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                    if (!string.IsNullOrEmpty(relation_order))
                    {
                        if (!files.Contains("'" + relation_order + "',"))
                        {
                            files += "'" + relation_order + "',";
                        }
                    }
                }
                files = files.Trim(',');
                if (files.Trim().Length > 0)
                {
                    files = " where a.order_num in (" + files + ")";
                }
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
                                                        --销售订单
                                                        select 
                                                        tb_order.order_num,tb_order.parts_code,sum(tb_bill.business_count) relation_count,tb_bill.ImportOrderType
                                                         from
                                                         (
                                                           --销售开单信息
                                                           select b.relation_order,b.parts_code,b.business_count,b.ImportOrderType from tb_parts_sale_billing a 
                                                           inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id
                                                           where a.order_status in ('1','2')
                                                         ) tb_bill
                                                        left join 
                                                         (
                                                           --销售订单信息
                                                           select a.order_num,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                           inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id {0}
                                                         ) tb_order 
                                                         on tb_order.order_num=tb_bill.relation_order and tb_order.parts_code=tb_bill.parts_code 
                                                         where len(tb_order.order_num)>0 and LEN(tb_order.parts_code)>0
                                                        group by tb_order.order_num,tb_order.parts_code
                                                        ,tb_bill.ImportOrderType

                                                        union

                                                        --销售开单（已收货）
                                                        select 
                                                         tb_sal_bill_revice.order_num,tb_sal_bill_revice.parts_code,sum(tb_sal_bill.business_count) relation_count,tb_sal_bill.ImportOrderType
                                                         from
                                                         (
                                                          --销售开单信息
                                                          select b.relation_order,b.parts_code,b.business_count,b.ImportOrderType from tb_parts_sale_billing a 
                                                          inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id
                                                          where a.order_status in ('1','2')
                                                         ) tb_sal_bill
                                                        left join 
                                                        (
                                                          --销售开单(已收货)信息
                                                          select a.order_num,b.parts_code,b.business_count from tb_parts_sale_billing a 
                                                          inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id {0}
                                                        ) tb_sal_bill_revice 
                                                         on tb_sal_bill_revice.order_num=tb_sal_bill.relation_order and tb_sal_bill_revice.parts_code=tb_sal_bill.parts_code 
                                                         where len(tb_sal_bill_revice.order_num)>0 and LEN(tb_sal_bill_revice.parts_code)>0
                                                         group by tb_sal_bill_revice.order_num,tb_sal_bill_revice.parts_code
                                                        ,tb_sal_bill.ImportOrderType
                                                        ) tb_sal_order_finishcount ", files);
                return dt = DBHelper.GetTable("销售订单导入中，获取销售计划单或销售订单已完成的配件数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            { return dt; }
        }
        /// <summary> 获取前置单据的业务信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        DataTable GetBusinessCount(string order_id)
        {
            DataTable dt = null;
            try
            {
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
                                                        --销售订单
	                                                    select a.sale_order_id,b.order_num,a.parts_code,a.business_count from
	                                                    (
		                                                     select * from tb_parts_sale_order_p where sale_order_id in
		                                                     (
			                                                     select sale_order_id from tb_parts_sale_order where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_sale_billing_p 
				                                                    where sale_billing_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_sale_order b on a.sale_order_id=b.sale_order_id
	                                                    union
	                                                    --销售开单
	                                                    select a.sale_billing_id as sale_order_id,b.order_num,a.parts_code,a.business_count from
	                                                    (
		                                                     select * from tb_parts_sale_billing_p where sale_billing_id in
		                                                     (
			                                                     select sale_billing_id from tb_parts_sale_billing where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_sale_billing_p 
				                                                    where sale_billing_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_sale_billing b on a.sale_billing_id=b.sale_billing_id
                                                    ) tb_sal_bill_businesscount ", order_id);
                return dt = DBHelper.GetTable("查询销售订单导入销售计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        /// <summary> 销售开单提交前先验证各个前置单据中各种配件可导入的剩余数量 
        /// </summary>
        /// <returns></returns>
        DataTable GetSurplusCount()
        {
            DataTable dt = null;
            try
            {
                string files = string.Empty;
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        continue;
                    }
                    string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                    if (!string.IsNullOrEmpty(relation_order))
                    {
                        if (!files.Contains("'" + relation_order + "',"))
                        {
                            files += "'" + relation_order + "',";
                        }
                    }
                }
                files = files.Trim(',');
                if (files.Trim().Length > 0)
                {
                    files = " where a.order_num in (" + files + ")";
                }
                string FileName = string.Format(@" *,business_count-relation_count as SurplusCount ");
                string TableName = string.Format(@" (
                                                        --销售订单
                                                        select 
                                                        tb_order.order_num,tb_order.parts_code,tb_order.business_count,
                                                        isnull(sum(tb_bill.business_count),0) relation_count,tb_bill.ImportOrderType
                                                         from
                                                         (
                                                           --销售开单信息
                                                           select b.relation_order,b.parts_code,b.business_count,b.ImportOrderType from tb_parts_sale_billing a 
                                                           inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id
                                                           where a.order_status in ('1','2')
                                                         ) tb_bill
                                                        right join 
                                                         (
                                                           --销售订单信息
                                                           select a.order_num,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                           inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id {0}
                                                         ) tb_order 
                                                         on tb_order.order_num=tb_bill.relation_order and tb_order.parts_code=tb_bill.parts_code 
                                                         where len(tb_order.order_num)>0 and LEN(tb_order.parts_code)>0
                                                        group by tb_order.order_num,tb_order.parts_code,tb_order.business_count,tb_bill.ImportOrderType

                                                        union

                                                        --销售开单（已收货）
                                                        select 
                                                         tb_sal_bill_revice.order_num,tb_sal_bill_revice.parts_code,tb_sal_bill_revice.business_count,
                                                          isnull(sum(tb_sal_bill.business_count),0) relation_count,tb_sal_bill.ImportOrderType
                                                         from
                                                         (
                                                          --销售开单信息
                                                          select b.relation_order,b.parts_code,b.business_count,b.ImportOrderType from tb_parts_sale_billing a 
                                                          inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id
                                                          where a.order_status in ('1','2')
                                                         ) tb_sal_bill
                                                        right join 
                                                        (
                                                          --销售开单(已收货)信息
                                                          select a.order_num,b.parts_code,b.business_count from tb_parts_sale_billing a 
                                                          inner join tb_parts_sale_billing_p b on a.sale_billing_id=b.sale_billing_id {0}
                                                        ) tb_sal_bill_revice 
                                                         on tb_sal_bill_revice.order_num=tb_sal_bill.relation_order and tb_sal_bill_revice.parts_code=tb_sal_bill.parts_code 
                                                         where len(tb_sal_bill_revice.order_num)>0 and LEN(tb_sal_bill_revice.parts_code)>0
                                                         group by tb_sal_bill_revice.order_num,tb_sal_bill_revice.parts_code,tb_sal_bill_revice.business_count,tb_sal_bill.ImportOrderType
                                                        ) tb_sal_order_surpluscount  ", files);
                return dt = DBHelper.GetTable("销售开单提交前先验证各个前置单据中各种配件可导入的剩余数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            { return dt; }
        }
        /// <summary> 获取当前单据中各个引用配件的业务数量 
        /// </summary>
        /// <param name="order_num"></param>
        /// <param name="parts_code"></param>
        /// <returns></returns>
        DataTable GetThisBusinessCount(string sale_order_id, string order_num, string parts_code)
        {
            DataTable dt = null;
            try
            {
                string files = " relation_order,parts_code,ImportOrderType,business_count ";
                string wherestr = " sale_billing_id='" + sale_order_id + "' and relation_order='" + order_num + "' and parts_code='" + parts_code + "'";
                return dt = DBHelper.GetTable("销售开单提交前先验证各个前置单据中各种配件可导入的剩余数量", "tb_parts_sale_billing_p", files, wherestr, "", "");
            }
            catch (Exception ex)
            { return dt; }
        }
        /// <summary> 对引用的前置单据的状态进行更新的方法
        /// </summary>
        /// <param name="list_order"></param>
        void ImportPurchasePlanStatus(List<OrderImportStatus> list_order, List<OrderFinishInfo> list_orderinfo)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //更新前置单据的导入状态字段
            foreach (OrderImportStatus item in list_order)
            {
                if (item.importtype == "销售订单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_order set is_occupy=@is_occupy where order_num=@order_num;";
                    dic.Add("is_occupy", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    dic.Add("order_num", item.order_num);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                else if (item.importtype == "销售开单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_billing set is_occupy=@is_occupy where order_num=@order_num;";
                    dic.Add("is_occupy", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    dic.Add("order_num", item.order_num);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
            //更新前置单据中的各个配件的已完成数量
            foreach (OrderFinishInfo item in list_orderinfo)
            {
                if (item.importtype == "销售订单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_order_p set finish_count=@finish_count where sale_order_id=@sale_order_id and parts_code=@parts_code;";
                    dic.Add("finish_count", item.finish_num);
                    dic.Add("sale_order_id", item.sale_order_id);
                    dic.Add("parts_code", item.parts_code);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                else if (item.importtype == "销售开单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_billing_p set finish_count=@finish_count where sale_billing_id=@sale_billing_id and parts_code=@parts_code;";
                    dic.Add("finish_count", item.finish_num);
                    dic.Add("sale_billing_id", item.sale_order_id);
                    dic.Add("parts_code", item.parts_code);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
            DBHelper.BatchExeSQLStringMultiByTrans("提交销售开单，更新引用的销售订单或销售开单的导入状态", listSql);
        }
        /// <summary> 提交成功时,对引用的前置单据的状态进行更新 
        /// </summary>
        /// <param name="orderid"></param>
        void SetOrderStatus(string orderid)
        {
            //前置单据中的配件信息是否在后置单据中全部导入完成（完成数量>=计划数量）
            List<OrderImportStatus> list_order = new List<OrderImportStatus>();
            List<OrderFinishInfo> list_orderinfo = new List<OrderFinishInfo>();
            OrderImportStatus orderimport_model = new OrderImportStatus();
            OrderFinishInfo orderfinish_info = new OrderFinishInfo();

            DataTable dt_Business = GetBusinessCount(orderid);
            DataTable dt_Finish = GetFinishCount();

            string sale_order_id = string.Empty;
            string order_num = string.Empty;
            string parts_code = string.Empty;
            string importtype = string.Empty;
            if (dt_Business.Rows.Count > 0)
            {
                for (int i = 0; i < dt_Business.Rows.Count; i++)
                {
                    bool isfinish = true;
                    int BusinessCount = int.Parse(dt_Business.Rows[i]["business_count"].ToString());
                    sale_order_id = dt_Business.Rows[i]["sale_order_id"].ToString();
                    order_num = dt_Business.Rows[i]["order_num"].ToString();
                    parts_code = dt_Business.Rows[i]["parts_code"].ToString();
                    DataRow[] dr = dt_Finish.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                    if (dr != null && dr.Count() > 0)
                    {
                        importtype = dr[0]["ImportOrderType"].ToString();

                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.sale_order_id = sale_order_id;
                        orderfinish_info.parts_code = parts_code;
                        orderfinish_info.finish_num = dr[0]["relation_count"].ToString();
                        orderfinish_info.importtype = importtype;
                        list_orderinfo.Add(orderfinish_info);
                        if (int.Parse(dr[0]["relation_count"].ToString()) < BusinessCount)
                        {
                            isfinish = false;
                        }
                    }
                    else
                    {
                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.sale_order_id = sale_order_id;
                        orderfinish_info.parts_code = parts_code;
                        orderfinish_info.finish_num = "0";
                        orderfinish_info.importtype = importtype;
                        list_orderinfo.Add(orderfinish_info);

                        isfinish = false;
                    }

                    orderimport_model = new OrderImportStatus();
                    orderimport_model.order_num = order_num;
                    orderimport_model.importtype = importtype;
                    orderimport_model.isfinish = isfinish;
                    if (list_order.Count > 0)
                    {
                        if (list_order.Where(p => p.order_num == order_num).Count() > 0)
                        {
                            if (!isfinish)
                            {
                                for (int a = 0; a < list_order.Count; a++)
                                {
                                    if (list_order[a].order_num == order_num && list_order[a].isfinish)
                                    { list_order[a].isfinish = isfinish; }
                                }
                            }
                        }
                        else
                        { list_order.Add(orderimport_model); }
                    }
                    else
                    { list_order.Add(orderimport_model); }
                }
            }
            ImportPurchasePlanStatus(list_order, list_orderinfo);
        } 
        #endregion

        #region --选择器获取数据后需执行的回调函数
        /// <summary> 供应商速查关联控件赋值
        /// </summary>
        /// <param name="dr"></param>
        private void Balance_UnitName_DataBack(DataRow dr)
        {
            if (dr.Table.Columns.Contains("cust_name"))
            {
                this.txtbalance_unit.Text = dr["cust_name"].ToString();
                if (!string.IsNullOrEmpty(txtcust_name.Text.Trim()))
                {
                    DataTable dt = DBHelper.GetTable("查询客户档案信息", "tb_customer", "*", " enable_flag != 0 and cust_name='" + txtcust_name + "'", "", "");
                    if (dt.Rows.Count > 0)
                    {
                        txtfax.Caption = dt.Rows[0]["cust_fax"].ToString();
                        txtcontacts_tel.Caption = dt.Rows[0]["cust_tel"].ToString();
                        string TableName = string.Format(@"(
                                                    select cont_name from tb_contacts where cont_id=
                                                    (select cont_id from tr_base_contacts where relation_object_id='{0}' and is_default='1')
                                                    ) tb_conts", dt.Rows[0]["cust_id"].ToString());
                        DataTable dt_conts = DBHelper.GetTable("查询客户默认的联系人信息", TableName, "*", "", "", "");
                        if (dt_conts != null && dt_conts.Rows.Count > 0)
                        {
                            txtcontacts.Caption = dt_conts.Rows[0]["cont_name"].ToString();
                        }
                        else
                        { txtcontacts.Caption = string.Empty; }
                        //当客户id不为空，同时变更客户id时，需清空配件信息列表
                        if (cust_id != string.Empty && cust_id != dt.Rows[0]["cust_id"].ToString())
                        {
                            if (gvPurchaseList.Rows.Count > 0)
                            {
                                int PurchaseListRows = gvPurchaseList.Rows.Count;
                                for (int i = 0; i < PurchaseListRows; i++)
                                {
                                    if (PurchaseListRows > 1)
                                    {
                                        DeleteListInfoEidtImportStatus(i);
                                        i = -1;
                                        PurchaseListRows = gvPurchaseList.Rows.Count;
                                    }
                                }
                            }
                        }
                        cust_id = dt.Rows[0]["cust_id"].ToString();
                    }
                    else
                    {
                        txtfax.Caption = string.Empty;
                        txtcontacts_tel.Caption = string.Empty;
                        txtcontacts.Caption = string.Empty;
                    }
                }
                else
                {
                    txtfax.Caption = string.Empty;
                    txtcontacts_tel.Caption = string.Empty;
                    txtcontacts.Caption = string.Empty;
                }
            }
        }
        #endregion

        //decimal old_business_count = 0;
        //int RowIndex = 0;
        //private void gvPurchaseList_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex > 0 && e.ColumnIndex > 0)//双击表头或列头时不起作用   
        //    {
        //        string fieldNmae = gvPurchaseList.Columns[e.ColumnIndex].DataPropertyName;
        //        if (fieldNmae.Equals("business_count"))
        //        {
        //            if (gvPurchaseList.CurrentCell.Value != null && gvPurchaseList.CurrentCell.Value.ToString() != "")
        //            {
        //                RowIndex = e.RowIndex;
        //                old_business_count = Convert.ToDecimal(gvPurchaseList.CurrentCell.Value.ToString());
        //            }
        //        }
        //    }
        //}
        //private void gvPurchaseList_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex > 0 && e.ColumnIndex > 0)//双击表头或列头时不起作用
        //    {
        //        if (RowIndex > 0)
        //        {
        //            if (gvPurchaseList.Rows[RowIndex].Cells["business_count"].EditedFormattedValue != null && gvPurchaseList.Rows[RowIndex].Cells["business_count"].EditedFormattedValue.ToString() != "")
        //            {
        //                if (Convert.ToDecimal(gvPurchaseList.Rows[RowIndex].Cells["business_count"].EditedFormattedValue.ToString()) > old_business_count)
        //                {
        //                    gvPurchaseList.CurrentCell.Value = old_business_count.ToString();
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary> 验证业务数量的正负数
        /// </summary>
        /// <param name="TypeNum"></param>
        /// <returns></returns>
        bool VerifBusinessCount(string TypeNum)
        {
            bool Positive = true;//是否必须为正数
            bool Negative = true;//是否必须为负数         
            //当销售开单时，业务数量必须全部大于0，否则验证不通过
            if (TypeNum == "1")
            {
                Positive = true; Negative = true;
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    { continue; }
                    decimal business_counts = decimal.Parse(dr.Cells["business_count"].Value == null ?
                            "0" : dr.Cells["business_count"].Value.ToString() == "" ? "0" : dr.Cells["business_count"].Value.ToString());
                    if (business_counts < 0)
                    {
                        Positive = false;
                        break;
                    }
                }
            }
            //当销售退货时，业务数量必须全部小于0，否则验证不通过
            else if (TypeNum == "2")
            {
                Positive = true; Negative = true;
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    { continue; }
                    decimal business_counts = decimal.Parse(dr.Cells["business_count"].Value == null ?
                            "0" : dr.Cells["business_count"].Value.ToString() == "" ? "0" : dr.Cells["business_count"].Value.ToString());
                    if (business_counts > 0)
                    {
                        Negative = false;
                        break;
                    }
                }
            }
            //当销售换货时，业务数量必须同时含有大于0和小于0的信息，否则验证不通过
            else if (TypeNum == "3")
            {
                Positive = false; Negative = false;
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    { continue; }
                    decimal business_counts = decimal.Parse(dr.Cells["business_count"].Value == null ?
                            "0" : dr.Cells["business_count"].Value.ToString() == "" ? "0" : dr.Cells["business_count"].Value.ToString());
                    if (business_counts > 0)
                    { Positive = true; }
                    if (business_counts < 0)
                    { Negative = true; }
                }
            }
            return Positive && Negative;
        }
        /// <summary> 更换单据类型时修改配件信息列表显示的字段
        /// </summary>
        /// <param name="HandleType"></param>
        void SetDataGridViewColumnName(string HandleType)
        {
            //销售开单
            if (HandleType == "1")
            {
                library_count.HeaderText = "入库数量";
                return_count.Visible = true;//退货数量
                assisted_auxiliary_count.Visible = true;//辅助数量
            }
            //销售退货
            else if (HandleType == "2")
            {
                library_count.HeaderText = "出库数量";
                return_count.Visible = false;//退货数量
                assisted_auxiliary_count.Visible = false;//辅助数量
            }
            //销售换货
            else if (HandleType == "3")
            {
                library_count.HeaderText = "出/入库数量";
                return_count.Visible = false;//退货数量
                assisted_auxiliary_count.Visible = false;//辅助数量
            }
        }
    }


    #region 前置单据信息类
    class OrderImportStatus
    {
        public string order_num;
        public bool isfinish;
        public string importtype;
    }
    class OrderFinishInfo
    {
        public string sale_order_id;
        public string parts_code;
        public string finish_num;
        public string importtype;
    } 
    #endregion

    class partunitprice
    {
        public string parts_id = string.Empty;
        public string unitid = string.Empty;
        public string unitname = string.Empty;
        public decimal rate = 0;
        public decimal price = 0;
    }
    class lastunitclass
    {
        public string unitid = string.Empty;
        public decimal rate = 0;
        public decimal price = 0;
    }
}
