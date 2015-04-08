using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Collections;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.Settlement
{
    /// <summary>
    /// 结算方式设置
    /// </summary>
    public partial class UCSettlementManage : UCBase
    {
        #region --成员变量
        private bool myLock = true;
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #endregion

        #region --构造函数
        public UCSettlementManage()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(this.btnSearch, this.btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白
        }
        #endregion

        #region --窗体初始化
        private void UCSettlementManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏

            //结算方式
            CommonCtrl.CmbBindDict(this.cmbJSFS, "sys_closing_way");
           
            //绑定状态
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);//绑定状态         
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.colStatus, typeof(DataSources.EnumStatus));

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            this.AddEvent += new ClickHandler(UCSettlementManage_AddEvent);
            this.EditEvent += new ClickHandler(UCSettlementManage_EditEvent);
            this.CopyEvent += new ClickHandler(UCSettlementManage_CopyEvent);
            this.ViewEvent += new ClickHandler(UCSettlementManage_ViewEvent);
            this.DeleteEvent += new ClickHandler(UCSettlementManage_DeleteEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);
            base.SetContentMenuScrip(dgvRecord);
            this.BindData();
        }
        #endregion

        #region --操作事件
        void UCSettlementManage_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }

        void UCSettlementManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }

        void UCSettlementManage_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }

        void UCSettlementManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        void UCSettlementManage_AddEvent(object sender, EventArgs e)
        {
            UCSettlementAddOrEdit uc = new UCSettlementAddOrEdit(WindowStatus.Add, null, this);
            uc.RefreshDataStart -= new UCSettlementAddOrEdit.RefreshData(this.BindData);
            uc.RefreshDataStart += new UCSettlementAddOrEdit.RefreshData(this.BindData);
            this.addUserControl(uc, "结算方式-新建", "UCSettlementAdd", this.Tag.ToString(), this.Name);
        }
        #endregion

        #region --私有操作方法
        void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCSettlementEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCSettlementCopy";
            }

            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show(string.Format("请选择要{0}的数据!", title));
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            UCSettlementAddOrEdit uc = new UCSettlementAddOrEdit(status, id, this);
            uc.RefreshDataStart -= new UCSettlementAddOrEdit.RefreshData(this.BindData);
            uc.RefreshDataStart += new UCSettlementAddOrEdit.RefreshData(this.BindData);
            base.addUserControl(uc, string.Format("结算方式-{0}", title), menuId + id, this.Tag.ToString(), this.Name);
        }

        void ViewData()
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择要预览的数据!");
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            UCSettlementAddOrEdit view = new UCSettlementAddOrEdit(WindowStatus.View, id, this);
            base.addUserControl(view, "结算方式-预览", "UCSettlementView" + id, this.Tag.ToString(), this.Name);
        }

        void DeleteData()
        {
            dgvRecord.EndEdit();
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
            {
                object isCheck = dgvr.Cells[this.colCheck.Name].Value;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dgvr.Cells[this.columnId.Name].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择要删除的数据!");
                return;
            }
            if (MessageBoxEx.Show("是否要删除当前选中的数据？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());
            if (DBHelper.BatchUpdateDataByIn("批量删除结算方式", "tb_balance_way", dicFileds, "balance_way_id", listField.ToArray()))
            {
                MessageBoxEx.Show("删除成功！");
                BindData();
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }

        /// <summary>
        /// 结算方式ID
        /// </summary>
        private string ID
        {
            get
            {
                if (dgvRecord.CurrentRow == null)
                {
                    return string.Empty;
                }
                object id = dgvRecord.CurrentRow.Cells[this.columnId.Name].Value;
                if (id == null)
                {
                    return string.Empty;
                }
                else
                {
                    return id.ToString();
                }
            }
        }
        #endregion

        #region --清除选择条件
        //清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            CommonCtrl.CmbBindDict(this.cmbJSFS, "sys_closing_way");
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);//绑定状态
        }
        #endregion

        #region --查询数据
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        private void BindData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));

                //结算方式
                if (this.cmbJSFS.SelectedValue != null
                    && this.cmbJSFS.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and balance_way_name like '%{0}%'", this.cmbJSFS.Text);
                }
                string status = CommonCtrl.IsNullToString(cboStatus.SelectedValue);//状态
                if (CommonCtrl.IsNullToString(cboStatus.SelectedValue).Length > 0)
                {
                    where += string.Format(" and status='{0}'", cboStatus.SelectedValue);
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTable("", "v_balance_way", "*", obj.ToString(), "", "order by create_time desc");

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            dgvRecord.DataSource = obj;
            this.myLock = true;
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
                BindData();
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
            string strSql = "update tb_balance_way set status=@status where balance_way_id in ({0})";
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
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "结算方式", listSql);
        }

        /// <summary>
        /// 全选复选框事件 
        /// </summary>
        private void dgvRecord_HeadCheckChanged()
        {
            SetSelectedStatus();

        }

        private void dgvRecord_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (e.ColumnIndex != 0)
            //{
            //    e.Cancel = true;
            //}
        }
        #endregion

        #region --格式化时间显示
        private void dgvRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = this.dgvRecord.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = long.Parse(e.Value.ToString());
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
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
                    listFiles.Add(dgvr.Cells[colStatus.Name].Value.ToString());
                    if (dgvr.Cells[colStatus.Name].Value == null)
                    {

                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[colStatus.Name].Value);//状态

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
