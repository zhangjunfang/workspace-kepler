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
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;
using System.Reflection;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder
{
    public partial class UCSaleOrderAddOrEdit : UCBase
    {
        #region 变量
        WindowStatus status;
        UCSaleOrderManager uc;
        int rowIndex = -1;
        int oldindex = -1;
        string sale_order_id = string.Empty;
        /// <summary>
        /// 通过选择器得到的客户id
        /// </summary>
        string cust_id = string.Empty;
        /// <summary>
        /// 通过选择器得到的客户编码
        /// </summary>
        string cust_code = string.Empty;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        DataTable dt_parts_brand = null;
        private string oldorder_num = string.Empty;
        /// <summary> 是否已操作完成，取消或是关闭窗体时是否需要弹出提示（true:需要弹出,false:不需要弹出）
        /// </summary>
        private bool IsClose = true;
        List<partunitprice> list_partunitprice = new List<partunitprice>();
        List<lastunitclass> list_lastunitprice = new List<lastunitclass>();
        tb_parts_sale_order tb_partspurchaseorder_Model = new tb_parts_sale_order();
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="status"></param>
        /// <param name="sale_order_id"></param>
        /// <param name="uc"></param>
        public UCSaleOrderAddOrEdit(WindowStatus status, string sale_order_id, UCSaleOrderManager uc)
        {
            InitializeComponent();
            this.status = status;
            this.uc = uc;
            this.sale_order_id = sale_order_id;
            this.Resize += new EventHandler(UCSaleOrderAddOrEdit_Resize);
            base.SaveEvent += new ClickHandler(UCSaleOrderAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCSaleOrderAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCSaleOrderAddOrEdit_ImportEvent);
            base.CancelEvent += new ClickHandler(UCSaleOrderAddOrEdit_CancelEvent);

            txtcontract_no.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtcontract_no_UserControlValueChanged);

            txtadvance_money.KeyPress+=new KeyPressEventHandler(txtadvance_money_KeyPress);
            txtadvance_money.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtadvance_money_UserControlValueChanged);
            
            ddldelivery_time.ValueChanged += new EventHandler(ddldelivery_time_ValueChanged);
            gvPurchaseList.CellDoubleClick += new DataGridViewCellEventHandler(gvPurchaseList_CellDoubleClick);

            business_count.ValueType = typeof(decimal);
            //business_count.MaxInputLength = 9;
            original_price.ValueType = typeof(decimal);
            //original_price.MaxInputLength = 9;
            discount.ValueType = typeof(decimal);
            discount.MaxInputLength = 3;
            business_price.ValueType = typeof(decimal);
            //business_price.MaxInputLength = 9;
            tax_rate.ValueType = typeof(decimal);
            tax_rate.MaxInputLength = 3;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSaleOrderAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            { base.SetButtonVisiableHandleAddCopy(); }
            else if (status == WindowStatus.Edit)
            { base.SetButtonVisiableHandleEdit(); }

            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "unit_id", "original_price", "business_price", "business_count", "discount", "tax_rate", "is_gift", "delivery_time", "remark" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);
            //CommonFuncCall.BindUnit(unit_id);

            ddtorder_date.Value = DateTime.Now;
            ddtvalid_till.Value = DateTime.Now;
            ddldelivery_time.Value = DateTime.Now;
            lbloperator_name.Text = GlobalStaticObj.UserName;

            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_way, "sys_trans_mode", "请选择");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlclosing_way, "请选择");
            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");
            dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");

            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(sale_order_id);
                GetAccessories(sale_order_id);
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
            }
            gvPurchaseList.Rows.Add(1);
            label31.Visible = !string.IsNullOrEmpty(txtadvance_money.Caption.Trim()) ? Convert.ToDecimal(txtadvance_money.Caption.Trim()) > 0 : false;
            Choosefrm.CusCodeChoose(txtcust_code1, Choosefrm.delDataBack = Closing_UnitName_DataBack);
            Choosefrm.CusNameChoose(txtclosing_unit1, Choosefrm.delDataBack = null);

            List<string> list_columns = new List<string>();
            
            //string[] total = { business_count.Name};
            //ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);

            //list_columns.Add("original_price");
            //list_columns.Add("business_price");
            //list_columns.Add("business_count");
            //list_columns.Add("discount");
            //list_columns.Add("tax_rate");
            //ControlsConfig.NumberLimitdgv(gvPurchaseList, list_columns, true);
        } 
        #endregion

        #region 控件事件
        /// <summary> 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("保存");
        }
        /// <summary> 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("提交");
        }
        /// <summary> 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderAddOrEdit_ImportEvent(object sender, EventArgs e)
        {
            UCChooseSalePlanOrder frm = new UCChooseSalePlanOrder();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<PartsInfoClassBySalePlan> List_PartInfo = frm.List_PartInfo;
                DataTable dt_SalePlanOrder = frm.dt_SalePlanOrder;
                if (List_PartInfo.Count > 0)
                {
                    for (int i = 0; i < List_PartInfo.Count; i++)
                    {
                        bool IsExist = false;//判断要导入的信息是否已经在列表中存在，默认是不存在
                        string sale_plan_id = List_PartInfo[i].sale_plan_id;
                        string plancode = string.Empty;
                        string partscode = List_PartInfo[i].parts_code;

                        #region 获取引用单号
                        if (dt_SalePlanOrder != null && dt_SalePlanOrder.Rows.Count > 0)
                        {
                            for (int a = 0; a < dt_SalePlanOrder.Rows.Count; a++)
                            {
                                if (dt_SalePlanOrder.Rows[a]["sale_plan_id"].ToString() == sale_plan_id)
                                {
                                    plancode = dt_SalePlanOrder.Rows[a]["order_num"].ToString();
                                    plancode = string.IsNullOrEmpty(plancode) ? sale_plan_id : plancode;
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
                            if (dr.Cells["parts_code"].Value.ToString() == partscode && relation_order == plancode)
                            {
                                IsExist = true;
                                break;
                            }
                        }
                        #endregion
                        #region 当不存在时，添加到配件列表中
                        if (!IsExist)
                        {
                            DataTable dt = DBHelper.GetTable("查询销售计划单配件表信息", "tb_parts_sale_plan_p", "*", " sale_plan_id='" + sale_plan_id + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                break;
                            }
                            foreach (DataRow dr in dt.Rows)
                            {
                                //int RowsIndex = gvPurchaseList.Rows.Add();
                                int RowsIndex = gvPurchaseList.Rows.Count - 1;
                                DataGridViewRow dgvr = gvPurchaseList.Rows[RowsIndex];
                                GetGridViewRowByDrImport(dgvr, dr, plancode, RowsIndex);
                            }
                            gvPurchaseList.Rows.Add(1);
                        }
                        #endregion
                        #region 当添加成功时，将销售计划单状态设置成占用，使前置单据不可以编辑、删除
                        DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_sale_plan", "import_status", " order_num='" + plancode + "'", "", "");
                        if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                        {
                            string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                            if (import_status == "0")
                            {
                                Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                dicValue.Add("import_status", "1");//单据导入状态，0正常，1占用，2锁定(部分导入),3锁定(全部导入)
                                DBHelper.Submit_AddOrEdit("修改销售计划单的导入状态为占用", "tb_parts_sale_plan", "order_num", plancode, dicValue);
                            }
                        }
                        #endregion
                    }
                }
            }
        }
        /// <summary> 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消操作关闭当前界面吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        /// <summary> 单条添加配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleAccessoriesAdd_Click(object sender, EventArgs e)
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
        /// <summary> 编辑配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleAccessoriesEdit_Click(object sender, EventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 删除配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsSaleAccessoriesDelete_Click(object sender, EventArgs e)
        {
            #region 原来的单个删除的方法
            //if (oldindex > -1)
            //{
            //    try
            //    {
            //        if (gvPurchaseList.Rows[oldindex].Cells["parts_code"].Value != null)
            //        {
            //            #region 当删除成功时，将销售计划单状态释放为正常
            //            DataGridViewRow dr = gvPurchaseList.Rows[oldindex];
            //            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
            //            if (!string.IsNullOrEmpty(relation_order))
            //            {
            //                if (!CheckPre_order_code(relation_order))
            //                {
            //                    DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_sale_plan", "import_status", " order_num='" + relation_order + "'", "", "");
            //                    if (dt_import_status != null && dt_import_status.Rows.Count > 0)
            //                    {
            //                        string import_status = dt_import_status.Rows[0]["import_status"].ToString();
            //                        if (import_status == "1")
            //                        {
            //                            Dictionary<string, string> dicValue = new Dictionary<string, string>();
            //                            dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定
            //                            DBHelper.Submit_AddOrEdit("修改销售计划单的导入状态为正常", "tb_parts_sale_plan", "order_num", relation_order, dicValue);
            //                        }
            //                    }
            //                }
            //            }
            //            gvPurchaseList.Rows.Remove(gvPurchaseList.Rows[oldindex]);
            //            #endregion
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
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_sale_plan", "import_status", " order_num='" + relation_order + "'", "", "");
                                    if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                                    {
                                        string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                                        if (import_status == "1")
                                        {
                                            Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                            dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定
                                            DBHelper.Submit_AddOrEdit("修改销售计划单的导入状态为正常", "tb_parts_sale_plan", "order_num", relation_order, dicValue);
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
        /// <summary> 删除零数量配件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsDeleteZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvPurchaseList.Rows.Count > 0)
                {
                    if (GetZeroPartsCount() > 0)
                    {
                        if (MessageBoxEx.Show("确认要删除业务数量为零的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        {
                            return;
                        }
                        for (int i = 0; i < gvPurchaseList.Rows.Count; i++)
                        {
                            if (gvPurchaseList.Rows[i].Cells["parts_code"].Value != null)
                            {
                                DataGridViewRow dr = gvPurchaseList.Rows[i];
                                string business_count = dr.Cells["business_count"].EditedFormattedValue == null ? "0" : dr.Cells["business_count"].EditedFormattedValue.ToString();
                                business_count = string.IsNullOrEmpty(business_count) ? "0" : business_count;
                                if (Convert.ToDecimal(business_count) == 0)
                                {
                                    #region 当删除成功时，将销售计划单状态释放为正常
                                    string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                                    if (!string.IsNullOrEmpty(relation_order))
                                    {
                                        if (!CheckPre_order_code(relation_order))
                                        {
                                            DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_sale_plan", "import_status", " order_num='" + relation_order + "'", "", "");
                                            if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                                            {
                                                string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                                                if (import_status == "1")
                                                {
                                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                                    dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定
                                                    DBHelper.Submit_AddOrEdit("修改销售计划单的导入状态为正常", "tb_parts_sale_plan", "order_num", relation_order, dicValue);
                                                }
                                            }
                                        }
                                    }
                                    gvPurchaseList.Rows.Remove(dr);
                                    i = -1;
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldindex = -1; }
        }
        /// <summary> 库存查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsStockSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (oldindex > -1)//双击表头或列头时不起作用   
                {
                    string parts_id = gvPurchaseList.Rows[oldindex].Cells["parts_id"].Value == null ? "" : gvPurchaseList.Rows[oldindex].Cells["parts_id"].Value.ToString();
                    if (!string.IsNullOrEmpty(parts_id))
                    {
                        frmStockCount frm = new frmStockCount(parts_id);
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 选择部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), "请选择");
        }
        /// <summary> 选择客户编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcust_code1_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo chooseSupplier = new frmCustomerInfo();
            chooseSupplier.ShowDialog();
            cust_id = chooseSupplier.strCustomerId;
            if (!string.IsNullOrEmpty(cust_id))
            {
                cust_code = chooseSupplier.strCustomerNo;
                txtcust_code1.Text = chooseSupplier.strCustomerNo;
                txtcust_name.Caption = chooseSupplier.strCustomerName;
                txtclosing_unit1.Text = chooseSupplier.strCustomerName;

                DataTable dt = DBHelper.GetTable("查询客户档案信息", "tb_customer", "*", " enable_flag != 0 and cust_id='" + cust_id + "'", "", "");
                if (dt.Rows.Count > 0)
                {
                    txtfax.Caption = dt.Rows[0]["cust_fax"].ToString();
                    txtcontacts_tel.Caption = dt.Rows[0]["cust_tel"].ToString();
                }
                string TableName = string.Format(@"(
                                                    select cont_name from tb_contacts where cont_id=
                                                    (select cont_id from tr_base_contacts where relation_object_id='{0}' and is_default='1')
                                                    ) tb_conts", cust_id);
                DataTable dt_conts = DBHelper.GetTable("查询客户默认的联系人信息", TableName, "*", "", "", "");
                if (dt_conts != null && dt_conts.Rows.Count > 0)
                {
                    txtcontacts.Caption = dt_conts.Rows[0]["cont_name"].ToString();
                }
                else
                { txtcontacts.Caption = string.Empty; }
            }
        }
        /// <summary> 选择结算单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtclosing_unit1_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo chooseSupplier = new frmCustomerInfo();
            chooseSupplier.ShowDialog();
            string supperID = chooseSupplier.strCustomerId;
            if (!string.IsNullOrEmpty(supperID))
            {
                txtclosing_unit1.Text = chooseSupplier.strCustomerName;
            }
        }
        /// <summary> 鼠标进入单元格事件
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
        /// <summary> 单元格内容格式化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
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
                        //string num = gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Value.ToString();
                        //num = string.IsNullOrEmpty(num) ? "0" : num;
                        //if (Convert.ToDecimal(num) < 0)
                        //{ gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Style.ForeColor = Color.Red; }
                        //else
                        //{ gvPurchaseList.Rows[e.RowIndex].Cells["business_count"].Style.ForeColor = Color.Black; }
                    }
                }
            }
            catch (Exception ex)
            { }
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
        /// <summary> 合同号输入长度限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcontract_no_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedLength(sender, 20);
        }
        /// <summary> 预收金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtadvance_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 预收金额输入金额限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtadvance_money_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 100000000);
            label31.Visible = !string.IsNullOrEmpty(txtadvance_money.Caption.Trim()) ? Convert.ToDecimal(txtadvance_money.Caption.Trim()) > 0 : false;
        }
        /// <summary> 单据的发货日期更改时，更新配件列表中的发货时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddldelivery_time_ValueChanged(object sender, EventArgs e)
        {
            if (ddldelivery_time.Value != null)
            {
                if (gvPurchaseList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                    {
                        if (dr.Cells["parts_code"].Value == null)
                        {
                            continue;
                        }
                        if (ddldelivery_time.Value != null)
                        {
                            dr.Cells["delivery_time"].Value = ddldelivery_time.Value.ToShortDateString();//发货时间
                        }
                    }
                }
            }
        }
        /// <summary> 统计数量、金额的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderAddOrEdit_Resize(object sender, EventArgs e)
        {
            string[] total = { business_count.Name, tax.Name, payment.Name, valorem_together.Name };
            ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);
        }
        #endregion

        #region 方法、函数
        /// <summary> 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            if (string.IsNullOrEmpty(cust_id))
            {
                MessageBoxEx.Show("请选择客户编码!"); return false;
            }
            if (string.IsNullOrEmpty(txtclosing_unit1.Text))
            {
                MessageBoxEx.Show("请选择结算单位!"); return false;
            }
            if (!string.IsNullOrEmpty(txtadvance_money.Caption.Trim()))
            {
                if (Convert.ToDecimal(txtadvance_money.Caption.Trim()) != 0)
                {
                    if (string.IsNullOrEmpty(ddlclosing_way.SelectedValue.ToString()))
                    {
                        MessageBoxEx.Show("预收金额不为0的情况下,请选择结算方式!");
                        return false;
                    }
                }
            }
            if (!txtcontacts_tel.Verify(true))
            { return false; }
            if (!txtfax.Verify(true))
            { return false; }
            //foreach (DataGridViewRow dgvr in gvPurchaseList.Rows)
            //{
            //    string SelectUnit = CommonCtrl.IsNullToString(dgvr.Cells["unit_id"].Value);//配件单位
            //    if (SelectUnit.Length == 0)
            //    {
            //        MessageBoxEx.Show("请选择配件单位!");
            //        gvPurchaseList.CurrentCell = dgvr.Cells["unit_id"];
            //        return false;
            //    }
            //}
            return true;
        }
        /// <summary> 加载销售订单信息和配件信息
        /// </summary>
        /// <param name="sale_order_id"></param>
        private void LoadInfo(string sale_order_id)
        {
            if (!string.IsNullOrEmpty(sale_order_id))
            {
                //1.查看一条销售订单信息
                DataTable dt = DBHelper.GetTable("查看一条销售订单信息", "tb_parts_sale_order", "*", " sale_order_id='" + sale_order_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_partspurchaseorder_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_partspurchaseorder_Model, "");
                    cust_id = tb_partspurchaseorder_Model.cust_id;
                    txtcust_code1.Text = tb_partspurchaseorder_Model.cust_code;
                    txtclosing_unit1.Text = tb_partspurchaseorder_Model.closing_unit;
                    chkis_suspend.Checked = tb_partspurchaseorder_Model.is_suspend == "0";//选中(中止)：0,未选中(不中止)：1
                    oldorder_num = tb_partspurchaseorder_Model.order_num;
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = "";
                    }
                    lblcreate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_partspurchaseorder_Model.create_time.ToString())).ToString();
                    if (tb_partspurchaseorder_Model.update_time > 0)
                    { lblupdate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_partspurchaseorder_Model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary> 根据销售订单号获取销售配件信息
        /// </summary>
        /// <param name="sale_order_id"></param>
        private void GetAccessories(string sale_order_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询销售订单单配件表信息", "tb_parts_sale_order_p", "*", " sale_order_id='" + sale_order_id + "'", "", "");
            if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_parts_purchase.Rows)
                {
                    string default_price = "0";
                    string default_unit_id = string.Empty;
                    string data_source = string.IsNullOrEmpty(dr["car_factory_code"].ToString()) ? "1" : "2";
                    DataGridViewComboBoxCell dgcombox = null;
                    int CurrentRowIndenx = gvPurchaseList.Rows.Add();
                    DataGridViewRow dgvr = gvPurchaseList.Rows[CurrentRowIndenx];

                    dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];
                    BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), CurrentRowIndenx, ref dgcombox, ref default_unit_id, ref default_price);
                    dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                    dgvr.Cells["parts_brand"].Value = dr["parts_brand"];
                    dgvr.Cells["business_count"].Value = dr["business_count"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["tax_rate"].Value = dr["tax_rate"];
                    dgvr.Cells["tax"].Value = dr["tax"];
                    dgvr.Cells["payment"].Value = dr["payment"];
                    dgvr.Cells["valorem_together"].Value = dr["valorem_together"];
                    dgvr.Cells["auxiliary_count"].Value = dr["auxiliary_count"];
                    if (status == WindowStatus.Copy)
                    {
                        dgvr.Cells["relation_order"].Value = string.Empty;
                        dgvr.Cells["create_by"].Value = null;
                        dgvr.Cells["create_name"].Value = null;
                        dgvr.Cells["create_time"].Value = null;
                    }
                    else
                    { 
                        dgvr.Cells["relation_order"].Value = dr["relation_order"];
                        dgvr.Cells["create_by"].Value = dr["create_by"];
                        dgvr.Cells["create_name"].Value = dr["create_name"];
                        dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                    }
                    dgvr.Cells["is_gift"].Value = dr["is_gift"];
                    dgvr.Cells["is_suspend"].Value = dr["is_suspend"];

                    dgvr.Cells["delivery_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["delivery_time"]));
                    dgvr.Cells["remark"].Value = dr["remark"];

                    dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码


                    
                }
            }
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
        /// <summary> 数据保存/提交 方法
        /// </summary>
        /// <param name="HandleTypeName">保存/提交</param>
        void SaveEventOrSubmitEventFunction(string HandleTypeName)
        {
            try
            {
                gvPurchaseList.EndEdit();
                if (CheckDataInfo())
                {
                    string SucessMess = "保存";
                    string opName = "销售订单操作";
                    if (HandleTypeName == "提交")
                    {
                        SucessMess = "提交";
                        if (lblorder_num.Text == "")
                        {
                            lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.SaleOrder);//获取销售订单单编号
                            if (oldorder_num == lblorder_num.Text)
                            {
                                MessageBoxEx.Show("复制的单据生成的编号和原单据编号重复了，原单号是:" + oldorder_num + "，新单号是:" + lblorder_num.Text + "");
                            }
                        }
                        lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//获取销售订单单状态(已提交)
                    }

                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        sale_order_id = Guid.NewGuid().ToString();
                        AddPurchaseOrderSqlString(listSql, sale_order_id, HandleTypeName);
                        opName = "新增销售订单";
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditPurchaseOrderSqlString(listSql, sale_order_id, tb_partspurchaseorder_Model, HandleTypeName);
                        opName = "修改销售订单";
                    }
                    DealAccessories(listSql, sale_order_id);
                    GetPre_Order_Code(listSql, sale_order_id, lblorder_num.Text);
                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("" + SucessMess + "成功！");

                        if (HandleTypeName == "保存")
                        { ImportSalePlanStatus("0"); }
                        else if (HandleTypeName == "提交")
                        { SetOrderStatus(sale_order_id); }

                        uc.BindgvSaleOrderList();
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
        /// <summary> 在编辑和添加时对主数据和销售订单配件表中的配件信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="sale_order_id"></param>
        private void DealAccessories(List<SysSQLString> listSql, string sale_order_id)
        {
            string PartsCodes = GetListPartsCodes();
            DataTable dt_CarType = CommonFuncCall.GetCarType(PartsCodes);
            DataTable dt_PartsType = CommonFuncCall.GetPartsType(PartsCodes);

            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql1 = "delete from tb_parts_sale_order_p where sale_order_id=@sale_order_id;";
            dic.Add("sale_order_id", sale_order_id);
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
                    dic.Add("sale_order_parts_id", Guid.NewGuid().ToString());
                    dic.Add("sale_order_id", sale_order_id);
                    string parts_code = string.Empty;
                    if (dr.Cells["parts_code"].Value == null)
                    { parts_code = string.Empty; }
                    else
                    { parts_code = dr.Cells["parts_code"].Value.ToString(); }


                    dic.Add("parts_id", dr.Cells["parts_id"].Value == null ? "" : dr.Cells["parts_id"].Value.ToString());
                    dic.Add("parts_code", parts_code);
                    dic.Add("parts_name", dr.Cells["parts_name"].Value == null ? "" : dr.Cells["parts_name"].Value.ToString());
                    dic.Add("drawing_num", dr.Cells["drawing_num"].Value == null ? "" : dr.Cells["drawing_num"].Value.ToString());
                    if (dr.Cells["unit_id"].Value == null || dr.Cells["unit_id"].Value.ToString() == "")
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
                    dic.Add("valorem_together", dr.Cells["valorem_together"].Value == null ? "0" : dr.Cells["valorem_together"].Value.ToString() == "" ? "0" : dr.Cells["valorem_together"].Value.ToString());
                    dic.Add("auxiliary_count", dr.Cells["auxiliary_count"].Value == null ? "0" : dr.Cells["auxiliary_count"].Value.ToString() == "" ? "0" : dr.Cells["auxiliary_count"].Value.ToString());
                    dic.Add("relation_order", dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString());
                    dic.Add("is_gift", dr.Cells["is_gift"].Value == null ? "0" : dr.Cells["is_gift"].Value.ToString());
                    dic.Add("is_suspend", dr.Cells["is_suspend"].Value == null ? "1" : dr.Cells["is_suspend"].Value.ToString());

                    if (dr.Cells["delivery_time"].Value != null)
                    {
                        dic.Add("delivery_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(Convert.ToDateTime(dr.Cells["delivery_time"].Value.ToString()).ToShortDateString() + " 23:59:59")).ToString());
                    }
                    else
                    {
                        dic.Add("delivery_time", "0");
                    }
                    dic.Add("remark", dr.Cells["remark"].Value == null ? "" : dr.Cells["remark"].Value.ToString());
                    dic.Add("parts_barcode", dr.Cells["parts_barcode"].Value == null ? "" : dr.Cells["parts_barcode"].Value.ToString());
                    dic.Add("car_factory_code", dr.Cells["car_factory_code"].Value == null ? "" : dr.Cells["car_factory_code"].Value.ToString());
                    dic.Add("create_by", dr.Cells["create_by"].Value == null ? "" : dr.Cells["create_by"].Value.ToString());
                    dic.Add("create_name", dr.Cells["create_name"].Value == null ? "" : dr.Cells["create_name"].Value.ToString());
                    dic.Add("create_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["create_time"].Value == null ? DateTime.Now.ToString() : dr.Cells["create_time"].Value.ToString())).ToString());
                    dic.Add("update_by", GlobalStaticObj.UserID);
                    dic.Add("update_name", GlobalStaticObj.UserName);
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
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

                    string sql2 = string.Format(@"Insert Into tb_parts_sale_order_p(sale_order_parts_id,sale_order_id,parts_id,parts_code,parts_name,drawing_num,unit_id,unit_name,
                    parts_brand,parts_brand_name,business_count,original_price,discount,business_price,tax_rate,tax,payment,valorem_together,
                    auxiliary_count,relation_order,is_gift,is_suspend,delivery_time,remark,parts_barcode,car_factory_code,create_by,create_name,create_time,
                    update_by,update_name,update_time,finish_count,vm_name,parts_type_id,parts_type_name) values(@sale_order_parts_id,@sale_order_id,@parts_id,@parts_code,
                    @parts_name,@drawing_num,@unit_id,@unit_name,
                    @parts_brand,@parts_brand_name,@business_count,@original_price,@discount,@business_price,@tax_rate,@tax,@payment,@valorem_together,
                    @auxiliary_count,@relation_order,@is_gift,@is_suspend,@delivery_time,@remark,@parts_barcode,@car_factory_code,@create_by,@create_name,@create_time,
                    @update_by,@update_name,@update_time,@finish_count,@vm_name,@parts_type_id,@parts_type_name);");
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
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

                dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                dgvr.Cells["parts_code"].Value = dr["ser_parts_code"];//配件编码
                dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                BindDataGridViewComboBoxCell(data_source, dr["ser_parts_code"].ToString(), rowIndex, ref dgcombox, ref default_unit_id, ref default_price);
                dgvr.Cells["unit_id"].Value = default_unit_id;//单位
                //dgvr.Cells["unit_id"].Value = dr["default_unit"];//单位
                dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌
                dgvr.Cells["business_price"].Value = dr["ref_in_price"];//业务单价 从配件信息的参考进价 中获取
                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码

                dgvr.Cells["business_count"].Value = "0";//业务数量
                dgvr.Cells["original_price"].Value = default_price;//原始单价
                dgvr.Cells["discount"].Value = "100";//折扣
                dgvr.Cells["business_price"].Value = default_price;//业务单价
                dgvr.Cells["tax_rate"].Value = "0";//税率
                dgvr.Cells["tax"].Value = "0";//税款
                dgvr.Cells["payment"].Value = "0";//货款
                dgvr.Cells["valorem_together"].Value = "0";//价税合计
                if (ddldelivery_time.Value != null)
                {
                    dgvr.Cells["delivery_time"].Value = ddldelivery_time.Value.ToShortDateString();//发货时间
                }
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
        void GetGridViewRowByDrImport(DataGridViewRow dgvr, DataRow dr, string relation_order, int rowIndex)
        {
            try
            {
                string default_price = "0";
                string default_unit_id = string.Empty;
                string data_source = string.IsNullOrEmpty(dr["car_factory_code"].ToString()) ? "1" : "2";
                DataGridViewComboBoxCell dgcombox = null;
                dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                dgvr.Cells["parts_code"].Value = dr["parts_code"];//配件编码
                dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), rowIndex, ref dgcombox, ref default_unit_id, ref default_price);
                dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌

                dgvr.Cells["business_count"].Value = decimal.Parse(dr["business_count"].ToString() == "" ? "0" : dr["business_count"].ToString()) - decimal.Parse(dr["finish_count"].ToString() == "" ? "0" : dr["finish_count"].ToString());//剩余的 业务数量
                dgvr.Cells["original_price"].Value = dr["original_price"];//原始单价
                dgvr.Cells["discount"].Value = dr["discount"];//折扣
                dgvr.Cells["business_price"].Value = dr["business_price"];//业务单价
                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码
                dgvr.Cells["relation_order"].Value = relation_order;
                if (ddldelivery_time.Value != null)
                {
                    dgvr.Cells["delivery_time"].Value = ddldelivery_time.Value.ToShortDateString();//发货时间
                }
                dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                dgvr.Cells["create_time"].Value = DateTime.Now;
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="sale_order_id"></param>
        private void AddPurchaseOrderSqlString(List<SysSQLString> listSql, string sale_order_id, string HandleType)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            decimal business_count = 0;
            decimal payment = 0;
            decimal tax = 0;
            decimal valorem_together = 0;
            GetBuesinessMoney(ref business_count, ref payment, ref tax, ref valorem_together);

            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            ddtvalid_till.Value = Convert.ToDateTime(ddtvalid_till.Value.ToShortDateString() + " 23:59:59");
            //ddldelivery_time.Value = Convert.ToDateTime(ddldelivery_time.Value.ToShortDateString() + " 23:59:59");
            tb_parts_sale_order model = new tb_parts_sale_order();
            CommonFuncCall.SetModelObjectValue(this, model);
            model.cust_id = cust_id;
            model.valid_till = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(ddldelivery_time.Value.ToShortDateString() + " 23:59:59"));
            model.cust_code = txtcust_code1.Text.Trim();
            model.cust_name = txtcust_name.Caption.Trim();
            model.closing_unit = txtclosing_unit1.Text;
            model.sale_order_id = sale_order_id;
            model.create_by = GlobalStaticObj.UserID;
            model.create_name = GlobalStaticObj.UserName;
            model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.is_suspend = chkis_suspend.Checked ? "0" : "1";//选中(中止)：0,未选中(不中止)：1

            model.suspend_reason = txtsuspend_reason.Caption.Trim();//中止原因
            model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
            model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称
            model.order_quantity = business_count;//订货数量
            model.payment = payment;//货款
            model.tax = tax;//税款
            model.money = valorem_together;//金额
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

            if (!string.IsNullOrEmpty(ddltrans_way.SelectedValue.ToString()))
            {
                model.trans_way = ddltrans_way.SelectedValue.ToString();
                model.trans_way_name = ddltrans_way.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlclosing_way.SelectedValue.ToString()))
            {
                model.closing_way = ddlclosing_way.SelectedValue.ToString();
                model.closing_way_name = ddlclosing_way.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlorg_id.SelectedValue.ToString()))
            {
                model.org_id = ddlorg_id.SelectedValue.ToString();
                model.org_name = ddlorg_id.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                model.handle = ddlhandle.SelectedValue.ToString();
                model.handle_name = ddlhandle.SelectedItem.ToString();
            }
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Insert Into tb_parts_sale_order( ");
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
        /// <param name="sale_order_id"></param>
        /// <param name="model"></param>
        private void EditPurchaseOrderSqlString(List<SysSQLString> listSql, string sale_order_id, tb_parts_sale_order model, string HandleType)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            decimal business_count = 0;
            decimal payment = 0;
            decimal tax = 0;
            decimal valorem_together = 0;
            GetBuesinessMoney(ref business_count, ref payment, ref tax, ref valorem_together);

            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            ddtvalid_till.Value = Convert.ToDateTime(ddtvalid_till.Value.ToShortDateString() + " 23:59:59");
            //ddldelivery_time.Value = Convert.ToDateTime(ddldelivery_time.Value.ToShortDateString() + " 23:59:59");
            CommonFuncCall.SetModelObjectValue(this, model);
            model.cust_id = cust_id;
            model.valid_till = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(ddldelivery_time.Value.ToShortDateString() + " 23:59:59"));
            model.cust_code = txtcust_code1.Text.Trim();
            model.cust_name = txtcust_name.Caption.Trim();
            model.closing_unit = txtclosing_unit1.Text;
            model.update_by = GlobalStaticObj.UserID;
            model.update_name = GlobalStaticObj.UserName;
            model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.is_suspend = chkis_suspend.Checked ? "0" : "1";//选中(中止)：0,未选中(不中止)：1

            model.suspend_reason = txtsuspend_reason.Caption.Trim();//中止原因
            model.order_quantity = business_count;//订货数量
            model.payment = payment;//货款
            model.tax = tax;//税款
            model.money = valorem_together;//金额

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

            if (!string.IsNullOrEmpty(ddltrans_way.SelectedValue.ToString()))
            {
                model.trans_way = ddltrans_way.SelectedValue.ToString();
                model.trans_way_name = ddltrans_way.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlclosing_way.SelectedValue.ToString()))
            {
                model.closing_way = ddlclosing_way.SelectedValue.ToString();
                model.closing_way_name = ddlclosing_way.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlorg_id.SelectedValue.ToString()))
            {
                model.org_id = ddlorg_id.SelectedValue.ToString();
                model.org_name = ddlorg_id.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                model.handle = ddlhandle.SelectedValue.ToString();
                model.handle_name = ddlhandle.SelectedItem.ToString();
            }
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Update tb_parts_sale_order Set ");
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
                sb.Append(" where sale_order_id='" + sale_order_id + "';");
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
                                if (dr.Cells["parts_code"].Value != null)
                                {
                                    string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                                    if (dr.Cells["parts_code"].Value.ToString() == PartsCode && relation_order == "")
                                    {
                                        MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
                                        return;
                                    }
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
        /// <summary> 获取业务数量为0的配件行数
        /// </summary>
        /// <returns></returns>
        private int GetZeroPartsCount()
        {
            int AllCount = 0;
            try
            {
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        string business_count = dr.Cells["business_count"].EditedFormattedValue == null ? "0" : dr.Cells["business_count"].EditedFormattedValue.ToString();
                        business_count = string.IsNullOrEmpty(business_count) ? "0" : business_count;
                        if (Convert.ToDecimal(business_count) == 0)
                        {
                            AllCount = AllCount + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return AllCount;
        }
        /// <summary> 重写关闭当前窗体的函数
        /// </summary>
        /// <returns></returns>
        public override bool CloseMenu()
        {
            if (IsClose)
            {
                //当取消单据时，要修改单据引用的前置单据状态。前置单据为占用时恢复为正常，其它的状态不做修改。
                try
                {
                    for (int i = 0; i < gvPurchaseList.Rows.Count; i++)
                    {
                        DataGridViewRow dr = gvPurchaseList.Rows[i];
                        if (dr.Cells["parts_code"].Value != null)
                        {
                            #region 当删除成功时，将销售计划单状态释放为正常
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_sale_plan", "import_status", " order_num='" + relation_order + "'", "", "");
                                    if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                                    {
                                        string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                                        if (import_status == "1")
                                        {
                                            Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                            dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定
                                            DBHelper.Submit_AddOrEdit("修改销售计划单的导入状态为正常", "tb_parts_sale_plan", "order_num", relation_order, dicValue);
                                        }
                                    }
                                }
                            }
                            gvPurchaseList.Rows.Remove(dr);
                            i = -1;
                            #endregion
                        }
                    }
                }
                catch (Exception ex)
                { }
            }
            return true;
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
                if (ColumnName == "original_price" || ColumnName == "discount" || ColumnName == "business_price" || 
                    ColumnName == "business_count" || ColumnName == "tax_rate")
                {
                    c.Text = string.IsNullOrEmpty(c.Text) ? "0" : c.Text;
                    decimal outret = 0;
                    if (!decimal.TryParse(c.Text, out outret))
                    {
                        return;
                    }
                    ValidationRegex.GridViewTextBoxChangedFolat(c, ValidationRegex.MaxTextNum);
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
                        string original_price = gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value == null ? "1" :
                                                gvPurchaseList.Rows[NewrowIndex].Cells["original_price"].Value.ToString();
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
                        //if (Convert.ToDecimal(c.Text) < 0)
                        //{ c.ForeColor = Color.Red; }
                        //else
                        //{ c.ForeColor = Color.Black; }
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

        /// <summary> 在提交/保存的时候 自动计算出列表中的订货数量、货款、税款、价税合计
        /// </summary>
        /// <param name="business_counts">订货数量</param>
        /// <param name="payment">货款</param>
        /// <param name="tax">税款</param>
        /// <param name="valorem_together">价税合计</param>
        void GetBuesinessMoney(ref decimal business_counts, ref decimal payment, ref decimal tax, ref decimal valorem_together)
        {
            decimal Newbusiness_counts = 0;
            decimal Newpayment = 0;
            decimal Newtax = 0;
            decimal Newvalorem_together = 0;
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                Newbusiness_counts = Newbusiness_counts + Convert.ToDecimal(dr.Cells["business_count"].Value == null ? "0" : dr.Cells["business_count"].Value.ToString() == "" ? "0" : dr.Cells["business_count"].Value.ToString());
                Newpayment = Newpayment + Convert.ToDecimal(dr.Cells["payment"].Value == null ? "0" : dr.Cells["payment"].Value.ToString() == "" ? "0" : dr.Cells["payment"].Value.ToString());
                Newtax = Newtax + Convert.ToDecimal(dr.Cells["tax"].Value == null ? "0" : dr.Cells["tax"].Value.ToString() == "" ? "0" : dr.Cells["tax"].Value.ToString());
                Newvalorem_together = Newvalorem_together + Convert.ToDecimal(dr.Cells["valorem_together"].Value == null ? "0" : dr.Cells["valorem_together"].Value.ToString() == "" ? "0" : dr.Cells["valorem_together"].Value.ToString());
            }
            business_counts = Newbusiness_counts;
            payment = Newpayment;
            tax = Newtax;
            valorem_together = Newvalorem_together;
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

                    //根据配件数据来源，判断给什么单价(采购业务时：自建的配件信息用参考进价，宇通的配件用销售2A价)
                    if (data_source == "1")//自建配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_out_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_out_price"].ToString()) ? "0" : dt.Rows[i]["ref_out_price"].ToString());
                    }
                    else if (data_source == "2")//宇通配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_out_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_out_price"].ToString()) ? "0" : dt.Rows[i]["ref_out_price"].ToString());
                    }
                    //采购业务中，判断单位是否是采购单位
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
        /// <summary> 提交时获取当前配件列表中存在的引用单号,保存到中间表中
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
                string FileName = string.Format(@" tb_plan.order_num,tb_plan.parts_code,sum(tb_order.business_count) relation_count ");
                string TableName = string.Format(@" (
                                                      select b.relation_order,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                      inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id
                                                      where a.order_status in ('1','2')
                                                    ) tb_order
                                                    left join 
                                                    (
                                                      select a.order_num,b.parts_code,b.business_count from tb_parts_sale_plan a 
                                                      inner join tb_parts_sale_plan_p b on a.sale_plan_id=b.sale_plan_id  {0}
                                                    ) tb_plan 
                                                     on tb_plan.order_num=tb_order.relation_order and tb_plan.parts_code=tb_order.parts_code 
                                                    group by tb_plan.order_num,tb_plan.parts_code ", files);
                return dt = DBHelper.GetTable("查询销售购订单导入销售计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            { return dt; }
            finally { }
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
                string FileName = string.Format(@" a.sale_plan_id,b.order_num,a.parts_code,a.business_count ");
                string TableName = string.Format(@" (
	                                                  select * from tb_parts_sale_plan_p where sale_plan_id in
	                                                  (
		                                                 select sale_plan_id from tb_parts_sale_plan where order_num in
		                                                 (
			                                                select relation_order from tb_parts_sale_order_p 
			                                                where sale_order_id='{0}' and len(relation_order)>0 group by relation_order
		                                                 )
	                                                  )
                                                    ) a left join tb_parts_sale_plan b on a.sale_plan_id=b.sale_plan_id ", order_id);
                return dt = DBHelper.GetTable("查询销售订单导入销售计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        /// <summary>批量修改销售计划单导入状态 
        /// </summary>
        /// <param name="status">保存时：0 释放为正常，提交时：2 锁定</param>
        void ImportSalePlanStatus(string status)
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
            Field.Add("import_status", status);//单据导入状态，0正常，1占用，2锁定
            DBHelper.BatchUpdateDataByIn("批量修改销售计划单导入状态", "tb_parts_sale_plan", Field, "order_num", listField.ToArray());
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
            List<string> list_plan = new List<string>();
            string plan_ids = string.Empty;

            #region 更新前置单据的导入状态字段
            foreach (OrderImportStatus item in list_order)
            {
                sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                dic = new Dictionary<string, string>();
                string sql1 = "update tb_parts_sale_plan set import_status=@import_status where order_num=@order_num;";
                dic.Add("import_status", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                dic.Add("order_num", item.order_num);
                sysStringSql.sqlString = sql1;
                sysStringSql.Param = dic;
                listSql.Add(sysStringSql);
            } 
            #endregion

            #region 更新前置单据中的各个配件的已完成数量
            foreach (OrderFinishInfo item in list_orderinfo)
            {
                if (!list_plan.Contains(item.sale_plan_id))
                {
                    list_plan.Add(item.sale_plan_id);
                    plan_ids = plan_ids + "'" + item.sale_plan_id + "',";
                }
                sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                dic = new Dictionary<string, string>();
                string sql1 = "update tb_parts_sale_plan_p set finish_count=@finish_count where sale_plan_id=@sale_plan_id and parts_code=@parts_code;";
                dic.Add("finish_count", item.finish_num);
                dic.Add("sale_plan_id", item.sale_plan_id);
                dic.Add("parts_code", item.parts_code);
                sysStringSql.sqlString = sql1;
                sysStringSql.Param = dic;
                listSql.Add(sysStringSql);
            } 
            #endregion
            bool ret = DBHelper.BatchExeSQLStringMultiByTrans("提交销售订单，更新引用的销售计划单或销售订单的导入状态", listSql);
            if (ret)
            {
                if (list_orderinfo.Count > 0)
                {
                    listSql.Clear();
                    plan_ids = plan_ids.Trim(',');
                    string TableName = string.Format(@"
                    (
                        select sale_plan_id,sum(finish_count) finish_count,
                        sum(finish_count*business_price) as finish_money 
                            from tb_parts_sale_plan_p 
                         where sale_plan_id in ({0})
                        group by sale_plan_id
                    ) tb_sale_finish", plan_ids);
                    DataTable dt = DBHelper.GetTable("查询销售计划单各配件完成数量和完成金额", TableName, "*", "", "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sysStringSql = new SysSQLString();
                            sysStringSql.cmdType = CommandType.Text;
                            dic = new Dictionary<string, string>();
                            string sql1 = "update tb_parts_sale_plan set finish_counts=@finish_counts,finish_money=@finish_money where sale_plan_id=@sale_plan_id;";
                            dic.Add("finish_counts", dt.Rows[i]["finish_count"].ToString());
                            dic.Add("finish_money", dt.Rows[i]["finish_money"].ToString());
                            dic.Add("sale_plan_id", dt.Rows[i]["sale_plan_id"].ToString());
                            sysStringSql.sqlString = sql1;
                            sysStringSql.Param = dic;
                            listSql.Add(sysStringSql);
                        }
                        DBHelper.BatchExeSQLStringMultiByTrans("完成销售订单后，更新销售计划单的完成数量和完成金额", listSql);
                    }
                }
            }
        }
        /// <summary> 提交成功时,对引用的前置单据的状态进行更新 
        /// </summary>
        /// <param name="saleorderid"></param>
        void SetOrderStatus(string saleorderid)
        {
            //前置单据中的配件信息是否在后置单据中全部导入完成（完成数量>=计划数量）
            List<OrderImportStatus> list_order = new List<OrderImportStatus>();
            List<OrderFinishInfo> list_orderinfo = new List<OrderFinishInfo>();
            OrderImportStatus orderimport_model = new OrderImportStatus();
            OrderFinishInfo orderfinish_info = new OrderFinishInfo();

            DataTable dt_Business = GetBusinessCount(saleorderid);
            DataTable dt_Finish = GetFinishCount();

            string sale_plan_id = string.Empty;
            string order_num = string.Empty;
            string parts_code = string.Empty;
            if (dt_Business.Rows.Count > 0)
            {
                for (int i = 0; i < dt_Business.Rows.Count; i++)
                {
                    bool isfinish = true;
                    int BusinessCount = int.Parse(dt_Business.Rows[i]["business_count"].ToString());
                    sale_plan_id = dt_Business.Rows[i]["sale_plan_id"].ToString();
                    order_num = dt_Business.Rows[i]["order_num"].ToString();
                    parts_code = dt_Business.Rows[i]["parts_code"].ToString();
                    DataRow[] dr = dt_Finish.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                    if (dr != null && dr.Count() > 0)
                    {
                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.sale_plan_id = sale_plan_id;
                        orderfinish_info.parts_code = parts_code;
                        orderfinish_info.finish_num = dr[0]["relation_count"].ToString();
                        list_orderinfo.Add(orderfinish_info);
                        if (int.Parse(dr[0]["relation_count"].ToString()) < BusinessCount)
                        {
                            isfinish = false;
                        }
                    }
                    else
                    {
                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.sale_plan_id = sale_plan_id;
                        orderfinish_info.parts_code = parts_code;
                        orderfinish_info.finish_num = "0";
                        list_orderinfo.Add(orderfinish_info);

                        isfinish = false;
                    }

                    orderimport_model = new OrderImportStatus();
                    orderimport_model.order_num = order_num;
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
        private void Closing_UnitName_DataBack(DataRow dr)
        {
            if (dr.Table.Columns.Contains("cust_name"))
            {
                this.txtclosing_unit1.Text = dr["cust_name"].ToString();
                this.txtcust_name.Caption = dr["cust_name"].ToString();
                if (!string.IsNullOrEmpty(txtcust_code1.Text.Trim()))
                {
                    DataTable dt = DBHelper.GetTable("查询客户档案信息", "tb_customer", "*", " enable_flag != 0 and cust_code='" + txtcust_code1 + "'", "", "");
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
    }

    #region 前置单据信息类
    class OrderImportStatus
    {
        public string order_num;
        public bool isfinish;
    }
    class OrderFinishInfo
    {
        public string sale_plan_id;
        public string parts_code;
        public string finish_num;
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
