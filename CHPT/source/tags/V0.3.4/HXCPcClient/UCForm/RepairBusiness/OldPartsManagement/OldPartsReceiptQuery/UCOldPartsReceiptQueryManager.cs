using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using SYSModel;
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsReceipt;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsReceiptQuery
{
    /// <summary>
    /// 旧件管理-旧件收货单查询
    /// Author：JC
    /// AddTime：2014.11.05
    /// </summary>
    public partial class UCOldPartsReceiptQueryManager : UCBase
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
        #endregion

        #region 初始化窗体
        public UCOldPartsReceiptQueryManager()
        {
            InitializeComponent();
            SetTopbuttonShow();
            SetDgvAnchor();
            BindOrderStatus();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            base.ViewEvent += new ClickHandler(UCOldPartsReceiptQueryManager_ViewEvent);
        }
        #endregion

        #region 预览事件
        void UCOldPartsReceiptQueryManager_ViewEvent(object sender, EventArgs e)
        {
            //DataView();
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

        #region 按旧件收货单查询

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

        #region  按旧件收货单查询客户编码选择器事件
        /// <summary>
        ///  按旧件收货单查询客户编码选择器事件
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

        #region 按旧件收货单查询清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContact.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtOrder.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            cobOrderStatus.SelectedValue = string.Empty;
            dtpReceiptSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReceiptETime.Value = DateTime.Now;
        }
        #endregion

        #region 按旧件收货单查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 按旧件收货单查询分页查询绑定数据
        /// <summary>
        /// 按旧件收货单查询分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReceiptSTime.Value > dtpReceiptETime.Value)
                {
                    MessageBoxEx.Show("收货日期,开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and a.receipt_type='1' and info_status in('1','2')");//enable_flag 1未删除receipt_type='1'为收货单

                if (!string.IsNullOrEmpty(txtCustomNO.Text.Trim()))//客户编码
                {
                    strWhere += string.Format(" and  a.customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  a.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtContact.Caption.Trim()))//联系人
                {
                    strWhere += string.Format(" and  a.linkman like '%{0}%'", txtContact.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOrderStatus.SelectedValue)))//单据状体
                {
                    strWhere += string.Format(" and a.info_status = '{0}'", cobOrderStatus.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtOrder.Caption.Trim())))//收货单号
                {
                    strWhere += string.Format(" and a.receipts_no like '%{0}%'", txtOrder.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtRemark.Caption.Trim())))//备注
                {
                    strWhere += string.Format(" and a.remarks like '%{0}%'", txtRemark.Caption.Trim());
                }

                strWhere += string.Format(" and a.receipt_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReceiptSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReceiptETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//领料时间
                string strFiles = @"*,(SELECT SUM(b.sum_money) FROM tb_maintain_oldpart_material_detail b  WHERE b.maintain_id=a.oldpart_id ) AS sum_money,
                                   (SELECT SUM(b.quantity) FROM tb_maintain_oldpart_material_detail b  WHERE b.maintain_id=a.oldpart_id ) AS quantity ";
                string strTables = "tb_maintain_oldpart_receiv_send a ";
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询旧件收货单管理", strTables, strFiles, strWhere, "", " order by a.receipt_time desc", page.PageIndex, page.PageSize, out recordCount);
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

        #region 按旧件收货单查询-重写datagridview中的时间、状态等
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
            if (fieldNmae.Equals("info_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            if (fieldNmae.Equals("org_name"))
            {
                e.Value = GetDepartmentName(e.Value.ToString());
            }
            if (fieldNmae.Equals("responsible_opid"))
            {
                e.Value = GetUserSetName(e.Value.ToString());
            }
        }
        #endregion

        #region 按旧件收货单查询-双击单元格进入预览窗体
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoubleClickCell(dgvRData, "oldpart_id");
        }
        #endregion
        #endregion

        #region 根据选型卡切换判断是哪个选选项卡而执行预览
        /// <summary>
        /// 根据选型卡切换判断是哪个选选项卡而执行预览
        /// </summary>
        private void DataView()
        {
            string strTabTag = tcQuery.SelectedIndex.ToString();//0为按旧件收货单查询，1为按配件查询旧件收货单
            if (strTabTag == "0")
            {
                if (!IsCheck(dgvRData, strTabTag))
                {
                    return;
                }
            }
            else if (strTabTag == "1")
            {
                if (!IsCheck(dgvPartsData, strTabTag))
                {
                    return;
                }
            }
            UCOldPartsReceiptView Qview = new UCOldPartsReceiptView(strReId);
            base.addUserControl(Qview, "旧件收货单-预览", "Qview" + strReId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 验证是否选择预览数据
        /// <summary>
        /// 验证是否选择预览数据
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="strTabtag"></param>
        /// <returns></returns>
        private bool IsCheck(DataGridView dgv, string strTabtag)
        {
            string strColCheck = string.Empty;//选择框的Name值
            string strIdName = string.Empty;//datagridview的Id-name值
            bool isOk = false;
            strColCheck = strTabtag == "0" ? "colCheck" : strTabtag == "1" ? "PcolCheck" : "";
            strIdName = strTabtag == "0" ? "oldpart_id" : strTabtag == "1" ? "Poldpart_id" : "";
            if (!string.IsNullOrEmpty(strColCheck) && !string.IsNullOrEmpty(strIdName))
            {

                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgv.Rows)
                {
                    object isCheck = dr.Cells[strColCheck].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells[strIdName].Value.ToString());
                        strReId = dr.Cells[strIdName].Value.ToString();
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择预览记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (listField.Count > 1)
                {
                    MessageBoxEx.Show(" 一次仅能预览1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (listField.Count == 1)
                {
                    isOk = true;
                }
            }
            return isOk;
        }
        #endregion

        #region 双击datagridview单元格进入浏览窗体
        /// <summary>
        /// 双击datagridview单元格进入浏览窗体
        /// </summary>
        /// <param name="dgvView">双击的datagridview</param>
        /// <param name="strIdName">datagridview中数据Id的name</param>
        private void DoubleClickCell(DataGridView dgvView, string strIdName)
        {
            if (dgvView.CurrentRow == null)
            {
                return;
            }
            strReId = dgvView.CurrentRow.Cells[strIdName].Value.ToString();
            UCOldPartsReceiptView view = new UCOldPartsReceiptView(strReId);
            base.addUserControl(view, "旧件收货单-预览", "view" + strReId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 窗体Load事件
        private void UCOldPartsReceiptQueryManager_Load(object sender, EventArgs e)
        {
            #region 按旧件收货单查询
            dtpReceiptSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReceiptETime.Value = DateTime.Now;
            BindPageData();//按旧件收货单
            #endregion
            #region 按配件查询旧件收货单查询
            dtpPSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPETime.Value = DateTime.Now;
            PBindPageData();//按配件查询旧件收货单
            #endregion
        }
        #endregion

        #region datagridview属性设置
        private void SetDgvAnchor()
        {
            #region 按旧件收货单查询
            palQBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
            dgvRData.Dock = DockStyle.Fill;
            //dgvRData.Columns["colCheck"].HeaderText = "选择";
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
            #endregion

            #region 按配件查询旧件收货单
            palPBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dgvPartsData.Dock = DockStyle.Fill;
            // dgvPartsData.Columns["PcolCheck"].HeaderText = "选择";
            dgvPartsData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvPartsData.Columns)
            {
                if (dgvc == PcolCheck)
                {
                    //dgvc.HeaderText = "选择";
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            #endregion


        }
        #endregion

        #region 获取部门名称
        private string GetDepartmentName(string strDId)
        {
            return DBHelper.GetSingleValue("获得部门名称", "tb_organization", "org_name", "org_id='" + strDId + "'", "");
        }
        #endregion

        #region 获得人员名称
        private string GetUserSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得人员名称", "tb_customer", "cust_name", "cust_id='" + strPid + "'", "");
        }
        #endregion

        #region 按配件查询旧件发货单
        #region 按配件查询旧件收货单清除事件
        private void btnPClear_Click(object sender, EventArgs e)
        {
            txtPartsCode.Text = string.Empty;
            txtPartsCode.Tag = string.Empty;
            txtPartsName.Caption = string.Empty;
            txtDrawingNo.Caption = string.Empty;
            txtPCustomCode.Text = string.Empty;
            txtPCustomCode.Tag = string.Empty;
            txtPCustomName.Caption = string.Empty;
            txtPContract.Caption = string.Empty;
            dtpPSTime.Value = DateTime.Now.AddMonths(-1);
            dtpPETime.Value = DateTime.Now;
        }
        #endregion

        #region 按配件查询旧件收货单查询事件
        private void btnPSelect_Click(object sender, EventArgs e)
        {
            PBindPageData();
        }
        #endregion

        #region 按配件查询旧件收货单分页查询绑定数据
        /// <summary>
        /// 按配件查询旧件收货单分页查询绑定数据
        /// </summary>
        public void PBindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpPSTime.Value > dtpPETime.Value)
                {
                    MessageBoxEx.Show("收货日期,开始日期不能大于结束日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                strWhere = string.Format(" a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and a.receipt_type='1' and info_status in('1','2')");//enable_flag 1未删除receipt_type='1'为收货单

                if (!string.IsNullOrEmpty(txtPartsCode.Text.Trim()))//配件编码
                {
                    strWhere += string.Format(" and  b.parts_code like '%{0}%'", txtPartsCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtPartsName.Caption.Trim()))//配件名称
                {
                    strWhere += string.Format(" and  b.parts_name like '%{0}%'", txtPartsName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtDrawingNo.Caption.Trim()))//图号
                {
                    strWhere += string.Format(" and  b.drawn_no like '%{0}%'", txtDrawingNo.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPCustomCode.Text.Trim())))//客户编码
                {
                    strWhere += string.Format(" and a.customer_code like '%{0}%'", txtPCustomCode.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPCustomName.Caption.Trim())))//客户名称
                {
                    strWhere += string.Format(" and a.customer_name like '%{0}%'", txtPCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtPContract.Caption.Trim())))//联系人
                {
                    strWhere += string.Format(" and a.linkman like '%{0}%'", txtPContract.Caption.Trim());
                }
                strWhere += string.Format(" and a.receipt_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpPSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpPETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//领料时间
                string strFiles = @"a.receipts_no,a.receipt_time,a.info_status,a.customer_code,a.customer_name,a.linkman,b.parts_code,b.parts_name,
                b.drawn_no,b.orders_no,b.vehicle_no,b.quantity,b.unit_price,b.sum_money,b.unit,b.whether_imported,b.vehicle_brand,
                a.create_name,a.org_name,a.responsible_name,b.remarks,a.oldpart_id";
                string strTables = "tb_maintain_oldpart_receiv_send a right join tb_maintain_oldpart_material_detail b on a.oldpart_id=b.maintain_id";
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("按配件查询旧件分页查询", strTables, strFiles, strWhere, "", " order by a.receipt_time desc", pageP.PageIndex, pageP.PageSize, out recordCount);
                dgvPartsData.DataSource = dt;
                pageP.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pageP_PageIndexChanged(object sender, EventArgs e)
        {
            PBindPageData();
        }

        #endregion

        #region  按配件查询旧件收货单客户编码选择器事件
        /// <summary>
        ///  按配件查询旧件收货单客户编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPCustomCode_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPCustomCode.Text = frmCInfo.strCustomerNo;
                txtPCustomCode.Tag = frmCInfo.strCustomerId;
                txtPCustomName.Caption = frmCInfo.strCustomerName;
            }
        }
        #endregion

        #region 按配件查询旧件收货单配件编码选择器事件
        /// <summary>
        /// 按配件查询旧件收货单配件编码选择器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts frmPart = new frmParts();
            DialogResult result = frmPart.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPartsCode.Text = frmPart.PartsCode;
                txtPartsCode.Tag = frmPart.PartsID;
                txtPartsName.Caption = frmPart.PartsName;
            }
        }
        #endregion

        #region 按配件查询旧件收货单查询-重写datagridview中的时间、状态等
        private void dgvPartsData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvPartsData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("receipt_time"))
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
            if (fieldNmae.Equals("org_name"))
            {
                e.Value = GetDepartmentName(e.Value.ToString());
            }
            if (fieldNmae.Equals("responsible_opid"))
            {
                e.Value = GetUserSetName(e.Value.ToString());
            }
            if (fieldNmae.Equals("vehicle_brand"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }


        }
        #endregion

        #region 按配件查询旧件收货单查询-双击单元格进入预览窗体
        private void dgvPartsData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoubleClickCell(dgvPartsData, "Poldpart_id");
        }
        #endregion
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

        #region 单击时选中行前的复选框      

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

        private void dgvPartsData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvPartsData.Rows)
            {
                object check = dgvr.Cells[PcolCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[PcolCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvPartsData.Rows[e.RowIndex].Cells[PcolCheck.Name].Value = true;
        }
        #endregion

    }
}
