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
    public partial class ImageButton : UserControl, IButtonControl
    {
        public enum Status 
        {
            Normal,
            Hover,
            Down     
        }
        public bool IngoreLeave { get; set; }
        Image normalImage = null;
        Image hoverImage = null;
        Image downImage = null;
        private DialogResult dialogResult;
        private Status buttonStatus = Status.Normal;
        private bool isClick = false;
        public Status ButtonStatus 
        {
            get { return buttonStatus; }
            set
            {
                buttonStatus = value;
                if (buttonStatus == Status.Normal) 
                {
                    isClick = false;
                }
                ChangeBackground(buttonStatus);
            }
        }

        public Image ImageNormal 
        {
            get { return normalImage; }
            set
            { 
                normalImage = value; 
            }
        }

        public Image ImageHover
        {
            get { return hoverImage; }
            set
            { 
                hoverImage = value;
            }
        }

        public Image ImageDown
        {
            get { return downImage; }
            set { downImage = value; }
        }


        public ImageButton()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ServiceStationClient.Skin.Properties.Resources._button01;
        }

        public DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }

            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    dialogResult = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
         
        }

        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnClick(EventArgs.Empty);
            }
        }
        private void ImageButton_MouseEnter(object sender, EventArgs e)
        {
            //if (IngoreLeave && isClick)
            //{
            //    // Not change background
            //}
            //else
            //{
                this.ButtonStatus = Status.Hover;
            //}  
            
        }

        private void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.ButtonStatus = Status.Down; 
        }

        private void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            //if (IngoreLeave && isClick)
            //{
            //    // Not change background
            //}
            //else
            //{
                this.ButtonStatus = Status.Normal;  
            //}      
        }

        private void ChangeBackground(Status btnStatus) 
        {
            Image backImage = normalImage;
            switch (btnStatus)
            {
                case Status.Normal:
                    backImage = normalImage;
                    break;
                case Status.Hover:
                    backImage = hoverImage;
                    break;
                case Status.Down:
                    //isClick = true;
                    backImage = downImage;
                    break;
            }
            if (backImage != null)
            {
                this.BackgroundImage = backImage;
            }
        }
        
    }
}
