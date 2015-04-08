using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using Model;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SaleOrder
{
    public partial class UCSaleOrderManager : UCBase
    {
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        BusinessPrint businessPrint;//业务打印功能
        #region 窗体初始化
        /// <summary> 窗体初始化 
        /// </summary>
        public UCSaleOrderManager()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCSaleOrderManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCSaleOrderManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCSaleOrderManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCSaleOrderManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCSaleOrderManager_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCSaleOrderManager_SubmitEvent);
            base.ExportEvent += new ClickHandler(UCSaleOrderManager_ExportEvent);
            base.ViewEvent += new ClickHandler(UCSaleOrderManager_ViewEvent);
            base.PrintEvent += new ClickHandler(UCSaleOrderManager_PrintEvent);
            base.SetEvent += new ClickHandler(UCSaleOrderManager_SetEvent);
            #region 预览、打印设置
            string printObject = "tb_parts_sale_order";
            string printTitle = "销售订单";
            List<string> listNotPrint = new List<string>();
            listNotPrint.Add(sale_order_id.Name);
            PaperSize paperSize = new PaperSize();
            paperSize.Width = 297;
            paperSize.Height = 210;
            businessPrint = new BusinessPrint(gvPurchaseOrderList, printObject, printTitle, paperSize, listNotPrint);
            #endregion
        }
        /// <summary> 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSaleOrderManager_Load(object sender, EventArgs e)
        {
            //base.SetBaseButtonStatus();
            //base.SetButtonVisiableManager();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseOrderList, NotReadOnlyColumnsName);
            //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(this,btnSearch, btnClear);  
            //列表的右键操作功能
            base.SetContentMenuScrip(gvPurchaseOrderList);
            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;

            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_mode, "sys_trans_mode", "全部");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlclosing_way, "全部");

            CommonFuncCall.BindCompany(ddlCompany, "全部");
            CommonFuncCall.BindDepartment(ddlDepartment, "", "全部");
            CommonFuncCall.BindHandle(ddlhandle, "", "全部");
            CommonFuncCall.BindOrderStatus(ddlState, true);
            BindgvSaleOrderList();

            Choosefrm.CusNameChoose(txtcus_name, Choosefrm.delDataBack = null);
        } 
        #endregion

        #region 控件事件

        #region 打印事件
        void UCSaleOrderManager_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(gvPurchaseOrderList.GetBoundData());
        }
        #endregion

        #region 预览事件
        void UCSaleOrderManager_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(gvPurchaseOrderList.GetBoundData());
        }
        #endregion

        #region 导出事件
        void UCSaleOrderManager_ExportEvent(object sender, EventArgs e)
        {
            if (this.gvPurchaseOrderList.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "销售订单" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, gvPurchaseOrderList);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【销售订单】" + ex.Message, "server");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        #endregion

        #region 预览、打印设置
        void UCSaleOrderManager_SetEvent(object sender, EventArgs e)
        {
            businessPrint.PrintSet(gvPurchaseOrderList);
        } 
        #endregion

        /// <summary> 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_AddEvent(object sender, EventArgs e)
        {
            UCSaleOrderAddOrEdit UCSaleOrderAdd = new UCSaleOrderAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCSaleOrderAdd, "销售订单-添加", "UCSaleOrderAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary> 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_CopyEvent(object sender, EventArgs e)
        {
            string sale_order_id = string.Empty;
            List<string> listField = GetSelectedRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要复制的数据!");
                return;
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show("一次只可以复制一条数据!");
                return;
            }
            sale_order_id = listField[0].ToString();
            UCSaleOrderAddOrEdit UCSaleOrderCopy = new UCSaleOrderAddOrEdit(WindowStatus.Copy, sale_order_id, this);
            base.addUserControl(UCSaleOrderCopy, "销售订单-复制", "UCSaleOrderCopy" + sale_order_id + "", this.Tag.ToString(), this.Name);
        }
        /// <summary> 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_EditEvent(object sender, EventArgs e)
        {
            string sale_order_id = string.Empty;
            bool IsHandle = true;
            List<string> listField = GetSelectedRecordByEditDelete(ref IsHandle);
            if (IsHandle)
            {
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择要编辑的数据!");
                    return;
                }
                if (listField.Count > 1)
                {
                    MessageBoxEx.Show("一次只可以编辑一条数据!");
                    return;
                }
                sale_order_id = listField[0].ToString();
                UCSaleOrderAddOrEdit UCSaleOrderEdit = new UCSaleOrderAddOrEdit(WindowStatus.Edit, sale_order_id, this);
                base.addUserControl(UCSaleOrderEdit, "销售订单-编辑", "UCSaleOrderEdit" + sale_order_id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary> 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_DeleteEvent(object sender, EventArgs e)
        {
            bool IsHandle = true;
            List<string> listField = GetSelectedRecordByEditDelete(ref IsHandle);
            if (IsHandle)
            {
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择要删除的数据!");
                    return;
                }
                if (MessageBoxEx.Show("确认要删除选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }
                Dictionary<string, string> purchaseOrderField = new Dictionary<string, string>();
                purchaseOrderField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除销售订单表", "tb_parts_sale_order", purchaseOrderField, "sale_order_id", listField.ToArray());
                if (flag)
                {
                    BindgvSaleOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }
        /// <summary> 审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = GetVerifyRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要审核的数据!");
                return;
            }
            UCVerify UcVerify = new UCVerify();
            if (UcVerify.ShowDialog() == DialogResult.OK)
            {
                string Content = UcVerify.Content;
                SYSModel.DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;

                List<SysSQLString> list_sql = new List<SysSQLString>();
                for (int i = 0; i < listField.Count; i++)
                {
                    SysSQLString sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        //获取销售订单状态(已审核)
                        dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                        dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                    }
                    else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                    {
                        //获取销售订单状态(审核不通过)
                        dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                        dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                    }
                    dic.Add("update_by", GlobalStaticObj.UserID);//修改人Id
                    dic.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间  
                    dic.Add("sale_order_id", listField[i]);
                    sysStringSql.sqlString = @"update tb_parts_sale_order set 
                                               order_status=@order_status,order_status_name=@order_status_name,
                                               update_by=@update_by,update_name=@update_name,update_time=@update_time 
                                               where sale_order_id=@sale_order_id";
                    sysStringSql.Param = dic;
                    list_sql.Add(sysStringSql);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("销售订单审核操作", list_sql))
                {
                    if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        AddBillPayReceive(listField);
                    }
                    BindgvSaleOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }
        /// <summary> 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSaleOrderManager_SubmitEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = GetSelectedRecord();
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择要提交的数据!");
                    return;
                }
                if (MessageBoxEx.Show("确认要提交选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }
                string strReceId = string.Empty;//单据Id值           
                foreach (DataGridViewRow dr in gvPurchaseOrderList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        SysSQLString obj = new SysSQLString();
                        obj.cmdType = CommandType.Text;
                        List<SysSQLString> listSql = new List<SysSQLString>();
                        Dictionary<string, string> dicParam = new Dictionary<string, string>();

                        string order_num = string.Empty;
                        if (dr.Cells["order_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())//草稿状态
                        {
                            if (dr.Cells["order_num"].Value != null && dr.Cells["order_num"].Value.ToString().Length > 0)
                            {
                                order_num = dr.Cells["order_num"].Value.ToString();
                            }
                            else
                            {
                                order_num = CommonUtility.GetNewNo(DataSources.EnumProjectType.SaleOrder);
                            }
                        }
                        else if (dr.Cells["order_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())//审核未通过
                        {
                            order_num = dr.Cells["order_num"].Value.ToString();
                        }
                        dicParam.Add("order_num", order_num);//单据编号
                        dicParam.Add("sale_order_id", dr.Cells["sale_order_id"].Value.ToString());//单据ID
                        dicParam.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//单据状态ID
                        dicParam.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//单据状态名称
                        dicParam.Add("update_by", GlobalStaticObj.UserID);//修改人ID
                        dicParam.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                        dicParam.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间
                        obj.sqlString = "update tb_parts_sale_order set order_num=@order_num,order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where sale_order_id=@sale_order_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                        GetPre_Order_Code(listSql, dr.Cells["sale_order_id"].Value.ToString(), dr.Cells["sale_order_id"].Value.ToString(), order_num);
                        if (DBHelper.BatchExeSQLStringMultiByTrans("更新销售订单单据状态为提交", listSql))
                        {
                            SetOrderStatus(dr.Cells["sale_order_id"].Value.ToString());
                        }
                    }
                }
                MessageBoxEx.Show("提交单据完成!");
                BindgvSaleOrderList();
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 选择客户名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcus_name_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo chooseSupplier = new frmCustomerInfo();
            chooseSupplier.ShowDialog();
            string supperID = chooseSupplier.strCustomerId;
            if (!string.IsNullOrEmpty(supperID))
            {
                txtcus_name.Text = chooseSupplier.strCustomerName;
            }
        }
        /// <summary> 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtcus_name.Text = string.Empty;
            txtorder_num.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;

            ddltrans_mode.SelectedIndex = 0;
            ddlclosing_way.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
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
                BindgvSaleOrderList();
        }
        /// <summary> 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvSaleOrderList();
        }
        /// <summary> 选择公司
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
        /// <summary> 选择部门
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
        /// <summary> 双击事件，查看明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string order_status = this.gvPurchaseOrderList.CurrentRow.Cells["order_status"].Value.ToString();
                string sale_order_Id = this.gvPurchaseOrderList.CurrentRow.Cells["sale_order_id"].Value.ToString();
                UCSaleOrderView UCSaleOrderView = new UCSaleOrderView(sale_order_Id, order_status, this);
                base.addUserControl(UCSaleOrderView, "销售订单-查看", "UCSaleOrderView" + sale_order_Id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary> 单元格格式化，转化单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
            {
                return;
            }
            string fieldNmae = gvPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date") || fieldNmae.Equals("delivery_time"))
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
            if (fieldNmae.Equals("order_status_name"))
            {
                string num = gvPurchaseOrderList.Rows[e.RowIndex].Cells["order_status"].Value.ToString();
                num = string.IsNullOrEmpty(num) ? "0" : num;
                if (num == "3")
                { gvPurchaseOrderList.Rows[e.RowIndex].Cells["order_status_name"].Style.ForeColor = Color.Red; }
                else
                { gvPurchaseOrderList.Rows[e.RowIndex].Cells["order_status_name"].Style.ForeColor = Color.Black; }
            }
        } 
        #endregion

        #region 方法、函数
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " enable_flag=1  ";
            if (!string.IsNullOrEmpty(txtcus_name.Text.Trim()))
            {
                Str_Where += " and cust_name like '%" + txtcus_name.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num like '%" + txtorder_num.Caption.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddltrans_mode.SelectedValue.ToString()))
            {
                Str_Where += " and trans_way='" + ddltrans_mode.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlclosing_way.SelectedValue.ToString()))
            {
                Str_Where += " and closing_way='" + ddlclosing_way.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlState.SelectedValue.ToString()))
            {
                Str_Where += " and order_status='" + ddlState.SelectedValue.ToString() + "'";
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
        /// 获取gvPurchaseOrderList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["sale_order_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 在编辑和删除时，获取列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecordByEditDelete(ref bool IsHandle)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    string import_status = dr.Cells["is_occupy"].Value.ToString();
                    if (import_status == "0")
                    { listField.Add(dr.Cells["sale_order_id"].Value.ToString()); }
                    else if (import_status == "1")
                    {
                        IsHandle = false;
                        MessageBoxEx.Show("单号为：" + dr.Cells["order_num"].Value.ToString() + "的单据，已经被占用,暂时不可操作!");
                        return listField;
                    }
                    else if (import_status == "2")
                    {
                        IsHandle = false;
                        MessageBoxEx.Show("单号为：" + dr.Cells["order_num"].Value.ToString() + "的单据，已经被锁定,不可以再次操作!");
                        return listField;
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 获取gvPurchaseOrderList列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchaseOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    //获取已提交/审核未通过的状态的编号
                    string order_status_SUBMIT = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    string order_status_NOTAUDIT = Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString();
                    string colorder_status = dr.Cells["order_status"].Value.ToString();
                    if (order_status_SUBMIT == colorder_status || order_status_NOTAUDIT == colorder_status)
                    {
                        listField.Add(dr.Cells["sale_order_id"].Value.ToString());
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载销售订单列表信息
        /// </summary>
        public void BindgvSaleOrderList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvPurchaseOrder_dt = DBHelper.GetTableByPage("查询销售订单列表信息", "tb_parts_sale_order", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvPurchaseOrderList.DataSource = gvPurchaseOrder_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }    
        /// <summary>  当金额不为0，审核通过时，自动生产预/应收单、预/应付单
        /// </summary>
        void AddBillPayReceive(List<string> listField)
        {
            if (listField.Count > 0)
            {
                for (int i = 0; i < listField.Count; i++)
                {
                    DataTable dt = DBHelper.GetTable("", "tb_parts_sale_order", "*", string.Format("sale_order_id='{0}'", listField[i]), "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tb_parts_sale_order model = new tb_parts_sale_order();
                        CommonFuncCall.SetModlByDataTable(model, dt);
                        if (model.advance_money > 0)
                        {
                            tb_bill_receivable a = new tb_bill_receivable();
                            tb_balance_documents b = new tb_balance_documents();
                            tb_payment_detail c = new tb_payment_detail();

                            a.cust_id = model.cust_id;//客户ID
                            a.order_num = CommonUtility.GetNewNo(DataSources.EnumProjectType.RECEIVABLE);//订单号
                            a.order_type = (int)DataSources.EnumOrderType.RECEIVABLE;
                            a.payment_type = (int)DataSources.EnumReceivableType.ADVANCES;
                            a.org_id = model.org_id;

                            b.billing_money = model.advance_money;//开单金额
                            b.documents_date = model.order_date;//单据日期
                            b.documents_id = model.sale_order_id;//单据ID
                            b.documents_name = "销售订单";//单据名称
                            b.documents_num = model.order_num;//单据编码

                            c.money = model.advance_money;//金额
                            c.balance_way = model.closing_way;//结算方式
                            //c.check_number = model.check_number;//票号

                            DBOperation.AddBillReceivable(a, b, c);
                        }
                    }
                }
            }
        }
        /// <summary> 提交时获取当前配件列表中存在的引用单号,保存到中间表中
        /// 并生成执行的sql
        /// </summary>
        /// <returns></returns>
        void GetPre_Order_Code(List<SysSQLString> listSql, string sale_order_id, string post_order_id, string post_order_code)
        {
            List<string> list = new List<string>();
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql1 = "delete from tr_order_relation where post_order_id=@post_order_id;";
            dic.Add("post_order_id", post_order_id);
            dic.Add("post_order_code", post_order_code);
            sysStringSql.sqlString = sql1;
            sysStringSql.Param = dic;
            listSql.Add(sysStringSql);

            DataTable dt_relation_order = DBHelper.GetTable("查询销售订单配件表的引用单号", "tb_parts_sale_order_p", " sale_order_id,relation_order ", " sale_order_id='" + sale_order_id + "'", "", "");
            if (dt_relation_order != null && dt_relation_order.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_relation_order.Rows)
                {
                    string relation_order = dr["relation_order"] == null ? "" : dr["relation_order"].ToString();
                    if (!string.IsNullOrEmpty(relation_order))
                    {
                        if (!list.Contains(relation_order))
                        {
                            list.Add(relation_order);
                            sysStringSql = new SysSQLString();
                            sysStringSql.cmdType = CommandType.Text;
                            dic = new Dictionary<string, string>();
                            dic.Add("order_relation_id", Guid.NewGuid().ToString());
                            dic.Add("pre_order_id", string.Empty);
                            dic.Add("pre_order_code", relation_order);
                            dic.Add("post_order_id", post_order_id);
                            dic.Add("post_order_code", post_order_code);
                            string sql2 = string.Format(@"Insert Into tr_order_relation(order_relation_id,pre_order_id,pre_order_code,
                                                      post_order_id,post_order_code)  values(@order_relation_id,@pre_order_id,
                                                      @pre_order_code,@post_order_id,@post_order_code);");
                            sysStringSql.sqlString = sql2;
                            sysStringSql.Param = dic;
                            listSql.Add(sysStringSql);
                        }
                    }
                }
            }
        }
        #endregion

        #region 点击行选中复选框的，控制工具栏按钮是否可用的功能代码
        /// <summary> 选中列标头的复选框事件
        /// </summary>
        private void gvPurchaseOrderList_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        /// <summary> 选择复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseOrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPurchaseOrderList.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (gvPurchaseOrderList.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        /// <summary> 单击一行，选择或取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchaseOrderList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in gvPurchaseOrderList.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            gvPurchaseOrderList.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            SetSelectedStatus();
        }
        /// <summary> 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in gvPurchaseOrderList.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[order_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[sale_order_id.Name].Value.ToString());
                }
            }

            //提交
            string submitStr = ((int)DataSources.EnumAuditStatus.SUBMIT).ToString();
            //审核
            string auditStr = ((int)DataSources.EnumAuditStatus.AUDIT).ToString();
            //草稿
            string draftStr = ((int)DataSources.EnumAuditStatus.DRAFT).ToString();
            //审核未通过
            string noAuditStr = ((int)DataSources.EnumAuditStatus.NOTAUDIT).ToString();
            //作废
            string invalid = ((int)DataSources.EnumAuditStatus.Invalid).ToString();
            //复制按钮，只选择一个并且不是作废，可以复制
            if (listFiles.Count == 1 && !listFiles.Contains(invalid))
            {
                btnCopy.Enabled = true;
            }
            else
            {
                btnCopy.Enabled = false;
            }
            //编辑按钮，只选择一个并且是草稿或未通过状态，可以编辑
            if (listFiles.Count == 1 && (listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr)))
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
            }
            //判断”审核“按钮是否可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr) || listFiles.Contains(invalid))
            {
                btnVerify.Enabled = false;
            }
            else
            {
                btnVerify.Enabled = true;
            }
            //包含已审核、已提交、已作废状态，提交、删除按钮不可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(submitStr) || listFiles.Contains(invalid))
            {
                btnSubmit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnDelete.Enabled = true;
            }

            if (listFiles.Contains(invalid))
            {

            }
        } 
        #endregion

        #region 提交成功后对前置单据状态和完成数量进行修改的功能代码
        /// <summary> 提交成功时,对引用的前置单据的状态进行更新 
        /// </summary>
        /// <param name="orderid"></param>
        bool SetOrderStatus(string saleorderid)
        {
            bool ret = false;
            try
            {
                #region 设置前置单据的状态和完成数量
                List<OrderImportStatus> list_order = new List<OrderImportStatus>();
                List<OrderFinishInfo> list_orderinfo = new List<OrderFinishInfo>();
                OrderImportStatus orderimport_model = new OrderImportStatus();
                OrderFinishInfo orderfinish_info = new OrderFinishInfo();

                DataTable dt_Business = GetBusinessCount(saleorderid);
                DataTable dt_Finish = GetFinishCount(saleorderid);

                string sale_plan_id = string.Empty;
                string order_num = string.Empty;
                string parts_code = string.Empty;
                if (dt_Business.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Business.Rows.Count; i++)
                    {
                        bool isfinish = true;
                        int BusinessCount = int.Parse(dt_Business.Rows[i]["business_count"].ToString());
                        sale_plan_id = dt_Business.Rows[i]["sale_plan_id"].ToString();
                        order_num = dt_Business.Rows[i]["order_num"].ToString();
                        parts_code = dt_Business.Rows[i]["parts_code"].ToString();
                        DataRow[] dr = dt_Finish.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                        if (dr != null && dr.Count() > 0)
                        {
                            orderfinish_info = new OrderFinishInfo();
                            orderfinish_info.sale_plan_id = sale_plan_id;
                            orderfinish_info.parts_code = parts_code;
                            orderfinish_info.finish_num = dr[0]["relation_count"].ToString();
                            list_orderinfo.Add(orderfinish_info);
                            if (int.Parse(dr[0]["relation_count"].ToString()) < BusinessCount)
                            {
                                isfinish = false;
                            }
                        }
                        else
                        {
                            orderfinish_info = new OrderFinishInfo();
                            orderfinish_info.sale_plan_id = sale_plan_id;
                            orderfinish_info.parts_code = parts_code;
                            orderfinish_info.finish_num = "0";
                            list_orderinfo.Add(orderfinish_info);

                            isfinish = false;
                        }

                        orderimport_model = new OrderImportStatus();
                        orderimport_model.order_num = order_num;
                        orderimport_model.isfinish = isfinish;
                        if (list_order.Count > 0)
                        {
                            if (list_order.Where(p => p.order_num == order_num).Count() > 0)
                            {
                                if (!isfinish)
                                {
                                    for (int a = 0; a < list_order.Count; a++)
                                    {
                                        if (list_order[a].order_num == order_num && list_order[a].isfinish)
                                        { list_order[a].isfinish = isfinish; }
                                    }
                                }
                            }
                            else
                            { list_order.Add(orderimport_model); }
                        }
                        else
                        { list_order.Add(orderimport_model); }
                    }
                }
                ret = ImportPurchasePlanStatus(list_order, list_orderinfo);
                #endregion
            }
            catch (Exception ex)
            { }
            return ret;
        }
        /// <summary> 获取各个前置单据中配件业务数量在后置单据中的已完成的数量
        /// </summary>
        DataTable GetFinishCount(string sale_order_id)
        {
            DataTable dt = null;
            try
            {
                string files = string.Empty;
                DataTable dt_relation_order = DBHelper.GetTable("查询销售订单配件表的引用单号", "tb_parts_sale_order_p", " sale_order_id,relation_order ", " sale_order_id='" + sale_order_id + "'", "", "");
                if (dt_relation_order != null && dt_relation_order.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_relation_order.Rows)
                    {
                        string relation_order = dr["relation_order"] == null ? "" : dr["relation_order"].ToString();
                        if (!string.IsNullOrEmpty(relation_order))
                        {
                            if (!files.Contains("'" + relation_order + "',"))
                            {
                                files += "'" + relation_order + "',";
                            }
                        }
                    }
                    files = files.Trim(',');
                    if (files.Trim().Length > 0)
                    {
                        files = " where a.order_num in (" + files + ")";
                    }
                    string FileName = string.Format(@" tb_plan.order_num,tb_plan.parts_code,sum(tb_order.business_count) relation_count ");
                    string TableName = string.Format(@" (
                                                      select b.relation_order,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                      inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id
                                                      where a.order_status in ('1','2')
                                                    ) tb_order
                                                    left join 
                                                    (
                                                      select a.order_num,b.parts_code,b.business_count from tb_parts_sale_plan a 
                                                      inner join tb_parts_sale_plan_p b on a.sale_plan_id=b.sale_plan_id  {0}
                                                    ) tb_plan 
                                                     on tb_plan.order_num=tb_order.relation_order and tb_plan.parts_code=tb_order.parts_code 
                                                    group by tb_plan.order_num,tb_plan.parts_code ", files);
                    dt = DBHelper.GetTable("查询销售购订单导入销售计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
                }
                return dt;
            }
            catch (Exception ex)
            { return dt; }
            finally { }
        }
        /// <summary> 获取前置单据的业务信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        DataTable GetBusinessCount(string sale_order_id)
        {
            DataTable dt = null;
            try
            {
                string FileName = string.Format(@" a.sale_plan_id,b.order_num,a.parts_code,a.business_count ");
                string TableName = string.Format(@" (
	                                                  select * from tb_parts_sale_plan_p where sale_plan_id in
	                                                  (
		                                                 select sale_plan_id from tb_parts_sale_plan where order_num in
		                                                 (
			                                                select relation_order from tb_parts_sale_order_p 
			                                                where sale_order_id='{0}' and len(relation_order)>0 group by relation_order
		                                                 )
	                                                  )
                                                    ) a left join tb_parts_sale_plan b on a.sale_plan_id=b.sale_plan_id ", sale_order_id);
                return dt = DBHelper.GetTable("查询销售订单导入销售计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        /// <summary> 对引用的前置单据的状态进行更新的方法
        /// </summary>
        /// <param name="list_order"></param>
        bool ImportPurchasePlanStatus(List<OrderImportStatus> list_order, List<OrderFinishInfo> list_orderinfo)
        {
            bool ret = false;
            string plan_ids = string.Empty;
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<string> list_plan = new List<string>();
            try
            {
                #region 更新前置单据的导入状态字段
                foreach (OrderImportStatus item in list_order)
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_plan set import_status=@import_status where order_num=@order_num;";
                    dic.Add("import_status", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                    dic.Add("order_num", item.order_num);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                #endregion

                #region 更新前置单据中的各个配件的已完成数量
                foreach (OrderFinishInfo item in list_orderinfo)
                {
                    if (!list_plan.Contains(item.sale_plan_id))
                    {
                        list_plan.Add(item.sale_plan_id);
                        plan_ids = plan_ids + "'" + item.sale_plan_id + "',";
                    }
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string sql1 = "update tb_parts_sale_plan_p set finish_count=@finish_count where sale_plan_id=@sale_plan_id and parts_code=@parts_code;";
                    dic.Add("finish_count", item.finish_num);
                    dic.Add("sale_plan_id", item.sale_plan_id);
                    dic.Add("parts_code", item.parts_code);
                    sysStringSql.sqlString = sql1;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
                #endregion
                ret = DBHelper.BatchExeSQLStringMultiByTrans("提交销售订单，更新引用的销售计划单或销售订单的导入状态", listSql);
                if (ret)
                {
                    #region 更新采购计划单的完成金额和完成数量
                    if (list_orderinfo.Count > 0)
                    {
                        listSql.Clear();
                        plan_ids = plan_ids.Trim(',');
                        string TableName = string.Format(@"
                        (
                            select sale_plan_id,sum(finish_count) finish_count,
                            sum(finish_count*business_price) as finish_money 
                                from tb_parts_sale_plan_p 
                             where sale_plan_id in ({0})
                            group by sale_plan_id
                        ) tb_sale_finish", plan_ids);
                        DataTable dt = DBHelper.GetTable("查询销售计划单各配件完成数量和完成金额", TableName, "*", "", "", "");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sysStringSql = new SysSQLString();
                                sysStringSql.cmdType = CommandType.Text;
                                dic = new Dictionary<string, string>();
                                string sql1 = "update tb_parts_sale_plan set finish_counts=@finish_counts,finish_money=@finish_money where sale_plan_id=@sale_plan_id;";
                                dic.Add("finish_counts", dt.Rows[i]["finish_count"].ToString());
                                dic.Add("finish_money", dt.Rows[i]["finish_money"].ToString());
                                dic.Add("sale_plan_id", dt.Rows[i]["sale_plan_id"].ToString());
                                sysStringSql.sqlString = sql1;
                                sysStringSql.Param = dic;
                                listSql.Add(sysStringSql);
                            }
                            DBHelper.BatchExeSQLStringMultiByTrans("完成销售订单后，更新销售计划单的完成数量和完成金额", listSql);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            { }
            return ret;
        }
        #endregion
    }
}
