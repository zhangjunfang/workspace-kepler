using System;
using System.Text.RegularExpressions;

namespace Utility.Common
{
    /// <summary>
    /// 用正则验证一些常用操作
    /// </summary>
    public class RegexCheck
    {
        /// <summary>
        /// 用正则验证Eamil
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            Regex regex = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            if (regex.Match(email).Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检察是否正确的日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            //考虑到了4年一度的366天，还有特殊的2月的日期
            Regex reg = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$");
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 验证是否包含汉语
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHanyu(string str)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            if (regex.Match(str).Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证是否全部汉语
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHanyuAll(string str)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            //匹配的内容长度和被验证的内容长度相同时，验证通过
            if (regex.Match(str).Success)
                if (regex.Matches(str).Count == str.Length)
                    return true;
            //其它，未通过
            return false;
        }

        /// <summary>
        /// 验证用户名：必须以字母开头，可以包含字母、数字、“_”、“.”，至少5个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkUserId(string str)
        {
            Regex regex = new Regex("[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}");
            if (regex.Match(str).Success)
                if (regex.Matches(str)[0].Value.Length == str.Length)
                    return true;
            return false;
        }

        /// <summary>
        /// 判读是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch { }
            return false;
        }


        public static bool IsLong(string str)
        {
            try
            {
                Convert.ToInt64(str);
                return true;
            }
            catch { }
            return false;
        }


        public static bool IsDecimalOrInt(string str)
        {
            Regex regex = new Regex("^[0-9]+\\.{0,1}[0-9]{0,2}$");
            if (regex.Match(str).Success)
                return true;
            return false;
        }

        //========================================================================================================================================================
        ////////////////////////////////////////////////////
        //
        // 功能：判断用户身份证号码是否合法
        //
        ////////////////////////////////////////////////////
        /// <summary>
        /// 判断用户身份证号码是否合法
        /// </summary>
        /// <param name="Id">十八或十五的身份证号码</param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id); return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证            
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证            
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证            
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证            
            }
            return true;//符合GB11643-1999标准        
        }

        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证            
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证            
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证            
            }
            return true;//符合15位身份证标准        
        }
        //========================================================================================================================================================

        /// <summary>
        /// 转化为安全字符串
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <returns>string 处理后的</returns>
        public static string ChangeToHtmlCode(string str)
        {
            try
            {
                str = str.Replace(";", "&#59;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("--", "&#45;&#45;");
                str = str.Replace("xp_", "&#120;&#112;&#95;");
                str = str.Replace("XP_", "&#88;&#80;&#95;");
                str = str.Replace("xP_", "&#120;&#80;&#95;");
                str = str.Replace("Xp_", "&#88;&#112;&#95;");
                str = str.Replace("\"", "&#34;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                return str;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把安全字符串反相转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnChangeToHtmlCode(string str)
        {
            try
            {
                str = str.Replace("&#39;", "'");
                str = str.Replace("&#45;&#45;", "--");
                str = str.Replace("&#120;&#112;&#95;", "xp_");
                str = str.Replace("&#88;&#80;&#95;", "XP_");
                str = str.Replace("&#120;&#80;&#95;", "xP_");
                str = str.Replace("&#88;&#112;&#95;", "Xp_");
                str = str.Replace("&#34;", "\"");
                str = str.Replace("&lt;", "<");
                str = str.Replace("&gt;", ">");
                str = str.Replace("&#59;", ";");
                return str;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把回车转换成&lt;br /&gt;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ChangeEnterToBr(string str)
        {
            return str.Replace("\n", "<br />");
        }

        /// <summary>
        /// 把&lt;br /&gt;转换成回车
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnChangeEnterToBr(string str)
        {
            return str.Replace("<br />", "\n");
        }

        /// <summary>
        /// 删除文本中带的HTML标记
        /// </summary>
        /// <param name="InString"></param>
        /// <returns></returns>
        public static string DelHtmlCode(string InString)
        {
            string results = "";
            InString = InString.Replace("&nbsp;", " ");
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<[^>]*>");
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(InString))
            {
                if (match.Success)
                {
                    results = match.Value;
                    InString = InString.Replace(results, "");
                }

            }
            return InString;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="Target">目标字符串</param>
        /// <param name="StartIndex">开始位置</param>
        /// <param name="Lenght">长度</param>
        /// <returns></returns>
        public static string Mid(string Target, int StartIndex, int Lenght)
        {
            if (Target.Length >= (StartIndex + Lenght))
            {
                return Target.Substring(StartIndex, Lenght);
            }
            else
            {
                return Target;
            }
        }
    }
}