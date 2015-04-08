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
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
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
            Add.windowStatus = WindowStatus.Add;
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
            Edit.windowStatus = WindowStatus.Edit;
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
            //this.dgvRecord.ReadOnly = false;

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
                        long strETime = Convert.ToInt64(CommonCtrl.IsNullToString(dgvRecord.Rows[e.RowIndex].Cells["valid_until"].Value));
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
            if (listStart.Count == 0 && listStop.Count == 0)
            {
                return;
            }
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }

            if (StatusSql())
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                BindPageData();
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        /// <summary>
        /// 执行启用停用
        /// </summary>
        /// <returns>是否成功</returns>
        private bool StatusSql()
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = "update sys_b_set_repair_package_set set status=@status where repair_package_set_id in ({0})";
            string ids = string.Empty;
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
                foreach (string id in listStart)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');

            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                foreach (string id in listStop)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "维修套餐", listSql);
        }

        /// <summary>
        /// 全选复选框事件 
        /// </summary>
        private void dgvRecord_HeadCheckChanged()
        {
           
            SetSelectedStatus();
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

        #region 按钮状态
        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            btnCopy.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnStatus.Enabled = false;
            //已选择状态列表
            List<string> listFiles = new List<string>();
            //记录选中数据状态
            RecordData(listFiles);

            SetStatus();

            SetMultiBtnStatus(listFiles);


        }

        /// <summary>
        /// 设置多选时按钮状态
        /// </summary>
        /// <param name="listFiles">选中的记录</param>
        private void SetMultiBtnStatus(List<string> listFiles)
        {
            if (listFiles.Count > 1)
            {
                btnCopy.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// 记录选中数据
        /// </summary>
        /// <param name="listFiles">数据状态的表</param>
        private void RecordData(List<string> listFiles)
        {
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();

            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
                {

                    //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string id = dgvr.Cells[columnId.Name].Value.ToString();
                    listIDs.Add(id);
                    listFiles.Add(dgvr.Cells[columnStatus.Name].Value.ToString());
                    if (dgvr.Cells[columnStatus.Name].Value == null)
                    {

                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[columnStatus.Name].Value);//状态

                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(id);
                    }
                }
            }
        }

        /// <summary>
        /// 设置启用停用
        /// </summary>
        private void SetStatus()
        {
            if (listStart.Count > 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count == 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count > 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "停用";
                enumStatus = DataSources.EnumStatus.Start;
            }
            else if (listStart.Count == 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "启用";
                enumStatus = DataSources.EnumStatus.Stop;
            }
        }

        #endregion
    }
}
