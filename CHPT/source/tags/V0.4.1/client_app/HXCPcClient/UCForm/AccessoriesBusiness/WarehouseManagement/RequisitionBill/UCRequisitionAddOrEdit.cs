using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using Model;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using Utility.Common;
using HXCPcClient.Chooser;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill
{
    public partial class UCRequisitionAddOrEdit : UCBase
    {

        #region 全局变量
        WindowStatus status;
        UCRequisitionManager UCReqBM;
        private string AllotId = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存
        private string SamePrice = DataSources.GetDescription(DataSources.EnumAllotType.SamePrice, true);//同价调拨
        private string ChangePrice = DataSources.GetDescription(DataSources.EnumAllotType.ChangePrice, true);//变价调拨
        int Initialindex = -1;//GridView初始行索引
        private DataTable BrandTable = null;//品牌信息表

        //要查询的开单信息表名
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单信息表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件信息表
        private const string AllotBillId = "stock_allot_id";//出入库单主键
        private const string AllotPartId = "stock_allot_parts_id";//出入库单配件主键
        private string AllotBillLogMsg = "查询调拨单表信息";
        private string AllotPartLogMsg = "查询调拨单配件表信息";
        private string ImportTitle = "导入调拨单配件信息";
        //配件单据列名
        private const string PNum = "partsnum";
        private const string PNam = "partname";
        private const string PSpc = "PartSpec";
        private const string PDrw = "drawingnum";
        private const string PUnt = "unitname";
        private const string PBrd = "partbrand";
        private const string PCpc = "carpartscode";
        private const string PBrc = "partsbarcode";
        private const string PCnt = "partcounts";
        private const string PClp = "callinprice";
        private const string PTmy = "totalmoney";
        private const string PMkd = "makedate";
        private const string PVld = "validitydate";
        private const string PRmk = "remarks";

        //创建表头实体类
        tb_parts_stock_allot AllotBillEntity = new tb_parts_stock_allot();
        #endregion


        /// <summary>
        /// 初始化出入单窗体信息
        /// </summary>
        public UCRequisitionAddOrEdit(WindowStatus state, string AllotBillId, UCRequisitionManager UCReqManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCReqBM = UCReqManager;//获取出入库单管理类
            this.status = state;//获取操作状态
            this.AllotId = AllotBillId;//出入库单ID
            base.SaveEvent += new ClickHandler(UCRequisitionAddOrEdit_SaveEvent);//保存
            base.DeleteEvent += new ClickHandler(UCRequisitionAddOrEdit_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCRequisitionAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCRequisitionAddOrEdit_ImportEvent);//导入
            //设置列表的可编辑状态
            gvPartsMsgList.ReadOnly = false;
            foreach (DataGridViewColumn dgCol in gvPartsMsgList.Columns)
            {
                if (dgCol.Name != colCheck.Name && dgCol.Name != partcounts.Name && dgCol.Name != callinprice.Name&&dgCol.Name!=remarks.Name) dgCol.ReadOnly = true;
            }
 
            base.btnExport.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnEdit.Visible = false;
            base.btnBalance.Visible = false;
            base.btnPrint.Visible = false;
        }

       private  void UCRequisitionAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            DelSelectedRows();//删除选中配件信息
        }

        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCRequisitionAddOrEdit_Load(object sender, EventArgs e)
        {
            try
            {
                CommonFuncCall.BindAllotBillType(Comborder_type_name, true, "请选择");//调拨单类型
                CommonFuncCall.BindComBoxDataSource(Combtrans_way_name, "sys_trans_mode", "请选择");//运输方式

                CommonFuncCall.BindCompany(combcall_out_org_name, "请选择");//调出机构
                CommonFuncCall.BindCompany(combcall_in_org_name, "请选择");//调入机构
                CommonFuncCall.BindWarehouse(combcall_out_wh_name, "请选择");//调出仓库
                CommonFuncCall.BindWarehouse(combcall_in_wh_name, "请选择");//调入仓库
                BrandTable = CommonFuncCall.BindDicDataSource("sys_parts_brand");//获得品牌名称

                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(AllotBillId);//获取单据头尾信息
                    GetBillPartsMsg(AllotBillId);//获取单据配件信息

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
        /// 调拨单保存
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(Comborder_type_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的单据类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(combcall_out_org_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的调出机构！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(combcall_out_wh_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的调出仓库！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(combcall_in_org_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的调入机构！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(combcall_in_wh_name.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请您选择对应的调入仓库！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (gvPartsMsgList.Rows.Count == 0)
            {
                MessageBoxEx.Show("请您导入要生成调拨单的配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 调拨单提交
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 调拨单配件信息导入
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCRequisitionAddOrEdit_ImportEvent(object send, EventArgs e)
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
        /// 根据用户输入单价与数量计算总金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                {
                    if (gvPartsMsgList.Rows[i].Cells["callinprice"].Value==null && gvPartsMsgList.Rows[i].Cells["partcounts"].Value==null)
                    {
                        MessageBoxEx.Show("您输入的调入数量或单价不能同时为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (gvPartsMsgList.Rows[i].Cells["callinprice"].Value == null)
                    {
                        MessageBoxEx.Show("您输入的调入单价不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (gvPartsMsgList.Rows[i].Cells["partcounts"].Value == null)
                    {
                        MessageBoxEx.Show("您输入的调入数量不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else  if (!IsNumber(gvPartsMsgList.Rows[i].Cells["partcounts"].Value.ToString()) || !IsNumber(gvPartsMsgList.Rows[i].Cells["callinprice"].Value.ToString()))
                    {
                        MessageBoxEx.Show("请您输入数字类型的数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvPartsMsgList.Rows[i].Cells["partcounts"].Value = string.Empty;
                        gvPartsMsgList.Rows[i].Cells["callinprice"].Value = string.Empty;
                        return;
                    }
                    int AllotCount = Convert.ToInt32(gvPartsMsgList.Rows[i].Cells["partcounts"].Value.ToString());//调拨数量
                    decimal AllotUntPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["callinprice"].Value.ToString());//调拨单价
                    if (AllotCount <= 0 || AllotUntPrice<=decimal.Zero)
                    {
                        MessageBoxEx.Show("您输入的调入数量或单价不能小于等于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    else
                    {
                        gvPartsMsgList.Rows[i].Cells["totalmoney"].Value = AllotUntPrice * AllotCount;//计算调拨总金额
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
        /// 调拨单保存与提交
        /// </summary>
        /// <param name="OperateMode"></param>
        private void SaveOrSubmitMethod(string HandleTypeName)
        {
            try
            {

                if (CheckListInfo())
                {
                    gvPartsMsgList.EndEdit();
                    string opName = "调拨单操作";
                    if (HandleTypeName == submit)
                    {

                        lblorder_num.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.AllotBill);//获取调拨单编号
                        txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                    }

                    List<SysSQLString> listSql = new List<SysSQLString>();
                    if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    {
                        AllotId = Guid.NewGuid().ToString();
                        AddAllotBillSql(listSql, AllotBillEntity, AllotId, HandleTypeName);
                        opName = "新增调拨单";
                        AddOrEditPartsListMsg(listSql, AllotId, WindowStatus.Add);
                    }
                    else if (status == WindowStatus.Edit)
                    {
                        EditAllotBillSql(listSql, AllotBillEntity, AllotId, HandleTypeName);
                        opName = "修改调拨单";
                        AddOrEditPartsListMsg(listSql, AllotId, WindowStatus.Edit);

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
                        string DefaultWhere = " enable_flag=1 and BillTb.order_date between  " + StartDate + " and " + EndDate;//默认查询条件
                        UCReqBM.GetAllotBillList(DefaultWhere);//更新单据列表
                        deleteMenuByTag(this.Tag.ToString(), UCReqBM.Name);
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
        private void AddAllotBillSql(List<SysSQLString> listSql, tb_parts_stock_allot AllotEntity, string StockAllotId, string HandleType)
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
                CommonFuncCall.FillEntityByControls(this, AllotEntity);
                AllotEntity.stock_allot_id = StockAllotId;

                AllotEntity.update_by = GlobalStaticObj.UserID;
                AllotEntity.operators = GlobalStaticObj.UserID;
                AllotEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    AllotEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    AllotEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    AllotEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    AllotEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (AllotEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Insert Into " + AllotBillTable + "( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in AllotEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(AllotEntity, null);
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
        private void EditAllotBillSql(List<SysSQLString> listSql, tb_parts_stock_allot AllotEntity, string AllotBillIdValue, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, AllotBillEntity);

                AllotEntity.handle = GlobalStaticObj.UserID;
                AllotEntity.operators = GlobalStaticObj.UserID;
                AllotEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    AllotEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    AllotEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    AllotEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    AllotEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (AllotBillEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update " + AllotBillTable + " Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in AllotEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(AllotEntity, null);
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
                    sb.Append(" where " + AllotBillId + "='" + AllotBillIdValue + "';");
                    sysStrSql.sqlString = sb.ToString();
                    sysStrSql.Param = dicParam;
                    listSql.Add(sysStrSql);//完成SQL语句的拼装
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message,"异常提示",MessageBoxButtons.OK,MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 根据调拨单主键ID和表头信息对配件信息列表进行添加或编辑
        /// </summary>
        /// <param name="listSQL">要执行的SQL语句列表</param>
        /// <param name="AllotIdValue">出入库单号</param>
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string AllotIdValue,WindowStatus WinState)
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
                        DicDelParam.Add("stock_allot_id", AllotIdValue);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", AllotPartTable, AllotBillId);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    sb.Append("insert into " + AllotPartTable + " (" + AllotPartId + "," + AllotBillId + ",num");
                    sb.Append(sb_ColName.ToString() + ") values(@" + AllotPartId + ",@" + AllotBillId + ",@num" + sb_ColValue.ToString() + ");");
                    int SerialNum = 1;
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {
                        SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                        DicPartParam.Add(AllotPartId, Guid.NewGuid().ToString());//生成调拨单配件主键ID
                        DicPartParam.Add(AllotBillId, AllotIdValue);//获取调拨单主键ID
                        DicPartParam.Add("num", SerialNum.ToString());
                        DicPartParam.Add("parts_code", DgVRow.Cells["partsnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsnum"].Value.ToString());
                        DicPartParam.Add("parts_name", DgVRow.Cells["partname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partname"].Value.ToString());
                        DicPartParam.Add("model", DgVRow.Cells["PartSpec"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["PartSpec"].Value.ToString());
                        DicPartParam.Add("drawing_num", DgVRow.Cells["drawingnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["drawingnum"].Value.ToString());
                        DicPartParam.Add("unit_name", DgVRow.Cells["unitname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["unitname"].Value.ToString());
                        DicPartParam.Add("parts_brand", DgVRow.Cells["partbrand"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partbrand"].Value.ToString());//品牌
                        DicPartParam.Add("car_parts_code", DgVRow.Cells["carpartscode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["carpartscode"].Value.ToString());//车厂编码
                        DicPartParam.Add("parts_barcode", DgVRow.Cells["partsbarcode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsbarcode"].Value.ToString());
                        DicPartParam.Add("counts", DgVRow.Cells["partcounts"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partcounts"].Value.ToString());
                        DicPartParam.Add("call_in_price", DgVRow.Cells["callinprice"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["callinprice"].Value.ToString());
                        DicPartParam.Add("money", DgVRow.Cells["totalmoney"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["totalmoney"].Value.ToString());
                        long MkDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DgVRow.Cells["makedate"].Value.ToString()));
                        DicPartParam.Add("make_date", MkDate.ToString() == string.Empty ? "" : MkDate.ToString());
                        long ValDate = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(DgVRow.Cells["validitydate"].Value.ToString()));
                        DicPartParam.Add("validity_date", ValDate.ToString() == string.Empty ? "" : ValDate.ToString());
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
        /// 配件新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartAdd_Click(object sender, EventArgs e)
        {
            try
            {
               PartsAdd();//新增配件
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
                    string PCpcExcelField = string.Empty;
                    string PBrcExcelField = string.Empty;
                    string PCntExcelField = string.Empty;
                    string PClpExcelField = string.Empty;
                    string PTmyExcelField = string.Empty;
                    string PMkdExcelField = string.Empty;
                    string PVldExcelField = string.Empty;
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
                            case PCpc:
                                PCpcExcelField = ExcelField;
                                break;
                            case PBrc:
                                PBrcExcelField = ExcelField;
                                break;
                            case PCnt:
                                PCntExcelField = ExcelField;
                                break;
                            case PClp:
                                PClpExcelField = ExcelField;
                                break;
                            case PTmy:
                                PTmyExcelField = ExcelField;
                                break;
                            case PMkd:
                                PMkdExcelField = ExcelField;
                                break;
                            case PVld:
                                PVldExcelField = ExcelField;
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
                            DgRow.Cells[PCpc].Value = frmExcel.ExcelTable.Rows[i][PCpcExcelField].ToString();
                            DgRow.Cells[PBrc].Value = frmExcel.ExcelTable.Rows[i][PBrcExcelField].ToString();
                            DgRow.Cells[PCnt].Value = frmExcel.ExcelTable.Rows[i][PCntExcelField].ToString();
                            DgRow.Cells[PClp].Value = frmExcel.ExcelTable.Rows[i][PClpExcelField].ToString();
                            DgRow.Cells[PTmy].Value = frmExcel.ExcelTable.Rows[i][PTmyExcelField].ToString();
                            DgRow.Cells[PMkd].Value = frmExcel.ExcelTable.Rows[i][PMkdExcelField].ToString();
                            DgRow.Cells[PVld].Value = frmExcel.ExcelTable.Rows[i][PVldExcelField].ToString();
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
            for (int i = 0; i < ExcelTable.Rows.Count;i++ )
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
        /// 调拨单配件信息添加或编辑
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
                                MessageBoxEx.Show("不能重复添加配件信息！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                dgvr.Cells["carpartscode"].Value = dr["car_parts_code"];
                dgvr.Cells["partsbarcode"].Value = dr["parts_barcode"];
                //dgvr.Cells["makedate"].Value = dr["make_date"];
                //dgvr.Cells["validitydate"].Value = dr["validity_date"];
                dgvr.Cells["remarks"].Value = dr["remark"];

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

                string PartCount = CommonCtrl.IsNullToString(dgvr.Cells["partcounts"].Value);//配件数量
                if (PartCount.Length == 0)
                {
                    MessageBoxEx.Show("请输入配件数量!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    gvPartsMsgList.CurrentCell = dgvr.Cells["partcounts"];
                    return false;
                }


            }
            return true;
        }

        /// <summary>
        /// 根据调拨单ID获取表头和尾部信息
        /// </summary>
        /// <param name="AllotOrderIdValue"></param>
        private void GetBillHeadEndMessage(string AllotOrderIdValue)
        {
            try
            {
                if (AllotOrderIdValue != null)
                {
                    //查询一条出入库单信息
                    DataTable InoutTable = DBHelper.GetTable(AllotBillLogMsg, AllotBillTable, "*", AllotBillId + "='" + AllotOrderIdValue + "'", "", "");
                    CommonFuncCall.FillEntityByTable(AllotBillEntity, InoutTable);
                    CommonFuncCall.FillControlsByEntity(this, AllotBillEntity);
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
        /// 根据调拨单ID获取配件信息表
        /// </summary>
        /// <param name="AllotBillIdValue"></param>
        private void GetBillPartsMsg(string AllotBillIdValue)
        {
            try
            {

                DataTable PartsMsgList = DBHelper.GetTable(AllotPartLogMsg, AllotPartTable, "*", AllotBillId + "='" + AllotBillIdValue + "'", "", "");//获取配件信息表
                if (PartsMsgList.Rows.Count > 0)
                {

                    foreach (DataRow dr in gvPartsMsgList.Rows)
                    {
                        DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];

                        dgvr.Cells["partsnum"].Value = dr["parts_code"];
                        dgvr.Cells["partname"].Value = dr["parts_name"];
                        dgvr.Cells["PartSpec"].Value = dr["model"];
                        dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                        dgvr.Cells["unitname"].Value = dr["unit_name"];
                        dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                        dgvr.Cells["carpartscode"].Value = dr["car_parts_code"];
                        dgvr.Cells["partsbarcode"].Value = dr["parts_barcode"];
                        dgvr.Cells["partcounts"].Value = dr["counts"];
                        dgvr.Cells["callinprice"].Value = dr["call_in_price"];
                        dgvr.Cells["totalmoney"].Value = dr["money"];
                        dgvr.Cells["makedate"].Value = dr["make_date"];
                        dgvr.Cells["validitydate"].Value = dr["validity_date"];
                        dgvr.Cells["remarks"].Value = dr["remark"];
                    }
                }
                else
                {
                    MessageBoxEx.Show("您要查询的配件信息不存在！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
    }

}
