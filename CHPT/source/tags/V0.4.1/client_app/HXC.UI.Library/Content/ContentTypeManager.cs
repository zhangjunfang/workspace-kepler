using System;
using System.Collections.Generic;

namespace HXC.UI.Library.Content
{
    /// <summary>
    /// ContentTypeManager
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/19 15:22:26</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class ContentTypeManager
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段
        private static readonly Dictionary<String, Type> KeyTypeDic = new Dictionary<String, Type>(); 
        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        /// <summary>
        /// 注册内容类型
        /// </summary>
        /// <param name="key">内容</param>
        /// <param name="type">内容类型</param>
        /// <returns>是否注册成功</returns>
        public static Boolean RegistType(String key, Type type)
        {
            try
            {
                if (KeyTypeDic.ContainsKey(key.ToUpper()))
                {
                    throw new Exception("不允许重复注册的内容类型KEY");
                }
                KeyTypeDic.Add(key.ToUpper(), type);
                return true;
            }
            catch (Exception ex)
            {
                LibraryAssistant.UILogService.WriteLog(ex);
                return false;
            }
        }
        /// <summary>
        /// 设置控件的Content属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        public static void ExtSetContent(IContentControl control)
        {
            try
            {
                if (control == null || String.IsNullOrEmpty(control.ContentTypeName) || String.IsNullOrEmpty(control.ContentTypeParameter)) return;
                if (!KeyTypeDic.ContainsKey(control.ContentTypeName.ToUpper())) return;
                var type = KeyTypeDic[control.ContentTypeName.ToUpper()];
                if (type == null) return;
                var contentKey = Activator.CreateInstance(type) as IContentType;
                if (contentKey != null)
                {
                    contentKey.SetContent(control);
                }
            }
            catch (Exception ex)
            {
                LibraryAssistant.UILogService.WriteLog(ex);
            }
        }
        /// <summary>
        /// 设置控件的Value属性值
        /// </summary>
        /// <param name="control">扩展控件</param>
        public static void ExtSetValue(IContentControl control)
        {
            try
            {
                if (control == null || String.IsNullOrEmpty(control.ContentTypeName) || String.IsNullOrEmpty(control.ContentTypeParameter)) return;
                if (!KeyTypeDic.ContainsKey(control.ContentTypeName.ToUpper())) return;
                var type = KeyTypeDic[control.ContentTypeName.ToUpper()];
                if (type == null) return;
                var contentKey = Activator.CreateInstance(type) as IContentType;
                if (contentKey != null)
                {
                    contentKey.SetValue(control);
                }
            }
            catch (Exception ex)
            {
                LibraryAssistant.UILogService.WriteLog(ex);
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
