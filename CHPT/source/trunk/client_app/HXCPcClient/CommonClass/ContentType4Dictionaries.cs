using System;
using System.Data;
using System.Linq;
using HXC.UI.Library;
using HXC.UI.Library.Content;
using HXC.UI.Library.Controls;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// ContentType4Dictionaries
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/20 13:57:34</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class ContentType4Dictionaries : IContentType
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        public void SetContent(IContentControl control)
        {
            if (control.ContentTypeParameter == null || String.IsNullOrEmpty(control.ContentTypeParameter.ToString()))
                return;

            if (LocalCache.DtDict == null)
            {
                LocalCache.Update(CacheList.Dict);
            }

            if (LocalCache.DtDict == null)
            {
                return;
            }
            var dt = LocalCache.DtDict.Clone();

            if (LocalCache.DtDict != null && LocalCache.DtDict.Rows.Count > 0)
            {
                var drs = LocalCache.DtDict.Select("dic_code = '" + control.ContentTypeParameter + "'");
                if (drs.Length > 0)
                {
                    drs = LocalCache.DtDict.Select("parent_id = '" + drs[0]["dic_id"] + "'");
                    foreach (var dr in drs)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            else
            {
                dt = LocalCache.DtDict;
            }

            var list = (from DataRow dr in dt.Rows select new ListItem(dr["dic_id"], dr["dic_name"].ToString())).ToList();
            list.Insert(0,new ListItem("",""));
            control.Content = list;
        }

        public void SetValue(IContentControl control)
        {
            return;
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
