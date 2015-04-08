using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.Common;
using BLL;
using HXC_FuncUtility;
using HXCServerWinForm.UCForm.Personnel;
using ServiceStationClient.ComponentUI;
using System.Collections.Generic;

namespace HXCServerWinForm.UCForm.OnlineQuery
{
    public partial class UCOnLineUser : UCBase
    {
        public UCOnLineUser()
        {
            InitializeComponent();
            base.ExportEvent += new ClickHandler(UCOnLineUser_ExportEvent);
            base.ShotoffEvent += new ClickHandler(UCOnLineUser_ShotoffEvent);
        }
        /// <summary> 查询条件
        /// </summary>
        private string where = string.Empty;
        /// <summary> 查询时间
        /// </summary>
        DateTime dtQuery;
        string accCode = "";
        private void UCOnLineUser_Load(object sender, EventArgs e)
        {
            base.SetOpButtonVisible(this.Name);//角色按钮权限-是否隐藏
            DataGridViewEx.SetDataGridViewStyle(dgvUser);
            dtQuery = GlobalStaticObj_Server.Instance.CurrentDateTime;
            BindAccCmb();
        }

        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbacc.SelectedIndex == 0)
                {
                    MessageBoxEx.ShowWarning("请选择帐套");
                    return;
                }
                if (!string.IsNullOrEmpty(dtploginstart.Value) && !string.IsNullOrEmpty(dtploginend.Value) && dtploginstart.Value.CompareTo(dtploginend.Value) > 0)
                {
                    MessageBoxEx.ShowWarning("登录开始时间不能大于结束时间");
                    return;
                }

                if (!string.IsNullOrEmpty(dtpregstart.Value) && !string.IsNullOrEmpty(dtpregend.Value) && dtpregstart.Value.CompareTo(dtpregend.Value) > 0)
                {
                    MessageBoxEx.ShowWarning("注册开始时间不能大于结束时间");
                    return;
                }

