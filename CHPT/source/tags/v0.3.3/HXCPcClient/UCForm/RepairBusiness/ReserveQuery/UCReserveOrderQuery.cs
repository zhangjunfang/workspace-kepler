using System;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using HXCPcClient.Chooser;
using System.Collections.Generic;
using HXCPcClient.UCForm.RepairBusiness.Reserve;

namespace HXCPcClient.UCForm.RepairBusiness.ReserveQuery
{
    public partial class UCReserveOrderQuery : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 查询条件
        /// </summary>
        private string strWhere = string.Empty;
        private DataTable dt;
        /// <summary>
        /// 单据ID值
        /// </summary>
        string strReId = string.Empty;
        #endregion

        #region 初始化窗体
        public UCReserveOrderQuery()
        {
            InitializeComponent();         
        }
        public UCReserveOrderQuery(DataTable _dt)
        {
            InitializeComponent();
            this.dt = _dt;
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
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnStatus.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnVerify.Visible = false;            
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 窗体Load事件
        private void UCReserveOrderQuery_Load(object sender, EventArgs e)
        {
            CommonCtrl.CmbBindDict(cobPayType, "sys_repair_pay_methods", true);//绑定维修付费方式
            this.SetTopbuttonShow();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式

            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
            dtpReInSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReInETime.Value = DateTime.Now;
            dgvRData.Dock = DockStyle.Fill;
            dgvRData.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvRData.Columns)
            {
                if (dgvc == colCheck)
                {                    
                    continue;
                }
                dgvc.ReadOnly = true;
            }
            if (dt == null)
            {
                this._BindPageData();
            }
            else
            {
                this.btnQuery.Visible = false;
                this.BindPageData();
            }
            this.page.PageIndexChanged -= new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {           
            DataTable _dt = DBHelper.GetTableByPage(this.dt, page.PageIndex, page.PageSize);
            dgvRData.DataSource = _dt;
            page.RecordCount = _dt.Rows.Count;
        }
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void _BindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("预约日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtpReInSTime.Value > dtpReInETime.Value)
                {
                    MessageBoxEx.Show("预约进场时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                string strFiles = @" reservation_no,reservation_date,maintain_time,reservation_man,reservation_mobile,case whether_greet when '1'  then '是'  when '0' then  '否' else '其他' end  as whether_greet,greet_site
                                     ,fault_describe,document_status,customer_code,customer_name,vehicle_no,linkman,link_man_mobile,maintain_payment,maintain_no,remark,reserv_id";
                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'and document_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString()+ "' ");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCarNO.Text.Trim())))//车牌号
                {
                    strWhere += string.Format(" and  vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Text.Trim())))//客户编码
                {
                    strWhere += string.Format(" and  customer_code like '%{0}%'", txtCustomNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtReserverNO.Caption.Trim()))//预约单号
                {
                    strWhere += string.Format(" and  reservation_no like '%{0}%'", txtReserverNO.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtRepairNO.Caption.Trim()))//维修单号
                {
                    strWhere += string.Format(" and  maintain_no like '%{0}%'", txtRepairNO.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtContact.Caption.Trim()))//联系人
                {
                    strWhere += string.Format(" and  linkman like '%{0}%'", txtContact.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtContactPhone.Caption.Trim()))//联系人手机
                {
                    strWhere += string.Format(" and  link_man_mobile like '%{0}%'", txtContactPhone.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtReservationman.Caption.Trim()))//预约人
                {
                    strWhere += string.Format(" and  reservation_man like '%{0}%'", txtReservationman.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(cobPayType.SelectedValue.ToString()))//维修付费方式
                {
                    strWhere += string.Format(" and  maintain_payment like '%{0}%'", cobPayType.SelectedValue.ToString());
                }               
                strWhere += string.Format(" and  reservation_date BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//预约日期
                strWhere += string.Format(" and maintain_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReInSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReInETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//预约进场时间
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询预约单管理", "tb_maintain_reservation", strFiles, strWhere, "", 
                                                          " order by create_time desc", page.PageIndex, page.PageSize, out recordCount);
                dgvRData.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                //MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {            
            if (dt == null)
            {
                this._BindPageData();
            }
            else
            {                
                this.BindPageData();
            }
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

        #region 查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            _BindPageData();
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Text = string.Empty;
            txtCarNO.Tag = string.Empty;
            txtContact.Caption = string.Empty;
            txtContactPhone.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtCustomNO.Text = string.Empty;
            txtCustomNO.Tag = string.Empty;
            txtReservationman.Caption = string.Empty;
            txtRepairNO.Caption = string.Empty;
            txtReserverNO.Caption = string.Empty;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
            dtpReInSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReInETime.Value = DateTime.Now;
            cobPayType.SelectedValue = string.Empty;
            txtCarNO.Focus();
        }
        #endregion

        #region 重写datagridview中的时间、状态等
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("reservation_date") || fieldNmae.Equals("maintain_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            else if (fieldNmae.Equals("document_status"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAuditStatus), int.Parse(e.Value.ToString()));
            }
            else if (fieldNmae.Equals("maintain_payment"))
            {
                e.Value = LocalCache.GetDictNameById(e.Value.ToString());
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
           
        }
        #endregion

        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRData.CurrentRow == null)
            {
                return;
            }
             strReId = dgvRData.CurrentRow.Cells["reserv_id"].Value.ToString();
            ViewData("1");
        }
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
            ReserveOrderView view = new ReserveOrderView(strReId);
            ReserveOrder rorder = new ReserveOrder();
            view.uc = rorder;
            base.addUserControl(view, "维修预约单-预览", "ReserveOrderView" + strReId, this.Tag.ToString(), this.Name);
        }
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
                    listField.Add(dr.Cells["reserv_id"].Value.ToString());
                    strReId = dr.Cells["reserv_id"].Value.ToString();
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
    }
}
