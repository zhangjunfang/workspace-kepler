using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCPcClient.UCForm
{
    public class MessageProcessor
    {
        public static event EventHandler OrginfoUpdate;

        public static void UpdateOrgInfo()
        {
            if (OrginfoUpdate != null)
            {
                OrginfoUpdate.Invoke(null, null);
            }
        }
    }
}
