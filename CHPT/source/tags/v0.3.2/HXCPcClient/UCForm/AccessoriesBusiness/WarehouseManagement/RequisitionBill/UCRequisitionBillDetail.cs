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
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.RequisitionBill
{
    public partial class UCRequisitionBillDetail : UCBase
    {

        #region
        private string StockAllotId = "stock_allot_id";//出入库单主键ID
        private string AllotBillID = string.Empty;//出入库单ID
        private string AllotBillTable = "tb_parts_stock_allot";//调拨单表
        private string AllotPartTable = "tb_parts_stock_allot_p";//调拨单配件表
        private string AllotQueryPartLogMsg = "查询调拨单配件表信息";//调拨单配件表操作日志
        private string AllotQueryBillLogMsg = "查询调拨单表信息";//调拨单表操作日志

        //调拨单表字段
        private const string OrderTypeName = "order_type_name";
        private const string OrderNum = "order_num";
        private const string OrderDate = "order_date";
        private const string CallOutOrg = "call_out_org_name";
        private const string CallOutWhouse = "call_out_wh_name";
        private const string CallInOrg = "call_in_org_name";
        private const string CallInWhouse = "call_in_wh_name";
        private const string TranWay = "trans_way_name";
        private const string DeliveryAddr = "delivery_address";
        private const string OrgName = "org_name";
        private const string HandleName = "handle_name";
        private const string OperatorName = "operator_name";
        private const string BillRemark = "remark";
        private const string OrderStatus = "order_status_name";
        private const string CreateName = "create_name";
        private const string UpdateName = "update_name";
        private const string CreateTime = "create_time";
        private const string UpdateTime = "update_time";
        #endregion

        public UCRequisitionBillDetail(string BillID)
        {
            InitializeComponent();
            this.AllotBillID = BillID;//获取查询的出入库单主键ID
            base.Enabled = false;
        }
        /// <summary>
        /// 窗体初始加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCRequisitionBillDetail_Load(object sender, EventArgs e)
        {
            GetBillList();//获取调拨单表头尾
            GetBillPartsList();//获取调拨单配件信息
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPartPager_PageIndexChanged(object sender, EventArgs e)
        {
            gvBillPartsList.Rows.Clear();//清空原来的记录行项
            GetBillPartsList();//获取调拨单配件信息
        }
        /// <summary>
        /// 获取调拨单表头和表尾信息
        /// </summary>
        private void GetBillList()
        {
            try
            {

                StringBuilder sbFields = new StringBuilder();
                sbFields.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", OrderNum, OrderDate,
                 OrderTypeName, CallOutOrg, CallOutWhouse, CallInOrg, CallInWhouse, TranWay, DeliveryAddr, OrderStatus, OrgName,
                 HandleName, OperatorName, CreateName, UpdateName, CreateTime, UpdateTime);
                DataTable BillTable = DBHelper.GetTable(AllotQueryBillLogMsg, AllotBillTable, sbFields.ToString(), StockAllotId + "='" + AllotBillID + "'", "", "");//获取调拨单信息表内容
                for (int i = 0; i < BillTable.Rows.Count; i++)
                {
                    txtAllotBill_No.Caption = BillTable.Rows[i][OrderNum].ToString();
                    DateTime OrdDate = Common.UtcLongToLocalDateTime(Convert.ToInt64(BillTable.Rows[i][OrderDate]));
                    DTPickorder_date.Value = OrdDate.ToLongDateString();
                    txtAllotBill_State.Caption = BillTable.Rows[i][OrderStatus].ToString();
                    txtAllotBill_Type.Caption = BillTable.Rows[i][OrderTypeName].ToString();
                    TxtAllotTranWay.Caption = BillTable.Rows[i][TranWay].ToString();
                    txtAllotdelivery_address.Caption = BillTable.Rows[i][DeliveryAddr].ToString();
                    TxtCallOutOrg.Caption = BillTable.Rows[i][CallOutOrg].ToString();
                    TxtCallOutWhouse.Caption = BillTable.Rows[i][CallOutWhouse].ToString();
                    TxtCallInOrg.Caption = BillTable.Rows[i][CallInOrg].ToString();
                    TxtCallInWhouse.Caption = BillTable.Rows[i][CallInWhouse].ToString();
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
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 获取选中调拨单配件信息
        /// </summary>
        private void GetBillPartsList()
        {
            try
            {
                int SerialNo = 1;//序号 
                int RowCount = 0;//查询的记录行
                DataTable PartsTable = DBHelper.GetTableByPage(AllotQueryPartLogMsg, AllotPartTable, "*", StockAllotId + "='" + AllotBillID + "'",
                "", " order by make_date desc", winFormPartPager.PageIndex, winFormPartPager.PageSize, out RowCount);//获取配件信息表内容
                winFormPartPager.RecordCount = RowCount;//分页查询所有配件记录行
                for (int i = 0; i < PartsTable.Rows.Count; i++)
                {
                    
                        DataGridViewRow DgRow = gvBillPartsList.Rows[gvBillPartsList.Rows.Add()];
                        DgRow.Cells["SerialNum"].Value = SerialNo;
                        DgRow.Cells["partscode"].Value = PartsTable.Rows[i]["parts_code"].ToString();
                        DgRow.Cells["partsname"].Value = PartsTable.Rows[i]["parts_name"].ToString();
                        DgRow.Cells["Specification"].Value = PartsTable.Rows[i]["model"].ToString();
                        DgRow.Cells["drawingnum"].Value = PartsTable.Rows[i]["drawing_num"].ToString();
                        DgRow.Cells["unit"].Value = PartsTable.Rows[i]["unit_name"].ToString();
                        DgRow.Cells["partsbrand"].Value = PartsTable.Rows[i]["parts_brand"].ToString();
                        DgRow.Cells["carpartscode"].Value = PartsTable.Rows[i]["car_parts_code"].ToString();
                        DgRow.Cells["partbarcode"].Value = PartsTable.Rows[i]["parts_barcode"].ToString();
                        DgRow.Cells["businesscounts"].Value = PartsTable.Rows[i]["counts"].ToString();
                        DgRow.Cells["CallInPrice"].Value = PartsTable.Rows[i]["call_in_price"].ToString();
                        DgRow.Cells["TotalMoney"].Value = PartsTable.Rows[i]["money"].ToString();
                        DgRow.Cells["businesscounts"].Value = PartsTable.Rows[i]["counts"].ToString();
                        DgRow.Cells["ProductionDate"].Value = PartsTable.Rows[i]["make_date"].ToString();
                        DgRow.Cells["ValidityDate"].Value = PartsTable.Rows[i]["validity_date"].ToString();
                        DgRow.Cells["Remark"].Value = PartsTable.Rows[i]["remark"].ToString();
                        SerialNo++;//序号自动增长
                    
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }





    }
}
