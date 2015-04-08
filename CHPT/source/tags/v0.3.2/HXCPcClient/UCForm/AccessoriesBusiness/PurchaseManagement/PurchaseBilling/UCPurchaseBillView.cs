using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling
{
    public partial class UCPurchaseBillView : UCBase
    {
        #region 变量
        UCPurchaseBillManang uc;
        string purchase_billing_id;
        tb_parts_purchase_billing tb_partspurchasebill_Model = new tb_parts_purchase_billing();
        #endregion

        public UCPurchaseBillView(string purchase_billing_id, UCPurchaseBillManang uc)
        {
            InitializeComponent();
            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();
            //CommonFuncCall.BindWarehouse(wh_id);
            //CommonFuncCall.BindUnit(unit_id);

            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);

            LoadInfo(purchase_billing_id);
            GetAccessories(purchase_billing_id);
        }

        /// <summary>
        /// 加载采购开单信息和配件信息
        /// </summary>
        /// <param name="order_id"></param>
        private void LoadInfo(string purchase_billing_id)
        {
            if (!string.IsNullOrEmpty(purchase_billing_id))
            {
                //1.查看一条采购订单信息
                DataTable dt = DBHelper.GetTable("查看一条采购开单信息", "tb_parts_purchase_billing", "*", " purchase_billing_id='" + purchase_billing_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_partspurchasebill_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_partspurchasebill_Model, "View");


                    if (!string.IsNullOrEmpty(lblorder_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblorder_date.Text);
                        lblorder_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblpayment_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblpayment_date.Text);
                        lblpayment_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_partspurchasebill_Model.create_time.ToString())).ToString();
                    if (tb_partspurchasebill_Model.update_time > 0)
                    { lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_partspurchasebill_Model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary> 
        /// 根据采购开单号获取配件信息
        /// </summary>
        /// <param name="purchase_billing_id"></param>
        private void GetAccessories(string purchase_billing_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询采购开单配件表信息", "tb_parts_purchase_billing_p", "*", " purchase_billing_id='" + purchase_billing_id + "'", "", "");
            if (dt_parts_purchase != null && dt_parts_purchase.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_parts_purchase.Rows)
                {
                    DataGridViewRow dgvr = gvPurchaseList.Rows[gvPurchaseList.Rows.Add()];
                    dgvr.Cells["wh_name"].Value = dr["wh_name"];
                    dgvr.Cells["parts_code"].Value = dr["parts_code"];
                    dgvr.Cells["parts_name"].Value = dr["parts_name"];
                    dgvr.Cells["model"].Value = dr["model"];
                    dgvr.Cells["parts_brand_name"].Value = dr["parts_brand_name"];
                    dgvr.Cells["drawing_num"].Value = dr["drawing_num"];
                    dgvr.Cells["unit_name"].Value = dr["unit_name"];
                    dgvr.Cells["business_counts"].Value = dr["business_counts"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["tax_rate"].Value = dr["tax_rate"];
                    dgvr.Cells["tax"].Value = dr["tax"];
                    dgvr.Cells["payment"].Value = dr["payment"];
                    dgvr.Cells["valorem_together"].Value = dr["valorem_together"];//税价合计
                    dgvr.Cells["is_gift"].Value = dr["is_gift"];

                    if (Convert.ToInt64(dr["make_date"]) > 0)
                    {
                        dgvr.Cells["make_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["make_date"]));//生产日期
                    }
                    if (Convert.ToInt64(dr["arrival_date"]) > 0)
                    {
                        dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"]));//到期日期
                    }

                    dgvr.Cells["relation_order"].Value = dr["relation_order"];//引用单号
                    dgvr.Cells["return_bus_count"].Value = dr["return_bus_count"];//退货业务数量
                    dgvr.Cells["storage_count"].Value = dr["storage_count"];//入库数量
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//车厂配件编码
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["assisted_count"].Value = dr["assisted_count"];//辅助数量

                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
            }
        }
    }
}
