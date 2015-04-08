using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;

namespace HXCPcClient.UCForm.FinancialManagement.Receivable
{
    public class UCPayment:UCReceivableManage
    {
        public UCPayment()
            : base(DataSources.EnumOrderType.PAYMENT)
        { }
    }
}
