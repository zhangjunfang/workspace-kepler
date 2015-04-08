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

namespace HXCFileTransferServiceWinService
{
    public partial class HXCFileTransferService : ServiceBase
    {
        ServiceHost HostFile = null;
        public HXCFileTransferService()
        {
            InitializeComponent();
            this.ServiceName =GlobalStaticObj_Server.FileTransferServiceName;
            this.CanStop = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            if (HostFile == null)
            {
                HostFile = new ServiceHost(typeof(HuiXiuCheWcfFileTransferService.HXCWCFFileTransferService));
            }
            //启动   
            if (HostFile.State != CommunicationState.Opened)
            {
                HostFile.Open();
                //Utility.Log.Log.writeServiceLog("大文件服务启动");
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
                    //Utility.Log.Log.writeServiceLog("大文件服务停止");
                    HostFile = null;
                }
            }
            HuiXiuCheWcfFileTransferService.HuiXiuCheWcfFileTransferServiceDispose.Dispose();
        }
    }
}
