using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;

namespace HXCPcClient.UCForm.BusinessAnalysis
{
    public partial class UCGiftReport : UCReport
    {
        public UCGiftReport()
            : base("v_gift_report", "赠品汇总报表")
        {
            InitializeComponent();

            Quick qParts = new Quick();
            qParts.BindParts(txtcPartsCode);
            txtcPartsCode.DataBacked += new ServiceStationClient.ComponentUI.TextBox.TextChooser.DataBackHandler(txtcPartsCode_DataBacked);
        }

        void txtcPartsCode_DataBacked(DataRow dr)
        {
            txtcPartsCode.Text = CommonCtrl.IsNullToString(dr["ser_parts_code"]);
            txtPartsName.Caption = CommonCtrl.IsNullToString(dr["parts_name"]);
        }

        private void UCGiftReport_Load(object sender, EventArgs e)
        {
            List<ListItem> listGroup = new List<ListItem>();
            listGroup.Add(new ListItem("1", "按仓库"));
            listGroup.Add(new ListItem("2", "按往来单位"));
            listGroup.Add(new ListItem("3", "按部门"));
            cboType.DataSource = listGroup;
            cboType.DisplayMember = "Text";
            cboType.ValueMember = "Value";
            BindData();
        }
        //配件
        private void txtcPartsCode_ChooserClick(object sender, EventArgs e)
        {
            frmParts parts = new frmParts();
            if (parts.ShowDialog() == DialogResult.OK)
            {
                txtcPartsCode.Text = parts.PartsCode;
                txtPartsName.Caption = parts.PartsName;
            }
        }
        //配件类型
        private void txtcPartsType_ChooserClick(object sender, EventArgs e)
        {
            frmPartsType partsType = new frmPartsType();
            if (partsType.ShowDialog() == DialogResult.OK)
            {
                txtcPartsType.Text = partsType.TypeName;
            }
        }
        //配件车型
        private void txtcVehicleModels_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels vehicleModels = new frmVehicleModels();
            if (vehicleModels.ShowDialog() == DialogResult.OK)
            {
                txtcVehicleModels.Text = vehicleModels.VMName;
            }
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        //绑定
        void BindData()
        {
            string fileds = "配件编码,配件名称,图号,配件类型,单位,sum(收入配件数量) 收入配件数量,sum(收入赠品数量) 收入赠品数量,sum(发出配件数量) 发出配件数量,sum(发出赠品数量) 发出赠品数量,车型,产地,品牌,车厂编码,备注";
            string groupBy = "group by 配件编码,配件名称,图号,配件类型,单位,车型,产地,品牌,车厂编码,备注";
            switch (CommonCtrl.IsNullToString(cboType.SelectedValue))
            {
                case "1":
                    fileds += ",wh_code,wh_name";
                    groupBy += ",wh_code,wh_name order by wh_code,wh_name";
                    dt = DBHelper.GetTable("", "v_gift_report", fileds, GetWhere(), "", groupBy);
                    //按仓库
                    dt.DataTableGroup("wh_code", "配件编码", "仓库编码：", "wh_name", "配件名称", "仓库名称：", null);
                    break;
                case "2":
                    fileds += ",sup_code,sup_name";
                    groupBy += ",sup_code,sup_name order by sup_code,sup_name";
                    dt = DBHelper.GetTable("", "v_gift_report", fileds, GetWhere(), "", groupBy);
                    //按往来单位
                    dt.DataTableGroup("sup_code", "配件编码", "单位编码：", "sup_name", "配件名称", "单位名称：", null);
                    break;
                case "3":
                    fileds += ",org_id,org_name";
                    groupBy += ",org_id,org_name order by org_id,org_name";
                    dt = DBHelper.GetTable("", "v_gift_report", fileds, GetWhere(), "", groupBy);
                    //按部门
                    dt.DataTableGroup("org_id", "配件编码", "部门编码：", "org_name", "配件名称", "部门名称：", null);
                    break;
            }
            dgvReport.DataSource = dt;
        }
    }
}
