using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Model;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    public partial class UCPurchasePlanOrderView : UCBase
    {
        #region 变量
        string planId = string.Empty;
        UCPurchasePlanOrderManager uc;
        private tb_parts_purchase_plan tb_purchase_Model = new tb_parts_purchase_plan();
        #endregion

        public UCPurchasePlanOrderView(string plan_id,UCPurchasePlanOrderManager uc)
        {
            InitializeComponent();
            this.uc = uc;
            this.planId = plan_id;

            base.SetBaseButtonStatus();
            base.SetButtonVisiableView();
            base.btnSave.Visible = true;
           
            
            //CommonFuncCall.BindUnit(unit_id);
            LoadInfo(plan_id);
            GetAccessories(plan_id);
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "is_suspend" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanList, NotReadOnlyColumnsName);
            base.SaveEvent += new ClickHandler(UCPurchasePlanOrderView_SaveEvent);
        }

        void UCPurchasePlanOrderView_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                gvPurchasePlanList.EndEdit();
                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();//参数

                string sql1 = string.Format(@" Update tb_parts_purchase_plan Set is_suspend=@is_suspend,suspend_reason=@suspend_reason,update_by=@update_by,
                update_name=@update_name,update_time=@update_time,operators=@operators,operator_name=@operator_name where plan_id=@plan_id;");
                dic.Add("is_suspend", chkis_suspend.Checked ? "0" : "1");//选中(中止)：0,未选中(不中止)：1
                dic.Add("suspend_reason", txtsuspend_reason.Caption.Trim());
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_name", GlobalStaticObj.UserName);
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                dic.Add("operators", GlobalStaticObj.UserID);
                dic.Add("operator_name", GlobalStaticObj.UserName);
                dic.Add("plan_id", planId);
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
                    dic.Add("plan_id", planId);
                    dic.Add("parts_code", dr.Cells["parts_code"].Value.ToString());
                    string sql2 = "Update tb_parts_purchase_plan_p set is_suspend=@is_suspend where plan_id=@plan_id and parts_code=@parts_code;";
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("修改采购计划单", listSql))
                {
                    MessageBoxEx.Show("保存成功！");
                    uc.BindgvPurchasePlanOrderList();
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

        private void LoadInfo(string planId)
        {
            if (!string.IsNullOrEmpty(planId))
            {
                //1.加载供应商档案主信息
                DataTable dt = DBHelper.GetTable("查看一条采购计划单信息", "tb_parts_purchase_plan", "*", " plan_id='" + planId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.SetModlByDataTable(tb_purchase_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_purchase_Model, "");
                    chkis_suspend.Checked = tb_purchase_Model.is_suspend == "0";//选中(中止)：0,未选中(不中止)：1
                    if (!string.IsNullOrEmpty(lblorder_date.Text))
                    {
                        long ticks = Convert.ToInt64(lblorder_date.Text);
                        lblorder_date.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblplan_start_time.Text))
                    {
                        long ticks = Convert.ToInt64(lblplan_start_time.Text);
                        lblplan_start_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    if (!string.IsNullOrEmpty(lblplan_end_time.Text))
                    {
                        long ticks = Convert.ToInt64(lblplan_end_time.Text);
                        lblplan_end_time.Text = Common.UtcLongToLocalDateTime(ticks).ToString();
                    }
                    lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.create_time.ToString())).ToString();
                    if (tb_purchase_Model.update_time > 0)
                    { lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(tb_purchase_Model.update_time.ToString())).ToString(); }
                }
            }
        }

        private void GetAccessories(string plan_id)
        {
            string conId = string.Empty;
            DataTable dt_parts_purchase = DBHelper.GetTable("查询采购计划单配件表信息", "tb_parts_purchase_plan_p", "*", " plan_id='" + plan_id + "'", "", "");
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
                    //dgvr.Cells["unit_name"].Value = dr["unit_name"];
                    dgvr.Cells["original_price"].Value = dr["original_price"];
                    dgvr.Cells["discount"].Value = dr["discount"];
                    dgvr.Cells["business_price"].Value = dr["business_price"];
                    dgvr.Cells["business_counts"].Value = dr["business_counts"];
                    dgvr.Cells["total_money"].Value = dr["total_money"];
                    dgvr.Cells["parts_barcode"].Value = dr["parts_barcode"];
                    dgvr.Cells["car_factory_code"].Value = dr["car_factory_code"];
                    dgvr.Cells["recent_supplier_code"].Value = dr["recent_supplier_code"];
                    dgvr.Cells["recent_purchase_price"].Value = dr["recent_purchase_price"];
                    dgvr.Cells["recent_purchase_name"].Value = dr["recent_purchase_name"];
                    dgvr.Cells["finish_counts"].Value = dr["finish_counts"];
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

        private void chkis_suspend_Click(object sender, EventArgs e)
        {
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            IsDataGridViewCheckBox(chkis_suspend.Checked);
        }
    }
}
