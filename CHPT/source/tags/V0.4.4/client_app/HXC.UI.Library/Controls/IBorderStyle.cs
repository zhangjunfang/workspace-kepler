using System;
using System.Drawing;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 控件外边框样式
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/10 11:22:10</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface IBorderStyle
    {
        #region Property -- 属性
        /// <summary>
        /// 边框宽度
        /// </summary>
        Int32 BorderWidth { get; set; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        Color BorderColor { get; set; }
        /// <summary>
        /// 倒角大小
        /// </summary>
        Int32 CornerRadiu { get; set; }
        /// <summary>
        /// 边框绘画区域
        /// </summary>
        Rectangle Rectangle { get; }
        #endregion

        #region Method -- 方法

        #endregion

        #region Event -- 事件
        event EventHandler BorderWidthChanged;
        #endregion
    }
}