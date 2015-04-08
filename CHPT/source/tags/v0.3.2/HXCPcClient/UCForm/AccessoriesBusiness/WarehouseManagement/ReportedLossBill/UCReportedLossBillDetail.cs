using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using ServiceStationClient.ComponentUI;
using System.Reflection;
using Utility.Common;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.ReportedLossBill
{
    public partial class UCReportedLossBillDetail : UCBase
    {
        #region 全局变量
        private string StockLossId = "stock_loss_id";//报损单主键ID
        private string LossBillID = string.Empty;//报损单ID
        private string RepLossTable = "tb_parts_stock_loss";//报损单表
        private string RepLossPartTable = "tb_parts_stock_loss_p";//报损单配件表
        private string RepLossQueryPartLogMsg = "查询报损单配件表信息";//报损单配件表操作日志
        private string RepLossQueryBillLogMsg = "查询报损单表信息";//报损单表操作日志
        private string WareHouseName = string.Empty;//仓库号


        //报损单表字段
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string OutWhourseType = "out_wh_type_name";
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
        //报损单配件表字段
        private const string PartModel = "model";
        private const string PartCount = "count";
        private const string AmountMoney = "money";
        #endregion
        public UCReportedLossBillDetail(string BillID, string WHName)
        {
            InitializeComponent();
            this.LossBillID = BillID;//获取查询的报损单主键ID
            this.WareHouseName = WHName;//获取查询的报损单仓库名称
        }


        //加载要查看的报损单配件详情
        private void UCSReportedLossDetails_Load(object sender, EventArgs e)
        {
            GetBillList();//获取报损单表头尾
            GetBillPartsList();//获取报损单配件信息
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            gvPartsMsgList.Rows.Clear();//清空原来的记录行项
            GetBillPartsList();//获取报损单配件信息
        }
        /// <summary>
        /// 获取报损单表头和表尾信息
        /// </summary>
        private void GetBillList()
        {
            try
            {

                StringBuilder sbFields = new StringBuilder();
                sbFields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", OrderNum, OrderDate,
                OrderStatus, ComName, OutWhourseType, Remark, OrgName, HandleName, OperatorName, CreateName, UpdateName, CreateTime, UpdateTime);
                DataTable BillTable = DBHelper.GetTable(RepLossQueryBillLogMsg, RepLossTable, sbFields.ToString(), StockLossId + "='" + LossBillID + "'", "", "");//获取配件信息表内容
                for (int i = 0; i < BillTable.Rows.Count; i++)
                {
                    lblorder_num.Text = BillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(BillTable.Rows[i][OrderDate]));
                    DTPickorder_date.Value = OrdDate.ToLongDateString();
                    txtorder_status_name.Caption = BillTable.Rows[i][OrderStatus].ToString();
                    txtcom_name.Caption = BillTable.Rows[i][ComName].ToString();
                    txtout_wh_type_name.Caption = BillTable.Rows[i][OutWhourseType].ToString();
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
        /// 获取选中报损单配件信息
        /// </summary>
        private void GetBillPartsList()
        {
            try
            {
                int SerialNo = 1;//序号 
                int RowCount = 0;
                DataTable PartsTable = DBHelper.GetTableByPage(RepLossQueryPartLogMsg, RepLossPartTable, "*", StockLossId + "='" + LossBillID + "'",
                "", "order by create_time desc", winFormPartPager.PageIndex, winFormPartPager.PageSize, out RowCount);//获取配件信息表内容
                winFormPartPager.RecordCount = RowCount;//分页查询所有配件记录行
                for (int i = 0; i < PartsTable.Rows.Count; i++)
                {

                    DataGridViewRow DgRow = gvPartsMsgList.Rows[gvPartsMsgList.Rows.Add()];//创建记录行项
                    DgRow.Cells["SerialNum"].Value = SerialNo;
                    DgRow.Cells["partsnum"].Value = PartsTable.Rows[i]["parts_code"].ToString();
                    DgRow.Cells["partname"].Value = PartsTable.Rows[i]["parts_name"].ToString();
                    DgRow.Cells["PartSpec"].Value = PartsTable.Rows[i]["model"].ToString();
                    DgRow.Cells["drawingnum"].Value = PartsTable.Rows[i]["drawing_num"].ToString();
                    DgRow.Cells["unitname"].Value = PartsTable.Rows[i]["unit_name"].ToString();
                    DgRow.Cells["partbrand"].Value = PartsTable.Rows[i]["parts_brand"].ToString();
                    DgRow.Cells["CarFactoryCode"].Value = PartsTable.Rows[i]["car_parts_code"].ToString();
                    DgRow.Cells["BarCode"].Value = PartsTable.Rows[i]["parts_barcode"].ToString();
                    DgRow.Cells["counts"].Value = PartsTable.Rows[i]["counts"].ToString();
                    DgRow.Cells["Unitprice"].Value = PartsTable.Rows[i]["price"].ToString();
                    DgRow.Cells["Calcmoney"].Value = PartsTable.Rows[i]["money"].ToString();
                    DgRow.Cells["remarks"].Value = PartsTable.Rows[i]["remark"].ToString();
                    SerialNo++;//序号自动增长

                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
    }
}
