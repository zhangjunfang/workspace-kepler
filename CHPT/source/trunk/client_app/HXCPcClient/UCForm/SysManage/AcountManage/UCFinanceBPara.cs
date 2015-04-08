using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Text.RegularExpressions;
using System.Threading;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    /// <summary>
    /// 财务业务参数
    /// </summary>
    public partial class UCFinanceBPara : UCBase
    {
        /// <summary>
        /// 财务业务参数id
        /// </summary>
        string strFinancial_ser_param_id = "";
        public UCFinanceBPara()
        {
            InitializeComponent();

            this.uiHandler -= new UiHandler(this.ShowData);
            this.uiHandler += new UiHandler(this.ShowData);

            base.EditEvent += new ClickHandler(UCFinanceBPara_EditEvent);
            base.SaveEvent += new ClickHandler(UCFinanceBPara_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);
        }

        #region --窗体加载
        private void UCFinanceBPara_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = true;

            CommonFuncCall.BindComBoxDataSource(cbocurrency, "sys_currency", "请选择");

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
        }
        #endregion

        #region --加载数据

        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _ShowData(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询财务业务参数", GlobalStaticObj.CommAccCode, 
                "sys_financial_ser_param", "*", "book_id='" + GlobalStaticObj.CurrAccID + "'", "", "");
            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowData(object obj)
        {
            DataTable dt = obj as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                strFinancial_ser_param_id = dt.Rows[0]["financial_ser_param_id"].ToString();

                if (dt.Rows[0]["tax_rate"].ToString().Length > 0)
                {
                    decimal tax_rate = decimal.Parse(dt.Rows[0]["tax_rate"].ToString());
                    tax_rate = 100 * tax_rate;
                    txttax_rate.Caption = tax_rate.ToString();
                }
                cbocurrency.SelectedValue = dt.Rows[0]["currency"].ToString();
                nudcounts.Value = Convert.ToDecimal(dt.Rows[0]["counts"].ToString());
                chkcounts_zero.Checked = dt.Rows[0]["counts_zero"].ToString() == "1" ? true : false;
                nudprice.Value = Convert.ToDecimal(dt.Rows[0]["price"].ToString());
                chkprice_zero.Checked = dt.Rows[0]["price_zero"].ToString() == "1" ? true : false;
            }
        }
        #endregion

        #region 编辑
        void UCFinanceBPara_EditEvent(object sender, EventArgs e)
        {
            txttax_rate.ReadOnly = false;
            nudcounts.ReadOnly = false;
            nudprice.ReadOnly = false;

            cbocurrency.Enabled = true;
            chkcounts_zero.Enabled = true;
            chkprice_zero.Enabled = true;
        }
        #endregion

        /// <summary> 取消
        /// </summary>
        void UC_CancelEvent(object sender, EventArgs e)
        {
            if (cbocurrency.Enabled)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._ShowData));
            }
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCFinanceBPara_SaveEvent(object sender, EventArgs e)
        {
            if (txttax_rate.ReadOnly)
            {
                return;
            }

            errorProvider1.Clear();
            string strTax_rate = txttax_rate.Caption.Trim();

            string pattern = @"\d+(\.\d+)?";  //正则表达式   换成百分比的正则
            if (!Regex.IsMatch(strTax_rate, pattern))
            {
                Utility.Common.Validator.SetError(errorProvider1, txttax_rate, "税率格式不正确!");
                return;
            }
            decimal dec = 0;
            if (decimal.TryParse(strTax_rate, out dec))
            {
                dec = dec / 100;
            }
            else
            {
                Utility.Common.Validator.SetError(errorProvider1, txttax_rate, "税率格式不正确!");
                return;
            }

            if (cbocurrency.SelectedValue == null
                && cbocurrency.SelectedValue.ToString().Length > 0)
            {
                Utility.Common.Validator.SetError(errorProvider1, cbocurrency, "请选择本位币!");
                return;
            }
            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "新增财务业务参数";
            Dictionary<string, string> Fileds = new Dictionary<string, string>();
            Fileds.Add("tax_rate", dec.ToString());
            Fileds.Add("currency", cbocurrency.SelectedValue.ToString());

            Fileds.Add("counts", nudcounts.Value.ToString());
            Fileds.Add("counts_zero", chkcounts_zero.Checked == true ? "1" : "0");
            Fileds.Add("price", nudprice.Value.ToString());
            Fileds.Add("price_zero", chkprice_zero.Checked == true ? "1" : "0");

            string nowUtcTicks = Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString();
            Fileds.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);
            Fileds.Add("update_time", nowUtcTicks);            

            if (strFinancial_ser_param_id.Length == 0)
            {
                Fileds.Add("book_id", GlobalStaticObj.CurrAccID);
                Fileds.Add("financial_ser_param_id", Guid.NewGuid().ToString());//新ID                   
                Fileds.Add("create_by", HXCPcClient.GlobalStaticObj.UserID);
                Fileds.Add("create_time", nowUtcTicks);
            }
            else
            {
                keyName = "financial_ser_param_id";
                keyValue = strFinancial_ser_param_id;
                opName = "更新财务业务参数";
            }
            bool bln = DBHelper.Submit_AddOrEdit(opName, GlobalStaticObj.CommAccCode, "sys_financial_ser_param", keyName, keyValue, Fileds);
            if (bln)
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                txttax_rate.ReadOnly = true;                
                nudcounts.ReadOnly = true;
                nudprice.ReadOnly = true;
                cbocurrency.Enabled = false;
                chkcounts_zero.Enabled = false;
                chkprice_zero.Enabled = false;
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion      
     
    }
}
