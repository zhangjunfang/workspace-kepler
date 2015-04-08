using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    /// <summary>
    /// 值验证管理器
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/23 13:58:48</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class VerifyManager
    {
        #region Constructor -- 构造函数
        static VerifyManager()
        {
            RegistType("Regex", typeof(RegexVerifyKey));    //正则表达式
            RegistType("Phone", typeof (Regex4PhoneVerifyKey)); //手机
            RegistType("Telephone", typeof(Regex4TelephoneVerifyKey));  //电话,传真
            RegistType("Postcode", typeof(Regex4PostcodeVerifyKey));    //邮编
            RegistType("IDCard", typeof(Regex4IDVerifyKey));    //身份证
            RegistType("IPAddress", typeof(Regex4IPAddressVerifyKey));  //IP地址
            RegistType("WebSite", typeof(Regex4WebSiteVerifyKey));  //网址
            RegistType("Email", typeof(Regex4EmailVerifyKey));  //邮件
            RegistType("Integer", typeof(Regex4IntegerVerifyKey));  //整数
            RegistType("UFloat", typeof(Regex4UFloatVerifyKey));  //浮点数
        }
        #endregion

        #region Field -- 字段
        private static readonly Dictionary<String, Type> KeyTypeList = new Dictionary<string, Type>();
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 注册资源索引键类型
        /// </summary>
        /// <param name="key">索引键</param>
        /// <param name="type">索引键类型</param>
        /// <returns>是否注册成功</returns>
        public static Boolean RegistType(String key, Type type)
        {
            try
            {
                if (KeyTypeList.ContainsKey(key.ToUpper()))
                {
                    throw new Exception("不允许重复注册的KEY");
                }
                KeyTypeList.Add(key.ToUpper(), type);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 设置扩展控件ExtContent属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        public static Boolean Verify(IContentControl control)
        {
            if (control == null || String.IsNullOrEmpty(control.VerifyType)) return true;
            var type = KeyTypeList[control.VerifyType.ToUpper()];
            if (type == null)
            {
                return false;
            }
            var valueValidation = Activator.CreateInstance(type) as IValueVerifyType;
            return valueValidation != null && valueValidation.Verify(control);
        }
        #endregion

        #region Event -- 事件

        #endregion
    }

    #region 正则表达式验证
    /// <summary>
    /// 正则表达式类型验证
    /// </summary>
    public class RegexVerifyKey:IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(control.VerifyCondition);
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 手机正则表达式类型验证
    /// </summary>
    public class Regex4PhoneVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\d{11}");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 电话正则表达式类型验证
    /// </summary>
    public class Regex4TelephoneVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\d{3}-\d{8}|\d{4}-\d{7}");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 邮编正则表达式类型验证
    /// </summary>
    public class Regex4PostcodeVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"[1-9]\d{5}(?!\d)");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 身份证正则表达式类型验证
    /// </summary>
    public class Regex4IDVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\d{15}|\d{18}");
            return reg.IsMatch(control.Value.ToString());
        }
    }
    
    /// <summary>
    /// IP地址正则表达式类型验证
    /// </summary>
    public class Regex4IPAddressVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\d+\.\d+\.\d+\.\d+");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 网址正则表达式类型验证
    /// </summary>
    public class Regex4WebSiteVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"[a-zA-z]+:\\[^\s]*");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 邮件正则表达式类型验证
    /// </summary>
    public class Regex4EmailVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 整数正则表达式类型验证
    /// </summary>
    public class Regex4IntegerVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^-?[1-9]\d*$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 正整数和正浮点数正则表达式类型验证
    /// </summary>
    public class Regex4UFloatVerifyKey : IValueVerifyType
    {
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^[0-9]+([.]{1}[0-9]+){0,1}$");
            return reg.IsMatch(control.Value.ToString());
        }
    }
    #endregion
}
