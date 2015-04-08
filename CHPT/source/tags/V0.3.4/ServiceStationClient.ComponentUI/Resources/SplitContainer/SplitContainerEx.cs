using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ServiceStationClient.ComponentUI
{
    /* 作者：Starts_2000
     * 日期：2010-03-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class SplitContainerEx : SplitContainer
    {
        private CollapsePanel _collapsePanel = CollapsePanel.Panel1;
        private SpliterPanelState _spliterPanelState = SpliterPanelState.Expanded;
        private ControlState _mouseState;
        private int _lastDistance;
        private int _minSize;
        private HistTest _histTest;
        private readonly object EventCollapseClick = new object();

        public SplitContainerEx()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);            
            _lastDistance = base.SplitterDistance;
        }

        public event EventHandler CollapseClick
        {
            add { base.Events.AddHandler(EventCollapseClick, value); }
            remove { base.Events.RemoveHandler(EventCollapseClick, value); }
        }

        [DefaultValue(typeof(CollapsePanel), "1")]
        public CollapsePanel CollapsePanel
        {
            get { return _collapsePanel; }
            set
            {
                if (_collapsePanel != value)
                {
                    Expand();
                    _collapsePanel = value;
                }
            }
        }

        protected virtual int DefaultCollapseWidth
        {
            get { return 80; }
        }

        protected virtual int DefaultArrowWidth
        {
            get { return 16; }
        }

        protected Rectangle CollapseRect
        {
            get
            {
                if (_collapsePanel == CollapsePanel.None)
                {
                    return Rectangle.Empty;
                }

                Rectangle rect = base.SplitterRectangle;
                if (base.Orientation == Orientation.Horizontal)
                {
                    rect.X = (base.Width - DefaultCollapseWidth) / 2;
                    rect.Width = DefaultCollapseWidth;
                }
                else
                {
                    rect.Y = (base.Height - DefaultCollapseWidth) / 2;
                    rect.Height = DefaultCollapseWidth;
                }

                return rect;
            }
        }

        internal SpliterPanelState SpliterPanelState
        {
            get { return _spliterPanelState; }
            set
            {
                if (_spliterPanelState != value)
                {
                    switch (value)
                    {
                        case SpliterPanelState.Expanded:
                            Expand();
                            break;
                        case SpliterPanelState.Collapsed:
                            Collapse();
                            break;

                    }
                    _spliterPanelState = value;
                }
            }
        }

        public SpliterPanelState SpliterState 
        {
            get { return _spliterPanelState; }
        }
        internal ControlState MouseState
        {
            get { return _mouseState; }
            set
            {
                if (_mouseState != value)
                {
                    _mouseState = value;
                    base.Invalidate(CollapseRect);
                }
            }
        }

        public void Collapse()
        {
            if (_collapsePanel != CollapsePanel.None &&
                _spliterPanelState == SpliterPanelState.Expanded)
            {
                _lastDistance = base.SplitterDistance;
                if (_collapsePanel == CollapsePanel.Panel1)
                {
                    _minSize = base.Panel1MinSize;
                    base.Panel1MinSize = 0;
                    base.SplitterDistance = 0;
                }
                else
                {
                    int width = base.Orientation == Orientation.Horizontal ?
                        base.Height : base.Width;
                    _minSize = base.Panel2MinSize;
                    base.Panel2MinSize = 0;
                    base.SplitterDistance = width - base.SplitterWidth- base.Padding.Vertical;
                }
                base.Invalidate(base.SplitterRectangle);
            }
        }

        public void Expand()
        {
            if (_collapsePanel != CollapsePanel.None &&
               _spliterPanelState == SpliterPanelState.Collapsed)
            {
                if (_collapsePanel == CollapsePanel.Panel1)
                {
                    base.Panel1MinSize = _minSize;
                }
                else
                {
                    base.Panel2MinSize = _minSize;
                }
                base.SplitterDistance = _lastDistance;
                base.Invalidate(base.SplitterRectangle);
            }
        }

        public virtual void OnCollapseClick(EventArgs e)
        {
            if (_spliterPanelState == SpliterPanelState.Collapsed)
            {
                SpliterPanelState = SpliterPanelState.Expanded;
            }
            else
            {
                SpliterPanelState = SpliterPanelState.Collapsed;
            }

            EventHandler handler = base.Events[EventCollapseClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Color gradientColor1 = Color.FromArgb(206, 238, 255);
        public Color GradientColor1
        {
            get
            {
                return gradientColor1;
            }
            set
            {
                gradientColor1 = value;
            }
        }
        private Color gradientColor2 = Color.FromArgb(223, 246, 255);
        public Color GradientColor2
        {
            get 
            {
                return gradientColor2;
            }
            set 
            {
                gradientColor2 = value;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (base.Panel1Collapsed || base.Panel2Collapsed)
            {
                return;
            }

            Graphics g = e.Graphics;
            Rectangle rect = base.SplitterRectangle;
            bool bHorizontal = base.Orientation == Orientation.Horizontal;

            System.Drawing.Drawing2D.LinearGradientMode gradientMode = bHorizontal ?
                System.Drawing.Drawing2D.LinearGradientMode.Vertical : System.Drawing.Drawing2D.LinearGradientMode.Horizontal;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, gradientColor1,gradientColor2, gradientMode))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, .5f, 1f };
                blend.Factors = new float[] { .5F, 1F, .5F };

                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if (_collapsePanel == CollapsePanel.None)
            {
                return;
            }

            Rectangle arrowRect;
            Rectangle topLeftRect;
            Rectangle bottomRightRect;

            CalculateRect(
                CollapseRect,
                out arrowRect,
                out topLeftRect,
                out bottomRightRect);

            ArrowDirection direction = ArrowDirection.Left;

            switch (_collapsePanel)
            {
                case CollapsePanel.Panel1:
                    if (bHorizontal)
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Down : ArrowDirection.Up;
                    }
                    else
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Right : ArrowDirection.Left;
                    }
                    break;
                case CollapsePanel.Panel2:
                    if (bHorizontal)
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Up : ArrowDirection.Down;
                    }
                    else
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Left : ArrowDirection.Right;
                    }
                    break;
            }

            Color foreColor = _mouseState == ControlState.Hover ?
                Color.FromArgb(21, 66, 139) : Color.FromArgb(80, 136, 228);
            using (SmoothingModeGraphics sg = new SmoothingModeGraphics(g))
            {
                RenderHelper.RenderGrid(g, topLeftRect, new Size(3, 3), foreColor);
                RenderHelper.RenderGrid(g, bottomRightRect, new Size(3, 3), foreColor);

                using (Brush brush = new SolidBrush(foreColor))
                {
                    RenderHelper.RenderArrowInternal(
                        g,
                        arrowRect,
                        direction,
                        brush);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //如果鼠标的左键没有按下，重置HistTest
            if (e.Button != MouseButtons.Left)
            {
                _histTest = HistTest.None;
            }

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            //鼠标在Button矩形里，并且不是在拖动
            if (collapseRect.Contains(mousePoint) &&
                _histTest != HistTest.Spliter)
            {
                base.Capture = false;
                SetCursor(Cursors.Hand);
                MouseState = ControlState.Hover;
                return;
            }//鼠标在分隔栏矩形里
            else if (base.SplitterRectangle.Contains(mousePoint))
            {
                MouseState = ControlState.Normal;

                //如果已经在按钮按下了鼠标或者已经收缩，就不允许拖动了
                if (_histTest == HistTest.Button ||
                    (_collapsePanel != CollapsePanel.None &&
                    _spliterPanelState == SpliterPanelState.Collapsed))
                {
                    base.Capture = false;
                    base.Cursor = Cursors.Default;
                    return;
                }

                //鼠标没有按下，设置Split光标
                if (_histTest == HistTest.None &&
                    !base.IsSplitterFixed)
                {
                    if (base.Orientation == Orientation.Horizontal)
                    {
                        SetCursor(Cursors.HSplit);
                    }
                    else
                    {
                        SetCursor(Cursors.VSplit);
                    }
                    return;
                }
            }

            MouseState = ControlState.Normal;

            //正在拖动分隔栏
            if (_histTest == HistTest.Spliter &&
                !base.IsSplitterFixed)
            {
                if (base.Orientation == Orientation.Horizontal)
                {
                    SetCursor(Cursors.HSplit);
                }
                else
                {
                    SetCursor(Cursors.VSplit);
                }
                base.OnMouseMove(e);
                return;
            }

            base.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.Cursor = Cursors.Default;
            MouseState = ControlState.Normal;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (collapseRect.Contains(mousePoint) ||
                (_collapsePanel != CollapsePanel.None &&
                _spliterPanelState == SpliterPanelState.Collapsed))
            {
                _histTest = HistTest.Button;
                return;
            }

            if (base.SplitterRectangle.Contains(mousePoint))
            {
                _histTest = HistTest.Spliter;
            }

            base.OnMouseDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            base.Invalidate(base.SplitterRectangle);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            base.Invalidate(base.SplitterRectangle);

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (_histTest == HistTest.Button && 
                e.Button == MouseButtons.Left &&
                collapseRect.Contains(mousePoint))
            {
                OnCollapseClick(EventArgs.Empty);
            }
            _histTest = HistTest.None;
        }

        private void SetCursor(Cursor cursor)
        {
            if (base.Cursor != cursor)
            {
                base.Cursor = cursor;
            }
        }

        private void CalculateRect(
            Rectangle collapseRect,
            out Rectangle arrowRect,
            out Rectangle topLeftRect,
            out Rectangle bottomRightRect)
        {
            int width;
            if (base.Orientation == Orientation.Horizontal)
            {
                width = (collapseRect.Width - DefaultArrowWidth) / 2;
                arrowRect = new Rectangle(
                    collapseRect.X + width,
                    collapseRect.Y,
                    DefaultArrowWidth,
                    collapseRect.Height);

                topLeftRect = new Rectangle(
                    collapseRect.X,
                    collapseRect.Y + 1,
                    width,
                    collapseRect.Height - 2);

                bottomRightRect = new Rectangle(
                    arrowRect.Right,
                    collapseRect.Y + 1,
                    width,
                    collapseRect.Height - 2);
            }
            else
            {
                width = (collapseRect.Height - DefaultArrowWidth) / 2;
                arrowRect = new Rectangle(
                    collapseRect.X,
                    collapseRect.Y + width,
                    collapseRect.Width,
                    DefaultArrowWidth);

                topLeftRect = new Rectangle(
                    collapseRect.X + 1,
                    collapseRect.Y,
                    collapseRect.Width - 2,
                    width);

                bottomRightRect = new Rectangle(
                    collapseRect.X + 1,
                    arrowRect.Bottom,
                    collapseRect.Width - 2,
                    width);
            }
        }

        private enum HistTest
        {
            None,
            Button,
            Spliter
        }
    }
}
