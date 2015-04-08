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
using System.IO;
using HXC_FuncUtility;
namespace HXCDataServiceWinService
{
    public partial class HXCDataService : ServiceBase
    {
        ServiceHost Host = null;       
        public HXCDataService()
        {
            InitializeComponent();
            this.ServiceName = GlobalStaticObj_Server.DataServiceName;
            this.CanStop = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            //if (!File.Exists("c:\\srvlog.txt"))
            //{
            //    StreamWriter sr = File.CreateText("c:\\bbbirdlog.txt");
            //    sr.WriteLine("-------------------------START SRV---------------------");
            //    sr.WriteLine("数据服务启动");
            //    sr.Close();
            //}
            //else
            //{
            //    StreamWriter sr = File.AppendText("c:\\bbbirdlog.txt");
            //    sr.WriteLine("-------------------------START SRV---------------------");
            //    sr.WriteLine("数据服务启动");
            //    sr.Close();
            //}
            if (Host == null)
            {
                Host = new ServiceHost(typeof(HuiXiuCheWcfService.HXCWCFService));
                ////绑定   
                //System.ServiceModel.Channels.Binding netTcp = new NetTcpBinding();
                ////终结点   
                //Host.AddServiceEndpoint(typeof(HuiXiuCheWcfContract.IHXCWCFService), netTcp, "net.tcp://127.0.0.1:9999/HXCWCFService");
                //if (Host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
                //{
                //    //行为   
                //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                //    behavior.HttpGetEnabled = true;
                //    //元数据地址   
                //    behavior.HttpGetUrl = new Uri("http://127.0.0.1:10000/HXCWCFService");
                //    Host.Description.Behaviors.Add(behavior);
                //}
            }
            //启动   
            if (Host.State != CommunicationState.Opened)
            {
                Host.Open();
                //Utility.Log.Log.writeServiceLog("数据服务启动");
                //SetTextBoxValue("停止服务", this.btnStart);
               
            }           
        }

        protected override void OnStop()
        {
            //if (!File.Exists("c:\\srvlog.txt"))
            //{
            //    StreamWriter sr = File.CreateText("c:\\bbbirdlog.txt");
            //    sr.WriteLine("-------------------------START SRV---------------------");
            //    sr.WriteLine("数据服务停止");
            //    sr.Close();
            //}
            //else
            //{
            //    StreamWriter sr = File.AppendText("c:\\bbbirdlog.txt");
            //    sr.WriteLine("-------------------------START SRV---------------------");
            //    sr.WriteLine("数据服务停止");
            //    sr.Close();
            //}
            if (Host != null)
            {
                if (Host.State == CommunicationState.Opened)
                {
                    Host.Close();
                    //Utility.Log.Log.writeServiceLog("数据服务停止");                   
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
