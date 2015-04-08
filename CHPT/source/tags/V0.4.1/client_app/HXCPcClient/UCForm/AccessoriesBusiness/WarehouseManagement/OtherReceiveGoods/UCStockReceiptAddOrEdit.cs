using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using Model;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser;
using Utility.Common;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherReceiveGoods
{
    public partial class UCStockReceiptAddOrEdit : UCBase
    {
        #region 全局变量
        WindowStatus status;
        UCStockReceiptManager UCReceiptBM;
        private string StockReceiptId = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存
        int Initialindex = -1;//GridView初始行索引
        private string ReceiptBillID = "stock_receipt_id";
        private string RecBillTable = "tb_parts_stock_receipt";
        private string RecPartTable = "tb_parts_stock_receipt_p";

        private string RecBillLogMsg = "查询其它收货单表信息";
        private string RecPartLogMsg = "查询其它收货单配件表信息";
        private string ImportTitle = "导入其它收货单配件信息";

        //配件单据列名
        private const string PID = "ID";
        private const string PNum = "partsnum";
        private const string PNam = "partname";
        private const string PSpc = "PartSpec";
        private const string PDrw = "drawingnum";
        private const string PUnt = "unitname";
        private const string PBrd = "partbrand";
        private const string PCfc = "CarFactoryCode";
        private const string PBrc = "BarCode";
        private const string PCnt = "counts";
        private const string PUtp = "Unitprice";
        private const string PCmy = "Calcmoney";
        private const string PRmk = "remarks";
        /// <summary>
        /// 获取其它收货库单表头表尾信息实体
        /// </summary>
        tb_parts_stock_receipt ReceiptBillEntity = new tb_parts_stock_receipt();
        #endregion
        public UCStockReceiptAddOrEdit(WindowStatus state, string ReceiptId, UCStockReceiptManager UCRecManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCReceiptBM = UCRecManager;//获取其它收货单管理类
            this.status = state;//获取操作状态
            this.StockReceiptId = ReceiptId;//其它收货单ID
            base.SaveEvent += new ClickHandler(UCStockReceiptAddOrEdit_SaveEvent);//保存
            base.DeleteEvent += new ClickHandler(UCStockReceiptAddOrEdit_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCStockReceiptAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCStockReceiptAddOrEdit_ImportEvent);//导入

            //设置列表的可编辑状态
            gvPartsMsgList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvPartsMsgList.Columns)
            {
                if (dgCol.Name != colCheck.Name && dgCol.Name != counts.Name && dgCol.Name != Unitprice.Name && dgCol.Name!=remarks.Name) dgCol.ReadOnly = true;
            }
            base.btnExport.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnEdit.Visible = false;
            base.btnBalance.Visible = false;
            base.btnPrint.Visible = false;

        }

      private   void UCStockReceiptAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            DelSelectedRows();//删除选中配件信息
        }

        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCStockReceiptAddOrEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //入库类型
                CommonFuncCall.BindInOutType(Combin_wh_type_name, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(Combwh_name, "请选择");
                //公司ID
                string com_id = GlobalStaticObj.CurrUserCom_Id;
                CommonFuncCall.BindCompany(combcom_name, "全部");//选择公司名称
                CommonFuncCall.BindDepartment(Comborg_name, com_id, "请选择");//选择部门名称
                CommonFuncCall.BindHandle(Combhandle_name, "", "请选择");//选择经手人

                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(StockReceiptId);//获取单据头尾信息
                    GetBillPartsMsg(StockReceiptId);//获取单据配件信息

                }
                else if (status == WindowStatus.Add || status == WindowStatus.Copy)
                {
                    txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);//获取单据状态
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 保存其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(Combwh_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的仓库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(Combin_wh_type_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的入库类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (gvPartsMsgList.Rows.Count == 0)
            {
                MessageBoxEx.Show("请您导入要生成其它收货单的配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 提交其它收货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 其它收货单导入单据
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockReceiptAddOrEdit_ImportEvent(object send, EventArgs e)
        {
            try
            {
                BtnImportMenu.Show(btnImport, 0, btnImport.Height);//指定导入菜单在导入按钮位置显示
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 配件新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PartsAdd();//配件新增
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 配件删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartDelete_Click(object sender, EventArgs e)
        {
            DelSelectedRows();//删除选中配件信息
        }
        /// <summary>
        /// 删除选中的配件信息
        /// </summary>
        private void DelSelectedRows()
        {
            try
            {
                List<string> SelectedRowsLst = GetSelectedRows();//获取选中记录主键ID
                int MaxCount = SelectedRowsLst.Count;//获取最大选中记录行
                if (gvPartsMsgList.SelectedRows.Count == 0 && SelectedRowsLst.Count == 0)
                {
                    MessageBoxEx.Show("您没有选中要删除的行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DialogResult DgResult = MessageBoxEx.Show("您确定要删除选中记录行项！", "删除操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DgResult == DialogResult.Yes)
                    {
                        while (MaxCount > 0)
                        {//删除选中行记录
                            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                            {
                                bool IsCheck = (bool)((DataGridViewCheckBoxCell)gvPartsMsgList.Rows[i].Cells["colCheck"]).EditedFormattedValue;//获取选择状态
                                if (IsCheck)
                                {
                                    gvPartsMsgList.Rows.RemoveAt(i);//删除选中记录行
                                    MaxCount--;
                                }
                            }
                        }

                        MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 获取选中的记录行
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRows()
        {
            try
            {
                List<string> BillingIDLst = new List<string>();//存放单据ID
                foreach (DataGridViewRow dr in gvPartsMsgList.Rows)
                {

                    bool isCheck = (bool)dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck)
                    {
                        BillingIDLst.Add(dr.Cells["partsnum"].ToString());
                    }
                }
                return BillingIDLst;

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }
        /// <summary>
        /// 以Execl文件格式导入配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelImport_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> PartFieldDic = new Dictionary<string, string>();//配件列表的列名
                for (int i = 1; i < gvPartsMsgList.Columns.Count; i++)
                {//添加配件列字段名和标题文本
                    PartFieldDic.Add(gvPartsMsgList.Columns[i].Name.ToString(), gvPartsMsgList.Columns[i].HeaderText.ToString());
                }
                //弹出导入Excel对话框
                FrmExcelImport frmExcel = new FrmExcelImport(PartFieldDic, ImportTitle);
                DialogResult DgResult = frmExcel.ShowDialog();
                if (DgResult == DialogResult.OK)
                {

                    string PNumExcelField = string.Empty;
                    string PNamExcelField = string.Empty;
                    string PSpcExcelField = string.Empty;
                    string PDrwExcelField = string.Empty;
                    string PUntExcelField = string.Empty;
                    string PBrdExcelField = string.Empty;
                    string PCfcExcelField = string.Empty;
                    string PBrcExcelField = string.Empty;
                    string PCntExcelField = string.Empty;
                    string PClpExcelField = string.Empty;
                    string PCmyExcelField = string.Empty;
                    string PRmkExcelField = string.Empty;
                    //获取与单据列名匹配的Excel表格列名
                    foreach (DictionaryEntry DicEty in frmExcel.MatchFieldHTable)
                    {
                        string BillField = DicEty.Key.ToString();//获取匹配单据的列名
                        string ExcelField = DicEty.Value.ToString();//获取匹配Excel表格的列名

                        switch (BillField)
                        {
                            case PNum:
                                PNumExcelField = ExcelField;
                                break;
                            case PNam:
                                PNamExcelField = ExcelField;
                                break;
                            case PSpc:
                                PSpcExcelField = ExcelField;
                                break;
                            case PDrw:
                                PDrwExcelField = ExcelField;
                                break;
                            case PUnt:
                                PUntExcelField = ExcelField;
                                break;
                            case PBrd:
                                PBrdExcelField = ExcelField;
                                break;
                            case PCfc:
                                PCfcExcelField = ExcelField;
                                break;
                            case PBrc:
                                PBrcExcelField = ExcelField;
                                break;
                            case PCnt:
                                PCntExcelField = ExcelField;
                                break;
                            case PUtp:
                                PClpExcelField = ExcelField;
                                break;
                            case PCmy:
                                PCmyExcelField = ExcelField;
                                break;
                            case PRmk:
                                PRmkExcelField = ExcelField;
                                break;

                        }
                    }
                    if (ValidityCellIsnull(frmExcel.ExcelTable))
                    {
                        for (int i = 0; i < frmExcel.ExcelTable.Rows.Count; i++)
                        {

                            DataGridViewRow DgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建新行项

                            DgRow.Cells[PNum].Value = frmExcel.ExcelTable.Rows[i][PNumExcelField].ToString();
                            DgRow.Cells[PNam].Value = frmExcel.ExcelTable.Rows[i][PNamExcelField].ToString();
                            DgRow.Cells[PSpc].Value = frmExcel.ExcelTable.Rows[i][PSpcExcelField].ToString();
                            DgRow.Cells[PDrw].Value = frmExcel.ExcelTable.Rows[i][PDrwExcelField].ToString();
                            DgRow.Cells[PUnt].Value = frmExcel.ExcelTable.Rows[i][PUntExcelField].ToString();
                            DgRow.Cells[PBrd].Value = frmExcel.ExcelTable.Rows[i][PBrdExcelField].ToString();
                            DgRow.Cells[PCfc].Value = frmExcel.ExcelTable.Rows[i][PCfcExcelField].ToString();
                            DgRow.Cells[PBrc].Value = frmExcel.ExcelTable.Rows[i][PBrcExcelField].ToString();
                            DgRow.Cells[PCnt].Value = frmExcel.ExcelTable.Rows[i][PCntExcelField].ToString();
                            DgRow.Cells[PUtp].Value = frmExcel.ExcelTable.Rows[i][PClpExcelField].ToString();
                            DgRow.Cells[PCmy].Value = frmExcel.ExcelTable.Rows[i][PCmyExcelField].ToString();
                            DgRow.Cells[PRmk].Value = frmExcel.ExcelTable.Rows[i][PRmkExcelField].ToString();

                        }
                    }

                    frmExcel.ExcelTable.Clear();//清空所有配件信息
                }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 验证导入excel表格是否有空值
        /// </summary>
        /// <param name="ExcelTable">导入的表格</param>
        /// <returns></returns>
        private bool ValidityCellIsnull(DataTable ExcelTable)
        {
            for (int i = 0; i < ExcelTable.Rows.Count; i++)
            {
                for (int j = 0; j < ExcelTable.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(ExcelTable.Rows[i][j].ToString()))
                    {
                        MessageBoxEx.Show("Excel配件信息模板中存在空数据行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvPartsMsgList.Rows.Clear();//清空之前导入数据
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 其它收货单配件信息添加
        /// </summary>
        private void PartsAdd()
        {
            try
            {

                frmParts frm = new frmParts();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string PartsCode = frm.PartsCode;
                    if (gvPartsMsgList.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in gvPartsMsgList.Rows)
                        {

                            if (dr.Cells["partsnum"].Value.ToString() == PartsCode)
                            {
                                MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    DataTable dt = GetPartsByCode(PartsCode);
                    if (dt == null || dt.Rows.Count == 0)return;
                    else
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];
                            GetGridViewRowByDr(dgvr, dr);

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
        /// 根据ID获取配件信息
        /// </summary>
        /// <param name="PartsID"></param>
        /// <returns></returns>
        private DataTable GetPartsByCode(string PartsCode)
        {
            //有问题，需要修改
            DataTable dt = DBHelper.GetTable("", "tb_parts", "*", string.Format("ser_parts_code='{0}'", PartsCode), "", "");
            return dt;
        }

        /// <summary>
        /// 单个添加或编辑配件
        /// </summary>
        /// <param name="dgvr"></param>
        /// <param name="dr"></param>
        /// <param name="relation_order"></param>
        /// <param name="HandleType"></param>
        private void GetGridViewRowByDr(DataGridViewRow dgvr, DataRow dr)
        {
            try
            {

                dgvr.Cells["partsnum"].Value = dr["ser_parts_code"];
                dgvr.Cells["partname"].Value = dr["parts_name"];
                dgvr.Cells["PartSpec"].Value = dr["model"];
                dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                dgvr.Cells["unitname"].Value = dr["sales_unit_name"];
                dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                dgvr.Cells["remarks"].Value = dr["remark"];

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 数据保存/提交 方法
        /// </summary>
        /// <param name="HandleTypeName">保存/提交</param>
        private void SaveOrSubmitMethod(string HandleTypeName)
        {
            try
            {

                if (CheckListInfo())
                {
                    gvPartsMsgList.EndEdit();
                    string opName = "其它收货单操作";
                    lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.ReceiptBill);//获取其它收货单编号
                    if (HandleTypeName == submit)
                    {
                        txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                    }
                    else if (HandleTypeName == save)
                    {
                        txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                    }

                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        StockReceiptId = Guid.NewGuid().ToString();
                        AddReceiptBillSql(listSql, ReceiptBillEntity, StockReceiptId, HandleTypeName);
                        opName = "新增其它收货单";
                        AddOrEditPartsListMsg(listSql, StockReceiptId, WindowStatus.Add);

                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditReceiptBillSql(listSql, ReceiptBillEntity, StockReceiptId, HandleTypeName);
                        opName = "修改其它收货单";
                        AddOrEditPartsListMsg(listSql, StockReceiptId,WindowStatus.Edit);
                    }
                    

                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        if (HandleTypeName == submit)
                        {
                            MessageBoxEx.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (HandleTypeName == save)
                        {
                            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        long StartDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.AddMonths(-6).ToShortDateString()));//获取当前日期的半年前的日期
                        long EndDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));//获取当前日期
                        string DefaultWhere = " enable_flag=1 and RptBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        UCReceiptBM.GetReceiptBillList(DefaultWhere);//更新单据列表
                        deleteMenuByTag(this.Tag.ToString(), UCReceiptBM.Name);
                    }
                    else
                    {
                        if (HandleTypeName == submit)
                        {
                            MessageBoxEx.Show("提交失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (HandleTypeName == save)
                        {
                            MessageBoxEx.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
        /// 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        private void AddReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_receipt stockReceiptEntity, string StockReceiptId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                //SQL语句拼装操作
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//保存SQL语句参数值
                CommonFuncCall.FillEntityByControls(this, stockReceiptEntity);
                stockReceiptEntity.stock_receipt_id = StockReceiptId;

                stockReceiptEntity.update_by = GlobalStaticObj.UserID;
                stockReceiptEntity.operators = GlobalStaticObj.UserID;
                stockReceiptEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockReceiptEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockReceiptEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockReceiptEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockReceiptEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (stockReceiptEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Insert Into tb_parts_stock_receipt( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in stockReceiptEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockReceiptEntity, null);
                        sb_PrName.Append("," + name);//数据表字段名
                        sb_PrValue.Append(",@" + name);//数据表字段值
                        dicParam.Add(name, value == null ? "" : value.ToString());
                    }
                    sb.Append(sb_PrName.ToString().Substring(1, sb_PrName.ToString().Length - 1) + ") Values (");//追加字段名
                    sb.Append(sb_PrValue.ToString().Substring(1, sb_PrValue.ToString().Length - 1) + ");");//追加字段值
                    //完成SQL语句的拼装
                    sysStringSql.sqlString = sb.ToString();
                    sysStringSql.Param = dicParam;
                    listSql.Add(sysStringSql);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        /// <param name="model"></param>
        private void EditReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_receipt stockReceiptEntity, string StockReceiptId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, ReceiptBillEntity);

                stockReceiptEntity.handle = GlobalStaticObj.UserID;
                stockReceiptEntity.operators = GlobalStaticObj.UserID;
                stockReceiptEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockReceiptEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockReceiptEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockReceiptEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockReceiptEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (ReceiptBillEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update tb_parts_stock_receipt Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in stockReceiptEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockReceiptEntity, null);
                        if (isFirstValue)
                        {
                            isFirstValue = false;
                            sb.Append(name);
                            sb.Append("=");
                            sb.Append("@" + name);
                        }
                        else
                        {
                            sb.Append("," + name);
                            sb.Append("=");
                            sb.Append("@" + name);
                        }
                        dicParam.Add(name, value == null ? "" : value.ToString());
                    }
                    sb.Append(" where stock_receipt_id='" + StockReceiptId + "';");
                    sysStrSql.sqlString = sb.ToString();
                    sysStrSql.Param = dicParam;
                    listSql.Add(sysStrSql);//完成SQL语句的拼装
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 根据其它收货单主键ID和表头信息对配件信息列表进行添加或编辑
        /// </summary>
        /// <param name="listSQL">要执行的SQL语句列表</param>
        /// <param name="ReceiptId">出入库单号</param>
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string ReceiptId,WindowStatus WinState)
        {
            try
            {

                if (gvPartsMsgList.Rows.Count > 0)
                {

                    //循环获取配件信息表中的数据记录，添加到数据库中
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb_ColName = new StringBuilder();
                    StringBuilder sb_ColValue = new StringBuilder();
                    string ColName = string.Empty;
                    for (int i = 1; i < gvPartsMsgList.Columns.Count; i++)
                    {
                        ColName = gvPartsMsgList.Columns[i].DataPropertyName.ToString();
                        if (!string.IsNullOrEmpty(ColName))
                        {
                            sb_ColName.Append("," + ColName);//数据表字段名
                            sb_ColValue.Append(",@" + ColName);//数据表值的参数名
                        }

                    }
                    if (WinState == WindowStatus.Edit)
                    {
                        SysSQLString SQLStr = new SysSQLString();//创建存储要删除sql语句的实体类
                        SQLStr.cmdType = CommandType.Text;//sql语句执行格式
                        Dictionary<string, string> DicDelParam = new Dictionary<string, string>();
                        StringBuilder sbDelSql = new StringBuilder();
                        DicDelParam.Add("stock_receipt_id", ReceiptId);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", RecPartTable, ReceiptBillID);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    sb.Append("insert into tb_parts_stock_receipt_p (stock_receipt_parts_id,stock_receipt_id,num");
                    sb.Append(sb_ColName.ToString() + ") values(@stock_receipt_parts_id,@stock_receipt_id,@num" + sb_ColValue.ToString() + ");");
                    int SerialNum = 1;
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {

                        SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                        DicPartParam.Add("stock_receipt_parts_id", Guid.NewGuid().ToString());
                        DicPartParam.Add("stock_receipt_id", ReceiptId);
                        DicPartParam.Add("num", SerialNum.ToString());
                        DicPartParam.Add("parts_code", DgVRow.Cells["partsnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsnum"].Value.ToString());
                        DicPartParam.Add("parts_name", DgVRow.Cells["partname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partname"].Value.ToString());
                        DicPartParam.Add("model", DgVRow.Cells["PartSpec"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["PartSpec"].Value.ToString());
                        DicPartParam.Add("drawing_num", DgVRow.Cells["drawingnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["drawingnum"].Value.ToString());
                        DicPartParam.Add("unit_name", DgVRow.Cells["unitname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["unitname"].Value.ToString());
                        DicPartParam.Add("parts_brand", DgVRow.Cells["partbrand"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partbrand"].Value.ToString());
                        DicPartParam.Add("car_parts_code", DgVRow.Cells["CarFactoryCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["CarFactoryCode"].Value.ToString());
                        DicPartParam.Add("parts_barcode", DgVRow.Cells["BarCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["BarCode"].Value.ToString());
                        DicPartParam.Add("counts", DgVRow.Cells["counts"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["counts"].Value.ToString());
                        DicPartParam.Add("price", DgVRow.Cells["Unitprice"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["Unitprice"].Value.ToString());
                        DicPartParam.Add("money", DgVRow.Cells["Calcmoney"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["Calcmoney"].Value.ToString());
                        DicPartParam.Add("remark", DgVRow.Cells["remarks"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["remarks"].Value.ToString());

                        StrSqlParts.sqlString = sb.ToString();//获取执行的sql语句
                        StrSqlParts.Param = DicPartParam;//获取执行的参数值
                        listSQL.Add(StrSqlParts);//添加记录行到list列表中
                        SerialNum++;//自动增加序号
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 根据输入的数量和单价计算总金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                {
                    if (gvPartsMsgList.Rows[i].Cells["counts"].Value == null)
                    {
                        MessageBoxEx.Show("数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (gvPartsMsgList.Rows[i].Cells["Unitprice"].Value == null)
                    {
                        MessageBoxEx.Show("单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                     else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString()))
                     {
                         MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         gvPartsMsgList.Rows[i].Cells["counts"].Value = string.Empty;
                         
                         return;
                     }
                     else if ( !IsNumber(gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString()))
                     {
                         MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                         gvPartsMsgList.Rows[i].Cells["Unitprice"].Value = string.Empty;
                         return;
                     }
                     else
                     {
                         int Amount = Convert.ToInt32(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString());//总量
                         decimal UntPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString());//单价
                         if (Amount <= 0 || UntPrice <= 0)
                         {
                             MessageBoxEx.Show("您输入的数量或单价不能为负数或零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             return;
                         }

                         else
                         {
                             gvPartsMsgList.Rows[i].Cells["Calcmoney"].Value = UntPrice * Amount;
                         }
                     }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        private void gvPartsMsgList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
            {

            }
        }

        /// <summary>
        /// 正则表达式判断字符串是否为数字
        /// </summary>
        /// <param name="strValue">要验证的数字</param>
        /// <returns></returns>
        private static bool IsNumber(string strValue)
        {
            string RegPattern = "^[0-9]*$";
            Regex RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            RegPattern = @"^\d+\.\d{1,3}?$";//判断一到三位小数数值
            RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            return false;

        }

        /// <summary>
        /// 验证表中列表数据信息完整性
        /// </summary>
        private bool CheckListInfo()
        {

            foreach (DataGridViewRow dgvr in gvPartsMsgList.Rows)
            {

                string PartCount = CommonCtrl.IsNullToString(dgvr.Cells["counts"].Value);//配件数量
                if (PartCount.Length == 0)
                {
                    MessageBoxEx.Show("请输入配件数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["counts"];
                    return false;
                }


            }
            return true;
        }

        /// <summary>
        /// 根据其它收货单ID获取表头和尾部信息
        /// </summary>
        /// <param name="ReceiptBillId"></param>
        private void GetBillHeadEndMessage(string ReceiptBillId)
        {
            try
            {
                if (ReceiptBillId != null)
                {
                    //查询一条其它收货单信息
                    DataTable ReceiptTable = DBHelper.GetTable(RecBillLogMsg, RecBillTable, "*", ReceiptBillID + "='" + ReceiptBillId + "'", "", "");
                    CommonFuncCall.FillEntityByTable(ReceiptBillEntity, ReceiptTable);
                    CommonFuncCall.FillControlsByEntity(this, ReceiptBillEntity);
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 根据其它收货单ID获取配件信息表
        /// </summary>
        /// <param name="RecBillIdValue"></param>
        private void GetBillPartsMsg(string RecBillIdValue)
        {
            try
            {

                DataTable PartsMsgList = DBHelper.GetTable(RecPartLogMsg, RecPartTable, "*", ReceiptBillID + "='" + RecBillIdValue + "'", "", "");//获取配件信息表
                if (PartsMsgList.Rows.Count > 0)
                {

                    foreach (DataRow dr in PartsMsgList.Rows)
                    {
                        DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];

                        dgvr.Cells["partsnum"].Value = dr["parts_code"];
                        dgvr.Cells["partname"].Value = dr["parts_name"];
                        dgvr.Cells["PartSpec"].Value = dr["model"];
                        dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                        dgvr.Cells["unitname"].Value = dr["unit_name"];
                        dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                        dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                        dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                        dgvr.Cells["counts"].Value = dr["counts"];
                        dgvr.Cells["Unitprice"].Value = dr["price"];
                        dgvr.Cells["Calcmoney"].Value = dr["money"];
                        dgvr.Cells["remarks"].Value = dr["remark"];

                    }
                }
                else
                {
                    MessageBoxEx.Show("要查询的配件信息不存在！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }


        /// <summary>
        /// 控制对GridView行的选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                Initialindex = e.RowIndex;
                gvPartsMsgList.Rows[Initialindex].Selected = true;
            }
        }




    }
}
