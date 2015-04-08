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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherSendGoods
{
    public partial class UCStockShippingManager : UCBase
    {

        #region 全局变量
        private string ShippingTable = "tb_parts_stock_shipping";//其它发单表
        private string ShippingPartTable = "tb_parts_stock_shipping_p";//其它发货单配件表
        private string ShippingID = "stock_shipping_id";//其它发货单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string ShippingQueryLogMsg = "查询其它发货单表信息";//其它发货单表操作日志
        private string ShippingDelLogMsg = "批量删除其它发货单表信息";//其它发货单表操作日志
        private string ShippingVerifyLogMsg = "批量审核其它发货单表信息";//其它发货单表操作日志
        private string ShipSubmitLogMsg = "提交其它发货单表信息";//提交其它发货单日志
        private string ShippingEdit = "编辑";
        private string ShippingCopy = "复制";
        private const string ExportXlsName = "其它发货单";
        private int SearchFlag = 0;
        List<string> ShippingIDValuelist = new List<string>();//存储选中其它发货单记录行主键ID
        //其它发货单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string OutWhourseType = "out_wh_type_name";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        //其它发货单配件表字段
        private const string PartCount = "counts";
        private const string AmountMoney = "money";
        #endregion
        public UCStockShippingManager()
        {
            InitializeComponent();

            //注册操作事件
            base.AddEvent += new ClickHandler(UCStockShippingManager_AddEvent);
            base.EditEvent += new ClickHandler(UCStockShippingManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCStockShippingManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCStockShippingManager_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCStockShippingManager_SubmitEvent);
            base.VerifyEvent += new ClickHandler(UCStockShippingManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCStockShippingManager_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            DataGridViewEx.SetDataGridViewStyle(gvShippingBillList, OrderState);//美化表格控件
            //设置列表的可编辑状态
            gvShippingBillList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvShippingBillList.Columns)
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
        /// 提交其它发货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private  void UCStockShippingManager_SubmitEvent(object sender, EventArgs e)
        {

            try
            {
                List<string> ShipIdLst = GetSubmitRecord();//获取需提交的单据记录
                if (ShipIdLst.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合提交条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Dictionary<string, string> ShipBillField = new Dictionary<string, string>();//存放更新字段
                    ShipBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//状态ID
                    ShipBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//状态name
                    bool flag = DBHelper.BatchUpdateDataByIn(ShipSubmitLogMsg, ShippingTable, ShipBillField, ShippingID, ShipIdLst.ToArray());
                    if (flag)
                    {
                        MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1 and order_status_name='已提交' and ShpBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        GetShippingBillList(DefaultWhere);//刷新出入库单列表
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
               if (ShippingIDValuelist.Count > 0) ShippingIDValuelist.Clear();//清除之前的数据
               foreach (DataGridViewRow dr in gvShippingBillList.Rows)
               {
                   bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                   if (isCheck != null && isCheck)
                   {
                       //获取保存草稿状态的单据记录
                       string BillStatusDraft = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                       string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                       if (BillStatusDraft == ColOrderStatus)
                       {
                           ShippingIDValuelist.Add(dr.Cells["ShipId"].Value.ToString());//添加草稿状态主键ID
                       }
                   }
               }
               return ShippingIDValuelist;

           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
               return null;
           }

       }


       private void UCStockShippingManager_ExportEvent(object sender, EventArgs e)
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
        private void UCStockShippingManager_Load(object sender, EventArgs e)
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
                if (gvShippingBillList.Rows.Count == 0) //判断gridview中是否有数据记录
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
                    XlsTable.Columns.Add("单据状态", typeof(string));
                    foreach (DataGridViewRow dgRow in gvShippingBillList.Rows)
                    {
                        bool SelectFlag=(bool)((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                        if (SelectFlag == true)
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
        /// 添加其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingManager_AddEvent(object send, EventArgs e)
        {
            UCStockShippingAddOrEdit UCShippingBillAdd = new UCStockShippingAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCShippingBillAdd, "其它发货单-添加", "UCShippingBillAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingManager_EditEvent(object send, EventArgs e)
        {

            EditOrCopyMethod(ShippingEdit, WindowStatus.Edit);
        }
        /// <summary>
        /// 复制其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingManager_CopyEvent(object send, EventArgs e)
        {
            EditOrCopyMethod(ShippingCopy, WindowStatus.Copy);
        }
        /// <summary>
        /// 删除其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的其它发货单记录ID
                if (listField.Count == 0 && gvShippingBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> ShippingBillField = new Dictionary<string, string>();
                        ShippingBillField.Add("enable_flag", "0");
                        bool flag = DBHelper.BatchUpdateDataByIn(ShippingDelLogMsg, ShippingTable, ShippingBillField, ShippingID, listField.ToArray());//批量更改记录行删除标记为0已删除
                        if (flag)
                        {
                            long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                            long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                            string DefaultWhere = " enable_flag=1  and ShpBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                            GetShippingBillList(DefaultWhere);//刷新其它发货单列表
                            MessageBoxEx.Show("操作成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        }
                        else
                        { MessageBoxEx.Show("操作失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); }
                    }
                    else
                    {
                        return;
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 审核其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingManager_VerifyEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetVerifyRecord();//获取需要核实的记录行
                if (listField.Count == 0 && gvShippingBillList.SelectedRows.Count==0)
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
                    //获取其它发货单状态(已审核)
                    ShippingBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    ShippingBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取其它发货单状态(审核不通过)
                    ShippingBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    ShippingBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(ShippingVerifyLogMsg, ShippingTable, ShippingBillField, ShippingID, listField.ToArray());//批量审核获取的其它发货单记录
                if (flag)
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and order_status_name='已审核通过' and ShpBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetShippingBillList(DefaultWhere);//刷新其它发货单列表

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
        /// 编辑或者复制其它发货单
        /// </summary>
        /// <param name="HandleType"></param>
        /// <param name="state"></param>
        private void EditOrCopyMethod(string HandleType, WindowStatus state)
        {
            try
            {
                string ShippingBillID = string.Empty;
                List<string> ShippingBillIDList = GetSelectedRecord();//获取要编辑或复制的其它发货单记录行

                if (ShippingBillIDList.Count == 0 && gvShippingBillList.SelectedRows.Count==0)
                {
                    MessageBoxEx.Show("请选择要" + HandleType + "的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (ShippingBillIDList.Count > 1 && gvShippingBillList.SelectedRows.Count >1)
                {
                    MessageBoxEx.Show("一次只能" + HandleType + "一条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ShippingBillIDList.Count == 1)
                    {
                        ShippingBillID = ShippingBillIDList[0].ToString();
                    }
                    else if (gvShippingBillList.SelectedRows.Count == 1)
                    {
                        ShippingBillID = gvShippingBillList.CurrentRow.Cells["ShipId"].ToString();
                    }
                    UCStockShippingAddOrEdit UCShippingBillHandle = new UCStockShippingAddOrEdit(state, ShippingBillID, this);
                    base.addUserControl(UCShippingBillHandle, "其它发货单-" + HandleType, "UCShippingBillHandle", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                 MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 查询其它发货单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetShippingBillList(QueryWhere);
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
                    string DefaultWhere = " enable_flag=1 and ShpBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetShippingBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvShippingBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetShippingBillList(QueryWhere);
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
        /// 列表双击查看其它发货单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvShippingBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string ShipIdValue = gvShippingBillList.CurrentRow.Cells["ShipId"].Value.ToString();//获取出入库单ID
                    string WHName = gvShippingBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCStockShippingDetail UCShippingDetails = new UCStockShippingDetail(ShipIdValue, WHName);
                    base.addUserControl(UCShippingDetails, "其它发货单-查看", "UCShippingDetails", this.Tag.ToString(), this.Name);
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 获取其它发货单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {

            if (ShippingIDValuelist.Count > 0) ShippingIDValuelist.Clear();//清除之前的数据
            foreach (DataGridViewRow dr in gvShippingBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    ShippingIDValuelist.Add(dr.Cells["ShipId"].Value.ToString());
                }
            }
            if (ShippingIDValuelist.Count == 0 && gvShippingBillList.SelectedRows.Count == 1)
            {
                string DelInoutId = gvShippingBillList.CurrentRow.Cells["ShipId"].Value.ToString();
                ShippingIDValuelist.Add(DelInoutId);
            }
            return ShippingIDValuelist;
        }
        /// <summary>
        /// 查询其它发货单列表
        /// </summary>
        public void GetShippingBillList(string WhereStr)
        {
            try
            {
                gvShippingBillList.Rows.Clear();//清除所有记录行
                StringBuilder sbField = new StringBuilder();//其它发货单查询字段集合
                sbField.AppendFormat("ShpBillTb.{0},{1},{2},ShpBillTb.{3},{4},PartAmount, PartMoney,{5},{6},ShpBillTb.{7},{8}", ShippingID,
                OrderNum, OrderDate, WareHouseName, OutWhourseType,OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as ShpBillTb "+
                " inner join (select {0}.{2},sum({3}) as PartAmount,sum({4}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as ShpBillPartTb  on  ShpBillPartTb.{2}=ShpBillTb.{2} ", ShippingTable,
                ShippingPartTable, ShippingID, PartCount, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable ShipBillTable = DBHelper.GetTableByPage(ShippingQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " ShpBillTb.order_date desc", winFormShippingPage.PageIndex, winFormShippingPage.PageSize, out RecCount);//获取出入库单表查询记录
                winFormShippingPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    return;

                }
                //把查询的其它发货单列表放入Gridview
                for (int i = 0; i < ShipBillTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvShippingBillList.Rows[gvShippingBillList.Rows.Add()];//创建行项
                    gvRow.Cells["ShipId"].Value = ShipBillTable.Rows[i][ShippingID].ToString();//存放出入库单ID
                    gvRow.Cells["BillNum"].Value = ShipBillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(ShipBillTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();
                    gvRow.Cells["WHName"].Value = ShipBillTable.Rows[i][WareHouseName].ToString();
                    gvRow.Cells["OutWhType"].Value = ShipBillTable.Rows[i][OutWhourseType].ToString();
                    gvRow.Cells["TotalCount"].Value = ShipBillTable.Rows[i]["PartAmount"].ToString();
                    gvRow.Cells["TotalMoney"].Value = ShipBillTable.Rows[i]["PartMoney"].ToString();
                    gvRow.Cells["DepartName"].Value = ShipBillTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["OpeName"].Value = ShipBillTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = ShipBillTable.Rows[i][Remark].ToString();
                    gvRow.Cells["OrderState"].Value = ShipBillTable.Rows[i][OrderStatus].ToString();


                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 获取其它发货单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvShippingBillList.Rows)
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
                        listField.Add(dr.Cells["ShipId"].Value.ToString());
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
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString() + " 00:00:00");//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString() + " 23:59:59");//结束时间
                if (!string.IsNullOrEmpty(ComBwh_name.SelectedValue.ToString()))
                {
                    Str_Where += " and ShpBillTb.wh_name = '" + ComBwh_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBout_wh_type_name.SelectedValue.ToString()))
                {
                    Str_Where += " and out_wh_type_name = '" + ComBout_wh_type_name.Text.ToString() + "'";
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
                 if (dateTimeStart.Value != null)
                {
                    Str_Where += " and ShpBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                 if (dateTimeEnd.Value != null)
                {

                    Str_Where += " and ShpBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
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
