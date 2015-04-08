using System;
using HXC.UI.Library.Controls;
using Utility.Common;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// DateTimeConvert
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/15 18:20:06</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class DateTimeConvert : IValueConvert
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime time;
            var result = DateTime.TryParse(value == null ? String.Empty : value.ToString(), out time);
            if (result)
            {
                return Common.LocalDateTimeToUtcLong(time);
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Common.UtcLongToLocalDateTime(value);
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
