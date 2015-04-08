using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautus;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using SYSModel;
using System.Threading;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsPalautusQuery
{
    /// <summary>
    /// 旧件管理-宇通旧件返厂查询
    /// Author：JC
    /// AddTime：2014.11.03
    /// </summary>
    public partial class UCOldPartsPalautusQueryManage : UCBase
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
        string strDReId = string.Empty;
        private bool myLock = true;
        int recordCount = 0;
        BusinessPrint businessPrint;//业务打印功能
        #endregion

        #region 初始化窗体
        public UCOldPartsPalautusQueryManage()
        {
            InitializeComponent();
            BindOrderStatus();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            base.ViewEvent += new ClickHandler(UCOldPartsPalautusQueryManage_ViewEvent);
            base.PrintEvent += new ClickHandler(UCOldPartsPalautusQueryManage_PrintEvent);
            base.SetEvent += new ClickHandler(UCOldPartsPalautusQueryManage_SetEvent);
            #region 预览、打印设置
            string printObject = "tb_maintain_oldpart_recycle";
            string printTitle = "宇通旧件返厂查询";
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
        void UCOldPartsPalautusQueryManage_SetEvent(object sender, EventArgs e)
        {
            businessPrint.PrintSet(dgvRData);
        }
        #endregion

        #region 打印事件
        void UCOldPartsPalautusQueryManage_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(dgvRData.GetBoundData());
        }
        #endregion

        #region 预览事件
        void UCOldPartsPalautusQueryManage_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(dgvRData.GetBoundData());
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
            base.btnVerify.Visible = false;
            base.btnSave.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnCancel.Visible = false;         
            base.btnActivation.Visible = false;
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
                        strWhere += string.Format(" and a.remarks = '{0}'", txtRemark.Caption.Trim());
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
                string fileName = "宇通旧件返厂查询" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvRData);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【宇通旧件返厂查询】" + ex.Message, "server");
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
        private void UCOldPartsPalautusQueryManage_Load(object sender, EventArgs e)
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

        #region 预览事件
        void UCOldPartsPalautusManager_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
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
            view.Quc = this;
            base.addUserControl(view, "宇通旧件返厂单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
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
