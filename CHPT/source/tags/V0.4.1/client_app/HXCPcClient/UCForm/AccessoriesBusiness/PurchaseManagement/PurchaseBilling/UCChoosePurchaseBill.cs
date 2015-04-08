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

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling
{
    public partial class UCChoosePurchaseBill : FormChooser
    {
        #region 变量、类
        private string order_type = string.Empty;
        private string sup_id = string.Empty;
        public int dgPlanRowIndex = -1;
        public List<PartsInfoClassByPurchaseBill> List_PartInfo = new List<PartsInfoClassByPurchaseBill>();
        public DataTable dt_PurchaseBill = null;
        #endregion

        #region 窗体初始化
        /// <summary> 窗体初始化
        /// </summary>
        public UCChoosePurchaseBill(string order_type, string sup_id)
        {
            InitializeComponent();

            this.order_type = order_type;
            this.sup_id = sup_id;

            string[] NotReadOnlyColumnsName1 = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(dgPurchaseOrder, NotReadOnlyColumnsName1);
            string[] NotReadOnlyColumnsName2 = new string[] { "colDetailCheck" };
            CommonFuncCall.SetColumnReadOnly(dgAccessoriesDetail, NotReadOnlyColumnsName2);

            //unit_id.ReadOnly = true;

            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;

            //CommonFuncCall.BindUnit(unit_id);

            //公司ID
            string com_id = GlobalStaticObj.CurrUserCom_Id;
            CommonFuncCall.BindDepartment(ddlorg_id, com_id, "请选择");
            CommonFuncCall.BindHandle(ddlhandle, "", "请选择");
            CommonFuncCall.BindHandle(ddloperator, "", "请选择");
            BinddgPurchaseOrder();

            //注册配件编码速查
            Choosefrm.PartsCodeChoose(txtparts_code, Choosefrm.delDataBack = PartsName_DataBack);
            //注册配件名称速查
            Choosefrm.PartsNameChoose(txtparts_name, Choosefrm.delDataBack = null);
            //注册配件类型速查
            Choosefrm.PartsTypeNameChoose(txtparts_type, Choosefrm.delDataBack = null);
        } 
        #endregion

        #region 控件事件
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
            frmPartsType choosePartsType = new frmPartsType();
            choosePartsType.ShowDialog();
            if (!string.IsNullOrEmpty(choosePartsType.TypeID))
            {
                txtparts_type.Text = choosePartsType.TypeName;
            }
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
            BinddgPurchaseOrder();
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
                List_PartInfo = new List<PartsInfoClassByPurchaseBill>();
                PartsInfoClassByPurchaseBill partsInfo = new PartsInfoClassByPurchaseBill();
                string CheckPlanID = string.Empty;
                //1.判断获取采购开单列表中选中的项
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
                            if (!List_PlanID.Contains(dr.Cells["purchase_billing_id"].Value.ToString()))
                            {
                                partsInfo = new PartsInfoClassByPurchaseBill();
                                partsInfo.billID = dr.Cells["purchase_billing_id"].Value.ToString();
                                partsInfo.parts_id = dr.Cells["parts_id"].Value.ToString();
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
                        DataTable dt_parts_purchase = DBHelper.GetTable("查询采购开单配件表信息", "tb_parts_purchase_billing_p", "*", " purchase_billing_id='" + billid + "' and finish_counts<business_counts ", "", "");
                        if (dt_parts_purchase.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_parts_purchase.Rows.Count; i++)
                            {
                                partsInfo = new PartsInfoClassByPurchaseBill();
                                partsInfo.billID = dt_parts_purchase.Rows[i]["purchase_billing_id"].ToString();
                                partsInfo.parts_id = dt_parts_purchase.Rows[i]["parts_id"].ToString();
                                partsInfo.parts_code = dt_parts_purchase.Rows[i]["parts_code"].ToString();
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
        /// <summary> 采购开单信息列表 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPurchaseOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用  
            {
                dgPlanRowIndex = e.RowIndex;
                string purchase_billing_id = Convert.ToString(this.dgPurchaseOrder.CurrentRow.Cells[0].Value.ToString());
                BinddgAccessoriesDetail(purchase_billing_id);
            }
        }
        /// <summary> 采购开单信息列表 单元格内容点击事件
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
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                string ColumnName = dgPurchaseOrder.Columns[e.ColumnIndex].Name;
                if (ColumnName.Equals("order_name"))
                {
                    e.Value = "采购开单";
                }
                return;
            }
            string fieldNmae = dgPurchaseOrder.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks).ToShortDateString();

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
            //采购开单可以导入的前提是：单据状态是已审核、导入状态是正常的 三种情况
            string Str_Where = " enable_flag=1 and order_status='2' and is_occupy in ('0','2') ";
            if (!string.IsNullOrEmpty(order_type))
            { Str_Where += " and order_type ='" + order_type + "' "; }
            if (!string.IsNullOrEmpty(sup_id))
            { Str_Where += " and sup_id='" + sup_id + "' "; }
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num like '%" + txtorder_num.Caption.Trim() + "%'";
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
        /// <summary> 绑定采购开单列表
        /// </summary>
        private void BinddgPurchaseOrder()
        {
            try
            {
                DataTable gvPurchaseBill_dt = DBHelper.GetTable("查询采购开单列表信息", "tb_parts_purchase_billing", "*", BuildString(), "", "");
                dgPurchaseOrder.DataSource = gvPurchaseBill_dt;
                dt_PurchaseBill = gvPurchaseBill_dt;
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
        /// <summary>绑定采购开单配件列表
        /// </summary>
        private void BinddgAccessoriesDetail(string purchase_billing_id)
        {
            if (!string.IsNullOrEmpty(purchase_billing_id))
            {
                string TableName = string.Format(@" 
                                                (
                                                select 
                                                ISNULL(business_price,0)*((ISNULL(tax_rate,0)/100)+1) tax_price,
                                                case when (ISNULL(business_counts,0)-ISNULL(finish_counts,0))>0 
                                                        then (ISNULL(business_counts,0)-ISNULL(finish_counts,0))
                                                        else 0 end surplus_counts,* 
                                                from tb_parts_purchase_billing_p
                                                ) a");
                DataTable dt_parts_purchase = DBHelper.GetTable("查询采购开单配件表信息", TableName, "*", " purchase_billing_id='" + purchase_billing_id + "' and isnull(finish_counts,0)<isnull(business_counts,0) ", "", "");
                if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
                {
                    dgAccessoriesDetail.DataSource = dt_parts_purchase;
                }
                else
                {
                    dgAccessoriesDetail.DataSource = null;
                }
            }
        } 
        #endregion

        #region --选择器获取数据后需执行的回调函数
        /// <summary> 配件编码速查关联控件赋值
        /// </summary>
        /// <param name="dr"></param>
        private void PartsName_DataBack(DataRow dr)
        {
            if (dr.Table.Columns.Contains("parts_name"))
            {
                this.txtparts_name.Text = dr["parts_name"].ToString();
            }
        }
        #endregion
    }
}
