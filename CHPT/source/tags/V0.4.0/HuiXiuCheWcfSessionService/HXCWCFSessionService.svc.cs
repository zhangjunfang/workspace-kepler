using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SYSModel;
using HuiXiuCheWcfSessionContract;
using System.IO;
using HXCLog;
using HXCSession;
namespace HuiXiuCheWcfSessionService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class HXCWCFSessionService : IHXCWCFSessionService
    {
        public string TestConnect()
        {
            return "1";
        }
        public string generatePCClientCookieStr(string userID)
        {
            string cookieStr = string.Empty;
            cookieStr = HXCCommon.DotNetEncrypt.Md5Helper.MD5(userID +"-"+DateTime.UtcNow.Ticks, 32);
            HXCSession.PCClientSession.Instance.Add(userID, cookieStr);
            return cookieStr;
        }

        public void removePCClientCookieStr(string userID)
        {
            string cookieStr = string.Empty;
            HXCSession.PCClientSession.Instance.Remove(userID, out cookieStr);
        }

        public bool getPCClientCookieStr(string userID, out string cookieStr)
        {
            return HXCSession.PCClientSession.Instance.Get(userID, out cookieStr);
        }
        public bool CheckPCClientCookieStr(string userID, string cookieStr)
        {
            string str = string.Empty;
            HXCSession.PCClientSession.Instance.Get(userID, out str);
            return string.Equals(cookieStr, str);
        }
        public string GetOnLineUserIDs()
        {
            List<string> myList = HXCSession.PCClientSession.Instance.GetOnLineUserIDList();
            return JsonConvert.SerializeObject(myList);
        }
    }
}
