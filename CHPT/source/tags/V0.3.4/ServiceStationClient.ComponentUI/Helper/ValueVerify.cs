using System;
using System.ComponentModel;

namespace ServiceStationClient.ComponentUI
{
    /// <summary>
    /// 数据值验证
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2014/12/23 13:41:29</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface IValueVerify
    {

        #region Property -- 属性
        /// <summary>
        /// 是否包含错误
        /// </summary>
        [Bindable(true)]
        [Description("是否显示错误")]
        Boolean ShowError { get; set; }
        /// <summary>
        /// 数据值验证所用的验证方法Key类型
        /// </summary>
        [Bindable(true)]
        [Description("数据值验证所用的验证方法Key类型")]
        String VerifyType { get; set; }
        /// <summary>
        /// 数据验证所用的验证条件值
        /// </summary>
        [Bindable(true)]
        [Description("数据验证所用的验证方法值")]
        String VerifyCondition { get; set; }
        #endregion

        #region Method -- 方法
        /// <summary>
        /// 是否包含错误
        /// </summary>
        Boolean HasError();
        #endregion

        #region Event -- 事件

        #endregion
    }

    public interface IValueVerifyType
    {

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Boolean Verify(IContentControl control);
        #endregion

        #region Event -- 事件

        #endregion
    }
}
