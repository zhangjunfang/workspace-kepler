using System;
using System.ComponentModel;

namespace ServiceStationClient.ComponentUI
{
    /// <summary>
    /// 内容控件接口
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/23 13:44:33</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface IContentControl : IValueVerify
    {
        #region Property -- 属性
        /// <summary>
        /// 对象控件值
        /// </summary>
        [Bindable(true)]
        [Category("Extend")]
        [Description("内容控件值")]
        Object Value { get; set; }
        #endregion

        #region Method -- 方法

        #endregion

        #region Event -- 事件
        event EventHandler ValueChanged;
        #endregion
    }
}
