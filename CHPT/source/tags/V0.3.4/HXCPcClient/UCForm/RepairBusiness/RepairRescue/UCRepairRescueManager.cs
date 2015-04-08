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
using SYSModel;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.RepairBusiness.RepairRescue
{
    /// <summary>
    /// 维修管理-救援单管理
    /// Author：JC
    /// AddTime：2014.10.27
    /// </summary>
    public partial class UCRepairRescueManager : UCBase
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
        #endregion

        #region 初始化窗体
        public UCRepairRescueManager()
        {
            InitializeComponent();
            BindOrderStatus();
            SetTopbuttonShow();
            CommonCtrl.BindComboBoxByDictionarr(cobRescueType, "sys_rescue_type", true);//救援类型
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            base.AddEvent += new ClickHandler(UCRepairRescueManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCRepairRescueManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCRepairRescueManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRepairRescueManager_DeleteEvent);
            base.ViewEvent += new ClickHandler(UCRepairRescueManager_ViewEvent);
            base.VerifyEvent += new ClickHandler(UCRepairRescueManager_VerifyEvent);
            base.BalanceEvent += new ClickHandler(UCRepairRescueManager_BalanceEvent);
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
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnBalance.Visible = true;
            base.btnStatus.Visible = false;
            base.btnActivation.Visible = false;
            
        }
        #endregion

        #region 结算事件
        void UCRepairRescueManager_BalanceEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要结算吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
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
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strReceId += dr.Cells["rescue_id"].Value.ToString() + ",";
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("rescue_no", new ParamObj("rescue_no", CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Rescue), SysDbType.VarChar, 40));//单据编号                   
                    dicParam.Add("rescue_id", new ParamObj("rescue_id", dr.Cells["rescue_id"].Value, SysDbType.VarChar, 40));//单据ID
                    dicParam.Add("document_status", new ParamObj("document_status", status, SysDbType.VarChar, 40));//单据状态
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = "update tb_maintain_rescue_info set document_status=@document_status,rescue_no=@rescue_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where rescue_id=@rescue_id";
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }

            if (string.IsNullOrEmpty(strReceId))
            {
                MessageBoxEx.Show("请选择需要" + strMessage + "的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为结算", listSql))
            {
                MessageBoxEx.Show("" + strMessage + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindPageData();
            }
        }
        #endregion

        #region 审核事件
        void UCRepairRescueManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["rescue_id"].Value.ToString());
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
                        dicParam.Add("rescue_id", new ParamObj("rescue_id", dr.Cells["rescue_id"].Value, SysDbType.VarChar, 40));//单据ID
                        dicParam.Add("document_status", new ParamObj("document_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                        dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                        dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = "update tb_maintain_rescue_info set document_status=@document_status,Verify_advice=@Verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where rescue_id=@rescue_id";
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
        void UCRepairRescueManager_ViewEvent(object sender, EventArgs e)
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
            UCRepairRescueView view = new UCRepairRescueView(strReId);
            view.uc = this;
            base.addUserControl(view, "救援单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 删除事件
        void UCRepairRescueManager_DeleteEvent(object sender, EventArgs e)
        {
           
            try
            {

                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["rescue_id"].Value.ToString());
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
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除救援单", "tb_maintain_rescue_info", comField, "rescue_id", listField.ToArray());
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

        #region 编辑事件
        void UCRepairRescueManager_EditEvent(object sender, EventArgs e)
        {
            //if (!IsCheck("编辑"))
            //{
            //    return;
            //}
            if (dgvRData.CurrentRow.Index < 0)
            {
                return;
            }
            int intIndex = dgvRData.CurrentRow.Index;
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["rescue_id"].Value);
            UCRepairRescueAddOrEdit RescueEdit = new UCRepairRescueAddOrEdit();
            RescueEdit.uc = this;
            RescueEdit.wStatus = WindowStatus.Edit;
            RescueEdit.strId = strReId;  //编辑单据的Id值
            base.addUserControl(RescueEdit, "救援单-编辑", "RescueEdit" + RescueEdit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 复制事件
        void UCRepairRescueManager_CopyEvent(object sender, EventArgs e)
        {
            //if (!IsCheck("复制"))
            //{
            //    return;
            //}
            if (dgvRData.CurrentRow.Index < 0)
            {
                return;
            }
            int intIndex = dgvRData.CurrentRow.Index;
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["rescue_id"].Value);
            UCRepairRescueAddOrEdit RescueCopy = new UCRepairRescueAddOrEdit();
            RescueCopy.uc = this;
            RescueCopy.wStatus = WindowStatus.Copy;
            RescueCopy.strId = strReId;  //复制单据的Id值
            base.addUserControl(RescueCopy, "救援单-复制", "RescueCopy" + RescueCopy.strId, this.Tag.ToString(), this.Name);
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
                    listField.Add(dr.Cells["rescue_id"].Value.ToString());
                    strReId = dr.Cells["rescue_id"].Value.ToString();
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

        #region 新增事件
        void UCRepairRescueManager_AddEvent(object sender, EventArgs e)
        {
            UCRepairRescueAddOrEdit RescueAdd = new UCRepairRescueAddOrEdit();
            RescueAdd.uc = this;
            RescueAdd.wStatus = WindowStatus.Add;
            base.addUserControl(RescueAdd, "救援单-新增", "RescueAdd", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtOrder.Caption = string.Empty;
            txtServerCarNo.Caption = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            cobRescueType.SelectedValue = string.Empty;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
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
                #region 事件选择判断
                if (dtpSTime.Value > dtpETime.Value)
                {
                    MessageBoxEx.Show("出发时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }                
                if (!string.IsNullOrEmpty(txtOrder.Caption.Trim()))//救援单号
                {
                    strWhere += string.Format(" and  rescue_no like '%{0}%'", txtOrder.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue)))//单据状体
                {
                    strWhere += string.Format(" and document_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobRescueType.SelectedValue)))//救援类型
                {
                    strWhere += string.Format(" and rescue_type = '{0}'", cobRescueType.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtServerCarNo.Caption)))//服务车号
                {
                    strWhere += string.Format(" and service_vehicle_no = '{0}'", txtServerCarNo.Caption);
                }               
                strWhere += string.Format(" and depart_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//结算日期
               
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询救援单管理", "tb_maintain_rescue_info", "*", strWhere, "", " order by depart_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvRData.DataSource = dt;
                page.RecordCount = recordCount;
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

        #endregion

        #region 窗体Load事件
        private void UCRepairRescueManager_Load(object sender, EventArgs e)
        {
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
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
            BindPageData();
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

        #region 双击单元格进入预览窗体
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
            strReId = dgvRData.CurrentRow.Cells["rescue_id"].Value.ToString();
            ViewData("1");
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
            if (fieldNmae.Equals("depart_time") || fieldNmae.Equals("completion_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("document_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("vehicle_model") || fieldNmae.Equals("rescue_type"))
            {
                e.Value = GetDicName(e.Value.ToString());
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
                    listFiles.Add(dgvr.Cells[document_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[rescue_id.Name].Value.ToString());
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
        /// 右键新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_AddEvent(null, null);
        }
        /// <summary>
        /// 右键复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_CopyEvent(null, null);
        }
        /// <summary>
        /// 右键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_DeleteEvent(null, null);
        }
        /// <summary>
        /// 右键结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSubmit_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_BalanceEvent(null, null);
        }
        /// <summary>
        /// 右键审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiVerify_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_VerifyEvent(null, null);
        }
        /// <summary>
        /// 右键编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            UCRepairRescueManager_EditEvent(null, null);
        }
        #endregion
        
    }
}
