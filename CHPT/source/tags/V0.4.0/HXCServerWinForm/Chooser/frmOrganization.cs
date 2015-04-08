using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCServerWinForm.CommonClass;
using BLL;
using HXC_FuncUtility;
using ServiceStationClient.ComponentUI;

namespace HXCServerWinForm.Chooser
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
        #endregion
        public frmOrganization()
        {
            InitializeComponent();
        }

        private void frmOrganization_Load(object sender, EventArgs e)
        {
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
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-组织", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        private void GetData()
        {
            try
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
                DataSet ds = DBHelper.GetDataSet("查询组织-组织选择器", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlobj);
                dgvOrg.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-组织", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        private void dgvOrg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    this.Com_Name = dgvOrg.Rows[e.RowIndex].Cells["com_name"].Value.ToString();
                    this.Org_Name = dgvOrg.Rows[e.RowIndex].Cells["org_name"].Value.ToString();
                    this.Org_Id = dgvOrg.Rows[e.RowIndex].Cells["org_id"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("选择器-组织", ex);
                    MessageBoxEx.ShowWarning("程序异常");
                }
            }
        }
    }
}
