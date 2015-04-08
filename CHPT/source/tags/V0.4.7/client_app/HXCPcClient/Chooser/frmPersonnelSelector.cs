using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using HXCPcClient.UCForm;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 人员选择器
    /// Author：JC
    /// AddTime：2014.10.28
    /// </summary>
    public partial class frmPersonnelSelector : FormChooser
    {
        #region  外部属性
        /// <summary>
        /// 人员编码
        /// </summary>
        public string strPersonCode = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        public string strPersonName = string.Empty;
        /// <summary>
        /// 部门
        /// </summary>
        public string strDepartment = string.Empty;
        /// <summary>
        /// 班组
        /// </summary>
        public string strDiName = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark = string.Empty;
        /// <summary>
        /// 人员Id
        /// </summary>
        public string strUserId = string.Empty;
        /// <summary>
        /// 是否允许多选-维修进度查询用
        /// </summary>
        public bool isMoreCheck = false;
        #endregion
        public frmPersonnelSelector()
        {
            InitializeComponent();
            this.AcceptButton = btnSearch;
            dgvUser.ReadOnly = false;
            dgvUser.Columns["user_code"].ReadOnly = true;
            dgvUser.Columns["user_name"].ReadOnly = true;
            dgvUser.Columns["post"].ReadOnly = true;
            dgvUser.Columns["user_phone"].ReadOnly = true;
            dgvUser.Columns["org_name"].ReadOnly = true;
            dgvUser.Columns["dic_name"].ReadOnly = true;
            dgvUser.Columns["remark"].ReadOnly = true;
           // dgvUser.Columns["vehicle_model"].ReadOnly = true;
            dgvUser.Columns["user_id"].ReadOnly = true;
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式    
        }

        #region 清除事件
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Caption = string.Empty;
            txtName.Caption = string.Empty;
        }
        #endregion

        #region 查询功能
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindUserData();
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
                    tvCompany.SelectedNode = tvCompany.Nodes[0];
                }
            }
        }
        #endregion

        #region 窗体Load事件
        private void frmPersonnelSelector_Load(object sender, EventArgs e)
        {
            BindTree();
            BindUserData();
        }
        #endregion

        #region 绑定人员信息
        private void BindUserData()
        {
            string strWhere = "enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
            if (!string.IsNullOrEmpty(txtName.Caption.Trim()))
            {
                strWhere += string.Format(" and  user_name like '%{0}%'", txtName.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtCode.Caption.Trim()))
            {
                strWhere += string.Format(" and  user_code like '%{0}%'", txtCode.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(tvCompany.SelectedNode.Text.ToString())))
            {
                strWhere += string.Format(" and org_name like '%{0}%'", tvCompany.SelectedNode.Text.ToString());
            }
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("查询人员信息", " v_user", "*", strWhere, "", " order by user_id desc", page.PageIndex, page.PageSize, out recordCount);
            dgvUser.DataSource = dt;
            page.RecordCount = recordCount;
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindUserData();
        }
        #endregion

        #region 关闭按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 当页保存
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!IsCheck())
            {
                return;
            }
            strPersonCode = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_code"].Value);
            strPersonName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_name"].Value);
            strDepartment = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["org_name"].Value);
            strDiName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["dic_name"].Value);
            strRemark = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["remark"].Value);
            strUserId = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_id"].Value);
            this.DialogResult = DialogResult.OK;
            txtCode.Caption = string.Empty;
            txtName.Caption = string.Empty;
        }
        #endregion

        #region 确定
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.cbAll.Checked)
            {
                this.strPersonName = "[全部],";
                this.strUserId = "[全部],";
            }
            else
            {
                if (!isMoreCheck)
                {
                    if (!IsCheck())
                    {
                        return;
                    }
                    strPersonCode = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_code"].Value);
                    strPersonName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_name"].Value);
                    strDepartment = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["org_name"].Value);
                    strDiName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["dic_name"].Value);
                    strRemark = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["remark"].Value);
                    strUserId = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_id"].Value);
                }
                else
                {
                    foreach (DataGridViewRow dr in dgvUser.Rows)
                    {
                        object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                        if (isCheck != null && (bool)isCheck)
                        {
                            strPersonName += dr.Cells["user_name"].Value.ToString() + ",";
                            strUserId += dr.Cells["user_id"].Value.ToString() + ",";
                        }
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 单元格双击功能
        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                strPersonCode =CommonCtrl.IsNullToString( dgvUser.Rows[e.RowIndex].Cells["user_code"].Value);
                strPersonName = CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["user_name"].Value);
                strDepartment = CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["org_name"].Value);
                strDiName = CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["dic_name"].Value);
                strRemark = CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["remark"].Value);
                strUserId = CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["user_id"].Value);
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region 判断是否选择了一条数据
        /// <summary>
        /// 判断是否选择了一条数据
        /// </summary>
        /// <returns></returns>
        private bool IsCheck()
        {
            bool isOk = false;
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
                MessageBoxEx.Show("请选择人员记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能选择1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 左侧树选定后查询数据
        private void tvCompany_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvCompany.SelectedNode = e.Node;
            BindUserData();
        }
        #endregion

        #region 选择行
        private void dgvUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvUser.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvUser.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion

        #region 回车事件
        private void dgvUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                strPersonCode = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_code"].Value);
                strPersonName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_name"].Value);
                strDepartment = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["org_name"].Value);
                strDiName = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["dic_name"].Value);
                strRemark = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["remark"].Value);
                strUserId = CommonCtrl.IsNullToString(dgvUser.CurrentRow.Cells["user_id"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion

    }
}
