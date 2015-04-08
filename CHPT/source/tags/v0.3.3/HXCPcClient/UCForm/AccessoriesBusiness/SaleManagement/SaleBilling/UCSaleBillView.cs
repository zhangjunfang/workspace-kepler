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

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling
{
    public partial class UCSaleBillView : UCBase
    {
        #region 变量
        UCSaleBillManang uc;
        string sale_billing_id;
        /// <summary>
        /// 存储配件品牌的信息
        /// </summary>
        DataTable dt_parts_brand = null;
        tb_parts_sale_billing tb_salebill_Model = new tb_parts_sale_billing();
        #endregion

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="status"></param>
        /// <param name="sale_billing_id"></param>
        /// <param name="uc"></param>
        public UCSaleBillView(string sale_billing_id, UCSaleBillManang uc)
        {
            InitializeComponent();
            //CommonFuncCall.BindWarehouse(wh_id);
            //CommonFuncCall.BindUnit(unit_id);
            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();

            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList, NotReadOnlyColumnsName);

            dt_parts_brand = CommonFuncCall.BindDicDataSource("sys_parts_brand");
            LoadInfo(sale_billing_id);
            GetAccessories(sale_billing_id);
        }

        /// <summary>
        /// 加载销售计划信息和配件信息
        /// </summary>
        /// <param name="sale_billing_id"></param>
        private void LoadInfo(string sale_billing_id)
        {
            if (!string.IsNullOrEmpty(sale_billing_id))
            {
                //1.查看一条销售开单信息
                DataTable dt = DBHelper.GetTable("查看一条销售开单信息", "tb_parts_sale_billing", "*", " sale_billing_id='" + sale_billing_id + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_salebill_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_salebill_Model, "");

                    lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.create_time.ToString())).ToString();
                    if (tb_salebill_Model.update_time > 0)
                    { lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.update_time.ToString())).ToString(); }
                }
            }
        }
        /// <summary> 
        /// 根据销售开单号获取配件信息
        /// </summary>
        /// <param name="sale_billing_id"></param>
        private void GetAccessories(string sale_billing_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询销售开单配件表信息", "tb_parts_sale_billing_p", "*", " sale_billing_id='" + sale_billing_id + "'", "", "");
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
                    dgvr.Cells["business_count"].Value = dr["business_count"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["tax_rate"].Value = dr["tax_rate"];
                    dgvr.Cells["tax"].Value = dr["tax"];
                    dgvr.Cells["payment"].Value = dr["payment"];
                    dgvr.Cells["valorem_together"].Value = dr["valorem_together"];//税价合计
                    dgvr.Cells["is_gift"].Value = dr["is_gift"];

                    //if (Convert.ToInt64(dr["make_date"]) > 0)
                    //{
                    //    dgvr.Cells["make_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["make_date"]));//生产日期
                    //}
                    //if (Convert.ToInt64(dr["arrival_date"]) > 0)
                    //{
                    //    dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"]));//到期日期
                    //}

                    dgvr.Cells["relation_order"].Value = dr["relation_order"];//引用单号
                    dgvr.Cells["return_count"].Value = dr["return_count"];//退货数量
                    dgvr.Cells["library_count"].Value = dr["library_count"];//出库数量
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];//车厂配件编码
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["auxiliary_count"].Value = dr["auxiliary_count"];//辅助数量

                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
            }
        }

        private void gvPurchaseList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dt_parts_brand.Rows.Count > 0)
            {
                if (e.Value == null || e.Value == string.Empty)
                {
                    return;
                }
                string fieldNmae = gvPurchaseList.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals("parts_brand"))
                {
                    for (int i = 0; i < dt_parts_brand.Rows.Count; i++)
                    {
                        if (dt_parts_brand.Rows[i]["dic_id"].ToString() == e.Value.ToString())
                        {
                            gvPurchaseList.Rows[e.RowIndex].Cells["parts_brand_name"].Value = dt_parts_brand.Rows[i]["dic_name"].ToString();
                            break;
                        }
                    }
                }
            }
        }
    }
}
