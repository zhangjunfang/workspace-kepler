using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HXCServerWinForm.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Collections;
using BLL;
using HXC_FuncUtility;

namespace HXCServerWinForm.UCForm.Role
{
    /// <summary>
    /// 角色添加修改复制 角色管理
    /// 孙明生
    /// </summary>
    public partial class UCRoleAddOrEdit : UCBase
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
        /// <summary>
        /// 编辑后的用户id集合
        /// </summary>
        ArrayList userIdList = new ArrayList();
        /// <summary>
        /// 修改时 初始userRole记录数
        /// </summary>
        int iUserRoleCount = 0;
        /// <summary>
        /// 编辑后的菜单权限
        /// </summary>
        DataTable dtFun;
        /// <summary>
        /// 编辑的菜单id 即删除的ids
        /// </summary>
        ArrayList funIdList = new ArrayList();
        /// <summary>
        /// 当前编辑的角色id
        /// </summary>
        string role_code;
        /// <summary>
        /// 用户ID
        /// </summary>
        private string UserId
        {
            get
            {
                if (dgvUser.CurrentRow == null)
                {
                    return string.Empty;
                }
                object user_id = dgvUser.CurrentRow.Cells["user_id"].Value;
                if (user_id == null)
                {
                    return string.Empty;
                }
                else
                {
                    return user_id.ToString();
                }
            }
        }
        #endregion

        #region 初始化
        public UCRoleAddOrEdit()
        {
            InitializeComponent();
            base.SaveEvent += new ClickHandler(UCRoleAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCRoleAddOrEdit_CancelEvent);
        }
        #endregion

