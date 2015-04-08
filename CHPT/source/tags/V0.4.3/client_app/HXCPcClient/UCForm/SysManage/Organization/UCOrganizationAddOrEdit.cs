using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Utility.Common;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.SysManage.Organization
{
    /// <summary>
    /// 组织管理 复制编辑新增
    /// 孙明生
    /// </summary>
    public partial class UCOrganizationAddOrEdit : UCBase
    {
        #region 属性
        /// <summary>
        /// 组织id
        /// </summary>
        public string strOrg_id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOrganizationManager uc;

        public WindowStatus wStatus;

        //上级ID
        public string parent_id = string.Empty;

        #endregion

        #region 初始化
        public UCOrganizationAddOrEdit()
        {
            InitializeComponent();
            base.SaveEvent += new ClickHandler(UCOrganizationAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCOrganizationAddOrEdit_CancelEvent);
        }
        #endregion

        #region Load
        private void UCOrganizationAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus(wStatus);
            BindTree();
            if (wStatus == WindowStatus.Add)
            {
                if (parent_id != string.Empty)
                {
                    TreeNode[] nodes = tvCompany.Nodes.Find(parent_id, true);
                    if (nodes.Length > 0)
                    {
                        tvCompany.SelectedNode = nodes[0];
                    }
                }
                SetAuthorAddManageBtn();
            }
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                DataTable dt = DBHelper.GetTable("查询组织", "tb_organization", "*", "org_id='" + strOrg_id + "'", "", "");
                if (dt.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("查询组织失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonCtrl.SelectTreeView(tvCompany, dt.Rows[0]["parent_id"].ToString());

                txtparent_id.Caption = tvCompany.SelectedNode.Text;
                txtorg_code.Caption = dt.Rows[0]["org_code"].ToString();
                txtorg_name.Caption = dt.Rows[0]["org_name"].ToString();
                txtorg_short_name.Caption = dt.Rows[0]["org_short_name"].ToString();
                txtcontact_name.Caption = dt.Rows[0]["contact_name"].ToString();
                txtcontact_telephone.Caption = dt.Rows[0]["contact_telephone"].ToString();
                txtremark.Caption = dt.Rows[0]["remark"].ToString();
                SetAuthorEditManageBtn();
            }
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region 控件内容验证
        /// <summary>
        /// 控件内容验证
        /// </summary>
        /// <param name="msg">返回提示信息</param>
        /// <returns></returns>
        private bool Validator(ref string msg)
        {
            if (tvCompany.SelectedNode == null)
            {
                Utility.Common.Validator.SetError(errorProvider1, txtparent_id, "请选择上级组织!");
                return false;
            }
            if (string.IsNullOrEmpty(txtorg_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtorg_name, "组织名称不能为空!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtcontact_telephone.Caption.Trim()) && !Utility.Common.Validator.IsTel(txtcontact_telephone.Caption.Trim()))
            {
                if (!string.IsNullOrEmpty(txtcontact_telephone.Caption.Trim()) && !Utility.Common.Validator.IsMobile(txtcontact_telephone.Caption.Trim()))
                {
                    Utility.Common.Validator.SetError(errorProvider1, txtcontact_telephone, "联系电话格式不正确!");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 保存
        ///<summary>
        /// 保存 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                string msg = "";
                bool bln = Validator(ref msg);
                if (!bln)
                {
                    return;
                }
                string newGuid;
                string currOrg_id = ""; ;

                string keyName = string.Empty;
                string keyValue = string.Empty;
                string opName = "新增组织管理";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();

                dicFileds.Add("org_name", txtorg_name.Caption.Trim());//组织全名
                dicFileds.Add("org_short_name", txtorg_short_name.Caption.Trim());//组织简称
                dicFileds.Add("contact_name", txtcontact_name.Caption.Trim());//联系人
                dicFileds.Add("contact_telephone", txtcontact_telephone.Caption.Trim());//联系电话                
                dicFileds.Add("remark", txtremark.Caption.Trim());//备注 


                string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
                dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
                dicFileds.Add("update_time", nowUtcTicks);

                if (tvCompany.SelectedNode != null)
                {
                    DataRowView drv = tvCompany.SelectedNode.Tag as DataRowView;
                    if (drv["ftype"].ToString() == "1")//1为公司 0为组织
                    {
                        dicFileds.Add("parent_id", drv["id"].ToString());
                        dicFileds.Add("com_id", drv["ocom_id"].ToString());//所属公司
                    }
                    else
                    {
                        dicFileds.Add("parent_id", drv["id"].ToString());
                        dicFileds.Add("com_id", drv["id"].ToString());//所属公司
                    }
                }



                if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                {
                    string strcode = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Organization);
                    dicFileds.Add("org_code", strcode);//组织编码
                    //txtorg_code.Caption = strcode;

                    newGuid = Guid.NewGuid().ToString();
                    currOrg_id = newGuid;
                    dicFileds.Add("org_id", newGuid);//新ID                   
                    dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                    dicFileds.Add("create_time", nowUtcTicks);

                    dicFileds.Add("status", Convert.ToInt16(SYSModel.DataSources.EnumStatus.Start).ToString());//启用
                    dicFileds.Add("data_sources", Convert.ToInt16(SYSModel.DataSources.EnumDataSources.SELFBUILD).ToString());//来源 自建

                    dicFileds.Add("enable_flag", SYSModel.DataSources.EnumEnableFlag.USING.ToString("d"));
                }
                else if (wStatus == WindowStatus.Edit)
                {
                    keyName = "org_id";
                    keyValue = strOrg_id;
                    currOrg_id = strOrg_id;
                    opName = "更新组织管理";
                }
                bln = DBHelper.Submit_AddOrEdit(opName, "tb_organization", keyName, keyValue, dicFileds);
                if (bln)
                {
                    LocalCache._Update(CacheList.Org);
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                    MessageProcessor.UpdateComOrgInfo();
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
                InitTree(this.tvCompany.Nodes, "-1", ds.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.Nodes[0].Expand();
                }
            }
        }

        private void tvCompany_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtparent_id.Caption = tvCompany.SelectedNode.Text.ToString();
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

    }
}
