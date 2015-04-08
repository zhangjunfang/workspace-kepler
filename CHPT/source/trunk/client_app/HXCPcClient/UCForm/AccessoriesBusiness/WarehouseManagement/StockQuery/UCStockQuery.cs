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
        private string PartType = "parts_type";//配件类别
        private string Wgt = "weight";//重量
        private string PlaceOrigin = "place_origin";//配件产地
        private string VehicleTypeName = "vm_name";//车型
        private string PartEnableFlag = "status";//配件启停用标志
        private string RefInPrice = "ref_in_price";//参考进价
        private string RefOutPrice = "ref_out_price";//参考售价
        private string PtBarCode = "parts_barcode";//条码
        private string HInPrice = "highest_in_price";//最高进价
        private string LOutPrice = "low_out_price";//最低售价
        private string FInPric = "into_price_one";//一级进价
        private string SInPric = "into_price_two";//二级进价
        private string TInPric = "into_price_three";//三级进价
        private string FOutPric = "out_price_one";//一级进价
        private string SOutPric = "out_price_two";//二级进价
        private string TOutPric = "out_price_three";//三级进价
        private string YutongPartStock = "宇通配件库";
        private string StockQueryLogMsg = "查询库存单据配件表信息";//库存单配件表操作日志
        private string Submited = DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true);//已提交
        private string Verify = DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true);//已审核
        private string CategoryName = string.Empty;//配件分类名称
        private string StockQueryPro = "p_stock_query";//库存查询存储过程名
        //出入库单配件表字段
        private const string PtCode = "parts_code";
        private const string SerPartNum = "ser_parts_code";
        private const string PartsName = "parts_name";
        private const string CarFactoryCode = "car_parts_code";//车厂配件编码
        private const string DrawNum = "drawing_num";//图号
        private const string PartSpec = "model";//规格
        private const string UnitName = "unit_name";//单位
        private const string PtBrand = "parts_brand_name";//品牌
        private const string PartRemk = "remark";
        private const string ExportXlsName = "库存统计查询";
        #endregion

        public UCStockQuery()
        {
            InitializeComponent();
            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindCompany(CombCompany, "请选择");//选择公司名称
            CommonFuncCall.BindCompanyWarehouse(CombWarehouse, null, "请选择"); //获取仓库名称

            base.ExportEvent += new ClickHandler(UCStockQuery_ExportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnPartSearch, btnPartClear);//美化查询和清除按钮控件
            //DataGridViewEx.SetDataGridViewStyle(gvStockList, ApplyFlag);//美化表格控件
        }

        /// <summary>
        /// 根据选择的公司获取对应仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CombCompany.SelectedValue.ToString()))
                {
                    CommonFuncCall.BindCompanyWarehouse(CombWarehouse, null, "请选择"); //获取仓库名称
                }
                else
                {
                    CommonFuncCall.BindCompanyWarehouse(CombWarehouse, CombCompany.SelectedValue.ToString(), "请选择"); //获取仓库名称
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCStockQuery_ExportEvent(object sender, EventArgs e)
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
                    //XlsTable.Columns.Add("会员价", typeof(string));
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
                        bool SelectFlag = (bool)((DataGridViewCheckBoxCell)dgRow.Cells["colcheck"]).EditedFormattedValue;//获取当前记录行的选择状态
                        if (SelectFlag)
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
                            //TableRow["会员价"] = dgRow.Cells["VIPPrice"].Value.ToString();
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
                    }
                    if (XlsTable.Rows.Count == 0)
                    {
                        MessageBoxEx.Show("请您选择要导出的单据记录行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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
            txtparts_cartype.Text = string.Empty;
            txtparts_name.Caption = string.Empty;
            txtdrawing_num.Caption = string.Empty;
            CombWarehouse.SelectedIndex = 0;
            CombCompany.SelectedIndex = 0;
            CheckBGuarantyStock.Checked = false;
            CategoryName = string.Empty;//清空分类名称
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
           
            string QueryWhere = QueryPartWhereCondition();//获取查询条件
            //显示查询进度
            ProgressBarFrm ProgFrm = new ProgressBarFrm();
            ProgFrm.MaxNum = 100;
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
        /// 根据分类查询配件库存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeVPartCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //显示查询进度
                ProgressBarFrm ProgFrm = new ProgressBarFrm();
                ProgFrm.MaxNum = 100;
                ProgFrm.ShowDialog();
                CategoryName = e.Node.Text.ToString();//获取选中节点分类名称
                if (CategoryName == "全部配件") CategoryName = string.Empty;
                string QueryWhere = QueryPartWhereCondition();//获取查询条件
                if (CheckBGuarantyStock.Checked)
                {
                    GetYuTongPartList(QueryWhere);//宇通三包库存查询
                }
                else
                {
                    GetPartList(QueryWhere);//配件库存查询
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
        /// 按配件查询库存记录
        /// </summary>
        private void GetPartList(String WhereStr)
        {
            try
            {
                if (gvStockList.Rows.Count != 0) gvStockList.Rows.Clear();//清空原始数据
                int RecCount = 0;//分页查询记录行数
                DataSet StockDataSet = null;//库存查询结果
                DataTable StockTable = null;
                int QueryTypeFlag = 0;//查询是类型
                SQLObj ObjectSql = new SQLObj();//创建执行存储过程对像
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                if (string.IsNullOrEmpty(CombWarehouse.SelectedValue.ToString()))
                {
                    QueryTypeFlag = (int)DataSources.EnumStockQueryType.AllStock;
                    dicParam.Add("WhereCondition", new ParamObj("WhereCondition", WhereStr, SysDbType.NVarChar));//查询条件
                }
                else
                {
                    QueryTypeFlag = (int)DataSources.EnumStockQueryType.SingleStock;
                    dicParam.Add("WhereCondition", new ParamObj("WhereCondition", WhereStr + " and wh_name='" 
                    + CombWarehouse.Text.ToString()+"'", SysDbType.NVarChar));//查询条件
                }
                dicParam.Add("QueryFlag", new ParamObj("QueryFlag", QueryTypeFlag, SysDbType.Int));//查询类型
                dicParam.Add("OrderExpression", new ParamObj("OrderExpression", "PhyCount", SysDbType.VarChar));//排序表达式
                dicParam.Add("PageIndex", new ParamObj("PageIndex", winFormStockPager.PageIndex, SysDbType.Int));//分页开始索引
                dicParam.Add("PageSize", new ParamObj("PageSize", winFormStockPager.PageSize, SysDbType.Int));//每页记录数量
                ObjectSql.cmdType = CommandType.StoredProcedure;//指定sql执行类型
                ObjectSql.Param = dicParam;
                ObjectSql.sqlString = StockQueryPro;
                StockDataSet = DBHelper.GetDataSet(StockQueryLogMsg, ObjectSql);//获取库存单配件表查询记录
                StockTable = StockDataSet.Tables[0];
                RecCount = StockTable.Rows.Count;//获取总记录行
                winFormStockPager.RecordCount = RecCount;
                if (RecCount == 0) return;
                //把查询的出入库单列表放入Gridview
                for (int i = 0; i < StockTable.Rows.Count; i++)
                {
                    int PhyCount = Convert.ToInt32(StockTable.Rows[i]["PhyCount"].ToString());//实际库存
                    int OcyCount = Convert.ToInt32(StockTable.Rows[i]["OcpyCount"].ToString());//占用库存
                    int AvblCount = PhyCount - OcyCount;//可用库存

                    DataGridViewRow dgRow = gvStockList.Rows[gvStockList.Rows.Add()];//创建新行项
                    dgRow.Cells["PartNum"].Value = StockTable.Rows[i][PtCode].ToString();
                    dgRow.Cells["PartName"].Value = StockTable.Rows[i][PartsName].ToString();
                    dgRow.Cells["DrawingNum"].Value = StockTable.Rows[i][DrawNum].ToString();
                    dgRow.Cells["PartModel"].Value = StockTable.Rows[i][PartSpec].ToString();//配件规格
                    dgRow.Cells["PartCategory"].Value = StockTable.Rows[i][PartType].ToString();//配件类别
                    dgRow.Cells["PapCount"].Value = StockTable.Rows[i]["PaperCount"].ToString();//账面库存
                    dgRow.Cells["PhysicalCount"].Value = StockTable.Rows[i]["PhyCount"].ToString();//实际库存
                    dgRow.Cells["OccupyCount"].Value = StockTable.Rows[i]["OcpyCount"].ToString();//占用库存
                    dgRow.Cells["AvailableCount"].Value = AvblCount;//可用库存
                    dgRow.Cells["UntName"].Value = StockTable.Rows[i][UnitName].ToString();
                    dgRow.Cells["ReferInPrice"].Value = StockTable.Rows[i][RefInPrice].ToString();//参考进价
                    dgRow.Cells["ReferOutPrice"].Value = StockTable.Rows[i][RefOutPrice].ToString();//参考售价
                    dgRow.Cells["BarCode"].Value = StockTable.Rows[i][PtBarCode].ToString();
                    dgRow.Cells["Weight"].Value = StockTable.Rows[i][Wgt].ToString();
                    dgRow.Cells["HighestInPrice"].Value = StockTable.Rows[i][HInPrice].ToString();//最高进价
                    dgRow.Cells["LowestOutPrice"].Value = StockTable.Rows[i][LOutPrice].ToString();//最低售价
                    //dgRow.Cells["VIPPrice"].Value = StockTable.Rows[i][PartsBrand].ToString();//会员价
                    dgRow.Cells["FirstInPrice"].Value = StockTable.Rows[i][FInPric].ToString();//一级进价
                    dgRow.Cells["SecondInPrice"].Value = StockTable.Rows[i][SInPric].ToString();//二级进价
                    dgRow.Cells["ThirdInPrice"].Value = StockTable.Rows[i][TInPric].ToString();//三级进价
                    dgRow.Cells["FirstOutPrice"].Value = StockTable.Rows[i][FOutPric].ToString();//一级销价
                    dgRow.Cells["SecondOutPrice"].Value = StockTable.Rows[i][SOutPric].ToString();//二级销价
                    dgRow.Cells["ThirdOutPrice"].Value = StockTable.Rows[i][TOutPric].ToString();//三级销价
                    dgRow.Cells["VehicleType"].Value = StockTable.Rows[i][VehicleTypeName].ToString();//车型
                    dgRow.Cells["ProductPlace"].Value = StockTable.Rows[i][PlaceOrigin].ToString();//产地
                    dgRow.Cells["PartBrand"].Value = StockTable.Rows[i][PtBrand].ToString();//品牌
                    dgRow.Cells["FactoryCode"].Value = StockTable.Rows[i][CarFactoryCode].ToString();//车厂编码
                    dgRow.Cells["PartRemark"].Value = StockTable.Rows[i][PartRemk].ToString();//配件备注
                    string AppFlag = StockTable.Rows[i][PartEnableFlag].ToString() == "1" ? "启用" : "停用";
                    dgRow.Cells["ApplyFlag"].Value = AppFlag;//配件启停用标志


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
                if (gvStockList.Rows.Count != 0) gvStockList.Rows.Clear();//清空原始数据
                int RecCount = 0;//分页查询记录行数
                DataSet StockDataSet = null;//库存查询结果
                DataTable YuTongStockTable = null;
                int QueryTypeFlag = (int)DataSources.EnumStockQueryType.YuTongStock;//查询是类型
                SQLObj ObjectSql = new SQLObj();//创建执行存储过程对像
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("WhereCondition", new ParamObj("WhereCondition", WhereStr, SysDbType.NVarChar));
                dicParam.Add("QueryFlag", new ParamObj("QueryFlag", QueryTypeFlag, SysDbType.Int));//查询类型
                dicParam.Add("OrderExpression", new ParamObj("OrderExpression", "PhyCount", SysDbType.VarChar));//排序表达式
                dicParam.Add("PageIndex", new ParamObj("PageIndex", winFormStockPager.PageIndex, SysDbType.Int));//分页开始索引
                dicParam.Add("PageSize", new ParamObj("PageSize", winFormStockPager.PageSize, SysDbType.Int));//每页记录数量
                ObjectSql.cmdType = CommandType.StoredProcedure;//指定sql执行类型
                ObjectSql.Param = dicParam;
                ObjectSql.sqlString = StockQueryPro;
                StockDataSet = DBHelper.GetDataSet(StockQueryLogMsg, ObjectSql);//获取库存单配件表查询记录
                YuTongStockTable = StockDataSet.Tables[0];
                RecCount = YuTongStockTable.Rows.Count;//获取总记录行
                winFormStockPager.RecordCount = RecCount;
                if (RecCount == 0) return;
                //把查询的出入库单列表放入Gridview
                for (int i = 0; i < YuTongStockTable.Rows.Count; i++)
                {
                    decimal PhyCount = Convert.ToDecimal(YuTongStockTable.Rows[i]["PhyCount"].ToString());//实际库存
                    decimal OcyCount = Convert.ToDecimal(YuTongStockTable.Rows[i]["OcpyCount"].ToString());//占用库存
                    decimal AvblCount = PhyCount - OcyCount;//可用库存
                    DataGridViewRow dgRow = gvStockList.Rows[gvStockList.Rows.Add()];//创建新行项
                    dgRow.Cells["PartNum"].Value = YuTongStockTable.Rows[i][SerPartNum].ToString();
                    dgRow.Cells["PartName"].Value = YuTongStockTable.Rows[i][PartsName].ToString();
                    dgRow.Cells["DrawingNum"].Value = YuTongStockTable.Rows[i][DrawNum].ToString();
                    dgRow.Cells["PartModel"].Value = YuTongStockTable.Rows[i][PartSpec].ToString();//配件规格
                    dgRow.Cells["PartCategory"].Value = YuTongStockTable.Rows[i][PartType].ToString();//配件类别
                    dgRow.Cells["PapCount"].Value = YuTongStockTable.Rows[i]["PaperCount"].ToString();//账面库存
                    dgRow.Cells["PhysicalCount"].Value = YuTongStockTable.Rows[i]["PhyCount"].ToString();//实际库存
                    dgRow.Cells["OccupyCount"].Value = YuTongStockTable.Rows[i]["OcpyCount"].ToString();//占用库存
                    dgRow.Cells["AvailableCount"].Value = AvblCount;//可用库存
                    dgRow.Cells["UntName"].Value = YuTongStockTable.Rows[i]["unit"].ToString();
                    dgRow.Cells["ReferInPrice"].Value = YuTongStockTable.Rows[i][RefInPrice].ToString();//参考进价
                    dgRow.Cells["ReferOutPrice"].Value = YuTongStockTable.Rows[i][RefOutPrice].ToString();//参考售价
                    dgRow.Cells["BarCode"].Value = YuTongStockTable.Rows[i][PtBarCode].ToString();
                    dgRow.Cells["Weight"].Value = YuTongStockTable.Rows[i][Wgt].ToString();
                    dgRow.Cells["HighestInPrice"].Value = YuTongStockTable.Rows[i][HInPrice].ToString();//最高进价
                    dgRow.Cells["LowestOutPrice"].Value = YuTongStockTable.Rows[i][LOutPrice].ToString();//最低售价
                    //dgRow.Cells["VIPPrice"].Value = YuTongStockTable.Rows[i][PartsBrand].ToString();//会员价
                    dgRow.Cells["FirstInPrice"].Value = YuTongStockTable.Rows[i][FInPric].ToString();//一级进价
                    dgRow.Cells["SecondInPrice"].Value = YuTongStockTable.Rows[i][SInPric].ToString();//二级进价
                    dgRow.Cells["ThirdInPrice"].Value = YuTongStockTable.Rows[i][TInPric].ToString();//三级进价
                    dgRow.Cells["FirstOutPrice"].Value = YuTongStockTable.Rows[i][FOutPric].ToString();//一级销价
                    dgRow.Cells["SecondOutPrice"].Value = YuTongStockTable.Rows[i][SOutPric].ToString();//二级销价
                    dgRow.Cells["ThirdOutPrice"].Value = YuTongStockTable.Rows[i][TOutPric].ToString();//三级销价
                    dgRow.Cells["VehicleType"].Value = YuTongStockTable.Rows[i][VehicleTypeName].ToString();//车型
                    dgRow.Cells["ProductPlace"].Value = YuTongStockTable.Rows[i][PlaceOrigin].ToString();//产地
                    dgRow.Cells["PartBrand"].Value = YuTongStockTable.Rows[i][PtBrand].ToString();//品牌
                    dgRow.Cells["FactoryCode"].Value = YuTongStockTable.Rows[i][CarFactoryCode].ToString();//车厂编码
                    dgRow.Cells["PartRemark"].Value = YuTongStockTable.Rows[i][PartRemk].ToString();//配件备注
                    string AppFlag = YuTongStockTable.Rows[i][PartEnableFlag].ToString() == "1" ? "启用" : "停用";
                    dgRow.Cells["ApplyFlag"].Value = AppFlag;//配件启停用标志
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
                string Str_Where = "enable_flag=1 and status=1";
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
  
                    Str_Where += " and parts_brand_name='" + txtparts_brand.Caption.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(txtCarFactoryCode.Caption.ToString()))
                {

                    Str_Where += " and car_parts_code='" + txtCarFactoryCode.Caption.ToString() + "'";
                }
                if (!string.IsNullOrEmpty(txtparts_cartype.Text.ToString()))
                {
                    Str_Where += " and vm_name='" + txtparts_cartype.Text.ToString() + "'";
                }
                if(!string.IsNullOrEmpty(CategoryName))
                {
                    Str_Where += " and parts_type='" + CategoryName+"'";
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
