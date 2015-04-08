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
using System.Reflection;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using HXCPcClient.Chooser;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    public partial class UCYTAddOrEdit : UCBase
    {
        List<partunitprice> list_partunitprice = new List<partunitprice>();
        List<lastunitclass> list_lastunitprice = new List<lastunitclass>();
        private string oldorder_num = string.Empty;
        //配件需求订单：100000001
        //产品升级订单：100000005
        //新三包调件订单：100000004

        #region 变量
        int rowIndex = -1;
        int oldindex = -1;
        UCYTManager uc;
        WindowStatus status;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        string purchase_order_yt_id = string.Empty;
        tb_parts_purchase_order_2 yt_purchaseorder_model = new tb_parts_purchase_order_2();

        UserControlXuQiu xuqiu = null;
        UserControlShengJi shengji = null;
        UserControlSanBao sanbao = null;
        #endregion

        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        /// <param name="status"></param>
        /// <param name="purchase_order_yt_id"></param>
        /// <param name="uc"></param>
        public UCYTAddOrEdit(WindowStatus status, string purchase_order_yt_id, UCYTManager uc)
        {
            InitializeComponent();

            this.uc = uc;
            this.status = status;
            this.purchase_order_yt_id = purchase_order_yt_id;
            this.Resize += new EventHandler(UCYTAddOrEdit_Resize);
            base.SaveEvent += new ClickHandler(UCYTAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCYTAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCYTAddOrEdit_ImportEvent);
            gvPurchaseList.CellDoubleClick += new DataGridViewCellEventHandler(gvPurchaseList_CellDoubleClick);

            application_count.ValueType = typeof(decimal);
            application_count.MaxInputLength = 9;
            //conf_count.ValueType = typeof(decimal);
            //conf_count.MaxInputLength = 9;
        }
        /// <summary> 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCYTAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            { base.SetButtonVisiableHandleAddCopy(); }
            else if (status == WindowStatus.Edit)
            { base.SetButtonVisiableHandleEdit(); }

            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "application_count", "parts_explain", "replaces", "center_library_explain", "cancel_reasons" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);

            ddtorder_date.Value = DateTime.Now;
            lbloperator_name.Text = GlobalStaticObj.UserName;
            //绑定宇通采购订单类型
            CommonFuncCall.BindYTPurchaseOrderType(ddlorder_type, false, "请选择");
            //动态创建三种订单的panel区域
            LoadAllOrderTypeInfo();

            //CommonFuncCall.BindUnit(unit_id);
            //获取公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");

            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(purchase_order_yt_id);
                GetAccessories(purchase_order_yt_id);
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblorder_num.Text = "";
                lblcrm_bill_id.Text = "";
                lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
            }
            gvPurchaseList.Rows.Add(1);
        }
        #endregion

        #region 控件事件
        /// <summary> 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCYTAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("保存");
        }
        /// <summary> 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCYTAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("提交");
        }
        /// <summary> 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCYTAddOrEdit_ImportEvent(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            ShowContextMenuStrip(x, y);
        }
        /// <summary> 点击导入采购计划单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImporPurchasePlan_Click(object sender, EventArgs e)
        {
            try
            {
                //导入采购计划单时，先判断列表中是否存在销售订单的信息，存在时不允许导入
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        continue;
                    }
                    string ImportType = dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString();
                    if (ImportType == "销售订单")
                    {
                        MessageBoxEx.Show("订单中已经存在销售订单的信息,不允许再次导入采购计划单的信息!");
                        return;
                    }
                }
                UCChoosePurchasePlanOrder frm = new UCChoosePurchasePlanOrder("宇通采购订单");
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<PartsInfoClassByPurchasePlan> List_PartInfo = frm.List_PartInfo;
                    DataTable dt_PurchasePlanOrder = frm.dt_PurchasePlanOrder;
                    if (List_PartInfo.Count > 0)
                    {
                        for (int i = 0; i < List_PartInfo.Count; i++)
                        {
                            bool IsExist = false;//判断要导入的信息是否已经在列表中存在，默认是不存在
                            string planid = List_PartInfo[i].planID;
                            string plancode = string.Empty;
                            string partscode = List_PartInfo[i].parts_code;

                            #region 获取引用单号
                            if (dt_PurchasePlanOrder != null && dt_PurchasePlanOrder.Rows.Count > 0)
                            {
                                for (int a = 0; a < dt_PurchasePlanOrder.Rows.Count; a++)
                                {
                                    if (dt_PurchasePlanOrder.Rows[a]["plan_id"].ToString() == planid)
                                    {
                                        plancode = dt_PurchasePlanOrder.Rows[a]["order_num"].ToString();
                                        plancode = string.IsNullOrEmpty(plancode) ? planid : plancode;
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
                                string wherestr = " plan_id='" + planid + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_counts,0)<isnull(business_counts,0) and len(car_factory_code)>0";
                                DataTable dt = DBHelper.GetTable("查询采购计划单配件表信息", "tb_parts_purchase_plan_p", "*", wherestr, "", "");
                                if (dt == null || dt.Rows.Count == 0)
                                {
                                    break;
                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //int RowsIndex = gvPurchaseList.Rows.Add();
                                    int RowsIndex = gvPurchaseList.Rows.Count - 1;
                                    DataGridViewRow dgvr = gvPurchaseList.Rows[RowsIndex];
                                    GetGridViewRowByDrImport(dgvr, dr, plancode,RowsIndex, "采购计划单");
                                }
                                gvPurchaseList.Rows.Add(1);
                            }
                            #endregion
                            #region 当添加成功时，将采购计划单状态设置成占用，使前置单据不可以编辑、删除
                            DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_purchase_plan", "import_status", " order_num='" + plancode + "'", "", "");
                            if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                            {
                                string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                                if (import_status == "0")
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    dicValue.Add("import_status", "1");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                                    DBHelper.Submit_AddOrEdit("修改采购计划单的导入状态为占用", "tb_parts_purchase_plan", "order_num", plancode, dicValue);
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
        /// <summary> 点击导入销售订单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImporSaleOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //导入销售订单时，先判断列表中是否存在采购计划单的信息，存在时不允许导入
                foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                {
                    if (dr.Cells["parts_code"].Value == null)
                    {
                        continue;
                    }
                    string ImportType = dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString();
                    if (ImportType == "采购计划单")
                    {
                        MessageBoxEx.Show("订单中已经存在采购计划单的信息,不允许再次导入销售订单的信息!");
                        return;
                    }
                }
                UCChooseSaleOrder frm = new UCChooseSaleOrder(string.Empty, "宇通采购订单");
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<PartsInfoClassBySaleOrder> List_PartInfo = frm.List_PartInfo;
                    DataTable dt_SaleOrder = frm.dt_SaleOrder;
                    if (List_PartInfo.Count > 0)
                    {
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
                                string wherestr = " sale_order_id='" + sale_order_id + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) and len(car_factory_code)>0 ";
                                DataTable dt = DBHelper.GetTable("查询销售订单配件表信息", "tb_parts_sale_order_p", "*", wherestr, "", "");
                                if (dt == null || dt.Rows.Count == 0)
                                {
                                    break;
                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //int RowsIndex = gvPurchaseList.Rows.Add();
                                    int RowsIndex = gvPurchaseList.Rows.Count - 1;
                                    DataGridViewRow dgvr = gvPurchaseList.Rows[RowsIndex];
                                    GetGridViewRowByDrImport(dgvr, dr, ordercode, RowsIndex, "销售订单");
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
                                    dicValue.Add("is_occupy", "1");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
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
        /// <summary> 添加配件信息到列表的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchaseAccessoriesAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //frmParts frm = new frmParts();
                frmPartsByYuTong frm = new frmPartsByYuTong();
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
        private void cmsPurchaseAccessoriesEdit_Click(object sender, EventArgs e)
        {
            EditPartsInfo();
        }
        /// <summary> 删除列表信息的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchaseAccessoriesDelete_Click(object sender, EventArgs e)
        {
            #region 原来的单个删除的方法
            //if (oldindex > -1)
            //{
            //    try
            //    {
            //        if (gvPurchaseList.Rows[oldindex].Cells["parts_code"].Value != null)
            //        {
            //            #region 当删除成功时，将前置单据的状态释放为正常
            //            DataGridViewRow dr = gvPurchaseList.Rows[oldindex];
            //            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
            //            string ImportType = dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString();
            //            if (!string.IsNullOrEmpty(relation_order))
            //            {
            //                if (!CheckPre_order_code(relation_order))
            //                {
            //                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
            //                    if (ImportType == "采购计划单")
            //                    {
            //                        DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_purchase_plan", "import_status", " order_num='" + relation_order + "'", "", "");
            //                        if (dt_import_status != null && dt_import_status.Rows.Count > 0)
            //                        {
            //                            string import_status = dt_import_status.Rows[0]["import_status"].ToString();
            //                            if (import_status == "1")
            //                            {
            //                                dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
            //                                DBHelper.Submit_AddOrEdit("修改采购计划单的导入状态为正常", "tb_parts_purchase_plan", "order_num", relation_order, dicValue);
            //                            }
            //                        }
            //                    }
            //                    else if (ImportType == "销售订单")
            //                    {
            //                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_order", "is_occupy", " order_num='" + relation_order + "'", "", "");
            //                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
            //                        {
            //                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
            //                            if (is_occupy == "1")
            //                            {
            //                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
            //                                DBHelper.Submit_AddOrEdit("修改销售订单的导入状态为正常", "tb_parts_sale_order", "order_num", relation_order, dicValue);
            //                            }
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
                            #region 当删除成功时，将前置单据的状态释放为正常
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            string ImportType = dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString();
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    if (ImportType == "采购计划单")
                                    {
                                        DataTable dt_import_status = DBHelper.GetTable("", "tb_parts_purchase_plan", "import_status", " order_num='" + relation_order + "'", "", "");
                                        if (dt_import_status != null && dt_import_status.Rows.Count > 0)
                                        {
                                            string import_status = dt_import_status.Rows[0]["import_status"].ToString();
                                            if (import_status == "1")
                                            {
                                                dicValue.Add("import_status", "0");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                                                DBHelper.Submit_AddOrEdit("修改采购计划单的导入状态为正常", "tb_parts_purchase_plan", "order_num", relation_order, dicValue);
                                            }
                                        }
                                    }
                                    else if (ImportType == "销售订单")
                                    {
                                        DataTable dt_is_occupy = DBHelper.GetTable("", "tb_parts_sale_order", "is_occupy", " order_num='" + relation_order + "'", "", "");
                                        if (dt_is_occupy != null && dt_is_occupy.Rows.Count > 0)
                                        {
                                            string is_occupy = dt_is_occupy.Rows[0]["is_occupy"].ToString();
                                            if (is_occupy == "1")
                                            {
                                                dicValue.Add("is_occupy", "0");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                                                DBHelper.Submit_AddOrEdit("修改销售订单的导入状态为正常", "tb_parts_sale_order", "order_num", relation_order, dicValue);
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
        /// <summary> 选择订单类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorder_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllOrderTypeInfo();
        }
        /// <summary> 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), "请选择");
        }
        /// <summary> 单元格内容格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = gvPurchaseList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("predict_arrival_date") || fieldNmae.Equals("predict_arrival_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            string fieldNmae1 = gvPurchaseList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae1.Equals("reality_arrival_date") || fieldNmae1.Equals("reality_arrival_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }
        /// <summary> 鼠标放在单元格上获取行索引的事件
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
        void UCYTAddOrEdit_Resize(object sender, EventArgs e)
        {
            string[] total = { application_count.Name, money.Name };
            ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);
        }
        #endregion 

        #region 方法、函数
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
        /// <summary> 加载三种类型订单的panel区域信息
        /// </summary>
        void LoadAllOrderTypeInfo()
        {
            try
            {
                if (ddlorder_type.SelectedValue != null)
                {
                    if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
                    {
                        switch (ddlorder_type.SelectedValue.ToString())
                        {
                            //配件需求订单
                            case "order_type_100000001":
                                LoadUCXuQiu();
                                break;
                            //产品升级订单
                            case "order_type_100000005":
                                LoadUCShengJi();
                                break;
                            //新三包调件订单
                            case "order_type_100000004":
                                LoadUCSanBao();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 加载 配件需求订单 用户控件
        /// </summary>
        void LoadUCXuQiu()
        {
            this.panelArea.Controls.Clear();
            UserControlXuQiu uc = new UserControlXuQiu();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            xuqiu = uc;
        }
        /// <summary> 加载 产品升级订单 用户控件
        /// </summary>
        void LoadUCShengJi()
        {
            this.panelArea.Controls.Clear();
            UserControlShengJi uc = new UserControlShengJi();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            shengji = uc;
        }
        /// <summary> 加载 新三包调件订单 用户控件
        /// </summary>
        void LoadUCSanBao()
        {
            this.panelArea.Controls.Clear();
            UserControlSanBao uc = new UserControlSanBao();
            this.panelArea.Controls.Add(uc);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            sanbao = uc;
        }

        /// <summary> 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            if (ddlorder_type.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
                {
                    if (ddlorder_type.SelectedValue.ToString() == "order_type_100000001")//配件需求订单
                    {
                        if (!xuqiu.CheckControlInfo())
                        { return false; }
                    }
                    else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000005")//产品升级订单
                    {
                        if (!shengji.CheckControlInfo())
                        { return false; }
                    }
                    else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000004")//新三包调件订单
                    {
                        if (!sanbao.CheckControlInfo())
                        { return false; }
                    }
                }
            }
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
        /// <summary> 加载采购计划信息和配件信息
        /// </summary>
        /// <param name="purchase_order_yt_id"></param>
        private void LoadInfo(string purchase_order_yt_id)
        {
            if (!string.IsNullOrEmpty(purchase_order_yt_id))
            {
                //1.查看一条宇通采购订单信息
                DataTable dt = DBHelper.GetTable("查看一条宇通采购订单信息", "tb_parts_purchase_order_2", "*", " purchase_order_yt_id='" + purchase_order_yt_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(yt_purchaseorder_model, dt);
                    CommonFuncCall.SetShowControlValue(this, yt_purchaseorder_model, "");
                    oldorder_num = yt_purchaseorder_model.order_num;
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = "";
                    }
                    lblcreate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(yt_purchaseorder_model.create_time.ToString())).ToString();
                    if (yt_purchaseorder_model.update_time > 0)
                    { lblupdate_time1.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(yt_purchaseorder_model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary> 根据采购订单号获取采购配件信息
        /// </summary>
        /// <param name="purchase_order_yt_id"></param>
        private void GetAccessories(string purchase_order_yt_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询宇通采购订单配件表信息", "tb_parts_purchase_order_p_2", "*", " purchase_order_yt_id='" + purchase_order_yt_id + "'", "", "");
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
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];//配件编码
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];//配件名称
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编号
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];//图号
                    dgvr.Cells["model"].Value = dr["model"];//规格型号
                    dgvr.Cells["application_count"].Value = dr["application_count"];//申请数量
                    dgvr.Cells["price"].Value = dr["price"];//单价
                    dgvr.Cells["money"].Value = dr["money"];//金额
                    dgvr.Cells["conf_count"].Value = dr["conf_count"];//确认数量
                    dgvr.Cells["parts_explain"].Value = dr["parts_explain"];//配件说明
                    dgvr.Cells["replaces"].Value = dr["replaces"];//代替
                    BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), CurrentRowIndenx, ref dgcombox, ref default_unit_id, ref default_price);
                    dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
                    dgvr.Cells["center_library_explain"].Value = dr["center_library_explain"];//中心站/库处理说明
                    //dgvr.Cells["total_library_explain"].Value = dr["total_library_explain"];//总库处理说明
                    dgvr.Cells["cancel_reasons"].Value = dr["cancel_reasons"];//取消原因
                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                    if (status == WindowStatus.Copy)
                    {
                        dgvr.Cells["relation_order"].Value = string.Empty;
                        dgvr.Cells["ImportType"].Value = string.Empty;
                    }
                    else
                    {
                        dgvr.Cells["relation_order"].Value = dr["relation_order"];//引用单号
                        dgvr.Cells["ImportType"].Value = dr["ImportType"];
                    }
                }
            }
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
                    string opName = "宇通采购订单操作";
                    if (HandleTypeName == "提交")
                    {
                        SucessMess = "提交";
                        if (lblorder_num.Text == "")
                        {
                            lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.YTPurchaseOrder);//获取宇通采购订单编号
                            if (oldorder_num == lblorder_num.Text)
                            {
                                MessageBoxEx.Show("复制的单据生成的编号和原单据编号重复了，原单号是:" + oldorder_num + "，新单号是:" + lblorder_num.Text + "");
                            }
                        }
                        lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//获取宇通采购订单状态(已提交)
                    }

                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        purchase_order_yt_id = Guid.NewGuid().ToString();
                        AddPurchaseOrderSqlString(listSql, purchase_order_yt_id, HandleTypeName);
                        opName = "新增宇通采购订单";
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditPurchaseOrderSqlString(listSql, purchase_order_yt_id, yt_purchaseorder_model, HandleTypeName);
                        opName = "修改宇通采购订单";
                    }
                    DealAccessories(listSql, purchase_order_yt_id);
                    GetPre_Order_Code(listSql, purchase_order_yt_id, lblorder_num.Text);
                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("" + SucessMess + "成功！");

                        if (HandleTypeName == "保存")
                        { ImportPurchasePlanStatus("0"); }
                        else if (HandleTypeName == "提交")
                        { SetOrderStatus(purchase_order_yt_id); }

                        uc.BindgvYTPurchaseOrderList();
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
        /// <summary> 在编辑和添加时对主数据和采购订单配件表中的配件信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_order_yt_id"></param>
        private void DealAccessories(List<SysSQLString> listSql, string purchase_order_yt_id)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql1 = "delete from tb_parts_purchase_order_p_2 where purchase_order_yt_id=@purchase_order_yt_id;";
            dic.Add("purchase_order_yt_id", purchase_order_yt_id);
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
                    dic.Add("id", Guid.NewGuid().ToString());
                    dic.Add("purchase_order_yt_id", purchase_order_yt_id);
                    dic.Add("parts_id", dr.Cells["parts_id"].Value == null ? "" : dr.Cells["parts_id"].Value.ToString());
                    dic.Add("parts_code", dr.Cells["parts_code"].Value == null ? "" : dr.Cells["parts_code"].Value.ToString());
                    dic.Add("parts_name", dr.Cells["parts_name"].Value == null ? "" : dr.Cells["parts_name"].Value.ToString());
                    dic.Add("car_factory_code", dr.Cells["car_factory_code"].Value == null ? "" : dr.Cells["car_factory_code"].Value.ToString());
                    dic.Add("drawing_num", dr.Cells["drawing_num"].Value == null ? "" : dr.Cells["drawing_num"].Value.ToString());
                    dic.Add("model", dr.Cells["model"].Value == null ? "" : dr.Cells["model"].Value.ToString());
                    dic.Add("application_count", dr.Cells["application_count"].Value == null ? "0" : dr.Cells["application_count"].Value.ToString() == "" ? "0" : dr.Cells["application_count"].Value.ToString());
                    dic.Add("price", dr.Cells["price"].Value == null ? "0" : dr.Cells["price"].Value.ToString() == "" ? "0" : dr.Cells["price"].Value.ToString());
                    dic.Add("money", dr.Cells["money"].Value == null ? "0" : dr.Cells["money"].Value.ToString() == "" ? "0" : dr.Cells["money"].Value.ToString());
                    dic.Add("conf_count", dr.Cells["conf_count"].Value == null ? "0" : dr.Cells["conf_count"].Value.ToString() == "" ? "0" : dr.Cells["conf_count"].Value.ToString());
                    dic.Add("parts_explain", dr.Cells["parts_explain"].Value == null ? "" : dr.Cells["parts_explain"].Value.ToString());
                    dic.Add("replaces", dr.Cells["replaces"].Value == null ? "" : dr.Cells["replaces"].Value.ToString());             
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
                    dic.Add("center_library_explain", dr.Cells["center_library_explain"].Value == null ? "" : dr.Cells["center_library_explain"].Value.ToString());
                    //dic.Add("total_library_explain", dr.Cells["total_library_explain"].Value == null ? "" : dr.Cells["total_library_explain"].Value.ToString());
                    dic.Add("cancel_reasons", dr.Cells["cancel_reasons"].Value == null ? "" : dr.Cells["cancel_reasons"].Value.ToString());
                    dic.Add("relation_order", dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString());
                    dic.Add("create_by", dr.Cells["create_by"].Value == null ? "" : dr.Cells["create_by"].Value.ToString());
                    dic.Add("create_name", dr.Cells["create_name"].Value == null ? "" : dr.Cells["create_name"].Value.ToString());
                    dic.Add("create_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["create_time"].Value == null ? DateTime.Now.ToString() : dr.Cells["create_time"].Value.ToString())).ToString());
                    dic.Add("update_by", GlobalStaticObj.UserID);
                    dic.Add("update_name", GlobalStaticObj.UserName);
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                    dic.Add("ImportType", dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString());


                    string sql2 = string.Format(@"Insert Into tb_parts_purchase_order_p_2(id,purchase_order_yt_id,parts_id,parts_code,parts_name,car_factory_code,
                    drawing_num,model,application_count,price,money,conf_count,parts_explain,replaces,
                    unit_id,unit_name,center_library_explain,cancel_reasons,relation_order,create_by,create_name,create_time,
                    update_by,update_name,update_time,ImportType) values(@id,@purchase_order_yt_id,@parts_id,@parts_code,@parts_name,@car_factory_code,
                    @drawing_num,@model,@application_count,@price,@money,@conf_count,@parts_explain,@replaces,
                    @unit_id,@unit_name,@center_library_explain,@cancel_reasons,@relation_order,@create_by,@create_name,@create_time,
                    @update_by,@update_name,@update_time,@ImportType);");
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
        }
        /// <summary> 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_order_yt_id"></param>
        private void AddPurchaseOrderSqlString(List<SysSQLString> listSql, string purchase_order_yt_id, string HandleType)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            decimal application_count = 0;
            decimal conf_count = 0;
            string parts_codes=string.Empty;
            string parts_names = string.Empty;
            GetBuesinessCountPartsInfo(ref application_count, ref conf_count, ref parts_codes, ref parts_names);

            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            tb_parts_purchase_order_2 model = new tb_parts_purchase_order_2();
            CommonFuncCall.SetModelObjectValue(this, model);
            if (model.crm_bill_id == ".")
            {
                model.crm_bill_id = string.Empty;
            }
            model.order_type_name = ddlorder_type.SelectedItem.ToString();
            if (ddlorder_type.SelectedValue.ToString() == "order_type_100000001")//配件需求订单
            {
                xuqiu.GetControlInfo(model);
            }
            else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000005")//产品升级订单
            {
                shengji.GetControlInfo(model);
            }
            else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000004")//新三包调件订单
            {
                sanbao.GetControlInfo(model);
            }
            model.application_count = application_count;
            model.conf_count = conf_count;
            model.purchase_order_yt_id = purchase_order_yt_id;
            model.create_by = GlobalStaticObj.UserID;
            model.create_name = GlobalStaticObj.UserName;
            model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
            model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称  
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
            model.parts_codes = parts_codes;
            model.parts_names = parts_names;
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
                sb.Append(" Insert Into tb_parts_purchase_order_2( ");
                StringBuilder sp = new StringBuilder();
                StringBuilder sb_prame = new StringBuilder();
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    //外部加入的属性
                    if (name == "listDetails")
                    {
                        break;
                    }
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
        /// <param name="purchase_order_yt_id"></param>
        /// <param name="model"></param>
        private void EditPurchaseOrderSqlString(List<SysSQLString> listSql, string purchase_order_yt_id, tb_parts_purchase_order_2 model, string HandleType)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            decimal application_count = 0;
            decimal conf_count = 0;
            string parts_codes = string.Empty;
            string parts_names = string.Empty;
            GetBuesinessCountPartsInfo(ref application_count, ref conf_count, ref parts_codes, ref parts_names);

            ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
            CommonFuncCall.SetModelObjectValue(this, model);
            if (model.crm_bill_id == ".")
            {
                model.crm_bill_id = string.Empty;
            }
            model.order_type_name = ddlorder_type.SelectedItem.ToString();
            if (ddlorder_type.SelectedValue.ToString() == "order_type_100000001")//配件需求订单
            {
                xuqiu.GetControlInfo(model);
            }
            else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000005")//产品升级订单
            {
                shengji.GetControlInfo(model);
            }
            else if (ddlorder_type.SelectedValue.ToString() == "order_type_100000004")//新三包调件订单
            {
                sanbao.GetControlInfo(model);
            }

            model.application_count = application_count;
            model.conf_count = conf_count;
            model.purchase_order_yt_id = purchase_order_yt_id;
            model.update_by = GlobalStaticObj.UserID;
            model.update_name = GlobalStaticObj.UserName;
            model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            model.operators = GlobalStaticObj.UserID;
            model.operator_name = GlobalStaticObj.UserName;
            model.com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            model.com_code = GlobalStaticObj.CurrUserCom_Code;//公司编码
            model.com_name = GlobalStaticObj.CurrUserCom_Name;//公司名称  
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
            model.parts_codes = parts_codes;
            model.parts_names = parts_names;
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
                sb.Append(" Update tb_parts_purchase_order_2 Set ");
                bool isFirstValue = true;
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    //外部加入的属性
                    if (name == "listDetails")
                    {
                        break;
                    }
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
                sb.Append(" where purchase_order_yt_id='" + purchase_order_yt_id + "';");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }     
        /// <summary> 点击导入按钮，显示导入方式的下拉选项
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ShowContextMenuStrip(int X, int Y)
        {
            ImportMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            ImportMenuStrip.Show();
            ImportMenuStrip.Location = new System.Drawing.Point(X, Y);
        }
        /// <summary> 单个添加配件信息的方法
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
                dgvr.Cells["price"].Value = default_price;//业务单价 从配件信息的参考进价 中获取
                dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码

                dgvr.Cells["relation_order"].Value = relation_order;
                dgvr.Cells["ImportType"].Value = "";
                

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
        /// <summary> 批量导入宇通采购订单配件信息的方法
        /// </summary>
        /// <param name="dgvr"></param>
        /// <param name="dr"></param>
        /// <param name="relation_order"></param>
        /// <param name="HandleType"></param>
        void GetGridViewRowByDrImport(DataGridViewRow dgvr, DataRow dr, string relation_order, int rowIndex, string ImportType)
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
                dgvr.Cells["price"].Value = dr["business_price"];//业务单价 从配件信息的参考进价 中获取
                if (ImportType == "采购计划单")
                {
                    dgvr.Cells["application_count"].Value = decimal.Parse(dr["business_counts"].ToString() == "" ? "0" : dr["business_counts"].ToString()) - decimal.Parse(dr["finish_counts"].ToString() == "" ? "0" : dr["finish_counts"].ToString());//剩余的 业务数量
                }
                else if(ImportType == "销售订单")
                {
                    dgvr.Cells["application_count"].Value = decimal.Parse(dr["business_count"].ToString() == "" ? "0" : dr["business_count"].ToString()) - decimal.Parse(dr["finish_count"].ToString() == "" ? "0" : dr["finish_count"].ToString());//剩余的 业务数量
                }
                dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码
                dgvr.Cells["relation_order"].Value = relation_order;
                dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                dgvr.Cells["create_time"].Value = DateTime.Now;
                dgvr.Cells["ImportType"].Value = ImportType;

                //根据申请数量、单价 计算金额
                dgvr.Cells["money"].Value = Convert.ToDecimal(dgvr.Cells["price"].Value) * Convert.ToInt32(dgvr.Cells["application_count"].Value);
                BindDataGridViewComboBoxCell(data_source, dr["parts_code"].ToString(), rowIndex, ref dgcombox, ref default_unit_id, ref default_price);
                dgvr.Cells["unit_id"].Value = dr["unit_id"] == null ? "" : dr["unit_id"].ToString();//单位
            }
            catch (Exception ex)
            { }
        }
        /// <summary>批量修改采购计划单/销售订单的导入状态 
        /// </summary>
        /// <param name="status">保存时：0 释放为正常，提交时：2 锁定</param>
        void ImportPurchasePlanStatus(string status)
        {
            List<string> listFieldPurchasePlan = new List<string>();
            List<string> listFieldSale = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                string ImportType = dr.Cells["ImportType"].Value == null ? "" : dr.Cells["ImportType"].Value.ToString();
                if (!string.IsNullOrEmpty(relation_order))
                {
                    if (ImportType == "采购计划单")
                    {
                        listFieldPurchasePlan.Add(relation_order);
                    }
                    else if (ImportType == "销售订单")
                    {
                        listFieldSale.Add(relation_order);
                    }
                }
            }
            Dictionary<string, string> Field = new Dictionary<string, string>();
            
            if (listFieldPurchasePlan.Count > 0)
            {
                Field.Add("import_status", status);//单据导入状态，0正常，1占用，2锁定
                DBHelper.BatchUpdateDataByIn("批量修改采购计划单导入状态", "tb_parts_purchase_plan", Field, "order_num", listFieldPurchasePlan.ToArray());
            }
            if (listFieldSale.Count > 0)
            {
                Field.Add("is_occupy", status);//单据导入状态，0正常，1占用，2锁定
                DBHelper.BatchUpdateDataByIn("批量修改销售订单单导入状态", "tb_parts_sale_order", Field, "order_num", listFieldSale.ToArray());
            }
        }
        /// <summary> 在提交/保存的时候 自动计算出列表中的申请数量、确认数量
        /// </summary>
        /// <param name="business_counts">申请数量</param>
        /// <param name="conf_count">确认数量</param>
        void GetBuesinessCountPartsInfo(ref decimal application_count, ref decimal conf_count, ref string parts_codes, ref string parts_names)
        {
            decimal Newapplication_count = 0;
            decimal Newconf_count = 0;
            foreach (DataGridViewRow dr in gvPurchaseList.Rows)
            {
                if (dr.Cells["parts_code"].Value == null)
                {
                    continue;
                }
                Newapplication_count = Newapplication_count + Convert.ToDecimal(dr.Cells["application_count"].Value == null ? "0" : dr.Cells["application_count"].Value.ToString() == "" ? "0" : dr.Cells["application_count"].Value.ToString());
                Newconf_count = Newconf_count + Convert.ToDecimal(dr.Cells["conf_count"].Value == null ? "0" : dr.Cells["conf_count"].Value.ToString() == "" ? "0" : dr.Cells["conf_count"].Value.ToString());
                parts_codes = parts_codes + dr.Cells["parts_code"].Value + ",";
                parts_names = parts_names + dr.Cells["parts_name"].Value + ",";
            }
            application_count = Newapplication_count;
            conf_count = Newconf_count;
            parts_codes = parts_codes.Trim(',');
            parts_names = parts_names.Trim(',');
        }
        /// <summary> 编辑配件列表中的配件信息
        /// </summary>
        void EditPartsInfo()
        {
            if (oldindex > -1)
            {
                try
                {
                    frmPartsByYuTong frm = new frmPartsByYuTong();
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
        #endregion

        #region 自动结算金额
        /// <summary> 改变单元格内容的事件
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
        /// <summary> 自动计算金额的方法
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
                if (ColumnName == "application_count" || ColumnName == "price")
                {
                    c.Text = string.IsNullOrEmpty(c.Text) ? "0" : c.Text;
                }
                if (ColumnName == "application_count")
                {
                    string price = gvPurchaseList.Rows[rowIndex].Cells["price"].Value == null ? "0" : gvPurchaseList.Rows[rowIndex].Cells["price"].Value.ToString();
                    price = string.IsNullOrEmpty(price) ? "0" : price;
                    gvPurchaseList.Rows[rowIndex].Cells["money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(price);
                }
                else if (ColumnName == "price")
                {
                    string application_count = gvPurchaseList.Rows[rowIndex].Cells["application_count"].Value == null ? "0" : gvPurchaseList.Rows[rowIndex].Cells["application_count"].Value.ToString();
                    application_count = string.IsNullOrEmpty(application_count) ? "0" : application_count;
                    gvPurchaseList.Rows[rowIndex].Cells["money"].Value = Convert.ToDecimal(c.Text) * Convert.ToDecimal(application_count);
                }
            }
            catch (Exception ex)
            { }
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
        void Set_Original_price(decimal price, int rowsindex)
        {
            gvPurchaseList.Rows[rowsindex].Cells["price"].Value = price.ToString();
            string application_count = gvPurchaseList.Rows[rowsindex].Cells["application_count"].Value == null ? "0" : gvPurchaseList.Rows[rowsindex].Cells["application_count"].Value.ToString();
            application_count = string.IsNullOrEmpty(application_count) ? "0" : application_count;
            gvPurchaseList.Rows[rowsindex].Cells["money"].Value = price * Convert.ToDecimal(application_count);
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
                        model.price = Convert.ToDecimal(dt.Rows[i]["ref_in_price"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["ref_in_price"].ToString()) ? "0" : dt.Rows[i]["ref_in_price"].ToString());
                    }
                    else if (data_source == "2")//宇通配件
                    {
                        model.price = Convert.ToDecimal(dt.Rows[i]["out_price_two"] == null ? "0" : string.IsNullOrEmpty(dt.Rows[i]["out_price_two"].ToString()) ? "0" : dt.Rows[i]["out_price_two"].ToString());
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
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
                                                        select 
                                                        tb_plan.order_num,tb_plan.parts_code,sum(tb_order.application_count) relation_count,tb_order.ImportType
                                                         from
                                                         (
                                                           select b.relation_order,b.parts_code,b.application_count,b.ImportType from tb_parts_purchase_order_2 a 
                                                           inner join tb_parts_purchase_order_p_2 b on a.purchase_order_yt_id=b.purchase_order_yt_id
                                                           where a.order_status in ('1','2')
                                                         ) tb_order
                                                        left join 
                                                         (
                                                           select a.order_num,b.parts_code,b.business_counts from tb_parts_purchase_plan a 
                                                           inner join tb_parts_purchase_plan_p b on a.plan_id=b.plan_id {0}
                                                         ) tb_plan 
                                                         on tb_plan.order_num=tb_order.relation_order and tb_plan.parts_code=tb_order.parts_code 
                                                         where len(tb_plan.order_num)>0 and LEN(tb_plan.parts_code)>0
                                                        group by tb_plan.order_num,tb_plan.parts_code
                                                        ,tb_order.ImportType
                                                        union
                                                        select 
                                                         tb_sale_order.order_num,tb_sale_order.parts_code,sum(tb_pur_order.application_count) relation_count,tb_pur_order.ImportType
                                                         from
                                                         (
                                                          select b.relation_order,b.parts_code,b.application_count,b.ImportType from tb_parts_purchase_order_2 a 
                                                          inner join tb_parts_purchase_order_p_2 b on a.purchase_order_yt_id=b.purchase_order_yt_id
                                                          where a.order_status in ('1','2')
                                                         ) tb_pur_order
                                                        left join 
                                                        (
                                                          select a.order_num,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                          inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id {0}
                                                        ) tb_sale_order 
                                                         on tb_sale_order.order_num=tb_pur_order.relation_order and tb_sale_order.parts_code=tb_pur_order.parts_code 
                                                         where len(tb_sale_order.order_num)>0 and LEN(tb_sale_order.parts_code)>0
                                                         group by tb_sale_order.order_num,tb_sale_order.parts_code
                                                        ,tb_pur_order.ImportType
                                                    ) tb_pur_order_finishcount ", files);
                return dt = DBHelper.GetTable("查询宇通采购订单导入采购计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            { return dt; }
            finally { }
        }
        /// <summary> 获取前置单据的业务信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        DataTable GetBusinessCount(string purchase_order_yt_id)
        {
            DataTable dt = null;
            try
            {
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
	                                                    select a.plan_id,b.order_num,a.parts_code,a.business_counts from
	                                                    (
		                                                     select * from tb_parts_purchase_plan_p where plan_id in
		                                                     (
			                                                     select plan_id from tb_parts_purchase_plan where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_purchase_order_p_2 
				                                                    where purchase_order_yt_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_purchase_plan b on a.plan_id=b.plan_id
	                                                    union
	                                                    select a.sale_order_id as plan_id,b.order_num,a.parts_code,a.business_count as business_counts from
	                                                    (
		                                                     select * from tb_parts_sale_order_p where sale_order_id in
		                                                     (
			                                                     select sale_order_id from tb_parts_sale_order where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_purchase_order_p_2 
				                                                    where purchase_order_yt_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_sale_order b on a.sale_order_id=b.sale_order_id
                                                    ) tb_pur_order_businesscount ", purchase_order_yt_id);
                return dt = DBHelper.GetTable("查询宇通采购订单导入采购计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            {
                return dt;
            }
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
                if (item.importtype == "采购计划单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_purchase_plan set import_status=@import_status where order_num=@order_num;";
                    dic.Add("import_status", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    dic.Add("order_num", item.order_num);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                else if (item.importtype == "销售订单")
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
            } 
            #endregion
            
            #region 更新前置单据中的各个配件的已完成数量
            foreach (OrderFinishInfo item in list_orderinfo)
            {
                if (!list_plan.Contains(item.plan_id))
                {
                    list_plan.Add(item.plan_id);
                    plan_ids = plan_ids + "'" + item.plan_id + "',";
                }
                if (item.importtype == "采购计划单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_purchase_plan_p set finish_counts=@finish_counts where plan_id=@plan_id and parts_code=@parts_code;";
                    dic.Add("finish_counts", item.finish_num);
                    dic.Add("plan_id", item.plan_id);
                    dic.Add("parts_code", item.parts_code);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                else if (item.importtype == "销售订单")
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_order_p set finish_count=@finish_count where sale_order_id=@sale_order_id and parts_code=@parts_code;";
                    dic.Add("finish_count", item.finish_num);
                    dic.Add("sale_order_id", item.plan_id);
                    dic.Add("parts_code", item.parts_code);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            } 
            #endregion
            bool ret = DBHelper.BatchExeSQLStringMultiByTrans("提交宇通采购订单，更新引用的采购计划单或销售订单的导入状态", listSql);
            if (ret)
            {
                if (list_orderinfo.Count > 0)
                {
                    listSql.Clear();
                    plan_ids = plan_ids.Trim(',');
                    string TableName = string.Format(@"
                   (
                    select plan_id,sum(finish_counts) finish_counts,
                    sum(finish_counts*business_price) as finish_money 
                        from tb_parts_purchase_plan_p 
                    where plan_id in ({0})
                    group by plan_id
                    ) tb_purchase_finish", plan_ids);
                    DataTable dt = DBHelper.GetTable("查询采购计划单各配件完成数量和完成金额", TableName, "*", "", "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sysStringSql = new SysSQLString();
                            sysStringSql.cmdType = CommandType.Text;
                            dic = new Dictionary<string, string>();
                            string sql1 = "update tb_parts_purchase_plan set finish_counts=@finish_counts,plan_finish_money=@plan_finish_money where plan_id=@plan_id;";
                            dic.Add("finish_counts", dt.Rows[i]["finish_counts"].ToString());
                            dic.Add("plan_finish_money", dt.Rows[i]["finish_money"].ToString());
                            dic.Add("plan_id", dt.Rows[i]["plan_id"].ToString());
                            sysStringSql.sqlString = sql1;
                            sysStringSql.Param = dic;
                            listSql.Add(sysStringSql);
                        }
                        DBHelper.BatchExeSQLStringMultiByTrans("完成宇通采购订单后，更新采购计划单的完成数量和完成金额", listSql);
                    }
                }
            }
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

            string plan_id = string.Empty;
            string order_num = string.Empty;
            string parts_code = string.Empty;
            string importtype = string.Empty;
            if (dt_Business.Rows.Count > 0)
            {
                for (int i = 0; i < dt_Business.Rows.Count; i++)
                {
                    bool isfinish = true;
                    decimal BusinessCount = decimal.Parse(dt_Business.Rows[i]["business_counts"].ToString());
                    plan_id = dt_Business.Rows[i]["plan_id"].ToString();
                    order_num = dt_Business.Rows[i]["order_num"].ToString();
                    parts_code = dt_Business.Rows[i]["parts_code"].ToString();
                    DataRow[] dr = dt_Finish.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                    if (dr != null && dr.Count() > 0)
                    {
                        importtype = dr[0]["ImportType"].ToString();

                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.plan_id = plan_id;
                        orderfinish_info.parts_code = parts_code;
                        orderfinish_info.finish_num = dr[0]["relation_count"].ToString();
                        orderfinish_info.importtype = importtype;
                        list_orderinfo.Add(orderfinish_info);
                        if (decimal.Parse(dr[0]["relation_count"].ToString()) < BusinessCount)
                        {
                            isfinish = false;
                        }
                    }
                    else
                    {
                        orderfinish_info = new OrderFinishInfo();
                        orderfinish_info.plan_id = plan_id;
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
        public string plan_id;
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
