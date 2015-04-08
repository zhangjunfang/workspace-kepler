using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace ServiceStationClient.ComponentUI
{
    public partial class ComboBoxEx : System.Windows.Forms.ComboBox
    {
        Color _borderColor = Color.FromArgb(166, 208, 226);//边框颜色
        public ComboBoxEx()
        {
            InitializeComponent();
        }

        public ComboBoxEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        //protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
        //{
        //    base.OnMeasureItem(e);
        //    e.ItemHeight = e.ItemHeight - 2;
        //}

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index < 0)
            {
                return;
            }
            float size = this.Font.Size;
            System.Drawing.Font myFont;
            System.Drawing.FontFamily family = this.Font.FontFamily;

            System.Drawing.Color animalColor = this.ForeColor;

            // Draw the background of the item.
            e.DrawBackground();

            // Create a square filled with the animals color. Vary the size
            // of the rectangle based on the length of the animals name.
            //System.Drawing.Rectangle rectangle = new Rectangle(2, e.Bounds.Top + 2,
            //        e.Bounds.Height, e.Bounds.Height - 4);
            //e.Graphics.FillRectangle(new SolidBrush(animalColor), rectangle);

            // Draw each string in the array, using a different size, color,
            // and font for each item.
            myFont = new Font(family, size, FontStyle.Regular);
            
            e.Graphics.DrawString(this.GetItemText(Items[e.Index]), myFont, System.Drawing.Brushes.Black, new PointF(e.Bounds.X + 5, e.Bounds.Y + 2));
            //new RectangleF(e.Bounds.X + e.Bounds.Height, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            //弹出下拉
            //if (e.State == (System.Windows.Forms.DrawItemState.NoAccelerator | System.Windows.Forms.DrawItemState.NoFocusRect))
            //{
            //    e.Graphics.DrawRectangle(new Pen(_borderColor), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            //}
            //else//选择时
            //{
            //    e.Graphics.DrawRectangle(new Pen(_borderColor), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            //}
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }


        protected new void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            #region 画边框
            Rectangle rectangle = e.ClipRectangle;
            Graphics g = e.Graphics;


            using (Pen borderPen = new Pen(_borderColor))
            {
                g.DrawRectangle(borderPen, new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 3, rectangle.Height - 3));

            }
            #endregion
        }
    }
}
