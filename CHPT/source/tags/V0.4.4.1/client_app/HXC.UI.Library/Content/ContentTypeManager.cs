using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using HXC.UI.Library.Controls;

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

        static ContentTypeManager()
        {
            RegistType("Enum", typeof(ContentType4Enum));   //枚举类型内容
        }
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
        public static void SetContent(IContentControl control)
        {
            try
            {
                if (control == null || String.IsNullOrEmpty(control.ContentTypeName) || control.ContentTypeParameter == null) return;
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
        public static void SetValue(IContentControl control)
        {
            try
            {
                if (control == null || String.IsNullOrEmpty(control.ContentTypeName) || control.ContentTypeParameter == null) return;
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

    /// <summary>
    /// 枚举类型内容
    /// </summary>
    public class ContentType4Enum:IContentType
    {
        public void SetContent(IContentControl control)
        {
            if (control.ContentTypeParameter == null) return;
            //var type = Type.GetType(control.ContentTypeParameter,true,true);
            //if (type == null) return;
            control.Content = GetEnumList(control.ContentTypeParameter as Type);
        }

        public void SetValue(IContentControl control)
        {
            return;
        }

        public static List<ListItem> GetEnumList(Type enumType)
        {
            var list = new List<ListItem>();
            if (enumType == null) return list;
            try
            {
                foreach (Enum enumValue in Enum.GetValues(enumType))
                {
                    var value = Convert.ToInt32(enumValue).ToString(CultureInfo.InvariantCulture);
                    var name = Enum.GetName(enumType, enumValue);
                    var field = enumType.GetField(name);
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    var text = attribute == null ? name : attribute.Description;
                    list.Add(new ListItem(value, text));
                }
            }
            catch (Exception ex)
            {
                LibraryAssistant.UILogService.WriteLog(ex);
            }
            return list;
        }
    }
}
