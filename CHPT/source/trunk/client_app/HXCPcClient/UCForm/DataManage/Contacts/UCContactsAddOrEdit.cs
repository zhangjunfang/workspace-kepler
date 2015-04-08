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
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.DataManage.Contacts
{
    public partial class UCContactsAddOrEdit : UCBase
    {

        #region 字段属性

        string contID;
        UCContactsManage uc;
        DataRow contactsInfo;
        #endregion

        #region 构造载入函数
        public UCContactsAddOrEdit(WindowStatus status, string contactsId, UCContactsManage uc)
        {
            InitializeComponent();
            this.windowStatus = status;
            this.contID = contactsId;
            base.SaveEvent += new ClickHandler(UCContactsAddOrEdit_SaveEvent);
            base.StatusEvent += new ClickHandler(UCContactsAddOrEdit_StatusEvent);
            base.DeleteEvent += new ClickHandler(UCContactsAddOrEdit_DeleteEvent);
            CancelEvent += new ClickHandler(UCContactsAddOrEdit_CancelEvent);
            this.uc = uc;
        }


        //加载
        private void UCContactsAddOrEdit_Load(object sender, EventArgs e)
        {
            //SetBtnStatus(status);
            //var btnCols = new ObservableCollection<ButtonEx_sms>()
            //{
            //    btnSave, btnImport, btnExport, btnSet, btnView, btnPrint,btnCancel
            //};
            //UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            CommonCtrl.BindComboBoxByDictionarr(cboNation, "nation_yt", false);//绑定民族
            CommonCtrl.BindComboBoxByDictionarr(cboContPost, "sys_position", false);//绑定职务
            cboNation.SelectedIndex = 27;
            //如果是编辑或者复制,则先绑定数据
            if ((windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy) && contID.Length > 0)
            {
                BindData();
            }
            SetBtnStatus();
        }

        /// <summary>
        /// 设置页面按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (windowStatus == WindowStatus.Edit)
            {
                SetDataEditBtn();
                if (contactsInfo["status"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "停用";
                }
                else
                {
                    btnStatus.Caption = "启用";
                }
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetDataAddBtn();
            }
        }
        #endregion

        #region 事件方法
        //取消事件
        void UCContactsAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
        }
        //保存事件
        void UCContactsAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #region 校验保存
        //保存数据
        private void Save()
        {
            #region 验证数据输入的正确性
            string name = txtContName.Caption.Trim();
            if (name.Length == 0)
            {
                MessageBoxEx.Show("请填写联系人姓名！");
                txtContName.Focus();
                return;
            }
            if (cboContPost.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择职务!");
                cboContPost.Focus();
                return;
            }
            string email = txtContEmail.Caption.Trim();
            //验证email
            if (email.Length != 0)
            {
                if (!Utility.Common.Validator.IsEmail(email))
                {
                    MessageBoxEx.Show("email验证错误,请填写正确的email");
                    txtContEmail.Focus();
                    return;
                }
            }
            //验证手机号
            string phone = txtContPhone.Caption.Trim();
            if (phone.Length > 0)
            {
                if (!Utility.Common.Validator.IsMobile(phone))
                {
                    MessageBoxEx.Show("手机号输入有误,请输入正确的手机号");
                    txtContPhone.Focus();
                    return;
                }

            }
            #endregion

            if (MessageBoxEx.Show("确认要保存吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            #region 不加密
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("cont_name", name);//姓名
            //dic.Add("cont_post", cboContPost.SelectedValue.ToString());//职务
            //dic.Add("cont_phone",EncryptByDB.GetEncFieldValue( phone));//手机
            //dic.Add("cont_tel", txtContTel.Caption);//电话
            //dic.Add("cont_email", email);//email
            //dic.Add("sex", rbMan.Checked?"1":"0");//性别
            //dic.Add("nation", CommonCtrl.IsNullToString(cboNation.SelectedValue));//民族
            ////dic.Add("is_default", chkIsDefault.Checked ? "1" : "0");//是否默认
            //dic.Add("remark", txtRemark.Caption);//备注
            //dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());//标识未删除
            ////判断窗体状态
            //if (status == WindowStatus.Add || status == WindowStatus.Copy)
            //{
            //    dic.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            //    dic.Add("data_source", ((int)DataSources.EnumDataSources.SELFBUILD).ToString());
            //    dic.Add("cont_id", Guid.NewGuid().ToString());
            //    dic.Add("create_by", GlobalStaticObj.UserID);
            //    dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
            //    if (DBHelper.Submit_AddOrEdit("新增联系人", "tb_contacts", "", "", dic))
            //    {
            //        MessageBoxEx.Show("保存成功!");
            //        uc.BindData(contID);
            //        deleteMenuByTag(this.Tag.ToString(), uc.Name);
            //    }
            //    else
            //    {
            //        MessageBoxEx.Show("保存失败!");
            //    }
            //}
            //else if (status == WindowStatus.Edit)//编辑
            //{
            //    dic.Add("update_by", GlobalStaticObj.UserID);
            //    dic.Add("update_time", DateTime.UtcNow.Ticks.ToString());
            //    if (DBHelper.Submit_AddOrEdit("修改联系人", "tb_contacts", "cont_id", contID, dic))
            //    {
            //        MessageBoxEx.Show("修改成功!");
            //        uc.BindData(contID);
            //        deleteMenuByTag(this.Tag.ToString(), uc.Name);
            //    }
            //    else
            //    {
            //        MessageBoxEx.Show("修改失败!");
            //    }
            //}
            #endregion
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            //dic.Add("cont_name",new ParamObj("cont_name", name,SysDbType.NVarChar,40));//姓名
            //dic.Add("cont_post", new ParamObj("cont_post", cboContPost.SelectedValue, SysDbType.NVarChar, 40));//职务
            //dic.Add("cont_phone",new ParamObj("cont_phone", EncryptByDB.GetEncFieldValue(phone),SysDbType.varbi);//手机
            //dic.Add("cont_tel", txtContTel.Caption);//电话
            //dic.Add("cont_email", email);//email
            //dic.Add("sex", rbMan.Checked ? "1" : "0");//性别
            //dic.Add("nation", CommonCtrl.IsNullToString(cboNation.SelectedValue));//民族
            ////dic.Add("is_default", chkIsDefault.Checked ? "1" : "0");//是否默认
            //dic.Add("remark", txtRemark.Caption);//备注
            //dic.Add("enable_flag", ((int)DataSources.EnumEnableFlag.USING).ToString());//标识未删除
            List<SysSQLString> listSql = new List<SysSQLString>();
            SysSQLString sysSql = new SysSQLString();
            sysSql.cmdType = CommandType.Text;
            sysSql.Param = new Dictionary<string, string>();
            //判断窗体状态
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                //dic.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                //dic.Add("data_source", ((int)DataSources.EnumDataSources.SELFBUILD).ToString());
                //dic.Add("cont_id", Guid.NewGuid().ToString());
                //dic.Add("create_by", GlobalStaticObj.UserID);
                //dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
                string cont_id = Guid.NewGuid().ToString();
                sysSql.sqlString = string.Format(@"INSERT INTO [tb_contacts]
           ([cont_id]
           ,[cont_name]
           ,[cont_post]
           ,[cont_tel]
           ,[cont_email]
           ,[sex]
           ,[nation]
           ,[remark]
           ,[enable_flag]
           ,[data_source]
           ,[status]
           ,[create_by]
           ,[create_time]
           ,[cont_phone])
     VALUES
           ('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13})",
            cont_id.ToString(), name, cboContPost.SelectedValue,
           EncryptByDB.GetEncFieldValue(txtContTel.Caption), email, rbMan.Checked ? "1" : "0", cboNation.SelectedValue, txtRemark.Caption, (int)DataSources.EnumEnableFlag.USING,
            (int)DataSources.EnumStatus.Start, (int)DataSources.EnumDataSources.SELFBUILD, GlobalStaticObj.UserID,
          Common.LocalDateTimeToUtcLong(DateTime.Now), EncryptByDB.GetEncFieldValue(phone));
                listSql.Add(sysSql);
                if (DBHelper.BatchExeSQLStringMultiByTrans("新增联系人", listSql))
                {
                    //Model.tb_contacts_ex tb_contacts=new Model.tb_contacts_ex ();
                    //tb_contacts.cont_email = txtContEmail.Caption.Trim();
                    //tb_contacts.cont_id = cont_id;
                    //tb_contacts.cont_name = txtContName.Caption.Trim();
                    //tb_contacts.cont_phone = txtContPhone.Caption.Trim();
                    //tb_contacts.cont_post = cboContPost.SelectedValue.ToString();
                    //tb_contacts.cont_tel = txtContTel.Caption.Trim();
                    //DBHelper.WebServHandler("", EnumWebServFunName.UpLoadCcontact, tb_contacts);
                    MessageBoxEx.Show("保存成功!");
                    uc.BindData(contID);
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!");
                }
            }
            else if (windowStatus == WindowStatus.Edit)//编辑
            {
                //dic.Add("update_by", GlobalStaticObj.UserID);
                //dic.Add("update_time", DateTime.UtcNow.Ticks.ToString());
                //if (DBHelper.Submit_AddOrEdit("修改联系人", "tb_contacts", "cont_id", contID, dic))
                //{
                //    MessageBoxEx.Show("修改成功!");
                //    uc.BindData(contID);
                //    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                //}
                //else
                //{
                //    MessageBoxEx.Show("修改失败!");
                //}
                sysSql.sqlString = string.Format(@"update tb_contacts set cont_name='{0}',cont_post='{1}',cont_phone={2},cont_tel={3},cont_email='{4}',sex='{5}',
                                    nation='{6}',remark='{7}',update_by='{8}',update_time='{9}' where cont_id='{10}'", name, cboContPost.SelectedValue, EncryptByDB.GetEncFieldValue(phone),
                                             EncryptByDB.GetEncFieldValue(txtContTel.Caption.Trim()), email, rbMan.Checked ? "1" : "0", cboNation.SelectedValue, txtRemark.Caption,
                                                GlobalStaticObj.UserID, Common.LocalDateTimeToUtcLong(DateTime.Now), contID);
                listSql.Add(sysSql);
                if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("修改联系人", listSql))
                {
                    MessageBoxEx.Show("修改成功!");
                    uc.BindData(contID);
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("修改失败!");
                }
            }

        }
        #endregion

        #region 数据绑定回显
        //绑定数据
        private void BindData()
        {
            string strWhere = string.Format("cont_id='{0}'", contID);
            DataTable dt = DBHelper.GetTable("查询联系人", "tb_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            contactsInfo = dr;
            txtContEmail.Caption = CommonCtrl.IsNullToString(dr["cont_email"]);
            txtContName.Caption = CommonCtrl.IsNullToString(dr["cont_name"]);
            txtContPhone.Caption = CommonCtrl.IsNullToString(dr["phone"]);
            txtContTel.Caption = CommonCtrl.IsNullToString(dr["cont_tel"]);
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);
            cboContPost.SelectedValue = CommonCtrl.IsNullToString(dr["cont_post"]);
            cboNation.SelectedValue = dr["nation"];
            //chkIsDefault.Checked = CommonCtrl.IsNullToString(dr["is_default"]) == "1";
            string sex = CommonCtrl.IsNullToString(dr["sex"]);
            rbMan.Checked = sex == "1";
            rbWoman.Checked = sex == "0";
        }

        #endregion

        #region 删除启用

        void UCContactsAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCContactsAddOrEdit_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
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

        #endregion
    }
}
