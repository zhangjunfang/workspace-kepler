using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm;
using System.Collections;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill
{
    public partial class UCImportSaleReturnBilling : FormChooser
    {
        
        #region 变量、类
        private string WareHosueName = string.Empty;//仓库名称
        private string SaleBack = DataSources.GetDescription(DataSources.EnumSaleOrderType.SaleBack,true);//销售退货
        private const string SaleBilling = "销售开单";
        private string SaleBillingLogMsg = "查询销售开单列表信息";
        private string SalepartsLogMsg = "查询销售开单配件表信息";
        private string DefaultValue = "请选择";
        private const string OpenState ="0";//单据开放状态
        private const string OccupyState = "1";//单据占用状态
        private const string LockState ="2";//单据锁定状态
        private const string NODelState = "1";//未删除标志
        private const string DelState = "0";//删除标志
        //销售开单数据库表
        private string SaleBillTable = "tb_parts_sale_billing";
        //销售开单配件数据库表
        private string SalePartTable = "tb_parts_sale_billing_p";

        //开单主键ID
        private const string salebillid = "sale_billing_id";
        //开单列表数据字段
        private const string ordertypename = "order_type_name";
        private const string ordernum = "order_num";
        private const string orderdate = "order_date";
        private const string balancemoney = "balance_money";
        private const string orgname = "org_name";
        private const string handlename = "handle_name";
        private const string operatorname = "operator_name";
        //配件列表数据字段
        private const string partscode = "parts_code";
        private const string partsname = "parts_name";
        private const string drawingnum = "drawing_num";
        private const string unitname = "unit_name";
        private const string originalprice = "original_price";
        private const string businessprice = "business_price";
        private const string returncounts = "business_counts";//退货数量
        private const string valoremtogether = "valorem_together";
        private const string arrivaldate = "arrival_date";
        private const string carpartscode = "car_parts_code";
        public int dgPlanRowIndex = -1;//GridView行索引初始值
        public List<string> StockBillIDLst = new List<string>();//存放库存开单主键ID与对应的开单编号
        public Hashtable PartHTable = new Hashtable();//存放开单主键ID与对应的配件编号

        #endregion

        #region 窗体初始化
        public UCImportSaleReturnBilling(string WhName)
        {
            InitializeComponent();
            this.WareHosueName = WhName;//获取出入库单据类型
            dateTimeStart.Value = DateTime.Now.ToShortDateString();//开始时间
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();//结束时间
            CommonFuncCall.BindUnit(unit);//从码表中获取单位名称


            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, DefaultValue);//部门名称
            CommonFuncCall.BindHandle(ddlhandle, "", DefaultValue);//经手人
            CommonFuncCall.BindHandle(ddloperator, "", DefaultValue);//操作人

        }
        #endregion


        #region 查询条件的控件事件
        /// <summary> 
        /// 选择不同部门显示对应部门的经办人和操作人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), DefaultValue);//经办人
            CommonFuncCall.BindHandle(ddloperator, ddlorg_id.SelectedValue.ToString(), DefaultValue);//操作人
        }

        /// <summary> 
        /// 选择配件编码 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_code.Text = chooseParts.PartsCode;

            }
        }
        /// <summary>
        /// 选择配件名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_name_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_name.Text = chooseParts.PartsName;
            }
        }
        /// <summary> 
        /// 选择配件类型 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type_ChooserClick(object sender, EventArgs e)
        {
            //显示配件选择窗口
            frmPartsType choosePartsType = new frmPartsType();
            choosePartsType.ShowDialog();
            if (!string.IsNullOrEmpty(choosePartsType.TypeID))
            {
                txtparts_type.Text = choosePartsType.TypeName;
            }
        }

        /// <summary> 
        /// 查询要导入的单据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                BinddgBilling(SaleBillTable, salebillid, SaleBillingLogMsg);//根据条件查询销售开单信息列表
      

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }

        /// <summary>
        /// 清空所有查询条件控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now.ToShortDateString();
            dateTimeEnd.Value = DateTime.Now.ToShortDateString();
            txtorder_num.Caption = string.Empty;
            ddlorg_id.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
            ddloperator.SelectedIndex = 0;
            txtparts_code.Tag = string.Empty;
            txtparts_code.Text = string.Empty;

            txtparts_name.Tag = string.Empty;
            txtparts_name.Text = string.Empty;

            txtparts_type.Tag = string.Empty;
            txtparts_type.Text = string.Empty;
        }
        /// <summary>
        /// 全选记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            bool Choice = true;
            IsDataGridViewCheckBox(Choice);
        }
        /// <summary>
        /// 反选记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotCheck_Click(object sender, EventArgs e)
        {
            bool Choice_Cancel = false;
            IsDataGridViewCheckBox(Choice_Cancel);
        }

        /// <summary>
        /// 确定导入配件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {


                //判断获取开单列表中是否有未选中项，如果存在并移除
                if (StockBillIDLst.Count > 0)
                {
                    for (int i = 0; i < dgBillList.Rows.Count; i++)
                    {
                        //遍历开单列表中未选择记录行
                        string OrderNumID = dgBillList.Rows[i].Cells["StockBillingId"].Value.ToString();
                        object IsCheck = dgBillList.Rows[i].Cells["colCheck"].EditedFormattedValue;
                        if (IsCheck != null && !(bool)IsCheck)
                        {
                            for (int j = 0; j < StockBillIDLst.Count; j++)
                            {
                                //遍历并移除未选择的开单主键ID和开单编号
                                if (StockBillIDLst[j].ToString() == OrderNumID) StockBillIDLst.Remove(StockBillIDLst[j].ToString());

                            }
                        }
                    }

                }
                else
                {
                    MessageBoxEx.Show("没有选择要导入的销售退货单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //判断获取配件明细列表中是否有选中项，如果存在并移除
                if (dgPartslist.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgPartslist.Rows)
                    {
                        object isCheck = dr.Cells["colDetailCheck"].EditedFormattedValue;
                        if (isCheck != null && !(bool)isCheck)
                        {
                            foreach (DictionaryEntry DicEty in PartHTable)
                            {
                                if (DicEty.Key.ToString() == dr.Cells["parts_code"].Value.ToString())
                                {   //遍历移除未选择的配件记录行开单ID和对应配件编码
                                    PartHTable.Remove(DicEty.Key.ToString());
                                }

                            }
                        }
                    }

                    //判断配件列表中是否全选
                    if (PartHTable.Count == dgPartslist.Rows.Count)
                    {
                        PartHTable.Clear();//清除配件信息列表
                    }

                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            finally
            { dgPlanRowIndex = -1; }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary> 
        /// 点击开单列表中记录行显示详细配件详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBillList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击表头或列头时不起作用
            if (e.RowIndex > -1 && e.ColumnIndex > -1)  
            {
                dgPlanRowIndex = e.RowIndex;
                string BillingNumID = dgBillList.CurrentRow.Cells["StockBillingId"].Value.ToString();//获取点击行开单主键ID
                string OrderType=dgBillList.CurrentRow.Cells["order_type_name"].Value.ToString();//获取单据类型名称
                BinddgPartsList(BillingNumID, WareHosueName, OrderType);//查询对应开单中的配件信息表               

                              
            }
        }
        /// <summary> 
        /// 通过开单记录行的选择状态设置配件记录行的选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBillList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             //双击表头或列头时不起作用  
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                bool ischeck = (bool)((DataGridViewCheckBoxCell)dgBillList.Rows[dgPlanRowIndex].Cells["colCheck"]).EditedFormattedValue;//获取当前开单记录选择状态        
                dgPartslist.EndEdit();
                if (dgPartslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        //设置当前配件信息记录行选择状态
                        ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colCheck"]).Value = ischeck;
                    }
                }
            }
        }
        /// <summary> 
        /// 格式化内容信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBillList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null)
                {
                    string ColumnName = dgBillList.Columns[e.ColumnIndex].Name.ToString(); //单据名称格式化
                    if (ColumnName.Equals("order_name"))
                    {
                        e.Value = SaleBilling;
                    }
                    return;
                }
                string fieldNmae = dgBillList.Columns[e.ColumnIndex].DataPropertyName.ToString();// 开单日期格式化
                if (fieldNmae.Equals("order_date"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 通过配件信息记录行选择状态设置开单记录选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPartslist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    bool isChkis_Suspend = true;
                    //判断配件信息列表记录行选择状态
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        object ischeck = dgPartslist.Rows[i].Cells["colDetailCheck"].EditedFormattedValue;
                        if (ischeck != null && !(bool)ischeck)
                        {
                            isChkis_Suspend = false;
                            break;
                        }
                    }
                    ((DataGridViewCheckBoxCell)dgBillList.Rows[dgPlanRowIndex].Cells["colCheck"]).Value = isChkis_Suspend;//设置开单信息记录行选择状态
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        private string BuildQueryWhere()
        {
            try
            {
                //销售开单可以导入的前提是：单据状态是已退货、导入状态是正常的 三种情况
                //获取出入库单与开单类型信息
                string StrWhere = " enable_flag='" + NODelState + "'" + " and is_occupy='" + OpenState + "'";//查询条件字符串
                 //销售退货开单
                StrWhere += " and order_type_name ='" + SaleBack + "'"; 

                //获取导入单据的查询条件信息  
                 if (!string.IsNullOrEmpty(dateTimeStart.Value.Trim().ToString()))
                {
                    StrWhere += " and order_date>='" + dateTimeStart.Value.Trim().ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(dateTimeEnd.Value.Trim().ToString()))
                {
                    StrWhere += " and order_date<='" + dateTimeEnd.Value.Trim().ToString() + "'";
                }
                  if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim().ToString()))
                {
                    StrWhere += " and order_num='" + txtorder_num.Caption.Trim().ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
                {
                    StrWhere += " and handle_name='" + ddlhandle.SelectedValue.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(ddloperator.SelectedValue.ToString()))
                {
                    StrWhere += "and operator_name='" + ddloperator.SelectedValue.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_code.Text.Trim().ToString()))
                {
                    StrWhere += "and operator_name='" + ddloperator.SelectedValue.ToString() + "'";
                }
                 
                return StrWhere;
           
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return ex.Message;
            }
            
        }
          /// <summary>
          ///  绑定开单列表
          /// </summary>
          /// <param name="BillTable">开单表名称</param>
          /// <param name="BillPrimID">开单主键ID</param>
          /// <param name="BillLogMsg">查询日志</param>
        private void BinddgBilling(string BillTable,string BillPrimID,string BillLogMsg)
        {
            try
            {

                DataTable gvBill_dt = null;
                dgBillList.Rows.Clear();//清空GridView中开单信息
                StockBillIDLst.Clear();//清除Hashtable开单信息
                //设置开单信息表中的数据查询字段
                StringBuilder sb_Bill=new StringBuilder();
                sb_Bill.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", BillPrimID,
                ordertypename, ordernum, orderdate, balancemoney, orgname, handlename, operatorname);

                 //绑定开单信息列表
                gvBill_dt = DBHelper.GetTable(BillLogMsg, BillTable, sb_Bill.ToString(), BuildQueryWhere(), "", "");
                GetBillLists(gvBill_dt);//获取开单信息列表

                //将开单主键ID与开单编号存放入list
                for (int i = 0; i < gvBill_dt.Rows.Count; i++)
                {
                    StockBillIDLst.Add(gvBill_dt.Rows[i][BillPrimID].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 查询开单信息列表
        /// </summary>
        /// <param name="BillDataTable"></param>
        private void GetBillLists(DataTable BillDataTable)
        { 
           try
           {
                int BillingID=1;
                for(int i=0;i<BillDataTable.Rows.Count;i++)
                {
                        DataGridViewRow dgRow = dgBillList.Rows[dgBillList.Rows.Add()];
                        dgRow.Cells["BillID"].Value = BillingID;//序号
                        dgRow.Cells["OrderName"].Value = SaleBilling;//销售开单
                        dgRow.Cells["order_type_name"].Value = BillDataTable.Rows[i]["order_type_name"].ToString();
                        dgRow.Cells["order_num"].Value = BillDataTable.Rows[i]["order_num"].ToString();
                        dgRow.Cells["order_date"].Value = BillDataTable.Rows[i]["order_date"].ToString();
                        dgRow.Cells["balance_money"].Value = BillDataTable.Rows[i]["balance_money"].ToString();
                        dgRow.Cells["org_name"].Value = BillDataTable.Rows[i]["org_name"].ToString();
                        dgRow.Cells["handle_name"].Value = BillDataTable.Rows[i]["handle_name"].ToString();
                        dgRow.Cells["operator_name"].Value = BillDataTable.Rows[i]["operator_name"].ToString();
                        BillingID++;//序号自动增长
                        
                    
                }
           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
           }
        }

        /// <summary>
        /// 设置配件信息记录行选择状态
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsCheckdgPartsList(bool ischeck)
        {
            dgPartslist.EndEdit();
            if (dgPartslist.Rows.Count > 0)
            {
              for (int i = 0; i < dgPartslist.Rows.Count; i++)
              {
                  //设置当前配件信息记录行选择状态
                  ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colCheck"]).Value = ischeck;
              }
            }
        }
        /// <summary>
        /// 开单与配件列表级联选择操作
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsDataGridViewCheckBox(bool ischeck)
        {
            try
            {
                dgBillList.EndEdit();//单据列表
                dgPartslist.EndEdit();//配件列表
                if (dgBillList.Rows.Count > 0)
                {
                    for (int i = 0; i < dgBillList.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)dgBillList.Rows[i].Cells["colCheck"]).Value = ischeck;//开单列表选择状态
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可以选择的销售退货单记录行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (dgPartslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dgPartslist.Rows.Count; i++)
                    {
                        ((DataGridViewCheckBoxCell)dgPartslist.Rows[i].Cells["colDetailCheck"]).Value = ischeck;//配件列表选择状态
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可以选择的配件记录行项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
     /// <summary>
     /// 绑定开单配件列表
     /// </summary>
     /// <param name="BillingId">开单编号</param>
     /// <param name="BType">出入库单类型</param>
     /// <param name="SubOrderType">开单中的子单据类型名称</param>
        private void BinddgPartsList(string BillingId,string BType,string SubOrderType)
        {
            try
            {
                
                //设置配件列表数据字段
                StringBuilder sb_Part = new StringBuilder();
                sb_Part.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                salebillid, partscode, partsname, drawingnum, unitname, originalprice,
                businessprice, returncounts, valoremtogether, arrivaldate, carpartscode);

                DataTable dt_parts_Billing = null;//配件信息数据表
                dgPartslist.Rows.Clear();//清空所有配件信息
                PartHTable.Clear();//开单主键ID与配件编码

                //获取销售退货入库配件信息列表
                dt_parts_Billing = DBHelper.GetTable(SalepartsLogMsg, SalePartTable, sb_Part.ToString(), salebillid + "='" + BillingId + "'", "", "");

                if (dt_parts_Billing != null && dt_parts_Billing.Rows.Count > 0)
                {
                    GetPartlists(dt_parts_Billing);//把配件信息表绑定在GridView列表中

                    //把最后点击查询的开单配件信息主键ID与对应的配件编号放入Hashtable
                    foreach (DataRow DRow in dt_parts_Billing.Rows)
                    {
                        PartHTable.Add(DRow["parts_code"].ToString(), DRow["sale_billing_id"].ToString());
                    }
                }
               

            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 批量导入销售退货配件列表信息
        /// </summary>
        /// <param name="partsTable">数据库中获取的配件信息表</param>
        private void GetPartlists(DataTable partsTable)
        {
            try
            {
                int id = 1;
                for (int i = 0; i < partsTable.Rows.Count; i++)
                {
                    
                        DataGridViewRow dgRow = dgPartslist.Rows[dgPartslist.Rows.Add()];
                        dgRow.Cells["ID"].Value = id;//序号
                        dgRow.Cells["parts_code"].Value = partsTable.Rows[i]["parts_code"].ToString();
                        dgRow.Cells["parts_name"].Value = partsTable.Rows[i]["parts_name"].ToString();
                        dgRow.Cells["drawing_num"].Value = partsTable.Rows[i]["drawing_num"].ToString();
                        dgRow.Cells["OrigPrice"].Value = partsTable.Rows[i]["original_price"].ToString();//原始单价
                        dgRow.Cells["BusPrice"].Value = partsTable.Rows[i]["business_price"].ToString();//含税单价
                        dgRow.Cells["BusCounts"].Value = partsTable.Rows[i]["return_count"].ToString();//退货数量
                        dgRow.Cells["ValTogether"].Value = partsTable.Rows[i]["valorem_together"].ToString();//价税合计
                        //dgRow.Cells["ReferCount"].Value = partsTable.Rows[i]["reference_count"].ToString();//引用数量
                        //dgRow.Cells["RemCount"].Value = partsTable.Rows[i]["remain_count"].ToString();//剩余数量
                        dgRow.Cells["ArrivDate"].Value = partsTable.Rows[i]["arrival_date"].ToString();
                        dgRow.Cells["CarPartCode"].Value = partsTable.Rows[i]["car_parts_code"].ToString();
                        id++;//序号自动增加

                   
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        
        #endregion
    }
}
