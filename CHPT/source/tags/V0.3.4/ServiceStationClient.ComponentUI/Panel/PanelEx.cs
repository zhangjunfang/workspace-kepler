using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{

    [FlagsAttribute] public enum CornerCurveMode
	{

		None = 0,
		TopLeft = 1,
		TopRight = 2,
		TopLeft_TopRight = 3,
		BottomLeft = 4,
		TopLeft_BottomLeft = 5,
		TopRight_BottomLeft = 6,
		TopLeft_TopRight_BottomLeft = 7,
		BottomRight = 8,
		BottomRight_TopLeft = 9,
		BottomRight_TopRight = 10,
		BottomRight_TopLeft_TopRight = 11,
		BottomRight_BottomLeft = 12,
		BottomRight_TopLeft_BottomLeft = 13,
		BottomRight_TopRight_BottomLeft = 14,
		All = 15

	}

    public enum LinearGradientMode
	{
		Horizontal = 0,
		Vertical = 1,
		ForwardDiagonal = 2,
		BackwardDiagonal = 3,
		None = 4
	}


    [ToolboxBitmapAttribute(typeof(System.Windows.Forms.Panel))]
    public partial class PanelEx : System.Windows.Forms.Panel
    {
        // Fields
        private Color _BackColour1 = Color.FromArgb(239, 248, 255);
        private Color _BackColour2 = ColorTranslator.FromHtml("#eff8ff");
        private LinearGradientMode _GradientMode = LinearGradientMode.None;
        private BorderStyle _BorderStyle = BorderStyle.FixedSingle;
        private Color _BorderColour = Color.FromArgb(189, 211, 254);
        private int _BorderWidth = 1;
        private int _Curvature;

        private CornerCurveMode _CurveMode = CornerCurveMode.All;

        public new Color BackColor
        {
            get
            {
                return _BackColour1;
            }
            set
            {
                _BackColour1 = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }
        public Color BackColor2
        {
            get
            {
                return _BackColour2;
            }
            set
            {
                _BackColour2 = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public LinearGradientMode GradientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                _GradientMode = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public new BorderStyle BorderStyle
        {
            get
            {
                return _BorderStyle;
            }
            set
            {
                _BorderStyle = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColour;
            }
            set
            {
                _BorderColour = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public int BorderWidth
        {
            get
            {
                return _BorderWidth;
            }
            set
            {
                _BorderWidth = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public int Curvature
        {
            get
            {
                return _Curvature;
            }
            set
            {
                _Curvature = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        public CornerCurveMode CurveMode
        {
            get
            {
                return _CurveMode;
            }
            set
            {
                _CurveMode = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private int adjustedCurve
        {
            get
            {
                var curve = 0;
                if (_CurveMode == CornerCurveMode.None) return curve;
                curve = _Curvature > (ClientRectangle.Width / 2) ? DoubleToInt(ClientRectangle.Width / 2) : _Curvature;
                if (curve > (ClientRectangle.Height / 2))
                {
                    curve = DoubleToInt(ClientRectangle.Height / 2);
                }
                return curve;
            }
        }

        public PanelEx()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            SetDefaultControlStyles();
            customInitialisation();
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void customInitialisation()
        {
            SuspendLayout();
            base.BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            ResumeLayout(false);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var graphPath = GetPath();
            var rect = ClientRectangle;
            if (ClientRectangle.Width == 0)
            {
                rect.Width += 1;
            }
            if (ClientRectangle.Height == 0)
            {
                rect.Height += 1;
            }
            var filler = _GradientMode == LinearGradientMode.None ? new LinearGradientBrush(rect, _BackColour1, _BackColour1, System.Drawing.Drawing2D.LinearGradientMode.Vertical) : new LinearGradientBrush(rect, _BackColour1, _BackColour2, ((System.Drawing.Drawing2D.LinearGradientMode)_GradientMode));
            pevent.Graphics.FillPath(filler, graphPath);
            filler.Dispose();
            switch (_BorderStyle)
            {
                case BorderStyle.FixedSingle:
                {
                    var borderPen = new Pen(_BorderColour, _BorderWidth);
                    pevent.Graphics.DrawPath(borderPen, graphPath);
                    borderPen.Dispose();
                }
                    break;
                case BorderStyle.Fixed3D:
                    DrawBorder3D(pevent.Graphics, ClientRectangle);
                    break;
                case BorderStyle.None:
                    break;
            }
            filler.Dispose();
            graphPath.Dispose();
        }

        protected GraphicsPath GetPath()
        {
            var graphPath = new GraphicsPath();
            if (_BorderStyle == BorderStyle.Fixed3D)
            {
                graphPath.AddRectangle(ClientRectangle);
            }
            else
            {
                try
                {
                    var curve = 0;
                    Rectangle rect = ClientRectangle;
                    try
                    {
                        rect = new Rectangle(ClientRectangle.Location, new Size(ClientRectangle.Size.Width - 1, ClientRectangle.Size.Height - 1));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    var offset = 0;
                    if (_BorderStyle == BorderStyle.FixedSingle)
                    {
                        if (_BorderWidth > 1)
                        {
                            offset = DoubleToInt(BorderWidth / 2);
                        }
                        curve = adjustedCurve;
                    }
                    else switch (_BorderStyle)
                    {
                        case BorderStyle.Fixed3D:
                            break;
                        case BorderStyle.None:
                            curve = adjustedCurve;
                            break;
                    }
                    if (curve == 0)
                    {
                        graphPath.AddRectangle(Rectangle.Inflate(rect, -offset, -offset));
                    }
                    else
                    {
                        var rectWidth = rect.Width - 1 - offset;
                        var rectHeight = rect.Height - 1 - offset;
                        int curveWidth;
                        if ((_CurveMode & CornerCurveMode.TopRight) != 0)
                        {
                            curveWidth = (curve * 2);
                        }
                        else
                        {
                            curveWidth = 1;
                        }
                        graphPath.AddArc(rectWidth - curveWidth, offset, curveWidth, curveWidth, 270, 90);
                        if ((_CurveMode & CornerCurveMode.BottomRight) != 0)
                        {
                            curveWidth = (curve * 2);
                        }
                        else
                        {
                            curveWidth = 1;
                        }
                        graphPath.AddArc(rectWidth - curveWidth, rectHeight - curveWidth, curveWidth, curveWidth, 0, 90);
                        if ((_CurveMode & CornerCurveMode.BottomLeft) != 0)
                        {
                            curveWidth = (curve * 2);
                        }
                        else
                        {
                            curveWidth = 1;
                        }
                        graphPath.AddArc(offset, rectHeight - curveWidth, curveWidth, curveWidth, 90, 90);
                        if ((_CurveMode & CornerCurveMode.TopLeft) != 0)
                        {
                            curveWidth = (curve * 2);
                        }
                        else
                        {
                            curveWidth = 1;
                        }
                        graphPath.AddArc(offset, offset, curveWidth, curveWidth, 180, 90);
                        graphPath.CloseFigure();
                    }
                }
                catch (Exception)
                {
                    graphPath.AddRectangle(ClientRectangle);
                }
            }
            return graphPath;
        }

        public static void DrawBorder3D(Graphics graphics, Rectangle rectangle)
        {
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.DrawLine(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Y);
            graphics.DrawLine(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.X, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlDarkDark, rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Y + 1);
            graphics.DrawLine(SystemPens.ControlDarkDark, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlLight, rectangle.X + 1, rectangle.Height - 2, rectangle.Width - 2, rectangle.Height - 2);
            graphics.DrawLine(SystemPens.ControlLight, rectangle.Width - 2, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
            graphics.DrawLine(SystemPens.ControlLightLight, rectangle.X, rectangle.Height - 1, rectangle.Width - 1, rectangle.Height - 1);
            graphics.DrawLine(SystemPens.ControlLightLight, rectangle.Width - 1, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
        }

        public static int DoubleToInt(double value)
        {
            return Decimal.ToInt32(Decimal.Floor(Decimal.Parse((value).ToString(CultureInfo.InvariantCulture))));
        }
    }
}
