using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using SYSModel;
using ServiceStationClient.ComponentUI;
using Utility.Common;

namespace HXCPcClient.UCForm.SysManage.MemberPara
{
    /// <summary>
    /// 会员参数设置 预览
    /// 孙明生
    /// </summary>
    public partial class UCMemberParaView : UCBase
    {
        public UCMemberParaManage uc;
        public string id;
        public WindowStatus wStatus;
        public UCMemberParaView()
        {
            InitializeComponent();
        }
        #region Load
        private void UCMemberParaView_Load(object sender, EventArgs e)
        {
            CommonFuncCall.BindComBoxDataSource(cbomember_grade_id, "sys_member_grade", "请选择");
            if (wStatus == WindowStatus.View)
            {
                string strSql = "select c.*,(select USER_NAME from sys_user where user_id =c.create_by )as create_Username , "
                    + "(select USER_NAME from sys_user where user_id =c.update_by ) as update_username  from tb_CustomerSer_member_setInfo c where c.setInfo_id='" + id + "'";
                SQLObj sqlobj = new SQLObj();
                sqlobj.cmdType = CommandType.Text;
                sqlobj.Param = new Dictionary<string, ParamObj>();
                sqlobj.sqlString = strSql;
                DataSet ds = DBHelper.GetDataSet("查询会员参数设置信息", sqlobj);
                if (ds == null || ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBoxEx.Show("查询会员参数设置信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dt = ds.Tables[0];
                cbomember_grade_id.SelectedValue = dt.Rows[0]["member_grade_id"].ToString();
                lblservice_project_discount.Text = dt.Rows[0]["service_project_discount"].ToString();
                lblparts_discount.Text = dt.Rows[0]["parts_discount"].ToString();
                lblSubscription_Ratio.Text = dt.Rows[0]["Subscription_Ratio"].ToString();

                lblcreate_by.Text = dt.Rows[0]["create_Username"].ToString();
                lblcreate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["create_time"].ToString())).ToString();
                lblupdate_by.Text = dt.Rows[0]["update_username"].ToString();
                lblupdate_time.Text = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["update_time"].ToString())).ToString();

                BindDgvProjrct();
                BindDgvParts();
            }
            DataGridViewStyle.DataGridViewBgColor(dgvprojrct);
            DataGridViewStyle.DataGridViewBgColor(dgvparts);
        }
        #endregion

        #region 查询绑定会员参数设置特殊维修项目折扣
        /// <summary>
        /// 查询绑定会员参数设置特殊维修项目折扣
        /// </summary>
        private void BindDgvProjrct()
        {
            SQLObj sqlobj = new SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, ParamObj>();
            sqlobj.sqlString = "SELECT p.*,w.project_name,w.project_num,w.quota_price,'' as discount_price  FROM tb_CustomerSer_member_setInfo_projrct p left join tb_workhours w on w.whours_id=p.service_project_id  "
+ " where p.enable_flag='1'  and p.setInfo_id='" + id + "'";
            DataSet dsProjrct = DBHelper.GetDataSet("查询会员参数设置特殊维修项目折扣", sqlobj);
            if (dsProjrct != null && dsProjrct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsProjrct.Tables[0].Rows)
                {
                    DataGridViewRow row = dgvprojrct.Rows[dgvprojrct.Rows.Add()];
                    row.Cells["project_num"].Value = dr["project_num"].ToString();
                    row.Cells["project_name"].Value = dr["project_name"].ToString();
                    row.Cells["quota_price"].Value = dr["quota_price"].ToString();
                    row.Cells["project_service_project_discount"].Value = dr["service_project_discount"].ToString();
                    row.Cells["project_remark"].Value = dr["remark"].ToString();
                    row.Cells["service_project_id"].Value = dr["service_project_id"].ToString();
                    row.Cells["p_setInfo_id"].Value = dr["setInfo_id"].ToString();
                    row.Cells["setInfo_projrct_id"].Value = dr["id"].ToString();
                    if (dr["service_project_discount"].ToString() != "")
                    {
                        int iValue = Convert.ToInt32(dr["service_project_discount"].ToString());
                        bool bln = false;
                        decimal quota_price = 0;
                        string strquota_price = Utility.Common.Validator.IsDecimal(dr["quota_price"].ToString(), 10, 2, ref bln);
                        if (bln)
                        {
                            quota_price = Convert.ToDecimal(strquota_price);
                            decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2));
                            row.Cells["project_discount_price"].Value = discount_price.ToString();
                        }
                        else
                        {
                            row.Cells["project_discount_price"].Value = "";
                        }
                    }
                }
            }
        }
        #endregion

        #region 查询绑定会员参数设置特殊配件折扣表
        /// <summary>
        /// 查询绑定会员参数设置特殊配件折扣表
        /// </summary>
        private void BindDgvParts()
        {
            SQLObj sqlobj = new SQLObj();
            sqlobj.cmdType = CommandType.Text;
            sqlobj.Param = new Dictionary<string, ParamObj>();
            sqlobj.sqlString = "  SELECT p.*,w.ser_parts_code,w.parts_name,w.ref_out_price,'' as discount_price  FROM tb_CustomerSer_member_setInfo_parts p left join tb_parts w on w.parts_id=p.parts_id  "
                             + " where p.enable_flag='1'  and p.setInfo_id='" + id + "'";
            DataSet dsParts = DBHelper.GetDataSet("查询会员参数设置特殊配件折扣", sqlobj);
            if (dsParts != null && dsParts.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsParts.Tables[0].Rows)
                {
                    DataGridViewRow row = dgvparts.Rows[dgvparts.Rows.Add()];

                    row.Cells["ser_parts_code"].Value = dr["ser_parts_code"].ToString();
                    row.Cells["parts_name"].Value = dr["parts_name"].ToString();
                    row.Cells["ref_out_price"].Value = dr["ref_out_price"].ToString();
                    row.Cells["parts_discount"].Value = dr["parts_discount"].ToString();
                    row.Cells["setInfo_parts_id"].Value = dr["id"].ToString();
                    row.Cells["remark"].Value = dr["remark"].ToString();
                    row.Cells["parts_id"].Value = dr["parts_id"].ToString();
                    row.Cells["setInfo_id"].Value = dr["setInfo_id"].ToString();
                    if (dr["ref_out_price"].ToString() != "")
                    {
                        int iValue = Convert.ToInt32(dr["parts_discount"].ToString());
                        bool bln = false;
                        decimal quota_price = 0;
                        string strquota_price = Utility.Common.Validator.IsDecimal(dr["ref_out_price"].ToString(), 10, 2, ref bln);
                        if (bln)
                        {
                            quota_price = Convert.ToDecimal(strquota_price);
                            decimal discount_price = Math.Abs(Math.Round((quota_price * iValue) / 100, 2));
                            row.Cells["discount_price"].Value = discount_price.ToString();
                        }
                        else
                        {
                            row.Cells["discount_price"].Value = "";
                        }
                    }
                }
            }
        }
        #endregion
    }
}
