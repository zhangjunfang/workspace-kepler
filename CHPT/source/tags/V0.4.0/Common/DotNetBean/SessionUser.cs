using System;
using System.Collections.Generic;
using System.Text;

namespace HXCCommon.DotNetBean
{
    /// <summary>
    /// 存 Session对象
    /// </summary>
    public class SessionUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public object UserId { get; set; }
        /// <summary>
        /// 登陆账户
        /// </summary>
        public object UserAccount { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public object UserPwd { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public object UserName { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public object DistrictID { get; set; }
        /// <summary>
        /// 行政区划名称
        /// </summary>
        public object DistrictName { get; set; }
        /// <summary>
        /// 行政区划级别
        /// </summary>
        public object DistrictLevel { get; set; }

        public SessionUser(object userId, object userAccount, object userPwd, object userName, object districtID, object districtName, object districtLevel)
        {
            this.UserId = userId;
            this.UserAccount = userAccount;
            this.UserName = userName;
            this.UserPwd = userPwd;
            this.DistrictID = districtID;
            this.DistrictName = districtName;
            this.DistrictLevel = districtLevel;
        }
        public SessionUser()
        {
        }
    }
}
