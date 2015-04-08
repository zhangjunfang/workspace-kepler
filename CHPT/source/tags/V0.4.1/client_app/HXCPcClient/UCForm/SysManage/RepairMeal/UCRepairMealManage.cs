using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using System.Collections;

namespace HXCPcClient.UCForm.SysManage.RepairMeal
{
    /// <summary>
    /// 系统管理—维修套餐设置管理
    /// Author：JC
    /// AddTime：2014.12.01
    /// </summary>
    public partial class UCRepairMealManage : UCBase
    {
        #region --成员变量
        /// <summary>
        /// 查询条件
        /// </summary>
        string strWhere = string.Empty;
        /// <summary>
        /// 维修套餐Id
        /// </summary>
        string strMId = string.Empty;
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus _eStatus;
        /// <summary>
        /// 列表中选中的是启用的 id集合
        /// </summary>
        ArrayList qi_list = new ArrayList();
        /// <summary>
        /// 列表中选中的是停用的 id集合
        /// </summary>
        ArrayList ti_list = new ArrayList();
        private string status = string.Empty;
        #endregion

        #region --构造函数
        public UCRepairMealManage()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白

            base.EditEvent += new ClickHandler(UCRepairMealManage_EditEvent);
            base.DeleteEvent += new ClickHandler(UCRepairMealManage_DeleteEvent);
            base.ViewEvent += new ClickHandler(UCRepairMealManage_ViewEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);
            base.AddEvent += new ClickHandler(UCRepairMealManage_AddEvent);
        }

      
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnSave.Visible = false;
            base.btnCopy.Visible = false;
            base.btnCancel.Visible = false;
            base.btnImport.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;           
            base.btnActivation.Visible = false;
            base.btnBalance.Visible = false;
            base.btnSubmit.Visible = false;           
            base.btnVerify.Visible = false;
            base.btnSet.Visible = false;
            base.btnPrint.Visible = false;
            base.btnRevoke.Visible = false;
            base.btnExport.Visible = false;
            
        }
        #endregion

        #region 新增事件
        void UCRepairMealManage_AddEvent(object sender, EventArgs e)
        {
            UCRepairMealAddOrEdit Add = new UCRepairMealAddOrEdit();
            Add.uc = this;
            Add.wStatus = WindowStatus.Add;
            base.addUserControl(Add, "维修套餐设置-新增", "UCRepairMealAddOrEdit", this.Tag.ToString(), this.Name);
        }
        #endregion      

        #region 预览事件
        void UCRepairMealManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }
        #endregion

        #region 删除事件
        void UCRepairMealManage_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvRecord.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells[this.columnId.Name].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
                comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id            
                comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
                bool flag = DBHelper.BatchUpdateDataByIn("批量删维修设置", "sys_b_set_repair_package_set", comField, "repair_package_set_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvRecord.Rows.Count > 0)
                    {
                        dgvRecord.CurrentCell = dgvRecord.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑事件
        void UCRepairMealManage_EditEvent(object sender, EventArgs e)
        {
            if (!IsCheck("编辑"))
            {
                return;
            }
            UCRepairMealAddOrEdit Edit = new UCRepairMealAddOrEdit();
            Edit.uc = this;
            Edit.wStatus = WindowStatus.Edit;
            Edit.strId = strMId;  //维修套餐设置ID
            base.addUserControl(Edit, "维修套餐设置-编辑", "UCRepairMealAddOrEdit" + Edit.strId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region 预览数据
        /// <summary>
        /// 预览数据
        /// </summary>
        /// <param name="strType">操作类型，0为预览，1为双击单元格</param>
        private void ViewData(string strType = "0")
        {
            if (strType == "0")
            {
                if (!IsCheck("预览"))
                {
                    return;
                }
            }
            UCRepairMealView view = new UCRepairMealView(strMId);
            view.uc = this;
            base.addUserControl(view, "维修套餐设置-预览", "UCRepairMealView" + strMId, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region  编辑、预览数据验证
        /// <summary>
        /// 编辑、预览数据验证
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private bool IsCheck(string strMessage)
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells[this.columnId.Name].Value.ToString());
                    strMId = dr.Cells[this.columnId.Name].Value.ToString();
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择" + strMessage + "记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能" + strMessage + "1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 窗体Load事件
        private void UCRepairMealManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            this.btnStatus.Enabled = false;
            this.dgvRecord.ReadOnly = false;

            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now.AddMonths(3);

            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));

            foreach (DataGridViewColumn dgvc in dgvRecord.Columns)
            {
                if (dgvc == colCheck)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }

            BindPageData();
        }
        #endregion

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {           
            txtMealCode.Caption = string.Empty;
            txtMealName.Caption = string.Empty;
            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;
        }
        #endregion

        #region 查询功能
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 分页查询绑定数据
        /// <summary>
        /// 分页查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                #region 事件选择判断
                if (dtpSTime.Value > dtpETime.Value)
                {
                    MessageBoxEx.Show("有效日期,开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                strWhere = string.Format(" enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "' ");//enable_flag 1未删除
                if (!string.IsNullOrEmpty(txtMealCode.Caption.Trim()))//套餐编码
                {
                    strWhere += string.Format(" and  package_code like '%{0}%'", txtMealCode.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtMealName.Caption.Trim()))//套餐名称
                {
                    strWhere += string.Format(" and  package_name like '%{0}%'", txtMealName.Caption.Trim());
                }               
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(cbbstatus.SelectedValue)))//状态
                {
                    strWhere += string.Format(" and status = '{0}'", cbbstatus.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpSTime.Value)))//有效期起
                {
                    strWhere += string.Format(" and period_validity >= '{0}'", Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date));
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpETime.Value)))//有效期至
                {
                    strWhere += string.Format(" and valid_until <= '{0}'", Common.LocalDateTimeToUtcLong(dtpETime.Value.Date.AddDays(1).AddMilliseconds(-1)));
                }
                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询维修维修套餐管理", "sys_b_set_repair_package_set", "*", strWhere, "", " order by repair_package_set_id desc", page.PageIndex, page.PageSize, out recordCount);
                dgvRecord.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }

        #endregion

        #region 重写有效日期、状态
        private void dgvRData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRecord.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("period_validity"))
            {
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(e.Value)))
                {
                    if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvRecord.Rows[e.RowIndex].Cells["valid_until"].Value)))
                    {
                        long strETime=Convert.ToInt64(CommonCtrl.IsNullToString(dgvRecord.Rows[e.RowIndex].Cells["valid_until"].Value));                       
                        long ticks = (long)e.Value;
                        e.Value = Common.UtcLongToLocalDateTime(ticks) + "-" + Common.UtcLongToLocalDateTime(strETime);
                    }
                }
            }           
        }
        #endregion        

        #region 双击单元格进入详情页面
        private void dgvRData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                return;
            }
            strMId = dgvRecord.CurrentRow.Cells[this.columnId.Name].Value.ToString();
            ViewData("1");
        }
        #endregion

        #region --启用停用
        //启动或者停止
        private void UC_StatusEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            string opName = "修改客户档案状态";
            string msg = "";
            if (this.status == DataSources.EnumStatus.Start.ToString("d"))//启用
            {
                StatusSql(listSql, ti_list, this.status);//停用的ti_list改为启用
                msg = "启用";
            }
            else//停用
            {
                StatusSql(listSql, qi_list, this.status);//启用的qi_list 改为停用
                msg = "停用";
            }
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                foreach (string id in this.qi_list)
                {
                    foreach (DataGridViewRow row in this.dgvRecord.Rows)
                    {
                        if (row.Cells[this.columnId.Name].Value.ToString() == id)
                        {
                            DataRow dr = (row.DataBoundItem as DataRowView).Row;
                            dr["status"] = DataSources.EnumStatus.Stop.ToString("d");
                        }
                    }
                }
                foreach (string id in this.ti_list)
                {
                    foreach (DataGridViewRow row in this.dgvRecord.Rows)
                    {
                        if (row.Cells[this.columnId.Name].Value.ToString() == id)
                        {
                            DataRow dr = (row.DataBoundItem as DataRowView).Row;
                            dr["status"] = DataSources.EnumStatus.Start.ToString("d");
                        }
                    }
                }
                base.btnStatus.Enabled = false;
                MessageBoxEx.Show(msg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBoxEx.Show(msg + "失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void StatusSql(List<SQLObj> listSql, ArrayList idList, string status)
        {
            if (idList.Count > 0)
            {
                foreach (string id in idList)
                {
                    SQLObj sqlObj = new SQLObj();
                    sqlObj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();//参数
                    dicParam.Add("com_id", new ParamObj("com_id", id, SysDbType.VarChar, 40));//ID
                    dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
                    dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
                    sqlObj.sqlString = @"update [tb_company] set status=@status,update_by=@update_by,update_time=@update_time where com_id=@com_id;";
                    sqlObj.Param = dicParam;
                    listSql.Add(sqlObj);
                }
            }
        }
        /// <summary>
        /// 全选复选框事件 
        /// </summary>
        private void dgvRecord_HeadCheckChanged()
        {
            this.qi_list.Clear();
            this.ti_list.Clear();
            this.status = string.Empty;

            foreach (DataGridViewRow row in this.dgvRecord.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    if (row.Cells[this.columnStatus.Name].Value.ToString() == DataSources.EnumStatus.Start.ToString("d")) //表格中是启用
                    {
                        this.qi_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                    else//表格中是停用
                    {
                        this.ti_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                }
            }
            SetBtnStatus(this.qi_list, this.ti_list);
        }
        /// <summary>
        /// 启用按钮状态设置
        /// </summary>
        /// <param name="qi_list">列表中选中的是启用的 id集合</param>
        /// <param name="ti_list">列表中选中的是停用的 id集合</param>
        private void SetBtnStatus(ArrayList qi_list, ArrayList ti_list)
        {
            if (qi_list.Count > 0 && ti_list.Count > 0)
            {
                base.btnStatus.Enabled = false;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
            }
            else if (qi_list.Count > 0 && ti_list.Count == 0)
            {
                base.btnStatus.Enabled = true;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
                this.status = DataSources.EnumStatus.Stop.ToString("d");
            }
            else if (qi_list.Count == 0 && ti_list.Count > 0)
            {
                base.btnStatus.Enabled = true;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
                this.status = DataSources.EnumStatus.Start.ToString("d");
            }
            else if (qi_list.Count == 0 && ti_list.Count == 0)
            {
                base.btnStatus.Enabled = false;
                base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
            }
        }        
        #endregion        

        #region 选择行
        private void dgvRecord_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvRecord.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion      
    }
}
