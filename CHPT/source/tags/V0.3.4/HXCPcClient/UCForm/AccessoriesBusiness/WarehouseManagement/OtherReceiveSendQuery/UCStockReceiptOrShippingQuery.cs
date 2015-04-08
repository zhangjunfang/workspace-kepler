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
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherSendGoods;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveSendQuery
{
    public partial class UCStockReceiptOrShippingQuery : UCBase
    {
        #region 全局变量
        private string ReceiptBillTable = "tb_parts_stock_receipt";//其它收货单表
        private string ShippingBillTable = "tb_parts_stock_shipping";//其它发货单表
        private string ReceiptPartTable = "tb_parts_stock_receipt_p";//其它收货单配件表
        private string ShippingPartTable = "tb_parts_stock_shipping_p";//其它发货单配件表
        private string VehModelTable = "tb_vehicle_models";//车型档案表
        private string PartsTable = "tb_parts";//配件档案表
        private string PartID = "parts_id";//配件ID
        private string PartsSerCode = "ser_parts_code";//配件表配件编码
        private string VehicleTypeId = "vm_id";//车型ID
        private string PartForVhcTable = "tb_parts_for_vehicle";//配件适用车型表
        private string ReceiptID = "stock_receipt_id";//其它收货单表主键
        private string ShippingID = "stock_shipping_id";//其它发货单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string InQueryLogMsg = "查询其它收货单表信息";//其它收发货单表操作日志
        private string OutQueryLogMsg = "查询其它发货单表信息";//其它发货单表操作日志
        private string InBill = "其它收货单";
        private string OutBill = "其它发货单";
        private string InoutBillStatus = "已开单";
        //其它收发货单表字段
        private const string InTypeName = "in_wh_type_name";
        private const string OutTypeName = "out_wh_type_name";
        private const string OrderNo = "order_num";
        private const string OrdDate = "order_date";
        private const string BussinUnit = "bussiness_units";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string BillRemk = "remark";
        private const string InOrderStatus = "in_status_name";
        private const string OutOrderStatus = "out_status_name";

        //其它收发货单配件表字段
        private const string PartsNum = "parts_num";
        private const string PartsName = "parts_name";
        private const string CarFactoryCode = "car_factory_code";//车厂配件编码
        private const string DrawNum = "drawing_num";//图号
        private const string PartSpec = "model";//规格
        private const string UnitName = "unit_name";//单位
        private const string PartCount = "counts";
        private const string AccountMoney = "money";
        private const string VmName = "vm_name";//车型名称
        private const string PartsBrand = "parts_brand";//品牌
        private const string PartGift = "is_gift";//赠品
        private const string PartRemk = "remark";


        #endregion
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public UCStockReceiptOrShippingQuery()
        {
            InitializeComponent();
            try
            {
                #region 窗体容器控件自适应大小
                //tabControlEx1自适应大小
                this.tabControlInOutBill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                          | System.Windows.Forms.AnchorStyles.Left)
                          | System.Windows.Forms.AnchorStyles.Right)));


                #region 按其它收发单查询界面控件的自适应
                this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
                this.gvInOutBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                           | System.Windows.Forms.AnchorStyles.Bottom));
                #endregion

                #region 按配件或往来单位查询界面控件的自适应
                this.panelEx3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
                this.gvInOutPartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                           | System.Windows.Forms.AnchorStyles.Bottom));
                #endregion
                #endregion
                UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
                UIAssistants.SetButtonStyle4QueryAndClear(btnPartSearch, btnPartClear);//美化查询和清除按钮控件
                DataGridViewEx.SetDataGridViewStyle(gvInOutBillList, Remarks);//美化出入库单表格控件
                DataGridViewEx.SetDataGridViewStyle(gvInOutPartList, Remarks);//美化配件或往来单位表格控件
                gvInOutBillList.AutoGenerateColumns = false;
                gvInOutPartList.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCStockReceiptOrShippingQuery_Load(object sender, EventArgs e)
        {
            try
            {
                base.SetBtnStatus(WindowStatus.View);// 根据窗体状态更改控件状态
                //根据其它收发货单查询其它收发货单
                dateTimeStart.Value = DateTime.Now.ToString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                CommonFuncCall.BindInOutBillType(CombOrderType, true, "请选择");//单据类型   
                CommonFuncCall.BindInOutType(Combinoutwhtype, true, "请选择"); //出入库类型         
                CommonFuncCall.BindWarehouse(ComBwhName, "请选择"); //获取仓库名称
                string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
                CommonFuncCall.BindCompany(ComBcom_name, "全部");//选择公司名称
                CommonFuncCall.BindDepartment(ComBorg_name, com_id, "全部");//选择部门名称
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");//选择经办人    


                //根据配件或往来单位查询其它收发货单
                dateTimePartStart.Value = DateTime.Now.ToString();
                dateTimePartEnd.Value = DateTime.Now.ToString();
                CommonFuncCall.BindCompany(ComBcom_name, "全部");//公司ID
                CommonFuncCall.BindCompany(CombPartCompany, "全部");
                CommonFuncCall.BindDepartment(ComBorg_name, "", "全部");
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 导出Excel表格数据
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_ExportEvent(object send, EventArgs e)
        {
            BtnExportMenu.Show(btnExport, 0, btnExport.Height); //指定导出菜单显示的位置 
        }
        /// <summary>
        /// 导出Excel表格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable XlsTable = null;//导出的数据表格
                if (tabControlInOutBill.SelectedTab == tabPgBill)
                {//导出按出入库单查询表格
                    if (gvInOutBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                    {
                        MessageBoxEx.Show("您要导出的单据列表不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvInOutBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列项
                        XlsTable.Columns.Add("单据编号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("仓库", typeof(string));
                        XlsTable.Columns.Add("出入库类型", typeof(string));
                        XlsTable.Columns.Add("数量", typeof(string));
                        XlsTable.Columns.Add("到货地点", typeof(string));
                        XlsTable.Columns.Add("部门", typeof(string));
                        XlsTable.Columns.Add("经办人", typeof(string));
                        XlsTable.Columns.Add("操作人", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        XlsTable.Columns.Add("出入库状态", typeof(string));
                        TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                        TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                        TableRow["出入库类型"] = dgRow.Cells["InOutWhType"].Value.ToString();
                        TableRow["数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                        TableRow["金额"] = dgRow.Cells["TotalMoney"].Value.ToString();
                        TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                        TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                        TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                        TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                        TableRow["出入库状态"] = dgRow.Cells["InOutState"].Value.ToString();
                        XlsTable.Rows.Add(TableRow);
                    }
                    CommonFuncCall.ExportExcelFile(XlsTable);//生成Excel表格文件

                }
                else if (tabControlInOutBill.SelectedTab == tabPgPart)
                {//导出按配件或往来单位查询表格
                    if (gvInOutPartList.Rows.Count == 0)
                    {
                        MessageBoxEx.Show("您要导出的单据列表不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvInOutPartList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列
                        XlsTable.Columns.Add("单据类型", typeof(string));
                        XlsTable.Columns.Add("出入库类型", typeof(string));
                        XlsTable.Columns.Add("出入库单号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("配件编码", typeof(string));
                        XlsTable.Columns.Add("车厂编码", typeof(string));
                        XlsTable.Columns.Add("配件名称", typeof(string));
                        XlsTable.Columns.Add("图号", typeof(string));
                        XlsTable.Columns.Add("规格", typeof(string));
                        XlsTable.Columns.Add("单位", typeof(string));
                        XlsTable.Columns.Add("数量", typeof(string));
                        XlsTable.Columns.Add("车型", typeof(string));
                        XlsTable.Columns.Add("是否赠品", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        TableRow["单据类型"] = dgRow.Cells["OrderPartType"].Value.ToString();
                        TableRow["出入库类型"] = dgRow.Cells["InOutType"].Value.ToString();
                        TableRow["出入库单号"] = dgRow.Cells["OrderNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["OrderDate"].Value.ToString();
                        TableRow["配件编码"] = dgRow.Cells["PartCode"].Value.ToString();
                        TableRow["车厂编码"] = dgRow.Cells["CarPartCode"].Value.ToString();
                        TableRow["配件名称"] = dgRow.Cells["PartName"].Value.ToString();
                        TableRow["图号"] = dgRow.Cells["DrawingNum"].Value.ToString();
                        TableRow["规格"] = dgRow.Cells["Spec"].Value.ToString();
                        TableRow["单位"] = dgRow.Cells["Unit"].Value.ToString();
                        TableRow["数量"] = dgRow.Cells["Count"].Value.ToString();
                        TableRow["车型"] = dgRow.Cells["VehicleModel"].Value.ToString();
                        if ((bool)((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).EditedFormattedValue)
                        {
                            TableRow["是否赠品"] = "是";
                        }
                        else
                        {
                            TableRow["是否赠品"] = "否";
                        }
                        TableRow["备注"] = dgRow.Cells["PartRemark"].Value.ToString();
                        XlsTable.Rows.Add(TableRow);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }



        /// <summary>
        /// 按其它收发货单双击单据查看单据详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInOutBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string InoutIdValue = gvInOutBillList.CurrentRow.Cells["InOutId"].Value.ToString();//获取其它收发货单ID
                    string WHName = gvInOutBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前其它收发单仓库名称
                    if (CombOrderType.SelectedValue.ToString() == InBill)
                    {
                        UCStockReceiptDetail UCReceiptBillDetails = new UCStockReceiptDetail(InoutIdValue, WHName);
                        base.addUserControl(UCReceiptBillDetails, "其它收货单-查看", "UCReceiptBillDetails" + InoutIdValue + "", this.Tag.ToString(), this.Name);
                    }
                    else if (CombOrderType.SelectedValue.ToString() == OutBill)
                    {
                        UCStockShippingDetail UCShippingBillDetails = new UCStockShippingDetail(InoutIdValue, WHName);
                        base.addUserControl(UCShippingBillDetails, "其它发货单-查看", "UCShippingBillDetails" + InoutIdValue + "", this.Tag.ToString(), this.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        ///按往来单位或者配件双击单据查看单据详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvInOutPartList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string InoutIdValue = gvInOutPartList.CurrentRow.Cells["InoutPartID"].Value.ToString();//获取其它收发货单ID
                    string WHName = gvInOutPartList.CurrentRow.Cells["PartWHName"].Value.ToString();//获取当前其它收发单仓库名称
                    if (CombOrderType.SelectedValue.ToString() == InBill)
                    {
                        UCStockReceiptDetail UCReceiptBillDetails = new UCStockReceiptDetail(InoutIdValue, WHName);
                        base.addUserControl(UCReceiptBillDetails, "其它收货单-查看", "UCReceiptBillDetails" + InoutIdValue + "", this.Tag.ToString(), this.Name);
                    }
                    else if (CombOrderType.SelectedValue.ToString() == OutBill)
                    {
                        UCStockShippingDetail UCShippingBillDetails = new UCStockShippingDetail(InoutIdValue, WHName);
                        base.addUserControl(UCShippingBillDetails, "其它发货单-查看", "UCShippingBillDetails" + InoutIdValue + "", this.Tag.ToString(), this.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary> 
        /// 选择公司事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ComBcom_name.SelectedValue.ToString()))
            {
                CommonFuncCall.BindDepartment(ComBorg_name, ComBcom_name.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindDepartment(ComBorg_name, "", "全部");
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");
            }
        }
        /// <summary> 
        /// 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ComBorg_name.SelectedValue.ToString()))
            {
                CommonFuncCall.BindHandle(ComBhandle_name, ComBorg_name.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");
            }
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtremark.Caption = string.Empty;
            ComBwhName.SelectedIndex = 0;
            CombOrderType.SelectedIndex = 0;
            Combinoutwhtype.SelectedIndex = 0;
            ComBinoutwhstatus.SelectedIndex = 0;
            ComBcom_name.SelectedIndex = 0;
            ComBorg_name.SelectedIndex = 0;
            ComBhandle_name.SelectedIndex = 0;
            dateTimeStart.Value = DateTime.Now.ToString();
            dateTimeEnd.Value = DateTime.Now.ToString();
        }

        /// <summary>
        /// 按其它收发货单查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string RecQueryWhere = QueryBillReceiptWhereCondition();//获取它收货单查询条件
            string ShpQueryWhere = QueryBillReceiptWhereCondition();//获取它发货单查询条件
            if (string.IsNullOrEmpty(RecQueryWhere) && string.IsNullOrEmpty(ShpQueryWhere)) return;
            GetInOutBillList(RecQueryWhere, ShpQueryWhere);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormInoutPage_PageIndexChanged(object sender, EventArgs e)
        {
            gvInOutBillList.Rows.Clear();//清空原数据
            string RecQueryWhere = "";//获取查询条件
            string ShpQueryWhere = "";//获取查询条件
            GetInOutBillList(RecQueryWhere, ShpQueryWhere);
        }

        /// <summary> 
        /// 选择配件编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmParts chooseParts = new frmParts();
                chooseParts.ShowDialog();
                if (!string.IsNullOrEmpty(chooseParts.PartsID))
                {
                    txtparts_code.Text = chooseParts.PartsCode;
                    txtparts_name.Caption = chooseParts.PartsName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary> 
        /// 选择配件类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType choosePartsType = new frmPartsType();
            choosePartsType.ShowDialog();
            if (!string.IsNullOrEmpty(choosePartsType.TypeID))
            {
                txtparts_type.Text = choosePartsType.TypeName;
            }
        }
        /// <summary> 
        /// 选择配件车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_cartype_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels chooseCarModel = new frmVehicleModels();
            chooseCarModel.ShowDialog();
            if (!string.IsNullOrEmpty(chooseCarModel.VMID))
            {
                txtparts_cartype.Text = chooseCarModel.VMName;
            }
        }
        /// <summary> 
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPartClear_Click(object sender, EventArgs e)
        {
            txtparts_code.Text = string.Empty;
            txtparts_name.Caption = string.Empty;
            txtparts_type.Text = string.Empty;
            txtparts_cartype.Text = string.Empty;
            txtdrawing_num.Caption = string.Empty;
            txtparts_brand.Caption = string.Empty;
            txtPartremark.Caption = string.Empty;

            dateTimePartStart.Value = DateTime.Now.ToString();
            dateTimePartEnd.Value = DateTime.Now.ToString();
            CombPartCompany.SelectedIndex = 0;

        }
        /// <summary> 
        /// 配件或往来单位查询其它收发货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPartSearch_Click(object sender, EventArgs e)
        {
            string RecQueryWhere = QueryPartReceiptWhereCondition();//获取其它收货单查询条件
            string ShpQueryWhere = QueryPartShippingWhereCondition();//获取其它发货单查询条件
            if (string.IsNullOrEmpty(RecQueryWhere) && string.IsNullOrEmpty(ShpQueryWhere)) return;
            GetPartList(RecQueryWhere, ShpQueryWhere);
        }
        /// <summary> 
        /// 按配件或往来单位分页查询其它收发货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            gvInOutPartList.Rows.Clear();//清空原数据
            string RecQueryWhere = "";//获取其它收货单查询条件
            string ShpQueryWhere = "";//获取其它发货单查询条件
            GetPartList(RecQueryWhere, ShpQueryWhere);
        }

        /// <summary>
        /// 其它收发货单查询记录
        /// </summary>
        private void GetInOutBillList(string RecWhereStr,string ShpWhereStr)
        {
            try
            {
                int InCount = 0;//其它收货单分页查询记录行数
                int OutCount = 0;//其它发货单分页查询记录行数
                int TotalCount = 0;//总记录行数
                //其它收货单数据表查询字段集
                StringBuilder sbBillField = new StringBuilder();
                sbBillField.AppendFormat("RptBillTb.{0},{1},{2},{3},RptBillTb.{4},PartAmount,PartMoney,{5},{6},{7},RptBillTb.{8},{9}", 
                ReceiptID,InTypeName, OrderNo, OrdDate, WareHouseName, OrgName, HandleName, OperatorName,BillRemk, InOrderStatus);
                //其它收货单数据表联合查询集
                StringBuilder sbRelationTable = new StringBuilder();
                sbRelationTable.AppendFormat("{0} as RptBillTb inner join {1} as RptPartTb on RptBillTb.{2}=RptPartTb.{2} "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as RptBillPartTb  on RptBillPartTb.{2}= RptBillTb.{2}",
                ReceiptBillTable, ReceiptPartTable, ReceiptID, PartCount, AccountMoney);

                DataTable InBillTable = DBHelper.GetTableByPage(InQueryLogMsg, sbRelationTable.ToString(), sbBillField.ToString(), RecWhereStr,
                "", " order by RptBillTb.create_time desc", winFormInoutPage.PageIndex, winFormInoutPage.PageSize, out InCount);//获取其它收货单表查询记录

                sbBillField.Clear();
                sbBillField.AppendFormat("ShpBillTb.{0},{1},{2},{3},ShpBillTb.{4},PartAmount,PartMoney,{5},{6},{7},ShpBillTb.{8},{9}", ShippingID,
                OutTypeName, OrderNo, OrdDate, WareHouseName,OrgName, HandleName, OperatorName, BillRemk, OutOrderStatus);
                //其它发货单数据表联合查询集
                sbRelationTable.Clear();
                sbRelationTable.AppendFormat("{0} as ShpBillTb inner join {1} as ShpPartTb on ShpBillTb.{2}=ShpPartTb.{2} " +
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} group by {0}.{2}) as ShpBillPartTb"+
                "  on ShpBillPartTb.{2}=ShpBillTb.{2}",
                ShippingBillTable,ShippingPartTable, ShippingID, PartCount, AccountMoney);

                DataTable OutBillTable = DBHelper.GetTableByPage(OutQueryLogMsg, sbRelationTable.ToString(), sbBillField.ToString(), ShpWhereStr,
                "", " order by ShpBillTb.create_time desc", winFormInoutPage.PageIndex, winFormInoutPage.PageSize, out OutCount);//获取其它发货单表查询记录

                //获取总记录行
                TotalCount = InCount + OutCount;
                winFormInoutPage.RecordCount = TotalCount;

                int SerialNum = 1;//记录行序号
                if (InBillTable.Rows.Count != 0)
                {
                    //把查询的其它收货单列表放入Gridview 
                    for (int i = 0; i < InBillTable.Rows.Count; i++)
                    {

                        DataGridViewRow dgRow = gvInOutBillList.Rows[gvInOutBillList.Rows.Add()];//添加新行项
                        dgRow.Cells["InOutId"].Value = InBillTable.Rows[i][ReceiptID].ToString();
                        dgRow.Cells["colIndex"].Value = SerialNum;
                        dgRow.Cells["BillNum"].Value = InBillTable.Rows[i][OrderNo].ToString();
                        DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(InBillTable.Rows[i][OrdDate]));//获取单据日期
                        dgRow.Cells["BillDate"].Value = OrdeDate.ToLongDateString();
                        dgRow.Cells["WHName"].Value = InBillTable.Rows[i][WareHouseName].ToString();
                        dgRow.Cells["InOutWhType"].Value = InBillTable.Rows[i][InTypeName].ToString();
                        dgRow.Cells["TotalCount"].Value = InBillTable.Rows[i][PartCount].ToString();
                        dgRow.Cells["TotalMoney"].Value = InBillTable.Rows[i][AccountMoney].ToString();
                        dgRow.Cells["DepartName"].Value = InBillTable.Rows[i][OrgName].ToString();
                        dgRow.Cells["HandlerName"].Value = InBillTable.Rows[i][HandleName].ToString();
                        dgRow.Cells["OpeName"].Value = InBillTable.Rows[i][OperatorName].ToString();
                        dgRow.Cells["Remarks"].Value = InBillTable.Rows[i][BillRemk].ToString();
                        dgRow.Cells["InOutState"].Value = InBillTable.Rows[i][InOrderStatus].ToString();
                        SerialNum++;//序号自增加

                    }
                }
                else if (OutBillTable.Rows.Count != 0)
                {
                    //把查询的其它发货单列表放入Gridview
                    for (int i = 0; i < OutBillTable.Rows.Count; i++)
                    {

                        DataGridViewRow dgRow = gvInOutBillList.Rows[gvInOutBillList.Rows.Add()];//添加新行项
                        dgRow.Cells["InOutId"].Value = OutBillTable.Rows[i][ShippingID].ToString();
                        dgRow.Cells["colIndex"].Value = SerialNum;
                        dgRow.Cells["BillNum"].Value = OutBillTable.Rows[i][OrderNo].ToString();
                        DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(OutBillTable.Rows[i][OrdDate]));//获取单据日期
                        dgRow.Cells["BillDate"].Value = OrdeDate.ToLongDateString();
                        dgRow.Cells["WHName"].Value = OutBillTable.Rows[i][WareHouseName].ToString();
                        dgRow.Cells["InOutWhType"].Value = OutBillTable.Rows[i][OutTypeName].ToString();
                        dgRow.Cells["TotalCount"].Value = OutBillTable.Rows[i][PartCount].ToString();
                        dgRow.Cells["TotalMoney"].Value = OutBillTable.Rows[i][AccountMoney].ToString();
                        dgRow.Cells["DepartName"].Value = OutBillTable.Rows[i][OrgName].ToString();
                        dgRow.Cells["HandlerName"].Value = OutBillTable.Rows[i][HandleName].ToString();
                        dgRow.Cells["OpeName"].Value = OutBillTable.Rows[i][OperatorName].ToString();
                        dgRow.Cells["Remarks"].Value = OutBillTable.Rows[i][BillRemk].ToString();
                        dgRow.Cells["InOutState"].Value = OutBillTable.Rows[i][OutOrderStatus].ToString();
                        SerialNum++;//序号自增加

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 按配件查询记录
        /// </summary>
        private void GetPartList(string RecWhereStr,string ShpWhereStr)
        {
            try
            {
                int InCount = 0;//其它收货单分页查询记录行数
                int OutCount = 0;//其它发货单分页查询记录行数
                int TotalCount = 0;//总记录行数
                //其它收货单数据表查询字段集
                StringBuilder sbPartField = new StringBuilder();
                sbPartField.AppendFormat("RptBillTb.{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},PartAmount,{11},{12},{13},RptPartTb.{14}",
                ReceiptID, WareHouseName,InTypeName, OrderNo, OrdDate, PartsNum, CarFactoryCode, PartsName, DrawNum, PartSpec, UnitName,//10
                VmName, PartsBrand, PartGift, PartRemk);
                //其它收货单数据表联合查询集
                StringBuilder sbRelationTable = new StringBuilder();
                sbRelationTable.AppendFormat("{0} as RptBillTb  inner join {1} as RptPartTb on RptBillTb.{5}=RptPartTb.{5}"+
                " inner join (select {0}.{5},sum(10) as PartAmount from {0} inner join {1} on {0}.{5}={1}.{5} group by {0}.{5})"+
                " as RptBillPartTb  on  RptBillPartTb.{5}=RptBillTb.{5} inner join {2} on RptPartTb.{7}={2}.{8}" +
                " inner join {3} on {2}.{6}={3}.{6} inner join {4} on {3}.{9}={4}.{9}",
                ReceiptBillTable, ReceiptPartTable, PartsTable, PartForVhcTable, VehModelTable,//4
                ReceiptID, PartID, PartsNum, PartsSerCode, VehicleTypeId, PartCount);//10
                //其它收货单表查询
                DataTable InWHTable = DBHelper.GetTableByPage(InQueryLogMsg, sbRelationTable.ToString(), sbPartField.ToString(), RecWhereStr,
                "", " order by RptBillTb.create_time desc", winFormInoutPartPager.PageIndex, winFormInoutPartPager.PageSize, out InCount);//获取其它收货单表查询记录


                //其它发货单数据表查询字段集
                sbPartField.Clear();//清除其它收货单字段集
                sbPartField.AppendFormat("ShpBillTb.{0},ShpBillTb.{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},PartAmount,{11},{12},{13},ShpPartTb.{14}",
                ShippingID, WareHouseName,OutTypeName, OrderNo, OrdDate, PartsNum, CarFactoryCode, PartsName, DrawNum, PartSpec, UnitName,//10
                VmName, PartsBrand, PartGift, PartRemk);
                //其它发货单数据表联合查询集
                sbRelationTable.Clear();//清除其它收货单数据表联合查询集
                sbRelationTable.AppendFormat("{0} as ShpBillTb inner join {1} as ShpPartTb on ShpBillTb.{5}=ShpPartTb.{5}"+
                " inner join (select {0}.{5},sum({10}) as PartAmount from {0} inner join {1} on {0}.{5}={1}.{5} group by {0}.{5})"+
                " as ShpBillPartTb on ShpBillPartTb.{5}=ShpBillTb.{5} inner join {2} on ShpPartTb.{7}={2}.{8}" +
                " inner join {3} on {2}.{6}={3}.{6} inner join {4} on {3}.{9}={4}.{9}",
                ShippingBillTable, ShippingPartTable, PartsTable, PartForVhcTable, VehModelTable,//4
                ShippingID, PartID, PartsNum, PartsSerCode, VehicleTypeId, PartCount);//10
                //其它发货单表查询
                DataTable OutWHTable = DBHelper.GetTableByPage(OutQueryLogMsg, sbRelationTable.ToString(), sbPartField.ToString(), ShpWhereStr,
                "", " order by ShpBillTb.create_time desc", winFormInoutPartPager.PageIndex, winFormInoutPartPager.PageSize, out OutCount);//获取其它发货单表查询记录

                //获取总记录行
                TotalCount = InCount + OutCount;
                winFormInoutPartPager.RecordCount = TotalCount;//获取总记录行

                //把查询的其它收发货单列表放入Gridview
                int SerialNum = 1;//记录行序号
                bool Gift = true;
                bool NoGift = false;
                if (InWHTable.Rows.Count != 0)//其它收货单记录显示
                {
                    for (int i = 0; i < InWHTable.Rows.Count; i++)
                    {

                        DataGridViewRow dgRow = gvInOutPartList.Rows[gvInOutPartList.Rows.Add()];//创建新行项
                        dgRow.Cells["InoutPartID"].Value = InWHTable.Rows[i][ReceiptID].ToString();
                        dgRow.Cells["PartWHName"].Value = InWHTable.Rows[i][WareHouseName].ToString();
                        dgRow.Cells["colIndex"].Value = SerialNum;
                        dgRow.Cells["OrderPartType"].Value = InBill;
                        dgRow.Cells["InOutType"].Value = InWHTable.Rows[i][InTypeName].ToString();
                        dgRow.Cells["OrderNum"].Value = InWHTable.Rows[i][OrderNo].ToString();
                        DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(InWHTable.Rows[i][OrdDate]));//获取单据日期
                        dgRow.Cells["OrderDate"].Value = OrdeDate.ToLongDateString();
                        dgRow.Cells["PartCode"].Value = InWHTable.Rows[i][PartsNum].ToString();
                        dgRow.Cells["CarPartCode"].Value = InWHTable.Rows[i][CarFactoryCode].ToString();
                        dgRow.Cells["PartName"].Value = InWHTable.Rows[i][PartsName].ToString();
                        dgRow.Cells["DrawingNum"].Value = InWHTable.Rows[i][DrawNum].ToString();
                        dgRow.Cells["Spec"].Value = InWHTable.Rows[i][PartSpec].ToString();
                        dgRow.Cells["Unit"].Value = InWHTable.Rows[i][UnitName].ToString();
                        dgRow.Cells["Count"].Value = InWHTable.Rows[i][PartCount].ToString();
                        dgRow.Cells["VehicleModel"].Value = InWHTable.Rows[i][VmName].ToString();
                        dgRow.Cells["partsbrand"].Value = InWHTable.Rows[i][PartsBrand].ToString();
                        if (InWHTable.Rows[i][BillRemk].ToString() == "1")
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = Gift;
                        }
                        else
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = NoGift;
                        }
                        dgRow.Cells["PartRemark"].Value = InWHTable.Rows[i][PartRemk].ToString();
                        SerialNum++;//序号自增加
                    }
                }

                else if (OutWHTable.Rows.Count != 0)//其它发货单记录显示
                {
                    for (int i = 0; i < OutWHTable.Rows.Count; i++)
                    {

                        DataGridViewRow dgRow = gvInOutPartList.Rows[gvInOutPartList.Rows.Add()];//创建新行项
                        dgRow.Cells["InoutPartID"].Value = OutWHTable.Rows[i][ShippingID].ToString();
                        dgRow.Cells["PartWHName"].Value = OutWHTable.Rows[i][WareHouseName].ToString();
                        dgRow.Cells["colIndex"].Value = SerialNum;
                        dgRow.Cells["OrderPartType"].Value = OutBill;
                        dgRow.Cells["InOutType"].Value = OutWHTable.Rows[i][OutTypeName].ToString();
                        dgRow.Cells["OrderNum"].Value = OutWHTable.Rows[i][OrderNo].ToString();
                        DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(OutWHTable.Rows[i][OrdDate]));//获取单据日期
                        dgRow.Cells["OrderDate"].Value = OrdeDate.ToLongDateString();
                        dgRow.Cells["PartCode"].Value = OutWHTable.Rows[i][PartsNum].ToString();
                        dgRow.Cells["CarPartCode"].Value = OutWHTable.Rows[i][CarFactoryCode].ToString();
                        dgRow.Cells["PartName"].Value = OutWHTable.Rows[i][PartsName].ToString();
                        dgRow.Cells["DrawingNum"].Value = OutWHTable.Rows[i][DrawNum].ToString();
                        dgRow.Cells["Spec"].Value = OutWHTable.Rows[i][PartSpec].ToString();
                        dgRow.Cells["Unit"].Value = OutWHTable.Rows[i][UnitName].ToString();
                        dgRow.Cells["Count"].Value = OutWHTable.Rows[i][PartCount].ToString();
                        dgRow.Cells["VehicleModel"].Value = OutWHTable.Rows[i][VmName].ToString();
                        dgRow.Cells["partsbrand"].Value = OutWHTable.Rows[i][PartsBrand].ToString();
                        if (OutWHTable.Rows[i][BillRemk].ToString() == "1")
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = Gift;
                        }
                        else
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = NoGift;
                        }
                        dgRow.Cells["PartRemark"].Value = OutWHTable.Rows[i][PartRemk].ToString();
                        SerialNum++;//序号自增加


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 按其它收货单查询的条件
        /// </summary>
        private string QueryBillReceiptWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString() );//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString() );//结束时间
                if (!string.IsNullOrEmpty(ComBwhName.Text.ToString()))
                {
                    Str_Where += " and RptBillTb.wh_name = '" + ComBwhName.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combinoutwhtype.Text.ToString()) && CombOrderType.Text.ToString() == InBill)
                {
                    Str_Where += " and in_wh_type_name = '" + Combinoutwhtype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combinoutwhtype.Text.ToString()) && string.IsNullOrEmpty(CombOrderType.Text.ToString()))
                {
                    Str_Where += " and in_wh_type_name = '" + Combinoutwhtype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBinoutwhstatus.Text.ToString()) && CombOrderType.Text.ToString() == InBill)
                {
                    Str_Where += " and in_status_name='" + ComBinoutwhstatus.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBinoutwhstatus.Text.ToString()))
                {
                    Str_Where += " and in_status_name='" + ComBinoutwhstatus.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcom_name.Text.ToString()))
                {
                    Str_Where += " and com_name='" + ComBcom_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorg_name.Text.ToString()))
                {
                    Str_Where += " and org_name='" + ComBorg_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBhandle_name.Text.ToString()))
                {
                    Str_Where += " and handle_name='" + ComBhandle_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and RptBillTb.remark='" + txtremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.create_time>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.create_time<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and in_status_name='" + InoutBillStatus + "'";
                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }
        /// <summary>
        /// 按其它发货单查询的条件
        /// </summary>
        private string QueryBillShippingWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(ComBwhName.Text.ToString()))
                {
                    Str_Where += " and ShpBillTb.wh_name = '" + ComBwhName.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combinoutwhtype.Text.ToString()) && CombOrderType.Text.ToString() == OutBill)
                {
                    Str_Where += " and out_wh_type_name='" + Combinoutwhtype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combinoutwhtype.Text.ToString()) && string.IsNullOrEmpty(CombOrderType.Text.ToString()))
                {
                    Str_Where += " and out_wh_type_name='" + Combinoutwhtype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBinoutwhstatus.Text.ToString()) && CombOrderType.Text.ToString() == OutBill)
                {
                    Str_Where += " and out_status_name='" + ComBinoutwhstatus.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBinoutwhstatus.Text.ToString()))
                {
                    Str_Where += " and out_status_name='" + ComBinoutwhstatus.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcom_name.Text.ToString()))
                {
                    Str_Where += " and com_name='" + ComBcom_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorg_name.Text.ToString()))
                {
                    Str_Where += " and org_name='" + ComBorg_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBhandle_name.Text.ToString()))
                {
                    Str_Where += " and handle_name='" + ComBhandle_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and ShpBillTb.remark='" + txtremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value.ToString() != null)
                {

                    Str_Where += " and ShpBillTb.create_time>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value.ToString() != null)
                {

                    Str_Where += " and ShpBillTb.create_time<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) >= Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于等于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and out_status_name='" + InoutBillStatus + "'";
                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }

        /// <summary>
        /// 按配件查询的条件
        /// </summary>
        private string QueryPartReceiptWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(txtparts_code.Text.ToString()))
                {
                    Str_Where += " and parts_num = '" + txtparts_code.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_name.Caption.ToString()))
                {
                    Str_Where += " and parts_name = '" + txtparts_name.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_type.Text.ToString()))
                {
                    Str_Where += " and parts_type='" + txtparts_type.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_cartype.Text.ToString()))
                {
                    Str_Where += " and vm_name='" + txtparts_cartype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtdrawing_num.Caption.ToString()))
                {
                    Str_Where += " and drawing_num='" + txtdrawing_num.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_brand.Caption.ToString()))
                {
                    Str_Where += " and parts_brand='" + txtparts_brand.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombPartCompany.Text.ToString()))
                {
                    Str_Where += " and com_name='" + CombPartCompany.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtPartremark.Caption.ToString()))
                {
                    Str_Where += " and RptPartTb.remark='" + txtPartremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.create_time>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.create_time<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (!string.IsNullOrEmpty(CombPartCompany.SelectedValue.ToString()))
                {
                    Str_Where += " and handle_name='" + CombPartCompany.SelectedValue.ToString() + "'";
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and in_status_name='" + InoutBillStatus + "'";
                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return string.Empty;
            }
        }
        /// <summary>
        /// 按配件查询的条件
        /// </summary>
        private string QueryPartShippingWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(txtparts_code.Text.ToString()))
                {
                    Str_Where += " and parts_num = '" + txtparts_code.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_name.Caption.ToString()))
                {
                    Str_Where += " and parts_name = '" + txtparts_name.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_type.Text.ToString()))
                {
                    Str_Where += " and parts_type='" + txtparts_type.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_cartype.Text.ToString()))
                {
                    Str_Where += " and vm_name='" + txtparts_cartype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtdrawing_num.Caption.ToString()))
                {
                    Str_Where += " and drawing_num='" + txtdrawing_num.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_brand.Caption.ToString()))
                {
                    Str_Where += " and parts_brand='" + txtparts_brand.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombPartCompany.Text.ToString()))
                {
                    Str_Where += " and com_name='" + CombPartCompany.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtPartremark.Caption.ToString()))
                {
                    Str_Where += " and ShpPartTb.remark='" + txtPartremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value.ToString() != null)
                {

                    Str_Where += " and ShpBillTb.create_time>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value.ToString() != null)
                {

                    Str_Where += " and ShpBillTb.create_time<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (!string.IsNullOrEmpty(CombPartCompany.SelectedValue.ToString()))
                {
                    Str_Where += " and handle_name='" + CombPartCompany.SelectedValue.ToString() + "'";
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {                 
                    Str_Where += " and out_status_name='" + InoutBillStatus + "'";
                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }
    }
}
