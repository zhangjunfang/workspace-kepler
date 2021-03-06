﻿using System;
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

            this.page.PageIndexChanged += new ServiceStationClient.ComponentUI.WinFormPager.EventHandler(this.page_PageIndexChanged);

        }
        #endregion

        #region 公司组织树
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
            DataSet ds = DBHelper.GetDataSet("查询公司组织树", sqlobj);

            tvCompany.Nodes.Clear();


            //节点加上去
            if (ds.Tables[0].Rows.Count > 0)
            {
                //clsGetTree cls = new clsGetTree();
                CommonCtrl.InitTree(this.tvCompany.Nodes, "-1", ds.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.Nodes[0].Expand();
                }
            }
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
                if (MessageBoxEx.ShowQuestion("是否确认删除?"))
                {
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
            base.addUserControl(OrganizationAdd, "组织管理-新增", "OrganizationAdd", this.Tag.ToString(), this.Name);
            //订阅新增的保存事件
            OrganizationAdd.SaveEvent += new ClickHandler(OrganizationAdd_SaveEvent);
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
                base.addUserControl(uc, "组织管理-浏览", "OrganizationView" + org_id, this.Tag.ToString(), this.Name);

            }
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtorg_code.Caption = string.Empty;
            this.txtcontact_name.Caption = string.Empty;
            this.txtorg_name.Caption = string.Empty;

            this.dtpcreate_time.Value = DateTime.Now.AddMonths(-1);
            this.dtpcreate_time_end.Value = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
        }
    }
}
