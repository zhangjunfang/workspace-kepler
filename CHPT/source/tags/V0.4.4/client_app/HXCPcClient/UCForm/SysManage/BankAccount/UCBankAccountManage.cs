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
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
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

            //dgvBank.ReadOnly = false;
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
            base.SetContentMenuScrip(dgvBank);
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
            this.addUserControl(add, "银行账户-新建", "UCBankAccountAdd", this.Tag.ToString(), this.Name);
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
                where.AppendFormat("a.enable_flag='{0}'", DataSources.EnumEnableFlag.USING.ToString("d"));
                string bankAccount = txtBankAccount.Caption.Trim();//银行账户
                if (bankAccount.Length > 0)
                {
                    where.AppendFormat(" and a.bank_account like '%{0}%'", bankAccount);
                }
                string bankName = txtBankName.Caption.Trim();//银行名称
                if (bankName.Length > 0)
                {
                    where.AppendFormat(" and a.bank_name like '%{0}%'", bankName);
                }
                string status = CommonCtrl.IsNullToString(cboStatus.SelectedValue);//状态
                if (status.Length > 0)
                {
                    where.AppendFormat(" and a.status='{0}'", status);
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindData), where);
            }
        }

        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindData(object obj)
        {
            //DataTable dt = DBHelper.GetTable("", "tb_bank_account", "*", obj.ToString(), "", "order by create_time");
            string tablename = "tb_bank_account a ";
            tablename += "left join  sys_user cu on cu.user_id=a.create_by ";
            tablename += "left join sys_user uu on uu.user_id=a.update_by ";
            SQLObj sqlinfo = new SQLObj();
            sqlinfo.Param = new Dictionary<string, ParamObj>();
            sqlinfo.sqlString = string.Format("select a.*,cu.user_name as create_name,uu.user_name as update_name from {0} where {1} order by a.create_time", tablename, obj.ToString());
            //DataTable dt = DBHelper.GetTable("", tablename, "a.*,cu.user_name as create_name,uu.user_name as update_name", obj.ToString(), "", "order by a.create_time");
            sqlinfo.cmdType=CommandType.Text;
            DataTable dt = DBHelper.GetDataSet("查询银行账户", sqlinfo).Tables[0];
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
            string strSql = "update tb_bank_account set status=@status where bank_account_id in ({0})";
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
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "银行账户", listSql);
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
        private void dgvBank_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = this.dgvBank.Columns[e.ColumnIndex].DataPropertyName;
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

            foreach (DataGridViewRow dgvr in dgvBank.Rows)
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
