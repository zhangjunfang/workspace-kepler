using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System;

namespace HXCPcClient.UCMainLayOut
{
    public partial class FormMenu : Form
    {
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //下面是可用的常量，根据不同的动画效果声明自己需要的  
        private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志  
        private const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志        
      
        private const int AW_HIDE = 0x10000;//隐藏窗口  
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志  
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略        

        Control parent;
        DockStyle dock;
        int x = 0;
        int y = -2000;
        private bool close = true;
        //private bool cancel = false;
        public FormMenu(int height, Control menu)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.panelTop.Visible = false;
            this.BringToFront();
            this.Height = height;
            this.Width = menu.Width;
            this.parent = menu.Parent;
            this.dock = menu.Dock;
            this.panelMain.Controls.Add(menu);
            menu.Dock = DockStyle.Fill;
            this.Location = new Point(this.x, this.y);
        }

        private void FormMenu_Deactivate(object sender, System.EventArgs e)
        {
            if (close)
            {
                this.close = false;
                this.Close();                
            }
        }

        public void AnimateShow(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.Show();
            this.Location = new Point(x, y);
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.x, this.y);
            AnimateWindow(this.Handle, 800, AW_SLIDE | AW_ACTIVE | AW_HOR_POSITIVE);
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 800, AW_SLIDE | AW_HIDE | AW_HOR_NEGATIVE);
            //if (this.cancel)
            //{
            //    this.cancel = false;
            //    AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_HOR_POSITIVE);                
            //    return;
            //}
            if (this.parent != null)
            {
                Control menu = this.panelMain.Controls[0];
                this.parent.Controls.Add(menu);
                menu.Dock = this.dock;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //鼠标位置
            Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标
            Point thisPoint = this.PointToScreen(new Point(0, 0));
            Rectangle rect = new Rectangle(thisPoint.X - 20, thisPoint.Y - 20, this.Width + 5, this.Height + 5);
            if (!rect.Contains(screenPoint))
            {
                this.FormMenu_Deactivate(null, null);
            }
        }
    }
}
