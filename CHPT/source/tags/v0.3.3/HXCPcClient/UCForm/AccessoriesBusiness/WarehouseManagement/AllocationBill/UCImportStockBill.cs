using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;
using System.Collections;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    public partial class UCImportStockBill : FormChooser
    {
        #region 变量、类
        private string BillType = string.Empty;//出入库单类型
        private string BillingType = string.Empty;//开单类型
        private string WareHouseName = string.Empty;//仓库名称
        private const string PrefixCaption = "导入";//主窗体标题前缀
        private string PurReceive = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseReceive, true);//采购收货
        private string PurExchange = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseExchange, true);//采购换货
        private string PurBack = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseBack, true);//采购退货
        private string ReceiveBill = DataSources.GetDescription(DataSources.EnumAllocationBillType.Storage, true);//入库
        private string OutBill = DataSources.GetDescription(DataSources.EnumAllocationBillType.OutboundOrder, true);//出库
        private string SaleBack = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleBack, true);//销售退货
        private string SaleExchange = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleExchange, true);//销售换货
        private string SaleSend = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleBill, true);//销售开单

        //开单类型
        private const string OtherReceiptBill = "其它收货单";
        private const string OtherSendBill = "其它发货单";
        private const string MaterialRequisition = "领料单";
        private const string RefundMaterial = "领料退货单";
        private const string AllotBill = "调拨单";
        private const string InventoryBill = "盘点单";
 
        private string RecBillLogMsg = "查询其它收货单列表信息";
        private string SenBillLogMsg = "查询其它发货单列表信息";
        private string AllotBillLogMsg = "查询调拨单列表信息";
        private string FetchBillLogMsg = "查询领料单列表信息";
        private string RefundBillLogMsg = "查询领料退货单列表信息";
        private string InvBillLogMsg = "查询盘点单列表信息";
        private string RecPartsLogMsg = "查询其它收货单配件表信息";
        private string SenPartsLogMsg = "查询其它发货单配件表信息";
        private string AllotPartsLogMsg = "查询调拨单配件表信息";
        private string FetchPartsLogMsg = "查询领料单配件表信息";
        private string RefundPartsLogMsg = "查询领料退货单配件表信息";
        private string InvPartsLogMsg = "查询盘点单配件表信息";
        private string DefaultValue = "请选择";
        private const string OpenState = "0";//单据开放状态
        private const string OccupyState = "1";//单据占用状态
        private const string LockState = "2";//单据锁定状态
        private const string NODelState = "1";//未删除标志
        private const string DelState = "0";//删除标志

        //开单信息数据库表
        private string RecBillTable = "tb_parts_stock_receipt";//其它收货单信息表
        private string ShipBillTable = "tb_parts_stock_shipping";//其它发货单信息表
        private string FetchBillTable = "tb_maintain_fetch_material";//领料单信息表
        private string RefundBillTable = "tb_maintain_refund_material";//领料退货单信息表
        private string AllotBillTable = "tb_parts_stock_allot_p";//领料单信息表
        private string InvBillTable = "tb_parts_stock_check_p";//盘点单信息表
        //开单配件信息数据库表
        private string RecPartTable = "tb_parts_stock_receipt_p";//其它收货单配件信息表
        private string ShipPartTable = "tb_parts_stock_shipping_p";//其它发货单配件信息表
        private string FetchPartTable = "tb_maintain_fetch_material_detai";//领料单配件信息表
        private string RefundPartTable = "tb_maintain_refund_material_detai";//领料退货单配件信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//领料单配件信息表
        private string InvPartTable = "tb_parts_stock_check_p";//盘点单配件信息表

        //各开单主键名称
        private const string stockreceiptid = "stock_receipt_id";//其它收货单表主键
        private const string stockshippingid = "stock_shipping_id";//其它发货单表主键
        private const string stockallotid = "stock_allot_id";//调拨单表主键
        private const string fetchid = "fetch_id";//领料单表主键
        private const string refundid = "refund_id";//领料退货单表主键
        private const string stockcheckid = "stock_check_id";//盘点单表主键
           
        //开单列表数据字段
        private const string ordertypename = "order_type_name";
        private const string ordernum = "order_num";
        private const string orderdate = "order_date";
        private const string balancemoney = "balance_money";
        private const string orgname = "org_name";
        private const string handlename = "handle_name";
        private const string operatorname = "operator_name";
        //配件列表数据字段
        private const string partscode = "parts_code";
        private const string partsname = "parts_name";
        private const string drawingnum = "drawing_num";
        private const string unitname = "unit_name";
        private const string originalprice = "original_price";
        private const string businessprice = "business_price";
        private const string businesscounts = "business_counts";
        private const string valoremtogether = "valorem_together";
        private const string storagecount = "storage_count";
        private const string arrivaldate = "arrival_date";
        private const string carpartscode = "car_parts_code";
        public int dgPlanRowIndex = -1;//GridView行索引初始值
        public List<string> StockBillIDLst = new List<string>();//存放库存开单主键ID与对应的开单编号
        public Hashtable PartHTable = new Hashtable();//存放开单主键ID与对应的配件编号

        #endregion

        #region 窗体初始化
        public UCImportStockBill(string Bill_Type,string Billing_Type)
        {
            InitializeComponent();
            this.Text = PrefixCaption + Billing_Type;//获取主窗体标题内容
            this.BillType = Bill_Type;//获取出入库单据类型
            this.BillingType = Billing_Type;//获取开单类型
            dateTimeStart.Value = DateTime.Now.ToShortDateString();//开始时间
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();//结束时间
            CommonFuncCall.BindUnit(unit);//从码表中获取单位名称


            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, DefaultValue);//部门名称
            CommonFuncCall.BindHandle(ddlhandle, "", DefaultValue);//经手人
            CommonFuncCall.BindHandle(ddloperator, "", DefaultValue);//操作人
  
        }
        public UCImportStockBill(string Bill_Type, string Billing_Type,string WareHouse)
        {
            InitializeComponent();
            this.Text = PrefixCaption + Billing_Type;//获取主窗体标题内容
            this.BillType = Bill_Type;//获取出入库单据类型
            this.BillingType = Billing_Type;//获取开单类型
            this.WareHouseName = WareHouse;//获取仓库名称
            dateTimeStart.Value = DateTime.Now.ToShortDateString();//开始时间
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();//结束时间
            CommonFuncCall.BindUnit(unit);//从码表中获取单位名称


            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, DefaultValue);//部门名称
            CommonFuncCall.BindHandle(ddlhandle, "", DefaultValue);//经手人
            CommonFuncCall.BindHandle(ddloperator, "", DefaultValue);//操作人

        }
        #endregion

        #region 查询条件的控件事件
        /// <summary> 
        /// 选择不同部门显示对应部门的经办人和操作人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), DefaultValue);//经办人
            CommonFuncCall.BindHandle(ddloperator, ddlorg_id.SelectedValue.ToString(), DefaultValue);//操作人
        }
        /// <summary> 
        /// 选择配件编码 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_code.Text = chooseParts.PartsCode;

            }
        }
        /// <summary>
        /// 选择配件名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_name_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_name.Text = chooseParts.PartsName;
            }
        }
        /// <summary> 
        /// 选择配件类型 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmPartsType choosePartsType = new frmPartsType();
            choosePartsType.ShowDialog();
            if (!string.IsNullOrEmpty(choosePartsType.TypeID))
            {
                txtparts_type.Text = choosePartsType.TypeName;
            }
        }
        /// <summary>
        /// 清空所有查询条件控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now.ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();
            txtorder_num.Caption = string.Empty;
            ddlorg_id.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
            ddloperator.SelectedIndex = 0;
            txtparts_code.Tag = string.Empty;
            txtparts_code.Text = string.Empty;

            txtparts_name.Tag = string.Empty;
            txtparts_name.Text = string.Empty;

            txtparts_type.Tag = string.Empty;
            txtparts_type.Text = string.Empty;
        }
        /// <summary> 
        /// 查询要导入的单据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string BillingNumber="order_num";//其它开单单号字段名
                string FetchNo="material_no";//领料单号字段名称
                string RefundNo="refund_no";//领料退货单号字段名称
                switch (BillingType)
                {

                    case OtherReceiptBill://绑定其它收货单信息列表
                        BinddgBilling(RecBillTable, stockreceiptid, BillingNumber,RecBillLogMsg);
                        break;
                    case OtherSendBill://绑定其它发货单信息列表
                        BinddgBilling(ShipBillTable,stockshippingid,BillingNumber,SenBillLogMsg);
                        break;
                    case AllotBill://绑定调拨单信息列表.
                        BinddgBilling(AllotBillTable, stockallotid, BillingNumber, AllotBillLogMsg);
                        break;
                    case MaterialRequisition://领料单
                        BinddgBilling(FetchBillTable, fetchid, FetchNo,FetchBillLogMsg);
                        break;
                    case RefundMaterial://领料退货单
                        BinddgBilling(RefundBillTable,refundid,RefundNo,RefundBillLogMsg);
                        break;
                    case InventoryBill://盘点单
                        BinddgBilling(InvBillTable, stockcheckid,BillingNumber,InvBillLogMsg);
                        break;

                }
               //根据条件查询开单信息列表

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 全选记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            bool Choice = true;
            IsDataGridViewCheckBox(Choice);
        }
        /// <summary>
        /// 反选记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotCheck_Click(object sender, EventArgs e)
        {
            bool Choice_Cancel = false;
            IsDataGridViewCheckBox(Choice_Cancel);
        }

        /// <summary>
        /// 开单与配件列表级联选择操作
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsDataGridViewCheckBox(bool ischeck)
        {
            try
            {
                dgBillList.EndEdit();//单据列表
                dgPartslist.EndEdit();//配件列表
                if (dgBillList.Rows.Count > 0)
                {
                    for (int i = 0; i < dgBillList.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)dgBillList.Rows[i].Cells["colCheck"]).Value = ischeck;//开单列表选择状态
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可以选择的" + BillingType + "记录行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (dgPartslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colDetailCheck"]).Value = ischeck;//配件列表选择状态
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可以选择的配件记录行项！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 确定导入配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {


                //判断获取开单列表中是否有未选中项，如果存在并移除
                if (StockBillIDLst.Count > 0)
                {
                    for (int i = 0; i < dgBillList.Rows.Count; i++)
                    {
                        //遍历开单列表中未选择记录行
                        string StockNumID = dgBillList.Rows[i].Cells["StockBillingId"].Value.ToString();
                        object IsCheck = dgBillList.Rows[i].Cells["colCheck"].EditedFormattedValue;
                        if (IsCheck != null && !(bool)IsCheck)
                        {
                            for (int j = 0; j < StockBillIDLst.Count;j++)
                            {
                                //遍历并移除未选择的开单主键ID和开单编号
                                if (StockBillIDLst[j].ToString() == StockNumID) StockBillIDLst.Remove(StockBillIDLst[j].ToString());

                            }
                        }
                    }

                }
                else
                {
                    MessageBoxEx.Show("没有选择要导入的" + BillingType,"提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

                //判断获取配件明细列表中是否有选中项，如果存在并移除
                if (dgPartslist.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgPartslist.Rows)
                    {
                        object isCheck = dr.Cells["colDetailCheck"].EditedFormattedValue;
                        if (isCheck != null && !(bool)isCheck)
                        {
                            foreach (DictionaryEntry DicEty in PartHTable)
                            {
                                if (DicEty.Key.ToString() == dr.Cells["parts_code"].Value.ToString())
                                {   //遍历移除未选择的配件记录行开单ID和对应配件编码
                                    PartHTable.Remove(DicEty.Key.ToString());
                                }

                            }
                        }
                    }

                    //判断配件列表中是否全选
                    if (PartHTable.Count == dgPartslist.Rows.Count)
                    {
                        PartHTable.Clear();//清除配件信息列表
                    }

                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { dgPlanRowIndex = -1; }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary> 
        /// 点击开单列表中记录行显示详细配件详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBillList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击表头或列头时不起作用
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                dgPlanRowIndex = e.RowIndex;
                string BillingNumID = dgBillList.CurrentRow.Cells["StockBillingId"].Value.ToString();//获取点击行开单主键ID
                string OrderType = dgBillList.CurrentRow.Cells["order_type_name"].Value.ToString();//获取单据类型名称
                BinddgPartsList(BillingNumID, BillType, OrderType);//查询对应开单中的配件信息表               
            }
        }
        /// <summary> 
        /// 通过开单记录行的选择状态设置配件记录行的选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBillList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击表头或列头时不起作用  
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                bool ischeck = (bool)((DataGridViewCheckBoxCell)dgBillList.Rows[dgPlanRowIndex].Cells["colCheck"]).EditedFormattedValue;//获取当前开单记录选择状态        
                dgPartslist.EndEdit();
                if (dgPartslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        //设置当前配件信息记录行选择状态
                        ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colCheck"]).Value = ischeck;
                    }
                }
            }
        }
        /// <summary> 
        /// 格式化内容信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPurchaseOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null)
                {
                    string ColumnName = dgBillList.Columns[e.ColumnIndex].Name.ToString(); //单据名称格式化
                    if (ColumnName.Equals("order_name"))
                    {
                        e.Value = OtherReceiptBill;
                    }
                    return;
                }
                string fieldNmae = dgBillList.Columns[e.ColumnIndex].DataPropertyName.ToString();// 开单日期格式化
                if (fieldNmae.Equals("order_date"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 通过配件信息记录行选择状态设置开单记录选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgAccessoriesDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    bool isChkis_Suspend = true;
                    //判断配件信息列表记录行选择状态
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        object ischeck = dgPartslist.Rows[i].Cells["colDetailCheck"].EditedFormattedValue;
                        if (ischeck != null && !(bool)ischeck)
                        {
                            isChkis_Suspend = false;
                            break;
                        }
                    }
                    ((DataGridViewCheckBoxCell)dgBillList.Rows[dgPlanRowIndex].Cells["colCheck"]).Value = isChkis_Suspend;//设置开单信息记录行选择状态
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 绑定开单列表
        /// </summary>
        private void BinddgBilling(string BillTable,string BillPrimaryID,string BillNumber,string QueryLogMsg)
        {
            try
            {

                DataTable gvBill_dt = null;
                dgBillList.Rows.Clear();//清空GridView中开单信息
                StockBillIDLst.Clear();//清除list开单ID信息
                //设置开单信息表中的数据查询字段
                StringBuilder sb_Bill = new StringBuilder();
                sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", BillPrimaryID,
                ordertypename, ordernum, orderdate, balancemoney, orgname, handlename, operatorname);
                if (BillType == ReceiveBill && BillingType == AllotBill)
                {
                    //获取调拨入库单信息列表
                    gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere() + " and call_in_wh_name='"+WareHouseName+"'", "", "");
                    GetBillLists(gvBill_dt);
                }
                else if (BillType == OutBill && BillingType == AllotBill)
                {
                    //获取调拨出库单信息列表
                    gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere() + " and call_out_wh_name='" + WareHouseName + "'", "", "");
                    GetBillLists(gvBill_dt);
                }
                else
                {
                    //获取开单信息列表
                    gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                    GetBillLists(gvBill_dt);
                }
                //将开单主键ID存放入list
                for (int i = 0; i < gvBill_dt.Rows.Count; i++)
                {
                    StockBillIDLst.Add(gvBill_dt.Rows[i][BillPrimaryID].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 绑定开单配件列表
        /// </summary>
        /// <param name="BillingId">开单编号字段名</param>
        /// <param name="BillingIdValue">开单编号字段值</param>
        /// <param name="BType">出入库单类型</param>
        /// <param name="SubOrderType">开单中的子单据类型名称</param>
        private void BinddgPartsList(string BillingIdValue, string BType, string SubOrderType)
        {
            try
            {

                string BillID = string.Empty;//开单主键ID
                if (SubOrderType == OtherReceiptBill) BillID = stockreceiptid;//其它收货单主键
                else if (SubOrderType == OtherSendBill) BillID = stockshippingid;//其它发货单主键
                else if (SubOrderType == AllotBill) BillID = stockallotid;//调拨单主键
                else if (SubOrderType == MaterialRequisition) BillID = fetchid;     //领料单主键
                else if (SubOrderType == RefundMaterial) BillID = refundid;     //领料退货单主键
                else if (SubOrderType == InventoryBill) BillID = stockallotid;//盘点单主键
                
                else if (SubOrderType == AllotBill) BillID = stockallotid;
                //设置配件列表数据字段
                StringBuilder sb_Part = new StringBuilder();
                sb_Part.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                BillID, partscode, partsname, drawingnum, unitname, originalprice,
                businessprice, businesscounts, valoremtogether, storagecount, arrivaldate, carpartscode);
                
                DataTable DtPartsBilling = null;//配件信息数据表
                dgPartslist.Rows.Clear();//清空所有配件信息
                PartHTable.Clear();//开单主键ID与配件编码

           
                //入库单
                if (!string.IsNullOrEmpty(BillingIdValue) && BType == ReceiveBill && SubOrderType == OtherReceiptBill)
                {
                    //获取其它收货入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(RecPartsLogMsg, RecPartTable, sb_Part.ToString(), stockreceiptid+"='" + BillingIdValue+"'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == ReceiveBill && SubOrderType == AllotBill)
                {
                    //获取调拨单入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(AllotPartsLogMsg, AllotPartTable, sb_Part.ToString(), stockallotid+"='" + BillingIdValue+"'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == ReceiveBill && SubOrderType == RefundMaterial)
                {
                    //获取领料退货单入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(RefundPartsLogMsg, RefundPartTable, sb_Part.ToString(), refundid+"='" + BillingIdValue+"'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == ReceiveBill && SubOrderType == InventoryBill)
                {
                    //获取盘点单盘亏入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(InvPartsLogMsg, InvPartTable, sb_Part.ToString(), stockcheckid + "='" + BillingIdValue + "'" + "and paper_count > firmoffer_count", "", "");
                }


                //出库单
                if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == OtherSendBill)
                {
                    //获取其它发货单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(SenPartsLogMsg, ShipPartTable, sb_Part.ToString(), stockshippingid + "='" + BillingIdValue+"'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == AllotBill)
                {
                    //获取调拨单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(AllotPartsLogMsg, AllotPartTable, sb_Part.ToString(), stockallotid + "='" + BillingIdValue+"'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == MaterialRequisition)
                {
                    //获取领料单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(FetchPartsLogMsg, FetchPartTable, sb_Part.ToString(), fetchid+"='" + BillingIdValue+"'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == InventoryBill)
                {
                    //获取盘点单盘盈出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(InvPartsLogMsg, InvPartTable, sb_Part.ToString(), stockcheckid + "='" + BillingIdValue + "'" + "and paper_count < firmoffer_count", "", "");
                }

                //配件信息显示在DataGridView控件中
                if (DtPartsBilling != null && DtPartsBilling.Rows.Count > 0)
                {
                    GetPartlists(DtPartsBilling);//把配件信息表绑定在GridView列表中

                    //把最后点击查询的开单配件信息主键ID与对应的配件编号放入Hashtable
                    foreach (DataRow DRow in DtPartsBilling.Rows)
                    {
                        switch(BillingType)
                        {
                            case OtherReceiptBill://其它收货
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["stock_receipt_id"].ToString());
                                break;
                            case OtherSendBill://其它发货
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["stock_shipping_id"].ToString());
                                break;
                            case AllotBill://调拨单
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["stock_allot_id"].ToString());
                                break;
                            case MaterialRequisition://领料单
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["fetch_id"].ToString());
                                break;
                            case RefundMaterial://领料退货单
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["refund_id"].ToString());
                                break;
                            case InventoryBill://盘点单
                                PartHTable.Add(DRow["parts_code"].ToString(), DRow["stock_check_id"].ToString());
                                break;
    
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 批量导入开单配件列表信息
        /// </summary>
        /// <param name="partsTable">数据库中获取的配件信息表</param>
        private void GetPartlists(DataTable partsTable)
        {
            try
            {
                int id = 1;
                for (int i = 0; i < partsTable.Rows.Count; i++)
                {
                   
                        DataGridViewRow dgRow = dgPartslist.Rows[dgPartslist.Rows.Add()];//添加新行项
                        dgRow.Cells["PartID"].Value = id;
                        dgRow.Cells["parts_code"].Value = partsTable.Rows[i]["parts_code"].ToString();
                        dgRow.Cells["parts_name"].Value = partsTable.Rows[i]["parts_name"].ToString();
                        dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["original_price"].Value = partsTable.Rows[i]["original_price"].ToString();
                        dgRow.Cells["business_price"].Value = partsTable.Rows[i]["business_price"].ToString();
                        dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["business_counts"].ToString();
                        dgRow.Cells["valorem_together"].Value = partsTable.Rows[i]["valorem_together"].ToString();
                        dgRow.Cells["valorem_together"].Value = partsTable.Rows[i]["storage_count"].ToString();
                        dgRow.Cells["return_bus_count"].Value = partsTable.Rows[i]["return_bus_count"].ToString();
                        dgRow.Cells["arrival_date"].Value = partsTable.Rows[i]["arrival_date"].ToString();
                        dgRow.Cells["car_parts_code"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                        id++;//序号自动增加

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        
        /// <summary>
        /// 查询开单信息列表
        /// </summary>
        /// <param name="BillDataTable"></param>
        private void GetBillLists(DataTable BillDataTable)
        {
            try
            {
                if (BillDataTable.Rows.Count == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的开单信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int BillingID = 1;
                for (int i = 0; i < BillDataTable.Rows.Count; i++)
                {
                    
                         DataGridViewRow dgRow = dgBillList.Rows[dgBillList.Rows.Add()];//创建新行项
                         dgRow.Cells["BillID"].Value = BillingID;//序号
                        switch(BillingType)
                        {
                            case OtherReceiptBill:
                                dgRow.Cells["OrderName"].Value = OtherReceiptBill;//其它收货单
                                break;
                            case OtherSendBill:
                                dgRow.Cells["OrderName"].Value = OtherSendBill;//其它发货单
                                break;
                            case MaterialRequisition:
                                dgRow.Cells["OrderName"].Value = MaterialRequisition;//领料单
                                break;
                            case RefundMaterial:
                                dgRow.Cells["OrderName"].Value = RefundMaterial;//领料退货单
                                break;
                            case AllotBill:
                                dgRow.Cells["OrderName"].Value = AllotBill;//调拨单
                                break;
                            case InventoryBill:
                                dgRow.Cells["OrderName"].Value = InventoryBill;//盘点单
                                break;

                        }
                        dgRow.Cells["order_type_name"].Value = BillDataTable.Rows[i]["order_type_name"].ToString();
                        dgRow.Cells["order_num"].Value = BillDataTable.Rows[i]["order_num"].ToString();
                        dgRow.Cells["order_date"].Value = BillDataTable.Rows[i]["order_date"].ToString();
                        dgRow.Cells["balance_money"].Value = BillDataTable.Rows[i]["balance_money"].ToString();
                        dgRow.Cells["org_name"].Value = BillDataTable.Rows[i]["org_name"].ToString();
                        dgRow.Cells["handle_name"].Value = BillDataTable.Rows[i]["handle_name"].ToString();
                        dgRow.Cells["operator_name"].Value = BillDataTable.Rows[i]["operator_name"].ToString();
                        BillingID++;//序号自动增长

                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        private string BuildQueryWhere()
        {
            try
            {
                //开单可以导入的前提是：单据状态是已审核、导入状态是正常的 三种情况
                //获取出入库单与开单类型信息
                string StrWhere = " enable_flag='" + OpenState + "'" + " and is_occupy='" + NODelState + "'";//查询条件字符串
  

                //获取导入单据的查询条件信息

                if (!string.IsNullOrEmpty(dateTimeStart.Value.Trim().ToString()))
                {
                    StrWhere += " and order_date>='" + dateTimeStart.Value.Trim().ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(dateTimeEnd.Value.Trim().ToString()))
                {
                    StrWhere += " and order_date<='" + dateTimeEnd.Value.Trim().ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim().ToString()))
                {
                    StrWhere += " and order_num='" + txtorder_num.Caption.Trim().ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
                {
                    StrWhere += " and handle_name='" + ddlhandle.SelectedValue.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ddloperator.SelectedValue.ToString()))
                {
                    StrWhere += "and operator_name='" + ddloperator.SelectedValue.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_code.Text.Trim().ToString()))
                {
                    StrWhere += "and operator_name='" + ddloperator.SelectedValue.ToString() + "'";
                }

                return StrWhere;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }
}
