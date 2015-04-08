using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HuiXiuCheSys.PCClient.ControlLibrary
{
   public class HXCControlImgButtonBase
    {
        public static Image NormalImg { get; set; }
        public static Image HoverImg { get; set; }
        public static Image DownImg { get; set; }
        public static int ButtonWidth { get; set; }
        public static int ButtonHeight { get; set; }        
        public static bool UseVisualStyleBackColor { get; set; }
        private static HXCControlImgButtonBase _instance;
        private static readonly object _syncLock = new object();
        private HXCControlImgButtonBase()
        {
        }
        public static HXCControlImgButtonBase Instance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new HXCControlImgButtonBase();
                    }
                }
            }
            return _instance;
        }
    }
}
