using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using Model;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using Utility.Common;
using HXCPcClient.Chooser;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Documents;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    public partial class UCAllocationBillAddOrEdit : UCBase
    {
        #region 全局变量
        WindowStatus status;
        UCAllocationBillManager UCAllocBM;
        private string stockinoutid = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存
        private string storage = DataSources.GetDescription(DataSources.EnumAllocationBillType.Storage, true);//入库
        private string OutBound = DataSources.GetDescription(DataSources.EnumAllocationBillType.OutboundOrder, true);//出库
        private List<string> BillingIDLst=null;//存储导入开单主键ID
        //开单类型
        private const string PurchaseBilling = "采购开单";
        private const string SaleBilling = "销售开单";
        private const string MaterialRequisition = "领料单";
        private const string RefundBill = "领料退货单";
        private const string AllotBill = "调拨单";
        private const string InventoryBill = "盘点单";
        private const string OtherReceiveBill = "其它收货单";
        private const string OtherSendBill = "其它发货单";

        private string AllocBillLogMsg = "查询出入库单表信息";
        private string AllocPartLogMsg = "查询出入库单配件表信息";
        private string PurpartsLogMsg = "查询采购开单配件表信息";
        private string SalepartsLogMsg = "查询销售开单配件表信息";
        private string FetchpartsLogMsg = "查询领料单配件表信息";
        private string RefundpartsLogMsg = "查询领料退货单配件表信息";
        private string AllotpartsLogMsg = "查询调拨单配件表信息";
        private string InvpartsLogMsg = "查询盘点单配件表信息";
        private string RecpartsLogMsg = "查询其它收货单配件表信息";
        private string ShippartsLogMsg = "查询其它发货单配件表信息";
        private string PurBillModifyLog = "修改采购开单的导入状态为占用";
        private string SaleBillModifyLog = "修改销售开单的导入状态为占用";
        private string FetchBillModifyLog = "修改领料单的导入状态为占用";
        private string RefundBillModifyLog = "修改领料退货单的导入状态为占用";
        private string AllotBillModifyLog = "修改调拨单的导入状态为占用";
        private string InvBillModifyLog = "修改盘点单的导入状态为占用";
        private string RecBillModifyLog = "修改其它收货单的导入状态为占用";
        private string ShipBillModifyLog = "修改其它发货单的导入状态为占用";

        //要查询的开单信息表名
        private string InoutBillTable = "tb_parts_stock_inout";//出入库单信息表
        private string PurBillTable = "tb_parts_purchase_billing";//采购开单信息表
        private string SaleBillTable = "tb_parts_sale_billing";//销售开单信息表
        private string FetchBillTable = "tb_maintain_fetch_material";//领料单信息表
        private string RefundBillTable = "tb_maintain_refund_material";//领料退货单信息表
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单配件信息表
        private string InvBillTable = "tb_parts_stock_check";//盘点单配件信息表
        private string RecBillTable = "tb_parts_stock_receipt";//其它收货单信息表
        private string ShipBillTable = "tb_parts_stock_shipping";//其它发货单信息表
        private string WareHouseTable = "tb_warehouse";//仓库表
        //要查询的开单配件信息表名
        private string InoutPartTable = "tb_parts_stock_inout_p";//出入库单配件信息表
        private string PurPartTable = "tb_parts_purchase_billing_p";//采购开单配件信息表
        private string SalePartTable = "tb_parts_sale_billing_p";//销售开单配件信息表
        private string FetchPartTable = "tb_maintain_fetch_material_detai";//领料单配件信息表
        private string RefundPartTable = "tb_maintain_refund_material_detai";//领料退货单配件信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件信息表
        private string InvPartTable = "tb_parts_stock_check_p";//盘点单配件信息表
        private string RecPartTable = "tb_parts_stock_receipt_p";//其它收货单配件信息表
        private string ShipPartTable = "tb_parts_stock_shipping_p";//其它收货单配件信息表

        //开单对应的配件信息表
        private const string InoutBillingId = "stock_inout_id";//出入库单主键
        private const string PurBillingId = "purchase_billing_id";//采购开单主键
        private const string SaleBillingId = "sale_billing_id";//销售开单主键
        private const string FetchBillingId = "fetch_id";//领料单主键
        private const string RefundBillingId = "refund_id";//领料单主键
        private const string AllotBillingId = "stock_allot_id";//调拨单主键
        private const string InvBillingId = "stock_check_id";//盘点单主键
        private const string RecBillingId = "stock_receipt_id";//其它收货单主键
        private const string ShipBillingId = "stock_shipping_id";//其它发货单主键
        private const string CallInWhouseId = "call_in_wh";//调入仓库id
        private const string CallInWhouseName = "call_in_wh_name";//调入仓库名称
        private const string CallOutWhouseId = "call_out_wh";//调出仓库id
        private const string CallOutWhouseName = "call_out_wh_name";//调出仓库名称
        private const string WhouseId = "wh_id";//仓库Id
        private const string WhouseName = "wh_name";//仓库名称
        private const string PartCode = "parts_code";//配件编码
        private const string PartsName = "parts_name";//配件名称
        private const string CarPCode = "car_parts_code";//车厂编码
        private const string CarFCode = "car_factory_code";//车厂编码
        private const string BarCode = "parts_barcode";//条码
        private const string PartModel = "model";//规格
        private const string DrawNum = "drawing_num";//图号
        private const string UnitName = "unit_name";//单名称
        private const string PartsBrand = "parts_brand";//品牌
        private const string BusinCount = "business_counts";//库存数量
        private const string Remark = "remark";//备注

        /// <summary>
        /// 获取出入库单表头表尾信息实体
        /// </summary>
        tb_parts_stock_inout AllocationBillEntity = new tb_parts_stock_inout();
        StringBuilder sb_Fields = new StringBuilder();//字符串集合
        #endregion

        /// <summary>
        /// 初始化出入单窗体信息
        /// </summary>
        public UCAllocationBillAddOrEdit(WindowStatus state, string InOutId, UCAllocationBillManager UCABManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCAllocBM = UCABManager;//获取出入库单管理类
            this.status = state;//获取操作状态
            this.stockinoutid = InOutId;//出入库单ID
            base.SaveEvent += new ClickHandler(UCAllocationBillAddOrEdit_SaveEvent);//保存
            base.SubmitEvent += new ClickHandler(UCAllocationBillAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCAllocationBillAddOrEdit_ImportEvent);//导入
            base.DeleteEvent += new ClickHandler(UCAllocationBillAddOrEdit_DeleteEvent);//删除
            //设置列表的可编辑状态
            gvPartsMsgList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvPartsMsgList.Columns)
            {
                if (dgCol.Name != colCheck.Name  && dgCol.Name != counts.Name && dgCol.Name != isgif.Name && dgCol.Name != remarks.Name) dgCol.ReadOnly = true;
            }
            if (string.IsNullOrEmpty(InOutId))
            {
                label14.Visible = false;
                label15.Visible = false;
                lblupdate_name.Visible = false;
                lblupdate_time.Visible = false;

            }
            //设置GridView单元格内容显示位置
            gvPartsMsgList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

       private  void UCAllocationBillAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            DelSelectedRows();//删除选中配件信息
        }

        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //单据类型
                CommonFuncCall.BindAllocationBillType(Comborder_type_name, true, "请选择");
                //开单类型
                CommonFuncCall.BindInStockBillingType(Combbilling_type_name, true, "请选择");
                ////获取仓库名称
                //CommonFuncCall.BindWarehouse(Combwh_name, "请选择");
                //公司ID
                string com_id = GlobalStaticObj.CurrUserCom_Id;
                CommonFuncCall.BindDepartment(Comborg_name, com_id, "请选择");//选择部门名称
                CommonFuncCall.BindHandle(Combhandle_name, "", "请选择");//选择经手人
                lbloperator_name.Text = GlobalStaticObj.UserName;//操作人
                lblcreate_name.Text = GlobalStaticObj.UserName;//创建人
                lblcreate_time.Text = DateTime.Now.ToLongDateString();//创建时间
                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(stockinoutid);//获取单据头尾信息
                    GetBillPartsMsg(stockinoutid);//获取单据配件信息
                }
                else if (status == WindowStatus.Add || status == WindowStatus.Copy)
                {
                    base.btnSubmit.Visible = false;//隐藏提交按钮
                    txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);//获取单据状态
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 单据类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comborder_type_name_SelectedIndexChanged(object sender, EventArgs e)
        {
             try
             {
                 if (string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
                 {
                     CommonFuncCall.BindInStockBillingType(Combbilling_type_name, true, "请选择");
                 }
                 else  if (Comborder_type_name.Text.ToString() == storage)//入库单选择
                 {
                     CommonFuncCall.BindInStockBillingType(Combbilling_type_name, true, "请选择");
                 }
                 else if (Comborder_type_name.Text.ToString() == OutBound)//出库单选择
                 {
                     CommonFuncCall.BindOutStockBillingType(Combbilling_type_name, true, "请选择");
                 }
             }
             catch (Exception ex)
             {
                 MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
             }
        }

        /// <summary>
        /// 开单类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combbilling_type_name_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
                {
                    CommonFuncCall.BindInStockBillingType(Combbilling_type_name, true, "请选择");
                    MessageBoxEx.Show("请您先选择单据类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        #region 出入库单操作事件
        /// <summary>
        /// 保存出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            if (gvPartsMsgList.Rows.Count == 0)
            {
                MessageBoxEx.Show("请您导入要生成出入库单的配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 提交出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 出入库单导入单据
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_ImportEvent(object send, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择要导入的单据类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
               else if (string.IsNullOrEmpty(Combbilling_type_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择要导入的开单类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string SelValue = Combbilling_type_name.Text.ToString();//获取选择的开单类型
                    switch (SelValue)
                    {
                        case PurchaseBilling://采购开单
                            ImportBillingMethod(PurBillTable, PurPartTable, PurBillingId, PurpartsLogMsg, PurBillModifyLog, PurchaseBilling);
                            break;
                        case SaleBilling://销售开单
                            ImportBillingMethod(SaleBillTable, SalePartTable, SaleBillingId, SalepartsLogMsg, SaleBillModifyLog, SaleBilling);
                            break;
                        case MaterialRequisition://领料单
                            ImportStockBillMethod(FetchBillTable, FetchPartTable, FetchBillingId, FetchpartsLogMsg, FetchBillModifyLog, MaterialRequisition);
                            break;
                        case RefundBill://领料退货单
                            ImportStockBillMethod(RefundBillTable, RefundPartTable, RefundBillingId, RefundpartsLogMsg, RefundBillModifyLog, RefundBill);
                            break;
                        case AllotBill://调拨单
                            ImportStockBillMethod(AllotBillTable, AllotPartTable, AllotBillingId, AllotpartsLogMsg, AllotBillModifyLog, AllotBill);
                            break;
                        case InventoryBill://盘点单
                            ImportStockBillMethod(InvBillTable, InvPartTable, InvBillingId, InvpartsLogMsg, InvBillModifyLog, InventoryBill);
                            break;
                        case OtherReceiveBill://其它收货单
                            ImportStockBillMethod(RecBillTable, RecPartTable, RecBillingId, RecpartsLogMsg, RecBillModifyLog, OtherReceiveBill);
                            break;
                        case OtherSendBill://其它发货单
                            ImportStockBillMethod(ShipBillTable, ShipPartTable, ShipBillingId, ShippartsLogMsg, ShipBillModifyLog, OtherSendBill);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        #endregion

        /// <summary>
        /// 输入数量的控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                {
                    if (gvPartsMsgList.Rows[i].Cells["counts"].Value == null)
                    {

                        return;

                    }
                    else
                    {
                        decimal Amount = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString());//总量
                        if (Amount < 0)
                        {
                            return;
                        }
                        else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString()))
                        {
                            MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
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
        /// 正则表达式判断输入字符串是否为数字
        /// </summary>
        /// <param name="strValue">要判断的字符串</param>
        /// <returns></returns>
        private static bool IsNumber(string strValue)
        {
            string RegPattern = "^-?[0-9]*$";
            Regex RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            RegPattern = @"^-?\d+\.\d{1,3}?$";//判断一到三位小数数值
            RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            return false;
        }

        /// <summary>
        /// 数据保存/提交 方法
        /// </summary>
        /// <param name="HandleTypeName">保存/提交</param>
        private void SaveOrSubmitMethod(string HandleTypeName)
        {
            try
            {

                if (CheckListInfo())
                {
                    gvPartsMsgList.EndEdit();
                    string opName = "出入库单操作";

                    if (lblorder_num.Text == "." && Comborder_type_name.Text.ToString() == storage)
                    {
                        lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.InBill);//获取入库单编号
                    }
                    else if (lblorder_num.Text == "." && Comborder_type_name.Text.ToString() == OutBound)
                    {
                        lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.OutBill);//获取出库单编号
                    }
                    if (HandleTypeName == submit)
                    {
                        txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                    }
                    else if (HandleTypeName == save)
                    {
                        txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                    }
                    List<SysSQLString> StrSql = new List<SysSQLString>();//存储所有执行的sql语句
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        stockinoutid = Guid.NewGuid().ToString();
                        AddAllocBillSql(StrSql, AllocationBillEntity, stockinoutid, HandleTypeName);
                        if (Comborder_type_name.Text.ToString() == storage)
                        {
                            opName = "新增入库单";
                            SetBillsState();//修改开单状态为已占用

                        }
                        else if (Comborder_type_name.Text.ToString() == OutBound) 
                        {
                            opName = "新增出库单";
                            SetBillsState();//修改开单状态为已占用
                        }

                        AddOrEditPartsListMsg(StrSql, stockinoutid,WindowStatus.Add);
                    }
                    else if (status == WindowStatus.Edit)
                    {

                        EditAllocBillSql(StrSql, AllocationBillEntity, stockinoutid, HandleTypeName);
                        if (Comborder_type_name.Text.ToString() == storage) opName = "修改入库单";
                        else if (Comborder_type_name.Text.ToString() == OutBound) opName = "修改出库单";
                        AddOrEditPartsListMsg(StrSql, stockinoutid,WindowStatus.Edit);
                    }
                    

                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, StrSql))
                    {
                        if (HandleTypeName==submit)
                        {
                            MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (HandleTypeName == save)
                        {
                            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1 and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        UCAllocBM.GetInOutBillList(DefaultWhere);//更新查询单据
                        deleteMenuByTag(this.Tag.ToString(), UCAllocBM.Name);
                    }
                    else
                    {
                        if (HandleTypeName == submit)
                        {
                            MessageBoxEx.Show("提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (HandleTypeName == save)
                        {
                            MessageBoxEx.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void SetBillsState()
        {
            try
            {
                    for (int i = 0; i < BillingIDLst.Count; i++)
                    {
                        if (status == WindowStatus.Add  && Combbilling_type_name.Text.ToString() == PurchaseBilling)
                        {

                            SetBillingStatus(PurBillTable, PurBillingId, BillingIDLst[i].ToString(), PurBillModifyLog);//修改采购开单状态为已占用
                    
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == SaleBilling)
                        {
                            SetBillingStatus(SaleBillTable, SaleBillingId, BillingIDLst[i].ToString(), SaleBillModifyLog);//修改销售开单状态为已占用
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == AllotBill)
                        {
                            SetBillingStatus(AllotBillTable, AllotBillingId, BillingIDLst[i].ToString(), AllotBillModifyLog);//修改调拨开单状态为已占用
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == MaterialRequisition)
                        {
                            SetBillingStatus(FetchBillTable, FetchBillingId, BillingIDLst[i].ToString(),FetchBillModifyLog);//修改领料开单状态为已占用
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == RefundBill)
                        {
                            SetBillingStatus(RefundBillTable, RefundBillingId, BillingIDLst[i].ToString(), RefundBillModifyLog);//修改领料退货开单状态为已占用
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == InventoryBill)
                        {
                            SetBillingStatus(InvBillTable, InvBillingId, BillingIDLst[i].ToString(), InvBillModifyLog);//修改盘点单状态为已占用
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == OtherReceiveBill)
                        {
                            SetBillingStatus(RecBillTable, RecBillingId, BillingIDLst[i].ToString(), RecBillModifyLog);//其它收货单信息表
                        }
                        else if (status == WindowStatus.Add && Combbilling_type_name.Text.ToString() == OtherSendBill)
                        {
                            SetBillingStatus(ShipBillTable, ShipBillingId, BillingIDLst[i].ToString(), ShipBillModifyLog);//其它发货单信息表
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 导入开单方法
        /// </summary>
        /// <param name="BillTable">开单表</param>
        /// <param name="PartTable">配件表</param>
        /// <param name="BillingPrimID">开单主键ID名称</param>
        /// <param name="QueryLogMsg">数据库查询日志</param>
        /// <param name="ModifyLogMsg">数据库修改日志</param>
        private void ImportBillingMethod(string BillTable, string PartTable, string BillingPrimID, string QueryLogMsg, string ModifyLogMsg,string BillType)
        {
            try
            {
                if (string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择单据类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else  if (string.IsNullOrEmpty(Combbilling_type_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择开单类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UCImportBilling UCPB = new UCImportBilling(Comborder_type_name.Text.ToString(), Combbilling_type_name.Text.ToString());
                DialogResult result = UCPB.ShowDialog();

                if (result == DialogResult.OK)
                {
                    sb_Fields.Clear();//清空所有字符串
                    sb_Fields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                    BillingPrimID, WhouseId, WhouseName, PartCode, PartsName, PartModel, CarFCode, BarCode , DrawNum, UnitName, PartsBrand, Remark);//要查询的字段名称格式项
                    DataTable dt_Parts = null;//配件信息数据表 
                    string BillPrimIDValue = string.Empty;//要查询的开单主键ID条件
                    string BillNo = string.Empty;//开单编号
                    string PartNumber = string.Empty;//查询的配件编号条件
                    string BillTypeName = Combbilling_type_name.Text.ToString();//开单类型名
                    BillingIDLst = new List<string>(UCPB.BillPrimIDDic.Keys);//获取导入开单主键ID
                    string StrFields = BillType == PurchaseBilling ? sb_Fields + ",business_counts" : sb_Fields + ",business_count";//查询字段列表
                    foreach (KeyValuePair<string, string> KVPair in UCPB.BillPrimIDDic)
                    {
                        BillPrimIDValue = KVPair.Key.ToString();//要查询的条件
                        BillNo = KVPair.Value.ToString();//获取开单编号
                        if (gvPartsMsgList.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                            {
                                //判断要导入开单信息是否已存在于列表中
                                string BPrimID = gvPartsMsgList.Rows[i].Cells["billing_ID"].Value.ToString();//开单主键ID


                                if (KVPair.ToString() == BPrimID)
                                {
                                    MessageBoxEx.Show("您要导入的" + BillTypeName + "已经存在！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    //查询符合条件的配件信息
                                    dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, StrFields, BillingPrimID + "='" + BillPrimIDValue + "'", "", "");
                                }


                            }
                        }
                        else
                        {
                            dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, StrFields, BillingPrimID + "='" + BillPrimIDValue + "'", "", "");
                           
                        }
                         GetGridViewRowByDrImport(dt_Parts, BillingPrimID, BillNo,BillType);//把查询到的配件信息导入控件列表
                    }
                  

                    //判断配件信息列表中的值是否需要导入
                    if (UCPB.PartHTable.Count != 0)
                    {

                        foreach (DictionaryEntry DicEty in UCPB.PartHTable)
                        {
                            PartNumber = DicEty.Key.ToString();//获取配件编码
                            BillPrimIDValue = DicEty.Value.ToString();//获取开单号
                            //对应单据的配件信息查询条件
                            string PartWhere = BillingPrimID + "='" + BillPrimIDValue + "'" + " and  parts_code='" + PartNumber + "'";
                            if (gvPartsMsgList.Rows.Count > 0)
                            {
                                for (int k = 0; k < gvPartsMsgList.Rows.Count; k++)
                                {

                                    string gvPartNumber = gvPartsMsgList.Rows[k].Cells["partsnum"].Value.ToString();//配件列表中的配件编号
                                    string gvBillId = gvPartsMsgList.Rows[k].Cells["billing_ID"].Value.ToString();//配件列表中隐藏的开单主键ID
                                    if (BillPrimIDValue == gvBillId && PartNumber == gvPartNumber)
                                    {
                                        MessageBoxEx.Show("您要导入的" + BillTypeName + "配件已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, StrFields, PartWhere, "", "");
                                           
                                    }
                                

                                 }
                            }
                            else
                            {
                                dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, StrFields, PartWhere, "", "");
                               
                            }
                            GetGridViewRowByDrImport(dt_Parts, BillingPrimID, BillNo, BillType);//导入配件信息列表 
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
        /// 导入库存管理开单方法
        /// </summary>
        /// <param name="BillTable">开单表</param>
        /// <param name="PartTable">配件表</param>
        /// <param name="BillingPrimID">开单主键ID名称</param>
        /// <param name="QueryLogMsg">数据库查询日志</param>
        /// <param name="ModifyLogMsg">数据库修改日志</param>
        private void ImportStockBillMethod(string BillTable, string PartTable, string BillingPrimID, string QueryLogMsg, string ModifyLogMsg, string BillType)
        {
            try
            {
                UCImportStockBill UCPSB =  new UCImportStockBill(Comborder_type_name.Text.ToString(), Combbilling_type_name.Text.ToString());
                DialogResult result = UCPSB.ShowDialog();

                if (result == DialogResult.OK)
                {
                    sb_Fields.Clear();//清空所有字符串
                    DataTable dt_Parts = null;//配件信息数据表 
                    string BillPrimIDValue = string.Empty;//要查询的开单主键ID条件
                    string BillNo = string.Empty;//开单编号
                    string PartNumber = string.Empty;//查询的配件编号条件
                    string BillTypeName = Combbilling_type_name.Text.ToString();//开单类型名
                    BillingIDLst =new List<string>( UCPSB.StockBillIDDic.Keys);//获取导入开单主键ID
                    if (Comborder_type_name.Text.ToString() == storage && BillTypeName == AllotBill) sb_Fields.Append(UCPSB.AllotFields + ","
                    + CallInWhouseId + "," + CarPCode + "," + BarCode + "," + CallInWhouseName + "," + PartsBrand + "," + Remark);//调拨入库单配件
                    else if (Comborder_type_name.Text.ToString() == OutBound && BillTypeName == AllotBill) sb_Fields.Append(UCPSB.AllotFields + ","
                      + CallOutWhouseId + "," + CarPCode + "," + BarCode + "," + CallOutWhouseName + "," + PartsBrand + "," + Remark);//调拨出库单配件
                    else if (BillTypeName == OtherReceiveBill || BillTypeName == OtherSendBill) sb_Fields.Append(UCPSB.ReceiveShipFields
                    + "," + WhouseId + "," + WhouseName + "," + PartModel + "," + CarPCode + "," + BarCode + "," + PartsBrand + "," + Remark);//其它收发货单配件
                    else if (BillTypeName == MaterialRequisition) sb_Fields.Append(UCPSB.FetchRefundFields + ",warehouse,wh_name,norms,remarks");//领料单配件
                    else if (BillTypeName == RefundBill) sb_Fields.Append(UCPSB.FetchRefundFields + ",warehouse,wh_name,norms,remarks");//领料退货单配件
                    else if (BillTypeName == InventoryBill) sb_Fields.Append(UCPSB.InventoryFields + "," + CarPCode + "," + BarCode + "," + PartsBrand + "," + Remark);//盘点单配件
                    foreach (KeyValuePair<string,string> KVPair in UCPSB.StockBillIDDic)
                    {
                        BillPrimIDValue = KVPair.Key.ToString();//要查询的条件
                        BillNo = KVPair.Value.ToString();//获取开单编号
                        if (gvPartsMsgList.Rows.Count > 0)
                        {

                            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                            {
                                //判断要导入开单信息是否已存在于列表中
                                string BPrimID = gvPartsMsgList.Rows[i].Cells["billing_ID"].Value.ToString();//开单主键ID
                                if (KVPair.ToString() == BPrimID)
                                {
                                    MessageBoxEx.Show("您要导入的" + BillTypeName + "已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    if (BillTypeName == MaterialRequisition || BillTypeName == RefundBill)
                                    {
                                        //查询符合条件的配件信息
                                        dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable+" left join "+WareHouseTable+" on "+WareHouseTable+"."+
                                        WhouseId + "=" + PartTable+"."+WhouseId, 
                                        sb_Fields.ToString(), BillingPrimID + "='" + BillPrimIDValue + "'", "", "");
                                    }
                                    else
                                    {
                                        //查询符合条件的配件信息
                                        dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, sb_Fields.ToString(), BillingPrimID + "='" + BillPrimIDValue + "'", "", "");
                                    }
                                }


                            }
                        }
                        else
                        {
                            dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, sb_Fields.ToString(), BillingPrimID + "='" + BillPrimIDValue + "'", "", "");
                        }

                        GetGridViewRowByDrImport(dt_Parts, BillingPrimID, BillNo,BillType);//把查询到的配件信息导入控件列表

                    }

                    //判断配件信息列表中的值是否需要导入
                    if (UCPSB.PartHTable.Count != 0)
                    {
                        foreach (DictionaryEntry DicEty in UCPSB.PartHTable)
                        {
                            PartNumber = DicEty.Key.ToString();//获取配件编码
                            BillPrimIDValue = DicEty.Value.ToString();//获取开单号
                            //对应单据的配件信息查询条件
                            string PartWhere =BillingPrimID+"='" + BillPrimIDValue + "'" + " and  parts_code='" + PartNumber + "'";
                            if (gvPartsMsgList.Rows.Count > 0)
                            {
                                for (int k = 0; k < gvPartsMsgList.Rows.Count; k++)
                                {
                                    string gvPartNumber = gvPartsMsgList.Rows[k].Cells["partsnum"].Value.ToString();//配件列表中的配件编号
                                    string gvBillId = gvPartsMsgList.Rows[k].Cells["billing_ID"].Value.ToString();//配件列表中隐藏的开单主键ID
                                    if (BillPrimIDValue == gvBillId && PartNumber == gvPartNumber)
                                    {
                                        MessageBoxEx.Show("您要导入的" + BillTypeName + "已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, sb_Fields.ToString(), PartWhere, "", "");
                                       
                                    }
                                }
                            }
                            else
                            {
                                dt_Parts = DBHelper.GetTable(QueryLogMsg, PartTable, sb_Fields.ToString(), PartWhere, "", "");
                            }
                            GetGridViewRowByDrImport(dt_Parts, BillingPrimID, BillNo, BillType);//导入配件信息列表

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
        /// 当添加成功时,将开单中的退货/换货状态设置成占用
        /// 使前置单据不可以编辑、删除
        /// </summary>
        /// <param name="BillTable">开单表名</param>
        /// <param name="BPrimIDName">开单主键名称</param>
        /// <param name="BPrimIDValue">开单主键值</param>
        /// <param name="ModifyLog">数据库操作日</param>
        private void SetBillingStatus(string BillTable, string BPrimIDName, string BPrimIDValue, string ModifyLog)
        {
            try
            {

                Dictionary<string, string> dicValue = new Dictionary<string, string>();
                dicValue.Add("is_occupy", "1");//单据导入状态，0正常，1占用，2锁定
                DBHelper.Submit_AddOrEdit(ModifyLog, BillTable, BPrimIDName, BPrimIDValue, dicValue);//修改单据状态

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 批量导入配件列表信息
        /// </summary>
        /// <param name="partsTable">数据库中获取的配件信息表</param>
        /// <param name="BillID">数据库中获取的配件信息表主键ID</param>
        private void GetGridViewRowByDrImport(DataTable partsTable, string BillID,string BillNumValue,string BillingType)
        {
            try
            {
              
                for (int i = 0; i < partsTable.Rows.Count; i++)
                {

                    DataGridViewRow dgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建GridView行项
                    dgRow.Cells["billing_ID"].Value = partsTable.Rows[i][BillID].ToString();
                    dgRow.Cells["billnumber"].Value = BillNumValue;
                    dgRow.Cells["WhName"].Value = partsTable.Rows[i]["wh_name"].ToString();
                    dgRow.Cells["partsnum"].Value = partsTable.Rows[i]["parts_code"].ToString();
                    dgRow.Cells["partname"].Value = partsTable.Rows[i]["parts_name"].ToString();

                   if (BillingType == PurchaseBilling)
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["wh_id"].ToString();//
                        dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i][CarFCode].ToString();
                        dgRow.Cells["PartBarCode"].Value = partsTable.Rows[i][BarCode].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit_name"].ToString();
                        dgRow.Cells["partbrand"].Value = partsTable.Rows[i]["parts_brand"].ToString();
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["model"].ToString();
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["business_counts"].ToString();//销售或采购
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remark"].ToString();
                    }
                    else  if ( BillingType == SaleBilling)
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["wh_id"].ToString();
                        dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i][CarFCode].ToString();
                        dgRow.Cells["PartBarCode"].Value = partsTable.Rows[i][BarCode].ToString();
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["model"].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit_name"].ToString();
                        dgRow.Cells["partbrand"].Value = partsTable.Rows[i]["parts_brand"].ToString();
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["business_count"].ToString();//销售或采购
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remark"].ToString();
                    }
                    else if (BillingType == MaterialRequisition )
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["warehouse"].ToString();
                        dgRow.Cells["CarPartCode"].Value ="";
                        dgRow.Cells["PartBarCode"].Value = "";
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["norms"].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawn_no"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit"].ToString();
                        dgRow.Cells["partbrand"].Value = "";//品牌
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["quantity"].ToString();//领料
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remarks"].ToString();
                    }
                    else if ( BillingType == RefundBill)
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["warehouse"].ToString();
                        dgRow.Cells["CarPartCode"].Value ="";
                        dgRow.Cells["PartBarCode"].Value = "";
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["norms"].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawn_no"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit"].ToString();
                        dgRow.Cells["partbrand"].Value ="";//品牌
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["quantity"].ToString();//退料
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remarks"].ToString();
                    }
                    else if (BillingType == AllotBill || BillingType == OtherReceiveBill  ||  BillingType ==OtherSendBill)
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["wh_id"].ToString();
                        dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i][CarPCode].ToString();
                        dgRow.Cells["PartBarCode"].Value = partsTable.Rows[i][BarCode].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit_name"].ToString();
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["model"].ToString();
                        dgRow.Cells["partbrand"].Value = partsTable.Rows[i]["parts_brand"].ToString();
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["counts"].ToString();//调拨
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remark"].ToString();
                    }
                    else if (BillingType == InventoryBill)
                    {
                        dgRow.Cells["WhId"].Value = partsTable.Rows[i]["wh_id"].ToString();
                        dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i][CarPCode].ToString();
                        dgRow.Cells["PartBarCode"].Value = partsTable.Rows[i][BarCode].ToString();
                        dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["model"].ToString();
                        dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit_name"].ToString();
                        dgRow.Cells["partbrand"].Value = partsTable.Rows[i]["parts_brand"].ToString();
                        dgRow.Cells["counts"].Value = partsTable.Rows[i]["profitloss_count"].ToString();//盘点
                        dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remark"].ToString();
                    }
                   

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        private void AddAllocBillSql(List<SysSQLString> listSql, tb_parts_stock_inout stockInoutEntity, string StockInoutId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                //SQL语句拼装操作
                SysSQLString StrSqlObj = new SysSQLString();
                StrSqlObj.cmdType = CommandType.Text;
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//保存SQL语句参数值
                CommonFuncCall.FillEntityByControls(this, stockInoutEntity);
                stockInoutEntity.stock_inout_id = StockInoutId;

                stockInoutEntity.update_by = GlobalStaticObj.UserID;
                stockInoutEntity.operators = GlobalStaticObj.UserID;
                stockInoutEntity.create_by = GlobalStaticObj.UserID;
                stockInoutEntity.update_name = GlobalStaticObj.UserName;
                stockInoutEntity.create_name = GlobalStaticObj.UserName;
                stockInoutEntity.operator_name = GlobalStaticObj.UserName;
                stockInoutEntity.create_time =Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                stockInoutEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockInoutEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockInoutEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockInoutEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockInoutEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (stockInoutEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" insert into tb_parts_stock_inout ( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in stockInoutEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockInoutEntity, null);
                        sb_PrName.Append("," +name);//数据表字段名
                        sb_PrValue.Append(",@" +name);//数据表字段值
                        dicParam.Add(name,value == null ? "" : value.ToString());
                    }
                    sb.Append(sb_PrName.ToString().Substring(1, sb_PrName.ToString().Length - 1) + ") Values (");//追加字段名
                    sb.Append(sb_PrValue.ToString().Substring(1, sb_PrValue.ToString().Length - 1) + ");");//追加字段值
                    //完成SQL语句的拼装
                    StrSqlObj.sqlString = sb.ToString();
                    StrSqlObj.Param = dicParam;
                    listSql.Add(StrSqlObj);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        /// <param name="model"></param>
        private void EditAllocBillSql(List<SysSQLString> listSql, tb_parts_stock_inout stockInoutEntity, string StockInoutId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, AllocationBillEntity);

                stockInoutEntity.handle = GlobalStaticObj.UserID;
                stockInoutEntity.operators = GlobalStaticObj.UserID;
                stockInoutEntity.update_name = GlobalStaticObj.UserName;
                stockInoutEntity.update_time = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                stockInoutEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockInoutEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockInoutEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockInoutEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockInoutEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (AllocationBillEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" update tb_parts_stock_inout Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in stockInoutEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockInoutEntity, null);
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
                    sb.Append(" where stock_inout_id='" + StockInoutId + "';");
                    sysStrSql.sqlString = sb.ToString();
                    sysStrSql.Param = dicParam;
                    listSql.Add(sysStrSql);//完成SQL语句的拼装
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 根据出入库单主键ID和表头信息对配件信息列表进行添加或编辑
        /// </summary>
        /// <param name="listSQL">要执行的SQL语句列表</param>
        /// <param name="InoutId">出入库单号</param>
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string InoutId, WindowStatus WinState)
        {
            try
            {
                string Gift = "1";
                string NoGift = "0";
                if (gvPartsMsgList.Rows.Count > 0)
                {

                    //循环获取配件信息表中的数据记录，添加到数据库中
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb_ColName = new StringBuilder();//字段名集合
                    StringBuilder sb_ColValue = new StringBuilder();//字段值参数名
                    string ColName = string.Empty;
                    for (int i = 1; i < gvPartsMsgList.Columns.Count; i++)
                    {
                         ColName = gvPartsMsgList.Columns[i].DataPropertyName.ToString();
                         if (!string.IsNullOrEmpty(ColName))
                        {                
                         sb_ColName.Append("," + ColName);//数据表字段名
                         sb_ColValue.Append(",@" + ColName);//数据表值的参数名
                        }

                    }
                    if (WinState == WindowStatus.Edit)
                    {
                        SysSQLString SQLStr = new SysSQLString();//创建存储要删除sql语句的实体类
                        SQLStr.cmdType = CommandType.Text;//sql语句执行格式
                        Dictionary<string, string> DicDelParam = new Dictionary<string, string>();
                        StringBuilder sbDelSql = new StringBuilder();
                        DicDelParam.Add("stock_inout_id",InoutId);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", InoutPartTable, InoutBillingId);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    //完成SQl语句的字段名拼装操作
                    sb.Append("insert into tb_parts_stock_inout_p (stock_inout_parts_id,stock_inout_id,num");
                    sb.Append(sb_ColName.ToString() + ") values(@stock_inout_parts_id,@stock_inout_id,@num" + sb_ColValue.ToString() + ");");
                    int SerialNum = 1;
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {
                        SysSQLString  StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合

                        DicPartParam.Add("stock_inout_parts_id", Guid.NewGuid().ToString());
                        DicPartParam.Add("stock_inout_id", InoutId);
                        DicPartParam.Add("reference_billid", DgVRow.Cells["billing_ID"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["billing_ID"].Value.ToString());
                        DicPartParam.Add("reference_billno", DgVRow.Cells["billnumber"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["billnumber"].Value.ToString());
                        DicPartParam.Add("num", SerialNum.ToString());
                        DicPartParam.Add("wh_id", DgVRow.Cells["WhId"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["WhId"].Value.ToString());
                        DicPartParam.Add("wh_name", DgVRow.Cells["WhName"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["WhName"].Value.ToString());
                        DicPartParam.Add("parts_code", DgVRow.Cells["partsnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsnum"].Value.ToString());
                        DicPartParam.Add("parts_name", DgVRow.Cells["partname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partname"].Value.ToString());
                        DicPartParam.Add("model", DgVRow.Cells["PartSpec"].Value.ToString()== string.Empty ? "" : DgVRow.Cells["PartSpec"].Value.ToString());
                        DicPartParam.Add("drawing_num", DgVRow.Cells["drawingnum"].Value.ToString()== string.Empty ? "" : DgVRow.Cells["drawingnum"].Value.ToString());
                        DicPartParam.Add("car_parts_code", DgVRow.Cells["CarPartCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["CarPartCode"].Value.ToString());
                        DicPartParam.Add("parts_barcode", DgVRow.Cells["PartBarCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["PartBarCode"].Value.ToString());
                        DicPartParam.Add("unit_name", DgVRow.Cells["UntName"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["UntName"].Value.ToString());
                        DicPartParam.Add("parts_brand", DgVRow.Cells["partbrand"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partbrand"].Value.ToString());
                        DicPartParam.Add("counts", DgVRow.Cells["counts"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["counts"].Value.ToString());
                        DicPartParam.Add("is_gift", (bool)((DataGridViewCheckBoxCell)DgVRow.Cells["isgif"]).EditedFormattedValue == true ? Gift : NoGift);
                        DicPartParam.Add("remark", DgVRow.Cells["remarks"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["remarks"].Value.ToString());

                        
                        StrSqlParts.sqlString = sb.ToString();//获取执行的sql语句
                        StrSqlParts.Param = DicPartParam;//获取执行的参数值
                        listSQL.Add(StrSqlParts);//添加记录行到list列表中
                        SerialNum++;//自动增加序号


                    }


                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 验证表中列表数据信息完整性
        /// </summary>
        private bool CheckListInfo()
        {

            for (int i = 0; i < gvPartsMsgList.Rows.Count;i++ )
            {

                if (gvPartsMsgList.Rows[i].Cells["counts"].Value == null)
                {
                    MessageBoxEx.Show("您输入的数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    decimal Amount = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString());//总量
                    if (Amount < decimal.Zero && Comborder_type_name.Text.ToString() == storage)
                    {
                        MessageBoxEx.Show("您输入的数量不能为负数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (Amount > decimal.Zero && Comborder_type_name.Text.ToString() == OutBound)
                    {
                        MessageBoxEx.Show("您输入的数量不能为正数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString()))
                    {
                        MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }


            }
            return true;
        }

        /// <summary>
        /// 根据出入库单ID获取表头和尾部信息
        /// </summary>
        /// <param name="AllocBillId"></param>
        private void GetBillHeadEndMessage(string AllocBillId)
        {
            try
            {
                if (AllocBillId != null)
                {
                    //查询一条出入库单信息
                    DataTable InoutTable = DBHelper.GetTable(AllocBillLogMsg, InoutBillTable, "*", InoutBillingId + "='" + AllocBillId + "'", "", "");
                    CommonFuncCall.FillEntityByTable(AllocationBillEntity, InoutTable);
                    CommonFuncCall.FillControlsByEntity(this, AllocationBillEntity);
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 根据出入库单ID获取配件信息表
        /// </summary>
        /// <param name="AllocBillId"></param>
        private void GetBillPartsMsg(string AllocBillId)
        {
            try
            {

                DataTable PartsMsgList = DBHelper.GetTable(AllocPartLogMsg, InoutPartTable, "*", InoutBillingId + "='" + AllocBillId + "'", "", "");//获取配件信息表
                if (PartsMsgList.Rows.Count > 0)
                {

                    foreach (DataRow dr in PartsMsgList.Rows)
                    {
                        DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];
                        dgvr.Cells["partsnum"].Value = dr["parts_code"];
                        dgvr.Cells["partname"].Value = dr["parts_name"];
                        dgvr.Cells["PartSpec"].Value = dr["model"];
                        dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                        dgvr.Cells["UntName"].Value = dr["unit_name"];
                        dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                        dgvr.Cells["counts"].Value = dr["counts"];
                        if (dr["is_gift"].ToString() == "1")
                        {
                            ((DataGridViewCheckBoxCell)dgvr.Cells["isgif"]).Value = true;
                        }
                        else if (dr["is_gift"].ToString() == "0")
                        {
                            ((DataGridViewCheckBoxCell)dgvr.Cells["isgif"]).Value = false;
                        }
                        dgvr.Cells["remarks"].Value = dr["remark"];
                    }
                }
                else
                {
                    MessageBoxEx.Show("要查询的配件信息不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 删除选中的配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartDelete_Click(object sender, EventArgs e)
        {
            DelSelectedRows();//删除选中配件信息
        }
        /// <summary>
        /// 删除选中的配件信息
        /// </summary>
        private void DelSelectedRows()
        {
            try
            {
                List<string> SelectedRowsLst = GetSelectedRows();//获取选中记录主键ID
                int MaxCount = SelectedRowsLst.Count;//获取最大选中记录行
                if (gvPartsMsgList.SelectedRows.Count == 0 && SelectedRowsLst.Count == 0)
                {
                    MessageBoxEx.Show("您没有选中要删除的行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("您确定要删除选中记录行项！", "删除操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.Yes)
                    {
                        while (MaxCount > 0)
                        {//删除选中行记录
                            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                            {
                                bool IsCheck = (bool)((DataGridViewCheckBoxCell)gvPartsMsgList.Rows[i].Cells["colCheck"]).EditedFormattedValue;//获取选择状态
                                if (IsCheck)
                                {
                                    gvPartsMsgList.Rows.RemoveAt(i);//删除选中记录行
                                    MaxCount--;
                                }
                            }

                        }
                        if (gvPartsMsgList.SelectedRows.Count != 0)
                        {
                            for (int j = 0; j < gvPartsMsgList.SelectedRows.Count; j++)
                            {
                                gvPartsMsgList.Rows.RemoveAt(j);
                            }
                        }
                        MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 获取选中的记录行
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRows()
        { 
          try
          {
              List<string> BillingIDLst = new List<string>();//存放单据ID
              foreach (DataGridViewRow dr in gvPartsMsgList.Rows)
              {

                  bool isCheck=(bool)dr.Cells["colCheck"].EditedFormattedValue;
                  if (isCheck)
                  {
                      BillingIDLst.Add(dr.Cells["billing_ID"].ToString());
                  }
              }
              return BillingIDLst;

          }
          catch (Exception ex)
          {
              MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
              return null;
          }
        }





    }
}
