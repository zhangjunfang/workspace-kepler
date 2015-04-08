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
        private string PartsTable = "tb_parts";//配件档案表
        private string StockLossID = "stock_loss_id";//报损单表主键
        private string WareHouseName = "wh_name";//仓库名称
        private string LossQueryLogMsg = "查询报损单表信息";//报损单表操作日志
        private string LossDelLogMsg = "批量删除报损单表信息";//报损单表操作日志
        private string LossVerifyLogMsg = "批量审核报损单表信息";//报损单表操作日志
        private string LossSubmitLogMsg = "提交报损单表信息";//报损单提交日志
        private string LossEdit = "编辑";
        private string LossCopy = "复制";
        private string BillStatus = "已开单";
        private int SearchFlag = 0;//存放搜索标志
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
        private const string PartID = "parts_id";//配件ID
        private const string SerPartsCode = "ser_parts_code";//配件档案配件编码
        private const string AmountMoney = "money";
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
        public UCReportedLossBillManager()
        {
            InitializeComponent();

            //注册操作事件
            base.AddEvent += new ClickHandler(UCReportedLossBillManager_AddEvent);
            base.EditEvent += new ClickHandler(UCReportedLossBillManager_EditEvent);
            base.CopyEvent += new ClickHandler(UCReportedLossBillManager_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCReportedLossBillManager_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCReportedLossBillManager_SubmitEvent);
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
            base.SetBaseButtonStatus();


        }
        /// <summary>
        /// 提交报损单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private  void UCReportedLossBillManager_SubmitEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> LossIdLst = GetSubmitRecord();//获取需提交的单据记录
                if (LossIdLst.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合提交条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    Dictionary<string, string> LossBillField = new Dictionary<string, string>();//存放更新字段
                    LossBillField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//状态ID
                    LossBillField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//状态name
                    bool flag = DBHelper.BatchUpdateDataByIn(LossSubmitLogMsg, LossTable, LossBillField, StockLossID, LossIdLst.ToArray());
                    if (flag)
                    {
                        MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1  and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        GetRepLossBillList(DefaultWhere);//刷新出入库单列表
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
               if (LossIDValuelist.Count > 0) LossIDValuelist.Clear();//清除之前的数据
               foreach (DataGridViewRow dr in gvLossBillList.Rows)
               {
                   bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                   if (isCheck)
                   {
                       //获取保存草稿状态的单据记录
                       string BillStatusDraft = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                       string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                       if (BillStatusDraft == ColOrderStatus)
                       {
                           LossIDValuelist.Add(dr.Cells["RepLossId"].Value.ToString());//添加草稿状态主键ID
                       }
                   }
               }
               return LossIDValuelist;

           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
               return null;
           }

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
                if (gvLossBillList.Rows.Count == 0) //判断gridview中是否有数据记录
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
                    XlsTable.Columns.Add("操作人", typeof(string));
                    XlsTable.Columns.Add("备注", typeof(string));
                    XlsTable.Columns.Add("单据状态", typeof(string));
                    foreach (DataGridViewRow dgRow in gvLossBillList.Rows)
                    {
                        bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colCheck"]).EditedFormattedValue;//获取当前记录行的选择状态
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
        private void UCReportedLossBillManager_Load(object sender, EventArgs e)
        {
            try
            {
                //获取默认系统时间
                dateTimeStart.Value = DateTime.Now.AddMonths(-3).ToShortDateString();
                dateTimeEnd.Value = DateTime.Now.ToString();
                //出库类型
                CommonFuncCall.BindInOutType(ComBout_wh_type_name, true, "请选择");
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
                    MessageBoxEx.Show("请选择要删除的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                            long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                            string DefaultWhere = " enable_flag=1  and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                            GetRepLossBillList(DefaultWhere);//刷新报损单列表
                            MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        { MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
                Dictionary<string, long> OrderIDDateDic = GetVerifyRecord();//获取需要核实的记录行
                if (LossIDValuelist.Count == 0 && gvLossBillList.SelectedRows.Count == 0)
                {
                    MessageBoxEx.Show("请选择符合审核条件的单据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCVerify UcVerify = new UCVerify();
                UcVerify.ShowDialog();
                if (UcVerify.DialogResult == DialogResult.OK)
                {
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
                    bool flag = DBHelper.BatchUpdateDataByIn(LossVerifyLogMsg, LossTable, ShippingBillField, StockLossID, LossIDValuelist.ToArray());//批量审核获取的报损单记录
                    if (flag)
                    {
                        if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                        {
                            CommonFuncCall.StatisticStock(GetLossPart(OrderIDDateDic), "报损单配件表");//同步更新报损单账面库存
                        }
                        MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1  and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        GetRepLossBillList(DefaultWhere);//刷新报损单列表

                    }
                    else
                    {
                        MessageBoxEx.Show("审核失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 获取调拨单配件表已审核的单据配件信息
        /// </summary>
        /// <param name="OrderIDDateDic">单据ID与日期对</param>
        /// <returns>返回待统计配件信息</returns>
        private DataTable GetLossPart(Dictionary<string, long> OrderIDDateDic)
        {
            try
            {
                DataTable TemplateTable = null;//模版表
                foreach (KeyValuePair<string, long> KVPair in OrderIDDateDic)
                {
                    StringBuilder sbField = new StringBuilder();
                    sbField.AppendFormat("{0},{1},PtTb.{2},LossTb.{3},LossTb.{4},LossTb.{5},LossTb.{6},LossTb.{7},LossTb.{8},{9},{10}",
                     WarehID, WarehName, PartID, PartsCode, PartName, PartSpec, PartBarCode, CarPartsCode, DrawNum, UnitName, PartCount);
                    string RelationTable = LossPartTable + " as LossTb left join " + PartsTable +
                    " as PtTb on LossTb.parts_code=PtTb.ser_parts_code ";//要查询的关联表名
                    TemplateTable = CommonFuncCall.CreatePartStatisticTable();//获取要填充的公用表
                    //获取对应单据的配件信息
                    DataTable IOPartTable = DBHelper.GetTable("查询报损单配件信息", RelationTable, sbField.ToString(), "stock_loss_id='" + KVPair.Key.ToString() + "'", "", "");
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
                        dr["StatisticType"] = (int)DataSources.EnumStatisticType.PaperCount;//统计类型
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
                    string DefaultWhere = " enable_flag=1 and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
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
                gvLossBillList.Rows.Clear();//清除所有记录行
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
                "", " LsBillTb.create_time desc", winFormLossPage.PageIndex, winFormLossPage.PageSize, out RecCount);//获取报损单表查询记录
                winFormLossPage.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0)
                {
                    return;
                }
                //把查询的报损单列表放入Gridview
                for (int i = 0; i < LossBillTable.Rows.Count; i++)
                {

                    DataGridViewRow gvRow = gvLossBillList.Rows[gvLossBillList.Rows.Add()];//创建行项
                    gvRow.Cells["RepLossId"].Value = LossBillTable.Rows[i][StockLossID].ToString();//存放出入库单ID
                    if (LossBillTable.Rows[i][OrderStatus].ToString() == DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true))
                    {
                        gvRow.Cells["BillNum"].Value = string.Empty;
                    }
                    else
                    {
                        gvRow.Cells["BillNum"].Value = LossBillTable.Rows[i][OrderNum].ToString();
                    }
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
        private Dictionary<string, long> GetVerifyRecord()
        {
            if (LossIDValuelist.Count > 0) LossIDValuelist.Clear();//清除之前的数据
            Dictionary<string, long> DicField = new Dictionary<string, long>();
            foreach (DataGridViewRow dr in gvLossBillList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    //获取已提交/审核未通过的状态的编号
                    string BillStatusSUBMIT = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT,true);
                    string BillStatusNOTAUDIT = DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT,true);
                    string ColOrderStatus = dr.Cells["OrderState"].Value.ToString();
                    if (BillStatusSUBMIT == ColOrderStatus || BillStatusNOTAUDIT == ColOrderStatus)
                    {
                        long OrdeDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dr.Cells["BillDate"].Value));
                        DicField.Add(dr.Cells["RepLossId"].Value.ToString(), OrdeDate);//添加已审核单据主键ID和单据日期键值对
                        LossIDValuelist.Add(dr.Cells["RepLossId"].Value.ToString());
                    }
                }
            }
            return DicField;
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
                if (dStarttime.ToShortDateString() == dEndtime.ToShortDateString())
                 {
                     Str_Where += " and LsBillTb.order_date=" + Common.LocalDateTimeToUtcLong(dStarttime);
                 }
                 else
                 {
                     if (dateTimeStart.Value != null)
                     {
                         Str_Where += " and LsBillTb.order_date>=" + Common.LocalDateTimeToUtcLong(dStarttime);
                     }
                     if (dateTimeEnd.Value != null)
                     {

                         Str_Where += " and LsBillTb.order_date<=" + Common.LocalDateTimeToUtcLong(dEndtime);
                     }
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
