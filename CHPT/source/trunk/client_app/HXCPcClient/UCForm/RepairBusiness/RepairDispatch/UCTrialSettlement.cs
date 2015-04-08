using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.RepairDispatch
{
    /// <summary>
    /// 维修管理-维修调度试结算
    /// Author：JC
    /// AddTime：2014.11.11
    /// </summary>
    public partial class UCTrialSettlement : FormChooser
    {
        #region 属性设置
        /// <summary>
        /// 工时费用
        /// </summary>
        public string strHMoney = string.Empty;
        /// <summary>
        /// 配件费用
        /// </summary>
        public string strPMoney = string.Empty;
        /// <summary>
        /// 其他项目费用
        /// </summary>
        public string strOMoney = string.Empty;
        #endregion
        public UCTrialSettlement()
        {
            InitializeComponent();
            ControlsConfig.TextToDecimal(txtPreMoney);
        }

        #region 窗体Load事件
        private void UCTrialSettlement_Load(object sender, EventArgs e)
        {
            txtHMoney.Caption = strHMoney;
            txtHToatal.Caption = strHMoney;
            txtPMoney.Caption = strPMoney;
            txtPToatal.Caption = strPMoney;
            txtOMoney.Caption =strOMoney;
            txtOToatal.Caption = strOMoney;
            BindHRate();
            BindPRate();
            BindORate();
        }
        #endregion 

        #region 获取工时税率
        /// <summary>
        /// 获取工时税率
        /// </summary>
        private void BindHRate()
        {
            //string strWhere = "period_validity<='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "'";
            //DataTable dt = DBHelper.GetTable("获取维修套餐信息", "tb_b_set_repair_package_set ", "repair_package_set_id,package_name", strWhere, "", "order by repair_package_set_id desc");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "0.00"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    list.Add(new ListItem(dr["repair_package_set_id"], dr["package_name"].ToString()));
            //}            
            cobHTaxRate.DataSource = list;
            cobHTaxRate.ValueMember = "Value";
            cobHTaxRate.DisplayMember = "Text";
        }
        #endregion

        #region 获取配件税率
        /// <summary>
        /// 获取配件税率
        /// </summary>
        private void BindPRate()
        {
            //string strWhere = "period_validity<='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "'";
            //DataTable dt = DBHelper.GetTable("获取维修套餐信息", "tb_b_set_repair_package_set ", "repair_package_set_id,package_name", strWhere, "", "order by repair_package_set_id desc");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "0.00"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    list.Add(new ListItem(dr["repair_package_set_id"], dr["package_name"].ToString()));
            //}            
            cobPTaxRate.DataSource = list;
            cobPTaxRate.ValueMember = "Value";
            cobPTaxRate.DisplayMember = "Text";
        }
        #endregion

        #region 获取其他项目税率
        /// <summary>
        /// 获取其他项目税率
        /// </summary>
        private void BindORate()
        {
            //string strWhere = "period_validity<='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "' and valid_until>='" + Common.LocalDateTimeToUtcLong(DateTime.Now).ToString() + "'";
            //DataTable dt = DBHelper.GetTable("获取维修套餐信息", "tb_b_set_repair_package_set ", "repair_package_set_id,package_name", strWhere, "", "order by repair_package_set_id desc");
            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "0.00"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    list.Add(new ListItem(dr["repair_package_set_id"], dr["package_name"].ToString()));
            //}            
            cobOTaxRate.DataSource = list;
            cobOTaxRate.ValueMember = "Value";
            cobOTaxRate.DisplayMember = "Text";
        }
        #endregion

        #region 工时费用税率值发生改变时
        private void cobHTaxRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobHTaxRate.SelectedValue)))
            {
                return;
            }
            txtHTax.Caption = (Convert.ToDecimal(cobHTaxRate.SelectedValue.ToString()) * Convert.ToDecimal(txtHMoney.Caption)).ToString();
            txtHToatal.Caption = (Convert.ToDecimal(txtHMoney.Caption.Trim()) * (1 + Convert.ToDecimal(cobHTaxRate.SelectedValue.ToString()))).ToString();
            //费用合计
            txtToal.Caption = (Convert.ToDecimal(txtHToatal.Caption.Trim()) + Convert.ToDecimal(txtPToatal.Caption.Trim()) + Convert.ToDecimal(txtOToatal.Caption.Trim()) - Convert.ToDecimal(txtPreMoney.Caption.Trim())).ToString();
        }
        #endregion

        #region 配件费用税率值发生改变时
        private void cobPTaxRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobPTaxRate.SelectedValue)))
            {
                return;
            }
            txtPTax.Caption = (Convert.ToDecimal(cobPTaxRate.SelectedValue.ToString()) * Convert.ToDecimal(txtPMoney.Caption)).ToString();
            txtPToatal.Caption = (Convert.ToDecimal(txtPMoney.Caption.Trim()) * (1 + Convert.ToDecimal(cobPTaxRate.SelectedValue.ToString()))).ToString();
            //费用合计
            txtToal.Caption = (Convert.ToDecimal(txtHToatal.Caption.Trim()) + Convert.ToDecimal(txtPToatal.Caption.Trim()) + Convert.ToDecimal(txtOToatal.Caption.Trim()) - Convert.ToDecimal(txtPreMoney.Caption.Trim())).ToString();

        }
        #endregion

        #region 其他项目费用税率值发生改变时
        private void cobOTaxRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobOTaxRate.SelectedValue)))
            {
                return;
            }
            txtOTax.Caption = (Convert.ToDecimal(cobOTaxRate.SelectedValue.ToString()) * Convert.ToDecimal(txtOMoney.Caption)).ToString();
            txtOToatal.Caption = (Convert.ToDecimal(txtOMoney.Caption.Trim()) * (1 + Convert.ToDecimal(cobOTaxRate.SelectedValue.ToString()))).ToString();
            //费用合计
            txtToal.Caption = (Convert.ToDecimal(txtHToatal.Caption.Trim()) + Convert.ToDecimal(txtPToatal.Caption.Trim()) + Convert.ToDecimal(txtOToatal.Caption.Trim()) - Convert.ToDecimal(txtPreMoney.Caption.Trim())).ToString();

        }
        #endregion

        #region -计算费用合计
        private void txtPreMoney_UserControlValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreMoney.Caption.Trim()))
            {
                if (Convert.ToDecimal(txtPreMoney.Caption.Trim()) > Convert.ToDecimal(txtToal.Caption.Trim()))
                {
                    MessageBoxEx.Show("优惠金额不能大于费用合计金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //费用合计
                txtToal.Caption = (Convert.ToDecimal(txtHToatal.Caption.Trim()) + Convert.ToDecimal(txtPToatal.Caption.Trim()) + Convert.ToDecimal(txtOToatal.Caption.Trim()) - Convert.ToDecimal(txtPreMoney.Caption.Trim())).ToString();
            }
        }
        #endregion

    }
}
