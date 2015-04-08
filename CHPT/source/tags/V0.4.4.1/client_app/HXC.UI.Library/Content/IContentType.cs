namespace HXC.UI.Library.Content
{
    /// <summary>
    /// IContentType
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/19 15:20:29</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public interface IContentType
    {
        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 根据资源Key设置控件的Content属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        void SetContent(IContentControl control);
        /// <summary>
        /// 根据资源Key设置控件的Value属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        void SetValue(IContentControl control);
        #endregion

        #region Event -- 事件

        #endregion
    }
}