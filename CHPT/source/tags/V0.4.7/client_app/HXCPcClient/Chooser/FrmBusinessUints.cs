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
using ServiceStationClient.ComponentUI;
using HXCPcClient.Chooser.CommonForm;
namespace HXCPcClient.Chooser
{
    public partial class FrmBusinessUints : FormChooser
    {

        public  string BusUntID = string.Empty;
        public string BusUntCode = string.Empty;
        public string BusUntName = string.Empty;
        private DataTable SuppTable = null;//供应商信息
        private DataTable CustTable = null;//客户信息
        private int SuppCounts = 0;//供应商记录数
        private int CustCounts = 0;//客户记录数
        public FrmBusinessUints()
        {
            InitializeComponent();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式
            BindTvCustom();//绑定客户分类
            BindTvSupplier();//绑定供应商分类
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContact.Caption = string.Empty;
            txtBusinUnitCode.Caption = string.Empty;
            txtBusinUnitName.Caption = string.Empty;
            txtTelephone.Caption = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                 string QueryStr=string.Empty;//查询条件
                 QueryStr += txtBusinUnitCode.Caption.Trim().ToString();
                 QueryStr += txtBusinUnitName.Caption.Trim().ToString();
                 QueryStr += txtContact.Caption.Trim().ToString();
                 QueryStr += txtTelephone.Caption.Trim().ToString();
                 //显示查询进度
                 ProgressBarFrm ProgFrm = new ProgressBarFrm();
                 ProgFrm.MaxNum = 100;
                 ProgFrm.ShowDialog();
                 BindSupData();//获取供应商信息
                 BindCustData();//获取客户信息
                 if (string.IsNullOrEmpty(QueryStr))
                 {              
                     //获取所有客户和供应商信息
                     GetSupMsg();
                     GetCustMsg();
                 }
                else
                 {
                     if (CustCounts == 0 && SuppCounts != 0)
                    {
                        GetSupMsg();//获取供应商信息
                    }
                     else if (SuppCounts == 0 && CustCounts != 0)
                     {
                         GetCustMsg();//获取客户信息
                     }
                     else
                     {
                         //获取所有客户和供应商信息
                         GetSupMsg();
                         GetCustMsg();
                     }
                 }


            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }

        }
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        private void GetSupMsg()
        {
            try
            {
                if (dgvBusinessUnits.Rows.Count != 0) dgvBusinessUnits.Rows.Clear();//清空原始数据
                for (int i = 0; i < SuppTable.Rows.Count; i++)
                {
                    DataGridViewRow dgRow = dgvBusinessUnits.Rows[dgvBusinessUnits.Rows.Add()];//创建新行项
                    dgRow.Cells["BusinUntID"].Value = SuppTable.Rows[i]["sup_id"].ToString();
                    dgRow.Cells["BusinUntCode"].Value = SuppTable.Rows[i]["sup_code"].ToString();
                    dgRow.Cells["BusinUntName"].Value = SuppTable.Rows[i]["sup_full_name"].ToString();
                    dgRow.Cells["BusUntAddress"].Value = SuppTable.Rows[i]["sup_address"].ToString();
                    dgRow.Cells["Contact"].Value = SuppTable.Rows[i]["cont_name"].ToString();
                    dgRow.Cells["Telephone"].Value = SuppTable.Rows[i]["cont_tel"].ToString();
                    dgRow.Cells["MobilePhone"].Value = SuppTable.Rows[i]["cont_phone"].ToString();
                    dgRow.Cells["BusUntEmail"].Value = SuppTable.Rows[i]["cont_email"].ToString();
                    dgRow.Cells["Fax"].Value = SuppTable.Rows[i]["sup_fax"].ToString();//传真
                    dgRow.Cells["ZipCode"].Value = SuppTable.Rows[i]["zip_code"].ToString();//邮编
                }
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        private void GetCustMsg()
        {
            try
            {
                if (dgvBusinessUnits.Rows.Count != 0) dgvBusinessUnits.Rows.Clear();
                for (int i = 0; i < CustTable.Rows.Count; i++)
                {
                    DataGridViewRow dgRow = dgvBusinessUnits.Rows[dgvBusinessUnits.Rows.Add()];//创建新行项
                    dgRow.Cells["BusinUntID"].Value = CustTable.Rows[i]["cust_id"].ToString();
                    dgRow.Cells["BusinUntCode"].Value = CustTable.Rows[i]["cust_code"].ToString();
                    dgRow.Cells["BusinUntName"].Value = CustTable.Rows[i]["cust_name"].ToString();
                    dgRow.Cells["BusUntAddress"].Value = CustTable.Rows[i]["cust_address"].ToString();
                    dgRow.Cells["Contact"].Value = CustTable.Rows[i]["cont_name"].ToString();
                    dgRow.Cells["Telephone"].Value = CustTable.Rows[i]["cont_tel"].ToString();
                    dgRow.Cells["MobilePhone"].Value = CustTable.Rows[i]["cont_phone"].ToString();
                    dgRow.Cells["BusUntEmail"].Value = CustTable.Rows[i]["cont_email"].ToString();
                    dgRow.Cells["Fax"].Value = CustTable.Rows[i]["cust_fax"].ToString();//传真
                    dgRow.Cells["ZipCode"].Value = CustTable.Rows[i]["zip_code"].ToString();//邮编
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        #region 绑定左侧往来单位级别树方法
        /// <summary>
        /// 绑定左侧客户级别树方法
        /// </summary>
        private void BindTvCustom()
        {
            try
            {

                DataTable dt = DBHelper.GetTable("客户分类查询", "sys_dictionaries", "*", "dic_code='sys_customer_category'", "", "order by dic_id");

                CreateTreeView(TreeVBusinUnt.Nodes, dt, "-1");
                if (dt.Rows.Count > 0)
                {
                    TreeVBusinUnt.Nodes[0].Expand();
                }
                TreeVBusinUnt.SelectedNode = TreeVBusinUnt.Nodes[0];
                TreeVBusinUnt.ExpandAll();//设置所有节点均展开
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }
        /// <summary>
        /// 绑定左侧供应商级别树方法
        /// </summary>
        private void BindTvSupplier()
        {
            try
            {

                DataTable dt = DBHelper.GetTable("供应商分类查询", "sys_dictionaries", "*", "dic_code='sys_supplier_category'", "", "order by dic_id");

                CreateTreeView(TreeVBusinUnt.Nodes, dt, "-1");
                if (dt.Rows.Count > 0)
                {
                    TreeVBusinUnt.Nodes[0].Expand();
                }
                TreeVBusinUnt.SelectedNode = TreeVBusinUnt.Nodes[0];
                TreeVBusinUnt.ExpandAll();//设置所有节点均展开
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }
        }
        #endregion
        #region 通过递归，将DataTable上的数据绑定到treeview上
        /// <summary>
        /// 通过递归，将数据绑定到treeview上
        /// </summary>
        /// <param name="nodes">控件上的节点</param>
        /// <param name="dt">数据库中查询到的table数据</param>
        /// <param name="parentId">表中父级ID</param>
        public void CreateTreeView(TreeNodeCollection nodes, DataTable dt, string parentId)
        {
            try
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
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

            }

        }

       private void   BindSupData()
        {
           try
           {
                StringBuilder sbWhere = new StringBuilder();
                sbWhere.AppendFormat(" enable_flag != 0 ");
                string BusUntCode =string.Empty;//往来单位编码
                string BusUntName = string.Empty;//往来单位名称
                if (!string.IsNullOrEmpty(txtBusinUnitCode.Caption.ToString()))
                {
                    BusUntCode = txtBusinUnitCode.Caption.Trim();//往来单位编码
                    sbWhere.AppendFormat(" and sup_code like '%{0}%'", BusUntCode);
                }
                if (!string.IsNullOrEmpty(txtBusinUnitName.Caption.ToString()))
                {
                    BusUntName = txtBusinUnitName.Caption.Trim();//往来单位名称
                    sbWhere.AppendFormat(" and sup_full_name like '%{0}%'", BusUntName);
                }
                if (!string.IsNullOrEmpty(QueryWhere()))
                {
                    sbWhere.AppendFormat(QueryWhere());
                }

                SuppTable = DBHelper.GetTable("", "V_SupplierMsg", "*", sbWhere.ToString(), "", "");
                if(SuppTable.Rows.Count==0)return;
                else
                {
                    SuppCounts = SuppTable.Rows.Count;//获取供应商记录数                   
                }
                
            
            }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            
           }
        }
       private  void  BindCustData()
        {
           try
           {
                string BusUntCode = string.Empty;
                string BusUntName = string.Empty;
                StringBuilder sbWhere = new StringBuilder();
                sbWhere.AppendFormat(" enable_flag != 0 ");
                if (!string.IsNullOrEmpty(txtBusinUnitCode.Caption.ToString()))
                {
                     BusUntCode = txtBusinUnitCode.Caption.Trim();//往来单位编码
                     sbWhere.AppendFormat(" and cust_code like '%{0}%'", BusUntCode);
                }
                if (!string.IsNullOrEmpty(txtBusinUnitName.Caption.ToString()))
                {
                     BusUntName = txtBusinUnitName.Caption.Trim();//往来单位名称
                     sbWhere.AppendFormat(" and cust_name like '%{0}%'", BusUntName);
                }
                if (!string.IsNullOrEmpty(QueryWhere()))
                {
                    sbWhere.AppendFormat(QueryWhere());
                }

                CustTable = DBHelper.GetTable("", "V_CustomerMsg", "*", sbWhere.ToString(), "", "");
                if (CustTable.Rows.Count == 0) return;
                else
                {
                    CustCounts = CustTable.Rows.Count;//获取客户记录数
                }

           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);

           }
        }

        private void dgvSupper_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedBusinUnit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedBusinUnit();
        }
        /// <summary>
        /// 获取选中项的往来单位信息
        /// </summary>
       private  void SelectedBusinUnit()
        {
           try
           {
                if (dgvBusinessUnits.CurrentRow == null)
                {
                    return;
                }
                BusUntID = CommonCtrl.IsNullToString(dgvBusinessUnits.CurrentRow.Cells["BusinUntID"].Value.ToString());
                BusUntCode = CommonCtrl.IsNullToString(dgvBusinessUnits.CurrentRow.Cells["BusinUntCode"].Value.ToString());
                BusUntName = CommonCtrl.IsNullToString(dgvBusinessUnits.CurrentRow.Cells["BusinUntName"].Value.ToString());
                if (BusUntID.Length > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
           }
           catch (Exception ex)
           {
               MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            
           }
        }
       private string QueryWhere()
       { 
         try
         {
             StringBuilder sbWhere = new StringBuilder();
             if (!string.IsNullOrEmpty(txtContact.Caption.ToString()))
             {
                 sbWhere.AppendFormat(" and cont_name like '%{0}%'", txtContact.Caption.Trim().ToString());

             }
             if (!string.IsNullOrEmpty(txtTelephone.Caption.ToString()))
             {
                 sbWhere.AppendFormat(" and cont_phone like '%{0}%'", txtTelephone.Caption.Trim().ToString());

             }
             return sbWhere.ToString();
         }
         catch (Exception ex)
         {
             MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
             return null;
         }

       }
        #endregion
    }
}
