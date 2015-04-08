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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ModifyPriceBill
{
    public partial class UCModifyPriceManager : UCBase
    {


        #region 全局变量
        private string ModifyPriceBillTable = "tb_parts_stock_modifyprice";//调价单表
        private string ModifyPricePartTable = "tb_parts_stock_modifyprice_p";//调价单配件表
        private string ModifyPriceID = "stock_modifyprice_id";//调价单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string ModifyPriceQueryLogMsg = "查询调价单表信息";//调价单表操作日志
        private string ModifyPriceDelLogMsg = "批量删除调价单表信息";//调价单表操作日志
        private string ModifyPriceVerifyLogMsg = "批量审核调价单表信息";//调价单表操作日志
        private string ModifyPriceSubmitLogMsg = "提交调价单更新信息";//调价单提交日志
        private string ModPricEdit = "编辑";
        private string ModPricCopy = "复制";
        private const string ExportXlsName = "调价单";
        private int SearchFlag = 0;//搜索标志
        private string VerifyFailure = DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true);//审核失败
        List<string> ModPricIDValuelist = new List<string>();//存储选中调价单记录行主键ID
        //调价单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string ModPricNum = "modifyprice_doc_num";
        private const string ModPricRate = "modifyprice_ratio";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        //调价单配件表字段
        private const string AmountMoney = "money";
        #endregion
        public UCModifyPriceManager()
        {
            InitializeComponent();

            //注册操作事件
            base.AddEvent += new ClickHandler(UCModifyPriceManager_AddEvent);
            base.EditEvent += new ClickHandler(UCModifyPriceManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCModifyPriceManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCModifyPriceManager_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCModifyPriceManager_SubmitEvent);
            base.VerifyEvent += new ClickHandler(UCModifyPriceManager_VerifyEvent);
            base.ExportEvent += new ClickHandler(UCModifyPriceManager_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);//美化查询和清除按钮控件
            DataGridViewEx.SetDataGridViewStyle(gvModifyPriceBillList, OrderState);//美化表格控件

            //设置列表的可编辑状态
            gvModifyPriceBillList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvModifyPriceBillList.Columns)
            {
                if (dgCol.Name != colCheck.Name) dgCol.ReadOnly = true;
            }
            base.btnImport.Visible = false;
            base.btnCommit.Visible = false;
            base.btnStatus.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSync.Visible = false;
        }
        /// <summary>
        /// 提交调价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private  void  UCModifyPriceManager_SubmitEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> MdyPricIdLst = GetSubmitRecord();//获取需提交的单据记录
                if (MdyPricIdLst.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合提交条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Dictionary<string, string> ModifyBillField = new Dictionary<string, string>();//存放更新字段
                    ModifyBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//状态ID
                    ModifyBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//状态name
                    bool flag = DBHelper.BatchUpdateDataByIn(ModifyPriceSubmitLogMsg, ModifyPriceBillTable, ModifyBillField, ModifyPriceID, MdyPricIdLst.ToArray());
                    if (flag)
                    {
                        MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1 and MdyBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        GetModifyPriceBillList(DefaultWhere);//刷新出入库单列表
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
               if (ModPricIDValuelist.Count > 0) ModPricIDValuelist.Clear();//清除之前的数据
               foreach (DataGridViewRow dr in gvModifyPriceBillList.Rows)
               {
                   bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                   if (isCheck != null && isCheck)
                   {
                       //获取保存草稿状态的单据记录
                       string BillStatusDraft = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                       string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                       if (BillStatusDraft == ColOrderStatus)
                       {
                           ModPricIDValuelist.Add(dr.Cells["ModPricId"].Value.ToString());//添加草稿状态主键ID
                       }
                   }
               }
               return ModPricIDValuelist;

           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
               return null;
           }

       }

       private  void UCModifyPriceManager_ExportEvent(object sender, EventArgs e)
       {
           try
           {
               if (gvModifyPriceBillList.Rows.Count == 0) //判断gridview中是否有数据记录
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
                   XlsTable.Columns.Add("调价文号", typeof(string));
                   XlsTable.Columns.Add("调价比率", typeof(string));
                   XlsTable.Columns.Add("金额", typeof(string));
                   XlsTable.Columns.Add("部门", typeof(string));
                   XlsTable.Columns.Add("经办人", typeof(string));
                   XlsTable.Columns.Add("操作人", typeof(string));
                   XlsTable.Columns.Add("备注", typeof(string));
                   XlsTable.Columns.Add("单据状态", typeof(string));
                   foreach (DataGridViewRow dgRow in gvModifyPriceBillList.Rows)
                   {
                       bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                       if (SelectFlag == true)
                       {
                           DataRow TableRow = XlsTable.NewRow();//创建表行项

                           TableRow["单据编号"] = dgRow.Cells["BillNum"].Value.ToString();
                           TableRow["单据日期"] = dgRow.Cells["BillDate"].Value.ToString();
                           TableRow["仓库"] = dgRow.Cells["WHName"].Value.ToString();
                           TableRow["调价文号"] = dgRow.Cells["ModifyPriceNum"].Value.ToString();
                           TableRow["调价比率"] = dgRow.Cells["ModifyPriceRate"].Value.ToString();
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
        ///  窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();

                //获取仓库名称
                CommonFuncCall.BindWarehouse(ComBwh_Name, "请选择");
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
        /// 查询调价单操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string QueryWhere = BuildWhereCondation();//获取查询条件
            if (string.IsNullOrEmpty(QueryWhere)) return;
            GetModifyPriceBillList(QueryWhere);
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormModifyPage_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchFlag == (int)DataSources.SearchState.InitSearch)
                {

                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and MdyBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetModifyPriceBillList(DefaultWhere);
                    SearchFlag = (int)DataSources.SearchState.WhereSearch;
                }
                else if (SearchFlag == (int)DataSources.SearchState.WhereSearch)
                {
                    gvModifyPriceBillList.Rows.Clear();
                    string QueryWhere = "";//获取查询条件
                    GetModifyPriceBillList(QueryWhere);
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
            dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToString();
            txtremark.Caption = string.Empty;
            ComBwh_Name.SelectedIndex = 0;
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
        /// 添加调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_AddEvent(object send, EventArgs e)
        {
            UCModifyPriceAddOrEdit UCModPriceBillAdd = new UCModifyPriceAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCModPriceBillAdd, "调价单-添加", "UCModPriceBillAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_EditEvent(object send, EventArgs e)
        {

            EditOrCopyMethod(ModPricEdit, WindowStatus.Edit);

        }
        /// <summary>
        /// 复制调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_CopyEvent(object send, EventArgs e)
        {

            EditOrCopyMethod(ModPricCopy, WindowStatus.Copy);

        }
        /// <summary>
        /// 删除调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_DeleteEvent(object send, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();//获取要删除的其它收货单记录ID
                if (listField.Count == 0 && gvModifyPriceBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要删除的单据!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    DialogResult DgResult = MessageBoxEx.Show("确定要删除选中单据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.OK)
                    {
                        Dictionary<string, string> ModPricBillField = new Dictionary<string, string>();
                        ModPricBillField.Add("enable_flag", "0");
                        bool flag = DBHelper.BatchUpdateDataByIn(ModifyPriceDelLogMsg, ModifyPriceBillTable, ModPricBillField, ModifyPriceID, listField.ToArray());//批量更改记录行删除标记为0已删除
                        if (flag)
                        {

                            long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                            long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                            string DefaultWhere = " enable_flag=1  and MdyBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                            GetModifyPriceBillList(DefaultWhere);//刷新调价单列表
                            MessageBoxEx.Show("删除成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 审核调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceManager_VerifyEvent(object send, EventArgs e)
        {

            try
            {
                List<string> listField = GetVerifyRecord();//获取需要核实的记录行

                if (listField.Count == 0 && gvModifyPriceBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合审核条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCVerify UcVerify = new UCVerify();
                UcVerify.ShowDialog();
                string Content = UcVerify.Content;
                DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;//获取审核状态

                Dictionary<string, string> ModPricBillField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取调价单状态(已审核)
                    ModPricBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    ModPricBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取调价单状态(审核不通过)
                    ModPricBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    ModPricBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn(ModifyPriceVerifyLogMsg, ModifyPriceBillTable, ModPricBillField, ModifyPriceID, listField.ToArray());//批量审核获取的调价单记录
                if (flag)
                {
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                    long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                    string DefaultWhere = " enable_flag=1 and  MdyBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                    GetModifyPriceBillList(DefaultWhere);//刷新调价单列表

                }
                else
                {
                    MessageBoxEx.Show("审核失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 调价单编辑或者复制
        /// </summary>
        /// <param name="HandlerType">处理方法</param>
        /// <param name="state">操作状态</param>
        private void EditOrCopyMethod(string HandlerType, WindowStatus state)
        {
            try
            {
                string ModPricBillID = string.Empty;
                List<string> ModPricBillIDList = GetSelectedRecord();//获取要编辑的调价单记录行

                if (ModPricBillIDList.Count == 0 && gvModifyPriceBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择要" + HandlerType + "的数据!" ,"提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else if (ModPricBillIDList.Count > 1 && gvModifyPriceBillList.SelectedRows.Count > 1)
                {
                    MessageBoxEx.Show("一次只能" + HandlerType + "一条数据!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (ModPricBillIDList.Count == 1)
                    {
                        ModPricBillID = ModPricBillIDList[0].ToString();
                    }
                    else if (gvModifyPriceBillList.SelectedRows.Count==1)
                    {
                        ModPricBillID = gvModifyPriceBillList.CurrentRow.Cells["ModPricId"].ToString();
                    }
                    UCModifyPriceAddOrEdit UCModPricBillHandle = new UCModifyPriceAddOrEdit(state, ModPricBillID, this);
                    base.addUserControl(UCModPricBillHandle, "调价单-" + HandlerType, "UCModPricBillHandle", this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 列表双击查看调价单配件明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvModifyPriceBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    string ModPricIdValue = gvModifyPriceBillList.CurrentRow.Cells["ModPricId"].Value.ToString();//获取其它收货单ID
                    string WHName = gvModifyPriceBillList.CurrentRow.Cells["WHName"].Value.ToString();//获取当前出入库单仓库名称
                    UCModifyPriceDetail UCModPricBillDetails = new UCModifyPriceDetail(ModPricIdValue, WHName);
                    base.addUserControl(UCModPricBillDetails, "调价单-查看", "UCModPricBillDetails" + ModPricIdValue, this.Tag.ToString(), this.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }

        /// <summary>
        /// 获取调价单列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {

            if (ModPricIDValuelist.Count > 0) ModPricIDValuelist.Clear();//清除之前的数据
            foreach (DataGridViewRow dr in gvModifyPriceBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    ModPricIDValuelist.Add(dr.Cells["ModPricId"].Value.ToString());
                }
            }
            if (ModPricIDValuelist.Count == 0 && gvModifyPriceBillList.SelectedRows.Count == 1)
            {
                string DelInoutId = gvModifyPriceBillList.CurrentRow.Cells["ModPricId"].Value.ToString();
                ModPricIDValuelist.Add(DelInoutId);
            }
            return ModPricIDValuelist;
        }



        /// <summary>
        /// 查询调价单列表
        /// </summary>
        public void GetModifyPriceBillList(string WhereStr)
        {
            try
            {
                gvModifyPriceBillList.Rows.Clear();//清空所有记录行
                StringBuilder sbField = new StringBuilder();//调价单查询字段集合
                sbField.AppendFormat("MdyBillTb.{0},{1},{2},MdyBillTb.{3},{4},{5},PartMoney,{6},{7},MdyBillTb.{8},{9}", ModifyPriceID,
                OrderNum, OrderDate, WareHouseName, ModPricNum, ModPricRate, OrgName, OperatorName, Remark, OrderStatus);
                StringBuilder sbRelationTable = new StringBuilder();//关联多表查询
                sbRelationTable.AppendFormat("{0} as MdyBillTb  "+
                " inner join (select {0}.{2},sum({3}) as PartMoney from {0} inner join {1} on {0}.{2}={1}.{2} group by {0}.{2})"+
                " as MdyBillPartTb  on MdyBillPartTb.{2}=MdyBillTb.{2}", ModifyPriceBillTable, ModifyPricePartTable, ModifyPriceID, AmountMoney);
                int RecCount = 0;//查询记录行数

                DataTable ModifyPriceTable = DBHelper.GetTableByPage(ModifyPriceQueryLogMsg, sbRelationTable.ToString(), sbField.ToString(), WhereStr,
                "", " MdyBillTb.create_time desc", winFormModifyPage.PageIndex, winFormModifyPage.PageSize, out RecCount);//获取出入库单表查询记录
                winFormModifyPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    return;

                }
                //把查询的调价单列表放入Gridview

                for (int i = 0; i < ModifyPriceTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvModifyPriceBillList.Rows[gvModifyPriceBillList.Rows.Add()];//创建行项
                    gvRow.Cells["ModPricId"].Value = ModifyPriceTable.Rows[i][ModifyPriceID].ToString();//存放出入库单ID
                    if (ModifyPriceTable.Rows[i][OrderStatus].ToString() == DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true))
                    {
                        gvRow.Cells["BillNum"].Value = string.Empty;
                    }
                    else
                    {
                        gvRow.Cells["BillNum"].Value = ModifyPriceTable.Rows[i][OrderNum].ToString();
                    }
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(ModifyPriceTable.Rows[i][OrderDate]));//获取单据日期
                    gvRow.Cells["BillDate"].Value = OrdDate.ToLongDateString();//单据日期
                    gvRow.Cells["WHName"].Value = ModifyPriceTable.Rows[i][WareHouseName].ToString();
                    gvRow.Cells["ModifyPriceNum"].Value = ModifyPriceTable.Rows[i][ModPricNum].ToString();//ModPricRate
                    gvRow.Cells["ModifyPriceRate"].Value = ModifyPriceTable.Rows[i][ModPricRate].ToString();
                    if (Convert.ToDecimal(ModifyPriceTable.Rows[i]["PartMoney"].ToString()) < decimal.Zero)
                    {
                        gvRow.Cells["TotalMoney"].Style.ForeColor = Color.Red;
                    }
                    gvRow.Cells["TotalMoney"].Value = ModifyPriceTable.Rows[i]["PartMoney"].ToString();
                    gvRow.Cells["DepartName"].Value = ModifyPriceTable.Rows[i][OrgName].ToString();
                    gvRow.Cells["OpeName"].Value = ModifyPriceTable.Rows[i][OperatorName].ToString();
                    gvRow.Cells["Remarks"].Value = ModifyPriceTable.Rows[i][Remark].ToString();
                    if (ModifyPriceTable.Rows[i][OrderStatus].ToString() == VerifyFailure)
                    {
                        gvRow.Cells["TotalMoney"].Style.ForeColor = Color.Red;
                    }
                    gvRow.Cells["OrderState"].Value = ModifyPriceTable.Rows[i][OrderStatus].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }


        /// <summary>
        /// 获取调价单列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvModifyPriceBillList.Rows)
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
                        listField.Add(dr.Cells["ModPricId"].Value.ToString());
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
                DateTime dStarttime = Convert.ToDateTime(dateTimeStart.Value.ToString() );//开始时间
                DateTime dEndtime = Convert.ToDateTime(dateTimeEnd.Value.ToString());//结束时间
                if (!string.IsNullOrEmpty(ComBwh_Name.SelectedValue.ToString()))
                {
                    Str_Where += " and MdyBillTb.wh_name = '" + ComBwh_Name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBorder_status_name.SelectedValue.ToString()))
                {
                    Str_Where += " and order_status_name='" + ComBorder_status_name.Text.ToString() + "'";
                }

                if (!string.IsNullOrEmpty(ComBcom_name.SelectedValue.ToString()))
                {
                    Str_Where += " and com_name='" + ComBcom_name.SelectedValue.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBorg_name.SelectedValue.ToString()))
                {
                    Str_Where += " and org_name='" + ComBorg_name.Text.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(ComBhandle_name.SelectedValue.ToString()))
                {
                    Str_Where += " and handle_name='" + ComBhandle_name.Text.ToString() + "'";
                }
                if (dStarttime.ToShortDateString() == dEndtime.ToShortDateString())
                {
                    Str_Where += " and MdyBillTb.order_date=" + Common.LocalDateTimeToUtcLong(dStarttime);
                }
                else
                {
                    if (dateTimeStart.Value.ToString() != null)
                    {

                        Str_Where += " and MdyBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                    }
                    if (dateTimeEnd.Value.ToString() != null)
                    {

                        Str_Where += " and MdyBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                    }
                }
                 if (Common.LocalDateTimeToUtcLong(dStarttime) >Common.LocalDateTimeToUtcLong(dEndtime))
                {
                    MessageBoxEx.Show("您输入的开始时间不能大于结束时间！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
