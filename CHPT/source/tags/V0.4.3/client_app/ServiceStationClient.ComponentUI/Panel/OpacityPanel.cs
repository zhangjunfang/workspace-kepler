using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ServiceStationClient.ComponentUI
{
    public class OpacityPanel : System.Windows.Forms.Panel
    {

        #region 定义字段
        /// <summary>
        /// 透明度
        /// </summary>
        private int _alpha = 100;
        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color _bkcolor = DefaultBackColor;
        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _bdcolor = DefaultBackColor;

        /// <summary>
        ///  
        /// </summary>
        private const int Band = 5;
        /// <summary>
        /// 最小宽度
        /// </summary>
        private const int MinWidth = 10;
        /// <summary>
        /// 鼠标状态
        /// </summary>
        private EnumMousePointPosition m_MousePointPosition;
        /// <summary>
        /// 鼠标按下点
        /// </summary>
        private Point DownPoint;
        /// <summary>
        /// 鼠标移动点
        /// </summary>
        private Point CurrPoint;
        #endregion

        #region 定义属性
        /// <summary>
        /// 透明度
        /// </summary>
        public int Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
                this.Invalidate();
                this.Update();
            }
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color bkColor
        {
            get
            {
                return _bkcolor;
            }
            set
            {
                _bkcolor = value;
                this.Invalidate();
                this.Update();
            }
        }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color bdColor
        {
            get
            {
                return _bdcolor;
            }
            set
            {
                _bdcolor = value;
                this.Invalidate();
                this.Update();
            }
        }
        #endregion

        #region 构造函数

        public OpacityPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary>
        /// 背景透明panel
        /// </summary>
        /// <param name="alpha">透明度</param>
        /// <param name="back">背景颜色</param>
        /// <param name="border">边框颜色</param>
        public OpacityPanel(int alpha, Color back, Color border)
        {
            this._alpha = alpha;
            this._bkcolor = back;
            this._bdcolor = border;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
            //this.MouseLeave += new System.EventHandler(MyMouseLeave);
            //this.MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
        }

        #endregion

        #region 私有函数


        #endregion

        #region 重载函数

        #region 开启 WS_EX_TRANSPARENT,使控件支持透明
        /// <summary>
        /// 开启 WS_EX_TRANSPARENT,使控件支持透明
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }
        #endregion

        #region 不绘制背景
        protected override void OnPaintBackground(PaintEventArgs paintg)
        {
            //不绘制背景
        }
        #endregion

        #region 绘制图形
        protected override void OnPaint(PaintEventArgs e)
        {           
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics bufg = Graphics.FromImage(bmp);
            bufg.DrawRectangle(new Pen(Color.FromArgb(this._alpha, this._bdcolor)), new Rectangle(0, 0, this.Size.Width, this.Size.Height));
            bufg.FillRectangle(new SolidBrush(Color.FromArgb(this._alpha, this._bkcolor)), 0, 0, this.Size.Width, this.Size.Height); //绘制背景
            e.Graphics.DrawImage(bmp, 0, 0);
            bufg.Dispose();
            bmp.Dispose();
        }

        #endregion

        #endregion

     }

    /// <summary>
    /// 光标状态
    /// </summary>
    enum EnumMousePointPosition
    {
        MouseSizeNone = 0, //'无   
        MouseSizeRight = 1, //'拉伸右边框   
        MouseSizeLeft = 2, //'拉伸左边框   
        MouseDrag = 9 // '鼠标拖动   
    }
}
