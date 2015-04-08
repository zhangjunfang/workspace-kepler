using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.Reserve
{
    /// <summary>
    /// 维修管理—预约单添加/修改
    /// Author：JC
    /// AddTime：2014.9.28
    /// </summary>
    public partial class ReserveOrderAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public ReserveOrder uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 预约单ID
        /// </summary>
        public string strId = string.Empty;
        string keyName = string.Empty;
        string keyValue = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public ReserveOrderAddOrEdit()
        {
            InitializeComponent();
            GetReserveNo();
            SetDgvAnchor();
            initializeData();
            BindDepartment();
            SetTopbuttonShow();
            dtpReTime.Value = DateTime.Now;
            dtpInTime.Value = DateTime.Now;
            base.CancelEvent += new ClickHandler(ReserveOrderAddOrEdit_CancelEvent);
            base.SaveEvent += new ClickHandler(ReserveOrderAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(ReserveOrderAddOrEdit_SubmitEvent);


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
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnImport.Visible = false;
            base.btnVerify.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 提交功能
        void ReserveOrderAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            SaveAndSubmit("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void ReserveOrderAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            SaveAndSubmit("保存", DataSources.EnumAuditStatus.DRAFT);
        }
        #endregion

        #region 保存、提交功能
        /// <summary>
        /// 保存、提交功能
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="status">单据状态</param>
        private void SaveAndSubmit(string strMessage, DataSources.EnumAuditStatus status)
        {
            try
            {
                string currCom_id = string.Empty;//当前信息编号               
                #region 必要的判断
                //是否接车，默认不接
                string strIsGreet = "0";
                if (ckbYes.Checked)
                {
                    strIsGreet = "1";
                }
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    MessageBoxEx.Show("车牌号不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtRepPerson.Caption.Trim()))
                {
                    MessageBoxEx.Show("预约人不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!string.IsNullOrEmpty(txtRepPersonPhone.Caption.Trim()))//预约人手机
                {
                    if (!Validator.IsMobile(txtRepPersonPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("预约人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBoxEx.Show("预约人手机不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                #endregion
                if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                string opName = "新增预约单";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();
                #region 表字段赋值

                dicFileds.Add("reservation_date", Common.LocalDateTimeToUtcLong(dtpReTime.Value).ToString());//预约日期
                dicFileds.Add("vehicle_no", CommonCtrl.IsNullToString(txtCarNO.Text.Trim()));//车牌号
                dicFileds.Add("vehicle_vin", txtVIN.Caption.Trim());//VIN
                dicFileds.Add("engine_type", txtEngineNo.Caption.Trim());//发动机号
                dicFileds.Add("vehicle_model", CommonCtrl.IsNullToString(txtCarType.Tag));//车型
                dicFileds.Add("vehicle_brand", cobCarBrand.SelectedValue != null ? cobCarBrand.SelectedValue.ToString() : "");//车辆品牌               
                dicFileds.Add("vehicle_color", cobColor.SelectedValue != null ? cobColor.SelectedValue.ToString() : "");//颜色
                dicFileds.Add("customer_code", txtCustomNO.Text.Trim());//客户编码
                dicFileds.Add("customer_name", txtCustomName.Caption.Trim());//客户名称 
                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    dicFileds.Add("customer_id", CommonCtrl.IsNullToString(txtCustomNO.Tag));//客户关联id
                }
                dicFileds.Add("linkman", txtContact.Caption.Trim());//联系人
                dicFileds.Add("link_man_mobile", txtContactPhone.Caption.Trim());
                dicFileds.Add("maintain_payment", cobPayType.SelectedValue != null ? cobPayType.SelectedValue.ToString() : "");//维修付费方式
                dicFileds.Add("maintain_type", cobRepType.SelectedValue != null ? cobRepType.SelectedValue.ToString() : "");//维修类别
                dicFileds.Add("reservation_man", txtRepPerson.Caption.Trim());//预约人                
                dicFileds.Add("reservation_mobile", txtRepPersonPhone.Caption.Trim());//预约人手机               
                dicFileds.Add("whether_greet", strIsGreet);//是否接车,1是0否
                dicFileds.Add("greet_site", txtAddress.Caption.Trim());//接车地址                
                dicFileds.Add("maintain_time", Common.LocalDateTimeToUtcLong(dtpInTime.Value).ToString());//预约进场时间
                dicFileds.Add("fault_describe", txtDesc.Caption.Trim());//故障描述
                dicFileds.Add("remark", txtRemark.Caption.Trim());//备注               
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)))
                {
                    dicFileds.Add("responsible_opid", cobYHandle.SelectedValue.ToString());//经办人id  
                    dicFileds.Add("responsible_name", CommonCtrl.IsNullToString(cobYHandle.SelectedText));//经办人
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)))
                {
                    dicFileds.Add("org_name", cboOrgId.SelectedValue.ToString());//部门
                }
                dicFileds.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString());//信息状态（1|激活；2|作废；0|删除） 
                if (status == DataSources.EnumAuditStatus.SUBMIT)//提交时生成预约单号
                {
                    dicFileds.Add("reservation_no", labReserveNoS.Text);//预约单号
                    dicFileds.Add("document_status", Convert.ToInt32(status).ToString());//单据状态（0为草稿）
                }
                else
                {
                    if (!string.IsNullOrEmpty(strStatus) && strStatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                    {
                        dicFileds.Add("reservation_no", labReserveNoS.Text.Trim());//预约单号
                        dicFileds.Add("document_status", Convert.ToInt32(status).ToString());//单据状态0为草稿
                    }
                    else
                    {
                        dicFileds.Add("document_status", Convert.ToInt32(status).ToString());//单据状态0为草稿
                    }
                }
                dicFileds.Add("Import_status", Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString());//导入状态

                #endregion
                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    currCom_id = Guid.NewGuid().ToString();
                    dicFileds.Add("reserv_id", currCom_id);////新ID
                    dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);//创建人id（制单人） 
                    dicFileds.Add("create_name", HXCPcClient.GlobalStaticObj.UserName);//创建人
                    dicFileds.Add("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//创建时间
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    keyName = "reserv_id";
                    keyValue = strId;
                    currCom_id = strId;
                    opName = "更新预约单";
                    dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//最后修改人id 
                    dicFileds.Add("update_name", HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                    dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间
                }
                bool bln = DBHelper.Submit_AddOrEdit(opName, "tb_maintain_reservation", keyName, keyValue, dicFileds);
                if (bln)
                {
                    SaveProjectData(currCom_id);
                    SaveMaterialsData(currCom_id);
                    MessageBoxEx.Show("" + strMessage + "成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("" + strMessage + "失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("" + strMessage + "失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 维修项目数据保存
        /// <summary>
        /// 维修项目数据保存
        /// </summary>
        /// <param name="strComID">预约单号</param>
        private void SaveProjectData(string strComID)
        {
            string opName = "新增预约单维修项目";
            Dictionary<string, string> dicPFileds = new Dictionary<string, string>();
            if (dgvproject.Rows.Count > 0)
            {
                for (int i = 0; i < dgvproject.Rows.Count; i++)
                {
                    if (dgvproject.Rows[i].Cells["item_name"].Value != null)
                    {
                        dicPFileds.Add("item_no", dgvproject.Rows[i].Cells["item_no"].Value.ToString());
                        dicPFileds.Add("item_name", dgvproject.Rows[i].Cells["item_name"].Value.ToString());
                        dicPFileds.Add("item_type", dgvproject.Rows[i].Cells["item_type"].Value.ToString());
                        dicPFileds.Add("man_hour_quantity", dgvproject.Rows[i].Cells["man_hour_quantity"].Value.ToString());
                        dicPFileds.Add("man_hour_norm_unitprice", dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value.ToString());
                        dicPFileds.Add("sum_money", dgvproject.Rows[i].Cells["sum_money"].Value.ToString());
                        string strType = dgvproject.Rows[i].Cells["man_hour_type"].Value != null ? dgvproject.Rows[i].Cells["man_hour_type"].Value.ToString() : "";
                        if (!string.IsNullOrEmpty(strType))
                        {
                            dicPFileds.Add("man_hour_type", dgvproject.Rows[i].Cells["man_hour_type"].Value.ToString());
                        }
                        string strIsThree = dgvproject.Rows[i].Cells["three_warranty"].Value != null ? dgvproject.Rows[i].Cells["three_warranty"].Value.ToString() : "";
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicPFileds.Add("three_warranty", strIsThree = strIsThree == "是" ? "1" : "0");
                        }
                        dicPFileds.Add("remarks", dgvproject.Rows[i].Cells["remarks"].Value.ToString());
                        if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                        {

                            dicPFileds.Add("item_id", Guid.NewGuid().ToString());////新ID
                            dicPFileds.Add("maintain_id", strComID);//关联预约单ID    
                            keyName = string.Empty;
                            keyValue = string.Empty;
                        }
                        else if (wStatus == WindowStatus.Edit)
                        {
                            if (DBHelper.IsExist("预约单维修项目是否存在", "tb_maintain_item", " maintain_id='" + strComID + "'"))
                            {
                                string strId = dgvproject.Rows[i].Cells["item_id"].Value != null ? dgvproject.Rows[i].Cells["item_id"].Value.ToString() : "";
                                if (!string.IsNullOrEmpty(strId))
                                {
                                    //if (DBHelper.IsExist("预约单维修项目是否存在", "tb_maintain_item", " item_id='" + strId + "'"))
                                    //{
                                    keyName = "item_id";
                                    keyValue = strId;
                                    opName = "更新预约单维修项目";
                                    //}
                                }
                                else
                                {
                                    dicPFileds.Add("item_id", Guid.NewGuid().ToString());////新ID
                                    dicPFileds.Add("maintain_id", strComID);//关联预约单ID    
                                    keyName = string.Empty;
                                    keyValue = string.Empty;
                                }
                            }
                            else
                            {
                                dicPFileds.Add("item_id", Guid.NewGuid().ToString());////新ID
                                dicPFileds.Add("maintain_id", strComID);//关联预约单ID    
                                keyName = string.Empty;
                                keyValue = string.Empty;
                            }
                        }
                        DBHelper.Submit_AddOrEdit(opName, "tb_maintain_item", keyName, keyValue, dicPFileds);
                        dicPFileds.Clear();
                    }
                }

            }
        }
        #endregion

        #region 维修用料信息保存
        private void SaveMaterialsData(string strComID)
        {
            string opName = "新增预约单维修用料";
            Dictionary<string, string> dicMFileds = new Dictionary<string, string>();
            if (dgvMaterials.Rows.Count > 0)
            {
                for (int i = 0; i < dgvMaterials.Rows.Count; i++)
                {
                    if (dgvMaterials.Rows[i].Cells["parts_name"].Value != null)
                    {

                        dicMFileds.Add("parts_code", dgvMaterials.Rows[i].Cells["parts_code"].Value.ToString());
                        dicMFileds.Add("parts_name", dgvMaterials.Rows[i].Cells["parts_name"].Value.ToString());
                        dicMFileds.Add("norms", dgvMaterials.Rows[i].Cells["norms"].Value.ToString());
                        dicMFileds.Add("unit", dgvMaterials.Rows[i].Cells["unit"].Value.ToString());
                        dicMFileds.Add("quantity", dgvMaterials.Rows[i].Cells["quantity"].Value.ToString());
                        dicMFileds.Add("unit_price", dgvMaterials.Rows[i].Cells["unit_price"].Value.ToString());
                        dicMFileds.Add("sum_money", dgvMaterials.Rows[i].Cells["Msum_money"].Value.ToString());
                        dicMFileds.Add("drawn_no", dgvMaterials.Rows[i].Cells["drawn_no"].Value.ToString());
                        dicMFileds.Add("vehicle_model", dgvMaterials.Rows[i].Cells["vehicle_model"].Value.ToString());

                        string strIsimport = dgvMaterials.Rows[i].Cells["whether_imported"].Value != null ? dgvMaterials.Rows[i].Cells["whether_imported"].Value.ToString() : "";
                        if (!string.IsNullOrEmpty(strIsimport))
                        {
                            dicMFileds.Add("whether_imported", strIsimport = strIsimport == "是" ? "1" : "0");
                        }

                        string strIsThree = dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value != null ? dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value.ToString() : "";
                        if (!string.IsNullOrEmpty(strIsThree))
                        {
                            dicMFileds.Add("three_warranty", strIsThree = strIsThree == "是" ? "1" : "0");
                        }
                        dicMFileds.Add("remarks", dgvMaterials.Rows[i].Cells["Mremarks"].Value.ToString());
                        if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                        {

                            dicMFileds.Add("material_id", Guid.NewGuid().ToString());////新ID
                            dicMFileds.Add("maintain_id", strComID);//关联预约单ID    
                            keyName = string.Empty;
                            keyValue = string.Empty;
                        }
                        else if (wStatus == WindowStatus.Edit)
                        {
                            if (DBHelper.IsExist("预约单维修用料是否存在", "tb_maintain_material_detail", " maintain_id='" + strComID + "'"))
                            {
                                string strId = dgvMaterials.Rows[i].Cells["material_id"].Value != null ? dgvMaterials.Rows[i].Cells["material_id"].Value.ToString() : "";
                                if (!string.IsNullOrEmpty(strId))
                                {
                                    //if (DBHelper.IsExist("预约单维修项目是否存在", "tb_maintain_item", " item_id='" + strId + "'"))
                                    //{
                                    keyName = "material_id";
                                    keyValue = strId;
                                    opName = "更新预约单维修用料";
                                    //}
                                }
                                else
                                {
                                    dicMFileds.Add("material_id", Guid.NewGuid().ToString());////新ID
                                    dicMFileds.Add("maintain_id", strComID);//关联预约单ID    
                                    keyName = string.Empty;
                                    keyValue = string.Empty;
                                }
                            }
                            else
                            {
                                dicMFileds.Add("material_id", Guid.NewGuid().ToString());////新ID
                                dicMFileds.Add("maintain_id", strComID);//关联预约单ID    
                                keyName = string.Empty;
                                keyValue = string.Empty;
                            }
                        }
                        DBHelper.Submit_AddOrEdit(opName, "tb_maintain_material_detail", keyName, keyValue, dicMFileds);
                        dicMFileds.Clear();
                    }
                }

            }
        }
        #endregion

        #region 取消事件
        void ReserveOrderAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region 生成预约单号&创建人姓名
        /// <summary>
        /// 生成预约单号&创建人姓名
        /// </summary>
        private void GetReserveNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Reserve);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
        }
        #endregion

        #region 获取当前用户欠款金额

        #endregion

        #region 初始化车辆品牌、颜色、维修付费方式、维修类别信息
        /// <summary>
        /// 初始化车型、车辆品牌、颜色、维修付费方式、维修类别信息
        /// </summary>
        private void initializeData()
        {
            CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定颜色
            CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式
            CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//绑定维修类别            
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
            frmVehicleGrade frmVehicle = new frmVehicleGrade();
            DialogResult result = frmVehicle.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarNO.Text = frmVehicle.strLicensePlate;
                txtVIN.Caption = frmVehicle.strVIN;
                txtEngineNo.Caption = frmVehicle.strEngineNum;
                txtCarType.Text = GetDicName(frmVehicle.strModel);
                txtCarType.Tag = frmVehicle.strModel;
                cobCarBrand.SelectedValue = frmVehicle.strBrand;
                cobColor.SelectedValue = frmVehicle.strColor;
                txtCustomNO.Text = frmVehicle.strCustCode;
                txtCustomNO.Tag = frmVehicle.strCustId;
                txtCustomName.Caption = frmVehicle.strCustName;
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
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
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
                txtContact.Caption = frmCInfo.strLegalPerson;
            }
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {

            #region 维修项目信息设置
            dgvproject.Dock = DockStyle.Fill;
            //dgvproject.Columns["colCheck"].HeaderText = "选择";
            dgvproject.ReadOnly = false;
            dgvproject.Rows.Add(3);
            dgvproject.AllowUserToAddRows = true;
            dgvproject.Columns["item_no"].ReadOnly = true;
            dgvproject.Columns["item_type"].ReadOnly = true;
            dgvproject.Columns["item_name"].ReadOnly = true;
            dgvproject.Columns["man_hour_norm_unitprice"].ReadOnly = true;
            dgvproject.Columns["sum_money"].ReadOnly = true;
            dgvproject.Columns["man_hour_type"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvproject, new List<string>() { "man_hour_quantity" });

            #endregion

            #region 维修用料设置
            dgvMaterials.Dock = DockStyle.Fill;
            //dgvMaterials.Columns["MCheck"].HeaderText = "选择";
            dgvMaterials.ReadOnly = false;
            dgvMaterials.Rows.Add(3);
            dgvMaterials.AllowUserToAddRows = true;
            dgvMaterials.Columns["parts_code"].ReadOnly = true;
            dgvMaterials.Columns["parts_name"].ReadOnly = true;
            dgvMaterials.Columns["norms"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["whether_imported"].ReadOnly = true;
            dgvMaterials.Columns["Msum_money"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["vehicle_model"].ReadOnly = true;
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity", "unit_price" });
            #endregion
        }
        #endregion

        #region 窗体Load事件
        private void ReserveOrderAddOrEdit_Load(object sender, EventArgs e)
        {
            string[] strPArrary = { "man_hour_quantity", "sum_money" };
            ControlsConfig.DatagGridViewTotalConfig(dgvproject, strPArrary);
            string[] strMArrary = { "quantity", "Msum_money" };
            ControlsConfig.DatagGridViewTotalConfig(dgvMaterials, strMArrary);
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

        #region 根据预约单ID获取信息，复制和编辑用
        /// <summary>
        /// 根据预约单ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("reserv_id='{0}'", strId);
            DataTable dt = DBHelper.GetTable("查询预约单", "tb_maintain_reservation", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];

            long Rticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["reservation_date"]));
            dtpReTime.Value = Common.UtcLongToLocalDateTime(Rticks);//预约日期   
            if (wStatus == WindowStatus.Edit)
            {
                txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtCarNO.Tag = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号
                txtVIN.Caption = CommonCtrl.IsNullToString(dr["vehicle_vin"]);//VIN
                txtEngineNo.Caption = CommonCtrl.IsNullToString(dr["engine_type"]);//发动机号
                txtCarType.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型   
                txtCarType.Tag = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型   
                cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_brand"]);//车辆品牌
                cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["vehicle_color"]);//颜色  
            }
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
            cobPayType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_payment"]);//维修付费方式
            cobRepType.SelectedValue = CommonCtrl.IsNullToString(dr["maintain_type"]);//维修类别
            txtRepPerson.Caption = CommonCtrl.IsNullToString(dr["reservation_man"]);//预约人
            txtRepPersonPhone.Caption = CommonCtrl.IsNullToString(dr["reservation_mobile"]);//预约人手机
            string strIsGreet = CommonCtrl.IsNullToString(dr["whether_greet"]);
            if (strIsGreet == "1")//是否接车,1是0否
            {
                ckbYes.Checked = true;
            }
            else
            {
                ckbYes.Checked = false;
            }
            txtAddress.Caption = CommonCtrl.IsNullToString(dr["greet_site"]);//接车地址  
            long ticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["maintain_time"]));
            dtpInTime.Value = Common.UtcLongToLocalDateTime(ticks); //预约进场时间           
            txtDesc.Caption = CommonCtrl.IsNullToString(dr["fault_describe"]);//故障描述
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注
            //cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["responsible_name"]);//经办人
            cobYHandle.SelectedValue = CommonCtrl.IsNullToString(dr["responsible_opid"]);//经办人id  
            cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["org_name"]);//部门

            if (wStatus == WindowStatus.Edit)
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["reservation_no"])))
                {
                    labReserveNoS.Text = CommonCtrl.IsNullToString(dr["reservation_no"]);//预约单号
                }
                else
                {
                    labReserveNoS.Visible = false;
                }
                strStatus = CommonCtrl.IsNullToString(dr["document_status"]);//单据状态
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
                //    //审核没通过时屏蔽审核按钮
                //    base.btnVerify.Enabled = false;
                //}
            }
            else
            {
                labReserveNoS.Visible = false;
            }
            #endregion

            #region 底部datagridview数据

            #region 维修项目数据
            //维修项目数据                
            DataTable dpt = DBHelper.GetTable("维修项目数据", "tb_maintain_item", "*", string.Format(" maintain_id='{0}'", strId), "", "");
            if (dpt.Rows.Count > 0)
            {
                if (dpt.Rows.Count > dgvproject.Rows.Count)
                {
                    dgvproject.Rows.Add(dpt.Rows.Count - dgvproject.Rows.Count + 1);
                }
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dpr = dpt.Rows[i];
                    dgvproject.Rows[i].Cells["item_id"].Value = CommonCtrl.IsNullToString(dpr["item_id"]);
                    dgvproject.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dpr["three_warranty"]) == "1" ? "是" : "否";
                    dgvproject.Rows[i].Cells["man_hour_type"].Value = CommonCtrl.IsNullToString(dpr["man_hour_type"]);
                    dgvproject.Rows[i].Cells["item_no"].Value = CommonCtrl.IsNullToString(dpr["item_no"]);
                    dgvproject.Rows[i].Cells["item_name"].Value = CommonCtrl.IsNullToString(dpr["item_name"]);
                    dgvproject.Rows[i].Cells["item_type"].Value = CommonCtrl.IsNullToString(dpr["item_type"]);
                    dgvproject.Rows[i].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["man_hour_quantity"]);
                    dgvproject.Rows[i].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["man_hour_norm_unitprice"]);
                    dgvproject.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dpr["remarks"]);
                    dgvproject.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dpr["sum_money"]);

                }
            }
            #endregion

            #region 维修用料数据
            //维修用料数据
            DataTable dmt = DBHelper.GetTable("维修用料数据", "tb_maintain_material_detail", "*", string.Format(" maintain_id='{0}'", strId), "", "");
            if (dmt.Rows.Count > 0)
            {

                if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dmt.Rows.Count; i++)
                {
                    DataRow dmr = dmt.Rows[i];
                    dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dmr["material_id"]);
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                    dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                    dgvMaterials.Rows[i].Cells["Msum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["vehicle_model"].Value = CommonCtrl.IsNullToString(dmr["vehicle_model"]);
                    dgvMaterials.Rows[i].Cells["Mthree_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";

                }
            }
            #endregion
            #endregion
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

        #region 绑定部门
        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindDepartment()
        {
            DataTable dt = DBHelper.GetTable("", "tb_organization", "org_id,org_name", "", "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["org_id"], dr["org_name"].ToString()));
            }
            cboOrgId.DataSource = list;
            cboOrgId.ValueMember = "Value";
            cboOrgId.DisplayMember = "Text";
        }
        #endregion

        #region 绑定经办人
        /// <summary>
        /// 绑定经办人
        /// </summary>
        /// <param name="strDepartId">部门Id</param>
        private void BindHandle(ComboBox cobox, string strDepartId = null)
        {
            string strWhere = !string.IsNullOrEmpty(strDepartId) ? string.Format("org_id='{0}'", strDepartId) : "";
            DataTable dt = DBHelper.GetTable("", "sys_user", "user_id,user_name", strWhere, "", "");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
            }
            cobox.DataSource = list;
            cobox.ValueMember = "Value";
            cobox.DisplayMember = "Text";
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
            if (cboOrgId.SelectedValue == null)
            {
                return;
            }
            BindHandle(cobYHandle, cboOrgId.SelectedValue.ToString());
        }
        #endregion

        #region 维修项目信息读取
        private void dgvproject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmWorkHours frmHours = new frmWorkHours();
            DialogResult result = frmHours.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvproject.Rows[e.RowIndex].Cells["item_no"].Value = frmHours.strProjectNum;
                dgvproject.Rows[e.RowIndex].Cells["item_type"].Value = GetDicName(frmHours.strRepairType);
                dgvproject.Rows[e.RowIndex].Cells["item_name"].Value = frmHours.strProjectName;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value = frmHours.strWhoursType == "1" ? "工时" : "定额";
                //if (frmHours.strWhoursType == "2")//定额
                //{
                //    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = "";
                //    //dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = true;
                //}
                //else
                //{
                //    //工时
                //    dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                //    //dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = false;
                //}
                dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = frmHours.strWhoursNum;
                dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = frmHours.strQuotaPrice;
                dgvproject.Rows[e.RowIndex].Cells["remarks"].Value = frmHours.strRemark;
                dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value = frmHours.strWhours_id;
                dgvproject.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(frmHours.strWhoursNum) * Convert.ToDecimal(frmHours.strQuotaPrice));
                dgvproject.Rows.Add(1);
            }
        }

        /// <summary>
        /// 工时数量发生改变时金额也发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvproject_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            //是否三包值
            string strIsThree = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["three_warranty"].Value);
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["item_name"].Value)))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value)))//工时
                {
                    if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                    {
                        ControlsConfig.SetCellsValue(dgvproject, e.RowIndex, "man_hour_quantity,man_hour_norm_unitprice");
                        string strNum = dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value != null ? dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value.ToString() : "0";
                        string strMoney = dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value != null ? dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value.ToString() : "0";
                        dgvproject.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strMoney = strMoney == "" ? "0" : strMoney));
                    }
                }
            }
            if (e.ColumnIndex == 8)
            {
                if (CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["man_hour_type"].Value) == "工时")
                {
                    //工时时设置数量和金额均可修改
                    dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].ReadOnly = strIsThree == "否" ? false : true;
                }
                dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].ReadOnly = strIsThree == "否" ? false : true;
                if (strIsThree == "是")
                {
                    string strOId = CommonCtrl.IsNullToString(dgvproject.Rows[e.RowIndex].Cells["OldItem_id"].Value);
                    if (!string.IsNullOrEmpty(strOId))
                    {
                        DataTable dot = DBHelper.GetTable("", "v_workhours_users", "*", "whours_id='" + strOId + "'", "", "");
                        if (dot.Rows.Count > 0)
                        {
                            DataRow dpr = dot.Rows[0];
                            dgvproject.Rows[e.RowIndex].Cells["man_hour_quantity"].Value = CommonCtrl.IsNullToString(dpr["whours_num_a"]);
                            dgvproject.Rows[e.RowIndex].Cells["man_hour_norm_unitprice"].Value = CommonCtrl.IsNullToString(dpr["quota_price"]);
                            dgvproject.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["whours_num_a"])) ? CommonCtrl.IsNullToString(dpr["whours_num_a"]) : "0") * Convert.ToDecimal(!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["quota_price"])) ? CommonCtrl.IsNullToString(dpr["quota_price"]) : "0"));
                        }
                    }
                }
            }
        }
        #endregion

        #region 维修用料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                string strPId = frmPart.PartsID;
                DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strPId + "'", "", "");
                if (dpt.Rows.Count > 0)
                {
                    DataRow dpr = dpt.Rows[0];

                    dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["default_unit_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                    string strNum = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value);
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Msum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["vehicle_model"].Value = CommonCtrl.IsNullToString(dpr["v_brand_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["Mremarks"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value = frmPart.PartsID;
                    dgvMaterials.Rows.Add(1);
                }
            }
        }

        /// <summary>
        /// 数量、原始单价发生改变时金额也跟着改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string strIsThree = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["Mthree_warranty"].Value);
            if (dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value != null)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    //if (strIsThree == "否")
                    //{                   
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                    string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["Msum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    //}
                }
            }
            if (e.ColumnIndex == 11)
            {
                if (strIsThree == "否")
                {
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].ReadOnly = false;
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = false;
                }
                else if (strIsThree == "是")
                {
                    dgvMaterials.Columns["quantity"].ReadOnly = false;
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].ReadOnly = true;
                    string strMId = CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["M_Id"].Value);
                    DataTable dpt = DBHelper.GetTable("", "v_parts", "*", " parts_id='" + strMId + "'", "", "");
                    if (dpt.Rows.Count > 0)
                    {
                        DataRow dpr = dpt.Rows[0];
                        dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dpr["highest_out_price"]);
                        string strUMoney = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value) : "0";
                        string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value) : "0";
                        dgvMaterials.Rows[e.RowIndex].Cells["Msum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(strUMoney = strUMoney == "" ? "0" : strUMoney));
                    }
                }
            }
        }
        #endregion

    }
}
