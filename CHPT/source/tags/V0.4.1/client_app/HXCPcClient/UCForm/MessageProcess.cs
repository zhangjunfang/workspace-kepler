using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCPcClient.UCForm
{
    public class MessageProcessor
    {
        /// <summary>
        /// 公司组织更新时触发的事件
        /// </summary>
        public static event EventHandler ComOrginfoUpdate;


        /// <summary>
        /// 公司组织更新的通知
        /// </summary>
        public static void UpdateComOrgInfo()
        {
            if (ComOrginfoUpdate != null)
            {
                ComOrginfoUpdate.Invoke(null, null);
            }
        }



    }
}
