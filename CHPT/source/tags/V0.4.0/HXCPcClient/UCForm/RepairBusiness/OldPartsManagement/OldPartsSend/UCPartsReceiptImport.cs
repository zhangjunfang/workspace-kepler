using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using HXCPcClient.CommonClass;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsSend
{
    /// <summary>
    /// 旧件管理-旧件收货单导入
    /// Author：JC
    /// AddTime：2014.11.27
    /// </summary>
    public partial class UCPartsReceiptImport : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 旧件收货单Id
        /// </summary>
        public string strOldId = string.Empty;
        /// <summary>
        /// 旧件收货单对应的配件详情Id串
        /// </summary>
        public string strOlddetailId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOldPartsSendAddOrEdit uc;
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        #endregion

        #region 初始化窗体
        public UCPartsReceiptImport()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSubmit, btnClear);  //设置查询按钮和清除按钮样式
            this.AcceptButton = btnSubmit;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
            SetDgvAnchor();
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOrder.Caption = string.Empty;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
        }
        #endregion

        #region datagridview属性设置
        /// <summary>
        /// datagridview属性设置
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvRDetailData.ReadOnly = false;           
            dgvRDetailData.Columns["parts_code"].ReadOnly = true;
            dgvRDetailData.Columns["parts_name"].ReadOnly = true;
            dgvRDetailData.Columns["drawn_no"].ReadOnly = true;
            dgvRDetailData.Columns["unit"].ReadOnly = true;
            dgvRDetailData.Columns["quantity"].ReadOnly = true;
            dgvRDetailData.Columns["unit_price"].ReadOnly = true;
            dgvRDetailData.Columns["sum_money"].ReadOnly = true;
            dgvRDetailData.Columns["whether_imported"].ReadOnly = true;
            dgvRDetailData.Columns["vehicle_model"].ReadOnly = true;            
            dgvRDetailData.Columns["remarks"].ReadOnly = true;            
            dgvRDetailData.Columns["parts_id"].ReadOnly = true;

        }
        #endregion

        #region 查询事件
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 绑定数据
        private void BindData()
        {
            try
            {
                #region 事件选择判断
                if (dtpSTime.Value > dtpETime.Value)
                {
                    MessageBoxEx.Show("单据时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' and Import_status='0' and receipt_type ='1'");//enable_flag 1未删除,receipt_type=1表示收货单
                if (!string.IsNullOrEmpty(txtOrder.Caption.Trim()))//收货单号
                {
                    strWhere += string.Format(" and  receipts_no like '%{0}%'", txtOrder.Caption.Trim());
                }
                strWhere += string.Format(" and receipt_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//发货日期
                string strTables = "tb_maintain_oldpart_receiv_send";
                DataTable dt = DBHelper.GetTable("旧件收货单单导入信息列表", strTables, "*", strWhere, "", "order by receipt_time desc");
                dgvRData.DataSource = dt;
                DgvDetail();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region 将查询到的订单详细信息绑定到订单详细dgv上
        /// <summary>
        /// 将查询到的订单详细信息绑定到订单详细dgv上
        /// </summary>
        private void DgvDetail()
        {
            if (this.dgvRData.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[0].Cells["oldpart_id"].Value)))
                {
                    return;
                }
                string strId = this.dgvRData.Rows[0].Cells["oldpart_id"].Value.ToString();
                DataTable ddt = DBHelper.GetTable("旧件收货单单导入详细信息列表", "tb_maintain_oldpart_material_detail", "*", "maintain_id='" + strId + "'", "", "order by parts_id desc");
                this.dgvRDetailData.DataSource = ddt.Rows.Count > 0 ? ddt : null;
            }
        }
        #endregion

        #region 窗体Load事件
        private void UCPartsReceiptImport_Load(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 单击收货单显示其详细信息
        private void dgvRData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex >= 0)
            {
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[rowindex].Cells["oldpart_id"].Value)))
                {
                    return;
                }
                string strDId = this.dgvRData.Rows[rowindex].Cells["oldpart_id"].Value.ToString();
                DataTable dt = DBHelper.GetTable("旧件收货单单导入详细信息列表", "tb_maintain_oldpart_material_detail", "*", "maintain_id='" + strDId + "'", "", "order by parts_id desc");
                this.dgvRDetailData.DataSource = dt.Rows.Count > 0 ? dt : null;
            }
        }
        #endregion

        #region 取消功能
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 导入事件
        private void btnImport_Click(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            #region 获取服务单号
            foreach (DataGridViewRow dr in dgvRDetailData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strOlddetailId += "'" + dr.Cells["parts_id"].Value.ToString() + "'" + ",";
                    strOldId = dr.Cells["maintain_id"].Value.ToString();
                    listField.Add(dr.Cells["parts_id"].Value.ToString());
                }
            }
            #endregion
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要导入的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            } 
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region 双击事件
        private void dgvRDetailData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            strOlddetailId += "'" + dgvRDetailData.Rows[e.RowIndex].Cells["parts_id"].Value.ToString() + "'" + ",";
            strOldId = dgvRDetailData.Rows[e.RowIndex].Cells["maintain_id"].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region 选择行
        private void dgvRDetailData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvRDetailData.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvRDetailData.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion
    }
}
