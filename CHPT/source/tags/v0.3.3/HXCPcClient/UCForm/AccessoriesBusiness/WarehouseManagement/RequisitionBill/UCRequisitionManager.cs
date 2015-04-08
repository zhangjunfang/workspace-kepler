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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill
{
    public partial class UCRequisitionManager : UCBase
    {
        #region 全局变量
        //要查询的开单信息表名
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件信息表
        private const string AllotBillID = "stock_allot_id";//调拨单单主键
        private int SearchFlag = 0;
        private string AllotBillLogMsg = "查询调拨单表信息";//调拨单查询日志
        private string AllotDelLogMsg = "批量删除调拨单表信息";//调拨单单表操作日志
        private string AllotVerifyLogMsg = "批量审核出调拨单表信息";//调拨单单表操作日志
        private string AllotEdit = "编辑";
        private string AllotCopy = "复制";
        private string BillingStatus = "已开单";
        List<string> AllotBillIDValuelist = new List<string>();//存储选中调拨单记录行主键ID集合

        //调拨单表字段
        private const string OrderTypeName = "order_type_name";
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string CallOutOrg = "call_out_org_name";
        private const string CallOutWhouse = "call_out_wh_name";
        private const string CallInOrg = "call_in_org_name";
        private const string CallInWhouse = "call_in_wh_name";
        private const string TranWay = "trans_way_name";
        private const string DeliveryAddr = "delivery_address";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        private const string ComName = "com_name";

        //调拨单配件表字段
        private const string PartCount = "counts";
        private const string TotalMoney = "money";
        #endregion

        public UCRequisitionManager()
        {
            InitializeComponent();
            //注册操作事件
            base.AddEvent += new ClickHandler(UCRequisitionManager_AddEvent);
            base.EditEvent += new ClickHandler(UCRequisitionManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCRequisitionManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCRequisitionManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCRequisitionManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCRequisitionManager_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            //DataGridViewEx.SetDataGridViewStyle(gvAllotBillList, OrderState);//美化表格控件
            gvAllotBillList.ReadOnly = false;
            OrderType.ReadOnly = true;
            BillNum.ReadOnly = true;
            BillDate.ReadOnly = true;
            OutDepartment.ReadOnly = true;
            OutWareHouse.ReadOnly = true;
            InDepartment.ReadOnly = true;
            InWareHouse.ReadOnly = true;
            TotalCount.ReadOnly = true;
            AmountMoney.ReadOnly = true;
            DeliveryWays.ReadOnly = true;
            ArrivePlace.ReadOnly = true;
            DepartName.ReadOnly = true;
            HandlerName.ReadOnly = true;
            OpeName.ReadOnly = true;
            Remarks.ReadOnly = true;
            OrderState.ReadOnly = true;
            base.btnImport.Enabled = false;
            base.btnCommit.Enabled = false;
            base.btnStatus.Enabled = false;
            base.btnImport.Enabled = false;
            base.btnBalance.Enabled = false;
            base.btnSync.Enabled = false;
        }
        /// <summary>
        /// 导出菜单显示位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private  void UCRequisitionManager_ExportEvent(object sender, EventArgs e)
       {
           try
           {
               BtnExportMenu.Show(btnExport,0,btnExport.Height);//指定导出菜单显示位置
           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
           }
       }
        /// <summary>
        /// 窗体初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCRequisitionManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                //单据类型
                CommonFuncCall.BindAllocationBillType(ComBorder_type_name, true, "请选择");
                //开单类型
                CommonFuncCall.BindAllocationBillingType(ComBcall_out_wh_name, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(ComBcall_in_org_name, "请选择");
                //单据状态
                CommonFuncCall.BindOrderStatus(ComBcall_in_wh_name, true);
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
        /// <summary>
        /// 导出Excel文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {

            try
            {
                if (gvAllotBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvAllotBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列项
                        XlsTable.Columns.Add("单据类型", typeof(string));
                        XlsTable.Columns.Add("单据编号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("调出机构", typeof(string));
                        XlsTable.Columns.Add("调出仓库", typeof(string));
                        XlsTable.Columns.Add("调入机构", typeof(string));
                        XlsTable.Columns.Add("调入仓库", typeof(string));
                        XlsTable.Columns.Add("业务数量", typeof(string));
                        XlsTable.Columns.Add("金额", typeof(string));
                        XlsTable.Columns.Add("运输方式", typeof(string));
                        XlsTable.Columns.Add("送货地点", typeof(string));
                        XlsTable.Columns.Add("部门", typeof(string));
                        XlsTable.Columns.Add("经办人", typeof(string));
                        XlsTable.Columns.Add("操作人", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        XlsTable.Columns.Add("单据状态", typeof(string));
                        TableRow["单据类型"] = dgRow.Cells["OrderType"].Value.ToString();
                        TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                        TableRow["调出机构"] = dgRow.Cells["OutDepartment"].Value.ToString();
                        TableRow["调出仓库"] = dgRow.Cells["OutWareHouse"].Value.ToString();
                        TableRow["调入机构"] = dgRow.Cells["InDepartment"].Value.ToString();
                        TableRow["调入仓库"] = dgRow.Cells["InWareHouse"].Value.ToString();
                        TableRow["业务数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                        TableRow["金额"] = dgRow.Cells["AmountMoney"].Value.ToString();
                        TableRow["运输方式"] = dgRow.Cells["DeliveryWays"].Value.ToString();
                        TableRow["送货地点"] = dgRow.Cells["ArrivePlace"].Value.ToString();
                        TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                        TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                        TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                        TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                        TableRow["单据状态"] = dgRow.Cells["OrderState"].Value.ToString();
                        XlsTable.Rows.Add(TableRow);
                    }
                    CommonFuncCall.ExportExcelFile(XlsTable);//生成Excel表格文件
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 查询调拨单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gvAllotBillList.Rows.Clear();//清除原来的查询记录
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetAllotBillList(QueryWhere);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormAllotBillPage_PageIndexChanged(object sender, EventArgs e)
        {
            try{

                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and BillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetAllotBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvAllotBillList.Rows.Clear();//清除原来的查询记录
                    string QueryWhere = "";//获取查询条件
                    GetAllotBillList(QueryWhere);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

           
        }
        /// <summary>
        /// 添加调拨单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionManager_AddEvent(object send, EventArgs e)
        {
            try
            {
                UCRequisitionAddOrEdit UCAllotBillAdd = new UCRequisitionAddOrEdit(WindowStatus.Add, null, this);
                base.addUserControl(UCAllotBillAdd, "调拨单-添加", "UCAllotBillAdd", this.Tag.ToString(), this.Name);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 编辑调拨单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionManager_EditEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(AllotEdit, WindowStatus.Edit);
        }
        /// <summary>
        /// 复制调拨单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionManager_CopyEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(AllotCopy, WindowStatus.Copy);
        }
        /// <summary>
        /// 删除调拨单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的调拨库单记录ID
                if (listField.Count == 0 && gvAllotBillList.SelectedRows.Count==0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> AllotBillField = new Dictionary<string, string>();
                        AllotBillField.Add("enable_flag", "0");
                        bool flag = DBHelper.BatchUpdateDataByIn(AllotDelLogMsg, AllotBillTable, AllotBillField, AllotBillID, listField.ToArray());//批量更改记录行删除标记为0已删除
                        if (flag)
                        {
                            string QueryWhere = "enable_flag=1";//获取查询条件
                            GetAllotBillList(QueryWhere);//刷新调拨单单列表
                            MessageBoxEx.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        { MessageBoxEx.Show("操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
        private void UCRequisitionManager_VerifyEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetVerifyRecord();//获取需要核实的记录行
                if (listField.Count == 0 && gvAllotBillList.SelectedRows.Count==0)
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
                    //获取调拨单单状态(已审核)
                    AllocBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    AllocBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取调拨单单状态(审核不通过)
                    AllocBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    AllocBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(AllotVerifyLogMsg, AllotBillTable, AllocBillField, AllotBillID, listField.ToArray());//批量审核获取的调拨单记录
                if (flag)
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    string QueryWhere = " order_status_name='已审核通过' ";
                    GetAllotBillList(QueryWhere);//刷新调拨单列表

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
        /// 编辑或者复制调拨单
        /// </summary>
        /// <param name="HandleType"></param>
        /// <param name="state"></param>
        private void EditOrCopyMethod(string HandleType, WindowStatus state)
        {
            try
            {

                string AllotId = string.Empty;
                List<string> BillIDlist = GetSelectedRecord();//获取要复制调拨单记录
                if (BillIDlist.Count == 0 && gvAllotBillList.SelectedRows.Count==0)
                {
                    MessageBoxEx.Show("请选择要" + HandleType + "的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (BillIDlist.Count > 1 && gvAllotBillList.SelectedRows.Count > 1)
                {
                    MessageBoxEx.Show("一次只能" + HandleType + "一条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (BillIDlist.Count == 1)
                    {
                        AllotId = BillIDlist[0].ToString();
                    }
                    else if (gvAllotBillList.SelectedRows.Count == 1)
                    {
                        AllotId = gvAllotBillList.CurrentRow.Cells["AllotID"].ToString();
                    }
                    UCRequisitionAddOrEdit UCAllotBillHandle = new UCRequisitionAddOrEdit(state, AllotId, this);
                    base.addUserControl(UCAllotBillHandle, "调拨单-" + HandleType, "UCAllotBillHandle" , this.Tag.ToString(), this.Name);
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }  
        }

        /// <summary>
        /// 清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now.ToString();
            dateTimeEnd.Value = DateTime.Now.ToString();
            ComBorder_type_name.SelectedIndex = 0;
            ComBcall_out_wh_name.SelectedIndex = 0;
            ComBcall_in_org_name.SelectedIndex = 0;
            ComBcall_in_wh_name.SelectedIndex = 0;
            Combtrans_way_name.SelectedIndex = 0;
            ComBcom_name.SelectedIndex = 0;
            ComBorg_name.SelectedIndex = 0;
            ComBhandle_name.SelectedIndex = 0;
            txtremark.Caption = string.Empty;
        }


        /// <summary>
        /// 公司选择
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
        /// 部门选择
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
        /// 列表双击查看调拨单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAllotBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string AllotIdValue = gvAllotBillList.CurrentRow.Cells["AllotID"].Value.ToString();//获取调拨单单ID
                    UCRequisitionBillDetail UCAllotBillDetails = new UCRequisitionBillDetail(AllotIdValue);
                    base.addUserControl(UCAllotBillDetails, "调拨单-查看", "UCAllotBillDetails" + AllotIdValue + "", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }


        /// <summary>
        /// 获取调拨单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            try
            {
                if (AllotBillIDValuelist.Count > 0) AllotBillIDValuelist.Clear();//清空之前
                foreach (DataGridViewRow dr in gvAllotBillList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        AllotBillIDValuelist.Add(dr.Cells["AllotID"].Value.ToString());
                    }
                }
                if (AllotBillIDValuelist.Count == 0 && gvAllotBillList.SelectedRows.Count == 1)
                {
                    string DelInoutId = gvAllotBillList.CurrentRow.Cells["AllotID"].Value.ToString();
                    AllotBillIDValuelist.Add(DelInoutId);
                }

                return AllotBillIDValuelist;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }
        /// <summary>
        /// 查询调拨单列表
        /// </summary>
        public void GetAllotBillList(string WhereStr)
        {
            try
            {

                StringBuilder sbField = new StringBuilder();//调拨单单查询字段集合
                sbField.AppendFormat("BillTb.{0},{1},{2},{3},{4},{5},{6},{7},Totalcount,Totalmoney,{8},{9},{10},{11},BillTb.{12},{13}", AllotBillID, OrderTypeName,
                OrderNum, OrderDate, CallOutOrg, CallOutWhouse, CallInOrg, CallInWhouse, TranWay, DeliveryAddr,
                OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as BillTb inner join "+
                " (select {0}.{2}, sum({3}) as Totalcount,sum(4) as Totalmoney from {0} inner join"+
                " {1} on {0}.{2}={1}.{2} group by {0}.{2}) as AllotBillPartTable "+
                " on AllotBillPartTable.{2}=BillTb.{2} ",
                AllotBillTable, AllotPartTable,AllotBillID, PartCount, TotalMoney);
                int RecCount = 0;//查询记录行数

                DataTable AllotTable = DBHelper.GetTableByPage(AllotBillLogMsg, sbRelationTable.ToString(), sbField.ToString(),WhereStr,
                "", " order by BillTb.create_time desc", winFormAllotBillPage.PageIndex, winFormAllotBillPage.PageSize, out RecCount);//获取调拨单单表查询记录
                winFormAllotBillPage.RecordCount = RecCount;//获取总记录行
                if (AllotTable.Rows.Count == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的调拨单据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //把查询的调拨单单列表放入Gridview
                int SerialNum = 1;//记录行序号
                for (int i = 0; i < AllotTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvAllotBillList.Rows[gvAllotBillList.Rows.Add()];//创建行项
                    gvRow.Cells["AllotID"].Value = AllotTable.Rows[i][AllotBillID].ToString();//存放调拨单ID
                    gvRow.Cells["colIndex"].Value = SerialNum;
                    gvRow.Cells["OrderType"].Value = AllotTable.Rows[i][OrderTypeName].ToString();
                    gvRow.Cells["BillNum"].Value = AllotTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(AllotTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();//单据日期
                    gvRow.Cells["OutDepartment"].Value = AllotTable.Rows[i][CallOutOrg].ToString();
                    gvRow.Cells["OutWareHouse"].Value = AllotTable.Rows[i][CallOutWhouse].ToString();
                    gvRow.Cells["InDepartment"].Value = AllotTable.Rows[i][CallInOrg].ToString();
                    gvRow.Cells["InWareHouse"].Value = AllotTable.Rows[i][CallInWhouse].ToString();
                    gvRow.Cells["TotalCount"].Value = AllotTable.Rows[i]["Totalcount"].ToString();
                    gvRow.Cells["AmountMoney"].Value = AllotTable.Rows[i]["Totalmoney"].ToString();
                    gvRow.Cells["DeliveryWays"].Value = AllotTable.Rows[i][TranWay].ToString();
                    gvRow.Cells["ArrivePlace"].Value = AllotTable.Rows[i][DeliveryAddr].ToString();
                    gvRow.Cells["DepartName"].Value = AllotTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["OpeName"].Value = AllotTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = AllotTable.Rows[i][Remark].ToString();
                    gvRow.Cells["OrderState"].Value = AllotTable.Rows[i][OrderStatus].ToString();
                    SerialNum++;//序号自增加

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }

        /// <summary>
        /// 获取调拨单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvAllotBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    //获取已提交/审核未通过的状态的编号
                    string BillStatusSUBMIT = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    string BillStatusNOTAUDIT = Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString();
                    string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                    if (BillStatusSUBMIT == ColOrderStatus || BillStatusNOTAUDIT == ColOrderStatus)
                    {
                        listField.Add(dr.Cells["AllotID"].Value.ToString());
                    }
                }
            }
            return listField;
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
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(ComBorder_type_name.Text.ToString()))
                {
                    Str_Where += " and " + OrderTypeName + " = '" + ComBorder_type_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcall_out_wh_name.Text.ToString()))
                {
                    Str_Where += " and " + CallOutWhouse + " = '" + ComBcall_out_wh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcall_in_org_name.Text.ToString()))
                {
                    Str_Where += " and " + CallInOrg + " = '" + ComBcall_in_org_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcall_in_wh_name.Text.ToString()))
                {
                    Str_Where += " and " + CallInWhouse + "='" + ComBcall_in_wh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcom_name.Text.ToString()))
                {
                    Str_Where += " and " + ComName + "='" + ComBcom_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorg_name.Text.ToString()))
                {
                    Str_Where += " and " + OrgName + "='" + ComBorg_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBhandle_name.Text.ToString()))
                {
                    Str_Where += " and " + HandleName + "='" + ComBhandle_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combtrans_way_name.Text.ToString()))
                {
                    Str_Where += " and " + TranWay + "='" + Combtrans_way_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Comborder_status_name.Text.ToString()))
                {
                    Str_Where += " and " + OrderStatus + "='" + Comborder_status_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and BillTb." + Remark + "='" + txtremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value != null)
                {
                    Str_Where += " and BillTb.order_date > " + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and BillTb.order_date <" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else 
                {
                    Str_Where += " and BillTb.order_status_name =" + BillingStatus;
                }
                return Str_Where;
            }
            catch (Exception ex)
            {

                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return ex.Message;
            }
        }



    }
}
