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
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
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
                    dgvr.Cells["relation_order"].Value = dr["relation_order"];
                    dgvr.Cells["is_suspend"].Value = dr["is_suspend"];
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
            }
        }
    }
}
