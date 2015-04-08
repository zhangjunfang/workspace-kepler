using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using System.IO;

namespace HXCPcClient.UCForm.SysManage.PersonalSet
{
    /// <summary>
    /// 个人设置
    /// 孙明生
    /// </summary>
    public partial class UCPersonalSet : UCBase
    {
        #region 属性
        private string photoID = string.Empty;//图片附件ID
        private string lock_screen_id = string.Empty;//自动锁屏id
        #endregion 

        #region 初始化
        public UCPersonalSet()
        {
            InitializeComponent();

            base.EditEvent += new ClickHandler(UCPersonalSet_EditEvent);
            base.SaveEvent += new ClickHandler(UCPersonalSet_SaveEvent);

            base.CancelEvent -= new ClickHandler(UC_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_SaveEvent);
        }
        #endregion

        #region --窗体初始化
        private void UCPersonalSet_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏

            #region 加载个人信息
            CommonFuncCall.BindComBoxDataSource(cbojkzk, "sys_health", "请选择");//健康状况          
         
            CommonFuncCall.BindComBoxDataSource(cbonation, "sys_nation", "请选择");
            CommonFuncCall.BindComBoxDataSource(cbosex, "sys_sex", "请选择");//性别
            CommonFuncCall.BindComBoxDataSource(cboidcard_type, "sys_certificates_type", "请选择");//证件类型
            CommonFuncCall.BindComBoxDataSource(cboeducation, "sys_education", "请选择");//学历
            CommonFuncCall.BindComBoxDataSource(cboposition, "sys_post", "请选择");//岗位
            CommonFuncCall.BindComBoxDataSource(cbolevel, "sys_personnel_level", "请选择");//级别

            this.LoadData();
        }
        private void LoadData()
        {
            this.txtpassword.Caption = string.Empty;
            this.txtpassword_new.Caption = string.Empty;
            this.txtpassword_new_d.Caption = string.Empty;

            DataTable dt = DBHelper.GetTable("查询人员信息", "v_User", "*", "user_id='" + GlobalStaticObj.UserID + "'", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                this.BindControls(dt);
            }
            #endregion

            #region 加载自动锁屏
            DataTable dt_sp = DBHelper.GetTable("查询自动锁屏设置", "sys_automatic_lock_screen", "*", "", "", "");
            if (dt_sp != null && dt_sp.Rows.Count > 0)
            {
                lock_screen_id = dt_sp.Rows[0]["lock_screen_id"].ToString();

                rdbis_open_y.Checked = dt_sp.Rows[0]["is_open"].ToString() == "1" ? true : false;
                rdbis_open_n.Checked = dt_sp.Rows[0]["is_open"].ToString() == "1" ? false : true;
                numsys_lock_screen_time.Value = Convert.ToInt32(dt_sp.Rows[0]["sys_lock_screen_time"].ToString());
                rdbis_use_login_password_y.Checked = dt_sp.Rows[0]["is_use_login_password"].ToString() == "1" ? true : false;
                rdbis_use_login_password_n.Checked = dt_sp.Rows[0]["is_use_login_password"].ToString() == "1" ? false : true;
                txtset_password.Caption = dt_sp.Rows[0]["set_password"].ToString();
            }
            #endregion
        }
        #endregion

