using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.AllocationBill;
using SYSModel;
using HXCPcClient.Chooser.CommonForm;
namespace HXCPcClient.UCForm.AccessoriesBusiness.WarehouseManagement.StockQuery
{
    public partial class UCStockQuery : UCBase
    {


        #region 全局变量
        //各开单配件表
        private string InOutBillTable = "tb_parts_stock_inout";//出入库单表
        private string InOutBillID = "stock_inout_id";//出入库单表主键ID
        private string InOutPartTable = "tb_parts_stock_inout_p";//出入库单配件表
        private string PurBillingTable = "tb_parts_purchase_billing";//采购开单开表
        private string PurBillingID = "purchase_billing_id";//采购开单主键ID
        private string DsnBillNum = "ration_send_code";//宇通配送单号
        private string PurBillingPartTable = "tb_parts_purchase_billing_p";//采购开单开配件表
        private string PurBillingPartCount = "business_counts";//采购开单配件数量
        private string SaleBillingPartTable = "tb_parts_sale_billing_p";//销售开单配件表
        private string SaleBillingPartCount = "business_count";//销售开单配件数量
        private string FetchPartTable = "tb_maintain_fetch_material_detai";//领料单配件表
        private string FetchCount = "received_num";//已领数量
        private string RefundPartTable = "tb_maintain_refund_material_detai";//领料退货单配件表
        private string RefundCount = "retreat_num";//应退数量
        private string ThreeWarFlag = "three_warranty";//是否三包标志
        private string CheckPartTable = "tb_parts_stock_check_p";//盘点单配件信息表
        private string CheckCount = "paper_count";//盘点单账面数量
        private string ReceiptPartTable = "tb_parts_stock_receipt_p";//其它收货单配件信息表
        private string ShipPartTable = "tb_parts_stock_shipping_p";//其它发货单配件信息表
        private string LossPartTable = "tb_parts_stock_loss_p";//报损单配件表
        private string YuTongThreeGuarantyTable = "tb_maintain_three_guaranty";//宇通三包服务单
        private string YuTonngApproveStatus = "approve_status_yt";//宇通审批状态
        private string YuTongThreeGuarantyPartTable = "tb_maintain_three_guaranty_material_detail";//宇通三包维修用料表
        private string YuTongPartCount = "quantity";//配件数量
        private string VehModelTable = "tb_vehicle_models";//车型档案表
        private string PartsTable = "tb_parts";//配件档案表
        private string PartsPriceTable = "tb_parts_price";//配件价格信息表
        private string PartID = "parts_id";//配件ID
        private string PartType = "parts_type";//配件类别
        private string PartBarCode = "parts_barcode";//配件条码
        private string SePartsCode = "ser_parts_code";//配件表配件编码
        private string Wgt = "weight";//重量
        private string PlaceOrigin = "place_origin";//配件产地
        private string VehicleTypeName = "vm_name";//车型
        private string PartEnableFlag = "status";//配件启停用标志
        private string RefInPrice = "ref_in_price";//参考进价
        private string RefOutPrice = "ref_out_price";//参考售价
        private string HInPrice = "highest_in_price";//最高进价
        private string LOutPrice = "low_out_price";//最低售价
        private string FInPric = "into_price_one";//一级进价
        private string SInPric = "into_price_two";//二级进价
        private string TInPric = "into_price_three";//三级进价
        private string FOutPric = "out_price_one";//一级进价
        private string SOutPric = "out_price_two";//二级进价
        private string TOutPric = "out_price_three";//三级进价
        private string VehicleTypeId = "vm_id";//车型ID
        private string YutongPartStock = "宇通配件库";
        private string PartForVhcTable = "tb_parts_for_vehicle";//配件适用车型表
        private string StockQueryLogMsg = "查询库存单据配件表信息";//库存单配件表操作日志
        private string YuTongStockQueryLogMsg = "查询宇通三包库存单据配件表信息";//库存单配件表操作日志
        private string Submited = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//已提交
        private string Verify = DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true);//已审核


