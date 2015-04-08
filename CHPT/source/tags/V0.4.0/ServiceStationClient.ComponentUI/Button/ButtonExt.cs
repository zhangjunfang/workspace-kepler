using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public class ButtonExt : Button
    {
        #region Contactor -- 构造函数
        public ButtonExt()
        {
            Init();
            InitBgImage();
        }
        #endregion

        #region Property -- 属性
        private Image _normarlImage = Skin.Properties.Resources._button01;
        /// <summary>
        /// 默认背景图片
        /// </summary>
        public Image NormalImage
        {
            get { return _normarlImage; }
            set { _normarlImage = value; }
        }

        private Image _downImage = Skin.Properties.Resources._button02;
        /// <summary>
        /// 点击背景图片
        /// </summary>
        public Image DownImage
        {
            get { return _downImage; }
            set { _downImage = value; }
        }

        private Image _moveImage = Skin.Properties.Resources._button03;
        /// <summary>
        /// 鼠标悬浮背景图片
        /// </summary>
        public Image MoveImage
        {
            get { return _moveImage; }
            set { _moveImage = value; }
        }
        #endregion

        #region Method -- 方法
        private void Init()
        {
            
        }
        private void InitBgImage()
        {
            MouseEnter += delegate
            {
                BackgroundImage = _moveImage;
            };
            MouseDown += delegate
            {
                BackgroundImage = _downImage;
            };
            MouseLeave += delegate
            {
                BackgroundImage = _normarlImage;
            };
            MouseUp += delegate
            {
                BackgroundImage = _normarlImage;
            };
        }
        #endregion
    }
}
