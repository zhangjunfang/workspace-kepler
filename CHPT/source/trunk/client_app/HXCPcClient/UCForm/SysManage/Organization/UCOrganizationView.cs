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
        #region 字段属性

        /// <summary>
        /// 组织id
        /// </summary>
        public string id = "";
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCOrganizationManager uc;

        public DataSet dsCom;

        int enumStatus;
        #endregion

        #region 构造和载入函数

        public UCOrganizationView(DataSet ComInfo)
        {
            InitializeComponent();
            base.CancelEvent += new ClickHandler(UCOrganizationView_CancelEvent);
            base.DeleteEvent += new ClickHandler(UCOrganizationView_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCOrganizationView_StatusEvent);
            base.EditEvent += new ClickHandler(UCOrganizationView_EditEvent);
        }

        private void UCOrganizationView_Load(object sender, EventArgs e)
        {

            DataSources.BindComBoxDataEnum(cbostate, typeof(DataSources.EnumStatus), true);//绑定状态 启用 停用
            BindTree();
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
            enumStatus = (int)dt.Rows[0]["status"];
            SetSysManageViewBtn();
            if (enumStatus == (int)DataSources.EnumStatus.Start)
            {
                btnStatus.Caption = "停用";

            }
            else
            {
                btnStatus.Caption = "启用";
            }
        }

        #endregion

        #region 事件方法
        //编辑
        void UCOrganizationView_EditEvent(object sender, EventArgs e)
        {
            UCOrganizationAddOrEdit editFrm = new UCOrganizationAddOrEdit(dsCom,WindowStatus.Edit);
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
            base.addUserControl(editFrm, "组织档案-编辑", "UCSupplierEdit" + id, this.Tag.ToString(), this.Name);
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

        //删除
        void UCOrganizationView_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                List<string> listField = new List<string>();
                listField.Add(id);
                string tipMsg = "是否确认删除?";
                if (dsCom != null && dsCom.Tables[0] != null)
                {
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
                if (listField.Count > 1)
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
                        MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        uc.BindPageData();
                        deleteMenuByTag(this.Tag.ToString(), uc.Name);
                        MessageProcessor.UpdateComOrgInfo();

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

        //启用停用
        void UCOrganizationView_StatusEvent(object sender, EventArgs e)
        {

            if (!MessageBoxEx.ShowQuestion(string.Format("确定要{0}吗？", btnStatus.Caption)))
            {
                if (enumStatus == (int)DataSources.EnumStatus.Start)
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

            }
            else
            {
                MessageBoxEx.ShowError(btnStatus.Caption + "失败！");
                if (enumStatus == (int)DataSources.EnumStatus.Start)
                {
                    btnStatus.Caption = "启用";

                }
                else
                {
                    btnStatus.Caption = "停用";
                }

            }
        }

        /// <summary>
        /// 执行启用停用
        /// </summary>
        /// <returns>是否成功</returns>
        private bool StatusSql()
        {
            List<SysSQLString> listSql = new List<SysSQLString>();
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            SysSQLString sql = new SysSQLString();
            sql.cmdType = CommandType.Text;
            sql.Param = new Dictionary<string, string>();
            string strSql = string.Format("update tb_organization set status=@status where org_id = '{0}' ", id);
            string ids = string.Empty;
            if (enumStatus == (int)DataSources.EnumStatus.Start)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Stop).ToString());


            }
            else if (enumStatus == (int)DataSources.EnumStatus.Stop)
            {
                sql.Param.Add("status", ((int)DataSources.EnumStatus.Start).ToString());
            }
            sql.sqlString = string.Format(strSql, ids);
            listSql.Add(sql);
            return DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup(btnStatus.Caption + "组织", listSql);
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
