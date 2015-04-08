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
        public Dictionary<string, Dictionary<string, string>> dicLoginInfos = new Dictionary<string, Dictionary<string, string>>();

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
        public string LoginIn(string accCode, string userID)
        {
            string loginCookieStr = GetLoginCookieStr(userID);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //判断是否包含该账套，如没有则添加
            if (dicLoginInfos.ContainsKey(accCode))
            {
                dic = dicLoginInfos[accCode];
            }
            else
            {
                dicLoginInfos.Add(accCode, dic);
            }
            //断该账套对应的在线用户是否包含登陆id，如无则将登陆id及cookie信息加入到缓存中
            if (dic.ContainsKey(userID))
            {
                dic[userID] = loginCookieStr;
            }
            else
            {
                dic.Add(userID, loginCookieStr);
            }
            return loginCookieStr;
        }


        /// <summary> 登出
        /// </summary>
        /// <param name="userID">登陆id</param>
        /// <returns>登陆cookie串</returns>
        public void LoginOut(string accCode, string userID)
        {
            return;
            //判断是否包含该账套
            if (dicLoginInfos.ContainsKey(accCode))
            {
                //判断该账套对应的在线用户是否包含登陆id
                if (dicLoginInfos[accCode].ContainsKey(userID))
                {
                    dicLoginInfos[accCode].Remove(userID);
                }
            }
        }

        /// <summary> 验证用户是否合法
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="cookieStr">cookie串</param>
        /// <returns>返回是否合法</returns>
        public string ValidaiteUser(string accCode, string userID, string cookieStr)
        {
            return "";
            if (!dicLoginInfos.ContainsKey(accCode))
            {
                return "掉线";
            }
            if (!dicLoginInfos[accCode].ContainsKey(userID))
            {
                return "掉线";
            }
            string loginCookieStr = dicLoginInfos[accCode][userID];
            bool flag = string.Equals(cookieStr, loginCookieStr);
            if (!flag)
            {
                return "验证失败，您已在其它地方登录";
            }
        }
        /// <summary> 踢出用户
        /// </summary>
        /// <param name="accCode">账套编码</param>
        /// <param name="userID">用户id</param>
        /// <returns>返回true or false</returns>
        public void ShotOffUser(string accCode, string userID)
        {
            if (dicLoginInfos.ContainsKey(accCode))
            {
                if (dicLoginInfos[accCode].ContainsKey(userID))
                {
                    dicLoginInfos[accCode].Remove(userID);
                }
            }
        }

        /// <summary> 获取账套在线用户数量
        /// </summary>
        /// <returns>返回在线用户数</returns>
        public int GetClientAccUserCount(string accCode)
        {
            int count = 0;
            if (dicLoginInfos.ContainsKey(accCode))
            {
                count = dicLoginInfos[accCode].Count;
            }
            return count;
        }

        /// <summary> 获取在线用户数量
        /// </summary>
        /// <returns>返回在线用户数</returns>
        public int GetClientUserCount()
        {
            int count = 0;
            foreach (string str in dicLoginInfos.Keys)
            {
                count += dicLoginInfos[str].Count;
            }
            return count;
        }
    }
}
