using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HXCCommon.DotNetEncrypt;
using System.Collections;

namespace HXCCommon.DotNetUI
{
    /// <summary>
    /// Web帮助类
    /// </summary>
    public class WebHelper
    {
        #region 防止刷新重复提交
        /// <summary>
        /// 防止刷新重复提交
        /// </summary>
        /// <returns></returns>
        public static bool SubmitCheckForm()
        {
            if (HttpContext.Current.Request.Form.Get("txt_hiddenToken").Equals(GetToken()))
            {
                SetToken();
                return true;
            }
            else
            {
                ShowMsgHelper.showWarningMsg("为了保证表单不重复提交，提交无效");
                return false;
            }
        }
        /// <summary>
        /// 获得当前Session里保存的标志
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            HttpContext rq = HttpContext.Current;
            if (null != rq.Session["Token"])
            {
                return rq.Session["Token"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 生成标志，并保存到Session
        /// </summary>
        public static void SetToken()
        {
            HttpContext rq = HttpContext.Current;
            rq.Session.Add("Token", Md5Helper.MD5(rq.Session.SessionID + DateTime.UtcNow.Ticks.ToString(), 32));
        }
        #endregion

        #region 拼接Insert Sql语句
        /// <summary>
        /// 拼接InsertSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public string InsertSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key);
                sp.Append(",:" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
        #endregion
    }
}
