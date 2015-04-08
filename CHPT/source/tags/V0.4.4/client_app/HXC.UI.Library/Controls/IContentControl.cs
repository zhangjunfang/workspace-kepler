using System;
using System.ComponentModel;
using HXC.UI.Library.Verify;

namespace HXC.UI.Library
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
        [Category("Extensions")]
        [Description("内容控件值")]
        Object Value { get; set; }
        /// <summary>
        /// 对象控件显示值
        /// </summary>
        [Bindable(true)]
        [Category("Extensions")]
        [Description("对象控件显示值")]
        String DisplayValue { get; set; }
        /// <summary>
        /// 对象控件内容
        /// </summary>
        [Bindable(true)]
        [Category("Extensions")]
        [Description("对象控件内容")]
        Object Content { get; set; }
        /// <summary>
        /// 对象控件内容索引键
        /// </summary>
        [Bindable(true)]
        [Category("Extensions")]
        [Description("对象控件内容类型名称")]
        String ContentTypeName { get; set; }
        /// <summary>
        /// 对象控件内容类型参数
        /// </summary>
        [Bindable(true)]
        [Category("Extensions")]
        [Description("对象控件内容类型参数")]
        Object ContentTypeParameter { get; set; }
        #endregion

        #region Method -- 方法
        void EmptyValue();
        #endregion

        #region Event -- 事件
        event EventHandler ValueChanged;
        #endregion
    }
}
