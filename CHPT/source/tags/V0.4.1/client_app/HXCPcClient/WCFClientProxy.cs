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
namespace HXCPcClient
{
    public class WCFClientProxy
    {
        public static void CreatePCClientProxy()
        {
            GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>(ConfigConst.WcfData);
            GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
            GlobalStaticObj.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService>(ConfigConst.WcfFile);
            GlobalStaticObj.proxyFile = GlobalStaticObj.channelFactoryFile.CreateChannel();          
        }
 
        /// <summary>
        /// 全面测试WCF服务连通情况
        /// </summary>
        /// <returns>全通：True;存在不通：False;</returns>
        public static bool TestPCClientProxy()
        {
            try
            {
                if (GlobalStaticObj.channelFactory == null)
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>(ConfigConst.WcfData);
                }
                if (GlobalStaticObj.channelFactoryFile == null)
                {
                    GlobalStaticObj.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService>(ConfigConst.WcfFile);
                }
                //if (GlobalStaticObj.channelFactoryFileTransfer == null)
                //{
                //    GlobalStaticObj.channelFactoryFileTransfer = new ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService>(ConfigConst.WcfFileTransfer);
                //}
                if (GlobalStaticObj.channelFactory.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                }
                if (GlobalStaticObj.channelFactoryFile.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.proxyFile = GlobalStaticObj.channelFactoryFile.CreateChannel();
                }
                //if (GlobalStaticObj.channelFactoryFileTransfer.State != CommunicationState.Opened)
                //{
                //    GlobalStaticObj.proxyFileTransfer = GlobalStaticObj.channelFactoryFileTransfer.CreateChannel();
                //}     
               string str = GlobalStaticObj.proxy.TestConnect();
               string strF = string.Empty;
               //string strFileTransfer = GlobalStaticObj.proxyFileTransfer.TestConnect();
               byte[] myBuffer = new byte[1];
               Stream sourceStream = GlobalStaticObj.proxyFile.TestConnect();
               if (sourceStream != null)
               {
                   if (sourceStream.CanRead)
                   {
                       sourceStream.Read(myBuffer, 0, 1);
                   }
               }
               strF = Encoding.Default.GetString(myBuffer);
               sourceStream.Close();
               sourceStream.Dispose();
               if (str == "1"&& strF == "1")
               {
                   return true;
               }
            }
            catch
            {
                try
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>(ConfigConst.WcfData);
                    GlobalStaticObj.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService>(ConfigConst.WcfFile);
                    //GlobalStaticObj.channelFactoryFileTransfer = new ChannelFactory<HuiXiuCheWcfFileTransferContract.IHXCWCFFileTransferService>(ConfigConst.WcfFileTransfer);
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                    GlobalStaticObj.proxyFile = GlobalStaticObj.channelFactoryFile.CreateChannel();
                    //GlobalStaticObj.proxyFileTransfer = GlobalStaticObj.channelFactoryFileTransfer.CreateChannel();
                    string str = GlobalStaticObj.proxy.TestConnect();
                    string strF = string.Empty;
                    //string strFileTransfer = GlobalStaticObj.proxyFileTransfer.TestConnect();
                    byte[] myBuffer = new byte[1];
                    Stream sourceStream = GlobalStaticObj.proxyFile.TestConnect();
                    if (sourceStream != null)
                    {
                        if (sourceStream.CanRead)
                        {
                            sourceStream.Read(myBuffer, 0, 1);
                        }
                    }
                    strF = Encoding.Default.GetString(myBuffer);
                    sourceStream.Close();
                    sourceStream.Dispose();
                    if (str == "1" && strF == "1")
                    {
                        return true;
                    }
                }
                catch (Exception ex1)
                {
                    return false;
                    throw ex1;
                }
            }
            return true;
        }

        #region WCF服务连通情况通讯测试
        /// <summary> 测试数据传输通讯
        /// </summary>
        /// <returns></returns>
        public static bool TestDataProxy()
        { 
            try
            {
                if (GlobalStaticObj.channelFactory == null)
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>(ConfigConst.WcfData);
                }         
                
                if (GlobalStaticObj.channelFactory.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                }
                string test = GlobalStaticObj.proxy.TestConnect();
                if (test == "1")
                {
                    return true;
                }
                throw new Exception("数据服务测试失败！");
            }
            catch(Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog(ex);
                GlobalStaticObj.GlobalLogService.WriteLog("尝试重新连接数据服务");
                try
                {
                    GlobalStaticObj.channelFactory = new ChannelFactory<HuiXiuCheWcfContract.IHXCWCFService>(ConfigConst.WcfData);
                    GlobalStaticObj.proxy = GlobalStaticObj.channelFactory.CreateChannel();
                    string test = GlobalStaticObj.proxy.TestConnect();
                    if (test == "1")
                    {
                        return true;
                    }
                }
                catch (Exception ex1)
                {
                    GlobalStaticObj.GlobalLogService.WriteLog(ex1);
                    return false;
                }             
            }
            return false;
        }
        /// <summary> 测试文件传输通讯(流传输模式)
        /// </summary>
        /// <returns></returns>
        public static bool TestFileProxy()
        {           
            try
            {
                if (GlobalStaticObj.channelFactoryFile == null)
                {
                    GlobalStaticObj.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService>(ConfigConst.WcfFile);
                }
                if (GlobalStaticObj.channelFactoryFile.State != CommunicationState.Opened)
                {
                    GlobalStaticObj.proxyFile = GlobalStaticObj.channelFactoryFile.CreateChannel();
                }
                byte[] myBuffer = new byte[1];
                Stream sourceStream = GlobalStaticObj.proxyFile.TestConnect();
                if (sourceStream != null)
                {
                    if (sourceStream.CanRead)
                    {
                        sourceStream.Read(myBuffer, 0, 1);
                    }
                }
                sourceStream.Close();
                sourceStream.Dispose();
                string test = Encoding.Default.GetString(myBuffer);
                if (test == "1")
                {
                    return true;
                }
                throw new Exception("文件服务测试失败！");
            }
            catch (Exception ex)
            {
                GlobalStaticObj.GlobalLogService.WriteLog(ex);
                GlobalStaticObj.GlobalLogService.WriteLog("尝试重新连接文件服务");
                try
                {                   
                        GlobalStaticObj.channelFactoryFile = new ChannelFactory<HuiXiuCheWcfFileContract.IHXCWCFFileService>(ConfigConst.WcfFile);                   
                        GlobalStaticObj.proxyFile = GlobalStaticObj.channelFactoryFile.CreateChannel();                   
                    byte[] myBuffer = new byte[1];
                    Stream sourceStream = GlobalStaticObj.proxyFile.TestConnect();
                    if (sourceStream != null)
                    {
                        if (sourceStream.CanRead)
                        {
                            sourceStream.Read(myBuffer, 0, 1);
                        }
                    }
                    sourceStream.Close();
                    sourceStream.Dispose();
                    string test = Encoding.Default.GetString(myBuffer);
                    if (test == "1")
                    {
                        return true;
                    }
                }
                catch (Exception ex1)
                {
                    return false;
                    throw ex1;
                }
            }
            return false;
        }
        #endregion
    }
}
