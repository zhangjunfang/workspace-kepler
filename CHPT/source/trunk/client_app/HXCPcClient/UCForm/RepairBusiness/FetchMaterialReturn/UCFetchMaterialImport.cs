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

namespace HXCPcClient.UCForm.RepairBusiness.FetchMaterialReturn
{
    /// <summary>
    /// 维修管理-领料单导入
    /// Author：JC
    /// AddTime：2014.10.30
    /// </summary>
    public partial class UCFetchMaterialImport : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCFMaterialReturnAddOrEdit uc;
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 领料单Id
        /// </summary>
        public string strMaterialId = string.Empty;
        /// <summary>
        /// 领料单详情Id
        /// </summary>
        public string strDMaterialId = string.Empty;
        #endregion

        #region 初始化窗体
        public UCFetchMaterialImport()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSubmit, btnClear);  //设置查询按钮和清除按钮样式
            this.AcceptButton = btnSubmit;
            dgvRData.ReadOnly = false;
            dgvRData.Columns["material_num"].ReadOnly = true;
            dgvRData.Columns["fetch_opid"].ReadOnly = true;
            //dgvRData.Columns["parts_name"].ReadOnly = true;
            dgvRData.Columns["quantity"].ReadOnly = true;
            dgvRData.Columns["customer_name"].ReadOnly = true;
            dgvRData.Columns["customer_code"].ReadOnly = true;
            dgvRData.Columns["vehicle_no"].ReadOnly = true;
            dgvRData.Columns["fetch_time"].ReadOnly = true;
            dgvRData.Columns["fetch_id"].ReadOnly = true;
            //dgvRData.Columns["material_id"].ReadOnly = true;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
            BindData();

        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomName.Caption = string.Empty;
            txtOrderNo.Caption = string.Empty;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
        }
        #endregion

        #region 查询事件
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region 取消事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 绑定数据
        private void BindData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("领料时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strFiles = "a.material_num,a.fetch_opid,a.customer_name,a.customer_code,a.vehicle_no,a.vehicle_model,a.fetch_time,a.fetch_id,(select sum(received_num)from tb_maintain_fetch_material_detai b where  a.fetch_id=b.fetch_id)as quantity";
                strWhere = string.Format(" a.enable_flag='1' and (a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' or a.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString() + "') and a.Import_status='" + Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtOrderNo.Caption.Trim()))//领料单号
                {
                    strWhere += string.Format(" and  a.material_num like '%{0}%'", txtOrderNo.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  a.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                strWhere += string.Format(" and a.fetch_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                string strTables = "tb_maintain_fetch_material  a ";
                DataTable dt = DBHelper.GetTable("领料单导入信息列表", strTables, strFiles, strWhere, "", "order by a.fetch_time desc");
                dgvRData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region 当页保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            strMaterialId = CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["fetch_id"].Value);
           // strDMaterialId = CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["material_id"].Value);
            uc.strId = strMaterialId;  //领料单Id   
            uc.strDId = strDMaterialId;  //领料单详情Id  
            uc.BindSetmentData();
            this.DialogResult = DialogResult.OK;
            txtCustomName.Caption = string.Empty;
            txtOrderNo.Caption = string.Empty;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
        }
        #endregion

        #region 确定
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            strMaterialId = CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["fetch_id"].Value);
            //strDMaterialId = CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["material_id"].Value);
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["quantity"].Value)))
            {
                if (Convert.ToDecimal(CommonCtrl.IsNullToString(dgvRData.CurrentRow.Cells["quantity"].Value)) > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBoxEx.Show("领料数据为零不能进行导入退货单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBoxEx.Show("领料数据为零不能进行导入退货单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 判断是否选择了一条数据
        /// <summary>
        /// 判断是否选择了一条数据
        /// </summary>
        /// <returns></returns>
        private bool IsCheck()
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["fetch_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择领料单记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能选择1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 双击单元格事件
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                strMaterialId = dgvRData.Rows[e.RowIndex].Cells["fetch_id"].Value.ToString();
                //strDMaterialId = dgvRData.Rows[e.RowIndex].Cells["material_id"].Value.ToString();
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRData.Rows[e.RowIndex].Cells["quantity"].Value)))
                {
                    if (Convert.ToDecimal(CommonCtrl.IsNullToString(dgvRData.Rows[e.RowIndex].Cells["quantity"].Value)) > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBoxEx.Show("领料数据为零不能进行导入退货单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBoxEx.Show("领料数据为零不能进行导入退货单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
           
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
            if (fieldNmae.Equals("fetch_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            if (fieldNmae.Equals("vehicle_model"))
            {
                e.Value = GetVmodel(e.Value.ToString());
            }
            if (fieldNmae.Equals("fetch_opid"))
            {
                e.Value = GetSetName(e.Value.ToString());
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

        #region 获得领料人名称
        private string GetSetName(string strPid)
        {
            return DBHelper.GetSingleValue("获得退料人名称", "sys_user", "user_name", "user_id='" + strPid + "'", "");
        }
        #endregion

        #region 选择行
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

        #region 获取车型名称
        private string GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", "");
        }
        #endregion
    }
}
