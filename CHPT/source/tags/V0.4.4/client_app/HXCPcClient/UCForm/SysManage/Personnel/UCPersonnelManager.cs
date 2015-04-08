using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Threading;
using System.Threading.Tasks;
using Model;
using System.Linq;
using System.Drawing.Printing;

namespace HXCPcClient.UCForm.SysManage.Personnel
{
    /// <summary>
    /// 人员管理
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCPersonnelManager : UCBase
    {
        #region --成员变量
        private bool myLock = true;
        private int recordCount = 0;
        /// <summary>
        /// 启用停用状态
        /// </summary>
        DataSources.EnumStatus enumStatus;
        List<string> listIDs = new List<string>();//已选择项的ID列表
        List<string> listStart = new List<string>();//启用状态
        List<string> listStop = new List<string>();//停用状态
        BusinessPrint businessPrint;//业务打印功能
        DataSet dscom = new DataSet();
        #endregion

        #region --构造函数

        public UCPersonnelManager()
        {
            InitializeComponent();

            UIAssistants.SetButtonStyle4QueryAndClear(btnQuery, btnClear);
            DataGridViewEx.SetDataGridViewStyle(this.dgvUser);
            dtpentry_date.Value = DateTime.Now.AddMonths(-1);
            dtpentry_date_end.Value = DateTime.Now;
            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);
            base.ViewEvent += new ClickHandler(UCPersonnelManager_ViewEvent);
            base.AddEvent += new ClickHandler(UCPersonnelManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCPersonnelManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCPersonnelManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCPersonnelManager_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCPersonnelManager_StatusEvent);
            MessageProcessor.ComOrginfoUpdate += new EventHandler(MessageProcessor_OrginfoUpdate);


        }
     
        void MessageProcessor_OrginfoUpdate(object sender, EventArgs e)
        {
            BindTree();
        }

        #endregion

        #region --窗体加载
        private void UCPersonnelManager_Load(object sender, EventArgs e)
        {

            base.RoleButtonStstus(this.Name);

            //dgvUser.ReadOnly = false;

            this.dtpentry_date.Value = DateTime.Now.AddMonths(-1);
            this.dtpentry_date_end.Value = DateTime.Now;

            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);//是否操作员

