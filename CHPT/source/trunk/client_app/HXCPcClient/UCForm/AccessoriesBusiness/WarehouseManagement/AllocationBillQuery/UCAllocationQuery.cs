using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill;
using SYSModel;
using ServiceStationClient.Skin;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBillQuery
{
    public partial class UCAllocationQuery : UCBase
    {


        #region 全局变量
        private string InOutBillTable = "tb_parts_stock_inout";//出入库单表
        private string InOutPartTable = "tb_parts_stock_inout_p";//出入库单配件表
        private string SupplierTable = "tb_supplier";//供应商表
        private string CustomerTable = "tb_customer";//往来单位表
        private string ContactTable = "tb_contacts";//联系人表
        private string VehModelTable = "tb_vehicle_models";//车型档案表
        private string PartsTable = "tb_parts";//配件档案表
        private string PartsSerCode = "ser_parts_code";//配件表配件编码
        private string PartForVhcTable = "tb_parts_for_vehicle";//配件适用车型表
        private string PartID = "parts_id";//配件ID
        private string VehicleTypeId = "vm_id";//车型ID
        private string RelationContactTable = "tr_base_contacts";//主数据和联系人关联表
        private string ContactId = "cont_id";//联系人ID
        private string RelObjId = "relation_object_id";//关联对像ID
        private string InOutID = "stock_inout_id";//出入库单表主键
        private string SupplierID = "sup_id";//供应商主键ID
        private string SupplierCode = "sup_code";//供应商编码
        private string SupFullName = "sup_full_name";//供应商全名称
        private string CustomerID = "cust_id";//往来单位主键ID
        private string Custcode = "cust_code";//往来单位编码
        private string CustName = "cust_name";//往来单位名称
        private string WareHouseName = "wh_name";//仓库名称
        private string InOutQueryLogMsg = "查询出入库单表信息";//出入库单表操作日志
        private string InBill = "入库单";
        private string OutBill = "出库单";
        private const string ExportXlsName = "出入库单";
        private int SearchFlag = 0;//查询标志
        private string Submited = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//已提交
        private string Verify = DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true);//已审核
        private string VPartOrBusUntView = "v_StockInoutBillPartMsg";//往来单位或客户查询视图
        //出入库单表字段
        private const string OrderTypeName = "order_type_name";
        private const string BillingTypeName = "billing_type_name";
        private const string OrderNo = "order_num";
        private const string OrdDate = "order_date";
        private const string BussinUnit = "bussiness_units";
        private const string ArrivalPlace = "arrival_place";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string BillRemk = "remark";
        private const string OrderStatus = "order_status";

        //出入库单配件表字段
        private const string PartsNum = "parts_code";
        private const string PartsName = "parts_name";
        private const string CarFactoryCode = "car_parts_code";//车厂配件编码
        private const string DrawNum = "drawing_num";//图号
        private const string PartSpec = "model";//规格
        private const string UnitName = "unit_name";//单位
        private const string PartCount = "counts";
        private const string VmName = "vm_name";//车型名称
        private const string PartsBrand = "parts_brand";//品牌
        private const string PartGift = "is_gift";//赠品
        private const string PartRemk = "remark";


        #endregion
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public UCAllocationQuery()
        {

            InitializeComponent();
            try
            {
                base.ExportEvent += new ClickHandler(UCAllocationBillAddOrEdit_ExportEvent);//导出
                #region 窗体容器控件自适应大小
                //tabControlEx1自适应大小
                this.TabCtrlAllocationBill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                          | System.Windows.Forms.AnchorStyles.Left)
                          | System.Windows.Forms.AnchorStyles.Right)));


                #region 按出入库单查询界面控件的自适应
                this.panelExtend2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
                this.gvAllocBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                           | System.Windows.Forms.AnchorStyles.Bottom));
                #endregion

                #region 按配件或往来单位查询界面控件的自适应
                this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
                this.gvAllocPartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                           | System.Windows.Forms.AnchorStyles.Bottom));
                #endregion
                #endregion
                UIAssistants.SetButtonStyle4QueryAndClear(btnBillSearch, btnBillClear);//美化查询和清除按钮控件
                UIAssistants.SetButtonStyle4QueryAndClear(btnPartSearch, btnPartClear);//美化查询和清除按钮控件
                DataGridViewEx.SetDataGridViewStyle(gvAllocBillList, Remarks);//美化出入库单表格控件
                DataGridViewEx.SetDataGridViewStyle(gvAllocPartList, Remarks);//美化配件或往来单位表格控件
                gvAllocBillList.AutoGenerateColumns = false;
                gvAllocPartList.AutoGenerateColumns = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCAllocationQuery_Load(object sender, EventArgs e)
        {
            try
            {
               
                base.SetBtnStatus(WindowStatus.View);// 根据窗体状态更改控件状态
                //根据出入库单查询出入库单
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                CommonFuncCall.BindAllocationBillType(Comborder_type_name, true, "请选择");//单据类型   
                CommonFuncCall.BindInStockBillingType(Combbilling_type_name, true, "请选择"); //开单类型         
                CommonFuncCall.BindWarehouse(CombWarehouse, "请选择"); //获取仓库名称
                string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
                CommonFuncCall.BindCompany(CombCompany, "全部");//选择公司名称
                CommonFuncCall.BindDepartment(CombDepartment, com_id, "全部");//选择部门名称
                CommonFuncCall.BindHandle(Combhandle, "", "全部");//选择经办人         
                CommonFuncCall.BindHandle(CombOperator,"", "全部");//选择操作人 


                //根据配件或往来单位查询出入库单
                dateTimePartStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimePartEnd.Value = DateTime.Now.ToString();
                CommonFuncCall.BindCompany(CombCompany, "全部");//公司ID
                CommonFuncCall.BindCompany(CombPartCompany, "全部");
                CommonFuncCall.BindDepartment(CombDepartment, "", "全部");
                CommonFuncCall.BindHandle(Combhandle, "", "全部");
                CommonFuncCall.BindIs_Gift(Combis_gift, true);//是否赠品

                InitBillQuery();//出入库单查询 

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
            try
            {
                DataTable XlsTable = null;//导出的数据表格
                if (TabCtrlAllocationBill.SelectedTab == tabPgBill)
                {//导出按出入库单查询表格
                    if (gvAllocBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                    {
                        MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    XlsTable = new DataTable();//导出的数据表格
                    //创建表列项
                    XlsTable.Columns.Add("单据类型", typeof(string));
                    XlsTable.Columns.Add("开单类型", typeof(string));
                    XlsTable.Columns.Add("单据编号", typeof(string));
                    XlsTable.Columns.Add("单据日期", typeof(string));
                    XlsTable.Columns.Add("仓库", typeof(string));
                    XlsTable.Columns.Add("往来单位", typeof(string));
                    XlsTable.Columns.Add("数量", typeof(string));
                    XlsTable.Columns.Add("到货地点", typeof(string));
                    XlsTable.Columns.Add("部门", typeof(string));
                    XlsTable.Columns.Add("经办人", typeof(string));
                    XlsTable.Columns.Add("操作人", typeof(string));
                    XlsTable.Columns.Add("备注", typeof(string));
                    foreach (DataGridViewRow dgRow in gvAllocBillList.Rows)
                    {
                        bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                        if (SelectFlag)
                        {
                            DataRow TableRow = XlsTable.NewRow();//创建表行项

                            TableRow["单据类型"] = dgRow.Cells["OrderType"].Value.ToString();
                            TableRow["开单类型"] = dgRow.Cells["BillType"].Value.ToString();
                            TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                            TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                            TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                            TableRow["往来单位"] = dgRow.Cells["BusinessUnit"].Value.ToString();
                            TableRow["数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                            TableRow["到货地点"] = dgRow.Cells["ArrivePlace"].Value.ToString();
                            TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                            TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                            TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                            TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                            XlsTable.Rows.Add(TableRow);
                        }

                    }
                    if (XlsTable.Rows.Count == 0)
                    {
                        MessageBoxEx.Show("请您选择要导出的单据记录行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ImportExportExcel.NPOIExportExcelFile(XlsTable, ExportXlsName);//生成Excel表格文件

                }
                else if (TabCtrlAllocationBill.SelectedTab == tabPgPartBusUnt)
                {//导出按配件或往来单位查询表格
                    if (gvAllocPartList.Rows.Count == 0)
                    {
                        MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    XlsTable = new DataTable();//导出的数据表格
                    //创建表列
                    XlsTable.Columns.Add("单据类型", typeof(string));
                    XlsTable.Columns.Add("开单类型", typeof(string));
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
                    XlsTable.Columns.Add("往来单位编码", typeof(string));
                    XlsTable.Columns.Add("往来单位名称", typeof(string));
                    XlsTable.Columns.Add("备注", typeof(string));
                    foreach (DataGridViewRow dgRow in gvAllocPartList.Rows)
                    {
                        bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colpartcheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                        if (SelectFlag)
                        {
                            DataRow TableRow = XlsTable.NewRow();//创建表行项
                            TableRow["单据类型"] = dgRow.Cells["OrderPartType"].Value.ToString();
                            TableRow["开单类型"] = dgRow.Cells["BillingType"].Value.ToString();
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
                            TableRow["往来单位编码"] = dgRow.Cells["BusinessUnitCode"].Value.ToString();
                            TableRow["往来单位名称"] = dgRow.Cells["BussinessunitName"].Value.ToString();
                            TableRow["备注"] = dgRow.Cells["PartRemark"].Value.ToString();
                            XlsTable.Rows.Add(TableRow);
                        }
                    }
                    if (XlsTable.Rows.Count == 0)
                    {
                        MessageBoxEx.Show("请您选择要导出的单据记录行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ImportExportExcel.NPOIExportExcelFile(XlsTable, ExportXlsName);//生成Excel表格文件
                }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
       

        /// <summary>
        /// 按出入库单查询双击查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAllocBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string InOutIdValue = gvAllocBillList.CurrentRow.Cells["Inout_Id"].Value.ToString();//获取出入库单ID
                    UCAllocationBillDetails UCAllocBillDetails = new UCAllocationBillDetails(InOutIdValue);
                    base.addUserControl(UCAllocBillDetails, "出入库单-查看", "UCAllocBillDetails" + InOutIdValue + "", this.Tag.ToString(), this.Name);
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
        private void gvAllocPartList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string InOutIdValue = gvAllocPartList.CurrentRow.Cells["InOutPartID"].Value.ToString();//获取出入库单ID
                    string WHName = gvAllocPartList.CurrentRow.Cells["PartWHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCAllocationBillDetails UCAllocPartDetails = new UCAllocationBillDetails(InOutIdValue);
                    base.addUserControl(UCAllocPartDetails, "出入库单-查看", "UCAllocPartDetails" + InOutIdValue + "", this.Tag.ToString(), this.Name);
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
            if (!string.IsNullOrEmpty(CombCompany.SelectedValue.ToString()))
            {
                CommonFuncCall.BindDepartment(CombDepartment, CombCompany.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindDepartment(CombDepartment, "", "全部");
                CommonFuncCall.BindHandle(Combhandle, "", "全部");
                CommonFuncCall.BindHandle(CombOperator, "", "全部");//选择操作人 
            }
        }
        /// <summary> 
        /// 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CombDepartment.SelectedValue.ToString()))
            {
                CommonFuncCall.BindHandle(Combhandle, CombDepartment.SelectedValue.ToString(), "全部");
                CommonFuncCall.BindHandle(CombOperator, CombDepartment.SelectedValue.ToString(), "全部");//选择操作人 
            }
            else
            {
                CommonFuncCall.BindHandle(Combhandle, "", "全部");
                CommonFuncCall.BindHandle(CombOperator, "", "全部");//选择操作人 
            }
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBillNum.Caption = string.Empty;
            txtBusinUnt_code.Text = string.Empty;
            txtBusinUnt_name.Caption = string.Empty;
            txtremark.Caption = string.Empty;

            Comborder_type_name.SelectedIndex = 0;
            Combbilling_type_name.SelectedIndex = 0;
            CombWarehouse.SelectedIndex = 0;
            CombOperator.SelectedIndex = 0;
            CombCompany.SelectedIndex = 0;
            CombDepartment.SelectedIndex = 0;
            Combhandle.SelectedIndex = 0;
            dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// 按出入库单查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gvAllocBillList.Rows.Clear();
            string QueryWhere = QueryBillWhereCondition();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetAllocBillList(QueryWhere);
        }
        /// <summary> 
        /// 按出入库单分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormBillPager_PageIndexChanged(object sender, EventArgs e)
        {
            InitBillQuery();//出入库单查询 
        }
        /// <summary>
        /// 初始化出入库单查询
        /// </summary>
        private void InitBillQuery()
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetAllocBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvAllocBillList.Rows.Clear();//清除以前的查询结果
                    string QueryWhere = "";//获取查询条件
                    GetAllocBillList(QueryWhere);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }    

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
        /// 选择往来单位编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBusinUnt_code_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                FrmBusinessUints chooseBusinUnt = new FrmBusinessUints();
                chooseBusinUnt.ShowDialog();
                if (!string.IsNullOrEmpty(chooseBusinUnt.BusUntCode))
                {
                    txtBusinUnt_code.Text = chooseBusinUnt.BusUntCode;
                    txtBusinUnt_name.Caption = chooseBusinUnt.BusUntName;
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
            try
            {
                frmPartsType choosePartsType = new frmPartsType();
                choosePartsType.ShowDialog();
                if (!string.IsNullOrEmpty(choosePartsType.TypeID))
                {
                    txtparts_type.Text = choosePartsType.TypeName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary> 
        /// 选择配件车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_cartype_ChooserClick(object sender, EventArgs e)
        {

            try
            {
                frmVehicleModels chooseCarModel = new frmVehicleModels();
                chooseCarModel.ShowDialog();
                if (!string.IsNullOrEmpty(chooseCarModel.VMID))
                {
                    txtparts_cartype.Text = chooseCarModel.VMName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 选择联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcontacts_ChooserClick(object sender, EventArgs e)
        {
            frmContacts ChooseContact = new frmContacts();
            ChooseContact.ShowDialog();
            if (!string.IsNullOrEmpty(ChooseContact.contID))
            {
                txtcontacts.Text = ChooseContact.contName;
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
            txtBusinUnt_code.Text = string.Empty;
            txtBusinUnt_name.Caption = string.Empty;
            txtparts_type.Text = string.Empty;
            txtparts_cartype.Text = string.Empty;
            txtcontacts.Text = string.Empty;
            txtPartPhone.Caption = string.Empty;
            txtdrawing_num.Caption = string.Empty;
            txtparts_brand.Caption = string.Empty;
            txtPartremark.Caption = string.Empty;
            Combis_gift.SelectedIndex = 0;
            dateTimePartStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
            dateTimePartEnd.Value = DateTime.Now.ToShortDateString();
            CombPartCompany.SelectedIndex = 0;

        }
        /// <summary> 
        /// 配件或往来单位查询出入库单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPartSearch_Click(object sender, EventArgs e)
        {
            gvAllocPartList.Rows.Clear();//清除以前的记录行项
            string QueryWhere = QueryPartOrBusUntWhereCondition();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetPartOrBusUntList(QueryWhere);

        }
        /// <summary> 
        /// 按配件或往来单位分页查询出入库单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetPartOrBusUntList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvAllocPartList.Rows.Clear();//清除以前的记录行项
                    string QueryWhere = "";//获取查询条件
                    GetPartOrBusUntList(QueryWhere);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 按出入库单查询记录
        /// </summary>
        private void GetAllocBillList(string WhereStr)
        {
            try
            {
                int RecCount = 0;//分页查询记录行数
                //数据表查询字段集
                StringBuilder sbBillField = new StringBuilder();
                sbBillField.AppendFormat("IOBillTb.{0},{1},{2},{3},{4},{5},{6},{7},PartAmount,{8},{9},IOBillTb.{10}", 
                InOutID, OrderTypeName,BillingTypeName, OrderNo, OrdDate, BussinUnit, ArrivalPlace,OrgName, 
                HandleName, OperatorName, BillRemk);
                //数据表联合查询集
                StringBuilder sbRelationTable = new StringBuilder();
                sbRelationTable.AppendFormat("{0} as IOBillTb  "+
                " inner join (select {0}.{2},sum({3}) as PartAmount from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as IOBillPartTb  on IOBillPartTb.{2}=IOBillTb.{2}", 
                InOutBillTable,InOutPartTable, InOutID, PartCount);

                DataTable InoutBillTable = DBHelper.GetTableByPage(InOutQueryLogMsg, sbRelationTable.ToString(), sbBillField.ToString(), WhereStr,
                "", " IOBillTb.create_time desc", winFormBillPager.PageIndex, winFormBillPager.PageSize, out RecCount);//获取出入库单表查询记录
                winFormBillPager.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的出入库单据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //把查询的出入库单列表放入Gridview
                for (int i = 0; i < InoutBillTable.Rows.Count; i++)
                {


                        DataGridViewRow dgRow=gvAllocBillList.Rows[gvAllocBillList.Rows.Add()];//创建新行项
                        dgRow.Cells["Inout_Id"].Value = InoutBillTable.Rows[i][InOutID].ToString();
                        dgRow.Cells["OrderType"].Value = InoutBillTable.Rows[i][OrderTypeName].ToString();
                        dgRow.Cells["BillType"].Value = InoutBillTable.Rows[i][BillingTypeName].ToString();
                        dgRow.Cells["BillNum"].Value = InoutBillTable.Rows[i][OrderNo].ToString();
                        DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(InoutBillTable.Rows[i][OrdDate].ToString()));
                        dgRow.Cells["BillDate"].Value = OrdeDate.ToLongDateString();//单据日期
                        dgRow.Cells["BusinessUnit"].Value = InoutBillTable.Rows[i][BussinUnit].ToString();
                        dgRow.Cells["ArrivePlace"].Value = InoutBillTable.Rows[i][ArrivalPlace].ToString();
                        dgRow.Cells["TotalCount"].Value = InoutBillTable.Rows[i]["PartAmount"].ToString();
                        dgRow.Cells["DepartName"].Value = InoutBillTable.Rows[i][OrgName].ToString();
                        dgRow.Cells["HandlerName"].Value = InoutBillTable.Rows[i][HandleName].ToString();
                        dgRow.Cells["OpeName"].Value = InoutBillTable.Rows[i][OperatorName].ToString();
                        dgRow.Cells["Remarks"].Value = InoutBillTable.Rows[i][BillRemk].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 按配件或者往来单位查询记录
        /// </summary>
        private void GetPartOrBusUntList(String WhereStr)
        {
            try
            {
                int RecCount = 0;//分页查询记录行数

                DataTable InoutTable = DBHelper.GetTableByPage(InOutQueryLogMsg, VPartOrBusUntView, "*", WhereStr,
                "", "create_time desc", winFormBillPager.PageIndex, winFormBillPager.PageSize, out RecCount);//获取出入库单表查询记录
                winFormBillPager.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的出入库单据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //把查询的出入库单列表放入Gridview
                bool Gift = true;
                bool NoGift = false;
                for (int i = 0; i < InoutTable.Rows.Count; i++)
                {

                    DataGridViewRow dgRow = gvAllocPartList.Rows[gvAllocPartList.Rows.Add()];//创建新行项
                        dgRow.Cells["InOutPartID"].Value = InoutTable.Rows[i][InOutID].ToString();
                        dgRow.Cells["PartWHName"].Value = InoutTable.Rows[i][WareHouseName].ToString();
                        dgRow.Cells["OrderPartType"].Value = InoutTable.Rows[i][OrderTypeName].ToString();
                        dgRow.Cells["BillingType"].Value = InoutTable.Rows[i][BillingTypeName].ToString();
                        dgRow.Cells["OrderNum"].Value = InoutTable.Rows[i][OrderNo].ToString();
                        DateTime BilDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(InoutTable.Rows[i][OrdDate].ToString()));
                        dgRow.Cells["OrderDate"].Value = BilDate.ToLongDateString();//单据日期
                        dgRow.Cells["PartCode"].Value = InoutTable.Rows[i][PartsNum].ToString();
                        dgRow.Cells["CarPartCode"].Value = InoutTable.Rows[i][CarFactoryCode].ToString();
                        dgRow.Cells["PartName"].Value = InoutTable.Rows[i][PartsName].ToString();
                        dgRow.Cells["DrawingNum"].Value = InoutTable.Rows[i][DrawNum].ToString();
                        dgRow.Cells["Spec"].Value = InoutTable.Rows[i][PartSpec].ToString();
                        dgRow.Cells["Unit"].Value = InoutTable.Rows[i][UnitName].ToString();
                        dgRow.Cells["Count"].Value = InoutTable.Rows[i][PartCount].ToString();
                        dgRow.Cells["VehicleModel"].Value = InoutTable.Rows[i][VmName].ToString();
                        dgRow.Cells["PartBrand"].Value = InoutTable.Rows[i][PartsBrand].ToString();
                        if (InoutTable.Rows[i][OrderTypeName].ToString() == InBill)
                        {//入库单获取显示往来单位编号为供应商编号
                            dgRow.Cells["BusinessUnitCode"].Value = InoutTable.Rows[i][SupplierCode].ToString();
                        }
                        else if (InoutTable.Rows[i][OrderTypeName].ToString() == OutBill)
                        {//出库单获取显示往来单位编号为往来单位编号
                            dgRow.Cells["BusinessUnitCode"].Value = InoutTable.Rows[i][Custcode].ToString();
                        }
                        dgRow.Cells["BussinessunitName"].Value = InoutTable.Rows[i][BussinUnit].ToString();

                        if (InoutTable.Rows[i][BillRemk].ToString() == "1")
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = Gift;
                        }
                        else
                        {
                            ((DataGridViewCheckBoxCell)dgRow.Cells["isgift"]).Value = NoGift;
                        }
                        dgRow.Cells["PartRemark"].Value = InoutTable.Rows[i][PartRemk].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 按出入库单查询的条件
        /// </summary>
        private string QueryBillWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString() );//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and order_type_name = '" + Comborder_type_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combbilling_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and billing_type_name = '" + Combbilling_type_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombWarehouse.SelectedValue.ToString()))
                {
                    Str_Where += " and IOBillTb.wh_name = '" + CombWarehouse.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtBillNum.Caption.ToString()))
                {
                    Str_Where += " and order_num like '%" + txtBillNum.Caption.ToString() + "%'";
                }
                 if (!string.IsNullOrEmpty(CombCompany.SelectedValue.ToString()))
                {
                    Str_Where += " and com_name='" + CombCompany.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombDepartment.SelectedValue.ToString()))
                {
                    Str_Where += " and org_name='" + CombDepartment.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combhandle.SelectedValue.ToString()))
                {
                    Str_Where += " and handle_name='" + Combhandle.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombOperator.SelectedValue.ToString()))
                {
                    Str_Where += " and operator_name='" + CombOperator.Text.ToString() + "'";
                }

                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and IOBillTb.remark='" + txtremark.Caption.ToString() + "'";
                }
                 if (dStarttime.ToShortDateString() == dEndtime.ToShortDateString())
                 {
                     Str_Where += " and IOBillTb.order_date=" + Common.LocalDateTimeToUtcLong(dStarttime);
                 }
                 else
                 {
                     if (dateTimeStart.Value != null)
                     {

                         Str_Where += " and IOBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                     }
                     if (dateTimeEnd.Value != null)
                     {

                         Str_Where += " and IOBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                     }
                 }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and (IOBillTb.order_status_name='" + Submited + "'";
                    Str_Where += " or IOBillTb.order_status_name='" + Verify + "')";

                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return ex.Message;
            }
        }
        /// <summary>
        /// 按配件或者往来单位查询的条件
        /// </summary>
        private string QueryPartOrBusUntWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime dStarttime = Convert.ToDateTime(dateTimePartStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimePartEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(txtparts_code.Text.ToString()))
                {
                    Str_Where += " and parts_num = '" + txtparts_code.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_name.Caption.ToString()))
                {
                    Str_Where += " and parts_name = '" + txtparts_name.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtBusinUnt_name.Caption.ToString()))
                {
                    Str_Where += " and bussiness_units='" + txtBusinUnt_name.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_type.Text.ToString()))
                {
                    Str_Where += " and parts_type='" + txtparts_type.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_cartype.Text.ToString()))
                {
                    Str_Where += " and vm_name='" + txtparts_cartype.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtcontacts.Text.ToString()))
                {
                    Str_Where += " and cont_name='" + txtcontacts.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtPartPhone.Caption.ToString()))
                {
                    Str_Where += " and cont_phone='" + txtPartPhone.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtdrawing_num.Caption.ToString()))
                {
                    Str_Where += " and drawing_num='" + txtdrawing_num.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_brand.Caption.ToString()))
                {
                    Str_Where += " and parts_brand='" + txtparts_brand.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combis_gift.SelectedValue.ToString()))
                {
                    Str_Where += " and is_gift='" + Combis_gift.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombPartCompany.SelectedValue.ToString()))
                {
                    Str_Where += " and com_name='" + CombPartCompany.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtPartremark.Caption.ToString()))
                {
                    Str_Where += " and remark='" + txtPartremark.Caption.ToString() + "'";
                }
                 if (dStarttime.ToShortDateString() == dEndtime.ToShortDateString())
                 {
                     Str_Where += " and order_date=" + Common.LocalDateTimeToUtcLong(dStarttime);
                 }
                 else
                 {
                     if (dateTimeStart.Value != null)
                     {
                         Str_Where += " and order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                     }
                     if (dateTimeEnd.Value != null)
                     {
                         Str_Where += " and order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                     }
                 }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }

                else
                {
                    Str_Where += " and(order_status_name='" + Submited + "'";
                    Str_Where += " or order_status_name='" + Verify + "')";

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
