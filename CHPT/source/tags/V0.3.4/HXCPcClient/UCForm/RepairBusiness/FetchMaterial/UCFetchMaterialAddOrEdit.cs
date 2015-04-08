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
        #endregion

        #region 初始化窗体
        public UCFetchMaterialAddOrEdit()
        {
            InitializeComponent();
            GetRepairNo();
            BindDepartment();
            SetDgvAnchor();
            BindWarehouseWay();
            BindAllocationWay();
            SetTopbuttonShow();
            base.SaveEvent += new ClickHandler(UCFetchMaterialAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCFetchMaterialAddOrEdit_SubmitEvent);
            base.CancelEvent += new ClickHandler(UCFetchMaterialAddOrEdit_CancelEvent);
            base.ImportEvent += new ClickHandler(UCFetchMaterialAddOrEdit_ImportEvent);
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
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
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
                if (string.IsNullOrEmpty(txtCarNO.Text.Trim()))
                {
                    MessageBoxEx.Show("车牌号不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomNO.Text.Trim()))
                {
                    MessageBoxEx.Show("客户编码不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
                {
                    MessageBoxEx.Show("客户名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    if (!Validator.IsMobile(txtContactPhone.Caption.Trim()))
                    {
                        MessageBoxEx.Show("联系人手机号码格式错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
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

        #region 领料单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            #region 基本信息
            dicParam.Add("vehicle_no", new ParamObj("vehicle_no", txtCarNO.Text.Trim(), SysDbType.VarChar, 20));//车牌号
            dicParam.Add("fetch_time", new ParamObj("fetch_time", Common.LocalDateTimeToUtcLong(dtpMakeOrderTime.Value).ToString(), SysDbType.BigInt));//领料时间
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
            dicParam.Add("fetch_opid", new ParamObj("fetch_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtFetchOpid.Tag)) ? txtFetchOpid.Tag.ToString() : null, SysDbType.VarChar, 40));//领料人
            dicParam.Add("maintain_id", new ParamObj("maintain_id", txtOrder.Caption.Trim(), SysDbType.VarChar, 40));//维修单号
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
                dicParam.Add("material_num", new ParamObj("material_num", labReserveNoS.Text.Trim(), SysDbType.VarChar, 40));//领料单号 
                //单据状态
                dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                //更新前置单据导入状态
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    UpdateMaintainInfo(listSql, strBefore_orderId, "2");
                }
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
                    dicParam.Add("material_num", new ParamObj("material_num", null, SysDbType.VarChar, 40));//领料单号
                }
                //更新前置单据导入状态
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                }
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
                 ,remarks,responsible_opid,responsible_name,org_name,enable_flag,info_status,material_num,fetch_id,create_by,create_name,create_time,import_status)
                 values (@vehicle_no,@fetch_time,@vehicle_model,@customer_code,@customer_name,@linkman,@link_man_mobile,@fetch_opid,@maintain_id
                 ,@remarks,@responsible_opid,@responsible_name,@org_name,@enable_flag,@info_status,@material_num,@fetch_id,@create_by,@create_name,@create_time,@import_status);";
            }
            else if (wStatus == WindowStatus.Edit)
            {
                dicParam.Add("fetch_id", new ParamObj("fetch_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update tb_maintain_fetch_material set vehicle_no=@vehicle_no,fetch_time=@fetch_time,vehicle_model=@vehicle_model,customer_code=@customer_code,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile,fetch_opid=@fetch_opid,maintain_id=@maintain_id
                 ,remarks=@remarks,responsible_opid=@responsible_opid,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,material_num=@material_num,update_by=@update_by,update_name=@update_name,update_time=@update_time,import_status=@import_status
                where fetch_id=@fetch_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 领料信息保存
        private void SaveMaterialsData(List<SQLObj> listSql, string partID)
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
                    dicParam.Add("goods_allocation", new ParamObj("goods_allocation", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["goods_allocation"].Value)) ? dgvr.Cells["goods_allocation"].Value : null, SysDbType.VarChar, 40));
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
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["material_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增领料单";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("material_id", new ParamObj("material_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = @"insert into tb_maintain_fetch_material_detai (fetch_id,parts_code,parts_name,unit,quantity,inventory_num
                         ,picking_num,received_num,warehouse,goods_allocation,three_warranty,whether_imported,remarks,drawn_no,vehicle_brand,enable_flag,material_id)
                         values (@fetch_id,@parts_code,@parts_name,@unit,@quantity,@inventory_num
                         ,@picking_num,@received_num,@warehouse,@goods_allocation,@three_warranty,@whether_imported,@remarks,@drawn_no,@vehicle_brand,@enable_flag,@material_id);";
                    }
                    else
                    {
                        dicParam.Add("material_id", new ParamObj("material_id", dgvr.Cells["material_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新领料单";
                        obj.sqlString = @"update tb_maintain_fetch_material_detai set fetch_id=@fetch_id,parts_code=@parts_code,parts_name=@parts_name,unit=@unit,quantity=@quantity,inventory_num=@inventory_num
                         ,picking_num=@picking_num,received_num=@received_num,warehouse=@warehouse,goods_allocation=@goods_allocation,three_warranty=@three_warranty,whether_imported=@whether_imported,remarks=@remarks,drawn_no=@drawn_no,vehicle_brand=@vehicle_brand,enable_flag=@enable_flag
                        where material_id=@material_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 生成领料单号创建人姓名
        /// <summary>
        /// 生成领料单号&创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReserveNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.MaterialNo);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
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
                txtCarType.Text = GetDicName(frmVehicle.strModel);
                txtCarType.Tag = frmVehicle.strModel;
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

        #region 领料人选择器
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

        #region 窗体Load事件
        private void UCFetchMaterialAddOrEdit_Load(object sender, EventArgs e)
        {
            dtpMakeOrderTime.Value = DateTime.Now;
            ControlsConfig.TextToDecimal(txtContactPhone);
           // base.SetBtnStatus(wStatus);
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
                txtCarType.Text = GetDicName(CommonCtrl.IsNullToString(dr["vehicle_model"]));//车型 
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
            cboOrgId.SelectedValue = CommonCtrl.IsNullToString(dr["org_name"]);//部门
            #endregion
            #endregion

            #region 底部数据信息
            DataTable dlt = DBHelper.GetTable("领料数据", "tb_maintain_fetch_material_detai", "*", string.Format(" fetch_id='{0}' and enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'", strId), "", "");
            if (dlt.Rows.Count > 0)
            {
                if (dlt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dlt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dlt.Rows.Count; i++)
                {
                    DataRow dor = dlt.Rows[i];
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dor["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dor["parts_name"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dor["unit"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dor["quantity"]);
                    dgvMaterials.Rows[i].Cells["inventory_num"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["inventory_num"])) ? CommonCtrl.IsNullToString(dor["inventory_num"]) : "0";
                    string strPNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["picking_num"])) ? CommonCtrl.IsNullToString(dor["picking_num"]) : "0";
                    dgvMaterials.Rows[i].Cells["picking_num"].Value = strPNum;
                    string strRNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dor["received_num"])) ? CommonCtrl.IsNullToString(dor["received_num"]) : "0";
                    dgvMaterials.Rows[i].Cells["received_num"].Value = strRNum;
                    dgvMaterials.Rows[i].Cells["notFetchNum"].Value = (Convert.ToDecimal(strPNum) + Convert.ToDecimal(strRNum)).ToString();
                    dgvMaterials.Rows[i].Cells["warehouse"].Value = CommonCtrl.IsNullToString(dor["warehouse"]);
                    dgvMaterials.Rows[i].Cells["goods_allocation"].Value = CommonCtrl.IsNullToString(dor["goods_allocation"]);
                    dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dor["whether_imported"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dor["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dor["vehicle_brand"]);
                    dgvMaterials.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dor["three_warranty"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dor["remarks"]);
                    dgvMaterials.Rows[i].Cells["material_id"].Value = CommonCtrl.IsNullToString(dor["material_id"]);
                    dgvMaterials.Rows[i].Cells["fetch_id"].Value = CommonCtrl.IsNullToString(dor["fetch_id"]);
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
            return DBHelper.GetSingleValue("获得领料人名称", "tb_customer", "cust_name", "cust_id='" + strPid + "'", "");
        }
        #endregion

        #region 领料信息读取
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listMater.Add(strPCode);
                }
            }
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
                        MessageBoxEx.Show("此配件已存在，请选择其他配件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }   
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);

                    dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);  
                   
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "1";
                    //dgvMaterials.Rows[e.RowIndex].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["brand_name"]);
                    dgvMaterials.Rows.Add(1);
                }
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
            ControlsConfig.NumberLimitdgv(dgvMaterials, new List<string>() { "quantity" });
            #endregion
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
            warehouse.DataSource = list;
            warehouse.ValueMember = "Value";
            warehouse.DisplayMember = "Text";
        }
        #endregion

        #region 绑定货位信息
        /// <summary>
        /// 绑定货位信息
        /// </summary>
        private void BindAllocationWay()
        {
            DataTable dgt = DBHelper.GetTable("", "tb_cargo_space", "cs_id,cs_name", "", "", "");
            List<ListItem> listg = new List<ListItem>();
            listg.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dgt.Rows)
            {
                listg.Add(new ListItem(dr["cs_id"], dr["cs_name"].ToString()));
            }
            goods_allocation.DataSource = listg;
            goods_allocation.ValueMember = "Value";
            goods_allocation.DisplayMember = "Text";
        }
        #endregion

        #region 维修单导入
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strBefore_orderId))
            {
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据状体", listSql);
            }
            UCRepairCallbackImport CallbackImport = new UCRepairCallbackImport();
            CallbackImport.Fetchuc = this;
            CallbackImport.strTag = "1";
            CallbackImport.ShowDialog();
        }
        #endregion

        #region 维修返修导入-根据前置单据绑定维修信息
        /// <summary>
        /// 维修返修导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData()
        {
            DataTable dt = DBHelper.GetTable("查询维修单单", "tb_maintain_info", "*", " maintain_id ='" + strBefore_orderId + "'", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            txtCarNO.Text = CommonCtrl.IsNullToString(dr["vehicle_no"]);//车牌号           
            txtCarType.Text = CommonCtrl.IsNullToString(dr["vehicle_model"]);//车型 
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
            txtOrder.Caption = CommonCtrl.IsNullToString(dr["maintain_no"]);//维修单号
            BindMData(strBefore_orderId);
            List<SQLObj> listSql = new List<SQLObj>();
            UpdateMaintainInfo(listSql, strBefore_orderId, "1");
            DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
        }
        #endregion

        #region 配件数据
        private void BindMData(string strOrderId)
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
                    dgvMaterials.Rows[i].Cells["quantity"].Value = "0";
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dmr["vehicle_brand"]);
                    dgvMaterials.Rows[i].Cells["three_warranty"].Value = CommonCtrl.IsNullToString(dmr["three_warranty"]) == "1" ? "是" : "否";
                    dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dmr["whether_imported"]) == "1" ? "是" : "否";
                    string strPCode = dgvMaterials.Rows[i].Cells["parts_code"].Value.ToString();
                    DataTable dmt1 = DBHelper.GetTable("", "tb_parts_stock_inout_p", "counts", " parts_code='" + strPCode + "'", "", "");
                    if (dmt1.Rows.Count > 0)
                    {
                        DataRow dmr1 = dmt.Rows[0];
                        dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dmr1["counts"]);
                    }
                    dgvMaterials.Rows[i].Cells["picking_num"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["received_num"].Value = "0";
                    dgvMaterials.Rows[i].Cells["notFetchNum"].Value = "0";
                }
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
        #endregion

        #region 当输入的数量大于库存数量时给出提示
        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value)))
            {
                if (e.ColumnIndex == 4)
                {
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                    string strWNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value.ToString() : "0";
                    if (Convert.ToDecimal(strNum) > Convert.ToDecimal(strWNum))
                    {
                        MessageBoxEx.Show("领取数量不能大于库存数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                    if (!string.IsNullOrEmpty(strBefore_orderId))//导入的数据计算
                    {
                        string strShouldNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value.ToString() : "0";
                        if (Convert.ToDecimal(strShouldNum) > 0)
                        {
                            if (Convert.ToDecimal(strNum) > Convert.ToDecimal(strShouldNum))
                            {
                                MessageBoxEx.Show("领取数量不能大于应领数量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                                return;
                            }
                            dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value = strNum;
                            dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value = (Convert.ToDecimal(strShouldNum) - Convert.ToDecimal(strNum)).ToString();
                        }
                    }
                    else
                    {
                        //普通的数据计算 
                        dgvMaterials.Rows[e.RowIndex].Cells["picking_num"].Value = strNum;
                        dgvMaterials.Rows[e.RowIndex].Cells["received_num"].Value = strNum;
                        dgvMaterials.Rows[e.RowIndex].Cells["notFetchNum"].Value = "0";
                    }
                }
            }
        }
        #endregion

        #region 右键添加配件信息
        private void addParts_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
            {
                string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                if (!string.IsNullOrEmpty(strPCode))
                {
                    listMater.Add(strPCode);
                }
            }
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
                        MessageBoxEx.Show("此配件已存在，请选择其他配件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dgvMaterials.CurrentRow.Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.CurrentRow.Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.CurrentRow.Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);

                    dgvMaterials.CurrentRow.Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);

                    dgvMaterials.CurrentRow.Cells["quantity"].Value = "1";
                    //dgvMaterials.CurrentRow.Cells["whether_imported"].Value = CommonCtrl.IsNullToString(dpr["is_import"]) == "1" ? "是" : "否";
                    dgvMaterials.CurrentRow.Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.CurrentRow.Cells["vehicle_brand"].Value = CommonCtrl.IsNullToString(dpr["brand_name"]);
                    dgvMaterials.Rows.Add(1);
                }
            }

        }
        #endregion

        #region 右键删除配件
        private void deleteParts_Click(object sender, EventArgs e)
        {
            int introw = dgvMaterials.CurrentRow.Index;
            if (introw < 0)
            {
                return;
            }
            if (dgvMaterials.Rows.Count <= 1)
            {
                MessageBoxEx.Show("至少保留一条配件信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strItemId = CommonCtrl.IsNullToString(dgvMaterials.CurrentRow.Cells["material_id"].Value);
            if (!string.IsNullOrEmpty(strItemId))
            {
                List<string> listField = new List<string>();
                Dictionary<string, string> comField = new Dictionary<string, string>();
                listField.Add(strItemId);
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                DBHelper.BatchUpdateDataByIn("删除配件", "tb_maintain_fetch_material_detai", comField, "material_id", listField.ToArray());
                dgvMaterials.Rows.RemoveAt(introw);
            }
            else
            {
                if (introw != dgvMaterials.Rows.Count - 1)
                {
                    dgvMaterials.Rows.RemoveAt(introw);
                }
            }
        }
        #endregion
    }
}
