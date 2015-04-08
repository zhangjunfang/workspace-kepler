using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using HuiXiuCheWcfService;
using HuiXiuCheWcfFileService;
using System.Threading;

namespace WcfServiceProxy
{
    /// <summary>  wcf服务处理类
    /// </summary>
    public class WcfServiceProxy
    {
        /// <summary> 数据服务
        /// </summary>
        public ServiceHost DataWcfService;

        /// <summary> 文件服务
        /// </summary>
        public ServiceHost FileWcfService;

        private static WcfServiceProxy _instance;
        /// <summary> wcf服务实例
        /// </summary>
        public static WcfServiceProxy Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WcfServiceProxy();
                }
                return _instance;
            }
        }


        /// <summary> 检测wcf连接是否正常
        /// </summary>
        /// <returns>返回 true or false</returns>
        public bool CheckWcfFerviceRuning()
        {
            if (DataWcfService.State != CommunicationState.Opened || FileWcfService.State != CommunicationState.Opened)
            {
                return false;
            }
            return true;
        }

        /// <summary> 启动服务
        /// </summary>
        public bool StartWcfService()
        {
            CreateWcfService();
            if (DataWcfService.State != CommunicationState.Opened)
            {
                try
                {
                    DataWcfService.Open();
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【数据服务】" + ex.Message, "wcf服务");
                    DataWcfService = null;
                    return false;
                }
            }
            if (FileWcfService.State != CommunicationState.Opened)
            {
                try
                {
                    FileWcfService.Open();
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【文件服务】" + ex.Message, "wcf服务");
                    FileWcfService = null;
                    return false;
                }
            }
            return true;
        }

        /// <summary> 停用服务
        /// </summary>
        public void StopWcfService()
        {
            if (DataWcfService != null)
            {
                try
                {
                    DataWcfService.Close();
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【数据服务】" + ex.Message, "wcf服务");
                }
                finally
                {
                    DataWcfService = null;
                }
            }
            if (FileWcfService != null)
            {
                try
                {
                    FileWcfService.Close();
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【文件服务】" + ex.Message, "wcf服务");
                }
                finally
                {
                    FileWcfService = null;
                }
            }
        }

        /// <summary> 创建服务
        /// </summary>
        private void CreateWcfService()
        {
            if (DataWcfService == null)
            {
                try
                {
                    DataWcfService = new ServiceHost(typeof(HuiXiuCheWcfService.HXCWCFService));
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【数据服务】" + ex.Message, "wcf服务");
                    DataWcfService = null;
                }
            }
            if (FileWcfService == null)
            {
                try
                {
                    FileWcfService = new ServiceHost(typeof(HuiXiuCheWcfFileService.HXCWCFFileService));
                }
                catch (Exception ex)
                {
                    Utility.Log.Log.writeLineToLog("【文件服务】" + ex.Message, "wcf服务");
                    FileWcfService = null;
                }
            }
        }

    }
}
