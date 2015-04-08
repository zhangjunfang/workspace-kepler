using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace ServiceStationClient.ComponentUI
{
    /* 作者：Starts_2000
     * 日期：2009-10-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    internal class TextRenderingHintGraphics : IDisposable
    {
        private Graphics _graphics;
        private TextRenderingHint _oldTextRenderingHint;

        public TextRenderingHintGraphics(Graphics graphics)
            : this(graphics, TextRenderingHint.AntiAlias)
        {
        }

        public TextRenderingHintGraphics(
            Graphics graphics,
            TextRenderingHint newTextRenderingHint)
        {
            _graphics = graphics;
            _oldTextRenderingHint = graphics.TextRenderingHint;
            _graphics.TextRenderingHint = newTextRenderingHint;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.TextRenderingHint = _oldTextRenderingHint;
        }

        #endregion
    }
}
