using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.UCForm.BusinessAnalysis.BankReport
{
    public partial class UCBankSummariz : UCReport
    {
        public UCBankSummariz()
            : base("v_bank_summariz", "现金银行汇总表")
        {
            InitializeComponent();
        }

        private void UCBankSummariz_Load(object sender, EventArgs e)
        {
            //公司
            CommonFuncCall.BindCompany(cboCompany, "全部");
            //绑定结算账户
            CommonFuncCall.BindAccount(cboAccount, "全部");
            BindData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {

        }
    }
}
