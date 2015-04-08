using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SYSModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using System.IO;
namespace HuiXiuCheWcfFileTransferService
{
    public class WCFClientProxy
    {
        public static void CreatePCClientProxy()
        {
            GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
            GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactory.CreateChannel();           
        }

        public static bool TestPCClientProxy()
        {
            try
            {
                if (GlobalStaticObj.channelFactory == null)
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
                }               
                if (GlobalStaticObj.channelFactory.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactory.CreateChannel();
                }
               string Str = GlobalStaticObj.sessionProxy.TestConnect();              
               if (Str == "1")
               {
                   return true;
               }
            }
            catch
            {
                try
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
                    GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactory.CreateChannel();
                    string Str = GlobalStaticObj.sessionProxy.TestConnect();                 
                    if (Str == "1")
                    {
                        return true;
                    }
                }
                catch (Exception ex1)
                {
                    return false;
                    throw ex1;                    
                }
                return false;
            }
            return true;
        }
    }
}
