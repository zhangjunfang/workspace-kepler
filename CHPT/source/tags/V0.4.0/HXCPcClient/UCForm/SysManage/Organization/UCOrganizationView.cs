using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.SysManage.Organization
{
    /// <summary>
    /// 组织管理 预览
    /// 孙明生
    /// </summary>
    public partial class UCOrganizationView : UCBase
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public string id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOrganizationManager uc;
        public WindowStatus wStatus;
        public UCOrganizationView()
        {
            InitializeComponent();
            base.CancelEvent += new ClickHandler(UCOrganizationView_CancelEvent);
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCOrganizationView_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #region Load
        private void UCOrganizationView_Load(object sender, EventArgs e)
        {
            //base.RoleButtonStstus(uc.Name);//角色按钮权限-是否隐藏
            //base.SetBtnStatus(wStatus);
            DataSources.BindComBoxDataEnum(cbostate, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            BindTree();
            //            string strSql = "SELECT o.*,(select USER_NAME from sys_user where user_id =o.create_by )as create_Username , "
            //+ "(select USER_NAME from sys_user where user_id =o.update_by ) as update_username  FROM  tb_organization o where org_id='" + id + "'";

            //SELECT o.*,(select USER_NAME from sys_user where user_id =o.create_by )as create_Username ,
            //(select USER_NAME from sys_user where user_id =o.update_by ) as update_username ,
            //po.org_code as parentcode,po.org_name as parentname ,c.com_name ,c.com_code FROM  tb_organization o 
            //left join tb_organization po on o.parent_id=po.org_id
            //left join tb_company c on o.com_id=c.com_id
            //where o.org_id='8b4471da-b9bd-4d59-b3f1-af1841dc3ab5'
            string strSql = "SELECT o.*,(select USER_NAME from sys_user where user_id =o.create_by )as create_Username,";
            strSql += "(select USER_NAME from sys_user where user_id =o.update_by ) as update_username ,";
            strSql += "po.org_code as parentcode,po.org_name as parentname ,c.com_name ,c.com_code FROM  tb_organization o ";
            strSql += "left join tb_organization po on o.parent_id=po.org_id ";
            strSql += "left join tb_company c on o.com_id=c.com_id ";
            strSql += "where o.org_id='" + id + "'";
            SQLObj sqlobj = new SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询组织", sqlobj);

            //DBHelper.GetTable("查询组织", "tb_organization", "*", "org_id='" + id + "'", "", "");
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                MessageBoxEx.Show("查询组织失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = ds.Tables[0];
            CommonCtrl.SelectTreeView(tvCompany, dt.Rows[0]["parent_id"].ToString());
            if (dt.Rows[0]["parentname"] != null && dt.Rows[0]["parentname"].ToString() != string.Empty)
            { lblporg_name.Text = dt.Rows[0]["parentname"].ToString(); }
            else
            { lblporg_name.Text = CommonCtrl.IsNullToString(dt.Rows[0]["com_name"]); }
            if (dt.Rows[0]["parentcode"] != null && dt.Rows[0]["parentcode"].ToString() != string.Empty)
            { lblporg_code.Text = dt.Rows[0]["parentcode"].ToString(); }
            else
            { lblporg_code.Text = CommonCtrl.IsNullToString(dt.Rows[0]["com_code"]); }


            lblorg_code.Text = dt.Rows[0]["org_code"].ToString();
            lblorg_name.Text = dt.Rows[0]["org_name"].ToString();
            lblorg_short_name.Text = dt.Rows[0]["org_short_name"].ToString();
            lblremark.Text = dt.Rows[0]["remark"].ToString();
            lblcontact_name.Text = dt.Rows[0]["contact_name"].ToString();
            lblcontact_telephone.Text = dt.Rows[0]["contact_telephone"].ToString();
            lblcreate_Username.Text = dt.Rows[0]["create_Username"].ToString();
            lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["create_time"].ToString())).ToString();
            lblupdate_username.Text = dt.Rows[0]["update_username"].ToString();
            lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["update_time"].ToString())).ToString();
            cbostate.SelectedValue = dt.Rows[0]["status"].ToString();
            SetSysBtnView();
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
                CommonCtrl.InitTree(this.tvCompany.Nodes, "-1", ds.Tables[0].DefaultView);
                if (tvCompany.Nodes.Count > 0)
                {
                    tvCompany.ExpandAll();//.Nodes[0].Expand();
                }
            }
        }
        #endregion
    }
}
