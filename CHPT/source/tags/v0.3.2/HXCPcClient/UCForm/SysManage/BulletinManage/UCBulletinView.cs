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
            if (this.dr != null)
            {
                id = dr["announcement_id"].ToString();
            }
            this.parentName = parentName;

            base.SetBtnStatus(WindowStatus.View);

            base.EditEvent += new ClickHandler(UCBulletinView_EditEvent);
            base.DeleteEvent += new ClickHandler(UCBulletinView_DeleteEvent);
            base.VerifyEvent += new ClickHandler(UCBulletinView_VerifyEvent);

            this.InitData();
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
        //审核
        void UCBulletinView_VerifyEvent(object sender, EventArgs e)
        {

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
