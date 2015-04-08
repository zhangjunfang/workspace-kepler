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
    public partial class frmChooseCompany : FormChooser
    {
        #region 外部属性
        /// <summary> 公司id
        /// </summary>
        public string Company_ID { get; set; }
        /// <summary> 人员编码
        /// </summary>
        public string Company_Code { get; set; }
        /// <summary> 姓名
        /// </summary>
        public string Company_Name { get; set; }
        public string Sap_Code { get; set; }
        #endregion

        public frmChooseCompany()
        {
            InitializeComponent();
            BindPageData();
        }


        private string BuildString()
        {
            string where = string.Format(" enable_flag='1' ");//enable_flag 1未删除
            if (!string.IsNullOrEmpty(txtCom_code.Caption.Trim()))//公司编码
            {
                where += string.Format(" and  Com_code like '%{0}%'", txtCom_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtLegal_person.Caption.Trim()))//法人|负责人
            {
                where += string.Format(" and  Legal_person like '%{0}%'", txtLegal_person.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtrepair_qualification.Caption.Trim())) //维修资质 
            {
                where += string.Format(" and  repair_qualification like '%{0}%'", txtrepair_qualification.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtcom_short_name.Caption.Trim()))//公司简称
            {
                where += string.Format(" and  com_short_name like '%{0}%'", txtcom_short_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtcom_tel.Caption.Trim()))//联系电话
            {
                where += string.Format(" and  com_tel like '%{0}%'", txtcom_tel.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtcom_type.Caption.Trim()))//公司类型
            {
                where += string.Format(" and  com_type like '%{0}%'", txtcom_type.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtcom_name.Caption.Trim()))//公司全称
            {
                where += string.Format(" and  com_name like '%{0}%'", txtcom_name.Caption.Trim());
            }
            return where;
        }

        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                int recordCount;
                string where = BuildString();
                DataTable dt = DBHelper.GetTableByPage("分页查询公司", "tb_company", "*", where, "", " order by com_id ", winFormPager1.PageIndex, winFormPager1.PageSize, out recordCount);
                dgvCompany.DataSource = dt;
                winFormPager1.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            BindPageData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCom_code.Caption = string.Empty;
            txtLegal_person.Caption = string.Empty;
            txtrepair_qualification.Caption = string.Empty;
            txtcom_short_name.Caption = string.Empty;
            txtcom_tel.Caption = string.Empty;
            txtcom_type.Caption = string.Empty;
            txtcom_name.Caption = string.Empty;
        }

        private void dgvCompany_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Company_ID = dgvCompany.Rows[e.RowIndex].Cells["colcom_id"].Value.ToString();
                this.Company_Code = dgvCompany.Rows[e.RowIndex].Cells["colcom_code"].Value.ToString();
                this.Company_Name = dgvCompany.Rows[e.RowIndex].Cells["colcom_name"].Value.ToString();
                this.Sap_Code = dgvCompany.Rows[e.RowIndex].Cells[drtxt_sap_code.Name].Value.ToString();

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
