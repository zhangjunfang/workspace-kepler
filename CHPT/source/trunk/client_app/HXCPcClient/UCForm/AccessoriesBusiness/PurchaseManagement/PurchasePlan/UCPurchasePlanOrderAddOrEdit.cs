using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;
using Utility.Common;
using Model;
using System.Reflection;
using System.Drawing;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    public partial class UCPurchasePlanOrderAddOrEdit : UCBase
    {
        List<partunitprice> list_partunitprice = new List<partunitprice>();
        List<lastunitclass> list_lastunitprice = new List<lastunitclass>();
        #region 变量
        WindowStatus status;
        UCPurchasePlanOrderManager uc;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        DataTable dt_parts_brand = null;
        private int rowIndex = -1;
        private int oldcolcontindex = -1;
        private string plan_id = string.Empty;
        private tb_parts_purchase_plan tb_purchase_Model = new tb_parts_purchase_plan();
        private string oldorder_num = string.Empty;
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="status"></param>
        /// <param name="plan_id"></param>
        /// <param name="uc"></param>
        public UCPurchasePlanOrderAddOrEdit(WindowStatus status, string plan_id, UCPurchasePlanOrderManager uc)
        {
            InitializeComponent();

            this.uc = uc;
            this.status = status;
            this.plan_id = plan_id;
            this.Resize += new EventHandler(UCPurchasePlanOrderAddOrEdit_Resize);
            base.SaveEvent += new ClickHandler(UCPurchasePlanOrderAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCPurchasePlanOrderAddOrEdit_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCPurchasePlanOrderAddOrEdit_CancelEvent);
            gvPurchasePlanList.CellDoubleClick += new DataGridViewCellEventHandler(gvPurchasePlanList_CellDoubleClick);


            business_counts.ValueType = typeof(decimal);
            //business_counts.MaxInputLength = 9;
            original_price.ValueType = typeof(decimal);
            //original_price.MaxInputLength = 9;
            discount.ValueType = typeof(decimal);
            discount.MaxInputLength = 3;
            business_price.ValueType = typeof(decimal);
            //business_price.MaxInputLength = 9;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchasePlanOrderAddOrEdit_Load(object sender, EventArgs e)
        {
            try
            {
                base.SetBaseButtonStatus();
                if (status == WindowStatus.Add || status == WindowStatus.Copy)
                { base.SetButtonVisiableHandleAddCopy(); }
                else if (status == WindowStatus.Edit)
                { base.SetButtonVisiableHandleEdit(); }
                base.btnImport.Visible = false;

                //CommonFuncCall.BindUnit(unit_id);
                string[] NotReadOnlyColumnsName = new string[] { "colCheck", "unit_id", "original_price", "business_price", "business_counts", "discount", "remark" };
                CommonFuncCall.SetColumnReadOnly(gvPurchasePlanList, NotReadOnlyColumnsName);

                ddtorder_date.Value = DateTime.Now;
                ddtplan_start_time.Value = DateTime.Now;
                ddtplan_end_time.Value = DateTime.Now.AddYears(1);
                lbloperator_name.Text = GlobalStaticObj.UserName;

                string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
                CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
                CommonFuncCall.BindHandle(ddlhandle, "", "请选择");
                dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");

                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    LoadInfo(plan_id);
                    GetAccessories(plan_id);
                }
                if (status == WindowStatus.Add || status == WindowStatus.Copy)
                {
                    lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);//获取采购计划单状态(草稿)
                }
                gvPurchasePlanList.Rows.Add(1);
                //List<string> list_columns = new List<string>();
                //list_columns.Add("original_price");
                //list_columns.Add("business_price");
                //list_columns.Add("business_counts");
                //list_columns.Add("discount");
                //ControlsConfig.NumberLimitdgv(gvPurchasePlanList, list_columns, true);
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 控件事件
        /// <summary> 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("保存");
        }
        /// <summary> 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("提交");
        }
        /// <summary> 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消操作关闭当前界面吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        /// <summary> 添加列表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchasePlanAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmParts frm = new frmParts();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string PartsCode = frm.PartsCode;
                    if (gvPurchasePlanList.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
                        {
                            if (dr.Cells["parts_code"].Value != null)
                            {
                                if (dr.Cells["parts_code"].Value.ToString() == PartsCode)
                                {
                                    MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
                                    return;
                                }
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
                        string default_price = "0";
                        string default_unit_id = string.Empty;
                        string data_source = dr["data_source"].ToString();
                        DataGridViewComboBoxCell dgcombox = null;

                        //int rowsindex = gvPurchasePlanList.Rows.Add();
                        int rowsindex = gvPurchasePlanList.Rows.Count - 1;
                        DataGridViewRow dgvr = gvPurchasePlanList.Rows[rowsindex];
                        dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                        dgvr.Cells["parts_code"].Value = dr["ser_parts_code"];//配件编码
                        dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                        dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                        BindDataGridViewComboBoxCell(data_source, dr["ser_parts_code"].ToString(), rowsindex, ref dgcombox, ref default_unit_id, ref default_price);
                        dgvr.Cells["unit_id"].Value = default_unit_id;//单位
                        //dgvr.Cells["unit_id"].Value = dr["default_unit"];//单位
                        dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌
                        dgvr.Cells["original_price"].Value = default_price;//原始单价
                        dgvr.Cells["discount"].Value = "100";//折扣
                        dgvr.Cells["business_price"].Value = default_price;//业务单价
                        dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                        dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码

                        dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                        dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                        dgvr.Cells["create_time"].Value = DateTime.Now;

                        string sup_code = string.Empty;
                        string sup_name = string.Empty;
                        string pur_price = string.Empty;
                        GetSupinfo(dr["ser_parts_code"].ToString(), ref sup_code, ref sup_name, ref pur_price);
                        dgvr.Cells["recent_supplier_code"].Value = sup_code;
                        dgvr.Cells["recent_purchase_name"].Value = sup_name;
                        dgvr.Cells["recent_purchase_price"].Value = pur_price;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvPurchasePlanList.Rows.Add(1);
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldcolcontindex = -1; }
        }
        /// <summary> 编辑列表中的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchasePlanEdit_Click(object sender, EventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 删除列表中的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchasePlanDelete_Click(object sender, EventArgs e)
        {
            #region 原来的单个删除的方法
            //if (oldcolcontindex > -1)
            //{
            //    try
            //    {
            //        if (gvPurchasePlanList.Rows[oldcolcontindex].Cells["parts_code"].Value != null)
            //        {
            //            gvPurchasePlanList.Rows.Remove(gvPurchasePlanList.Rows[oldcolcontindex]);
            //            GetBuessinessMoney();
            //        }
            //    }
            //    catch (Exception ex)
            //    { }
            //    finally
            //    { oldcolcontindex = -1; }
            //} 
            #endregion
            try
            {
                List<DataGridViewRow> list_dr = GetSelectedRecord();
                if (list_dr.Count > 0)
                {
                    if (MessageBoxEx.Show("确认要删除选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        return;
                    }
                    foreach (var item in list_dr)
                    {
                        gvPurchasePlanList.Rows.Remove(item);
                    }
                    GetBuessinessMoney();
                }
                else
                {
                    MessageBoxEx.Show("请勾选要删除的配件!");
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldcolcontindex = -1; }
        }
        /// <summary> 删除零数量配件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsDeleteZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvPurchasePlanList.Rows.Count > 0)
                {
                    if (GetZeroPartsCount() > 0)
                    {
                        if (MessageBoxEx.Show("确认要删除业务数量为零的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        {
                            return;
                        }
                        for (int i = 0; i < gvPurchasePlanList.Rows.Count; i++)
                        {
                            DataGridViewRow dr = gvPurchasePlanList.Rows[i];
                            if (dr.Cells["parts_code"].Value != null)
                            {
                                string business_counts = dr.Cells["business_counts"].EditedFormattedValue == null ? "0" : dr.Cells["business_counts"].EditedFormattedValue.ToString();
                                business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
                                if (Convert.ToDecimal(business_counts) == 0)
                                {
                                    gvPurchasePlanList.Rows.Remove(dr);
                                    i = -1;
                                }
                            }
                        }
                        GetBuessinessMoney();
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldcolcontindex = -1; }
        }
        /// <summary> 库存查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsStockSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (oldcolcontindex > -1)//双击表头或列头时不起作用   
                {
                    string parts_id = gvPurchasePlanList.Rows[oldcolcontindex].Cells["parts_id"].Value == null ? "" : gvPurchasePlanList.Rows[oldcolcontindex].Cells["parts_id"].Value.ToString();
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
        /// <summary> 鼠标进入单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    oldcolcontindex = e.RowIndex;
                    gvPurchasePlanList.Rows[oldcolcontindex].Selected = true;
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 单元格格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dt_parts_brand.Rows.Count > 0)
                {
                    if (e.Value == null || e.Value.ToString().Length == 0)
                    {
                        return;
                    }
                    string fieldNmae = gvPurchasePlanList.Columns[e.ColumnIndex].DataPropertyName;
                    if (fieldNmae.Equals("parts_brand"))
                    {
                        for (int i = 0; i < dt_parts_brand.Rows.Count; i++)
                        {
                            if (dt_parts_brand.Rows[i]["dic_id"].ToString() == e.Value.ToString())
                            {
                                gvPurchasePlanList.Rows[e.RowIndex].Cells["parts_brand_name"].Value = dt_parts_brand.Rows[i]["dic_name"].ToString();
                                break;
                            }
                        }
                    }
                    if (fieldNmae.Equals("business_counts"))
                    {
                        //string num = gvPurchasePlanList.Rows[e.RowIndex].Cells["business_counts"].Value.ToString();
                        //num = string.IsNullOrEmpty(num) ? "0" : num;
                        //if (Convert.ToDecimal(num) < 0)
                        //{ gvPurchasePlanList.Rows[e.RowIndex].Cells["business_counts"].Style.ForeColor = Color.Red; }
                        //else
                        //{ gvPurchasePlanList.Rows[e.RowIndex].Cells["business_counts"].Style.ForeColor = Color.Black; }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 双击列表，弹出配件选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvPurchasePlanList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 统计数量、金额的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderAddOrEdit_Resize(object sender, EventArgs e)
        {
            string[] total = { business_counts.Name, total_money.Name };
            ControlsConfig.DatagGridViewTotalConfig(gvPurchasePlanList, total);
        } 
        #endregion

        #region 方法、函数
        /// <summary> 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            bool check = true;
            try
            {
                if (Convert.ToDateTime(ddtplan_start_time.Value.ToShortDateString() + " 00:00:00") > Convert.ToDateTime(ddtplan_end_time.Value.ToShortDateString() + " 00:00:00"))
                {
                    MessageBoxEx.Show("计划区间，开始时间不可以大于结束时间"); return false;
                }
                if ((Convert.ToDateTime(ddtplan_end_time.Value.ToShortDateString() + " 00:00:00")-Convert.ToDateTime(ddtplan_start_time.Value.ToShortDateString() + " 00:00:00")).Days>365)
                {
                    MessageBoxEx.Show("计划区间时间跨越值不可以超出一年，请修改"); return false;
                }
                //foreach (DataGridViewRow dgvr in gvPurchasePlanList.Rows)
                //{
                //    string SelectUnit = CommonCtrl.IsNullToString(dgvr.Cells["unit_id"].Value);//配件单位
                //    if (SelectUnit.Length == 0)
                //    {
                //        MessageBoxEx.Show("请选择配件单位!");
                //        gvPurchasePlanList.CurrentCell = dgvr.Cells["unit_id"];
                //        return false;
                //    }
                //}
            }
            catch (Exception ex)
            { }
            return check;
        }
        /// <summary> 加载采购计划信息和配件信息
        /// </summary>
        /// <param name="planId"></param>
        private void LoadInfo(string planId)
        {
            try
            {
                if (!string.IsNullOrEmpty(planId))
                {
                    //1.查看一条采购计划单信息
                    DataTable dt = DBHelper.GetTable("查看一条采购计划单信息", "tb_parts_purchase_plan", "*", " plan_id='" + planId + "'", "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CommonFuncCall.SetModlByDataTable(tb_purchase_Model, dt);
                        CommonFuncCall.SetShowControlValue(this, tb_purchase_Model, "");
                        chkis_suspend.Checked = tb_purchase_Model.is_suspend == "0";//选中(中止)：0,未选中(不中止)：1
                        oldorder_num = tb_purchase_Model.order_num;
                        if (status == WindowStatus.Copy)
                        {
                            lblorder_num.Text = "";
                            txtplan_finish_money.Caption = "0";
                        }
                        lblcreate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.create_time.ToString())).ToString();
                        if (tb_purchase_Model.update_time > 0)
                        { lblupdate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.update_time.ToString())).ToString(); }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 根据采购计划单号获取采购配件信息
        /// </summary>
        /// <param name="plan_id"></param>
        private void GetAccessories(string plan_id)
        {
            try
            {
                string conId = string.Empty;
                DataTable dt_parts_purchase = DBHelper.GetTable("查询采购计划单配件表信息", "tb_parts_purchase_plan_p", "*", " plan_id='" + plan_id + "'", "", "");
                if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_parts_purchase.Rows)
                    {
                        string default_price = "0";
                        string default_unit_id = string.Empty;
                        string data_source = string.IsNullOrEmpty(dr["car_factory_code"].ToString()) ? "1" : "2";
                        DataGridViewComboBoxCell dgcombox = null;
                        int CurrentRowIndenx = gvPurchasePlanList.Rows.Add();
                        DataGridViewRow dgvr = gvPurchasePlanList.Rows[CurrentRowIndenx];
                        dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                        dgvr.Cells["parts_code"].Value = dr["parts_code"];
                        dgvr.Cells["parts_name"].Value = dr["parts_name"];
                        dgvr.Cells["parts_brand"].Value = dr["parts_brand"];
                        dgvr.Cells["drawing_num"].Value = dr["drawing_num"];
                        BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), CurrentRowIndenx, ref dgcombox, ref default_unit_id, ref default_price);
                        dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                        //dgvr.Cells["unit_name"].Value = dr["unit_name"];
                        dgvr.Cells["original_price"].Value = dr["original_price"];
                        dgvr.Cells["discount"].Value = dr["discount"];
                        dgvr.Cells["business_price"].Value = dr["business_price"];
                        dgvr.Cells["business_counts"].Value = dr["business_counts"];
                        dgvr.Cells["total_money"].Value = dr["total_money"];
                        dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];
                        dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];
                        dgvr.Cells["recent_supplier_code"].Value = dr["recent_supplier_code"];
                        dgvr.Cells["recent_purchase_price"].Value = dr["recent_purchase_price"];
                        dgvr.Cells["recent_purchase_name"].Value = dr["recent_purchase_name"];
                        dgvr.Cells["finish_counts"].Value = status == WindowStatus.Copy ? "0" : dr["finish_counts"];
                        dgvr.Cells["relation_order"].Value = dr["relation_order"];
                        dgvr.Cells["is_suspend"].Value = dr["is_suspend"];
                        dgvr.Cells["remark"].Value = dr["remark"];
                        if (status == WindowStatus.Copy)
                        {
                            dgvr.Cells["create_by"].Value = null;
                            dgvr.Cells["create_name"].Value = null;
                            dgvr.Cells["create_time"].Value = null;
                        }
                        else
                        {
                            dgvr.Cells["create_by"].Value = dr["create_by"];
                            dgvr.Cells["create_name"].Value = dr["create_name"];
                            dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
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
        /// <summary> 在编辑和添加时对主数据和采购计划单配件表中的配件信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="supID"></param>
        private void DealAccessories(List<SysSQLString> listSql, string plan_id)
        {
            try
            {
                string PartsCodes = GetListPartsCodes();
                DataTable dt_CarType = CommonFuncCall.GetCarType(PartsCodes);
                DataTable dt_PartsType = CommonFuncCall.GetPartsType(PartsCodes);

                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                if (gvPurchasePlanList.Rows.Count > 0)
                {
                    string sql1 = "delete from tb_parts_purchase_plan_p where plan_id=@plan_id;";
                    dic.Add("plan_id", plan_id);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);

                    foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
                    {
                        if (dr.Cells["parts_code"].Value == null)
                        {
                            continue;
                        }
                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        dic.Add("ID", Guid.NewGuid().ToString());
                        dic.Add("plan_id", plan_id);

                        string parts_code = string.Empty;
                        if (dr.Cells["parts_code"].Value == null)
                        { parts_code = string.Empty; }
                        else
                        { parts_code = dr.Cells["parts_code"].Value.ToString(); }

                        dic.Add("parts_id", dr.Cells["parts_id"].Value.ToString());
                        dic.Add("parts_code", parts_code);
                        dic.Add("parts_name", dr.Cells["parts_name"].Value == null ? "" : dr.Cells["parts_name"].Value.ToString());
                        dic.Add("parts_brand", dr.Cells["parts_brand"].Value == null ? "" : dr.Cells["parts_brand"].Value.ToString());
                        dic.Add("parts_brand_name", dr.Cells["parts_brand_name"].Value == null ? "" : dr.Cells["parts_brand_name"].Value.ToString());
                        dic.Add("drawing_num", dr.Cells["drawing_num"].Value == null ? "" : dr.Cells["drawing_num"].Value.ToString());
                        if (dr.Cells["unit_id"].Value == null || dr.Cells["unit_id"].Value.ToString()=="")
                        {
                            dic.Add("unit_id", string.Empty);
                            dic.Add("unit_name", string.Empty);
                        }
                        else
                        {
                            dic.Add("unit_id", dr.Cells["unit_id"].Value.ToString());
                            dic.Add("unit_name", dr.Cells["unit_id"].EditedFormattedValue.ToString());
                        }
                        dic.Add("business_price", dr.Cells["business_price"].Value == null ? "0" : dr.Cells["business_price"].Value.ToString() == "" ? "0" : dr.Cells["business_price"].Value.ToString());
                        dic.Add("business_counts", dr.Cells["business_counts"].Value == null ? "0" : dr.Cells["business_counts"].Value.ToString() == "" ? "0" : dr.Cells["business_counts"].Value.ToString());
                        dic.Add("total_money", dr.Cells["total_money"].Value == null ? "0" : dr.Cells["total_money"].Value.ToString() == "" ? "0" : dr.Cells["total_money"].Value.ToString());
                        dic.Add("parts_barcode", dr.Cells["parts_barcode"].Value == null ? "" : dr.Cells["parts_barcode"].Value.ToString());
                        dic.Add("car_factory_code", dr.Cells["car_factory_code"].Value == null ? "" : dr.Cells["car_factory_code"].Value.ToString());
                        dic.Add("recent_supplier_code", dr.Cells["recent_supplier_code"].Value == null ? "" : dr.Cells["recent_supplier_code"].Value.ToString());
                        dic.Add("recent_purchase_price", dr.Cells["recent_purchase_price"].Value == null ? "0" : dr.Cells["recent_purchase_price"].Value.ToString() == "" ? "0" : dr.Cells["recent_purchase_price"].Value.ToString());
                        dic.Add("recent_purchase_name", dr.Cells["recent_purchase_name"].Value == null ? "" : dr.Cells["recent_purchase_name"].Value.ToString());
                        dic.Add("finish_counts", dr.Cells["finish_counts"].Value == null ? "0" : dr.Cells["finish_counts"].Value.ToString() == "" ? "0" : dr.Cells["finish_counts"].Value.ToString());
                        dic.Add("relation_order", dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString());
                        dic.Add("is_suspend", dr.Cells["is_suspend"].Value == null ? "1" : dr.Cells["is_suspend"].Value.ToString());
                        dic.Add("remark", dr.Cells["remark"].Value == null ? "" : dr.Cells["remark"].Value.ToString());
                        dic.Add("create_by", dr.Cells["create_by"].Value == null ? "" : dr.Cells["create_by"].Value.ToString());
                        dic.Add("create_name", dr.Cells["create_name"].Value == null ? "" : dr.Cells["create_name"].Value.ToString());
                        dic.Add("create_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["create_time"].Value == null ? DateTime.Now.ToString() : dr.Cells["create_time"].Value.ToString())).ToString());
                        dic.Add("update_by", GlobalStaticObj.UserID);
                        dic.Add("update_name", GlobalStaticObj.UserName);
                        dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

                        dic.Add("original_price", dr.Cells["original_price"].Value == null ? "0" : dr.Cells["original_price"].Value.ToString() == "" ? "0" : dr.Cells["original_price"].Value.ToString());
                        dic.Add("discount", dr.Cells["discount"].Value == null ? "100" : dr.Cells["discount"].Value.ToString() == "" ? "100" : dr.Cells["discount"].Value.ToString());

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

                        string sql2 = "Insert Into tb_parts_purchase_plan_p(ID,plan_id,parts_id,parts_code,parts_name,parts_brand,parts_brand_name,drawing_num,unit_id,"
                         + "unit_name,business_price,business_counts,total_money,parts_barcode,car_factory_code,recent_supplier_code,recent_purchase_price,recent_purchase_name,"
                         + "finish_counts,relation_order,is_suspend,remark,create_by,create_name,create_time,update_by,update_name,update_time,original_price,"
                         + "discount,vm_name,parts_type_id,parts_type_name) values("
                         + "@ID,@plan_id,@parts_id,@parts_code,@parts_name,@parts_brand,@parts_brand_name,@drawing_num,@unit_id,"
                         + "@unit_name,@business_price,@business_counts,@total_money,@parts_barcode,@car_factory_code,@recent_supplier_code,@recent_purchase_price,"
                         + "@recent_purchase_name,@finish_counts,@relation_order,@is_suspend,@remark,@create_by,@create_name,@create_time,@update_by,@update_name,"
                         + "@update_time,@original_price,@discount,@vm_name,@parts_type_id,@parts_type_name);";
                        sysStringSql.sqlString = sql2;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="planId"></param>
        private void AddPurchasePlanSqlString(List<SysSQLString> listSql, string planId, string HandleType)
        {
            try
            {
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

                decimal plan_counts = 0;
                decimal finish_counts = 0;
                GetBuessinessCount(ref plan_counts, ref finish_counts);

                ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
                ddtplan_start_time.Value = Convert.ToDateTime(ddtplan_start_time.Value.ToShortDateString() + " 00:00:00");
                ddtplan_end_time.Value = Convert.ToDateTime(ddtplan_end_time.Value.ToShortDateString() + " 23:59:59");

                tb_parts_purchase_plan model = new tb_parts_purchase_plan();
                CommonFuncCall.SetModelObjectValue(this, model);
                model.plan_id = planId;
                model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
                model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
                model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称
                model.create_by = GlobalStaticObj.UserID;
                model.create_name = GlobalStaticObj.UserName;
                model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
                model.operators = GlobalStaticObj.UserID;
                model.operator_name = GlobalStaticObj.UserName;
                model.is_suspend = chkis_suspend.Checked ? "0" : "1";//选中(中止)：0,未选中(不中止)：1
                model.plan_counts = plan_counts;
                model.finish_counts = finish_counts;
                model.import_status = "0";
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
                    sb.Append(" Insert Into tb_parts_purchase_plan( ");
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
            catch (Exception ex)
            { }
        }
        /// <summary> 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="planId"></param>
        /// <param name="model"></param>
        private void EditPurchasePlanSqlString(List<SysSQLString> listSql, string planId, tb_parts_purchase_plan model, string HandleType)
        {
            try
            {
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;

                decimal plan_counts = 0;
                decimal finish_counts = 0;
                GetBuessinessCount(ref plan_counts, ref finish_counts);

                ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
                ddtplan_start_time.Value = Convert.ToDateTime(ddtplan_start_time.Value.ToShortDateString() + " 00:00:00");
                ddtplan_end_time.Value = Convert.ToDateTime(ddtplan_end_time.Value.ToShortDateString() + " 23:59:59");

                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.SetModelObjectValue(this, model);

                model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
                model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
                model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称
                model.update_by = GlobalStaticObj.UserID;
                model.update_name = GlobalStaticObj.UserName;
                model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
                model.operators = GlobalStaticObj.UserID;
                model.operator_name = GlobalStaticObj.UserName;
                model.plan_counts = plan_counts;
                model.finish_counts = finish_counts;
                model.is_suspend = chkis_suspend.Checked ? "0" : "1";//选中(中止)：0,未选中(不中止)：1
                model.import_status = "0";
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
                    sb.Append(" Update tb_parts_purchase_plan Set ");
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
                    sb.Append(" where plan_id='" + planId + "';");
                    sysStringSql.sqlString = sb.ToString();
                    sysStringSql.Param = dicParam;
                    listSql.Add(sysStringSql);
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 数据保存/提交 方法
        /// </summary>
        /// <param name="HandleTypeName">保存/提交</param>
        void SaveEventOrSubmitEventFunction(string HandleTypeName)
        {
            try
            {
                gvPurchasePlanList.EndEdit();
                if (CheckDataInfo())
                {
                    string SucessMess = "保存";
                    string opName = "采购计划单操作";
                    if (HandleTypeName == "提交")
                    {
                        SucessMess = "提交";
                        if (lblorder_num.Text == "")
                        {
                            lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.PurchasePlan);//获取采购计划单编号
                            if (oldorder_num == lblorder_num.Text)
                            {
                                MessageBoxEx.Show("复制的单据生成的编号和原单据编号重复了，原单号是:" + oldorder_num + "，新单号是:" + lblorder_num.Text + "");
                            }
                        }
                        lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//获取采购计划单状态(已提交)
                    }
                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        plan_id = Guid.NewGuid().ToString();
                        AddPurchasePlanSqlString(listSql, plan_id, HandleTypeName);
                        opName = "新增采购计划单";
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditPurchasePlanSqlString(listSql, plan_id, tb_purchase_Model, HandleTypeName);
                        opName = "修改采购计划单";
                    }
                    DealAccessories(listSql, plan_id);

                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("" + SucessMess + "成功！");
                        uc.BindgvPurchasePlanOrderList();
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
        /// <summary> 获取配件列表中所有配件的编号
        /// </summary>
        /// <returns></returns>
        string GetListPartsCodes()
        {
            string Parts_Codes = string.Empty;
            if (gvPurchasePlanList.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
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
            if (oldcolcontindex > -1)
            {
                try
                {
                    frmParts frm = new frmParts();
                    DialogResult result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string PartsCode = frm.PartsCode;
                        if (gvPurchasePlanList.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
                            {
                                if (dr.Cells["parts_code"].Value != null)
                                {
                                    if (dr.Cells["parts_code"].Value.ToString() == PartsCode)
                                    {
                                        MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
                                        return;
                                    }
                                }
                            }
                        }
                        DataGridViewRow dgvr = gvPurchasePlanList.Rows[oldcolcontindex];
                        DataTable dt = GetAccessoriesByCode(PartsCode);
                        foreach (DataRow dr in dt.Rows)
                        {
                            string default_price = "0";
                            string default_unit_id = string.Empty;
                            string data_source = dr["data_source"].ToString();
                            DataGridViewComboBoxCell dgcombox = null;
                            dgvr.Cells["parts_id"].Value = dr["parts_id"];//配件ID
                            dgvr.Cells["parts_code"].Value = dr["ser_parts_code"];//配件编码
                            dgvr.Cells["parts_name"].Value = dr["parts_name"];//名称
                            dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                            BindDataGridViewComboBoxCell(data_source, dr["ser_parts_code"].ToString(), oldcolcontindex, ref dgcombox, ref default_unit_id, ref default_price);
                            dgvr.Cells["unit_id"].Value = default_unit_id;//单位
                            //dgvr.Cells["unit_id"].Value = dr["default_unit"];//单位
                            dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌
                            dgvr.Cells["original_price"].Value = default_price;//原始单价
                            dgvr.Cells["discount"].Value = "100";//折扣
                            dgvr.Cells["business_price"].Value = default_price;//业务单价
                            dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                            dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码

                            string sup_code = string.Empty;
                            string sup_name = string.Empty;
                            string pur_price = string.Empty;
                            GetSupinfo(dr["ser_parts_code"].ToString(), ref sup_code, ref sup_name, ref pur_price);
                            dgvr.Cells["recent_supplier_code"].Value = sup_code;
                            dgvr.Cells["recent_purchase_name"].Value = sup_name;
                            dgvr.Cells["recent_purchase_price"].Value = pur_price;
                        }
                        if (gvPurchasePlanList.Rows.Count - 1 == oldcolcontindex)
                        {
                            gvPurchasePlanList.Rows.Add(1);
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { oldcolcontindex = -1; }
            }
        }
        /// <summary> 获取gvPurchasePlanList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<DataGridViewRow> GetSelectedRecord()
        {
            List<DataGridViewRow> listField = new List<DataGridViewRow>();
            foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
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
                foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
                {
                    if (dr.Cells["parts_code"].Value != null)
                    {
                        string business_counts = dr.Cells["business_counts"].EditedFormattedValue == null ? "0" : dr.Cells["business_counts"].EditedFormattedValue.ToString();
                        business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
                        if (Convert.ToDecimal(business_counts) == 0)
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
        /// <summary> 查询配件的最后一个供应商编码，名称
        /// </summary>
        /// <param name="parts_codes"></param>
        /// <param name="sup_code"></param>
        /// <param name="sup_name"></param>
        void GetSupinfo(string parts_codes, ref string sup_code, ref string sup_name, ref string business_price)
        {
//            string FilesName = "tb_bill.sup_code,tb_bill.sup_name,ISNULL(tb_bill_p.valorem_together,0)/ISNULL(tb_bill_p.business_counts,1) parts_price,tb_bill_p.unit_name";
//            string TableName = string.Format(@"(select sup_code,sup_name,purchase_billing_id from tb_parts_purchase_billing where order_status in ('2','5')) as tb_bill
//                                                left join 
//                                                tb_parts_purchase_billing_p as tb_bill_p on tb_bill.purchase_billing_id=tb_bill_p.purchase_billing_id");
//            string WhereStr = string.Format(@"tb_bill_p.parts_code='{0}' order by tb_bill_p.create_time desc", parts_codes);
//            DataTable dt = DBHelper.GetTable("", TableName, FilesName, WhereStr, "", "");

            string TableName = string.Format(@"(
                                                  select 
                                                  top 1 tb_bill.sup_code,tb_bill.sup_name,
                                                  ISNULL(tb_bill_p.valorem_together,0)/case when ISNULL(tb_bill_p.business_counts,1)=0 then 1 else ISNULL(tb_bill_p.business_counts,1) end parts_price,
                                                  tb_bill_p.unit_name from 
                                                  (select sup_code,sup_name,purchase_billing_id from tb_parts_purchase_billing where order_status in ('2','5')) as tb_bill
                                                  left join 
                                                  tb_parts_purchase_billing_p as tb_bill_p on tb_bill.purchase_billing_id=tb_bill_p.purchase_billing_id
                                                  where tb_bill_p.parts_code='{0}' order by tb_bill_p.create_time desc
                                                ) T_SupInfo", parts_codes);
            DataTable dt = DBHelper.GetTable("", TableName, "*", "", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                sup_code = dt.Rows[0]["sup_code"].ToString();
                sup_name = dt.Rows[0]["sup_name"].ToString();
                business_price = dt.Rows[0]["parts_price"].ToString() + "/" + dt.Rows[0]["unit_name"].ToString();
            }
        }
        #endregion

        #region 限制文本框输入内容的事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtplan_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtplan_finish_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        } 
        #endregion

        #region 自动结算金额
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                rowIndex = ((System.Windows.Forms.DataGridViewTextBoxEditingControl)(sender)).EditingControlDataGridView.CurrentRow.Index;
                DataGridViewTextBoxEditingControl c = sender as DataGridViewTextBoxEditingControl;
                if (ColumnName == "original_price" || ColumnName == "discount" || ColumnName == "business_price" || ColumnName == "business_counts")
                {
                    c.Text = string.IsNullOrEmpty(c.Text) ? "0" : c.Text;
                    decimal outret = 0;
                    if (!decimal.TryParse(c.Text, out outret))
                    {
                        return;
                    }
                    ValidationRegex.GridViewTextBoxChangedFolat(c, ValidationRegex.MaxTextNum);
                }

                //原始单价
                string original_price = gvPurchasePlanList.Rows[rowIndex].Cells["original_price"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["original_price"].Value.ToString();
                original_price = string.IsNullOrEmpty(original_price) ? "0" : original_price;
                //折扣
                string discount = gvPurchasePlanList.Rows[rowIndex].Cells["discount"].Value == null ? "100" : gvPurchasePlanList.Rows[rowIndex].Cells["discount"].Value.ToString();
                discount = string.IsNullOrEmpty(discount) ? "100" : discount;
                //业务单价
                string business_price = gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value.ToString();
                business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;
                //业务数量
                string business_counts = gvPurchasePlanList.Rows[rowIndex].Cells["business_counts"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["business_counts"].Value.ToString();
                business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
                //完成数量
                string finish_counts = gvPurchasePlanList.Rows[rowIndex].Cells["finish_counts"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["finish_counts"].Value.ToString();
                finish_counts = string.IsNullOrEmpty(finish_counts) ? "0" : finish_counts;

                if (ColumnName == "business_counts")
                {
                    //if (Convert.ToDecimal(c.Text) < 0)
                    //{ c.ForeColor = Color.Red; }
                    //else
                    //{ c.ForeColor = Color.Black; }
                    gvPurchasePlanList.Rows[rowIndex].Cells["total_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(business_price);
                    GetBuessinessMoney();
                }
                else if (ColumnName == "original_price")
                {
                    gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(discount) / 100;

                    business_price = gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value.ToString();
                    gvPurchasePlanList.Rows[rowIndex].Cells["total_money"].Value = Convert.ToDecimal(business_price) * Convert.ToDecimal(business_counts);
                    //gvPurchasePlanList.Rows[rowIndex].Cells["plan_finish_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(finish_counts);
                    GetBuessinessMoney();
                }
                else if (ColumnName == "discount")
                {
                    gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value = Convert.ToDecimal(c.Text) / 100 * Convert.ToDecimal(original_price);

                    business_price = gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value == null ? "0" : gvPurchasePlanList.Rows[rowIndex].Cells["business_price"].Value.ToString();
                    gvPurchasePlanList.Rows[rowIndex].Cells["total_money"].Value = Convert.ToDecimal(business_price) * Convert.ToDecimal(business_counts);
                    //gvPurchasePlanList.Rows[rowIndex].Cells["plan_finish_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(finish_counts);
                    GetBuessinessMoney();
                }
                else if (ColumnName == "business_price")
                {
                    //当修改业务单价时，自动更改折扣
                    original_price = Convert.ToDecimal(original_price) == 0 ? "1" : original_price;
                    gvPurchasePlanList.Rows[rowIndex].Cells["discount"].Value = Convert.ToDecimal(c.Text) / Convert.ToDecimal(original_price) * 100;

                    gvPurchasePlanList.Rows[rowIndex].Cells["total_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(business_counts);
                    //gvPurchasePlanList.Rows[rowIndex].Cells["plan_finish_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(finish_counts);
                    GetBuessinessMoney();
                }
                //else if (ColumnName == "finish_counts")
                //{
                //    gvPurchasePlanList.Rows[rowIndex].Cells["plan_finish_money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(business_price);
                //    GetBuessinessMoney();
                //}
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 自动计算计划数量，完成数量的总和 
        /// </summary>
        /// <param name="business_counts"></param>
        /// <param name="finish_counts"></param>
        void GetBuessinessCount(ref decimal business_counts, ref decimal finish_counts)
        {
            foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                business_counts = business_counts + Convert.ToDecimal(dr.Cells["business_counts"].Value == null ? "0" : dr.Cells["business_counts"].Value.ToString() == "" ? "0" : dr.Cells["business_counts"].Value.ToString());
                finish_counts = finish_counts + Convert.ToDecimal(dr.Cells["finish_counts"].Value == null ? "0" : dr.Cells["finish_counts"].Value.ToString() == "" ? "0" : dr.Cells["finish_counts"].Value.ToString());
            }
        }
        /// <summary> 自动计算计划金额和完成金额的总和
        /// </summary>
        void GetBuessinessMoney()
        {
            decimal plan_money = 0;
            //decimal finish_money = 0;
            foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                plan_money += Convert.ToDecimal(dr.Cells["total_money"].Value == null ? "0" : dr.Cells["total_money"].Value.ToString() == "" ? "0" : dr.Cells["total_money"].Value.ToString());
                //finish_money += Convert.ToDecimal(dr.Cells["plan_finish_money"].Value == null ? "0" : dr.Cells["plan_finish_money"].Value.ToString() == "" ? "0" : dr.Cells["plan_finish_money"].Value.ToString());
            }
            txtplan_money.Caption = plan_money.ToString("f2");
            //txtplan_finish_money.Caption = finish_money.ToString("f2");
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
                                Set_Original_price(original_price, gvPurchasePlanList.CurrentRow.Index);
                            }
                        }
                    }
                    else
                    {
                        Set_Original_price(0, gvPurchasePlanList.CurrentRow.Index);
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
        void Set_Original_price(decimal price, int rowsindex)
        {
            gvPurchasePlanList.Rows[rowsindex].Cells["original_price"].Value = price.ToString();
            //折扣
            string discount = gvPurchasePlanList.Rows[rowsindex].Cells["discount"].Value == null ? "100" : gvPurchasePlanList.Rows[rowsindex].Cells["discount"].Value.ToString();
            discount = string.IsNullOrEmpty(discount) ? "100" : discount;

            gvPurchasePlanList.Rows[rowsindex].Cells["business_price"].Value = price * Convert.ToDecimal(discount) / 100;
            //业务单价
            string business_price = gvPurchasePlanList.Rows[rowsindex].Cells["business_price"].Value == null ? "0" : gvPurchasePlanList.Rows[rowsindex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;
            //业务数量
            string business_counts = gvPurchasePlanList.Rows[rowsindex].Cells["business_counts"].Value == null ? "0" : gvPurchasePlanList.Rows[rowsindex].Cells["business_counts"].Value.ToString();
            business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;

            gvPurchasePlanList.Rows[rowsindex].Cells["total_money"].Value = Convert.ToDecimal(business_price) * Convert.ToDecimal(business_counts);
            GetBuessinessMoney();
        }
        /// <summary> 为每一行的配件信息绑定下拉框信息
        /// </summary>
        /// <param name="parts_code"></param>
        /// <param name="rowsindex"></param>
        /// <param name="dgcombox"></param>
        public void BindDataGridViewComboBoxCell(string data_source,string parts_code, int rowsindex, ref DataGridViewComboBoxCell dgcombox,ref string default_unit_id,ref string default_price)
        {
            dgcombox = (DataGridViewComboBoxCell)gvPurchasePlanList.Rows[rowsindex].Cells["unit_id"];

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

                    //根据配件数据来源，判断给什么单价(采购业务时：自建的配件信息用参考进价，宇通的配件用销售2A价)
                    if (data_source == "1")//自建配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_in_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_in_price"].ToString()) ? "0" : dt.Rows[i]["ref_in_price"].ToString());
                    }
                    else if (data_source == "2")//宇通配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["out_price_two"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["out_price_two"].ToString()) ? "0" : dt.Rows[i]["out_price_two"].ToString());
                    }
                    //采购业务中，判断单位是否是采购单位
                    if (dt.Rows[i]["is_purchase"].ToString() == "1")
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
    }

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