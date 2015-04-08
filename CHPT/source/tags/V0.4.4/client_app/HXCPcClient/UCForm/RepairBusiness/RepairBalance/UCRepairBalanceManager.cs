using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using ServiceStationClient.ComponentUI.TextBox;
using System.Threading;

namespace HXCPcClient.UCForm.RepairBusiness.RepairBalance
{
    /// <summary>
    /// 维修管理-维修结算单管理
    /// Author：JC
    /// AddTime：2014.10.21
    /// </summary>
    public partial class UCRepairBalanceManager : UCBase
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
        UCVerify verify;//审核窗体
        List<string> listIDs = new List<string>();//已选择ID
        private bool myLock = true;
        int recordCount = 0;
        #endregion

        #region 初始化窗体
        public UCRepairBalanceManager()
        {
            InitializeComponent();
            CommonCtrl.BindComboBoxByDictionarr(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式   
            CommonCtrl.BindComboBoxByDictionarr(cobRepType, "sys_repair_category", true);//绑定维修类别    
            BindOrderStatus();           
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            base.AddEvent += new ClickHandler(UCRepairBalanceManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCRepairBalanceManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCRepairBalanceManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRepairBalanceManager_DeleteEvent);
            base.ViewEvent += new ClickHandler(UCRepairBalanceManager_ViewEvent);
            base.VerifyEvent += new ClickHandler(UCRepairBalanceManager_VerifyEvent);
            base.BalanceEvent += new ClickHandler(UCRepairBalanceManager_BalanceEvent);
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

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            //base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnStatus.Visible = false;
            base.btnActivation.Visible = false;
            base.btnBalance.Visible = true;
            base.btnSubmit.Visible = false;
        }
        #endregion

        #region 结算操作
        void UCRepairBalanceManager_BalanceEvent(object sender, EventArgs e)
        {           
            SubmitAndVerify("结算", DataSources.EnumAuditStatus.Balance);
        }
        #endregion

        #region 结算功能
        /// <summary>
        /// 结算功能,结算时添加单号
        /// </summary>
        /// <param name="strMessage">提示信息</param>
        /// <param name="status">单据状态</param>
        private void SubmitAndVerify(string strMessage, DataSources.EnumAuditStatus status)
        {
           
            List<SQLObj> listSql = new List<SQLObj>();
            string strReceId = string.Empty;//单据Id值      
            string strOrderNo = string.Empty;//单据编号
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strReceId += dr.Cells["maintain_id"].Value.ToString() + ",";
                    strOrderNo = dr.Cells["maintain_no"].Value.ToString();
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("maintain_no", new ParamObj("maintain_no",!string.IsNullOrEmpty(strOrderNo)?strOrderNo: CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Repair), SysDbType.VarChar, 40));//单据编号                   
                    dicParam.Add("maintain_id", new ParamObj("maintain_id", dr.Cells["maintain_id"].Value, SysDbType.VarChar, 40));//单据ID
                    dicParam.Add("info_status", new ParamObj("info_status", status, SysDbType.VarChar, 40));//单据状态
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = "update tb_maintain_info set info_status=@info_status,maintain_no=@maintain_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where maintain_id=@maintain_id";
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }

            if (string.IsNullOrEmpty(strReceId))
            {
                MessageBoxEx.Show("请选择需要" + strMessage + "的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show("确认要" + strMessage + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为结算", listSql))
            {
                MessageBoxEx.Show("" + strMessage + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindPageData();
            }
        }
        #endregion

