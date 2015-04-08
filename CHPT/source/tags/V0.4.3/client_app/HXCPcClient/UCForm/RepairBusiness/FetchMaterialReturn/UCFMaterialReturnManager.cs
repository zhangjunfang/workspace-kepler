using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using SYSModel;
using ServiceStationClient.ComponentUI.TextBox;
using System.Threading;

namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterialReturn
{
    /// <summary>
    /// 维修管理-领料退货单管理
    /// Author：JC
    /// AddTime：2014.10.29
    /// </summary>
    public partial class UCFMaterialReturnManager : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 单据ID值
        /// </summary>
        string strReId = string.Empty;
        /// <summary>
        /// 单据详情ID值
        /// </summary>
        //string strDReId = string.Empty;
        UCVerify verify;//审核窗体
        List<string> listIDs = new List<string>();//已选择ID
        private bool myLock = true;
        int recordCount = 0;
        #endregion

        #region 初始化窗体
        public UCFMaterialReturnManager()
        {
            InitializeComponent(); 
            BindOrderStatus();
           // BindWarehouseWay();
            base.ViewEvent += new ClickHandler(UCFMaterialReturnManager_ViewEvent);
            base.DeleteEvent += new ClickHandler(UCFMaterialReturnManager_DeleteEvent);
            base.EditEvent += new ClickHandler(UCFMaterialReturnManager_EditEvent);
            base.VerifyEvent += new ClickHandler(UCFMaterialReturnManager_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCFMaterialReturnManager_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCFMaterialReturnManager_ImportEvent);
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            SetQuick();
            base.SetContentMenuScrip(dgvRData);
        }
        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            //设置车牌号速查
            txtCarNO.SetBindTable("tb_vehicle", "license_plate", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            //设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);
            //设置退料人速查
            txtFetchOpid.SetBindTable("v_user", "user_name");
            txtFetchOpid.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
        }
        /// <summary>
        /// 客户编码速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCustomNO_DataBacked(DataRow dr)
        {
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
        }
        /// <summary>
        /// 设置速查
        /// </summary>
        /// <param name="sqlString"></param>
        void tc_GetDataSourced(TextChooser tc, string sqlString)
        {
            DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
            tc.SetDataSource(dvt);
            if (dvt != null)
            {
                tc.Search();
            }
        }
        #endregion       

        #region 导入事件
        void UCFMaterialReturnManager_ImportEvent(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            ShowContextMenuStrip(x, y);
        }
        /// <summary> 点击导入按钮，显示导入方式的下拉选项
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void ShowContextMenuStrip(int X, int Y)
        {
            contextMenuM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            contextMenuM.Show();
            contextMenuM.Location = new System.Drawing.Point(X, Y);
        }
        #endregion

        #region 提交事件
        void UCFMaterialReturnManager_SubmitEvent(object sender, EventArgs e)
        {
            SubmitAndVerify("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 提交功能
        /// <summary>
        /// 提交功能,提交时添加单号
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="status">单据状态</param>
        private void SubmitAndVerify(string strMessage, DataSources.EnumAuditStatus status)
        {
            if (MessageBoxEx.Show("确认要提交吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            string strReceId = string.Empty;//单据Id值      
            string strOrderNo = string.Empty;//单据编号
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strReceId += dr.Cells["refund_id"].Value.ToString() + ",";
                    strOrderNo = dr.Cells["refund_no"].Value.ToString();
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("refund_no", new ParamObj("refund_no",!string.IsNullOrEmpty(strOrderNo)?strOrderNo: CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.ReturnMaterialNo), SysDbType.VarChar, 40));//单据编号                   
                    dicParam.Add("refund_id", new ParamObj("refund_id", dr.Cells["refund_id"].Value, SysDbType.VarChar, 40));//单据ID
                    dicParam.Add("info_status", new ParamObj("info_status", status, SysDbType.VarChar, 40));//单据状态
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = "update tb_maintain_refund_material set info_status=@info_status,refund_no=@refund_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where refund_id=@refund_id";
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }

            if (string.IsNullOrEmpty(strReceId))
            {
                MessageBoxEx.Show("请选择需要" + strMessage + "的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为提交", listSql))
            {
                MessageBoxEx.Show("" + strMessage + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindPageData();
            }
        }
        #endregion

        #region 审核事件
        void UCFMaterialReturnManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["refund_id"].Value.ToString());
                }
            }
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要审核的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, long> OrderIDDateDic = new Dictionary<string, long>();//获取需要核实的记录行
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("refund_id", new ParamObj("refund_id", dr.Cells["refund_id"].Value, SysDbType.VarChar, 40));//单据ID
                        dicParam.Add("info_status", new ParamObj("info_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                        dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                        dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = "update tb_maintain_refund_material set info_status=@info_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where refund_id=@refund_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                        long OrdeDate = (long)dr.Cells["refund_time"].Value;
                        OrderIDDateDic.Add(dr.Cells["refund_id"].Value.ToString(), OrdeDate);//添加已审核单据主键ID和单据日期键值对
                    }
                }
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为审核", listSql))
                {
                    string strMsg = string.Empty;
                    if (verify.auditStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        strMsg = "成功";
                        CommonFuncCall.StatisticStock(GetInoutPart(OrderIDDateDic), "领料单明细数据表");//同步更新实际库存
                    }
                    else
                    {
                        strMsg = "不通过";
                    }
                    MessageBoxEx.Show("审核" + strMsg + "！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindPageData();
                }
            }
        }
        #endregion

        #region 添加库存
        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="OrderIDDateDic"></param>
        /// <returns></returns>
        private DataTable GetInoutPart(Dictionary<string, long> OrderIDDateDic)
        {
            try
            {
                DataTable TemplateTable = null;//模版表
                foreach (KeyValuePair<string, long> KVPair in OrderIDDateDic)
                {
                    string strFileds = " a.warehouse,c.wh_name,b.parts_id,a.parts_code,a.parts_name,a.norms,b.parts_barcode,b.car_parts_code,a.drawn_no,a.unit,a.retreat_num,a.refund_id  ";//所查询的字段
                    string RelationTable = "tb_maintain_refund_material_detai a left join tb_parts b on a.parts_code=b.ser_parts_code left join tb_warehouse c on a.warehouse=c.wh_id";//要查询的关联表名
                    TemplateTable = CommonFuncCall.CreatePartStatisticTable();//获取要填充的公用表
                    //获取对应单据的
                    DataTable IOPartTable = DBHelper.GetTable("领料退料数据明细", RelationTable, strFileds, "refund_id='" + KVPair.Key.ToString() + "'", "", "");
                    for (int i = 0; i < IOPartTable.Rows.Count; i++)
                    {
                        DataRow dr = TemplateTable.NewRow();//创建模版表行项
                        dr["OrderDate"] = KVPair.Value.ToString();//单据日期
                        dr["WareHouseID"] = CommonCtrl.IsNullToString(IOPartTable.Rows[i]["warehouse"]);//仓库ID
                        dr["WareHouseName"] = CommonCtrl.IsNullToString(IOPartTable.Rows[i]["wh_name"]);//仓库名称
                        dr["PartID"] = CommonCtrl.IsNullToString(IOPartTable.Rows[i]["parts_id"]);//配件ID
                        dr["PartCode"] = IOPartTable.Rows[i]["parts_code"].ToString();//配件编码
                        dr["PartName"] = IOPartTable.Rows[i]["parts_name"].ToString();//配件名称
                        dr["PartSpec"] = IOPartTable.Rows[i]["norms"].ToString();//配件规格
                        dr["PartBarCode"] = IOPartTable.Rows[i]["parts_barcode"].ToString();//配件条码
                        dr["CarPartsCode"] = IOPartTable.Rows[i]["car_parts_code"].ToString();//车厂编码
                        dr["DrawNum"] = IOPartTable.Rows[i]["drawn_no"].ToString();//配件图号
                        dr["UnitName"] = IOPartTable.Rows[i]["unit"].ToString();//单位名称
                        dr["PartCount"] = IOPartTable.Rows[i]["retreat_num"].ToString();//配件数量
                        dr["StatisticType"] = (int)DataSources.EnumStatisticType.PaperCount;//统计类型
                        TemplateTable.Rows.Add(dr);//添加新的数据行项
                    }
                }
                return TemplateTable;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }

        }
        #endregion

        #region 编辑事件
        void UCFMaterialReturnManager_EditEvent(object sender, EventArgs e)
        {
            //if (!IsCheck("编辑"))
            //{
            //    return;
            //}
            if (dgvRData.CurrentRow == null || dgvRData.CurrentRow.Index < 0)
            {
                return;
            }
            int intIndex = dgvRData.CurrentRow.Index;
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["refund_id"].Value);
            UCFMaterialReturnAddOrEdit ReturnEdit = new UCFMaterialReturnAddOrEdit();
            ReturnEdit.uc = this;
            ReturnEdit.wStatus = WindowStatus.Edit;
            ReturnEdit.strId = strReId;  //编辑单据的Id值
            base.addUserControl(ReturnEdit, "领料退货单-编辑", "ReturnEdit" + ReturnEdit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 删除事件
        void UCFMaterialReturnManager_DeleteEvent(object sender, EventArgs e)
        {
            try
            {

                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["refund_id"].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
                comField.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除领料退货单", "tb_maintain_refund_material", comField, "refund_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvRData.Rows.Count > 0)
                    {
                        dgvRData.CurrentCell = dgvRData.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 预览事件
        void UCFMaterialReturnManager_ViewEvent(object sender, EventArgs e)
        {
            //ViewData();
        }
        #endregion

        #region 进入预览窗体的方法
        /// <summary>
        /// 预览数据
        /// </summary>
        /// <param name="strType">操作类型，0为预览，1为双击单元格</param>
        private void ViewData(string strType = "0")
        {
            if (strType == "0")
            {
                if (!IsCheck("预览"))
                {
                    return;
                }
            }
            UCFMaterialReturnView view = new UCFMaterialReturnView(strReId, "");
            view.uc = this;
            base.addUserControl(view, "领料退货单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region  编辑、预览数据验证
        /// <summary>
        /// 编辑、复制、预览数据验证
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private bool IsCheck(string strMessage)
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["refund_id"].Value.ToString());
                    strReId = dr.Cells["refund_id"].Value.ToString();
                    //strDReId = dr.Cells["detailmaterial_id"].Value.ToString();
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择" + strMessage + "记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能" + strMessage + "1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 顶部按钮显示
        private void TopButtonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCancel.Visible = false;
            base.btnCopy.Visible = false;
            base.btnSave.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnStatus.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCarNO.Tag = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtFetchOpid.Text = string.Empty;
            txtFetchOpid.Tag = string.Empty;
            txtMaintainId.Caption = string.Empty;
            txtRMaterialNo.Caption = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            //cobWarehouse.SelectedValue = string.Empty;
            dtpEFetchTime.Value = DateTime.Now;
            dtpSFetchTime.Value = DateTime.Now.AddMonths(-1);

        }
        #endregion

        #region 查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                if (this.myLock)
                {
                    this.myLock = false;
                    #region 事件选择判断
                    if (dtpSFetchTime.Value > dtpEFetchTime.Value)
                    {
                        MessageBoxEx.Show("退料时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion
                    strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                    if (!string.IsNullOrEmpty(txtRMaterialNo.Caption.Trim()))//领料退货单号
                    {
                        strWhere += string.Format(" and  a.refund_no like '%{0}%'", txtRMaterialNo.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                    {
                        strWhere += string.Format(" and  a.vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                    {
                        strWhere += string.Format(" and  a.customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                    {
                        strWhere += string.Format(" and  a.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtMaintainId.Caption.Trim()))//领料单号
                    {
                        strWhere += string.Format(" and  a.material_no like '%{0}%'", txtMaintainId.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue)))//单据状体
                    {
                        strWhere += string.Format(" and a.info_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                    }
                    //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobWarehouse.SelectedValue)))//仓库名称
                    //{
                    //    strWhere += string.Format(" and b.warehouse = '{0}'", cobWarehouse.SelectedValue.ToString());
                    //}
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtFetchOpid.Tag)))//退料人
                    {
                        strWhere += string.Format(" and a.refund_opid = '{0}'", txtFetchOpid.Tag.ToString());
                    }

                    strWhere += string.Format(" and a.refund_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpSFetchTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpEFetchTime.Value.Date.AddDays(1).AddMilliseconds(-1)));//领料时间
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), strWhere);
                    //string strFiles = "a.refund_no,a.refund_time,a.customer_name,a.customer_code,a.vehicle_no,a.refund_opid,a.info_status,b.quantity,a.material_no,a.linkman,a.link_man_mobile,b.warehouse,a.remarks,a.refund_id,b.refund_id as detailmaterial_id ";
                    //string strTables = "tb_maintain_refund_material a left join tb_maintain_refund_material_detai b on a.refund_id=b.refund_id";
                    //int recordCount;
                    //DataTable dt = DBHelper.GetTableByPage("分页查询领料退货单管理", strTables, strFiles, strWhere, "", " order by a.refund_time desc", page.PageIndex, page.PageSize, out recordCount);
                    //dgvRData.DataSource = dt;
                    //page.RecordCount = recordCount;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            //string strFiles = "a.refund_no,a.refund_time,a.customer_name,a.customer_code,a.vehicle_no,a.refund_opid,a.info_status,b.quantity,a.material_no,a.linkman,a.link_man_mobile,b.warehouse,a.remarks,a.refund_id,b.refund_id as detailmaterial_id ";
            //string strTables = "tb_maintain_refund_material a left join tb_maintain_refund_material_detai b on a.refund_id=b.refund_id";   
            string strFiles = "a.refund_no,a.refund_time,a.customer_name,a.customer_code,a.vehicle_no,a.refund_opid,a.info_status,(select sum(retreat_num)from tb_maintain_refund_material_detai b where  a.refund_id=b.refund_id)as quantity,a.material_no,a.linkman,a.link_man_mobile,a.remarks,a.refund_id";
            string strTables = "tb_maintain_refund_material a";       
            DataTable dt = DBHelper.GetTableByPage("分页查询领料退货单管理", strTables, strFiles, obj.ToString(), "", " order by a.refund_time desc", page.PageIndex, page.PageSize, out this.recordCount);
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvRData.DataSource = obj;
            page.RecordCount = recordCount;

            this.myLock = true;
        }
        #region --初始化事件和数据执行异步操作
        private void InitEvent()
        {
            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

        }
        #endregion
        #endregion

        #region 车牌号选择器事件
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNO_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarNO.Text = frmVehicle.strLicensePlate;
            }
        }
        #endregion

        #region 客户编码选择器事件
        /// <summary>
        /// 客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCustomNO_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerNo;
                txtCustomNO.Tag = frmCInfo.strCustomerId;
                txtCustomName.Caption = frmCInfo.strCustomerName;
            }
        }
        #endregion

        #region 退料人选择器
        /// <summary>
        /// 领料人选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFetchOpid_ChooserClick(object sender, EventArgs e)
        {
            frmPersonnelSelector frmPInfo = new frmPersonnelSelector();
            DialogResult result = frmPInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFetchOpid.Text = frmPInfo.strPersonName;
                txtFetchOpid.Tag = frmPInfo.strUserId;
            }
        }
        #endregion

        #region 绑定单据状态
        /// 绑定单据状态
        /// </summary>
        private void BindOrderStatus()
        {
            cobOrderStatus.DataSource = DataSources.EnumToList(typeof(DataSources.EnumAuditStatus), true);
            cobOrderStatus.ValueMember = "Value";
            cobOrderStatus.DisplayMember = "Text";
        }
        #endregion

        #region 重写datagridview中的时间、状态等
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("refund_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("info_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("refund_opid"))
            {
                e.Value = GetSetName(e.Value.ToString());
            }
            //if (fieldNmae.Equals("warehouse"))
            //{
            //    e.Value = GetWarehouseName(e.Value.ToString());
            //}
        }
        #endregion

        #region 获得退料人名称
        private string GetSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得领料人名称", "tb_customer", "cust_name", "cust_id='" + strPid + "'", "");
        }
        #endregion

        #region 根据仓库Id获取仓库名称
        private string GetWarehouseName(string strWId)
        {
            return DBHelper.GetSingleValue("获取仓库名称", "tb_warehouse", "wh_name", "wh_id='" + strWId + "'", "");
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 双击单元格进入预览窗体
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
            strReId = dgvRData.CurrentRow.Cells["refund_id"].Value.ToString();
            //strDReId = dgvRData.CurrentRow.Cells["detailmaterial_id"].Value.ToString();
            ViewData("1");
        }
        #endregion

        #region 领料单导入
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UCFetchMaterialImport MateriaImport = new UCFetchMaterialImport();
            UCFMaterialReturnAddOrEdit Materialadd = new UCFMaterialReturnAddOrEdit();
            MateriaImport.uc = Materialadd;
            DialogResult result = MateriaImport.ShowDialog();
            if (result == DialogResult.OK)
            {
                Materialadd.uc = this;
                Materialadd.wStatus = WindowStatus.Add;
                Materialadd.strId = MateriaImport.strMaterialId;  //领料单据的Id值
                Materialadd.strDId = MateriaImport.strDMaterialId;  //领料单据的详情Id值
                Materialadd.BindSetmentData();
                base.addUserControl(Materialadd, "领料退货单-添加", "Materialadd" + Materialadd.strId, this.Tag.ToString(), this.Name);
            }

        }
        #endregion

        #region 绑定仓库信息
        /// <summary>
        /// 绑定仓库信息
        /// </summary>
        private void BindWarehouseWay()
        {
            DataTable dt = DBHelper.GetTable("", "tb_warehouse", "wh_id,wh_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["wh_id"], dr["wh_name"].ToString()));
            }
            cobWarehouse.DataSource = list;
            cobWarehouse.ValueMember = "Value";
            cobWarehouse.DisplayMember = "Text";
        }
        #endregion

        #region 窗体Load事件
        private void UCFMaterialReturnManager_Load(object sender, EventArgs e)
        {
            TopButtonShow();
            dtpSFetchTime.Value = DateTime.Now.AddMonths(-1);
            dtpEFetchTime.Value = DateTime.Now;
            dgvRData.Dock = DockStyle.Fill;
            dgvRData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvRData.Columns)
            {
                if (dgvc == colCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            this.InitEvent();
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            BindPageData();
        }
        #endregion

        #region 全选事件
        private void dgvRData_HeadCheckChanged()
        {
            SetSelectedStatus();

        }
        #endregion

        #region 选择，设置工具栏状态
        /// <summary>
        /// 选择，设置工具栏状态
        /// </summary>
        private void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[info_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[refund_id.Name].Value.ToString());
                }
            }

            //提交
            string submitStr = ((int)DataSources.EnumAuditStatus.SUBMIT).ToString();
            //审核
            string auditStr = ((int)DataSources.EnumAuditStatus.AUDIT).ToString();
            //草稿
            string draftStr = ((int)DataSources.EnumAuditStatus.DRAFT).ToString();
            //审核未通过
            string noAuditStr = ((int)DataSources.EnumAuditStatus.NOTAUDIT).ToString();
            //作废
            string invalid = ((int)DataSources.EnumAuditStatus.Invalid).ToString();
            //复制按钮，只选择一个并且不是作废，可以复制
            if (listFiles.Count == 1 && !listFiles.Contains(invalid))
            {
                btnCopy.Enabled = true;              
            }
            else
            {
                btnCopy.Enabled = false;              
            }
            //编辑按钮，只选择一个并且是草稿或未通过状态，可以编辑
            if (listFiles.Count == 1 && (listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr)))
            {
                btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
            }
            //判断”审核“按钮是否可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr) || listFiles.Contains(invalid))
            {
                btnVerify.Enabled = false;
                tsmiVerify.Enabled = false;
            }
            else
            {
                btnVerify.Enabled = true;
                tsmiVerify.Enabled = true;
            }
            //包含已审核、已提交、已作废状态，提交、删除按钮不可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(submitStr) || listFiles.Contains(invalid))
            {
                btnSubmit.Enabled = false;
                btnDelete.Enabled = false;
                tsmiSubmit.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnDelete.Enabled = true;
                tsmiSubmit.Enabled = true;
                tsmiDelete.Enabled = true;
            }

            if (listFiles.Contains(invalid))
            {

            }

        }
        #endregion

        #region 单击行时选中或取消选中
        private void dgvRData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (dgvRData.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        #endregion

        #region 在单元格的任意位置单击鼠标时发生
        private void dgvRData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvRData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            SetSelectedStatus();
        }
        #endregion


        #region 右键功能
        /// <summary>
        /// 右键查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        /// <summary>
        /// 右键清除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
        }       
        /// <summary>
        /// 右键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            UCFMaterialReturnManager_DeleteEvent(null, null);
        }
        /// <summary>
        /// 右键提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSubmit_Click(object sender, EventArgs e)
        {
            UCFMaterialReturnManager_SubmitEvent(null, null);
        }
        /// <summary>
        /// 右键审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiVerify_Click(object sender, EventArgs e)
        {
            UCFMaterialReturnManager_VerifyEvent(null, null);
        }
        /// <summary>
        /// 右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            UCFMaterialReturnManager_EditEvent(null, null);
        }
        #endregion
    }
}
