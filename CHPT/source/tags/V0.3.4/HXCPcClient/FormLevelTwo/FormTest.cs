using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient.FormLevelTwo
{
    public partial class FormTest : Form
    {
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        public FormTest()
        {
            InitializeComponent();
            //this.Opacity = 0.2;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息   
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标   
                    m.LParam = IntPtr.Zero;//默认值   
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            //for (int i = 1; i < 10000000; i++)
            //{
            //    if (i % 1000000 == 0 && i > 1000000)
            //    {
            //        this.Opacity += 0.1;
            //    }
            //}
        }

        //Show或Hide被调用时
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                //启用窗口淡入淡出
                if (!DesignMode)
                {
                    //淡入特效
                    FormWin32.AnimateWindow(this.Handle, 800, FormWin32.AW_BLEND | FormWin32.AW_ACTIVATE);
                }
                //判断不是在设计器中

                base.OnVisibleChanged(e);
            }
            else
            {
                base.OnVisibleChanged(e);
                FormWin32.AnimateWindow(this.Handle, 800, FormWin32.AW_BLEND | FormWin32.AW_HIDE);
            }
        }

        //窗体关闭时
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //在Form_FormClosing中添加代码实现窗体的淡出
            FormWin32.AnimateWindow(this.Handle, 800, FormWin32.AW_BLEND | FormWin32.AW_HIDE);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
