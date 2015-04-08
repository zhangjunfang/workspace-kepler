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
using HXCPcClient.CommonClass;
using Utility.Common;

namespace HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.YTOldPartsTagPrint
{
    /// <summary>
    /// 维修管理-宇通旧件返厂单导入
    /// Author：JC
    /// AddTime：2014.11.04
    /// </summary>
    public partial class UCYTOldPartsImport : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCYTOldPartsTagPrintManager uc;
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 服务单号
        /// </summary>
        public string strServersNo = string.Empty;
        /// <summary>
        /// 配件Id
        /// </summary>
        public string strPartsId = string.Empty;
        #endregion
        public UCYTOldPartsImport()
        {
            InitializeComponent();
            this.AcceptButton = btnSubmit;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
            SetDgvAnchor();
        }

        #region datagridview属性设置
        /// <summary>
        /// datagridview属性设置
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvRDetailData.ReadOnly = false;
            dgvRDetailData.Columns["service_no"].ReadOnly = true;
            dgvRDetailData.Columns["parts_code"].ReadOnly = true;
            dgvRDetailData.Columns["parts_name"].ReadOnly = true;
            dgvRDetailData.Columns["change_num"].ReadOnly = true;
            dgvRDetailData.Columns["send_num"].ReadOnly = true;
            dgvRDetailData.Columns["receive_num"].ReadOnly = true;
            dgvRDetailData.Columns["unit"].ReadOnly = true;
            dgvRDetailData.Columns["need_recycle_mark"].ReadOnly = true;
            dgvRDetailData.Columns["all_recycle_mark"].ReadOnly = true;
            dgvRDetailData.Columns["original"].ReadOnly = true;
            dgvRDetailData.Columns["process_mode"].ReadOnly = true;
            dgvRDetailData.Columns["remarks"].ReadOnly = true;
            dgvRDetailData.Columns["receive_explain"].ReadOnly = true;
            dgvRDetailData.Columns["parts_id"].ReadOnly = true;

        }
        #endregion

        #region 清除功能
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOrderNo.Caption = string.Empty;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
        }
        #endregion

        #region 查询功能
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
                string strFiles = @"a.receipts_no,a.oldpart_receipts_no,a.info_status,a.info_status_yt,
                (SELECT SUM(b.change_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS change_num,
                (SELECT SUM(b.send_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS send_num,
                (SELECT SUM(b.receive_num) FROM tb_maintain_oldpart_recycle_material_detail b  WHERE b.maintain_id=a.return_id ) AS receive_num,
                a.receipt_time,create_time_start,a.create_time_end,a.remarks,a.return_id ";
                strWhere = string.Format(" a.enable_flag='1' and a.info_status in ('1','2')");//enable_flag 1未删除,a.info_status in ('1','2')表示已提交或已审核的
                if (!string.IsNullOrEmpty(txtOrderNo.Caption.Trim()))//返厂单号
                {
                    strWhere += string.Format(" and  a.receipts_no like '%{0}%'", txtOrderNo.Caption.Trim());
                }
                strWhere += string.Format(" and a.receipt_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpETime.Value.Date));//单据时间
                string strTables = "tb_maintain_oldpart_recycle a";
                DataTable dt = DBHelper.GetTable("宇通旧件返厂单导入信息列表", strTables, strFiles, strWhere, "", "order by a.receipt_time desc");
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
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[0].Cells["return_id"].Value)))
                {
                    return;
                }
                string strId = this.dgvRData.Rows[0].Cells["return_id"].Value.ToString();
                DataTable ddt = DBHelper.GetTable("宇通旧件返厂单导入详细信息列表", "tb_maintain_oldpart_recycle_material_detail", "*", "maintain_id='" + strId + "'", "", "order by parts_id desc");
                this.dgvRDetailData.DataSource = ddt.Rows.Count > 0 ? ddt : null;
            }
        }
        #endregion

        #region 取消功能
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 导入功能
        private void btnImport_Click(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            #region 获取服务单号
            foreach (DataGridViewRow dr in dgvRDetailData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strServersNo += "'" + dr.Cells["service_no"].Value.ToString() + "'" + ",";
                    strPartsId += "'" + dr.Cells["parts_id"].Value.ToString() + "'" + ",";
                    listField.Add(dr.Cells["service_no"].Value.ToString());
                }
            }
            #endregion
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要导入的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (listField.Count > 4)
            {
                MessageBoxEx.Show("目前一次最多仅能导入4条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region 窗体Load事件
        private void UCYTOldPartsImport_Load(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 单击宇通旧件返厂信息显示其对应的详细信息
        private void dgvRData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex >= 0)
            {
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[rowindex].Cells["return_id"].Value)))
                {
                    return;
                }
                string strDId = this.dgvRData.Rows[rowindex].Cells["return_id"].Value.ToString();
                DataTable dt = DBHelper.GetTable("宇通旧件返厂单导入详细信息列表", "tb_maintain_oldpart_recycle_material_detail", "*", "maintain_id='" + strDId + "'", "", "order by parts_id desc");
                this.dgvRDetailData.DataSource = dt.Rows.Count > 0 ? dt : null;
            }
        }
        #endregion

        #region 双击事件
        private void dgvRDetailData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            strServersNo += "'" + dgvRDetailData.Rows[e.RowIndex].Cells["service_no"].Value.ToString() + "'" + ",";
            strPartsId += "'" + dgvRDetailData.Rows[e.RowIndex].Cells["parts_id"].Value.ToString() + "'" + ",";
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

        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("receipt_time") || fieldNmae.Equals("create_time_start"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
        }

        private void dgvRDetailData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("process_mode"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    e.Value = GetDicName(e.Value.ToString());
                }
            }

        }

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
    }
}
