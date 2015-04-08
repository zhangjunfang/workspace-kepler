using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

//添加新的命名空间
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ServiceStationClient.ComponentUI
{
    [DefaultEvent("PageIndexChanged"), DefaultProperty("RecordCount"), Description("WinForm下的分页控件"), ToolboxBitmap(typeof(WinFormPager), "Resources.Bitmap.pager")]
    public partial class WinFormPager : UserControl
    {
        #region Members
        private string _BtnTextFirst = "首页";
        private string _BtnTextPrevious = "上页";
        private string _BtnTextNext = "下页";
        private string _BtnTextLast = "末页";
        private DisplayStyleEnum _DisplayStyle = DisplayStyleEnum.图片;
        private string _JumpText = "跳转";

        private int _PageIndex = 1;
        private int _PageSize = 15;
        protected int _PageCount = 0;
        private int _RecordCount = 0;
        private TextImageRalitionEnum _TextImageRalition = TextImageRalitionEnum.图片显示在文字前方;
        private string PagerText = "当前{0}/{1}页 总数:{2}";
        //总共{0}条记录,当前第{1}页,共{2}页,每页{3}条记录
        public enum DisplayStyleEnum
        {
            图片 = 1,
            图片及文字 = 3,
            文字 = 2
        }

        public delegate void EventHandler(object sender, EventArgs e);

        public enum TextImageRalitionEnum
        {
            图片显示在文字后方 = 4,
            图片显示在文字前方 = 3,
            图片显示在文字上方 = 1,
            图片显示在文字下方 = 2
        }

        [Description("更改页面索引事件"), Category("自定义事件")]
        public event EventHandler PageIndexChanged;

        [Category("自定义属性"), DefaultValue("首页"), Description("首页按钮文字,当DisplayStyle=文字或DisplayStyle=图片及文字时生效")]
        public string BtnTextFirst
        {
            get
            {
                return this._BtnTextFirst;
            }
            set
            {
                this._BtnTextFirst = value;
                this.SetDisplayStyle();
            }
        }

        [Description("末页按钮文字,当DisplayStyle=文字或DisplayStyle=图片及文字时生效"), DefaultValue("末页"), Category("自定义属性")]
        public string BtnTextLast
        {
            get
            {
                return this._BtnTextLast;
            }
            set
            {
                this._BtnTextLast = value;
                this.SetDisplayStyle();
            }
        }

        [DefaultValue("下一页"), Category("自定义属性"), Description("下一页按钮文字,当DisplayStyle=文字或DisplayStyle=图片及文字时生效")]
        public string BtnTextNext
        {
            get
            {
                return this._BtnTextNext;
            }
            set
            {
                this._BtnTextNext = value;
                this.SetDisplayStyle();
            }
        }

        [DefaultValue("上一页"), Description("上一页按钮文字,当DisplayStyle=文字或DisplayStyle=图片及文字时生效"), Category("自定义属性")]
        public string BtnTextPrevious
        {
            get
            {
                return this._BtnTextPrevious;
            }
            set
            {
                this._BtnTextPrevious = value;
                this.SetDisplayStyle();
            }
        }

        [Category("自定义属性"), DefaultValue(1), Description("显示类型：图片、文字、图片及文字")]
        public DisplayStyleEnum DisplayStyle
        {
            get
            {
                return this._DisplayStyle;
            }
            set
            {
                this._DisplayStyle = value;
                this.SetDisplayStyle();
            }
        }

        [Description("跳转按钮文字"), Category("自定义属性"), DefaultValue("跳转")]
        public string JumpText
        {
            get
            {
                return this._JumpText;
            }
            set
            {
                this._JumpText = value;
                this.btnToPageIndex.Text = this._JumpText;
            }
        }

        [DefaultValue(1), Category("自定义属性"), Description("当前显示的页数")]
        /// <summary> 当前页:从1开始
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_PageIndex < 1)
                {
                    this._PageIndex = 1;
                }
                return this._PageIndex;
            }
            set
            {

                this._PageIndex = value;
            }
        }

        [DefaultValue(10), Description("每页显示的记录数"), Category("自定义属性")]
        /// <summary> 每页显示的记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                if (value <= 1)
                {
                    value = 10;
                }
                this._PageSize = value;
                this.SetLabelLocation();
            }
        }

        /// <summary> 总页数，外部不要调用，分页空间调用
        /// </summary>
        public int PageCount
        {
            get
            {
                return this._PageCount;
            }
            set
            {
                this._PageCount = value;
            }
        }

        [Description("要分页的总记录数"), Category("自定义属性")]
        /// <summary> 要分页的总记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this._RecordCount;
            }
            set
            {
                this._RecordCount = value;
            }
        }

        [DefaultValue(3), Description("图片和文字显示相对位置,当DisplayStyle=文字或DisplayStyle=图片及文字时生效"), Category("自定义属性")]
        public TextImageRalitionEnum TextImageRalitions
        {
            get
            {
                return this._TextImageRalition;
            }
            set
            {
                this._TextImageRalition = value;
                this.SetDisplayStyle();
            }
        }


        #endregion

        public WinFormPager()
        {
            InitializeComponent();           
        }

        private void WinFormPager_Load(object sender, EventArgs e)
        {
            this.cbPageSize.SelectedIndexChanged += new System.EventHandler(this.cbPageSize_SelectedIndexChanged);
            this.SetBtnEnabled();
            this.btnToPageIndex.Text = this._JumpText;
            cbPageSize.SelectedIndex = 1;
            cbPageSize.Text = "15";
        }

        #region Methods
        protected int GetPageCount(int RecordCounts, int PageSizes)
        {
            int num = 0;
            string str = (Convert.ToDouble(RecordCounts) / Convert.ToDouble(PageSizes)).ToString();
            if (str.IndexOf(".") < 0)
            {
                return Convert.ToInt32(str);
            }
            string[] strArray = Regex.Split(str, @"\.", RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(strArray[1].ToString()))
            {
                num = Convert.ToInt32(strArray[0]) + 1;
            }
            return num;
        }

        protected void SetBtnEnabled()
        {
            if (this._PageIndex == 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
                if (this.PageCount <= 1)
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                }
            }
            else if ((this.PageCount > 1) && (this._PageIndex < this.PageCount))
            {
                this.btnFirst.Enabled = true;
                this.btnPrevious.Enabled = true;
                this.btnNext.Enabled = true;
                this.btnLast.Enabled = true;
            }
            else if (this._PageIndex == this.PageCount)
            {
                this.btnFirst.Enabled = true;
                this.btnPrevious.Enabled = true;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
            }
        }

        private void SetDisplayStyle()
        {
            TextImageRelation imageBeforeText = TextImageRelation.ImageBeforeText;
            if (this.TextImageRalitions == TextImageRalitionEnum.图片显示在文字上方)
            {
                imageBeforeText = TextImageRelation.ImageAboveText;
            }
            else if (this.TextImageRalitions == TextImageRalitionEnum.图片显示在文字下方)
            {
                imageBeforeText = TextImageRelation.TextAboveImage;
            }
            else if (this.TextImageRalitions == TextImageRalitionEnum.图片显示在文字前方)
            {
                imageBeforeText = TextImageRelation.ImageBeforeText;
            }
            else if (this.TextImageRalitions == TextImageRalitionEnum.图片显示在文字后方)
            {
                imageBeforeText = TextImageRelation.TextBeforeImage;
            }
            if (this.DisplayStyle == DisplayStyleEnum.图片)
            {
                this.btnFirst.ImageList = this.btnPrevious.ImageList = this.btnNext.ImageList = this.btnLast.ImageList = this.imglstPager;
                this.btnFirst.ImageIndex = 0;
                this.btnPrevious.ImageIndex = 1;
                this.btnNext.ImageIndex = 2;
                this.btnLast.ImageIndex = 3;
                this.btnFirst.Text = this.btnPrevious.Text = this.btnNext.Text = this.btnLast.Text = "";
                this.btnFirst.TextImageRelation = this.btnPrevious.TextImageRelation = this.btnNext.TextImageRelation = this.btnLast.TextImageRelation = TextImageRelation.Overlay;
            }
            else if (this.DisplayStyle == DisplayStyleEnum.文字)
            {
                this.btnFirst.ImageList = this.btnPrevious.ImageList = this.btnNext.ImageList = (ImageList)(this.btnLast.ImageList = null);
                this.btnFirst.Text = string.IsNullOrEmpty(this.BtnTextFirst) ? "首页" : this.BtnTextFirst;
                this.btnPrevious.Text = string.IsNullOrEmpty(this.BtnTextPrevious) ? "上页" : this.BtnTextPrevious;
                this.btnNext.Text = string.IsNullOrEmpty(this.BtnTextNext) ? "下页" : this.BtnTextNext;
                this.btnLast.Text = string.IsNullOrEmpty(this.BtnTextLast) ? "末页" : this.BtnTextLast;
                this.btnFirst.TextImageRelation = this.btnPrevious.TextImageRelation = this.btnNext.TextImageRelation = this.btnLast.TextImageRelation = TextImageRelation.Overlay;
            }
            else if (this.DisplayStyle == DisplayStyleEnum.图片及文字)
            {
                this.btnFirst.ImageList = this.btnPrevious.ImageList = this.btnNext.ImageList = this.btnLast.ImageList = this.imglstPager;
                this.btnFirst.ImageIndex = 0;
                this.btnPrevious.ImageIndex = 1;
                this.btnNext.ImageIndex = 2;
                this.btnLast.ImageIndex = 3;
                this.btnFirst.Text = string.IsNullOrEmpty(this.BtnTextFirst) ? "首页" : this.BtnTextFirst;
                this.btnPrevious.Text = string.IsNullOrEmpty(this.BtnTextPrevious) ? "上页" : this.BtnTextPrevious;
                this.btnNext.Text = string.IsNullOrEmpty(this.BtnTextNext) ? "下页" : this.BtnTextNext;
                this.btnLast.Text = string.IsNullOrEmpty(this.BtnTextLast) ? "末页" : this.BtnTextLast;
                this.btnFirst.TextImageRelation = this.btnPrevious.TextImageRelation = this.btnNext.TextImageRelation = this.btnLast.TextImageRelation = imageBeforeText;
            }
        }

        protected void SetLabelLocation()
        {
            this.cbPageSize.Left = (this.lblPager.Left + this.lblPager.Width) + 3;
            this.btnFirst.Left = (this.cbPageSize.Left + this.cbPageSize.Width) + 5;
            this.btnPrevious.Left = this.btnFirst.Left + this.btnFirst.Width;
            this.btnNext.Left = this.btnPrevious.Left + this.btnPrevious.Width;
            this.btnLast.Left = this.btnNext.Left + this.btnNext.Width;
            this.lbPre.Left = (this.btnLast.Left + this.btnLast.Width) + 5;
            this.txtToPageIndex.Left = this.lbPre.Left + this.lbPre.Width;
            this.lbEnd.Left = this.txtToPageIndex.Left + this.txtToPageIndex.Width;
            this.btnToPageIndex.Left = this.lbEnd.Left + this.lbEnd.Width;
        }

        public void SetPagerText()
        {
            string[] strArray = new string[] { this.PageIndex.ToString(), this.PageCount.ToString(), this.RecordCount.ToString() };
            this.lblPager.Text = string.Format(this.PagerText, (object[])strArray);
        }
        #endregion

        #region Events


        private void CustomEvent(object sender, EventArgs e)
        {
            try
            {
                this.PageIndexChanged(sender, e);
            }
            catch (Exception)
            {

            }
        }


        private void cbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageSize = Convert.ToInt32(cbPageSize.Text);
            this._PageIndex = 1;
            SetBtnState();
            this.CustomEvent(sender, e);
        }

        public void btnFirst_Click(object sender, EventArgs e)
        {
            this._PageIndex = 1;
            SetBtnState();
            this.CustomEvent(sender, e);
        }

        public void btnLast_Click(object sender, EventArgs e)
        {
            this._PageIndex = this.PageCount;
            SetBtnState();
            this.CustomEvent(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int num = this._PageIndex;
            try
            {
                int num2 = Convert.ToInt32(num) + 1;
                if (num2 > this.PageCount)
                {
                    num2 = this.PageCount;
                    return;
                }
                this._PageIndex = num2;
                SetBtnState();
                this.CustomEvent(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int num = this._PageIndex;
            try
            {
                int num2 = Convert.ToInt32(num) - 1;
                if (num2 <= 0)
                {
                    num2 = 1;
                }
                this._PageIndex = num2;
                SetBtnState();
                this.CustomEvent(sender, e);
            }
            catch (Exception)
            {
            }
        }

        public void btnToPageIndex_Click(object sender, EventArgs e)
        {
            int pageIndex = Convert.ToInt32(this.txtToPageIndex.Text.Trim());
            int num = this._PageIndex;
            if (this.PageCount == 0)
            {
                this.txtToPageIndex.Text = "0";
            }
            else if (pageIndex < 1)
            {
                num = 1;
                this.txtToPageIndex.Text = "1";
            }
            else
            {
                num = pageIndex;
                if (num > this.PageCount)
                {
                    num = this.PageCount;
                    this.txtToPageIndex.Text = this.PageCount.ToString();
                }
                else
                {
                    this._PageIndex = num;
                    SetBtnState();
                    this.CustomEvent(sender, e);
                }
            }
        }

        public void SetBtnState()
        {
            this.PageCount = this.GetPageCount(this.RecordCount, this._PageSize);
            this.SetPagerText();
            this.SetBtnEnabled();
            this.SetLabelLocation();
        }

        private void WinFormPager_Paint(object sender, PaintEventArgs e)
        {
            SetBtnState();
        }

        private void WinFormPager_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Control control in base.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.Transparent;
                    button.FlatAppearance.BorderColor = Color.White;
                    button.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                }
            }
        }
        #endregion
    }
}
