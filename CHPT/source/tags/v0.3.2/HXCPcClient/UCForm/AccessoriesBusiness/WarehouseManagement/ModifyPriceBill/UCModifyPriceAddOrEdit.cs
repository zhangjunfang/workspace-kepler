﻿using System;
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
using HXCPcClient.Chooser;
using System.Collections;
using System.Text.RegularExpressions;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ModifyPriceBill
{
    public partial class UCModifyPriceAddOrEdit : UCBase
    {

        #region 全局变量
        WindowStatus status;
        UCModifyPriceManager UCModifyPriceBM;
        private string ModifyPriceId = string.Empty;
        private string submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交
        private string save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存
        int Initialindex = -1;//GridView初始行索引
        private string ModifyPriceBillID = "stock_modifyprice_id";
        private string ModBillTable = "tb_parts_stock_modifyprice";
        private string ModPartTable = "tb_parts_stock_modifyprice_p";

        private string ModBillLogMsg = "查询调价单表信息";
        private string ModPartLogMsg = "查询调价单配件表信息";
        private string ImportTitle = "导入调价单配件信息";

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
        private const string PMup = "ModyUnitprice";
        private const string PMpr = "ModyPricRate";
        private const string PMap = "ModyAfterPric";
        private const string PCmy = "Calcmoney";
        private const string PRmk = "remarks";
        /// <summary>
        /// 获取其它收货库单表头表尾信息实体
        /// </summary>
        tb_parts_stock_modifyprice ModifyPriceBillEntity = new tb_parts_stock_modifyprice();
        #endregion
        public UCModifyPriceAddOrEdit(WindowStatus state, string ModPricId, UCModifyPriceManager UCModManager)
        {
            InitializeComponent();
            DTPickorder_date.Value = DateTime.Now.ToShortDateString();//获取当前系统时间
            this.UCModifyPriceBM = UCModManager;//获取其它收货单管理类
            this.status = state;//获取操作状态
            this.ModifyPriceId = ModPricId;//其它收货单ID
            base.SaveEvent += new ClickHandler(UCModifyPriceAddOrEdit_SaveEvent);//保存
            base.SubmitEvent += new ClickHandler(UCModifyPriceAddOrEdit_SubmitEvent);//提交
            base.ImportEvent += new ClickHandler(UCModifyPriceAddOrEdit_ImportEvent);//导入
            gvPartsMsgList.ReadOnly = false;
            ID.ReadOnly = true;
            partsnum.ReadOnly = true;
            partname.ReadOnly = true;
            PartSpec.ReadOnly = true;
            drawingnum.ReadOnly = true;
            unitname.ReadOnly = true;
            partbrand.ReadOnly = true;
            CarFactoryCode.ReadOnly = true;
            BarCode.ReadOnly = true;
            ModyUnitprice.ReadOnly = true;
            Calcmoney.ReadOnly = true;
            base.btnExport.Enabled = false;
            base.btnConfirm.Enabled = false;
            base.btnEdit.Enabled = false;
            base.btnBalance.Enabled = false;
            base.btnPrint.Enabled = false;

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
                //获取仓库名称
                CommonFuncCall.BindWarehouse(Combwh_name, "请选择");
                //公司ID
                string com_id = GlobalStaticObj.CurrUserCom_Id;
                CommonFuncCall.BindCompany(combcom_name, "全部");//选择公司名称
                CommonFuncCall.BindDepartment(Comborg_name, com_id, "请选择");//选择部门名称
                CommonFuncCall.BindHandle(Combhandle_name, "", "请选择");//选择经手人

                if (status == WindowStatus.Edit || status == WindowStatus.Copy)
                {
                    GetBillHeadEndMessage(ModifyPriceId);//获取单据头尾信息
                    GetBillPartsMsg(ModifyPriceId);//获取单据配件信息

                }
                else if (status == WindowStatus.Add || status == WindowStatus.Copy)
                {
                    txtorder_status_name.Caption = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);//获取单据状态
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        /// <summary>
        /// 保存调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceAddOrEdit_SaveEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(save);
        }
        /// <summary>
        /// 提交调价单
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceAddOrEdit_SubmitEvent(object send, EventArgs e)
        {
            SaveOrSubmitMethod(submit);
        }
        /// <summary>
        /// 导入调价单配件信息
        /// </summary>
        /// <param name="send"></param>
        /// <param name="e"></param>
        private void UCModifyPriceAddOrEdit_ImportEvent(object send, EventArgs e)
        {
            try
            {
                BtnImportMenu.Show(btnImport, 0, btnImport.Height);//指定导入菜单在导入按钮位置显示
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
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


                gvPartsMsgList.EndEdit();
                string opName = "调价单操作";
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
                    ModifyPriceId = Guid.NewGuid().ToString();
                    AddReceiptBillSql(listSql, ModifyPriceBillEntity, ModifyPriceId, HandleTypeName);
                    opName = "新增调价单";
                    AddOrEditPartsListMsg(listSql, ModifyPriceId, WindowStatus.Add);

                }
                else if (status == WindowStatus.Edit)
                {
                    EditReceiptBillSql(listSql, ModifyPriceBillEntity, ModifyPriceId, HandleTypeName);
                    opName = "修改调价单";
                    AddOrEditPartsListMsg(listSql, ModifyPriceId, WindowStatus.Edit);

                }
                

                if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("保存成功！");
                    string QueryWhere = "enable_flag=1 ";//获取查询条件
                    UCModifyPriceBM.GetModifyPriceBillList(QueryWhere);//更新单据列表
                    deleteMenuByTag(this.Tag.ToString(), UCModifyPriceBM.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败！");
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！" + ex.Message);
            }
        }




        /// <summary>
        /// 添加情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        private void AddReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_modifyprice ModyPricEntity, string ModyPricId, string HandleType)
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
                CommonFuncCall.FillEntityByControls(this, ModyPricEntity);
                ModyPricEntity.stock_modifyprice_id = ModyPricId;

                ModyPricEntity.update_by = GlobalStaticObj.UserID;
                ModyPricEntity.operators = GlobalStaticObj.UserID;
                ModyPricEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    ModyPricEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    ModyPricEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    ModyPricEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    ModyPricEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (ModyPricEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Insert Into tb_parts_stock_modifyprice( ");
                    StringBuilder sb_PrValue = new StringBuilder();
                    StringBuilder sb_PrName = new StringBuilder();
                    foreach (PropertyInfo info in ModyPricEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(ModyPricEntity, null);
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
                MessageBoxEx.Show(ex.Message);
            }
        }


        /// <summary>
        /// 编辑情况下组装sql的方法
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="purchase_billing_id"></param>
        /// <param name="model"></param>
        private void EditReceiptBillSql(List<SysSQLString> listSql, tb_parts_stock_modifyprice ModyPricEntity, string ModyPricId, string HandleType)
        {
            try
            {
                const string NoDelFlag = "1";//默认删除标记，1表示未删除，0表示删除
                string Save = DataSources.GetDescription(DataSources.EnumOperateType.save, true);//保存操作
                string Submit = DataSources.GetDescription(DataSources.EnumOperateType.submit, true);//提交操作
                SysSQLString sysStrSql = new SysSQLString();
                sysStrSql.cmdType = CommandType.Text;//sql字符串语句执行函数
                Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
                CommonFuncCall.FillEntityByControls(this, ModifyPriceBillEntity);

                ModyPricEntity.handle = GlobalStaticObj.UserID;
                ModyPricEntity.operators = GlobalStaticObj.UserID;
                ModyPricEntity.enable_flag = NoDelFlag;
                if (HandleType == Save)
                {
                    ModyPricEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                    ModyPricEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                }
                else if (HandleType == Submit)
                {
                    ModyPricEntity.order_status = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    ModyPricEntity.order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);
                }
                if (ModifyPriceBillEntity != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Update tb_parts_stock_modifyprice Set ");
                    bool isFirstValue = true;
                    foreach (PropertyInfo info in ModyPricEntity.GetType().GetProperties())
                    {
                        string name = info.Name;
                        object value = info.GetValue(ModyPricEntity, null);
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
                    sb.Append(" where stock_modifyprice_id='" + ModyPricId + "';");
                    sysStrSql.sqlString = sb.ToString();
                    sysStrSql.Param = dicParam;
                    listSql.Add(sysStrSql);//完成SQL语句的拼装
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }


        /// <summary>
        /// 根据调价单主键ID和表头信息对配件信息列表进行添加或编辑
        /// </summary>
        /// <param name="listSQL">要执行的SQL语句列表</param>
        /// <param name="ModyPricId">调价单号</param>
        private void AddOrEditPartsListMsg(List<SysSQLString> listSQL, string ModyPricId,WindowStatus WinState)
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
                    for (int i = 2; i < gvPartsMsgList.Columns.Count; i++)
                    {
                        ColName = gvPartsMsgList.Columns[i].Name.ToString();
                        sb_ColName.Append("," + ColName);//数据表字段名
                        sb_ColValue.Append(",@" + ColName);//数据表值的参数名

                    }
                    if (WinState == WindowStatus.Edit)
                    {
                        SysSQLString SQLStr = new SysSQLString();//创建存储要删除sql语句的实体类
                        SQLStr.cmdType = CommandType.Text;//sql语句执行格式
                        Dictionary<string, string> DicDelParam = new Dictionary<string, string>();
                        StringBuilder sbDelSql = new StringBuilder();
                        DicDelParam.Add("stock_modifyprice_id", ModyPricId);
                        sbDelSql.AppendFormat("delete from {0} where {1}=@{1}", ModPartTable, ModifyPriceBillID);
                        SQLStr.Param = DicDelParam;//获取删除的参数名值对
                        SQLStr.sqlString = sbDelSql.ToString();//获取执行删除的操作
                        listSQL.Add(SQLStr);//添加到执行实例对像中
                    }
                    sb.Append("insert into tb_parts_stock_modifyprice_p (stock_modifyprice_parts_id,stock_modifyprice_id,num");
                    sb.Append(sb_ColName.ToString() + ") values(@stock_modifyprice_parts_id,@stock_modifyprice_id,@num" + sb_ColValue.ToString() + ");");
                    foreach (DataGridViewRow DgVRow in gvPartsMsgList.Rows)
                    {

                        SysSQLString StrSqlParts = new SysSQLString();//创建存储要执行的sql语句的实体类
                        StrSqlParts.cmdType = CommandType.Text;//指定sql语句执行格式
                        Dictionary<string, string> DicPartParam = new Dictionary<string, string>();//字段参数值集合
                        DicPartParam.Add("stock_modifyprice_parts_id", Guid.NewGuid().ToString());
                        DicPartParam.Add("stock_modifyprice_id", ModyPricId);
                        DicPartParam.Add("num", DgVRow.Cells["num"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["ID"].Value.ToString());
                        DicPartParam.Add("parts_code", DgVRow.Cells["parts_num"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partsnum"].Value.ToString());
                        DicPartParam.Add("parts_name", DgVRow.Cells["parts_name"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partname"].Value.ToString());
                        DicPartParam.Add("model", DgVRow.Cells["PartSpec"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["PartSpec"].Value.ToString());
                        DicPartParam.Add("drawing_num", DgVRow.Cells["drawingnum"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["drawingnum"].Value.ToString());
                        DicPartParam.Add("unit_name", DgVRow.Cells["unitname"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["unitname"].Value.ToString());
                        DicPartParam.Add("parts_brand", DgVRow.Cells["partbrand"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["partbrand"].Value.ToString());
                        DicPartParam.Add("car_parts_code", DgVRow.Cells["CarFactoryCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["CarFactoryCode"].Value.ToString());
                        DicPartParam.Add("parts_barcode", DgVRow.Cells["BarCode"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["BarCode"].Value.ToString());
                        DicPartParam.Add("counts", DgVRow.Cells["counts"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["counts"].Value.ToString());
                        DicPartParam.Add("modify_price", DgVRow.Cells["ModyUnitprice"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["ModyUnitprice"].Value.ToString());
                        DicPartParam.Add("modifyprice_ratio", DgVRow.Cells["ModyPricRate"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["ModyPricRate"].Value.ToString());
                        DicPartParam.Add("modify_after_price", DgVRow.Cells["ModyAfterPric"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["ModyAfterPric"].Value.ToString());
                        DicPartParam.Add("money", DgVRow.Cells["Calcmoney"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["Calcmoney"].Value.ToString());
                        DicPartParam.Add("remark", DgVRow.Cells["remarks"].Value.ToString() == string.Empty ? "" : DgVRow.Cells["remarks"].Value.ToString());
                        StrSqlParts.sqlString = sb.ToString();//获取执行的sql语句
                        StrSqlParts.Param = DicPartParam;//获取执行的参数值
                        listSQL.Add(StrSqlParts);//添加记录行到list列表中

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 调价比率和调后单价输入控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < gvPartsMsgList.Rows.Count;i++ )
                {
                    
                        if (gvPartsMsgList.Rows[i].Cells["ModyPricRate"].Value==null )
                        {
                            MessageBoxEx.Show("您输入的调价比率不能同时为空！");
                            return;
                        }
                        else if (gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].Value == null)
                        {
                            MessageBoxEx.Show("调后单价不能同时为空！");
                            return;
                        }
                        else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["ModyPricRate"].Value.ToString()))
                       {
                        MessageBoxEx.Show("请您输入数字类型的数值！");
                        gvPartsMsgList.Rows[i].Cells["ModyPricRate"].Value = string.Empty;
                        return;
                       }
                        else if (!IsNumber(gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].Value.ToString()))
                      {
                        MessageBoxEx.Show("请您输入数字类型的数值！");
                        gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].Value = string.Empty;
                        return;
                      }
                    else
                    {
                        decimal ModyPriceRate = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["ModyPricRate"].ToString());//调价比率
                        decimal ModyAfterPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].ToString());//调后单价
                        decimal ModyUntPrice = Convert.ToDecimal(gvPartsMsgList.Rows[i].Cells["ModyUnitprice"].ToString());//调整单价
                        int TAmount = Convert.ToInt32(gvPartsMsgList.Rows[i].Cells["counts"].ToString());//数量

                         if (!string.IsNullOrEmpty(gvPartsMsgList.Rows[i].Cells["ModyPricRate"].ToString()))
                        {

                            gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].Value = ModyUntPrice * ModyPriceRate / 100;//计算调后单价
                            gvPartsMsgList.Rows[i].Cells["Calcmoney"].Value = (ModyAfterPrice - ModyUntPrice) * TAmount;//计算总金额
                        }
                        else if (!string.IsNullOrEmpty(gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].ToString()))
                        {
                            gvPartsMsgList.Rows[i].Cells["ModyPricRate"].Value = ModyAfterPrice / ModyUntPrice * 100;//计算调价比率
                            gvPartsMsgList.Rows[i].Cells["Calcmoney"].Value = (ModyAfterPrice - ModyUntPrice) * TAmount;//计算总金额
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据调价单ID获取表头和尾部信息
        /// </summary>
        /// <param name="ModifyPriceBillId"></param>
        private void GetBillHeadEndMessage(string ModifyPriceBillId)
        {
            try
            {
                if (ModifyPriceBillId != null)
                {
                    //查询一条调价单信息
                    DataTable ModyPricTable = DBHelper.GetTable(ModBillLogMsg, ModBillTable, "*", ModifyPriceBillID + "='" + ModifyPriceBillId + "'", "", "");
                    CommonFuncCall.FillEntityByTable(ModifyPriceBillEntity, ModyPricTable);
                    CommonFuncCall.FillControlsByEntity(this, ModifyPriceBillEntity);
                    if (status == WindowStatus.Copy)
                    {
                        lblorder_num.Text = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据调价单ID获取配件信息表
        /// </summary>
        /// <param name="ModBillIdValue"></param>
        private void GetBillPartsMsg(string ModBillIdValue)
        {
            try
            {
                int conId = 1;//序号
                DataTable PartsMsgList = DBHelper.GetTable(ModPartLogMsg, ModPartTable, "*", ModifyPriceBillID + "='" + ModBillIdValue + "'", "", "");//获取配件信息表
                if (PartsMsgList.Rows.Count > 0)
                {

                    foreach (DataRow dr in PartsMsgList.Rows)
                    {
                        DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];
                        dgvr.Cells["ID"].Value = conId;
                        dgvr.Cells["partsnum"].Value = dr["parts_code"];
                        dgvr.Cells["partname"].Value = dr["parts_name"];
                        dgvr.Cells["PartSpec"].Value = dr["model"];
                        dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                        dgvr.Cells["unitname"].Value = dr["unit_name"];
                        dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                        dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                        dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                        dgvr.Cells["counts"].Value = dr["counts"];
                        dgvr.Cells["ModyUnitprice"].Value = dr["modify_price"];
                        dgvr.Cells["ModyPricRate"].Value = dr["modifyprice_ratio"];
                        dgvr.Cells["ModyAfterPric"].Value = dr["modify_after_price"];
                        dgvr.Cells["Calcmoney"].Value = dr["money"];
                        dgvr.Cells["remarks"].Value = dr["remark"];
                        conId++;//自动增加
                    }
                }
                else
                {
                    MessageBoxEx.Show("要查询的配件信息不存在！");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
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
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 配件删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartDelete_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult DgResult = MessageBoxEx.Show("您确定要删除选中记录行项！", "删除操作", MessageBoxButtons.YesNo);
                if (DgResult == DialogResult.Yes)
                {
                    for (int i = 0; i < gvPartsMsgList.SelectedRows.Count; i++)
                    {

                        gvPartsMsgList.Rows.RemoveAt(i);//删除选中行配件
                        MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }


        /// <summary>
        /// 调价单配件信息添加
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

                            if (dr.Cells["parts_code"].Value.ToString() == PartsCode)
                            {
                                MessageBoxEx.Show("该配件信息已经存在与列表中，不能再次添加!");
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
                        int SerialID = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            DataGridViewRow dgvr = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];
                            GetGridViewRowByDr(dgvr, dr, SerialID);
                            SerialID++;//序号自动增长
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

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
                    int SerialNum = 1;//列序号
                    string PNumExcelField = string.Empty;
                    string PNamExcelField = string.Empty;
                    string PSpcExcelField = string.Empty;
                    string PDrwExcelField = string.Empty;
                    string PUntExcelField = string.Empty;
                    string PBrdExcelField = string.Empty;
                    string PCfcExcelField = string.Empty;
                    string PBrcExcelField = string.Empty;
                    string PCntExcelField = string.Empty;
                    string PMupExcelField = string.Empty;
                    string PMprExcelField = string.Empty;
                    string PMapExcelField = string.Empty;
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
                            case PMup:
                                PMupExcelField = ExcelField;
                                break;
                            case PMpr:
                                PMprExcelField = ExcelField;
                                break;
                            case PMap:
                                PMapExcelField = ExcelField;
                                break;
                            case PCmy:
                                PCmyExcelField = ExcelField;
                                break;
                            case PRmk:
                                PRmkExcelField = ExcelField;
                                break;

                        }
                    }

                    for (int i = 0; i < frmExcel.ExcelTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < frmExcel.ExcelTable.Columns.Count; j++)
                        {
                            if (string.IsNullOrEmpty(frmExcel.ExcelTable.Rows[i][j].ToString()))
                            {
                                MessageBoxEx.Show("Excel配件信息模板中存在空数据行项！");
                                gvPartsMsgList.Rows.Clear();//清空之前导入数据
                                return;
                            }
                            else
                            {
                                DataGridViewRow DgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建新行项
                                DgRow.Cells[PID].Value = SerialNum;
                                DgRow.Cells[PNum].Value = frmExcel.ExcelTable.Rows[i][PNumExcelField].ToString();
                                DgRow.Cells[PNam].Value = frmExcel.ExcelTable.Rows[i][PNamExcelField].ToString();
                                DgRow.Cells[PSpc].Value = frmExcel.ExcelTable.Rows[i][PSpcExcelField].ToString();
                                DgRow.Cells[PDrw].Value = frmExcel.ExcelTable.Rows[i][PDrwExcelField].ToString();
                                DgRow.Cells[PUnt].Value = frmExcel.ExcelTable.Rows[i][PUntExcelField].ToString();
                                DgRow.Cells[PBrd].Value = frmExcel.ExcelTable.Rows[i][PBrdExcelField].ToString();
                                DgRow.Cells[PCfc].Value = frmExcel.ExcelTable.Rows[i][PCfcExcelField].ToString();
                                DgRow.Cells[PBrc].Value = frmExcel.ExcelTable.Rows[i][PBrcExcelField].ToString();
                                DgRow.Cells[PCnt].Value = frmExcel.ExcelTable.Rows[i][PCntExcelField].ToString();
                                DgRow.Cells[PMup].Value = frmExcel.ExcelTable.Rows[i][PMupExcelField].ToString();
                                DgRow.Cells[PMpr].Value = frmExcel.ExcelTable.Rows[i][PMprExcelField].ToString();
                                DgRow.Cells[PMap].Value = frmExcel.ExcelTable.Rows[i][PMapExcelField].ToString();
                                DgRow.Cells[PCmy].Value = frmExcel.ExcelTable.Rows[i][PCmyExcelField].ToString();
                                DgRow.Cells[PRmk].Value = frmExcel.ExcelTable.Rows[i][PRmkExcelField].ToString();
                                SerialNum++;//序号自动增加

                            }

                        }

                    }

                    frmExcel.ExcelTable.Clear();//清空所有配件信息

                }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
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
        private void GetGridViewRowByDr(DataGridViewRow dgvr, DataRow dr, int SerialNum)
        {
            try
            {
                dgvr.Cells["ID"].Value = SerialNum;
                dgvr.Cells["partsnum"].Value = dr["ser_parts_code"];
                dgvr.Cells["partname"].Value = dr["parts_name"];
                dgvr.Cells["PartSpec"].Value = dr["model"];
                dgvr.Cells["drawingnum"].Value = dr["drawing_num"];
                dgvr.Cells["unitname"].Value = dr["sales_unit_name"];
                dgvr.Cells["partbrand"].Value = dr["parts_brand"];
                dgvr.Cells["CarFactoryCode"].Value = dr["car_parts_code"];
                dgvr.Cells["BarCode"].Value = dr["parts_barcode"];
                dgvr.Cells["counts"].Value = dr["sales_unit_quantity"];//该配件的账面库存数量
                dgvr.Cells["ModyUnitprice"].Value = dr["ref_in_price"];
                dgvr.Cells["remarks"].Value = dr["remark"];

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
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
            return RegNum.IsMatch(strValue);

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


        /// <summary>
        /// 单击要输入单元格位置变为可编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPartsMsgList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < gvPartsMsgList.Rows.Count; i++)
                {
                    if (e.ColumnIndex == gvPartsMsgList.Rows[i].Cells["ModyPricRate"].ColumnIndex)
                    {
                        gvPartsMsgList.Rows[i].Cells["ModyPricRate"].ReadOnly = false;//单击调价比率列变为可编辑状态
                    }
                    else if (e.ColumnIndex == gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].ColumnIndex)
                    {
                        gvPartsMsgList.Rows[i].Cells["ModyAfterPric"].ReadOnly = false;//单击调后单价列变为可编辑状态 
                    }   
                    else if (e.ColumnIndex == gvPartsMsgList.Rows[i].Cells["remarks"].ColumnIndex)
                    {
                        gvPartsMsgList.Rows[i].Cells["remarks"].ReadOnly = false;//单击备注列变为可编辑状态 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

    }
}
