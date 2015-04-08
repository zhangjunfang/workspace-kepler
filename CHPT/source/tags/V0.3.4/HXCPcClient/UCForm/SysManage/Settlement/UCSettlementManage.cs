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
            this.dgvRecord.ReadOnly = false;

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
                if (this.cmbJSFS.SelectedValue!=null
                    &&this.cmbJSFS.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and balance_way_name like '%{0}%'", this.cmbJSFS.SelectedValue);
                }
                string status = CommonCtrl.IsNullToString(cboStatus.SelectedValue);//状态
                if (CommonCtrl.IsNullToString(cboStatus.SelectedValue).Length > 0)
                {
                    where+=string.Format(" and status='{0}'", cboStatus.SelectedValue);
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
            List<SQLObj> listSql = new List<SQLObj>();
            string opName = "修改结算方式状态";
            string msg = msg = DataSources.GetDescription(typeof(DataSources.EnumStatus), this.status);
            if (this.status == DataSources.EnumStatus.Start.ToString("d"))//启用
            {
                StatusSql(listSql, ti_list, this.status);//停用的ti_list改为启用                
            }
            else//停用
            {
                StatusSql(listSql, qi_list, this.status);//启用的qi_list 改为停用               
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
                    dicParam.Add("balance_way_id", new ParamObj("balance_way_id", id, SysDbType.VarChar, 40));//ID
                    dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
                    dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
                    sqlObj.sqlString = @"update [tb_balance_way] set status=@status,update_by=@update_by,update_time=@update_time where balance_way_id=@balance_way_id;";
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
                    if (row.Cells[this.colStatus.Name].Value.ToString() == DataSources.EnumEnableFlag.USING.ToString("d")) //表格中是启用
                    {
                        this.qi_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                    else//表格中是停用
                    {
                        this.ti_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                }
            }
            btnStatus(this.qi_list, this.ti_list);
        }
        /// <summary>
        /// 启用按钮状态设置
        /// </summary>
        /// <param name="qi_list">列表中选中的是启用的 id集合</param>
        /// <param name="ti_list">列表中选中的是停用的 id集合</param>
        private void btnStatus(ArrayList qi_list, ArrayList ti_list)
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
        /// <summary>
        /// 点击复选框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.qi_list.Clear();
                this.ti_list.Clear();
                this.status = string.Empty;

                foreach (DataGridViewRow row in this.dgvRecord.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
                    {
                        if (row.Cells[this.colStatus.Name].Value.ToString() == DataSources.EnumStatus.Start.ToString("d")) //表格中是启用
                        {
                            this.qi_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                        }
                        else//表格中是停用
                        {
                            this.ti_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                        }
                    }
                }
                btnStatus(this.qi_list, this.ti_list);
            }
        }
        private void dgvRecord_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
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
    }
}
