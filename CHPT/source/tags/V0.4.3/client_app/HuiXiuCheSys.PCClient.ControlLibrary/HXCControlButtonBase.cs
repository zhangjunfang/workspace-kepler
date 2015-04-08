using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace HuiXiuCheSys.PCClient.ControlLibrary
{
    public class HXCControlButtonBase
    {
        public static Image BgImg { get; set; }
        public static Image HoverImg { get; set; }
        public static Image DownImg { get; set; }
        public static int _buttonWidth = 100;
        public static int _buttonHeight = 50;
        public static int ButtonWidth { get { return _buttonWidth; } set { _buttonWidth = value; } }
        public static int ButtonHeight { get { return _buttonHeight; } set { _buttonHeight = value; } }
        public static Font ControlCaptionText { get; set; }
        public static Color _backColor = System.Drawing.Color.FromArgb(Color.Black.ToArgb());
        public static Color _foreColor = System.Drawing.Color.FromArgb(Color.Red.ToArgb());
        public static Color BackColor { get { return _backColor; } set { _backColor = value; } }
        public static Color ForeColor { get { return _foreColor; } set { _foreColor = value; } }
        public static BorderStyle ControlBorderStyle { get; set; }
        public static bool _useVisualStyleBackColor = false;
        public static bool UseVisualStyleBackColor { get { return _useVisualStyleBackColor; } set { _useVisualStyleBackColor = value; } }
        private static HXCControlButtonBase _instance;
        // Lock synchronization object
        private static readonly object _syncLock = new object();
        // Constructor is 'private'
        private HXCControlButtonBase()
        {
        }
        public static HXCControlButtonBase Instance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new HXCControlButtonBase();
                    }
                }
            }
            return _instance;
        }
    }
}
