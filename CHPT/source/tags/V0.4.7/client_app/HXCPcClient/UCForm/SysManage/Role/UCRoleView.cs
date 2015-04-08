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
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.Role
{
    /// <summary>
    /// 角色管理预览
    /// 孙明生
    /// </summary>
    public partial class UCRoleView : UCBase
    {
        #region 属性
        /// <summary>
        /// 角色id
        /// </summary>
        public string id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCRoleManager uc;

        public WindowStatus wStatus;
        #endregion

        #region 初始化
        public UCRoleView()
        {
            InitializeComponent();
            base.CancelEvent += new ClickHandler(UCRoleView_CancelEvent);
        }
        #endregion

        #region Load
        private void UCRoleView_Load(object sender, EventArgs e)
        {
            // base.RoleButtonStstus(uc.Name);//角色按钮权限-是否隐藏
            //base.SetBtnStatus(wStatus);
            DataSources.BindComBoxDataEnum(cbostate, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            DataSources.BindComBoxDataEnum(cbodata_sources, typeof(DataSources.EnumDataSources), true);//数据来源 自建 宇通

            DataTable dt = DBHelper.GetTable("查询角色", "v_role", "*", "role_id='" + id + "'", "", "");
            if (dt.Rows.Count <= 0)
            {
                MessageBoxEx.Show("查询角色失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblRole_code.Text = dt.Rows[0]["role_code"].ToString();
            lblRole_name.Text = dt.Rows[0]["Role_name"].ToString();
            lblremark.Text = dt.Rows[0]["remark"].ToString();

            lblcreate_by.Text = dt.Rows[0]["create_Username"].ToString();
            lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["create_time"].ToString())).ToString();
            lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();
            lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["update_time"].ToString())).ToString();
            cbostate.SelectedValue = dt.Rows[0]["state"].ToString();
            cbodata_sources.SelectedValue = dt.Rows[0]["data_sources"].ToString();

            string strSql = "select u.user_id,u.user_code,u.user_name,u.user_phone,u.com_name,u.org_id,u.remark,  u.org_name from v_User u ,tr_user_role ur,sys_role r "
                        + " where u.user_id=ur.user_id and r.role_id=ur.role_id and r.enable_flag='1' and  r.role_id='" + id + "' ";

            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询用户角色关系", sqlobj);
            //dgvUser.DataSource = ds.Tables[0].DefaultView;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataGridViewRow gvr = dgvUser.Rows[dgvUser.Rows.Add()];
                    gvr.Cells["user_id"].Value = dr["user_id"];
                    gvr.Cells["user_code"].Value = dr["user_code"];
                    gvr.Cells["user_name"].Value = dr["user_name"];
                    gvr.Cells["com_name"].Value = dr["com_name"];
                    gvr.Cells["org_name"].Value = dr["org_name"];
                    gvr.Cells["user_phone"].Value = dr["user_phone"];
                    gvr.Cells["remark"].Value = dr["remark"];
                }
            }
            bindTree();
            GetRoleFun("CL_ROOT");
            DataGridViewStyle.DataGridViewBgColor(dgvFunction);
            DataGridViewStyle.DataGridViewBgColor(dgvUser);
            SetSysManageViewBtn();
        }
        #endregion

        #region 取消
        void UCRoleView_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), uc.Name); 
        }
        #endregion

        #region 绑定tree
        private void bindTree()
        {
            string fileds = "fun_id as id,fun_name as name,parent_id,fun_idx,fun_run,fun_level";
            DataTable dt = DBHelper.GetTable("查询功能菜单", "sys_function", fileds, "enable_flag='1' and fun_flag='1' and fun_cbs='2'", "", "order by fun_idx");//fun_level<=3 and 
            CommonCtrl.InitTree(tvFunction.Nodes, "-1", dt.DefaultView);
            if (this.tvFunction.Nodes.Count > 0)
            {
                this.tvFunction.Nodes[0].Expand();
            }
        }
        #endregion

        #region tree节点点击事件
        private void tvFunction_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetRoleFun(e.Node.Name);
        }
        #endregion

        #region 获取菜单权限
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="fun_id">菜单id</param>
        private void GetRoleFun(string fun_id)
        {
            DataSet ds = new DataSet();
            string strSql = " with fun as(  select * from sys_function   where fun_id = '" + fun_id + "' union all  "
                + "select sf.* from fun f inner join sys_function sf  on f.fun_id = sf.parent_id ) "
                + "select fun.fun_id,fun.num,fun.fun_name,fun.fun_ename,fun.fun_uri ,fun.fun_img,fun.parent_id,fun.fun_cbs ,fun.fun_level ,fun.fun_idx ,fun.fun_flag ,fun.fun_run "
                + " ,CONVERT(bit,rf.button_browse) button_browse,CONVERT(bit,rf.button_add) button_add ,CONVERT(bit,rf.button_edit)button_edit "
                + " ,CONVERT(bit,rf.button_copy )button_copy ,CONVERT(bit,rf.button_delete)button_delete,CONVERT(bit,rf.button_cancel) button_cancel "
                + " ,CONVERT(bit,rf.button_save) button_save ,CONVERT(bit,rf.button_submit) button_submit ,CONVERT(bit,rf.button_import) button_import "
                + " ,CONVERT(bit,rf.button_export) button_export ,CONVERT(bit,rf.button_print) button_print ,CONVERT(bit,rf.button_operation_record)button_operation_record "
                + " ,CONVERT(bit,rf.button_dispatching) button_dispatching  "
                + " ,fun.enable_flag ,fun.remark ,fun.create_by,fun.create_time,fun.update_by ,fun.update_time,CONVERT(bit, '0') as isall "
                + " from  fun ,tr_role_function rf,sys_role sr where rf.role_id =sr.role_id and rf.fun_id=fun.fun_id  and sr.role_id='" + id + "' "
                + " and fun.fun_run='1'  and fun.fun_flag='1' and fun.enable_flag='1' ";
            SYSModel.SQLObj sobj = new SYSModel.SQLObj();
            sobj.cmdType = CommandType.Text;
            sobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sobj.sqlString = strSql;
            ds = DBHelper.GetDataSet("查询角色权限", sobj);
            if (ds != null && ds.Tables.Count > 0)
            {
                dgvFunction.DataSource = ds.Tables[0].DefaultView;
            }
        }
        #endregion
      
    }
}
