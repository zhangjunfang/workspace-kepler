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
    public partial class frmVehicleModels : FormEx
    {
        /// <summary>
        /// 车型ID
        /// </summary>
        public string VMID = string.Empty;
        public string VMName = string.Empty;
        /// <summary>
        /// 车型分类
        /// </summary>
        public String VMClass { get; set; }
        public frmVehicleModels()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVehicleModels_Load(object sender, EventArgs e)
        {
            BindTree();
        }
        //绑定树控件
        private void BindTree()
        {
            string where = string.Format("enable_flag='{0}' and status='{1}'", (int)DataSources.EnumEnableFlag.USING, (int)DataSources.EnumStatus.Start);
            DataTable dtModels = DBHelper.GetTable("", "tb_vehicle_models", "vm_id,vm_name,vm_class", where, "", "order by vm_name");
            foreach (DataRow dr in dtModels.Rows)
            {
                TreeNode node = new TreeNode(CommonCtrl.IsNullToString(dr["vm_name"]));
                node.Name = CommonCtrl.IsNullToString(dr["vm_id"]);
                node.Tag = CommonCtrl.IsNullToString(dr["vm_class"]);
                tvModels.Nodes.Add(node);
            }
        }

        //选择节点
        private void CheckedNode()
        {
            if (tvModels.SelectedNode == null)
            {
                return;
            }
            VMID = tvModels.SelectedNode.Name;
            VMName = tvModels.SelectedNode.Text;
            VMClass = CommonCtrl.IsNullToString(tvModels.SelectedNode.Tag);
            if (VMID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckedNode();
        }


        private void tvModels_OnCustomDoubleClick(object sender, MouseEventArgs e)
        {
            CheckedNode();
        }
    }
}
