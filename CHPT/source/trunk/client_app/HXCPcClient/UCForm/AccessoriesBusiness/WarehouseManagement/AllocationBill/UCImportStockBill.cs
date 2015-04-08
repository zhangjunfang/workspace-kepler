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
        private const string PrefixCaption = "导入";//主窗体标题前缀
        private string PurReceive = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseReceive, true);//采购收货
        private string PurExchange = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseExchange, true);//采购换货
        private string PurBack = DataSources.GetDescription(DataSources.EnumPurchaseOrderType.PurchaseBack, true);//采购退货
        private string InBill = DataSources.GetDescription(DataSources.EnumAllocationBillType.Storage, true);//入库
        private string OutBill = DataSources.GetDescription(DataSources.EnumAllocationBillType.OutboundOrder, true);//出库
        private string SaleBack = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleBack, true);//销售退货
        private string SaleExchange = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleExchange, true);//销售换货
        private string SaleSend = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleBill, true);//销售开单
        private string Verified = "2";//单据已审核状态标志
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
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单信息表
        private string InvBillTable = "tb_parts_stock_check";//盘点单信息表
        //开单配件信息数据库表
        private string RecPartTable = "tb_parts_stock_receipt_p";//其它收货单配件信息表
        private string ShipPartTable = "tb_parts_stock_shipping_p";//其它发货单配件信息表
        private string FetchPartTable = "tb_maintain_fetch_material_detai";//领料单配件信息表
        private string RefundPartTable = "tb_maintain_refund_material_detai";//领料退货单配件信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件信息表
        private string InvPartTable = "tb_parts_stock_check_p";//盘点单配件信息表
        private string PartsTable = "tb_parts";//配件档案表
        //各开单主键名称
        private const string stockreceiptid = "stock_receipt_id";//其它收货单表主键
        private const string stockshippingid = "stock_shipping_id";//其它发货单表主键
        private const string stockallotid = "stock_allot_id";//调拨单表主键
        private const string fetchid = "fetch_id";//领料单表主键
        private const string refundid = "refund_id";//领料退货单表主键
        private const string stockcheckid = "stock_check_id";//盘点单表主键
           
        //开单列表数据字段
        private const string MaterialNo = "material_num";
        private const string RefundNo = "refund_no";
        private const string ordertypename = "order_type_name";
        private const string ordernum = "order_num";
        private const string orderdate = "order_date";
        private const string orgname = "org_name";
        private const string handlename = "handle_name";
        private const string operatorname = "operator_name";
        private const string rmk = "remark";
        //配件列表数据字段
        private const string partsercode = "ser_parts_code";
        private const string partscode = "parts_code";
        private const string partmodel = "model";
        private const string partsname = "parts_name";
        private const string drawingnum = "drawing_num";
        private const string unitname = "unit_name";
        private const string businessprice = "price";
        private const string businesscounts = "counts";
        private const string carpartscode = "car_parts_code";
        private const string amtmoney = "money";
        public int dgPlanRowIndex = -1;//GridView行索引初始值
        public Dictionary<string,string> StockBillIDDic = new Dictionary<string,string>();//存放库存开单主键ID与对应的开单编号
        public Hashtable PartHTable = new Hashtable();//存放开单主键ID与对应的配件编号
        public string ReceiveShipFields = string.Empty;//其它收发货单配件字段列表
        public string FetchRefundFields=string.Empty;//领料单/退货单配件字段列表
        public string AllotFields = string.Empty;//调拨单配件字段列表
        public string InventoryFields = string.Empty;//盘点单配件字段列表
        #endregion

        #region 窗体初始化
        public UCImportStockBill(string Bill_Type,string Billing_Type)
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            this.Text = PrefixCaption + Billing_Type;//获取主窗体标题内容
            this.BillType = Bill_Type;//获取出入库单据类型
            this.BillingType = Billing_Type;//获取开单类型
            dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToString();//开始时间
            dateTimeEnd.Value = DateTime.Now.ToString();//结束时间
            if (Billing_Type == InventoryBill) business_counts.HeaderText = "盈亏数量";//盘点单
            if (Billing_Type == AllotBill) business_price.HeaderText = "调入单价";//调拨单

            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, DefaultValue);//部门名称
            CommonFuncCall.BindHandle(ddlhandle, "", DefaultValue);//经手人
            CommonFuncCall.BindHandle(ddloperator, "", DefaultValue);//操作人
            GetStockBillResult();//获取要导入单据查询结果
         }



        private void UCImportStockBill_Load(object sender, EventArgs e)
        {
            try
            {
                string BillID = string.Empty;//开单主键ID
                if (BillingType == OtherReceiptBill) BillID = stockreceiptid;//其它收货单主键
                else if (BillingType == OtherSendBill) BillID = stockshippingid;//其它发货单主键
                else if (BillingType == AllotBill) BillID = stockallotid;//调拨单主键
                else if (BillingType == MaterialRequisition) BillID = fetchid; //领料单主键
                else if (BillingType == RefundMaterial) BillID = refundid;     //领料退货单主键
                else if (BillingType == InventoryBill) BillID = stockallotid;//盘点单主键        
                else if (BillingType == AllotBill) BillID = stockallotid;//调拨单
                //设置单据列表
                dgBillList.ReadOnly = false;
                foreach (DataGridViewColumn dgCol in dgBillList.Columns)
                {
                    if (dgCol.Name != colCheck.Name) dgCol.ReadOnly = true;
                }
                //设置配件列表
                dgPartslist.ReadOnly = false;
                foreach (DataGridViewColumn dgCol in dgPartslist.Columns)
                {
                    if (dgCol.Name != colDetailCheck.Name) dgCol.ReadOnly = true;
                }
                //设置配件列表数据字段
                //其它收发货单字段列表
                ReceiveShipFields =BillID + "," + partscode + "," + partsname + "," + drawingnum + "," + partmodel +
                "," + carpartscode + "," + unitname + "," + businessprice + "," + businesscounts + "," + amtmoney;
                //领料单/退料单字段列表
                FetchRefundFields =BillID + "," + partscode + "," + partsname +
                ",drawn_no,norms,unit,unit_price,quantity,sum_money";
                //调拨单字段列表
                AllotFields += BillID + "," + partscode + "," + partsname + "," + drawingnum + "," + partmodel +
               "," + carpartscode + "," + unitname + ",call_in_price," + businesscounts + "," + amtmoney;
                //盘点单字段列表
                InventoryFields = BillID + "," + partscode + "," + partsname + "," + drawingnum + "," + partmodel +
                "," + carpartscode + "," + unitname + ",business_price,profitloss_count," + amtmoney;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
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
            GetStockBillResult();//获取要导入单据查询结果

        }
        /// <summary>
        /// 获取要导入单据查询结果
        /// </summary>
        private void GetStockBillResult()
        {

            try
            {
                string BillingNumber = "order_num";//其它开单单号字段名
                string FetchNo = "material_no";//领料单号字段名称
                string RefundNo = "refund_no";//领料退货单号字段名称
                switch (BillingType)
                {

                    case OtherReceiptBill://绑定其它收货单信息列表
                        BinddgBilling(RecBillTable, stockreceiptid, BillingNumber, RecBillLogMsg);
                        break;
                    case OtherSendBill://绑定其它发货单信息列表
                        BinddgBilling(ShipBillTable, stockshippingid, BillingNumber, SenBillLogMsg);
                        break;
                    case AllotBill://绑定调拨单信息列表.
                        BinddgBilling(AllotBillTable, stockallotid, BillingNumber, AllotBillLogMsg);
                        break;
                    case MaterialRequisition://领料单
                        BinddgBilling(FetchBillTable, fetchid, FetchNo, FetchBillLogMsg);
                        break;
                    case RefundMaterial://领料退货单
                        BinddgBilling(RefundBillTable, refundid, RefundNo, RefundBillLogMsg);
                        break;
                    case InventoryBill://盘点单
                        BinddgBilling(InvBillTable, stockcheckid, BillingNumber, InvBillLogMsg);
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
                    return;
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
                    return;
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
                if (StockBillIDDic.Count > 0)
                {
                    List<string> BillIdList = new List<string>(StockBillIDDic.Keys);
                    for (int i = 0; i < dgBillList.Rows.Count; i++)
                    {
                        //遍历开单列表中未选择记录行
                        string StockNumID = dgBillList.Rows[i].Cells["StockBillingId"].Value.ToString();
                        object IsCheck = dgBillList.Rows[i].Cells["colCheck"].EditedFormattedValue;
                        if (IsCheck != null && !(bool)IsCheck)
                        {
                            for (int j = 0; j < BillIdList.Count; j++)
                            {
                                //遍历并移除未选择的开单主键ID和开单编号
                                if (BillIdList[j].ToString() == StockNumID) StockBillIDDic.Remove(BillIdList[j].ToString());

                            }
                        }
                    }

                }
                else
                {
                    MessageBoxEx.Show("没有选择要导入的" + BillingType,"提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                //判断获取配件明细列表中是否有选中项，如果存在并移除
                if (dgPartslist.Rows.Count > 0)
                {
                    ArrayList PartNumList = new ArrayList(PartHTable.Keys);//存放所有hashtable键名
                    foreach (DataGridViewRow dr in dgPartslist.Rows)
                    {
                        object isCheck = dr.Cells["colDetailCheck"].EditedFormattedValue;
                        if (isCheck != null && !(bool)isCheck)
                        {
                            for (int k = 0; k < PartNumList.Count;k++ )
                            {
                                if (PartNumList[k].ToString() == dr.Cells["parts_code"].Value.ToString())
                                {   //遍历移除未选择的配件记录行开单ID和对应配件编码
                                    PartHTable.Remove(PartNumList[k].ToString());
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
                BinddgPartsList(BillingNumID, BillType, BillingType);//查询对应开单中的配件信息表               
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
                if (dgPartslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        //设置当前配件信息记录行选择状态
                        ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colDetailCheck"]).Value = ischeck;
                    }
                }
            }
        }
        /// <summary>
        /// 通过配件信息记录行选择状态设置开单记录选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPartslist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    //判断配件信息列表记录行选择状态
                    bool ischeck = (bool)((DataGridViewCheckBoxCell)dgPartslist.CurrentRow.Cells["colDetailCheck"]).EditedFormattedValue;
                    string PtBillID = dgPartslist.CurrentRow.Cells["Stockpartbillingid"].Value.ToString();//获取配件选择记录行的单据主键ID
                    List<string> CheckRows = new List<string>();//存储未选中配件记录行项
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        if (!(bool)dgPartslist.Rows[i].Cells["colDetailCheck"].EditedFormattedValue)
                        {
                            CheckRows.Add(dgPartslist.Rows[i].Cells["parts_code"].Value.ToString());
                        }
                    }

                    //判断当前单据列表中的选择状态
                    foreach (DataGridViewRow dgRow in dgBillList.Rows)
                    {

                        if (dgRow.Cells["StockBillingId"].Value.ToString() == PtBillID && CheckRows.Count == 0)
                        {

                            ((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).Value = true ;//设置开单信息记录行选择状态

                      
                        }
                        else if (dgRow.Cells["StockBillingId"].Value.ToString() == PtBillID && CheckRows.Count != 0)
                        {

                            ((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).Value = false;//设置开单信息记录行选择状态

                        }

                    }

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
                DataTable gvBillID_dt = null;
                dgBillList.Rows.Clear();//清空GridView中开单信息
                StockBillIDDic.Clear();//清除list开单ID信息
                //设置开单信息表中的数据查询字段
                StringBuilder sb_Bill = new StringBuilder();
                string RelationTable = string.Empty;//关联查询
                string DefaultWhere=BillPrimaryID + " is not null ";
                string StrWhere = DefaultWhere;//查询条件
                if (!string.IsNullOrEmpty(txtparts_code.Text.ToString()))
                {
                    StrWhere += " and parts_code='" + txtparts_code.Text.Trim().ToString() + "'";
                }
                if (!string.IsNullOrEmpty(txtparts_name.Text.ToString()))
                {
                    StrWhere += " and parts_name='" + txtparts_code.Text.Trim().ToString() + "'";
                }
                if (!string.IsNullOrEmpty(txtparts_type.Text.ToString()))
                {
                    StrWhere += " and parts_type='" + txtparts_code.Text.Trim().ToString() + "'";
                }
                if (BillType == InBill && BillingType == AllotBill)
                {//调拨单入库
                    sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    { //获取根据配件条件查询到的调拨单主键ID
                        RelationTable = AllotPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_allot_p  right join tb_parts ON tb_parts_stock_allot_p.parts_code = dbo.tb_parts.ser_parts_code) 
                        AS NumTypeTable ON tb_parts_stock_allot_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_allot_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere() 
                            + "'" + " and stock_allot_id='" + gvBillID_dt.Rows[i]["stock_allot_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        //获取调拨入库单信息列表
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere() , "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
              
                }
                else if (BillType == OutBill && BillingType == AllotBill)
                {//调拨单出库
                    sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的调拨单主键ID
                        RelationTable = AllotPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_allot_p  right join tb_parts ON tb_parts_stock_allot_p.parts_code = dbo.tb_parts.ser_parts_code) 
                        AS NumTypeTable ON tb_parts_stock_allot_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_allot_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere()
                            + "'" + " and stock_allot_id='" + gvBillID_dt.Rows[i]["stock_allot_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }

                    }
                    else
                    {
                        //获取调拨出库单信息列表
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
                }
                else if (BillType == OutBill && BillingType == MaterialRequisition)
                {//领料单出库
                    //获取开单信息列表
                    string FetchFields = "tb_maintain_fetch_material.fetch_id,material_num," +
                    "fetch_time,org_name,create_name,responsible_name,tb_maintain_fetch_material.remarks";
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的领料单主键ID
                        RelationTable = FetchPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_maintain_fetch_material_detai right join tb_parts ON tb_maintain_fetch_material_detai.material_num = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_maintain_fetch_material_detai.material_num = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT fetch_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, FetchFields,
                            BuildQueryWhere() + "'" + " and fetch_id='" + gvBillID_dt.Rows[i]["fetch_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, FetchFields,BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
                }
                else if (BillType == InBill && BillingType == RefundMaterial)
                {//退料单入库
                    //获取开单信息列表
                    string RefundFields = "tb_maintain_refund_material.refund_id,refund_no," +
                    "refund_time,org_name,create_name,responsible_name,tb_maintain_refund_material.remarks";
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的领料退货单主键ID
                        RelationTable = RefundPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_maintain_refund_material_detai right join tb_parts ON tb_maintain_refund_material_detai.refund_no = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_maintain_refund_material_detai.refund_no = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT refund_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, RefundFields,
                            BuildQueryWhere() + "'" + " and refund_id='" + gvBillID_dt.Rows[i]["refund_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, RefundFields, BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
                   

                }
                else if (BillType == InBill && BillingType == InventoryBill)
                {//盘点单盘亏入库
                    //获取开单信息列表
                    sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的盘点单主键ID
                        RelationTable = InvPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_check_p right join tb_parts ON tb_parts_stock_check_p.parts_code = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_parts_stock_check_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_check_id",
                        StrWhere + " and  profitloss_count<0", "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere() + "'" + " and stock_check_id='" + gvBillID_dt.Rows[i]["stock_check_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
                }
                else if (BillType == OutBill && BillingType == InventoryBill)
                {   //盘点单盘盈出库
                    //获取开单信息列表
                    sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的盘点单主键ID
                        RelationTable = InvPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_check_p right join tb_parts ON tb_parts_stock_check_p.parts_code = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_parts_stock_check_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_check_id",
                        StrWhere + " and  profitloss_count>0", "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere() + "'" + " and stock_check_id='" + gvBillID_dt.Rows[i]["stock_check_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }

                }
                else if (BillType == InBill && BillingType == OtherReceiptBill)
                {   //其它收货单入库
                    //获取开单信息列表
                    sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的其它收货单主键ID
                        RelationTable = RecPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_receipt_p right join tb_parts ON tb_parts_stock_receipt_p.parts_code = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_parts_stock_receipt_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_receipt_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere() + "'" + " and stock_receipt_id='" + gvBillID_dt.Rows[i]["stock_receipt_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }

                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }
                    
                    
                }
                else if (BillType == OutBill && BillingType == OtherSendBill)
                {//其它发货单出库
                    //获取开单信息列表
                    sb_Bill.AppendFormat("tb_parts_stock_shipping.{0},{1},{2},{3},{4},{5},tb_parts_stock_shipping.{6}", BillPrimaryID,
                    ordernum, orderdate, orgname, handlename, operatorname, rmk);//查询字段
                    if (StrWhere != DefaultWhere)
                    {   //获取根据配件条件查询到的其它发货单主键ID
                        RelationTable = ShipPartTable + " right join " + @"(SELECT  tb_parts.ser_parts_code,tb_parts.parts_type
                        FROM tb_parts_stock_shipping_p right join tb_parts ON tb_parts_stock_shipping_p.parts_code = dbo.tb_parts.ser_parts_code)
                        AS NumTypeTable ON tb_parts_stock_shipping_p.parts_code = NumTypeTable.ser_parts_code";//关联查询表
                        gvBillID_dt = DBHelper.GetTable(QueryLogMsg, RelationTable, "DISTINCT stock_shipping_id",
                        StrWhere, "", "");
                        for (int i = 0; i < gvBillID_dt.Rows.Count; i++)
                        {
                            gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(),
                            BuildQueryWhere() + "'" + " and stock_shipping_id='" + gvBillID_dt.Rows[i]["stock_shipping_id"] + "'", "", "");
                            if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                            GetBillLists(gvBill_dt);//显示查询结果
                        }
                    }
                    else
                    {
                        gvBill_dt = DBHelper.GetTable(QueryLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                        if (gvBill_dt.Rows.Count == 0) return;//如果未查询到数据返回
                        GetBillLists(gvBill_dt);//显示查询结果
                    }

                }
                //将开单主键ID和单据编号存放入字典
                for (int i = 0; i < gvBill_dt.Rows.Count; i++)
                {
                    if (BillingType == MaterialRequisition)
                    {
                        StockBillIDDic.Add(gvBill_dt.Rows[i][BillPrimaryID].ToString(), gvBill_dt.Rows[i][MaterialNo].ToString());
                    }
                    else if(BillingType == RefundMaterial)
                    {
                        StockBillIDDic.Add(gvBill_dt.Rows[i][BillPrimaryID].ToString(), gvBill_dt.Rows[i][RefundNo].ToString());
                    }
                    else 
                    {
                     
                        StockBillIDDic.Add(gvBill_dt.Rows[i][BillPrimaryID].ToString(), gvBill_dt.Rows[i][ordernum].ToString());
                    
                    }
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

                DataTable DtPartsBilling = null;//配件信息数据表
                dgPartslist.Rows.Clear();//清空所有配件信息
                PartHTable.Clear();//开单主键ID与配件编码

                //入库单
                if (!string.IsNullOrEmpty(BillingIdValue) && BType == InBill && SubOrderType == OtherReceiptBill)
                {
                    //获取其它收货入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(RecPartsLogMsg, RecPartTable, ReceiveShipFields, stockreceiptid + "='" + BillingIdValue + "'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == InBill && SubOrderType == AllotBill)
                {
                    //获取调拨单入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(AllotPartsLogMsg, AllotPartTable, AllotFields, stockallotid + "='" + BillingIdValue + "'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == InBill && SubOrderType == RefundMaterial)
                {
                    //获取领料退货单入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(RefundPartsLogMsg, RefundPartTable, FetchRefundFields, refundid + "='" + BillingIdValue + "'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == InBill && SubOrderType == InventoryBill)
                {
                    //获取盘点单盘亏入库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(InvPartsLogMsg, InvPartTable, InventoryFields, stockcheckid + "='" + BillingIdValue + "'" + "and paper_count > firmoffer_count", "", "");
                }


                //出库单
                if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == OtherSendBill)
                {
                    //获取其它发货单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(SenPartsLogMsg, ShipPartTable, FetchRefundFields.ToString(), stockshippingid + "='" + BillingIdValue + "'", "", "");

                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == AllotBill)
                {
                    //获取调拨单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(AllotPartsLogMsg, AllotPartTable, AllotFields.ToString(), stockallotid + "='" + BillingIdValue + "'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == MaterialRequisition)
                {
                    //获取领料单出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(FetchPartsLogMsg, FetchPartTable,FetchRefundFields, fetchid + "='" + BillingIdValue + "'", "", "");
                }
                else if (!string.IsNullOrEmpty(BillingIdValue) && BType == OutBill && SubOrderType == InventoryBill)
                {
                    //获取盘点单盘盈出库配件信息列表
                    DtPartsBilling = DBHelper.GetTable(InvPartsLogMsg, InvPartTable, InventoryFields, stockcheckid + "='" + BillingIdValue + "'" + "and paper_count < firmoffer_count", "", "");
                }
                //配件信息显示在DataGridView控件中
                //把配件信息表绑定在GridView列表中
                if (DtPartsBilling != null && DtPartsBilling.Rows.Count > 0) GetPartlists(DtPartsBilling);
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
                for (int i = 0; i < partsTable.Rows.Count; i++)
                {
                    DataGridViewRow dgRow = dgPartslist.Rows[dgPartslist.Rows.Add()];//GridView中添加新行项
                    dgRow.Cells["parts_code"].Value = partsTable.Rows[i]["parts_code"].ToString();//配件编码
                    dgRow.Cells["parts_name"].Value = partsTable.Rows[i]["parts_name"].ToString();//配件名称
                    //把最后点击查询的开单配件信息主键ID与对应的配件编号放入Hashtable
                    switch (BillingType)
                    {
                        case OtherReceiptBill://其它收货
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["stock_receipt_id"].ToString());
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["stock_receipt_id"].ToString();
                            dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["model"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit_name"].ToString();                   
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["price"].ToString();//业务单价
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["counts"].ToString();//数量
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["money"].ToString();//金额
                            break;
                        case OtherSendBill://其它发货
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["stock_shipping_id"].ToString());
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["stock_shipping_id"].ToString();
                            dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["model"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit_name"].ToString();                   
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["price"].ToString();//业务单价
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["counts"].ToString();//数量
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["money"].ToString();//金额
                            break;
                        case AllotBill://调拨单
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["stock_allot_id"].ToString());
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["stock_allot_id"].ToString();
                            dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["model"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit_name"].ToString();     
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["call_in_price"].ToString();//调入单价
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["counts"].ToString();//数量
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["money"].ToString();//金额
                            break;
                        case MaterialRequisition://领料单
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["fetch_id"].ToString());
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["fetch_id"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawn_no"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["norms"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit"].ToString();
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["unit_price"].ToString();
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["quantity"].ToString();
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["sum_money"].ToString();//金额
                            break;
                        case RefundMaterial://领料退货单
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["refund_id"].ToString());
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["refund_id"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawn_no"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["norms"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit"].ToString();
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["unit_price"].ToString();
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["quantity"].ToString();
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["sum_money"].ToString();//金额
                            break;
                        case InventoryBill://盘点单
                            PartHTable.Add(partsTable.Rows[i]["parts_code"].ToString(), partsTable.Rows[i]["stock_check_id"].ToString());//把配件编码与单据ID
                            dgRow.Cells["Stockpartbillingid"].Value = partsTable.Rows[i]["stock_check_id"].ToString();
                            dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                            dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                            dgRow.Cells["PartSpecs"].Value = partsTable.Rows[i]["model"].ToString();
                            dgRow.Cells["unit"].Value = partsTable.Rows[i]["unit_name"].ToString();                   
                            dgRow.Cells["business_price"].Value = partsTable.Rows[i]["business_price"].ToString();//业务单价
                            dgRow.Cells["business_counts"].Value = partsTable.Rows[i]["profitloss_count"].ToString();//盈亏数量
                            dgRow.Cells["total_money"].Value = partsTable.Rows[i]["money"].ToString();//金额
                            break;

                    }
               
                    //dgRow.Cells["ReferenCount"].Value = partsTable.Rows[i]["storage_count"].ToString();//引用数量
                    //dgRow.Cells["RemainCount"].Value = partsTable.Rows[i]["return_bus_count"].ToString();//剩余数量


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
                if (BillDataTable.Rows.Count == 0) return;
                for (int i = 0; i < BillDataTable.Rows.Count; i++)
                {
                    
                        DataGridViewRow dgRow = dgBillList.Rows[dgBillList.Rows.Add()];//创建新行项
                        switch(BillingType)
                        {
                            case OtherReceiptBill:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["stock_receipt_id"].ToString();//其它收货单主键ID
                                dgRow.Cells["OrderName"].Value = OtherReceiptBill;//其它收货单
                                break;
                            case OtherSendBill:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["stock_shipping_id"].ToString();//其它发货单主键ID
                                dgRow.Cells["OrderName"].Value = OtherSendBill;//其它发货单
                                break;
                            case MaterialRequisition:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["fetch_id"].ToString();//领料单主键ID
                                dgRow.Cells["OrderName"].Value = MaterialRequisition;//领料单
                                break;
                            case RefundMaterial:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["refund_id"].ToString();//领料退货单主键ID
                                dgRow.Cells["OrderName"].Value = RefundMaterial;//领料退货单
                                break;
                            case AllotBill:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["stock_allot_id"].ToString();//调拨单主键ID
                                dgRow.Cells["OrderName"].Value = AllotBill;//调拨单
                                break;
                            case InventoryBill:
                                dgRow.Cells["StockBillingId"].Value = BillDataTable.Rows[i]["stock_check_id"].ToString();//盘点单主键ID
                                dgRow.Cells["OrderName"].Value = InventoryBill;//盘点单
                                break;

                        }
                        if (BillingType == MaterialRequisition)
                        {
                            
                            dgRow.Cells["order_num"].Value = BillDataTable.Rows[i]["material_num"].ToString();
                            long FetchDate = (long)BillDataTable.Rows[i]["fetch_time"];
                            dgRow.Cells["order_date"].Value =Common.UtcLongToLocalDateTime(FetchDate).ToShortDateString();
                            dgRow.Cells["handle_name"].Value = BillDataTable.Rows[i]["responsible_name"].ToString();
                            dgRow.Cells["operator_name"].Value = BillDataTable.Rows[i]["create_name"].ToString();
                            dgRow.Cells["Remarks"].Value = BillDataTable.Rows[i]["remarks"].ToString();
                        }
                        else if (BillingType == RefundMaterial)
                        {
                            
                            dgRow.Cells["order_num"].Value = BillDataTable.Rows[i]["refund_no"].ToString();
                            long RefundDate=(long)BillDataTable.Rows[i]["refund_time"];
                            dgRow.Cells["order_date"].Value = Common.UtcLongToLocalDateTime(RefundDate).ToShortDateString();
                            dgRow.Cells["handle_name"].Value = BillDataTable.Rows[i]["responsible_name"].ToString();
                            dgRow.Cells["operator_name"].Value = BillDataTable.Rows[i]["create_name"].ToString();
                            dgRow.Cells["Remarks"].Value = BillDataTable.Rows[i]["remarks"].ToString();
                        }
                        else
                        {
                            dgRow.Cells["order_num"].Value = BillDataTable.Rows[i]["order_num"].ToString();
                            long OrderDate=(long)BillDataTable.Rows[i]["order_date"];
                            dgRow.Cells["order_date"].Value = Common.UtcLongToLocalDateTime(OrderDate).ToShortDateString();
                            dgRow.Cells["handle_name"].Value = BillDataTable.Rows[i]["handle_name"].ToString();
                            dgRow.Cells["operator_name"].Value = BillDataTable.Rows[i]["operator_name"].ToString();
                            dgRow.Cells["Remarks"].Value = BillDataTable.Rows[i]["remark"].ToString();
                        }                        
                        dgRow.Cells["org_name"].Value = BillDataTable.Rows[i]["org_name"].ToString();
                       


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
                string StrWhere = " enable_flag='" + NODelState + "'" + " and is_occupy='" + OpenState + "'" + " and is_lock='" + OpenState + "'";
                    if (BillingType == MaterialRequisition || BillingType == RefundMaterial)
                    {
                        StrWhere +=  " and info_status='" + Verified + "'";//查询单据审核状态
                    }
                    else
                    {
                        StrWhere += " and order_status_name='"+ Verified + "'";//查询单据审核状态
                    }
                    string StDate = dateTimeStart.Value.ToString();
                    string EdDate = dateTimeEnd.Value.ToString();
                    DateTime StartTime = Convert.ToDateTime(StDate );//开始时间
                    DateTime EndTime = Convert.ToDateTime(EdDate );//结束时间
                      //获取导入单据的查询条件信息
                  if (StDate == EdDate)
                 {
                     if (BillingType == MaterialRequisition)
                     {
                         StrWhere += " and fetch_time='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                     }
                     else if (BillingType == RefundMaterial)
                     {
                         StrWhere += " and refund_time='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                     }
                     else
                     {
                         StrWhere += " and order_date='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                     }
                 }
                else
                {
                    if (!string.IsNullOrEmpty(dateTimeStart.Value.Trim().ToString()))
                    {
                        if(BillingType==MaterialRequisition)
                        {
                            StrWhere += " and fetch_time>='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                        }
                        else if(BillingType==RefundMaterial)
                        {
                            StrWhere += " and refund_time>='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                        }
                        else
                        {
                            StrWhere += " and order_date>='" + Common.LocalDateTimeToUtcLong(StartTime) + "'";
                        }

                    }     
                    if (!string.IsNullOrEmpty(dateTimeEnd.Value.Trim().ToString()))
                    {
                        if (BillingType == MaterialRequisition)
                        {
                            StrWhere += " and fetch_time<='" + Common.LocalDateTimeToUtcLong(EndTime) + "'";
                        }
                        else if (BillingType == RefundMaterial)
                        {
                            StrWhere += " and refund_time<='" + Common.LocalDateTimeToUtcLong(EndTime) + "'";
                        }
                        else
                        {
                            StrWhere += " and order_date<='" + Common.LocalDateTimeToUtcLong(EndTime) + "'";
                        }
                    }
                }
                     if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim().ToString()))
                    {
                        if (BillingType == MaterialRequisition)
                        {
                            StrWhere += " and material_num='" + txtorder_num.Caption.Trim().ToString() + "'";
                        }
                        else if (BillingType == RefundMaterial)
                        {
                            StrWhere += " and refund_no='" + txtorder_num.Caption.Trim().ToString() + "'";
                        }
                        else
                        {
                            StrWhere += " and order_num='" +txtorder_num.Caption.Trim().ToString() + "'";
                        }

                    }
                     if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
                    {
                        if (BillingType == MaterialRequisition || BillingType == RefundMaterial)
                        {
                            StrWhere += " and responsible_name='" + ddlhandle.Text.ToString() + "'";
                        }
                        else
                        {
                            StrWhere += " and handle_name='" + ddlhandle.Text.ToString() + "'";
                        }
                    }
                     if (!string.IsNullOrEmpty(ddloperator.SelectedValue.ToString()))
                    {
                        if (BillingType == MaterialRequisition || BillingType == RefundMaterial)
                        {
                            StrWhere += " and create_name='" + ddloperator.Text.Trim().ToString() + "'";
                        }
                        else
                        {
                            StrWhere += " and operator_name='" + ddloperator.Text.Trim().ToString() + "'";
                        }

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
