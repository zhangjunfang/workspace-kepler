using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using SYSModel;

namespace HXCPcClient.Chooser
{
    public partial class frmBtype : FormChooser
    {
        /// <summary>
        /// 往来单位ID
        /// </summary>
        public string BtypeID = string.Empty;
        /// <summary>
        /// 往来单位编码
        /// </summary>
        public string BtypeCode = string.Empty;
        /// <summary>
        /// 往来单位名称
        /// </summary>
        public string BtypeName = string.Empty;
        public frmBtype()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearchCust, btnClearCust);
            CommonCtrl.BindComboBoxByDictionarr(cobCustType, "sys_credit_rating", true);
        }
        private void frmBtype_Load(object sender, EventArgs e)
        {
            BindTvCustom();
            BindPageData();
            BindDataSup();
        }
        #region 供应商
        //清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContact.Caption = string.Empty;
            txtOther.Caption = string.Empty;
            txtSupperCode.Caption = string.Empty;
            txtSupperName.Caption = string.Empty;
            txtTelephone.Caption = string.Empty;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataSup();
        }
        //绑定供应商
        void BindDataSup()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendFormat(" enable_flag != 0 ");
            string supperCode = txtSupperCode.Caption.Trim();//供应商编码
            if (supperCode.Length > 0)
            {
                sbWhere.AppendFormat(" and sup_code like '%{0}%'", supperCode);
            }
            string supperName = txtSupperName.Caption.Trim();//供应商名称
            if (supperName.Length > 0)
            {
                sbWhere.AppendFormat(" and sup_full_name like '%{0}%'", supperName);
            }
            string tel = txtTelephone.Caption.Trim();
            if (tel.Length > 0)
            {
                sbWhere.AppendFormat(" and sup_tel like '%{0}%'", tel);
            }
            DataTable dt = DBHelper.GetTable("", "v_supplier_user", "*", sbWhere.ToString(), "", "");
            dgvSupper.DataSource = dt;
        }
        #endregion
        #region 客户
        #region 绑定左侧客户级别树方法
        /// <summary>
        /// 绑定左侧客户级别树方法
        /// </summary>
        private void BindTvCustom()
        {

            DataTable dt = DBHelper.GetTable("客户分类查询", "sys_dictionaries", "*", "dic_code='sys_customer_category'", "", "order by dic_id");

            CreateTreeView(tvCustom.Nodes, dt, "-1");
            if (dt.Rows.Count > 0)
            {
                tvCustom.Nodes[0].Expand();
            }
            tvCustom.SelectedNode = tvCustom.Nodes[0];
            tvCustom.ExpandAll();//设置所有节点均展开
        }
        #region 通过递归，将DataTable上的数据绑定到treeview上
        /// <summary>
        /// 通过递归，将数据绑定到treeview上
        /// </summary>
        /// <param name="nodes">控件上的节点</param>
        /// <param name="dt">数据库中查询到的table数据</param>
        /// <param name="parentId">表中父级ID</param>
        public void CreateTreeView(TreeNodeCollection nodes, DataTable dt, string parentId)
        {
            string filter;
            filter = string.Format("parent_id='{0}'", parentId);               //根据父级ID的值，进行查询
            DataRow[] dr = dt.Select(filter);
            TreeNode tn;
            foreach (DataRow dtr in dr)
            {
                tn = new TreeNode();
                tn.Text = dtr["dic_name"].ToString();                             //将分类名称绑定到节点上
                tn.Tag = dtr["dic_id"];                                     //将分类编号绑定到节点上  

                nodes.Add(tn);                                                       //将节点上的数据绑定到treeview上
                CreateTreeView(tn.Nodes, dt, tn.Tag.ToString());                           //重新调用此方法，绑定此节点下的加点
            }

        }
        #endregion

        #endregion
        private void btnClearCust_Click(object sender, EventArgs e)
        {
            txtCustomNo.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtContract.Caption = string.Empty;
            cobCustType.SelectedValue = string.Empty;
            txtCustomNo.Focus();
        }

        private void btnSearchCust_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindPageData()
        {
            string strfileds = " a.cust_code as cust_code ,a.legal_person as legal_person,a.cust_short_name as cust_short_name,a.cust_name as cust_name, b.dic_code as dic_code ,b.dic_name as dic_name,c.credit_rating as credit_rating, a.cust_remark as cust_remark,a.cust_id";
            string strsql = " left join sys_dictionaries b on a.cust_type=b.dic_code left join ( select d.dic_name as credit_rating,d.dic_code from sys_dictionaries d) c on c.dic_code=a.credit_rating";

            string strWhere = string.Empty;
            strWhere += " a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
            if (!string.IsNullOrEmpty(txtCustomNo.Caption.Trim()))
            {
                strWhere += string.Format(" and a.cust_code like '%{0}%'", txtCustomNo.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtCustomName.Caption.Trim()))
            {
                strWhere += string.Format(" and a.cust_name like '%{0}%'", txtCustomName.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtContract.Caption.Trim()))
            {
                strWhere += string.Format(" and a.legal_person like '%{0}%'", txtContract.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(cobCustType.SelectedValue.ToString()))
            {
                strWhere += string.Format(" and b.dic_id like '%{0}%'", cobCustType.SelectedValue.ToString());
            }
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询客户", " tb_customer a " + strsql, strfileds, strWhere, "", " order by a.cust_id desc", page.PageIndex, page.PageSize, out recordCount);
            dgvCustom.DataSource = dt;
            page.RecordCount = recordCount;
        }
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tcBtype.SelectedTab == tpCust)
            {
                SelectedCust();
            }
            else if (tcBtype.SelectedTab == tpSup)
            {
                SelectedSup();
            }
        }

        void SelectedCust()
        {
            if (dgvCustom.CurrentRow == null)
            {
                return;
            }
            BtypeCode = dgvCustom.CurrentRow.Cells["cust_code"].Value.ToString();
            BtypeName = dgvCustom.CurrentRow.Cells["cust_name"].Value.ToString();
            BtypeID = dgvCustom.CurrentRow.Cells["cust_id"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void SelectedSup()
        {
            if (dgvSupper.CurrentRow == null)
            {
                return;
            }
            BtypeID = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells["colSupperID"].Value);
            BtypeCode = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperCode.Name].Value);
            BtypeName = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperName.Name].Value);
            if (BtypeID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvSupper_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSup();
        }

        private void dgvCustom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedCust();
        }
    }
}
