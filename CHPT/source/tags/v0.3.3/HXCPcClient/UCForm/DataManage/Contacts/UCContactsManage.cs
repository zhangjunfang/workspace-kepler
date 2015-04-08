using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using System.Collections.ObjectModel;

namespace HXCPcClient.UCForm.DataManage.Contacts
{
    public partial class UCContactsManage : UCBase
    {
        #region 属性
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        /// <summary>
        /// 当前选择联系人ID
        /// </summary>
        private string ContactsID
        {
            get
            {
                //if (dgvContacts.CurrentRow == null)
                //{
                //    return string.Empty;
                //}
                //object vm_id = dgvContacts.CurrentRow.Cells["colContID"].Value;
                //if (vm_id == null)
                //{
                //    return string.Empty;
                //}
                //else
                //{
                //    return vm_id.ToString();
                //}
                if (listIDs.Count == 1)
                {
                    return listIDs[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion

        #region 构造函数和初始化
        public UCContactsManage()
        {
            InitializeComponent();
            base.AddEvent += new ClickHandler(UCContactsManage_AddEvent);
            base.EditEvent += new ClickHandler(UCContactsManage_EditEvent);
            base.CopyEvent += new ClickHandler(UCContactsManage_CopyEvent);
            base.ViewEvent += new ClickHandler(UCContactsManage_ViewEvent);
            base.DeleteEvent += new ClickHandler(UCContactsManage_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCContactsManage_StatusEvent);
            dgvContacts.ReadOnly = false;
            dgvContacts.HeadCheckChanged += new DataGridViewEx.DelegateOnClick(dgvContacts_HeadCheckChanged);
            foreach (DataGridViewColumn dgvc in dgvContacts.Columns)
            {
                if (dgvc == colChk)
                {
                    continue;
                }
                dgvc.ReadOnly = true;
            }
        }
        //加载
        private void UCContactsManage_Load(object sender, EventArgs e)
        {
            diCreate.StartDate = DateTime.Now.AddYears(-1).ToString(diCreate.customFormat);
            diCreate.EndDate = DateTime.Now.ToString(diCreate.customFormat);
            #region 初始化右键的权限
            tsmiView.Visible = btnView.Visible;
            tsmiEdit.Visible = btnEdit.Visible;
            tsmiDelete.Visible = btnDelete.Visible;
            tsmiCopy.Visible = btnCopy.Visible;
            #endregion
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
            btnView.Enabled = false;
            btnDelete.Enabled = false;
            tsmiView.Enabled = false;
            tsmiEdit.Enabled = false;
            tsmiDelete.Enabled = false;
            tsmiCopy.Enabled = false;
            //设置页面按钮可见性
            var btnCols = new ObservableCollection<ButtonEx_sms>
            {
                btnAdd, btnCopy, btnEdit, btnDelete, btnStatus, btnCancel, btnExport, btnImport, btnPrint,btnSet
            };
            UIAssistants.SetUCBaseFuncationVisible(this, btnCols);
            btnStatus.Enabled = false;
            BindSearch();
            BindData();
        }
        #endregion

        #region 工具栏事件
        //启停用事件
        void UCContactsManage_StatusEvent(object sender, EventArgs e)
        {
            if (listStart.Count == 0 && listStop.Count == 0)
            {
                return;
            }
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                return;
            }
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = "update tb_contacts set status=@status where cont_id in ({0})";
            string ids = string.Empty;
            if (enumStatus == DataSources.EnumStatus.Start)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());
                foreach (string id in listStart)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');

            }
            else if (enumStatus == DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
                foreach (string id in listStop)
                {
                    ids += string.Format("'{0}',", id);
                }
                ids = ids.TrimEnd(',');
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "联系人", listSql))
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                BindData();
            }
            else
            {
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";
                    
                }
                else
                {
                    btnStatus.Caption = "停用";
                }
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
            }
        }

        //删除事件
        void UCContactsManage_DeleteEvent(object sender, EventArgs e)
        {
            DeleteData();
        }
        //查看事件
        void UCContactsManage_ViewEvent(object sender, EventArgs e)
        {
            ViewData();
        }
        //复制事件
        void UCContactsManage_CopyEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }
        //编辑事件
        void UCContactsManage_EditEvent(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }
        //新增事件
        void UCContactsManage_AddEvent(object sender, EventArgs e)
        {
            AddData();
        }
        #endregion

        #region 方法

        //绑定数据
        private void BindData()
        {
            StringBuilder sbWhere = new StringBuilder();//条件
            sbWhere.AppendFormat(" create_time between {0} and {1}", Common.LocalDateTimeToUtcLong(DateTime.Parse(diCreate.StartDate).Date),
               Common.LocalDateTimeToUtcLong(DateTime.Parse(diCreate.EndDate).Date.AddDays(1).AddMilliseconds(-1)));
            sbWhere.AppendFormat(" and enable_flag='{0}'", (int)DataSources.EnumEnableFlag.USING);//标识未删除
            string name = txtContName.Caption.Trim();//联系人名
            if (name.Length > 0)
            {
                sbWhere.AppendFormat(" and cont_name like '%{0}%'", name);
            }
            string post = CommonCtrl.IsNullToString(cboContPost.SelectedValue);//职务
            if (post.Length > 0)
            {
                sbWhere.AppendFormat(" and cont_post='{0}'", post);
            }
            //string isDefault = CommonCtrl.IsNullToString(cboDefault.SelectedValue);//是否默认
            //if (isDefault.Length > 0)
            //{
            //    sbWhere.AppendFormat(" and is_default='{0}'", isDefault);
            //}
            string sex = CommonCtrl.IsNullToString(cboSex.SelectedValue);//性别
            if (sex.Length > 0)
            {
                sbWhere.AppendFormat(" and sex='{0}'", sex);
            }
            string dianhua = txtDianHua.Caption.Trim();//电话
            if (dianhua.Length > 0)
            {
                //sbWhere.AppendFormat(" and (cont_phone like '%{0}%' or cont_tel like '%{0}%')", dianhua);
                sbWhere.AppendFormat(" and ({1} like '%{0}%' or {2} like '%{0}%')", dianhua, EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel"));
            }
            string relation = CommonCtrl.IsNullToString(cboAffiliation.SelectedValue);//归属类型
            switch (relation)
            {
                case "1":
                    sbWhere.AppendFormat(" and isnull(cust_count,0)>0");//客户类型
                    break;
                case "2":
                    sbWhere.AppendFormat(" and isnull(vehi_count,0)>0");//车辆类型
                    break;
                case "3":
                    sbWhere.AppendFormat(" and isnull(supp_count,0)>0");//供应商类型
                    break;
            }
            int recordCount;//总数量
            DataTable dt = DBHelper.GetTableByPage("联系人查询", "v_contacts", string.Format("*,{0} phone", EncryptByDB.GetDesFieldValue("cont_phone")), sbWhere.ToString(), "", "order by create_time desc", page.PageIndex, page.PageSize, out recordCount);
            dgvContacts.RowCount = 0;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataGridViewRow dgvr = dgvContacts.Rows[dgvContacts.Rows.Add()];
                    dgvr.Cells["colContID"].Value = dr["cont_id"];
                    dgvr.Cells["colContName"].Value = dr["cont_name"];
                    string sexName = CommonCtrl.IsNullToString(dr["sex"]);
                    if (sexName == "1")
                    {
                        dgvr.Cells["colSex"].Value = "男";
                    }
                    else if (sexName == "0")
                    {
                        dgvr.Cells["colSex"].Value = "女";
                    }

                    dgvr.Cells["colContPost"].Value = dr["cont_post_name"];
                    dgvr.Cells["colNation"].Value = dr["nation_name"];
                    dgvr.Cells["colContPhone"].Value = dr["phone"];
                    dgvr.Cells["colContTel"].Value = dr["cont_tel"];
                    //string isdefault = CommonCtrl.IsNullToString(dr["is_default"]);
                    //dgvr.Cells["colIsDefault"].Value = isdefault == "1" ? "是" : "否";
                    string relationName = string.Empty;//归属类型
                    if (CommonCtrl.IsNullToString(dr["cust_count"]).Length > 0)
                    {
                        relationName = "客户联系人";
                    }
                    if (CommonCtrl.IsNullToString(dr["vehi_count"]).Length > 0)
                    {
                        if (relationName.Length > 0)
                        {
                            relationName += ",";
                        }
                        relationName += "车辆联系人";
                    }
                    if (CommonCtrl.IsNullToString(dr["supp_count"]).Length > 0)
                    {
                        if (relationName.Length > 0)
                        {
                            relationName += ",";
                        }
                        relationName += "供应商联系人";
                    }
                    //dgvr.Cells["colAffiliation"].Value = relationName;
                    dgvr.Cells["colDataSources"].Value = DataSources.GetDescription(typeof(DataSources.EnumDataSources), dr["data_source"]);
                    dgvr.Cells[colDataSources.Name].Tag = dr["data_source"];
                    string createTime = CommonCtrl.IsNullToString(dr["create_time"]);
                    if (createTime.Length > 0)
                    {
                        dgvr.Cells["colCreateTime"].Value = Common.UtcLongToLocalDateTime(Convert.ToInt64(createTime));
                    }
                    //dgvr.Cells[colStatus.Name].Value = dr["status"];
                    dgvr.Cells[colStatus.Name].Value = DataSources.GetDescription(typeof(DataSources.EnumStatus), dr["status"]);
                    dgvr.Cells[colStatus.Name].Tag = dr["status"];
                }
                page.RecordCount = recordCount;
                page.SetBtnState();
            }
        }
        /// <summary>
        /// 绑定数据后定位到指定联系人
        /// </summary>
        /// <param name="contID">指定联系人ID</param>
        public void BindData(string contID)
        {
            BindData();
            foreach (DataGridViewRow dgvr in dgvContacts.Rows)
            {
                if (CommonCtrl.IsNullToString(dgvr.Cells[colContID.Name].Value) == contID)
                {
                    dgvContacts.CurrentCell = dgvr.Cells[colContName.Name];
                    break;
                }
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        private void AddData()
        {
            UCContactsAddOrEdit add = new UCContactsAddOrEdit(WindowStatus.Add, null, this);
            base.addUserControl(add, "联系人档案-新增", "UCContactsAdd", this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        private void EditData(WindowStatus status)
        {
            if (status != WindowStatus.Edit && status != WindowStatus.Copy)
            {
                return;
            }
            string title = "编辑";
            string menuId = "UCContactsEdit";
            if (status == WindowStatus.Copy)
            {
                title = "复制";
                menuId = "UCContactsCopy";
            }
            if (dgvContacts.CurrentRow == null)
            {
                MessageBoxEx.Show(string.Format("请选择要{0}的数据!", title));
                return;
            }
            string con_id = ContactsID;
            if (string.IsNullOrEmpty(con_id))
            {
                return;
            }
            UCContactsAddOrEdit add = new UCContactsAddOrEdit(status, con_id, this);
            base.addUserControl(add, string.Format("联系人档案-{0}", title), menuId + con_id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 预览数据
        /// </summary>
        private void ViewData()
        {
            if (dgvContacts.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择要预览的数据!");
                return;
            }
            string cont_id = ContactsID;
            if (string.IsNullOrEmpty(cont_id))
            {
                return;
            }
            UCContactsView view = new UCContactsView(cont_id);
            base.addUserControl(view, "联系人档案-预览", "UCContactsView" + cont_id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        private void DeleteData()
        {
            dgvContacts.EndEdit();
            List<SysSQLString> listSql = new List<SysSQLString>();
            foreach (DataGridViewRow dgvr in dgvContacts.Rows)
            {
                object isCheck = dgvr.Cells["colChk"].Value;
                if (isCheck != null && (bool)isCheck)
                {
                    SysSQLString sql = new SysSQLString();
                    sql.cmdType = CommandType.Text;
                    sql.Param = new Dictionary<string, string>();
                    sql.sqlString = string.Format("update tb_contacts set enable_flag='{0}' where cont_id='{1}'",
                        ((int)DataSources.EnumEnableFlag.DELETED), dgvr.Cells[colContID.Name].Value);
                    listSql.Add(sql);
                }
            }
            if (listSql.Count == 0)
            {
                MessageBoxEx.Show("请选择要删除的数据!");
                return;
            }
            if (MessageBoxEx.Show("确认要删除该数据吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("删除联系人", listSql))
                {
                    MessageBoxEx.Show("删除成功");
                    BindData();
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }

        }

        //绑定性别
        private void BindSex()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["name"] = "全部";
            dt.Rows.Add(dr);
            DataRow dr1 = dt.NewRow();
            dr1["id"] = "1";
            dr1["name"] = "男";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["id"] = "0";
            dr2["name"] = "女";
            dt.Rows.Add(dr2);
            cboSex.DataSource = dt;
            cboSex.ValueMember = "id";
            cboSex.DisplayMember = "name";
        }
        //绑定是否默认
        private void BindIsDefault()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["name"] = "全部";
            dt.Rows.Add(dr);
            DataRow dr1 = dt.NewRow();
            dr1["id"] = "1";
            dr1["name"] = "是";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["id"] = "0";
            dr2["name"] = "否";
            dt.Rows.Add(dr2);
            //cboDefault.DataSource = dt;
            //cboDefault.ValueMember = "id";
            //cboDefault.DisplayMember = "name";
        }
        //绑定归属类型
        private void BindBelongTo()
        {
            List<ListItem> lists = new List<ListItem>();
            lists.Add(new ListItem("", "全部"));
            lists.Add(new ListItem("1", "客户"));
            lists.Add(new ListItem("2", "车辆"));
            lists.Add(new ListItem("3", "供应商"));
            cboAffiliation.DataSource = lists;
            cboAffiliation.ValueMember = "Value";
            cboAffiliation.DisplayMember = "Text";
        }
        //绑定查询条件
        private void BindSearch()
        {
            BindIsDefault();
            BindBelongTo();
            BindSex();
            //CommonCtrl.BindComboBoxByDictionarr(cboSex, "sys_sex", true);//绑定性别
            CommonCtrl.BindComboBoxByDictionarr(cboContPost, "sys_position", true);//绑定职务
        }

        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            //已选择状态列表
            List<string> listFiles = new List<string>();
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();
            foreach (DataGridViewRow dgvr in dgvContacts.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[colChk.Name].EditedFormattedValue))
                {
                    listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string cont_id = dgvr.Cells[colContID.Name].Value.ToString();
                    listIDs.Add(cont_id);
                    if (dgvr.Cells[colStatus.Name].Tag == null)
                    {
                        continue;
                    }
                    //enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[colStatus.Name].Value);//状态

                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[colStatus.Name].Tag);//状态
                 
                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(cont_id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(cont_id);
                    }
                }
            }
            #region 设置启用/停用
            if (listStart.Count > 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count == 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = false;
            }
            else if (listStart.Count > 0 && listStop.Count == 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "停用";
                enumStatus = DataSources.EnumStatus.Start;
            }
            else if (listStart.Count == 0 && listStop.Count > 0)
            {
                btnStatus.Enabled = true;
                btnStatus.Caption = "启用";
                enumStatus = DataSources.EnumStatus.Stop;
            }
            #endregion
            //宇通
            string dataSource = ((int)DataSources.EnumDataSources.YUTONG).ToString();
            if (listFiles.Count == 1 && !listFiles.Contains(dataSource))
            {
                base.btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
                base.btnCopy.Enabled = true;
                tsmiCopy.Enabled = true;
                base.btnView.Enabled = true;
                tsmiView.Enabled = true;
            }
            else
            {
                base.btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
                base.btnCopy.Enabled = false;
                tsmiCopy.Enabled = false;
                if (listFiles.Count == 1)
                {
                    base.btnView.Enabled = true;
                    tsmiView.Enabled = true;
                }
                else
                {
                    base.btnView.Enabled = false;
                    tsmiView.Enabled = false;
                }
            }

            //如果选择包含宇通来源，则不能删除
            if (listFiles.Count == 0 || listFiles.Contains(dataSource))
            {
                btnDelete.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                tsmiDelete.Enabled = true;
            }
        }

        #endregion

        #region 事件
        //清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSearch(pnlSearch);
            diCreate.StartDate = DateTime.Now.AddYears(-1).ToString(diCreate.customFormat);
            diCreate.EndDate = DateTime.Now.ToString(diCreate.customFormat);
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Action data = BindData;
            this.BeginInvoke(data);
        }

        //双击查看
        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            ViewData();
        }

        //菜单查看
        private void tsmiView_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        //菜单编辑
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Edit);
        }

        //菜单复制
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            EditData(WindowStatus.Copy);
        }

        //菜单删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        //翻页
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        //选择复选框
        private void dgvContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvContacts.CurrentCell == null)
            {
                return;
            }
            //单击选择框
            if (dgvContacts.CurrentCell.OwningColumn.Name == colChk.Name)
            {
                SetSelectedStatus();
            }
        }

        //单击一行，选择或取消选择
        private void dgvContacts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colChk.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvContacts.Rows)
            {
                object check = dgvr.Cells[colChk.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colChk.Name].Value = false;
                }
            }
            if ((bool)dgvContacts.CurrentRow.Cells[colChk.Name].EditedFormattedValue)
            {
                dgvContacts.CurrentRow.Cells[colChk.Name].Value = false;
            }
            else
            {
                dgvContacts.CurrentRow.Cells[colChk.Name].Value = true;
            }
            SetSelectedStatus();
        }
        //全选
        void dgvContacts_HeadCheckChanged()
        {
            SetSelectedStatus();
        }

        //只允许电话栏输入数字和进行修改
        private void txtDianHua_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
          
        }

        //只允许输入数字的控制
        private static void OnlyNum(KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                return;
            }
        }
        #endregion


    }
}
