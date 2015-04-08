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
using Utility.Common;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling
{
    public partial class UCChooseSaleBill : FormChooser
    {
        #region 变量、类
        private string order_type = string.Empty;
        private string cust_id = string.Empty;
        public int dgPlanRowIndex = -1;
        public List<PartsInfoClassBySaleBill> List_PartInfo = new List<PartsInfoClassBySaleBill>();
        public DataTable dt_SaleBill = null;
        #endregion

        #region 控件事件
        public UCChooseSaleBill(string order_type, string cust_id)
        {
            InitializeComponent();

            this.order_type = order_type;
            this.cust_id = cust_id;

            dgPurchaseOrder.ReadOnly = false;
            dgAccessoriesDetail.ReadOnly = false;
            //unit_id.ReadOnly = true;

            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;

            //CommonFuncCall.BindUnit(unit_id);

            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");
            CommonFuncCall.BindHandle(ddloperator, "", "请选择");

            BinddgSaleOrder();
        }
        /// <summary> 选择部门 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlorg_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFuncCall.BindHandle(ddlhandle, ddlorg_id.SelectedValue.ToString(), "请选择");
            CommonFuncCall.BindHandle(ddloperator, ddlorg_id.SelectedValue.ToString(), "请选择");
        }
        /// <summary> 选择配件编码 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_code.Text = chooseParts.PartsCode;
                txtparts_name.Text = chooseParts.PartsName;
            }
        }
        /// <summary> 选择配件名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_name_ChooserClick(object sender, EventArgs e)
        {
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_name.Text = chooseParts.PartsName;
            }
        }
        /// <summary> 选择配件类型 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type_ChooserClick(object sender, EventArgs e)
        {

        }
        /// <summary>清除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;
            txtorder_num.Caption = string.Empty;
            ddlorg_id.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
            ddloperator.SelectedIndex = 0;
            txtparts_code.Tag = string.Empty;
            txtparts_code.Text = string.Empty;

            txtparts_name.Tag = string.Empty;
            txtparts_name.Text = string.Empty;

            txtparts_type.Tag = string.Empty;
            txtparts_type.Text = string.Empty;
        }
        /// <summary> 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BinddgSaleOrder();
        }
        /// <summary>全选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            IsDataGridViewCheckBox(true);
        }
        /// <summary>反选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotCheck_Click(object sender, EventArgs e)
        {
            IsDataGridViewCheckBox(false);
        }
        /// <summary>确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> List_PlanID = new List<string>();
                List_PartInfo = new List<PartsInfoClassBySaleBill>();
                PartsInfoClassBySaleBill partsInfo = new PartsInfoClassBySaleBill();
                string CheckPlanID = string.Empty;
                //1.判断获取销售开单列表中选中的项
                if (dgPurchaseOrder.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgPurchaseOrder.Rows)
                    {
                        object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                        if (isCheck != null && (bool)isCheck)
                        {
                            List_PlanID.Add(dr.Cells["ID"].Value.ToString());
                        }
                    }
                }
                //2.判断获取配件明细列表中选中的项
                if (dgAccessoriesDetail.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgAccessoriesDetail.Rows)
                    {
                        object isCheck = dr.Cells["colDetailCheck"].EditedFormattedValue;
                        if (isCheck != null && (bool)isCheck)
                        {
                            if (!List_PlanID.Contains(dr.Cells["sale_billing_id"].Value.ToString()))
                            {
                                partsInfo = new PartsInfoClassBySaleBill();
                                partsInfo.sale_billing_id = dr.Cells["sale_billing_id"].Value.ToString();
                                partsInfo.parts_code = dr.Cells["parts_code"].Value.ToString();
                                List_PartInfo.Add(partsInfo);
                            }
                        }
                    }
                }
                if (List_PlanID.Count > 0)
                {
                    foreach (string billid in List_PlanID)
                    {
                        DataTable dt_parts_sale = DBHelper.GetTable("查询销售开单配件表信息", "tb_parts_sale_billing_p", "*", " sale_billing_id='" + billid + "' and finish_count<business_count ", "", "");
                        if (dt_parts_sale.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_parts_sale.Rows.Count; i++)
                            {
                                partsInfo = new PartsInfoClassBySaleBill();
                                partsInfo.sale_billing_id = dt_parts_sale.Rows[i]["sale_billing_id"].ToString();
                                partsInfo.parts_code = dt_parts_sale.Rows[i]["parts_code"].ToString();
                                List_PartInfo.Add(partsInfo);
                            }
                        }
                    }
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            { }
            finally
            { dgPlanRowIndex = -1; }
        }
        /// <summary>关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary> 销售开单信息列表 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPurchaseOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用  
            {
                dgPlanRowIndex = e.RowIndex;
                string sale_billing_id = Convert.ToString(this.dgPurchaseOrder.CurrentRow.Cells[0].Value.ToString());
                BinddgAccessoriesDetail(sale_billing_id);
            }
        }
        /// <summary> 销售开单信息列表 单元格内容点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPurchaseOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用  
            {
                bool ischeck = (bool)((DataGridViewCheckBoxCell)dgPurchaseOrder.Rows[dgPlanRowIndex].Cells["colCheck"]).EditedFormattedValue;
                IsCheckdgAccessoriesDetail(ischeck);
            }
        }
        /// <summary> 格式化内容信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPurchaseOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                string ColumnName = dgPurchaseOrder.Columns[e.ColumnIndex].Name;
                if (ColumnName.Equals("order_name"))
                {
                    e.Value = "销售开单";
                }
                return;
            }
            string fieldNmae = dgPurchaseOrder.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }
        /// <summary>配件明细列表 点击单元格内容事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgAccessoriesDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
                {
                    bool isChkis_Suspend = true;
                    //获取控件的值
                    for (int i = 0; i < dgAccessoriesDetail.Rows.Count; i++)
                    {
                        object ischeck = dgAccessoriesDetail.Rows[i].Cells["colDetailCheck"].EditedFormattedValue;
                        if (ischeck != null && !(bool)ischeck)
                        {
                            isChkis_Suspend = false;
                            break;
                        }
                    }
                    ((DataGridViewCheckBoxCell)dgPurchaseOrder.Rows[dgPlanRowIndex].Cells["colCheck"]).Value = isChkis_Suspend;
                }
            }
            catch (Exception ex)
            { }
        } 
        #endregion

        #region 方法、函数
        /// <summary>组合查询条件
        /// </summary>
        /// <returns></returns>
        private string BuildString()
        {
            //销售开单可以导入的前提是：单据状态是已换货、已退货、导入状态是正常的 三种情况
            string Str_Where = " enable_flag=1 and is_occupy in ('0','2')";
            if (!string.IsNullOrEmpty(order_type))
            { Str_Where += " and order_type ='" + order_type + "' "; }
            if (!string.IsNullOrEmpty(cust_id))
            { Str_Where += " and cust_id='" + cust_id + "' "; }
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num='" + txtorder_num.Caption.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(ddlorg_id.SelectedValue.ToString()))
            {
                Str_Where += " and org_id='" + ddlorg_id.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                Str_Where += " and handle='" + ddlhandle.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddloperator.SelectedValue.ToString()))
            {
                Str_Where += " and operators='" + ddloperator.SelectedItem.ToString() + "'";
            }
            //配件名称
            //配件类型
            if (dateTimeStart.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeStart.Value.ToShortDateString() + " 00:00:00");
                Str_Where += " and order_date>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeEnd.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeEnd.Value.ToShortDateString() + " 23:59:59");
                Str_Where += " and order_date<=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            return Str_Where;
        }
        /// <summary> 绑定销售开单列表
        /// </summary>
        private void BinddgSaleOrder()
        {
            try
            {
                DataTable gvSaleBill_dt = DBHelper.GetTable("查询销售开单列表信息", "tb_parts_sale_billing", "*", BuildString(), "", "");
                dgPurchaseOrder.DataSource = gvSaleBill_dt;
                dt_SaleBill = gvSaleBill_dt;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        /// <summary>选中配件明细信息的checkbox
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsCheckdgAccessoriesDetail(bool ischeck)
        {
            dgAccessoriesDetail.EndEdit();
            if (dgAccessoriesDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dgAccessoriesDetail.Rows.Count; i++)
                {
                    ((DataGridViewCheckBoxCell)dgAccessoriesDetail.Rows[i].Cells["colDetailCheck"]).Value = ischeck;
                }
            }
        }
        /// <summary> 选中两个列表的checkbox
        /// </summary>
        /// <param name="ischeck"></param>
        private void IsDataGridViewCheckBox(bool ischeck)
        {
            dgPurchaseOrder.EndEdit();
            dgAccessoriesDetail.EndEdit();
            if (dgPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < dgPurchaseOrder.Rows.Count; i++)
                {
                    ((DataGridViewCheckBoxCell)dgPurchaseOrder.Rows[i].Cells["colCheck"]).Value = ischeck;
                }
            }
            if (dgAccessoriesDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dgAccessoriesDetail.Rows.Count; i++)
                {
                    ((DataGridViewCheckBoxCell)dgAccessoriesDetail.Rows[i].Cells["colDetailCheck"]).Value = ischeck;
                }
            }
        }
        /// <summary>绑定销售开单配件列表
        /// </summary>
        private void BinddgAccessoriesDetail(string sale_billing_id)
        {
            if (!string.IsNullOrEmpty(sale_billing_id))
            {
                DataTable dt_parts_sale = DBHelper.GetTable("查询销售开单配件表信息", "tb_parts_sale_billing_p", "*", " sale_billing_id='" + sale_billing_id + "' and finish_count<business_count ", "", "");
                if (dt_parts_sale != null && dt_parts_sale.Rows.Count > 0)
                {
                    dgAccessoriesDetail.DataSource = dt_parts_sale;
                }
                else
                {
                    dgAccessoriesDetail.DataSource = null;
                }
            }
        }
        #endregion
    }
}
