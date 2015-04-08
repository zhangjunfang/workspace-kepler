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

namespace HXCPcClient.UCForm.BusinessAnalysis.PurchaseReport
{
    public partial class UCPurchaseDaily : UCReport
    {
        public UCPurchaseDaily()
            : base("v_purchase_daily_report","采购日报表")
        {
            InitializeComponent();
        }
        //窗体加载
        private void UCPurchaseDaily_Load(object sender, EventArgs e)
        {
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
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        void BindData()
        {
            dt = DBHelper.GetTable("", "v_purchase_daily_report", "*", GetWhere(), "", "order by 开单类型");
            List<string> listNot = new List<string>();
            listNot.Add("create_time");
            //按配件分组
            dt.DataTableGroup("开单类型","配件名称",listNot);
            dgvReport.DataSource = dt;
        }

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex<0 || e.RowIndex<0)
            //{
            //    return;
            //}
            //DataGridViewCell dgvc=dgvReport.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //if (dgvc.OwningColumn.DataPropertyName == "数量" || dgvc.OwningColumn.DataPropertyName == "货款" ||
            //    dgvc.OwningColumn.DataPropertyName == "税额" || dgvc.OwningColumn.DataPropertyName == "金额")
            //{
            //    string num = CommonCtrl.IsNullToString(e.Value);
            //    if (num.Length == 0)
            //    {
            //        return;
            //    }
            //    if (Convert.ToDecimal(num) < 0)
            //    {
            //        e.CellStyle.ForeColor = Color.Red;
            //        e.Value = Convert.ToDecimal(num) * -1;
            //    }
            //}
        }
    }
}
