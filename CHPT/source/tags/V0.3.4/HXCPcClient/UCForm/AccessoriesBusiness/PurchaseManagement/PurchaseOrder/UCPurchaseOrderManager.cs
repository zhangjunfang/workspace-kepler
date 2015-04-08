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
            base.SubmitEvent += new ClickHandler(UCPurchaseOrderManager_SubmitEvent);

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
            //列表的右键操作功能
            base.SetContentMenuScrip(gvPurchaseOrderList);

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
        /// <summary> 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchaseOrderManager_AddEvent(object sender, EventArgs e)
        {
            UCPurchaseOrderAddOrEdit UCPurchaseOrderAdd = new UCPurchaseOrderAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCPurchaseOrderAdd, "采购订单-添加", "UCPurchaseOrderAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary> 复制事件
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
        /// <summary> 编辑事件
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
        /// <summary> 删除事件
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
                    if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                    {
                        AddBillPayReceive(listField);
                    }
                    BindgvPurchaseOrderList();
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
        void UCPurchaseOrderManager_SubmitEvent(object sender, EventArgs e)
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
                        string order_num=CommonUtility.GetNewNo(DataSources.EnumProjectType.PurchaseOrder);
                        dicParam.Add("order_num", order_num);//单据编号
                        dicParam.Add("order_id", dr.Cells["order_id"].Value.ToString());//单据ID
                        dicParam.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//单据状态ID
                        dicParam.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//单据状态名称
                        dicParam.Add("update_by", GlobalStaticObj.UserID);//修改人ID
                        dicParam.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                        dicParam.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间
                        obj.sqlString = "update tb_parts_purchase_order set order_num=@order_num,order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where order_id=@order_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                        GetPre_Order_Code(listSql,dr.Cells["order_id"].Value.ToString(), dr.Cells["order_id"].Value.ToString(), order_num);
                        if (DBHelper.BatchExeSQLStringMultiByTrans("更新单据状态为提交", listSql))
                        {
                            SetOrderStatus(dr.Cells["order_id"].Value.ToString());
                        }
                    }
                }
                MessageBoxEx.Show("提交单据完成!");
                BindgvPurchaseOrderList();
            }
            catch (Exception ex)
            { }
        }
        /// <summary> 清除按钮事件
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
        /// <summary> 查询按钮事件
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
        /// <summary> 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchaseOrderList();
        }
        /// <summary> 公司选择事件
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
        /// <summary> 部门选择事件
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
        /// <summary> 当金额不为0，审核通过时，自动生产预/应收单、预/应付单
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
        /// <summary> 提交时获取当前配件列表中存在的引用单号,保存到中间表中
        /// 并生成执行的sql
        /// </summary>
        /// <returns></returns>
        void GetPre_Order_Code(List<SysSQLString> listSql, string order_id,string post_order_id, string post_order_code)
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

            DataTable dt_relation_order = DBHelper.GetTable("查询采购订单配件表的引用单号", "tb_parts_purchase_order_p", " order_id,relation_order ", " order_id='" + order_id + "'", "", "");
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
        #endregion

        #region 提交成功后对前置单据状态和完成数量进行修改的功能代码
        /// <summary> 提交成功时,对引用的前置单据的状态进行更新 
        /// </summary>
        /// <param name="orderid"></param>
        bool SetOrderStatus(string orderid)
        {
            bool ret = false;
            try
            {
                #region 设置前置单据的状态和完成数量
                //前置单据中的配件信息是否在后置单据中全部导入完成（完成数量>=计划数量）
                List<OrderImportStatus> list_order = new List<OrderImportStatus>();
                List<OrderFinishInfo> list_orderinfo = new List<OrderFinishInfo>();
                OrderImportStatus orderimport_model = new OrderImportStatus();
                OrderFinishInfo orderfinish_info = new OrderFinishInfo();

                DataTable dt_Business = GetBusinessCount(orderid);
                DataTable dt_Finish = GetFinishCount(orderid);

                string plan_id = string.Empty;
                string order_num = string.Empty;
                string parts_code = string.Empty;
                string importtype = string.Empty;
                if (dt_Business.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Business.Rows.Count; i++)
                    {
                        bool isfinish = true;
                        int BusinessCount = int.Parse(dt_Business.Rows[i]["business_counts"].ToString());
                        plan_id = dt_Business.Rows[i]["plan_id"].ToString();
                        order_num = dt_Business.Rows[i]["order_num"].ToString();
                        parts_code = dt_Business.Rows[i]["parts_code"].ToString();
                        DataRow[] dr = null;
                        if (dt_Finish != null && dt_Finish.Rows.Count > 0)
                        {
                            dr = dt_Finish.Select(" order_num='" + order_num + "' and parts_code='" + parts_code + "'");
                        }
                        if (dr != null && dr.Count() > 0)
                        {
                            importtype = dr[0]["ImportOrderType"].ToString();

                            orderfinish_info = new OrderFinishInfo();
                            orderfinish_info.plan_id = plan_id;
                            orderfinish_info.parts_code = parts_code;
                            orderfinish_info.finish_num = dr[0]["relation_count"].ToString();
                            orderfinish_info.importtype = importtype;
                            list_orderinfo.Add(orderfinish_info);
                            if (int.Parse(dr[0]["relation_count"].ToString()) < BusinessCount)
                            {
                                isfinish = false;
                            }
                        }
                        else
                        {
                            orderfinish_info = new OrderFinishInfo();
                            orderfinish_info.plan_id = plan_id;
                            orderfinish_info.parts_code = parts_code;
                            orderfinish_info.finish_num = "0";
                            orderfinish_info.importtype = importtype;
                            list_orderinfo.Add(orderfinish_info);

                            isfinish = false;
                        }

                        orderimport_model = new OrderImportStatus();
                        orderimport_model.order_num = order_num;
                        orderimport_model.importtype = importtype;
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
        DataTable GetFinishCount(string order_id)
        {
            DataTable dt = null;
            try
            {
                string files = string.Empty;
                DataTable dt_relation_order = DBHelper.GetTable("查询采购订单配件表的引用单号", "tb_parts_purchase_order_p", " order_id,relation_order ", " order_id='" + order_id + "'", "", "");
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
                    string FileName = string.Format(@" * ");
                    string TableName = string.Format(@" (
                                                        select 
                                                        tb_plan.order_num,tb_plan.parts_code,sum(tb_order.business_counts) relation_count,tb_order.ImportOrderType
                                                         from
                                                         (
                                                           select b.relation_order,b.parts_code,b.business_counts,b.ImportOrderType from tb_parts_purchase_order a 
                                                           inner join tb_parts_purchase_order_p b on a.order_id=b.order_id
                                                           where a.order_status in ('1','2')
                                                         ) tb_order
                                                        left join 
                                                         (
                                                           select a.order_num,b.parts_code,b.business_counts from tb_parts_purchase_plan a 
                                                           inner join tb_parts_purchase_plan_p b on a.plan_id=b.plan_id {0}
                                                         ) tb_plan 
                                                         on tb_plan.order_num=tb_order.relation_order and tb_plan.parts_code=tb_order.parts_code 
                                                         where len(tb_plan.order_num)>0 and LEN(tb_plan.parts_code)>0
                                                         group by tb_plan.order_num,tb_plan.parts_code,tb_order.ImportOrderType
                                                         union
                                                         select 
                                                         tb_sale_order.order_num,tb_sale_order.parts_code,sum(tb_pur_order.business_counts) relation_count,tb_pur_order.ImportOrderType
                                                         from
                                                         (
                                                          select b.relation_order,b.parts_code,b.business_counts,b.ImportOrderType from tb_parts_purchase_order a 
                                                          inner join tb_parts_purchase_order_p b on a.order_id=b.order_id
                                                          where a.order_status in ('1','2')
                                                         ) tb_pur_order
                                                         left join 
                                                         (
                                                          select a.order_num,b.parts_code,b.business_count from tb_parts_sale_order a 
                                                          inner join tb_parts_sale_order_p b on a.sale_order_id=b.sale_order_id {0}
                                                         ) tb_sale_order 
                                                         on tb_sale_order.order_num=tb_pur_order.relation_order and tb_sale_order.parts_code=tb_pur_order.parts_code 
                                                         where len(tb_sale_order.order_num)>0 and LEN(tb_sale_order.parts_code)>0
                                                         group by tb_sale_order.order_num,tb_sale_order.parts_code,tb_pur_order.ImportOrderType
                                                    ) tb_pur_order_finishcount", files);
                    dt = DBHelper.GetTable("采购订单导入中，获取采购计划单或销售订单已完成的配件数量", TableName, FileName, "", "", "");
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
        DataTable GetBusinessCount(string order_id)
        {
            DataTable dt = null;
            try
            {
                string FileName = string.Format(@" * ");
                string TableName = string.Format(@" (
	                                                    select a.plan_id,b.order_num,a.parts_code,a.business_counts from
	                                                    (
		                                                     select * from tb_parts_purchase_plan_p where plan_id in
		                                                     (
			                                                     select plan_id from tb_parts_purchase_plan where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_purchase_order_p 
				                                                    where order_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_purchase_plan b on a.plan_id=b.plan_id
	                                                    union
	                                                    select a.sale_order_id as plan_id,b.order_num,a.parts_code,a.business_count as business_counts from
	                                                    (
		                                                     select * from tb_parts_sale_order_p where sale_order_id in
		                                                     (
			                                                     select sale_order_id from tb_parts_sale_order where order_num in
			                                                     (
				                                                    select relation_order from tb_parts_purchase_order_p 
				                                                    where order_id='{0}' and len(relation_order)>0 group by relation_order
			                                                     )
		                                                     )
	                                                    ) a left join tb_parts_sale_order b on a.sale_order_id=b.sale_order_id
                                                    ) tb_pur_order_businesscount ", order_id);
                return dt = DBHelper.GetTable("查询采购订单导入采购计划单时,获取计划单中配件已完成的数量", TableName, FileName, "", "", "");
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
                    if (item.importtype == "采购计划单")
                    {
                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        string sql1 = "update tb_parts_purchase_plan set import_status=@import_status where order_num=@order_num;";
                        dic.Add("import_status", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                        dic.Add("order_num", item.order_num);
                        sysStringSql.sqlString = sql1;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                    else if (item.importtype == "销售订单")
                    {
                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        string sql1 = "update tb_parts_sale_order set is_occupy=@is_occupy where order_num=@order_num;";
                        dic.Add("is_occupy", !item.isfinish ? "2" : "3");//单据导入状态，0正常，1占用，2锁定(部分导入), 3锁定(全部导入)
                        dic.Add("order_num", item.order_num);
                        sysStringSql.sqlString = sql1;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                }
                #endregion

                #region 更新前置单据中的各个配件的已完成数量
                foreach (OrderFinishInfo item in list_orderinfo)
                {
                    if (!list_plan.Contains(item.plan_id))
                    {
                        list_plan.Add(item.plan_id);
                        plan_ids = plan_ids + "'" + item.plan_id + "',";
                    }
                    if (item.importtype == "采购计划单")
                    {
                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        string sql1 = "update tb_parts_purchase_plan_p set finish_counts=@finish_counts where plan_id=@plan_id and parts_code=@parts_code;";
                        dic.Add("finish_counts", item.finish_num);
                        dic.Add("plan_id", item.plan_id);
                        dic.Add("parts_code", item.parts_code);
                        sysStringSql.sqlString = sql1;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                    else if (item.importtype == "销售订单")
                    {
                        sysStringSql = new SysSQLString();
                        sysStringSql.cmdType = CommandType.Text;
                        dic = new Dictionary<string, string>();
                        string sql1 = "update tb_parts_sale_order_p set finish_count=@finish_count where sale_order_id=@sale_order_id and parts_code=@parts_code;";
                        dic.Add("finish_count", item.finish_num);
                        dic.Add("sale_order_id", item.plan_id);
                        dic.Add("parts_code", item.parts_code);
                        sysStringSql.sqlString = sql1;
                        sysStringSql.Param = dic;
                        listSql.Add(sysStringSql);
                    }
                }
                #endregion
                ret = DBHelper.BatchExeSQLStringMultiByTrans("提交采购订单，更新引用的采购计划单或销售订单的导入状态", listSql);
                if (ret)
                {
                    #region 更新采购计划单的完成金额和完成数量
                    if (list_orderinfo.Count > 0)
                    {
                        listSql.Clear();
                        plan_ids = plan_ids.Trim(',');
                        string TableName = string.Format(@"
                        (
                            select plan_id,sum(finish_counts) finish_counts,
                            sum(finish_counts*business_price) as finish_money 
                                from tb_parts_purchase_plan_p 
                            where plan_id in ({0})
                            group by plan_id
                        ) tb_purchase_finish", plan_ids);
                        DataTable dt = DBHelper.GetTable("查询采购计划单各配件完成数量和完成金额", TableName, "*", "", "", "");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sysStringSql = new SysSQLString();
                                sysStringSql.cmdType = CommandType.Text;
                                dic = new Dictionary<string, string>();
                                string sql1 = "update tb_parts_purchase_plan set finish_counts=@finish_counts,plan_finish_money=@plan_finish_money where plan_id=@plan_id;";
                                dic.Add("finish_counts", dt.Rows[i]["finish_counts"].ToString());
                                dic.Add("plan_finish_money", dt.Rows[i]["finish_money"].ToString());
                                dic.Add("plan_id", dt.Rows[i]["plan_id"].ToString());
                                sysStringSql.sqlString = sql1;
                                sysStringSql.Param = dic;
                                listSql.Add(sysStringSql);
                            }
                            DBHelper.BatchExeSQLStringMultiByTrans("完成采购订单后，更新采购计划单的完成数量和完成金额", listSql);
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
