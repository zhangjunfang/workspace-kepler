using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HXCServerWinForm
{
    public static class ClassStyles
    {
        public static readonly Int32
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_SAVEBITS = 0x0800,
        CS_VREDRAW = 0x0001;
    }

    public enum MouseDirection
    {
        Herizontal,//水平方向拖动，只改变窗体的宽度
        Vertical,//垂直方向拖动，只改变窗体的高度
        Declining,//倾斜方向，同时改变窗体的宽度和高度
        None//不做标志，即不拖动窗体改变大小
    } 
}
