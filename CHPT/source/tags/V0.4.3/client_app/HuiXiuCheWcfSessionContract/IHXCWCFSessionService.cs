using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace HuiXiuCheWcfSessionContract
{
    [ServiceContract(Name = "HXCWCFSessionService")]
    public interface IHXCWCFSessionService
    {
        [OperationContract]
        string TestConnect();
        [OperationContract]
        string generatePCClientCookieStr(string userID);
        [OperationContract]
        void removePCClientCookieStr(string userID);
        [OperationContract]
        bool getPCClientCookieStr(string userID, out string cookieStr);
        [OperationContract]
        bool CheckPCClientCookieStr(string userID, string cookieStr);
        [OperationContract]
        string GetOnLineUserIDs();
    }
}
