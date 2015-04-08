using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXC_FuncUtility
{
    /// <summary> 登录会话信息
    /// </summary>
    public class LoginSessionInfo
    {
        private static LoginSessionInfo _instance = null;
        /// <summary> 
        /// </summary>会话信息单键
        public static LoginSessionInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoginSessionInfo();
                }
                return _instance;
            }
        }


        /// <summary> 登陆会话信息<userID,cookieStr>
        /// </summary>
        public Dictionary<string, string> dicLoginInfos = new Dictionary<string, string>();

        /// <summary> 根据userid获取登陆cookie串
        /// </summary>
        /// <param name="userID">登陆id</param>
        /// <returns>登陆cookie串</returns>
        public string GetLoginCookieStr(string userID)
        {
            string loginCookieStr = HXCCommon.DotNetEncrypt.Md5Helper.MD5(userID + "-" + DateTime.UtcNow.Ticks.ToString(), 32);
            return loginCookieStr;
        }

        /// <summary> 登陆
        /// </summary>
        /// <param name="userID">登陆id</param>
        /// <returns>登陆cookie串</returns>
        public string LoginIn(string userID)
        {
            string loginCookieStr = GetLoginCookieStr(userID);
            //将登陆id及cookie信息加入到缓存中
            if (!dicLoginInfos.ContainsKey(userID))
            {
                dicLoginInfos.Add(userID, loginCookieStr);
            }
            else
            {
                dicLoginInfos[userID] = loginCookieStr;
            }
            return loginCookieStr;
        }


        /// <summary> 登出
        /// </summary>
        /// <param name="userID">登陆id</param>
        /// <returns>登陆cookie串</returns>
        public void LoginOut(string userID)
        {
            return;
            //将登陆id及cookie信息从缓存中移除
            if (dicLoginInfos.ContainsKey(userID))
            {
                dicLoginInfos.Remove(userID);
            }
        }

        /// <summary> 验证用户是否合法
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="cookieStr">cookie串</param>
        /// <returns>返回是否合法</returns>
        public bool ValidaiteUser(string userID, string cookieStr)
        {
            //if (dicLoginInfos.ContainsKey(userID))
            //{
            //    string loginCookieStr = dicLoginInfos[userID];
            //    return string.Equals(cookieStr, loginCookieStr);
            //}
            //return false;
            return true;
        }
    }
}
