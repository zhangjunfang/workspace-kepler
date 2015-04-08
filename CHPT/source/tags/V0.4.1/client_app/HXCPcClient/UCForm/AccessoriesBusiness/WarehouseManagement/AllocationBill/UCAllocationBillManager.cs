using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;
using Utility.Common;
using SYSModel;
using Utility.CommonForm;
using System.IO;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    public partial class UCAllocationBillManager :UCBase
    {

        #region 全局变量
        private string InOutTable = "tb_parts_stock_inout";//出入库单表
        private string InOutPartTable = "tb_parts_stock_inout_p";//出入库单配件表
        private string PartsTable = "tb_parts";//配件档案表
        private string InOutID = "stock_inout_id";//出入库单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string InOutQueryLogMsg = "查询出入库单表信息";//出入库单表操作日志
        private string InOutDelLogMsg = "批量删除出入库单表信息";//出入库单表操作日志
        private string InOutSubmitLogMsg = "批量提交出入库单表信息";//出入库单表操作日志
        private string InOutVerifyLogMsg = "批量审核出入库单表信息";//出入库单表操作日志
        private string InOutEdit = "编辑";
        private string InOutCopy = "复制";
        private const string InBill = "入库单";
        private const string OutBill = "出库单";
        private const string ExportXlsName = "出入库单";
        private int SearchFlag = 0;
        List<string> InoutIDValuelist = new List<string>();//存储选中出入库单记录行主键ID
        //出入库单与配件表字段
        private const string OrderTypeName = "order_type_name";
        private const string BillingTypeName = "billing_type_name";
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string BussinessUnits = "bussiness_units";
        private const string ArrivalPlace = "arrival_place";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        private const string PartID = "parts_id";//配件ID
        private const string SerPartsCode = "ser_parts_code";//配件档案配件编码
        private const string PartsCode = "parts_code";//配件编码
        private const string PartName = "parts_name";//配件名称
        private const string PartSpec = "model";//配件规格
        private const string PartBarCode = "parts_barcode";//配件条码
        private const string CarPartsCode = "car_parts_code";//车厂编码
        private const string DrawNum = "drawing_num";//配件图号
        private const string UnitName = "unit_name";//单位名称
        private const string PartCount = "counts";//配件数量
        private const string WarehID = "wh_id";//仓库ID
        private const string WarehName = "wh_name";//仓库名称
        #endregion

        #region 窗体初始化
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public UCAllocationBillManager()
        {
            InitializeComponent();
            //注册操作事件
            base.AddEvent += new ClickHandler(UCAllocationBillManager_AddEvent);
            base.EditEvent += new ClickHandler(UCAllocationBillManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCAllocationBillManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCAllocationBillManager_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCAllocationBillAddOrEdit_SubmitEvent);
            base.VerifyEvent += new ClickHandler(UCAllocationBillManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCAllocationBillManager_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            // DataGridViewEx.SetDataGridViewStyle(gvAllocBillList, OrderState);//美化表格控件
            //设置列表的可编辑状态
            gvAllocBillList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvAllocBillList.Columns)
            {
                if (dgCol.Name != colCheck.Name) dgCol.ReadOnly = true;
            }
            base.btnImport.Visible = false;
            base.btnCommit.Visible = false;
            base.btnStatus.Visible = false;
            base.btnImport.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSync.Visible = false;
        }
        /// <summary>
        /// 导出Excel文件菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                BtnExportMenu.Show(btnExport, 0, btnExport.Height);//指定导出菜单显示的位置
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 导出Excel文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAllocBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
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
                    XlsTable.Columns.Add("单据状态", typeof(string));
                    foreach (DataGridViewRow dgRow in gvAllocBillList.Rows)
                    {
                        bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                        if (SelectFlag == true)
                        {
                            DataRow TableRow = XlsTable.NewRow();//创建表行项
                            TableRow["单据类型"] = dgRow.Cells["OrderType"].Value.ToString();
                            TableRow["开单类型"] = dgRow.Cells["BillType"].Value.ToString();
                            TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                            TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                            TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                            TableRow["往来单位"] = dgRow.Cells["BusinessUnit"].Value.ToString();
                            TableRow["数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                            TableRow["到货地点"] = dgRow.Cells["ArrivPlace"].Value.ToString();
                            TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                            TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                            TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                            TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                            TableRow["单据状态"] = dgRow.Cells["OrderState"].Value.ToString();
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
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToShortDateString();
                //单据类型
                CommonFuncCall.BindAllocationBillType(ComBorder_type_name, true, "请选择");
                //开单类型
                CommonFuncCall.BindInStockBillingType(ComBbilling_type_name, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(ComBwh_name, "请选择");
                //单据状态
                CommonFuncCall.BindOrderStatus(ComBorder_status_name, true);
                //公司
                CommonFuncCall.BindCompany(ComBcom_name, "全部");
                //部门
                CommonFuncCall.BindDepartment(ComBorg_name, "", "全部");
                //经办人
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }



        #endregion
        /// <summary>
        /// 查询出入库单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gvAllocBillList.Rows.Clear();//清除原来的查询记录
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetInOutBillList(QueryWhere);

        }
        /// <summary>
        /// 添加出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_AddEvent(object send, EventArgs e)
        {
            UCAllocationBillAddOrEdit UCAllocBillAdd = new UCAllocationBillAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCAllocBillAdd, "出入库单-添加", "UCAllocBillAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_EditEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(InOutEdit, WindowStatus.Edit);
        }
        /// <summary>
        /// 复制出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_CopyEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(InOutCopy, WindowStatus.Copy);
        }
        /// <summary>
        /// 删除出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的出入库单记录ID
                if (listField.Count == 0 && gvAllocBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> AllocBillField = new Dictionary<string, string>();
                        AllocBillField.Add("enable_flag", "0");
                        bool BillDelFlag = DBHelper.BatchUpdateDataByIn(InOutDelLogMsg, InOutTable, AllocBillField, InOutID, listField.ToArray());//批量更改单据记录行删除标记为0已删除
                        if (BillDelFlag)
                        {
                            long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                            long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                            string DefaultWhere = " enable_flag=1 and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                            GetInOutBillList(DefaultWhere);//刷新出入库单列表
                            MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        { MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 提交出入库单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            try
            {
                List<string> InoutIdLst = GetSubmitRecord();//获取需提交的单据记录
                if (InoutIdLst.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合提交条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Dictionary<string,string> AllocBillField=new Dictionary<string,string>();//存放更新字段
                    AllocBillField.Add("order_status",Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//状态ID
                    AllocBillField.Add("order_status_name",DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT,true));//状态name
                    bool flag = DBHelper.BatchUpdateDataByIn(InOutSubmitLogMsg, InOutTable, AllocBillField, InOutID, InoutIdLst.ToArray());
                    if (flag)
                    {
                        MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1 and order_status_name='已提交' and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        GetInOutBillList(DefaultWhere);//刷新出入库单列表
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCAllocationBillManager_VerifyEvent(object send, EventArgs e)
        {
            try
            {

                Dictionary<string, long> OrderIDDateDic = GetVerifyRecord();//获取需要核实的记录行
                if (InoutIDValuelist.Count == 0 && gvAllocBillList.SelectedRows.Count==0)
                {
                    MessageBoxEx.Show("请选择符合审核条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCVerify UcVerify = new UCVerify();
                UcVerify.ShowDialog();
                string Content = UcVerify.Content;
                DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;//获取审核状态

                Dictionary<string, string> AllocBillField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取出入库单状态(已审核)
                    AllocBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    AllocBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取出入库单状态(审核不通过)
                    AllocBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    AllocBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(InOutVerifyLogMsg, InOutTable, AllocBillField, InOutID, InoutIDValuelist.ToArray());//批量审核获取的出入库单记录
                if (flag)
                {
                    CommonFuncCall.StatisticStock(GetInoutPart(OrderIDDateDic), "出入库单配件表");//同步更新实际库存
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and order_status_name='已审核通过' and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetInOutBillList(DefaultWhere);//刷新出入库单列表

                }
                else
                {
                    MessageBoxEx.Show("审核失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 获取出入库单配件表已审核的单据配件信息
        /// </summary>
        /// <param name="OrderIDDateDic">单据ID与日期对</param>
        /// <returns>返回待统计配件信息</returns>
        private DataTable GetInoutPart(Dictionary<string, long> OrderIDDateDic)
        {
            try
            {
                DataTable TemplateTable = null;//模版表
                foreach (KeyValuePair<string, long> KVPair in OrderIDDateDic)
                {
                    StringBuilder sbField = new StringBuilder();
                    sbField.AppendFormat("{0},{1},PtTb.{2},IOTb.{3},IOTb.{4},IOTb.{5},IOTb.{6},IOTb.{7},IOTb.{8},{9},{10}",
                     WarehID, WarehName, PartID, PartsCode, PartName, PartSpec, PartBarCode, CarPartsCode, DrawNum, UnitName, PartCount);
                    string RelationTable = InOutPartTable + " as IOTb left join " + PartsTable +
                    " as PtTb on IOTb.parts_code=PtTb.ser_parts_code ";//要查询的关联表名
                    TemplateTable = CommonFuncCall.CreatePartStatisticTable();//获取要填充的公用表
                    //获取对应单据的配件信息
                    DataTable IOPartTable = DBHelper.GetTable("查询出入库单配件信息", RelationTable, sbField.ToString(), "stock_inout_id='" + KVPair.Key.ToString() + "'", "", "");
                    for (int i = 0; i < IOPartTable.Rows.Count; i++)
                    {
                        DataRow dr = TemplateTable.NewRow();//创建模版表行项
                        dr["OrderDate"] = KVPair.Value.ToString();//单据日期
                        dr["WareHouseID"] = IOPartTable.Rows[i][WarehID].ToString();//仓库ID
                        dr["WareHouseName"] = IOPartTable.Rows[i][WarehName].ToString();//仓库名称
                        dr["PartID"] = IOPartTable.Rows[i][PartID].ToString();//配件ID
                        dr["PartCode"] = IOPartTable.Rows[i][PartsCode].ToString();//配件编码
                        dr["PartName"] = IOPartTable.Rows[i][PartName].ToString();//配件名称
                        dr["PartSpec"] = IOPartTable.Rows[i][PartSpec].ToString();//配件规格
                        dr["PartBarCode"] = IOPartTable.Rows[i][PartBarCode].ToString();//配件条码
                        dr["CarPartsCode"] = IOPartTable.Rows[i][CarPartsCode].ToString();//车厂编码
                        dr["DrawNum"] = IOPartTable.Rows[i][DrawNum].ToString();//配件图号
                        dr["UnitName"] = IOPartTable.Rows[i][UnitName].ToString();//单位名称
                        dr["PartCount"] = IOPartTable.Rows[i][PartCount].ToString();//配件数量
                        dr["StatisticType"] = (int)DataSources.EnumStatisticType.ActualCount;//统计类型
                        TemplateTable.Rows.Add(dr);//添加新的数据行项
                    }
                }
                return TemplateTable;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }

        }


        /// <summary>
        /// 出入库单编辑或复制
        /// </summary>
        /// <param name="HandleType"></param>
        /// <param name="state"></param>
        private void EditOrCopyMethod(string HandleType, WindowStatus state)
        {
            try
            {
                string InoutBillID = string.Empty;
                List<string> InoutBillIDList = GetSelectedRecord();//获取要编辑的出入库单记录行

                if (InoutBillIDList.Count == 0 && gvAllocBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要" + HandleType + "的数据!");
                    return;
                }
                else if (InoutBillIDList.Count > 1 && gvAllocBillList.SelectedRows.Count > 1)
                {
                    MessageBoxEx.Show("一次只能" + HandleType + "一条数据!");
                    return;
                }
                else
                {
                    if (InoutBillIDList.Count == 1)
                    {
                        InoutBillID = InoutBillIDList[0].ToString();
                    }
                    else if (gvAllocBillList.SelectedRows.Count == 1)
                    {
                        InoutBillID = gvAllocBillList.CurrentRow.Cells["Inout_Id"].Value.ToString();
                    }
                    UCAllocationBillAddOrEdit UCAllocBillHandle = new UCAllocationBillAddOrEdit(state, InoutBillID, this);
                    base.addUserControl(UCAllocBillHandle, "出入库单-" + HandleType, "UCAllocBillHandle", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormAllocBillPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and IOBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetInOutBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvAllocBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetInOutBillList(QueryWhere);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now.ToString();
            dateTimeEnd.Value = DateTime.Now.ToString();
            ComBorder_type_name.SelectedIndex = 0;
            ComBbilling_type_name.SelectedIndex = 0;
            ComBwh_name.SelectedIndex = 0;
            ComBorder_status_name.SelectedIndex = 0;
            ComBcom_name.SelectedIndex = 0;
            ComBorg_name.SelectedIndex = 0;
            ComBhandle_name.SelectedIndex = 0;
            txtremark.Caption = string.Empty;
        }


        /// <summary>
        /// 公司选择事件
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
        /// 部门选择事件
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
        /// 列表双击查看出入库单配件明细
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
                    string WHName = gvAllocBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCAllocationBillDetails UCAllocBillDetails = new UCAllocationBillDetails(InOutIdValue, WHName);
                    base.addUserControl(UCAllocBillDetails, "出入库单-查看", "UCAllocBillDetails", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }



        /// <summary>
        /// 获取出入库单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            try
            {
                if (InoutIDValuelist.Count > 0) InoutIDValuelist.Clear();//清除之前的数据
                foreach (DataGridViewRow dr in gvAllocBillList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        InoutIDValuelist.Add(dr.Cells["Inout_Id"].Value.ToString());
                    }

                }
                if (InoutIDValuelist.Count == 0 && gvAllocBillList.SelectedRows.Count == 1)
                {
                    string DelInoutId = gvAllocBillList.CurrentRow.Cells["Inout_Id"].Value.ToString();
                    InoutIDValuelist.Add(DelInoutId);
                }
                return InoutIDValuelist;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }

        }
        /// <summary>
        /// 查询出入库单列表
        /// </summary>
        public void GetInOutBillList(string WhereStr)
        {
            try
            {
                gvAllocBillList.Rows.Clear();//清空所有记录行
                StringBuilder sbField = new StringBuilder();//出入库单查询字段集合
                sbField.AppendFormat("IOBillTb.{0},{1},{2},{3},{4},{5},{6},{7},TotalCount,{8},{9},{10},IOBillTb.{11},{12}",
                InOutID, OrderTypeName, BillingTypeName, OrderNum, OrderDate, WareHouseName, BussinessUnits,
                ArrivalPlace, OrgName, HandleName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as IOBillTb inner join" +
                " (select {0}.{2},sum({3}) as TotalCount from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2}) as IOBillPartTb" +
                " on  IOBillPartTb.{2}=IOBillTb.{2}",
                InOutTable, InOutPartTable, InOutID, PartCount);
                int RecCount = 0;//查询记录行数

                DataTable InoutBillTable = DBHelper.GetTableByPage(InOutQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " order by IOBillTb.create_time desc", winFormAllocBillPage.PageIndex, winFormAllocBillPage.PageSize, out RecCount);//获取出入库单表查询记录
                winFormAllocBillPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    return;

                }
                else
                {

                    //把查询的出入库单列表放入Gridview
                    if (gvAllocBillList.Rows.Count > 0) gvAllocBillList.Rows.Clear();//清空原来的记录行项       
                    for (int i = 0; i < InoutBillTable.Rows.Count; i++)
                    {

                        DataGridViewRow gvRow = gvAllocBillList.Rows[gvAllocBillList.Rows.Add()];//创建行项
                        gvRow.Cells["Inout_Id"].Value = InoutBillTable.Rows[i][InOutID].ToString();//存放出入库单ID
                        gvRow.Cells["OrderType"].Value = InoutBillTable.Rows[i][OrderTypeName].ToString();
                        gvRow.Cells["BillType"].Value = InoutBillTable.Rows[i][BillingTypeName].ToString();
                        gvRow.Cells["BillNum"].Value = InoutBillTable.Rows[i][OrderNum].ToString();
                        DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(InoutBillTable.Rows[i][OrderDate]));//获取单据日期
                        gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();//单据日期
                        gvRow.Cells["WHName"].Value = InoutBillTable.Rows[i][WareHouseName].ToString();
                        gvRow.Cells["BusinessUnit"].Value = InoutBillTable.Rows[i][BussinessUnits].ToString();
                        gvRow.Cells["TotalCount"].Value = InoutBillTable.Rows[i]["TotalCount"].ToString();
                        gvRow.Cells["ArrivPlace"].Value = InoutBillTable.Rows[i][ArrivalPlace].ToString();
                        gvRow.Cells["DepartName"].Value = InoutBillTable.Rows[i][OrgName].ToString();
                        gvRow.Cells["HandlerName"].Value = InoutBillTable.Rows[i][HandleName].ToString();
                        gvRow.Cells["OpeName"].Value = InoutBillTable.Rows[i][OperatorName].ToString();
                        gvRow.Cells["Remarks"].Value = InoutBillTable.Rows[i][Remark].ToString();
                        gvRow.Cells["OrderState"].Value = InoutBillTable.Rows[i][OrderStatus].ToString();

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 获取选中的草稿单据记录行
        /// </summary>
        /// <returns></returns>
        private List<string> GetSubmitRecord()
        {
            try
            {
                if (InoutIDValuelist.Count > 0) InoutIDValuelist.Clear();//清除之前的数据
                foreach(DataGridViewRow dr in gvAllocBillList.Rows)
                {
                    bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                    if(isCheck!=null&&isCheck)
                    {
                      //获取保存草稿状态的单据记录
                        string BillStatusDraft = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT,true);
                        string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                        if (BillStatusDraft==ColOrderStatus)
                        {
                            InoutIDValuelist.Add(dr.Cells["Inout_Id"].Value.ToString());//添加草稿状态主键ID
                        }
                    }
                }
                return InoutIDValuelist;
            
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
          
        }
        /// <summary>
        /// 获取出入库单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, long> GetVerifyRecord()
        {
            try
            {
                if (InoutIDValuelist.Count > 0) InoutIDValuelist.Clear();//清除之前的数据
                Dictionary<string, long> DicField = new Dictionary<string, long>();
                foreach (DataGridViewRow dr in gvAllocBillList.Rows)
                {
                    bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && isCheck)
                    {
                        //获取已提交/审核未通过的状态
                        string BillStatusSUBMIT = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                        string BillStatusNOTAUDIT = DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true);
                        string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                        if (BillStatusSUBMIT == ColOrderStatus || BillStatusNOTAUDIT == ColOrderStatus)
                        {
                            long OrdeDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["BillDate"].Value));
                            DicField.Add(dr.Cells["Inout_Id"].Value.ToString(), OrdeDate);//添加已审核单据主键ID和单据日期键值对
                            InoutIDValuelist.Add(dr.Cells["Inout_Id"].Value.ToString());//添加已审核单据主键ID
                        }
                    }
                }


                return DicField;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }

        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <returns></returns>
        private string BuildWhereCondation()
        {
            try
            {
                string Str_Where = " enable_flag=1 ";
                DateTime StartDate = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime EndDate = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(ComBorder_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and order_type_name = '" + ComBorder_type_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBbilling_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and billing_type_name = '" + ComBbilling_type_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBwh_name.SelectedValue.ToString()))
                {
                    Str_Where += " and wh_name = '" + ComBwh_name.Text.ToString() + "'";
                }

                if (!string.IsNullOrEmpty(ComBorder_status_name.SelectedValue.ToString()))
                {
                    Str_Where += " and order_status_name='" + ComBorder_status_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBcom_name.SelectedValue.ToString()))
                {
                    Str_Where += " and com_name='" + ComBcom_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBorg_name.SelectedValue.ToString()))
                {
                    Str_Where += " and org_name='" + ComBorg_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBhandle_name.SelectedValue.ToString()))
                {
                    Str_Where += " and handle_name='" + ComBhandle_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and IOBillTb.remark='" + txtremark.Caption.ToString() + "'";
                }

                if (dateTimeStart.Value != null)
                {

                    Str_Where += " and IOBillTb.order_date>= " + Common.LocalDateTimeToUtcLong(StartDate);
                }
                if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and IOBillTb.order_date<= " + Common.LocalDateTimeToUtcLong(EndDate);
                }
                if (StartDate > EndDate)
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
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
