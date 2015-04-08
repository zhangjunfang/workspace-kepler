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
    public partial class UCPurchasePlanOrderManager : UCBase
    {
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #region 初始化窗体
        public UCPurchasePlanOrderManager()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCPurchasePlanOrderManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCPurchasePlanOrderManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCPurchasePlanOrderManager_EditEvent);
            base.DeleteEvent+=new ClickHandler(UCPurchasePlanOrderManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCPurchasePlanOrderManager_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCPurchasePlanOrderManager_SubmitEvent);
        }
        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCPurchasePlanOrderManager_Load(object sender, EventArgs e)
        {
            //base.SetBaseButtonStatus();
            //base.SetButtonVisiableManager();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvPurchasePlanOrderList, NotReadOnlyColumnsName);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            //列表的右键操作功能
            base.SetContentMenuScrip(gvPurchasePlanOrderList);

            //禁止列表自增列
            gvPurchasePlanOrderList.AutoGenerateColumns = false;
            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;
            
            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlDepartment, com_id, "全部");
            CommonFuncCall.BindHandle(ddlResponsiblePerson, "", "全部");
            CommonFuncCall.BindOrderStatus(ddlState, true);
            BindgvPurchasePlanOrderList();
        } 
        #endregion

        #region 按钮事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_AddEvent(object sender, EventArgs e)
        {
            UCPurchasePlanOrderAddOrEdit UCPurchasePlanOrderAdd = new UCPurchasePlanOrderAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCPurchasePlanOrderAdd, "采购计划单-添加", "UCPurchasePlanOrderAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_CopyEvent(object sender, EventArgs e)
        {
            string plan_id = string.Empty;
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
            plan_id = listField[0].ToString();
            UCPurchasePlanOrderAddOrEdit UCPurchasePlanOrderCopy = new UCPurchasePlanOrderAddOrEdit(WindowStatus.Copy, plan_id, this);
            base.addUserControl(UCPurchasePlanOrderCopy, "采购计划单-复制", "UCPurchasePlanOrderCopy" + plan_id + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_EditEvent(object sender, EventArgs e)
        {
            string plan_id = string.Empty;
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
                plan_id = listField[0].ToString();
                UCPurchasePlanOrderAddOrEdit UCPurchasePlanOrderEdit = new UCPurchasePlanOrderAddOrEdit(WindowStatus.Edit, plan_id, this);
                base.addUserControl(UCPurchasePlanOrderEdit, "采购计划单-编辑", "UCPurchasePlanOrderEdit" + plan_id + "", this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_DeleteEvent(object sender, EventArgs e)
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
                Dictionary<string, string> purchasePlanField = new Dictionary<string, string>();
                purchasePlanField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除采购计划单表", "tb_parts_purchase_plan", purchasePlanField, "plan_id", listField.ToArray());
                if (flag)
                {
                    BindgvPurchasePlanOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }
        /// <summary>
        /// 审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_VerifyEvent(object sender, EventArgs e)
        {
            List<string> listField = GetVerifyRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要审核的数据!");
                return;
            }
            UCVerify UcVerify= new UCVerify();
            if (UcVerify.ShowDialog() == DialogResult.OK)
            {
                string Content = UcVerify.Content;
                SYSModel.DataSources.EnumAuditStatus UcVerifyStatus = UcVerify.auditStatus;

                Dictionary<string, string> purchasePlanField = new Dictionary<string, string>();
                if (UcVerifyStatus == DataSources.EnumAuditStatus.AUDIT)
                {
                    //获取采购计划单状态(已审核)
                    purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                    purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));

                    //生成预支付单
                }
                else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                {
                    //获取采购计划单状态(审核不通过)
                    purchasePlanField.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                    purchasePlanField.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                }
                bool flag = DBHelper.BatchUpdateDataByIn("批量审核采购计划单表", "tb_parts_purchase_plan", purchasePlanField, "plan_id", listField.ToArray());
                if (flag)
                {
                    BindgvPurchasePlanOrderList();
                    MessageBoxEx.Show("操作成功！");
                }
                else
                {
                    MessageBoxEx.Show("操作失败！");
                }
            }
        }
        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPurchasePlanOrderManager_SubmitEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要提交选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }
                List<SysSQLString> listSql = new List<SysSQLString>();
                string strReceId = string.Empty;//单据Id值           
                foreach (DataGridViewRow dr in gvPurchasePlanOrderList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strReceId += dr.Cells["plan_id"].Value.ToString() + ",";
                        SysSQLString obj = new SysSQLString();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, string> dicParam = new Dictionary<string, string>();

                        string order_num = string.Empty;
                        if (dr.Cells["colorder_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())//草稿状态
                        {
                            order_num = CommonUtility.GetNewNo(DataSources.EnumProjectType.PurchasePlan);
                        }
                        else if (dr.Cells["colorder_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())//审核未通过
                        {
                            order_num = dr.Cells["colorder_num"].Value.ToString();
                        }

                        dicParam.Add("order_num", order_num);//单据编号
                        dicParam.Add("plan_id", dr.Cells["plan_id"].Value.ToString());//单据ID
                        dicParam.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//单据状态ID
                        dicParam.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//单据状态名称
                        dicParam.Add("update_by", GlobalStaticObj.UserID);//修改人ID
                        dicParam.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                        dicParam.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间
                        obj.sqlString = "update tb_parts_purchase_plan set order_num=@order_num,order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where plan_id=@plan_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }

                if (string.IsNullOrEmpty(strReceId.Replace(",", "")))
                {
                    MessageBoxEx.Show("请选择需要提交的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("更新单据状态为提交", listSql))
                {
                    MessageBoxEx.Show("提交成功！");
                    BindgvPurchasePlanOrderList();
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPlanNo.Caption = string.Empty;
            txtRemark.Caption = string.Empty;
            ddlState.SelectedIndex = 0;
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
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvPurchasePlanOrderList();
        }
        /// <summary>
        /// 双击查看明细事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string plan_Id = Convert.ToString(this.gvPurchasePlanOrderList.CurrentRow.Cells["plan_id"].Value.ToString());
                UCPurchasePlanOrderView UCPurchasePlanOrderView = new UCPurchasePlanOrderView(plan_Id, this);
                base.addUserControl(UCPurchasePlanOrderView, "采购计划单-查看", "UCPurchasePlanOrderView" + plan_Id + "", this.Tag.ToString(), this.Name);
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
            if (fieldNmae.Equals("order_status_name"))
            {
                string num = gvPurchasePlanOrderList.Rows[e.RowIndex].Cells["colorder_status"].Value.ToString();
                num = string.IsNullOrEmpty(num) ? "0" : num;
                if (num == "3")
                { gvPurchasePlanOrderList.Rows[e.RowIndex].Cells["colorder_status_name"].Style.ForeColor = Color.Red; }
                else
                { gvPurchasePlanOrderList.Rows[e.RowIndex].Cells["colorder_status_name"].Style.ForeColor = Color.Black; }
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
                CommonFuncCall.BindHandle(ddlResponsiblePerson, ddlDepartment.SelectedValue.ToString(), "全部");
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
            string Str_Where = " enable_flag=1 ";
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
            if (!string.IsNullOrEmpty(ddlState.SelectedValue.ToString()))
            {
                Str_Where += " and order_status='" + ddlState.SelectedValue.ToString() + "'";
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
        /// 获取gvPurchasePlanOrderList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchasePlanOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["plan_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 在编辑和删除时，获取gvPurchasePlanOrderList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecordByEditDelete(ref bool IsHandle)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvPurchasePlanOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    string import_status = dr.Cells["import_status"].Value.ToString();
                    if (import_status == "0")
                    { listField.Add(dr.Cells["plan_id"].Value.ToString()); }
                    else if(import_status == "1")
                    {
                        IsHandle = false;
                        MessageBoxEx.Show("单号为：" + dr.Cells["colorder_num"].Value.ToString() + "的单据，已经被占用,暂时不可操作!");
                        return listField;
                    }
                    else if (import_status == "2")
                    {
                        IsHandle = false;
                        MessageBoxEx.Show("单号为：" + dr.Cells["colorder_num"].Value.ToString() + "的单据，已经被锁定,不可以再次操作!");
                        return listField;
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 获取gvPurchasePlanOrderList列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            string order_status_SUBMIT = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
            string order_status_NOTAUDIT = Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString();
            foreach (DataGridViewRow dr in gvPurchasePlanOrderList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    //获取已提交/审核未通过的状态的编号
                    string colorder_status = dr.Cells["colorder_status"].Value.ToString();
                    if (order_status_SUBMIT == colorder_status || order_status_NOTAUDIT == colorder_status)
                    {
                        listField.Add(dr.Cells["plan_id"].Value.ToString());
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载采购计划单列表信息
        /// </summary>
        public void BindgvPurchasePlanOrderList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvPurchasePlanOrder_dt = DBHelper.GetTableByPage("查询采购计划单列表信息", "tb_parts_purchase_plan", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvPurchasePlanOrderList.DataSource = gvPurchasePlanOrder_dt;
                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        } 
        #endregion

        #region 点击行选中复选框的，控制工具栏按钮是否可用的功能代码
        /// <summary> 选中列标头的复选框事件
        /// </summary>
        private void gvPurchasePlanOrderList_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        /// <summary> 选择复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanOrderList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvPurchasePlanOrderList.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (gvPurchasePlanOrderList.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        /// <summary> 单击一行，选择或取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurchasePlanOrderList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            SetSelectedStatus();
        }
        /// <summary> 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in gvPurchasePlanOrderList.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[colorder_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[plan_id.Name].Value.ToString());
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
    }
}
