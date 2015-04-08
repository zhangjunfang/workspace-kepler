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

namespace HXCFileServiceWinService
{
    public partial class HXCFileService : ServiceBase
    {
        ServiceHost HostFile = null;
        public HXCFileService()
        {
            InitializeComponent();
            this.ServiceName = GlobalStaticObj_Server.FileServiceName;
            this.CanStop = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            if (HostFile == null)
            {
                HostFile = new ServiceHost(typeof(HuiXiuCheWcfFileService.HXCWCFFileService));
            }
            //启动   
            if (HostFile.State != CommunicationState.Opened)
            {
                HostFile.Open();
                //Utility.Log.Log.writeServiceLog("文件服务启动");
                //SetTextBoxValue("停止服务", this.btnStart);
            }
        }

        protected override void OnStop()
        {
            if (HostFile != null)
            {
                if (HostFile.State == CommunicationState.Opened)
                {
                    HostFile.Close();
                    //Utility.Log.Log.writeServiceLog("文件服务停止");
                    HostFile = null;
                }
            }
            DBUtility.DBUtilityDispose.Dispose();
            HXC_FuncUtility.HXC_FuncUtilityDispose.Dispose();
            HXCLog.HXCLogDispose.Dispose();
            HXCSession.PCClientSession.Instance.Dispose();
        }
    }
}
