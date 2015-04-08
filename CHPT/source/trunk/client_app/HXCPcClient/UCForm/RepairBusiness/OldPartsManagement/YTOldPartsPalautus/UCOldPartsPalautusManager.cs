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
using Model;
using System.Threading;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus
{
    /// <summary>
    /// 旧件管理-宇通旧件返厂单管理
    /// Author：JC
    /// AddTime：2014.11.03
    /// </summary>
    public partial class UCOldPartsPalautusManager : UCBase
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
        private bool myLock = true;
        int recordCount = 0;
        BusinessPrint businessPrint;//业务打印功能
        #endregion

        #region 初始化窗体
        public UCOldPartsPalautusManager()
        {
            InitializeComponent();
            BindOrderStatus();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            BindStatusYT();// 绑定宇通旧件回收单状态  
            base.AddEvent += new ClickHandler(UCOldPartsPalautusManager_AddEvent);
            base.EditEvent += new ClickHandler(UCOldPartsPalautusManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCOldPartsPalautusManager_DeleteEvent);
            base.SubmitEvent += new ClickHandler(UCOldPartsPalautusManager_SubmitEvent);
            base.VerifyEvent += new ClickHandler(UCOldPartsPalautusManager_VerifyEvent);
            base.ViewEvent += new ClickHandler(UCOldPartsPalautusManager_ViewEvent);
            base.ConfirmEvent += new ClickHandler(UCOldPartsPalautusManager_ConfirmEvent);
            base.PrintEvent += new ClickHandler(UCOldPartsPalautusManager_PrintEvent);
            base.SetEvent += new ClickHandler(UCOldPartsPalautusManager_SetEvent);
            #region 预览、打印设置
            string printObject = "tb_maintain_oldpart_recycle";
            string printTitle = "宇通旧件返厂单";
            List<string> listNotPrint = new List<string>();
            listNotPrint.Add(return_id.Name);
            //listNotPrint.Add(v_brand.Name);
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 297;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(dgvRData, printObject, printTitle, paperSize, listNotPrint);
            #endregion
        }
        #endregion

        #region 获取键盘的Enter事件实现查询
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                BindPageData();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 设置事件
        void UCOldPartsPalautusManager_SetEvent(object sender, EventArgs e)
        {
            businessPrint.PrintSet(dgvRData);
        }
        #endregion

        #region 打印事件
        void UCOldPartsPalautusManager_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(dgvRData.GetBoundData());
        }
        #endregion

        #region 顶部按钮显示
        private void SetTopbuttonShow()
        {
            base.btnCopy.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnSync.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 确认事件
        void UCOldPartsPalautusManager_ConfirmEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            string strReceId = string.Empty;//单据Id值   
            string strSTime = string.Empty;//回收周期开始时间
            string strETime = string.Empty;//回收周期结束时间
            string oldBillNum = string.Empty;//旧件回收单号
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strReceId += dr.Cells["return_id"].Value.ToString() + ",";
                    oldBillNum += dr.Cells["oldpart_receipts_no"].Value.ToString() + ",";
                    long tickeS = Convert.ToInt64(CommonCtrl.IsNullToString(dr.Cells["create_time_start"].Value));
                    long tickeE = Convert.ToInt64(CommonCtrl.IsNullToString(dr.Cells["create_time_end"].Value));
                    strSTime = Common.UtcLongToLocalDateTime(tickeS).ToString("yyyy-MM-dd");
                    strETime = Common.UtcLongToLocalDateTime(tickeE).ToString("yyyy-MM-dd");
                }
            }

            if (string.IsNullOrEmpty(strReceId))
            {
                MessageBoxEx.Show("请选择需要确认的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ProcessModeToYT(oldBillNum.Substring(0, oldBillNum.Length - 1), strReceId.Substring(0, strReceId.Length - 1), strSTime, strETime);
            MessageBoxEx.Show("确认成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindPageData();

        }
        #endregion

        #region 旧件回收确认到宇通
        /// <summary>
        /// 旧件回收确认到宇通
        /// </summary>
        /// <param name="oldBillNum">旧件回收单号</param>
        /// <param name="strReId">单据Id</param>
        /// <param name="strStartTime">回收周期开始时间</param>
        /// <param name="strEndTime">回收周期结束时间</param>
        private void ProcessModeToYT(string oldBillNum, string strReId, string strStartTime, string strEndTime)
        {
            try
            {
                partReturn model = new partReturn();
                model.PartDetails = new partReturnPartDetail[1];
                model.crm_old_bill_num = oldBillNum;
                model.info_status_yt = DBHelper.GetSingleValue("", "sys_dictionaries", "dic_id", "dic_code='oldpart_recycle_status_PCM_FIX_CALLBACK_ENTER'", "");
                model.create_time_start = strStartTime;
                model.create_time_end = strEndTime;
                DataTable dmt = DBHelper.GetTable("宇通旧件返厂明细数据", "tb_maintain_oldpart_recycle_material_detail", "*", string.Format(" maintain_id='{0}'", strReId), "", "");
                model.PartDetails = new partReturnPartDetail[dmt.Rows.Count];
                if (dmt.Rows.Count > 0)
                {
                    for (int i = 0; i < dmt.Rows.Count; i++)
                    {
                        DataRow dmr = dmt.Rows[i];
                        partReturnPartDetail detail = new partReturnPartDetail();
                        detail.parts_id = CommonCtrl.IsNullToString(dmr["parts_id"]);
                        detail.service_no = CommonCtrl.IsNullToString(dmr["service_no"]);
                        detail.car_parts_code = CommonCtrl.IsNullToString(dmr["parts_code"]);
                        detail.parts_remark = CommonCtrl.IsNullToString(dmr["receive_explain"]);
                        detail.change_num = CommonCtrl.IsNullToString(dmr["change_num"]);
                        detail.send_num = CommonCtrl.IsNullToString(dmr["send_num"]);
                        detail.process_mode = CommonCtrl.IsNullToString(dmr["process_mode"]);
                        detail.remark = "";
                        model.PartDetails[i] = detail;
                    }

                }
                DBHelper.WebServHandler("回收旧件-更新", EnumWebServFunName.UpPartRetureUpdate, model);
            }
            catch (Exception ee)
            {

            }
        }
        #endregion

        #region 绑定宇通旧件回收单状态
        /// <summary>
        /// 绑定宇通旧件回收单状态
        /// </summary>
        private void BindStatusYT()
        {
            DataTable dot = CommonCtrl.GetDictByCode("oldpart_recycle_status_yt", true);
            cobYTStatus.DataSource = dot;
            cobYTStatus.ValueMember = "dic_id";
            cobYTStatus.DisplayMember = "dic_name";
        }
        #endregion

        #region 预览事件
        void UCOldPartsPalautusManager_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(dgvRData.GetBoundData());
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
            UCOldPartsPalautusView view = new UCOldPartsPalautusView(strReId);
            view.uc = this;
            base.addUserControl(view, "宇通旧件返厂单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 审核事件
        void UCOldPartsPalautusManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["return_id"].Value.ToString());
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
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("return_id", new ParamObj("return_id", dr.Cells["return_id"].Value, SysDbType.VarChar, 40));//单据ID
                        dicParam.Add("info_status", new ParamObj("info_status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                        dicParam.Add("verify_advice", new ParamObj("verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                        dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                        dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                        dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                        obj.sqlString = "update tb_maintain_oldpart_recycle set info_status=@info_status,verify_advice=@verify_advice,update_by=@update_by,update_name=@update_name,update_time=@update_time where return_id=@return_id";
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

        #region 提交事件
        void UCOldPartsPalautusManager_SubmitEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要提交吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
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
            List<SQLObj> listSql = new List<SQLObj>();
            string strReceId = string.Empty;//单据Id值       
            string strOrderNo = string.Empty;//单据编号
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strReceId += dr.Cells["return_id"].Value.ToString() + ",";
                    strOrderNo = dr.Cells["receipts_no"].Value.ToString();
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("receipts_no", new ParamObj("receipts_no",!string.IsNullOrEmpty(strOrderNo)?strOrderNo: CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.OldPartsPalautus), SysDbType.VarChar, 40));//单据编号                   
                    dicParam.Add("return_id", new ParamObj("return_id", dr.Cells["return_id"].Value, SysDbType.VarChar, 40));//单据ID
                    dicParam.Add("info_status", new ParamObj("info_status", status, SysDbType.VarChar, 40));//单据状态
                    dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                    dicParam.Add("update_name", new ParamObj("update_name", HXCPcClient.GlobalStaticObj.UserName, SysDbType.VarChar, 40));//修改人姓名
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                    obj.sqlString = "update tb_maintain_oldpart_recycle set info_status=@info_status,receipts_no=@receipts_no,update_by=@update_by,update_name=@update_name,update_time=@update_time where return_id=@return_id";
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

        #region 删除事件
        void UCOldPartsPalautusManager_DeleteEvent(object sender, EventArgs e)
        {
            try
            {

                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRData.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["return_id"].Value.ToString());
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
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除宇通旧件返厂单", "tb_maintain_oldpart_recycle", comField, "return_id", listField.ToArray());
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
        void UCOldPartsPalautusManager_EditEvent(object sender, EventArgs e)
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
            strReId = CommonCtrl.IsNullToString(dgvRData.Rows[intIndex].Cells["return_id"].Value);
            UCOldPartsPalautusAddOrEdit PalautusEdit = new UCOldPartsPalautusAddOrEdit();
            PalautusEdit.uc = this;
            PalautusEdit.wStatus = WindowStatus.Edit;
            PalautusEdit.strId = strReId;  //编辑单据的Id值
            base.addUserControl(PalautusEdit, "宇通旧件返厂单-编辑", "PalautusEdit" + PalautusEdit.strId, this.Tag.ToString(), this.Name);
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
                    listField.Add(dr.Cells["return_id"].Value.ToString());
                    strReId = dr.Cells["return_id"].Value.ToString();
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

        #region 新增功能
        void UCOldPartsPalautusManager_AddEvent(object sender, EventArgs e)
        {
            UCOldPartsPalautusAddOrEdit PalautusAdd = new UCOldPartsPalautusAddOrEdit();
            PalautusAdd.uc = this;
            PalautusAdd.wStatus = WindowStatus.Add;
            base.addUserControl(PalautusAdd, "宇通旧件返厂单-新增", "PalautusAdd", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 清除功能
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOrder.Caption = string.Empty;
            txtReceiptOrderNO.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            cobYTStatus.SelectedValue = string.Empty;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
        }
        #endregion

        #region 查询功能
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
                    if (dtpSTime.Value > dtpETime.Value)
                    {
                        MessageBoxEx.Show("单据日期,开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion
                    strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'");//enable_flag 1未删除receipt_type='1'为收货单
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtOrder.Caption.Trim())))//返厂单号
                    {
                        strWhere += string.Format(" and a.receipts_no = '{0}'", txtOrder.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue)))//单据状体
                    {
                        strWhere += string.Format(" and a.info_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                    }

                    if (!string.IsNullOrEmpty(txtReceiptOrderNO.Caption.Trim()))//旧件回收单号
                    {
                        strWhere += string.Format(" and  a.oldpart_receipts_no like '%{0}%'", txtReceiptOrderNO.Caption.Trim());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobYTStatus.SelectedValue)))//宇通旧件回收单状体
                    {
                        strWhere += string.Format(" and a.info_status_yt = '{0}'", cobYTStatus.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtRemark.Caption.Trim())))//备注
                    {
                        strWhere += string.Format(" and a.remarks like '%{0}%'", txtRemark.Caption.Trim());
                    }

                    strWhere += string.Format(" and a.receipt_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//领料时间
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), strWhere);
//                    string strFiles = @"a.receipts_no,a.oldpart_receipts_no,a.info_status,a.info_status_yt,
//                (SELECT SUM(b.change_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS change_num,
//                (SELECT SUM(b.send_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS send_num,
//                (SELECT SUM(b.receive_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS receive_num,
//                a.receipt_time,create_time_start,a.create_time_end,a.remarks,a.return_id ";
//                    string strTables = "tb_maintain_oldpart_recycle a ";
//                    int recordCount;
//                    DataTable dt = DBHelper.GetTableByPage("分页查询旧件收货单管理", strTables, strFiles, strWhere, "", " order by a.receipt_time desc", page.PageIndex, page.PageSize, out recordCount);
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
            string strFiles = @"a.receipts_no,a.oldpart_receipts_no,a.info_status,a.info_status_yt,
                            (SELECT SUM(b.change_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS change_num,
                            (SELECT SUM(b.send_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS send_num,
                            (SELECT SUM(b.receive_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS receive_num,
                            a.receipt_time,create_time_start,a.create_time_end,a.remarks,a.return_id ";
            string strTables = "tb_maintain_oldpart_recycle a ";
            DataTable dt = DBHelper.GetTableByPage("分页查询旧件收货单管理", strTables, strFiles, obj.ToString(), "", " order by a.receipt_time desc", page.PageIndex, page.PageSize, out this.recordCount);
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
            base.ExportEvent -= new ClickHandler(UC_ExportEvent);
            base.ExportEvent += new ClickHandler(UC_ExportEvent);
        }
        #endregion
        #endregion

        #region 导出事件
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvRData.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "宇通旧件返厂单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvRData);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【宇通旧件返厂单】" + ex.Message, "server");
                MessageBoxEx.ShowWarning("导出失败！");
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

        #region 窗体Load事件
        private void UCOldPartsPalautusManager_Load(object sender, EventArgs e)
        {
            SetTopbuttonShow();
            dtpSTime.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            dtpETime.Value = dtpSTime.Value.AddMonths(1).AddDays(-1);
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

        #region 重写单据状体、时间等值
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("receipt_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("create_time_start"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    object objEndTime = string.Empty;
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[e.RowIndex].Cells["create_time_end"].Value)))
                    {
                        object objETime = dgvRData.Rows[e.RowIndex].Cells["create_time_end"].Value;
                        long ticke = (long)objETime;
                        objEndTime = Common.UtcLongToLocalDateTime(ticke);
                    }
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks) + "-" + objEndTime;
                }
            }
            if (fieldNmae.Equals("info_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("info_status_yt"))
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

        #region 双击单元格进入详情窗体
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
            strReId = dgvRData.CurrentRow.Cells["return_id"].Value.ToString();
            ViewData("1");
        }
        #endregion

        #region 根据选择的数据判断编辑、预览、提交按钮的显示状态
        private void dgvRData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<string> listField = new List<string>();
            string strSatus = string.Empty;
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["return_id"].Value.ToString());
                    strSatus += dr.Cells["info_status"].Value.ToString() + ",";
                }
            }
            base.btnEdit.Enabled = listField.Count > 1 || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString()) ? false : true;
            base.btnView.Enabled = listField.Count > 1 ? false : true;
            base.btnDelete.Enabled = strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString()) ? false : true;
            base.btnVerify.Enabled = strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString()) ? false : true;
            base.btnSubmit.Enabled = strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString()) || strSatus.Contains(Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString()) ? false : true;
            base.btnConfirm.Enabled = listField.Count > 1 ? false : true;
        }
        #endregion              

        #region 复选框选择
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
        }
        #endregion
    }
}
