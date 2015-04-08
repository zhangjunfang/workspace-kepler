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
using System.Collections;
using System.Text.RegularExpressions;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill;
using Model;
using System.Reflection;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill
{
    public partial class UCReportedLossBillAddOrEdit : UCBase
    {
        #region 全局变量
        WindowStatus status;
        UCReportedLossBillManager UCLossBM;
        private string StockLossId = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存
        private string LossBillID = "stock_loss_id";//报损单主键
        private const string SaleBillingId = "sale_billing_id";//销售开单主键
        private const string SalebillingNo = "order_num";//销售开单编号
        private string LossBillTable = "tb_parts_stock_loss";//报损单表
        private string LossPartTable = "tb_parts_stock_loss_p";//报损单配件表
        private string SaleBillTable = "tb_parts_sale_billing";//销售开单信息表
        private string SalePartTable = "tb_parts_sale_billing_p";//销售开单配件信息表
        private string LossBillLogMsg = "查询报损单表信息";
        private string LossPartLogMsg = "查询报损单配件表信息";
        private string SalepartsLogMsg = "查询销售开单配件表信息";
        private string SaleBillModifyLog = "修改销售开单的导入状态为占用";
        private string ImportTitle = "导入报损单配件信息";
        private List<string> BillIDlist = null;//存放导入配件表主键ID
        StringBuilder sb_Fields = new StringBuilder();//字符串集合
        //销售退货单配件信息表
        private string WhouseId = "wh_id";//仓库Id
        private string WhouseName = "wh_name";//仓库名称
        private string PartCode = "parts_code";
        private string PartsName = "parts_name";
        private string PartsSpec = "model";
        private string DrawNum = "drawing_num";
        private string UnitName = "unit_name";
        private string PartsBrand = "parts_brand_name";
        private string PartsFacCode = "car_factory_code";
        private string PartsBarCode = "parts_barcode";
        private string SaleRetCnt = "return_count";//退货数量
        private string UntPrice = "original_price";
        private string Remark = "remark";

        //配件单据列名
        private const string PNum = "partsnum";
        private const string PNam = "partname";
        private const string PSpc = "PartSpec";
        private const string PDrw = "drawingnum";
        private const string PUnt = "UntName";
        private const string PBrd = "partbrand";
        private const string PCfc = "CarFactoryCode";
        private const string PBrc = "BarCode";
        private const string PCnt = "counts";
        private const string PUtp = "Unitprice";
        private const string PCmy = "Calcmoney";
        private const string PRmk = "remarks";
        /// <summary>
        /// 获取报损单表头表尾信息实体
        /// </summary>
        tb_parts_stock_loss LossBillEntity = new tb_parts_stock_loss();
        #endregion
        public UCReportedLossBillAddOrEdit(WindowStatus state, string LossId, UCReportedLossBillManager UCLosManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCLossBM = UCLosManager;//获取报损单管理类
            this.status = state;//获取操作状态
            this.StockLossId = LossId;//报损单ID
            base.SaveEvent += new ClickHandler(UCReportedLossBillAddOrEdit_SaveEvent);//保存
            base.DeleteEvent += new ClickHandler(UCReportedLossBillAddOrEdit_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCReportedLossBillAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCReportedLossBillAddOrEdit_ImportEvent);//导入
            //设置列表的可编辑状态
            gvPartsMsgList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvPartsMsgList.Columns)
            {
                if (dgCol.Name != colCheck.Name && dgCol.Name != counts.Name&&dgCol.Name!=remarks.Name) dgCol.ReadOnly = true;
            }
            base.btnExport.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnEdit.Visible = false;
            base.btnBalance.Visible = false;
            base.btnPrint.Visible = false;

        }

       private   void UCReportedLossBillAddOrEdit_DeleteEvent(object sender, EventArgs e)
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
                CommonFuncCall.BindInOutType(CombOut_wh_type_name, true, "请选择");
                //获取仓库名称
                CommonFuncCall.BindWarehouse(Combwh_name, "请选择");
                //公司ID
                string com_id = GlobalStaticObj.CurrUserCom_Id;
                CommonFuncCall.BindCompany(combcom_name, "全部");//选择公司名称
                CommonFuncCall.BindDepartment(Comborg_name, com_id, "请选择");//选择部门名称
                CommonFuncCall.BindHandle(Combhandle_name, "", "请选择");//选择经手人
                gvPartsMsgList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(StockLossId);//获取单据头尾信息
                    GetBillPartsMsg(StockLossId);//获取单据配件信息

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
        /// 保存报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(Combwh_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的仓库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(CombOut_wh_type_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的出库类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (gvPartsMsgList.Rows.Count == 0)
            {
                MessageBoxEx.Show("请您导入要生成报损单的配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
            {
                if (gvPartsMsgList.Rows[i].Cells["counts"].Value == null)
                {
                    MessageBoxEx.Show("您输入的数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 提交报损单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 报损单导入单据
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCReportedLossBillAddOrEdit_ImportEvent(object send, EventArgs e)
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
                        if (gvPartsMsgList.Rows[i].Cells["counts"].Value==null) return;
                        else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString()))
                        {
                            MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gvPartsMsgList.Rows[i].Cells["counts"].Value = string.Empty;
                            return;
                        }
                        else
                        {
                            decimal LossCount = Convert.ToInt32(gvPartsMsgList.Rows[i].Cells["counts"].Value.ToString());//报损数量
                            decimal LossUntPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["Unitprice"].Value.ToString());//报损单价
                            if (LossCount >= decimal.Zero)
                            {
                                return;
                            }

                            else
                            {
                                gvPartsMsgList.Rows[i].Cells["Calcmoney"].Value = LossUntPrice * LossCount;//计算报损总金额
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
        /// 导入销售退货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Combwh_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择仓库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(CombOut_wh_type_name.SelectedValue.ToString()))
                {
                    MessageBoxEx.Show("请您选择出库类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UCImportSaleReturnBilling UCPB = new UCImportSaleReturnBilling(Combwh_name.Text.ToString());
                DialogResult result = UCPB.ShowDialog();

                if (result == DialogResult.OK)
                {
                    sb_Fields.Clear();//清空所有字符串
                    sb_Fields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",
                    SaleBillingId, SalebillingNo, WhouseId, WhouseName, PartCode, PartsName, PartsSpec, DrawNum, UnitName, PartsBrand, PartsFacCode,
                    PartsBarCode, SaleRetCnt,UntPrice, Remark);//要查询的字段名称格式项
                    DataTable dt_Parts = null;//配件信息数据表 
                    string BillPrimIDValue = string.Empty;//要查询的开单主键ID条件
                    string PartNumber = string.Empty;//查询的配件编号条件
                    string OutWhTypeName = CombOut_wh_type_name.Text.ToString();//开单类型名
                    BillIDlist = UCPB.StockBillIDLst;//获取开单主键ID集合
                    foreach (string item in UCPB.StockBillIDLst)
                    {
                        BillPrimIDValue = item.ToString();//要查询的条件
                        if (gvPartsMsgList.Rows.Count > 0)
                        {
                            
                            for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                            {
                                //判断要导入销售退货单信息是否已存在于列表中
                                string BPrimID = gvPartsMsgList.Rows[i].Cells["BillingID"].Value.ToString();//开单主键ID
                                if (item.ToString() == BPrimID)
                                {
                                    MessageBoxEx.Show("您要导入的销售退货单已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    //查询符合条件的配件信息
                                    dt_Parts = DBHelper.GetTable(SalepartsLogMsg, SalePartTable, sb_Fields.ToString(), SaleBillingId + "='" + BillPrimIDValue + "'", "", "");
                                }

                            }
                        }
                        else
                        {
                            dt_Parts = DBHelper.GetTable(SalepartsLogMsg, SalePartTable, sb_Fields.ToString(), SaleBillingId + "='" + BillPrimIDValue + "'", "", "");

                        }

                       
                        GetGridViewRowByDrImport(dt_Parts, SaleBillingId);//把查询到的配件信息导入控件列表
                    }
                   

                    //判断配件信息列表中的值是否需要导入
                    if (UCPB.PartHTable.Count != 0)
                    {
                        foreach (DictionaryEntry DicEty in UCPB.PartHTable)
                        {

                            PartNumber = DicEty.Key.ToString();//获取配件编码
                            BillPrimIDValue = DicEty.Value.ToString();//获取开单号
                            //对应单据的配件信息查询条件
                            string PartWhere = " sale_billing_id='" + BillPrimIDValue + "'" + " and  parts_code='" + PartNumber + "'";
                            if (gvPartsMsgList.Rows.Count > 0)
                            {
                                for (int k = 0; k < gvPartsMsgList.Rows.Count; k++)
                                {
                                    string gvPartNumber = gvPartsMsgList.Rows[k].Cells["partsnum"].Value.ToString();//配件列表中的配件编号
                                    string gvBillId = gvPartsMsgList.Rows[k].Cells["BillingID"].Value.ToString();//配件列表中隐藏的开单主键ID
                                   
                                    if (BillPrimIDValue == gvBillId && PartNumber == gvPartNumber)
                                    {
                                        MessageBoxEx.Show("您要导入的" + OutWhTypeName + "配件已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        dt_Parts = DBHelper.GetTable(SalepartsLogMsg, SalePartTable, sb_Fields.ToString(), PartWhere, "", "");
                                        
                                    }
                                }
                            }
                            else
                            {
                                dt_Parts = DBHelper.GetTable(SalepartsLogMsg, SalePartTable, sb_Fields.ToString(), PartWhere, "", "");
                            }

                            GetGridViewRowByDrImport(dt_Parts, SaleBillingId);//导入配件信息列表
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
        /// 修改导入单据的执行状态
        /// </summary>
        private  void SetOrderState()
        { 
          try
          {
              for (int i = 0; i < BillIDlist.Count;i++ )
              {
                  SetBillingStatus(SaleBillTable, SaleBillingId, BillIDlist[i].ToString(), SaleBillModifyLog);//修改开单状态为已占用
              }
          }
          catch (Exception ex)
          {
              MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
          }  
        }
        /// <summary>
        /// 当添加成功时,将开单中的退货/换货状态设置成占用
        /// 使前置单据不可以编辑、删除
        /// </summary>
        /// <param name="BillTable">开单表名</param>
        /// <param name="BPrimIDName">开单主键名称</param>
        /// <param name="BPrimIDValue">开单主键值</param>
        /// <param name="ModifyLog">数据库操作日</param>
        private void SetBillingStatus(string BillTable, string BPrimIDName, string BPrimIDValue, string ModifyLog)
        {
            try
            {

                Dictionary<string, string> dicValue = new Dictionary<string, string>();
                dicValue.Add("is_occupy", "1");//单据导入状态，0正常，1占用，2锁定
                DBHelper.Submit_AddOrEdit(ModifyLog, BillTable, BPrimIDName, BPrimIDValue, dicValue);//修改单据状态

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }



        /// <summary>
        /// 批量导入配件列表信息
        /// </summary>
        /// <param name="partsTable">数据库中获取的配件信息表</param>
        /// <param name="BillID">数据库中获取的配件信息表主键ID</param>
        private void GetGridViewRowByDrImport(DataTable partsTable, string BillID)
        {
            try
            {
                decimal CostAmount = decimal.Zero;//成本总计
                decimal RtnCnt = decimal.Zero;//退货数量
                decimal UntPrice = decimal.Zero;//成本单价
                for (int i = 0; i < partsTable.Rows.Count; i++)
                {

                    DataGridViewRow dgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建行项 
                    dgRow.Cells["BillingID"].Value = partsTable.Rows[i][BillID].ToString();
                    dgRow.Cells["billingno"].Value = partsTable.Rows[i][SalebillingNo].ToString();
                    dgRow.Cells["WhId"].Value = partsTable.Rows[i]["wh_id"].ToString();
                    dgRow.Cells["WhName"].Value = partsTable.Rows[i]["wh_name"].ToString();
                    dgRow.Cells["partsnum"].Value = partsTable.Rows[i]["parts_code"].ToString();
                    dgRow.Cells["partname"].Value = partsTable.Rows[i]["parts_name"].ToString();
                    dgRow.Cells["PartSpec"].Value = partsTable.Rows[i]["model"].ToString();
                    dgRow.Cells["drawingnum"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                    dgRow.Cells["UntName"].Value = partsTable.Rows[i]["unit_name"].ToString();
                    dgRow.Cells["partbrand"].Value = partsTable.Rows[i]["parts_brand_name"].ToString();
                    dgRow.Cells["CarFactoryCode"].Value = partsTable.Rows[i]["car_factory_code"].ToString();
                    dgRow.Cells["BarCode"].Value = partsTable.Rows[i]["parts_barcode"].ToString();
                    dgRow.Cells["counts"].Value = partsTable.Rows[i]["return_count"].ToString();
                    dgRow.Cells["Unitprice"].Value = partsTable.Rows[i]["original_price"].ToString();
                    RtnCnt = Convert.ToDecimal(partsTable.Rows[i]["return_count"]);
                    UntPrice = Convert.ToDecimal(partsTable.Rows[i]["original_price"]);
                    CostAmount = UntPrice * RtnCnt;//计算成本总计
                    dgRow.Cells["Calcmoney"].Value = CostAmount;
                    //dgRow.Cells["MakDate"].Value = partsTable.Rows[i]["make_date"].ToString();
                    //dgRow.Cells["ValDate"].Value = partsTable.Rows[i]["validity_date"].ToString();
                    dgRow.Cells["remarks"].Value = partsTable.Rows[i]["remark"].ToString();


                }

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
                        if (gvPartsMsgList.SelectedRows.Count != 0)
                        {
                            for (int j = 0; j < gvPartsMsgList.SelectedRows.Count; j++)
                            {
                                gvPartsMsgList.Rows.RemoveAt(j);
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
                        BillingIDLst.Add(dr.Cells["BillingID"].ToString());
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
                    for (int i = 2; i < gvPartsMsgList.Columns.Count; i++)
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
        ///报损单配件信息添加
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
                dgvr.Cells["UntName"].Value = dr["sales_unit_name"];
                dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                dgvr.Cells["Unitprice"].Value = dr["ref_in_price"];
                //dgvr.Cells["MakDate"].Value = dr["make_date"].ToString();
                //dgvr.Cells["ValDate"].Value = dr["validity_date"].ToString();
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
                    string opName = "报损单操作";
                    lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.LossBill);//获取报损单编号
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
                        StockLossId = Guid.NewGuid().ToString();
                        AddLossBillSql(listSql, LossBillEntity, StockLossId, HandleTypeName);
                        opName = "新增报损单";
                        if (BillIDlist!= null)
                        {
                            SetOrderState();//修改单据占用状态
                        }
                        AddOrEditPartsListMsg(listSql, StockLossId, WindowStatus.Add);
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditReceiptBillSql(listSql, LossBillEntity, StockLossId, HandleTypeName);
                        opName = "修改报损单";
                        if (BillIDlist != null)
                        {
                            SetOrderState();//修改单据占用状态
                        }
                        AddOrEditPartsListMsg(listSql, StockLossId, WindowStatus.Edit);

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
                        string DefaultWhere = " enable_flag=1 and LsBillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        UCLossBM.GetRepLossBillList(DefaultWhere);//更新单据列表
                        deleteMenuByTag(this.Tag.ToString(), UCLossBM.Name);
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
        private void AddLossBillSql(List<SysSQLString> listSql, tb_parts_stock_loss stockLossEntity, string StockLossId, string HandleType)
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
                CommonFuncCall.FillEntityByControls(this, stockLossEntity);
                stockLossEntity.stock_loss_id = StockLossId;

                stockLossEntity.update_by = GlobalStaticObj.UserID;
                stockLossEntity.operators = GlobalStaticObj.UserID;
                stockLossEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockLossEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockLossEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockLossEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockLossEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (stockLossEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Insert Into tb_parts_stock_loss( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in stockLossEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockLossEntity, null);
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
        private void EditReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_loss stockLossEntity, string StockLosstId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, LossBillEntity);

                stockLossEntity.handle = GlobalStaticObj.UserID;
                stockLossEntity.operators = GlobalStaticObj.UserID;
                stockLossEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    stockLossEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    stockLossEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    stockLossEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    stockLossEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (LossBillEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update tb_parts_stock_loss Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in stockLossEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(stockLossEntity, null);
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
                    sb.Append(" where stock_loss_id='" + StockLosstId + "';");
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
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string LossBillId, WindowStatus WinState)
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
                        DicDelParam.Add("stock_loss_id", LossBillId);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", LossPartTable, LossBillId);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    sb.Append("insert into tb_parts_stock_loss_p (stock_loss_parts_id,stock_loss_id,num");
                    sb.Append(sb_ColName.ToString() + ") values(@stock_loss_parts_id,@stock_loss_id,@num" + sb_ColValue.ToString() + ");");
                    int SerialNum = 1;
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {

                        SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                        DicPartParam.Add("stock_loss_parts_id", Guid.NewGuid().ToString());
                        DicPartParam.Add("stock_loss_id", LossBillId);
                        DicPartParam.Add("reference_billid", DgVRow.Cells["BillingID"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["BillingID"].Value.ToString());
                        DicPartParam.Add("reference_billno", DgVRow.Cells["billingno"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["billingno"].Value.ToString());
                        DicPartParam.Add("wh_id", DgVRow.Cells["WhId"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["WhId"].Value.ToString());
                        DicPartParam.Add("wh_name", DgVRow.Cells["WhName"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["WhName"].Value.ToString());
                        DicPartParam.Add("num", SerialNum.ToString());
                        DicPartParam.Add("parts_code", DgVRow.Cells["partsnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsnum"].Value.ToString());
                        DicPartParam.Add("parts_name", DgVRow.Cells["partname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partname"].Value.ToString());
                        DicPartParam.Add("model", DgVRow.Cells["PartSpec"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["PartSpec"].Value.ToString());
                        DicPartParam.Add("drawing_num", DgVRow.Cells["drawingnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["drawingnum"].Value.ToString());
                        DicPartParam.Add("unit_name", DgVRow.Cells["UntName"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["UntName"].Value.ToString());
                        DicPartParam.Add("parts_brand", DgVRow.Cells["partbrand"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partbrand"].Value.ToString());
                        DicPartParam.Add("car_parts_code", DgVRow.Cells["CarFactoryCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["CarFactoryCode"].Value.ToString());
                        DicPartParam.Add("parts_barcode", DgVRow.Cells["BarCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["BarCode"].Value.ToString());
                        DicPartParam.Add("counts", DgVRow.Cells["counts"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["counts"].Value.ToString());
                        DicPartParam.Add("price", DgVRow.Cells["Unitprice"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["Unitprice"].Value.ToString());
                        DicPartParam.Add("money", DgVRow.Cells["Calcmoney"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["Calcmoney"].Value.ToString());
                        //DicPartParam.Add("make_date", DgVRow.Cells["MakDate"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["MakDate"].Value.ToString());
                        //DicPartParam.Add("validity_date", DgVRow.Cells["ValDate"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["ValDate"].Value.ToString());
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
        /// 验证表中列表数据信息完整性
        /// </summary>
        private bool CheckListInfo()
        {

            foreach (DataGridViewRow dgvr in gvPartsMsgList.Rows)
            {
                decimal LossCount = Convert.ToInt32(dgvr.Cells["counts"].Value.ToString());//报损数量
                string PartCount = CommonCtrl.IsNullToString(dgvr.Cells["counts"].Value);//配件数量
                if (PartCount.Length ==decimal.Zero)
                {
                    MessageBoxEx.Show("请输入配件数量!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["counts"];
                    return false;
                }
                else if (LossCount >= decimal.Zero)
                {
                    MessageBoxEx.Show("您输入的数量不能大于等于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["counts"];
                    return false;

                }


            }
            return true;
        }

        /// <summary>
        /// 根据报损单ID获取表头和尾部信息
        /// </summary>
        /// <param name="ShippingBillId"></param>
        private void GetBillHeadEndMessage(string ShippingBillId)
        {
            try
            {
                if (ShippingBillId != null)
                {
                    //查询一条出入库单信息
                    DataTable ReceiptTable = DBHelper.GetTable(LossBillLogMsg, LossBillTable, "*", LossBillID + "='" + ShippingBillId + "'", "", "");
                    CommonFuncCall.FillEntityByTable(LossBillEntity, ReceiptTable);
                    CommonFuncCall.FillControlsByEntity(this, LossBillEntity);
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
        /// 根据报损单ID获取配件信息表
        /// </summary>
        /// <param name="LossBillIdValue"></param>
        private void GetBillPartsMsg(string LossBillIdValue)
        {
            try
            {
                DataTable PartsMsgList = DBHelper.GetTable(LossPartLogMsg, LossPartTable, "*", LossBillID + "='" + LossBillIdValue + "'", "", "");//获取配件信息表
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

    }
}
