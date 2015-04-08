using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ServiceStationClient.ComponentUI.TextBox
{

    [DefaultEvent("ChooserClick")]
    public partial class TextChooser : UserControl
    {
        private bool choserFlag = true;
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="text">sql语句</param>
        public delegate void GetDataSourceHandler(TextChooser tc, string sqlString);
        public event GetDataSourceHandler GetDataSourced;

        public delegate void DataBackHandler(DataRow dr);
        public event DataBackHandler DataBacked;

        private SelectWndEx selectWnd;

        [Browsable(true), Description("选择事件")]
        public event EventHandler ChooserClick;
        public TextChooser()
        {
            InitializeComponent();
            //btnChooser.BackgroundImage = ServiceStationClient.Skin.Properties.Resources.PartTypChBtn;
            ChooserTypeImage = ChooserType.Default;
            BtnChooser.Image = Skin.Properties.Resources.Chooser_Default;
        }

        [Browsable(true), Description("文件本属性")]
        public override string Text
        {
            get { return txtText.Caption; }
            set { txtText.Caption = value; }
        }

        [Browsable(true), Description("只读属性")]
        public bool ReadOnly
        {
            get { return txtText.ReadOnly; }
            set { txtText.ReadOnly = value; }
        }

        public new bool Enabled
        {
            set
            {
                BtnChooser.Enabled = value;
                txtText.ReadOnly = !value;
            }
            get { return BtnChooser.Enabled; }
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
            if (this.ChooserClick != null && this.BtnChooser.Enabled)
            {
                this.choserFlag = false;
                this.ChooserClick(sender, e);
                this.choserFlag = true;
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

        #region --回调方法
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="sqlString"></param>
        private void GetDataSource(string sqlString)
        {
            if (this.GetDataSourced != null)
            {
                this.GetDataSourced(this, sqlString);
            }
        }
        /// <summary>
        /// 选中数据后
        /// </summary>
        /// <param name="selectedText"></param>
        private void AfterSelect(string selectedText)
        {
            this.txtText.InnerTextBox.Text = selectedText;
        }
        /// <summary>
        /// 选中数据后
        /// </summary>
        /// <param name="dr"></param>
        private void Mluti_AfterSelect(DataRow dr)
        {
            if (this.DataBacked != null)
            {
                this.DataBacked(dr);
            }
        }
        #endregion

        public void SetDataSource(DataTable dt)
        {
            this.selectWnd.DataSource = dt;
        }
        public void Search()
        {
            string text = this.txtText.InnerTextBox.Text.Trim();
            this.selectWnd.Search(text);

        }
        /// <summary>
        /// 设置表和显示字段
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="column">字段</param>
        public void SetBindTable(string table, string column)
        {
            this.SetBindTable(table, column, column);
        }
        /// <summary>
        /// 设置表和显示字段
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="displayColumn">文本框中要显示的字段</param>
        /// <param name="valueColumn">实际检索的字段</param>
        public void SetBindTable(string table, string displayColumn, string valueColumn)
        {
            if (table.Length > 0 && displayColumn.Length > 0 && valueColumn.Length > 0)
            {
                this.selectWnd = new SelectWndEx(table, displayColumn, valueColumn, this.txtText.InnerTextBox);

                this.selectWnd.GetDataSourced -= new SelectWndEx.GetDataSourceHandler(this.GetDataSource);
                this.selectWnd.GetDataSourced += new SelectWndEx.GetDataSourceHandler(this.GetDataSource);

                this.selectWnd.AfterSeleted -= new SelectWndEx.AfterSeletedHandler(this.AfterSelect);
                this.selectWnd.AfterSeleted += new SelectWndEx.AfterSeletedHandler(this.AfterSelect);

                this.selectWnd.Multi_AfterSeleted -= new SelectWndEx.Multi_AfterSeletedHandler(this.Mluti_AfterSelect);
                this.selectWnd.Multi_AfterSeleted += new SelectWndEx.Multi_AfterSeletedHandler(this.Mluti_AfterSelect);
            }
        }

        #region --快捷键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.txtText.InnerTextBox.Focused && keyData == Keys.Enter)
            {
                BtnChooser_Click(this.BtnChooser, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
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
