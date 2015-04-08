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

namespace HXCPcClient.UCForm.AccessoriesBusiness.SaleManagement.SalePlan
{
    public partial class UCSalePlanManager : UCBase
    {
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #region 初始化窗体
        public UCSalePlanManager()
        {
            InitializeComponent();

            base.AddEvent += new ClickHandler(UCSalePlanManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCSalePlanManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCSalePlanManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCSalePlanManager_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCSalePlanManager_VerifyEvent);
            base.SubmitEvent += new ClickHandler(UCSalePlanManager_SubmitEvent);
        }
        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCSalePlanManager_Load(object sender, EventArgs e)
        {
            //base.SetBaseButtonStatus();
            //base.SetButtonVisiableManager();
            string[] NotReadOnlyColumnsName = new string[] { "colCheck" };
            CommonFuncCall.SetColumnReadOnly(gvSalePlanList, NotReadOnlyColumnsName);
            //列表的右键操作功能
            base.SetContentMenuScrip(gvSalePlanList);
            //禁止列表自增列
            gvSalePlanList.AutoGenerateColumns = false;
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式

            dateTimeStart.Value = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            dateTimeEnd.Value = DateTime.Now;

            string com_id = GlobalStaticObj.CurrUserCom_Id;//公司ID
            CommonFuncCall.BindDepartment(ddlDepartment, com_id, "全部");
            CommonFuncCall.BindHandle(ddlResponsiblePerson, "", "全部");
            CommonFuncCall.BindOrderStatus(ddlState, true);
            BindgvSalePlanList();
        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSalePlanManager_AddEvent(object sender, EventArgs e)
        {
            UCSalePlanAddOrEdit UCSalePlanAdd = new UCSalePlanAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCSalePlanAdd, "销售计划单-添加", "UCSalePlanAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSalePlanManager_CopyEvent(object sender, EventArgs e)
        {
            string sale_plan_id = string.Empty;
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
            sale_plan_id = listField[0].ToString();
            UCSalePlanAddOrEdit UCSalePlanEdit = new UCSalePlanAddOrEdit(WindowStatus.Copy, sale_plan_id, this);
            base.addUserControl(UCSalePlanEdit, "销售计划单-复制", "UCSalePlanEdit" + sale_plan_id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSalePlanManager_EditEvent(object sender, EventArgs e)
        {
            string sale_plan_id = string.Empty;
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
                sale_plan_id = listField[0].ToString();
                UCSalePlanAddOrEdit UCSalePlanEdit = new UCSalePlanAddOrEdit(WindowStatus.Edit, sale_plan_id, this);
                base.addUserControl(UCSalePlanEdit, "销售计划单-编辑", "UCSalePlanEdit" + sale_plan_id, this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSalePlanManager_DeleteEvent(object sender, EventArgs e)
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
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除销售计划单表", "tb_parts_sale_plan", purchasePlanField, "sale_plan_id", listField.ToArray());
                if (flag)
                {
                    BindgvSalePlanList();
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
        void UCSalePlanManager_VerifyEvent(object sender, EventArgs e)
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
                        //获取销售计划单状态(已审核)
                        dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.AUDIT).ToString());
                        dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.AUDIT, true));
                    }
                    else if (UcVerifyStatus == DataSources.EnumAuditStatus.NOTAUDIT)
                    {
                        //获取销售计划单状态(审核不通过)
                        dic.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString());
                        dic.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.NOTAUDIT, true));
                    }
                    dic.Add("update_by", GlobalStaticObj.UserID);//修改人Id
                    dic.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                    dic.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间  
                    dic.Add("sale_plan_id", listField[i]);
                    sysStringSql.sqlString = @"update tb_parts_sale_plan set 
                                               order_status=@order_status,order_status_name=@order_status_name,
                                               update_by=@update_by,update_name=@update_name,update_time=@update_time 
                                               where sale_plan_id=@sale_plan_id";
                    sysStringSql.Param = dic;
                    list_sql.Add(sysStringSql);
                }
                if (DBHelper.BatchExeSQLStringMultiByTrans("销售计划单审核操作", list_sql))
                {
                    BindgvSalePlanList();
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
        void UCSalePlanManager_SubmitEvent(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确认要提交选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }
                List<SysSQLString> listSql = new List<SysSQLString>();
                string strReceId = string.Empty;//单据Id值           
                foreach (DataGridViewRow dr in gvSalePlanList.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        strReceId += dr.Cells["sale_plan_id"].Value.ToString() + ",";
                        SysSQLString obj = new SysSQLString();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, string> dicParam = new Dictionary<string, string>();

                        string order_num = string.Empty;
                        if (dr.Cells["colorder_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString())//草稿状态
                        {
                            if (dr.Cells["colorder_num"].Value != null && dr.Cells["colorder_num"].Value.ToString().Length > 0)
                            {
                                order_num = dr.Cells["colorder_num"].Value.ToString();
                            }
                            else
                            {
                                order_num = CommonUtility.GetNewNo(DataSources.EnumProjectType.SalePlan);
                            }
                        }
                        else if (dr.Cells["colorder_status"].Value.ToString() == Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString())//审核未通过
                        {
                            order_num = dr.Cells["colorder_num"].Value.ToString();
                        }

                        dicParam.Add("order_num", order_num);//单据编号
                        dicParam.Add("sale_plan_id", dr.Cells["sale_plan_id"].Value.ToString());//单据ID
                        dicParam.Add("order_status", Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString());//单据状态ID
                        dicParam.Add("order_status_name", DataSources.GetDescription(DataSources.EnumAuditStatus.SUBMIT, true));//单据状态名称
                        dicParam.Add("update_by", GlobalStaticObj.UserID);//修改人ID
                        dicParam.Add("update_name", GlobalStaticObj.UserName);//修改人姓名
                        dicParam.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());//修改时间
                        obj.sqlString = "update tb_parts_sale_plan set order_num=@order_num,order_status=@order_status,order_status_name=@order_status_name,update_by=@update_by,update_name=@update_name,update_time=@update_time where sale_plan_id=@sale_plan_id";
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
                    BindgvSalePlanList();
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
                UCSalePlanView UCSalePlanView = new UCSalePlanView(sale_plan_id, this);
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
            if (fieldNmae.Equals("order_status_name"))
            {
                string num = gvSalePlanList.Rows[e.RowIndex].Cells["colorder_status"].Value.ToString();
                num = string.IsNullOrEmpty(num) ? "0" : num;
                if (num == "3")
                { gvSalePlanList.Rows[e.RowIndex].Cells["colorder_status_name"].Style.ForeColor = Color.Red; }
                else
                { gvSalePlanList.Rows[e.RowIndex].Cells["colorder_status_name"].Style.ForeColor = Color.Black; }
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
        /// 获取gvSalePlanList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvSalePlanList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["sale_plan_id"].Value.ToString());
                }
            }
            return listField;
        }
        /// <summary>
        /// 在编辑和删除时，获取gvSalePlanList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecordByEditDelete(ref bool IsHandle)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvSalePlanList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    string import_status = dr.Cells["import_status"].Value.ToString();
                    if (import_status == "0")
                    { listField.Add(dr.Cells["sale_plan_id"].Value.ToString()); }
                    else if (import_status == "1")
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
        /// 获取gvSalePlanList列表选中要审核的记录
        /// 只有工单状态是已提交的才可以被审核
        /// </summary>
        /// <returns></returns>
        private List<string> GetVerifyRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvSalePlanList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    //获取已提交/审核未通过的状态的编号
                    string order_status_SUBMIT = Convert.ToInt32(DataSources.EnumAuditStatus.SUBMIT).ToString();
                    string order_status_NOTAUDIT = Convert.ToInt32(DataSources.EnumAuditStatus.NOTAUDIT).ToString();
                    string colorder_status = dr.Cells["colorder_status"].Value.ToString();
                    if (order_status_SUBMIT == colorder_status || order_status_NOTAUDIT == colorder_status)
                    {
                        listField.Add(dr.Cells["sale_plan_id"].Value.ToString());
                    }
                }
            }
            return listField;
        }
        /// <summary>
        /// 加载销售计划单列表信息
        /// </summary>
        public void BindgvSalePlanList()
        {
            try
            {
                int RecordCount = 0;
                DataTable gvPurchasePlanOrder_dt = DBHelper.GetTableByPage("查询销售计划单列表信息", "tb_parts_sale_plan", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                gvSalePlanList.DataSource = gvPurchasePlanOrder_dt;
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
        private void gvSalePlanList_HeadCheckChanged()
        {
            SetSelectedStatus();
        }
        /// <summary> 选择复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSalePlanList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvSalePlanList.CurrentCell == null)
            {
                return;
            }
            //点击选择框
            if (gvSalePlanList.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
        /// <summary> 单击一行，选择或取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSalePlanList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            SetSelectedStatus();
        }
        /// <summary> 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in gvSalePlanList.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[colorder_status.Name].Value.ToString());
                    listIDs.Add(dgvr.Cells[sale_plan_id.Name].Value.ToString());
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
