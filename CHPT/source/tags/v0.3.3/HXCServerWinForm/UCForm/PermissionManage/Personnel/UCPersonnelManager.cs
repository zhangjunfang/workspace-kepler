using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCServerWinForm.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using BLL;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm.Personnel
{
    /// <summary>
    /// 人员管理
    /// 孙明生
    /// </summary>
    public partial class UCPersonnelManager : UCBase
    {
        #region 属性
        private string where = string.Format(" enable_flag='1' ");//enable_flag 1未删除
        #endregion

        #region 初始化
        public UCPersonnelManager()
        {
            InitializeComponent();

            base.AddEvent += new ClickHandler(UCPersonnelManager_AddEvent);
            base.EditEvent += new ClickHandler(UCPersonnelManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCPersonnelManager_DeleteEvent);
        }
        #endregion

        #region Load
        private void UCPersonnelManager_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//角色按钮权限-是否隐藏
            base.SetBtnStatus(WindowStatus.Normal);
            dgvUser.ReadOnly = false;
            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);//是否操作员

            BindTree();
            DataGridViewEx.SetDataGridViewStyle(dgvUser);
        }
        #endregion

        #region 公司组织树
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
            DataSet ds = DBHelper.GetDataSet("查询公司组织树", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlobj);

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
            if (tvCompany.Nodes.Count > 0)
            {
                tvCompany.Nodes[0].Expand();
                tvCompany.SelectedNode = tvCompany.Nodes[0];
                tvCompany.SelectedNode.Expand();
            }
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 转到添加页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_AddEvent(object sender, EventArgs e)
        {
            UCPersonnelAddOrEdit UCPersonnelAddOrEdit = new UCPersonnelAddOrEdit();
            DataRowView drv = tvCompany.SelectedNode.Tag as DataRowView;
            if (drv == null)
            {
                MessageBoxEx.Show("请选择所属公司");
                return;
            }
            UCPersonnelAddOrEdit.comid = drv["id"].ToString();//选中公司
            UCPersonnelAddOrEdit.comName = drv["name"].ToString();//选中公司
            UCPersonnelAddOrEdit.uc = this;
            UCPersonnelAddOrEdit.wStatus = WindowStatus.Add;
            base.addUserControl(UCPersonnelAddOrEdit, "人员管理-新增", "UCPersonnelAdd", this.Tag.ToString(), this.Name);
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
            UCPersonnelAddOrEdit UCPersonnelAddOrEdit = new UCPersonnelAddOrEdit();
            UCPersonnelAddOrEdit.uc = this;
            UCPersonnelAddOrEdit.wStatus = WindowStatus.Edit;
            UCPersonnelAddOrEdit.id = dgvUser.CurrentRow.Cells["user_id"].Value.ToString();  //参数 人员管理ID
            base.addUserControl(UCPersonnelAddOrEdit, "人员管理-编辑", "RoleEdit" + UCPersonnelAddOrEdit.id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_DeleteEvent(object sender, EventArgs e)
        {
            try
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
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除人员", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_user", comField, "user_id", listField.ToArray());
                if (flag)
                {
                    BindPageData();
                    if (dgvUser.Rows.Count > 0)
                    {
                        dgvUser.CurrentCell = dgvUser.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindPageData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtuser_code.Caption = string.Empty;
            txtuser_name.Caption = string.Empty;
            txtidcard_num.Caption = string.Empty;
            txtuser_telephone.Caption = string.Empty;
            cbbstatus.SelectedIndex = 0;
            cbbis_operator.SelectedIndex = 0;
            dtpstart.Value = string.Empty;
            dtpend.Value = string.Empty;
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                where = string.Format(" user_code!='ServerAdmin' and enable_flag='1' ");//enable_flag 1未删除
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
                        DataSet ds_ids = DBHelper.GetDataSet("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sobj);
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

                int recordCount;
                DataTable dt = DBHelper.GetTableByPage("分页查询人员管理", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_user", "*", where, "", "order by user_id", page.PageIndex, page.PageSize, out recordCount);
                dgvUser.DataSource = dt;
                page.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region dgv事件
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
                string id = dgvUser.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
                UCPersonnelView uc = new UCPersonnelView();
                uc.id = id;
                uc.uc = this;
                uc.wStatus = WindowStatus.View;
                base.addUserControl(uc, "人员管理-浏览", "PersonnelView" + id, this.Tag.ToString(), this.Name);

            }
        }

        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = dgvUser.Columns[e.ColumnIndex].DataPropertyName;
            //if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            //{
            //    long ticks = (long)e.Value;
            //    e.Value = Common.UtcLongToLocalDateTime(ticks);
            //}
            //if (fieldNmae.Equals("enable_flag"))
            //{
            //    DataSources.EnableFlag enumEnableFlag = (DataSources.EnableFlag)Convert.ToInt16(e.Value.ToString());
            //    e.Value = DataSources.GetDescription(enumEnableFlag, true);
            //}
            //if (fieldNmae.Equals("data_sources"))
            //{
            //    DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
            //    e.Value = DataSources.GetDescription(enumDataSources, true);
            //}
            if (fieldNmae.Equals("is_operator"))
            {
                DataSources.EnumYesNo EnumYesNo = (DataSources.EnumYesNo)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(EnumYesNo, true);
            }
            if (fieldNmae.Equals("status"))//状态
            {
                SYSModel.DataSources.EnumStatus EnumStatus = (SYSModel.DataSources.EnumStatus)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(EnumStatus, true);
                //e.Value = e.Value.ToString() == "58b325d2-0792-4847-8e4a-22b3f25628f3" ? "启用" : "停用";
            }
        }
        #endregion

    }
}
