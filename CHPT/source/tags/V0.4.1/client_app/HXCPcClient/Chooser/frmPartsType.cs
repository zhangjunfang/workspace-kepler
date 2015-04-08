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

namespace HXCPcClient.Chooser
{
    public partial class frmPartsType : FormEx
    {
        public string TypeID = string.Empty;
        public string TypeName = string.Empty;
        public frmPartsType()
        {
            InitializeComponent();
        }

        private void frmPartsType_Load(object sender, EventArgs e)
        {
            tvData.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Text = "全部配件";
            root.Name = "";
            tvData.Nodes.Add(root);
            CreateTreeViewChildNode(root, "sys_parts_category");
            tvData.ExpandAll();
        }

        /// <summary>
        /// 创建绑定树节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentID">父编码</param>
        void CreateTreeViewChildNode(TreeNode parentNode, string parentID)
        {
            if (parentID.Length == 0)
            {
                return;
            }
            DataTable dt = CommonCtrl.GetDictByCode(parentID, false);
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode(CommonCtrl.IsNullToString(dr["dic_name"]));
                node.Name = CommonCtrl.IsNullToString(dr["dic_id"]);
                parentNode.Nodes.Add(node);
                string dicCode = CommonCtrl.IsNullToString(dr["dic_Code"]);
                CreateTreeViewChildNode(node, dicCode);
            }
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        private void CheckedNode()
        {
            if (tvData.SelectedNode == null)
            {
                return;
            }
            TypeID = CommonCtrl.IsNullToString(tvData.SelectedNode.Name);
            TypeName = tvData.SelectedNode.Text;
            if (TypeID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckedNode();
        }

        private void tvData_OnCustomDoubleClick(object sender, MouseEventArgs e)
        {
            CheckedNode();
        }


    }
}
