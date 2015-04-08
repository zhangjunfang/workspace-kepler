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
using Utility.Common;

namespace HXCPcClient.Chooser
{
    public partial class frmDictionaries : FormEx
    {
        public frmDictionaries()
        {
            InitializeComponent();
        }

        #region 外部属性
        /// <summary> 码表id
        /// </summary>
        public string Dic_ID { get; set; }
        /// <summary> 码表编码
        /// </summary>
        public string Dic_Code { get; set; }
        /// <summary> 码表名称
        /// </summary>
        public string Dic_Name { get; set; }
        #endregion

        private void frmDictionaries_Load(object sender, EventArgs e)
        {
            DataGridViewEx.SetDataGridViewStyle(dgvDicList);
            btnSearch_Click(null, null);
        }

        #region 事件
        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string where = "parent_id='-1' and enable_flag='1'";
            if (!string.IsNullOrEmpty(txtdic_code.Caption.Trim()))
            {
                where += string.Format(" and  dic_code like '%{0}%'", txtdic_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtdic_name.Caption.Trim()))
            {
                where += string.Format(" and  dic_name like '%{0}%'", txtdic_name.Caption.Trim());
            }
            BindPageData(where);
        }

        /// <summary> 清除查询条件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtdic_code.Caption = string.Empty;
            txtdic_name.Caption = string.Empty;
            txtdic_code.Focus();
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = dgvDicList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("enable_flag"))
            {
                string str = e.Value.ToString();
                e.Value = str.Equals("1") ? "未删除" : "删除";
            }
            if (fieldNmae.Equals("data_sources"))
            {
                string str = e.Value.ToString();
                e.Value = str.Equals("1") ? "自建" : "宇通";
            }
        }
        /// <summary> 选择码表
        /// </summary>
        private void dgvDicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Dic_ID = dgvDicList.Rows[e.RowIndex].Cells["dic_id"].Value.ToString();
                this.Dic_Code = dgvDicList.Rows[e.RowIndex].Cells["dic_code"].Value.ToString();
                this.Dic_Name = dgvDicList.Rows[e.RowIndex].Cells["dic_name"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            DataTable dt = DBHelper.GetTable("选择器查询码表", "v_dictionaries", "*", where, "", "order by dic_code");
            dgvDicList.DataSource = dt;
        }
        #endregion
    }
}
