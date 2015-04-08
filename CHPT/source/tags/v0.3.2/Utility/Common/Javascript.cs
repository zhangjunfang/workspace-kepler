using System;
using System.Web;
using System.Web.UI;

/// <summary>
/// 常用JavaScript操作
/// </summary>
public class Javascript
{
    /// <summary>
    /// 出现提示信息“错误：\\n 至少存在一个未知错误,请检查”
    /// </summary>
    /// <param name="page"></param>
    public static void alert(Page page)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script type=\"text/javascript\">alert('错误：\\n 至少存在一个未知错误,请检查!');</script>");
    }

    /// <summary>
    /// 出错提示信息
    /// </summary>
    /// <param name="str">提示内容</param>
    /// <param name="page"></param>
    public static void alert(Page page, string str)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script type=\"text/javascript\">alert('" + str + "');</script>");
    }

    /// <summary>
    /// 弹出成功信息
    /// </summary>
    /// <param name="str">成功信息内容</param>
    //public static void alert(string str)
    //{
    //    System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + str + "');location='" + System.Web.HttpContext.Current.Request.Url.ToString() + "';</script>");

    //}

    /// <summary>
    /// 弹出成功信息，并返回到指定的URL
    /// </summary>
    /// <param name="str"></param>
    /// <param name="url"></param>
    public static void alert(string str, string url)
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + str + "');location='" + url + "';</script>");
    }

    /// <summary>
    /// 登录成功，父级页面刷新
    /// </summary>
    /// <param name="str"></param>
    /// <param name="IsTop"></param>
    public static void alert(string str, bool IsTop)
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + str + "');top.window.location=top.window.location;</script>");
    }

    public static void alertParent(string str)
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + str + "');parent.window.location=parent.window.location;</script>");
    }

    /// <summary>
    /// 无提示信息,刷新页面
    /// </summary>    
    /// <param name="IsTop">是否刷新父级页面</param>
    public static void alert(bool IsTop)
    {
        if (IsTop)
            System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">top.window.location=top.window.location;</script>");
        else
            System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.location=window.location;</script>");
    }

    /// <summary>
    /// 登录成功，父级页面刷新
    /// </summary>
    /// <param name="page"></param>
    /// <param name="str"></param>
    /// <param name="IsTop"></param>
    public static void alert(Page page, string str, bool IsTop)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script type=\"text/javascript\">alert('" + str + "');top.window.location=top.window.location;</script>");
    }

    /// <summary>
    /// 弹出提示内容，并关闭页面。
    /// </summary>
    /// <param name="str"></param>
    public static void WindowClose(string str)
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + str + "');window.close()</script>");
    }

    /// <summary>
    /// 关闭页面
    /// </summary>
    public static void WindowClose()
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">window.close()</script>");
    }

    /// <summary>
    /// 页面跳转
    /// </summary>
    /// <param name="url"></param>
    public static void Redirect(string url)
    {
        System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">location='" + url + "';</script>");
        //System.Web.HttpContext.Current.Response.Redirect(url);
        System.Web.HttpContext.Current.Response.End();
    }

    public static void ExtJS(Page page, string strJS)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script type=\"text/javascript\">" + strJS + "</script>");    
    }
}
