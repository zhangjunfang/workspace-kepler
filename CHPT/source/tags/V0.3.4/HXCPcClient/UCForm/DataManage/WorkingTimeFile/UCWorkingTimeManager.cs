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

namespace HXCPcClient.UCForm.DataManage.WorkingTimeFile
{
    public partial class UCWorkingTimeManager : UCBase
    {

        #region 属性
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体方法
        /// </summary>
        public UCWorkingTimeManager()
        {

            InitializeComponent();
            //禁止列表自增列
            gvWorkList.AutoGenerateColumns = false;

            base.AddEvent += new ClickHandler(UCWorkingTimeManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCWorkingTimeManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCWorkingTimeManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCWorkingTimeManager_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCWorkingTimeManager_StatusEvent);
            //dateTimeEnd.Value = DateTime.Now;
            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;

            BindDllInfo();
            BindgvWorkList();
        }


        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCWorkingTimeManager_Load(object sender, EventArgs e)
        {
            gvWorkList.ReadOnly = false;
            base.SetBtnStatus(WindowStatus.View);
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
        }
        #endregion

        #region 方法、函数
        /// <summary>
        /// 清除查询条件控件中的内容
        /// </summary>
        /// <param name="ControlTypeName"></param>
        void ClearControlInfo()
        {
            txtProNo.Caption = string.Empty;
            txtProName.Caption = string.Empty;
            txtWorkTimeA.Caption = string.Empty;

            txtWorkTimeB.Caption = string.Empty;
            txtWorkTimeC.Caption = string.Empty;
            txtQuotaPrice.Caption = string.Empty;

            radIsWorkTime.Checked = false;
            radIsQuota.Checked = false;

            ddlDataSource.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;

            dateTimeStart.Value = DateTime.Now.AddMonths(-1);
            dateTimeEnd.Value = DateTime.Now;


        }
        /// <summary>
        /// 绑定下拉框信息
        /// </summary>
        void BindDllInfo()
        {
            ddlDataSource.Items.Clear();
            ddlState.Items.Clear();

            ddlDataSource.DataSource = DataSources.EnumToList(typeof(DataSources.EnumDataSources), true);
            ddlDataSource.ValueMember = "Value";
            ddlDataSource.DisplayMember = "Text";
            ddlState.DataSource = DataSources.EnumToList(typeof(DataSources.EnumStatus), true);
            ddlState.ValueMember = "Value";
            ddlState.DisplayMember = "Text";
        }
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        string BuildString()
        {
            string Str_Where = " enable_flag != 0 ";
            if (!string.IsNullOrEmpty(ddlDataSource.SelectedValue.ToString()))
            {
                Str_Where += " and data_source='" + ddlDataSource.SelectedValue.ToString() + "'";
            }
            if (!string.IsNullOrEmpty(txtProNo.Caption.Trim()))
            {
                Str_Where += " and project_num='" + txtProNo.Caption.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(txtProName.Caption.Trim()))
            {
                Str_Where += " and project_name like '%" + txtProName.Caption.Trim() + "%'";
            }
            if (radIsWorkTime.Checked)
            {
                Str_Where += " and whours_type=1 ";
                if (!string.IsNullOrEmpty(txtWorkTimeA.Caption.Trim()))
                {
                    Str_Where += " and whours_num_a='" + txtWorkTimeA.Caption.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(txtWorkTimeB.Caption.Trim()))
                {
                    Str_Where += " and whours_num_b='" + txtWorkTimeB.Caption.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(txtWorkTimeC.Caption.Trim()))
                {
                    Str_Where += " and whours_num_c='" + txtWorkTimeC.Caption.Trim() + "'";
                }
            }
            else if (radIsQuota.Checked)
            {
                Str_Where += " and whours_type=2 ";
                if (!string.IsNullOrEmpty(txtQuotaPrice.Caption.Trim()))
                {
                    Str_Where += " and quota_price='" + txtQuotaPrice.Caption.Trim() + "'";
                }
            }
            if (!string.IsNullOrEmpty(ddlState.SelectedValue.ToString()))
            {
                Str_Where += " and status='" + ddlState.SelectedValue.ToString() + "'";
            }

            if (dateTimeStart.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeStart.Value.ToShortDateString() + " 00:00:00");
                Str_Where += " and create_time>=" + Common.LocalDateTimeToUtcLong(dtime);
            }
            if (dateTimeEnd.Value != null)
            {
                DateTime dtime = Convert.ToDateTime(dateTimeEnd.Value.ToShortDateString() + " 23:59:59");
                Str_Where += " and create_time<=" + Common.LocalDateTimeToUtcLong(dtime);
            }