        #region 页面控件绑定数据
        /// <summary>
        /// 页面控件绑定数据
        /// </summary>
        /// <param name="dt">用户信息</param>
        private void BindControls(DataTable dt)
        {
            txtuser_code.Caption = dt.Rows[0]["user_code"].ToString();
            txtuser_name.Caption = dt.Rows[0]["user_name"].ToString();
            txtland_name.Caption = dt.Rows[0]["land_name"].ToString();

            txtuser_phone.Caption = dt.Rows[0]["user_phone"].ToString();
            txtuser_telephone.Caption = dt.Rows[0]["user_telephone"].ToString();
            txtremark.Caption = dt.Rows[0]["remark"].ToString();

            txcorg_name.Text = dt.Rows[0]["org_name"].ToString();
            txcorg_name.Tag = dt.Rows[0]["org_id"].ToString();
            txtuser_fax.Caption = dt.Rows[0]["user_fax"].ToString();
            cbosex.SelectedValue = dt.Rows[0]["sex"].ToString();

            cboidcard_type.SelectedValue = dt.Rows[0]["idcard_type"].ToString();
            txtuser_email.Caption = dt.Rows[0]["user_email"].ToString();
            txtidcard_num.Caption = dt.Rows[0]["idcard_num"].ToString();
            txtuser_address.Caption = dt.Rows[0]["user_address"].ToString();

            cbonation.SelectedValue = dt.Rows[0]["nation"].ToString();
            txtgraduate_institutions.Caption = dt.Rows[0]["graduate_institutions"].ToString();
            txttechnical_expertise.Caption = dt.Rows[0]["technical_expertise"].ToString();
            txtuser_height.Caption = dt.Rows[0]["user_height"].ToString();
            txtnative_place.Caption = dt.Rows[0]["native_place"].ToString();
            txtspecialty.Caption = dt.Rows[0]["specialty"].ToString();
            dtpentry_date.Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["entry_date"].ToString()));
            txtuser_weight.Caption = dt.Rows[0]["user_weight"].ToString();
            txtregister_address.Caption = dt.Rows[0]["register_address"].ToString();
            dtpgraduate_date.Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["graduate_date"].ToString()));
            txtwage.Caption = dt.Rows[0]["wage"].ToString();
            dtpbirthday.Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["birthday"].ToString()));
            cboeducation.SelectedValue = dt.Rows[0]["education"].ToString();
            cboposition.SelectedValue = dt.Rows[0]["position"].ToString();
            txtpolitical_status.Caption = dt.Rows[0]["political_status"].ToString();
            cbolevel.SelectedValue = dt.Rows[0]["level"].ToString();

            cbojkzk.SelectedValue = dt.Rows[0]["health"].ToString();//健康状况 

            DataTable dt_pic = DBHelper.GetTable("查询用户图片", "attachment_info", "*", "relation_object_id='" + GlobalStaticObj.UserID + "' and att_name='用户图片' and att_type='图片'", "", "");
            if (dt_pic != null && dt_pic.Rows.Count > 0)
            {
                string photoPath = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_path"]);
                photoID = CommonCtrl.IsNullToString(dt_pic.Rows[0]["att_id"]);
                picuser.BackgroundImage = FileOperation.DownLoadImage(photoPath);
            }
        }
        #endregion

        #region --操作事件

        #region 编辑 取消只读
        /// <summary>
        /// 编辑 取消只读
        /// </summary>
        void UCPersonalSet_EditEvent(object sender, EventArgs e)
        {
            if (tabControlEx1.SelectedIndex == 0)
            { //密码
                txtpassword.ReadOnly = false;
                txtpassword_new.ReadOnly = false;
                txtpassword_new_d.ReadOnly = false;
            }
            else if (tabControlEx1.SelectedIndex == 1)
            { //个人信息
                //txtuser_code.ReadOnly=false;
                txtuser_name.ReadOnly = false;
                //txtland_name.ReadOnly = false;
                txtuser_phone.ReadOnly = false;
                txtuser_telephone.ReadOnly = false;
                txtremark.ReadOnly = false;
                txcorg_name.ReadOnly = false;
                txtuser_fax.ReadOnly = false;
                txtuser_email.ReadOnly = false;
                txtidcard_num.ReadOnly = false;
                txtuser_address.ReadOnly = false;
                txtgraduate_institutions.ReadOnly = false;
                txttechnical_expertise.ReadOnly = false;
                txtuser_height.ReadOnly = false;
                txtnative_place.ReadOnly = false;
                txtspecialty.ReadOnly = false;
                txtwage.ReadOnly = false;
                txtuser_weight.ReadOnly = false;
                txtregister_address.ReadOnly = false;
                txtpolitical_status.ReadOnly = false;

                cbosex.Enabled = true;
                cboidcard_type.Enabled = true;
                cbonation.Enabled = true;
                dtpentry_date.Enabled = true;
                dtpgraduate_date.Enabled = true;
                dtpbirthday.Enabled = true;
                cboeducation.Enabled = true;
                cboposition.Enabled = true;
                cbolevel.Enabled = true;
                cbojkzk.Enabled = true;//健康状况 
                picuser.Enabled = true;
            }
            else if (tabControlEx1.SelectedIndex == 2)
            { //自动锁屏 
                rdbis_open_y.Enabled = true;
                rdbis_open_n.Enabled = true;
                rdbis_use_login_password_y.Enabled = true;
                rdbis_use_login_password_n.Enabled = true;
                numsys_lock_screen_time.ReadOnly = false;
                if (rdbis_use_login_password_n.Checked)
                {
                    txtset_password.ReadOnly = false;
                }
                else
                {
                    txtset_password.ReadOnly = true;
                }

            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        void UCPersonalSet_SaveEvent(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string opName = "修改密码";
            if (tabControlEx1.SelectedIndex == 0)
            { //密码
                List<SQLObj> listSql = new List<SQLObj>();
                opName = "修改密码";
                if (GlobalStaticObj.PassWord != txtpassword.Caption.Trim())
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtpassword, "旧密码输入不正确!");
                    return;
                }
                if (!Utility.Common.ValidateUtil.IsPassword(txtpassword_new.Caption.Trim()))
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtpassword_new, "密码为6-20字母数字混合组成!");
                    return;
                }
                if (txtpassword_new.Caption.Trim() != txtpassword_new_d.Caption.Trim())
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtpassword_new_d, "两次密码输入不一致!");
                    return;
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("password", this.txtpassword_new.Caption.Trim());
                dic.Add("update_by", GlobalStaticObj.UserID);
                dic.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());               

                if (DBHelper.Submit_AddOrEdit("修改用户密码", "sys_user", "user_id", GlobalStaticObj.UserID, dic))
                {               
                    GlobalStaticObj.PassWord = txtpassword_new.Text.Trim();                    
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (tabControlEx1.SelectedIndex == 1)
            { //个人信息
                try
                {
                    string msg = "";
                    bool bln = Validator(ref msg);
                    if (!bln)
                    {
                        return;
                    }                  
                    string currUser_id = ""; ;

                    string keyName = string.Empty;
                    string keyValue = string.Empty;
                    Dictionary<string, string> dicFileds = new Dictionary<string, string>();

                    dicFileds.Add("user_name", txtuser_name.Caption.Trim());//人员姓名
                    dicFileds.Add("land_name", txtland_name.Caption.Trim());//账号
                    dicFileds.Add("user_phone", txtuser_phone.Caption.Trim());//手机
                    dicFileds.Add("user_telephone", txtuser_telephone.Caption.Trim());//固话    
                    dicFileds.Add("org_id", txcorg_name.Tag.ToString().Trim());//组织id 
                    dicFileds.Add("sex", cbosex.SelectedValue.ToString());//性别
                    dicFileds.Add("user_fax", txtuser_fax.Caption.Trim());//传真 
                    dicFileds.Add("idcard_type", cboidcard_type.SelectedValue.ToString());//证件类型 
                    dicFileds.Add("user_email", txtuser_email.Caption.Trim());//邮箱 
                    dicFileds.Add("idcard_num", txtidcard_num.Caption.Trim());//证件号码 
                    dicFileds.Add("user_address", txtuser_address.Caption.Trim());//联系地址
                    dicFileds.Add("remark", txtremark.Caption.Trim());//备注 

                    dicFileds.Add("nation", cbonation.SelectedValue.ToString());//民族 
                    dicFileds.Add("graduate_institutions", txtgraduate_institutions.Caption.Trim());//毕业学校 
                    dicFileds.Add("technical_expertise", txttechnical_expertise.Caption.Trim());//技术特长 
                    dicFileds.Add("user_height", txtuser_height.Caption.Trim());//身高 
                    dicFileds.Add("native_place", txtnative_place.Caption.Trim());//籍贯 
                    dicFileds.Add("specialty", txtspecialty.Caption.Trim());//专业
                    dicFileds.Add("entry_date", Common.LocalDateTimeToUtcLong(dtpentry_date.Value).ToString());//  入职日期 
                    dicFileds.Add("user_weight", txtuser_weight.Caption.Trim());//体重 
                    dicFileds.Add("register_address", txtregister_address.Caption.Trim());//户籍所在地 
                    dicFileds.Add("graduate_date", Common.LocalDateTimeToUtcLong(dtpgraduate_date.Value).ToString());// 毕业时间 
                    dicFileds.Add("wage", txtwage.Caption.Trim());// 工资 
                    dicFileds.Add("birthday", Common.LocalDateTimeToUtcLong(dtpbirthday.Value).ToString());// 出生日期  
                    dicFileds.Add("education", cboeducation.SelectedValue.ToString());//学历
                    dicFileds.Add("position", cboposition.SelectedValue.ToString());//岗位
                    dicFileds.Add("political_status", txtpolitical_status.Caption.Trim());//政治面貌
                    dicFileds.Add("level", cbolevel.SelectedValue.ToString());//级别

                    dicFileds.Add("health", cbojkzk.SelectedValue.ToString());//健康状况

                    string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
                    dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
                    dicFileds.Add("update_time", nowUtcTicks);

                    keyName = "user_id";
                    keyValue = GlobalStaticObj.UserID;
                    currUser_id = GlobalStaticObj.UserID;
                    opName = "更新人员管理";

                    bln = DBHelper.Submit_AddOrEdit(opName, "sys_user", keyName, keyValue, dicFileds);
                                   
                    if (bln)
                    {
                        string photo = string.Empty;
                        if (picuser.Tag != null)
                        {
                            photo = Guid.NewGuid().ToString() + Path.GetExtension(picuser.Tag.ToString());
                            if (!FileOperation.UploadFile(picuser.Tag.ToString(), photo))
                            {
                                return;
                            }
                        }

                        List<SQLObj> listSql = new List<SQLObj>();
                        listSql = AddPhoto(listSql, currUser_id, photo);
                        DBHelper.BatchExeSQLMultiByTrans(opName, listSql);    

                        txtuser_name.ReadOnly = true;
                        txtuser_phone.ReadOnly = true;
                        txtuser_telephone.ReadOnly = true;
                        txtremark.ReadOnly = true;
                        txcorg_name.ReadOnly = true;
                        txtuser_fax.ReadOnly = true;
                        txtuser_email.ReadOnly = true;
                        txtidcard_num.ReadOnly = true;
                        txtuser_address.ReadOnly = true;
                        txtgraduate_institutions.ReadOnly = true;
                        txttechnical_expertise.ReadOnly = true;
                        txtuser_height.ReadOnly = true;
                        txtnative_place.ReadOnly = true;
                        txtspecialty.ReadOnly = true;
                        txtwage.ReadOnly = true;
                        txtuser_weight.ReadOnly = true;
                        txtregister_address.ReadOnly = true;
                        txtpolitical_status.ReadOnly = true;

                        cbosex.Enabled = false;
                        cboidcard_type.Enabled = false;
                        cbonation.Enabled = false;
                        dtpentry_date.Enabled = false;
                        dtpgraduate_date.Enabled = false;
                        dtpbirthday.Enabled = false;
                        cboeducation.Enabled = false;
                        cboposition.Enabled = false;
                        cbolevel.Enabled = false;
                        cbojkzk.Enabled = false;//健康状况 
                        picuser.Enabled = false;

                        MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("保存失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (tabControlEx1.SelectedIndex == 2)
            { //自动锁屏 
                try
                {
                    if (rdbis_use_login_password_n.Checked && txtset_password.Caption.Trim().Length == 0)
                    {
                        Utility.Common.Validator.SetError(errorProvider1, txtset_password, "请输入口令!");
                        return;
                    }
                    string keyName = string.Empty;
                    string keyValue = string.Empty;
                    opName = "自动锁屏";
                    Dictionary<string, string> Fileds = new Dictionary<string, string>();

                    Fileds.Add("is_open", rdbis_open_y.Checked ? "1" : "0");
                    string sys_lock_screen_time = Math.Round(numsys_lock_screen_time.Value, 0).ToString();
                    Fileds.Add("sys_lock_screen_time", sys_lock_screen_time);
                    Fileds.Add("is_use_login_password", rdbis_use_login_password_y.Checked ? "1" : "0");
                    Fileds.Add("set_password", txtset_password.Caption.ToString().Trim());

                    string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
                    Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
                    Fileds.Add("update_time", nowUtcTicks);
                    if (lock_screen_id == "")
                    {
                        Fileds.Add("lock_screen_id", Guid.NewGuid().ToString());//新ID                   
                        Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                        Fileds.Add("create_time", nowUtcTicks);
                    }
                    else
                    {
                        keyName = "lock_screen_id";
                        keyValue = lock_screen_id;
                        opName = "更新自动锁屏";
                    }
                    bool bln = DBHelper.Submit_AddOrEdit(opName, "sys_automatic_lock_screen", keyName, keyValue, Fileds);
                    if (bln)
                    {
                        rdbis_open_y.Enabled = false;
                        rdbis_open_n.Enabled = false;
                        rdbis_use_login_password_y.Enabled = false;
                        rdbis_use_login_password_n.Enabled = false;

                        numsys_lock_screen_time.ReadOnly = true;
                        txtset_password.ReadOnly = true;
                        MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("保存失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //取消
        void UC_SaveEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            this.LoadData();
        }
        #endregion

        #endregion

        #region 修改密码sql
        /// <summary>
        /// 修改密码sql
        /// </summary>
        /// <param name="listSql"></param>
        private void AddPwdSql(List<SQLObj> listSql)
        {
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();//参数
            dicParam.Add("user_id", new ParamObj("user_id", GlobalStaticObj.UserID, SysDbType.VarChar, 40));//ID
            dicParam.Add("password", new ParamObj("password", this.txtpassword_new.Caption.Trim(), SysDbType.VarChar, 40));//密码
            dicParam.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.VarChar, 40));
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime), SysDbType.BigInt));
            sqlObj.sqlString = @"update [sys_user] set password=@password,update_by=@update_by,update_time=@update_time where user_id=@user_id;";
            sqlObj.Param = dicParam;
            listSql.Add(sqlObj);
        }
        #endregion        

        #region 个人信息控件内容验证
        /// <summary>
        /// 个人信息控件内容验证
        /// </summary>
        /// <param name="msg">返回提示信息</param>
        /// <returns></returns>
        private bool Validator(ref string msg)
        {
            if (string.IsNullOrEmpty(txtuser_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtuser_name, "人员姓名不能为空!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtuser_phone.Caption.Trim()) && !Utility.Common.Validator.IsMobile(txtuser_phone.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtuser_phone, "手机格式不正确!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtuser_telephone.Caption.Trim()) && !Utility.Common.Validator.IsTel(txtuser_telephone.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtuser_telephone, "固话格式不正确!");
                return false;
            }
            if (txcorg_name.Tag == null)// string.IsNullOrEmpty(txcorg_name.Tag.ToString().Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txcorg_name, "请选择公司组织!");
                return false;
            }
            if (cbosex.SelectedValue.ToString() == "")
            {
                Utility.Common.Validator.SetError(errorProvider1, cbosex, "请选择性别!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtuser_fax.Caption.Trim()) && !Utility.Common.Validator.IsTel(txtuser_fax.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtuser_fax, "传真格式不正确!");
                return false;
            }
            // dicFileds.Add("is_operator", cbois_operator.SelectedValue.ToString());//是否操作员 
            //dicFileds.Add("idcard_type", cboidcard_type.SelectedValue.ToString());//证件类型 
            if (!string.IsNullOrEmpty(txtuser_email.Caption.Trim()) && !Utility.Common.Validator.IsEmail(txtuser_email.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtuser_email, "邮箱格式不正确!");
                return false;
            }

            if (string.IsNullOrEmpty(txtland_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtland_name, "账号不能为空!");
                return false;
            }

            //dicFileds.Add("nation", cbonation.SelectedValue.ToString());//民族 
            //dicFileds.Add("entry_date", Common.LocalDateTimeToUtcLong(dtpentry_date.Value).ToString());//  入职日期 
            //dicFileds.Add("graduate_date", Common.LocalDateTimeToUtcLong(dtpgraduate_date.Value).ToString());// 毕业时间
            // dicFileds.Add("birthday", Common.LocalDateTimeToUtcLong(dtpbirthday.Value).ToString());// 出生日期 
            //dicFileds.Add("education", cboeducation.SelectedValue.ToString());//学历
            //dicFileds.Add("position", cboposition.SelectedValue.ToString());//岗位
            return true;
        }
        #endregion

        #region 添加sql语句-图片
        /// <summary>
        /// 添加sql语句-图片
        /// </summary>
        /// <param name="listSql">listSql</param>
        /// <param name="partID">用户id</param>
        /// <param name="path">图片id</param>
        /// <returns></returns>
        private List<SQLObj> AddPhoto(List<SQLObj> listSql, string partID, string path)
        {
            if (path != string.Empty)
            {
                SQLObj sqlObj = new SQLObj();
                sqlObj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
                dic.Add("att_path", new ParamObj("att_path", path, SysDbType.VarChar, 40));
                sqlObj.Param = dic;
                if (photoID.Length == 0)
                {
                    dic.Add("att_name", new ParamObj("att_name", "用户图片", SysDbType.NVarChar, 15));
                    dic.Add("att_type", new ParamObj("att_type", "图片", SysDbType.NVarChar, 15));
                    photoID = Guid.NewGuid().ToString();
                    dic.Add("is_main", new ParamObj("is_main", (int)DataSources.EnumYesNo.Yes, SysDbType.VarChar, 5));
                    dic.Add("relation_object", new ParamObj("relation_object", "tb_parts", SysDbType.NVarChar, 30));
                    dic.Add("relation_object_id", new ParamObj("relation_object_id", partID, SysDbType.VarChar, 40));
                    dic.Add("enable_flag", new ParamObj("enable_flag", (int)DataSources.EnumEnableFlag.USING, SysDbType.VarChar, 5));
                    dic.Add("create_by", new ParamObj("create_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dic.Add("create_time", new ParamObj("create_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));
                    sqlObj.sqlString = @"insert into [attachment_info] ([att_id],[att_name],[att_type],[att_path],[relation_object],[relation_object_id],[enable_flag],[create_by],[create_time],[is_main])
                            values (@att_id,@att_name,@att_type,@att_path,@relation_object,@relation_object_id,@enable_flag,@create_by,@create_time,@is_main);";
                }
                else
                {
                    dic.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                    dic.Add("update_time", new ParamObj("update_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));
                    sqlObj.sqlString = "update [attachment_info] set [att_path]=@att_path,[update_by]=@update_by,[update_time]=@update_time where [att_id]=@att_id;";
                }
                dic.Add("att_id", new ParamObj("att_id", photoID, SysDbType.VarChar, 40));
                listSql.Add(sqlObj);
            }
            return listSql;
        }
        #endregion      

        #region 图片点击事件 选择图片
        /// <summary>
        /// 图片点击事件 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picuser_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "图片|*.jpg;*.png;*.gif;*.bmp";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fileDialog.FileName.Length == 0)
                {
                    return;
                }
                Image image = new Bitmap(fileDialog.FileName);
                //picPhoto.Image = image;
                picuser.BackgroundImage = image;
                picuser.Tag = fileDialog.FileName;
            }
        }
        #endregion

        #region 自动锁屏 是否启用
        private void rdbis_use_login_password_n_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbis_use_login_password_n.Checked)
            {
                txtset_password.ReadOnly = false;
            }
            else
            {
                txtset_password.ReadOnly = true;
            }
        }
        #endregion
    }
}
