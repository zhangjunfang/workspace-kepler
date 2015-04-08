using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.Skin;

namespace ServiceStationClient.ComponentUI.TextBox
{
    

    [DefaultEvent("ChooserClick")]
    public partial class TextChooser : UserControl
    {
        
        [Browsable(true),Description("选择事件")]
        public event EventHandler ChooserClick;
        public TextChooser()
        {
            InitializeComponent();
            //btnChooser.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.PartTypChBtn;
            ChooserTypeImage = ChooserType.Default;
            BtnChooser.Image = Skin.Properties.Resources.Chooser_Default;
        }

        [Browsable(true),Description("文件本属性")]
        public override string Text
        {
            get { return txtText.Caption; }
            set { txtText.Caption = value;}
        }

        [Browsable(true),Description("只读属性")]
        public bool ReadOnly
        {
            get { return txtText.ReadOnly; }
            set { txtText.ReadOnly = value; }
        }

        //[Browsable(true), Description("控件右侧显示图标属性")]
        //public new Image BackgroundImage
        //{
        //    get
        //    {
        //        return BtnChooser.BackgroundImage;
        //    }
        //    set
        //    {
        //        BtnChooser.BackgroundImage = value;
        //    }
        //}
        private ChooserType _chooserTypeImage;
        [Browsable(true), Description("选择器图标类型")]
        public ChooserType ChooserTypeImage
        {
            get { return _chooserTypeImage; }
            set
            {
                if (_chooserTypeImage == value) return;
                _chooserTypeImage = value;
                switch (_chooserTypeImage)
                {
                    case ChooserType.Company:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Company;
                        break;
                    case ChooserType.Contact:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Contact;
                        break;
                    case ChooserType.Default:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Default;
                        break;
                    case ChooserType.Part:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Part;
                        break;
                    case ChooserType.PartCode:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Part_Code;
                        break;
                    case ChooserType.PartType:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Part_Type;
                        break;
                    case ChooserType.Supplier:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Supplier;
                        break;
                    case ChooserType.User:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_User;
                        break;
                    case ChooserType.Vehicle:
                        BtnChooser.Image = Skin.Properties.Resources.Chooser_Vehicle;
                        break;
                }
            }
        }

        [Browsable(true), Description("控件显示汽泡文本属性")]
        public string ToolTipTitle
        {
            get
            {
                return toolTip1.ToolTipTitle;
            }
            set
            {
                toolTip1.ToolTipTitle = value;
            }
        }

        private void BtnChooser_Click(object sender, EventArgs e)
        {
            if (ChooserClick != null && BtnChooser.Enabled)
            {
                ChooserClick(sender, e);
            }

        }

        private void BtnChooser_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(BtnChooser, ToolTipTitle);
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseFading = true;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
            this.toolTip1.Active = true;

        }
    }

    /// <summary>
    /// 选择器类型
    /// </summary>
    public enum ChooserType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 往来单位
        /// </summary>
        Company,
        /// <summary>
        /// 联系人
        /// </summary>
        Contact,
        /// <summary>
        /// 配件
        /// </summary>
        Part,
        /// <summary>
        /// 配件编码
        /// </summary>
        PartCode,
        /// <summary>
        /// 配件类型
        /// </summary>
        PartType,
        /// <summary>
        /// 供应商
        /// </summary>
        Supplier,
        /// <summary>
        /// 用户
        /// </summary>
        User,
        /// <summary>
        /// 车型
        /// </summary>
        Vehicle,
    }
}