        #region Load
        private void UCRoleAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(uc.Name);
            DataGridViewEx.SetDataGridViewStyle(dgvFunction);
            dgvFunction.ReadOnly = false;
            base.SetBtnStatus(windowStatus);
            dtFunction();
            if (windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.View || windowStatus == WindowStatus.Copy)
            {
                DataTable dt = DBHelper.GetTable("查询角色", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_role", "*", "role_id='" + id + "'", "", "");
                if (dt.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("查询角色失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                role_code = dt.Rows[0]["role_code"].ToString();
                if (role_code == "system")
                {
                    txtRole_code.Enabled = false;
                    txtRole_name.Enabled = false;
                }
                txtRole_code.Caption = dt.Rows[0]["role_code"].ToString();
                txtRole_name.Caption = dt.Rows[0]["Role_name"].ToString();
                txtremark.Caption = dt.Rows[0]["remark"].ToString();

                string strSql = "select u.user_id,u.user_code,u.user_name,u.user_phone,u.com_name,u.org_id,u.remark,  u.org_name from v_User u ,tr_user_role ur,sys_role r "
                          + " where u.user_id=ur.user_id and r.role_id=ur.role_id and r.enable_flag='1' and  r.role_id='" + id + "' ";

                SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
                sqlobj.cmdType = CommandType.Text;
                sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
                sqlobj.sqlString = strSql;
                DataSet ds = DBHelper.GetDataSet("查询用户角色关系", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlobj);
                //dgvUser.DataSource = ds.Tables[0].DefaultView;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        userIdList.Add(dr["user_id"].ToString());
                        DataGridViewRow gvr = dgvUser.Rows[dgvUser.Rows.Add()];
                        gvr.Cells["user_id"].Value = dr["user_id"];
                        gvr.Cells["user_code"].Value = dr["user_code"];
                        gvr.Cells["user_name"].Value = dr["user_name"];
                        gvr.Cells["com_name"].Value = dr["com_name"];
                        gvr.Cells["org_name"].Value = dr["org_name"];
                        gvr.Cells["user_phone"].Value = dr["user_phone"];
                    }
                    iUserRoleCount = ds.Tables[0].Rows.Count;
                }
            }
            else
            {
                txtRole_code.Caption = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Role);

            }
            bindTree();
            if (windowStatus == WindowStatus.View)
            {
                Common.SetControlForLable(this, true);
                dgvUser.ContextMenuStrip = null;
            }
        }
        #endregion

        #region 事件

        ///<summary> 保存 
        /// </summary>
        void UCRoleAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                errProvider.Clear();
                string msg = "";
                bool bln = Validator(ref msg);
                if (!bln)
                {
                    return;
                }
                string newGuid;
                string currRole_id = "";

                string keyName = string.Empty;
                string keyValue = string.Empty;
                string opName = "新增角色管理";
                Dictionary<string, string> dicFileds = new Dictionary<string, string>();

                dicFileds.Add("Role_name", txtRole_name.Caption.Trim());//角色全名               
                dicFileds.Add("remark", txtremark.Caption.Trim());//备注 


                string nowUtcTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString();
                dicFileds.Add("update_by", GlobalStaticObj_Server.Instance.UserID);
                dicFileds.Add("update_time", nowUtcTicks);

                if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
                {
                    string strcode = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Role);
                    dicFileds.Add("Role_code", strcode);//角色编码
                    //txtRole_code.Caption = strcode;

                    newGuid = Guid.NewGuid().ToString();
                    currRole_id = newGuid;
                    dicFileds.Add("role_id", newGuid);//新ID                   
                    dicFileds.Add("create_by", GlobalStaticObj_Server.Instance.UserID);
                    dicFileds.Add("create_time", nowUtcTicks);

                    dicFileds.Add("state", Convert.ToInt16(SYSModel.DataSources.EnumStatus.Start).ToString());//启用
                    dicFileds.Add("data_sources", Convert.ToInt16(SYSModel.DataSources.EnumDataSources.SELFBUILD).ToString());//来源 自建
                    dicFileds.Add("enable_flag", "1");

                    bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_role", keyName, keyValue, dicFileds);
                    if (bln)
                    {
                        if (userIdList.Count > 0)
                        { //添加角色用户关系
                            bln = AddUserRole(newGuid, userIdList);
                        }
                        if (bln && dtFun.Rows.Count > 0)
                        {
                            //添加角色菜单权限
                            bln = addFunctionRole(newGuid, dtFun);
                        }
                    }
                }
                else if (windowStatus == WindowStatus.Edit)
                {
                    keyName = "role_id";
                    keyValue = id;
                    currRole_id = id;
                    opName = "编辑用户角色关系";
                    bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_role", keyName, keyValue, dicFileds);
                    if (bln)
                    {
                        if (iUserRoleCount == 0)
                        {
                            if (userIdList.Count > 0)
                                bln = AddUserRole(id, userIdList);
                        }
                        else
                        {
                            if (userIdList.Count > 0)
                            {
                                bln = UpdateUserRole(id, userIdList);
                            }
                            else
                            {
                                bln = DeleteUserRole(id);
                            }
                        }
                        if (bln)
                        {
                            if (funIdList.Count > 0)
                            {
                                bln = DeleteFunRole(id, funIdList);
                            }
                            if (dtFun.Rows.Count > 0)
                            {
                                //添加角色菜单权限
                                bln = addFunctionRole(id, dtFun);
                            }
                        }
                    }
                    iUserRoleCount = dgvUser.Rows.Count;
                }
                if (bln)
                {
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindData();
                    deleteMenuByTag(this.Tag.ToString(), uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 取消
        /// </summary>
        void UCRoleAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                deleteMenuByTag(this.Tag.ToString(), uc.Name);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        #region 树节点点击事件
        private void tvFunction_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Name == "S_ROOT")
                {
                    dgvFunction.DataSource = null;
                    return;
                }
                DataView dv = new DataView(dtFun);// fun level 0 1 2
                dv.RowFilter = string.Format("fun_level='2' and (fun_id='{0}' or parent_id='{0}')", e.Node.Name);
                dgvFunction.DataSource = dv;
                return;
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion

        #region 菜单dgv事件
        private void dgvFunction_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Cancel = true;
                }
                if (e.ColumnIndex > 1 && dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        private void dgvFunction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex >= 0 && dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
            {
                try
                {
                    DataGridViewCell cell = dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    bool ifcheck1 = Convert.ToBoolean(cell.FormattedValue);
                    bool cellCheckBox = Convert.ToBoolean(cell.EditedFormattedValue);
                    if (e.ColumnIndex == 1)//当单击复选框，同时处于组合编辑状态时
                    {
                        for (int i = 1; i < dgvFunction.Columns.Count - 2; i++)
                        {
                            if (dgvFunction.Rows[e.RowIndex].Cells[i].Value != DBNull.Value)
                                dgvFunction.Rows[e.RowIndex].Cells[i].Value = cellCheckBox;
                        }
                    }
                    else
                    {
                        if (!cellCheckBox)
                        {
                            dgvFunction.Rows[e.RowIndex].Cells[1].Value = false;
                        }
                    }
                    bool isChecked = false; //行复选框 是否有选中项  有一个选中 即行为有效数据
                    dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellCheckBox;//点击改变当前复选框状态
                    if (cellCheckBox)
                    {
                        isChecked = true;
                    }
                    else
                    {
                        for (int i = 1; i < dgvFunction.Columns.Count - 2; i++)
                        {
                            if (dgvFunction.Rows[e.RowIndex].Cells[i].Value != DBNull.Value && Convert.ToBoolean(dgvFunction.Rows[e.RowIndex].Cells[i].Value) && i != e.ColumnIndex)
                            {
                                isChecked = true;
                                break;
                            }
                        }
                    }
                    if (funIdList.BinarySearch(dgvFunction.Rows[e.RowIndex].Cells["fun_id"].Value.ToString()) == -1)
                        funIdList.Add(dgvFunction.Rows[e.RowIndex].Cells["fun_id"].Value.ToString());//id添加到funIdList
                    DataRow dRow = (dgvFunction.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                    #region 删除重建
                    DataRow dr = dtFun.NewRow();
                    foreach (DataRow row in dtFun.Rows)
                    {
                        if (row["fun_id"] == dRow["fun_id"])
                        {
                            dr = row;
                            break;
                        }
                    }
                    dr = ConvertDtRow(dtFun, dRow);
                    #endregion
                }
                catch (Exception ex)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("角色管理", ex);
                    MessageBoxEx.ShowWarning("程序异常");
                }
            }
        }
        #endregion
        #endregion

        #region 方法
        /// <summary>  dtFun表结构
        /// </summary>
        private void dtFunction()
        {
            StringBuilder strBD;
            if (windowStatus == WindowStatus.Add)
            {
                strBD = new StringBuilder();
                strBD.Append(" select fun_id,num,fun_name,fun_ename,fun_uri ,fun_img,parent_id,fun_cbs ,fun_level ,fun_idx ,fun_flag ,fun_run ");
                strBD.Append(",CONVERT(bit,button_add) button_add ,CONVERT(bit,button_edit) button_edit ");
                strBD.Append(" ,CONVERT(bit,button_delete)button_delete,CONVERT(bit,button_save ) button_save ,CONVERT(bit,button_cancel) button_cancel ");
                strBD.Append(" ,CONVERT(bit,button_view) button_view,CONVERT(bit,button_status) button_status,CONVERT(bit,button_download) button_download,CONVERT(bit,button_shotoff) button_shotoff,CONVERT(bit,button_set) button_set,CONVERT(bit,button_refresh) button_refresh,CONVERT(bit,button_viewlist) button_viewlist,CONVERT(bit,button_resetpwd) button_resetpwd,CONVERT(bit,button_backup) button_backup ");
                strBD.Append("  ,CONVERT(bit,button_restore) button_restore,CONVERT(bit,button_import) button_import,CONVERT(bit,button_export) button_export  ");
                strBD.Append(" ,CONVERT(bit,button_print) button_print ,CONVERT(bit,button_preview) button_preview ");
                strBD.Append(" ,CONVERT(bit,button_sync) button_sync,CONVERT(bit,button_oprecord) button_oprecord, ");
                strBD.Append(" CONVERT(bit, '0') as isall  from sys_function where fun_run='1'  and fun_flag='1' and enable_flag='1' ");

            }
            else
            {
                strBD = new StringBuilder();
                strBD.Append("select  ");
                strBD.Append("f.fun_id,CONVERT(int,f.num) num,f.fun_name,f.fun_ename,f.fun_uri ,f.fun_img,f.parent_id,f.fun_cbs ,CONVERT(int,f.fun_level) fun_level,f.fun_idx ,CONVERT(int,f.fun_flag) fun_flag,CONVERT(int,f.fun_run) fun_run ");
                strBD.Append(",CONVERT(bit,rf.button_add) button_add ,CONVERT(bit,rf.button_edit) button_edit ");
                strBD.Append(" ,CONVERT(bit,rf.button_delete)button_delete,CONVERT(bit,rf.button_save ) button_save ,CONVERT(bit,rf.button_cancel) button_cancel ");
                strBD.Append(" ,CONVERT(bit,rf.button_view) button_view,CONVERT(bit,rf.button_status) button_status,CONVERT(bit,rf.button_download) button_download,CONVERT(bit,rf.button_shotoff) button_shotoff,CONVERT(bit,rf.button_set) button_set,CONVERT(bit,rf.button_refresh) button_refresh,CONVERT(bit,rf.button_viewlist) button_viewlist,CONVERT(bit,rf.button_resetpwd) button_resetpwd,CONVERT(bit,rf.button_backup) button_backup ");
                strBD.Append("  ,CONVERT(bit,rf.button_restore) button_restore,CONVERT(bit,rf.button_import) button_import,CONVERT(bit,rf.button_export) button_export  ");
                strBD.Append(" ,CONVERT(bit,rf.button_print) button_print ,CONVERT(bit,rf.button_preview) button_preview ");
                strBD.Append(" ,CONVERT(bit,rf.button_sync) button_sync,CONVERT(bit,rf.button_oprecord) button_oprecord "); ;
                strBD.Append("from sys_function f ,tr_role_function rf,sys_role r ");
                strBD.Append("where  ");
                strBD.Append("rf.role_id =r.role_id and rf.fun_id=f.fun_id   ");
                strBD.Append("and r.role_id='" + id + "' ");
                strBD.Append("and f.fun_flag='1' and f.enable_flag='1'  ");
                strBD.Append("order by f.fun_idx; ");
            }

            SYSModel.SQLObj sobj = new SYSModel.SQLObj();
            sobj.cmdType = CommandType.Text;
            sobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sobj.sqlString = strBD.ToString();
            dtFun = DBHelper.GetDataSet("查询角色权限", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sobj).Tables[0];

        }

        /// <summary> 控件内容验证
        /// </summary>
        /// <param name="msg">返回提示信息</param>
        /// <returns></returns>
        private bool Validator(ref string msg)
        {
            if (string.IsNullOrEmpty(txtRole_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errProvider, txtRole_name, "角色名称不能为空!");
                return false;
            }
            return true;
        }

        /// <summary> 删除角色菜单关联
        /// </summary> 
        private bool DeleteFunRole(string id, ArrayList funIdList)
        {
            bool bln = false;
            foreach (string fun_id in funIdList)
            {
                bln = DBHelper.BatchDeleteDataByWhere("删除角色菜单权限", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_role_function", "role_id='" + id + "' and fun_id = '" + fun_id + "'");
                if (bln == false)
                    break;
            }
            return bln;
        }

        /// <summary> 添加角色菜单权限
        /// </summary>
        /// <param name="RoleID">角色id</param>
        /// <param name="dt">菜单列表</param>
        /// <returns></returns>
        private bool addFunctionRole(string RoleID, DataTable dt)
        {
            bool bln = false;
            try
            {
                DBHelper.ExtNonQuery("删除角色关系", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "delete from tr_role_function where role_id='" + id + "'", CommandType.Text, null);
                foreach (DataRow dr in dt.Rows)
                {
                    string keyName = string.Empty;
                    string keyValue = string.Empty;
                    string opName = "新增角色菜单权限";
                    Dictionary<string, string> uFileds = new Dictionary<string, string>();
                    uFileds.Add("role_fun_id", Guid.NewGuid().ToString());
                    uFileds.Add("fun_id", dr["fun_id"].ToString());
                    uFileds.Add("role_id", RoleID);
                    if (dr["button_add"] != DBNull.Value)
                    {
                        if ((bool)dr["button_add"])
                            uFileds.Add("button_add", "1");
                        else
                            uFileds.Add("button_add", "0");
                    }
                    if (dr["button_edit"] != DBNull.Value)
                    {
                        if ((bool)dr["button_edit"])
                            uFileds.Add("button_edit", "1");
                        else
                            uFileds.Add("button_edit", "0");
                    }
                    if (dr["button_delete"] != DBNull.Value)
                    {
                        if ((bool)dr["button_delete"])
                            uFileds.Add("button_delete", "1");
                        else
                            uFileds.Add("button_delete", "0");
                    }
                    if (dr["button_save"] != DBNull.Value)
                    {
                        if ((bool)dr["button_save"])
                            uFileds.Add("button_save", "1");
                        else
                            uFileds.Add("button_save", "0");
                    }
                    if (dr["button_cancel"] != DBNull.Value)
                    {
                        if ((bool)dr["button_cancel"])
                            uFileds.Add("button_cancel", "1");
                        else
                            uFileds.Add("button_cancel", "0");
                    }
                    if (dr["button_view"] != DBNull.Value)
                    {
                        if ((bool)dr["button_view"])
                            uFileds.Add("button_view", "1");
                        else
                            uFileds.Add("button_view", "0");
                    }
                    if (dr["button_status"] != DBNull.Value)
                    {
                        if ((bool)dr["button_status"])
                            uFileds.Add("button_status", "1");
                        else
                            uFileds.Add("button_status", "0");
                    }
                    if (dr["button_download"] != DBNull.Value)
                    {
                        if ((bool)dr["button_download"])
                            uFileds.Add("button_download", "1");
                        else
                            uFileds.Add("button_download", "0");
                    }
                    if (dr["button_shotoff"] != DBNull.Value)
                    {
                        if ((bool)dr["button_shotoff"])
                            uFileds.Add("button_shotoff", "1");
                        else
                            uFileds.Add("button_shotoff", "0");
                    }
                    if (dr["button_set"] != DBNull.Value)
                    {
                        if ((bool)dr["button_set"])
                            uFileds.Add("button_set", "1");
                        else
                            uFileds.Add("button_set", "0");
                    }
                    if (dr["button_refresh"] != DBNull.Value)
                    {
                        if ((bool)dr["button_refresh"])
                            uFileds.Add("button_refresh", "1");
                        else
                            uFileds.Add("button_refresh", "0");
                    }
                    if (dr["button_viewlist"] != DBNull.Value)
                    {
                        if ((bool)dr["button_viewlist"])
                            uFileds.Add("button_viewlist", "1");
                        else
                            uFileds.Add("button_viewlist", "0");
                    }
                    if (dr["button_resetpwd"] != DBNull.Value)
                    {
                        if ((bool)dr["button_resetpwd"])
                            uFileds.Add("button_resetpwd", "1");
                        else
                            uFileds.Add("button_resetpwd", "0");
                    }

                    if (dr["button_backup"] != DBNull.Value)
                    {
                        if ((bool)dr["button_backup"])
                            uFileds.Add("button_backup", "1");
                        else
                            uFileds.Add("button_backup", "0");
                    }
                    if (dr["button_restore"] != DBNull.Value)
                    {
                        if ((bool)dr["button_restore"])
                            uFileds.Add("button_restore", "1");
                        else
                            uFileds.Add("button_restore", "0");
                    }
                    if (dr["button_import"] != DBNull.Value)
                    {
                        if ((bool)dr["button_import"])
                            uFileds.Add("button_import", "1");
                        else
                            uFileds.Add("button_import", "0");
                    }
                    if (dr["button_export"] != DBNull.Value)
                    {
                        if ((bool)dr["button_export"])
                            uFileds.Add("button_export", "1");
                        else
                            uFileds.Add("button_export", "0");
                    }
                    if (dr["button_print"] != DBNull.Value)
                    {
                        if ((bool)dr["button_print"])
                            uFileds.Add("button_print", "1");
                        else
                            uFileds.Add("button_print", "0");
                    }
                    if (dr["button_preview"] != DBNull.Value)
                    {
                        if ((bool)dr["button_preview"])
                            uFileds.Add("button_preview", "1");
                        else
                            uFileds.Add("button_preview", "0");
                    }

                    if (dr["button_sync"] != DBNull.Value)
                    {
                        if ((bool)dr["button_sync"])
                            uFileds.Add("button_sync", "1");
                        else
                            uFileds.Add("button_sync", "0");
                    }

                    if (dr["button_oprecord"] != DBNull.Value)
                    {
                        if ((bool)dr["button_oprecord"])
                            uFileds.Add("button_oprecord", "1");
                        else
                            uFileds.Add("button_oprecord", "0");
                    }
                    bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_role_function", keyName, keyValue, uFileds);
                    if (bln == false)
                        break;
                }
                return bln;
            }
            catch
            {
                return false;
            }
        }


        /// <summary> 删除UserRole
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns>bool</returns>
        private bool DeleteUserRole(string roleid)
        {
            string keyName = "role_id";
            string keyValue = roleid;
            string opName = "删除用户角色关系";
            return DBHelper.DeleteDataByID(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_user_role", keyName, keyValue);
        }

        /// <summary>　UserRole先删除在添加
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="userIdList">用户id list</param>
        /// <returns>bool</returns>
        private bool UpdateUserRole(string roleid, ArrayList userIdList)
        {
            bool bln = false;
            bln = DeleteUserRole(roleid);
            if (bln)
                bln = AddUserRole(roleid, userIdList);
            return bln;
        }

        /// <summary> 添加角色用户关系
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="userIdList">用户Id</param>
        private bool AddUserRole(string roleid, ArrayList userIdList)
        {
            bool bln = false;
            try
            {
                foreach (string str in userIdList)
                {
                    string keyName = string.Empty;
                    string keyValue = string.Empty;
                    string opName = "新增角色用户关系";
                    Dictionary<string, string> uFileds = new Dictionary<string, string>();
                    uFileds.Add("user_role_id", Guid.NewGuid().ToString());
                    uFileds.Add("user_id", str);
                    uFileds.Add("role_id", roleid);
                    bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_user_role", keyName, keyValue, uFileds);
                    if (bln == false)
                        break;
                }
                return bln;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>　绑定功能菜单树tree　
        /// </summary>
        private void bindTree()
        {
            string fileds = "fun_id as id,fun_name as name,parent_id,fun_idx,fun_run,fun_level";
            DataTable dt = DBHelper.GetTable("查询功能菜单", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_function", fileds, "enable_flag='1' and fun_flag='1' and fun_cbs='1'", "", "order by fun_idx");//fun_level<=3 and 
            CommonCtrl.InitTree(tvFunction.Nodes, "-1", dt.DefaultView);
            tvFunction.ExpandAll();
        }

        /// <summary>　添加操作员
        /// </summary>　
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            string user_id = "";
            string user_code = "";
            string user_name = "";
            Chooser.frmUsers fm = new Chooser.frmUsers();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                user_id = fm.User_ID;
                user_code = fm.User_Code;
                user_name = fm.User_Name;
                foreach (DataGridViewRow row in dgvUser.Rows)
                {
                    if (row.Cells["user_id"].Value.ToString() == user_id)
                    {
                        row.Selected = true;
                        return;
                    }
                }
                string strCount;
                if (windowStatus == WindowStatus.Edit)
                {
                    strCount = DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_user_role", "count(*)", "user_id='" + user_id + "' and role_id!='" + id + "'", "");
                    // strsql = "select user_id  from tr_user_role where user_id='" + user_id + "' and role_id!='" + id + "'";
                }
                else
                {
                    strCount = DBHelper.GetSingleValue("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tr_user_role", "count(*)", "user_id='" + user_id + "'", "");
                    //strsql = "select user_id  from tr_user_role where user_id='" + user_id + "'";
                }
                if (strCount != "0")
                {
                    MessageBoxEx.Show("该用户已经分配到其它角色,请选择其他用户!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataGridViewRow gvr = dgvUser.Rows[dgvUser.Rows.Add()];
                gvr.Cells["user_id"].Value = user_id;
                gvr.Cells["user_code"].Value = user_code;
                gvr.Cells["user_name"].Value = user_name;
                DataTable dt = DBHelper.GetTable("获取用户信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "v_user", "*", "user_id='" + user_id + "'", "", "");
                if (dt.Rows.Count > 0)
                {
                    gvr.Cells["org_name"].Value = dt.Rows[0]["org_name"].ToString();
                    gvr.Cells["com_name"].Value = dt.Rows[0]["com_name"].ToString();
                    gvr.Cells["user_phone"].Value = dt.Rows[0]["user_phone"].ToString();
                }
                userIdList.Add(user_id);
            }
        }

        /// <summary> 删除操作员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDel_Click(object sender, EventArgs e)
        {
            string id = UserId;
            if (dgvUser.CurrentRow == null)
            {
                MessageBoxEx.ShowInformation("请选择删除记录");
                return;
            }
            dgvUser.Rows.Remove(dgvUser.CurrentRow);
            userIdList.Remove(id);
        }

        /// <summary> dtFun行数据
        /// </summary> 
        private DataRow ConvertDtRow(DataTable dt, DataRow dRow)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dRow.Table.Columns.Contains(dc.ColumnName))
                {
                    dr[dc.ColumnName] = dRow[dc.ColumnName];
                }
            }
            return dr;
        }
        #endregion
    }
}