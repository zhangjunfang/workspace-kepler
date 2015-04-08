using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
using HXCPcClient.CommonClass;
using Utility.Common;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan
{
    public partial class UCSalePlanManagerSearch : UCBase
    {
        BusinessPrint businessPrint;//业务打印功能
        #region 初始化窗体
        /// <summary> 初始化窗体
        /// </summary>
        public UCSalePlanManagerSearch()
        {
            InitializeComponent();
            gvSalePlanList.CellMouseClick += new DataGridViewCellMouseEventHandler(gvSalePlanList_CellMouseClick);
            base.ExportEvent += new ClickHandler(UCSalePlanManagerSearch_ExportEvent);
            base.ViewEvent += new ClickHandler(UCSalePlanManagerSearch_ViewEvent);
            base.PrintEvent += new ClickHandler(UCSalePlanManagerSearch_PrintEvent);
            base.SetEvent += new ClickHandler(UCSalePlanManagerSearch_SetEvent);
            #region 预览、打印设置
            string printObject = "tb_parts_sale_plan_s";
            string printTitle = "销售计划单查询";
            List<string> listNotPrint = new List<string>();
            listNotPrint.Add(sale_plan_id.Name);
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 297;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(gvSalePlanList, printObject, printTitle, paperSize, listNotPrint);
            #endregion
        }
       
        /// <summary> 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSalePlanManager_Load(object sender, EventArgs e)
        {
            //base.SetBaseButtonStatus();
            //base.SetButtonVisiableManagerSearch();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvSalePlanList, NotReadOnlyColumnsName);

            base.SetContentMenuScrip(gvSalePlanList);
            base.ClearAllToolStripItem();
            base.AddToolStripItem(base.btnExport);
            base.AddToolStripItem(base.btnView);
            base.AddToolStripItem(base.btnSet);
            base.AddToolStripItem(base.btnPrint);

            //禁止列表自增列
            gvSalePlanList.AutoGenerateColumns = false;
            //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(this,btnSearch, btnClear);  

            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;

            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlDepartment, com_id, "全部");
            CommonFuncCall.BindHandle(ddlResponsiblePerson, "", "全部");
            CommonFuncCall.BindPurchasePlanFinishStatus(ddlFinishStatus, true);
            BindgvSalePlanList();
        }
        #endregion

        #region 打印事件
        void UCSalePlanManagerSearch_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(gvSalePlanList.GetBoundData());
        }
        #endregion

        #region 预览事件
        void UCSalePlanManagerSearch_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(gvSalePlanList.GetBoundData());
        }
        #endregion

        #region 导出事件
        void UCSalePlanManagerSearch_ExportEvent(object sender, EventArgs e)
        {

            if (this.gvSalePlanList.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "销售计划单查询" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, gvSalePlanList);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【销售计划单查询】" + ex.Message, "server");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        #endregion

        #region 预览、打印设置
        void UCSalePlanManagerSearch_SetEvent(object sender, EventArgs e)
        {
            businessPrint.PrintSet(gvSalePlanList);
        } 
        #endregion

        #region 控件事件
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
            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
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
                BindgvSalePlanList();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvSalePlanList();
        }

        private void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue.ToString()))
            {
                CommonFuncCall.BindHandle(ddlResponsiblePerson, ddlDepartment.SelectedValue.ToString(), "全部");
            }
        }

        private void gvSalePlanList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string sale_plan_id = Convert.ToString(this.gvSalePlanList.CurrentRow.Cells["sale_plan_id"].Value.ToString());
                UCSalePlanViewSearch UCSalePlanView = new UCSalePlanViewSearch(sale_plan_id, this);
                base.addUserControl(UCSalePlanView, "销售计划单-查看", "UCSalePlanView" + sale_plan_id + "", this.Tag.ToString(), this.Name);
            }
        }

        private void gvSalePlanList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = gvSalePlanList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date") || fieldNmae.Equals("plan_from") || fieldNmae.Equals("plan_to"))
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

        void gvSalePlanList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in gvSalePlanList.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            gvSalePlanList.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
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
                //Str_Where += " and order_status='" + ddlFinishStatus.SelectedValue.ToString() + "'";

                //已完成
                if (ddlFinishStatus.SelectedValue.ToString() == "1")
                { Str_Where += " and FinishStatus='已完成' "; }
                //未完成
                else if (ddlFinishStatus.SelectedValue.ToString() == "2")
                { Str_Where += " and FinishStatus='未完成' "; }
                //已超期
                else if (ddlFinishStatus.SelectedValue.ToString() == "3")
                { Str_Where += " and FinishStatus='已超期' "; }
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
        /// 加载销售计划单列表信息
        /// </summary>
        public void BindgvSalePlanList()
        {
            try
            {
                long CurrteTimeTicks = Common.LocalDateTimeToUtcLong(DateTime.Now);
                string tableName = string.Format(@" (select case import_status
                                                                when '3' then '已完成'
                                                                else 
                                                                    case when {0}<=plan_to 
                                                                    then '未完成' 
                                                                    else '已过期' 
                                                                end 
                                                            end FinishStatus,*
                                                    from tb_parts_sale_plan 
                                                    ) tb_sal_plan ", CurrteTimeTicks);
                int RecordCount = 0;
                DataTable gvPurchasePlanOrder_dt = DBHelper.GetTableByPage("查询销售计划单列表信息", tableName, "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvSalePlanList.DataSource = gvPurchasePlanOrder_dt;
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
