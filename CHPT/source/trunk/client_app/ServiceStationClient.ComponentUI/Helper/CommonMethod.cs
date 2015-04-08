using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace ServiceStationClient.ComponentUI
{
    public class CommonMethod
    {
        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            try
            {
                if (form == null)
                {
                    MessageBox.Show("程序闪退调试记录");
                    return;
                }
                int hRgn = 0;
                hRgn = Win32API.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                Win32API.SetWindowRgn(form.Handle, hRgn, true);
                Win32API.DeleteObject(hRgn);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "程序闪退调试记录");
            }
        }

        /// <summary>
        /// 取低位 X 坐标
        /// </summary>
        public static int LOWORD(int value)
        {
            return value & 0xFFFF;
        }

        /// <summary>
        /// 取高位 Y 坐标
        /// </summary>
        public static int HIWORD(int value)
        {
            return value >> 16;
        }

        /// <summary>
        /// 移动窗体
        /// </summary>
        public static void MoveWindow(Form form)
        {
            Win32API.ReleaseCapture();
            Win32API.SendMessage(form.Handle, Win32API.WM_NCLBUTTONDOWN, Win32API.HTCAPTION, 0);
        }


        /// <summary>
        /// 利用九宫图绘制图像
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <param name="img">所需绘制的图片</param>
        /// <param name="targetRect">目标矩形</param>
        /// <param name="srcRect">来源矩形</param>
        public static void DrawImageWithNineRect(Graphics g, Image img, Rectangle targetRect, Rectangle srcRect)
        {
            int offset = 5;
            Rectangle NineRect = new Rectangle(img.Width / 2 - offset, img.Height / 2 - offset, 2 * offset, 2 * offset);
            int x = 0, y = 0, nWidth, nHeight;
            int xSrc = 0, ySrc = 0, nSrcWidth, nSrcHeight;
            int nDestWidth, nDestHeight;
            nDestWidth = targetRect.Width;
            nDestHeight = targetRect.Height;
            // 左上-------------------------------------;
            x = targetRect.Left;
            y = targetRect.Top;
            nWidth = NineRect.Left - srcRect.Left;
            nHeight = NineRect.Top - srcRect.Top;
            xSrc = srcRect.Left;
            ySrc = srcRect.Top;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nWidth, nHeight, GraphicsUnit.Pixel);
            // 上-------------------------------------;
            x = targetRect.Left + NineRect.Left - srcRect.Left;
            nWidth = nDestWidth - nWidth - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            nSrcWidth = NineRect.Right - NineRect.Left;
            nSrcHeight = NineRect.Top - srcRect.Top;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nSrcWidth, nSrcHeight, GraphicsUnit.Pixel);
            // 右上-------------------------------------;
            x = targetRect.Right - (srcRect.Right - NineRect.Right);
            nWidth = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nWidth, nHeight, GraphicsUnit.Pixel);
            // 左-------------------------------------;
            x = targetRect.Left;
            y = targetRect.Top + NineRect.Top - srcRect.Top;
            nWidth = NineRect.Left - srcRect.Left;
            nHeight = targetRect.Bottom - y - (srcRect.Bottom - NineRect.Bottom);
            xSrc = srcRect.Left;
            ySrc = NineRect.Top;
            nSrcWidth = NineRect.Left - srcRect.Left;
            nSrcHeight = NineRect.Bottom - NineRect.Top;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nSrcWidth, nSrcHeight, GraphicsUnit.Pixel);
            // 中-------------------------------------;
            x = targetRect.Left + NineRect.Left - srcRect.Left;
            nWidth = nDestWidth - nWidth - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            nSrcWidth = NineRect.Right - NineRect.Left;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nSrcWidth, nSrcHeight, GraphicsUnit.Pixel);

            // 右-------------------------------------;
            x = targetRect.Right - (srcRect.Right - NineRect.Right);
            nWidth = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            nSrcWidth = srcRect.Right - NineRect.Right;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nSrcWidth, nSrcHeight, GraphicsUnit.Pixel);

            // 左下-------------------------------------;
            x = targetRect.Left;
            y = targetRect.Bottom - (srcRect.Bottom - NineRect.Bottom);
            nWidth = NineRect.Left - srcRect.Left;
            nHeight = srcRect.Bottom - NineRect.Bottom;
            xSrc = srcRect.Left;
            ySrc = NineRect.Bottom;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nWidth, nHeight, GraphicsUnit.Pixel);
            // 下-------------------------------------;
            x = targetRect.Left + NineRect.Left - srcRect.Left;
            nWidth = nDestWidth - nWidth - (srcRect.Right - NineRect.Right);
            xSrc = NineRect.Left;
            nSrcWidth = NineRect.Right - NineRect.Left;
            nSrcHeight = srcRect.Bottom - NineRect.Bottom;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nSrcWidth, nSrcHeight, GraphicsUnit.Pixel);
            // 右下-------------------------------------;
            x = targetRect.Right - (srcRect.Right - NineRect.Right);
            nWidth = srcRect.Right - NineRect.Right;
            xSrc = NineRect.Right;
            g.DrawImage(img, new Rectangle(x, y, nWidth, nHeight), xSrc, ySrc, nWidth, nHeight, GraphicsUnit.Pixel);
        }


        /// <summary>
        /// 绘制窗体边框
        /// </summary>
        /// <param name="destForm">需要绘制边框的窗体</param>
        /// <param name="g">绘制边框所用的绘图对象</param>
        /// <param name="fringeImg">边框图片</param>
        /// <param name="radius">边框的圆角矩形</param>
        public static void DrawFormFringe(Form destForm, Graphics g, Image fringeImg, int radius)
        {
            DrawImageWithNineRect(
                g,
                fringeImg,
                new Rectangle(-radius, -radius, destForm.ClientSize.Width + 2 * radius, destForm.ClientSize.Height + 2 * radius),
                new Rectangle(0, 0, fringeImg.Width, fringeImg.Height));
        }
    }
}
