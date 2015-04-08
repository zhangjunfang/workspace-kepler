using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using HXCPcClient.Chooser;
using Utility.Common;
using Model;

namespace HXCPcClient.UCForm.AccessoriesBusiness.PurchaseManagement.YuTongPurchaseOrder
{
    public partial class UCYTManager : UCBase
    {
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #region 变量
        tb_parts_purchase_order_2 yt_purchaseorder_model = new tb_parts_purchase_order_2();
        tb_parts_purchase_order_p_2 yt_partsorder_model = new tb_parts_purchase_order_p_2();
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public UCYTManager()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCYTManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCYTManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCYTManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCYTManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCYTManager_VerifyEvent);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCYTManager_Load(object sender, EventArgs e)
        {
            base.SetBaseButtonStatus();
            base.SetButtonVisiableManager();
            dateTimeReqDeliveryTimeStart.Value = DateTime.Now;
            dateTimeReqDeliveryTimeEnd.Value = DateTime.Now;

            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvYTPurchaseOrderList, NotReadOnlyColumnsName);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            //列表的右键操作功能
            base.SetContentMenuScrip(gvYTPurchaseOrderList);
            //绑定宇通采购订单类型
            CommonFuncCall.BindYTPurchaseOrderType(ddlorder_type, true, "全部");
            //绑定紧急程度
            CommonFuncCall.BindComBoxDataSource(ddlemergency_level, "emergency_level_yt", "全部");
            //调拨类型
            CommonFuncCall.BindYTAllotType(ddlallot_type, true, "全部");

            CommonFuncCall.BindCompany(ddlCompany, "全部");
            CommonFuncCall.BindDepartment(ddlDepartment, "", "全部");
            CommonFuncCall.BindHandle(ddlhandle, "", "全部");
            CommonFuncCall.BindOrderStatus(ddlorder_status, true);


