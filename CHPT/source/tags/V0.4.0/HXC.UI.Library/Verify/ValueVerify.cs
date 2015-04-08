using System;
using System.ComponentModel;

namespace HXC.UI.Library.Verify
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
        /// 数据值验证所用的验证类型名称
        /// </summary>
        [Bindable(true)]
        [Description("数据值验证所用的验证类型名称")]
        String VerifyTypeName { get; set; }
        /// <summary>
        /// 数据值验证所用的验证类型
        /// </summary>
        [Bindable(true)]
        [Description("数据值验证所用的验证类型")]
        Type VerifyType { get; set; }
        /// <summary>
        /// 数据验证所用的验证条件值
        /// </summary>
        [Bindable(true)]
        [Description("数据验证所用的验证方法值")]
        String VerifyCondition { get; set; }
        /// <summary>
        /// 实时数据验证所用的验证条件值
        /// </summary>
        [Bindable(true)]
        [Description("实时数据验证所用的验证方法值")]
        String InputtingVerifyCondition { get; set; }
        #endregion

        #region Method -- 方法
        /// <summary>
        /// 验证控件值有效性,并决定是否将错误信息呈现到界面
        /// </summary>
        Boolean Verify(bool showError = false);
        /// <summary>
        /// 验证当前输入控件值有效性,并决定是否将错误信息呈现到界面
        /// </summary>
        Boolean InputtingVerify(bool showError = false);
        #endregion

        #region Event -- 事件

        #endregion
    }

    public interface IValueVerifyType
    {
        #region Property -- 属性
        /// <summary>
        /// 验证不通过时的消息
        /// </summary>
        String VerifyMessage { get; set; }
        #endregion

        #region Method -- 方法
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Boolean Verify(IContentControl control);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        Boolean InputtingVerify(IContentControl control);
        #endregion

        #region Event -- 事件

        #endregion
    }
}
