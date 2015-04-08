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

        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        #endregion

        #region 构造和载入
        public UCCashierAccManage()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(this.btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            //DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白

            this.AddEvent += new ClickHandler(UCCashierAccManage_AddEvent);
            this.EditEvent += new ClickHandler(UCCashierAccManage_EditEvent);
            this.CopyEvent += new ClickHandler(UCCashierAccManage_CopyEvent);
            this.ViewEvent += new ClickHandler(UCCashierAccManage_ViewEvent);
            this.DeleteEvent += new ClickHandler(UCCashierAccManage_DeleteEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);
        }

        private void UCCashierAccManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            base.btnStatus.Visible = true;
            this.btnStatus.Enabled = false;
            //this.dgvRecord.ReadOnly = false;

            foreach (DataGridViewColumn dgvc in dgvRecord.Columns)
            {
                if (dgvc.Name == colCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }

            DataSources.BindComBoxDataEnum(cboAccountType, typeof(DataSources.EnumAccountType), true);
            //DataSources.BindComDataGridViewBoxColumnDataEnum(this.colAccountType, typeof(DataSources.EnumAccountType));

            DataSources.BindComBoxDataEnum(cboStatus, typeof(DataSources.EnumStatus), true);//绑定状态
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));
            base.SetContentMenuScrip(dgvRecord);
            this.BindData();
        }
        #endregion

        #region 事件方法
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
        #endregion

        #region 查询清除

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

        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
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

        /// <summary>
        /// 绑定数据并选定指定数据
        /// </summary>
        /// <param name="id">数据ID</param>
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
            string strSql = "update tb_cashier_account set status=@status where cashier_account in ({0})";
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
            else if (fieldNmae.Equals("account_type"))
            {
                e.Value = DataSources.GetDescription(typeof(DataSources.EnumAccountType), e.Value.ToString().Trim());
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
