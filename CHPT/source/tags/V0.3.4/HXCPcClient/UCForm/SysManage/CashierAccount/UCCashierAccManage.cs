using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using System.Collections;
using Utility.Common;

namespace HXCPcClient.UCForm.SysManage.CashierAccount
{
    /// <summary>
    /// 出纳账户设置
    /// </summary>
    public partial class UCCashierAccManage : UCBase
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

        public UCCashierAccManage()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(this.btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白

            this.AddEvent += new ClickHandler(UCCashierAccManage_AddEvent);
            this.EditEvent += new ClickHandler(UCCashierAccManage_EditEvent);
            this.CopyEvent += new ClickHandler(UCCashierAccManage_CopyEvent);
            this.ViewEvent += new ClickHandler(UCCashierAccManage_ViewEvent);
            this.DeleteEvent += new ClickHandler(UCCashierAccManage_DeleteEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);            
        }

        void UCCashierAccManage_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }

        void UCCashierAccManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }

        void UCCashierAccManage_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }

        void UCCashierAccManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        void UCCashierAccManage_AddEvent(object sender, EventArgs e)
        {
            AddData();
        }

        void AddData()
        {
            UCCashierAccountAdd add = new UCCashierAccountAdd(WindowStatus.Add, null, this);
            this.addUserControl(add, "出纳账户-新建", "UCCashierAccountAdd", this.Tag.ToString(), this.Name);
        }

        void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCCashierAccountEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCCashierAccountCopy";
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

            UCCashierAccountAdd add = new UCCashierAccountAdd(status, id, this);
            base.addUserControl(add, string.Format("出纳账户-{0}", title), menuId + id, this.Tag.ToString(), this.Name);
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

            UCCashierAccountAdd view = new UCCashierAccountAdd(WindowStatus.View, id, this);
            base.addUserControl(view, "出纳账户-预览", "UCCashierAccountView" + id, this.Tag.ToString(), this.Name);
        }

        void DeleteData()
        {
            dgvRecord.EndEdit();
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
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
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.DELETED).ToString());
            if (DBHelper.BatchUpdateDataByIn("批量删除出纳账户", "tb_cashier_account", dic, "cashier_account", listField.ToArray()))
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
        /// 出纳账户ID
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

        private void UCCashierAccManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            base.btnStatus.Visible = true;
            dgvRecord.ReadOnly = false;

            foreach (DataGridViewColumn dgvc in dgvRecord.Columns)
            {
                if (dgvc.Name == colCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }

            DataSources.BindComBoxDataEnum(cboAccountType, typeof(DataSources.EnumAccountType), true);
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.colAccountType, typeof(DataSources.EnumAccountType));
            
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);//绑定状态
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));

            this.BindData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAccountName.Caption = string.Empty;
            DataSources.BindComBoxDataEnum(cboAccountType, typeof(DataSources.EnumAccountType), true);
            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);//绑定状态
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        void BindData()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat("enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d"));
            string accountName = txtAccountName.Caption.Trim();//账户名称
            if (accountName.Length > 0)
            {
                sbWhere.AppendFormat(" and account_name like '%{0}%'", accountName);
            }
            string accountType = CommonCtrl.IsNullToString(cboAccountType.SelectedValue);//账户类型
            if (accountType.Length > 0)
            {
                sbWhere.AppendFormat(" and account_type='{0}'", accountType);
            }
            string status = CommonCtrl.IsNullToString(cboStatus.SelectedValue);//状态
            if (status.Length > 0)
            {
                sbWhere.AppendFormat(" and status='{0}'", status);
            }
            DataTable dt = DBHelper.GetTable("", "v_cashier_account", "*", sbWhere.ToString(), "", "order by create_time");
            this.dgvRecord.DataSource = dt;
        }

        public void BindData(string id)
        {
            BindData();
            foreach (DataGridViewRow dgvr in dgvRecord.Rows)
            {
                if (CommonCtrl.IsNullToString(dgvr.Cells[this.columnId.Name].Value) == id)
                {
                    dgvRecord.CurrentCell = dgvr.Cells[colBankName.Name];
                    break;
                }
            }
        }

        #region --启用停用
        //启动或者停止
        private void UC_StatusEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            string opName = "修改出纳账户状态";
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
                    dicParam.Add("cashier_account", new ParamObj("cashier_account", id, SysDbType.VarChar, 40));//ID
                    dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
                    dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
                    sqlObj.sqlString = @"update [tb_cashier_account] set status=@status,update_by=@update_by,update_time=@update_time where cashier_account=@cashier_account;";
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
                btnStatus(this.qi_list, this.ti_list);
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
