using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using HXCPcClient.CommonClass;
using System.Reflection;

namespace HXCPcClient.UCForm.DataManage.SupplierFile
{
    public partial class UCSupplierView : UCBase
    {
        #region 构造和载入
        public UCSupplierView(string supperId)
        {
            InitializeComponent();
            LoadInfo(supperId);
            GetAllContacts(supperId);
            ucAttr.TableName = "tb_supplier";
            ucAttr.TableNameKeyValue = supperId;
            ucAttr.BindAttachment();
            ucAttr.ReadOnly = true;
        }

        private void UCSupplierView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            ucAttr.Size = tabPage2.Size;
            btnSync.Enabled = false;
            SetDataViewBtn();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 加载供应商档案信息
        /// </summary>
        private void LoadInfo(string supperId)
        {
            if (!string.IsNullOrEmpty(supperId))
            {
                //1.加载供应商档案主信息
                DataTable dt = DBHelper.GetTable("查看一条供应商档案信息", "tb_supplier", "*", " sup_id='" + supperId + "'", "", "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    tb_supplier model = new tb_supplier();
                    CommonFuncCall.SetModlByDataTable(model, dt);
                    CommonFuncCall.SetShowControlValue(this, model, "View");

                    DataTable dt_bill = CommonFuncCall.GetDataTable();
                    if (!string.IsNullOrEmpty(model.sup_type))
                    {
                        lblsup_type.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, model.sup_type);
                    }
                    if (!string.IsNullOrEmpty(model.unit_properties))
                    {
                        lblunit_properties.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, model.unit_properties);
                    }
                    if (!string.IsNullOrEmpty(model.price_type))
                    {
                        lblprice_type.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, model.price_type);
                    }
                    if (!string.IsNullOrEmpty(model.credit_class))
                    {
                        lblcredit_class.Text = CommonFuncCall.GetBillNameByBillCode(dt_bill, model.credit_class);
                    }
                    if (!string.IsNullOrEmpty(model.county))
                    {
                        lblsup_address.Text = CommonFuncCall.GetAddress(model.county) + " " + lblsup_address.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.city))
                        {
                            lblsup_address.Text = CommonFuncCall.GetAddress(model.city) + " " + lblsup_address.Text;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(model.province))
                            {
                                lblsup_address.Text = CommonFuncCall.GetAddress(model.province) + " " + lblsup_address.Text;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取该供应商下所关联的所有联系人信息
        /// </summary>
        /// <param name="suppId"></param>
        /// <returns></returns>
        private void GetAllContacts(string suppId)
        {
            //string conId = string.Empty;
            //DataTable dt_contacts = new DataTable();
            //DataTable dt_base_contacts = DBHelper.GetTable("查询关联的联系人ID集合", "tr_base_contacts", "cont_id", " relation_object_id='" + suppId + "'", "", "");
            //if (dt_base_contacts != null && dt_base_contacts.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt_base_contacts.Rows)
            //    {
            //        conId += "'" + dr["cont_id"] + "',";
            //    }
            //    conId = conId.Trim(',');
            //    gvUserInfoList.DataSource = DBHelper.GetTable("查询关联的联系人信息", "tb_contacts", "*", " cont_id in (" + conId + ")", "", "");
            //}
            string conId = string.Empty;
            DataTable dt_base_contacts = DBHelper.GetTable("查询关联的联系人ID集合", "tr_base_contacts", "*", " relation_object_id='" + suppId + "'", "", "");
            if (dt_base_contacts != null && dt_base_contacts.Rows.Count > 0)
            {
                isdefault defaultModel = new isdefault();
                List<isdefault> list_default = new List<isdefault>();
                foreach (DataRow dr in dt_base_contacts.Rows)
                {
                    conId += "'" + dr["cont_id"] + "',";
                    defaultModel = new isdefault();
                    defaultModel.cont_id = dr["cont_id"].ToString();
                    defaultModel.is_default = dr["is_default"] == null || dr["is_default"].ToString() == "" ? "0" : dr["is_default"].ToString();
                    list_default.Add(defaultModel);
                }
                conId = conId.Trim(',');

                string TableName = string.Format(@"(select dic_name as con_post_name,tb_contacts.* from 
                                                    tb_contacts 
                                                    left join 
                                                    sys_dictionaries on 
                                                    tb_contacts.cont_post=sys_dictionaries.dic_id)
                                                     as tb_contacts");
                DataTable dt_contacts = DBHelper.GetTable("查询关联的联系人信息", TableName, string.Format("*,{0} phone,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), " cont_id in (" + conId + ")", "", "");
                foreach (DataRow dr in dt_contacts.Rows)
                {
                    DataGridViewRow dgvr = gvUserInfoList.Rows[gvUserInfoList.Rows.Add()];
                    dgvr.Cells["colcont_id"].Value = dr["cont_id"];
                    dgvr.Cells["colcont_name"].Value = dr["cont_name"];
                    dgvr.Cells["colcont_post"].Value = dr["con_post_name"];
                    dgvr.Cells["colcont_phone"].Value = dr["phone"];
                    dgvr.Cells["colcont_tel"].Value = dr["tel"];
                    dgvr.Cells["colremark"].Value = dr["remark"];
                    dgvr.Cells["colcont_email"].Value = dr["cont_email"];
                    if (list_default.Count > 0)
                    {
                        dgvr.Cells["colis_default"].Value = list_default.Where(p => p.cont_id == dr["cont_id"].ToString()).First().is_default;
                    }
                    string createTime = CommonCtrl.IsNullToString(dr["cont_birthday"]);
                    if (createTime.Length > 0)
                    {
                        dgvr.Cells["colcont_birthday"].Value = DateTime.MinValue.AddTicks(Convert.ToInt64(createTime)).ToLocalTime();
                    }
                }
            }
        }
        #endregion

        #region 附件控件大小
        private void tabPage2_SizeChanged(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
        }
        #endregion
        
    }
}
