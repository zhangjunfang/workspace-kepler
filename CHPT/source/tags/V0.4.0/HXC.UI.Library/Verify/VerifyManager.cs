using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HXC.UI.Library.Verify
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
        private static readonly Dictionary<String, Type> KeyTypeDic = new Dictionary<String, Type>();
        private static readonly Dictionary<String, String> VerifyMessageDic = new Dictionary<String, String>();
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        public static IValueVerifyType GetVerifyType(IValueVerify valueVerify)
        {
            try
            {
                if (valueVerify == null || (String.IsNullOrEmpty(valueVerify.VerifyTypeName) && valueVerify.VerifyType == null)) return null;
                var type = valueVerify.VerifyType ?? KeyTypeDic[valueVerify.VerifyTypeName.ToUpper()];
                if (type == null)
                {
                    return null;
                }
                return Activator.CreateInstance(type) as IValueVerifyType;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String GetVerifyMessage(IValueVerify valueVerify)
        {
            try
            {
                if (valueVerify == null || (String.IsNullOrEmpty(valueVerify.VerifyTypeName) && valueVerify.VerifyType == null)) return null;
                if (valueVerify.VerifyType == null)
                {
                    if (VerifyMessageDic.ContainsKey(valueVerify.VerifyTypeName.ToUpper()))
                    {
                        return VerifyMessageDic[valueVerify.VerifyTypeName.ToUpper()];
                    }
                }
                if (!String.IsNullOrEmpty(valueVerify.VerifyTypeName)) return null;
                return KeyTypeDic.ContainsValue(valueVerify.VerifyType) ? (from type in KeyTypeDic where type.Value == valueVerify.VerifyType where VerifyMessageDic.ContainsKey(type.Key) select VerifyMessageDic[type.Key.ToUpper()]).FirstOrDefault() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

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
                if (KeyTypeDic.ContainsKey(key.ToUpper()))
                {
                    throw new Exception("不允许重复注册的KEY");
                }
                KeyTypeDic.Add(key.ToUpper(), type);
                var valueValidation = Activator.CreateInstance(type) as IValueVerifyType;
                if (valueValidation != null) VerifyMessageDic.Add(key.ToUpper(), valueValidation.VerifyMessage);
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
            if (control == null || (String.IsNullOrEmpty(control.VerifyTypeName) && control.VerifyType == null)) return true;
            var type = control.VerifyType ?? KeyTypeDic[control.VerifyTypeName.ToUpper()];
            if (type == null)
            {
                return false;
            }
            var valueValidation = Activator.CreateInstance(type) as IValueVerifyType;
            return valueValidation != null && valueValidation.Verify(control);
        }
        /// <summary>
        /// 设置扩展控件ExtContent属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        public static Boolean InputtingVerify(IContentControl control)
        {
            if (control == null || (String.IsNullOrEmpty(control.VerifyTypeName) && control.VerifyType == null)) return true;
            var type = control.VerifyType ?? KeyTypeDic[control.VerifyTypeName.ToUpper()];
            if (type == null)
            {
                return false;
            }
            var valueValidation = Activator.CreateInstance(type) as IValueVerifyType;
            return valueValidation != null && valueValidation.InputtingVerify(control);
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
        private string _verityMessage = "输入内容格式不正确";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(control.VerifyCondition);
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(control.InputtingVerifyCondition);
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 手机正则表达式类型验证
    /// </summary>
    public class Regex4PhoneVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的手机格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }
        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{11}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{0,11}$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 电话正则表达式类型验证
    /// </summary>
    public class Regex4TelephoneVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的电话格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{0,4}-?\d{7,8}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^[0-9\-]{0,13}$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 邮编正则表达式类型验证
    /// </summary>
    public class Regex4PostcodeVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的邮编格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^[1-9]\d{5}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{0,6}$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 身份证正则表达式类型验证
    /// </summary>
    public class Regex4IDVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的身份证格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d+(x|X)?$");
            return reg.IsMatch(control.Value.ToString());
        }
    }
    
    /// <summary>
    /// IP地址正则表达式类型验证
    /// </summary>
    public class Regex4IPAddressVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的IP地址格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^((^\d{1,3}\.?$)|(^\d{1,3}\.\d{1,3}\.?$)|(^\d{1,3}\.\d{1,3}\.\d{1,3}\.?$)|(^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$))$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 网址正则表达式类型验证
    /// </summary>
    public class Regex4WebSiteVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的网址格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^((https?|ftp|news):\/\/)?([a-z]([a-z0-9\-]*[\.。])+([a-z]{2}|aero|arpa|biz|com|coop|edu|gov|info|int|jobs|mil|museum|name|nato|net|org|pro|travel)|(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))(\/[a-z0-9_\-\.~]+)*(\/([a-z0-9_\-\.]*)(\?[a-z0-9+_\-\.%=&]*)?)?(#[a-z][a-z0-9_]*)?$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            return true;
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d+(\.\d+)?$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 邮件正则表达式类型验证
    /// </summary>
    public class Regex4EmailVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "内容不符合正确的邮件格式";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^(?=[0-9a-zA-Z@_.]+$)");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 整数正则表达式类型验证
    /// </summary>
    public class Regex4IntegerVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "输入的内容必须为0到9之间的一个或多个(最多32个)数字";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^-?[1-9]\d{0,32}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^\d{0,32}$");
            return reg.IsMatch(control.Value.ToString());
        }
    }

    /// <summary>
    /// 正整数和正浮点数正则表达式类型验证
    /// </summary>
    public class Regex4UFloatVerifyKey : IValueVerifyType
    {
        private string _verityMessage = "输入的内容必须为正整数或小数";
        public string VerifyMessage
        {
            get { return _verityMessage; }
            set { _verityMessage = value; }
        }

        public bool Verify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^[0-9]+([.]{1}[0-9]+){0,1}$");
            return reg.IsMatch(control.Value.ToString());
        }

        public bool InputtingVerify(IContentControl control)
        {
            if (control.Value == null || String.IsNullOrEmpty(control.Value.ToString())) return true;
            var reg = new Regex(@"^[0-9]{0}([0-9]|[.])+$");
            return reg.IsMatch(control.Value.ToString());
        }
    }
    #endregion
}
