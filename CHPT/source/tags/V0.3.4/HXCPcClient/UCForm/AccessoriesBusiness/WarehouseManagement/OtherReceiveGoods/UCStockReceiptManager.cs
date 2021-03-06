﻿using System;
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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods
{
    public partial class UCStockReceiptManager : UCBase
    {

        #region 全局变量
        private string ReceiptTable = "tb_parts_stock_receipt";//其它收货单表
        private string ReceiptPartTable = "tb_parts_stock_receipt_p";//其它收货单配件表
        private string ReceiptID = "stock_receipt_id";//其它收货单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string ReceiptQueryLogMsg = "查询其它收货单表信息";//其它收货单表操作日志
        private string ReceiptDelLogMsg = "批量删除其它收货单表信息";//其它收货单表操作日志
        private string ReceiptVerifyLogMsg = "批量审核其它收货单表信息";//其它收货单表操作日志
        private string ReceiptEdit = "编辑";
        private string ReceiptCopy = "复制";
        private int SearchFlag = 0;
        List<string> ReceiptIDValuelist = new List<string>();//存储选中其它收货单记录行主键ID
        //其它收货单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string InWhourseType = "in_wh_type";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        //其它收货单配件表字段
        private const string PartCount = "counts";
        private const string AmountMoney = "money";
        #endregion
        public UCStockReceiptManager()
        {
            InitializeComponent();

            //注册操作事件
            base.AddEvent += new ClickHandler(UCStockReceiptManager_AddEvent);
            base.EditEvent += new ClickHandler(UCStockReceiptManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCStockReceiptManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCStockReceiptManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCStockReceiptManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCStockReceiptManager_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            DataGridViewEx.SetDataGridViewStyle(gvReceiptBillList, OrderState);//美化表格控件
            //设置列表的可编辑状态
            gvReceiptBillList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvReceiptBillList.Columns)
            {
                if (dgCol.Name != colCheck.Name) dgCol.ReadOnly = true;
            }
            gvReceiptBillList.HeadCheckChanged += new DataGridViewEx.DelegateOnClick(gvReceiptBillList_HeadCheckChanged); //复选框标题显示为复选框状态 
            base.btnImport.Visible = false;
            base.btnCommit.Visible = false;
            base.btnStatus.Visible = false;
            base.btnImport.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSync.Visible = false;
        }
        //复选框选择状态
        private void gvReceiptBillList_HeadCheckChanged()
        {
            IsDataGridViewCheckBox();
        }
        /// <summary>
        /// 导出Excel文件菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void UCStockReceiptManager_ExportEvent(object sender, EventArgs e)
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
        private void UCStockReceiptManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                //入库类型
                CommonFuncCall.BindInOutType(ComBin_wh_type_name, true, "请选择");
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
                if (gvReceiptBillList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
                    foreach (DataGridViewRow dgRow in gvReceiptBillList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项
                        //创建表列项
                        XlsTable.Columns.Add("单据编号", typeof(string));
                        XlsTable.Columns.Add("单据日期", typeof(string));
                        XlsTable.Columns.Add("仓库", typeof(string));
                        XlsTable.Columns.Add("入库类型", typeof(string));
                        XlsTable.Columns.Add("业务数量", typeof(string));
                        XlsTable.Columns.Add("金额", typeof(string));
                        XlsTable.Columns.Add("部门", typeof(string));
                        XlsTable.Columns.Add("经办人", typeof(string));
                        XlsTable.Columns.Add("操作人", typeof(string));
                        XlsTable.Columns.Add("备注", typeof(string));
                        XlsTable.Columns.Add("单据状态", typeof(string));
                        TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                        TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                        TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                        TableRow["入库类型"] = dgRow.Cells["InWhType"].Value.ToString();
                        TableRow["业务数量"] = dgRow.Cells["TotalCount"].Value.ToString();
                        TableRow["金额"] = dgRow.Cells["TotalMoney"].Value.ToString();
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
        /// 添加其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptManager_AddEvent(object send, EventArgs e)
        {
            UCStockReceiptAddOrEdit UCReceiptBillAdd = new UCStockReceiptAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCReceiptBillAdd, "其它收货单-添加", "UCReceiptBillAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptManager_EditEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(ReceiptEdit, WindowStatus.Edit);
        }
        /// <summary>
        /// 复制其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptManager_CopyEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(ReceiptCopy, WindowStatus.Copy);
        }
        /// <summary>
        /// 删除其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的其它收货单记录ID
                if (listField.Count == 0 && gvReceiptBillList.SelectedRows.Count==0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> ReceiptBillField = new Dictionary<string, string>();
                        ReceiptBillField.Add("enable_flag", "0");
                        bool flag = DBHelper.BatchUpdateDataByIn(ReceiptDelLogMsg, ReceiptTable, ReceiptBillField, ReceiptID, listField.ToArray());//批量更改记录行删除标记为0已删除
                        if (flag)
                        {
                            string QueryWhere = " enable_flag=1 ";
                            GetReceiptBillList(QueryWhere);//刷新其它收货单列表
                            MessageBoxEx.Show("操作成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
        /// 审核其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptManager_VerifyEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetVerifyRecord();//获取需要核实的记录行

                if (listField.Count == 0 && gvReceiptBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合审核条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCVerify UcVerify = new UCVerify();
                UcVerify.ShowDialog();
                string Content = UcVerify.Content;
                DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;//获取审核状态

                Dictionary<string, string> ReceiptBillField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取其它收货单状态(已审核)
                    ReceiptBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    ReceiptBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取其它收货单状态(审核不通过)
                    ReceiptBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    ReceiptBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(ReceiptVerifyLogMsg, ReceiptTable, ReceiptBillField, ReceiptID, listField.ToArray());//批量审核获取的其它收货单记录
                if (flag)
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    string QueryWhere = " order_status_name='已审核通过' ";
                    GetReceiptBillList(QueryWhere);//刷新其它收货单列表

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
        /// 查询其它收货单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetReceiptBillList(QueryWhere);
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormReceiptPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and RptBillTb.create_time between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetReceiptBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvReceiptBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetReceiptBillList(QueryWhere);
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
            ComBin_wh_type_name.SelectedIndex = 0;
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
        /// 列表双击查看其它收货单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvReceiptBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string RecptIdValue = gvReceiptBillList.CurrentRow.Cells["RecptId"].Value.ToString();//获取出入库单ID
                    string WHName = gvReceiptBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCStockReceiptDetail UCReceiptDetails = new UCStockReceiptDetail(RecptIdValue, WHName);
                    base.addUserControl(UCReceiptDetails, "其它收货单-查看", "UCReceiptDetails" , this.Tag.ToString(), this.Name);
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 其它收货单编辑或复制
        /// </summary>
        /// <param name="HandleType"></param>
        /// <param name="state"></param>
        private void EditOrCopyMethod(string HandleType, WindowStatus state)
        {
            try
            {
                string ReceiptId = string.Empty;
                List<string> BillIDlist = GetSelectedRecord();//获取复制的其它收货单记录
                if (BillIDlist.Count == 0 && gvReceiptBillList.Rows.Count==0)
                {
                    MessageBoxEx.Show("请选择要" + HandleType + "的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (BillIDlist.Count > 1 && gvReceiptBillList.Rows.Count >1)
                {
                    MessageBoxEx.Show("一次只能" + HandleType + "一条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (BillIDlist.Count == 1)
                    {
                        ReceiptId = BillIDlist[0].ToString();
                    }
                    else if (gvReceiptBillList.SelectedRows.Count == 1)
                    {
                        ReceiptId = gvReceiptBillList.CurrentRow.Cells["RecptId"].ToString();
                    }
                    UCStockReceiptAddOrEdit UCReceiptBillHandle = new UCStockReceiptAddOrEdit(state, ReceiptId, this);
                    base.addUserControl(UCReceiptBillHandle, "其它收货单-" + HandleType, "UCReceiptBillHandle", this.Tag.ToString(), this.Name);
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
                gvReceiptBillList.EndEdit();//单据列表
                if (gvReceiptBillList.Rows.Count == 0)
                {
                    MessageBoxEx.Show("没有可以选择的记录行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    bool ischeck = (bool)((DataGridViewCheckBoxCell)gvReceiptBillList.Rows[0].Cells["colCheck"]).EditedFormattedValue;
                    bool SetCheck = ischeck == true ? true : false;
                    for (int i = 1; i < gvReceiptBillList.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)gvReceiptBillList.Rows[i].Cells["colCheck"]).Value = SetCheck;//开单列表选择状态
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 获取其它收货单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {

            if (ReceiptIDValuelist.Count > 0) ReceiptIDValuelist.Clear();//清除之前的数据
            foreach (DataGridViewRow dr in gvReceiptBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    ReceiptIDValuelist.Add(dr.Cells["RecptId"].Value.ToString());
                }
            }
            if (ReceiptIDValuelist.Count == 0 && gvReceiptBillList.SelectedRows.Count == 1)
            {
                string DelInoutId = gvReceiptBillList.CurrentRow.Cells["RecptId"].Value.ToString();
                ReceiptIDValuelist.Add(DelInoutId);
            }
            return ReceiptIDValuelist;
        }
        /// <summary>
        /// 查询其它收货单列表
        /// </summary>
        public void GetReceiptBillList(string WhereStr)
        {
            try
            {

                StringBuilder sbField = new StringBuilder();//其它收货单查询字段集合
                sbField.AppendFormat("RptBillTb.{0},{1},{2},RptBillTb.{3},{4},PartAmount,PartMoney,{5},{6},RptBillTb.{7},{8}", ReceiptID,
                OrderNum, OrderDate, WareHouseName, InWhourseType,OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as RptBillTb inner join {1} as RptPartTb on RptBillTb.{2}=RptPartTb.{2} "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as RptBillPartTb  on RptBillPartTb.{2}=RptBillTb.{2}", ReceiptTable,
                ReceiptPartTable,ReceiptID,PartCount, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable ReceiptBillTable = DBHelper.GetTableByPage(ReceiptQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " RptBillTb.order_date desc", winFormReceiptPage.PageIndex, winFormReceiptPage.PageSize, out RecCount);//获取出入库单表查询记录
                winFormReceiptPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    MessageBoxEx.Show("报歉没有找到您要的其它收货单据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
                //把查询的其它收货单列表放入Gridview
                for (int i = 0; i < ReceiptBillTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvReceiptBillList.Rows[gvReceiptBillList.Rows.Add()];//创建行项
                    gvRow.Cells["RecptId"].Value = ReceiptBillTable.Rows[i][ReceiptID].ToString();//存放出入库单ID
                    gvRow.Cells["BillNum"].Value = ReceiptBillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(ReceiptBillTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();
                    gvRow.Cells["WHName"].Value = ReceiptBillTable.Rows[i][WareHouseName].ToString();
                    gvRow.Cells["InWhType"].Value = ReceiptBillTable.Rows[i][InWhourseType].ToString();
                    gvRow.Cells["TotalCount"].Value = ReceiptBillTable.Rows[i]["PartAmount"].ToString();
                    gvRow.Cells["TotalMoney"].Value = ReceiptBillTable.Rows[i]["PartMoney"].ToString();
                    gvRow.Cells["DepartName"].Value = ReceiptBillTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["OpeName"].Value = ReceiptBillTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = ReceiptBillTable.Rows[i][Remark].ToString();
                    gvRow.Cells["OrderState"].Value = ReceiptBillTable.Rows[i][OrderStatus].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 获取其它收货单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvReceiptBillList.Rows)
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
                        listField.Add(dr.Cells["RecptId"].Value.ToString());
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
                string Str_Where = " enable_flag=1 "; //未删除记录行
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString());//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(ComBwh_name.Text.ToString()))
                {
                    Str_Where += " and RptBillTb.wh_name = '" + ComBwh_name.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ComBin_wh_type_name.Text.ToString()))
                {
                    Str_Where += " and in_wh_type_name = '" + ComBin_wh_type_name.Text.ToString() + "'";
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


                 if (dateTimeStart.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value.ToString() != null)
                {

                    Str_Where += " and RptBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) > Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Str_Where = string.Empty;
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
