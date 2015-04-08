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
using ServiceStationClient.ComponentUI;
using SYSModel;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan
{
    public partial class UCSalePlanView : UCBase
    {
        #region 变量
        string planId = string.Empty;
        UCSalePlanManager uc;
        private tb_parts_sale_plan tb_purchase_Model = new tb_parts_sale_plan();
        #endregion

        public UCSalePlanView(string sale_plan_id, UCSalePlanManager uc)
        {
            InitializeComponent();
            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();
            base.btnSave.Visible = true;
            this.planId = sale_plan_id;
            this.uc = uc;

            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "is_suspend" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanList, NotReadOnlyColumnsName);

            //CommonFuncCall.BindUnit(unit_id);
            LoadInfo(sale_plan_id);
            GetAccessories(sale_plan_id);
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            base.SaveEvent += new ClickHandler(UCSalePlanView_SaveEvent);
        }

        void UCSalePlanView_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                gvPurchasePlanList.EndEdit();
                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();//参数

                string sql1 = string.Format(@" Update tb_parts_sale_plan Set is_suspend=@is_suspend,suspend_reason=@suspend_reason,update_by=@update_by,
                update_name=@update_name,update_time=@update_time,operators=@operators,operator_name=@operator_name where sale_plan_id=@sale_plan_id;");
                dic.Add("is_suspend", chkis_suspend.Checked ? "0" : "1");//选中(中止)：0,未选中(不中止)：1
                dic.Add("suspend_reason", txtsuspend_reason.Caption.Trim());
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_name", GlobalStaticObj.UserName);
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                dic.Add("operators", GlobalStaticObj.UserID);
                dic.Add("operator_name", GlobalStaticObj.UserName);
                dic.Add("sale_plan_id", planId);
                sysStringSql.sqlString = sql1;
                sysStringSql.Param = dic;
                listSql.Add(sysStringSql);
                foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
                {
                    string is_suspend = "1";
                    if (dr.Cells["is_suspend"].Value == null)
                    { is_suspend = "1"; }
                    if ((bool)dr.Cells["is_suspend"].EditedFormattedValue)
                    { is_suspend = "0"; }
                    else
                    { is_suspend = "1"; }

                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    dic.Add("is_suspend", is_suspend);
                    dic.Add("sale_plan_id", planId);
                    dic.Add("parts_code", dr.Cells["parts_code"].Value.ToString());
                    string sql2 = "Update tb_parts_sale_plan_p set is_suspend=@is_suspend where sale_plan_id=@sale_plan_id and parts_code=@parts_code;";
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("修改采购计划单", listSql))
                {
                    MessageBoxEx.Show("保存成功！");
                    uc.BindgvSalePlanList();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("操作失败！");
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

        void IsDataGridViewCheckBox(bool ischeck)
        {
            gvPurchasePlanList.EndEdit();
            if (gvPurchasePlanList.Rows.Count > 0)
            {
                for (int i = 0; i < gvPurchasePlanList.Rows.Count; i++)
                {
                    ((DataGridViewCheckBoxCell)gvPurchasePlanList.Rows[i].Cells["is_suspend"]).Value = ischeck;
                }
            }
        }

        private void gvPurchasePlanList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
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

        private void gvPurchasePlanList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void chkis_suspend_Click(object sender, EventArgs e)
        {
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            IsDataGridViewCheckBox(chkis_suspend.Checked);
        }
    }
}
