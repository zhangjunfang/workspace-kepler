using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using BLL;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using SYSModel;
using HXC_FuncUtility;
using HXCServerWinForm.CommonClass;

namespace HXCServerWinForm.UCForm
{
    /// <summary>
    /// Creator:yangtianshuai
    /// CreateTime:2014.10.31
    /// Function:compay manage
    /// UpdateTime:2014.10.31
    /// </summary>
    public partial class UCCompany : UCBase
    {
        #region --成员变量
        private DataTable dtRecord;
        /// <summary>
        /// 启用停用状态
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
        public UCCompany()
        {
            InitializeComponent();
            //UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);

            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            base.btnPrint.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;


            base.ViewEvent += new ClickHandler(btnScan_Click);
            base.AddEvent += new ClickHandler(btnAdd_Click);
            base.StatusEvent += new ClickHandler(btnStartOrStop_Click);
            base.EditEvent += new ClickHandler(btnEdit_Click);
            base.DeleteEvent += new ClickHandler(btnDelete_Click);
            base.ImportEvent += new ClickHandler(btnInput_Click);
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
        //浏览详细信息
        private void btnScan_Click(object sender, EventArgs e)
        {
            this.ViewOrEdit(WindowStatus.View, "公司管理-浏览");
        }
        /// <summary>
        /// 浏览或者编辑
        /// </summary>
        /// <param name="status"></param>
        private void ViewOrEdit(WindowStatus status, string menuName)
        {
            if (this.dgvRecord.CurrentRow == null)
            {
                return;
            }
            DataRow dr = (this.dgvRecord.CurrentRow.DataBoundItem as DataRowView).Row;
            if (dr == null)
            {
                return;
            }
            UCCompanyEdit comEdit = new UCCompanyEdit(dr, status, this.Name);
            comEdit.uc = this;
            base.addUserControl(comEdit, menuName, dr["com_id"].ToString(), this.Tag.ToString(), this.Name);
        }
        //新增
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UCCompanyViewOrAdd comAdd = new UCCompanyViewOrAdd(WindowStatus.Add, this.Name);
            comAdd.uc = this;
            comAdd.refreshDataStart += new UCCompanyViewOrAdd.RefreshData(this.BindData);
            base.addUserControl(comAdd, "公司管理-添加", "Company_Add", this.Tag.ToString(), this.Name);
        }
        //编辑
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.ViewOrEdit(WindowStatus.Edit, "公司管理-编辑");
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> listId = new List<string>();
            foreach (DataGridViewRow dr in this.dgvRecord.Rows)
            {
                object isCheck = dr.Cells[this.columnCheck.Name].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listId.Add(dr.Cells[this.com_id.Name].Value.ToString());
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
            bool flag = DBHelper.BatchUpdateDataByIn("批量删除公司档案", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", comField, "com_id", listId.ToArray());
            if (flag)
            {
                for (int i = 0; i < this.dtRecord.Rows.Count; i++)
                {
                    if (listId.Contains(this.dtRecord.Rows[i][this.com_id.Name].ToString()))
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
        private void UCCompany_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//按钮权限-是否隐藏
            base.SetBtnStatus(WindowStatus.Normal);
            this.dgvRecord.ReadOnly = false;
            DataGridViewEx.SetDataGridViewStyle(dgvRecord);
            CommonClass.CommonFuncCall.BindProviceComBox(this.cmbProvince, "省");
            this.InitSearchCondition();
            this.BindData();
        }
        #endregion

        #region --设置窗体布局
        private void SetFormLayout()
        {
            int width = 0;
            foreach (DataGridViewColumn dgvc in this.dgvRecord.Columns)
            {
                if (dgvc.Visible)
                {
                    width += dgvc.Width;
                }
            }
            if (width < this.dgvRecord.Width)
            {
                foreach (DataGridViewColumn dgvc in this.dgvRecord.Columns)
                {
                    dgvc.Width = this.dgvRecord.Width * dgvc.Width / width;
                }
            }
        }
        #endregion

        #region --所在地级联选择
        //选择省
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbProvince.SelectedValue.ToString()))
            {
                CommonClass.CommonFuncCall.BindCityComBox(this.cmbCity, this.cmbProvince.SelectedValue.ToString(), "市");
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, this.cmbCity.SelectedValue.ToString(), "县");
            }
            else
            {
                CommonClass.CommonFuncCall.BindCityComBox(this.cmbCity, "", "市");
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, "", "县");
            }
        }
        //选择市
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbCity.SelectedValue.ToString()))
            {
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, this.cmbCity.SelectedValue.ToString(), "市");
            }
            else
            {
                CommonClass.CommonFuncCall.BindCountryComBox(this.cmbTown, "", "县");
            }
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
                if (!string.IsNullOrEmpty(dtpstart.Value))
                {
                    long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpstart.Value));
                    where += " and create_time>=" + startTicks.ToString();
                }
                if (!string.IsNullOrEmpty(dtpend.Value))
                {
                    long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpend.Value).AddDays(1));
                    where += " and create_time<" + endTicks.ToString();
                }

                if (!string.IsNullOrEmpty(this.tbCompany.Caption.Trim()))//公司名称
                {
                    where += string.Format(" and  com_name like '%{0}%'", this.tbCompany.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(this.tbLinkMan.Caption.Trim()))//联系人
                {
                    where += string.Format(" and  com_contact like '%{0}%'", this.tbLinkMan.Caption.Trim());
                }
                if (this.cmbStatus.SelectedValue != null && this.cmbStatus.SelectedValue.ToString().Length > 0)//状态
                {
                    where += string.Format(" and  status = '{0}'", this.cmbStatus.SelectedValue.ToString());
                }
                string tempStr = string.Empty;
                if (this.cmbProvince.SelectedValue != null
                    && this.cmbProvince.SelectedValue.ToString().Length > 0)//所在地
                {
                    where += string.Format(" and  province like '%{0}%'", this.cmbProvince.SelectedValue);
                }
                if (this.cmbCity.SelectedValue != null
                    && this.cmbCity.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  city like '%{0}%'", this.cmbCity.SelectedValue);
                }
                if (this.cmbTown.SelectedValue != null
                    && this.cmbTown.SelectedValue.ToString().Length > 0)
                {
                    where += string.Format(" and  county like '%{0}%'", this.cmbTown.SelectedValue);
                }
                this.dtRecord = DBHelper.GetTable("查询公司", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", "*", where, "", "");
                this.dgvRecord.DataSource = this.dtRecord;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region --初始化检索条件
        private void InitSearchCondition()
        {
            this.tbCompany.Caption = string.Empty;
            this.tbLinkMan.Caption = string.Empty;
            if (this.cmbStatus.Items.Count > 0)
            {
                this.cmbStatus.SelectedIndex = 0;
            }
            if (this.cmbProvince.Items.Count > 0)
            {
                this.cmbProvince.SelectedIndex = 0;
            }
            if (this.cmbCity.Items.Count > 0)
            {
                this.cmbCity.SelectedIndex = 0;
            }
            if (this.cmbTown.Items.Count > 0)
            {
                this.cmbTown.SelectedIndex = 0;
            }
            dtpstart.Value = "";
            dtpend.Value = "";
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
            if (fieldNmae == this.columnCreateTime.Name)
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

        #region --启用停用
        /// <summary> 启动或者停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartOrStop_Click(object sender, EventArgs e)
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

            if (MessageBoxEx.Show("将要" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中记录，是否继续？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            dicField.Add("status", enumStatus == DataSources.EnumStatus.Start ? Convert.ToInt16(DataSources.EnumStatus.Stop).ToString() : Convert.ToInt16(DataSources.EnumStatus.Start).ToString());
            bool flag = DBHelper.BatchUpdateDataByIn("批量" + (DataSources.EnumStatus.Start == enumStatus ? "停用" : "启用") + "选中记录", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_company", dicField, "com_id", arrField);
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

        /// <summary> 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvtasklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
