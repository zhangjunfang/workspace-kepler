using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.Chooser
{
    public partial class frmOrganization : FormChooser
    {
        #region 外部属性
        /// <summary> 公司名称
        /// </summary>
        public string Com_Name { get; set; }
        /// <summary> 组织名称
        /// </summary>
        public string Org_Name { get; set; }
        /// <summary> 
        /// 组织id
        /// </summary>
        public string Org_Id { get; set; }
        /// <summary> 组织是否可以多选
        /// </summary>
        public bool IsCanSelectMore = false;
        #endregion
        public frmOrganization()
        {
            InitializeComponent();
            dgvOrg.ReadOnly = false;

            foreach (DataGridViewColumn dgvc in dgvOrg.Columns)
            {
                if (dgvc.Name == this.colCheck.Name)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }

        private void frmOrganization_Load(object sender, EventArgs e)
        {
            if (!IsCanSelectMore)
            {
                dgvOrg.ShowCheckBox = false;
                dgvOrg.Columns[0].Visible = false;
            }
            GetData();
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtCom_code.Caption = "";
            this.txtcom_name.Caption = "";
            this.txtcontact_name.Caption = "";
            this.txtlegal_person.Caption = "";
            this.txtorg_code.Caption = "";
            this.txtorg_name.Caption = "";
            this.txtPhone.Caption = "";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            string strsql = "select c.com_code,c.com_name,o.org_code,o.org_name,o.org_id,c.legal_person,c.com_contact,c.com_tel,o.contact_name,o.contact_telephone "
                + " from tb_organization o left join tb_company c on o.com_id=c.com_id where c.enable_flag='1' and o.enable_flag='1' ";
            if (!string.IsNullOrEmpty(this.txtCom_code.Caption.Trim()))
            {
                strsql += " and c.com_code like '%" + this.txtCom_code.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtcom_name.Caption.Trim()))
            {
                strsql += " and c.com_name like '%" + this.txtcom_name.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtorg_code.Caption.Trim()))
            {
                strsql += " and o.org_code like '%" + this.txtorg_code.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtorg_name.Caption.Trim()))
            {
                strsql += " and o.org_name like '%" + this.txtorg_name.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtlegal_person.Caption.Trim()))
            {
                strsql += " and c.legal_person like '%" + this.txtlegal_person.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtcontact_name.Caption.Trim()))
            {
                strsql += " and c.com_contact like '%" + this.txtcontact_name.Caption.Trim() + "%' and o.contact_name like '%" + this.txtcontact_name.Caption.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(this.txtPhone.Caption.Trim()))
            {
                strsql += " and c.com_tel like '%" + this.txtPhone.Caption.Trim() + "%' and o.contact_telephone like '%" + this.txtPhone.Caption.Trim() + "%' ";
            }
            strsql += " order by c.com_name";

            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strsql;
            DataSet ds = DBHelper.GetDataSet("查询组织-组织选择器", sqlobj);
            dgvOrg.DataSource = ds.Tables[0].DefaultView;
        }

        private void dgvOrg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Com_Name = dgvOrg.Rows[e.RowIndex].Cells["com_name"].Value.ToString();
                this.Org_Name = dgvOrg.Rows[e.RowIndex].Cells["org_name"].Value.ToString();
                this.Org_Id = dgvOrg.Rows[e.RowIndex].Cells["org_id"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }

        #region 关闭按钮 2014.11.13-JC
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 确定事件 2014.11.13-JC
        private void buttonEx1_Click(object sender, EventArgs e)
        {
            if (IsCanSelectMore)
            {
                if (!IsCheck())
                {
                    return;
                }
                foreach (DataGridViewRow dr in dgvOrg.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        Org_Name += dr.Cells["org_name"].Value.ToString() + ",";
                        Org_Id += dr.Cells["org_id"].Value.ToString() + ",";
                    }
                }
            }
            else
            {
                if (dgvOrg.CurrentRow != null)
                {
                    this.Com_Name = dgvOrg.CurrentRow.Cells["com_name"].Value.ToString();
                    this.Org_Name = dgvOrg.CurrentRow.Cells["org_name"].Value.ToString();
                    this.Org_Id = dgvOrg.CurrentRow.Cells["org_id"].Value.ToString();
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 判断是否选择了数据
        /// <summary>
        /// 判断是否选择了数据
        /// </summary>
        /// <returns></returns>
        private bool IsCheck()
        {
            bool isOk = true;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvOrg.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["org_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择人员记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isOk = false;
            }
            return isOk;
        }
        #endregion
    }
}