        //出入库单配件表字段
        private const string PartsNum = "parts_code";
        private const string PartsName = "parts_name";
        private const string CarFactoryCode = "car_parts_code";//车厂配件编码
        private const string DrawNum = "drawing_num";//图号
        private const string PartSpec = "model";//规格
        private const string UnitName = "unit_name";//单位
        private const string WareHouse= "wh_name";//仓库名称
        private const string PartCount = "counts";//配件数量
        private const string VmName = "vm_name";//车型名称
        private const string PartsBrand = "parts_brand";//品牌
        private const string PartRemk = "remark";
        private const string ExportXlsName = "库存统计查询";
        //配件主数据表
        private const string PartStatus = "status";//配件启停用标志

        #endregion

        public UCStockQuery()
        {
            InitializeComponent();
            CommonFuncCall.BindWarehouse(CombWarehouse, "请选择"); //获取仓库名称
            CommonFuncCall.BindCompany(CombCompany, "全部");//选择公司名称
            base.ExportEvent += new ClickHandler(UCStockQuery_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnPartSearch, btnPartClear);//美化查询和清除按钮控件
            //DataGridViewEx.SetDataGridViewStyle(gvStockList, ApplyFlag);//美化表格控件
        }

       private  void UCStockQuery_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                BtnExportMenu.Show(btnExport, 0, btnExport.Height);//指定导出菜单显示位置
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

      
        /// <summary>
        /// 导出Excel文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvStockList.Rows.Count == 0) //判断gridview中是否有数据记录
                {
                    MessageBoxEx.Show("您要导出的单据列表不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable XlsTable = new DataTable();//导出的数据表格
                    //创建表列项
                    XlsTable.Columns.Add("配件编码", typeof(string));
                    XlsTable.Columns.Add("配件名称", typeof(string));
                    XlsTable.Columns.Add("图号", typeof(string));
                    XlsTable.Columns.Add("规格", typeof(string));
                    XlsTable.Columns.Add("配件类别", typeof(string));
                    XlsTable.Columns.Add("账面库存", typeof(string));
                    XlsTable.Columns.Add("实际库存", typeof(string));
                    XlsTable.Columns.Add("占用库存", typeof(string));
                    XlsTable.Columns.Add("可用库存", typeof(string));
                    XlsTable.Columns.Add("参考进价", typeof(string));
                    XlsTable.Columns.Add("条码", typeof(string));
                    XlsTable.Columns.Add("重量", typeof(string));
                    XlsTable.Columns.Add("最高进价", typeof(string));
                    XlsTable.Columns.Add("最低售价", typeof(string));
                    XlsTable.Columns.Add("会员价", typeof(string));
                    XlsTable.Columns.Add("一级进价", typeof(string));
                    XlsTable.Columns.Add("二级进价", typeof(string));
                    XlsTable.Columns.Add("三级进价", typeof(string));
                    XlsTable.Columns.Add("一级销价", typeof(string));
                    XlsTable.Columns.Add("二级销价", typeof(string));
                    XlsTable.Columns.Add("三级销价", typeof(string));
                    XlsTable.Columns.Add("车型", typeof(string));
                    XlsTable.Columns.Add("产地", typeof(string));
                    XlsTable.Columns.Add("品牌", typeof(string));
                    XlsTable.Columns.Add("车厂编码", typeof(string));
                    XlsTable.Columns.Add("备注", typeof(string));
                    XlsTable.Columns.Add("启停用标志", typeof(string));
                    foreach (DataGridViewRow dgRow in gvStockList.Rows)
                    {
                        DataRow TableRow = XlsTable.NewRow();//创建表行项

                        TableRow["配件编码"] = dgRow.Cells["PartNum"].Value.ToString();
                        TableRow["配件名称"] = dgRow.Cells["PartName"].Value.ToString();
                        TableRow["图号"] = dgRow.Cells["DrawingNum"].Value.ToString();
                        TableRow["规格"] = dgRow.Cells["PartModel"].Value.ToString();
                        TableRow["配件类别"] = dgRow.Cells["PartCategory"].Value.ToString();
                        TableRow["账面库存"] = dgRow.Cells["PapCount"].Value.ToString();
                        TableRow["实际库存"] = dgRow.Cells["PhysicalCount"].Value.ToString();
                        TableRow["占用库存"] = dgRow.Cells["OccupyCount"].Value.ToString();
                        TableRow["可用库存"] = dgRow.Cells["AvailableCount"].Value.ToString();
                        TableRow["参考进价"] = dgRow.Cells["ReferInPrice"].Value.ToString();
                        TableRow["条码"] = dgRow.Cells["BarCode"].Value.ToString();
                        TableRow["重量"] = dgRow.Cells["Weight"].Value.ToString();
                        TableRow["最高进价"] = dgRow.Cells["HighestInPrice"].Value.ToString();
                        TableRow["最低售价"] = dgRow.Cells["LowestOutPrice"].Value.ToString();
                        TableRow["会员价"] = dgRow.Cells["VIPPrice"].Value.ToString();
                        TableRow["一级进价"] = dgRow.Cells["FirstInPrice"].Value.ToString();
                        TableRow["二级进价"] = dgRow.Cells["SecondInPrice"].Value.ToString();
                        TableRow["三级进价"] = dgRow.Cells["ThirdInPrice"].Value.ToString();
                        TableRow["一级销价"] = dgRow.Cells["FirstOutPrice"].Value.ToString();
                        TableRow["二级销价"] = dgRow.Cells["SecondOutPrice"].Value.ToString();
                        TableRow["三级销价"] = dgRow.Cells["ThirdOutPrice"].Value.ToString();
                        TableRow["车型"] = dgRow.Cells["VehicleType"].Value.ToString();
                        TableRow["产地"] = dgRow.Cells["ProductPlace"].Value.ToString();
                        TableRow["品牌"] = dgRow.Cells["PartBrand"].Value.ToString();
                        TableRow["车厂编码"] = dgRow.Cells["FactoryCode"].Value.ToString();
                        TableRow["备注"] = dgRow.Cells["PartRemark"].Value.ToString();
                        TableRow["启停用标志"] = dgRow.Cells["ApplyFlag"].Value.ToString();
                        XlsTable.Rows.Add(TableRow);
                    }
                    ImportExportExcel.NPOIExportExcelFile(XlsTable, ExportXlsName);//生成Excel表格文件
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 窗体初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCStockQuery_Load(object sender, EventArgs e)
        {
            try
            {


                //初始化配件分类
                TreeVPartCategory.Nodes.Clear();
                string PartCategory = "sys_parts_category";//配件分类
                TreeNode root = new TreeNode();
                root.Text = "全部配件";
                root.Name = "";
                TreeVPartCategory.Nodes.Add(root);
                CreateTreeViewChildNode(root, PartCategory);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 创建配件分类树节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentID">父编码</param>
        private void CreateTreeViewChildNode(TreeNode parentNode, string parentID)
        {
            try
            {
                if (parentID.Length == 0)
                {
                    return;
                }
                DataTable dt = CommonCtrl.GetDictByCode(parentID, false);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = CommonCtrl.IsNullToString(dr["dic_name"]);
                    node.Name = CommonCtrl.IsNullToString(dr["dic_id"]);
                    parentNode.Nodes.Add(node);
                    string dicCode = CommonCtrl.IsNullToString(dr["dic_Code"]);
                    CreateTreeViewChildNode(node, dicCode);//递归调用添加子分类节点
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// 根据选择的配件编码获取编码和名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmParts chooseParts = new frmParts();
                chooseParts.ShowDialog();
                if (!string.IsNullOrEmpty(chooseParts.PartsID))
                {
                    txtparts_code.Text = chooseParts.PartsCode;
                    txtparts_name.Caption = chooseParts.PartsName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary> 
        /// 选择配件类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmPartsType choosePartsType = new frmPartsType();
                choosePartsType.ShowDialog();
                if (!string.IsNullOrEmpty(choosePartsType.TypeID))
                {
                    txtparts_type.Text = choosePartsType.TypeName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }
        /// <summary> 
        /// 选择配件车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_cartype_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmVehicleModels chooseCarModel = new frmVehicleModels();
                chooseCarModel.ShowDialog();
                if (!string.IsNullOrEmpty(chooseCarModel.VMID))
                {
                    txtparts_cartype.Text = chooseCarModel.VMName;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductPlace.Caption = string.Empty;
            txtBarCode.Caption = string.Empty;
            txtProductPlace.Caption = string.Empty;
            txtparts_code.Text = string.Empty;
            txtparts_brand.Caption = string.Empty;
            txtCarFactoryCode.Caption = string.Empty;
            txtparts_type.Text = string.Empty;
            txtparts_cartype.Text = string.Empty;
            CombWarehouse.SelectedIndex = 0;
            CombCompany.SelectedIndex = 0;
            CheckBGuarantyStock.Checked = false;
        }

        private void CheckBGuarantyStock_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBGuarantyStock.Checked)
            {
                CombWarehouse.Text = YutongPartStock;
                CombWarehouse.Enabled = false;//禁止选择此控件
            }
            else
            {
                CombWarehouse.Enabled = true;
                CommonFuncCall.BindWarehouse(CombWarehouse, "请选择"); //获取仓库名称
            }
        }

        /// <summary>
        /// 配件库存查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (gvStockList.Rows.Count != 0) gvStockList.Rows.Clear();//清空原始数据
            string QueryWhere = QueryPartWhereCondition();//获取查询条件
            //显示查询进度
            QueryProgressFrm ProgFrm = new QueryProgressFrm();
            ProgFrm.ShowDialog();
            if (CheckBGuarantyStock.Checked)
            {
                GetYuTongPartList(QueryWhere);//宇通三包库存查询
            }
            else
            {
                GetPartList(QueryWhere);//配件库存查询
            }
        }
        /// <summary> 
        /// 按配件库存分页查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormStockPager_PageIndexChanged(object sender, EventArgs e)
        {
            string QueryWhere = QueryPartWhereCondition();//获取查询条件
            GetPartList(QueryWhere);
        }

        /// <summary>
        /// 按配件查询库存记录
        /// </summary>
        private void GetPartList(String WhereStr)
        {
            try
            {
                if (gvStockList.Rows.Count != 0) gvStockList.Rows.Clear();//清空原始数据
                int RecCount = 0;//分页查询记录行数
                ////统计账面总库存
                //string PapCountField = "sum(distinct PBT.{20})+sum(distinct SBT.{21})+sum(distinct RFT.{22})+sum(distinct CPT.{23})+" +
                //"sum(distinct RCT.{19})+sum(distinct SPT.{19})+sum(distinct LPT.{19}) as PaperCount";
                ////数据表查询字段集
                //StringBuilder sbPartField = new StringBuilder();
                //sbPartField.AppendFormat("IOPartTable.{0},IOPartTable.{1},IOPartTable.{2},IOPartTable.{3},PTT.{4},PaperCount,PhyCount,OcpyCount," +
                //"IOPartTable.{5},PTT.{6},PTT.{7},IOPartTable.{8},PTT.{9},{10},{11},{12},{13},{14},{15},{16},{17},VMT.{18},PTT.{19},IOPartTable.{20},IOPartTable.{21},IOPartTable.{22},PTT.{23}",
                //PartsNum, PartsName, DrawNum, PartSpec, PartType, UnitName, RefInPrice, RefOutPrice,//7
                //PartBarCode, Wgt, HInPrice, LOutPrice, FInPric, SInPric, TInPric, FOutPric, SOutPric, TOutPric, VehicleTypeName, PlaceOrigin, //19
                //PartBrand, CarFactoryCode, PartRemk, PartEnableFlag);//23
                //库存单据配件实际库存数据表联合查询集
                //StringBuilder sbRelationTable = new StringBuilder();
                //sbRelationTable.AppendFormat("{25} as IOBillTb inner join {0} as IOPartTable on IOBillTb.{26}=IOPartTable.{26} inner join {8} as PTT on PTT.{16}=IOPartTable.{14}" + 
                //" inner join {9} as PVT on PVT.{15}=PTT.{15} inner join {10} as VMT on VMT.{17}=PVT.{17} "+
                //" inner join {11} as YuTongT on YuTongT.{14}=IOPartTable.{14} inner join {12} as YuTongPT on YuTongPT.{14}=YuTongT.{14}" +
                //" inner join {13} as PPT on PPT.{14}=PTT.{14} inner join (select IOT.{14},sum(distinct IOT.{19}) as PhyCount," + PapCountField +
                //" from {0} as IOT inner join {1} as PBT on IOT.{14}=PBT.{14} inner join {2} as SBT on PBT.{14}=SBT.{14} inner join {3} as RFT on RFT.{14}=IOT.{14}" +
                //" inner join {4} as CPT on CPT.{14}=IOT.{14}  inner join {5} as RCT on RCT.{14}=IOT.{14} inner join {6} as SPT on SPT.{14}=RCT.{14} inner join {7} as LPT on LPT.{14}=IOT.{14} group by IOT.{14})" +
                //" as PaperCntTable on PaperCntTable.{14}=IOPartTable.{14} inner join (select YTPT.{14}, sum(distinct {24}) as OcpyCount from {11} as YTT  inner join {12} as YTPT on YTPT.{14}=YTT.{14} where {18}='" + Submited +
                //"' group by YTPT.{14},{18}) as OcpyCntTable on OcpyCntTable.{14}=IOPartTable.{14}",
                //InOutPartTable, PurBillingPartTable, SaleBillingPartTable, RefundPartTable, CheckPartTable, ReceiptPartTable, ShipPartTable, LossPartTable,//7
                //PartsTable, PartForVhcTable, VehModelTable, YuTongThreeGuarantyTable, YuTongThreeGuarantyPartTable, PartsPriceTable, PartsNum, //14
                //PartID, SePartsCode, VehicleTypeId, YuTonngApproveStatus, PartCount, PurBillingPartCount, SaleBillingPartCount,//21
                //RefundCount, CheckCount, YuTongPartCount,InOutBillTable,InOutBillID);//26

                DataTable InoutTable = DBHelper.GetTableByPage(StockQueryLogMsg, "v_stock_statistic_p", "*", WhereStr,
                "", "PhyCount", winFormStockPager.PageIndex, winFormStockPager.PageSize, out RecCount);//获取库存单配件表查询记录
                winFormStockPager.RecordCount = RecCount;//获取总记录行
                if (RecCount == 0) return;

                //把查询的出入库单列表放入Gridview
                for (int i = 0; i < InoutTable.Rows.Count; i++)
                {
                    int PhyCount = Convert.ToInt32(InoutTable.Rows[i]["PhyCount"].ToString());//实际库存
                    int OcyCount = Convert.ToInt32(InoutTable.Rows[i]["OcpyCount"].ToString());//占用库存
                    int AvblCount = PhyCount - OcyCount;//可用库存

                        DataGridViewRow dgRow = gvStockList.Rows[gvStockList.Rows.Add()];//创建新行项
                        dgRow.Cells["PartNum"].Value = InoutTable.Rows[i][PartsNum].ToString();
                        dgRow.Cells["PartName"].Value = InoutTable.Rows[i][PartsName].ToString();
                        dgRow.Cells["DrawingNum"].Value = InoutTable.Rows[i][DrawNum].ToString();
                        dgRow.Cells["PartModel"].Value = InoutTable.Rows[i][PartSpec].ToString();//配件规格
                        dgRow.Cells["PartCategory"].Value = InoutTable.Rows[i][PartType].ToString();//配件类别
                        dgRow.Cells["PapCount"].Value = InoutTable.Rows[i]["PaperCount"].ToString();//账面库存
                        dgRow.Cells["PhysicalCount"].Value = InoutTable.Rows[i]["PhyCount"].ToString();//实际库存
                        dgRow.Cells["OccupyCount"].Value = InoutTable.Rows[i]["OcpyCount"].ToString();//占用库存
                        dgRow.Cells["AvailableCount"].Value = AvblCount;//可用库存
                        dgRow.Cells["UntName"].Value = InoutTable.Rows[i][UnitName].ToString();
                        dgRow.Cells["ReferInPrice"].Value = InoutTable.Rows[i][RefInPrice].ToString();//参考进价
                        dgRow.Cells["ReferOutPrice"].Value = InoutTable.Rows[i][RefOutPrice].ToString();//参考售价
                        dgRow.Cells["Weight"].Value = InoutTable.Rows[i][Wgt].ToString();
                        dgRow.Cells["HighestInPrice"].Value = InoutTable.Rows[i][HInPrice].ToString();//最高进价
                        dgRow.Cells["LowestOutPrice"].Value = InoutTable.Rows[i][LOutPrice].ToString();//最低售价
                        //dgRow.Cells["VIPPrice"].Value = InoutTable.Rows[i][PartsBrand].ToString();//会员价
                        dgRow.Cells["FirstInPrice"].Value = InoutTable.Rows[i][FInPric].ToString();//一级进价
                        dgRow.Cells["SecondInPrice"].Value = InoutTable.Rows[i][SInPric].ToString();//二级进价
                        dgRow.Cells["ThirdInPrice"].Value = InoutTable.Rows[i][TInPric].ToString();//三级进价
                        dgRow.Cells["FirstOutPrice"].Value = InoutTable.Rows[i][FOutPric].ToString();//一级销价
                        dgRow.Cells["SecondOutPrice"].Value = InoutTable.Rows[i][SOutPric].ToString();//二级销价
                        dgRow.Cells["ThirdOutPrice"].Value = InoutTable.Rows[i][TOutPric].ToString();//三级销价
                        dgRow.Cells["VehicleType"].Value = InoutTable.Rows[i][VehicleTypeName].ToString();//车型
                        dgRow.Cells["ProductPlace"].Value = InoutTable.Rows[i][PlaceOrigin].ToString();//产地
                        dgRow.Cells["PartBrand"].Value = InoutTable.Rows[i][PartsBrand].ToString();//品牌
                        dgRow.Cells["FactoryCode"].Value = InoutTable.Rows[i][CarFactoryCode].ToString();//车厂编码
                        dgRow.Cells["PartRemark"].Value = InoutTable.Rows[i][PartRemk].ToString();//配件备注
                        dgRow.Cells["ApplyFlag"].Value = InoutTable.Rows[i][PartEnableFlag].ToString();//配件启停用标志

                   
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }

        /// <summary>
        /// 按配件查询宇通三包库存记录
        /// </summary>
        private void GetYuTongPartList(String WhereStr)
        {
            try
            {
                
                int RecCount = 0;//分页查询记录行数
                //统计账面总库存
                string PapCountField = "sum(distinct {20})+sum(distinct {21})+sum(distinct {22} as PaperCount";
                //数据表查询字段集
                StringBuilder sbPartField = new StringBuilder();
                sbPartField.AppendFormat("IOT.{0},IOT.{1},IOT.{2},IOT.{3},PTT.{4},PaperCount,PhyCount,OcpyCount," +
                "IOT.{5},PTT.{6},PTT.{7},IOT.{8},PTT.{9},{10},{11},{12},{13},{14},{15},{16},{17},VMT.{18},PTT.{19},IOT.{20},IOT.{21},IOT.{22},PTT.{23}",
                PartsNum, PartsName, DrawNum, PartSpec, PartType, UnitName, RefInPrice, RefOutPrice,//7
                PartBarCode, Wgt, HInPrice, LOutPrice, FInPric, SInPric, TInPric, FOutPric, SOutPric, TOutPric, VehicleTypeName, PlaceOrigin, //19
                PartBrand, CarFactoryCode, PartRemk, PartEnableFlag);//23
                //库存单据配件实际库存数据表联合查询集
                StringBuilder sbRelationTable = new StringBuilder();
                sbRelationTable.AppendFormat("{26} as IOBillTb inner join {0} as IOT on IOBillTb.{27}=IOT.{27} inner join {5} as PTT on PTT.{13}=IOT.{11} inner join {6} as PVT on PVT.{12}=PTT.{12} " +
                " inner join {7} as VMT on VMT.{14}=PVT.{14} inner join {10} as PPT on PPT.{12}=PTT.{12} inner join (select {11},sum(distinct {19}) as PhyCount" +
                " from {0} inner join {26} on {0}.{27}={26}.{27} where {17}='{18}' group by {11},{17}) as YTIOTable on YTIOTable.{11}=IOT.{11} inner join (select PBPT.{11}," + PapCountField +
                " from {1} as PBT inner join {2} as PBPT on PBT.{15}=PBPT.{15}" +
                " inner join {3} as FPT on FPT.{11}=PBPT.{11} inner join {4} as RFT on RFT.{11}=FPT.{11} where {16} is not null and FPT.{25}='1' group by PBPT.{11},{16},FPT.{25}) "+
                " as YTPapCntTable on YTIOTable.{11}=YTPapCntTable.{11}  inner join (select YTPT.{11},sum(distinct {23}) as OcpyCount from {8} as YTT inner join {9} as YTPT on YTPT.{11}=YTT.{11} where {24}='" + Submited +
                " group by YTPT.{11},{24}) as YTOcpyCntTable on YTIOTable.{11}=YTOcpyCntTable.{11} ",
                InOutPartTable,PurBillingTable,PurBillingPartTable,FetchPartTable,RefundPartTable,PartsTable,PartForVhcTable,VehModelTable,YuTongThreeGuarantyTable,//8
                YuTongThreeGuarantyPartTable, PartsPriceTable, PartsNum, PartID, SePartsCode,VehicleTypeId,PurBillingID,DsnBillNum,//16
                WareHouse, YutongPartStock, PartCount, PurBillingPartCount, FetchCount, RefundCount, YuTongPartCount, YuTonngApproveStatus, ThreeWarFlag, InOutBillTable, InOutBillID);//27
                DataTable YuTongInoutTable = DBHelper.GetTableByPage(YuTongStockQueryLogMsg, sbRelationTable.ToString(), sbPartField.ToString(), WhereStr,
                "", "", winFormStockPager.PageIndex, winFormStockPager.PageSize, out RecCount);//获取库存单配件表查询记录
                winFormStockPager.RecordCount = RecCount;//获取总记录行

                if (YuTongInoutTable.Rows.Count == 0) return;
                //把查询的出入库单列表放入Gridview
                for (int i = 0; i < YuTongInoutTable.Rows.Count; i++)
                {
                    int PhyCount = Convert.ToInt32(YuTongInoutTable.Rows[i]["PhyCount"].ToString());//实际库存
                    int OcyCount = Convert.ToInt32(YuTongInoutTable.Rows[i]["OcpyCount"].ToString());//占用库存
                    int AvblCount = PhyCount - OcyCount;//可用库存
                    DataGridViewRow dgRow = gvStockList.Rows[gvStockList.Rows.Add()];//创建新行项
                    dgRow.Cells["PartNum"].Value = YuTongInoutTable.Rows[i][PartsNum].ToString();
                    dgRow.Cells["PartName"].Value = YuTongInoutTable.Rows[i][PartsName].ToString();
                    dgRow.Cells["DrawingNum"].Value = YuTongInoutTable.Rows[i][DrawNum].ToString();
                    dgRow.Cells["PartModel"].Value = YuTongInoutTable.Rows[i][PartSpec].ToString();//配件规格
                    dgRow.Cells["PartCategory"].Value = YuTongInoutTable.Rows[i][PartType].ToString();//配件类别
                    dgRow.Cells["PapCount"].Value = YuTongInoutTable.Rows[i]["PaperCount"].ToString();//账面库存
                    dgRow.Cells["PhysicalCount"].Value = YuTongInoutTable.Rows[i]["PhyCount"].ToString();//实际库存
                    dgRow.Cells["OccupyCount"].Value = YuTongInoutTable.Rows[i]["OcpyCount"].ToString();//占用库存
                    dgRow.Cells["AvailableCount"].Value = AvblCount;//可用库存
                    dgRow.Cells["UntName"].Value = YuTongInoutTable.Rows[i][UnitName].ToString();
                    dgRow.Cells["ReferInPrice"].Value = YuTongInoutTable.Rows[i][RefInPrice].ToString();//参考进价
                    dgRow.Cells["ReferOutPrice"].Value = YuTongInoutTable.Rows[i][RefOutPrice].ToString();//参考售价
                    dgRow.Cells["Weight"].Value = YuTongInoutTable.Rows[i][Wgt].ToString();
                    dgRow.Cells["HighestInPrice"].Value = YuTongInoutTable.Rows[i][HInPrice].ToString();//最高进价
                    dgRow.Cells["LowestOutPrice"].Value = YuTongInoutTable.Rows[i][LOutPrice].ToString();//最低售价
                    //dgRow.Cells["VIPPrice"].Value = InoutTable.Rows[i][PartsBrand].ToString();//会员价
                    dgRow.Cells["FirstInPrice"].Value = YuTongInoutTable.Rows[i][FInPric].ToString();//一级进价
                    dgRow.Cells["SecondInPrice"].Value = YuTongInoutTable.Rows[i][SInPric].ToString();//二级进价
                    dgRow.Cells["ThirdInPrice"].Value = YuTongInoutTable.Rows[i][TInPric].ToString();//三级进价
                    dgRow.Cells["FirstOutPrice"].Value = YuTongInoutTable.Rows[i][FOutPric].ToString();//一级销价
                    dgRow.Cells["SecondOutPrice"].Value = YuTongInoutTable.Rows[i][SOutPric].ToString();//二级销价
                    dgRow.Cells["ThirdOutPrice"].Value = YuTongInoutTable.Rows[i][TOutPric].ToString();//三级销价
                    dgRow.Cells["VehicleType"].Value = YuTongInoutTable.Rows[i][VehicleTypeName].ToString();//车型
                    dgRow.Cells["ProductPlace"].Value = YuTongInoutTable.Rows[i][PlaceOrigin].ToString();//产地
                    dgRow.Cells["PartBrand"].Value = YuTongInoutTable.Rows[i][PartsBrand].ToString();//品牌
                    dgRow.Cells["FactoryCode"].Value = YuTongInoutTable.Rows[i][CarFactoryCode].ToString();//车厂编码
                    dgRow.Cells["PartRemark"].Value = YuTongInoutTable.Rows[i][PartRemk].ToString();//配件备注
                    dgRow.Cells["ApplyFlag"].Value = YuTongInoutTable.Rows[i][PartEnableFlag].ToString();//配件启停用标志
                }
            }
            catch (Exception ex)


            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }

        /// <summary>
        /// 按配件查询总库存的条件
        /// </summary>
        private string QueryPartWhereCondition()
        {
            try
            {
                string Str_Where = " enable_flag=1 and status=1";
                if (!string.IsNullOrEmpty(CombCompany.SelectedValue.ToString()))
                {
                    Str_Where += " and com_name = '" + CombCompany.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(CombWarehouse.SelectedValue.ToString()))
                {
                    Str_Where += " and wh_name = '" + CombWarehouse.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtProductPlace.Caption.ToString()))
                {
                    Str_Where += " and place_origin='" + txtProductPlace.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtBarCode.Text.ToString()))
                {
                    Str_Where += " and parts_barcode='" + txtBarCode.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_code.Text.ToString()))
                {
                    Str_Where += " and parts_code='" + txtparts_code.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_name.Caption.ToString()))
                {
                    Str_Where += " and parts_name='" + txtparts_name.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtdrawing_num.Caption.ToString()))
                {
                    Str_Where += " and drawing_num='" + txtdrawing_num.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_brand.Caption.ToString()))
                {
                    Str_Where += " and parts_brand='" + txtparts_brand.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtCarFactoryCode.Caption.ToString()))
                {
                    Str_Where += " and car_factory_code='" + txtCarFactoryCode.Caption.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_type.Text.ToString()))
                {
                    Str_Where += " and parts_type='" + txtparts_type.Text.ToString() + "'";
                }
                 if (!string.IsNullOrEmpty(txtparts_cartype.Text.ToString()))
                {
                    Str_Where += " and vm_name='" + txtparts_cartype.Text.ToString() + "'";
                }
                return Str_Where;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }





      
    }
}