            BindgvYTPurchaseOrderList();
        } 
        #endregion

        #region 控件事件
        void UCYTManager_AddEvent(object sender, EventArgs e)
        {
            UCYTAddOrEdit UCYTPurchaseOrderAdd = new UCYTAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCYTPurchaseOrderAdd, "宇通采购订单-添加", "UCYTPurchaseOrderAdd", this.Tag.ToString(), this.Name);
        }

        void UCYTManager_CopyEvent(object sender, EventArgs e)
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
            UCYTAddOrEdit UCPurchaseOrderCopy = new UCYTAddOrEdit(WindowStatus.Copy, order_id, this);
            base.addUserControl(UCPurchaseOrderCopy, "宇通采购订单-复制", "UCPurchaseOrderCopy" + order_id + "", this.Tag.ToString(), this.Name);
        }

        void UCYTManager_EditEvent(object sender, EventArgs e)
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
                UCYTAddOrEdit UCPurchaseOrderEdit = new UCYTAddOrEdit(WindowStatus.Edit, order_id, this);
                base.addUserControl(UCPurchaseOrderEdit, "宇通采购订单-编辑", "UCPurchaseOrderEdit" + order_id + "", this.Tag.ToString(), this.Name);
            }
        }

        void UCYTManager_DeleteEvent(object sender, EventArgs e)
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
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除宇通采购订单表", "tb_parts_purchase_order_2", purchaseOrderField, "purchase_order_yt_id", listField.ToArray());
                if (flag)
                {
                    BindgvYTPurchaseOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }

        void UCYTManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = GetVerifyRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要审核的数据!");
                return;
            }
            UCVerify UcVerify = new UCVerify();
            UcVerify.ShowDialog();
            string Content = UcVerify.Content;
            SYSModel.DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;

            Dictionary<string, string> purchasePlanField = new Dictionary<string, string>();
            if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
            {
                //获取宇通采购订单状态(已审核)
                purchasePlanField.Add("apply_date_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
                purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
            }
            else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
            {
                //获取宇通采购订单状态(审核不通过)
                purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
            }
            bool flag = DBHelper.BatchUpdateDataByIn("批量审核宇通采购订单表", "tb_parts_purchase_order_2", purchasePlanField, "purchase_order_yt_id", listField.ToArray());
            if (flag)
            {
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    DealPurascherToYT(listField);
                }
                BindgvYTPurchaseOrderList();
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        /// <summary>
        /// 清除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtorder_num.Caption = string.Empty;
            txtparts_code.Text = string.Empty;
            txtparts_name.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            ddlorder_type.SelectedIndex = 0;
            ddlallot_type.SelectedIndex = 0;
            ddlorder_status.SelectedIndex = 0;
            ddlemergency_level.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlhandle.SelectedIndex = 0;
            dateTimeEnd.Value = string.Empty;
            dateTimeStart.Value = string.Empty;
            dateTimeReqDeliveryTimeStart.Value = DateTime.Now;
            dateTimeReqDeliveryTimeEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dateTimeStart.Value) && !string.IsNullOrEmpty(dateTimeEnd.Value))
            {
                if (Convert.ToDateTime(Convert.ToDateTime(dateTimeStart.Value).ToShortDateString() + " 00:00:00") > Convert.ToDateTime(Convert.ToDateTime(dateTimeEnd.Value).ToShortDateString() + " 00:00:00"))
                {
                    MessageBoxEx.Show("申请日期的开始时间不可以大于结束时间");
                }
            }
            else if (Convert.ToDateTime(dateTimeReqDeliveryTimeStart.Value.ToShortDateString() + " 00:00:00") > Convert.ToDateTime(dateTimeReqDeliveryTimeEnd.Value.ToShortDateString() + " 00:00:00"))
            { MessageBoxEx.Show("要求发货时间的开始时间不可以大于结束时间"); }
            else
                BindgvYTPurchaseOrderList();
        }
        /// <summary>
        /// 选择配件编码事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtparts_code_ChooserClick(object sender, EventArgs e)
        {
            frmParts chooseParts = new frmParts();
            chooseParts.ShowDialog();
            if (!string.IsNullOrEmpty(chooseParts.PartsID))
            {
                txtparts_code.Text = chooseParts.PartsCode;
                txtparts_name.Text = chooseParts.PartsName;
            }
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvYTPurchaseOrderList();
        }
        /// <summary>
        /// 选择公司事件
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
        /// 选择部门事件
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
        /// <summary>
        /// 双击列表查看明细事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvYTPurchaseOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string order_status = this.gvYTPurchaseOrderList.CurrentRow.Cells["order_status"].Value.ToString();
                string order_Id = this.gvYTPurchaseOrderList.CurrentRow.Cells[0].Value.ToString();
                UCYTView UCPurchaseOrderView = new UCYTView(order_Id, this);
                base.addUserControl(UCPurchaseOrderView, "宇通采购订单-查看", "UCPurchaseOrderView" + order_Id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 列表单元格格式化内容事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvYTPurchaseOrderList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string order_date = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (order_date.Equals("order_date") || order_date.Equals("order_date"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            string req_delivery_time = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (req_delivery_time.Equals("req_delivery_time") || req_delivery_time.Equals("req_delivery_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            string fieldNmae = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("order_status_name"))
            {
                string num = gvYTPurchaseOrderList.Rows[e.RowIndex].Cells["order_status"].Value.ToString();
                num = string.IsNullOrEmpty(num) ? "0" : num;
                if (num == "3")
                { gvYTPurchaseOrderList.Rows[e.RowIndex].Cells["order_status_name"].Style.ForeColor = Color.Red; }
                else
                { gvYTPurchaseOrderList.Rows[e.RowIndex].Cells["order_status_name"].Style.ForeColor = Color.Black; }
            }
            //string fieldNmae1 = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            //if (fieldNmae1.Equals("reality_arrival_date") || fieldNmae1.Equals("reality_arrival_date"))
            //{
            //    long ticks = (long)e.Value;
            //    e.Value = Common.UtcLongToLocalDateTime(ticks);
            //}
            //string fieldNmae2 = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
            //if (fieldNmae1.Equals("reality_arrival_date") || fieldNmae1.Equals("reality_arrival_date"))
            //{
            //    long ticks = (long)e.Value;
            //    e.Value = Common.UtcLongToLocalDateTime(ticks);
            //}
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
            if (!string.IsNullOrEmpty(txtorder_num.Caption.Trim()))
            {
                Str_Where += " and order_num='" + txtorder_num.Caption.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(ddlorder_type.SelectedValue.ToString()))
            {
                Str_Where += " and order_type='" + ddlorder_type.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlallot_type.SelectedValue.ToString()))
            {
                Str_Where += " and allot_type='" + ddlallot_type.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(ddlemergency_level.SelectedValue.ToString()))
            {
                Str_Where += " and emergency_level='" + ddlemergency_level.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(txtparts_code.Text.Trim()))
            {
                Str_Where += " and parts_code = '" + txtparts_code.Text.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtparts_name.Text.Trim()))
            {
                Str_Where += " and parts_name like '%" + txtparts_name.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(ddlorder_status.SelectedValue.ToString()))
            {
                Str_Where += " and order_status='" + ddlorder_status.SelectedValue.ToString() + "'";
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
            if (!string.IsNullOrEmpty(dateTimeStart.Value))
            {
                DateTime dtime = Convert.ToDateTime(Convert.ToDateTime(dateTimeStart.Value).ToShortDateString() + " 00:00:00");
                Str_Where += " and apply_date_time>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (!string.IsNullOrEmpty(dateTimeEnd.Value))
            {
                DateTime dtime = Convert.ToDateTime(Convert.ToDateTime(dateTimeEnd.Value).ToShortDateString() + " 23:59:59");
                Str_Where += " and apply_date_time<=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeReqDeliveryTimeStart.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeReqDeliveryTimeStart.Value.ToShortDateString() + " 00:00:00");
                Str_Where += " and req_delivery_time>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeReqDeliveryTimeEnd.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeReqDeliveryTimeEnd.Value.ToShortDateString() + " 23:59:59");
                Str_Where += " and req_delivery_time<=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            return Str_Where;
        }
        /// <summary>
        /// 获取gvYTPurchaseOrderList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvYTPurchaseOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["purchase_order_yt_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 获取gvYTPurchaseOrderList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecordByEditDelete(ref bool IsHandle)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvYTPurchaseOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["purchase_order_yt_id"].Value.ToString());
                    //string import_status = dr.Cells["is_occupy"].Value.ToString();
                    //if (import_status == "0")
                    //{ listField.Add(dr.Cells["purchase_order_yt_id"].Value.ToString()); }
                    //else if (import_status == "1")
                    //{
                    //    IsHandle = false;
                    //    MessageBoxEx.Show("单号为：" + dr.Cells["order_num"].Value.ToString() + "的单据，已经被占用,暂时不可操作!");
                    //    return listField;
                    //}
                    //else if (import_status == "2")
                    //{
                    //    IsHandle = false;
                    //    MessageBoxEx.Show("单号为：" + dr.Cells["order_num"].Value.ToString() + "的单据，已经被锁定,不可以再次操作!");
                    //    return listField;
                    //}
                }
            }
            return listField;
        }
        /// <summary>
        /// 获取gvYTPurchaseOrderList列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvYTPurchaseOrderList.Rows)
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
                        listField.Add(dr.Cells["purchase_order_yt_id"].Value.ToString());
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载采购订单列表信息
        /// </summary>
        public void BindgvYTPurchaseOrderList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvYTPurchaseOrder_dt = DBHelper.GetTableByPage("查询宇通采购订单列表信息", "tb_parts_purchase_order_2", "*,'查看' as viewfile", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvYTPurchaseOrderList.DataSource = gvYTPurchaseOrder_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        /// <summary> 通过审核后向宇通发送宇通采购订单信息
        /// </summary>
        /// <param name="listField">宇通采购订单号集合</param>
        void DealPurascherToYT(List<string> listField)
        {
            try
            {
                if (listField.Count > 0)
                {
                    for (int a = 0; a < listField.Count; a++)
                    {
                        DataTable dt = DBHelper.GetTable("查看一条宇通采购订单信息", "tb_parts_purchase_order_2", "*", " purchase_order_yt_id='" + listField[a] + "'", "", "");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            yt_purchaseorder_model = new tb_parts_purchase_order_2();
                            yt_purchaseorder_model.listDetails = new List<tb_parts_purchase_order_p_2>();
                            CommonFuncCall.SetModlByDataTable(yt_purchaseorder_model, dt);
                            DataTable dt_parts = DBHelper.GetTable("查看宇通采购订单配件信息", "tb_parts_purchase_order_p_2", "*", " purchase_order_yt_id='" + listField[a] + "'", "", "");
                            if (dt_parts != null && dt_parts.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_parts.Rows.Count; i++)
                                {
                                    yt_partsorder_model = new tb_parts_purchase_order_p_2();
                                    CommonFuncCall.SetModlByDataTable(yt_partsorder_model, dt_parts, i);
                                    yt_purchaseorder_model.listDetails.Add(yt_partsorder_model);
                                }
                            }
                            if (yt_purchaseorder_model.crm_bill_id == ".")
                            {
                                yt_purchaseorder_model.crm_bill_id = string.Empty;
                            }
                            DBHelper.WebServHandler("", EnumWebServFunName.UpLoadPartPurchase, yt_purchaseorder_model);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        private void gvYTPurchaseOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string viewfile = gvYTPurchaseOrderList.Columns[e.ColumnIndex].DataPropertyName;
                if (viewfile == "viewfile")
                {
                    string YTOrder_num = this.gvYTPurchaseOrderList.CurrentRow.Cells["order_num"].Value.ToString(); 
                    string BusinessCount = this.gvYTPurchaseOrderList.CurrentRow.Cells["application_count"].Value.ToString();
                    string dsn_adjustable_parts = this.gvYTPurchaseOrderList.CurrentRow.Cells["crm_bill_id"].Value.ToString(); 
                    frmDistributionView frm = new frmDistributionView(YTOrder_num, BusinessCount, dsn_adjustable_parts);
                    frm.ShowDialog();
                }
            }
        }

        /// <summary> 选中列标头的复选框事件
        /// </summary>
        private void gvYTPurchaseOrderList_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        /// <summary> 选择复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvYTPurchaseOrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvYTPurchaseOrderList.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (gvYTPurchaseOrderList.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        /// <summary> 单击一行，选择或取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvYTPurchaseOrderList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in gvYTPurchaseOrderList.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            gvYTPurchaseOrderList.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
            SetSelectedStatus();
        }
        /// <summary> 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            //listIDs.Clear();
            ////已选择状态列表
            //List<string> listFiles = new List<string>();
            //foreach (DataGridViewRow dgvr in gvYTPurchaseOrderList.Rows)
            //{
            //    if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
            //    {
            //        listFiles.Add(dgvr.Cells[order_status.Name].Value.ToString());
            //        listIDs.Add(dgvr.Cells[purchase_order_yt_id.Name].Value.ToString());
            //    }
            //}

            ////提交
            //string submitStr = ((int)DataSources.EnumAuditStatus.SUBMIT).ToString();
            ////审核
            //string auditStr = ((int)DataSources.EnumAuditStatus.AUDIT).ToString();
            ////草稿
            //string draftStr = ((int)DataSources.EnumAuditStatus.DRAFT).ToString();
            ////审核未通过
            //string noAuditStr = ((int)DataSources.EnumAuditStatus.NOTAUDIT).ToString();
            ////作废
            //string invalid = ((int)DataSources.EnumAuditStatus.Invalid).ToString();
            ////复制按钮，只选择一个并且不是作废，可以复制
            //if (listFiles.Count == 1 && !listFiles.Contains(invalid))
            //{
            //    btnCopy.Enabled = true;
            //}
            //else
            //{
            //    btnCopy.Enabled = false;
            //}
            ////编辑按钮，只选择一个并且是草稿或未通过状态，可以编辑
            //if (listFiles.Count == 1 && (listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr)))
            //{
            //    btnEdit.Enabled = true;
            //    //tsmiEdit.Enabled = true;
            //}
            //else
            //{
            //    btnEdit.Enabled = false;
            //    //tsmiEdit.Enabled = false;
            //}
            ////判断”审核“按钮是否可用
            //if (listFiles.Contains(auditStr) || listFiles.Contains(draftStr) || listFiles.Contains(noAuditStr) || listFiles.Contains(invalid))
            //{
            //    btnVerify.Enabled = false;
            //    //tsmiVerify.Enabled = false;
            //}
            //else
            //{
            //    btnVerify.Enabled = true;
            //    //tsmiVerify.Enabled = true;
            //}
            ////包含已审核、已提交、已作废状态，提交、删除按钮不可用
            //if (listFiles.Contains(auditStr) || listFiles.Contains(submitStr) || listFiles.Contains(invalid))
            //{
            //    btnSubmit.Enabled = false;
            //    btnDelete.Enabled = false;
            //    //tsmiSubmit.Enabled = false;
            //    //tsmiDelete.Enabled = false;
            //}
            //else
            //{
            //    btnSubmit.Enabled = true;
            //    btnDelete.Enabled = true;
            //    //tsmiSubmit.Enabled = true;
            //    //tsmiDelete.Enabled = true;
            //}

            //if (listFiles.Contains(invalid))
            //{

            //}
        }
    }
}
