using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCPcClient.UCForm.FinancialManagement.Receivable
{
    public class UCPaymentSearch : UCReceivableManage
    {
        public UCPaymentSearch():base(SYSModel.DataSources.EnumOrderType.PAYMENT)
        {
            base.isSearch = true;
        }
    }
}
