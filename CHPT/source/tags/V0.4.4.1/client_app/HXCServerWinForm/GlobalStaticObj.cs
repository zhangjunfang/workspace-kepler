using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace HXCServerWinForm
{
    public static class GlobalStaticObj
    {
        public static HXCServerMainForm AppMainForm { get; set; }
        /// <summary>
        /// ds包括用户记录UserID、关联的权限菜单列表
        /// </summary>
        public static DataSet gLoginDataSet;
        public static string CookieStr { get; set; }

        #region datagridview相关
        /// <summary> 单元格背景色
        /// </summary>
        public static readonly Color RowBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
        /// <summary> 单元格被选定时背景色
        /// </summary>
        public static readonly Color RowSelectBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));


        /// <summary> 可编辑单元格背景色
        /// </summary>
        public static readonly Color RowEditBgColor = Color.BlueViolet;
        /// <summary> 可编辑单元格被选定时背景色
        /// </summary>
        public static readonly Color RowEditSelectBgColor = Color.BlueViolet;
        #endregion
    }
}
