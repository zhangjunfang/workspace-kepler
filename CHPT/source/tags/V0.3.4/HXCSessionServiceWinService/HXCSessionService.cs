using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using HXC_FuncUtility;
namespace HXCSessionServiceWinService
{
    public partial class HXCSessionService : ServiceBase
    {
        ServiceHost Host = null;
        public HXCSessionService()
        {
            InitializeComponent();  
            this.ServiceName = GlobalStaticObj_Server.SessionServiceName;
            this.CanStop = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            if (Host == null)
            {   
                
                Host = new ServiceHost(typeof(HuiXiuCheWcfSessionService.HXCWCFSessionService));              
            }
            //启动   
            if (Host.State != CommunicationState.Opened)
            {
                Host.Open();
                //Utility.Log.Log.writeServiceLog("会话服务启动");
            }           
        }

        protected override void OnStop()
        {
            if (Host != null)
            {
                if (Host.State == CommunicationState.Opened)
                {
                    Host.Close();
                    //Utility.Log.Log.writeServiceLog("会话服务停止");
                    Host = null;
                }
            }
            DBUtility.DBUtilityDispose.Dispose();
            HXC_FuncUtility.HXC_FuncUtilityDispose.Dispose();
            HXCLog.HXCLogDispose.Dispose();
            HXCSession.PCClientSession.Instance.Dispose();
        }
    }
}
