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
    /// 派工选择器
    /// Author：JC
    /// AddTime：2014.10.18
    /// </summary>
    public partial class frmDispatching : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 用户Id值
        /// </summary>
        public string strUserId = string.Empty;
        #endregion
        public frmDispatching()
        {
            InitializeComponent();
            dgvUser.ReadOnly = false;
            dgvUser.Columns["user_code"].ReadOnly = true;
            dgvUser.Columns["user_name"].ReadOnly = true;
            dgvUser.Columns["user_phone"].ReadOnly = true;            
            dgvUser.Columns["org_name"].ReadOnly = true;
            dgvUser.Columns["dic_name"].ReadOnly = true;
            dgvUser.Columns["remark"].ReadOnly = true;           
            dgvUser.Columns["user_id"].ReadOnly = true;
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
        }

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

        #region 绑定人员信息
        private void BindUserData()
        {
            string strWhere = " enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
            if (!string.IsNullOrEmpty(txtCode.Caption.Trim()))
            {
                strWhere += string.Format(" and user_code like '%{0}%'", txtCode.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtName.Caption.Trim()))
            {
                strWhere += string.Format(" and user_name like '%{0}%'", txtName.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(tvCompany.SelectedNode.Text.ToString())))
            {
                strWhere += string.Format(" and org_name like '%{0}%'", tvCompany.SelectedNode.Text.ToString());
            }
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("查询人员信息", "v_user", "*", strWhere, "", " order by user_id desc", page.PageIndex, page.PageSize, out recordCount);
            dgvUser.DataSource = dt;
            page.RecordCount = recordCount;          
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindUserData();
        }
        #endregion

        #region 窗体Load事件
        private void frmDispatching_Load(object sender, EventArgs e)
        {
            BindTree();
            BindUserData();
        }
        #endregion

        #region 查询功能
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindUserData();
        }
        #endregion

        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 确认功能
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Caption = string.Empty;
            txtName.Caption = string.Empty;
        }
        #endregion

        #region 左侧树选定后查询数据
        private void tvCompany_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvCompany.SelectedNode = e.Node;
            BindUserData();
        }
        #endregion

        #region 检测数据是否选中
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
                    strUserId += "'" + dr.Cells["user_id"].Value.ToString() + "'" + ",";
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择人员信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 单元格双击事件传递UserId
        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                strUserId = string.Empty;
                strUserId = "'" + CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["user_id"].Value) + "'" + ",";
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion


    }
}
