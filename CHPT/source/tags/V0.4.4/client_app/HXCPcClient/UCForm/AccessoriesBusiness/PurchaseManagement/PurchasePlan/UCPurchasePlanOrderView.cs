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
        string oldorderstatus = string.Empty;
        UCPurchasePlanOrderManager uc;
        private tb_parts_purchase_plan tb_purchase_Model = new tb_parts_purchase_plan();
        #endregion

        public UCPurchasePlanOrderView(string plan_id,UCPurchasePlanOrderManager uc)
        {
            InitializeComponent();
            this.uc = uc;
            this.planId = plan_id;
            base.SetBaseButtonStatus();
            if (uc != null)
            {
                base.SetButtonVisiableView();
                base.btnSave.Visible = true;
            }

            //CommonFuncCall.BindUnit(unit_id);
            LoadInfo(plan_id);
            GetAccessories(plan_id);
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            string[] NotReadOnlyColumnsName = new string[] { "colCheck", "is_suspend" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanList, NotReadOnlyColumnsName);
            base.SaveEvent += new ClickHandler(UCPurchasePlanOrderView_SaveEvent);
            base.InvalidOrActivationEvent += new ClickHandler(UCPurchasePlanOrderView_InvalidOrActivationEvent);
            gvPurchasePlanList.CellClick += new DataGridViewCellEventHandler(gvPurchasePlanList_CellClick);
        }
        /// <summary> 加载主单据信息
        /// </summary>
        /// <param name="planId"></param>
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
                    oldorderstatus = tb_purchase_Model.order_status;
                    if (oldorderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString())
                    {
                        //已提交状态屏蔽提交、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (oldorderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString())
                    {
                        //已审核时屏蔽提交、审核、编辑、删除按钮
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                        base.btnDelete.Enabled = false;
                        base.btnActivation.Enabled = false;
                    }
                    else if (oldorderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString() || oldorderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                    {
                        //审核没通过时屏蔽审核按钮
                        base.btnVerify.Enabled = false;
                    }
                    else if (oldorderstatus == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        base.btnActivation.Caption = "激活";
                        base.btnSubmit.Enabled = false;
                        base.btnVerify.Enabled = false;
                        base.btnEdit.Enabled = false;
                    }
                }
            }
        }
        /// <summary> 加载列表配件信息
        /// </summary>
        /// <param name="plan_id"></param>
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
                    //dgvr.Cells["relation_order"].Value = dr["relation_order"];
                    dgvr.Cells["is_suspend"].Value = dr["is_suspend"];
                    dgvr.Cells["remark"].Value = dr["remark"];
                    dgvr.Cells["create_by"].Value = dr["create_by"];
                    dgvr.Cells["create_name"].Value = dr["create_name"];
                    dgvr.Cells["create_time"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dr["create_time"].ToString()));
                }
            }
        }
        /// <summary> 自动计算计划金额的总和
        /// </summary>
        void GetBuessinessMoney(ref decimal business_counts, ref decimal plan_money)
        {
            foreach (DataGridViewRow dr in gvPurchasePlanList.Rows)
            {
                if (dr.Cells["is_suspend"].Value == null)
                { continue; }
                if (!(bool)dr.Cells["is_suspend"].EditedFormattedValue)
                {
                    business_counts = business_counts + Convert.ToDecimal(dr.Cells["business_counts"].Value == null ? "0" : dr.Cells["business_counts"].Value.ToString() == "" ? "0" : dr.Cells["business_counts"].Value.ToString());
                    plan_money += Convert.ToDecimal(dr.Cells["total_money"].Value == null ? "0" : dr.Cells["total_money"].Value.ToString() == "" ? "0" : dr.Cells["total_money"].Value.ToString());
                }
            }
        }
        /// <summary> 是否选择DataGridView的CheckBox
        /// </summary>
        /// <param name="ischeck"></param>
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
        /// <summary> 是否选择中止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkis_suspend_Click(object sender, EventArgs e)
        {
            txtsuspend_reason.Enabled = chkis_suspend.Checked;
            IsDataGridViewCheckBox(chkis_suspend.Checked);
        }
        /// <summary> 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderView_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (chkis_suspend.Checked)
                {
                    if (string.IsNullOrEmpty(txtsuspend_reason.Caption.Trim()))
                    {
                        MessageBoxEx.Show("请填写中止原因!");
                        return;
                    }
                }
                gvPurchasePlanList.EndEdit();
                decimal plan_money=0;
                decimal plan_counts = 0;
                GetBuessinessMoney(ref plan_counts, ref plan_money);

                List<SysSQLString> listSql = new List<SysSQLString>();
                SysSQLString sysStringSql = new SysSQLString();
                sysStringSql.cmdType = CommandType.Text;
                Dictionary<string, string> dic = new Dictionary<string, string>();//参数
                string sql1 = string.Format(@" Update tb_parts_purchase_plan Set is_suspend=@is_suspend,suspend_reason=@suspend_reason,update_by=@update_by,
                update_name=@update_name,update_time=@update_time,operators=@operators,operator_name=@operator_name,plan_counts=@plan_counts,
                plan_money=@plan_money where plan_id=@plan_id;");
                dic.Add("is_suspend", chkis_suspend.Checked ? "0" : "1");//选中(中止)：0,未选中(不中止)：1
                dic.Add("suspend_reason", txtsuspend_reason.Caption.Trim());
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_name", GlobalStaticObj.UserName);
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                dic.Add("operators", GlobalStaticObj.UserID);
                dic.Add("operator_name", GlobalStaticObj.UserName);
                dic.Add("plan_counts", plan_counts.ToString());
                dic.Add("plan_money", plan_money.ToString());
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
            dic.Add("plan_id", planId);//单据ID
            dic.Add("update_by",GlobalStaticObj.UserID);//修改人Id
            dic.Add("update_name",GlobalStaticObj.UserName);//修改人姓名
            dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间               
            if (oldorderstatus != Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
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
                DataTable dvt = DBHelper.GetTable("获得采购计划单的前一个状态", "tb_parts_purchase_plan_BackUp", "order_status,order_status_name", "plan_id='" + planId + "'", "", "order by update_time desc");
                if (dvt != null && dvt.Rows.Count > 0)
                {
                    DataRow dr = dvt.Rows[0];
                    order_status = CommonCtrl.IsNullToString(dr["order_status"]);
                    if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.Invalid).ToString())
                    {
                        DataRow dr1 = dvt.Rows[1];
                        order_status = CommonCtrl.IsNullToString(dr1["order_status"]);
                    }
                }
                order_status = !string.IsNullOrEmpty(order_status) ? order_status : Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString();
                if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())
                { order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true); }
                else if (order_status == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())
                { order_status_name = DataSources.GetDescription(DataSources.EnumAuditStatus.DRAFT, true); }

                dic.Add("order_status", order_status);//单据状态
                dic.Add("order_status_name", order_status_name);//单据状态名称
            }
            sysStringSql.sqlString = "update tb_parts_purchase_plan set order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where plan_id=@plan_id";
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);
            if (MessageBoxEx.Show("确认要" + strmsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("更新单据状态为" + strmsg + "", listSql))
            {
                MessageBoxEx.Show("" + strmsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindgvPurchasePlanOrderList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.Show("" + strmsg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary> 点击单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvPurchasePlanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string viewfile = gvPurchasePlanList.Columns[e.ColumnIndex].DataPropertyName;
                if (viewfile == "relation_order")
                {
                    string parts_name = this.gvPurchasePlanList.CurrentRow.Cells["parts_name"].Value.ToString();
                    string plan_count = this.gvPurchasePlanList.CurrentRow.Cells["business_counts"].Value.ToString();
                    string finish_count = this.gvPurchasePlanList.CurrentRow.Cells["finish_counts"].Value.ToString();
                    string relation_order = lblorder_num.Text;
                    string parts_code = this.gvPurchasePlanList.CurrentRow.Cells["parts_code"].Value.ToString();
                    frmRelationOrder frm = new frmRelationOrder(parts_name, plan_count, finish_count, relation_order, parts_code);
                    frm.ShowDialog();
                }
            }
        }
        /// <summary> 点击单元格内容事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
