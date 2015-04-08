using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.SysManage.Role
{
    /// <summary>
    /// 角色管理
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCRoleManager : UCBase
    {
        #region --成员变量
        private int recordCount = 0;
        /// <summary>
        /// 列表中选中的是启用的 id集合
        /// </summary>
        ArrayList qi_list = new ArrayList();
        /// <summary>
        /// 列表中选中的是停用的 id集合
        /// </summary>
        ArrayList ti_list = new ArrayList();
        private string status = string.Empty;

        private string _system = "system";
        private string sysid = string.Empty;

        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        BusinessPrint businessPrint;//业务打印功能
        #endregion

        #region --构造函数
        public UCRoleManager()
        {
            InitializeComponent();
            DataGridViewEx.SetDataGridViewStyle(this.dgvRole);
        }
        #endregion

        #region --窗体初始化
        private void UCRoleManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏           

            //dgvRole.ReadOnly = false;

            this.InitEvent();

            //状态
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));
            //数据来源
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnSources, typeof(DataSources.EnumDataSources));

            base.SetContentMenuScrip(dgvRole);
            List<string> listNotPrint = new List<string>();
            PaperSize printsize = new PaperSize("printsize", dgvRole.Width / 4 + 40, dgvRole.Height);
            businessPrint = new BusinessPrint(dgvRole, "sys_role", "角色档案", printsize, listNotPrint);
        }
        #endregion

        #region --初始化事件
        private void InitEvent()
        {
            base.AddEvent -= new ClickHandler(UC_AddEvent);
            base.AddEvent += new ClickHandler(UC_AddEvent);

            base.CopyEvent -= new ClickHandler(UC_CopyEvent);
            base.CopyEvent += new ClickHandler(UC_CopyEvent);

            base.EditEvent -= new ClickHandler(UC_EditEvent);
            base.EditEvent += new ClickHandler(UC_EditEvent);

            base.DeleteEvent -= new ClickHandler(UC_DeleteEvent);
            base.DeleteEvent += new ClickHandler(UC_DeleteEvent);

            //base.ViewEvent -= new ClickHandler(UC_ViewEvent);
            //base.ViewEvent += new ClickHandler(UC_ViewEvent);
            base.ViewEvent -= new ClickHandler(UCRoleManager_ViewEvent);
            base.ViewEvent += new ClickHandler(UCRoleManager_ViewEvent);

            base.StatusEvent -= new ClickHandler(UC_StatusEvent);
            base.StatusEvent += new ClickHandler(UC_StatusEvent);
            base.PrintEvent += new ClickHandler(UCRoleManager_PrintEvent);

            //base.ExportEvent -= new ClickHandler(UC_ExportEvent);
            //base.ExportEvent += new ClickHandler(UC_ExportEvent);
            base.ExportEvent += new ClickHandler(UCRoleManager_ExportEvent);
        }





        #endregion

        #region 按钮事件
        /// <summary>
        /// 转向添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_AddEvent(object sender, EventArgs e)
        {
            UCRoleAddOrEdit uc = new UCRoleAddOrEdit(null, this.Name);
            uc.windowStatus = WindowStatus.Add;
            uc.RefreshDataStart -= new UCRoleAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCRoleAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "角色管理-新增", "RoleAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 转向复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_CopyEvent(object sender, EventArgs e)
        {
            if (dgvRole.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择复制记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string> listId = new List<string>();
            foreach (DataGridViewRow sdr in this.dgvRole.Rows)
            {
                object obj = sdr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (obj != null && (bool)obj)
                {
                    if (sdr.Cells[this.role_code.Name].Value.ToString() == this._system)
                    {
                        continue;
                    }
                    listId.Add(sdr.Cells[this.columnId.Name].Value.ToString());
                }
            }
            if (listId.Count > 1)
            {
                MessageBoxEx.Show("请选中一条信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DataRow dr = (this.dgvRole.CurrentRow.DataBoundItem as DataRowView).Row;
                string id = dr["role_id"].ToString();
                UCRoleAddOrEdit uc = new UCRoleAddOrEdit(dr, this.Name);
                uc.windowStatus = WindowStatus.Copy;
                uc.id = id;
                uc.RefreshDataStart -= new UCRoleAddOrEdit.RefreshData(this.BindPageData);
                uc.RefreshDataStart += new UCRoleAddOrEdit.RefreshData(this.BindPageData);
                base.addUserControl(uc, "角色管理-复制", "RoleCopy" + id, this.Tag.ToString(), this.Name);
            }
        }
        /// <summary>
        /// 去编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_EditEvent(object sender, EventArgs e)
        {
            if (dgvRole.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvRole.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["role_id"].ToString();
            UCRoleAddOrEdit uc = new UCRoleAddOrEdit(dr, this.Name);
            uc.windowStatus = WindowStatus.Edit;
            uc.id = id;
            uc.RefreshDataStart -= new UCRoleAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCRoleAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "角色管理-编辑", "RoleEdit" + id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UC_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listId = RecordSelectID();
            if (listId.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBoxEx.ShowQuestion("是否确认删除?"))
            {
                Dictionary<string, string> fileds = new Dictionary<string, string>();
                fileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                fileds.Add("update_by", GlobalStaticObj.UserID);
                fileds.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());

                bool flag = DBHelper.BatchUpdateDataByIn("批量删除角色", "sys_role", fileds, "role_id", listId.ToArray());

                if (flag)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this._DeleteOther), listId);
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.BindPageData();
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private List<string> RecordSelectID()
        {
            List<string> listId = new List<string>();
            foreach (DataGridViewRow dr in this.dgvRole.Rows)
            {
                object obj = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (obj != null && (bool)obj)
                {
                    if (dr.Cells[this.role_code.Name].Value.ToString() == this._system)
                    {
                        continue;
                    }
                    listId.Add(dr.Cells[this.columnId.Name].Value.ToString());
                }
            }
            return listId;
        }
        /// <summary>
        /// 预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCRoleManager_ViewEvent(object sender, EventArgs e)
        {
            businessPrint.Preview(dgvRole.GetBoundData());
        }
        void UCRoleManager_PrintEvent(object sender, EventArgs e)
        {
            businessPrint.Print(dgvRole.GetBoundData());
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCRoleManager_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvRole.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "角色管理" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, ExcelHandler.HandleDataTableForExcel(dgvRole.GetBoundData(), dgvRole));
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("角色管理" + ex.Message, "client");
                MessageBoxEx.ShowWarning("导出失败！");
            }
        }
        /// <summary>
        /// 删除其他关联表
        /// </summary>
        /// <param name="obj"></param>
        private void _DeleteOther(object obj)
        {
            List<string> listId = obj as List<string>;

            List<SysSQLString> listSql = new List<SysSQLString>();
            foreach (string id in listId)
            {
                SysSQLString sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                sqlString.sqlString = string.Format("delete from tr_user_role where role_id='{0}'", id);
                listSql.Add(sqlString);

                sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                sqlString.sqlString = string.Format("delete from tr_role_function where role_id='{0}'", id);
                listSql.Add(sqlString);
            }
            DBHelper.BatchExeSQLStringMultiByTrans("", listSql);
        }
        #endregion

        #region --数据查询
        /// <summary> 
        /// 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        public void BindPageData()
        {
            string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));
            DataTable dt = DBHelper.GetTableByPage("分页查询角色管理", "v_role", "*", where, "", "order by create_time desc",
                           page.PageIndex, page.PageSize, out this.recordCount);
            dt.Columns.Add("createtime", typeof(DateTime));
            dt.Columns.Add("updatetime", typeof(DateTime));
            for (int rolenum = 0; rolenum < dt.Rows.Count; rolenum++)
            {
                dt.Rows[rolenum]["createtime"] = Common.UtcLongToLocalDateTime(dt.Rows[rolenum]["create_time"]);
                dt.Rows[rolenum]["updatetime"] = Common.UtcLongToLocalDateTime(dt.Rows[rolenum]["update_time"]);
            }

            this.dgvRole.DataSource = dt;
            this.page.RecordCount = this.recordCount;
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        #endregion

        #region --dgv事件
        private void dgvRole_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
            if (dgvRole.Rows[e.RowIndex].Cells[this.role_code.Name].Value.ToString() == this._system)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 双击时间 去浏览页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvRole.Rows[e.RowIndex].Cells[this.columnId.Name].Value.ToString();
                UCRoleView uc = new UCRoleView();
                uc.uc = this;
                uc.wStatus = WindowStatus.View;
                uc.id = id;
                uc.Name = this.Name;
                uc.RoleButtonStstus(Name);
                base.addUserControl(uc, "角色管理-浏览", "RoleView" + id, this.Tag.ToString(), this.Name);
            }
        }

        private void dgvRole_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string fieldNmae = dgvRole.Columns[e.ColumnIndex].DataPropertyName;
                if (fieldNmae.Equals(this.columnCreateTime.Name)
                    || fieldNmae.Equals(this.columnUpdateTime.Name))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
        }

        //点击单元格
        private void dgvRole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (listIDs.Count <= 1)
            {
                btnStatus.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                if (dgvRole.CurrentRow != null && dgvRole.CurrentRow.Cells[columnId.Name].Value.ToString() == sysid)
                {
                    btnStatus.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }

            }
        }

        private void dgvRole_HeadCheckChanged()
        {
            SetSelectedStatus();
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

        private bool StatusSql()
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = "update sys_role set state=@state where role_id in ({0})";
            string ids = string.Empty;
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                sql.Param.Add("state", ((int)DataSources.EnumStatus.Stop).ToString());
                foreach (string id in listStart)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');

            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("state", ((int)DataSources.EnumStatus.Start).ToString());
                foreach (string id in listStop)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "角色", listSql);

        }

        #endregion

        #region --标记系统角色
        //数据绑定完成
        private void dgvRole_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dgvr in this.dgvRole.Rows)
            {
                if (dgvr.Cells[this.role_code.Name].Value.ToString() == this._system)
                {
                    dgvr.DefaultCellStyle.ForeColor = Color.Red;
                    dgvr.DefaultCellStyle.SelectionForeColor = Color.Red;
                    sysid = dgvr.Cells[columnId.Name].Value.ToString();
                }
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

            #region 选中系统角色后的按钮处理
            if (listIDs.Where(id => id == sysid).Count() > 0)
            {
                btnStatus.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            #endregion
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


            foreach (DataGridViewRow dgvr in dgvRole.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[columnCheck.Name].Value))
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

        #region 过期方法
        //private void StatusSql(List<SQLObj> listSql, ArrayList idList, string status)
        //{
        //    if (idList.Count > 0)
        //    {
        //        foreach (string id in idList)
        //        {
        //            SQLObj sqlObj = new SQLObj();
        //            sqlObj.cmdType = CommandType.Text;
        //            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();//参数
        //            dicParam.Add("com_id", new ParamObj("com_id", id, SysDbType.VarChar, 40));//ID
        //            dicParam.Add("status", new ParamObj("status", status, SysDbType.VarChar, 40));
        //            dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
        //            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString(), SysDbType.BigInt));
        //            sqlObj.sqlString = @"update [tb_company] set status=@status,update_by=@update_by,update_time=@update_time where com_id=@com_id;";
        //            sqlObj.Param = dicParam;
        //            listSql.Add(sqlObj);
        //        }
        //    }
        //}


        //<summary>
        //全选复选框事件 
        //</summary>
        //private void dgvRecord_HeadCheckChanged()
        //{
        //    this.qi_list.Clear();
        //    this.ti_list.Clear();
        //    this.status = string.Empty;

        //    foreach (DataGridViewRow row in this.dgvRole.Rows)
        //    {
        //        if (Convert.ToBoolean(row.Cells[0].Value))
        //        {
        //            if (row.Cells[this.columnStatus.Name].Value.ToString() == DataSources.EnumStatus.Start.ToString("d")) //表格中是启用
        //            {
        //                this.qi_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
        //            }
        //            else//表格中是停用
        //            {
        //                this.ti_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
        //            }
        //        }
        //    }
        //    SetBtnStatus(this.qi_list, this.ti_list);

        //}
        ///// <summary>
        ///// 启用按钮状态设置
        ///// </summary>
        ///// <param name="qi_list">列表中选中的是启用的 id集合</param>
        ///// <param name="ti_list">列表中选中的是停用的 id集合</param>
        //private void SetBtnStatus(ArrayList qi_list, ArrayList ti_list)
        //{
        //    if (qi_list.Count > 0 && ti_list.Count > 0)
        //    {
        //        base.btnStatus.Enabled = false;
        //        base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
        //    }
        //    else if (qi_list.Count > 0 && ti_list.Count == 0)
        //    {
        //        base.btnStatus.Enabled = true;
        //        base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Stop);
        //        this.status = DataSources.EnumStatus.Stop.ToString("d");
        //    }
        //    else if (qi_list.Count == 0 && ti_list.Count > 0)
        //    {
        //        base.btnStatus.Enabled = true;
        //        base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
        //        this.status = DataSources.EnumStatus.Start.ToString("d");
        //    }
        //    else if (qi_list.Count == 0 && ti_list.Count == 0)
        //    {
        //        base.btnStatus.Enabled = false;
        //        base.btnStatus.Caption = DataSources.GetDescription(DataSources.EnumStatus.Start);
        //    }
        //}

        /// <summary>
        /// 点击复选框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 0)
        //    {
        //        this.qi_list.Clear();
        //        this.ti_list.Clear();
        //        this.status = string.Empty;

        //        foreach (DataGridViewRow row in this.dgvRole.Rows)
        //        {
        //            if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
        //            {
        //                if (row.Cells[this.columnStatus.Name].Value.ToString() == DataSources.EnumStatus.Start.ToString("d")) //表格中是启用
        //                {
        //                    this.qi_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
        //                }
        //                else//表格中是停用
        //                {
        //                    this.ti_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
        //                }
        //            }
        //        }
        //        SetBtnStatus(this.qi_list, this.ti_list);
        //    }
        //}
        #endregion

    }
}
