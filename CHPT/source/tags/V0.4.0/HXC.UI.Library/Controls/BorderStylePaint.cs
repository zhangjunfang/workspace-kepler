using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace HXC.UI.Library.Controls
{
    /// <summary>
    /// 控件边框绘画
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/10 11:27:52</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public class BorderStylePaint
    {
        #region Constructor -- 构造函数

        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法
        public static void DrawRoundRectangle(Graphics g, IBorderStyle arg)
        {
            if (g == null || arg.BorderWidth == 0)
            {
                return;
            }

            using (var path = CreateRoundPath(arg.Rectangle, arg.CornerRadiu))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                using (var pen = new Pen(new SolidBrush(arg.BorderColor), arg.BorderWidth))
                    g.DrawPath(pen, path);
            }
        }
        public static GraphicsPath CreateRoundPath(Rectangle rect, int cornerRadiu)
        {
            var path = new GraphicsPath();

            if (rect.Width == 0 || rect.Height == 0)
            {
                return path;
            }

            if (cornerRadiu > 0)
            {
                path.AddArc(
                    rect.Left, rect.Top, cornerRadiu, cornerRadiu, 180, 90);
            }

            path.AddLine(new Point(rect.Left + cornerRadiu, rect.Top),
                         new Point(rect.Right - cornerRadiu, rect.Top));

            if (cornerRadiu > 0)
            {
                path.AddArc(rect.Right - cornerRadiu, rect.Top,
                    cornerRadiu, cornerRadiu, -90, 90);
            }

            path.AddLine(new Point(rect.Right, rect.Top + cornerRadiu),
                         new Point(rect.Right, rect.Bottom - cornerRadiu));

            if (cornerRadiu > 0)
            {
                path.AddArc(rect.Right - cornerRadiu, rect.Bottom - cornerRadiu,
                    cornerRadiu, cornerRadiu, 0, 90);
            }

            path.AddLine(new Point(rect.Right - cornerRadiu, rect.Bottom),
                         new Point(rect.Left + cornerRadiu, rect.Bottom));

            if (cornerRadiu > 0)
            {
                path.AddArc(rect.Left, rect.Bottom - cornerRadiu,
                    cornerRadiu, cornerRadiu, 90, 90);
            }

            path.AddLine(new Point(rect.Left, rect.Bottom - cornerRadiu),
                         new Point(rect.Left, rect.Top + cornerRadiu));

            path.CloseFigure();

            return path;
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}
