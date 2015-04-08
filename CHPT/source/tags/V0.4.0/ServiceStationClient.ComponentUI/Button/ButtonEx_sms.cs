/*----------------------------------------------------------------
 * Copyright (C) 2012 北京中交慧联信息科技有限公司 版权所有。
 * 保留所有权利

 * 文件名称: ButtonEx_sms.cs 
 * 编程语言: C# 
 * 文件说明: 自定义按钮Button
 * 实现IButtonControl接口，是为了窗体能够接收默认回车及Esc事件，实现方法及事件有DialogResult,NotifyDefault,PerformClick

 * 功能: 
 * 自定义按钮Button

 * 当前版本: 1.0.0.0
 * 替换版本：

 * 创建人: 孙明生 
 * EMail: sunmingsheng@ctfo.com
 * 创建日期: 2014-11-02
 * 最后修改日期: 

 * 历史修改记录: 
 * 修改人：
 * 修改时间: 
 * 修改内容: 
 * 1.
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    [DefaultEvent("Click")]//默认为Click事件
    public partial class ButtonEx_sms : UserControl,IButtonControl
    {
        #region 变量

        //三种不同状态下的图片
        Image _normalImage = null;
        Image _moveImage = null;
        Image _downImage = null;
        Image _icon = null;
        private DialogResult myDialogResult;

        #endregion

        #region 属性
        [Category("图标")]
        public Image Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                IconChange();
            }
        }
        //[Category("NormalImage")]
        //public Image NormalImage
        //{
        //    get
        //    {
        //        return _normalImage;
        //    }
        //    set
        //    {
        //        _normalImage = value;
        //    }
        //}

        [Category("DownImage")]
        public Image DownImage
        {
            get
            {
                return _downImage;
            }
            set
            {
                _downImage = value;
            }
        }

        [Browsable(false), Category("MoveImage")]//Browsable(false) 隐藏属性
        public Image MoveImage
        {
            get
            {
                return _moveImage;
            }
            set
            {
                _moveImage = value;
            }
        }

        [Description("显示的文字"), Category("Caption")]
        public string Caption
        {
            get
            {
                return this.label_Button.Text;//控件运行时会自动运行get方法得到值
            }
            set
            {
                this.label_Button.Text = value;
            }
        }

        public DialogResult DialogResult
        {
            get
            {
                return this.myDialogResult;
            }

            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    this.myDialogResult = value;
                }
            }
        }

        #endregion

        #region 构造函数

        public ButtonEx_sms()
       {
           //第一句是开始了窗体的双缓冲。双缓冲的意思就是现在内容中将图像画好了然后再显示到界面上去。
           //在c#中图像一多最怕的就是图像闪烁的问题，开启了双缓冲虽说不能完全避免图像闪烁，但起码也能有一定的效果，我们就先开着吧^ ^！
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //默认的是自带的图片样式，如果使用该按钮则必须手工指定当前按钮你想要的背景图片
            //_normalImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(@"WindowsFormsControlLibrary1.Skin.1.png"));

           // _normalImage = ServiceStationClient.Skin.Properties.Resources._button01;
           //这句话前面的照写，后面的路径规则是“命名空间+文件夹名+你的文件名+文件名后缀
            _moveImage = ServiceStationClient.Skin.Properties.Resources._button01;
            _downImage = ServiceStationClient.Skin.Properties.Resources._button03;
            _icon = ServiceStationClient.Skin.Properties.Resources.page_down_down;
            //MakeTransparent(_normalImage);
            MakeTransparent(_moveImage);
            MakeTransparent(_downImage);
            
           InitializeComponent();
           // this.BackgroundImage= _normalImage;
           this.pic_box.Image = _icon;
       }

        #endregion

        #region 辅助函数

        private void MakeTransparent(Image image)
        {
            Bitmap bitmap = image as Bitmap;
            bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
            //这个函数的作用是使指定的颜色对 Bitmap 透明。也就是说我们只要将这个函数的Color设置为我们需要去掉的粉红色不就行了?!
        }

        #endregion

        #region 事件

        private void label_Button_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = _moveImage;
        }

        private void label_Button_MouseDown(object sender, MouseEventArgs e)
        {
            var cSender = sender as Control;
            if (cSender != null)
            {
                ForeColor = cSender.ForeColor = Color.White;
            }
            this.BackgroundImage = _downImage;
        }

        private void label_Button_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = _normalImage;
        }

        private void label_Button_MouseUp(object sender, MouseEventArgs e)
        {
            var cSender = sender as Control;
            if (cSender != null)
            {
                ForeColor = cSender.ForeColor = Color.Black;
            }
            this.BackgroundImage = _normalImage;
        }

        private void label_Button_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }      

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = _downImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = _moveImage;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = _normalImage;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = _normalImage;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        public void NotifyDefault(bool value)
        {

            //if (this.IsDefault != value)
            //{
            //    this.IsDefault = value;
            //}
        }

        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnClick(EventArgs.Empty);
            }
        }

        public void Init()
        {
           // _normalImage = ServiceStationClient.Skin.Properties.Resources._button01;
            //这句话前面的照写，后面的路径规则是“命名空间+文件夹名+你的文件名+文件名后缀
            _moveImage = ServiceStationClient.Skin.Properties.Resources._button01;
            _downImage = ServiceStationClient.Skin.Properties.Resources._button03;
            _icon = ServiceStationClient.Skin.Properties.Resources.page_down_down;
            //MakeTransparent(_normalImage);
            MakeTransparent(_moveImage);
            MakeTransparent(_downImage);
           // this.BackgroundImage = _normalImage;
            this.pic_box.Image = _icon;
        }
        #endregion

        private void ButtonEx_sms_Load(object sender, EventArgs e)
        {
            IconChange();
        }

        private void IconChange()
        {
            this.pic_box.Image = _icon;
        }

      

        //public ZJXLbutton()
        //{
        //    InitializeComponent();
        //}
    }
}
