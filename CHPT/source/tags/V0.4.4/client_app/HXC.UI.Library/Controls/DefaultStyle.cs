using System.Drawing;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 控件默认样式
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/10 14:46:53</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public static class DefaultStyle
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        #region DefaultBorderColor -- 默认边框颜色
        private static Color _defaultBorderColor = Color.FromArgb(189, 211, 254);
        /// <summary>
        /// 默认边框颜色
        /// </summary>
        public static Color DefaultBorderColor
        {
            get { return _defaultBorderColor; }
            set
            {
                if (!Equals(_defaultBorderColor, value))
                {
                    _defaultBorderColor = value;
                }
            }
        }
        #endregion

        #region DefaultBackColor -- 默认面板控件背景颜色
        private static Color _defaultBackColor = Color.FromArgb(239, 248, 255);
        /// <summary>
        /// 默认面板控件背景颜色
        /// </summary>
        public static Color DefaultBackColor
        {
            get { return _defaultBackColor; }
            set
            {
                if (!Equals(_defaultBackColor, value))
                {
                    _defaultBackColor = value;
                }
            }
        }
        #endregion

        #region DefaultBackColor -- 默认面板控件背景颜色
        private static Color _defaultInputBackColor = Color.White;
        /// <summary>
        /// 默认输入控件背景颜色
        /// </summary>
        public static Color DefaultInputBackColor
        {
            get { return _defaultInputBackColor; }
            set
            {
                if (!Equals(_defaultInputBackColor, value))
                {
                    _defaultInputBackColor = value;
                }
            }
        }
        #endregion
        #endregion

        #region Method -- 方法

        #endregion

        #region Event -- 事件

        #endregion
    }
}
