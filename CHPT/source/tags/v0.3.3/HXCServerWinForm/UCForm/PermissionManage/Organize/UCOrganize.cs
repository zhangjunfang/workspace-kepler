using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using BLL;
using System.Collections;
using HXCServerWinForm.CommonClass;
using SYSModel;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.31
    /// Function:organize manage
    /// UpdateTime:2014.10.31
    /// </summary>
    public partial class UCOrganize : UCBase
    {
        #region --成员变量
        private DataTable dtRecord;
        /// <summary> 启用停用状态
        /// </summary>
        DataSources.EnumStatus eStatus;
        List<string> listStart = new List<string>();
        List<string> listStop = new List<string>();

        /// <summary>
        /// 列表中选中的是启用的 id集合
        /// </summary>
        ArrayList qi_list = new ArrayList();
        /// <summary>
        /// 列表中选中的是停用的 id集合
        /// </summary>
        ArrayList ti_list = new ArrayList();
        #endregion

        #region --构造函数
        public UCOrganize()
        {
            InitializeComponent();

            base.ViewEvent += new ClickHandler(btnScan_Click);
            base.StatusEvent +=new ClickHandler(UCOrganize_StatusEvent);
            base.AddEvent += new ClickHandler(btnAdd_Click);
            base.EditEvent += new ClickHandler(btnEdit_Click);
            base.DeleteEvent += new ClickHandler(btnDelete_Click);
            base.ImportEvent += new ClickHandler(btnInput_Click);
            base.ExportEvent += new ClickHandler(btnOutput_Click);
        }

        #endregion

        #region --按钮操作
        //检索
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }
        //清理,初始化查询条件
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.InitSearchCondition();
        }
        //浏览
        private void btnScan_Click(object sender, EventArgs e)
        {
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            if (dr == null)
            {
                return;
            }
            UCOrganizeAddOrEdit ucOrgView = new UCOrganizeAddOrEdit();
            ucOrgView.uc = this;
            ucOrgView.windowStatus = WindowStatus.View;
            ucOrgView.drRecord = dr;            
            base.addUserControl(ucOrgView, "组织管理-浏览", dr["org_id"].ToString(), this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 添加或者编辑
        /// </summary>
        /// <param name="status"></param>
        /// <param name="menuName"></param>
        private void AddOrEdit(WindowStatus status, string menuName)
        {
            DataRow dr = null;
            string menuId = string.Empty;
            if (status == WindowStatus.Add)
            {
                menuId = "Organize_Add";
            }
            else
            {
                if (this.dgvRecord.CurrentRow == null)
                {
                    return;
                }
                dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
                menuId = dr["org_id"].ToString();
            }
            UCOrganizeAddOrEdit orgAddOrEdit = new UCOrganizeAddOrEdit();
            orgAddOrEdit.uc = this;
            orgAddOrEdit.drRecord = dr;
            orgAddOrEdit.windowStatus = status;
            base.addUserControl(orgAddOrEdit, menuName, menuId, this.Tag.ToString(), this.Name);
        }
        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddOrEdit(WindowStatus.Add, "组织管理-添加");
        }
        //编辑
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.AddOrEdit(WindowStatus.Edit, "组织管理-编辑");
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvRecord.SelectedRows.Count == 0)
            {
                return;
            }

            List<string> listId = new List<string>();
            foreach (DataGridViewRow dr in this.dgvRecord.Rows)
            {
                object isCheck = dr.Cells["columnCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listId.Add(dr.Cells["columnId"].Value.ToString());
                }
            }
            if (listId.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult result = MessageBox.Show("是否删除该行数据？", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", "0");
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除组织档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", comField, "org_id", listId.ToArray());
            if (flag)
            {
                for (int i = 0; i < this.dtRecord.Rows.Count; i++)
                {
                    if (listId.Contains(this.dtRecord.Rows[i]["org_id"].ToString()))
                    {
                        this.dtRecord.Rows.RemoveAt(i);
                        i--;
                    }
                }
                if (this.dgvRecord.Rows.Count > 0)
                {
                    this.dgvRecord.CurrentCell = this.dgvRecord.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void  UCOrganize_StatusEvent(object sender, EventArgs e)
        {
           if (listStart.Count == 0 && listStop.Count == 0)
            {
                MessageBoxEx.Show("请选择" + base.btnStatus.Caption + "记录！");
                return;
            }
            DataSources.EnumStatus enumStatus = listStart.Count > 0 ? DataSources.EnumStatus.Start : DataSources.EnumStatus.Stop;
            string[] arrField; ;
            if (listStart.Count > 0)
            {
                arrField = new string[listStart.Count];
                listStart.CopyTo(arrField);
            }
            else
            {
                arrField = new string[listStop.Count];
                listStop.CopyTo(arrField);
            }

            if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中任务，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("status", enumStatus == DataSources.EnumStatus.Start ? Convert.ToInt16(DataSources.EnumStatus.Stop).ToString() : Convert.ToInt16(DataSources.EnumStatus.Start).ToString());
            bool flag = DBHelper.BatchUpdateDataByIn("批量" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中任务", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", dicField, "com_id", arrField);
            if (flag)
            {
                BindData();
                if (dgvRecord.Rows.Count > 0)
                {
                    dgvRecord.CurrentCell = dgvRecord.Rows[0].Cells[0];
                }
                MessageBoxEx.Show("操作成功！", "系统提示");
            }
            else
            {
                MessageBoxEx.Show("操作失败！", "系统提示");
            }
        }
        //导入
        private void btnInput_Click(object sender, EventArgs e)
        {

        }
        //导出
        private void btnOutput_Click(object sender, EventArgs e)
        {

        }
        //操作记录
        private void btnRecord_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region --窗体初始化
        private void UCOrganize_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            base.SetBtnStatus(WindowStatus.Normal);
            this.dgvRecord.ReadOnly = false;
            DataGridViewEx.SetDataGridViewStyle(dgvRecord);
            //加载公司
            CommonFuncCall.BindCompanyComBox(this.com_id);
            this.InitSearchCondition();
            this.BindData();
        }
        #endregion

        #region --数据查询
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindData()
        {
            try
            {
                string where = "enable_flag='1'";
                //where += string.Format(" and (create_time >= {0} and create_time <= {1})", Common.LocalDateTimeToUtcLong(this.dtpStartTime.Value.Date),
                //    Common.LocalDateTimeToUtcLong(this.dtpEndTime.Value.Date.Add(new TimeSpan(23, 59, 59))));
                if (!string.IsNullOrEmpty(this.txtorg_code.Caption.Trim()))//组织编码
                {
                    where += string.Format(" and  org_code like '%{0}%'", this.txtorg_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(this.tborg_name.Caption.Trim()))//组织名称
                {
                    where += string.Format(" and  org_name like '%{0}%'", this.tborg_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(this.tbLinkMan.Caption.Trim()))//联系人
                {
                    where += string.Format(" and  org_contact like '%{0}%'", this.tbLinkMan.Caption.Trim());
                }
                if (this.cmbStatus.SelectedValue != null && this.cmbStatus.SelectedValue.ToString().Length > 0)//状态
                {
                    where += string.Format(" and  status = '{0}'", this.cmbStatus.SelectedValue.ToString());
                }

                this.dtRecord = DBHelper.GetTable("查询组织", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_organization", "*", where, "", "");
                this.dgvRecord.DataSource = this.dtRecord;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region --初始化检索条件
        /// <summary>
        /// 初始化检索条件
        /// </summary>
        private void InitSearchCondition()
        {
            this.tborg_name.Caption = string.Empty;
            this.tbLinkMan.Caption = string.Empty;
            if (this.cmbStatus.Items.Count > 0)
            {
                this.cmbStatus.SelectedIndex = 0;
            }
        }
        #endregion

        #region --数据格式
        private void dgvRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = this.dgvRecord.Columns[e.ColumnIndex].Name;
            if (fieldNmae == "create_time")
            {
                e.Value = Common.UtcLongToLocalDateTime(e.Value);
            }
            if (fieldNmae.Equals("status"))
            {
                DataSources.EnumStatus enumStatus = (DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumStatus, true);
            }
        }
        #endregion

        #region --选择编辑
        private void dgvRecord_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region --选中某一行
        private void dgvRecord_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || this.dgvRecord.CurrentRow == null)
            {
                return;
            }

            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            if (dr["status"].ToString() == DataSources.EnumStatus.Start.ToString("d"))
            {
                base.btnStatus.Caption = "禁用";
            }
            else
            {
                base.btnStatus.Caption = "启用";
            }
        }
        #endregion

        #region --启用停用
        /// <summary> 点击复选框事件
        /// </summary>
        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex == 0)
            {
                object obj = dgvRecord.Rows[e.RowIndex].Cells["status"].Value;
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                {
                    return;
                }
                DataSources.EnumStatus enumState = (DataSources.EnumStatus)Convert.ToInt32(obj.ToString());
                string com_id = dgvRecord.Rows[e.RowIndex].Cells["com_id"].Value.ToString();
                bool flag = Convert.ToBoolean(dgvRecord.Rows[e.RowIndex].Cells[0].EditedFormattedValue);
                if (flag)//选中
                {
                    if (enumState == DataSources.EnumStatus.Start)
                    {
                        if (!listStart.Contains(com_id))
                        {
                            listStart.Add(com_id);
                        }
                    }
                    else
                    {
                        if (!listStop.Contains(com_id))
                        {
                            listStop.Add(com_id);
                        }
                    }
                }
                else
                {
                    if (enumState == DataSources.EnumStatus.Start)
                    {
                        if (listStart.Contains(com_id))
                        {
                            listStart.Remove(com_id);
                        }
                    }
                    else
                    {
                        if (listStop.Contains(com_id))
                        {
                            listStop.Remove(com_id);
                        }
                    }
                }
                SetBtnStatus(false);
            }
        }

        /// <summary> 设置按钮状态
        /// </summary>
        public void SetBtnStatus(bool isInit)
        {
            if (isInit)
            {
                listStart = new List<string>();
                listStop = new List<string>();
            }
            bool isCanUse = (listStart.Count == 0 && listStop.Count > 0) || (listStart.Count > 0 && listStop.Count == 0);
            base.btnStatus.Enabled = isCanUse;
            base.btnStatus.Caption = listStart.Count > 0 ? "停用" : "启用";
        }
        #endregion
    }
}
