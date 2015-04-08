using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;

namespace HXCPcClient.UCForm.SysManage.BulletinManage
{
    /// <summary>
    /// 公告管理-预览窗体
    /// Author：JC
    /// AddTime：2014.11.13
    /// </summary>
    public partial class UCBulletinView : UCBase
    {
        #region --成员变量
        private string parentName = string.Empty;
        private DataRow dr = null;
        private string id = string.Empty;             
        /// <summary>
        /// 审核窗体
        /// </summary>
        UCVerify verify;        
        #endregion

        #region --构造函数
        public UCBulletinView(DataRow dr, string parentName)
        {
            InitializeComponent();
            this.dr = dr;
            this.ucAttr.ContextMenuStrip = null;

            if (this.dr != null)
            {
                id = dr["announcement_id"].ToString();
            }
            this.parentName = parentName;

            this.SetBtnStatus();

            base.AddEvent -= new ClickHandler(UC_AddEvent);
            base.AddEvent += new ClickHandler(UC_AddEvent);

            base.EditEvent += new ClickHandler(UCBulletinView_EditEvent);
            base.DeleteEvent += new ClickHandler(UCBulletinView_DeleteEvent);
          
            base.VerifyEvent -= new ClickHandler(UC_VerifyEvent);
            base.VerifyEvent += new ClickHandler(UC_VerifyEvent);

            base.RevokeEvent -= new ClickHandler(UC_RevokeEvent);
            base.RevokeEvent += new ClickHandler(UC_RevokeEvent);

            this.InitData();
        }    
        #endregion  
    
        #region --设置按钮隐显
        private void SetBtnStatus()
        {          
            btnCopy.Visible = false;
            btnStatus.Visible = false;
            btnBalance.Visible = false;          
            btnImport.Visible = false;
            btnExport.Visible = false;
            btnPrint.Visible = false;
            btnSet.Visible = false;
            btnSubmit.Visible = false;
            btnConfirm.Visible = false;
            btnCommit.Visible = false;
            btnActivation.Visible = false;
            btnSync.Visible = false;
            this.btnSave.Visible = false;
            this.btnCancel.Visible = false;

            this.btnAdd.Visible = false;
            this.btnEdit.Visible = true;
            this.btnDelete.Visible = false;         
            this.btnVerify.Visible = true;
            this.btnRevoke.Visible = true;
            this.btnRevoke.Caption = "撤回";
            this.btnView.Visible = true;
            this.btnSet.Visible = true;
            this.btnPrint.Visible = true;

            if (this.dr != null)
            {
                if (this.dr["status"].ToString() == DataSources.EnumAuditStatus.DRAFT.ToString("d"))
                {
                    this.btnRevoke.Enabled = false;
                }
                else if (this.dr["status"].ToString() == DataSources.EnumAuditStatus.AUDIT.ToString("d"))
                {                    
                    this.btnVerify.Enabled = false;
                }
            }        
        }
        #endregion

        #region --初始化数据
        /// <summary>
        /// 初始化数据
        /// </summary>      
        private void InitData()
        {          
            #region 维修表信息

            //公告标题
            labelTitle.Text = CommonCtrl.IsNullToString(this.dr["announcement_title"]);
            this.panelTitle.Location = new Point(this.labelTitle.Width + 50, this.panelTitle.Location.Y);
            //公告分类
            labType.Text = LocalCache.GetDictNameById(this.dr["announcement_type"].ToString());
            //发布日期            
            DateTime time;
            if (DateTime.TryParse(Common.UtcLongToLocalDateTime(dr["date_up"]), out time))
            {
                labTime.Text = time.ToShortDateString();
            }
            else
            {
                labTime.Text = string.Empty;
            }
           
            labPerson.Text = LocalCache.GetDeptNameById(CommonCtrl.IsNullToString(dr["org_id"]))
                + "—" + LocalCache.GetUserById(CommonCtrl.IsNullToString(dr["user_id"]));

            string strContent = CommonCtrl.IsNullToString(dr["content"]);
            webrowsContent.DocumentText = strContent;
            webrowsContent.Document.Write(strContent);
            webrowsContent.Refresh();

            #endregion


            #region 附件信息数据

            //附件信息数据
            ucAttr.TableName = "sys_announcement";
            ucAttr.TableNameKeyValue = dr["announcement_id"].ToString();
            ucAttr.BindAttachment();

            #endregion

        }
        #endregion 

        #region --操作事件
        //新增事件
        void UC_AddEvent(object sender, EventArgs e)
        {
            UCBulletinAddOrEdit uc = new UCBulletinAddOrEdit(WindowStatus.Add, null, this.parentName);           
            base.addUserControl(uc, "公告管理-新增", "BulletinAdd", this.Tag.ToString(), this.parentName);
        }
        //审核
        void UC_VerifyEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要审核该公告吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("announcement_id", new ParamObj("announcement_id", this.dr["announcement_id"], SysDbType.VarChar, 40));//单据ID
            dicParam.Add("status", new ParamObj("status", DataSources.EnumAuditStatus.AUDIT.ToString("d"), SysDbType.VarChar, 40));//单据状态

            obj.sqlString = "update sys_announcement set status=@status where announcement_id=@announcement_id";
            obj.Param = dicParam;
            listSql.Add(obj);


            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为已审核", listSql))
            {
                MessageBoxEx.Show("审核成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dr["status"] = DataSources.EnumAuditStatus.AUDIT.ToString("d");
                this.btnRevoke.Enabled = false;
                this.btnVerify.Enabled = true;
            }
            else
            {
                MessageBoxEx.Show("审核失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //撤回
        void UC_RevokeEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要撤回该公告吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("announcement_id", new ParamObj("announcement_id", this.dr["announcement_id"], SysDbType.VarChar, 40));//单据ID
            dicParam.Add("status", new ParamObj("status", DataSources.EnumAuditStatus.DRAFT.ToString("d"), SysDbType.VarChar, 40));//单据状态

            obj.sqlString = "update sys_announcement set status=@status where announcement_id=@announcement_id";
            obj.Param = dicParam;
            listSql.Add(obj);


            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为草稿", listSql))
            {
                MessageBoxEx.Show("撤回成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dr["status"] = DataSources.EnumAuditStatus.DRAFT.ToString("d");
                this.btnRevoke.Enabled = false;
                this.btnVerify.Enabled = true;
            }
            else
            {
                MessageBoxEx.Show("撤回失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //删除事件
        void UCBulletinView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            Dictionary<string, string> comField = new Dictionary<string, string>();

            comField.Add("enable_flag", SYSModel.DataSources.EnumStatus.Stop.ToString("d"));
            bool flag = DBHelper.BatchUpdateDataByIn("删除公告", "sys_announcement", comField,
                    "announcement_id", new string[] { this.dr["announcement_id"].ToString() });
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                deleteMenuByTag(this.Tag.ToString(), this.parentName);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //编辑事件
        void UCBulletinView_EditEvent(object sender, EventArgs e)
        {
            UCBulletinAddOrEdit BulletinEdit = new UCBulletinAddOrEdit(WindowStatus.Edit, this.dr, this.Name);
            string id = this.dr["announcement_id"].ToString();
            base.addUserControl(BulletinEdit, "公告管理-编辑", "BulletinEdit" + id, this.Tag.ToString(), this.parentName);
            deleteMenuByTag(this.Tag.ToString(), "UCBulletinView");
        }
        #endregion 
    }    
}
