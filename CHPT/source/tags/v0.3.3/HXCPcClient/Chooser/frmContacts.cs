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

namespace HXCPcClient.Chooser
{
    public partial class frmContacts : FormEx
    {

        public string contID = string.Empty;//联系人ID
        public string contName = string.Empty;  //联系人名称
        public string crmId = string.Empty; //cur guid
        public string contDuty = string.Empty;  //职务
        public string contPhone = string.Empty;     //手机
        /// <summary> 是否可以多选
        /// </summary>
        public bool IsCanSelectMore = false;
        public frmContacts()
        {
            InitializeComponent();

            CommonCtrl.BindComboBoxByDictionarr(cbo_data_source, "sys_data_source", true);

            BinddgvContacts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Caption = string.Empty;
            txtTel.Caption = string.Empty;
            cbo_data_source.SelectedIndex = 0;
        }

        private void BinddgvContacts()
        {
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.Append("enable_flag='1'");//未删除标识
            string name = txtName.Caption.Trim();//姓名
            if (name.Length > 0)
            {
                sbWhere.AppendFormat(" and cont_name like '%{0}%'", name);
            }
            string tel = txtTel.Caption.Trim();//电话
            if (tel.Length > 0)
            {
                sbWhere.AppendFormat(" and cont_tel like '%{0}%'", tel);
            }
            if (cbo_data_source.SelectedValue != null && !String.IsNullOrEmpty(cbo_data_source.SelectedValue.ToString()))
            {
                sbWhere.AppendFormat(" and data_source like '%{0}%'", cbo_data_source.SelectedValue);
            }
            //DataTable dt = DBHelper.GetTable("联系人查询", "v_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), sbWhere.ToString(), "", "order by cont_name");
            int recordCount = 0;
            DataTable dt = DBHelper.GetTableByPage("联系人查询", "v_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), sbWhere.ToString(), "", "order by cont_name", page.PageIndex, page.PageSize, out recordCount);
            dgvContacts.RowCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvContacts.Rows[dgvContacts.Rows.Add()];
                dgvr.Cells["colContID"].Value = dr["cont_id"];
                dgvr.Cells["colContName"].Value = dr["cont_name"];
                dgvr.Cells["colSex"].Value = dr["sex"];
                dgvr.Cells["colContPost"].Value = dr["cont_post_name"];
                dgvr.Cells["colNation"].Value = dr["nation_name"];
                dgvr.Cells["colContPhone"].Value = dr["phone"];
                dgvr.Cells["colContTel"].Value = dr["cont_tel"];
                dgvr.Cells["col_cont_crm_guid"].Value = dr["cont_crm_guid"];

                string createTime = CommonCtrl.IsNullToString(dr["create_time"]);
                if (createTime.Length > 0)
                {
                    dgvr.Cells["colCreateTime"].Value = DateTime.MinValue.AddTicks(Convert.ToInt64(createTime)).ToLocalTime();
                }
            }
            page.RecordCount = recordCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BinddgvContacts();
        }

        //双击选择
        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRow();
        }

        private void SelectRow()
        {
            if (dgvContacts.CurrentRow == null)
            {
                return;
            }
            contID = CommonCtrl.IsNullToString(dgvContacts.CurrentRow.Cells["colContID"].Value);
            contName = CommonCtrl.IsNullToString(dgvContacts.CurrentRow.Cells["colContName"].Value);
            crmId = CommonCtrl.IsNullToString(dgvContacts.CurrentRow.Cells["col_cont_crm_guid"].Value);
            contDuty = CommonCtrl.IsNullToString(dgvContacts.CurrentRow.Cells["colContPost"].Value);
            contPhone = CommonCtrl.IsNullToString(dgvContacts.CurrentRow.Cells["colContPhone"].Value);

            if (contID.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BinddgvContacts();
        }


        //关闭
        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectRow();
            this.Close();
        }

        //快速新增联系人
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmContactsQuickADD contactsadd = new frmContactsQuickADD();
            contactsadd.SaveEvent += new SaveDelegate(contactsadd_SaveEvent);
            contactsadd.ShowDialog();
            //contactsadd.
        }

        //快速新增联系人保存事件的实现
        void contactsadd_SaveEvent(ContactsInfo contactsInfo)
        {
            contID = contactsInfo.contID;
            contName = contactsInfo.contName;
            crmId = contactsInfo.crmId;
            contDuty = contactsInfo.contDuty;
            contPhone = contactsInfo.contPhone;
            this.DialogResult = DialogResult.OK;
        }


    }

    /// <summary>
    /// 联系人信息(用于窗体保存事件传递数据)
    /// </summary>
    public struct ContactsInfo
    {
        /// <summary>
        /// 联系人ID
        /// </summary>
        public string contID;
        /// <summary>
        /// 联系人名称
        /// </summary>
        public string contName;
        /// <summary>
        /// cur guid
        /// </summary>
        public string crmId;
        /// <summary>
        /// 职务
        /// </summary>
        public string contDuty;
        /// <summary>
        /// 手机
        /// </summary>
        public string contPhone;
    }
}
