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
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.SupplierFile
{
    public partial class UCSupplierView : UCBase
    {

        #region 字段属性
        tb_supplier model;

        UCSupplierManager uc;
        #endregion

        #region 构造和载入

        public UCSupplierView(string supperId, UCSupplierManager ParentFrm)
        {
            InitializeComponent();
            LoadInfo(supperId);
            GetAllContacts(supperId);
            ucAttr.TableName = "tb_supplier";
            ucAttr.TableNameKeyValue = supperId;
            ucAttr.BindAttachment();
            ucAttr.ReadOnly = true;
            uc = ParentFrm;
        }

        private void UCSupplierView_Load(object sender, EventArgs e)
        {
            //base.SetBtnStatus(WindowStatus.View);
            ucAttr.Size = tabPage2.Size;
            btnSync.Enabled = false;
            SetDataViewBtn();
            SubscribeEvent();
            SetBtnStatus();
        }


        #endregion

        #region 事件订阅处理

        private void SubscribeEvent()
        {
            EditEvent += new ClickHandler(UCSupplierView_EditEvent);
            DeleteEvent += new ClickHandler(UCSupplierView_DeleteEvent);
            StatusEvent += new ClickHandler(UCSupplierView_StatusEvent);
        }

        void UCSupplierView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCSupplierView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCSupplierView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }

            if (StatusSql())
            {
                MessageBoxEx.Show(btnStatus.Caption + "成功！");
                uc.BindgvSupplierList();
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        private bool StatusSql()
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "供应商", "tb_supplier", "sup_id", model.sup_id, status);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit()
        {
            UCSupplierAddOrEdit editFrm = new UCSupplierAddOrEdit(WindowStatus.Edit, model.sup_id, uc);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(editFrm, "供应商档案-编辑", "UCSupplierEdit" + model.sup_id, this.Tag.ToString(), this.Name);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (MessageBoxEx.Show("是否要删除当前数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Dictionary<string, string> updateField = new Dictionary<string, string>();
                updateField.Add("enable_flag", "0");
                if (DBHelper.Submit_AddOrEdit("删除供应商", "tb_supplier", "sup_id", model.sup_id, updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindgvSupplierList();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (model.status == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
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
                    model = new tb_supplier();
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
