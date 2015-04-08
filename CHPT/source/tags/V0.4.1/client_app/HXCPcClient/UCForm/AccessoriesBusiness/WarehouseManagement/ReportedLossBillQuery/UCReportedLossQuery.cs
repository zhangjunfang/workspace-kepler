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
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBillQuery
{
    public partial class UCReportedLossQuery : UCBase
    {
        #region 全局变量
        private string LossTable = "tb_parts_stock_loss";//报损单表
        private string LossPartTable = "tb_parts_stock_loss_p";//报损单配件表
        private string StockLossID = "stock_loss_id";//报损单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string LossQueryLogMsg = "查询报损单表信息";//报损单表操作日志
        private string BillState = "已开单";
        private int SearchFlag = 0;//存放标志
        private const string ExportXlsName = "报损单";
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

        public UCReportedLossQuery()
        {
            InitializeComponent();
            base.ExportEvent += new ClickHandler(UCReportedLossQuery_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            DataGridViewEx.SetDataGridViewStyle(gvLossQueryBillList, OutWhState);//美化表格控件
        }
        /// <summary>
        /// 导出Excel文件菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void UCReportedLossQuery_ExportEvent(object sender, EventArgs e)
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
        private void UCReportedLossQuery_Load(object sender, EventArgs e)
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
                if (gvLossQueryBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
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
                    foreach (DataGridViewRow dgRow in gvLossQueryBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项

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
                    ImportExportExcel.NPOIExportExcelFile(XlsTable, ExportXlsName);//生成Excel表格文件
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
        private void winFormLossQueryPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetRepLossBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvLossQueryBillList.Rows.Clear();
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
        private void gvLossQueryBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                 if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                    {
                        string LossIdValue = gvLossQueryBillList.CurrentRow.Cells["RepLossId"].Value.ToString();//获取出入库单ID
                        string WHName = gvLossQueryBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
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
        /// 获取报损单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {

            if (LossIDValuelist.Count > 0) LossIDValuelist.Clear();//清除之前的数据
            foreach (DataGridViewRow dr in gvLossQueryBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    LossIDValuelist.Add(dr.Cells["RepLossId"].Value.ToString());
                }
            }
            if (LossIDValuelist.Count == 0 && gvLossQueryBillList.SelectedRows.Count == 1)
            {
                string DelInoutId = gvLossQueryBillList.CurrentRow.Cells["RepLossId"].Value.ToString();
                LossIDValuelist.Add(DelInoutId);
            }
            return LossIDValuelist;
        }
        /// <summary>
        /// 查询报损单列表
        /// </summary>
        private void GetRepLossBillList(string WhereStr)
        {
            try
            {

                StringBuilder sbField = new StringBuilder();//报损单查询字段集合
                sbField.AppendFormat("LsBillTb.{0},{1},{2},LsBillTb.{3},{4},PartAmount,PartMoney,{5},{6},LsBillTb.{7},{8}",
                StockLossID,OrderNum, OrderDate, WareHouseName, OutWhourseType, OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as LsBillTb "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as LsBillPartTb  on LsBillPartTb.{2}=LsBillTb.{2}",
                LossTable,LossPartTable, StockLossID, PartCount, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable LossBillTable = DBHelper.GetTableByPage(LossQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                StockLossID, " order by LsBillTb.create_time desc", winFormLossQueryPage.PageIndex, winFormLossQueryPage.PageSize, out RecCount);//获取报损单表查询记录
                winFormLossQueryPage.RecordCount = RecCount;//获取总记录行
                //把查询的报损单列表放入Gridview
                for (int i = 0; i < LossBillTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvLossQueryBillList.Rows[gvLossQueryBillList.Rows.Add()];//创建行项
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
                if (!string.IsNullOrEmpty(ComBwh_name.SelectedValue.ToString()))
                {
                    Str_Where += " and LsBillTb.wh_name = '" + ComBwh_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBout_wh_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and out_wh_type_name = '" + ComBout_wh_type_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBorder_status_name.SelectedValue.ToString()))
                {
                    Str_Where += " and LsBillTb.order_status_name='" + ComBorder_status_name.Text.ToString() + "'";
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
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and LsBillTb.order_status_name='" + BillState + "'";
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
