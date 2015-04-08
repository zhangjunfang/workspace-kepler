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
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.Contacts
{
    public partial class UCContactsView : UCBase
    {

        #region 字段属性
        string contID;

        UCContactsManage uc;

        DataRow contactsInfo;
        #endregion

        #region 构造和载入方法

        public UCContactsView(string contactsID, UCContactsManage ParentFrm)
        {
            InitializeComponent();
            this.contID = contactsID;
            SubscribeEvent();
            uc = ParentFrm;
        }

        private void UCContactsView_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(WindowStatus.View);
            BindData();
            SetDataViewBtn();
            SetBtnStatus();
        }
        #endregion

        #region 事件订阅和处理
        /// <summary>
        /// 订阅事件
        /// </summary>
        private void SubscribeEvent()
        {
            EditEvent += new ClickHandler(UCContactsView_EditEvent);
            DeleteEvent += new ClickHandler(UCContactsView_DeleteEvent);
            StatusEvent += new ClickHandler(UCContactsView_StatusEvent);
        }

        void UCContactsView_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        void UCContactsView_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCContactsView_EditEvent(object sender, EventArgs e)
        {
            Edit();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (contactsInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
                uc.BindData(contID);
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (contactsInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
            if (contactsInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "联系人", "tb_contacts", "cont_id", contactsInfo["cont_id"].ToString(), status);
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
                if (DBHelper.Submit_AddOrEdit("删除联系人", "tb_contacts", "cont_id", contID, updateField))
                {
                    MessageBoxEx.Show("删除成功！");
                    uc.BindData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit()
        {
            UCContactsAddOrEdit partsEdit = new UCContactsAddOrEdit(WindowStatus.Edit, contID, uc);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(partsEdit, "联系人档案-编辑", "UCVehicleModelsEdit" + contID, this.Tag.ToString(), this.Name);
        }

        #endregion

        #region 按钮状态

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (contactsInfo["data_source"].ToString() != ((int)DataSources.EnumDataSources.SELFBUILD).ToString())
            {
                btnStatus.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                return;
            }
            if (contactsInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                btnStatus.Caption = "停用";
            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

        #region 数据绑定
        private void BindData()
        {
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
                contactsInfo = dt.Rows[0];
                lblContEmail.Text = CommonCtrl.IsNullToString(contactsInfo["cont_email"]);
                lblContName.Text = CommonCtrl.IsNullToString(contactsInfo["cont_name"]);
                lblContPhone.Text = CommonCtrl.IsNullToString(contactsInfo["phone"]);
                lblContTel.Text = CommonCtrl.IsNullToString(contactsInfo["tel"]);
                lblContPost.Text = CommonCtrl.IsNullToString(contactsInfo["cont_post_name"]);
                lblCreateBy.Text = CommonCtrl.IsNullToString(contactsInfo["create_by_name"]);
                long createTime = Convert.ToInt64(CommonCtrl.IsNullToString(contactsInfo["create_time"]));
                lblCreateTime.Text = Common.UtcLongToLocalDateTime(createTime).ToString();
                lblNation.Text = CommonCtrl.IsNullToString(contactsInfo["nation_name"]);
                lblRemark.Text = CommonCtrl.IsNullToString(contactsInfo["remark"]);
                string sex = CommonCtrl.IsNullToString(contactsInfo["sex"]);
                lblSex.Text = sex == "1" ? "男" : "女";
                llblCustomer.Visible = CommonCtrl.IsNullToString(contactsInfo["cust_count"]).Length > 0;
                llblVehicle.Visible = CommonCtrl.IsNullToString(contactsInfo["vehi_count"]).Length > 0;
                llblSupplier.Visible = CommonCtrl.IsNullToString(contactsInfo["supp_count"]).Length > 0;
            }
        }
        #endregion

    }
}
