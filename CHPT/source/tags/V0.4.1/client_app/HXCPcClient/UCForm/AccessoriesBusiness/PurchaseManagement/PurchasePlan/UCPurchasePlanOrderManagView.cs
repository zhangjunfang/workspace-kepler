using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchasePlan
{
    public partial class UCPurchasePlanOrderManagView : UCBase
    {
        #region 变量
        //private long CurrteTimeTicks = 0;
        #endregion

        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        public UCPurchasePlanOrderManagView()
        {
            InitializeComponent();

            //base.SetBaseButtonStatus();
            //base.SetButtonVisiableManagerSearch();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanOrderList, NotReadOnlyColumnsName);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            //禁止列表自增列
            gvPurchasePlanOrderList.AutoGenerateColumns = false;
            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;
            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlDepartment, com_id, "全部");
            CommonFuncCall.BindHandle(ddlResponsiblePerson, "", "全部");
            CommonFuncCall.BindPurchasePlanFinishStatus(ddlFinishStatus, true);
            BindgvPurchasePlanOrderList();

            gvPurchasePlanOrderList.CellMouseClick += new DataGridViewCellMouseEventHandler(gvPurchasePlanOrderList_CellMouseClick);
        }
        /// <summary> 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchasePlanOrderManager_Load(object sender, EventArgs e)
        {
           
        } 
        #endregion

        #region 按钮事件
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPlanNo.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            ddlFinishStatus.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlResponsiblePerson.SelectedIndex = 0;
            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 查询按钮事件
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
                BindgvPurchasePlanOrderList();
        }
        /// <summary>
        /// 部门选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.ToString()))
            {
                CommonFuncCall.BindHandle(ddlResponsiblePerson, ddlDepartment.SelectedValue.ToString(), "全部");
            }
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchasePlanOrderList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string plan_Id = Convert.ToString(this.gvPurchasePlanOrderList.CurrentRow.Cells["plan_id"].Value.ToString());
                UCPurchasePlanOrderView2 UCPurchasePlanOrderView = new UCPurchasePlanOrderView2(plan_Id, this);
                base.addUserControl(UCPurchasePlanOrderView, "采购计划单查询-查看", "UCPurchasePlanOrderView" + plan_Id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 列表单元格内容格式化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = gvPurchasePlanOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date") || fieldNmae.Equals("plan_start_time") || fieldNmae.Equals("plan_end_time"))
            {
                long ticks = (long)e.Value;
                if (fieldNmae.Equals("order_date"))
                {
                    e.Value = Common.UtcLongToLocalDateTime(ticks).ToShortDateString();
                }
                else
                {
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
        }
        /// <summary> 单击一行，选择或取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvPurchasePlanOrderList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in gvPurchasePlanOrderList.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            gvPurchasePlanOrderList.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            //CurrteTimeTicks = Common.LocalDateTimeToUtcLong(DateTime.Now);
            string Str_Where = " enable_flag='1' and order_status='2' ";
            if (!string.IsNullOrEmpty(txtPlanNo.Caption.Trim()))
            {
                Str_Where += " and order_num like '%" + txtPlanNo.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.ToString()))
            {
                Str_Where += " and org_id='" + ddlDepartment.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlResponsiblePerson.SelectedValue.ToString()))
            {
                Str_Where += " and handle='" + ddlResponsiblePerson.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlFinishStatus.SelectedValue.ToString()))
            {
                //已完成
                if (ddlFinishStatus.SelectedValue.ToString() == "1")
                { Str_Where += " and FinishStatus='已完成' "; }
                //未完成
                else if (ddlFinishStatus.SelectedValue.ToString() == "2")
                { Str_Where += " and FinishStatus='未完成' "; }
                //已超期
                else if (ddlFinishStatus.SelectedValue.ToString() == "3")
                { Str_Where += " and FinishStatus='已超期' "; }

                ////已完成
                //if (ddlFinishStatus.SelectedValue.ToString() == "1")
                //{ Str_Where += " and 完成数量>=计划数量 "; }
                ////未完成
                //else if (ddlFinishStatus.SelectedValue.ToString() == "2")
                //{ Str_Where += " and 完成数量<计划数量 and " + CurrteTimeTicks + "<=结束时间 "; }
                ////已超期
                //else if (ddlFinishStatus.SelectedValue.ToString() == "3")
                //{ Str_Where += " and 完成数量<计划数量 and " + CurrteTimeTicks + ">结束时间 "; }
            }
            if (!string.IsNullOrEmpty(txtRemark.Caption.Trim()))
            {
                Str_Where += " and remark like '%" + txtRemark.Caption.Trim() + "%'";
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
        /// <summary>
        /// 加载采购计划单列表信息
        /// </summary>
        public void BindgvPurchasePlanOrderList()
        {
            try
            {
                //绑定的单据信息必须是已审核通过的
                long CurrteTimeTicks = Common.LocalDateTimeToUtcLong(DateTime.Now);
                string tableName = string.Format(@"(select case import_status
                                                                when '3' then '已完成'
                                                                else 
                                                                    case when {0}<=plan_end_time 
                                                                    then '未完成' 
                                                                    else '已过期' 
                                                                end 
                                                            end FinishStatus,*
                                                    from tb_parts_purchase_plan 
                                                    ) tb_parts_purchase_plan ", CurrteTimeTicks);
                int RecordCount = 0;
                DataTable gvPurchasePlanOrder_dt = DBHelper.GetTableByPage("查询采购计划单查询列表信息", tableName, "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvPurchasePlanOrderList.DataSource = gvPurchasePlanOrder_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        #endregion
    }
}
