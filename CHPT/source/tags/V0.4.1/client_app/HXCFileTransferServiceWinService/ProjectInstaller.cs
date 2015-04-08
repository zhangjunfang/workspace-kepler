using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using HXC_FuncUtility;


namespace HXCFileTransferServiceWinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.HXCFileTransferService.ServiceName = GlobalStaticObj_Server.FileTransferServiceName;
        }
    }
}
