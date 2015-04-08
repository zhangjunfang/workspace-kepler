using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ModifyPriceBill
{
    public partial class UCModifyPriceDetail : UCBase
    {

        #region 全局变量
        private string ModifyPriceId = "stock_modifyprice_id";//其它收货单主键ID
        private string ModPricBillID = string.Empty;//其它收货单ID
        private string ModifyPriceBillTable = "tb_parts_stock_modifyprice";//其它收货单表
        private string ModifyPricePartTable = "tb_parts_stock_modifyprice_p";//其它收货单配件表
        private string ModifyPriceQueryPartLogMsg = "查询调价单配件表信息";//其它收货单配件表操作日志
        private string ModifyPriceQueryBillLogMsg = "查询调价单表信息";//其它收货单表操作日志
        private string WareHouseName = string.Empty;//仓库号


        //调价单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string ModyPricNum = "modifyprice_doc_num";
        private const string ModyPricRate = "modifyprice_ratio";
        private const string ComName = "com_name";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string Remark = "remark";
        private const string OrderStatus = "order_status_name";
        private const string CreateName = "create_name";
        private const string UpdateName = "update_name";
        private const string CreateTime = "create_time";
        private const string UpdateTime = "update_time";
        //调价单配件表字段  
        private const string PartModPric = "modify_price";
        private const string PartModRate = "modifyprice_ratio";
        private const string PartModAfterPric = "modify_after_price";
        private const string PartModel = "model";
        private const string PartCount = "counts";
        private const string AmountMoney = "money";
        #endregion
        public UCModifyPriceDetail(string BillID, string WHName)
        {
            InitializeComponent();
            this.ModPricBillID = BillID;//获取查询的调价单主键ID
            this.WareHouseName = WHName;//获取查询的调价单仓库名称
        }


        //加载要查看的调价单配件详情
        private void UCModifyPriceDetails_Load(object sender, EventArgs e)
        {
            GetBillList();//获取调价单表头尾
            GetBillPartsList();//获取调价单配件信息
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            
            GetBillPartsList();//获取调价单配件信息
        }
        /// <summary>
        /// 获取调价单表头和表尾信息
        /// </summary>
        private void GetBillList()
        {
            try
            {
                gvPartsMsgList.Rows.Clear();//清空原来的记录行项
                StringBuilder sbFields = new StringBuilder();
                sbFields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", OrderNum, OrderDate,
                OrderStatus, ComName, ModyPricNum, ModyPricRate, Remark, OrgName, HandleName, OperatorName, CreateName, UpdateName, CreateTime, UpdateTime);
                DataTable BillTable = DBHelper.GetTable(ModifyPriceQueryPartLogMsg, ModifyPriceBillTable, sbFields.ToString(), ModifyPriceId + "='" + ModPricBillID + "'", "", "");//获取调价单表内容
                for (int i = 0; i < BillTable.Rows.Count; i++)
                {
                    lblorder_num.Text = BillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(BillTable.Rows[i][OrderDate]));
                    DTPickorder_date.Value = OrdDate.ToLongDateString();
                    txtorder_status_name.Caption = BillTable.Rows[i][OrderStatus].ToString();
                    txtcom_name.Caption = BillTable.Rows[i][ComName].ToString();
                    txtmodifydocnum.Caption = BillTable.Rows[i][ModyPricNum].ToString();//调价文号
                    txtmodifyratio.Caption = BillTable.Rows[i][ModyPricRate].ToString();//调价比率
                    txtwh_name.Caption = this.WareHouseName;//获取仓库名称
                    txtremark.Caption = BillTable.Rows[i][Remark].ToString();
                    ddlorg_id.SelectedValue = BillTable.Rows[i][OrgName].ToString();
                    ddlhandle.SelectedValue = BillTable.Rows[i][HandleName].ToString();
                    lbloperator_name.Text = BillTable.Rows[i][OperatorName].ToString();
                    lblcreate_name.Text = BillTable.Rows[i][CreateName].ToString();
                    lblupdate_name.Text = BillTable.Rows[i][UpdateName].ToString();
                    lblcreate_time.Text = BillTable.Rows[i][CreateTime].ToString();
                    lblupdate_time.Text = BillTable.Rows[i][UpdateTime].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        /// <summary>
        /// 获取选中调价单配件信息
        /// </summary>
        private void GetBillPartsList()
        {
            try
            {

                int RowCount = 0;
                DataTable PartsTable = DBHelper.GetTableByPage(ModifyPriceQueryPartLogMsg, ModifyPricePartTable, "*", ModifyPriceId + "='" + ModPricBillID + "'",
                "", "order by create_time desc", winFormPartPager.PageIndex, winFormPartPager.PageSize, out RowCount);//获取配件信息表内容
                winFormPartPager.RecordCount = RowCount;//分页查询所有配件记录行
                for (int i = 0; i < PartsTable.Rows.Count; i++)
                {

                    DataGridViewRow DgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建记录行项
                    DgRow.Cells["partsnum"].Value = PartsTable.Rows[i]["parts_code"].ToString();
                    DgRow.Cells["partname"].Value = PartsTable.Rows[i]["parts_name"].ToString();
                    DgRow.Cells["PartSpec"].Value = PartsTable.Rows[i][PartModel].ToString();
                    DgRow.Cells["drawingnum"].Value = PartsTable.Rows[i]["drawing_num"].ToString();
                    DgRow.Cells["unitname"].Value = PartsTable.Rows[i]["unit_name"].ToString();
                    DgRow.Cells["partbrand"].Value = PartsTable.Rows[i]["parts_brand"].ToString();
                    DgRow.Cells["CarFactoryCode"].Value = PartsTable.Rows[i]["car_parts_code"].ToString();
                    DgRow.Cells["BarCode"].Value = PartsTable.Rows[i]["parts_barcode"].ToString();
                    DgRow.Cells["counts"].Value = PartsTable.Rows[i][PartCount].ToString();
                    DgRow.Cells["ModifyUnitprice"].Value = PartsTable.Rows[i][PartModPric].ToString();
                    DgRow.Cells["ModRate"].Value = PartsTable.Rows[i][PartModRate].ToString();
                    DgRow.Cells["ModAfterPrice"].Value = PartsTable.Rows[i][PartModAfterPric].ToString();
                    DgRow.Cells["Calcmoney"].Value = PartsTable.Rows[i]["money"].ToString();
                    DgRow.Cells["remarks"].Value = PartsTable.Rows[i]["remark"].ToString();


                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
