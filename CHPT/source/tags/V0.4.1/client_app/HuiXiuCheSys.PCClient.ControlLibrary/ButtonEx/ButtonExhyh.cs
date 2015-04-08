using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuiXiuCheSys.PCClient.ControlLibrary.ButtonEx
{
     [System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.Button))]
    public partial class ButtonExhyh : System.Windows.Forms.Button
    {
         public ButtonExhyh()
             : base()
        {            
            InitializeComponent();       
        }
         private System.Drawing.Color _backColor = System.Drawing.SystemColors.Window;
         [System.ComponentModel.DefaultValueAttribute(typeof(System.Drawing.Color), "Window"), System.ComponentModel.CategoryAttribute("Appearance"), System.ComponentModel.DescriptionAttribute("The primary background color used to display text and graphics in the control.")]
         public new System.Drawing.Color BackColor
         {
             get
             {
                 if (HXCControlButtonBase.BackColor == null)
                 {
                     return System.Drawing.Color.FromArgb(_backColor.ToArgb());
                 }
                 else
                 {
                     return System.Drawing.Color.FromArgb(HXCControlButtonBase.BackColor.ToArgb());
                 }
             }
             set
             {
                 if (HXCControlButtonBase.BackColor == null)
                 {
                     this._backColor = value;
                 }
                 else
                 {
                     this._backColor = HXCControlButtonBase.BackColor;
                 }
                 if (this.DesignMode == true)
                 {
                     this.Invalidate();
                 }
             }
         }

         private System.Drawing.Color _foreColor = System.Drawing.SystemColors.Window;
         [System.ComponentModel.DefaultValueAttribute(typeof(System.Drawing.Color), "Window"), System.ComponentModel.CategoryAttribute("Appearance"), System.ComponentModel.DescriptionAttribute("The primary background color used to display text and graphics in the control.")]
         public new System.Drawing.Color ForeColor
         {
             get
             {
                 if (HXCControlButtonBase.ForeColor == null)
                 {
                     return _foreColor;
                 }
                 else
                 {
                     return HXCControlButtonBase.ForeColor;
                 }
             }
             set
             {
                 if (HXCControlButtonBase.ForeColor == null)
                 {
                     this._foreColor = value;
                 }
                 else
                 {
                     this._foreColor = HXCControlButtonBase.ForeColor;
                 }
                 if (this.DesignMode == true)
                 {
                     this.Invalidate();
                 }
             }
         }
         private int _height;
         public new int Height
         {
             get
             {
                 if (HXCControlButtonBase.ButtonHeight > 0)
                 {
                     return HXCControlButtonBase.ButtonHeight;
                 }
                 else
                 {
                     return _height;
                 }
             }
             set
             {
                 if (HXCControlButtonBase.ButtonHeight > 0)
                 {
                     this._height = HXCControlButtonBase.ButtonHeight;
                 }
                 else
                 {
                     this._height = value;
                 }
                 if (this.DesignMode == true)
                 {
                     this.Invalidate();
                 }
             }
         }
         private int _width;
         public new int Width
         {
             get
             {
                 if (HXCControlButtonBase.ButtonWidth > 0)
                 {
                     return HXCControlButtonBase.ButtonWidth;
                 }
                 else
                 {
                     return _width;
                 }
             }
             set
             {
                 if (HXCControlButtonBase.ButtonWidth > 0)
                 {
                     this._width = HXCControlButtonBase.ButtonWidth;
                 }
                 else
                 {
                     this._width = value;
                 }
                 if (this.DesignMode == true)
                 {
                     this.Invalidate();
                 }
             }
         }
         private bool _mouseEntered;
         protected override void OnPaint(PaintEventArgs e)
         {
             base.OnPaint(e);
             base.OnPaintBackground(e);
             //将文本绘制到正中央
             e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
             SizeF stringSize = e.Graphics.MeasureString(Text, Font);
             float startx = (Width - stringSize.Width) / 2;
             float starty = (Height - stringSize.Height) / 2;
             e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), startx, starty);
             //this.InvokePaintBackground(this, e);


         }

         protected override void OnPaintBackground(PaintEventArgs pevent)
         {
             base.OnPaintBackground(pevent);
             ////绘制过程为 控件整体背景色->控件有效区背景色->控件状态表示区域->控件外框 
             ////**********
             ////控件整体背景色
             ////**********
             if (this != null)
                 pevent.Graphics.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
             ////使用高质量平滑模式消除椭圆边缘锯齿
             //pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality; 
             ////***********
             ////控件有效区背景色
             ////************** Control.MouseButtons静态成员
             //if (_mouseEntered && (MouseButtons & MouseButtons.Left) == MouseButtons.Left)
             //{
             //    Color mouseDownColor = Color.FromArgb(128, BackColor);
             //    pevent.Graphics.FillEllipse(new SolidBrush(mouseDownColor), 0, 0, Width - 1, Height - 1);
             //}
             //else
             //    pevent.Graphics.FillEllipse(new SolidBrush(BackColor), 0, 0, Width - 1, Height - 1); 
             ////***********
             ////控件状态表示区域
             ////************
             ////左键未按下时绘制状态表示区域
             //if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left)
             //{
             //    //鼠标进入 绘制橙色边框
             //    if (_mouseEntered)
             //    {
             //        Pen mouseEnterPen = new Pen(Color.Orange, 2);
             //        pevent.Graphics.DrawEllipse(mouseEnterPen, 1, 1, Width - 3, Height - 3);
             //        mouseEnterPen.Dispose();
             //    }
             //    //控件获得焦点 但鼠标未进入 绘制蓝色边框
             //    else if (Focused)
             //    {
             //        Pen focusedPen = new Pen(Color.PowderBlue, 2);
             //        pevent.Graphics.DrawEllipse(focusedPen, 1, 1, Width - 3, Height - 3);
             //        focusedPen.Dispose();
             //    }
             //}
             ////如果有焦点，绘制焦点框
             //if (Focused)
             //{
             //    Pen focusedDotPen = new Pen(Color.Black);
             //    focusedDotPen.DashStyle = DashStyle.Dot;
             //    pevent.Graphics.DrawEllipse(focusedDotPen, 3, 3, Width - 7, Height - 7);
             //    focusedDotPen.Dispose();
             //} 
             ////*********
             ////控件外框
             ////**********
             //pevent.Graphics.DrawEllipse(Pens.Black, 0, 0, Width - 1, Height - 1);
             //

         }   
    }
}
