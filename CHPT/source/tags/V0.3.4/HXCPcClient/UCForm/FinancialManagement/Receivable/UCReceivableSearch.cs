using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCPcClient.UCForm.FinancialManagement.Receivable
{
    public class UCReceivableSearch:UCReceivableManage
    {
        public UCReceivableSearch()
            : base(SYSModel.DataSources.EnumOrderType.RECEIVABLE)
        {
            base.isSearch = true;
        }
    }
}
