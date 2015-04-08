using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Model;
using System.Reflection;
using Utility.Common;
using HXCPcClient.Chooser;

namespace HXCPcClient.UCForm.DataManage.SupplierFile
{
    public partial class UCSupplierAddOrEdit : UCBase
    {

        #region 变量
        UCSupplierManager uc;

        private string suppId = string.Empty;
        private int oldcolcontindex = -1;
        private string oldcolcont_id = string.Empty;
        private tb_supplier tb_supp_Model = new tb_supplier();
        #endregion

        #region 初始化窗体

        //构造函数
        public UCSupplierAddOrEdit(WindowStatus status, string suppId, UCSupplierManager uc)
        {
            InitializeComponent();
            //gvUserInfoList.ReadOnly = false;
            this.windowStatus = status;
            this.suppId = suppId;
            this.uc = uc;
            BindDllInfo();
            if (status == WindowStatus.Edit || status == WindowStatus.Copy)
            {
                LoadInfo(suppId);
                GetAllContacts(suppId);
                ucAttr.TableName = "tb_supplier";
                ucAttr.TableNameKeyValue = suppId;
                ucAttr.BindAttachment();
            }
            if (status == WindowStatus.Add || status == WindowStatus.Copy)
            {
                lblsup_code.Text = CommonUtility.GetNewNo(DataSources.EnumProjectType.Supplier);
            }
            base.SaveEvent += new ClickHandler(UCSupplierAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCSupplierAddOrEdit_CancelEvent);
            base.StatusEvent += new ClickHandler(UCSupplierAddOrEdit_StatusEvent);
            base.DeleteEvent += new ClickHandler(UCSupplierAddOrEdit_DeleteEvent);
        }


        //载入方法
        private void UCSupplierAddOrEdit_Load(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
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
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetDataAddBtn();
            }
        }
        #endregion

