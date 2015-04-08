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

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder
{
    public partial class UCChooseSalePlanOrder : FormChooser
    {
        #region 变量、类
        public int dgPlanRowIndex = -1;
        public List<PartsInfoClassBySalePlan> List_PartInfo = new List<PartsInfoClassBySalePlan>();
        public DataTable dt_SalePlanOrder = null;
        #endregion

        #region 窗体初始化
        /// <summary> 窗体初始化
        /// </summary>
        public UCChooseSalePlanOrder()
        {
            InitializeComponent();

            string[] NotReadOnlyColumnsName1 = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(dgSalePlan, NotReadOnlyColumnsName1);
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
            BinddgSalePlan();

            //注册配件编码速查
            Choosefrm.PartsCodeChoose(txtparts_code, Choosefrm.delDataBack = PartsName_DataBack);
            //注册配件名称速查
            Choosefrm.PartsNameChoose(txtparts_name, Choosefrm.delDataBack = null);
            //注册配件类型速查
            Choosefrm.PartsTypeNameChoose(txtparts_type, Choosefrm.delDataBack = null);
        } 
        #endregion

        #region 控件事件
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
            BinddgSalePlan();
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
        /// <summary> 配件编码选择器
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
        /// <summary> 配件名称选择器
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
        /// <summary> 配件类型选择器
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
        /// <summary> 单击销售计划单列表事件,加载配件明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSalePlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用  
            {
                dgPlanRowIndex = e.RowIndex;
                string sale_plan_id = Convert.ToString(this.dgSalePlan.CurrentRow.Cells[0].Value.ToString());
                BinddgAccessoriesDetail(sale_plan_id);
            }
        }
        /// <summary> 选择销售计划单信息，操作下级的配件明细信息的checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSalePlan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用  
            {
                bool ischeck = (bool)((DataGridViewCheckBoxCell)dgSalePlan.Rows[dgPlanRowIndex].Cells["colCheck"]).EditedFormattedValue;
                IsCheckdgAccessoriesDetail(ischeck);
            }
        }
        /// <summary> 配件明细信息checkbox选中事件
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
                    ((DataGridViewCheckBoxCell)dgSalePlan.Rows[dgPlanRowIndex].Cells["colCheck"]).Value = isChkis_Suspend;
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 格式化内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSalePlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                string ColumnName = dgSalePlan.Columns[e.ColumnIndex].Name;
                if (ColumnName.Equals("order_name"))
                {
                    e.Value = "销售计划单";
                }
                return;
            }
            string fieldNmae = dgSalePlan.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks).ToShortDateString();
            }
        }
        /// <summary> 全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            IsDataGridViewCheckBox(true);
        }
        /// <summary> 反选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotCheck_Click(object sender, EventArgs e)
        {
            IsDataGridViewCheckBox(false);
        }
        /// <summary> 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> List_PlanID = new List<string>();
                List_PartInfo = new List<PartsInfoClassBySalePlan>();
                PartsInfoClassBySalePlan partsInfo = new PartsInfoClassBySalePlan();
                string CheckPlanID = string.Empty;
                //1.判断获取采购计划单列表中选中的项
                if (dgSalePlan.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in dgSalePlan.Rows)
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
                            if (!List_PlanID.Contains(dr.Cells["sale_plan_id"].Value.ToString()))
                            {
                                partsInfo = new PartsInfoClassBySalePlan();
                                partsInfo.sale_plan_id = dr.Cells["sale_plan_id"].Value.ToString();
                                partsInfo.parts_id = dr.Cells["parts_id"].Value.ToString();
                                partsInfo.parts_code = dr.Cells["parts_code"].Value.ToString();
                                List_PartInfo.Add(partsInfo);
                            }
                        }
                    }
                }
                if (List_PlanID.Count > 0)
                {
                    foreach (string sale_plan_id in List_PlanID)
                    {
                        DataTable dt_parts_sale = DBHelper.GetTable("查询销售计划单配件表信息", "tb_parts_sale_plan_p", "*", " sale_plan_id='" + sale_plan_id + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
                        if (dt_parts_sale.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_parts_sale.Rows.Count; i++)
                            {
                                partsInfo = new PartsInfoClassBySalePlan();
                                partsInfo.sale_plan_id = dt_parts_sale.Rows[i]["sale_plan_id"].ToString();
                                partsInfo.parts_id = dt_parts_sale.Rows[i]["parts_id"].ToString();
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
        /// <summary> 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region 方法、函数
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            ////销售计划单可以导入的前提是：已审核、在有效期、未中止、完成数量小于计划数量、导入状态是正常的 五种情况
            //string Str_Where = " enable_flag=1 and order_status=2 and is_suspend=1 and import_status=0 and order_date>=" + Common.LocalDateTimeToUtcLong(DateTime.Now);
            
            ////销售计划单可以导入的前提是：已审核、在有效期、未中止、完成数量小于计划数量、导入状态是正常的 五种情况
            //string Str_Where = " enable_flag=1 and order_status=2 and is_suspend=1 and import_status=0 ";

            //销售计划单可以导入的前提是：已审核、在有效期、未中止、完成数量小于计划数量、导入状态是正常的和部分导入 五种情况
            string Str_Where = " enable_flag=1 and order_status=2 and is_suspend=1 and import_status in ('0','2') ";
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
            //if (!string.IsNullOrEmpty(txtparts_code.Text.Trim()))
            //{
            //    Str_Where += " and remark='" + txtparts_code.Text.Trim() + "'";
            //}
            //if (!string.IsNullOrEmpty(txtparts_code.Text.Trim()))
            //{
            //    Str_Where += " and remark='" + txtparts_code.Text.Trim() + "'";
            //}
            //if (!string.IsNullOrEmpty(txtparts_code.Text.Trim()))
            //{
            //    Str_Where += " and remark='" + txtparts_code.Text.Trim() + "'";
            //}
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
        /// <summary>
        /// 绑定销售计划单列表
        /// </summary>
        private void BinddgSalePlan()
        {
            try
            {
                DataTable gvSalePlanOrder_dt = DBHelper.GetTable("查询销售计划单列表信息", "tb_parts_sale_plan", "*", BuildString(), "", "");
                dgSalePlan.DataSource = gvSalePlanOrder_dt;
                dt_SalePlanOrder = gvSalePlanOrder_dt;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        /// <summary>
        /// 选中两个列表的checkbox
        /// </summary>
        /// <param name="ischeck"></param>
        void IsDataGridViewCheckBox(bool ischeck)
        {
            dgSalePlan.EndEdit();
            dgAccessoriesDetail.EndEdit();
            if (dgSalePlan.Rows.Count > 0)
            {
                for (int i = 0; i < dgSalePlan.Rows.Count; i++)
                {
                    ((DataGridViewCheckBoxCell)dgSalePlan.Rows[i].Cells["colCheck"]).Value = ischeck;
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
        /// <summary>
        /// 选中配件明细信息的checkbox
        /// </summary>
        /// <param name="ischeck"></param>
        void IsCheckdgAccessoriesDetail(bool ischeck)
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
        /// <summary>
        /// 绑定销售计划单配件列表
        /// </summary>
        private void BinddgAccessoriesDetail(string sale_plan_id)
        {
            if (!string.IsNullOrEmpty(sale_plan_id))
            {

                DataTable dt_parts_purchase = DBHelper.GetTable("查询销售计划单配件表信息", "tb_parts_sale_plan_p", "*", " sale_plan_id='" + sale_plan_id + "' and is_suspend=1 and isnull(finish_count,0)<isnull(business_count,0) ", "", "");
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
