using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;
using System.Threading;
using System.Collections;

namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    /// <summary>
    /// 公告管理-管理窗体
    /// Author：JC
    /// AddTime：2014.11.13
    /// 修改人：杨天帅
    /// 修改时间：2014.12.11
    /// </summary>
    public partial class UCBulletinManage : UCBase
    {
        #region --成员变量
        private bool myLock = true;
        private int recordCount = 0;       
        /// <summary>
        /// 审核窗体
        /// </summary>
        UCVerify verify;
        /// <summary>
        /// 未审核列表
        /// </summary>
        ArrayList verify_list = new ArrayList();
        /// <summary>
        /// 可撤销列表
        /// </summary>
        ArrayList revoke_list = new ArrayList();
        #endregion

        #region --构造函数
        public UCBulletinManage()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);  //设置查询按钮和清除按钮样式
            DataGridViewEx.SetDataGridViewStyle(this.dgvRecord);   //设置数据表格样式,并将最后一列填充其余空白               
        }
        #endregion

        #region --初始化窗体
        private void UCBulletinManage_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            this.dgvRecord.ReadOnly = false;

            this.btnVerify.Visible = true;
            this.btnRevoke.Visible = true;

            dtpSTime.Value = DateTime.Now.AddMonths(-1);
            dtpETime.Value = DateTime.Now;

            this.InitEvent();

            this.InitData();
            
            CommonCtrl.DgCmbBindDeptment(this.columnOrg, string.Empty);
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumAuditStatus));            

            this.cmbOrg.SelectedIndexChanged += new System.EventHandler(this.cboOrgId_SelectedIndexChanged);
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);

            this.BindPageData();
        }
        #endregion

        #region --初始化事件和数据
        private void InitEvent()
        {
            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.AddEvent -= new ClickHandler(UC_AddEvent);
            base.AddEvent += new ClickHandler(UC_AddEvent);

            base.DeleteEvent -= new ClickHandler(UC_DeleteEvent);
            base.DeleteEvent += new ClickHandler(UC_DeleteEvent);

            base.EditEvent -= new ClickHandler(UC_EditEvent);
            base.EditEvent += new ClickHandler(UC_EditEvent);

            base.VerifyEvent -= new ClickHandler(UC_VerifyEvent);
            base.VerifyEvent += new ClickHandler(UC_VerifyEvent);

            base.RevokeEvent -= new ClickHandler(UC_RevokeEvent);
            base.RevokeEvent += new ClickHandler(UC_RevokeEvent);

            base.ViewEvent -= new ClickHandler(UC_ViewEvent);
            base.ViewEvent += new ClickHandler(UC_ViewEvent);

            base.ExportEvent -= new ClickHandler(UC_ExportEvent);
            base.ExportEvent += new ClickHandler(UC_ExportEvent);            
        }
        private void InitData()
        {
            CommonCtrl.CmbBindDict(this.cboannouncement_type, "announcement_type");//公告分类
            DataSources.BindComBoxDataEnum(this.cmbStatus, typeof(DataSources.EnumStatus), true);
            CommonCtrl.CmbBindDeptment(this.cmbOrg, string.Empty);
        }
        #endregion

        #region --操作事件
        //新增事件
        void UC_AddEvent(object sender, EventArgs e)
        {
            UCBulletinAddOrEdit uc = new UCBulletinAddOrEdit(WindowStatus.Add,null,this.Name);
            uc.RefreshDataStart -= new UCBulletinAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCBulletinAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "公告管理-新增", "BulletinAdd", this.Tag.ToString(), this.Name);
        }
        //编辑事件
        void UC_EditEvent(object sender, EventArgs e)
        {
            if (this.dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["announcement_id"].ToString();            
            UCBulletinAddOrEdit uc = new UCBulletinAddOrEdit(WindowStatus.Edit, dr, this.Name);
            uc.RefreshDataStart -= new UCBulletinAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCBulletinAddOrEdit.RefreshData(this.BindPageData);
            base.addUserControl(uc, "公告管理-编辑", "BulletinEdit" + id, this.Tag.ToString(), this.Name);
        }
        //删除事件
        void UC_DeleteEvent(object sender, EventArgs e)
        {
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
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
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();

            dicFileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());

            bool flag = DBHelper.BatchUpdateDataByIn("批量删除公告", "sys_announcement", dicFileds, "announcement_id", listField.ToArray());
            if (flag)
            {
                this.BindPageData();
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
        //审核事件
        void UC_VerifyEvent(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                MessageBoxEx.Show("请选择需要审核的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();               
                foreach (DataGridViewRow dr in dgvRecord.Rows)
                {
                    object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        SQLObj obj = new SQLObj();
                        obj.cmdType = CommandType.Text;
                        Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                        dicParam.Add("announcement_id", new ParamObj("announcement_id", dr.Cells[this.columnId.Name].Value, SysDbType.VarChar, 40));//单据ID
                        dicParam.Add("status", new ParamObj("status", verify.auditStatus.ToString("d"), SysDbType.VarChar, 40));//单据状态
                      
                        obj.sqlString = "update sys_announcement set status=@status where announcement_id=@announcement_id";
                        obj.Param = dicParam;
                        listSql.Add(obj);
                    }
                }
                if (DBHelper.BatchExeSQLMultiByTrans("", listSql))
                {
                    this.BindPageData();
                    MessageBoxEx.Show("撤回成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
            }
        }
        //撤回
        void UC_RevokeEvent(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                MessageBoxEx.Show("请选择要撤回的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.Show("确认要撤回吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<SQLObj> listSql = new List<SQLObj>();
            foreach (DataGridViewRow dr in dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("announcement_id", new ParamObj("announcement_id", dr.Cells[this.columnId.Name].Value, SysDbType.VarChar, 40));//单据ID
                    dicParam.Add("status", new ParamObj("status", DataSources.EnumAuditStatus.DRAFT.ToString("d"), SysDbType.VarChar, 40));//单据状态

                    obj.sqlString = "update sys_announcement set status=@status where announcement_id=@announcement_id";
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为审核", listSql))
            {
                this.BindPageData();
                MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        // 预览       
        void UC_ViewEvent(object sender, EventArgs e)
        {
            if (this.dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择预览记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["announcement_id"].ToString();
            UCBulletinView uc = new UCBulletinView(dr, this.Name);
            base.addUserControl(uc, "公告管理-浏览", "BulletinView" + id, this.Tag.ToString(), this.Name);
        }
        //导出
        void UC_ExportEvent(object sender, EventArgs e)
        {
            if (this.dgvRecord.Rows.Count == 0)
            {
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel格式(*.xls)|*.xls";            
            sfd.Title = "导出报表";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                CommonCtrl.OutPutExcel(this.dgvRecord, sfd.OpenFile(), fileName);
            }
        }
        #endregion       

        #region --数据查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));
               
                if (!string.IsNullOrEmpty(txtKeyNmae.Caption.Trim()))
                {
                    where += string.Format(" and  announcement_title like '%{0}%'", txtKeyNmae.Caption.Trim());
                    where += string.Format(" and  content like '%{0}%'", txtKeyNmae.Caption.Trim());
                }
                if (cboannouncement_type.SelectedValue != null
                    && cboannouncement_type.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  announcement_type = '{0}'", cboannouncement_type.SelectedValue);
                }
                if (cmbStatus.SelectedValue != null
                    && cmbStatus.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  status = '{0}'", cmbStatus.SelectedValue);
                }
                if (cmbOrg.SelectedValue != null
                    && cmbOrg.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  org_id = '{0}'", cmbOrg.SelectedValue);
                }
                if (cmbUser.SelectedValue != null
                    && cmbUser.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  user_id = '{0}'", cmbUser.SelectedValue);
                }
                if (dtpSTime.Value != null && dtpETime.Value != null)
                {
                    where += string.Format(" and date_up BETWEEN {0} and {1}", 
                        Common.LocalDateTimeToUtcLong(dtpSTime.Value.Date), 
                        Common.LocalDateTimeToUtcLong(dtpETime.Value.Date.Add(new TimeSpan(23,59,59))));//发布时间                   
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页查询公告", "sys_announcement", "*", obj.ToString(), "", "order by create_time desc",
                           page.PageIndex, page.PageSize, out this.recordCount);

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvRecord.DataSource = obj;
            page.RecordCount = recordCount;

            this.myLock = true;
        }       
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        #endregion

        #region 双击单元格进入详情窗体
        private void dgvannouncement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = dr["announcement_id"].ToString();
            UCBulletinView uc = new UCBulletinView(dr, this.Name);
            base.addUserControl(uc, "公告管理-预览", "view" + id, this.Tag.ToString(), this.Name);
        }
        #endregion

        #region --数据显示格式化
        private void dgvannouncement_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvRecord.Columns[e.ColumnIndex].Name;
            if (fieldNmae.Equals(this.columnType.Name))
            {
                e.Value = LocalCache.GetDictNameById(e.Value.ToString());
            }
            else if (fieldNmae.Equals(this.columnTime.Name))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);                
            }
            else if (fieldNmae.Equals(this.columnUser.Name))
            {
                e.Value = LocalCache.GetUserById(e.Value.ToString());
            }        
        }
        #endregion                  

        #region  --根据部门绑定发布人
        /// <summary>
        /// 根据部门绑定发布人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboOrgId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbOrg.SelectedValue == null)
            {
                return;
            }
            CommonCtrl.CmbBindUser(cmbUser, this.cmbOrg.SelectedValue.ToString());
        }
        #endregion              

        #region --清除查询条件
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InitData();
        }
        #endregion

        #region --CellClick事件
        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.dgvRecord_HeadCheckChanged();
            }
        }
        #endregion

        /// <summary>
        /// 启用按钮状态设置
        /// </summary>       
        private void btnStatus()
        {
            if (this.verify_list.Count > 0 && this.revoke_list.Count == 0)
            {
                base.btnVerify.Enabled = true;
                base.btnRevoke.Enabled = false;
            }
            else if (this.verify_list.Count == 0 && this.revoke_list.Count > 0)
            {
                base.btnVerify.Enabled = false;
                base.btnRevoke.Enabled = true;
            }
            else
            {
                base.btnVerify.Enabled = false;
                base.btnRevoke.Enabled = false;
            }           
        }

        #region --HeadCheckChanged事件
        private void dgvRecord_HeadCheckChanged()
        {           
            this.verify_list.Clear();
            this.revoke_list.Clear();

            string value = string.Empty;
            base.btnRevoke.Enabled = true;
            base.btnVerify.Enabled = true;

            foreach (DataGridViewRow row in this.dgvRecord.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].EditedFormattedValue))
                {
                    value = row.Cells[this.columnStatus.Name].Value.ToString();
                    if (value == DataSources.EnumAuditStatus.DRAFT.ToString("d")) //草稿
                    {
                        this.verify_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                    else if (value == DataSources.EnumAuditStatus.AUDIT.ToString("d"))
                    {
                        this.revoke_list.Add(row.Cells[this.columnId.Name].EditedFormattedValue.ToString());
                    }
                    else
                    {
                        base.btnRevoke.Enabled = false;
                        base.btnVerify.Enabled = false;

                        this.verify_list.Clear();
                        this.revoke_list.Clear();
                        break;
                    }
                }
            }
            this.btnStatus();
        }
        #endregion       
    }
}
