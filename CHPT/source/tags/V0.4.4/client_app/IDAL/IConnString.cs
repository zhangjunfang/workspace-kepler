using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IConnString
    {
        string connManageWrite { get; set; }
        string connWrite { get; set; }
        string connReadonly { get; set; }
        string ConStrManageSql { get; set; }
    }
}
