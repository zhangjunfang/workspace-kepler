using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.Organization
{
    /// <summary>
    /// 组织管理
    /// 孙明生
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCOrganizationManager : UCBase
    {
        private bool myLock = true;
        private int recordCount = 0;
        DataSet dsCom;//公司组织信息
        BusinessPrint businessPrint;//业务打印功能
        #region --构造函数
        public UCOrganizationManager()
        {
            InitializeComponent();
            DataGridViewStyle.DataGridViewBgColor(dgvorganization);

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.AddEvent += new ClickHandler(UCOrganizationManager_AddEvent);
            base.CopyEvent += new ClickHandler(UCOrganizationManager_CopyEvent);
            base.EditEvent += new ClickHandler(UCOrganizationManager_EditEvent);
            base.DeleteEvent += new ClickHandler(UCOrganizationManager_DeleteEvent);
            base.ViewEvent += new ClickHandler(UCOrganizationManager_ViewEvent);
            base.ExportEvent += new ClickHandler(UCOrganizationManager_ExportEvent);
        }


        #endregion

        #region --窗体初始化
        private void UCOrganizationManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            dgvorganization.ReadOnly = false;

            this.dtpcreate_time.Value = DateTime.Now.AddMonths(-1);
            this.dtpcreate_time_end.Value = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));

            this.BindPageData();

            this.BindTree();
            List<string> listNotPrint = new List<string>();
            //listNotPrint.Add("create_time");
            //listNotPrint.Add("update_time");
            listNotPrint.Add(colCheck.Name);
            businessPrint = new BusinessPrint(dgvorganization, "tb_organization", "组织档案", null, listNotPrint);
            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.columnStatus, typeof(DataSources.EnumStatus));
        }
        #endregion

        #region 公司组织树
        /// <summary>
        /// 公司组织树
        /// </summary>
        private void BindTree()
        {
            dsCom = GetComOrg();

            tvCompany.Nodes.Clear();
            TreeNode tmpNd = new TreeNode();
            //获取根公司信息
            DataTable comDT = DBHelper.GetTable("获取根公司", GlobalStaticObj.CommAccCode, "tb_company", "*", "data_source=" + DataSources.EnumDataSources.YUTONG.ToString("d") + "", string.Empty, string.Empty);
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
            if (dsCom.Tables[0].Rows.Count > 0)
            {
                //clsGetTree cls = new clsGetTree();
                InitTree(tmpNd.Nodes, "-1", dsCom.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.Nodes[0].Expand();
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
        /// <summary>
        /// 获得公司组织
        /// </summary>
        /// <returns>公司组织信息</returns>
        private static DataSet GetComOrg()
        {
            string strSql = "select '0' as ftype, c.com_id as id,c.parent_id,'' as ocom_id,c.com_code as code,c.com_name as name from tb_company c where c.enable_flag ='1' union all "
                          + "select '1' as ftype, o.org_id as id,o.parent_id,o.com_id as ocom_id,o.org_code as code,o.org_name as name from tb_company c,tb_organization o where  "
                          + " c.enable_flag ='1' and o.enable_flag='1' and c.com_id=o.com_id ";
            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询公司组织树", sqlobj);
            return ds;
        }

        #endregion

        #region 按钮事件
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationManager_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                foreach (DataGridViewRow dr in dgvorganization.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        listField.Add(dr.Cells["org_id"].Value.ToString());
                    }
                }
                if (listField.Count == 0)
                {
                    MessageBoxEx.Show("请选择删除记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string tipMsg = "是否确认删除?";
                if (dsCom != null && dsCom.Tables[0] != null)
                {
                    //遍历要删除的组织ID
                    for (int i = 0; i < listField.Count; i++)
                    {
                        //查找组织ID的下级组织
                        DataRow[] drcom = dsCom.Tables[0].Select("parent_id='" + listField[i] + "'");
                        if (drcom != null && drcom.Length > 0)
                        {
                            //将下级组织也加入到要删除的列表中
                            listField.Add(drcom[0]["id"].ToString());
                        }
                    }
                }
                if (listField.Count > 0)
                {
                    tipMsg = "该组织存在下级组织,是否确认删除?";
                }
                if (MessageBoxEx.ShowQuestion(tipMsg))
                {
                    //DataSet dscom = GetComOrg();

                    Dictionary<string, string> comField = new Dictionary<string, string>();
                    comField.Add("enable_flag", "0");
                    bool flag = DBHelper.BatchUpdateDataByIn("批量删除组织", "tb_organization", comField, "org_id", listField.ToArray());
                    if (flag)
                    {
                        BindPageData();
                        if (dgvorganization.Rows.Count > 0)
                        {
                            dgvorganization.CurrentCell = dgvorganization.Rows[0].Cells[0];
                        }
                        MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LocalCache._Update(CacheList.Org);
                        //更新组织树
                        BindTree();
                    }
                    else
                    {
                        MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("删除失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationManager_EditEvent(object sender, EventArgs e)
        {
            if (dgvorganization.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UCOrganizationAddOrEdit OrganizationEdit = new UCOrganizationAddOrEdit();
            OrganizationEdit.uc = this;
            OrganizationEdit.wStatus = WindowStatus.Edit;
            OrganizationEdit.strOrg_id = dgvorganization.CurrentRow.Cells["org_id"].Value.ToString();  //参数 组织管理ID
            base.addUserControl(OrganizationEdit, "组织管理-编辑", "OrganizationEdit" + OrganizationEdit.strOrg_id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 转向 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationManager_CopyEvent(object sender, EventArgs e)
        {
            if (dgvorganization.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择复制记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UCOrganizationAddOrEdit OrganizationCopy = new UCOrganizationAddOrEdit();
            OrganizationCopy.uc = this;
            OrganizationCopy.wStatus = WindowStatus.Copy;
            OrganizationCopy.strOrg_id = dgvorganization.CurrentRow.Cells["org_id"].Value.ToString();  //参数 组织管理ID
            base.addUserControl(OrganizationCopy, "组织管理-复制", "OrganizationCopy" + OrganizationCopy.strOrg_id, this.Tag.ToString(), this.Name);
        }
        /// <summary>
        /// 转到添加页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationManager_AddEvent(object sender, EventArgs e)
        {
            UCOrganizationAddOrEdit OrganizationAdd = new UCOrganizationAddOrEdit();
            OrganizationAdd.uc = this;
            OrganizationAdd.wStatus = WindowStatus.Add;
            if (this.tvCompany.SelectedNode != null)
            {
                OrganizationAdd.parent_id = this.tvCompany.SelectedNode.Name;
            }
            base.addUserControl(OrganizationAdd, "组织管理-新增", "OrganizationAdd", this.Tag.ToString(), this.Name);
            //订阅新增的保存事件
            OrganizationAdd.SaveEvent += new ClickHandler(OrganizationAdd_SaveEvent);
        }
        //清除按钮
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtorg_code.Caption = string.Empty;
            this.txtcontact_name.Caption = string.Empty;
            this.txtorg_name.Caption = string.Empty;

            this.dtpcreate_time.Value = DateTime.Now.AddMonths(-1);
            this.dtpcreate_time_end.Value = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
        }
        //新增窗口保存以后 刷新组织树的数据
        void OrganizationAdd_SaveEvent(object sender, EventArgs e)
        {
            BindTree();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
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
                        where += string.Format(" and  com_id = '{0}'", drv["ftype"].ToString() == "1" ? drv["ocom_id"].ToString() : drv["id"].ToString());
                }
                if (!string.IsNullOrEmpty(txtorg_code.Caption.Trim()))//组织编码
                {
                    where += string.Format(" and  org_code like '%{0}%'", txtorg_code.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtorg_name.Caption.Trim()))//组织名
                {
                    where += string.Format(" and  org_name like '%{0}%'", txtorg_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtcontact_name.Caption.Trim())) //联系人 
                {
                    where += string.Format(" and  contact_name like '%{0}%'", txtcontact_name.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(dtpcreate_time.Value.ToString()) && !string.IsNullOrEmpty(dtpcreate_time_end.Value.ToString()))
                {
                    where += string.Format(" and  create_time >= '{0}' and  create_time <= '{1}' ", Common.LocalDateTimeToUtcLong(dtpcreate_time.Value).ToString(), Common.LocalDateTimeToUtcLong(dtpcreate_time_end.Value).ToString());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页查询组织管理", "tb_organization", "*", obj.ToString(), "", "org_id",
                page.PageIndex, page.PageSize, out this.recordCount);
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvorganization.DataSource = obj;
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
        private void dgvorganization_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }

        }
        /// <summary>
        /// 双击单元格 进浏览页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvorganization_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string org_id = dgvorganization.Rows[e.RowIndex].Cells["org_id"].Value.ToString();
                UCOrganizationView uc = new UCOrganizationView();
                uc.windowStatus = WindowStatus.View;
                uc.uc = this;
                uc.id = org_id;
                uc.Name = this.Name;
                uc.RoleButtonStstus(Name);
                base.addUserControl(uc, "组织管理-浏览", "OrganizationView" + org_id, this.Tag.ToString(), this.Name);

            }
        }
        #endregion

        #region 基础事件
        //导出事件
        void UCOrganizationManager_ExportEvent(object sender, EventArgs e)
        {

            if (this.dgvorganization.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string fileName = "组织档案" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvorganization);
            }
            catch (Exception ex)
            {
                Utility.Log.Log.writeLineToLog("【组织档案】" + ex.Message, "server");
                MessageBoxEx.ShowWarning("导出失败！");
            }

        }

        //打印预览事件
        void UCOrganizationManager_ViewEvent(object sender, EventArgs e)
        {
            DataTable dtData = this.dgvorganization.DataSource as DataTable;
            if (dtData != null)
            {
                businessPrint.Preview(dtData);
            }
        }
        #endregion

    }
}
