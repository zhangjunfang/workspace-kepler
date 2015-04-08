using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis.SaleReport
{
    public partial class UCSaleDaily : UCReport
    {
        public UCSaleDaily()
            : base("v_sale_daily_report", "销售日报表表")
        {
            InitializeComponent();
            
        }
        //窗体加载
        private void UCSaleDaily_Load(object sender, EventArgs e)
        {

            //dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.Black;
            //dgvReport.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#cceaff");
            //dgvReport.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#cceaff");
            //dgvReport.RowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#cceaff");
            //dgvReport.RowsDefaultCellStyle.BackColor = Color.Black;
            //dgvReport.DefaultCellStyle.BackColor = Color.Black;
            //设置负数要显示成红色的字段
            listNegative = new List<string>();
            listNegative.Add("数量");
            listNegative.Add("货款");
            listNegative.Add("税额");
            listNegative.Add("金额");
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            BindData();
        }
        //选择公司，绑定公司所有部门
        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue == null)
            {
                return;
            }
            CommonFuncCall.BindDepartment(cboorg_id, cboCompany.SelectedValue.ToString(), "全部");
        }
        //配件选择
        private void txtcparts_code_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcparts_code.Text = parts.PartsCode;
                txt配件名称.Caption = parts.PartsName;
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            dt = DBHelper.GetTable("", "v_sale_daily_report", "*", GetWhere(), "", "order by 开单类型");
            List<string> listNot = new List<string>();
            listNot.Add("create_time");
            //按配件分组
            dt.DataTableGroup("开单类型", "配件名称", listNot);
            dgvReport.DataSource = dt;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
