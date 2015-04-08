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

namespace HXCServerWinForm.UCForm.OnlineQuery
{
    public partial class UCLoginLog : UCBase
    {
        public UCLoginLog()
        {
            InitializeComponent();
            base.ExportEvent += new ClickHandler(UCLoginLog_ExportEvent);
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
            if (cmbacc.SelectedIndex == 0)
            {
                MessageBoxEx.ShowWarning("请选择帐套");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(dtploginstart.Value) && !string.IsNullOrEmpty(dtploginend.Value) && dtploginstart.Value.CompareTo(dtploginend.Value) > 0)
                {
                    MessageBoxEx.ShowWarning("登录开始时间不能大于结束时间");
                    return;
                }

                if (!string.IsNullOrEmpty(dtpexitstart.Value) && !string.IsNullOrEmpty(dtpexitend.Value) && dtpexitstart.Value.CompareTo(dtpexitend.Value) > 0)
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
                if (!string.IsNullOrEmpty(dtpexitstart.Value))
                {
                    long ticks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpexitstart.Value));
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
                GlobalStaticObj_Server.GlobalLogService.WriteLog("登录日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 登录日志-导出
        /// </summary>
        void UCLoginLog_ExportEvent(object sender, EventArgs e)
        {
            try
            {
                string fileName = "登录日志" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                ExcelHandler.ExportExcel(fileName, dgvUser);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("登录日志", ex);
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
            dtpexitstart.Value = "";
            txtname.Caption = "";
        }

        /// <summary> 换页
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPageData(where);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("登录日志", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string fieldNmae = dgvUser.Columns[e.ColumnIndex].DataPropertyName;
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }

            if (fieldNmae.Equals("exit_time") || fieldNmae.Equals("login_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);

            }
        }

        /// <summary> 选择帐套
        /// </summary>
        private void cmbacc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbacc.SelectedIndex > 0)
            {
                accCode = cmbacc.SelectedValue.ToString();
                BindCmb();
            }
            else
            {
                cmbcom.DataSource = null;
                cmborg.DataSource = null;
                cmbrole.DataSource = null;
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
            dr = dt.NewRow();
            dr["setbook_code"] = "000";
            dr["setbook_name"] = "服务端";
            dt.Rows.InsertAt(dr, 1);
            cmbacc.DataSource = dt;
            cmbacc.ValueMember = "setbook_code";
            cmbacc.DisplayMember = "setbook_name";
            cmbacc.SelectedIndex = 0;

        }

        /// <summary> 绑定下拉框
        /// </summary>
        private void BindCmb()
        {
            DataTable dt = DBHelper.GetTable("获取公司信息", GlobalStaticObj_Server.DbPrefix + accCode, "tb_company", "com_id,com_name", "", "", "");
            DataRow dr = dt.NewRow();
            dr["com_id"] = "0";
            dr["com_name"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            cmbcom.DataSource = dt;
            cmbcom.ValueMember = "com_id";
            cmbcom.DisplayMember = "com_name";

            dt = DBHelper.GetTable("获取组织信息", GlobalStaticObj_Server.DbPrefix + accCode, "tb_organization", "org_id,org_name", "", "", "");
            dr = dt.NewRow();
            dr["org_id"] = "0";
            dr["org_name"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            cmborg.DataSource = dt;
            cmborg.ValueMember = "org_id";
            cmborg.DisplayMember = "org_name";

            dt = DBHelper.GetTable("获取角色信息", GlobalStaticObj_Server.DbPrefix + accCode, "sys_role", "role_id,role_name", "", "", "");
            dr = dt.NewRow();
            dr["role_id"] = "0";
            dr["role_name"] = "全部";
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
            if (cmbacc.SelectedValue == null)
            {
                return;
            }
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询在线用户", GlobalStaticObj_Server.DbPrefix + cmbacc.SelectedValue.ToString(), "v_login_log", "*", where, "", "order by login_time desc", page.PageIndex, page.PageSize, out recordCount);
            dgvUser.DataSource = dt;
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        #endregion
    }
}