        #region 审核功能
        void UCRepairBalanceManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                }
            }
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要审核的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            //{
            //    return;
            //}
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();
                string strReceId = string.Empty;//单据Id值
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strReceId += dr.Cells["maintain_id"].Value.ToString() + ",";
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("maintain_id", new ParamObj("maintain_id", dr.Cells["maintain_id"].Value, SysDbType.VarChar, 40));//单据ID
                        dicParam.Add("info_status", new ParamObj("info_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                        dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                        dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = "update tb_maintain_info set info_status=@info_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where maintain_id=@maintain_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为审核", listSql))
                {
                    string strMsg = string.Empty;
                    if (verify.auditStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        strMsg = "成功";
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

        #region 预览事件
        void UCRepairBalanceManager_ViewEvent(object sender, EventArgs e)
        {
            //ViewData();
        }
        #endregion

        #region 进入预览窗体的方法
       /// <summary>
        /// 预览数据
       /// </summary>
       /// <param name="strType">操作类型，0为预览，1为双击单元格</param>
        private void ViewData(string strType="0")
        {
            if (strType == "0")
            {
                if (!IsCheck("预览"))
                {
                    return;
                }
            }
            UCRepairBalanceView view = new UCRepairBalanceView(strReId);
            view.uc = this;
            base.addUserControl(view, "维修结算单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
        }
        #region 删除功能
        void UCRepairBalanceManager_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
               
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["maintain_id"].Value.ToString());                       
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
                comField.Add("update_name",HXCPcClient.GlobalStaticObj.UserName);//修改人姓名
                comField.Add("update_time",Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间               
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除维修结算单", "tb_maintain_info", comField, "maintain_id", listField.ToArray());
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

        #region 编辑功能
        void UCRepairBalanceManager_EditEvent(object sender, EventArgs e)
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
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["maintain_id"].Value);
            UCRepairBalanceAddOrEdit BalanceEdit = new UCRepairBalanceAddOrEdit();
            BalanceEdit.uc = this;
            BalanceEdit.wStatus = WindowStatus.Edit;
            BalanceEdit.strId = strReId;  //编辑单据的Id值
            base.addUserControl(BalanceEdit, "维修结算-编辑", "BalanceEdit" + BalanceEdit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 复制功能
        void UCRepairBalanceManager_CopyEvent(object sender, EventArgs e)
        {
            //if (!IsCheck("复制"))
            //{
            //    return;
            //}
            if (dgvRData.CurrentRow == null || dgvRData.CurrentRow.Index < 0)
            {
                return;
            }
            int intIndex = dgvRData.CurrentRow.Index;
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["maintain_id"].Value);
            UCRepairBalanceAddOrEdit BalanceCopy = new UCRepairBalanceAddOrEdit();
            BalanceCopy.uc = this;
            BalanceCopy.wStatus = WindowStatus.Copy;
            BalanceCopy.strId = strReId;  //复制单据的Id值
            base.addUserControl(BalanceCopy, "维修结算-复制", "BalanceCopy" + BalanceCopy.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region  编辑、复制、预览数据验证
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
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                    strReId = dr.Cells["maintain_id"].Value.ToString();
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

        #endregion

        #region 新增事件
        void UCRepairBalanceManager_AddEvent(object sender, EventArgs e)
        {
            UCRepairBalanceAddOrEdit BalanceeAdd = new UCRepairBalanceAddOrEdit();
            BalanceeAdd.uc = this;
            BalanceeAdd.wStatus = WindowStatus.Add;
            base.addUserControl(BalanceeAdd, "维修结算-新增", "BalanceeAdd", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 车牌号选择器
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

        #region 客户编码选择器
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
                    if (dtpBalanceSTime.Value > dtpBalanceETime.Value)
                    {
                        MessageBoxEx.Show("结算日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion
                    strWhere = string.Format(" b.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and b.orders_source='3'");//enable_flag 1未删除b.orders_source='3'为维修结算单标识
                    if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                    {
                        strWhere += string.Format(" and  b.vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                    {
                        strWhere += string.Format(" and  b.customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                    {
                        strWhere += string.Format(" and  b.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtContact.Caption.Trim()))//联系人
                    {
                        strWhere += string.Format(" and  b.linkman like '%{0}%'", txtContact.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                    {
                        strWhere += string.Format(" and  b.link_man_mobile like '%{0}%'", txtContactPhone.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtOrder.Caption.Trim()))//维修单号
                    {
                        strWhere += string.Format(" and  b.maintain_no like '%{0}%'", txtOrder.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue)))//单据状体（具体状态目前不详）
                    {
                        strWhere += string.Format(" and b.info_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPayType.SelectedValue)))//维修付费方式
                    {
                        strWhere += string.Format(" and  b.maintain_payment like '%{0}%'", cobPayType.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobRepType.SelectedValue)))//维修类别
                    {
                        strWhere += string.Format(" and  b.maintain_type like '%{0}%'", cobRepType.SelectedValue.ToString());
                    }
                    strWhere += string.Format(" and a.create_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpBalanceSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpBalanceETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//结算日期
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), strWhere);
//                    string strFiles = @"b.maintain_no,a.man_hour_sum,a.fitting_sum,a.other_item_sum,a.privilege_cost,a.should_sum,a.received_sum
//                                     ,a.debt_cost,b.info_status,a.create_time,b.vehicle_no,b.customer_code,b.customer_name,b.maintain_payment
//                                     ,b.maintain_type,b.remark,a.maintain_id,b.before_orderId";
//                    int recordCount;
//                    DataTable dt = DBHelper.GetTableByPage("分页查询维修结算单管理", "tb_maintain_settlement_info a left join tb_maintain_info b on a.maintain_id=b.maintain_id", strFiles, strWhere, "", " order by a.create_time desc", page.PageIndex, page.PageSize, out recordCount);
//                    dgvRData.DataSource = dt;
//                    page.RecordCount = recordCount;
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
            string strFiles = @"b.maintain_no,a.man_hour_sum,a.fitting_sum,a.other_item_sum,a.privilege_cost,a.should_sum,a.received_sum
                                     ,a.debt_cost,b.info_status,a.create_time,b.vehicle_no,b.customer_code,b.customer_name,b.maintain_payment
                                     ,b.maintain_type,b.remark,a.maintain_id,b.before_orderId";
            DataTable dt = DBHelper.GetTableByPage("分页查询维修结算单管理", "tb_maintain_settlement_info a left join tb_maintain_info b on a.maintain_id=b.maintain_id", strFiles, obj.ToString(), "", " order by a.create_time desc", page.PageIndex, page.PageSize, out this.recordCount);
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

        #region 窗体Load事件
        private void UCRepairBalanceManager_Load(object sender, EventArgs e)
        {
            SetTopbuttonShow();
            dtpBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpBalanceETime.Value = DateTime.Now;
            dgvRData.Dock = DockStyle.Fill;
            dgvRData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvRData.Columns)
            {
                if (dgvc == colCheck)
                {
                   // dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            this.InitEvent();
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            BindPageData();
        }
        #endregion

        #region 查询功能
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 清空功能
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCarNO.Tag = string.Empty;
            txtContact.Caption = string.Empty;
            txtContactPhone.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtOrder.Caption = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            cobPayType.SelectedValue = string.Empty;
            cobRepType.SelectedValue = string.Empty;
            dtpBalanceSTime.Value = DateTime.Now.AddMonths(-1);
            dtpBalanceETime.Value = DateTime.Now;
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
            if (fieldNmae.Equals("info_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("maintain_payment") || fieldNmae.Equals("maintain_type"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
        }
        #endregion       

        #region 双击单元格进入预览窗体
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
            strReId = dgvRData.CurrentRow.Cells["maintain_id"].Value.ToString();
            ViewData("1");
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
            List<string> listBefor = new List<string>();
            foreach (DataGridViewRow dgvr in dgvRData.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[info_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[maintain_id.Name].Value.ToString());
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvr.Cells[before_orderId.Name].Value)))
                    {
                        listBefor.Add(CommonCtrl.IsNullToString(dgvr.Cells[before_orderId.Name].Value));
                    }
                }
            }

            //结算
            string submitStr = ((int)DataSources.EnumAuditStatus.Balance).ToString();
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
                tsmiCopy.Enabled = true;
            }
            else
            {
                btnCopy.Enabled = false;
                tsmiCopy.Enabled = false;
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
                btnBalance.Enabled = false;
                tsmiBalance.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnDelete.Enabled = true;
                tsmiSubmit.Enabled = true;
                tsmiDelete.Enabled = true;
                btnBalance.Enabled = true;
                tsmiBalance.Enabled = true;
            }
            //由前置单据导入的不能修改
            //if (listBefor.Count>0)
            //{
            //    btnEdit.Enabled = false;
            //}

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

    }
}
