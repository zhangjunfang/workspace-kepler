using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using HXC_FuncUtility;


namespace HXCSessionServiceWinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.HXCSessionService.ServiceName = GlobalStaticObj_Server.SessionServiceName;
        }
    }
}