                dtQuery = GlobalStaticObj_Server.Instance.CurrentDateTime;
                where = string.Format("1=1 ");
                if (cmbcom.SelectedIndex > 0)
                {
                    where += string.Format(" and  com_id like '%{0}%'", cmbcom.SelectedValue.ToString().Trim());
                }
                if (cmborg.SelectedIndex > 0)
                {
                    where += string.Format(" and  org_id = '{0}'", cmborg.SelectedValue.ToString().ToString());
                }
                if (cmbrole.SelectedIndex > 0)
                {
                    where += string.Format(" and  role_id = '{0}'", cmbrole.SelectedValue.ToString().ToString());
                }
                if (!string.IsNullOrEmpty(dtploginstart.Value))
                {
                    long ticks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtploginstart.Value));
                    where += " and login_time>=" + ticks.ToString();
                }
                if (!string.IsNullOrEmpty(dtploginend.Value))
                {
                    long ticks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtploginend.Value).AddDays(1));
                    where += " and login_time<" + ticks.ToString();
                }
                if (!string.IsNullOrEmpty(dtpregstart.Value))
                {
                    long ticks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpregstart.Value));
                    where += " and create_time>=" + ticks.ToString();
                }
                if (!string.IsNullOrEmpty(txtname.Caption.Trim()))
                {
                    where += " and user_name like '%" + txtname.Caption.Trim() + "%'";
                }
                page.PageIndex = 1;
                BindPageData(where);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("在线用户", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 踢出用户
        /// </summary>
        void UCOnLineUser_ShotoffEvent(object sender, EventArgs e)
        {
            try
            {
                if (cmbacc.SelectedIndex == 0)
                {
                    MessageBoxEx.ShowInformation("请选择所属账套");
                    return;
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (DataGridViewRow dr in dgvUser.Rows)
                {
                    object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                    if (isCheck != null && (bool)isCheck)
                    {
                        string userid = dr.Cells["user_id"].Value.ToString();
                        if (!dic.ContainsKey("userid"))
                        {
                            dic.Add(userid, dr.Cells["land_name"].Value.ToString());
                        }
                    }
                }
                if (dic.Keys.Count == 0)
                {
                    MessageBoxEx.ShowInformation("请选择踢出用户！");
                    return;
                }
                if (MessageBoxEx.ShowQuestion("你确定要将选中用户踢出系统吗？"))
                {
                    string accCode = cmbacc.SelectedValue.ToString();
                    foreach (string str in dic.Keys)
                    {
                        LoginSessionInfo.Instance.ShotOffUser(accCode, str);
                        if (accCode == GlobalStaticObj_Server.CommAccCode)
                        {
                            ClientUser.UserLoginOut(dic[str], accCode);
                        }
                        else
                        {
                            ServerUser.UserLoginOut(dic[str], accCode);
                        }
                    }
                }
                MessageBoxEx.ShowInformation("操作成功");
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("在线用户", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 在线用户
        /// </summary>
        void UCOnLineUser_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string fileName = "在线用户" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvUser);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("在线用户", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 清除
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbacc.SelectedIndex = 0;
            dtploginstart.Value = "";
            dtploginend.Value = "";
            dtpregstart.Value = "";
            txtname.Caption = "";
        }

        /// <summary> 换页
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData(where);
        }

        /// <summary> 查看人员详细
        /// </summary>
        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUser.Columns[e.ColumnIndex].DataPropertyName == "user_name")
            {
                try
                {
                    string id = dgvUser.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
                    UCPersonnelView uc = new UCPersonnelView();
                    uc.id = id;
                    uc.ucName = this.Name;
                    uc.accCode = cmbacc.SelectedValue.ToString();
                    uc.wStatus = WindowStatus.View;
                    base.addUserControl(uc, "在线用户-查看", "UCPersonnelView" + id, this.Tag.ToString(), this.Name);
                }
                catch (Exception ex)
                {
                    GlobalStaticObj_Server.GlobalLogService.WriteLog("在线用户", ex);
                    MessageBoxEx.ShowWarning("程序异常");
                }
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                string fieldNmae = dgvUser.Columns[e.ColumnIndex].DataPropertyName;
                if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    if (fieldNmae.Equals("online_time"))
                    {
                        object obj = dgvUser.Rows[e.RowIndex].Cells["login_time"].Value;
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        {
                            DateTime dtLogin = Common.UtcLongToLocalDateTime(Convert.ToInt64(obj.ToString()));
                            TimeSpan ts = dtQuery - dtLogin;
                            e.Value = string.Format("{0}:{1}:{2}", (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0'), ts.Minutes.ToString().PadLeft(2, '0'), ts.Seconds.ToString().PadLeft(2, '0'));
                        }
                    }
                    return;
                }

                if (fieldNmae.Equals("create_time") || fieldNmae.Equals("login_time"))
                {
                    long ticks = (long)e.Value;
                    e.Value = Common.UtcLongToLocalDateTime(ticks);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("在线用户", ex);
            }
        }

        /// <summary> 选择帐套
        /// </summary>
        private void cmbacc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbacc.SelectedIndex > 0)
            {
                accCode = cmbacc.SelectedValue.ToString();
                BindCom();
                BindRole();
            }
            else
            {
                cmbcom.DataSource = null;
                cmborg.DataSource = null;
                cmbrole.DataSource = null;
            }
        }

        /// <summary> 选择公司
        /// </summary>
        private void cmbcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbacc.SelectedIndex > 0)
            {
                accCode = cmbacc.SelectedValue.ToString();
                BindOrg();
            }
            else
            {
                cmborg.DataSource = null;
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定下拉框
        /// </summary>
        private void BindAccCmb()
        {
            DataTable dt = DBHelper.GetTable("获取帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "setbook_code,setbook_name", "enable_flag=1", "", "order by setbook_code");
            DataRow dr = dt.NewRow();
            dr["setbook_code"] = "0";
            dr["setbook_name"] = "请选择";
            dt.Rows.InsertAt(dr, 0);
            cmbacc.DataSource = dt;
            cmbacc.ValueMember = "setbook_code";
            cmbacc.DisplayMember = "setbook_name";
            cmbacc.SelectedIndex = 0;

        }

        /// <summary> 绑定公司
        /// </summary>
        private void BindCom()
        {
            DataTable dt = DBHelper.GetTable("获取公司信息", GlobalStaticObj_Server.DbPrefix + accCode, "tb_company", "com_id,com_name", "status ='1' and enable_flag='1' and data_source!='2'", "", "");
            DataRow dr = dt.NewRow();
            dr["com_id"] = "0";
            dr["com_name"] = "不限";
            dt.Rows.InsertAt(dr, 0);
            cmbcom.DataSource = dt;
            cmbcom.ValueMember = "com_id";
            cmbcom.DisplayMember = "com_name";
        }

        /// <summary> 绑定组织
        /// </summary>
        private void BindOrg()
        {
            DataTable dt = DBHelper.GetTable("获取组织信息", GlobalStaticObj_Server.DbPrefix + accCode, "tb_organization", "org_id,org_name", "status ='1' and enable_flag='1' and com_id='" + cmbcom.SelectedValue.ToString() + "'", "", "");
            DataRow dr = dt.NewRow();
            dr["org_id"] = "0";
            dr["org_name"] = "不限";
            dt.Rows.InsertAt(dr, 0);
            cmborg.DataSource = dt;
            cmborg.ValueMember = "org_id";
            cmborg.DisplayMember = "org_name";
        }

        /// <summary> 绑定角色
        /// </summary>
        private void BindRole()
        {
            DataTable dt = DBHelper.GetTable("获取角色信息", GlobalStaticObj_Server.DbPrefix + accCode, "sys_role", "role_id,role_name", "state ='1' and enable_flag='1'", "", "");
            DataRow dr = dt.NewRow();
            dr["role_id"] = "0";
            dr["role_name"] = "不限";
            dt.Rows.InsertAt(dr, 0);
            cmbrole.DataSource = dt;
            cmbrole.ValueMember = "role_id";
            cmbrole.DisplayMember = "role_name";
        }

        /// <summary> 绑定数据
        /// </summary>
        /// <param name="where">查询条件</param>
        public void BindPageData(string where)
        {
            where += " and is_online='1'";
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询在线用户", GlobalStaticObj_Server.DbPrefix + cmbacc.SelectedValue.ToString(), "v_onlineuser", "*", where, "", "order by login_time", page.PageIndex, page.PageSize, out recordCount);
            dgvUser.DataSource = dt;
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        #endregion

    }
}
