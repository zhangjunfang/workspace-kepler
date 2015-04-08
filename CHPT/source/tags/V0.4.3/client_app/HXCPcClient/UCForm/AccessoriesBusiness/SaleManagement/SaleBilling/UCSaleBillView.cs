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
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleBilling
{
    public partial class UCSaleBillView : UCBase
    {
        #region 变量
        UCSaleBillManang uc;
        string sale_billing_id;
        string orderstatus = string.Empty;
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
            this.uc = uc;
            this.sale_billing_id = sale_billing_id;
            base.SetBaseButtonStatus();
            if (uc != null)
            {
                base.SetButtonVisiableView();
            }
            base.InvalidOrActivationEvent += new ClickHandler(UCPurchasePlanOrderView_InvalidOrActivationEvent);
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
                    if (!string.IsNullOrEmpty(lblorder_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblorder_date.Text);
                        lblorder_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblreceivables_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblreceivables_date.Text);
                        lblreceivables_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.create_time.ToString())).ToString();
                    if (tb_salebill_Model.update_time > 0)
                    { lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_salebill_Model.update_time.ToString())).ToString(); }
                    orderstatus = tb_salebill_Model.order_status;
                    if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        //已提交状态屏蔽提交、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽提交、审核、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString() || orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                    {
                        //审核没通过时屏蔽审核按钮
                        base.btnVerify.Enabled = false;
                    }
                    else if (orderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        base.btnActivation.Caption = "激活";
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                    }
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

                    if (Convert.ToInt64(dr["make_date"].ToString() == "" ? "0" : dr["make_date"].ToString()) > 0)
                    {
                        dgvr.Cells["make_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["make_date"])).ToShortDateString();//生产日期
                    }
                    if (Convert.ToInt64(dr["arrival_date"].ToString() == "" ? "0" : dr["arrival_date"].ToString()) > 0)
                    {
                        dgvr.Cells["arrival_date"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["arrival_date"])).ToShortDateString();//到期日期
                    }

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
                if (e.Value == null || e.Value.ToString().Length == 0)
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

        /// <summary> 激活/作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderView_InvalidOrActivationEvent(object sender, EventArgs e)
        {
            string strmsg = string.Empty;
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();//参数
            dic.Add("sale_billing_id", sale_billing_id);//单据ID
            dic.Add("update_by", GlobalStaticObj.UserID);//修改人Id
            dic.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
            dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间               
            if (orderstatus != Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
            {
                strmsg = "作废";
                dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString());//单据状态编号
                dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.Invalid, true));//单据状态名称
            }
            else
            {
                strmsg = "激活";
                string order_status = string.Empty;
                string order_status_name = string.Empty;
                DataTable dvt = DBHelper.GetTable("获得销售开单的前一个状态", "tb_parts_sale_billing_BackUp", "order_status,order_status_name", "sale_billing_id='" + sale_billing_id + "'", "", "order by update_time desc");
                if (dvt != null && dvt.Rows.Count > 0)
                {
                    DataRow dr = dvt.Rows[0];
                    order_status = CommonCtrl.IsNullToString(dr["order_status"]);
                    if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        DataRow dr1 = dvt.Rows[1];
                        order_status = CommonCtrl.IsNullToString(dr1["order_status"]);
                        order_status_name = CommonCtrl.IsNullToString(dr1["order_status_name"]);
                    }
                }
                order_status = !string.IsNullOrEmpty(order_status) ? order_status : Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                order_status_name = !string.IsNullOrEmpty(order_status_name) ? order_status_name : DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true);
                dic.Add("order_status", order_status);//单据状态
                dic.Add("order_status_name", order_status_name);//单据状态名称
            }
            sysStringSql.sqlString = "update tb_parts_sale_billing set order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where sale_billing_id=@sale_billing_id";
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);
            if (MessageBoxEx.Show("确认要" + strmsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("更新单据状态为" + strmsg + "", listSql))
            {
                MessageBoxEx.Show("" + strmsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindgvPurchaseOrderList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show("" + strmsg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
