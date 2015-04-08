using System;

namespace Utility.Common
{
    /// <summary>
    /// 功能：通用分页类
    /// 作者：杨政伟
    /// 时间：2008年5月12日
    /// </summary>
    public class PageSelect
    {
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="_pageSize">每页显示数</param>
        public PageSelect(int _pageSize)
        {
            this.pageSize = _pageSize;
        }

        /// <summary>
        /// 显示分页按键数量
        /// </summary>
        public int ShowPagNum = 8;
        /// <summary>
        /// 每页面显示的条数
        /// </summary>
        protected int pageSize = 1;

        /// <summary>
        /// 当前显示的页
        /// </summary>
        public int CurPage = 1;

        /// <summary>
        /// 此值大于－1时，会显示记录总数
        /// </summary>
        public int counts = -1;

        /// <summary>
        /// 是否显示总数
        /// </summary>
        public bool CountsIsShow = true;

        /// <summary>
        /// 分页的URL，例如：abc.aspx?page=
        /// </summary>
        public string PageUrlString = "";

        /// <summary>
        /// 分页页面的后缀名，一般在URL重写时用到。
        /// </summary>
        public string PageDotName = "";

        /// <summary>
        /// 当前页码按键固定显示的位置
        /// </summary>
        public int FixedNum = 3;

        /// <summary>
        /// 是否显示Go按键
        /// </summary>
        public bool IsGotoPage = false;

        public bool IsPreviousNext = true;

        public string Main()
        {
            //最后一页的计算
            int LastPage = counts / pageSize + (counts % pageSize == 0 ? 0 : 1);

            //最小页为第一页
            if (CurPage < 1) CurPage = 1;
            //最大页为最后一页
            CurPage = CurPage > LastPage ? LastPage : CurPage;


            //如果只有一页时，不显示分页
            if (LastPage <= 1) return "";

            //最外层容器
            string layerOutermost = "\n<div id=\"fenye\" class=\"fenye\">\n\t<ul>{0}\n\t</ul>\n</div>\n";
            layerOutermost = "\n{0}";

            //总数容器
            string layerCounts = String.Format("\n\t\t<strong>总数：{0}</strong>", counts);

            //页码数
            string layerPages = String.Format("\n\t\t<strong title=\"共{0}页，当前第{1}页\">{1}/{0} 页</strong>", LastPage, CurPage);

            //当前页码容器
            string layerCurrent = "\n\t\t<span class=\"focus\">{0}</span>";

            //省略号
            string layerOmit = "\n\t\t<span class=\"noborder\">…</span>";

            //首页
            string layerFirstPage = String.Format("\n\t\t<a href=\"{0}{1}{2}\" title=\"首页\" >首页</a>", PageUrlString, "1", PageDotName) + layerOmit;

            //尾页
            string layerLastPage = layerOmit + String.Format("\n\t\t<a href=\"{0}{1}{2}\" title=\"尾页\" >尾页</a>", PageUrlString, LastPage, PageDotName);

            //显示的页码
            string layerShowPage = String.Format("\n\t\t<a href=\"{0}{1}{2}\">{1}</a>", PageUrlString, "{0}", PageDotName);

            //前一页
            string layerPreviousPage = String.Format("\n\t\t<a href=\"{0}{1}{2}\" title=\"上一页\" >上一页</a>", PageUrlString, CurPage - 1, PageDotName);
            string layerNextPage = String.Format("\n\t\t<a href=\"{0}{1}{2}\" title=\"下一页\" >下一页</a>", PageUrlString, CurPage + 1, PageDotName);

            string result = "";
            if (CountsIsShow)
                result += layerCounts;
            result += layerPages;

            //前一面显示条件
            if (IsPreviousNext && CurPage > 1)
                result += layerPreviousPage;

            //显示的按键数必须大于按键固定显示的位置数
            if (ShowPagNum < (FixedNum + 1)) ShowPagNum = FixedNum + 1;

            //显示首页条件
            if (LastPage > ShowPagNum && CurPage > FixedNum)
                result += layerFirstPage;

            //中间页码计算
            #region 中间页码计算
            //循环开始页的计算公式：
            //如果 当前页小于或等于固定页，循环开始页＝1
            //否则，判断最一页是否大于显示页加当前页减固定页？是，循环开始页＝当前页减固定页加1
            //否则，循环开始页＝总页数减显示数加1
            int ShowPageTempNum = (CurPage <= FixedNum || LastPage <= ShowPagNum) ? 1 : (LastPage > ShowPagNum + CurPage - FixedNum ? CurPage - FixedNum + 1 : LastPage - ShowPagNum + 1);
            //计算公式同上
            int EndPageTempNum =
                LastPage <= ShowPagNum ? LastPage :
                    (CurPage - FixedNum <= 0 ? ShowPagNum :
                        (LastPage > ShowPagNum + CurPage - FixedNum ? (ShowPagNum + CurPage - FixedNum) : LastPage)
                );
            for (; ShowPageTempNum <= EndPageTempNum; ShowPageTempNum++)
                result += ShowPageTempNum == CurPage ? String.Format(layerCurrent, CurPage) : String.Format(layerShowPage, ShowPageTempNum);
            #endregion


            //显示尾页的条件
            if (LastPage > ShowPagNum && CurPage < (LastPage - (ShowPagNum - FixedNum)) && LastPage != ShowPagNum)
                result += layerLastPage;

            //后一面显示条件
            if (IsPreviousNext && LastPage > CurPage)
                result += layerNextPage;

            //显示GO按键的条件
            if (LastPage > ShowPagNum && IsGotoPage)
            {
                string js = "var keypress = function(e){var ev = !e ? window.event : e; if (ev.keyCode == 13) {var GoToPageValue=document.getElementById('pageGoToPage').value;GoToPageValue>0?window.location.href='" + PageUrlString + "'+GoToPageValue+'':alert('请输入页码');return false;} };";
                result += "\n\t\t<em>转到</em><input class=\"suru\"  type=\"text\" id=\"pageGoToPage\" onkeypress=\"" + js + "keypress(event);\" /><em>页</em>";
                result += "\n\t\t<input class=\"annv\" onclick=\"var GoToPageValue=document.getElementById('pageGoToPage').value;GoToPageValue>0?window.location.href='" + PageUrlString + "'+GoToPageValue+'" + PageDotName + "':alert('请输入页码');return false;\" />";
            }

            //加上最外层的
            result = String.Format(layerOutermost, result);
            return result;
        }

    }
}