using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYSModel;

namespace HXCPcClient.Chooser
{
    public class frmPartsByYuTong:frmParts
    {
        public override void PartsLoad()
        {
            cboDataSource.SelectedValue = ((int)DataSources.EnumDataSources.YUTONG).ToString();
            cboDataSource.Enabled = false;
        }
    }
}