        #region 控件事件

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCSupplierAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (CheckDataInfo())
                {
                    if (MessageBoxEx.Show("确认要保存吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        return;
                    }
                    string opName = "供应商档案操作";
                    List<SysSQLString> listSql = new List<SysSQLString>();

                    if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
                    {
                        suppId = Guid.NewGuid().ToString();
                        AddSupplierSqlString(listSql, suppId);
                        opName = "新增供应商档案";
                    }
                    else if (windowStatus == WindowStatus.Edit)
                    {
                        EditSupplierSqlString(listSql, suppId, tb_supp_Model);
                        opName = "修改供应商档案";
                    }
                    DealContacts(listSql, suppId);

                    ucAttr.TableName = "tb_supplier";
                    ucAttr.TableNameKeyValue = suppId;
                    ucAttr.GetAttachmentSql(listSql);

                    if (DBHelper.BatchExeSQLStringMultiByTrans(opName, listSql))
                    {
                        MessageBoxEx.Show("保存成功！");
                        uc.BindgvSupplierList();
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    }
                    else
                    {
                        MessageBoxEx.Show("保存失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
                throw;
            }
        }
        /// <summary>
        /// 新增联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiContactAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmContacts frm = new frmContacts();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string contID = frm.contID;
                    if (gvUserInfoList.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dr in gvUserInfoList.Rows)
                        {
                            if (dr.Cells["colcont_id"].Value.ToString() == contID)
                            {
                                MessageBoxEx.Show("该联系人已经存在与列表中，不能再次添加!");
                                return;
                            }
                        }
                    }
                    DataTable dt = GetContacts(contID);
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataGridViewRow dgvr = gvUserInfoList.Rows[gvUserInfoList.Rows.Add()];
                        dgvr.Cells["colcont_id"].Value = dr["cont_id"];
                        dgvr.Cells["colis_default"].Value = "0";
                        dgvr.Cells["colcont_name"].Value = dr["cont_name"];
                        dgvr.Cells["colcont_post"].Value = dr["con_post_name"];
                        dgvr.Cells["colcont_phone"].Value = dr["phone"];
                        dgvr.Cells["colcont_tel"].Value = dr["tel"];
                        dgvr.Cells["colremark"].Value = dr["remark"];
                        dgvr.Cells["colcont_email"].Value = dr["cont_email"];
                        string createTime = CommonCtrl.IsNullToString(dr["cont_birthday"]);
                        if (createTime.Length > 0)
                        {
                            dgvr.Cells["colcont_birthday"].Value = DateTime.MinValue.AddTicks(Convert.ToInt64(createTime)).ToLocalTime();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            { oldcolcontindex = -1; }
        }
        /// <summary>
        /// 编辑联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiContactEdit_Click(object sender, EventArgs e)
        {
            if (oldcolcontindex > -1)
            {
                try
                {
                    DataGridViewRow dgvr = gvUserInfoList.Rows[oldcolcontindex];
                    frmContacts frm = new frmContacts();
                    DialogResult result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string contID = frm.contID;
                        if (gvUserInfoList.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dr in gvUserInfoList.Rows)
                            {
                                if (dr.Cells["colcont_id"].Value.ToString() == contID)
                                {
                                    MessageBoxEx.Show("该联系人已经存在与列表中，不能再次添加!");
                                    return;
                                }
                            }
                        }

                        DataTable dt = GetContacts(contID);
                        foreach (DataRow dr in dt.Rows)
                        {
                            dgvr.Cells["colcont_id"].Value = dr["cont_id"];
                            dgvr.Cells["colis_default"].Value = "0";
                            dgvr.Cells["colcont_name"].Value = dr["cont_name"];
                            dgvr.Cells["colcont_post"].Value = dr["con_post_name"];
                            dgvr.Cells["colcont_phone"].Value = dr["phone"];
                            dgvr.Cells["colcont_tel"].Value = dr["cont_tel"];
                            dgvr.Cells["colremark"].Value = dr["remark"];
                            dgvr.Cells["colcont_email"].Value = dr["cont_email"];
                            string createTime = CommonCtrl.IsNullToString(dr["cont_birthday"]);
                            if (createTime.Length > 0)
                            {
                                dgvr.Cells["colcont_birthday"].Value = DateTime.MinValue.AddTicks(Convert.ToInt64(createTime)).ToLocalTime();
                            }
                        }
                    }
                }
                catch (Exception ex)
                { }
                finally
                { oldcolcontindex = -1; }
            }
        }
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiContactDelete_Click(object sender, EventArgs e)
        {
            if (oldcolcontindex > -1)
            {
                try
                {
                    //string cont_name = gvUserInfoList.Rows[oldcolcontindex].Cells["colcont_name"].Value.ToString();
                    //if (MessageBoxEx.Show("确认要删除联系人名称为:" + cont_name + " 这条数据吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    //{
                    //    return;
                    //}
                    //if (status == WindowStatus.Add || status == WindowStatus.Copy)
                    //{
                    gvUserInfoList.Rows.Remove(gvUserInfoList.Rows[oldcolcontindex]);
                    //}
                    //else if (status == WindowStatus.Edit)
                    //{
                    //    SysSQLString sysStringSql = new SysSQLString();
                    //    List<SysSQLString> listSql = new List<SysSQLString>();
                    //    Dictionary<string, string> dic = new Dictionary<string, string>();

                    //    sysStringSql.cmdType = CommandType.Text;
                    //    oldcolcont_id = gvUserInfoList.Rows[oldcolcontindex].Cells["colcont_id"].Value.ToString();
                    //    string sql1 = "delete from tr_base_contacts where relation_object_id=@relation_object_id and cont_id=@cont_id";
                    //    dic.Add("relation_object_id", suppId);
                    //    dic.Add("cont_id", oldcolcont_id);
                    //    sysStringSql.sqlString = sql1;
                    //    sysStringSql.Param = dic;
                    //    listSql.Add(sysStringSql);
                    //    if (DBHelper.BatchExeSQLStringMultiByTrans("供应商档案信息删除关联联系人", listSql))
                    //    {
                    //        GetAllContacts(suppId);
                    //        MessageBoxEx.Show("删除成功!");
                    //    }
                    //    else
                    //    {
                    //        MessageBoxEx.Show("删除失败!");
                    //    }
                    //}
                }
                catch (Exception ex)
                { }
                finally
                { oldcolcontindex = -1; }
            }
        }
        /// <summary>
        /// 鼠标放进单元格后获取当前单元格行索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUserInfoList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                oldcolcontindex = e.RowIndex;
                gvUserInfoList.Rows[oldcolcontindex].Selected = true;
            }
        }
        //取消事件
        void UCSupplierAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        // 标签页大小改变事件
        private void tabPage2_ClientSizeChanged(object sender, EventArgs e)
        {
            ucAttr.Size = tabPage2.Size;
        }
        //省份选择改变事件
        private void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlprovince.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCityComBox(ddlcity, ddlprovince.SelectedValue.ToString(), "请选择");
                CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
                CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
            }
        }
        //城市下拉框选择改变事件
        private void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlcity.SelectedValue.ToString()))
            {
                CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
            }
            else
            {
                CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
            }
        }
        //点击表单事件
        private void gvUserInfoList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)//双击表头或列头时不起作用   
            {
                //获取控件的值
                for (int i = 0; i < gvUserInfoList.Rows.Count; i++)
                {
                    gvUserInfoList.Rows[i].Cells["colis_default"].Value = "0";
                }
                gvUserInfoList.Rows[e.RowIndex].Cells["colis_default"].Value = "1";
            }
        }
        //限制信用额度得输入
        private void txtcredit_line_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnlyNum(e);
        }
        //限制信用账期的输入
        private void txtcredit_account_period_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnlyNum(e);
        }
        #endregion

        #region 方法、函数

        /// <summary>
        /// 验证数据信息完整性
        /// </summary>
        private bool CheckDataInfo()
        {
            //供应商名称
            if (string.IsNullOrEmpty(txtsup_full_name.Caption.Trim()))
            {
                MessageBoxEx.Show("请填写供应商名称!");
                return false;
            }
            //供应商名称
            if (string.IsNullOrEmpty(txtsup_short_name.Caption.Trim()))
            {
                MessageBoxEx.Show("请填写供应商简称!");
                return false;
            }
            //供应商分类编号,供应商分类
            if (string.IsNullOrEmpty(ddlsup_type.SelectedValue.ToString()))
            {
                MessageBoxEx.Show("请选择供应商分类!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtsup_tel.Caption.Trim()))
            { if (!Validator.IsTel(txtsup_tel.Caption.Trim())) { MessageBoxEx.Show("公司电话格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtsup_fax.Caption.Trim()))
            { if (!Validator.IsTel(txtsup_fax.Caption.Trim())) { MessageBoxEx.Show("公司传真格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtsup_email.Caption.Trim()))
            { if (!Validator.IsEmail(txtsup_email.Caption.Trim())) { MessageBoxEx.Show("公司邮箱格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtsup_website.Caption.Trim()))
            { if (!Validator.IsUrl(txtsup_website.Caption.Trim())) { MessageBoxEx.Show("公司网址格式不正确!"); return false; } }
            if (!string.IsNullOrEmpty(txtzip_code.Caption.Trim()))
            { if (!Validator.IsPostCode(txtzip_code.Caption.Trim())) { MessageBoxEx.Show("公司邮编格式不正确!"); return false; } }
            //if (!string.IsNullOrEmpty(txtcredit_line.Caption.Trim()))

            //检验附件列表单元格
            if (!ucAttr.CheckAttachment())
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 绑定下拉框信息
        /// </summary>
        private void BindDllInfo()
        {
            ddlsup_type.Items.Clear();
            ddlunit_properties.Items.Clear();
            ddlcredit_class.Items.Clear();
            ddlprice_type.Items.Clear();

            //绑定供应商分类
            CommonFuncCall.BindComBoxDataSource(ddlsup_type, "sys_supplier_category", "请选择");
            //绑定企业性质类型
            CommonFuncCall.BindComBoxDataSource(ddlunit_properties, "sys_enterprise_property", "请选择");
            //绑定信用等级类型
            CommonFuncCall.BindComBoxDataSource(ddlcredit_class, "sys_enterprise_credit_class", "请选择");
            //绑定价格商类型
            CommonFuncCall.BindComBoxDataSource(ddlprice_type, "sys_price_type", "请选择");

            CommonFuncCall.BindProviceComBox(ddlprovince, "请选择");
            CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
            CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");

        }
        /// <summary>
        /// 添加供应商档案的sql语句
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="partID"></param>
        private void AddSupplierSqlString(List<SysSQLString> listSql, string supID)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数

            tb_supplier model = new tb_supplier();
            CommonFuncCall.SetModelObjectValue(this, model);
            model.sup_first_spell = "ZJHLCH";
            model.sup_id = supID;
            model.enable_flag = "1";
            int StatusNum = (int)SYSModel.DataSources.EnumStatus.Start;//0停用，1启用
            model.status = StatusNum.ToString();
            model.create_by = GlobalStaticObj.UserID;
            model.create_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Insert Into tb_supplier( ");
                StringBuilder sp = new StringBuilder();
                StringBuilder sb_prame = new StringBuilder();
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    sb_prame.Append("," + name);
                    sp.Append(",@" + name);
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
                sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")").Append(";");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary>
        /// 编辑供应商档案的sql语句
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="supID"></param>
        /// <param name="model"></param>
        private void EditSupplierSqlString(List<SysSQLString> listSql, string supID, tb_supplier model)
        {
            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;
            Dictionary<string, string> dicParam = new Dictionary<string, string>();//参数
            CommonFuncCall.SetModelObjectValue(this, model);
            model.update_by = GlobalStaticObj.UserID;
            model.update_time = Common.LocalDateTimeToUtcLong(DateTime.Now);
            if (model != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" Update tb_supplier Set ");
                bool isFirstValue = true;
                foreach (PropertyInfo info in model.GetType().GetProperties())
                {
                    string name = info.Name;
                    object value = info.GetValue(model, null);
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    else
                    {
                        sb.Append("," + name);
                        sb.Append("=");
                        sb.Append("@" + name);
                    }
                    dicParam.Add(name, value == null ? "" : value.ToString());
                }
                sb.Append(" where sup_id='" + supID + "';");
                sysStringSql.sqlString = sb.ToString();
                sysStringSql.Param = dicParam;
                listSql.Add(sysStringSql);
            }
        }
        /// <summary>
        /// 添加附件的sql语句
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="partID"></param>
        private void AddFileSqlObj()
        {

        }
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
                    CommonFuncCall.SetModlByDataTable(tb_supp_Model, dt);
                    CommonFuncCall.SetShowControlValue(this, tb_supp_Model, "");
                }
            }
        }
        /// <summary>
        /// 选择联系人后，获取所选择的联系人信息
        /// </summary>
        private DataTable GetContacts(string contactsId)
        {
            string TableName = string.Format(@"(select dic_name as con_post_name,tb_contacts.* from 
                                                    tb_contacts 
                                                    left join 
                                                    sys_dictionaries on 
                                                    tb_contacts.cont_post=sys_dictionaries.dic_id)
                                                     as tb_contacts");
            DataTable dt = DBHelper.GetTable("查询一条联系人信息", TableName, string.Format("*,{0} phone,{1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), " cont_id='" + contactsId + "'", "", "");
            return dt;
        }
        /// <summary>
        /// 获取该供应商下所关联的所有联系人信息
        /// </summary>
        /// <param name="suppId"></param>
        /// <returns></returns>
        private void GetAllContacts(string suppId)
        {
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
                DataTable dt_contacts = DBHelper.GetTable("查询关联的联系人信息", TableName, string.Format("*,{0} phone,{1} tel ", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), " cont_id in (" + conId + ")", "", "");
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
        /// <summary>
        /// 在编辑和添加时对主数据和联系人关联表中的联系人信息进行操作
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="supID"></param>
        private void DealContacts(List<SysSQLString> listSql, string relation_id)
        {
            gvUserInfoList.EndEdit();

            SysSQLString sysStringSql = new SysSQLString();
            sysStringSql.cmdType = CommandType.Text;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (gvUserInfoList.Rows.Count > 0)
            {
                string sql1 = "delete from tr_base_contacts where relation_object_id=@relation_object_id;";
                dic.Add("relation_object_id", relation_id);
                sysStringSql.sqlString = sql1;
                sysStringSql.Param = dic;
                listSql.Add(sysStringSql);

                foreach (DataGridViewRow dr in gvUserInfoList.Rows)
                {
                    sysStringSql = new SysSQLString();
                    sysStringSql.cmdType = CommandType.Text;
                    dic = new Dictionary<string, string>();
                    string contId = dr.Cells["colcont_id"].Value.ToString();
                    dic.Add("id", Guid.NewGuid().ToString());
                    dic.Add("cont_id", contId);
                    dic.Add("relation_object", "供应商档案");
                    dic.Add("relation_object_id", relation_id);

                    string is_default = "0";
                    if (dr.Cells["colis_default"].Value == null)
                    { is_default = "0"; }
                    if ((bool)dr.Cells["colis_default"].EditedFormattedValue)
                    { is_default = "1"; }
                    else
                    { is_default = "0"; }
                    dic.Add("is_default", is_default);

                    string sql2 = "Insert Into tr_base_contacts(id,cont_id,relation_object,relation_object_id,is_default) values(@id,@cont_id,@relation_object,@relation_object_id,@is_default);";
                    sysStringSql.sqlString = sql2;
                    sysStringSql.Param = dic;
                    listSql.Add(sysStringSql);
                }
            }
        }

        #endregion

        #region 删除启用
        void UCSupplierAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            Delete();
        }

        void UCSupplierAddOrEdit_StatusEvent(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void SetStatus()
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (tb_supp_Model.status == ((int)DataSources.EnumStatus.Start).ToString())
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
                if (tb_supp_Model.status == ((int)DataSources.EnumStatus.Start).ToString())
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
            if (tb_supp_Model.status == ((int)DataSources.EnumStatus.Start).ToString())
            {
                status.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
            }
            else
            {
                status.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            return DBHelper.Submit_AddOrEdit(btnStatus.Caption + "供应商", "tb_supplier", "sup_id", tb_supp_Model.sup_id, status);
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
                if (DBHelper.Submit_AddOrEdit("删除供应商", "tb_supplier", "sup_id", tb_supp_Model.sup_id, updateField))
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
    }

    /// <summary>
    /// 对UI控件的扩展 
    /// </summary>
    static class ExtendUCOnlyNum
    {
        /// <summary>
        /// 限制控件只能输入数字
        /// </summary>
        /// <param name="ucontrol">基础控件</param>
        /// <param name="e">键盘事件数据</param>
        public static void OnlyNum(this UCBase ucontrol, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
                return;
            }
        }
    }

    class isdefault
    {
        public string cont_id;
        public string is_default;
    }
}
