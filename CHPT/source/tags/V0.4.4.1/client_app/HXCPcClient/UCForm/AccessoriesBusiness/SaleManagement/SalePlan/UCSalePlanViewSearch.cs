using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Utility.Common;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan
{
    public partial class UCSalePlanViewSearch : UCBase
    {
        #region 变量
        string planId = string.Empty;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        DataTable dt_parts_brand = null;
        UCSalePlanManagerSearch uc;
        private tb_parts_sale_plan tb_purchase_Model = new tb_parts_sale_plan();
        #endregion

        public UCSalePlanViewSearch(string plan_id, UCSalePlanManagerSearch uc)
        {
            InitializeComponent();
            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();
            base.btnSave.Visible = true;
            this.planId = plan_id;
            this.uc = uc;
            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "is_suspend" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanList, NotReadOnlyColumnsName);
            //CommonFuncCall.BindUnit(unit_id);
            dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");
            LoadInfo(plan_id);
            GetAccessories(plan_id);
            //txtsuspend_reason.Enabled = chkis_suspend.Checked;
            gvPurchasePlanList.CellClick += new DataGridViewCellEventHandler(gvPurchasePlanList_CellClick);
        }

        void gvPurchasePlanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string viewfile = gvPurchasePlanList.Columns[e.ColumnIndex].DataPropertyName;
                if (viewfile == "relation_order")
                {
                    string parts_name = this.gvPurchasePlanList.CurrentRow.Cells["parts_name"].Value.ToString();
                    string plan_count = this.gvPurchasePlanList.CurrentRow.Cells["business_count"].Value.ToString();
                    string finish_count = this.gvPurchasePlanList.CurrentRow.Cells["finish_count"].Value.ToString();
                    string relation_order = lblorder_num.Text;
                    string parts_code = this.gvPurchasePlanList.CurrentRow.Cells["parts_code"].Value.ToString();
                    frmRelationOrder frm = new frmRelationOrder(parts_name, plan_count, finish_count, relation_order, parts_code);
                    frm.ShowDialog();
                }
            }
        }
        /// <summary>
        /// 加载销售计划信息和配件信息
        /// </summary>
        /// <param name="planId"></param>
        private void LoadInfo(string planId)
        {
            if (!string.IsNullOrEmpty(planId))
            {
                //1.查看一条销售计划单信息
                DataTable dt = DBHelper.GetTable("查看一条销售计划单信息", "tb_parts_sale_plan", "*", " sale_plan_id='" + planId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_purchase_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_purchase_Model, "");
                    chkis_suspend.Checked = tb_purchase_Model.is_suspend == "0";//选中(中止)：0,未选中(不中止)：1

                    if (!string.IsNullOrEmpty(lblplan_from.Text))
                    {
                        long ticks = Convert.ToInt64(lblplan_from.Text);
                        lblplan_from.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblplan_to.Text))
                    {
                        long ticks = Convert.ToInt64(lblplan_to.Text);
                        lblplan_to.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.create_time.ToString())).ToString();
                    if (tb_purchase_Model.update_time > 0)
                    { lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary>
        /// 根据销售计划单号获取销售配件信息
        /// </summary>
        /// <param name="sale_plan_id"></param>
        private void GetAccessories(string sale_plan_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询销售计划单配件表信息", "tb_parts_sale_plan_p", "*", " sale_plan_id='" + sale_plan_id + "'", "", "");
            if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_parts_purchase.Rows)
                {
                    DataGridViewRow dgvr = gvPurchasePlanList.Rows[gvPurchasePlanList.Rows.Add()];
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];
                    dgvr.Cells["parts_brand_name"].Value = dr["parts_brand_name"];
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];
                    dgvr.Cells["unit_name"].Value = dr["unit_name"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["business_count"].Value = dr["business_count"];
                    dgvr.Cells["money"].Value = dr["money"];
                    dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];
                    dgvr.Cells["finish_count"].Value = dr["finish_count"];
                    //dgvr.Cells["relation_order"].Value = dr["relation_order"];
                    dgvr.Cells["is_suspend"].Value = dr["is_suspend"];
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
                SelectCheckBox();
            }
        }
        /// <summary> 循环配件列表中的中止项，来判断中止控件是否可选
        /// </summary>
        void SelectCheckBox()
        {
            bool isChkis_Suspend = true;
            bool istxtsuspend_reasonEnable = false;
            //获取控件的值
            for (int i = 0; i < gvPurchasePlanList.Rows.Count; i++)
            {
                object ischeck = gvPurchasePlanList.Rows[i].Cells["is_suspend"].EditedFormattedValue;
                if (ischeck != null && !(bool)ischeck)
                {
                    isChkis_Suspend = false;
                    break;
                }
            }
            for (int i = 0; i < gvPurchasePlanList.Rows.Count; i++)
            {
                object ischeck = gvPurchasePlanList.Rows[i].Cells["is_suspend"].EditedFormattedValue;
                if (ischeck != null && (bool)ischeck)
                {
                    istxtsuspend_reasonEnable = true;
                    break;
                }
            }
            chkis_suspend.Checked = isChkis_Suspend;
            txtsuspend_reason.Enabled = istxtsuspend_reasonEnable;
        }
    }
}
