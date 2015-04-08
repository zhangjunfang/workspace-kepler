using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.Chooser;
using Utility.Common;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseBilling
{
    public partial class UCPurchaseBillManangView : UCBase
    {
        #region 窗体初始化
        /// <summary> 窗体初始化
        /// </summary>
        public UCPurchaseBillManangView()
        {
            InitializeComponent();

            #region 窗体容器控件自适应大小
            //tabControlEx1自适应大小
            this.tabControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));


            #region 按采购订单查询界面控件的自适应
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchaseOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                       | System.Windows.Forms.AnchorStyles.Bottom));
            #endregion

            #region 按配件或客户查询界面控件的自适应
            this.panelEx3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPurchaseList2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)
                       | System.Windows.Forms.AnchorStyles.Bottom));
            #endregion
            #endregion

            gvPurchaseOrderList.AutoGenerateColumns = false;
            gvPurchaseList2.AutoGenerateColumns = false;
        }
        /// <summary> 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchaseBillManangView_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            base.SetButtonVisiableManagerSearch();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseOrderList, NotReadOnlyColumnsName);
            string[] NotReadOnlyColumnsName2 = new string[] { "p_colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseList2, NotReadOnlyColumnsName2);

            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch2, btnClear2);  //设置查询按钮和清除按钮样式

            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;
            dateTimeStart2.Value = DateTime.Now;
            dateTimeEnd2.Value = DateTime.Now;

            //单据类型
            CommonFuncCall.BindPurchaseOrderType(ddlorder_type, true, "全部");
            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_way, "sys_trans_mode", "全部");
            //发票类型
            CommonFuncCall.BindComBoxDataSource(ddlreceipt_type, "sys_receipt_type", "全部");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlbalance_way, "全部");
            //结算账户
            CommonFuncCall.BindAccount(ddlbalance_account, "", "全部");
            //是否赠品
            CommonFuncCall.BindIs_Gift(ddlis_gift2, true);
            //结算情况
            CommonFuncCall.BindBalanceStatus(ddlbalance, true, "全部");
            //入库状态
            CommonFuncCall.BindIntoStockStatus(ddlStockInStatus2, true, "全部");

            //公司ID
            CommonFuncCall.BindCompany(ddlCompany, "全部");
            CommonFuncCall.BindCompany(ddlCompany2, "全部");
            CommonFuncCall.BindDepartment(ddlDepartment, "", "全部");
            CommonFuncCall.BindHandle(ddlhandle, "", "全部");
        } 
        #endregion

        #region 按采购开单查询
        #region 控件事件
        /// <summary> 选择供应商编码事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtsup_code_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier chooseSupplier = new frmSupplier();
            chooseSupplier.ShowDialog();
            string supperID = chooseSupplier.supperID;
            if (!string.IsNullOrEmpty(supperID))
            {
                txtsup_code.Text = chooseSupplier.supperCode;
                txtsup_name.Caption = chooseSupplier.supperName;
            }
        }
        /// <summary> 选择公司事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCompany.SelectedValue.ToString()))
            {
                CommonFuncCall.BindDepartment(ddlDepartment, ddlCompany.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindDepartment(ddlDepartment, "", "全部");
                CommonFuncCall.BindHandle(ddlhandle, "", "全部");
            }
        }
        /// <summary> 选择部门事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.ToString()))
            {
                CommonFuncCall.BindHandle(ddlhandle, ddlDepartment.SelectedValue.ToString(), "全部");
            }
            else
            {
                CommonFuncCall.BindHandle(ddlhandle, "", "全部");
            }
        }
        /// <summary> 选择结算方式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlbalance_way_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            {
                string sql_where = " enable_flag='1' and balance_way_id='" + ddlbalance_way.SelectedValue.ToString() + "'";
                DataTable dt = DBHelper.GetTable("查询结算方式", "tb_balance_way", "default_account", sql_where, "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonFuncCall.BindAccount(ddlbalance_account, dt.Rows[0]["default_account"].ToString(), "全部");
                }
                else
                { CommonFuncCall.BindAccount(ddlbalance_account, "", "全部"); }
            }
            else
            { CommonFuncCall.BindAccount(ddlbalance_account, "", "全部"); }
        }
        /// <summary> 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtorder_num.Caption = string.Empty;
                txtsup_code.Text = string.Empty;
                txtsup_name.Caption = string.Empty;
                txtremark.Caption = string.Empty;

                ddlorder_type.SelectedIndex = 0;
                ddlbalance.SelectedIndex = 0;
                ddlreceipt_type.SelectedIndex = 0;
                ddltrans_way.SelectedIndex = 0;
                ddlbalance_way.SelectedIndex = 0;
                ddlbalance_account.SelectedIndex = 0;
                ddlCompany.SelectedIndex = 0;
                ddlDepartment.SelectedIndex = 0;
                ddlhandle.SelectedIndex = 0;
                dateTimeStart.Value = DateTime.Now;
                dateTimeEnd.Value = DateTime.Now;
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimeStart.Value.ToShortDateString() + " 00:00:00") > Convert.ToDateTime(dateTimeEnd.Value.ToShortDateString() + " 00:00:00"))
            {
                MessageBoxEx.Show("单据日期的开始时间不可以大于结束时间");
            }
            else
                BindgvPurchaseOrderList();
        }
        /// <summary> 按采购开单查询的分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchaseOrderList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string fieldNmae = gvPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }
        #endregion

        #region 方法、函数
        /// <summary> 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " enable_flag='1' and order_status='2' ";
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num='" + txtorder_num.Caption.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtsup_code.Text.Trim()))
            {
                Str_Where += " and sup_code='" + txtsup_code.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtsup_name.Caption.Trim()))
            {
                Str_Where += " and sup_name like '%" + txtsup_name.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtremark.Caption.Trim()))
            {
                Str_Where += " and remark like '%" + txtremark.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                Str_Where += " and order_type='" + ddlorder_type.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlbalance.SelectedValue.ToString()))
            {
                Str_Where += " and balance_status='" + ddlbalance.SelectedItem.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlreceipt_type.SelectedValue.ToString()))
            {
                Str_Where += " and receipt_type='" + ddlreceipt_type.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddltrans_way.SelectedValue.ToString()))
            {
                Str_Where += " and trans_way='" + ddltrans_way.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlbalance_way.SelectedValue.ToString()))
            {
                Str_Where += " and balance_way='" + ddlbalance_way.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlbalance_account.SelectedValue.ToString()))
            {
                Str_Where += " and balance_account='" + ddlbalance_account.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlCompany.SelectedValue.ToString()))
            {
                Str_Where += " and com_id='" + ddlCompany.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.ToString()))
            {
                Str_Where += " and org_id='" + ddlDepartment.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlhandle.SelectedValue.ToString()))
            {
                Str_Where += " and handle='" + ddlhandle.SelectedValue.ToString() + "'";
            }
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
        /// <summary> 加载采购开单列表信息
        /// </summary>
        public void BindgvPurchaseOrderList()
        {
            try
            {
                int RecordCount = 0;
                string TableName = string.Format(@"
                (
                  select case when isnull(balance_money,0)=0 then '未结算' 
                   else 
                      case when isnull(balance_money,0)>=allmoney then '已结算' 
                      else '结算中' end
                  end balance_status,* 
                  from tb_parts_purchase_billing
                ) tb_parts_purchase_billing_search");
                DataTable gvPurchaseBill_dt = DBHelper.GetTableByPage("查询采购开单查询信息", TableName, "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvPurchaseOrderList.DataSource = gvPurchaseBill_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        #endregion
        #endregion

        #region 按配件或客户查询
        #region 控件事件
        /// <summary> 选择配件编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code2_ChooserClick(object sender, EventArgs e)
        {
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_code2.Text = chooseParts.PartsCode;
                txtparts_name2.Caption = chooseParts.PartsName;
            }
        }
        /// <summary> 选择供应商编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtsup_code2_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier chooseSupplier = new frmSupplier();
            chooseSupplier.ShowDialog();
            if (!string.IsNullOrEmpty(chooseSupplier.supperID))
            {
                txtsup_code2.Text = chooseSupplier.supperCode;
                txtsup_name2.Caption = chooseSupplier.supperName;
            }
        }
        /// <summary> 选择配件类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_type2_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType choosePartsType = new frmPartsType();
            choosePartsType.ShowDialog();
            if (!string.IsNullOrEmpty(choosePartsType.TypeID))
            {
                txtparts_type2.Text = choosePartsType.TypeName;
            }
        }
        /// <summary> 选择配件车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_cartype2_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels chooseCarModel = new frmVehicleModels();
            chooseCarModel.ShowDialog();
            if (!string.IsNullOrEmpty(chooseCarModel.VMID))
            {
                txtparts_cartype2.Text = chooseCarModel.VMName;
            }
        }
        /// <summary> 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtparts_code2.Text = string.Empty;
            txtparts_name2.Caption = string.Empty;
            txtsup_code2.Text = string.Empty;
            txtsup_name2.Caption = string.Empty;
            txtparts_type2.Text = string.Empty;
            txtparts_cartype2.Text = string.Empty;
            txtcontacts2.Caption = string.Empty;
            txtcontacts_tel2.Caption = string.Empty;
            txtdrawing_num2.Caption = string.Empty;
            txtparts_brand2.Caption = string.Empty;
            ddlis_gift2.SelectedIndex = 0;
            txtremark2.Caption = string.Empty;
            dateTimeStart2.Value = DateTime.Now;
            dateTimeEnd2.Value = DateTime.Now;
            ddlCompany2.SelectedIndex = 0;
            if (ddlStockInStatus2.Items.Count > 0)
                ddlStockInStatus2.SelectedIndex = 0;
        }
        /// <summary> 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch2_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimeStart2.Value.ToShortDateString() + " 00:00:00") > Convert.ToDateTime(dateTimeEnd2.Value.ToShortDateString() + " 00:00:00"))
            {
                MessageBoxEx.Show("单据日期的开始时间不可以大于结束时间");
            }
            else
                BindgvPurchaseList2();
        }
        /// <summary> 按配件或客户查询 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager2_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchaseList2();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseList2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string fieldNmae = gvPurchaseList2.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("is_gift"))
            {
                e.Value = e.Value.ToString() == "1" ? "否" : "是";
            }
        }
        #endregion

        #region 方法函数
        /// <summary> 按配件或客户信息查询的查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString2()
        {
            string Str_Where = " enable_flag='1' and order_status='2' ";
            if (!string.IsNullOrEmpty(txtparts_code2.Text.Trim()))
            {
                Str_Where += " and parts_code='" + txtparts_code2.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtparts_name2.Caption.Trim()))
            {
                Str_Where += " and parts_name like '%" + txtparts_name2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtsup_code2.Text.Trim()))
            {
                Str_Where += " and sup_code='" + txtsup_code2.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtsup_name2.Caption.Trim()))
            {
                Str_Where += " and sup_name like '%" + txtsup_name2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtparts_type2.Text.Trim()))
            {
                Str_Where += " and parts_type_name like '%" + txtparts_type2.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtparts_cartype2.Text.Trim()))
            {
                Str_Where += " and vm_name like '%" + txtparts_cartype2.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtcontacts2.Caption.Trim()))
            {
                Str_Where += " and contacts like '%" + txtcontacts2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtcontacts_tel2.Caption.Trim()))
            {
                Str_Where += " and contacts_tel like '%" + txtcontacts_tel2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtdrawing_num2.Caption.Trim()))
            {
                Str_Where += " and drawing_num like '%" + txtdrawing_num2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtparts_brand2.Caption.Trim()))
            {
                Str_Where += " and parts_brand_name like '%" + txtparts_brand2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtremark2.Caption.Trim()))
            {
                Str_Where += " and remark like '%" + txtremark2.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlis_gift2.SelectedValue.ToString()))
            {
                Str_Where += " and is_gift='" + ddlis_gift2.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlCompany2.SelectedValue.ToString()))
            {
                Str_Where += " and com_id='" + ddlCompany2.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlStockInStatus2.SelectedValue.ToString()))
            {
                Str_Where += " and FinishStatus='" + ddlStockInStatus2.SelectedItem.ToString() + "'";
            }
            if (dateTimeStart2.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeStart2.Value.ToShortDateString() + " 00:00:00");
                Str_Where += " and order_date>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeEnd2.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeEnd2.Value.ToShortDateString() + " 23:59:59");
                Str_Where += " and order_date<=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            return Str_Where;
        }
        /// <summary> 绑定按配件或客户信息查询的列表
        /// </summary>
        void BindgvPurchaseList2()
        {
            try
            {
                int RecordCount = 0;
                string StrTableName = string.Format(@" 
                (
                    select 
                    tb_pur_bill.enable_flag,tb_pur_bill.order_type_name, tb_pur_bill.order_num,tb_pur_bill.sup_id,tb_pur_bill.sup_code,tb_pur_bill.sup_name,
                    tb_pur_bill.order_status,tb_pur_bill.order_status_name,
                    tb_pur_bill.contacts,tb_pur_bill.contacts_tel,tb_pur_bill.order_date,
                    tb_pur_bill.com_id,tb_pur_bill.com_code,tb_pur_bill.com_name,tb_pur_bill.FinishStatus,tb_pur_bill_p.*
                    from tb_parts_purchase_billing_p as tb_pur_bill_p
                    left join 
                    (select case is_occupy 
		                    when '3' then '已入库' else '未入库'
	                        end FinishStatus,* 
                     from tb_parts_purchase_billing
                    ) tb_pur_bill on tb_pur_bill_p.purchase_billing_id=tb_pur_bill.purchase_billing_id
                ) tb_bill_search  ");
                DataTable gvPurchaseList2_dt = DBHelper.GetTableByPage("查询采购开单列表信息", StrTableName, "*", BuildString2(), "", " order by create_time desc ", winFormPager2.PageIndex, winFormPager2.PageSize, out RecordCount);
                gvPurchaseList2.DataSource = gvPurchaseList2_dt;
                winFormPager2.RecordCount = RecordCount;
            }
            catch (Exception ex)
            { }
        }
        #endregion
        #endregion  
    }
}
