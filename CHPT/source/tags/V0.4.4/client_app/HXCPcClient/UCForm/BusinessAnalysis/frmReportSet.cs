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
using Utility.Common;

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
            DataTable dt = DBHelper.GetTable("", "tb_report_set", "*", string.Format("set_object='{0}' and set_user='{1}'", setObject, GlobalStaticObj.UserID), "", "order by set_num");
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
                sql.sqlString = @"update tb_report_set set set_width=@set_width,is_show=@is_show,is_print=@is_print,set_num=@set_num,update_time=@update_time where set_id=@set_id;";
                sql.Param = new Dictionary<string, string>();
                sql.Param.Add("set_id", dgvr.Cells[colSetID.Name].Value.ToString());
                sql.Param.Add("set_width", dgvr.Cells[colSetWidth.Name].Value.ToString());
                sql.Param.Add("is_show", Convert.ToBoolean(dgvr.Cells[colIsShow.Name].Value) ? "1" : "0");
                sql.Param.Add("is_print", Convert.ToBoolean(dgvr.Cells[colIsPrint.Name].Value) ? "1" : "0");
                sql.Param.Add("set_num", dgvr.Index.ToString());
                sql.Param.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString());
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
        //上移
        private void btnUp_Click(object sender, EventArgs e)
        {
            dgvSet.EndEdit();
            // 选择的行号  
            int selectedRowIndex = GetSelectedRowIndex(this.dgvSet);

            if (selectedRowIndex >= 1)
            {
                // 拷贝选中的行  
                DataGridViewRow newRow = dgvSet.Rows[selectedRowIndex];

                // 删除选中的行  
                dgvSet.Rows.Remove(dgvSet.Rows[selectedRowIndex]);

                // 将拷贝的行，插入到选中的上一行位置  
                dgvSet.Rows.Insert(selectedRowIndex - 1, newRow);

                // 选中最初选中的行  
                dgvSet.Rows[selectedRowIndex - 1].Selected = true;
                SetEnabled(selectedRowIndex - 1);
            }
        }
        //下移
        private void btnDown_Click(object sender, EventArgs e)
        {
            dgvSet.EndEdit();
            int selectedRowIndex = GetSelectedRowIndex(this.dgvSet);
            if (selectedRowIndex < dgvSet.Rows.Count - 1)
            {
                // 拷贝选中的行  
                DataGridViewRow newRow = dgvSet.Rows[selectedRowIndex];

                // 删除选中的行  
                dgvSet.Rows.Remove(dgvSet.Rows[selectedRowIndex]);

                // 将拷贝的行，插入到选中的下一行位置  
                dgvSet.Rows.Insert(selectedRowIndex + 1, newRow);

                // 选中最初选中的行  
                dgvSet.Rows[selectedRowIndex + 1].Selected = true;
                SetEnabled(selectedRowIndex + 1);
            }
        }

        // 获取DataGridView中选择的行索引号  
        private int GetSelectedRowIndex(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Selected)
                {
                    return row.Index;
                }
            }
            return 0;
        }

        void SetEnabled(int rowIndex)
        {
            if (rowIndex == 0)
            {
                btnUp.Enabled = false;
                btnDown.Enabled = true;
            }
            else if (rowIndex == dgvSet.RowCount - 1)
            {
                btnDown.Enabled = false;
                btnUp.Enabled = true;
            }
            else
            {
                btnDown.Enabled = true;
                btnUp.Enabled = true;
            }
        }

        private void dgvSet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            SetEnabled(e.RowIndex);
        }
    }
}
