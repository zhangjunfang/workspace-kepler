using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    public partial class frmReportSet : FormEx
    {
        /// <summary>
        /// 报表对象
        /// </summary>
        public string setObject = string.Empty;
        /// <summary>
        /// 报表设置
        /// </summary>
        public frmReportSet()
        {
            InitializeComponent();
            colSetWidth.ValueType = typeof(int);
            dgvSet.ReadOnly = false;
            dgvSet.Columns[colSetName.Name].ReadOnly = true;
            pnlContainer.BackColor = ColorTranslator.FromHtml("#eff8ff");
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //加载
        private void frmReportSet_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(setObject))
            {
                return;
            }
            #region 绑定用户对报表的设置
            DataTable dt = DBHelper.GetTable("", "tb_report_set", "*", string.Format("set_object='{0}' and set_user='{1}'", setObject,GlobalStaticObj.UserID), "", "order by set_num");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow dgvr = dgvSet.Rows[dgvSet.Rows.Add()];
                dgvr.Cells[colSetID.Name].Value = dr["set_id"];
                dgvr.Cells[colSetName.Name].Value = dr["set_name"];
                dgvr.Cells[colSetWidth.Name].Value = dr["set_width"];
                dgvr.Cells[colIsShow.Name].Value = CommonCtrl.IsNullToString(dr["is_show"]) == "1";
                dgvr.Cells[colIsPrint.Name].Value = CommonCtrl.IsNullToString(dr["is_print"]) == "1";
            }
            #endregion
        }

        //确认，保存到数据库并关闭当前窗体
        private void btnOK_Click(object sender, EventArgs e)
        {
            dgvSet.EndEdit();
            List<SysSQLString> listSql = new List<SysSQLString>();
            bool isCheck = true;
            foreach (DataGridViewRow dgvr in dgvSet.Rows)
            {
                string width = CommonCtrl.IsNullToString(dgvr.Cells[colSetWidth.Name].Value);
                if (width.Length == 0)
                {
                    isCheck = false;
                    dgvSet.CurrentCell = dgvr.Cells[colSetWidth.Name];
                    break;
                }
                SysSQLString sql = new SysSQLString();
                sql.cmdType = CommandType.Text;
                sql.sqlString = @"update tb_report_set set set_width=@set_width,is_show=@is_show,is_print=@is_print where set_id=@set_id;";
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("set_id", dgvr.Cells[colSetID.Name].Value.ToString());
                sql.Param.Add("set_width", dgvr.Cells[colSetWidth.Name].Value.ToString());
                sql.Param.Add("is_show", Convert.ToBoolean(dgvr.Cells[colIsShow.Name].Value)?"1":"0");
                sql.Param.Add("is_print", Convert.ToBoolean(dgvr.Cells[colIsPrint.Name].Value)?"1":"0");
                listSql.Add(sql);
            }
            if (!isCheck)
            {
                return;
            }
            if (DBHelper.BatchExeSQLStringMultiByTrans("", listSql))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
