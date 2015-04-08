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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill
{
    public partial class UCAllocationBillDetails : UCBase
    {
        #region
        private string StockInoutId = "stock_inout_id";//出入库单主键ID
        private string InoutBillID = string.Empty;//出入库单ID
        private string InOutBillTable = "tb_parts_stock_inout";//出入库单表
        private string InOutPartTable = "tb_parts_stock_inout_p";//出入库单配件表
        private string InOutQueryPartLogMsg = "查询出入库单配件表信息";//出入库单配件表操作日志
        private string InOutQueryBillLogMsg = "查询出入库单表信息";//出入库单表操作日志
        private string WareHouseName = string.Empty;//仓库号
        //出入库单表字段名
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string OrderStatusName = "order_status_name";
        private const string OrderTypeName = "order_type_Name";
        private const string BillingTypeName = "billing_type_Name";
        private const string BillRemark = "remark";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string CreateName = "create_name";
        private const string UpdateName = "update_name";
        private const string CreateTime = "create_time";
        private const string UpdateTime = "update_time";
        #endregion
        public UCAllocationBillDetails(string BillID, string WHName)
        {
            InitializeComponent();
            this.InoutBillID = BillID;//获取查询的出入库单主键ID
            this.WareHouseName = WHName;//获取查询的出入库单仓库名称
            base.Enabled = false;
        }
        //加载要查看的出入库配件详情
        private void UCAllocationBillDetails_Load(object sender, EventArgs e)
        {
            GetBillList();//获取出入库单表头尾
            GetBillPartsList();//获取出入库单配件信息
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            gvBillPartsList.Rows.Clear();//清空原来的记录行项
            GetBillPartsList();//获取出入库单配件信息
        }
        /// <summary>
        /// 获取出入库单表头和表尾信息
        /// </summary>
        private void GetBillList()
        {
            try
            {

                StringBuilder sbFields = new StringBuilder();
                sbFields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", OrderNum, OrderDate,
                OrderStatusName, OrderTypeName, BillingTypeName, BillRemark, OrgName, HandleName, OperatorName, CreateName, UpdateName, CreateTime, UpdateTime);
                DataTable BillTable = DBHelper.GetTable(InOutQueryBillLogMsg, InOutBillTable, sbFields.ToString(), StockInoutId + "='" + InoutBillID + "'", "", "");//获取配件信息表内容
                for (int i = 0; i < BillTable.Rows.Count; i++)
                {
                    txtAllocationBill_No.Caption = BillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(BillTable.Rows[i][OrderDate]));
                    DTPickorder_date.Value = OrdDate.ToLongDateString();
                    txtAllocBill_State.Caption = BillTable.Rows[i][OrderStatusName].ToString();
                    TxtAllocBill_Type.Caption = BillTable.Rows[i][OrderTypeName].ToString();
                    TxtAllocBilling_Type.Caption = BillTable.Rows[i][BillingTypeName].ToString();
                    TxtAllocBill_WareHouse.Caption = this.WareHouseName;//获取仓库名称
                    txtAllocBill_Comment.Caption = BillTable.Rows[i][BillRemark].ToString();
                    ddlorg_id.Text = BillTable.Rows[i][OrgName].ToString();
                    ddlhandle.Text = BillTable.Rows[i][HandleName].ToString();
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
        /// 获取选中出入库单配件信息
        /// </summary>
        private void GetBillPartsList()
        {
            try
            {
                bool Gift = true;//赠送
                bool NoGift = false;//非赠送
                int SerialNo = 1;//序号 
                int RowCount = 0;
                DataTable PartsTable = DBHelper.GetTableByPage(InOutQueryPartLogMsg, InOutPartTable, "distinct *", StockInoutId + "='" + InoutBillID + "'",
                "", "order by make_date desc", winFormPartPager.PageIndex, winFormPartPager.PageSize, out RowCount);//获取配件信息表内容
                winFormPartPager.RecordCount = RowCount;//分页查询所有配件记录行
                for (int i = 0; i < PartsTable.Rows.Count; i++)
                {

                    DataGridViewRow DgRow = gvBillPartsList.Rows[gvBillPartsList.Rows.Add()];//创建记录行项
                    DgRow.Cells["SerialNum"].Value = SerialNo;
                    DgRow.Cells["partscode"].Value = PartsTable.Rows[i]["parts_code"].ToString();
                    DgRow.Cells["partsname"].Value = PartsTable.Rows[i]["parts_name"].ToString();
                    DgRow.Cells["drawingnum"].Value = PartsTable.Rows[i]["drawing_num"].ToString();
                    DgRow.Cells["unit"].Value = PartsTable.Rows[i]["unit_name"].ToString();
                    DgRow.Cells["partsbrandname"].Value = PartsTable.Rows[i]["parts_brand"].ToString();
                    DgRow.Cells["businesscounts"].Value = PartsTable.Rows[i]["counts"].ToString();
                    DgRow.Cells["ProductionDate"].Value = PartsTable.Rows[i]["make_date"].ToString();
                    DgRow.Cells["ValidityDate"].Value = PartsTable.Rows[i]["validity_date"].ToString();
                    if (PartsTable.Rows[i]["parts_code"].ToString() == "1")
                    {
                        ((DataGridViewCheckBoxCell)DgRow.Cells["is_gift"]).Value = Gift;
                    }
                    else if (PartsTable.Rows[i]["parts_code"].ToString() == "0")
                    {
                        ((DataGridViewCheckBoxCell)DgRow.Cells["is_gift"]).Value = NoGift;
                    }
                    DgRow.Cells["Remark"].Value = PartsTable.Rows[i]["remark"].ToString();
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
