using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Collections;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.BankAccount
{
    /// <summary>
    /// 银行账户设置
    /// </summary>
    public partial class UCBankAccountManage : UCBase
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
        public UCBankAccountManage()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(this.btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvBank);   //设置数据表格样式,并将最后一列填充其余空白

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            this.AddEvent += new ClickHandler(UCBankAccountManage_AddEvent);
            this.EditEvent += new ClickHandler(UCBankAccountManage_EditEvent);
            this.CopyEvent += new ClickHandler(UCBankAccountManage_CopyEvent);
            this.ViewEvent += new ClickHandler(UCBankAccountManage_ViewEvent);
            this.DeleteEvent += new ClickHandler(UCBankAccountManage_DeleteEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);

            dgvBank.ReadOnly = false;
            foreach (DataGridViewColumn dgvc in dgvBank.Columns)
            {
                if (dgvc.Name == colCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }
        #endregion

        #region --窗体初始化
        private void UCBankAccountManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            this.btnStatus.Enabled = false;
            //绑定状态
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.colStatus, typeof(DataSources.EnumStatus));

            this.BindData();
        }
        #endregion

        #region --操作事件
        void UCBankAccountManage_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }

        void UCBankAccountManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }

        void UCBankAccountManage_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }

        void UCBankAccountManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        void UCBankAccountManage_AddEvent(object sender, EventArgs e)
        {
            AddData();
        }
        #endregion

        #region --私有方法
        void AddData()
        {
            UCBankAccountAddOrEdit add = new UCBankAccountAddOrEdit(WindowStatus.Add, null, this);
            this.addUserControl(add,"银行账户-新建", "UCBankAccountAdd", this.Tag.ToString(), this.Name);
        }

        void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCBankAccountEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCBankAccountCopy";
            }

            if (dgvBank.CurrentRow == null)
            {
                MessageBoxEx.Show(string.Format("请选择要{0}的数据!", title));
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            UCBankAccountAddOrEdit add = new UCBankAccountAddOrEdit(status, id, this);
            base.addUserControl(add, string.Format("银行账户-{0}", title), menuId + id, this.Tag.ToString(), this.Name);
        }

        void ViewData()
        {
            if (dgvBank.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择要预览的数据!");
                return;
            }
            string id = ID;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            UCBankAccountAddOrEdit view = new UCBankAccountAddOrEdit(WindowStatus.View, id, this);
            base.addUserControl(view, "银行帐户-预览", "UCBankAccountView" + id, this.Tag.ToString(), this.Name);
        }

        void DeleteData()
        {
            dgvBank.EndEdit();
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dgvr in dgvBank.Rows)
            {
                object isCheck = dgvr.Cells[colCheck.Name].Value;
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
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

            if (DBHelper.BatchUpdateDataByIn("批量删除银行账户", "tb_bank_account", dicFileds, "bank_account_id", listField.ToArray()))
            {
                MessageBoxEx.Show("删除成功！");
                this.BindData();
            }
            else
            {
                MessageBoxEx.Show("删除失败！");
            }
        }
        #endregion

        #region --主键ID
        /// <summary>
        /// 银行账户ID
        /// </summary>
        private string ID
        {
            get
            {
                if (dgvBank.CurrentRow == null)
                {
                    return string.Empty;
                }
                object id = dgvBank.CurrentRow.Cells[this.columnId.Name].Value;
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

        #region --清除查询条件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBankAccount.Caption = string.Empty;
            txtBankName.Caption = string.Empty;
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);
        }
        #endregion

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                StringBuilder where = new StringBuilder();
                where.AppendFormat("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d"));
                string bankAccount = txtBankAccount.Caption.Trim();//银行账户
                if (bankAccount.Length > 0)
                {
                    where.AppendFormat(" and bank_account like '%{0}%'", bankAccount);
                }
                string bankName = txtBankName.Caption.Trim();//银行名称
                if (bankName.Length > 0)
                {
                    where.AppendFormat(" and bank_name like '%{0}%'", bankName);
                }
                string status = CommonCtrl.IsNullToString(cboStatus.SelectedValue);//状态
                if (status.Length > 0)
                {
                    where.AppendFormat(" and status='{0}'", status);
                }              

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindData(object obj)
        {
             DataTable dt = DBHelper.GetTable("", "tb_bank_account", "*", obj.ToString(), "", "order by create_time");
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvBank.DataSource = obj;

            this.myLock = true;
        }
        public void BindData(string id)
        {
            this.BindData();
            foreach (DataGridViewRow dgvr in dgvBank.Rows)
            {
                if (CommonCtrl.IsNullToString(dgvr.Cells[this.columnId.Name].Value) == id)
                {
                    dgvBank.CurrentCell = dgvr.Cells[colBankName.Name];
                    break;
                }
            }
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
            if (listSql.Count == 0)
            {
                return;
            }
            if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
            {
                foreach (string id in this.qi_list)
                {
                    foreach (DataGridViewRow row in this.dgvBank.Rows)
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
                    foreach (DataGridViewRow row in this.dgvBank.Rows)
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
                    dicParam.Add("bank_account_id", new ParamObj("bank_account_id", id, SysDbType.VarChar, 40));//ID
                    dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
                    dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
                    sqlObj.sqlString = @"update [tb_bank_account] set status=@status,update_by=@update_by,update_time=@update_time where bank_account_id=@bank_account_id;";
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

            foreach (DataGridViewRow row in this.dgvBank.Rows)
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

        #region --格式化时间显示
        private void dgvBank_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }           
            string fieldNmae = this.dgvBank.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
        }
        #endregion                
    }
}
