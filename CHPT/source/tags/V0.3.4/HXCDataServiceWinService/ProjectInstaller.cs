using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using HXC_FuncUtility;


namespace HXCDataServiceWinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.HXCDataService.ServiceName = GlobalStaticObj_Server.DataServiceName;
        }

        private void HXCDataService_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
