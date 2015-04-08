using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.Personnel
{
    /// <summary>
    /// 人员管理
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCPersonnelManager : UCBase
    {
        #region --成员变量
        private bool myLock = true;
        private int recordCount = 0;
        #endregion

        #region --构造函数
        public UCPersonnelManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);
            DataGridViewEx.SetDataGridViewStyle(this.dgvUser);

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.AddEvent += new ClickHandler(UCPersonnelManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCPersonnelManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCPersonnelManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCPersonnelManager_DeleteEvent);
        }
        #endregion

        #region --窗体加载
        private void UCPersonnelManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);
            dgvUser.ReadOnly = false;

            this.dtpentry_date.Value = DateTime.Now.AddMonths(-1);
            this.dtpentry_date_end.Value = DateTime.Now;

            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);//是否操作员

            DataSources.BindComDataGridViewBoxColumnDataEnum(this.is_operator, typeof(DataSources.EnumYesNo));
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));

            this.BindTree();
        }
        #endregion

        #region --公司组织树
        /// <summary>
        /// 公司组织树
        /// </summary>
        private void BindTree()
        {
            string strSql = "select '0' as ftype, c.com_id as id,c.parent_id,'' as ocom_id,c.com_code as code,c.com_name as name from tb_company c where c.enable_flag ='1' union all "
                          + "select '1' as ftype, o.org_id as id,o.parent_id,o.com_id as ocom_id,o.org_code as code,o.org_name as name from tb_company c,tb_organization o where  "
                          + " c.enable_flag ='1' and o.enable_flag='1' and c.com_id=o.com_id ";
            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询公司组织树", sqlobj);

            tvCompany.Nodes.Clear();
            //节点加上去
            if (ds.Tables[0].Rows.Count > 0)
            {
                //clsGetTree cls = new clsGetTree();
                CommonCtrl.InitTree(this.tvCompany.Nodes, "-1", ds.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.Nodes[0].Expand();
                }
            }
        }
        #endregion

        #region --按钮事件
        /// <summary>
        /// 转到添加页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_AddEvent(object sender, EventArgs e)
        {
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(null, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.wStatus = WindowStatus.Add;
            base.addUserControl(uc, "人员管理-新增", "UCPersonnelAdd", this.Tag.ToString(), this.Name);
            //订阅新增窗口保存事件
            uc.SaveEvent += new ClickHandler(uc_SaveEvent);
        }

        //新增以后重新刷新人员数据
        void uc_SaveEvent(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 转向 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_CopyEvent(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择复制记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvUser.CurrentRow.DataBoundItem as DataRowView).Row;
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(dr, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.wStatus = WindowStatus.Copy;
            uc.id = dr["user_id"].ToString();
            base.addUserControl(uc, "人员管理-复制", "UserCopy" + uc.id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_EditEvent(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvUser.CurrentRow.DataBoundItem as DataRowView).Row;
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(dr, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.wStatus = WindowStatus.Edit;
            uc.id = dr["user_id"].ToString();
            base.addUserControl(uc, "人员管理-编辑", "RoleEdit" + uc.id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_DeleteEvent(object sender, EventArgs e)
        {

            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["user_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.ShowQuestion("是否确认删除?"))
            {
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除人员", "sys_user", comField, "user_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvUser.Rows.Count > 0)
                    {
                        dgvUser.CurrentCell = dgvUser.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LocalCache._Update(CacheList.User);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (tvCompany.SelectedNode != null)
                {
                    DataRowView drv = tvCompany.SelectedNode.Tag as DataRowView;
                    if (drv["ftype"].ToString() == "0")//选中公司
                    {
                        where += string.Format(" and  com_id = '{0}'", drv["id"].ToString());
                    }
                    else//选中组织
                    {
                        SQLObj sobj = new SQLObj();
                        sobj.cmdType = CommandType.Text;
                        sobj.Param = new Dictionary<string, ParamObj>();
                        sobj.sqlString = " with tem as  (select org_id from tb_organization where org_id='" + drv["id"].ToString() + "' union all "
                             + " select o.org_id from tb_organization o,tem where o.parent_id=tem.org_id)   select * from tem ";
                        DataSet ds_ids = DBHelper.GetDataSet("", sobj);
                        if (ds_ids != null && ds_ids.Tables[0].Rows.Count > 0)
                        {
                            string ids = string.Empty;
                            foreach (DataRow row in ds_ids.Tables[0].Rows)
                            {
                                ids += "'" + row["org_id"] + "',";
                            }
                            if (ids != string.Empty)
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                                where += string.Format(" and  org_id in  ({0})", ids);
                            }
                        }
                        else
                        {
                            where += string.Format(" and  org_id = '{0}'", drv["id"].ToString());
                        }
                    }
                }
                if (!string.IsNullOrEmpty(txtuser_code.Caption.Trim()))//人员编码
                {
                    where += string.Format(" and  user_code like '%{0}%'", txtuser_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtidcard_num.Caption.Trim()))//证件号码
                {
                    where += string.Format(" and  idcard_num like '%{0}%'", txtidcard_num.Caption.Trim());
                }
                string status = CommonCtrl.IsNullToString(cbbstatus.SelectedValue);
                if (!string.IsNullOrEmpty(status)) //状态
                {
                    where += string.Format(" and  status = '{0}'", status);
                }
                if (!string.IsNullOrEmpty(txtuser_name.Caption.Trim()))//人员名称
                {
                    where += string.Format(" and  user_name like '%{0}%'", txtuser_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtuser_telephone.Caption.Trim()))//联系电话
                {
                    where += string.Format(" and  user_telephone like '%{0}%'", txtuser_telephone.Caption.Trim());
                }
                string is_operator = CommonCtrl.IsNullToString(cbbis_operator.SelectedValue);
                if (!string.IsNullOrEmpty(is_operator)) //状态
                {
                    where += string.Format(" and  is_operator = '{0}'", is_operator);
                }
                if (!string.IsNullOrEmpty(dtpentry_date.Value.ToString()) && !string.IsNullOrEmpty(dtpentry_date_end.Value.ToString()))
                {
                    where += string.Format(" and  create_time >= '{0}' and  create_time <= '{1}' ", Common.LocalDateTimeToUtcLong(dtpentry_date.Value).ToString(), Common.LocalDateTimeToUtcLong(dtpentry_date_end.Value).ToString());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页查询人员管理", "v_user", "*", obj.ToString(), "", "order by create_time desc",
                           page.PageIndex, page.PageSize, out this.recordCount);

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvUser.DataSource = obj;
            this.page.RecordCount = this.recordCount;

            this.myLock = true;
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
        private void dgvUser_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvUser.Rows[e.RowIndex].Cells[this.user_id.Name].Value.ToString();
                UCPersonnelView uc = new UCPersonnelView();
                uc.id = id;
                uc.uc = this;
                uc.wStatus = WindowStatus.View;
                base.addUserControl(uc, "人员管理-浏览", "PersonnelView" + id, this.Tag.ToString(), this.Name);
            }
        }
        #endregion

        #region --清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtuser_code.Caption = string.Empty;
            this.txtidcard_num.Caption = string.Empty;
            this.txtuser_name.Caption = string.Empty;
            this.txtuser_telephone.Caption = string.Empty;

            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);//是否操作员
        }
        #endregion
    }
}
