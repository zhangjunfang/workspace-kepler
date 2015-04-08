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
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.OtherSendGoods
{
    public partial class UCStockShippingAddOrEdit : UCBase
    {

        #region 全局变量
        WindowStatus status;
        UCStockShippingManager UCShippingBM;
        private string StockShippingId = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存

        private string ShippingBillID = "stock_shipping_id";
        private string ShipBillTable = "tb_parts_stock_shipping";
        private string ShipPartTable = "tb_parts_stock_shipping_p";

        private string ShipBillLogMsg = "查询其它发货单表信息";
        private string ShipPartLogMsg = "查询其它发货单配件表信息";
        private string ImportTitle = "导入其它发货单配件信息";

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
        /// 获取其它发货库单表头表尾信息实体
        /// </summary>
        tb_parts_stock_shipping ShippingBillEntity = new tb_parts_stock_shipping();
        #endregion
        public UCStockShippingAddOrEdit(WindowStatus state, string ShippingId, UCStockShippingManager UCShipManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCShippingBM = UCShipManager;//获取其它发货单管理类
            this.status = state;//获取操作状态
            this.StockShippingId = ShippingId;//其它发货单ID
            base.SaveEvent += new ClickHandler(UCStockShippingAddOrEdit_SaveEvent);//保存
            base.DeleteEvent += new ClickHandler(UCStockShippingAddOrEdit_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCStockShippingAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCStockShippingAddOrEdit_ImportEvent);//导入
            //设置列表的可编辑状态
            gvPartsMsgList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvPartsMsgList.Columns)
            {
                if (dgCol.Name != colCheck.Name && dgCol.Name != counts.Name && dgCol.Name != Unitprice.Name && dgCol.Name != remarks.Name) dgCol.ReadOnly = true;
            }

            base.btnExport.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnEdit.Visible = false;
            base.btnBalance.Visible = false;
            base.btnPrint.Visible = false;
        }



       private  void UCStockShippingAddOrEdit_DeleteEvent(object sender, EventArgs e)
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
                //出库类型
                CommonFuncCall.BindInOutType(Combout_wh_type_name, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(Combwh_name, "请选择");
                //公司ID
                string com_id = GlobalStaticObj.CurrUserCom_Id;
                CommonFuncCall.BindDepartment(Comborg_name, com_id, "请选择");//选择部门名称

                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(StockShippingId);//获取单据头尾信息
                    GetBillPartsMsg(StockShippingId);//获取单据配件信息

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
        /// 部门选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comborg_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(Combhandle_name, Comborg_name.SelectedValue.ToString(), "请选择");//选择经手人
        }

        /// <summary>
        /// 选择经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combhandle_name_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(Comborg_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您先选择部门！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 保存其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(Combwh_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的仓库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(Combout_wh_type_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的出库类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (gvPartsMsgList.Rows.Count == 0)
            {
                MessageBoxEx.Show("请您导入要生成其它出库单的配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 提交其它发货单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 其它发货单导入单据
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCStockShippingAddOrEdit_ImportEvent(object send, EventArgs e)
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
        /// 正则表达式判断字符串是否为数字
        /// </summary>
        /// <param name="strValue">要验证的数字</param>
        /// <returns></returns>
        private static bool IsNumber(string strValue)
        {
            string RegPattern = "^-?[0-9]*$";
            Regex RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            RegPattern = @"^-?\d+\.\d{1,3}?$";//判断一到三位小数数值
            RegNum = new Regex(RegPattern);
            if (RegNum.IsMatch(strValue.Trim())) return true;
            return false;

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
        /// 其它发货单配件信息添加
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
                                MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    DataTable dt = GetPartsByCode(PartsCode);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return;
                    }
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
            DataTable dt = DBHelper.GetTable("", "v_stock_part_import", "*", string.Format("ser_parts_code='{0}'", PartsCode), "", "");
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
                dgvr.Cells["unitname"].Value = dr["unit"];
                dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                dgvr.Cells["counts"].Value = "-"+dr["paper_count"];//该配件的账面库存数量
                dgvr.Cells["Unitprice"].Value = dr["ref_out_price"];//销售价
                decimal Amount = Convert.ToDecimal(dgvr.Cells["counts"].Value.ToString());//总量
                decimal UntPrice = Convert.ToDecimal(dgvr.Cells["Unitprice"].Value.ToString());//单价
                dgvr.Cells["Calcmoney"].Value = Amount * UntPrice;//销售金额
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
                    string opName = "其它发货单操作";
                    lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.ShippingBill);//获取其它发货单编号
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
                        StockShippingId = Guid.NewGuid().ToString();
                        AddShippingBillSql(listSql, ShippingBillEntity, StockShippingId, HandleTypeName);
                        opName = "新增其它发货单";
                        AddOrEditPartsListMsg(listSql, StockShippingId, WindowStatus.Add);
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditReceiptBillSql(listSql, ShippingBillEntity, StockShippingId, HandleTypeName);
                        opName = "修改其它发货单";

                        AddOrEditPartsListMsg(listSql, StockShippingId, WindowStatus.Edit);
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
                        string DefaultWhere = " enable_flag=1 and ShpBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        UCShippingBM.GetShippingBillList(DefaultWhere);//更新单据列表
                        deleteMenuByTag(this.Tag.ToString(), UCShippingBM.Name);
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
        private void AddShippingBillSql(List<SysSQLString> listSql, tb_parts_stock_shipping stockShippingEntity, string StockShippingId, string HandleType)
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
                CommonFuncCall.FillEntityByControls(this, stockShippingEntity);
                stockShippingEntity.stock_shipping_id = StockShippingId;

                stockShippingEntity.update_by = GlobalStaticObj.UserID;
                stockShippingEntity.operators = GlobalStaticObj.UserID;
                stockShippingEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockShippingEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockShippingEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockShippingEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockShippingEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (stockShippingEntity != null)
                {
                    long CurrentDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Insert Into tb_parts_stock_shipping( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in stockShippingEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockShippingEntity, null);
                        sb_PrName.Append("," + name);//数据表字段名
                        sb_PrValue.Append(",@" + name);//数据表字段值
                        if (name == "create_time")
                        {
                            dicParam.Add(name, CurrentDate.ToString());//添加创建时间
                        }
                        else
                        {
                            dicParam.Add(name, value == null ? "" : value.ToString());
                        }
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
        private void EditReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_shipping stockShippingEntity, string StockShippingtId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, ShippingBillEntity);

                stockShippingEntity.handle = GlobalStaticObj.UserID;
                stockShippingEntity.operators = GlobalStaticObj.UserID;
                stockShippingEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockShippingEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockShippingEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockShippingEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockShippingEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (ShippingBillEntity != null)
                {
                    long CurrentDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update tb_parts_stock_shipping Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in stockShippingEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockShippingEntity, null);
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
                        if (name == "update_time")
                        {
                            dicParam.Add(name, CurrentDate.ToString());//添加更新时间
                        }
                        else
                        {
                            dicParam.Add(name, value == null ? "" : value.ToString());
                        }
                    }
                    sb.Append(" where stock_shipping_id='" + StockShippingtId + "';");
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
        /// 根据出入库单主键ID和表头信息对配件信息列表进行添加或编辑
        /// </summary>
        /// <param name="listSQL">要执行的SQL语句列表</param>
        /// <param name="ShippingId">出入库单号</param>
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string ShippingId, WindowStatus WinState)
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
                        DicDelParam.Add("stock_shipping_id", ShippingId);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", ShipPartTable, ShippingBillID);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    sb.Append("insert into tb_parts_stock_shipping_p (stock_shipping_parts_id,stock_shipping_id,num");
                    sb.Append(sb_ColName.ToString() + ") values(@stock_shipping_parts_id,@stock_shipping_id,@num" + sb_ColValue.ToString() + ");");
                    int SerialNum = 1;
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {

                        SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                        DicPartParam.Add("stock_shipping_parts_id", Guid.NewGuid().ToString());
                        DicPartParam.Add("stock_shipping_id", ShippingId);
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

                        long CurrentDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DateTime.Now.ToString()));
                        if (status == WindowStatus.Add)
                        {

                            DicPartParam.Add("create_time", CurrentDate.ToString());//添加创建时间
                            DicPartParam.Add("update_time", "0");//添加更新时间
                        }
                        else if (status == WindowStatus.Edit)
                        {
                            DicPartParam.Add("create_time", CurrentDate.ToString());//添加创建时间
                            DicPartParam.Add("update_time", CurrentDate.ToString());//添加更新时间
                        }

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
        /// 验证表中列表数据信息完整性
        /// </summary>
        private bool CheckListInfo()
        {

            foreach (DataGridViewRow dgvr in gvPartsMsgList.Rows)
            {

                if (dgvr.Cells["counts"].Value == null || dgvr.Cells["counts"].Value.ToString() == "")
                {
                    MessageBoxEx.Show("请输入配件数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["counts"];
                    return false;
                }
                else if (dgvr.Cells["Unitprice"].Value == null || dgvr.Cells["Unitprice"].Value.ToString() == "")
                {
                    MessageBoxEx.Show("请输入单价！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["Unitprice"];
                    return false;
                }
                else
                {
                    int Amount = Convert.ToInt32(dgvr.Cells["counts"].Value.ToString());//总量
                    decimal UntPrice = Convert.ToDecimal(dgvr.Cells["Unitprice"].Value.ToString());//单价
                    if (Amount >= 0)
                    {
                        MessageBoxEx.Show("您输入的数量不能为正数或零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (UntPrice <= 0)
                    {
                        MessageBoxEx.Show("您输入的单价不能为负数或零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 根据其它发货单ID获取表头和尾部信息
        /// </summary>
        /// <param name="ShippingBillId"></param>
        private void GetBillHeadEndMessage(string ShippingBillId)
        {
            try
            {
                if (ShippingBillId != null)
                {
                    //查询一条其它发货库单信息
                    DataTable ReceiptTable = DBHelper.GetTable(ShipBillLogMsg, ShipBillTable, "*", ShippingBillID + "='" + ShippingBillId + "'", "", "");
                    CommonFuncCall.FillEntityByTable(ShippingBillEntity, ReceiptTable);
                    CommonFuncCall.FillControlsByEntity(this, ShippingBillEntity);
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
        /// 根据其它发货单ID获取配件信息表
        /// </summary>
        /// <param name="ShipBillIdValue"></param>
        private void GetBillPartsMsg(string ShipBillIdValue)
        {
            try
            {

                DataTable PartsMsgList = DBHelper.GetTable(ShipPartLogMsg, ShipPartTable, "*", ShippingBillID + "='" + ShipBillIdValue + "'", "", "");//获取配件信息表
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
                    MessageBoxEx.Show("要查询的配件信息不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (gvPartsMsgList.Rows[i].Cells["counts"].Value == null || gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString() =="")
                    {
                        return;
                    }
                    else if (gvPartsMsgList.Rows[i].Cells["Unitprice"].Value == null ||gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString() =="")
                    {

                        return;
                    }
                    else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString()))
                    {
                        return;
                    }
                    else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString()))
                    {
                        return;
                    }
                    else
                    {
                        decimal Amount = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString());//总量
                        decimal UntPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString());//单价
                        if (Amount >= decimal.Zero)
                        {
                            return;
                        }
                        else if (UntPrice <= decimal.Zero)
                        {
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

    }
}
