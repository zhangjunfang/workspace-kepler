using System;
using System.Drawing;


namespace ServiceStationClient.Skin
{
    ///*************************************************************************//
    /// System:  
    /// FileName:     SkinAssistant         
    /// Author:       Kord
    /// Date:         2014/11/24 17:36:56
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	资源信息辅助类
    ///***************************************************************************//
    /// <summary>
    /// 资源信息辅助类
    /// </summary>
    public static class SkinAssistant
    {
        #region Constructor -- 构造函数
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        /// <summary>
        /// 根据图片资源名称获取图片资源
        /// </summary>
        /// <param name="imgName">图片资源名称</param>
        /// <param name="useDefault">当查找不到指定资源图片时,是否使用默认图片</param>
        /// <returns></returns>
        public static Image GetResourcesImage(object imgName, Boolean useDefault)
        {
            try
            {
                var img = GetResourcesImage(imgName);
                if (img != null)
                {
                    return img;
                }
                if(useDefault)
                {
                    return GetResourcesImage("CL_DEFAULT");
                }
                return null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 根据图片资源名称获取图片资源
        /// </summary>
        /// <param name="imgName">图片资源名称</param>
        /// <returns></returns>
        public static Image GetResourcesImage(object imgName)
        {
            Properties.Resources.ResourceManager.IgnoreCase = true;
            try
            {
                if (imgName == null) return null;
                var img = Properties.Resources.ResourceManager.GetObject(imgName.ToString());
                if (img == null) return null;
                return (Image)img;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
