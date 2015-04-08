using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterialReturn
{
    /// <summary>
    /// 维修管理-领料退货单新增编辑
    /// Author：JC
    /// AddTime：2014.10.29
    /// </summary>
    public partial class UCFMaterialReturnAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCFMaterialReturnManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 单据Id
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 单据详情Id
        /// </summary>
        public string strDId = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 是否自动关闭
        /// </summary>
        bool isAutoClose = false;
        #endregion

        #region 初始化窗体
        public UCFMaterialReturnAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            SetDgvAnchor();
            BindWarehouseWay();
            SetTopbuttonShow();
            base.SaveEvent += new ClickHandler(UCFMaterialReturnAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCFMaterialReturnAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCFMaterialReturnAddOrEdit_ImportEvent);
            base.CancelEvent += new ClickHandler(UCFMaterialReturnAddOrEdit_CancelEvent);
            dgvMaterials.CellValueChanged += new DataGridViewCellEventHandler(dgvMaterials_CellValueChanged);
            SetQuick();
        }

        void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value)))
            {
                dgvMaterials.Rows[e.RowIndex].Cells["retreat_num"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["retreat_num"].Value, 1);
            }
        }
        #endregion

        #region 设置速查功能
        /// <summary>
        /// 设置速查功能
        /// </summary>
        private void SetQuick()
        {

            //设置车牌号速查
            txtCarNO.SetBindTable("v_vehicle", "license_plate");
            txtCarNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCarNO.DataBacked += new TextChooser.DataBackHandler(txtCarNO_DataBacked);
            //设置客户编码速查
            txtCustomNO.SetBindTable("tb_customer", "cust_code");
            txtCustomNO.GetDataSourced += new ServiceStationClient.ComponentUI.TextBox.TextChooser.GetDataSourceHandler(tc_GetDataSourced);
            txtCustomNO.DataBacked += new TextChooser.DataBackHandler(txtCustomNO_DataBacked);

        }
        /// <summary>
        /// 客户编码速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCustomNO_DataBacked(DataRow dr)
        {
            try
            {
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }

        }
        /// <summary>
        /// 车牌号速查连带信息
        /// </summary>
        /// <param name="dr"></param>
        void txtCarNO_DataBacked(DataRow dr)
        {
            try
            {
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["v_model"]));
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["v_model"]);
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["cust_code"]);
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["cust_name"]);
                txtContact.Caption = CommonCtrl.IsNullToString(dr["cont_name"]);
                //txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["cont_phone"]);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 设置速查
        /// </summary>
        /// <param name="sqlString"></param>
        void tc_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }

        #endregion

        #region 取消事件
        void UCFMaterialReturnAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(strId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strId, Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString());
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                isAutoClose = true;
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnEdit.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnView.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 导入事件
        void UCFMaterialReturnAddOrEdit_ImportEvent(object sender, EventArgs e)
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
        void UCFMaterialReturnAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            opName = "提交领料退货单信息";
            SaveOrSubmitMethod("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void UCFMaterialReturnAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            opName = "保存领料退货单信息";
            SaveOrSubmitMethod("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 验证必填项
        private Boolean CheckControlValue()
        {
            try
            {
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCarNO, "车牌号不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    Validator.SetError(errorProvider1, txtCustomNO, "客户编码不能为空!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称不能为空
                {
                    Validator.SetError(errorProvider1, txtCustomName, "客户名称不能为空!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                return false;
            }
            return true;
        }
        #endregion

        #region 保存、提交方法
        /// <summary>
        /// 保存、提交方法
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="Estatus">单据操作状态</param>
        private void SaveOrSubmitMethod(string strMessage, DataSources.EnumAuditStatus Estatus)
        {
            try
            {
                #region 必要的判断
                if (!CheckControlValue()) return;
                #endregion
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                SaveOrderInfo(listSql, Estatus);
                SaveMaterialsData(listSql, strId);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    isAutoClose = true;
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 领料退货单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                dicParam.Add("vehicle_no", new ParamObj("vehicle_no", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//车牌号
                dicParam.Add("refund_time", new ParamObj("refund_time", Common.LocalDateTimeToUtcLong(dtpMakeOrderTime.Value).ToString(), SysDbType.BigInt));//退料时间
                dicParam.Add("vehicle_model", new ParamObj("vehicle_model", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)) ? txtCarType.Tag : null, SysDbType.VarChar, 40));//车型 
                dicParam.Add("customer_code", new ParamObj("customer_code", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Text)) ? txtCustomNO.Text.Trim() : null, SysDbType.VarChar, 40));//客户编码
                dicParam.Add("customer_name", new ParamObj("customer_name", txtCustomName.Caption.Trim(), SysDbType.VarChar, 20));//客户名称
                dicParam.Add("linkman", new ParamObj("linkman", txtContact.Caption.Trim(), SysDbType.VarChar, 20));//联系人
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", txtContactPhone.Caption.Trim(), SysDbType.VarChar, 15));//联系人手机 
                }
                else
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", null, SysDbType.VarChar, 15));//联系人手机 
                }
                dicParam.Add("refund_opid", new ParamObj("refund_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtRFetchOpid.Tag)) ? txtRFetchOpid.Tag.ToString() : null, SysDbType.VarChar, 40));//退料人
                dicParam.Add("material_no", new ParamObj("material_no", txtOrder.Caption.Trim(), SysDbType.VarChar, 40));//领料单号
                dicParam.Add("fetch_opid", new ParamObj("fetch_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtFetchOpid.Tag)) ? txtFetchOpid.Tag.ToString() : null, SysDbType.VarChar, 40));//领料人
                dicParam.Add("packing_time", new ParamObj("packing_time", Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpFetchTime.Caption)).ToString(), SysDbType.BigInt));//领料时间
                dicParam.Add("remarks", new ParamObj("remarks", txtRemark.Caption.Trim(), SysDbType.VarChar, 200));//备注
                #endregion

                //经办人id
                dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
                //经办人
                dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedText : null, SysDbType.VarChar, 40));
                //部门
                dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
                if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号
                {
                    dicParam.Add("refund_no", new ParamObj("refund_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//领料退货单号 
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    //更新前置单据导入状态
                    if (!string.IsNullOrEmpty(strId))
                    {
                        UpdateMaintainInfo(listSql, strId, Convert.ToInt32(DataSources.EnumImportStaus.LOCK).ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    {
                        dicParam.Add("refund_no", new ParamObj("refund_no", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//领料退货单号 
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    }
                    else
                    {
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                        dicParam.Add("refund_no", new ParamObj("refund_no", null, SysDbType.VarChar, 40));//领料退货单号
                    }

                    //更新前置单据导入状态
                    if (!string.IsNullOrEmpty(strId))
                    {
                        UpdateMaintainInfo(listSql, strId, Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString());
                    }
                }
                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    strId = Guid.NewGuid().ToString();
                    dicParam.Add("refund_id", new ParamObj("refund_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                    dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                    obj.sqlString = @"insert into tb_maintain_refund_material (vehicle_no,refund_time,vehicle_model,customer_code,customer_name,linkman,link_man_mobile
                 ,refund_opid,material_no,fetch_opid,packing_time,remarks,responsible_opid,responsible_name,org_name,enable_flag,info_status,refund_no,refund_id
                 ,create_by,create_name,create_time)
                 values (@vehicle_no,@refund_time,@vehicle_model,@customer_code,@customer_name,@linkman,@link_man_mobile
                 ,@refund_opid,@material_no,@fetch_opid,@packing_time,@remarks,@responsible_opid,@responsible_name,@org_name,@enable_flag,@info_status,@refund_no,@refund_id
                 ,@create_by,@create_name,@create_time);";
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    dicParam.Add("refund_id", new ParamObj("refund_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = @"update tb_maintain_refund_material set vehicle_no=@vehicle_no,refund_time=@refund_time,vehicle_model=@vehicle_model,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile
                 ,refund_opid=@refund_opid,material_no=@material_no,fetch_opid=@fetch_opid,packing_time=@packing_time,remarks=@remarks,responsible_opid=@responsible_opid,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,refund_no=@refund_no
                 ,update_by=@update_by,update_name=@update_name,update_time=@update_time
                where refund_id=@refund_id";
                }
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 领料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["parts_name"].Value);
                    string strPCmoney = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (strPname.Length > 0 && strPCmoney.Length > 0)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("refund_id", new ParamObj("refund_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                        dicParam.Add("retreat_num", new ParamObj("retreat_num", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["retreat_num"].Value)) ? dgvr.Cells["retreat_num"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("warehouse", new ParamObj("warehouse", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["warehouse"].Value)) ? dgvr.Cells["warehouse"].Value : null, SysDbType.VarChar, 40));
                       
                        string strIsimport = CommonCtrl.IsNullToString(dgvr.Cells["whether_imported"].Value);
                        if (!string.IsNullOrEmpty(strIsimport))
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", strIsimport == "是" ? "1" : "0", SysDbType.VarChar, 1));
                        }
                        else
                        {
                            dicParam.Add("whether_imported", new ParamObj("whether_imported", null, SysDbType.VarChar, 1));
                        }
                        dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                        dicParam.Add("drawn_no", new ParamObj("drawn_no", dgvr.Cells["drawn_no"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("vehicle_brand", new ParamObj("vehicle_brand", dgvr.Cells["vehicle_brand"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增领料退货单";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = @"insert into tb_maintain_refund_material_detai (refund_id,parts_code,parts_name,unit,retreat_num,
                         warehouse,whether_imported,remarks,drawn_no,vehicle_brand,enable_flag,material_id)
                         values (@refund_id,@parts_code,@parts_name,@unit,@retreat_num
                         ,@warehouse,@whether_imported,@remarks,@drawn_no,@vehicle_brand,@enable_flag,@material_id);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新领料退货单";
                            obj.sqlString = @"update tb_maintain_refund_material_detai set refund_id=@refund_id,parts_code=@parts_code,parts_name=@parts_name,unit=@unit,retreat_num=@retreat_num
                        ,warehouse=@warehouse,whether_imported=@whether_imported,remarks=@remarks,drawn_no=@drawn_no,vehicle_brand=@vehicle_brand,enable_flag=@enable_flag
                        where material_id=@material_id";
                        }
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 绑定仓库信息
        /// <summary>
        /// 绑定仓库信息
        /// </summary>
        private void BindWarehouseWay()
        {
            try
            {
                DataTable dt = DBHelper.GetTable("", "tb_warehouse", "wh_id,wh_name", "", "", "");
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("", "全部"));
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new ListItem(dr["wh_id"], dr["wh_name"].ToString()));
                }
                warehouse.DataSource = list;
                warehouse.ValueMember = "Value";
                warehouse.DisplayMember = "Text";
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion    

        #region 生成领料单号创建人姓名
        /// <summary>
        /// 生成领料单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.ReturnMaterialNo);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
        }
        #endregion

        #region 导入功能—领料单信息导入
        public void BindSetmentData()
        {
            try
            {
                #region 基本信息
                DataTable dt = DBHelper.GetTable("查询领料单", "tb_maintain_fetch_material", "*", " fetch_id ='" + strId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号           
                txtCarType.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型  
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称 
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机 
                txtOrder.Caption = CommonCtrl.IsNullToString(dr["material_num"]);//领料单号
                txtFetchOpid.Caption = GetSetName(CommonCtrl.IsNullToString(dr["fetch_opid"]));//领料人
                txtFetchOpid.Tag = CommonCtrl.IsNullToString(dr["fetch_opid"]);//领料人Id
                string strReTime = CommonCtrl.IsNullToString(dr["fetch_time"]);//领料时间
                if (!string.IsNullOrEmpty(strReTime))
                {
                    dtpFetchTime.Caption = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReTime)).ToShortDateString();
                }
                else
                {
                    dtpFetchTime.Caption = string.Empty;
                }
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                #endregion

                #region 配件信息
                DataTable dmt = DBHelper.GetTable("领料明细数据", "tb_maintain_fetch_material_detai", "*", string.Format(" fetch_id='{0}'", strId), "", "");
                if (dmt.Rows.Count > 0)
                {
                    if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["retreat_num"].Value = CommonCtrl.IsNullToString(dmr["received_num"]);
                        dgvMaterials.Rows[i].Cells["warehouse"].Value = CommonCtrl.IsNullToString(dmr["warehouse"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);                        
                    }
                }

                #endregion

                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strId, Convert.ToInt32(DataSources.EnumImportStaus.OCCUPY).ToString());
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 获得领/退料人名称
        private string GetSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得领/退料人名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion

        #region 窗体Load事件
        private void UCFMaterialReturnAddOrEdit_Load(object sender, EventArgs e)
        {
            dtpMakeOrderTime.Value = DateTime.Now;
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
            else if (wStatus == WindowStatus.Add)
            {
                labReserveNoS.Visible = false;
            }
        }
        #endregion

        #region 根据领料退货单ID获取信息编辑用
        /// <summary>
        /// 根据维修单ID获取信息，编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                #region 基础信息
                string strWhere = string.Format("refund_id='{0}'", strId);
                DataTable dt = DBHelper.GetTable("查询领料退货单", "tb_maintain_refund_material ", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];


                #region 车辆信息
                string strCtime = CommonCtrl.IsNullToString(dr["refund_time"]);
                if (!string.IsNullOrEmpty(strCtime))
                {
                    long Rticks = Convert.ToInt64(strCtime);
                    dtpMakeOrderTime.Value = Common.UtcLongToLocalDateTime(Rticks);//退料料时间
                }
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型 
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                #endregion

                #region 客户信息
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                // txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id  
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                #endregion

                #region 退料信息
                txtRFetchOpid.Text = GetSetName(CommonCtrl.IsNullToString(dr["refund_opid"]));//退料人
                txtRFetchOpid.Tag = CommonCtrl.IsNullToString(dr["refund_opid"]);//退料人Id
                txtOrder.Caption = CommonCtrl.IsNullToString(dr["material_no"]);//领料单号
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                txtFetchOpid.Caption = GetSetName(CommonCtrl.IsNullToString(dr["fetch_opid"]));//领料人
                txtFetchOpid.Tag = CommonCtrl.IsNullToString(dr["fetch_opid"]);//领料人Id
                string strReTime = CommonCtrl.IsNullToString(dr["packing_time"]);//领料时间
                if (!string.IsNullOrEmpty(strReTime))
                {
                    dtpFetchTime.Caption = Common.UtcLongToLocalDateTime(Convert.ToInt64(strReTime)).ToShortDateString();
                }
                else
                {
                    dtpFetchTime.Caption = string.Empty;
                }
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                #endregion
                #endregion

               
                #region 配件信息
                DataTable dmt = DBHelper.GetTable("领料退料明细数据", "tb_maintain_refund_material_detai", "*", string.Format(" refund_id='{0}'", strId), "", "");
                if (dmt.Rows.Count > 0)
                {
                    if (dmt.Rows.Count >= dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["retreat_num"].Value = CommonCtrl.IsNullToString(dmr["retreat_num"]);
                        dgvMaterials.Rows[i].Cells["warehouse"].Value = CommonCtrl.IsNullToString(dmr["warehouse"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                        
                    }
                }
                #endregion               

                #region 顶部按钮设置
                if (wStatus == WindowStatus.Edit)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["refund_no"])))
                    {
                        labReserveNoS.Text = CommonCtrl.IsNullToString(dr["refund_no"]);//领料退料单号
                    }
                    else
                    {
                        labReserveNoS.Visible = false;
                    }
                    strStatus = CommonCtrl.IsNullToString(dr["info_status"]);//单据状态
                    if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        //已提交状态屏蔽提交按钮
                        base.btnSubmit.Enabled = false;
                    }
                    else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽提交、审核按钮
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                    }
                    //else if (strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    //{
                    //    //审核没通过时屏蔽提交按钮
                    //    base.btnSubmit.Enabled = false;
                    //}
                }
                else
                {
                    labReserveNoS.Visible = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
        }
        #endregion

        #region 车牌号选择器事件
        /// <summary>
        /// 车牌号选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNO_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmVehicleGrade frmVehicle = new frmVehicleGrade();
                DialogResult result = frmVehicle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCarNO.Text = frmVehicle.strLicensePlate;
                    txtCarType.Text = GetVmodel(frmVehicle.strModel);
                    txtCarType.Tag = frmVehicle.strModel;
                    txtCustomNO.Text = frmVehicle.strCustCode;
                    txtCustomNO.Tag = frmVehicle.strCustId;
                    txtCustomName.Caption = frmVehicle.strCustName;
                    txtContact.Caption = frmVehicle.strContactName;
                    txtContactPhone.Caption = frmVehicle.strContactPhone;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 车型选择器事件
        /// <summary>
        /// 车型选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmVehicleModels frmModels = new frmVehicleModels();
                DialogResult result = frmModels.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DataTable dtModels = DBHelper.GetTable("", "tb_vehicle_models", "vm_id,vm_name", "enable_flag='1' and vm_id='" + frmModels.VMID + "'", "", "order by vm_name");
                    DataRow dr = dtModels.Rows[0];
                    txtCarType.Text = CommonCtrl.IsNullToString(dr["vm_name"]);
                    txtCarType.Tag = frmModels.VMID;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
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
            try
            {
                frmCustomerInfo frmCInfo = new frmCustomerInfo();
                DialogResult result = frmCInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtCustomNO.Text = frmCInfo.strCustomerNo;
                    txtCustomNO.Tag = frmCInfo.strCustomerId;
                    txtCustomName.Caption = frmCInfo.strCustomerName;
                    txtContact.Caption = frmCInfo.strLegalPerson;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 退料人选择器
        /// <summary>
        /// 领料人选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRFetchOpid_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmCustomerInfo frmCInfo = new frmCustomerInfo();
                DialogResult result = frmCInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtRFetchOpid.Text = frmCInfo.strCustomerName;
                    txtRFetchOpid.Tag = frmCInfo.strCustomerId;
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 导入后更新维修单导入状态
        /// <summary>
        /// 导入后更新维修单导入状态
        /// </summary>
        /// <param name="strReservId">维修单Id</param>
        /// <param name="status">操作状体，0保存开放、1导入占用、2提交审核锁定</param>
        private void UpdateMaintainInfo(List<SQLObj> listSql, string strReservId, string status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

                dicParam.Add("fetch_id", new ParamObj("fetch_id", strReservId, SysDbType.VarChar, 40));
                if (status == Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString())
                {
                    //保存时，前置单据被释放               
                    dicParam.Add("import_status", new ParamObj("import_status", Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString(), SysDbType.VarChar, 40));//开放
                    obj.sqlString = "update tb_maintain_fetch_material set import_status=@import_status where fetch_id=@fetch_id";

                }
                else if (status == Convert.ToInt32(DataSources.EnumImportStaus.OCCUPY).ToString())
                {
                    //导入时，前置单据被占用 
                    dicParam.Add("import_status", new ParamObj("import_status", "1", SysDbType.VarChar, 40));//占用
                    obj.sqlString = "update tb_maintain_fetch_material set import_status=@import_status where fetch_id=@fetch_id";
                }
                else if (status == Convert.ToInt32(DataSources.EnumImportStaus.LOCK).ToString())
                {
                    //审核提交时，前置单据被锁定
                    dicParam.Add("import_status", new ParamObj("import_status", Convert.ToInt32(DataSources.EnumImportStaus.LOCK).ToString(), SysDbType.VarChar, 40));//锁定
                    obj.sqlString = "update tb_maintain_fetch_material set import_status=@import_status where fetch_id=@fetch_id";

                }
                obj.Param = dicParam;
                listSql.Add(obj);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion       

        #region 根据部门的选择绑定经办人
        /// <summary>
        /// 根据部门的选择绑定经办人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOrgId.SelectedValue == null)
                {
                    return;
                }
                CommonFuncCall.BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString(), "请选择");
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["retreat_num"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_brand"].ReadOnly = true;
            #endregion
        }
        #endregion

        #region 领料单导入
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(strId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strId, Convert.ToInt32(DataSources.EnumImportStaus.OCCUPY).ToString());
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
                UCFetchMaterialImport MateriaImport = new UCFetchMaterialImport();
                DialogResult result = MateriaImport.ShowDialog();
                if (result == DialogResult.OK)
                {
                    strId = MateriaImport.strMaterialId;
                    //strDId = MateriaImport.strDMaterialId;
                    BindSetmentData();

                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }

        }
        #endregion

        #region 限制手机号码只能数据数字
        private void txtContactPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationRegex.KeyPressInt(sender, e);
        }
        #endregion

        #region 点击页签上的叉号提示是否关闭当前窗体
        /// <summary>
        /// 点击页签上的叉号提示是否关闭当前窗体
        /// </summary>
        /// <returns></returns>
        public override bool CloseMenu()
        {
            if (isAutoClose)
            {
                return true;
            }
            if (MessageBoxEx.Show("确定要关闭当前窗体吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(strId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strId, "0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }
            }
            return true;
        }
        #endregion
        
    }
}
