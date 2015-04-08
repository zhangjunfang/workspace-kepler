using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using HXCPcClient.UCForm.RepairBusiness.RepairCallback;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsSend
{
    /// <summary>
    /// 旧件管理-旧件发货单新增编辑
    /// Author：JC
    /// AddTime：2014.11.05
    /// </summary>
    public partial class UCOldPartsSendAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOldPartsSendManager uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 救援单ID
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
        /// 业务单号-导入数据时用
        /// </summary>
        string strYWOrders = string.Empty;
        /// <summary>
        /// 车牌号-导入数据时用
        /// </summary>
        string strVehicleNo = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public UCOldPartsSendAddOrEdit()
        {
            InitializeComponent();
            BindDepartment();
            GetRepairNo();
            SetTopbuttonShow();
            SetDgvAnchor();
            base.SaveEvent += new ClickHandler(UCOldPartsReceiptAddOrEdit_SaveEvent);
            base.SubmitEvent += new ClickHandler(UCOldPartsReceiptAddOrEdit_SubmitEvent);
            base.ImportEvent += new ClickHandler(UCOldPartsReceiptAddOrEdit_ImportEvent);
            base.CancelEvent += new ClickHandler(UCOldPartsReceiptAddOrEdit_CancelEvent);
        }
        #endregion

        #region 取消功能
        void UCOldPartsReceiptAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (!string.IsNullOrEmpty(strBefore_orderId))
            {
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
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
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnView.Visible = false;
            base.btnEdit.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 导入事件
        void UCOldPartsReceiptAddOrEdit_ImportEvent(object sender, EventArgs e)
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
        void UCOldPartsReceiptAddOrEdit_SubmitEvent(object sender, EventArgs e)
        {
            opName = "提交领料退货单信息";
            SaveOrSubmitMethod("提交", DataSources.EnumAuditStatus.SUBMIT);
        }
        #endregion

        #region 保存事件
        void UCOldPartsReceiptAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            opName = "保存旧件发货单信息";
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

                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpReceiptTime.Value)))
                {
                    MessageBoxEx.Show("发货日期不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
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
                int intPartCount = 0;
                foreach (DataGridViewRow dgvr in dgvMaterials.Rows)
                {
                    string strPname = CommonCtrl.IsNullToString(dgvr.Cells["parts_name"].Value);
                    string strPCode = CommonCtrl.IsNullToString(dgvr.Cells["parts_code"].Value);
                    if (strPname.Length > 0 && strPCode.Length > 0)
                    {
                        intPartCount++;
                    }
                }
                if (intPartCount == 0)
                {
                    MessageBoxEx.Show("配件信息不能为空，请添加配件信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
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

        #region 旧件发货单基本信息保存
        private void SaveOrderInfo(List<SQLObj> listSql, DataSources.EnumAuditStatus status)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            #region 基本信息
            dicParam.Add("receipt_time", new ParamObj("receipt_time", Common.LocalDateTimeToUtcLong(dtpReceiptTime.Value).ToString(), SysDbType.BigInt));//制单时间            
            dicParam.Add("customer_code", new ParamObj("customer_code", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Text)) ? txtCustomNO.Text.Trim() : null, SysDbType.VarChar, 40));//客户编码
            dicParam.Add("customer_id", new ParamObj("customer_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Tag)) ? txtCustomNO.Tag.ToString() : null, SysDbType.VarChar, 40));//客户Id
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
            dicParam.Add("remarks", new ParamObj("remarks", txtRemark.Caption.Trim(), SysDbType.VarChar, 200));//备注
            #endregion

            //经办人id
            dicParam.Add("responsible_opid", new ParamObj("responsible_opid", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
            //经办人
            dicParam.Add("responsible_name", new ParamObj("responsible_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYHandle.SelectedValue)) ? cobYHandle.SelectedValue : null, SysDbType.VarChar, 40));
            //部门
            dicParam.Add("org_name", new ParamObj("org_name", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cboOrgId.SelectedValue)) ? cboOrgId.SelectedValue : null, SysDbType.VarChar, 40));
            dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）            
            //单据类型
            dicParam.Add("receipt_type", new ParamObj("receipt_type", "2", SysDbType.VarChar, 2));
            if (status == DataSources.EnumAuditStatus.SUBMIT)//提交操作时生成单号
            {
                dicParam.Add("receipts_no", new ParamObj("receipts_no", labReceiptNoS.Text.Trim(), SysDbType.VarChar, 40));//发货单号 
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
                    dicParam.Add("receipts_no", new ParamObj("receipts_no", labReceiptNoS.Text.Trim(), SysDbType.VarChar, 40));//发货单号 
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                }
                else
                {
                    //单据状态
                    dicParam.Add("info_status", new ParamObj("info_status", Convert.ToInt32(status).ToString(), SysDbType.VarChar, 40));
                    dicParam.Add("receipts_no", new ParamObj("receipts_no", null, SysDbType.VarChar, 40));//发货单号
                }
                //更新前置单据导入状态
                if (!string.IsNullOrEmpty(strBefore_orderId))
                {
                    UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                }
            }
            if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
            {
                strId = Guid.NewGuid().ToString();
                dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人） 
                dicParam.Add("create_name", new ParamObj("create_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//创建人
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into tb_maintain_oldpart_receiv_send (receipt_time,customer_code,customer_id,customer_name,linkman,link_man_mobile,remarks,responsible_opid
                 ,responsible_name,org_name,enable_flag,info_status,receipt_type,receipts_no,oldpart_id, create_by,create_name,create_time)
                 values (@receipt_time,@customer_code,@customer_id,@customer_name,@linkman,@link_man_mobile,@remarks,@responsible_opid
                 ,@responsible_name,@org_name,@enable_flag,@info_status,@receipt_type,@receipts_no,@oldpart_id,@create_by,@create_name,@create_time);";
            }
            else if (wStatus == WindowStatus.Edit)
            {
                dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update tb_maintain_oldpart_receiv_send set receipt_time=@receipt_time,customer_code=@customer_code,customer_id=@customer_id,customer_name=@customer_name,linkman=@linkman,link_man_mobile=@link_man_mobile,remarks=@remarks,responsible_opid=@responsible_opid
                 ,responsible_name=@responsible_name,org_name=@org_name,enable_flag=@enable_flag,info_status=@info_status,receipt_type=@receipt_type,receipts_no=@receipts_no,update_by=@update_by,update_name=@update_name,update_time=@update_time
                where oldpart_id=@oldpart_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 配件信息保存
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
                    dicParam.Add("maintain_id", new ParamObj("maintain_id", partID, SysDbType.VarChar, 40));
                    dicParam.Add("parts_code", new ParamObj("parts_code", dgvr.Cells["parts_code"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("parts_name", new ParamObj("parts_name", dgvr.Cells["parts_name"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("vehicle_no", new ParamObj("vehicle_no", dgvr.Cells["vehicle_no"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("unit", new ParamObj("unit", dgvr.Cells["unit"].Value, SysDbType.VarChar, 20));
                    dicParam.Add("drawn_no", new ParamObj("drawn_no", dgvr.Cells["drawn_no"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("quantity", new ParamObj("quantity", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["quantity"].Value)) ? dgvr.Cells["quantity"].Value : null, SysDbType.Decimal, 15));
                    dicParam.Add("unit_price", new ParamObj("unit_price", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["unit_price"].Value)) ? dgvr.Cells["unit_price"].Value : null, SysDbType.Decimal, 15));
                    dicParam.Add("sum_money", new ParamObj("sum_money", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["sum_money"].Value)) ? dgvr.Cells["sum_money"].Value : null, SysDbType.Decimal, 15));

                    dicParam.Add("remarks", new ParamObj("remarks", dgvr.Cells["remarks"].Value, SysDbType.VarChar, 200));
                    dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));
                    dicParam.Add("norms", new ParamObj("norms", dgvr.Cells["norms"].Value, SysDbType.VarChar, 40));
                    dicParam.Add("inventory_num", new ParamObj("inventory_num", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells["inventory_num"].Value)) ? dgvr.Cells["inventory_num"].Value : null, SysDbType.Decimal, 15));
                    dicParam.Add("process_mode", new ParamObj("process_mode", dgvr.Cells["process_mode"].Value, SysDbType.VarChar, 40));
                    string strPID = CommonCtrl.IsNullToString(dgvr.Cells["parts_id"].Value);
                    if (strPID.Length == 0)
                    {
                        opName = "新增旧件发货单";
                        strPID = Guid.NewGuid().ToString();
                        dicParam.Add("parts_id", new ParamObj("parts_id", strPID, SysDbType.VarChar, 40));
                        obj.sqlString = @"insert into tb_maintain_oldpart_material_detail (maintain_id,parts_code,parts_name,vehicle_no,unit,drawn_no,quantity,unit_price
                         ,sum_money,remarks,enable_flag,parts_id,norms,inventory_num,process_mode)
                         values (@maintain_id,@parts_code,@parts_name,@vehicle_no,@unit,@drawn_no,@quantity,@unit_price
                         ,@sum_money,@remarks,@enable_flag,@parts_id,@norms,@inventory_num,@process_mode);";
                    }
                    else
                    {
                        dicParam.Add("parts_id", new ParamObj("parts_id", dgvr.Cells["parts_id"].Value, SysDbType.VarChar, 40));
                        opName = "更新领料退货单";
                        obj.sqlString = @"update tb_maintain_oldpart_material_detail set maintain_id=@maintain_id,parts_code=@parts_code,parts_name=@parts_name,vehicle_no=@vehicle_no,unit=@unit,drawn_no=@drawn_no,quantity=@quantity,unit_price=@unit_price
                         ,sum_money=@sum_money,remarks=@remarks,enable_flag=@enable_flag,norms=@norms,inventory_num=@inventory_num,process_mode=@process_mode
                        where parts_id=@parts_id";
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
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

        #region 生成旧件发货单号创建人姓名
        /// <summary>
        /// 生成旧件发货单号创建人姓名
        /// </summary>
        private void GetRepairNo()
        {
            labReceiptNoS.Text = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.PartsSend);
            labCreatePersonS.Text = HXCPcClient.GlobalStaticObj.UserName;
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
        private void UCOldPartsSendAddOrEdit_Load(object sender, EventArgs e)
        {

            dtpReceiptTime.Value = DateTime.Now;
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
            else if (wStatus == WindowStatus.Add)
            {
                labReceiptNoS.Visible = false;
            }
        }
        #endregion

        #region 根据旧件发货单ID获取信息编辑用
        /// <summary>
        /// 根据旧件发货单ID获取信息，编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("oldpart_id='{0}'", strId);
            DataTable dt = DBHelper.GetTable("查询旧件发货货单", "tb_maintain_oldpart_receiv_send ", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];

            string strCtime = CommonCtrl.IsNullToString(dr["receipt_time"]);
            if (!string.IsNullOrEmpty(strCtime))
            {
                long Rticks = Convert.ToInt64(strCtime);
                dtpReceiptTime.Value = Common.UtcLongToLocalDateTime(Rticks);//发货日期
            }

            #region 客户信息
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id  
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注


            #endregion
            #endregion

            #region 配件信息
            DataTable dmt = DBHelper.GetTable("旧件发货明细数据", "tb_maintain_oldpart_material_detail", "*", string.Format(" maintain_id='{0}'", strId), "", "");
            if (dmt.Rows.Count > 0)
            {
                if (dmt.Rows.Count > dgvMaterials.Rows.Count)
                {
                    dgvMaterials.Rows.Add(dmt.Rows.Count - dgvMaterials.Rows.Count + 1);
                }
                for (int i = 0; i < dmt.Rows.Count; i++)
                {
                    DataRow dmr = dmt.Rows[i];
                    dgvMaterials.Rows[i].Cells["parts_id"].Value = CommonCtrl.IsNullToString(dmr["parts_id"]);
                    dgvMaterials.Rows[i].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dmr["parts_code"]);
                    dgvMaterials.Rows[i].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dmr["parts_name"]);
                    dgvMaterials.Rows[i].Cells["vehicle_no"].Value = CommonCtrl.IsNullToString(dmr["vehicle_no"]);
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                    dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);

                    dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                    dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dmr["inventory_num"]);
                    //dgvMaterials.Rows[i].Cells["process_mode"].Value = DataSources.GetDescription(typeof(DataSources.EnumOldSendProcessMode), int.Parse(CommonCtrl.IsNullToString(dmr["process_mode"]))); 
                    //dgvMaterials.Rows[i].Cells["process_mode"].Value =!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["process_mode"]))? CommonCtrl.IsNullToString(dmr["process_mode"])== "0" ? "返回供应商" : "就地处理":"";
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["process_mode"])))
                    {
                        dgvMaterials.Rows[i].Cells["process_mode"].Value = Convert.ToInt32(CommonCtrl.IsNullToString(dmr["process_mode"]));
                    }
                }
            }
            #endregion

            #region 顶部按钮设置
            if (wStatus == WindowStatus.Edit)
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["receipts_no"])))
                {
                    labReceiptNoS.Text = CommonCtrl.IsNullToString(dr["receipts_no"]);//旧件发货单号
                }
                else
                {
                    labReceiptNoS.Visible = false;
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
                labReceiptNoS.Visible = false;
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
            dgvMaterials.Columns["vehicle_no"].ReadOnly = true;
            dgvMaterials.Columns["drawn_no"].ReadOnly = true;
            dgvMaterials.Columns["unit"].ReadOnly = true;
            dgvMaterials.Columns["sum_money"].ReadOnly = true;
            dgvMaterials.Columns["parts_id"].ReadOnly = true;
            dgvMaterials.Columns["norms"].ReadOnly = true;
            dgvMaterials.Columns["inventory_num"].ReadOnly = true;
            process_mode.DataSource = DataSources.EnumToList(typeof(DataSources.EnumOldSendProcessMode), true);
            process_mode.ValueMember = "Value";
            process_mode.DisplayMember = "Text";
            #endregion
        }
        #endregion


        #region 旧件收货单导入功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strBefore_orderId))
            {
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "0");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
            UCPartsReceiptImport import = new UCPartsReceiptImport();
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK)
            {
                strBefore_orderId = import.strOldId;
                BindSetmentData(import.strOldId);
                BindMData(import.strOlddetailId.Substring(0, import.strOlddetailId.Length - 1));
                List<SQLObj> listSql = new List<SQLObj>();
                UpdateMaintainInfo(listSql, strBefore_orderId, "1");
                DBHelper.BatchExeSQLMultiByTrans("更新前置单据导入状态", listSql);
            }
        }
        #endregion

        #region 旧件收货单导入-根据前置单据绑定维修信息
        /// <summary>
        /// 旧件收货单导入-根据前置单据绑定维修信息
        /// </summary>
        public void BindSetmentData(string strOldId)
        {
            DataTable dt = DBHelper.GetTable("查询旧件收货单", "tb_maintain_oldpart_receiv_send", "*", " oldpart_id ='" + strOldId + "'", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            txtCustomNO.Text = CommonCtrl.IsNullToString(dr["customer_code"]);//客户编码
            txtCustomName.Caption = CommonCtrl.IsNullToString(dr["customer_name"]);//客户名称  
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["customer_id"]);//客户关联id
            txtContact.Caption = CommonCtrl.IsNullToString(dr["linkman"]);//联系人
            txtContactPhone.Caption = CommonCtrl.IsNullToString(dr["link_man_mobile"]);//联系人手机  
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remarks"]);//备注  
        }
        #endregion

        #region 配件数据
        private void BindMData(string strPartId)
        {
            //维修用料数据
            DataTable dmt = DBHelper.GetTable("配件数据", "tb_maintain_oldpart_material_detail", "*", string.Format(" parts_id in ({0})", strPartId), "", "");
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
                    dgvMaterials.Rows[i].Cells["vehicle_no"].Value = strVehicleNo;
                    dgvMaterials.Rows[i].Cells["unit"].Value = CommonCtrl.IsNullToString(dmr["unit"]);
                    dgvMaterials.Rows[i].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dmr["drawn_no"]);
                    dgvMaterials.Rows[i].Cells["quantity"].Value = CommonCtrl.IsNullToString(dmr["quantity"]);
                    dgvMaterials.Rows[i].Cells["unit_price"].Value = CommonCtrl.IsNullToString(dmr["unit_price"]);
                    dgvMaterials.Rows[i].Cells["sum_money"].Value = CommonCtrl.IsNullToString(dmr["sum_money"]);

                    dgvMaterials.Rows[i].Cells["remarks"].Value = CommonCtrl.IsNullToString(dmr["remarks"]);
                    dgvMaterials.Rows[i].Cells["norms"].Value = CommonCtrl.IsNullToString(dmr["norms"]);
                    dgvMaterials.Rows[i].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dmr["inventory_num"]);
                    //dgvMaterials.Rows[i].Cells["process_mode"].Value = DataSources.GetDescription(typeof(DataSources.EnumOldSendProcessMode), int.Parse(CommonCtrl.IsNullToString(dmr["process_mode"]))); 
                    //dgvMaterials.Rows[i].Cells["process_mode"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dmr["process_mode"])) ? CommonCtrl.IsNullToString(dmr["process_mode"]) == "0" ? "返回供应商" : "就地处理" : "";
                }
            }
        }
        #endregion

        #region 导入后更新维修单导入状态
        /// <summary>
        /// 导入后更新旧件收货单导入状态
        /// </summary>
        /// <param name="strReservId">旧件收货单Id</param>
        /// <param name="status">操作状体，0保存开放、1导入占用、2提交审核锁定</param>
        private void UpdateMaintainInfo(List<SQLObj> listSql, string strReservId, string status)
        {

            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

            dicParam.Add("oldpart_id", new ParamObj("oldpart_id", strReservId, SysDbType.VarChar, 40));
            if (status == "0")
            {
                //保存时，前置单据被释放               
                dicParam.Add("Import_status", new ParamObj("Import_status", "0", SysDbType.VarChar, 40));//开放
                obj.sqlString = "update tb_maintain_oldpart_receiv_send set Import_status=@Import_status where oldpart_id=@oldpart_id";

            }
            else if (status == "1")
            {
                //导入时，前置单据被占用 
                dicParam.Add("Import_status", new ParamObj("Import_status", "1", SysDbType.VarChar, 40));//占用
                obj.sqlString = "update tb_maintain_oldpart_receiv_send set Import_status=@Import_status where oldpart_id=@oldpart_id";
            }
            else if (status == "2")
            {
                //审核提交时，前置单据被锁定
                dicParam.Add("Import_status", new ParamObj("Import_status", "2", SysDbType.VarChar, 40));//锁定
                obj.sqlString = "update tb_maintain_oldpart_receiv_send set Import_status=@Import_status where oldpart_id=@oldpart_id";

            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 单元格双击读取配件信息
        private void dgvMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value = CommonCtrl.IsNullToString(dpr["ser_parts_code"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value = CommonCtrl.IsNullToString(dpr["parts_name"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit"].Value = CommonCtrl.IsNullToString(dpr["unit"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["drawn_no"].Value = CommonCtrl.IsNullToString(dpr["drawing_num"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value = "0";
                    string unit_price = CommonCtrl.IsNullToString(dpr["retail"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value = !string.IsNullOrEmpty(unit_price)?unit_price:"0";
                    dgvMaterials.Rows[e.RowIndex].Cells["norms"].Value = CommonCtrl.IsNullToString(dpr["model"]);
                    dgvMaterials.Rows[e.RowIndex].Cells["inventory_num"].Value = CommonCtrl.IsNullToString(dpr["paper_count"]);
                    dgvMaterials.Rows.Add(1);
                }
            }
        }

        private void dgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_code"].Value)) && !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["parts_name"].Value)))
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 7)
                {
                    ControlsConfig.SetCellsValue(dgvMaterials, e.RowIndex, "quantity,unit_price");
                    string strNum = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["quantity"].Value.ToString() : "0";
                    string strUnitPrice = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value)) ? dgvMaterials.Rows[e.RowIndex].Cells["unit_price"].Value.ToString() : "0";
                    dgvMaterials.Rows[e.RowIndex].Cells["sum_money"].Value = Convert.ToString(Convert.ToDecimal(strNum = strNum == "" ? "0" : strNum) * Convert.ToDecimal(Convert.ToDecimal(strUnitPrice = strUnitPrice == "" ? "0" : strUnitPrice)));
                }
            }

        }
        #endregion
    }
}