            DataSources.BindComDataGridViewBoxColumnDataEnum(this.is_operator, typeof(DataSources.EnumYesNo));
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));

            this.BindTree();
            List<string> listNotPrint = new List<string>();

            PaperSize printsize = new PaperSize("printsize", dgvUser.Width / 4 + 20, dgvUser.Height);
            businessPrint = new BusinessPrint(dgvUser, "sys_user", "人员档案", printsize, listNotPrint);
            base.SetContentMenuScrip(dgvUser);
        }
        #endregion

        #region --公司组织树
        /// <summary>
        /// 公司组织树
        /// </summary>
        private void BindTree()
        {
            string strSql = "select '0' as ftype, c.com_id as id,c.parent_id,'' as ocom_id,c.com_code as code,c.com_name as name from tb_company c where c.enable_flag ='1' union all "
                          + "select '1' as ftype, o.org_id as id,o.parent_id,o.com_id as ocom_id,o.org_code as code,o.org_name as name from tb_company c,tb_organization o where  "
                          + " c.enable_flag ='1' and o.enable_flag='1' and c.com_id=o.com_id ";
            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strSql;
            dscom = DBHelper.GetDataSet("查询公司组织树", sqlobj);

            tvCompany.Nodes.Clear();
            TreeNode tmpNd = new TreeNode();

            //获取根公司信息
            DataTable comDT = DBHelper.GetTable("获取根公司", GlobalStaticObj.CommAccCode, "tb_company", "*", " data_source='" + DataSources.EnumDataSources.YUTONG.ToString("d") + "'", string.Empty, string.Empty);
            if (comDT != null && comDT.Rows.Count > 0)
            {
                tmpNd.Text = comDT.Rows[0]["com_name"].ToString();//name
                tmpNd.Name = comDT.Rows[0]["com_id"].ToString(); //id
            }
            else
            {
                tmpNd.Text = "全部";//name
                tmpNd.Name = "Root";//id
            }
            tvCompany.Nodes.Add(tmpNd);
            //节点加上去
            if (dscom != null && dscom.Tables[0].Rows.Count > 0)
            {
                //clsGetTree cls = new clsGetTree();
                //CL_ROOT
                //CommonCtrl.InitTree(this.tvCompany.Nodes, "-1", ds.Tables[0].DefaultView);
                InitTree(tmpNd.Nodes, "-1", dscom.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.Nodes[0].Expand();
                    tvCompany.SelectedNode = tvCompany.Nodes[0];
                }
            }
        }
        public void InitTree(TreeNodeCollection Nds, string parentId, DataView dv)
        {
            tvCompany.ImageList = new ImageList();
            tvCompany.ImageList.Images.Add("company", ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("tree_company"));
            tvCompany.ImageList.Images.Add("group", ServiceStationClient.Skin.SkinAssistant.GetResourcesImage("tree_group"));
            TreeNode tmpNd;
            dv.RowFilter = "parent_id='" + parentId + "'";
            foreach (DataRowView drv in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Tag = drv;
                tmpNd.Text = drv["name"].ToString(); //name
                tmpNd.Name = drv["id"].ToString();//id
                if (CommonCtrl.IsNullToString(drv["ftype"]) == "0")
                {
                    tmpNd.ImageKey = "company";
                }
                else
                {
                    tmpNd.ImageKey = "group";
                }
                Nds.Add(tmpNd);
                InitTree(tmpNd.Nodes, drv["id"].ToString(), dv);
            }
        }
        #endregion

        #region --按钮事件
        /// <summary>
        /// 转到添加页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_AddEvent(object sender, EventArgs e)
        {
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(null, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            DataRowView drv = tvCompany.SelectedNode.Tag as DataRowView;
            DataRow[] dr;

            if (tvCompany.SelectedNode != null && drv != null && drv["ftype"].ToString() != "0")//选中的是组织
            {
                uc.org_id = drv["id"].ToString();
                uc.org_name = drv["name"].ToString();
                if (dscom != null && dscom.Tables[0] != null)
                {
                    dr = dscom.Tables[0].Select("id='" + drv["parent_id"].ToString() + "'");
                    if (dr != null & dr.Length > 0)
                    {
                        uc.com_name = dr[0]["name"].ToString();
                    }
                }
            }
            uc.windowStatus = WindowStatus.Add;
            base.addUserControl(uc, "人员管理-新增", "UCPersonnelAdd", this.Tag.ToString(), this.Name);
            //订阅新增窗口保存事件
            uc.SaveEvent += new ClickHandler(uc_SaveEvent);
        }
        //新增以后重新刷新人员数据
        void uc_SaveEvent(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 转向 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_CopyEvent(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择复制记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvUser.CurrentRow.DataBoundItem as DataRowView).Row;
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(dr, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.windowStatus = WindowStatus.Copy;
            uc.id = dr["user_id"].ToString();
            base.addUserControl(uc, "人员管理-复制", "UserCopy" + uc.id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_EditEvent(object sender, EventArgs e)
        {
            if (dgvUser.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow dr = (this.dgvUser.CurrentRow.DataBoundItem as DataRowView).Row;
            UCPersonnelAddOrEdit uc = new UCPersonnelAddOrEdit(dr, this.Name);
            uc.RefreshDataStart -= new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.RefreshDataStart += new UCPersonnelAddOrEdit.RefreshData(this.BindPageData);
            uc.windowStatus = WindowStatus.Edit;
            uc.id = dr["user_id"].ToString();
            base.addUserControl(uc, "人员管理-编辑", "RoleEdit" + uc.id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_DeleteEvent(object sender, EventArgs e)
        {

            List<string> listField = new List<string>();
            List<DataGridViewRow> deleteUser = new List<DataGridViewRow>();
            foreach (DataGridViewRow dr in dgvUser.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["user_id"].Value.ToString());
                    deleteUser.Add(dr);
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBoxEx.ShowQuestion("是否确认删除?"))
            {
                Dictionary<string, string> comField = new Dictionary<string, string>();
                comField.Add("enable_flag", "0");
                bool flag = DBHelper.BatchUpdateDataByIn("批量删除人员", "sys_user", comField, "user_id", listField.ToArray());
                if (flag)
                {
                    UploadUserData(listField);
                    BindPageData();
                    if (dgvUser.Rows.Count > 0)
                    {
                        dgvUser.CurrentCell = dgvUser.Rows[0].Cells[0];
                    }
                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LocalCache._Update(CacheList.User);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCPersonnelManager_StatusEvent(object sender, EventArgs e)
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
            string strSql = "update sys_user set status=@status where user_id in ({0})";
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
            if (DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "人员", listSql))
            {
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                BindPageData();
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (enumStatus == DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        /// <summary>
        /// 删除时上传工作人员信息
        /// </summary>
        /// <param name="deleteUser">要删除的人员信息</param>
        void UploadUserData(List<string> DeleteUser)
        {
            DataTable dtuserinfo = dgvUser.DataSource as DataTable;
            if (dtuserinfo != null)
            {
                Parallel.ForEach(DeleteUser, userid =>
                {
                    DataRow[] druserinf = dtuserinfo.Select("user_id='" + userid + "'");
                    if (druserinf != null && druserinf.Length > 0)
                    {
                        var cont = new tb_contacts_ex();
                        cont.cont_id = userid;//id
                        cont.cont_name = CommonCtrl.IsNullToString(druserinf[0]["user_name"].ToString());//name
                        cont.cont_post = CommonCtrl.IsNullToString(druserinf[0]["post"].ToString());//post 
                        cont.cont_phone = CommonCtrl.IsNullToString(druserinf[0]["user_phone"].ToString());//phone
                        cont.nation = LocalCache.GetDictNameById(druserinf[0]["nation"].ToString());
                        cont.parent_customer = CommonCtrl.IsNullToString("");
                        cont.sex = UpSex(druserinf[0]["sex"].ToString());//sex
                        //cont.status = CommonCtrl.IsNullToString(SYSModel.DataSources.EnumStatus.Start.ToString("d"));//status
                        cont.status = "1";//status
                        cont.cont_post_remark = CommonCtrl.IsNullToString("");
                        cont.cont_crm_guid = CommonCtrl.IsNullToString(druserinf[0]["cont_crm_guid"].ToString());//crmguid
                        cont.contact_type = "02";
                        DBHelper.WebServHandlerByFun("删除服务站工作人员", SYSModel.EnumWebServFunName.UpLoadCcontact, cont);
                    }
                });

                //foreach (string userid in DeleteUser)
                //{
                //    DataRow[] druserinf = dtuserinfo.Select("user_id='" + userid + "'");
                //    if (druserinf != null && druserinf.Length > 0)
                //    {
                //        var cont = new tb_contacts_ex();
                //        cont.cont_id = userid;//id
                //        cont.cont_name = CommonCtrl.IsNullToString(druserinf[0]["user_name"].ToString());//name
                //        cont.cont_post = CommonCtrl.IsNullToString(druserinf[0]["post"].ToString());//post 
                //        cont.cont_phone = CommonCtrl.IsNullToString(druserinf[0]["user_phone"].ToString());//phone
                //        cont.nation = LocalCache.GetDictNameById(druserinf[0]["nation"].ToString());
                //        cont.parent_customer = CommonCtrl.IsNullToString("");
                //        cont.sex = UpSex(druserinf[0]["sex"].ToString());//sex
                //        //cont.status = CommonCtrl.IsNullToString(SYSModel.DataSources.EnumStatus.Start.ToString("d"));//status
                //        cont.status = "1";//status
                //        cont.cont_post_remark = CommonCtrl.IsNullToString("");
                //        cont.cont_crm_guid = CommonCtrl.IsNullToString(druserinf[0]["cont_crm_guid"].ToString());//crmguid
                //        cont.contact_type = "02";
                //        DBHelper.WebServHandlerByFun("删除服务站工作人员", SYSModel.EnumWebServFunName.UpLoadCcontact, cont);
                //    }
                //}
            }
        }

        //上传到宇通接口的性别转化 男为1 女为2
        private string UpSex(string sexguid)
        {
            if (sexguid != null && sexguid != string.Empty)
            {
                if (LocalCache.GetDictNameById(sexguid) == "")
                {
                    return "100000000";
                }
                if (LocalCache.GetDictNameById(sexguid) == "女")
                    return "100000001";
                else
                    return "100000000";
            }
            else
                return "100000000";
        }


        void UCPersonnelManager_ViewEvent(object sender, EventArgs e)
        {
            DataTable dtData = dgvUser.DataSource as DataTable;
            if (dtData != null)
            {
                businessPrint.Preview(dtData);
            }
        }

        #endregion

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));

                if (tvCompany.SelectedNode != null)
                {
                    DataRowView drv = tvCompany.SelectedNode.Tag as DataRowView;
                    if (drv != null)
                    {
                        if (drv["ftype"].ToString() == "0")//选中公司
                        {
                            //where += string.Format(" and  com_id = '{0}'", drv["id"].ToString());
                            where += string.Format(" and  com_name = '{0}'", drv["id"].ToString());
                        }
                        else//选中组织
                        {
                            SQLObj sobj = new SQLObj();
                            sobj.cmdType = CommandType.Text;
                            sobj.Param = new Dictionary<string, ParamObj>();
                            sobj.sqlString = " with tem as  (select org_id from tb_organization where org_id='" + drv["id"].ToString() + "' union all "
                                 + " select o.org_id from tb_organization o,tem where o.parent_id=tem.org_id)   select * from tem ";
                            DataSet ds_ids = DBHelper.GetDataSet("", sobj);
                            if (ds_ids != null && ds_ids.Tables[0].Rows.Count > 0)
                            {
                                string ids = string.Empty;
                                foreach (DataRow row in ds_ids.Tables[0].Rows)
                                {
                                    ids += "'" + row["org_id"] + "',";
                                }
                                if (ids != string.Empty)
                                {
                                    ids = ids.Substring(0, ids.Length - 1);
                                    where += string.Format(" and  org_id in  ({0})", ids);
                                }
                            }
                            else
                            {
                                where += string.Format(" and  org_id = '{0}'", drv["id"].ToString());
                            }
                        }
                    }
                    else//全部
                    {

                    }
                }
                if (!string.IsNullOrEmpty(txtuser_code.Caption.Trim()))//人员编码
                {
                    where += string.Format(" and  user_code like '%{0}%'", txtuser_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtidcard_num.Caption.Trim()))//证件号码
                {
                    where += string.Format(" and  idcard_num like '%{0}%'", txtidcard_num.Caption.Trim());
                }
                string status = CommonCtrl.IsNullToString(cbbstatus.SelectedValue);
                if (!string.IsNullOrEmpty(status)) //状态
                {
                    where += string.Format(" and  status = '{0}'", status);
                }
                if (!string.IsNullOrEmpty(txtuser_name.Caption.Trim()))//人员名称
                {
                    where += string.Format(" and  user_name like '%{0}%'", txtuser_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtuser_telephone.Caption.Trim()))//联系电话
                {
                    where += string.Format(" and  user_telephone like '%{0}%'", txtuser_telephone.Caption.Trim());
                }
                string is_operator = CommonCtrl.IsNullToString(cbbis_operator.SelectedValue);
                if (!string.IsNullOrEmpty(is_operator)) //状态
                {
                    where += string.Format(" and  is_operator = '{0}'", is_operator);
                }
                if (!string.IsNullOrEmpty(dtpentry_date.Value.ToString()) && !string.IsNullOrEmpty(dtpentry_date_end.Value.ToString()))
                {
                    where += string.Format(" and  create_time >= '{0}' and  create_time < '{1}' ", Common.LocalDateTimeToUtcLong(dtpentry_date.Value).ToString(), Common.LocalDateTimeToUtcLong(dtpentry_date_end.Value.AddDays(1)).ToString());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页查询人员管理", "v_user", "*", obj.ToString(), "", "order by create_time desc",
                           page.PageIndex, page.PageSize, out this.recordCount);

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvUser.DataSource = obj;
            this.page.RecordCount = this.recordCount;

            this.myLock = true;
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData();
        }

        #endregion

        #region --dgv事件

        private void dgvUser_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvUser.Rows[e.RowIndex].Cells[this.user_id.Name].Value.ToString();
                UCPersonnelView uc = new UCPersonnelView();
                uc.id = id;
                uc.uc = this;
                uc.wStatus = WindowStatus.View;
                uc.Name = this.Name;
                uc.RoleButtonStstus(Name);
                base.addUserControl(uc, "人员管理-浏览", "PersonnelView" + id, this.Tag.ToString(), this.Name);
            }
        }

        /// <summary>
        /// 表格单元点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvUser.CurrentCell == null)
            //{
            //    return;
            //}

            ////单击选择框
            //if (dgvUser.CurrentCell.OwningColumn.Name == colCheck.Name)
            //{
            //    SetSelectedStatus();
            //}
        }

        //全选
        private void dgvUser_HeadCheckChanged()
        {
            this.Invalidate();
            SetSelectedStatus();
        }

        #endregion

        #region --清除事件

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtuser_code.Caption = string.Empty;
            this.txtidcard_num.Caption = string.Empty;
            this.txtuser_name.Caption = string.Empty;
            this.txtuser_telephone.Caption = string.Empty;
            dtpentry_date.Value = DateTime.Now.AddMonths(-1);
            dtpentry_date_end.Value = DateTime.Now;
            DataSources.BindComBoxDataEnum(cbbstatus, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbbis_operator, typeof(DataSources.EnumYesNo), true);//是否操作员

        }

        //电话输入事件控制
        private void txtuser_telephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnlyNum(e);
        }

        #endregion

        #region 状态方法
        ///// <summary>
        ///// 设置选择项后状态
        ///// </summary>
        //void SetSelectedStatus()
        //{
        //    //已选择状态列表
        //    List<string> listFiles = new List<string>();
        //    listIDs.Clear();
        //    listStart.Clear();
        //    listStop.Clear();
        //    foreach (DataGridViewRow dgvr in dgvUser.Rows)
        //    {

        //        if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
        //        {

        //            //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
        //            string id = dgvr.Cells[user_id.Name].Value.ToString();
        //            listIDs.Add(id);
        //            if (dgvr.Cells[columnStatus.Name].Value == null)
        //            {
        //                continue;
        //            }
        //            enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[columnStatus.Name].Value);//状态

        //            //enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[status.Name].Tag);//状态

        //            if (enumStatus == DataSources.EnumStatus.Start)
        //            {
        //                listStart.Add(id);
        //            }
        //            else if (enumStatus == DataSources.EnumStatus.Stop)
        //            {
        //                listStop.Add(id);
        //            }
        //        }
        //    }
        //    #region 设置启用/停用
        //    if (listStart.Count > 0 && listStop.Count > 0)
        //    {
        //        btnStatus.Enabled = false;
        //    }
        //    else if (listStart.Count == 0 && listStop.Count == 0)
        //    {
        //        btnStatus.Enabled = false;
        //    }
        //    else if (listStart.Count > 0 && listStop.Count == 0)
        //    {
        //        btnStatus.Enabled = true;
        //        btnStatus.Caption = "停用";
        //        enumStatus = DataSources.EnumStatus.Start;
        //    }
        //    else if (listStart.Count == 0 && listStop.Count > 0)
        //    {
        //        btnStatus.Enabled = true;
        //        btnStatus.Caption = "启用";
        //        enumStatus = DataSources.EnumStatus.Stop;
        //    }
        //    #endregion

        //}
        #endregion

        #region 按钮状态
        /// <summary>
        /// 设置选择项后状态
        /// </summary>
        void SetSelectedStatus()
        {
            btnCopy.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnStatus.Enabled = false;
            //已选择状态列表
            List<string> listFiles = new List<string>();
            //记录选中数据状态
            RecordData(listFiles);

            SetStatus();

            SetMultiBtnStatus(listFiles);


        }

        /// <summary>
        /// 设置多选时按钮状态
        /// </summary>
        /// <param name="listFiles">选中的记录</param>
        private void SetMultiBtnStatus(List<string> listFiles)
        {
            if (listFiles.Count > 1)
            {
                btnCopy.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// 记录选中数据
        /// </summary>
        /// <param name="listFiles">数据状态的表</param>
        private void RecordData(List<string> listFiles)
        {
            listIDs.Clear();
            listStart.Clear();
            listStop.Clear();

            foreach (DataGridViewRow dgvr in dgvUser.Rows)
            {

                if (Convert.ToBoolean(dgvr.Cells[colCheck.Name].Value))
                {

                    //listFiles.Add(dgvr.Cells[colDataSources.Name].Tag.ToString());
                    string id = dgvr.Cells[user_id.Name].Value.ToString();
                    listIDs.Add(id);
                    listFiles.Add(dgvr.Cells[columnStatus.Name].Value.ToString());
                    if (dgvr.Cells[columnStatus.Name].Value == null)
                    {

                        continue;
                    }
                    enumStatus = (DataSources.EnumStatus)Convert.ToInt32(dgvr.Cells[columnStatus.Name].Value);//状态

                    if (enumStatus == DataSources.EnumStatus.Start)
                    {
                        listStart.Add(id);
                    }
                    else if (enumStatus == DataSources.EnumStatus.Stop)
                    {
                        listStop.Add(id);
                    }
                }
            }
        }

        /// <summary>
        /// 设置启用停用
        /// </summary>
        private void SetStatus()
        {
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
        }


        #endregion
    }


}
