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
using HuiXiuCheWcfFileTransferContract;
namespace HXCFileTransferCache_Client
{
   public class WCFClientProxy
    {
        public static void CreatePCClientProxy()
        {
            GlobalStaticObj.Instance.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService>("HuiXiuCheWcfFileTransferService");
            GlobalStaticObj.Instance.proxyFile = GlobalStaticObj.Instance.channelFactoryFile.CreateChannel();
        }
        public static bool TestPCClientProxy()
        {
            try
            {
                if (GlobalStaticObj.Instance.channelFactoryFile == null)
                {
                    GlobalStaticObj.Instance.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService>("HuiXiuCheWcfFileTransferService");
                }
                if (GlobalStaticObj.Instance.channelFactoryFile.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.Instance.proxyFile = GlobalStaticObj.Instance.channelFactoryFile.CreateChannel();
                }
                string Str = GlobalStaticObj.Instance.proxyFile.TestConnect();              
                if (Str == "1")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (GlobalStaticObj.Instance.channelFactoryFile == null)
                    {
                        GlobalStaticObj.Instance.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService>("HuiXiuCheWcfFileTransferService");
                    }
                    if (GlobalStaticObj.Instance.channelFactoryFile.State != CommunicationState.Opened)
                    {
                        GlobalStaticObj.Instance.proxyFile = GlobalStaticObj.Instance.channelFactoryFile.CreateChannel();
                    }
                    string Str = GlobalStaticObj.Instance.proxyFile.TestConnect();
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
