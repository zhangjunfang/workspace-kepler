using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Collections;
using SYSModel;
using System.Threading;
using System.Linq;

namespace HXCPcClient.UCForm.SysManage.Role
{
    /// <summary>
    /// 角色添加修改复制 角色管理
    /// 孙明生
    /// </summary>
    public partial class UCRoleAddOrEdit : UCBase
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --成员变量
        /// <summary>
        /// 角色id
        /// </summary>
        public string id = string.Empty;
        private DataRow dr;
        private string parentName = string.Empty;
        public WindowStatus wStatus;
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
        DataTable dtEditedFun;
        //需要删除的功能
        List<string> deleteFun = new List<string>();
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
        /// <summary>
        /// 方法比较器
        /// </summary>
        private FunComparer funCompar = new FunComparer();
        #endregion

        #region --构造函数
        public UCRoleAddOrEdit(DataRow dr, string parentName)
        {
            InitializeComponent();
            this.cmsMenu.Items.Insert(0, this.tsmiAdd);
            this.dr = dr;
            this.parentName = parentName;

            DataGridViewEx.SetDataGridViewStyle(this.dgvFunction);
            DataGridViewEx.SetDataGridViewStyle(this.dgvUser);
        }
        #endregion

        #region --窗体初始化
        private void UCRoleAddOrEdit_Load(object sender, EventArgs e)
        {
            //base.RoleButtonStstus(this.parentName);
            //base.SetBtnStatus(wStatus);
            this.dgvFunction.ReadOnly = false;

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.SaveEvent += new ClickHandler(UCRoleAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCRoleAddOrEdit_CancelEvent);
            base.DeleteEvent += new ClickHandler(UCRoleAddOrEdit_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCRoleAddOrEdit_StatusEvent);
            this.CreateFunction();


            if (windowStatus == WindowStatus.Edit || windowStatus == WindowStatus.Copy)
            {
                role_code = this.dr["role_code"].ToString();
                if (role_code == "system")
                {
                    txtRole_code.Enabled = false;
                    txtRole_name.Enabled = false;
                }
                this.txtRole_code.Caption = this.dr["role_code"].ToString();
                this.txtRole_name.Caption = this.dr["Role_name"].ToString();
                this.txtremark.Caption = this.dr["remark"].ToString();

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadData));

            }
            SetBtnStatus();
            this.BindTree();
        }



        /// <summary>
        /// 设置页面按钮状态
        /// </summary>
        private void SetBtnStatus()
        {
            if (windowStatus == WindowStatus.Edit)
            {
                SetSysManageEditBtn();
                if (dr["state"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "停用";

                }
                else
                {
                    btnStatus.Caption = "启用";
                }
            }
            if (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Copy)
            {
                SetSysManageAddBtn();
            }
        }
        #endregion

        #region --显示信息
        private void _LoadData(object obj)
        {
            string strSql = "select u.user_id,u.user_code,u.user_name,u.user_phone,u.com_name,u.org_id,u.remark,u.org_name from v_User u ,tr_user_role ur,sys_role r "
                      + " where u.user_id=ur.user_id and r.role_id=ur.role_id and r.enable_flag='1' and  r.role_id='" + id + "' ";

            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strSql;
            DataSet ds = DBHelper.GetDataSet("查询用户角色关系", sqlobj);

            this.Invoke(this.uiHandler, ds);
        }
        private void ShowData(object obj)
        {
            DataSet ds = obj as DataSet;
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
                    gvr.Cells["remark"].Value = dr["remark"];
                }
                iUserRoleCount = ds.Tables[0].Rows.Count;
            }
        }
        #endregion

        #region --创建dtFun表结构
        /// <summary>
        /// dtFun表结构
        /// </summary>
        private void CreateFunction()
        {
            dtEditedFun = new DataTable();
            dtEditedFun.Columns.Add("fun_id", typeof(string));
            dtEditedFun.Columns.Add("num", typeof(int));
            dtEditedFun.Columns.Add("fun_name", typeof(string));
            dtEditedFun.Columns.Add("fun_ename", typeof(string));
            dtEditedFun.Columns.Add("fun_uri", typeof(string));
            dtEditedFun.Columns.Add("fun_img", typeof(string));
            dtEditedFun.Columns.Add("parent_id", typeof(string));
            dtEditedFun.Columns.Add("fun_cbs", typeof(string));
            dtEditedFun.Columns.Add("fun_level", typeof(int));
            dtEditedFun.Columns.Add("fun_idx", typeof(string));
            dtEditedFun.Columns.Add("fun_flag", typeof(int));
            dtEditedFun.Columns.Add("fun_run", typeof(int));
            dtEditedFun.Columns.Add("isall", typeof(bool));
            dtEditedFun.Columns.Add("button_browse", typeof(bool));
            dtEditedFun.Columns.Add("button_add", typeof(bool));
            dtEditedFun.Columns.Add("button_edit", typeof(bool));
            dtEditedFun.Columns.Add("button_copy", typeof(bool));
            dtEditedFun.Columns.Add("button_delete", typeof(bool));
            dtEditedFun.Columns.Add("button_cancel", typeof(bool));
            dtEditedFun.Columns.Add("button_activate", typeof(bool));
            dtEditedFun.Columns.Add("button_status", typeof(bool));
            dtEditedFun.Columns.Add("button_sync", typeof(bool));
            dtEditedFun.Columns.Add("button_revoke", typeof(bool));
            dtEditedFun.Columns.Add("button_confirm", typeof(bool));
            dtEditedFun.Columns.Add("button_save", typeof(bool));
            dtEditedFun.Columns.Add("button_submit", typeof(bool));
            dtEditedFun.Columns.Add("button_verify", typeof(bool));
            dtEditedFun.Columns.Add("button_import", typeof(bool));
            dtEditedFun.Columns.Add("button_export", typeof(bool));
            dtEditedFun.Columns.Add("button_print", typeof(bool));
            dtEditedFun.Columns.Add("button_operation_record", typeof(bool));
            dtEditedFun.Columns.Add("button_dispatching", typeof(bool));
            dtEditedFun.Columns.Add("button_balance", typeof(bool));
            dtEditedFun.Columns.Add("button_commit", typeof(bool));
            dtEditedFun.Columns.Add("enable_flag", typeof(string));
            dtEditedFun.Columns.Add("remark", typeof(string));
            dtEditedFun.Columns.Add("create_by", typeof(string));
            dtEditedFun.Columns.Add("create_time", typeof(long));
            dtEditedFun.Columns.Add("update_by", typeof(string));
            dtEditedFun.Columns.Add("update_time", typeof(long));


        }

        private void HandleTable(DataTable func)
        {
            for (int i = 0; i < func.Rows.Count; i++)
            {

                if (func.Rows[i]["button_browse"] != DBNull.Value)
                {
                    func.Rows[i]["button_browse"] = false;
                }
                if (func.Rows[i]["button_add"] != DBNull.Value)
                {
                    func.Rows[i]["button_add"] = false;
                }
                if (func.Rows[i]["button_edit"] != DBNull.Value)
                {
                    func.Rows[i]["button_edit"] = false;
                }
                if (func.Rows[i]["button_copy"] != DBNull.Value)
                {
                    func.Rows[i]["button_copy"] = false;
                }
                if (func.Rows[i]["button_delete"] != DBNull.Value)
                {
                    func.Rows[i]["button_delete"] = false;
                }
                if (func.Rows[i]["button_cancel"] != DBNull.Value)
                {
                    func.Rows[i]["button_cancel"] = false;
                }
                if (func.Rows[i]["button_activate"] != DBNull.Value)
                {
                    func.Rows[i]["button_activate"] = false;
                }
                if (func.Rows[i]["button_status"] != DBNull.Value)
                {
                    func.Rows[i]["button_status"] = false;
                }
                if (func.Rows[i]["button_confirm"] != DBNull.Value)
                {
                    func.Rows[i]["button_confirm"] = false;
                }
                if (func.Rows[i]["button_save"] != DBNull.Value)
                {
                    func.Rows[i]["button_save"] = false;
                }
                if (func.Rows[i]["button_submit"] != DBNull.Value)
                {
                    func.Rows[i]["button_submit"] = false;
                }
                if (func.Rows[i]["button_verify"] != DBNull.Value)
                {
                    func.Rows[i]["button_verify"] = false;
                }
                if (func.Rows[i]["button_revoke"] != DBNull.Value)
                {
                    func.Rows[i]["button_revoke"] = false;
                }
                if (func.Rows[i]["button_sync"] != DBNull.Value)
                {
                    func.Rows[i]["button_sync"] = false;
                }
                if (func.Rows[i]["button_import"] != DBNull.Value)
                {
                    func.Rows[i]["button_import"] = false;
                }
                if (func.Rows[i]["button_export"] != DBNull.Value)
                {
                    func.Rows[i]["button_export"] = false;
                }
                if (func.Rows[i]["button_print"] != DBNull.Value)
                {
                    func.Rows[i]["button_print"] = false;
                }
                if (func.Rows[i]["button_operation_record"] != DBNull.Value)
                {
                    func.Rows[i]["button_operation_record"] = false;
                }
                if (func.Rows[i]["button_dispatching"] != DBNull.Value)
                {
                    func.Rows[i]["button_dispatching"] = false;
                }
                if (func.Rows[i]["button_balance"] != DBNull.Value)
                {
                    func.Rows[i]["button_balance"] = false;
                }
                if (func.Rows[i]["button_commit"] != DBNull.Value)
                {
                    func.Rows[i]["button_commit"] = false;
                }

            }
        }
        #endregion

        #region --取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCRoleAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }
        #endregion

        #region --控件内容验证
        /// <summary>
        /// 控件内容验证
        /// </summary>
        /// <param name="msg">返回提示信息</param>
        /// <returns></returns>
        private bool Validator(ref string msg)
        {
            if (string.IsNullOrEmpty(txtRole_name.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(errorProvider1, txtRole_name, "角色名称不能为空!");
                return false;
            }
            return true;
        }
        #endregion

        #region --保存
        ///<summary>
        /// 保存 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCRoleAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();
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


            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            dicFileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            dicFileds.Add("update_time", nowUtcTicks);

            if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
            {
                string strcode = CommonUtility.GetNewNo(SYSModel.DataSources.EnumProjectType.Role);
                dicFileds.Add("Role_code", strcode);//角色编码
                //txtRole_code.Caption = strcode;

                newGuid = Guid.NewGuid().ToString();
                currRole_id = newGuid;
                dicFileds.Add("role_id", newGuid);//新ID                   
                dicFileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                dicFileds.Add("create_time", nowUtcTicks);

                dicFileds.Add("state", Convert.ToInt16(SYSModel.DataSources.EnumStatus.Start).ToString());//启用
                dicFileds.Add("data_sources", Convert.ToInt16(SYSModel.DataSources.EnumDataSources.SELFBUILD).ToString());//来源 自建
                dicFileds.Add("enable_flag", "1");


                bln = DBHelper.Submit_AddOrEdit(opName, "sys_role", keyName, keyValue, dicFileds);
                if (bln)
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }

                    if (userIdList.Count > 0)
                    { //添加角色用户关系
                        bln = AddUserRole(newGuid, userIdList);
                    }
                    if (bln && dtEditedFun.Rows.Count > 0)
                    {
                        //添加角色菜单权限
                        bln = addFunctionRole(newGuid, dtEditedFun);
                    }
                }
            }
            else if (wStatus == WindowStatus.Edit)
            {
                keyName = "role_id";
                keyValue = id;
                currRole_id = id;
                opName = "编辑用户角色关系";
                bln = DBHelper.Submit_AddOrEdit(opName, "sys_role", keyName, keyValue, dicFileds);
                if (bln)
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }

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
                            //bln = DeleteFunRole(id, funIdList);

                        }
                        if (dtEditedFun.Rows.Count > 0)
                        {
                            bln = DeleteFunRole(id);
                            for (int i = 0; i < deleteFun.Count; i++)
                            {
                                DataRow[] delfun = dtEditedFun.Select("fun_id='" + deleteFun[i] + "'");
                                if (delfun != null && delfun.Length > 0)
                                {
                                    dtEditedFun.Rows.Remove(delfun[0]);
                                }
                            }
                        }
                        if (dtEditedFun.Rows.Count > 0)
                        {

                            //添加角色菜单权限
                            bln = addFunctionRole(id, dtEditedFun);
                        }
                    }
                }
                iUserRoleCount = dgvUser.Rows.Count;
            }
            if (bln)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                deleteMenuByTag(this.Tag.ToString(), this.parentName);
                //uc.BindPageData();//.SaveAfter(currRole_id);
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region --删除角色菜单关联
        /// <summary>
        /// 删除角色菜单关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="funIdList"></param>
        /// <returns></returns>
        private bool DeleteFunRole(string id, ArrayList funIdList)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            foreach (string fun_id in funIdList)
            {
                SysSQLString sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                sqlString.sqlString = string.Format("delete from tr_role_function where role_id='{0}' and fun_id ='{1}'", id, fun_id);
                sqlString.Param = new Dictionary<string, string>();
                listSql.Add(sqlString);
            }
            return DBHelper.BatchExeSQLStringMultiByTrans("", listSql);
        }

        /// <summary>
        /// 删除角色菜单关联
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool DeleteFunRole(string id)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            foreach (DataRow func in dtEditedFun.Rows)
            {
                SysSQLString sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                sqlString.sqlString = string.Format("delete from tr_role_function where role_id='{0}' and fun_id ='{1}'", id, func["fun_id"].ToString());
                sqlString.Param = new Dictionary<string, string>();
                listSql.Add(sqlString);
            }

            return DBHelper.BatchExeSQLStringMultiByTrans("", listSql);
        }
        #endregion

        #region --添加角色菜单权限
        /// <summary>
        /// 添加角色菜单权限
        /// </summary>
        /// <param name="RoleID">角色id</param>
        /// <param name="dt">菜单列表</param>
        /// <returns></returns>
        private bool addFunctionRole(string RoleID, DataTable dt)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();

            foreach (DataRow dr in dt.Rows)
            {
                //[button_revoke] = <button_revoke, varchar(5),>

                SysSQLString sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into tr_role_function");
                sb.Append(" ");
                sb.Append("(role_fun_id,fun_id,role_id,");
                sb.Append("button_browse,button_add,button_edit,button_copy,button_delete,button_cancel,");
                sb.Append("button_activate,button_status,button_confirm,button_save,button_submit,button_verify,button_import,");
                sb.Append("button_export,button_print,button_sync,button_revoke,button_operation_record,button_dispatching,button_balance,button_commit)");
                sb.Append(" ");
                sb.Append("values");
                sb.Append(" ");
                sb.Append("('");
                sb.Append(Guid.NewGuid().ToString());
                sb.Append("','");
                sb.Append(dr["fun_id"].ToString());
                sb.Append("','");
                sb.Append(RoleID);
                sb.Append("',");

                #region --填充
                if (dr["button_browse"] != DBNull.Value)
                {
                    if ((bool)dr["button_browse"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_add"] != DBNull.Value)
                {
                    if ((bool)dr["button_add"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_edit"] != DBNull.Value)
                {
                    if ((bool)dr["button_edit"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_copy"] != DBNull.Value)
                {
                    if ((bool)dr["button_copy"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_delete"] != DBNull.Value)
                {
                    if ((bool)dr["button_delete"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_cancel"] != DBNull.Value)
                {
                    if ((bool)dr["button_cancel"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_activate"] != DBNull.Value)
                {
                    if ((bool)dr["button_activate"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_status"] != DBNull.Value)
                {
                    if ((bool)dr["button_status"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_confirm"] != DBNull.Value)
                {
                    if ((bool)dr["button_confirm"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_save"] != DBNull.Value)
                {
                    if ((bool)dr["button_save"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_submit"] != DBNull.Value)
                {
                    if ((bool)dr["button_submit"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_verify"] != DBNull.Value)
                {
                    if ((bool)dr["button_verify"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_import"] != DBNull.Value)
                {
                    if ((bool)dr["button_import"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_export"] != DBNull.Value)
                {
                    if ((bool)dr["button_export"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_print"] != DBNull.Value)
                {
                    if ((bool)dr["button_print"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_sync"] != DBNull.Value)
                {
                    if ((bool)dr["button_sync"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_revoke"] != DBNull.Value)
                {
                    if ((bool)dr["button_revoke"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_operation_record"] != DBNull.Value)
                {
                    if ((bool)dr["button_operation_record"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_dispatching"] != DBNull.Value)
                {
                    if ((bool)dr["button_dispatching"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_balance"] != DBNull.Value)
                {
                    if ((bool)dr["button_balance"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                if (dr["button_commit"] != DBNull.Value)
                {
                    if ((bool)dr["button_commit"])
                    {
                        sb.Append("'1'");
                    }
                    else
                    {
                        sb.Append("'0'");
                    }
                }
                else
                {
                    sb.Append("null");
                }

                #endregion

                sb.Append(")");
                sqlString.sqlString = sb.ToString();
                sqlString.Param = new Dictionary<string, string>();
                listSql.Add(sqlString);

            }
            return DBHelper.BatchExeSQLStringMultiByTrans("新增角色菜单权限", listSql);
        }
        #endregion

        #region 删除用户角色关系
        /// <summary>
        /// 删除UserRole
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns>bool</returns>
        private bool DeleteUserRole(string roleid)
        {
            string keyName = "role_id";
            string keyValue = roleid;
            string opName = "删除用户角色关系";
            return DBHelper.DeleteDataByID(opName, "tr_user_role", keyName, keyValue);
        }
        #endregion

        #region UserRole先删除在添加 用户角色关系
        /// <summary>
        /// UserRole先删除在添加
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
        #endregion

        #region 添加角色用户关系
        /// <summary>
        /// 添加角色用户关系
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="userIdList">用户Id</param>
        private bool AddUserRole(string roleId, ArrayList userIdList)
        {
            List<SysSQLString> listSql = new List<SysSQLString>();

            foreach (string userId in userIdList)
            {
                SysSQLString sqlString = new SysSQLString();
                sqlString.cmdType = CommandType.Text;
                sqlString.sqlString = string.Format("insert into tr_user_role (user_role_id,user_id,role_id) values ('{0}','{1}','{2}')",
                    Guid.NewGuid().ToString(), userId, roleId);
                sqlString.Param = new Dictionary<string, string>();
                listSql.Add(sqlString);
            }
            return DBHelper.BatchExeSQLStringMultiByTrans("新增用户角色权限", listSql);
        }
        #endregion

        #region 绑定功能菜单树tree
        private void BindTree()
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

        #region 添加操作员
        /// <summary>
        /// 添加操作员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            string user_id = "";
            string user_code = "";
            string user_name = "";
            HXCPcClient.Chooser.frmUsers fm = new Chooser.frmUsers();
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
                if (wStatus == WindowStatus.Edit)
                {
                    strCount = DBHelper.GetSingleValue("", "tr_user_role", "count(*)", "user_id='" + user_id + "' and role_id!='" + id + "'", "");
                    // strsql = "select user_id  from tr_user_role where user_id='" + user_id + "' and role_id!='" + id + "'";
                }
                else
                {
                    strCount = DBHelper.GetSingleValue("", "tr_user_role", "count(*)", "user_id='" + user_id + "'", "");
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
                userIdList.Add(user_id);
            }
        }
        #endregion

        #region 删除操作员
        /// <summary>
        /// 删除操作员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDel_Click(object sender, EventArgs e)
        {
            string id = UserId;

            if (dgvUser.CurrentRow != null)
            {
                dgvUser.Rows.Remove(dgvUser.CurrentRow);
                userIdList.Remove(id);
            }
            else
            {
                MessageBoxEx.Show("当前行没有数据!");
            }


        }
        #endregion

        #region 树节点点击事件
        private void tvFunction_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DataSet dsFunc = new DataSet();
            DataSet dsRoleFunc = new DataSet();
            dsFunc.Clear();
            StringBuilder strBD = new StringBuilder();
            strBD.Append("with fun as(  select * from sys_function  where fun_id = '" + e.Node.Name + "' union all  select sf.* from fun f inner join sys_function sf  on f.fun_id = sf.parent_id ) ");
            strBD.Append(" select fun_id,num,fun_name,fun_ename,fun_uri ,fun_img,parent_id,fun_cbs ,fun_level ,fun_idx ,fun_flag ,fun_run ");
            strBD.Append(" ,CONVERT(bit,button_browse) button_browse,CONVERT(bit,button_add) button_add ,CONVERT(bit,button_edit)button_edit ");
            strBD.Append(" ,CONVERT(bit,button_copy )button_copy ,CONVERT(bit,button_delete)button_delete,CONVERT(bit,button_cancel) button_cancel ");
            strBD.Append(" ,CONVERT(bit,button_activate) button_activate,CONVERT(bit,button_status) button_status,CONVERT(bit,button_confirm) button_confirm ");
            strBD.Append("  ,CONVERT(bit,button_save) button_save,CONVERT(bit,button_submit) button_submit ,CONVERT(bit,button_verify) button_verify");
            strBD.Append(" ,CONVERT(bit,button_revoke) button_revoke,CONVERT(bit,button_sync) button_sync,CONVERT(bit,button_import) button_import");
            strBD.Append("  ,CONVERT(bit,button_export) button_export ,CONVERT(bit,button_print) button_print ,CONVERT(bit,button_operation_record)button_operation_record ");
            strBD.Append(" ,CONVERT(bit,button_dispatching) button_dispatching ,CONVERT(bit,button_balance) button_balance,CONVERT(bit,button_commit) button_commit ");
            strBD.Append(" ,enable_flag ,remark ,create_by,create_time,update_by ,update_time,CONVERT(bit, '0') as isall  from fun where fun_run='1'  and fun_flag='1' and enable_flag='1' ");

            SYSModel.SQLObj sqlobj = new SYSModel.SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, SYSModel.ParamObj>();
            sqlobj.sqlString = strBD.ToString();
            dsFunc = DBHelper.GetDataSet("查询角色权限", sqlobj);

            if (dsFunc != null && dsFunc.Tables[0] != null)
            {
                HandleTable(dsFunc.Tables[0]);
            }
            #region 编辑时的填充方法
            if (wStatus == WindowStatus.Edit)
            {
                dsRoleFunc.Clear();
                string strSql = " with fun as(  select * from sys_function   where fun_id = '" + e.Node.Name + "' union all  "
                + "select sf.* from fun f inner join sys_function sf  on f.fun_id = sf.parent_id ) "
                + "select fun.fun_id,fun.num,fun.fun_name,fun.fun_ename,fun.fun_uri ,fun.fun_img,fun.parent_id,fun.fun_cbs ,fun.fun_level ,fun.fun_idx ,fun.fun_flag ,fun.fun_run "
                + " ,CONVERT(bit,rf.button_browse) button_browse,CONVERT(bit,rf.button_add) button_add ,CONVERT(bit,rf.button_edit)button_edit "
                + " ,CONVERT(bit,rf.button_copy )button_copy ,CONVERT(bit,rf.button_delete)button_delete,CONVERT(bit,rf.button_cancel) button_cancel "
                + " ,CONVERT(bit,rf.button_activate) button_activate,CONVERT(bit,rf.button_status) button_status,CONVERT(bit,rf.button_confirm) button_confirm "
                + "  ,CONVERT(bit,rf.button_save) button_save,CONVERT(bit,rf.button_submit) button_submit ,CONVERT(bit,rf.button_verify) button_verify"
                + " ,CONVERT(bit,rf.button_revoke) button_revoke,CONVERT(bit,rf.button_sync) button_sync,CONVERT(bit,rf.button_import) button_import"
                + "  ,CONVERT(bit,rf.button_export) button_export ,CONVERT(bit,rf.button_print) button_print ,CONVERT(bit,rf.button_operation_record)button_operation_record "
                + " ,CONVERT(bit,rf.button_dispatching) button_dispatching ,CONVERT(bit,rf.button_balance) button_balance,CONVERT(bit,rf.button_commit) button_commit "
                + " ,fun.enable_flag ,fun.remark ,fun.create_by,fun.create_time,fun.update_by ,fun.update_time,CONVERT(bit, '0') as isall "
                + " from  fun ,tr_role_function rf,sys_role sr where rf.role_id =sr.role_id and rf.fun_id=fun.fun_id  and sr.role_id='" + id + "' "
                + " and fun.fun_run='1'  and fun.fun_flag='1' and fun.enable_flag='1' ";

                SYSModel.SQLObj sobj = new SYSModel.SQLObj();
                sobj.cmdType = CommandType.Text;
                sobj.Param = new Dictionary<string, SYSModel.ParamObj>();
                sobj.sqlString = strSql;
                dsRoleFunc = DBHelper.GetDataSet("查询角色权限", sobj);//角色的权限
                if (dsRoleFunc != null && dsRoleFunc.Tables[0].Rows.Count > 0 && dsFunc != null && dsFunc.Tables[0].Rows.Count > 0)
                {
                    #region 过期方法
                    //DataTable dtAllFun = dsFunc.Tables[0].Clone();
                    //ArrayList iList = new ArrayList();
                    //for (int i = 0; i < dsFunc.Tables[0].Rows.Count; i++)
                    //{
                    //    foreach (DataRow dtFunRow in dsRoleFunc.Tables[0].Rows)
                    //    {

                    //        if (dsFunc.Tables[0].Rows[i]["fun_id"].ToString() == dtFunRow["fun_id"].ToString())
                    //        {
                    //            //row.Delete();
                    //            if (iList.BinarySearch(i, funCompar) == 1)
                    //                iList.Add(i);
                    //            dtAllFun.Rows.Add(ConvertDtRow(dtAllFun, dtFunRow));
                    //        }
                    //    }
                    //}
                    //if (iList.Count > 0)
                    //{
                    //    iList.Sort();
                    //    for (int j = iList.Count - 1; j >= 0; j--)
                    //    {
                    //        if (dsFunc.Tables[0].Rows.Count > Convert.ToInt32(iList[j]))
                    //            dsFunc.Tables[0].Rows.RemoveAt(Convert.ToInt32(iList[j]));

                    //    }
                    //}
                    //if (dtAllFun.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dtAllFun.Rows)
                    //    {
                    //        dsFunc.Tables[0].Rows.Add(ConvertDtRow(dsFunc.Tables[0], dr));
                    //    }
                    //}

                    #endregion
                    //遍历所有功能,用角色拥有的功能权限替换默认功能权限
                    for (int rnum = 0; rnum < dsRoleFunc.Tables[0].Rows.Count; rnum++)
                    {
                        DataRow[] dr = dsFunc.Tables[0].Select("fun_id='" + dsRoleFunc.Tables[0].Rows[rnum]["fun_id"].ToString() + "'");
                        if (dr != null && dr.Length > 0)
                        {
                            dsFunc.Tables[0].Rows.Remove(dr[0]);
                            dsFunc.Tables[0].ImportRow(dsRoleFunc.Tables[0].Rows[rnum]);
                        }
                    }
                }
            }
            #endregion


            if (dsFunc != null && dsFunc.Tables[0].Rows.Count > 0)
            {
                #region 过期方法
                //DataTable tem = dsFunc.Tables[0].Clone();
                //if (dtEditedFun.Rows.Count > 0)
                //{

                //    // ds.Tables[0].AcceptChanges(); 
                //    ArrayList iList = new ArrayList();
                //    for (int i = 0; i < dsFunc.Tables[0].Rows.Count; i++)
                //    {
                //        foreach (DataRow dtFunRow in dtEditedFun.Rows)
                //        {
                //            if (dsFunc.Tables[0].Rows[i]["fun_id"].ToString() == dtFunRow["fun_id"].ToString())
                //            {
                //                //row.Delete();
                //                iList.Add(i);
                //                tem.Rows.Add(ConvertDtRow(tem, dtFunRow));
                //            }
                //        }
                //    }
                //    if (iList.Count > 0)
                //    {
                //        for (int j = iList.Count - 1; j >= 0; j--)
                //        {
                //            if (dsFunc.Tables[0].Rows.Count > Convert.ToInt32(iList[j]))
                //                dsFunc.Tables[0].Rows.RemoveAt(Convert.ToInt32(iList[j]));
                //        }
                //    }
                //    if (tem.Rows.Count > 0)
                //    {
                //        foreach (DataRow dr in tem.Rows)
                //        {
                //            dsFunc.Tables[0].Rows.Add(ConvertDtRow(dsFunc.Tables[0], dr));
                //        }
                //    }
                //}
                #endregion
                if (dtEditedFun.Rows.Count > 0)
                {
                    //遍历编辑后的功能权限,用编辑后的功能权限替换默认功能权限
                    for (int rnum = 0; rnum < dtEditedFun.Rows.Count; rnum++)
                    {
                        DataRow[] dr = dsFunc.Tables[0].Select("fun_id='" + dtEditedFun.Rows[rnum]["fun_id"].ToString() + "'");
                        if (dr != null && dr.Length > 0)
                        {
                            dsFunc.Tables[0].Rows.Remove(dr[0]);
                            dsFunc.Tables[0].ImportRow(dtEditedFun.Rows[rnum]);
                        }
                    }
                }
            }
            if (dsFunc != null && dsFunc.Tables.Count > 0 && dsFunc.Tables[0] != null)
            {
                dgvFunction.DataSource = dsFunc.Tables[0].DefaultView;
            }
        }

        #endregion

        #region 菜单dgv事件
        private void dgvFunction_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
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
        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFunction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0 && dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value)
            {
                DataGridViewCell cell = dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex];

                bool ifcheck1 = Convert.ToBoolean(cell.FormattedValue);
                bool cellCheckBox = Convert.ToBoolean(cell.EditedFormattedValue);
                #region 全选处理
                if (e.ColumnIndex == isall.Index)//当单击全选，同时处于组合编辑状态时
                {
                    for (int i = button_browse.Index; i <= button_operation_record.Index; i++)
                    {
                        if (dgvFunction.Rows[e.RowIndex].Cells[i].Value != DBNull.Value)
                            dgvFunction.Rows[e.RowIndex].Cells[i].Value = cellCheckBox;
                    }
                }
                else
                {
                    if (!cellCheckBox)
                    {
                        dgvFunction.Rows[e.RowIndex].Cells[isall.Index].Value = false;
                    }
                }
                #endregion

                bool isChecked = false; //行复选框是否有选中项  有一个选中 即行为有效数据
                dgvFunction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellCheckBox;//点击改变当前复选框状态
                if (cellCheckBox)
                {
                    isChecked = true;
                }
                else
                {
                    //for (int i = 3; i < dgvFunction.Columns.Count - 1; i++)
                    for (int i = button_browse.Index; i <= button_operation_record.Index; i++)//遍历所有按钮查看是否有选中项
                    {
                        if (dgvFunction.Rows[e.RowIndex].Cells[i].Value != DBNull.Value
                            && Convert.ToBoolean(dgvFunction.Rows[e.RowIndex].Cells[i].Value)
                            && i != e.ColumnIndex)
                        {
                            isChecked = true;
                            break;
                        }
                    }
                }

                //修改搜索比对方法
                //if (funIdList.BinarySearch(dgvFunction.Rows[e.RowIndex].Cells["fun_id"].Value.ToString()) == -1)
                if (funIdList.BinarySearch(dgvFunction.Rows[e.RowIndex].Cells["fun_id"].Value.ToString(), funCompar) == 1)
                    funIdList.Add(dgvFunction.Rows[e.RowIndex].Cells["fun_id"].Value.ToString());//id添加到funIdList
                DataRow dRow = (dgvFunction.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                if (isChecked)
                {
                    #region 如果有选中的复选框 就删除原有的 在从新添加
                    //foreach (DataRow row in dtFun.Rows)
                    //{
                    //    if (row["fun_id"] == dRow["fun_id"])
                    //    {
                    //        row.Delete();
                    //        break;
                    //    }
                    //}
                    for (int denum = 0; denum < dtEditedFun.Rows.Count; denum++)
                    {
                        if (dtEditedFun.Rows[denum]["fun_id"] == dRow["fun_id"])
                        {
                            dtEditedFun.Rows.RemoveAt(denum);
                            break;
                        }
                    }
                    dtEditedFun.Rows.Add(ConvertDtRow(dtEditedFun, dRow));
                    //从要删除的ID中删除
                    deleteFun.Remove(dRow["fun_id"].ToString());
                    #endregion
                }
                else
                {
                    #region 如果没有选中的复选框 就遍历dtFun 发现就删掉
                    //foreach (DataRow row in dtFun.Rows)
                    //{
                    //    if (row["fun_id"] == dRow["fun_id"])
                    //    {
                    //        row.Delete();
                    //        break;
                    //    }
                    //}
                    if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
                    {
                        for (int denum = 0; denum < dtEditedFun.Rows.Count; denum++)
                        {
                            if (dtEditedFun.Rows[denum]["fun_id"] == dRow["fun_id"])
                            {
                                dtEditedFun.Rows.RemoveAt(denum);
                                break;
                            }
                        }
                    }
                    //添加需要删除的功能
                    if (deleteFun.Where(a => a == dRow["fun_id"].ToString()).Count() <= 0)
                        deleteFun.Add(dRow["fun_id"].ToString());
                    #endregion
                }
            }

        }
        #endregion

        #region dtFun行数据转换
        /// <summary>
        /// dtFun行数据
        /// </summary>
        /// <param name="dRow"></param>
        /// <returns></returns>
        private DataRow ConvertDtRow(DataTable dt, DataRow dRow)
        {
            DataRow dr = dt.NewRow();
            dr["fun_id"] = dRow["fun_id"].ToString();
            dr["num"] = dRow["num"];
            dr["fun_name"] = dRow["fun_name"].ToString();
            dr["fun_ename"] = "";
            dr["fun_uri"] = "";
            dr["fun_img"] = "";
            dr["fun_cbs"] = "";
            dr["fun_level"] = 0;
            dr["fun_idx"] = "";
            dr["fun_flag"] = 0;
            dr["fun_run"] = 0;
            dr["enable_flag"] = "";
            dr["remark"] = "";
            dr["create_by"] = "";
            dr["create_time"] = 0;
            dr["update_by"] = "";
            dr["update_time"] = 0;
            dr["parent_id"] = dRow["parent_id"].ToString();
            dr["isall"] = dRow["isall"];
            dr["button_browse"] = dRow["button_browse"];
            dr["button_add"] = dRow["button_add"];
            dr["button_edit"] = dRow["button_edit"];
            dr["button_copy"] = dRow["button_copy"];
            dr["button_delete"] = dRow["button_delete"];
            dr["button_cancel"] = dRow["button_cancel"];
            dr["button_activate"] = dRow["button_activate"];
            dr["button_status"] = dRow["button_status"];
            dr["button_confirm"] = dRow["button_confirm"];
            dr["button_sync"] = dRow["button_sync"];
            dr["button_revoke"] = dRow["button_revoke"];
            dr["button_save"] = dRow["button_save"];
            dr["button_submit"] = dRow["button_submit"];
            dr["button_verify"] = dRow["button_verify"];
            dr["button_import"] = dRow["button_import"];
            dr["button_export"] = dRow["button_export"];
            dr["button_print"] = dRow["button_print"];
            dr["button_operation_record"] = dRow["button_operation_record"];
            dr["button_dispatching"] = dRow["button_dispatching"];
            dr["button_balance"] = dRow["button_balance"];
            dr["button_commit"] = dRow["button_commit"];
            return dr;
        }
        #endregion

        #region --启用停用

        private bool StatusSql()
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = "update sys_role set state=@state where role_id = '" + id + "'";
            string ids = string.Empty;
            if (dr["state"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
            {
                sql.Param.Add("state", ((int)DataSources.EnumStatus.Stop).ToString());

            }
            else
            {
                sql.Param.Add("state", ((int)DataSources.EnumStatus.Start).ToString());
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "角色", listSql);

        }

        void UCRoleAddOrEdit_StatusEvent(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (dr["state"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
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
                MessageBoxEx.ShowInformation(btnStatus.Caption + "成功！");
                if (this.RefreshDataStart != null)
                {
                    this.RefreshDataStart();
                }
                deleteMenuByTag(this.Tag.ToString(), this.parentName);
            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (dr["state"].ToString() == ((int)DataSources.EnumStatus.Start).ToString())
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        #endregion

        #region 删除角色

        void UCRoleAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            DeleteRole();
            if (this.RefreshDataStart != null)
            {
                this.RefreshDataStart();
            }
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }

        void DeleteRole()
        {
            if (MessageBoxEx.ShowQuestion("是否确认删除?"))
            {
                List<string> listId = new List<string>();
                listId.Add(id);
                Dictionary<string, string> fileds = new Dictionary<string, string>();
                fileds.Add("enable_flag", DataSources.EnumEnableFlag.DELETED.ToString("d"));
                fileds.Add("update_by", GlobalStaticObj.UserID);
                fileds.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());

                bool flag = DBHelper.BatchUpdateDataByIn("删除角色", "sys_role", fileds, "role_id", listId.ToArray());

                if (flag)
                {

                    MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeleteUserRole(id);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

    }

    /// <summary>
    /// 方法ID比较器
    /// </summary>
    public class FunComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null || x.ToString() == string.Empty)
            {
                return 1;
            }

            if (y == null || y.ToString() == string.Empty)
            {
                return 1;
            }

            if (x.ToString() == y.ToString())
                return 0;
            else
                return 1;

        }
    }
}