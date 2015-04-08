using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using HXCPcClient.UCForm.DataManage.SupplierFile;
using ServiceStationClient.ComponentUI;
using HXCPcClient.UCForm;
using System.Collections;

namespace HXCPcClient.Chooser
{
    public partial class frmSupplier : FormChooser
    {
        public string supperID = string.Empty;
        public string supperCode = string.Empty;
        public string supperName = string.Empty;
        public frmSupplier()
        {
            InitializeComponent();
            //绑定企业性质类型
            CommonFuncCall.BindComBoxDataSource(ddlCompanyNature, "sys_enterprise_property", "全部");
            //绑定企业性质类型
            CommonCtrl.BindComboBoxByDictionarr(unit_properties, "sys_enterprise_property");

            CommonCtrl.BindComboBoxByDictionarr(sup_type, "sys_supplier_category");
            CreatPropertiesTree();
            ddlCompanyNature.SelectedIndex = 0;
        }

        private void CreatPropertiesTree()
        {
            DataTable dtproperty = CommonFuncCall.GetDictionariesByDic_codes("sys_enterprise_property");
            TreeNode tmpNd;
            if (dtproperty != null)
            {
                foreach (DataRow drv in dtproperty.Rows)
                {
                    tmpNd = new TreeNode();
                    tmpNd.Tag = drv;
                    tmpNd.Text = drv["dic_name"].ToString(); //name
                    //tmpNd.Name = drv["dic_id"].ToString();//id
                    tmpNd.Name = string.Empty;//id
                    this.tvNature.Nodes.Add(tmpNd);
                }
            }
            ArrayList dic_name = new ArrayList();
            dic_name.Add("sys_enterprise_property");
            dtproperty = CommonFuncCall.GetDictionariesByPDic_codes(dic_name);
            if (dtproperty != null)
            {
                foreach (DataRow drv in dtproperty.Rows)
                {
                    tmpNd = new TreeNode();
                    tmpNd.Tag = drv;
                    tmpNd.Text = drv["dic_name"].ToString(); //name
                    tmpNd.Name = drv["dic_id"].ToString();//id
                    this.tvNature.Nodes[0].Nodes.Add(tmpNd);
                }
            }
            this.tvNature.Nodes[0].Expand();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContact.Caption = string.Empty;
            txtOther.Caption = string.Empty;
            txtSupperCode.Caption = string.Empty;
            txtSupperName.Caption = string.Empty;
            txtTelephone.Caption = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            StringBuilder sbWhere = new StringBuilder();
            //sbWhere.AppendFormat(" enable_flag != 0 ");
            sbWhere.AppendFormat(" enable_flag != 0  and status='1' ");
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
            ////联系人
            if (!string.IsNullOrEmpty(txtContact.Caption.Trim()))
            {
                //Str_Where += "and sup_id in(select relation_object_id from tr_base_contacts  where cont_id='" + txtSupplierUser.Caption.Trim() + "'";
                sbWhere.AppendFormat(" and sup_id in(select b.relation_object_id from dbo.tr_base_contacts b where b.cont_id in ( select c.cont_id from tb_contacts c where c.cont_name like '%" + txtContact.Caption.Trim() + "%'))");
            }
            //企业性质
            if (tvNature.SelectedNode != null && !string.IsNullOrEmpty(tvNature.SelectedNode.Name.ToString()))
            {
                sbWhere.AppendFormat(" and unit_properties='" + tvNature.SelectedNode.Name.ToString() + "'");
            }

            DataTable dt = DBHelper.GetTable("", "v_supplier_user", "*", sbWhere.ToString(), "", "");
            dgvSupper.DataSource = dt;
        }

        private void dgvSupper_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSupper();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedSupper();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            SelectedSupper();
        }

        void SelectedSupper()
        {
            if (dgvSupper.CurrentRow == null)
            {
                return;
            }
            supperID = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells["colSupperID"].Value);
            supperCode = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperCode.Name].Value);
            supperName = CommonCtrl.IsNullToString(dgvSupper.CurrentRow.Cells[colSupperName.Name].Value);
            if (supperID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnname_Click(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion("新增供应商会关闭当前页面，确认要新增吗？"))
            {
                return;
            }
            if (GlobalStaticObj.AppMainForm is HXCMainForm)
            {
                UCSupplierAddOrEdit UCSupplierAdd = new UCSupplierAddOrEdit(WindowStatus.Add, null, null);
                string tag = "UCSupplierAdd|CL_DataManagement_BasicData|CL_DataManagement_BasicData_Accessories";
                ((HXCMainForm)GlobalStaticObj.AppMainForm).addUserControls(UCSupplierAdd, "供应商档案-新增", "UCSupplierAdd", tag, "");

            }
            this.Close();


        }


    }
}
