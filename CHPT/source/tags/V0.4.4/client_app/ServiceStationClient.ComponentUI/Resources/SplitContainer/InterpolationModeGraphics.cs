using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace ServiceStationClient.ComponentUI
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class InterpolationModeGraphics : IDisposable
    {
        private InterpolationMode _oldMode;
        private Graphics _graphics;

        public InterpolationModeGraphics(Graphics graphics)
            : this(graphics, InterpolationMode.HighQualityBicubic)
        {
        }

        public InterpolationModeGraphics(
            Graphics graphics, InterpolationMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.InterpolationMode;
            graphics.InterpolationMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.InterpolationMode = _oldMode;
        }

        #endregion
    }
}
