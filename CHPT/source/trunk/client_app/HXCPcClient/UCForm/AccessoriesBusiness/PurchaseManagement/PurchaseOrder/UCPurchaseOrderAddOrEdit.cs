using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using Model;
using System.Reflection;
using HXCPcClient.Chooser;
using HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling;
using HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder
{
    public partial class UCPurchaseOrderAddOrEdit : UCBase
    {
        #region 变量
        WindowStatus status;
        UCPurchaseOrderManager uc;
        int rowIndex = -1;
        int oldindex = -1;
        string order_id = string.Empty;
        /// <summary>
        /// 通过选择器得到的供应商id
        /// </summary>
        string sup_id = string.Empty;
        /// <summary>
        /// 通过选择器得到的供应商编码
        /// </summary>
        string sup_code = string.Empty;
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
        tb_parts_purchase_order tb_partspurchaseorder_Model = new tb_parts_purchase_order();
        #endregion

        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        /// <param name="status"></param>
        /// <param name="order_id"></param>
        /// <param name="uc"></param>
        public UCPurchaseOrderAddOrEdit(WindowStatus status, string order_id, UCPurchaseOrderManager uc)
        {
            InitializeComponent();
            this.uc = uc;
            this.status = status;
            this.order_id = order_id;
            this.Resize += new EventHandler(UCPurchaseOrderAddOrEdit_Resize);
            base.SaveEvent += new ClickHandler(UCPurchaseOrderAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCPurchaseOrderAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCPurchaseOrderManager_ImportEvent);
            base.CancelEvent += new ClickHandler(UCPurchaseOrderAddOrEdit_CancelEvent);
            gvPurchaseList.CellDoubleClick += new DataGridViewCellEventHandler(gvPurchaseList_CellDoubleClick);

            business_counts.ValueType = typeof(decimal);
            //business_counts.MaxInputLength = 9;
            original_price.ValueType = typeof(decimal);
            //original_price.MaxInputLength = 9;
            discount.ValueType = typeof(decimal);
            discount.MaxInputLength = 3;
            business_price.ValueType = typeof(decimal);
            //business_price.MaxInputLength = 9;
            tax_rate.ValueType = typeof(decimal);
            tax_rate.MaxInputLength = 3;

            txtcontract_no.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtcontract_no_UserControlValueChanged);

            txtprepaid_money.KeyPress += new KeyPressEventHandler(txtprepaid_money_KeyPress);
            txtprepaid_money.UserControlValueChanged += new TextBoxEx.TextBoxChangedHandle(txtprepaid_money_UserControlValueChanged);
            
            ddtarrival_date.ValueChanged += new EventHandler(ddtarrival_date_ValueChanged);
        }
        /// <summary> 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchaseOrderAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            { base.SetButtonVisiableHandleAddCopy(); }
            else if (status == WindowStatus.Edit)
            { base.SetButtonVisiableHandleEdit(); }

            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "unit_id", "original_price", "business_price", "business_counts", "discount", "tax_rate", "is_gift", "arrival_date", "remark" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);
            //CommonFuncCall.BindUnit(unit_id);

            ddtorder_date.Value = DateTime.Now;
            ddtvalid_till.Value = DateTime.Now;
            ddtarrival_date.Value = DateTime.Now;
            lbloperator_name.Text = GlobalStaticObj.UserName;

            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_mode, "sys_trans_mode", "请选择");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlclosing_way, "请选择");
            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");
            dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");

            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(order_id);
                GetAccessories(order_id);
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblorder_status_name.Text = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                txtarrival_place.Caption = CommonFuncCall.GetStationAdress();
            }
            gvPurchaseList.Rows.Add(1);
            label31.Visible = !string.IsNullOrEmpty(txtprepaid_money.Caption.Trim()) ? Convert.ToDecimal(txtprepaid_money.Caption.Trim()) > 0 : false;
            Choosefrm.SupperCodeChoose(txtsup_code1, Choosefrm.delDataBack = Closing_UnitName_DataBack);
            Choosefrm.SupperNameChoose(txtclosing_unit1, Choosefrm.delDataBack = null);
            //string[] total = { business_counts.Name};
            //ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);

            //List<string> list_columns = new List<string>();
            //list_columns.Add("original_price");
            //list_columns.Add("business_price");
            //list_columns.Add("business_counts");
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
        void UCPurchaseOrderAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("保存");
        }
        /// <summary> 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveEventOrSubmitEventFunction("提交");
        }
        /// <summary> 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消操作关闭当前界面吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        /// <summary> 导入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_ImportEvent(object sender, EventArgs e)
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
                    string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                    if (ImportOrderType == "销售订单")
                    {
                        MessageBoxEx.Show("订单中已经存在销售订单的信息,不允许再次导入采购计划单的信息!");
                        return;
                    }
                }
                UCChoosePurchasePlanOrder frm = new UCChoosePurchasePlanOrder("");
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
                                DataTable dt = DBHelper.GetTable("查询采购计划单配件表信息", "tb_parts_purchase_plan_p", "*", " plan_id='" + planid + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_counts,0)<isnull(business_counts,0) ", "", "");
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
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
                    string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                    if (ImportOrderType == "采购计划单")
                    {
                        MessageBoxEx.Show("订单中已经存在采购计划单的信息,不允许再次导入销售订单的信息!");
                        return;
                    }
                }
                UCChooseSaleOrder frm = new UCChooseSaleOrder(string.Empty,"");
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
                                DataTable dt = DBHelper.GetTable("查询销售订单配件表信息", "tb_parts_sale_order_p", "*", " sale_order_id='" + sale_order_id + "' and parts_code='" + partscode + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
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
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 添加配件信息到列表的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsPurchaseAccessoriesAdd_Click(object sender, EventArgs e)
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
                        GetGridViewRowByDr(dgvr, dr, "", rowsindex, "Add");
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        gvPurchaseList.Rows.Add(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
            //            string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
            //            if (!string.IsNullOrEmpty(relation_order))
            //            {
            //                if (!CheckPre_order_code(relation_order))
            //                {
            //                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
            //                    if (ImportOrderType == "采购计划单")
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
            //                    else if (ImportOrderType == "销售订单")
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
            //    {
            //        MessageBoxEx.Show("操作失败！");
            //        HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            //    }
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
                            string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    if (ImportOrderType == "采购计划单")
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
                                    else if (ImportOrderType == "销售订单")
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
                                string business_counts = dr.Cells["business_counts"].EditedFormattedValue == null ? "0" : dr.Cells["business_counts"].EditedFormattedValue.ToString();
                                business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
                                if (Convert.ToDecimal(business_counts) == 0)
                                {
                                    #region 当删除成功时，将前置单据的状态释放为正常
                                    string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                                    string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                                    if (!string.IsNullOrEmpty(relation_order))
                                    {
                                        if (!CheckPre_order_code(relation_order))
                                        {
                                            Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                            if (ImportOrderType == "采购计划单")
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
                                            else if (ImportOrderType == "销售订单")
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
        /// <summary> 选择供应商编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtsup_code1_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmSupplier chooseSupplier = new frmSupplier();
                chooseSupplier.ShowDialog();
                sup_id = chooseSupplier.supperID;
                if (!string.IsNullOrEmpty(sup_id))
                {
                    sup_code = chooseSupplier.supperCode;
                    txtsup_code1.Text = chooseSupplier.supperCode;
                    txtsup_name.Caption = chooseSupplier.supperName;
                    txtclosing_unit1.Text = chooseSupplier.supperName;

                    DataTable dt = DBHelper.GetTable("查询供应商档案信息", "tb_supplier", "*", " enable_flag != 0 and sup_id='" + sup_id + "'", "", "");
                    if (dt.Rows.Count > 0)
                    {
                        txtfax.Caption = dt.Rows[0]["sup_fax"].ToString();
                        txtcontacts_tel.Caption = dt.Rows[0]["sup_tel"].ToString();
                    }
                    string TableName = string.Format(@"(
                                                    select cont_name from tb_contacts where cont_id=
                                                    (select cont_id from tr_base_contacts where relation_object_id='{0}' and is_default='1')
                                                    ) tb_conts", sup_id);
                    DataTable dt_conts = DBHelper.GetTable("查询供应商默认的联系人信息", TableName, "*", "", "", "");
                    if (dt_conts != null && dt_conts.Rows.Count > 0)
                    {
                        txtcontacts.Caption = dt_conts.Rows[0]["cont_name"].ToString();
                    }
                    else
                    { txtcontacts.Caption = string.Empty; }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 选择结算单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtclosing_unit1_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmSupplier chooseSupplier = new frmSupplier();
                chooseSupplier.ShowDialog();
                if (!string.IsNullOrEmpty(chooseSupplier.supperID))
                {
                    txtclosing_unit1.Text = chooseSupplier.supperName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 限制文本框输入的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtprepaid_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressFolat(sender, e);
        }
        /// <summary> 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), "请选择");
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
                    if (fieldNmae.Equals("business_counts"))
                    {
                        //string num = gvPurchaseList.Rows[e.RowIndex].Cells["business_counts"].Value.ToString();
                        //num = string.IsNullOrEmpty(num) ? "0" : num;
                        //if (Convert.ToDecimal(num) < 0)
                        //{ gvPurchaseList.Rows[e.RowIndex].Cells["business_counts"].Style.ForeColor = Color.Red; }
                        //else
                        //{ gvPurchaseList.Rows[e.RowIndex].Cells["business_counts"].Style.ForeColor = Color.Black; }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
        /// <summary> 合同号输入长度限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtcontract_no_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedLength(sender, 20);
        }
        /// <summary> 预付金额输入金额限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtprepaid_money_UserControlValueChanged(object sender, EventArgs e)
        {
            ValidationRegex.TextChangedFolat(sender, 100000000);
            label31.Visible = !string.IsNullOrEmpty(txtprepaid_money.Caption.Trim()) ? Convert.ToDecimal(txtprepaid_money.Caption.Trim()) > 0 : false;
        }
        /// <summary> 单据的到货时间更改时，更新配件列表中的到货时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddtarrival_date_ValueChanged(object sender, EventArgs e)
        {
            if (ddtarrival_date.Value != null)
            {
                if (gvPurchaseList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in gvPurchaseList.Rows)
                    {
                        if (dr.Cells["parts_code"].Value == null)
                        {
                            continue;
                        }
                        if (ddtarrival_date.Value != null)
                        {
                            dr.Cells["arrival_date"].Value = ddtarrival_date.Value.ToShortDateString();//到货时间
                        }
                    }
                }
            }
        }
        /// <summary> 统计数量、金额的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderAddOrEdit_Resize(object sender, EventArgs e)
        {
            string[] total = { business_counts.Name, tax.Name, payment.Name, valorem_together.Name };
            ControlsConfig.DatagGridViewTotalConfig(gvPurchaseList, total);
        }
        #endregion

        #region 方法、函数
        /// <summary> 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            if (string.IsNullOrEmpty(sup_id))
            {
                MessageBoxEx.Show("请选择供应商编码!"); return false;
            }
            if (string.IsNullOrEmpty(txtclosing_unit1.Text))
            {
                MessageBoxEx.Show("请选择结算单位!"); return false;
            }
            if (!string.IsNullOrEmpty(txtprepaid_money.Caption.Trim()))
            {
                if (Convert.ToDecimal(txtprepaid_money.Caption.Trim()) != 0)
                {
                    if (string.IsNullOrEmpty(ddlclosing_way.SelectedValue.ToString()))
                    {
                        MessageBoxEx.Show("预付金额不为0的情况下,请选择结算方式!");
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
        /// <summary> 加载采购计划信息和配件信息
        /// </summary>
        /// <param name="order_id"></param>
        private void LoadInfo(string order_id)
        {
            try
            {
                if (!string.IsNullOrEmpty(order_id))
                {
                    //1.查看一条采购订单信息
                    DataTable dt = DBHelper.GetTable("查看一条采购订单信息", "tb_parts_purchase_order", "*", " order_id='" + order_id + "'", "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CommonFuncCall.SetModlByDataTable(tb_partspurchaseorder_Model, dt);
                        CommonFuncCall.SetShowControlValue(this, tb_partspurchaseorder_Model, "");
                        sup_id = tb_partspurchaseorder_Model.sup_id;
                        txtsup_code1.Text = tb_partspurchaseorder_Model.sup_code;
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
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>  根据采购订单号获取采购配件信息
        /// </summary>
        /// <param name="order_id"></param>
        private void GetAccessories(string order_id)
        {
            try
            {
                string conId = string.Empty;
                DataTable dt_parts_purchase = DBHelper.GetTable("查询采购订单配件表信息", "tb_parts_purchase_order_p", "*", " order_id='" + order_id + "'", "", "");
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
                        dgvr.Cells["business_counts"].Value = dr["business_counts"];
                        dgvr.Cells["original_price"].Value = dr["original_price"];
                        dgvr.Cells["discount"].Value = dr["discount"];
                        dgvr.Cells["business_price"].Value = dr["business_price"];
                        dgvr.Cells["tax_rate"].Value = dr["tax_rate"];
                        dgvr.Cells["tax"].Value = dr["tax"];
                        dgvr.Cells["payment"].Value = dr["payment"];
                        dgvr.Cells["valorem_together"].Value = dr["valorem_together"];
                        dgvr.Cells["auxiliary_count"].Value = dr["auxiliary_count"];
                        dgvr.Cells["is_gift"].Value = dr["is_gift"];
                        dgvr.Cells["is_suspend"].Value = dr["is_suspend"];
                        dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"]));
                        dgvr.Cells["remark"].Value = dr["remark"];
                        if (status == WindowStatus.Copy)
                        {
                            dgvr.Cells["relation_order"].Value = string.Empty;
                            dgvr.Cells["ImportOrderType"].Value = string.Empty;
                            dgvr.Cells["create_by"].Value = null;
                            dgvr.Cells["create_name"].Value = null;
                            dgvr.Cells["create_time"].Value = null;
                        }
                        else
                        {
                            dgvr.Cells["relation_order"].Value = dr["relation_order"];
                            dgvr.Cells["ImportOrderType"].Value = dr["ImportOrderType"];
                            dgvr.Cells["create_by"].Value = dr["create_by"];
                            dgvr.Cells["create_name"].Value = dr["create_name"];
                            dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                        }

                        dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                        dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码

                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
        /// <summary>检查单价输入的完整性 
        /// </summary>
        /// <returns></returns>
        private bool CheckPartsSetup()
        {
            bool check = true;
            foreach (DataGridViewRow dgvr in gvPurchaseList.Rows)
            {
                string purchaseUnit = CommonCtrl.IsNullToString(dgvr.Cells["unit_id"].EditedFormattedValue);//采购单位
                if (purchaseUnit.Length == 0)
                {
                    MessageBoxEx.Show("请选择配件单位!");
                    gvPurchaseList.CurrentCell = dgvr.Cells["unit_id"];
                    check = false;
                    break;
                }
            }
            return check;
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
                    string opName = "采购订单操作";
                    if (HandleTypeName == "提交")
                    {
                        SucessMess = "提交";
                        if (lblorder_num.Text == "")
                        {
                            lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.PurchaseOrder);//获取采购订单编号
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
                        order_id = Guid.NewGuid().ToString();
                        AddPurchaseOrderSqlString(listSql, order_id, HandleTypeName);
                        opName = "新增采购订单";
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditPurchaseOrderSqlString(listSql, order_id, tb_partspurchaseorder_Model, HandleTypeName);
                        opName = "修改采购订单";
                    }
                    DealAccessories(listSql, order_id);
                    GetPre_Order_Code(listSql, order_id, lblorder_num.Text);
                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("" + SucessMess + "成功！");

                        if (HandleTypeName == "保存")
                        { ImportPurchasePlanStatus("0"); }
                        else if (HandleTypeName == "提交")
                        {
                            SetOrderStatus(order_id);
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
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 在编辑和添加时对主数据和采购订单配件表中的配件信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="order_id"></param>
        private void DealAccessories(List<SysSQLString> listSql, string order_id)
        {
            try
            {
                string PartsCodes = GetListPartsCodes();
                DataTable dt_CarType = CommonFuncCall.GetCarType(PartsCodes);
                DataTable dt_PartsType = CommonFuncCall.GetPartsType(PartsCodes);

                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();

                string sql1 = "delete from tb_parts_purchase_order_p where order_id=@order_id;";
                dic.Add("order_id", order_id);
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
                        dic.Add("ID", Guid.NewGuid().ToString());
                        dic.Add("order_id", order_id);
                        string parts_code = string.Empty;
                        if (dr.Cells["parts_code"].Value == null)
                        { parts_code = string.Empty; }
                        else
                        { parts_code = dr.Cells["parts_code"].Value.ToString(); }

                        dic.Add("parts_id", dr.Cells["parts_id"].Value.ToString());
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
                        dic.Add("business_counts", dr.Cells["business_counts"].Value == null ? "0" : dr.Cells["business_counts"].Value.ToString() == "" ? "0" : dr.Cells["business_counts"].Value.ToString());
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

                        if (dr.Cells["arrival_date"].Value != null)
                        {
                            dic.Add("arrival_date", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(Convert.ToDateTime(dr.Cells["arrival_date"].Value.ToString()).ToShortDateString() + " 23:59:59")).ToString());
                        }
                        else
                        {
                            dic.Add("arrival_date", "0");
                        }
                        dic.Add("remark", dr.Cells["remark"].Value == null ? "" : dr.Cells["remark"].Value.ToString());
                        dic.Add("ImportOrderType", dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString());
                        dic.Add("parts_barcode", dr.Cells["parts_barcode"].Value == null ? "" : dr.Cells["parts_barcode"].Value.ToString());
                        dic.Add("car_factory_code", dr.Cells["car_factory_code"].Value == null ? "" : dr.Cells["car_factory_code"].Value.ToString());
                        dic.Add("create_by", dr.Cells["create_by"].Value == null ? "" : dr.Cells["create_by"].Value.ToString());
                        dic.Add("create_name", dr.Cells["create_name"].Value == null ? "" : dr.Cells["create_name"].Value.ToString());
                        dic.Add("create_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["create_time"].Value == null ? DateTime.Now.ToString() : dr.Cells["create_time"].Value.ToString())).ToString());
                        dic.Add("update_by", GlobalStaticObj.UserID);
                        dic.Add("update_name", GlobalStaticObj.UserName);
                        dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                        dic.Add("finish_counts", "0");

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

                        string sql2 = string.Format(@"Insert Into tb_parts_purchase_order_p(ID,order_id,parts_id,parts_code,parts_name,drawing_num,unit_id,unit_name,
                    parts_brand,parts_brand_name,business_counts,original_price,discount,business_price,tax_rate,tax,payment,valorem_together,
                    auxiliary_count,relation_order,is_gift,is_suspend,arrival_date,remark,ImportOrderType,parts_barcode,car_factory_code,create_by,create_name,create_time,
                    update_by,update_name,update_time,finish_counts,vm_name,parts_type_id,parts_type_name) values(@ID,@order_id,@parts_id,@parts_code,@parts_name,@drawing_num,@unit_id,@unit_name,
                    @parts_brand,@parts_brand_name,@business_counts,@original_price,@discount,@business_price,@tax_rate,@tax,@payment,@valorem_together,
                    @auxiliary_count,@relation_order,@is_gift,@is_suspend,@arrival_date,@remark,@ImportOrderType,@parts_barcode,@car_factory_code,@create_by,@create_name,@create_time,
                    @update_by,@update_name,@update_time,@finish_counts,@vm_name,@parts_type_id,@parts_type_name);");
                        sysStringSql.sqlString = sql2;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
                dgvr.Cells["original_price"].Value = default_price;//原始单价
                dgvr.Cells["discount"].Value = "100";//折扣
                dgvr.Cells["business_price"].Value = default_price;//业务单价
                dgvr.Cells["parts_brand"].Value = dr["parts_brand"];//品牌
                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_parts_code"];//厂商编码
                if (ddtarrival_date.Value != null)
                {
                    dgvr.Cells["arrival_date"].Value = ddtarrival_date.Value.ToShortDateString();//到货时间
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
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 批量导入的方式
        /// </summary>
        /// <param name="dgvr"></param>
        /// <param name="dr"></param>
        /// <param name="relation_order"></param>
        /// <param name="HandleType"></param>
        void GetGridViewRowByDrImport(DataGridViewRow dgvr, DataRow dr, string relation_order,int rowIndex,  string ImportOrderType)
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
                if (ImportOrderType == "采购计划单")
                    dgvr.Cells["business_counts"].Value = decimal.Parse(dr["business_counts"].ToString() == "" ? "0" : dr["business_counts"].ToString()) - decimal.Parse(dr["finish_counts"].ToString() == "" ? "0" : dr["finish_counts"].ToString());//剩余的 业务数量
                else if (ImportOrderType == "销售订单")
                    dgvr.Cells["business_counts"].Value = decimal.Parse(dr["business_count"].ToString() == "" ? "0" : dr["business_count"].ToString()) - decimal.Parse(dr["finish_count"].ToString() == "" ? "0" : dr["finish_count"].ToString());//剩余的 业务数量
                dgvr.Cells["original_price"].Value = dr["original_price"];//原始单价
                dgvr.Cells["discount"].Value = dr["discount"];//折扣
                dgvr.Cells["business_price"].Value = dr["business_price"];//业务单价
                dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];//条码
                dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//厂商编码

                dgvr.Cells["relation_order"].Value = relation_order;

                dgvr.Cells["create_by"].Value = GlobalStaticObj.UserID;
                dgvr.Cells["create_name"].Value = GlobalStaticObj.UserName;
                dgvr.Cells["create_time"].Value = DateTime.Now;
                dgvr.Cells["ImportOrderType"].Value = ImportOrderType;

                //根据业务单价、业务数量 计算货款
                dgvr.Cells["payment"].Value = Convert.ToDecimal(dgvr.Cells["business_price"].Value) * Convert.ToInt32(dgvr.Cells["business_counts"].Value);
                //根据货款、税率 计算税款
                dgvr.Cells["tax"].Value = Convert.ToDecimal(dgvr.Cells["payment"].Value) * 0;//默认税率是0
                //根据税款、货款 计算税价合计
                dgvr.Cells["valorem_together"].Value = Convert.ToDecimal(dgvr.Cells["tax"].Value) + Convert.ToDecimal(dgvr.Cells["payment"].Value);
                if (ddtarrival_date.Value != null)
                {
                    dgvr.Cells["arrival_date"].Value = ddtarrival_date.Value.ToShortDateString();//到货时间
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>批量修改采购计划单/销售订单的导入状态 
        /// </summary>
        /// <param name="status">保存时：0 释放为正常，提交时：2 锁定(部分导入) 3 锁定(全部导入)</param>
        void ImportPurchasePlanStatus(string status)
        {
            try
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
                    string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                    if (!string.IsNullOrEmpty(relation_order))
                    {
                        if (ImportOrderType == "采购计划单")
                        {
                            listFieldPurchasePlan.Add(relation_order);
                        }
                        else if (ImportOrderType == "销售订单")
                        {
                            listFieldSale.Add(relation_order);
                        }
                    }
                }
                Dictionary<string, string> Field = new Dictionary<string, string>();
                if (listFieldPurchasePlan.Count > 0)
                {
                    Field.Add("import_status", status);//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    DBHelper.BatchUpdateDataByIn("批量修改采购计划单导入状态", "tb_parts_purchase_plan", Field, "order_num", listFieldPurchasePlan.ToArray());
                }
                if (listFieldSale.Count > 0)
                {
                    Field.Add("is_occupy", status);//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    DBHelper.BatchUpdateDataByIn("批量修改销售订单单导入状态", "tb_parts_sale_order", Field, "order_num", listFieldSale.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
        /// <summary> 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="order_id"></param>
        private void AddPurchaseOrderSqlString(List<SysSQLString> listSql, string order_id, string HandleType)
        {
            try
            {
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

                decimal business_counts = 0;
                decimal payment = 0;
                decimal tax = 0;
                decimal valorem_together = 0;
                GetBuesinessMoney(ref business_counts, ref payment, ref tax, ref valorem_together);

                ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
                ddtvalid_till.Value = Convert.ToDateTime(ddtvalid_till.Value.ToShortDateString() + " 23:59:59");
                //ddtarrival_date.Value = Convert.ToDateTime(ddtarrival_date.Value.ToShortDateString() + " 23:59:59");
                tb_parts_purchase_order model = new tb_parts_purchase_order();
                CommonFuncCall.SetModelObjectValue(this, model);
                model.sup_id = sup_id;
                model.arrival_date = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(ddtarrival_date.Value.ToShortDateString() + " 23:59:59"));
                model.sup_code = txtsup_code1.Text.Trim();
                model.sup_name = txtsup_name.Caption.Trim();
                model.closing_unit = txtclosing_unit1.Text;
                model.order_id = order_id;
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
                model.order_quantity = business_counts;//订货数量
                model.payment = payment;//货款
                model.tax = tax;//税款
                model.money = valorem_together;//金额
                model.is_occupy = "0";
                model.is_lock = "0";
                model.enable_flag = "1";
                if (HandleType == "保存")
                {
                    model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT);
                    model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == "提交")
                {
                    model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT);
                    model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }

                if (!string.IsNullOrEmpty(ddltrans_mode.SelectedValue.ToString()))
                {
                    model.trans_mode = ddltrans_mode.SelectedValue.ToString();
                    model.trans_mode_name = ddltrans_mode.SelectedItem.ToString();
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
                    sb.Append(" Insert Into tb_parts_purchase_order( ");
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
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="order_id"></param>
        /// <param name="model"></param>
        private void EditPurchaseOrderSqlString(List<SysSQLString> listSql, string order_id, tb_parts_purchase_order model, string HandleType)
        {
            try
            {
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

                decimal business_counts = 0;
                decimal payment = 0;
                decimal tax = 0;
                decimal valorem_together = 0;
                GetBuesinessMoney(ref business_counts, ref payment, ref tax, ref valorem_together);

                ddtorder_date.Value = Convert.ToDateTime(ddtorder_date.Value.ToShortDateString() + " 23:59:59");
                ddtvalid_till.Value = Convert.ToDateTime(ddtvalid_till.Value.ToShortDateString() + " 23:59:59");
                //ddtarrival_date.Value = Convert.ToDateTime(ddtarrival_date.Value.ToShortDateString() + " 23:59:59");
                CommonFuncCall.SetModelObjectValue(this, model);
                model.sup_id = sup_id;
                model.arrival_date = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(ddtarrival_date.Value.ToShortDateString() + " 23:59:59"));
                model.sup_code = txtsup_code1.Text.Trim();
                model.sup_name = txtsup_name.Caption.Trim();
                model.sup_code = txtsup_code1.Text;
                model.closing_unit = txtclosing_unit1.Text;
                model.update_by = GlobalStaticObj.UserID;
                model.update_name = GlobalStaticObj.UserName;
                model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
                model.operators = GlobalStaticObj.UserID;
                model.operator_name = GlobalStaticObj.UserName;
                model.is_suspend = chkis_suspend.Checked ? "0" : "1";//选中(中止)：0,未选中(不中止)：1
                model.suspend_reason = txtsuspend_reason.Caption.Trim();//中止原因
                model.order_quantity = business_counts;//订货数量
                model.payment = payment;//货款
                model.tax = tax;//税款
                model.money = valorem_together;//金额

                model.enable_flag = "1";
                if (HandleType == "保存")
                {
                    model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT);
                    model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == "提交")
                {
                    model.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT);
                    model.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }

                if (!string.IsNullOrEmpty(ddltrans_mode.SelectedValue.ToString()))
                {
                    model.trans_mode = ddltrans_mode.SelectedValue.ToString();
                    model.trans_mode_name = ddltrans_mode.SelectedItem.ToString();
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
                    sb.Append(" Update tb_parts_purchase_order Set ");
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
                    sb.Append(" where order_id='" + order_id + "';");
                    sysStringSql.sqlString = sb.ToString();
                    sysStringSql.Param = dicParam;
                    listSql.Add(sysStringSql);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
                {
                    MessageBoxEx.Show("操作失败！");
                    HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                }
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
                            #region 当删除成功时，将前置单据的状态释放为正常
                            string relation_order = dr.Cells["relation_order"].Value == null ? "" : dr.Cells["relation_order"].Value.ToString();
                            string ImportOrderType = dr.Cells["ImportOrderType"].Value == null ? "" : dr.Cells["ImportOrderType"].Value.ToString();
                            if (!string.IsNullOrEmpty(relation_order))
                            {
                                if (!CheckPre_order_code(relation_order))
                                {
                                    Dictionary<string, string> dicValue = new Dictionary<string, string>();
                                    if (ImportOrderType == "采购计划单")
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
                                    else if (ImportOrderType == "销售订单")
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
        /// <summary> 自动计算金额的功能
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
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 金额变更的事件
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
                if (ColumnName == "original_price" || ColumnName == "discount" || ColumnName == "business_price" || ColumnName == "business_counts" || ColumnName == "tax_rate")
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
                #region 原始单价
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
                #endregion
                //折扣
                #region 折扣
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
                #endregion
                //业务单价
                #region 业务单价
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
                #endregion
                //业务数量
                #region 业务数量
                else if (ColumnName == "business_counts")
                {
                    //if (Convert.ToDecimal(c.Text) < 0)
                    //{ c.ForeColor = Color.Red; }
                    //else
                    //{ c.ForeColor = Color.Black; }
                    //货款
                    GetPaymentByBusiness_counts(c.Text,NewrowIndex);
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                } 
                #endregion
                //税率
                #region 税率
                else if (ColumnName == "tax_rate")
                {
                    //税款
                    GetTax(NewrowIndex);
                    //税价合计
                    GetValorem_Together(NewrowIndex);
                } 
                #endregion
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
            string business_counts = gvPurchaseList.Rows[NewrowIndex].Cells["business_counts"].Value == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["business_counts"].Value.ToString();
            business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
            //货款
            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_price) * Convert.ToDecimal(business_counts);
        }
        /// <summary>修改业务数量后, 获取货款
        /// </summary>
        void GetPaymentByBusiness_counts(string business_counts, int NewrowIndex)
        {
            //业务单价
            string business_price = gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;
            //货款
            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_counts) * Convert.ToDecimal(business_price);
        }
        /// <summary> 获取货款
        /// </summary>
        void GetPayment(int NewrowIndex)
        {
            //业务数量
            string business_counts = gvPurchaseList.Rows[NewrowIndex].Cells["business_counts"].Value == null ? "0" : gvPurchaseList.Rows[NewrowIndex].Cells["business_counts"].Value.ToString();
            business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
            //业务单价
            string business_price = gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value == null ? "" : gvPurchaseList.Rows[NewrowIndex].Cells["business_price"].Value.ToString();
            business_price = string.IsNullOrEmpty(business_price) ? "0" : business_price;

            gvPurchaseList.Rows[NewrowIndex].Cells["payment"].Value = Convert.ToDecimal(business_counts) * Convert.ToDecimal(business_price);
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
                Newbusiness_counts = Newbusiness_counts + Convert.ToDecimal(dr.Cells["business_counts"].Value == null ? "0" : dr.Cells["business_counts"].Value.ToString() == "" ? "0" : dr.Cells["business_counts"].Value.ToString());
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
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
            string business_counts = gvPurchaseList.Rows[rowsindex].Cells["business_counts"].Value == null ? "0" : gvPurchaseList.Rows[rowsindex].Cells["business_counts"].Value.ToString();
            business_counts = string.IsNullOrEmpty(business_counts) ? "0" : business_counts;
            //货款
            gvPurchaseList.Rows[rowsindex].Cells["payment"].Value = Convert.ToDecimal(business_counts) * Convert.ToDecimal(business_price);
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
            try
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
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
                                                        tb_plan.order_num,tb_plan.parts_code,sum(tb_order.business_counts) relation_count,tb_order.ImportOrderType
                                                         from
                                                         (
                                                           select b.relation_order,b.parts_code,b.business_counts,b.ImportOrderType from tb_parts_purchase_order a 
                                                           inner join tb_parts_purchase_order_p b on a.order_id=b.order_id
                                                           where a.order_status in ('1','2')
                                                         ) tb_order
                                                        left join 
                                                         (
                                                           select a.order_num,b.parts_code,b.business_counts from tb_parts_purchase_plan a 
                                                           inner join tb_parts_purchase_plan_p b on a.plan_id=b.plan_id {0}
                                                         ) tb_plan 
                                                         on tb_plan.order_num=tb_order.relation_order and tb_plan.parts_code=tb_order.parts_code 
                                                         where len(tb_plan.order_num)>0 and LEN(tb_plan.parts_code)>0
                                                         group by tb_plan.order_num,tb_plan.parts_code,tb_order.ImportOrderType
                                                         union
                                                         select 
                                                         tb_sale_order.order_num,tb_sale_order.parts_code,sum(tb_pur_order.business_counts) relation_count,tb_pur_order.ImportOrderType
                                                         from
                                                         (
                                                          select b.relation_order,b.parts_code,b.business_counts,b.ImportOrderType from tb_parts_purchase_order a 
                                                          inner join tb_parts_purchase_order_p b on a.order_id=b.order_id
                                                          where a.order_status in ('1','2')
                                                         ) tb_pur_order
                                                         left join 
                                                         (
                                                          select a.order_num,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                          inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id {0}
                                                         ) tb_sale_order 
                                                         on tb_sale_order.order_num=tb_pur_order.relation_order and tb_sale_order.parts_code=tb_pur_order.parts_code 
                                                         where len(tb_sale_order.order_num)>0 and LEN(tb_sale_order.parts_code)>0
                                                         group by tb_sale_order.order_num,tb_sale_order.parts_code,tb_pur_order.ImportOrderType
                                                    ) tb_pur_order_finishcount", files);
                return dt = DBHelper.GetTable("采购订单导入中，获取采购计划单或销售订单已完成的配件数量", TableName, FileName, "", "", "");
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
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
	                                                    select a.plan_id,b.order_num,a.parts_code,a.business_counts from
	                                                    (
		                                                     select * from tb_parts_purchase_plan_p where plan_id in
		                                                     (
			                                                     select plan_id from tb_parts_purchase_plan where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_purchase_order_p 
				                                                    where order_id='{0}' and len(relation_order)>0 group by relation_order
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
				                                                    select relation_order from tb_parts_purchase_order_p 
				                                                    where order_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_sale_order b on a.sale_order_id=b.sale_order_id
                                                    ) tb_pur_order_businesscount ", order_id);
                return dt = DBHelper.GetTable("查询采购订单导入采购计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
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
            try
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
                bool ret = DBHelper.BatchExeSQLStringMultiByTrans("提交采购订单，更新引用的采购计划单或销售订单的导入状态", listSql);
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
                            DBHelper.BatchExeSQLStringMultiByTrans("完成采购订单后，更新采购计划单的完成数量和完成金额", listSql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary> 提交成功时,对引用的前置单据的状态进行更新 
        /// </summary>
        /// <param name="orderid"></param>
        void SetOrderStatus(string orderid)
        {
            try
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
                            importtype = dr[0]["ImportOrderType"].ToString();

                            orderfinish_info = new OrderFinishInfo();
                            orderfinish_info.plan_id = plan_id;
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
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        } 
        #endregion

        #region --选择器获取数据后需执行的回调函数
        /// <summary> 供应商速查关联控件赋值
        /// </summary>
        /// <param name="dr"></param>
        private void Closing_UnitName_DataBack(DataRow dr)
        {
            if (dr.Table.Columns.Contains("sup_full_name"))
            {
                sup_id = dr["sup_id"].ToString();
                this.txtclosing_unit1.Text = dr["sup_full_name"].ToString();
                this.txtsup_name.Caption = dr["sup_full_name"].ToString();
                if (!string.IsNullOrEmpty(txtsup_code1.Text.Trim()))
                {
                    DataTable dt = DBHelper.GetTable("查询供应商档案信息", "tb_supplier", "*", " enable_flag != 0 and sup_code='" + txtsup_code1.Text.Trim() + "'", "", "");
                    if (dt.Rows.Count > 0)
                    {
                        txtfax.Caption = dt.Rows[0]["sup_fax"].ToString();
                        txtcontacts_tel.Caption = dt.Rows[0]["sup_tel"].ToString();

                        string TableName = string.Format(@"(
                                                    select cont_name from tb_contacts where cont_id=
                                                    (select cont_id from tr_base_contacts where relation_object_id='{0}' and is_default='1')
                                                    ) tb_conts", dt.Rows[0]["sup_id"].ToString());
                        DataTable dt_conts = DBHelper.GetTable("查询供应商默认的联系人信息", TableName, "*", "", "", "");
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
            }
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
