
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    [DefaultEvent("Click")]//默认为Click事件
    public partial class ButtonEx : UserControl,IButtonControl
    {
        #region 变量

        //三种不同状态下的图片
        Image _normalImage = null;
        Image _moveImage = null;
        Image _downImage = null;

        private DialogResult myDialogResult;

        #endregion

        #region 属性

        [Category("NormalImage")]
        public Image NormalImage
        {
            get
            {
                return _normalImage;
            }
            set
            {
                _normalImage = value;
            }
        }

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

        [Browsable(true), Category("MoveImage")]//Browsable(false) 隐藏属性
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
                return label_Button.Text;//控件运行时会自动运行get方法得到值
            }
            set
            {
                label_Button.Text = value;
            }
        }

        public DialogResult DialogResult
        {
            get
            {
                return myDialogResult;
            }

            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    myDialogResult = value;
                }
            }
        }

        #endregion

        #region 构造函数

        public ButtonEx()
       {
           //第一句是开始了窗体的双缓冲。双缓冲的意思就是现在内容中将图像画好了然后再显示到界面上去。
           //在c#中图像一多最怕的就是图像闪烁的问题，开启了双缓冲虽说不能完全避免图像闪烁，但起码也能有一定的效果，我们就先开着吧^ ^！
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //默认的是自带的图片样式，如果使用该按钮则必须手工指定当前按钮你想要的背景图片
            //_normalImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(@"WindowsFormsControlLibrary1.Skin.1.png"));

            _normalImage = Skin.Properties.Resources._button01;
           //这句话前面的照写，后面的路径规则是“命名空间+文件夹名+你的文件名+文件名后缀
            _moveImage = Skin.Properties.Resources._button02;
            _downImage = Skin.Properties.Resources._button03;

            MakeTransparent(_normalImage);
            MakeTransparent(_moveImage);
            MakeTransparent(_downImage);
            
           InitializeComponent();
           BackgroundImage= _normalImage;
       }

        #endregion

        #region Method -- 方法
        public void NotifyDefault(bool value)
        {

        }
        public void PerformClick()
        {
            if (CanSelect)
            {
                OnClick(EventArgs.Empty);
            }
        }
        public void Init()
        {
            _normalImage = Skin.Properties.Resources._button01;
            //这句话前面的照写，后面的路径规则是“命名空间+文件夹名+你的文件名+文件名后缀
            _moveImage = Skin.Properties.Resources._button02;
            _downImage = Skin.Properties.Resources._button03;

            MakeTransparent(_normalImage);
            MakeTransparent(_moveImage);
            MakeTransparent(_downImage);
            BackgroundImage = _normalImage;
        }
        private void MakeTransparent(Image image)
        {
            var bitmap = image as Bitmap;
            if (bitmap != null) bitmap.MakeTransparent(Color.FromArgb(255, 0, 255));
        }
        #endregion

        #region Event -- 事件

        private void label_Button_MouseEnter(object sender, EventArgs e)
        {
            BackgroundImage = _moveImage;
        }

        private void label_Button_MouseDown(object sender, MouseEventArgs e)
        {
            BackgroundImage = _downImage;
        }

        private void label_Button_MouseLeave(object sender, EventArgs e)
        {
            BackgroundImage = _normalImage;
        }

        private void label_Button_MouseUp(object sender, MouseEventArgs e)
        {
            BackgroundImage = _normalImage;
        }

        private void label_Button_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
        #endregion
    }
}
