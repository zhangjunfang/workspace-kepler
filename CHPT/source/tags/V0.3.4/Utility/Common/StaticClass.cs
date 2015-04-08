using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Utility.Common
{
    /// <summary>
    /// 常用的一些静态操作类
    /// </summary>
    public class StaticClass
    {
        public StaticClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 补齐空格
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <param name="fillLength">需补充的总长度</param>
        /// <param name="userChar">使用的字符</param>
        /// <returns></returns>
        public static string FilledSpaces(int len, int fillLength, string userChar)
        {
            string results = "";

            for (int i = len; i < fillLength; i++)
            {
                results += userChar;
            }
            //results += len;
            return results;
        }

        /// <summary>
        /// 为DropDownList下拉框添加值
        /// </summary>
        /// <param name="select">下拉框控件的Id名称</param>
        /// <param name="ds">待添加的数据集</param>
        /// <param name="name">负值给下拉框内容的字段</param>
        /// <param name="value">负值给下拉框Value值的字段</param>
        public static void SelectItemsAdd(DropDownList select, DataSet ds, string name, string value)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
                select.Items.Add(new ListItem(row[name].ToString(), row[value].ToString()));
        }

        /// <summary>
        /// 为DropDownList下拉框添加值
        /// </summary>
        /// <param name="select">下拉框控件的Id名称</param>
        /// <param name="ds">待添加的数据集</param>
        /// <param name="name">负值给下拉框内容的字段</param>
        /// <param name="value">负值给下拉框Value值的字段</param>
        public static void SelectItemsAdd(HtmlSelect select, DataSet ds, string name, string value)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
                select.Items.Add(new ListItem(row[name].ToString(), row[value].ToString()));
        }

        /// <summary>
        /// 为单选框增加值
        /// </summary>
        /// <param name="select">单选框组ID</param>
        /// <param name="dt">数据集</param>
        /// <param name="name">负值给显示内容的字段</param>
        /// <param name="value">负值给Value值的字段</param>
        public static void SelectItemsAdd(RadioButtonList select, DataTable dt, string name, string value)
        {
            foreach (DataRow row in dt.Rows)
                select.Items.Add(new ListItem(row[name].ToString(), row[value].ToString()));
        }

        /// <summary>
        /// 为DropDownList下拉框添加值
        /// </summary>
        /// <param name="select">下拉框控件的Id名称</param>
        /// <param name="ds">待添加的数据集</param>
        /// <param name="name">负值给下拉框内容的字段</param>
        /// <param name="value">负值给下拉框Value值的字段</param>
        public static void SelectItemsAddLevel(HtmlSelect select, DataSet ds, string name, string value)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int level = (int)row["level"];
                string levelStr = "　├";
                if (level > 1)
                {
                    for (int i = 1; i < level; i++)
                        levelStr = "　│" + levelStr;
                }
                select.Items.Add(new ListItem(levelStr + row[name].ToString(), row[value].ToString()));
            }
        }

        /// <summary>
        /// 为DropDownList下拉框添加值
        /// </summary>
        /// <param name="select">下拉框控件的Id名称</param>
        /// <param name="ds">待添加的数据集</param>
        /// <param name="name">负值给下拉框内容的字段</param>
        /// <param name="name2">负值给下拉框内容的字段第二个</param>
        /// <param name="value">负值给下拉框Value值的字段</param>
        public static void SelectItemsAdd(HtmlSelect select, DataSet ds, string name, string name2, string value)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
                select.Items.Add(new ListItem(row[name].ToString() + "[" + row[name2].ToString() + "]", row[value].ToString()));
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(object obj, int length)
        {
            if (obj.ToString().Length > length)
            {
                return obj.ToString().Substring(0, length);
            }
            else
                return obj.ToString();
        }

        #region 后台操作日志类型
        /// <summary>
        /// 后台操作日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 代码=0
            /// </summary>
            未知 = 0,
            /// <summary>
            /// 代码=1
            /// </summary>
            系统,
            /// <summary>
            /// 代码=2
            /// </summary>
            会员,
            /// <summary>
            /// 代码=3
            /// </summary>
            商城,
            /// <summary>
            /// 代码=4
            /// </summary>
            团购,
            /// <summary>
            /// 代码=5
            /// </summary>
            联盟,
            /// <summary>
            /// 代码=6
            /// </summary>
            影院,
            /// <summary>
            /// 代码=7
            /// </summary>
            新闻,
            /// <summary>
            /// 代码=8
            /// </summary>
            配置
        }
        /// <summary>
        /// 后台操作日志类型
        /// </summary>
        public static DataTable LogTypeDataTable
        {
            get
            {
                DataTable dt = new DataTable("LogType");
                dt.Columns.Add("id");
                dt.Columns.Add("Name");
                dt.Rows.Add(new object[] { "0", "未知" });
                dt.Rows.Add(new object[] { "1", "系统" });
                dt.Rows.Add(new object[] { "2", "会员" });
                dt.Rows.Add(new object[] { "3", "商城" });
                dt.Rows.Add(new object[] { "4", "团购" });
                dt.Rows.Add(new object[] { "5", "联盟" });
                dt.Rows.Add(new object[] { "6", "影院" });
                dt.Rows.Add(new object[] { "7", "新闻" });
                dt.Rows.Add(new object[] { "8", "配置" });
                return dt;
            }
        }
        #endregion
    }
}