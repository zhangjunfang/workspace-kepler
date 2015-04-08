using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HXCCommon.DotNetCode
{
    /// <summary>
    /// 过滤单行文本框的过滤危险字符
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// web页面书写值到页面
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="curValue"></param>
        /// <returns></returns>
        public static string WriteString(object obj, string curValue)
        {
            try
            {
                string val = obj.ToString();
                if (val == curValue)
                {
                    return "selected";
                }
                else
                {
                    return "";
                }
            }
            catch { return ""; }
        }

        /// <summary>   
        /// 得到字符串的长度，一个汉字算2个字符   
        /// </summary>   
        /// <param name="str">字符串</param>   
        /// <returns>返回字符串长度</returns>   
        public static int GetLength(string str)
        {
            if (str.Length == 0) return 0;

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }

        public static string GetCenterShow(string p, int size, int font)
        {
            // p = p.Trim();
            if (font == 2)
            {
                int s = 0;
                int len = p.Length;
                if (len >= 11) return p;
                if (len == 9) s = 2;
                if (len == 8) s = 2;
                if (len == 7) s = 3;
                if (len == 6) s = 4;
                if (len == 5) s = 4;
                if (len == 4) s = 5;
                if (len == 3) s = 6;
                if (len == 2) s = 7;
                string ttt = "";
                ttt = ttt.PadLeft(s, ' ');
                return ttt + p;
            }
            if (font == 1)
            {
                int len = GetLength(p);
                int tmp = 34 - len;
                if (tmp < 0) return p;
                double index = ((double)tmp / 2);  // 5
                string ttt = "";
                int s = (int)index;
                ttt = ttt.PadLeft(s, ' ');
                return ttt + p;
            }
            return p;
        }
        /// <summary>
        /// 格式化TextArea输入内容为html显示
        /// </summary>
        /// <param name="s">要格式化内容</param>
        /// <returns>完成内容</returns>
        public static string FormatTextArea(string s)
        {
            s = s.Replace("\n", "<br>");
            s = s.Replace("\x20", "&nbsp;");
            return s;
        }
        /// <summary>
        /// 省略字符串
        /// </summary>
        /// <param name="RawString">字符</param>
        /// <param name="Length">字节</param>
        /// <param name="status">是否开启省略字符串 0：否，1：是</param>
        /// <returns></returns>
        public static string GetOmitString(string str, int length, string status)
        {
            string temp = str;
            if (status == "1")
            {
                int j = 0;
                int k = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                    {
                        j += 2;
                    }
                    else
                    {
                        j += 1;
                    }
                    if (j <= length)
                    {
                        k += 1;
                    }
                    if (j >= length)
                    {
                        return temp.Substring(0, k) + "...";
                    }
                }
            }
            return temp;
        }
    }
}
