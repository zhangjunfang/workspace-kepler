using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HXCPcClient.Chooser;
using DevComponents.DotNetBar;
using HXCPcClient.UCForm.RepairBusiness.YuTongThreeGuaranty.MaintainThreeGuaranty;
using Utility.Common;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm.RepairBusiness.FetchMaterial;
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsReceipt;
using HXCPcClient.UCForm.RepairBusiness.OldPartsManagement.OldPartsSend;
using HXCPcClient.UCForm.RepairBusiness.RepairBalance;
using SYSModel;

namespace HXCPcClient.UCForm.RepairBusiness.RepairCallback
{
    public partial class UCRepairCallbackImport : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 返修单父窗体
        /// </summary>
        public UCRepairCallbackAddOrEdit uc;
        /// <summary>
        /// 领料单父窗体
        /// </summary>
        public UCFetchMaterialAddOrEdit Fetchuc;
        /// <summary>
        /// 旧件收货单父窗体
        /// </summary>
        public UCOldPartsReceiptAddOrEdit PatrsRuc;
        /// <summary>
        /// 旧件发货单父窗体
        /// </summary>
        public UCOldPartsSendAddOrEdit PartsSenduc;
        /// <summary>
        /// 三包服务单父窗体
        /// </summary>
        public UCMaintainThreeGuarantyEdit MaintainThreeGuarantyEdit;       
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 父窗体0代表返修单，1代表领料单,2代表旧件收货单,3代表旧件发货单,4三包服务单
        /// </summary>
        public string strTag = "0";

        string strRId = string.Empty;
        #endregion

        public UCRepairCallbackImport()
        {
            InitializeComponent();
            dgvRData.ReadOnly = false;
            dgvRData.Columns["maintain_no"].ReadOnly = true;
            dgvRData.Columns["create_time"].ReadOnly = true;
            dgvRData.Columns["customer_name"].ReadOnly = true;
            dgvRData.Columns["vehicle_no"].ReadOnly = true;
            dgvRData.Columns["item_no"].ReadOnly = true;
            dgvRData.Columns["item_type"].ReadOnly = true;
            dgvRData.Columns["item_name"].ReadOnly = true;
            dgvRData.Columns["man_hour_type"].ReadOnly = true;
            dgvRData.Columns["man_hour_quantity"].ReadOnly = true;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;          
        }

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCarNO.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            dtpReserveSTime.Value = DateTime.Now.AddMonths(-1);
            dtpReserveETime.Value = DateTime.Now;
        }
        #endregion

        #region 导入功能
        private void btnImport_Click(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            #region 获取信息Id值

            foreach (DataGridViewRow dr in dgvRData.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    strRId = dr.Cells["maintain_id"].Value.ToString();
                    listField.Add(dr.Cells["maintain_id"].Value.ToString());
                }
            }
            #endregion
            if (listField.Count <= 0)
            {
                MessageBoxEx.Show("请选择需要导入的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次仅能导入1条数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            if (strTag == "0")//返修单
            {
                uc.strBefore_orderId = strRId;  //导入单据的Id值          
                uc.BindSetmentData();
            }
            else if (strTag == "1")//代表领料单
            {
                Fetchuc.strBefore_orderId = strRId;
                Fetchuc.BindSetmentData();
            }
            else if (strTag == "2")//旧件收货单
            {
                PatrsRuc.strBefore_orderId = strRId;
                PatrsRuc.BindSetmentData();
            }
            //else if (strTag == "3")//旧件发货单
            //{
            //    PartsSenduc.strBefore_orderId = strRId;
            //    PartsSenduc.BindSetmentData();
            //}
            else if (strTag == "4") //三包服务单
            {
                MaintainThreeGuarantyEdit.BeforeOrderId = strRId;
                MaintainThreeGuarantyEdit.BindSetmentData();
            }           
            Close();
        }
        #endregion

        #region 取消功能
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 查询功能
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            BindDataByOType();
        }
        #endregion

        #region 返修单所需数据绑定数据
        private void BindData()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("结算时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strFiles = " b.maintain_no,a.create_time,b.customer_name,b.vehicle_no,c.item_no,c.item_type,c.item_name,c.man_hour_type,c.man_hour_quantity,a.maintain_id";
                strWhere = string.Format(" b.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' and b.info_status='" + Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString() + "' and b.Import_status='" + Convert.ToInt32(DataSources.EnumImportStaus.OPEN).ToString() + "'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  b.vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  b.customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                strWhere += string.Format(" and a.create_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date.AddDays(1).AddMilliseconds(-1)));//接待日期
                string strTables = "tb_maintain_settlement_info  a left join tb_maintain_info b on a.maintain_id=b.maintain_id left join tb_maintain_item c on a.maintain_id=c.maintain_id ";
                DataTable dt = DBHelper.GetTable("维修明细导入信息列表", strTables, strFiles, strWhere, "", "order by a.create_time desc");
                dgvRData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region 领料单所选数据绑定
        private void BindDataP()
        {
            try
            {
                #region 事件选择判断
                if (dtpReserveSTime.Value > dtpReserveETime.Value)
                {
                    MessageBoxEx.Show("结算时间,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                string strFiles = " *";
                strWhere = string.Format(" enable_flag='1' and info_status='2' and Import_status='0'");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtCarNO.Text.Trim()))//车牌号
                {
                    strWhere += string.Format(" and  vehicle_no like '%{0}%'", txtCarNO.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))//客户名称
                {
                    strWhere += string.Format(" and  customer_name like '%{0}%'", txtCustomName.Caption.Trim());
                }
                strWhere += string.Format(" and create_time BETWEEN {0} and {1}", Common.LocalDateTimeToUtcLong(dtpReserveSTime.Value.Date), Common.LocalDateTimeToUtcLong(dtpReserveETime.Value.Date));//接待日期
                string strTables = "tb_maintain_info";
                DataTable dt = DBHelper.GetTable("维修明细导入信息列表", strTables, strFiles, strWhere, "", "order by create_time desc");
                dgvRData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 绑定不同类型单据所需要信息
        private void BindDataByOType()
        {
            if (strTag == "0")//返修单
            {
                BindData();
            }
            else if (strTag == "1")//代表领料单
            {
                BindDataP();
            }
            else if (strTag == "2")//旧件收货单
            {
                BindDataP();
            }
            else if (strTag == "3")//旧件发货单
            {
                BindDataP();
            }
            else if (strTag == "4") //三包服务单
            {
                BindDataP();
            }
          
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairCallbackImport_Load(object sender, EventArgs e)
        {
            BindDataByOType();
        }
        #endregion

        #region 重写时间
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRData.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
        }
        #endregion
    }
}
