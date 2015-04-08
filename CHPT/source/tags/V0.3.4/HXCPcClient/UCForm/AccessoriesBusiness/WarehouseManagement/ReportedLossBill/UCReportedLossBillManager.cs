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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill
{
    public partial class UCReportedLossBillManager : UCBase
    {
        #region 全局变量
        private string LossTable = "tb_parts_stock_loss";//报损单表
        private string LossPartTable = "tb_parts_stock_loss_p";//报损单配件表
        private string WareHouseTable = "tb_warehouse";//仓库表
        private string StockLossID = "stock_loss_id";//报损单表主键
        private string WareHouseID = "wh_id";//仓库ID
        private string WareHouseName = "wh_name";//仓库名称
        private string LossQueryLogMsg = "查询报损单表信息";//报损单表操作日志
        private string LossDelLogMsg = "批量删除报损单表信息";//报损单表操作日志
        private string LossVerifyLogMsg = "批量审核报损单表信息";//报损单表操作日志
        private string LossEdit = "编辑";
        private string LossCopy = "复制";
        private string BillStatus = "已开单";
        private int SearchFlag = 0;//存放搜索标志
        List<string> LossIDValuelist = new List<string>();//存储选中报损单记录行主键ID
        //报损单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string OutWhourseType = "out_wh_type_name";
        private const string OrgName = "org_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        //报损单配件表字段
        private const string PartCount = "counts";
        private const string AmountMoney = "money";
        #endregion
        public UCReportedLossBillManager()
        {
            InitializeComponent();

            //注册操作事件
            base.AddEvent += new ClickHandler(UCReportedLossBillManager_AddEvent);
            base.EditEvent += new ClickHandler(UCReportedLossBillManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCReportedLossBillManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCReportedLossBillManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCReportedLossBillManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCReportedLossBillManager_ExportEvent);

            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            DataGridViewEx.SetDataGridViewStyle(gvLossBillList, OrderState);//美化表格控件
            //设置列表的可编辑状态
            gvLossBillList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvLossBillList.Columns)
            {
                if (dgCol.Name != colCheck.Name) dgCol.ReadOnly = true;
            }
            base.btnImport.Visible = false;
            base.btnCommit.Visible = false;
            base.btnStatus.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSync.Visible = false;
            gvLossBillList.HeadCheckChanged += new DataGridViewEx.DelegateOnClick(gvLossBillList_HeadCheckChanged); //复选框标题显示为复选框状态 
        }
        //复选框选择状态
        private void gvLossBillList_HeadCheckChanged()
        {
            IsDataGridViewCheckBox();
        }
        /// <summary>
        /// 导出Excel文件菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void UCReportedLossBillManager_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                BtnExportMenu.Show(btnExport, 0, btnExport.Height);//指定导出菜单显示位置
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        ///  窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();

                //出库类型
                CommonFuncCall.BindInOutType(ComBout_wh_type_name, true, "请选择");
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
        /// <summary>
        /// 导出Excel文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvLossBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvLossBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列项
                        XlsTable.Columns.Add("单据编号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("仓库", typeof(string));
                        XlsTable.Columns.Add("出库类型", typeof(string));
                        XlsTable.Columns.Add("业务数量", typeof(string));
                        XlsTable.Columns.Add("金额", typeof(string));
                        XlsTable.Columns.Add("部门", typeof(string));
                        XlsTable.Columns.Add("经办人", typeof(string));
                        XlsTable.Columns.Add("操作人", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        XlsTable.Columns.Add("出库状态", typeof(string));
                        TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                        TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                        TableRow["出库类型"] = dgRow.Cells["OutWhType"].Value.ToString();
                        TableRow["业务数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                        TableRow["金额"] = dgRow.Cells["TotalMoney"].Value.ToString();
                        TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                        TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                        TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                        TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                        TableRow["出库状态"] = dgRow.Cells["OutWhState"].Value.ToString();
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
        /// 添加报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_AddEvent(object send, EventArgs e)
        {
            UCReportedLossBillAddOrEdit UCLossBillAdd = new UCReportedLossBillAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCLossBillAdd, "报损单-添加", "UCLossBillAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_EditEvent(object send, EventArgs e)
        {

            EditOrCopyMethod(LossEdit, WindowStatus.Edit);
        }
        /// <summary>
        /// 复制报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_CopyEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(LossCopy, WindowStatus.Copy);
        }
        /// <summary>
        /// 删除报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的报损单记录ID
                if (listField.Count == 0 && gvLossBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> LossBillField = new Dictionary<string, string>();
                        LossBillField.Add("enable_flag", "0");
                        bool flag = DBHelper.BatchUpdateDataByIn(LossDelLogMsg, LossTable, LossBillField, StockLossID, listField.ToArray());//批量更改记录行删除标记为0已删除
                        if (flag)
                        {
                            string QueryWhere = " enable_flag=1 ";
                            GetRepLossBillList(QueryWhere);//刷新报损单列表
                            MessageBoxEx.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        { MessageBoxEx.Show("操作失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    }
                    else
                    {
                        return;
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        /// <summary>
        /// 审核报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillManager_VerifyEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetVerifyRecord();//获取需要核实的记录行
                if (listField.Count == 0 && gvLossBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合审核条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCVerify UcVerify = new UCVerify();
                UcVerify.ShowDialog();
                string Content = UcVerify.Content;
                DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;//获取审核状态

                Dictionary<string, string> ShippingBillField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取报损单状态(已审核)
                    ShippingBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    ShippingBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取报损单状态(审核不通过)
                    ShippingBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    ShippingBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(LossVerifyLogMsg, LossTable, ShippingBillField, StockLossID, listField.ToArray());//批量审核获取的报损单记录
                if (flag)
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    string QueryWhere = " order_status_name='已审核通过' ";
                    GetRepLossBillList(QueryWhere);//刷新报损单列表

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
        /// 编辑或者复制报损单
        /// </summary>
        /// <param name="HandleType"></param>
        /// <param name="state"></param>
        private void EditOrCopyMethod(string HandleType, WindowStatus state)
        {
            try
            {
                string LossBillID = string.Empty;
                List<string> LossBillIDList = GetSelectedRecord();//获取要编辑或复制的报损单记录行

                if (LossBillIDList.Count == 0 && gvLossBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要" + HandleType + "的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (LossBillIDList.Count > 1 && gvLossBillList.SelectedRows.Count > 1)
                {
                    MessageBoxEx.Show("一次只能" + HandleType + "一条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    if (LossBillIDList.Count == 1)
                    {
                        LossBillID = LossBillIDList[0].ToString();
                    }
                    else if (gvLossBillList.SelectedRows.Count == 1)
                    {
                        LossBillID = gvLossBillList.CurrentRow.Cells["RepLossId"].ToString();
                    }
                    UCReportedLossBillAddOrEdit UCLossBillHandle = new UCReportedLossBillAddOrEdit(state, LossBillID, this);
                    base.addUserControl(UCLossBillHandle, "报损单-" + HandleType, "UCLossBillHandle" + LossBillID, this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 查询报损单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetRepLossBillList(QueryWhere);
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormLossPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and LsBillTb.create_time between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetRepLossBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvLossBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetRepLossBillList(QueryWhere);
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
            ComBout_wh_type_name.SelectedIndex = 0;
            ComBwh_name.SelectedIndex = 0;
            ComBorder_status_name.SelectedIndex = 0;
            ComBcom_name.SelectedIndex = 0;
            ComBorg_name.SelectedIndex = 0;
            ComBhandle_name.SelectedIndex = 0;

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
        /// 列表双击查看报损单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLossBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string LossIdValue = gvLossBillList.CurrentRow.Cells["RepLossId"].Value.ToString();//获取出入库单ID
                    string WHName = gvLossBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCReportedLossBillDetail UCLossBillDetails = new UCReportedLossBillDetail(LossIdValue, WHName);
                    base.addUserControl(UCLossBillDetails, "报损单-查看", "UCLossBillDetails" + LossIdValue + "", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 开单列表选择操作
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsDataGridViewCheckBox()
        {
            try
            {

                if (gvLossBillList.Rows.Count != 0)
                {
                    bool ischeck = (bool)((DataGridViewCheckBoxCell)gvLossBillList.Rows[0].Cells["colCheck"]).EditedFormattedValue;
                    bool SetCheck = ischeck == true ? true : false;
                    for (int i = 1; i < gvLossBillList.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)gvLossBillList.Rows[i].Cells["colCheck"]).Value = SetCheck;//开单列表选择状态
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 获取报损单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {

            if (LossIDValuelist.Count > 0) LossIDValuelist.Clear();//清除之前的数据
            foreach (DataGridViewRow dr in gvLossBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    LossIDValuelist.Add(dr.Cells["RepLossId"].Value.ToString());
                }
            }
            if (LossIDValuelist.Count == 0 && gvLossBillList.SelectedRows.Count == 1)
            {
                string DelInoutId = gvLossBillList.CurrentRow.Cells["RepLossId"].Value.ToString();
                LossIDValuelist.Add(DelInoutId);
            }
            return LossIDValuelist;
        }
        /// <summary>
        /// 查询报损单列表
        /// </summary>
        public void GetRepLossBillList(string WhereStr)
        {
            try
            {

                StringBuilder sbField = new StringBuilder();//报损单查询字段集合
                sbField.AppendFormat("LsBillTb.{0},{1},{2},LsBillTb.{3},{4},PartAmount,PartMoney,{5},{6},LsBillTb.{7},{8}", 
                StockLossID,OrderNum, OrderDate, WareHouseName, OutWhourseType,OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as LsBillTb  "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as LsBillPartTb on LsBillPartTb.{2}=LsBillTb.{2}",
                LossTable,LossPartTable, StockLossID,PartCount, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable LossBillTable = DBHelper.GetTableByPage(LossQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " LsBillTb.order_date desc", winFormLossPage.PageIndex, winFormLossPage.PageSize, out RecCount);//获取报损单表查询记录
                winFormLossPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的出入库单据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
                //把查询的报损单列表放入Gridview
                for (int i = 0; i < LossBillTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvLossBillList.Rows[gvLossBillList.Rows.Add()];//创建行项
                    gvRow.Cells["RepLossId"].Value = LossBillTable.Rows[i][StockLossID].ToString();//存放出入库单ID
                    gvRow.Cells["BillNum"].Value = LossBillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(LossBillTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();
                    gvRow.Cells["WHName"].Value = LossBillTable.Rows[i][WareHouseName].ToString();
                    gvRow.Cells["OutWhType"].Value = LossBillTable.Rows[i][OutWhourseType].ToString();
                    gvRow.Cells["TotalCount"].Value = LossBillTable.Rows[i]["PartAmount"].ToString();
                    gvRow.Cells["TotalMoney"].Value = LossBillTable.Rows[i]["PartMoney"].ToString();
                    gvRow.Cells["DepartName"].Value = LossBillTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["OpeName"].Value = LossBillTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = LossBillTable.Rows[i][Remark].ToString();
                    gvRow.Cells["OrderState"].Value = LossBillTable.Rows[i][OrderStatus].ToString();


                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 获取报损单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvLossBillList.Rows)
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
                        listField.Add(dr.Cells["RepLossId"].Value.ToString());
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
                if (!string.IsNullOrEmpty(ComBwh_name.Text.ToString()))
                {
                    Str_Where += " and LsBillTb.wh_name = '" + ComBwh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBout_wh_type_name.Text.ToString()))
                {
                    Str_Where += " and out_wh_type_name = '" + ComBout_wh_type_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorder_status_name.Text.ToString()))
                {
                    Str_Where += " and order_status_name='" + ComBorder_status_name.Text.ToString() + "'";
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
                 if (dateTimeStart.Value != null)
                {
                    Str_Where += " and LsBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and LsBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！");
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += "  and order_status_name='" + BillStatus + "'";
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
