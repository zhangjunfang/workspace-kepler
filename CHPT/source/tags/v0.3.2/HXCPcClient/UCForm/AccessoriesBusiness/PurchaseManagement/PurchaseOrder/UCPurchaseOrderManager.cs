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
using HXCPcClient.Chooser;
using Model;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.PurchaseOrder
{
    public partial class UCPurchaseOrderManager : UCBase
    {
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #region 初始化事件
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public UCPurchaseOrderManager()
        {
            InitializeComponent();

            gvPurchaseOrderList.AutoGenerateColumns = false;

            base.AddEvent += new ClickHandler(UCPurchaseOrderManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCPurchaseOrderManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCPurchaseOrderManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCPurchaseOrderManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCPurchaseOrderManager_VerifyEvent);

            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchaseOrderManager_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            base.SetButtonVisiableManager();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchaseOrderList, NotReadOnlyColumnsName);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式

            //运输方式
            CommonFuncCall.BindComBoxDataSource(ddltrans_mode, "sys_trans_mode", "全部");
            //结算方式
            CommonFuncCall.BindBalanceWay(ddlclosing_way, "全部");

            CommonFuncCall.BindCompany(ddlCompany, "全部");
            CommonFuncCall.BindDepartment(ddlDepartment, "", "全部");
            CommonFuncCall.BindHandle(ddlhandle, "", "全部");
            CommonFuncCall.BindOrderStatus(ddlState, true);

            BindgvPurchaseOrderList();
        } 
        #endregion

        #region 按钮事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_AddEvent(object sender, EventArgs e)
        {
            UCPurchaseOrderAddOrEdit UCPurchaseOrderAdd = new UCPurchaseOrderAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCPurchaseOrderAdd, "采购订单-添加", "UCPurchaseOrderAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_CopyEvent(object sender, EventArgs e)
        {
            string order_id = string.Empty;
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
            order_id = listField[0].ToString();
            UCPurchaseOrderAddOrEdit UCPurchaseOrderCopy = new UCPurchaseOrderAddOrEdit(WindowStatus.Copy, order_id, this);
            base.addUserControl(UCPurchaseOrderCopy, "采购订单-复制", "UCPurchaseOrderCopy" + order_id + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_EditEvent(object sender, EventArgs e)
        {
            string order_id = string.Empty;
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
                order_id = listField[0].ToString();
                UCPurchaseOrderAddOrEdit UCPurchaseOrderEdit = new UCPurchaseOrderAddOrEdit(WindowStatus.Edit, order_id, this);
                base.addUserControl(UCPurchaseOrderEdit, "采购订单-编辑", "UCPurchaseOrderEdit" + order_id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_DeleteEvent(object sender, EventArgs e)
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
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除采购订单表", "tb_parts_purchase_order", purchaseOrderField, "order_id", listField.ToArray());
                if (flag)
                {
                    BindgvPurchaseOrderList();
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
        void UCPurchaseOrderManager_VerifyEvent(object sender, EventArgs e)
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

                Dictionary<string, string> purchasePlanField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取采购订单状态(已审核)
                    purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取采购订单状态(审核不通过)
                    purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn("批量审核采购订单表", "tb_parts_purchase_order", purchasePlanField, "order_id", listField.ToArray());
                if (flag)
                {
                    AddBillPayReceive(listField);
                    BindgvPurchaseOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtsup_name.Text = string.Empty;
            txtorder_num.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now;

            ddltrans_mode.SelectedIndex = 0;
            ddlclosing_way.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
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
                BindgvPurchaseOrderList();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchaseOrderList();
        }
        /// <summary>
        /// 公司选择事件
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
        /// <summary>
        /// 部门选择事件
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

        private void gvPurchaseOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string fieldNmae = gvPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_date") || fieldNmae.Equals("arrival_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
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

        private void gvPurchaseOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string order_status = this.gvPurchaseOrderList.CurrentRow.Cells["order_status"].Value.ToString();
                string order_Id = this.gvPurchaseOrderList.CurrentRow.Cells[0].Value.ToString();
                UCPurchaseOrderView UCPurchaseOrderView = new UCPurchaseOrderView(order_Id,order_status, this);
                base.addUserControl(UCPurchaseOrderView, "采购订单-查看", "UCPurchaseOrderView" + order_Id + "", this.Tag.ToString(), this.Name);
            }
        }

        private void txtsup_name_ChooserClick(object sender, EventArgs e)
        {
            frmSupplier chooseSupplier = new frmSupplier();
            chooseSupplier.ShowDialog();
            string supperID = chooseSupplier.supperID;
            if (!string.IsNullOrEmpty(supperID))
            {
                txtsup_name.Text = chooseSupplier.supperName;
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
            if (!string.IsNullOrEmpty(txtsup_name.Text.Trim()))
            {
                Str_Where += " and sup_name like '%" + txtsup_name.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num='" + txtorder_num.Caption.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(ddltrans_mode.SelectedValue.ToString()))
            {
                Str_Where += " and trans_mode='" + ddltrans_mode.SelectedValue.ToString() + "'";
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
                    listField.Add(dr.Cells["order_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 获取gvPurchaseOrderList列表选中的记录条数
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
                    { listField.Add(dr.Cells["order_id"].Value.ToString()); }
                    else if (import_status == "1")
                    {
                        IsHandle = false;
                        MessageBoxEx.Show("单号为：" + dr.Cells["order_num"].Value.ToString() + "的单据，已经被占用,暂时不可操作!");
                        return listField;
                    }
                    else if (import_status == "2" || import_status == "3")
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
                        listField.Add(dr.Cells["order_id"].Value.ToString());
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载采购订单列表信息
        /// </summary>
        public void BindgvPurchaseOrderList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvPurchaseOrder_dt = DBHelper.GetTableByPage("查询采购订单列表信息", "tb_parts_purchase_order", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvPurchaseOrderList.DataSource = gvPurchaseOrder_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        #endregion

        /// <summary>
        /// 当金额不为0，审核通过时，自动生产预/应收单、预/应付单
        /// </summary>
        void AddBillPayReceive(List<string> listField)
        {
            if (listField.Count > 0)
            {
                for (int i = 0; i < listField.Count; i++)
                {
                    DataTable dt = DBHelper.GetTable("", "tb_parts_purchase_order", "*", string.Format("order_id='{0}'", listField[i]), "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tb_parts_purchase_order model = new tb_parts_purchase_order();
                        CommonFuncCall.SetModlByDataTable(model, dt);
                        if (model.prepaid_money > 0)
                        {
                            tb_bill_receivable a = new tb_bill_receivable();
                            tb_balance_documents b = new tb_balance_documents();
                            tb_payment_detail c = new tb_payment_detail();

                            a.cust_id = model.sup_id;//供应商ID
                            a.order_num = CommonUtility.GetNewNo(DataSources.EnumProjectType.PAYMENT);//订单号
                            a.order_type = (int)DataSources.EnumOrderType.PAYMENT;
                            a.payment_type = (int)DataSources.EnumPaymentType.ADVANCES;
                            a.org_id = model.org_id;

                            b.billing_money = model.prepaid_money;//开单金额
                            b.documents_date = model.order_date;//单据日期
                            b.documents_id = model.order_id;//单据ID
                            b.documents_name = "采购订单";//单据名称
                            b.documents_num = model.order_num;//单据编码

                            c.money = model.prepaid_money;
                            c.balance_way = model.closing_way;//结算方式
                            //c.check_number = model.check_number;//票号

                            DBOperation.AddBillReceivable(a, b, c);
                        }
                    }
                }
            }
        }

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
                    listIDs.Add(dgvr.Cells[order_id.Name].Value.ToString());
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
                //tsmiEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                //tsmiEdit.Enabled = false;
            }
            //判断”审核“按钮是否可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr) || listFiles.Contains(invalid))
            {
                btnVerify.Enabled = false;
                //tsmiVerify.Enabled = false;
            }
            else
            {
                btnVerify.Enabled = true;
                //tsmiVerify.Enabled = true;
            }
            //包含已审核、已提交、已作废状态，提交、删除按钮不可用
            if (listFiles.Contains(auditStr) || listFiles.Contains(submitStr) || listFiles.Contains(invalid))
            {
                btnSubmit.Enabled = false;
                btnDelete.Enabled = false;
                //tsmiSubmit.Enabled = false;
                //tsmiDelete.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
                btnDelete.Enabled = true;
                //tsmiSubmit.Enabled = true;
                //tsmiDelete.Enabled = true;
            }

            if (listFiles.Contains(invalid))
            {

            }
        }
    }
}
