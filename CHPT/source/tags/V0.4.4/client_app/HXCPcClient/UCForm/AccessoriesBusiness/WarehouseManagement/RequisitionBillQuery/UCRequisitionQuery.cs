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
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill;
using SYSModel;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBillQuery
{
    public partial class UCRequisitionQuery : UCBase
    {
        #region 全局变量
        //要查询的开单信息表名
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件信息表
        private const string AllotBillID = "stock_allot_id";//调拨单主键
        private string AllotBillLogMsg = "查询调拨单表信息";//调拨单查询日志
        private string BillState = "已开单";
        private const string ExportXlsName = "调拨单";
        private int SearchFlag = 0;//存放查询标志

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
        private const string InoutStatus = "order_status_name";
        private const string ComName = "com_name";
        //调拨单配件表字段
        private const string PartCount = "counts";
        private const string TotalMoney = "money";

        #endregion

        public UCRequisitionQuery()
        {
            InitializeComponent();
            base.ExportEvent += new ClickHandler(UCRequisitionQuery_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
           // DataGridViewEx.SetDataGridViewStyle(gvAllotQueryBillList, InOutState);//美化表格控件
        }

       private  void UCRequisitionQuery_ExportEvent(object sender, EventArgs e)
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

        private void UCRequisitionQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToShortDateString();
                //单据类型
                CommonFuncCall.BindAllocationBillType(ComBorder_type_name, true, "请选择");
                CommonFuncCall.BindCompany(ComBcall_in_org_name, "请选择");//调入机构
                //调出仓库
                CommonFuncCall.BindWarehouse(ComBcall_out_wh_name, "请选择");
                //调入仓库名称
                CommonFuncCall.BindWarehouse(ComBcall_in_wh_name, "请选择");
                //调拨单出入库状态
                CommonFuncCall.BindBillInOutStatus(Combinout_status_name, true, "请选择");
                //公司
                CommonFuncCall.BindCompany(ComBcom_name, "全部");
                //部门
                CommonFuncCall.BindDepartment(ComBorg_name, "", "全部");
                //经办人
                CommonFuncCall.BindHandle(ComBhandle_name, "", "全部");
                //运输方式
                CommonFuncCall.BindComBoxDataSource(Combtrans_way_name, "sys_trans_mode", "请选择");

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
        /// 查询调拨单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gvAllotQueryBillList.Rows.Clear();//清除原来的查询记录
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetAllotBillList(QueryWhere);
        }
        /// <summary>
        /// 分页查询调拨单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormAllotQueryPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and AtBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetAllotBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvAllotQueryBillList.Rows.Clear();//清除以前的查询结果
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
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();
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
        /// 导出Excel文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {

            try
            {
                if (gvAllotQueryBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
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
                    foreach (DataGridViewRow dgRow in gvAllotQueryBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项

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
                    ImportExportExcel.NPOIExportExcelFile(XlsTable, ExportXlsName);//生成Excel表格文件
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 列表双击查看调拨单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAllotQueryBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string AllotIdValue = gvAllotQueryBillList.CurrentRow.Cells["AllotID"].Value.ToString();//获取出入库单ID
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
        /// 查询调拨单列表
        /// </summary>
        private void GetAllotBillList(string WhereStr)
        {
            try
            {

                StringBuilder sbField = new StringBuilder();//调拨单查询字段集合
                sbField.AppendFormat("AtBillTb.{0},{1},{2},{3},{4},{5},{6},{7},PartAmount,PartMoney,{8},{9},{10},{11},{12},AtBillTb.{13},{14}", AllotBillID, OrderTypeName,
                OrderNum, OrderDate, CallOutOrg, CallOutWhouse, CallInOrg, CallInWhouse,TranWay, DeliveryAddr,
                OrgName, HandleName, OperatorName, Remark, InoutStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as AtBillTb  "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as AtBillPartTb  on AtBillPartTb.{2}=AtBillTb.{2}", AllotBillTable, AllotPartTable, AllotBillID, PartCount, TotalMoney);
                int RecCount = 0;//查询记录行数

                DataTable AllotTable = DBHelper.GetTableByPage(AllotBillLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", "AtBillTb.order_date desc", winFormAllotQueryPage.PageIndex, winFormAllotQueryPage.PageSize, out RecCount);//获取调拨单表查询记录
                winFormAllotQueryPage.RecordCount = RecCount;//获取总记录行
                if (AllotTable.Rows.Count ==0) return;
                //把查询的调拨单列表放入Gridview
                for (int i = 0; i < AllotTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvAllotQueryBillList.Rows[gvAllotQueryBillList.Rows.Add()];//创建行项
                    gvRow.Cells["AllotID"].Value = AllotTable.Rows[i][AllotBillID].ToString();//存放调拨单ID
                    gvRow.Cells["OrderType"].Value = AllotTable.Rows[i][OrderTypeName].ToString();
                    gvRow.Cells["BillNum"].Value = AllotTable.Rows[i][OrderNum].ToString();
                    DateTime OrdeDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(AllotTable.Rows[i][OrderDate].ToString()));
                    gvRow.Cells["BillDate"].Value = OrdeDate.ToLongDateString();//单据日期
                    gvRow.Cells["OutDepartment"].Value = AllotTable.Rows[i][CallOutOrg].ToString();
                    gvRow.Cells["OutWareHouse"].Value = AllotTable.Rows[i][CallOutWhouse].ToString();
                    gvRow.Cells["InDepartment"].Value = AllotTable.Rows[i][CallInOrg].ToString();
                    gvRow.Cells["InWareHouse"].Value = AllotTable.Rows[i][CallInWhouse].ToString();
                    gvRow.Cells["TotalCount"].Value = AllotTable.Rows[i]["PartAmount"].ToString();
                    gvRow.Cells["AmountMoney"].Value = AllotTable.Rows[i]["PartMoney"].ToString();
                    gvRow.Cells["DeliveryWays"].Value = AllotTable.Rows[i][TranWay].ToString();
                    gvRow.Cells["ArrivePlace"].Value = AllotTable.Rows[i][DeliveryAddr].ToString();
                    gvRow.Cells["DepartName"].Value = AllotTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["HandlerName"].Value = AllotTable.Rows[i][HandleName].ToString();
                    gvRow.Cells["OpeName"].Value = AllotTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = AllotTable.Rows[i][Remark].ToString();
                    gvRow.Cells["InOutState"].Value = AllotTable.Rows[i][InoutStatus].ToString();

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
                if (!string.IsNullOrEmpty(ComBorder_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + OrderTypeName + " = '" + ComBorder_type_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcall_out_wh_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + CallOutWhouse + "= '" + ComBcall_out_wh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcall_in_org_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + CallInOrg + "= '" + ComBcall_in_org_name.Text.ToString() + "'";
                }

                 if (!string.IsNullOrEmpty(ComBcall_in_wh_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + CallInWhouse + "='" + ComBcall_in_wh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBcom_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + ComName + "='" + ComBcom_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBorg_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + OrgName + "='" + ComBorg_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBhandle_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + HandleName + "='" + ComBhandle_name.Text.ToString() + "'";
                }
                else if (!string.IsNullOrEmpty(Combtrans_way_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + TranWay + "='" + Combtrans_way_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(Combinout_status_name.SelectedValue.ToString()))
                {
                    Str_Where += " and " + InoutStatus + "='" + Combinout_status_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtremark.Caption.ToString()))
                {
                    Str_Where += " and AtBillTb." + Remark + "='" + txtremark.Caption.ToString() + "'";
                }

                 if (dateTimeStart.Value != null)
                {
                    Str_Where += " and AtBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and AtBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
                }
                else
                {
                    Str_Where += " and AtBillTb.inout_status_name ='" + BillState+"'";
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
