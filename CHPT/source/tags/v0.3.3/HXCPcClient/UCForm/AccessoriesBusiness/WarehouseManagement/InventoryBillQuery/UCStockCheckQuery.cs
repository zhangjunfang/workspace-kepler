using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using Utility.Common;
using HXCPcClient.Chooser;
using System.Collections;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBill;
using SYSModel;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.InventoryBillQuery
{
    public partial class UCStockCheckQuery : UCBase
    {
        #region 全局变量

        private string CheckBillTable = "tb_parts_stock_check";//盘点单表
        private string CheckPartTable = "tb_parts_stock_check_p";//盘点单配件信息表
        private const string CheckID = "stock_check_id";//盘点单主键
        private string CheckQueryLogMsg = "查询盘点单表信息";//盘点单表操作日志
        private int SearchFlag = 0;

        //盘点单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string WhouseName = "wh_name";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        //盘点单配件表字段
        private const string PartPapCount = "paper_count";
        private const string PartFirmCount = "firmoffer_count";
        private const string PartProCount = "profitloss_count";
        private const string AmountMoney = "money";

        #endregion

        public UCStockCheckQuery()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            //DataGridViewEx.SetDataGridViewStyle(gvCheckQueryBillList, InOutState);//美化表格控件
            base.ExportEvent += new ClickHandler(UCStockCheckQuery_ExportEvent);
        }
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private  void UCStockCheckQuery_ExportEvent(object sender, EventArgs e)
        {
            BtnExportMenu.Show(btnExport,0,btnExport.Height);//指定导出菜单显示位置
        }

        private void UCRequisitionQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                //盘点单出入库状态
                CommonFuncCall.BindBillInOutStatus(ComBInOutStatus, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(ComBwh_name, "请选择");
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
        /// 选择公司
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
        /// 选择部门
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
            dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToString();
            ComBwh_name.SelectedIndex = 0;
            ComBInOutStatus.SelectedIndex = 0;
            ComBcom_name.SelectedIndex = 0;
            ComBorg_name.SelectedIndex = 0;
            ComBhandle_name.SelectedIndex = 0;
            txtremark.Caption = string.Empty;
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
                if (gvCheckQueryBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！");
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvCheckQueryBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列项
                        XlsTable.Columns.Add("单据编号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("仓库", typeof(string));
                        XlsTable.Columns.Add("账面数量", typeof(string));
                        XlsTable.Columns.Add("实盘数量", typeof(string));
                        XlsTable.Columns.Add("盈亏数量", typeof(string));
                        XlsTable.Columns.Add("金额", typeof(string));
                        XlsTable.Columns.Add("部门", typeof(string));
                        XlsTable.Columns.Add("经办人", typeof(string));
                        XlsTable.Columns.Add("操作人", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        XlsTable.Columns.Add("出入库状态", typeof(string));
                        TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                        TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                        TableRow["账面数量"] = dgRow.Cells["PapCount"].Value.ToString();
                        TableRow["实盘数量"] = dgRow.Cells["FirmCount"].Value.ToString();
                        TableRow["盈亏数量"] = dgRow.Cells["ProfitLosCount"].Value.ToString();
                        TableRow["金额"] = dgRow.Cells["Calcmoney"].Value.ToString();
                        TableRow["部门"] = dgRow.Cells["DepartName"].Value.ToString();
                        TableRow["经办人"] = dgRow.Cells["HandlerName"].Value.ToString();
                        TableRow["操作人"] = dgRow.Cells["OpeName"].Value.ToString();
                        TableRow["备注"] = dgRow.Cells["Remarks"].Value.ToString();
                        TableRow["出入库状态"] = dgRow.Cells["InOutState"].Value.ToString();
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
        /// 列表双击查看盘点单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCheckQueryBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string ChkIdValue = gvCheckQueryBillList.CurrentRow.Cells["ChkId"].Value.ToString();//获取出入库单ID
                    string WHName = gvCheckQueryBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCStockCheckDetail UCCheckBillDetails = new UCStockCheckDetail(ChkIdValue, WHName);
                    base.addUserControl(UCCheckBillDetails, "盘点单-查看", "UCCheckBillDetails" + ChkIdValue + "", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 查询所有盘点单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetCheckQueryBillList(QueryWhere);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormCheckQueryPage_PageIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and CkBillTb.create_time between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetCheckQueryBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvCheckQueryBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetCheckQueryBillList(QueryWhere);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 查询盘点单列表
        /// </summary>
        private void GetCheckQueryBillList(string WhereStr)
        {
            try
            {


                StringBuilder sbField = new StringBuilder();//盘点单查询字段集合
                sbField.AppendFormat("CkBillTb.{0},{1},{2},CkBillTb.{3},PapAmount,FirmAmount,ProAmount,AmtMoney,{4},{5},{6},CkBillTb.{7}", CheckID,
                OrderNum, OrderDate, WhouseName,OrgName, HandleName, OperatorName, Remark);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as CkBillTb inner join "+
                " (select {0}.{2},sum(3) as PapAmount,sum(4) as FirmAmount,sum(5) as ProAmount,sum(6) as AmtMoney from {0} inner join {1} "+
                "  on {0}.{2}={1}.{2} group by {0}.{2}) as CkBillPartTb on CkBillPartTb.{2}=CkBillTb.{2} ",
                CheckBillTable, CheckPartTable,CheckID,PartPapCount, PartFirmCount, PartProCount, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable CheckTable = DBHelper.GetTableByPage(CheckQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " order by CkBillTb.create_time desc", winFormCheckQueryPage.PageIndex, winFormCheckQueryPage.PageSize, out RecCount);//获取盘点单表查询记录
                winFormCheckQueryPage.RecordCount = RecCount;//获取总记录行
                //把查询的盘点单列表放入Gridview
                int SerialNum = 1;//记录行序号
                for (int i = 0; i < CheckTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvCheckQueryBillList.Rows[gvCheckQueryBillList.Rows.Add()];//创建行项
                    gvRow.Cells["ChkId"].Value = CheckTable.Rows[i][CheckID].ToString();//存放盘点单ID
                    gvRow.Cells["colIndex"].Value = SerialNum;
                    gvRow.Cells["BillNum"].Value = CheckTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(CheckTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();//单据日期
                    gvRow.Cells["WHName"].Value = CheckTable.Rows[i][WhouseName].ToString();
                    gvRow.Cells["PapCount"].Value = CheckTable.Rows[i]["PapAmount"].ToString();//账面数量
                    gvRow.Cells["FirmCount"].Value = CheckTable.Rows[i]["FirmAmount"].ToString();//实盘数量
                    gvRow.Cells["ProfitLosCount"].Value = CheckTable.Rows[i]["ProAmount"].ToString();//盈亏数量
                    if (Convert.ToInt32(gvRow.Cells["ProfitLosCount"].Value.ToString()) < 0)
                    {
                        gvRow.Cells["ProfitLosCount"].Style.ForeColor = Color.Red;
                    }
                    gvRow.Cells["Calcmoney"].Value = CheckTable.Rows[i]["AmtMoney"].ToString();//总金额
                    gvRow.Cells["DepartName"].Value = CheckTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["HandlerName"].Value = CheckTable.Rows[i][HandleName].ToString();
                    gvRow.Cells["OpeName"].Value = CheckTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = CheckTable.Rows[i][Remark].ToString();
                    SerialNum++;//序号自增加

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
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString() );//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString() );//结束时间
                if (!string.IsNullOrEmpty(ComBwh_name.Text.ToString()))
                {
                    Str_Where += " and CkBillTb." + WhouseName + " = '" + ComBwh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorg_name.Text.ToString()))
                {
                    Str_Where += " and " + OrgName + "='" + ComBorg_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBhandle_name.Text.ToString()))
                {
                    Str_Where += " and " + HandleName + "='" + ComBhandle_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and CkBillTb." + Remark + "='" + txtremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value != null)
                {
                    Str_Where += " and CkBillTb.create_time>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and CkBillTb.create_time<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) >= Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于等于结束时间！");
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
