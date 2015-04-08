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
using ClientBLL;
using SYSModel;
using HXCPcClient.UCForm;
namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 客户选择器
    /// Author：JC
    /// AddTime：2014.9.29
    /// </summary>
    public partial class frmCustomerInfo : FormChooser
    {
        #region  外部属性
        /// <summary>
        /// 客户信息
        /// </summary>
        /// <remarks>
        /// create by kord
        /// </remarks>
        public CustomerInfo CustomerInfo { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string strCustomerId = string.Empty;
        /// <summary>
        /// 客户编码
        /// </summary>
        public string strCustomerNo = string.Empty;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string strCustomerName = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
       public string   strLegalPerson =string.Empty;
        /// <summary>
        /// 联系人手机
        /// </summary>
       public string strLegalPersonPhone = string.Empty;
        #endregion
        public frmCustomerInfo()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            CommonCtrl.BindComboBoxByDictionarr(cobCustType, "sys_credit_rating", true);
            BindTvCustom();
            BindPageData();
        }

        #region 清除事件
        /// <summary>
        /// 清除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomNo.Caption = string.Empty;
            txtCustomName.Caption = string.Empty;
            txtContract.Caption = string.Empty;
            cobCustType.SelectedValue = string.Empty;
            txtCustomNo.Focus();
        }
        #endregion

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

        #region 查询事件
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindPageData()
        {
            string strfileds = " a.cust_code as cust_code ,a.legal_person as legal_person,a.cust_short_name as cust_short_name,a.cust_name as cust_name, b.dic_code as dic_code ,b.dic_name as dic_name,c.credit_rating as credit_rating, a.cust_remark as cust_remark,a.cust_id";
            string strsql =" left join sys_dictionaries b on a.cust_type=b.dic_code left join ( select d.dic_name as credit_rating,d.dic_code from sys_dictionaries d) c on c.dic_code=a.credit_rating";

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
           DataTable dt = DBHelper.GetTableByPage("分页查询客户", " tb_customer a "+strsql, strfileds, strWhere, "", " order by a.cust_id desc", page.PageIndex, page.PageSize, out recordCount);           
            dgvCustom.DataSource = dt;
            page.RecordCount = recordCount;
        }
        #endregion

        #region 分页控件事件
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData();
        }
        #endregion

        #region DataGridView单机事件
        private void dgvCustom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {               
                strCustomerNo = dgvCustom.Rows[e.RowIndex].Cells["cust_code"].Value.ToString();
                strCustomerName = dgvCustom.Rows[e.RowIndex].Cells["cust_name"].Value.ToString();
                strCustomerId = dgvCustom.Rows[e.RowIndex].Cells["cust_id"].Value.ToString();
                strLegalPerson = dgvCustom.Rows[e.RowIndex].Cells["legal_person"].Value.ToString();
                CustomerInfo = new CustomerInfo(strCustomerId);
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion
    }


    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     CustomerInfo         
    /// Author:       Kord
    /// Date:         2014/10/30 10:55:34
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	客户档案信息
    ///***************************************************************************//
    public class CustomerInfo
    {
        #region Constructor - 构造函数
        public CustomerInfo()
        {
        }
        public CustomerInfo(String custId)
        {
            if (String.IsNullOrEmpty(custId)) return;

            var custInfo = DBHelper.GetTable("查询客户信息", "tb_customer", "*", "cust_id = '" + custId + "'", "", "");
            if (custInfo == null || custInfo.DefaultView.Count <= 0) return;

            CustId = custId;
            CustCode = custInfo.DefaultView[0]["cust_code"].ToString();
            CustName = custInfo.DefaultView[0]["cust_name"].ToString();
            LegalPerson = custInfo.DefaultView[0]["legal_person"].ToString();
            CustPhone = custInfo.DefaultView[0]["cust_phone"].ToString();
            EnterpriseNature = custInfo.DefaultView[0]["enterprise_nature"].ToString();
            CustAddress = custInfo.DefaultView[0]["cust_address"].ToString();
            SAPCode = custInfo.DefaultView[0]["sap_code"].ToString();
            //txt_cust_short_name.Caption = custInfo.DefaultView[0]["cust_short_name"].ToString();
            //txt_cust_quick_code.Caption = custInfo.DefaultView[0]["cust_quick_code"].ToString();
            //cbo_province.Text = custInfo.DefaultView[0]["province"].ToString();
            //cbo_city.Text = custInfo.DefaultView[0]["city"].ToString();
            //txt_cust_fax.Caption = custInfo.DefaultView[0]["cust_fax"].ToString();
            //cbo_county.Text = custInfo.DefaultView[0]["county"].ToString();
            //txt_cust_address.Caption = custInfo.DefaultView[0]["cust_address"].ToString();
            //txt_cust_email.Caption = custInfo.DefaultView[0]["cust_email"].ToString();
            //chk_is_member.Checked = custInfo.DefaultView[0]["is_member"].ToString() == "1";
            //txt_member_number.Caption = custInfo.DefaultView[0]["member_number"].ToString();
            //cbo_member_class.Text = custInfo.DefaultView[0]["member_class"].ToString();
        }
        #endregion

        #region Property - 属性
        /// <summary>
        /// ID
        /// </summary>
        public String CustId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public String CustCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public String CustName { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public String ZipCode { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public String CustPhone { get; set; }
        /// <summary>
        /// 联系人(法人)
        /// </summary>
        public String LegalPerson { get; set; }
        /// <summary>
        /// 性质
        /// </summary>
        public String EnterpriseNature { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public String CustAddress { get; set; }
        /// <summary>
        /// 客户SAP代码
        /// </summary>
        public String SAPCode { get; set; }
        #endregion
    }
}
