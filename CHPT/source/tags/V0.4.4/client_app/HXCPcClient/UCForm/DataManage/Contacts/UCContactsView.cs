using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient;
using Utility.Common;

namespace HXCPcClient.UCForm.DataManage.Contacts
{
    public partial class UCContactsView : UCBase
    {
        string contID;

        public UCContactsView(string contactsID)
        {
            InitializeComponent();
            this.contID = contactsID;
        }

        private void UCContactsView_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(WindowStatus.View);
            DataTable dt = DBHelper.GetTable("联系人预览", "v_contacts", string.Format("*,{0} phone,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), string.Format(" cont_id='{0}'", contID), "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                lblContEmail.Text = string.Empty;
                lblContName.Text = string.Empty;
                lblContPhone.Text = string.Empty;
                lblContPost.Text = string.Empty;
                lblCreateBy.Text = string.Empty;
                lblCreateTime.Text = string.Empty;
                lblNation.Text = string.Empty;
                lblRemark.Text = string.Empty;
                lblSex.Text = string.Empty;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                lblContEmail.Text = CommonCtrl.IsNullToString(dr["cont_email"]);
                lblContName.Text = CommonCtrl.IsNullToString(dr["cont_name"]);
                lblContPhone.Text = CommonCtrl.IsNullToString(dr["phone"]);
                lblContTel.Text = CommonCtrl.IsNullToString(dr["tel"]);
                lblContPost.Text = CommonCtrl.IsNullToString(dr["cont_post_name"]);
                lblCreateBy.Text = CommonCtrl.IsNullToString(dr["create_by_name"]);
                long createTime = Convert.ToInt64(CommonCtrl.IsNullToString(dr["create_time"]));
                lblCreateTime.Text = Common.UtcLongToLocalDateTime(createTime).ToString();
                lblNation.Text = CommonCtrl.IsNullToString(dr["nation_name"]);
                lblRemark.Text = CommonCtrl.IsNullToString(dr["remark"]);
                string sex=CommonCtrl.IsNullToString(dr["sex"]);
                lblSex.Text = sex == "1" ? "男" : "女";
                llblCustomer.Visible = CommonCtrl.IsNullToString(dr["cust_count"]).Length > 0;
                llblVehicle.Visible = CommonCtrl.IsNullToString(dr["vehi_count"]).Length > 0;
                llblSupplier.Visible = CommonCtrl.IsNullToString(dr["supp_count"]).Length > 0;
            }
            SetDataViewBtn();
        }

    }
}
