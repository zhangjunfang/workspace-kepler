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
namespace HXCServerWinForm
{
    public class WCFClientProxy
    {
        public static void CreatePCClientProxy()
        {
            GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>("HuiXiuCheWcfService");
            GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
            GlobalStaticObj.channelFactorySession = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
            GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactorySession.CreateChannel();
        }
       

        public static bool TestPCClientProxy()
        {
            try
            {
                if (GlobalStaticObj.channelFactory == null)
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>("HuiXiuCheWcfService");
                }             
                if (GlobalStaticObj.channelFactory.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                }
                if (GlobalStaticObj.channelFactorySession == null)
                {
                    GlobalStaticObj.channelFactorySession = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
                }
                if (GlobalStaticObj.channelFactorySession.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactorySession.CreateChannel();
                }         
               string Str = GlobalStaticObj.proxy.TestConnect();
               string Str0 = GlobalStaticObj.sessionProxy.TestConnect();
               if (Str == "1" && Str0=="1")
               {
                   return true;
               }
            }
            catch (Exception ex)
            {
                try
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>("HuiXiuCheWcfService");
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                    GlobalStaticObj.channelFactorySession = new ChannelFactory<HuiXiuCheWcfSessionContract.IHXCWCFSessionService>("HuiXiuCheWcfSessionService");
                    GlobalStaticObj.sessionProxy = GlobalStaticObj.channelFactorySession.CreateChannel();
                    string Str = GlobalStaticObj.proxy.TestConnect();
                    string Str0 = GlobalStaticObj.sessionProxy.TestConnect();
                    if (Str == "1" && Str0 == "1")
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
