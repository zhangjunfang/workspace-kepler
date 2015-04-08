using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm.RepairBusiness.RepairCallback;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterial
{
    /// <summary>
    /// 维修管理-领料单新增编辑
    /// Author：JC
    /// AddTime：2014.10.28
    /// </summary>
    public partial class UCFetchMaterialAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCFetchMaterialManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 预约单ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 前置单据Id
        /// </summary>
        public string strBefore_orderId = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 用于判断用料是否重复添加
        /// </summary>
        List<string> listMater = new List<string>();
        /// <summary>
        /// 是否自动关闭
        /// </summary>
        bool isAutoClose = false;       
        #endregion

        #region 初始化窗体
        public UCFetchMaterialAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            CommonFuncCall.BindDepartment(cboOrgId, GlobalStaticObj.CurrUserCom_Id, "请选择");
            SetDgvAnchor();
            BindWarehouseWay();
            base.SaveEvent += new ClickHandler(UCFetchMaterialAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCFetchMaterialAddOrEdit_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCFetchMaterialAddOrEdit_CancelEvent);
            base.ImportEvent += new ClickHandler(UCFetchMaterialAddOrEdit_ImportEvent);
            SetQuick();
        }
        #endregion

        #region 该方法用于调度单自动生成领料单读取数据
        public void CreateFetch()
        {
            CreateSetmentData();           
            CreateMData();
        }

        #region 用于调度单自动生成领料单读取基本数据
        /// <summary>
        /// 用于调度单自动生成领料单读取基本数据
        /// </summary>
        public void CreateSetmentData()
        {
            try
            {
                DataTable dt = DBHelper.GetTable("查询维修单单", "tb_maintain_info", "*", " maintain_id ='" + strBefore_orderId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号   
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型 
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                txtOrder.Caption = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号              
                //List<SQLObj> listSql = new List<SQLObj>();
                //UpdateMaintainInfo(listSql, strBefore_orderId, "1");
                //DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 用于调度单自动生成领料单配件数据
        private void CreateMData()
        {
            try
            {
                #region 获取数据
                //配件数据
                DataTable dmt = DBHelper.GetTable("配件数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'  and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strBefore_orderId), "", "");
                if (dmt.Rows.Count > 0)
                {
                    if (dmt.Rows.Count >= dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 2);
                    }
                    else if ((dgvMaterials.Rows.Count - dmt.Rows.Count) == 1)
                    {
                        dgvMaterials.Rows.Add(1);
                    }
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        string strPCode = dgvMaterials.Rows[i].Cells["parts_code"].Value.ToString();
                        DataTable dmt1 = DBHelper.GetTable("", "v_parts_chooser", "paper_count", " ser_parts_code='" + strPCode + "'", "", "");
                        if (dmt1.Rows.Count > 0)
                        {
                            DataRow dmr1 = dmt1.Rows[0];
                            dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dmr1["paper_count"]);
                        }
                        else
                        {
                            dgvMaterials.Rows[i].Cells["inventory_num"].Value = "0";
                        }
                        string strFetchId = string.Empty;//领料单Id
                        decimal decHQuantity =0;//历史领料数量
                        DataTable dct = DBHelper.GetTable("", "tb_maintain_fetch_material", "fetch_id", "maintain_id='" + txtOrder.Caption.Trim() + "' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "", "order by create_time desc ");
                         if (dct.Rows.Count > 0)//该领料单号已添加过领料单
                         {
                             for (int j = 0; j < dct.Rows.Count; j++)
                             {
                                 DataRow dfr = dct.Rows[j];
                                  strFetchId = CommonCtrl.IsNullToString(dfr["fetch_id"]);//领料单Id                                  
                                  DataTable dft = DBHelper.GetTable("领料单详细数据", "tb_maintain_fetch_material_detai", "quantity", string.Format(" fetch_id='{0}' and parts_code='{1}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strFetchId, CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)), "", "");
                                 if (dft.Rows.Count > 0)
                                 {
                                     DataRow dhr = dft.Rows[0];
                                     decHQuantity += Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dhr["quantity"])) ? CommonCtrl.IsNullToString(dhr["quantity"]) : "0");
                                 }
                             }                             
                         }
                         dgvMaterials.Rows[i].Cells["quantity"].Value = "0";
                         dgvMaterials.Rows[i].Cells["picking_num"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                         dgvMaterials.Rows[i].Cells["received_num"].Value = decHQuantity;
                         dgvMaterials.Rows[i].Cells["notFetchNum"].Value = Convert.ToDecimal(CommonCtrl.IsNullToString(dmr["quantity"]))-decHQuantity;
                         dgvMaterials.Rows[i].Cells["warehouse"].Value = "";
                    }
                }
                #endregion

                #region 删除已经删除的用料对应的领料信息
                 DataTable ddt = DBHelper.GetTable("配件数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'  and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString() + "'", strBefore_orderId), "", "");
                 if (ddt.Rows.Count > 0)
                 {
                     string strDeleteFId = string.Empty;
                     string strItemId = string.Empty;//领料单详细数据Id
                     for (int i = 0; i < ddt.Rows.Count; i++)
                     {
                         DataRow ddr = ddt.Rows[i];
                         string strPcode = CommonCtrl.IsNullToString(ddr["parts_code"]);
                         DataTable dct = DBHelper.GetTable("", "tb_maintain_fetch_material", "fetch_id", "maintain_id='" + txtOrder.Caption.Trim() + "'", "", "order by create_time desc ");
                         if (dct.Rows.Count > 0)//该领料单号已添加过领料单
                         {
                             for (int j = 0; j < dct.Rows.Count; j++)
                             {
                                 DataRow dddr = dct.Rows[j];
                                 strDeleteFId = CommonCtrl.IsNullToString(dddr["fetch_id"]);//领料单Id                                  
                                 DataTable dddt = DBHelper.GetTable("领料单详细数据", "tb_maintain_fetch_material_detai", "material_id", string.Format(" fetch_id='{0}' and parts_code='{1}'", strDeleteFId, strPcode), "", "");
                                 if (dddt.Rows.Count > 0)
                                 {
                                     DataRow ddddr = dddt.Rows[0];
                                     strItemId = CommonCtrl.IsNullToString(ddddr["material_id"]);
                                     List<string> listField = new List<string>();
                                     Dictionary<string, string> comField = new Dictionary<string, string>();
                                     listField.Add(strItemId);
                                     comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                                     DBHelper.BatchUpdateDataByIn("删除领料单详细信息", "tb_maintain_fetch_material_detai", comField, "material_id", listField.ToArray());
                                 }
                             }
                         }
                     }

                 }
                #endregion

            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion
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

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnDelete.Visible = false;
            base.btnEdit.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnExport.Visible = false;
        }
        #endregion

        #region 导入事件
        void UCFetchMaterialAddOrEdit_ImportEvent(object sender, EventArgs e)
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

        #region 取消事件
        void UCFetchMaterialAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId, "0");
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

        #region 提交事件
        void UCFetchMaterialAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            opName = "提交领料单信息";
            SaveOrSubmitMethod("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void UCFetchMaterialAddOrEdit_SaveEvent(object sender, EventArgs e)
        {

            opName = "保存领料单信息";
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
                if (!CheckControlValue()) return;
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
                    //base.addUserControl(uc, "领料单", "", this.Tag.ToString(), uc.Name);
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

        #region 领料单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            try
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                #region 基本信息
                dicParam.Add("vehicle_no", new ParamObj("vehicle_no", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//车牌号
                dicParam.Add("fetch_time", new ParamObj("fetch_time", Common.LocalDateTimeToUtcLong(dtpMakeOrderTime.Value).ToString(), SysDbType.BigInt));//领料时间
                dicParam.Add("vehicle_model", new ParamObj("vehicle_model", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarType.Tag)) ? txtCarType.Tag : null, SysDbType.VarChar, 40));//车型 
                dicParam.Add("customer_code", new ParamObj("customer_code", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Text)) ? txtCustomNO.Text.Trim() : null, SysDbType.VarChar, 40));//客户编码
                dicParam.Add("customer_name", new ParamObj("customer_name", txtCustomName.Caption.Trim(), SysDbType.NVarChar, 100));//客户名称
                dicParam.Add("linkman", new ParamObj("linkman", txtContact.Caption.Trim(), SysDbType.NVarChar, 20));//联系人
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", txtContactPhone.Caption.Trim(), SysDbType.VarChar, 15));//联系人手机 
                }
                else
                {
                    dicParam.Add("link_man_mobile", new ParamObj("link_man_mobile", null, SysDbType.VarChar, 15));//联系人手机 
                }
                dicParam.Add("fetch_opid", new ParamObj("fetch_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtFetchOpid.Tag)) ? txtFetchOpid.Tag.ToString() : null, SysDbType.VarChar, 40));//领料人
                dicParam.Add("maintain_id", new ParamObj("maintain_id", txtOrder.Caption.Trim(), SysDbType.VarChar, 40));//维修单号
                dicParam.Add("remarks", new ParamObj("remarks", txtRemark.Caption.Trim(), SysDbType.NVarChar, 200));//备注
                #endregion

                //经办人id
                dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
                //经办人
                dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedText : null, SysDbType.VarChar, 40));
                //部门
                dicParam.Add("org_id", new ParamObj("org_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
                if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号
                {
                    dicParam.Add("material_num", new ParamObj("material_num",!string.IsNullOrEmpty(labReserveNoS.Text.Trim())?labReserveNoS.Text.Trim(): CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.MaterialNo), SysDbType.VarChar, 40));//领料单号 
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    ////更新前置单据导入状态
                    //if (!string.IsNullOrEmpty(strBefore_orderId))
                    //{
                    //    UpdateMaintainInfo(listSql, strBefore_orderId, "2");
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    {
                        dicParam.Add("material_num", new ParamObj("material_num", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//领料单号 
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    }
                    else
                    {
                        //单据状态
                        dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                        dicParam.Add("material_num", new ParamObj("material_num", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//领料单号
                    }
                    ////更新前置单据导入状态
                    //if (!string.IsNullOrEmpty(strBefore_orderId))
                    //{
                    //    UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                    //}
                }

                dicParam.Add("import_status", new ParamObj("import_status", Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString(), SysDbType.VarChar, 5));

                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    strId = Guid.NewGuid().ToString();
                    dicParam.Add("fetch_id", new ParamObj("fetch_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                    dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                    dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                    obj.sqlString = @"insert into tb_maintain_fetch_material (vehicle_no,fetch_time,vehicle_model,customer_code,customer_name,linkman,link_man_mobile,fetch_opid,maintain_id
                 ,remarks,responsible_opid,responsible_name,org_id,enable_flag,info_status,material_num,fetch_id,create_by,create_name,create_time,import_status)
                 values (@vehicle_no,@fetch_time,@vehicle_model,@customer_code,@customer_name,@linkman,@link_man_mobile,@fetch_opid,@maintain_id
                 ,@remarks,@responsible_opid,@responsible_name,@org_id,@enable_flag,@info_status,@material_num,@fetch_id,@create_by,@create_name,@create_time,@import_status);";
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    dicParam.Add("fetch_id", new ParamObj("fetch_id", strId, SysDbType.VarChar, 40));//Id
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = @"update tb_maintain_fetch_material set vehicle_no=@vehicle_no,fetch_time=@fetch_time,vehicle_model=@vehicle_model,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile,fetch_opid=@fetch_opid,maintain_id=@maintain_id
                 ,remarks=@remarks,responsible_opid=@responsible_opid,responsible_name=@responsible_name,org_id=@org_id,enable_flag=@enable_flag,info_status=@info_status,material_num=@material_num,update_by=@update_by,update_name=@update_name,update_time=@update_time,import_status=@import_status
                where fetch_id=@fetch_id";
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
                        dicParam.Add("fetch_id", new ParamObj("fetch_id", partID, SysDbType.VarChar, 40));
                        dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));
                        dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                        dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? dgvr.Cells["quantity"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("inventory_num", new ParamObj("inventory_num", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["inventory_num"].Value)) ? dgvr.Cells["inventory_num"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("picking_num", new ParamObj("picking_num", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["picking_num"].Value)) ? dgvr.Cells["picking_num"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("received_num", new ParamObj("received_num", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["received_num"].Value)) ? dgvr.Cells["received_num"].Value : null, SysDbType.Decimal, 15));
                        dicParam.Add("warehouse", new ParamObj("warehouse", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["warehouse"].Value)) ? dgvr.Cells["warehouse"].Value : null, SysDbType.VarChar, 40));
                       
                        string strIsThree = CommonCtrl.IsNullToString(dgvr.Cells["three_warranty"].Value);
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", strIsThree == "是" ? "1" : "0", SysDbType.VarChar, 1));
                        }
                        else
                        {
                            dicParam.Add("three_warranty", new ParamObj("three_warranty", null, SysDbType.VarChar, 2));
                        }
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
                        dicParam.Add("data_source", new ParamObj("data_source", dgvr.Cells["Mdata_source"].Value, SysDbType.VarChar, 5)); 
                        string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                        if (strPID.Length == 0)
                        {
                            opName = "新增领料单";
                            strPID = Guid.NewGuid().ToString();
                            dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                            obj.sqlString = @"insert into tb_maintain_fetch_material_detai (fetch_id,parts_code,parts_name,unit,quantity,inventory_num
                         ,picking_num,received_num,warehouse,three_warranty,whether_imported,remarks,drawn_no,vehicle_brand,enable_flag,data_source,material_id)
                         values (@fetch_id,@parts_code,@parts_name,@unit,@quantity,@inventory_num
                         ,@picking_num,@received_num,@warehouse,@three_warranty,@whether_imported,@remarks,@drawn_no,@vehicle_brand,@enable_flag,@data_source,@material_id);";
                        }
                        else
                        {
                            dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                            opName = "更新领料单";
                            obj.sqlString = @"update tb_maintain_fetch_material_detai set fetch_id=@fetch_id,parts_code=@parts_code,parts_name=@parts_name,unit=@unit,quantity=@quantity,inventory_num=@inventory_num
                         ,picking_num=@picking_num,received_num=@received_num,warehouse=@warehouse,three_warranty=@three_warranty,whether_imported=@whether_imported,remarks=@remarks,drawn_no=@drawn_no,vehicle_brand=@vehicle_brand,enable_flag=@enable_flag
                        ,data_source=@data_source where material_id=@material_id";
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

        #region 生成领料单号创建人姓名
        /// <summary>
        /// 生成领料单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            //labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.MaterialNo);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
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
                    txtCarType.Text = frmModels.VMName;
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

        #region 领料人选择器
        /// <summary>
        /// 领料人选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFetchOpid_ChooserClick(object sender, EventArgs e)
        {
            try
            {
                frmPersonnelSelector frmPInfo = new frmPersonnelSelector();
                DialogResult result = frmPInfo.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtFetchOpid.Text = frmPInfo.strPersonName;
                    txtFetchOpid.Tag = frmPInfo.strUserId;
                }
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

        #region 窗体Load事件
        private void UCFetchMaterialAddOrEdit_Load(object sender, EventArgs e)
        {

            SetTopbuttonShow();
            dtpMakeOrderTime.Value = DateTime.Now;
            ControlsConfig.TextForPhone(txtContactPhone);
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

        #region 根据维修单ID获取信息，复制和编辑用
        /// <summary>
        /// 根据维修单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            try
            {
                #region 基础信息
                string strWhere = string.Format("fetch_id='{0}'", strId);
                DataTable dt = DBHelper.GetTable("查询领料单", "tb_maintain_fetch_material ", "*", strWhere, "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];

                #region 车辆信息
                string strCtime = CommonCtrl.IsNullToString(dr["fetch_time"]);
                if (!string.IsNullOrEmpty(strCtime))
                {
                    long Rticks = Convert.ToInt64(strCtime);
                    dtpMakeOrderTime.Value = Common.UtcLongToLocalDateTime(Rticks);//领料时间
                }
                if (wStatus == WindowStatus.Edit)
                {
                    txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                    txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型 
                    txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                }
                #endregion

                #region 客户信息
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                // txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id  
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                #endregion

                #region 领料信息
                txtFetchOpid.Text = GetSetName(CommonCtrl.IsNullToString(dr["fetch_opid"]));//领料人
                txtFetchOpid.Tag = CommonCtrl.IsNullToString(dr["fetch_opid"]);//领料人Id
                txtOrder.Caption = CommonCtrl.IsNullToString(dr["maintain_id"]);//维修单号
                txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注
                cobYHandle.SelectedValue = CommonCtrl.IsNullToString(dr["responsible_opid"]);//经办人id  
                cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["org_id"]);//部门
                #endregion
                #endregion

                #region 底部数据信息
                DataTable dlt = DBHelper.GetTable("领料数据", "tb_maintain_fetch_material_detai", "*", string.Format(" fetch_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strId), "", "");
                if (dlt.Rows.Count > 0)
                {
                    if (dlt.Rows.Count >= dgvMaterials.Rows.Count)
                    {
                        dgvMaterials.Rows.Add(dlt.Rows.Count - dgvMaterials.Rows.Count + 2);
                    }
                    else if ((dgvMaterials.Rows.Count - dlt.Rows.Count) == 1)
                    {
                        dgvMaterials.Rows.Add(1);
                    }
                    for (int i = 0; i < dlt.Rows.Count; i++)
                    {
                        DataRow dor = dlt.Rows[i];
                        dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dor["parts_code"]);
                        dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dor["parts_name"]);
                        dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dor["unit"]);                      
                        dgvMaterials.Rows[i].Cells["inventory_num"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["inventory_num"])) ? CommonCtrl.IsNullToString(dor["inventory_num"]) : "0";
                        dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dor["quantity"]);
                        string strPNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["picking_num"])) ? CommonCtrl.IsNullToString(dor["picking_num"]) : "0";
                        dgvMaterials.Rows[i].Cells["picking_num"].Value = strPNum;
                        string strRNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["received_num"])) ? CommonCtrl.IsNullToString(dor["received_num"]) : "0";
                        dgvMaterials.Rows[i].Cells["received_num"].Value = strRNum;
                        dgvMaterials.Rows[i].Cells["notFetchNum"].Value = (Convert.ToDecimal(strPNum) - Convert.ToDecimal(strRNum)).ToString();
                        dgvMaterials.Rows[i].Cells["warehouse"].Value = CommonCtrl.IsNullToString(dor["warehouse"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dor["whether_imported"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dor["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dor["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dor["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                        dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dor["material_id"]);
                        dgvMaterials.Rows[i].Cells["fetch_id"].Value = CommonCtrl.IsNullToString(dor["fetch_id"]);
                        dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dor["data_source"]);
                        dgvMaterials.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dor["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                        listMater.Add(CommonCtrl.IsNullToString(dor["parts_code"]));
                    }
                }
                #endregion

                #region 顶部按钮设置
                if (wStatus == WindowStatus.Edit)
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["material_num"])))
                    {
                        labReserveNoS.Text = CommonCtrl.IsNullToString(dr["material_num"]);//领料单号
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

        #region 获得领料人名称
        private string GetSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion

        #region 领料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int intMrows = e.RowIndex;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            if (MessageBoxEx.Show("已添加此配件,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);
                                dgvMaterials.Rows[i].Cells["quantity"].Value = "0.00";
                                //dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["brand_name"]);
                                dgvMaterials.Rows[i].Cells["three_warranty"].Value = "否";
                                dgvMaterials.Rows[i].Cells["warehouse"].Value = "";
                                dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode)&&!listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
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
            #region 配件设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_brand"].ReadOnly = true;
            dgvMaterials.Columns["inventory_num"].ReadOnly = true;
            dgvMaterials.Columns["picking_num"].ReadOnly = true;
            dgvMaterials.Columns["received_num"].ReadOnly = true;
            dgvMaterials.Columns["notFetchNum"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity" },true);
            #endregion
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
                DataTable dt = DBHelper.GetTable("", "tb_warehouse", "wh_id,wh_name", "enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", "", "order by wh_id desc");
                List<ListItem> list = new List<ListItem>();
                list.Add(new ListItem("", "请选择仓库"));
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

        #region 维修单导入
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(strBefore_orderId))
                //{
                //    List<SQLObj> listSql = new List<SQLObj>();
                //    UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                //    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                //}
                UCRepairCallbackImport CallbackImport = new UCRepairCallbackImport();
                CallbackImport.Fetchuc = this;
                CallbackImport.strTag = "1";
                CallbackImport.ShowDialog();
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 维修返修导入-根据前置单据绑定维修信息
        /// <summary>
        /// 维修返修导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData()
        {
            try
            {
                DataTable dt = DBHelper.GetTable("查询维修单单", "tb_maintain_info", "*", " maintain_id ='" + strBefore_orderId + "'", "", "");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号 
                txtCarType.Text = GetVmodel(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型 
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);
                txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
                txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
                txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
                txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
                txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
                txtOrder.Caption = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
                BindMData(strBefore_orderId);
                //List<SQLObj> listSql = new List<SQLObj>();
                //UpdateMaintainInfo(listSql, strBefore_orderId, "1");
                //DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 配件数据
        private void BindMData(string strOrderId)
        {
            try
            {
                //配件数据
                DataTable dmt = DBHelper.GetTable("配件数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'", strOrderId), "", "");
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
                        dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                        dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                        dgvMaterials.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                        dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                        dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                        string strPCode = dgvMaterials.Rows[i].Cells["parts_code"].Value.ToString();
                        DataTable dmt1 = DBHelper.GetTable("", "v_parts_chooser", "paper_count", " ser_parts_code='" + strPCode + "'", "", "");
                        if (dmt1.Rows.Count > 0)
                        {
                            DataRow dmr1 = dmt1.Rows[0];
                            dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dmr1["paper_count"]);
                        }
                        else
                        {
                            dgvMaterials.Rows[i].Cells["inventory_num"].Value = "0";
                        }
                        dgvMaterials.Rows[i].Cells["quantity"].Value ="0";
                        dgvMaterials.Rows[i].Cells["picking_num"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                        dgvMaterials.Rows[i].Cells["received_num"].Value = "0";
                        dgvMaterials.Rows[i].Cells["notFetchNum"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                        dgvMaterials.Rows[i].Cells["warehouse"].Value = "";
                    }
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

                dicParam.Add("maintain_id", new ParamObj("maintain_id", strReservId, SysDbType.VarChar, 40));
                if (status == "0")
                {
                    //保存时，前置单据被释放               
                    dicParam.Add("Import_status", new ParamObj("Import_status", "0", SysDbType.VarChar, 40));//开放
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

                }
                else if (status == "1")
                {
                    //导入时，前置单据被占用 
                    dicParam.Add("Import_status", new ParamObj("Import_status", "1", SysDbType.VarChar, 40));//占用
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";
                }
                else if (status == "2")
                {
                    //审核提交时，前置单据被锁定
                    dicParam.Add("Import_status", new ParamObj("Import_status", "2", SysDbType.VarChar, 40));//锁定
                    obj.sqlString = "update tb_maintain_info set Import_status=@Import_status where maintain_id=@maintain_id";

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

        #region 当输入的数量大于库存数量时给出提示
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value)))
                {
                    if (e.ColumnIndex == 5)
                    {
                        ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity");
                        string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                        string strWNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value.ToString() : "0";
                        if (Convert.ToDecimal(strNum) > Convert.ToDecimal(strWNum))
                        {
                            MessageBoxEx.Show("领取数量不能大于库存数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                            dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "0.00";
                            return;
                        }
                        if (!string.IsNullOrEmpty(strBefore_orderId))//导入的数据计算
                        {
                            string strShouldNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value.ToString() : "0";
                            //if (Convert.ToDecimal(strShouldNum) >= 0)
                            //{
                                if (Math.Abs(Convert.ToDecimal(strNum)) > Math.Abs(Convert.ToDecimal(strShouldNum)))
                                {
                                    MessageBoxEx.Show("领取数量不能大于未领数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "0.00";
                                    return;
                                }
                                if (Convert.ToDecimal(strNum)<0&& Convert.ToDecimal(strShouldNum)==0)
                                {
                                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "0.00";
                                    return;
                                }
                                //dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value = strNum;
                                //dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value = (Convert.ToDecimal(strShouldNum) - Convert.ToDecimal(strNum)).ToString();
                            //}
                        }
                        else
                        {
                            //普通的数据计算 
                            dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value = strNum;
                            dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value = strNum;
                            dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value = "0";
                        }
                    }
                    dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value, 2);
                    dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value = ControlsConfig.SetNewValue(dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value, 2);
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 右键添加配件信息
        private void addParts_Click(object sender, EventArgs e)
        {
            try
            {
                int intMrows = dgvMaterials.CurrentRow.Index;//当前行的索引
                frmParts frmPart = new frmParts();
                DialogResult result = frmPart.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string strPId = frmPart.PartsID;
                    DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                        {
                            if (MessageBoxEx.Show("已添加此配件,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i <= intMrows; i++)
                        {
                            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value)))
                            {
                                dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);

                                dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);

                                dgvMaterials.Rows[i].Cells["quantity"].Value = "0.00";
                                //dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["brand_name"]);
                                dgvMaterials.Rows[i].Cells["three_warranty"].Value = "否";
                                dgvMaterials.Rows[i].Cells["warehouse"].Value = "";
                                dgvMaterials.Rows[i].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[i].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                dgvMaterials.Rows.Add(1);
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                    {
                        listMater.Add(strPCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }

        }
        #endregion

        #region 右键删除配件
        private void deleteParts_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvMaterials.RowCount; i++)
                {
                    DataGridViewRow dgvr = dgvMaterials.Rows[i];
                    object isCheck = dgvr.Cells["Mcheck"].Value;
                    //将选中的删除
                    if (isCheck != null && (bool)isCheck)
                    {
                        string strItemId = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["material_id"].Value);
                        string strPartCode = CommonCtrl.IsNullToString(dgvMaterials.Rows[i].Cells["parts_code"].Value);
                        if (!string.IsNullOrEmpty(strItemId))
                        {
                            List<string> listField = new List<string>();
                            Dictionary<string, string> comField = new Dictionary<string, string>();
                            listField.Add(strItemId);
                            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                            DBHelper.BatchUpdateDataByIn("删除领料单详细信息", "tb_maintain_fetch_material_detai", comField, "material_id", listField.ToArray());
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        else
                        {
                            dgvMaterials.Rows.RemoveAt(i--);
                        }
                        listMater.Remove(strPartCode);
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
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
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    List<SQLObj> listSql = new List<SQLObj>();
                    UpdateMaintainInfo(listSql, strBefore_orderId,"0");
                    DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
                }               
            }
            return true;
        }
        #endregion

        #region 右键编辑配件
        private void editParts_Click(object sender, EventArgs e)
        {
            try
            {
                int intMrows = dgvMaterials.CurrentRow.Index;
                if (intMrows >= 0)
                {
                    string strPcode = CommonCtrl.IsNullToString(dgvMaterials.Rows[intMrows].Cells["parts_code"].Value);
                    if (!string.IsNullOrEmpty(strPcode))
                    {
                        frmParts frmPart = new frmParts();
                        DialogResult result = frmPart.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string strPId = frmPart.PartsID;
                            DataTable dpt = DBHelper.GetTable("", "v_parts_chooser", "*", " parts_id='" + strPId + "'", "", "");
                            if (dpt.Rows.Count > 0)
                            {
                                DataRow dpr = dpt.Rows[0];
                                if (listMater.Contains(CommonCtrl.IsNullToString(dpr["ser_parts_code"])))
                                {
                                    if (MessageBoxEx.Show("已添加此配件,是否继续添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                                dgvMaterials.Rows[intMrows].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                                dgvMaterials.Rows[intMrows].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                                dgvMaterials.Rows[intMrows].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                                dgvMaterials.Rows[intMrows].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);
                                dgvMaterials.Rows[intMrows].Cells["quantity"].Value = "0.00";
                                //dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                                dgvMaterials.Rows[intMrows].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                                dgvMaterials.Rows[intMrows].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["brand_name"]);
                                dgvMaterials.Rows[intMrows].Cells["three_warranty"].Value = "否";
                                dgvMaterials.Rows[intMrows].Cells["warehouse"].Value = "";
                                dgvMaterials.Rows[intMrows].Cells["Mdata_source"].Value = CommonCtrl.IsNullToString(dpr["data_source"]);
                                dgvMaterials.Rows[intMrows].Cells["three_warranty"].ReadOnly = CommonCtrl.IsNullToString(dpr["data_source"]) == Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString() ? true : false;
                                listMater.Remove(strPcode);
                                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                                {
                                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                                    if (!string.IsNullOrEmpty(strPCode) && !listMater.Contains(strPCode))
                                    {
                                        listMater.Add(strPCode);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion
    }
}
