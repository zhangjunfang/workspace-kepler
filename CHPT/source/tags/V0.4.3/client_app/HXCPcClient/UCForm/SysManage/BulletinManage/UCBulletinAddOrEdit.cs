using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HXCPcClient.CommonClass;
using SYSModel;
using HXCPcClient.Chooser;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Text;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    /// <summary>
    /// 公告管理-新增与修改
    /// Author：JC
    /// AddTime：2014.11.13
    /// 修改人：杨天帅
    /// </summary>
    [ComVisible(true)]
    public partial class UCBulletinAddOrEdit : UCBase
    {
        #region --回调更新事件
        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --成员变量
        /// <summary>
        /// 公告信息ID值
        /// </summary>
        public string id = string.Empty;

        private string parentName = string.Empty;
        private DataRow dr;
        private WindowStatus winStatus;
        /// <summary>
        /// 公告内容
        /// </summary>
        string strContent = string.Empty;
        /// <summary>
        /// 接收人员Id
        /// </summary>
        string strPId = string.Empty;
        /// <summary>
        /// 组织机构Id
        /// </summary>
        string strOId = string.Empty;
        /// <summary>
        /// 审核窗体
        /// </summary>
        UCVerify verify;
        #endregion

        #region --构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="winStatus">窗体状态</param>
        /// <param name="dr">数据行</param>
        /// <param name="parentName">父窗体名称</param>
        public UCBulletinAddOrEdit(WindowStatus winStatus, DataRow dr, string parentName)
        {
            InitializeComponent();

            this.winStatus = winStatus;
            this.dr = dr;
            if (this.dr != null)
            {
                this.id = dr["announcement_id"].ToString();
            }

            this.webContent.Url = new System.Uri(Application.StartupPath + "\\kindeditor\\e.html", System.UriKind.Absolute);
            this.parentName = parentName;

            this.webContent.ObjectForScripting = this;
        }
        #endregion

        #region --窗体Load事件
        private void UCBulletinAddOrEdit_Load(object sender, EventArgs e)
        {
            base.SetBtnStatus(this.winStatus);

            this.InitEvent();

            this.InitData();

            ucAttr.TableName = "sys_announcement";
            ucAttr.TableNameKeyValue = id;

            if (this.winStatus == WindowStatus.Edit || this.winStatus == WindowStatus.Copy)
            {
                this.BindData();
            }
        }
        #endregion

        #region --初始化事件和数据
        private void InitEvent()
        {
            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            base.SaveEvent += new ClickHandler(UCBulletinAddOrEdit_SaveEvent);
            base.DeleteEvent += new ClickHandler(UCBulletinAddOrEdit_DeleteEvent);
            base.CancelEvent += new ClickHandler(UCBulletinAddOrEdit_CancelEvent);
            base.VerifyEvent += new ClickHandler(UCBulletinAddOrEdit_VerifyEvent);
        }
        private void InitData()
        {
            this.dtpSTime.Value = DateTime.Now;
            CommonCtrl.CmbBindUser(cmbUser, "");
            CommonCtrl.CmbBindDeptment(this.cmbOrg, string.Empty);
        }
        #endregion

        #region 审核事件
        void UCBulletinAddOrEdit_VerifyEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要审核吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            verify = new UCVerify();
            if (verify.ShowDialog() == DialogResult.OK)
            {
                List<SQLObj> listSql = new List<SQLObj>();

                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;

                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();

                dicParam.Add("announcement_id", new ParamObj("announcement_id", id, SysDbType.VarChar, 40));//单据ID
                dicParam.Add("status", new ParamObj("status", verify.auditStatus, SysDbType.VarChar, 40));//单据状态
                dicParam.Add("Verify_advice", new ParamObj("Verify_advice", verify.Content, SysDbType.VarChar, 200));//审核意见
                obj.sqlString = "update sys_announcement set status=@status,Verify_advice=@Verify_advice where announcement_id=@announcement_id";
                obj.Param = dicParam;
                listSql.Add(obj);
                if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为提交", listSql))
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                    MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    deleteMenuByTag(this.Tag.ToString(), "UCBulletinAddOrEdit");
                }
            }
        }
        #endregion

        #region 取消事件
        void UCBulletinAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), this.parentName);
        }
        #endregion

        #region 删除事件
        void UCBulletinAddOrEdit_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();

            dicFileds.Add("enable_flag", DataSources.EnumStatus.Stop.ToString("d"));
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            dicFileds.Add("update_time", Common.LocalDateTimeToUtcLong(DateTime.Now).ToString());


            bool flag = DBHelper.BatchUpdateDataByIn("删除公告", "sys_announcement", dicFileds,
                "announcement_id", new string[] { this.id });

            if (flag)
            {
                this.dr.Table.Rows.Remove(this.dr);
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                deleteMenuByTag(this.Tag.ToString(), this.parentName);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 保存事件
        void UCBulletinAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Caption.Trim()))
                {
                    MessageBoxEx.Show("请填写公告标题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(rtbOrganization.Text.Trim()))
                {
                    MessageBoxEx.Show("请选择接收组织！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(rtbPerson.Text.Trim()))
                {
                    MessageBoxEx.Show("请选择接收人员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(strContent))
                {
                    MessageBoxEx.Show("请填写公告内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!ucAttr.CheckAttachment())
                {
                    return;
                }
                List<SQLObj> listSql = new List<SQLObj>();
                this.SaveAnnouncementInfo(listSql);
                this.SaveOrg(listSql, id);
                this.SavePerson(listSql, id);
                ucAttr.TableNameKeyValue = id;
                listSql.AddRange(ucAttr.AttachmentSql);
                if (DBHelper.BatchExeSQLMultiByTrans("", listSql))
                {
                    if (this.RefreshDataStart != null)
                    {
                        this.RefreshDataStart();
                    }
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    deleteMenuByTag(this.Tag.ToString(), this.parentName);
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

        #region 公告基本信息保存
        /// <summary>
        /// 公告基本信息保存
        /// </summary>
        /// <param name="listSql">SQLObj listSql</param>
        /// <param name="status">保存状态</param>
        private void SaveAnnouncementInfo(List<SQLObj> listSql)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("announcement_title", new ParamObj("announcement_title", txtTitle.Caption.Trim(), SysDbType.VarChar, 40));//公告标题 
            dicParam.Add("date_up", new ParamObj("date_up", Common.LocalDateTimeToUtcLong(dtpSTime.Value).ToString(), SysDbType.BigInt));//日期
            dicParam.Add("content", new ParamObj("content", strContent, SysDbType.VarChar, 4000));//公告内容
            //发布人
            dicParam.Add("user_id", new ParamObj("user_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cmbUser.SelectedValue)) ? cmbUser.SelectedValue.ToString() : null, SysDbType.VarChar, 40));
            //发布部门
            dicParam.Add("org_id", new ParamObj("org_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cmbOrg.SelectedValue)) ? cmbOrg.SelectedValue : null, SysDbType.VarChar, 40));
            dicParam.Add("data_source", new ParamObj("data_source", "2", SysDbType.VarChar, 5));//数据来源 
            dicParam.Add("enable_flag", new ParamObj("enable_flag", "1", SysDbType.VarChar, 1));//信息状态（1未删除0|删除）
            dicParam.Add("announcement_type", new ParamObj("announcement_type", "748985a6-c3ca-4e92-b7ea-98ed423b1a08", SysDbType.VarChar, 40));//公告类型，公司公告
            dicParam.Add("status", new ParamObj("status", Convert.ToInt32(DataSources.EnumAuditStatus.DRAFT).ToString(), SysDbType.VarChar, 40));
            if (this.winStatus == WindowStatus.Add)
            {
                id = Guid.NewGuid().ToString();
                dicParam.Add("announcement_id", new ParamObj("announcement_id", id, SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）               
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into sys_announcement (announcement_title,date_up,content,user_id,org_id,data_source,enable_flag,announcement_type,announcement_id,create_by,create_time,status)
                 values (@announcement_title,@date_up,@content,@user_id,@org_id,@data_source,@enable_flag,@announcement_type,@announcement_id,@create_by,@create_time,@status);";
            }
            else if (this.winStatus == WindowStatus.Edit)
            {
                dicParam.Add("announcement_id", new ParamObj("announcement_id", id, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update sys_announcement set announcement_title=@announcement_title,date_up=@date_up,content=@content,user_id=@user_id,org_id=@org_id,data_source=@data_source,enable_flag=@enable_flag,announcement_type=@announcement_type,status=@status,update_by=@update_by,update_time=@update_time
                 where announcement_id=@announcement_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);
        }
        #endregion

        #region 接收组织信息保存
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listSql">SQLObj listSql</param>
        /// <param name="strId">公告Id</param>
        private void SaveOrg(List<SQLObj> listSql, string strId)
        {
            if (this.winStatus == WindowStatus.Edit)
            {
                List<SQLObj> listSqlO = new List<SQLObj>();
                SQLObj objO = new SQLObj();
                objO.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParamO = new Dictionary<string, ParamObj>();
                dicParamO.Add("announcement_id", new ParamObj("announcement_id", strId, SysDbType.VarChar, 40));//Id
                objO.sqlString = @"delete from sys_announcement_org where announcement_id=@announcement_id ;";
                objO.Param = dicParamO;
                listSqlO.Add(objO);
                DBHelper.BatchExeSQLMultiByTrans("", listSqlO);
            }
            string strOrgName = rtbOrganization.Text.Trim();
            string strOrgId = rtbOrganization.Tag.ToString();
            string[] OrgIdArrary = strOrgId.Split(',');
            string[] OrgNameArrary = strOrgName.Split(',');
            for (int i = 0; i < OrgIdArrary.Length; i++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("announcement_id", new ParamObj("announcement_id", strId, SysDbType.VarChar, 40));//公告Id 
                dicParam.Add("org_id", new ParamObj("org_id", OrgIdArrary[i], SysDbType.VarChar, 40));//组织结构ID
                dicParam.Add("org_name", new ParamObj("org_name", OrgNameArrary[i], SysDbType.VarChar, 40));//组织结构名
                dicParam.Add("announcement_org_id", new ParamObj("announcement_org_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                obj.sqlString = @"insert into sys_announcement_org (announcement_id,org_id,org_name,announcement_org_id)
                 values (@announcement_id,@org_id,@org_name,@announcement_org_id);";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
        }
        #endregion

        #region 接收人员信息保存
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listSql">SQLObj listSql</param>
        /// <param name="strId">公告Id</param>
        private void SavePerson(List<SQLObj> listSql, string strId)
        {
            if (this.winStatus == WindowStatus.Edit)
            {
                List<SQLObj> listSqlU = new List<SQLObj>();
                SQLObj objU = new SQLObj();
                objU.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("announcement_id", new ParamObj("announcement_id", strId, SysDbType.VarChar, 40));//Id
                objU.sqlString = @"delete from sys_announcement_user where announcement_id=@announcement_id ;";
                objU.Param = dicParam;
                listSqlU.Add(objU);
                DBHelper.BatchExeSQLMultiByTrans("", listSqlU);
            }
            string strPerName = rtbPerson.Text.Trim();
            if (strPerName == "[全部]")
            {
                string strOrgId = rtbOrganization.Tag.ToString();
                string[] OrgIdArrary = strOrgId.Split(',');
                strOrgId = string.Empty;
                for (int i = 0; i < OrgIdArrary.Length; i++)
                {
                    if (i > 0)
                    {
                        strOrgId += " or ";
                    }
                    strOrgId += "org_id='" + OrgIdArrary[i] + "'";
                }
                DataRow[] drs = LocalCache.DtUser.Select(strOrgId);
                foreach (DataRow dr in drs)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                    dicParam.Add("announcement_id", new ParamObj("announcement_id", strId, SysDbType.VarChar, 40));//公告Id 
                    dicParam.Add("user_id", new ParamObj("user_id", dr["user_id"], SysDbType.VarChar, 40));//用户ID
                    dicParam.Add("user_name", new ParamObj("user_name", dr["user_name"], SysDbType.VarChar, 40));//用户名
                    dicParam.Add("announcement_user_id", new ParamObj("announcement_user_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                    obj.sqlString = @"insert into sys_announcement_user (announcement_id,user_id,user_name,announcement_user_id)
                                    values (@announcement_id,@user_id,@user_name,@announcement_user_id);";
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
                return;
            }
            string strPerId = rtbPerson.Tag.ToString();
            string[] PerIdArrary = strPerId.Split(',');
            string[] PerNameArrary = strPerName.Split(',');
            for (int i = 0; i < PerIdArrary.Length; i++)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("announcement_id", new ParamObj("announcement_id", strId, SysDbType.VarChar, 40));//公告Id 
                dicParam.Add("user_id", new ParamObj("user_id", PerIdArrary[i], SysDbType.VarChar, 40));//用户ID
                dicParam.Add("user_name", new ParamObj("user_name", PerNameArrary[i], SysDbType.VarChar, 40));//用户名
                dicParam.Add("announcement_user_id", new ParamObj("announcement_user_id", Guid.NewGuid().ToString(), SysDbType.VarChar, 40));//Id
                obj.sqlString = @"insert into sys_announcement_user (announcement_id,user_id,user_name,announcement_user_id)
                 values (@announcement_id,@user_id,@user_name,@announcement_user_id);";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
        }
        #endregion

        #region 根据公告ID获取信息编辑用
        /// <summary>
        /// 根据公告ID获取信息编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            //string strWhere = string.Format("announcement_id='{0}'", id);
            //DataTable dt = DBHelper.GetTable("查询公告", "sys_announcement", "*", strWhere, "", "");
            if (this.dr == null)
            {
                return;
            }
            this.txtTitle.Caption = CommonCtrl.IsNullToString(dr["announcement_title"]);//公告标题          
            this.dtpSTime.Value = DateTime.Parse(Common.UtcLongToLocalDateTime(dr["date_up"]));//公告日期          
            this.strContent = CommonCtrl.IsNullToString(dr["content"]);//公告内容           
            this.cmbOrg.SelectedValue = CommonCtrl.IsNullToString(dr["org_id"]);//发布部门
            this.cmbUser.SelectedValue = CommonCtrl.IsNullToString(dr["user_id"]);//发布人           
            #endregion
            //接收组织信息、接收人员信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindData), id);

            #region 底部datagridview数据
            //附件信息数据
            ucAttr.BindAttachment();
            #endregion
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindData(object obj)
        {
            string id = obj.ToString();
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("select * from sys_announcement_org where announcement_id='{0}'", id));
            sb.Append(";");
            sb.Append(string.Format("select * from sys_announcement_user where announcement_id='{0}'", id));

            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = sb.ToString();

            DataSet ds = null;
            try
            {
                ds = DBHelper.GetDataSet("获取公告接收组织和用户信息", sql);
            }
            catch
            {
                ds = null;
            }

            this.Invoke(this.uiHandler, ds);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            if (obj == null || !(obj is DataSet))
            {
                return;
            }
            DataSet ds = obj as DataSet;

            #region 接收组织信息
            DataTable dot = ds.Tables[0];
            if (dot.Rows.Count > 0)
            {
                string strOrName = string.Empty;
                for (int i = 0; i < dot.Rows.Count; i++)
                {
                    DataRow dor = dot.Rows[i];
                    strOId += CommonCtrl.IsNullToString(dor["org_id"]) + ",";
                    strOrName += CommonCtrl.IsNullToString(dor["org_name"]) + ",";
                }
                strOId = strOId.Substring(0, strOId.Length - 1);
                strOrName = strOrName.Substring(0, strOrName.Length - 1);
                rtbOrganization.Tag = strOId;
                rtbOrganization.Text = strOrName;
            }
            #endregion

            #region 接收人员信息
            DataTable dut = ds.Tables[1];
            if (dut.Rows.Count > 0)
            {
                string strUName = string.Empty;
                for (int i = 0; i < dut.Rows.Count; i++)
                {
                    DataRow dur = dut.Rows[i];
                    strPId += CommonCtrl.IsNullToString(dur["user_id"]) + ",";
                    strUName += CommonCtrl.IsNullToString(dur["user_name"]) + ",";
                }
                strPId = strPId.Substring(0, strPId.Length - 1);
                strUName = strUName.Substring(0, strUName.Length - 1);
                rtbPerson.Tag = strPId;
                rtbPerson.Text = strUName;
            }
            #endregion
        }
        #endregion

        #region webbrows 设置

        #region 在调整webbrows大小时刷新
        private void webContent_Resize(object sender, EventArgs e)
        {
            this.webContent.Refresh();
        }
        #endregion
        public void SetDetailContent()
        {
            webContent.Document.InvokeScript("setContent", new object[] { strContent });
        }
        public string GetContent()
        {
            return strContent;
        }
        public void RequestContent(string str)
        {
            strContent = str;
        }

        #endregion

        #region 根据部门的选择绑定发布人人
        /// <summary>
        /// 根据部门的选择绑定发布人人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrg_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cmbOrg.SelectedValue == null)
            {
                return;
            }
            CommonCtrl.CmbBindUser(cmbUser, this.cmbOrg.SelectedValue.ToString());
        }
        #endregion

        #region 组织选择器
        private void btnOrganization_Click(object sender, EventArgs e)
        {
            frmOrganization Organization = new frmOrganization();
            Organization.IsCanSelectMore = true;
            DialogResult result = Organization.ShowDialog();
            if (result == DialogResult.OK)
            {

                rtbOrganization.Text = Organization.Org_Name.Contains(",") ? Organization.Org_Name.Substring(0, Organization.Org_Name.Length - 1) : Organization.Org_Name;
                rtbOrganization.Tag = Organization.Org_Id.Contains(",") ? Organization.Org_Id.Substring(0, Organization.Org_Id.Length - 1) : Organization.Org_Id;
            }
        }
        #endregion

        #region 人员选择器
        private void btnPerson_Click(object sender, EventArgs e)
        {
            frmPersonnelSelector frmSelector = new frmPersonnelSelector();
            frmSelector.isMoreCheck = true;
            DialogResult result = frmSelector.ShowDialog();
            if (result == DialogResult.OK)
            {
                rtbPerson.Text = frmSelector.strPersonName.Substring(0, frmSelector.strPersonName.Length - 1);
                rtbPerson.Tag = frmSelector.strUserId.Substring(0, frmSelector.strUserId.Length - 1);
            }
        }
        #endregion
    }
}
