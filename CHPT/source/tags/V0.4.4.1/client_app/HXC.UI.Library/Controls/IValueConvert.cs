using System;
using System.Globalization;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 提供一种将自定义逻辑应用于绑定的方式
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/15 10:31:38</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface IValueConvert
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值</returns>
        object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">绑定目标生成的值</param>
        /// <param name="targetType">要转换到的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值</returns>
        object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion

        #region Event -- 事件

        #endregion
    }
}
