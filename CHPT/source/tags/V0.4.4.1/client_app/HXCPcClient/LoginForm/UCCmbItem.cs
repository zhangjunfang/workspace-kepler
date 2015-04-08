using System;
using System.Drawing;
using System.Windows.Forms;

namespace HXCPcClient.LoginForms
{
    public partial class UCCmbItem : UserControl
    {
        #region --委托事件
        /// <summary>
        /// 单击子项
        /// </summary>
        /// <param name="obj"></param>
        public delegate void ItemClickedHandler(string display,string value);
        public event ItemClickedHandler ItemClicked;
        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="obj"></param>
        public delegate void ItemDeleteHandler(string value);
        public event ItemDeleteHandler ItemDeleted;
        #endregion

        #region --属性
        private string display;
        public string Display 
        {
            get
            {
                return this.display;
            }
            set
            {
                this.display = value;
            }
        }

        private string value;
        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        /// <summary> 选中颜色 
        /// </summary>
        public Color SelectedColor { get; set; }
        /// <summary> 子项目是否可删除 
        /// </summary>
        public bool CanDelete
        {
            set
            {
                this.pbDelete.Visible = value;
            }
            get
            {
                return this.pbDelete.Visible;
            }
        }
        #endregion

        #region --构造函数
        public UCCmbItem()
        {
            InitializeComponent();         
            foreach (Control tr in this.Controls)
            {
                tr.MouseEnter -= new EventHandler(UCCmbItem_MouseEnter);
                tr.MouseEnter += new EventHandler(UCCmbItem_MouseEnter);
                tr.MouseLeave -= new EventHandler(UCCmbItem_MouseLeave);
                tr.MouseLeave += new EventHandler(UCCmbItem_MouseLeave);
            }
        }
        public UCCmbItem(string _display,string _value)
        {
            InitializeComponent();
            this.labelDisplay.Text = _display;
            this.display = _display;
            this.value = _value;

            foreach (Control tr in this.Controls)
            {
                tr.MouseEnter -= new EventHandler(UCCmbItem_MouseEnter);
                tr.MouseEnter += new EventHandler(UCCmbItem_MouseEnter);
                tr.MouseLeave -= new EventHandler(UCCmbItem_MouseLeave);
                tr.MouseLeave += new EventHandler(UCCmbItem_MouseLeave);
            }
        }
        #endregion

        #region --设置大小
        public void SetSize(Font font, int height)
        {           
            this.labelDisplay.Font = font;
            if (height < font.Height - 10)
            {
                height = font.Height + 10;
            }
            this.Height = height;
            this.pbDelete.Location = new Point(this.pbDelete.Location.X, (this.Height - this.pbDelete.Height) / 2);
            this.labelDisplay.Location = new Point(this.labelDisplay.Location.X, (this.Height - this.labelDisplay.Height) / 2 - 1);
        }
        #endregion

        #region --事件
        private void UCCmbItem_Click(object sender, EventArgs e)
        {
            if (this.ItemClicked != null)
            {
                this.ItemClicked(this.display,this.value);
            }
        }

        private void UCCmbItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = this.SelectedColor;
            this.labelDisplay.ForeColor = Color.White;            
        }

        private void UCCmbItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = this.Parent.BackColor;
            this.labelDisplay.ForeColor = Color.Black;
        }

        private void UCCmbItem_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void UCCmbItem_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion

        #region --删除
        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (this.ItemDeleted != null)
            {
                this.ItemDeleted(this.value);
            }
        }
        #endregion
    }
}
