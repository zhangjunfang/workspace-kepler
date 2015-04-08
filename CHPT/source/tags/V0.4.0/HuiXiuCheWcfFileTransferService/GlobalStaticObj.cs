using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using System.Drawing;
namespace HuiXiuCheWcfFileTransferService
{
    public static class GlobalStaticObj
    {
        public static ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService> channelFactory { get; set; }
        public static HuiXiuCheWcfSessionContract.IHXCWCFSessionService sessionProxy { get; set; }

    }
}
