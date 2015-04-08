using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXC_FuncUtility
{
    public class TimeHelper
    {
        /// <summary>
        /// 获取当前时间的毫秒数
        /// </summary>
        /// <returns></returns>
        public static string GetTimeInMillis()
        {
            DateTime startTime = DateTime.Parse("1970-1-1");
            long millis = (DateTime.Now.Ticks-startTime.Ticks) / 10000;
            return millis.ToString();
        }

        /// <summary>
        /// 获取当前时间的毫秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeInMillis(DateTime time)
        {
            DateTime startTime = DateTime.Parse("1970-1-1");
            long millis = (time.Ticks - startTime.Ticks) / 10000;
            return millis.ToString();
        }

        /// <summary>
        /// 获取当前时间的毫秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeInMillis(string time)
        {
            try
            {                
                DateTime startTime = DateTime.Parse("1970-1-1");
                long millis = (long.Parse(time) - startTime.Ticks) / 10000;
                return millis.ToString();
            }
            catch(Exception ex)
            {
                return string.Empty;
            }            
        }

        /// <summary>
        /// 毫秒转化为时间
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        public static DateTime MillisToTime(string millis)
        {
            long millisLong=0;
            if (long.TryParse(millis, out millisLong))
            {
                DateTime startTime = DateTime.Parse("1970-1-1");
                millisLong = startTime.Ticks + millisLong * 10000;
                return Convert.ToDateTime(millisLong);
            }
            return DateTime.Now;          
        }

        /// <summary>
        /// 获取当前时间的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long GetTimeInSecond(DateTime time)
        {
            long seconds = time.Ticks / 10000000;
            return seconds;
        }
    }
}