            return Str_Where;
        }
        /// <summary>
        /// 加载工时信息
        /// </summary>
        public void BindgvWorkList()
        {
            try
            {
                int RecordCount = 0;
                //DataTable gvWork_dt = DBHelper.GetTableByPage("查询工时列表信息", "tb_workhours", "*", BuildString(), "", "whours_id ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                DataTable gvWork_dt = DBHelper.GetTableByPage("查询工时列表信息", "v_workhours_users", "*", BuildString(), "", " order by create_time desc ", winFormPager1.PageIndex, winFormPager1.PageSize, out RecordCount);
                foreach (DataRow dr in gvWork_dt.Rows)
                {
                    dr["status"] = DataSources.GetDescription(typeof(DataSources.EnumStatus), dr["status"]);
                }
                gvWorkList.DataSource = gvWork_dt.DefaultView;

                winFormPager1.RecordCount = RecordCount;
            }
            catch (Exception ex)
            {
                //异常日志
            }
        }
        /// <summary>
        /// 获取gvSupplierList列表选中的记录条数
        /// </summary>
        /// <returns></returns>
        private List<string> GetSelectedRecord()
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in gvWorkList.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["whours_id"].Value.ToString());
                }
            }
            return listField;
        }

        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();
            //已选择状态列表
            List<string> listFiles = new List<string>();
            foreach (DataGridViewRow dgvr in gvWorkList.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[data_source.Name].Value.ToString());//数据来源
                    string wt_id = dgvr.Cells[whours_id.Name].Value.ToString();
                    listIDs.Add(wt_id);//数据ID
                    if (dgvr.Cells[status.Name].Tag == null)
                    {
                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[status.Name].Tag);//状态
                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(wt_id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(wt_id);
                    }
                }
            }
            #region 设置启用/停用
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
            #endregion
            //宇通
            string dataSource = ((int)DataSources.EnumDataSources.YUTONG).ToString();
            if (listFiles.Count == 1 && !listFiles.Contains(dataSource))
            {
                base.btnEdit.Enabled = true;
                //tsmiEdit.Enabled = true;
                base.btnCopy.Enabled = true;
                //tsmiCopy.Enabled = true;
                base.btnView.Enabled = true;
                //tmsiView.Enabled = true;
            }
            else
            {
                base.btnEdit.Enabled = false;
                //tsmiEdit.Enabled = false;
                base.btnCopy.Enabled = false;
                //tsmiCopy.Enabled = false;
                if (listFiles.Count == 1)
                {
                    base.btnView.Enabled = true;
                    //tmsiView.Enabled = true;
                }
                else
                {
                    base.btnView.Enabled = false;
                    //tmsiView.Enabled = false;
                }
            }

            //如果选择包含宇通来源，则不能删除
            if (listFiles.Count == 0 || listFiles.Contains(dataSource))
            {
                btnDelete.Enabled = false;
                //tsmiDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                //tsmiDelete.Enabled = true;
            }
        }
        #endregion

        #region 界面按钮事件
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_AddEvent(object sender, EventArgs e)
        {
            UCWorkingTimeAddOrEdit UCWorkingTimeAdd = new UCWorkingTimeAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(UCWorkingTimeAdd, "工时档案-添加", "UCWorkingTimeAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 复制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_CopyEvent(object sender, EventArgs e)
        {
            string whoursId = string.Empty;
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
            whoursId = listField[0].ToString();
            UCWorkingTimeAddOrEdit UCWorkingTimeCopy = new UCWorkingTimeAddOrEdit(WindowStatus.Copy, whoursId, this);
            base.addUserControl(UCWorkingTimeCopy, "工时档案-复制", "UCWorkingTimeCopy" + whoursId + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_EditEvent(object sender, EventArgs e)
        {
            string whoursId = string.Empty;
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
            whoursId = listField[0].ToString();
            UCWorkingTimeAddOrEdit UCWorkingTimeEdit = new UCWorkingTimeAddOrEdit(WindowStatus.Edit, whoursId, this);
            base.addUserControl(UCWorkingTimeEdit, "工时档案-编辑", "UCWorkingTimeEdit" + whoursId + "", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = GetSelectedRecord();
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要复制的数据!");
                return;
            }
            if (MessageBoxEx.Show("确认要删除选中的数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            Dictionary<string, string> workField = new Dictionary<string, string>();
            workField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除工时档案表", "tb_workhours", workField, "whours_id", listField.ToArray());
            if (flag)
            {
                BindgvWorkList();
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_ExportEvent(object sender, EventArgs e)
        { }
        /// <summary>
        /// 预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCWorkingTimeManager_ViewEvent(object sender, EventArgs e)
        { }
        /// <summary>
        /// 清除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControlInfo();
        }
        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindgvWorkList();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winFormPager1_PageIndexChanged(object sender, EventArgs e)
        {
            BindgvWorkList();
        }
        /// <summary>
        /// 双击列表单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                string suppId = Convert.ToString(this.gvWorkList.CurrentRow.Cells[0].Value.ToString());
                UCWorkingTimeView UCWorkView = new UCWorkingTimeView(suppId);
                base.addUserControl(UCWorkView, "工时档案-查看", "UCWorkView" + suppId + "", this.Tag.ToString(), this.Name);
            }
        }

        //启用停用方法
        void UCWorkingTimeManager_StatusEvent(object sender, EventArgs e)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                if (!MessageBoxEx.ShowQuestion("确定要停用吗？"))
                {
                    btnStatus.Caption = "启用";
                    return;
                }
                dicStatus.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
                if (DBHelper.BatchUpdateDataByIn("停用车型", "tb_vehicle_models", dicStatus, "vm_id", listStart.ToArray()))
                {
                    MessageBoxEx.ShowInformation("停用成功！");
                    BindgvWorkList();
                }
                else
                {
                    btnStatus.Caption = "启用";
                    MessageBoxEx.ShowError("停用失败！");
                }
            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                if (!MessageBoxEx.ShowQuestion("确定要启用吗？"))
                {
                    btnStatus.Caption = "停用";
                    return;
                }
                dicStatus.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                if (DBHelper.BatchUpdateDataByIn("启用车型", "tb_vehicle_models", dicStatus, "vm_id", listStop.ToArray()))
                {
                    MessageBoxEx.ShowInformation("启用成功！");
                    BindgvWorkList();
                }
                else
                {
                    btnStatus.Caption = "停用";
                    MessageBoxEx.ShowError("启用失败！");
                }
            }

        }
        #endregion

        private void radIsWorkTime_CheckedChanged(object sender, EventArgs e)
        {
            txtWorkTimeA.Enabled = true;
            txtWorkTimeB.Enabled = true;
            txtWorkTimeC.Enabled = true;
            txtQuotaPrice.Enabled = false;
        }

        private void radIsQuota_CheckedChanged(object sender, EventArgs e)
        {
            txtWorkTimeA.Enabled = false;
            txtWorkTimeB.Enabled = false;
            txtWorkTimeC.Enabled = false;
            txtQuotaPrice.Enabled = true;
        }

        private void gvWorkList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.Value == string.Empty)
            {
                return;
            }
            string fieldNmae = gvWorkList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("data_source"))
            {
                DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
                //e.Value = enumDataSources.ToString();
                e.Value = DataSources.GetDescription(enumDataSources, true);
            }
            if (fieldNmae.Equals("status"))
            {
                //DataSources.EnumStatus enumDataSources = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                //e.Value = enumDataSources.ToString();
                //e.Value = DataSources.GetDescription(enumDataSources, true);
            }
        }

        private void gvWorkList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvWorkList.CurrentCell == null)
            {
                return;
            }
            if (gvWorkList.CurrentCell.OwningColumn.Name == colCheck.Name)
            {
                SetSelectedStatus();
            }
        }
    }
}
