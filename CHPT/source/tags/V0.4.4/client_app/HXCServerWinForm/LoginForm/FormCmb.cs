using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HXCServerWinForm.LoginForms
{
    public partial class FormCmb : Form
    {
        #region --委托事件
        /// <summary>
        /// 单击子项
        /// </summary>
        /// <param name="obj"></param>
        public delegate void ItemSelectedHandler(string display, string value);
        public event ItemSelectedHandler ItemSelected;

        public delegate void ItemDeletedHandler(string value);
        public event ItemDeletedHandler ItemDeleted;
        #endregion

        #region --成员变量
        private Control ctl;
        private int offSetX = 0;
        private int offSetY = 0;
        private Color selectedColor = Color.Blue;
        private bool canDelete = false;
        #endregion

        #region --构造函数
        public FormCmb()
        {
            InitializeComponent();
        }
        #endregion

        #region --属性  
        /// <summary> 边框颜色 
        /// </summary>
        public Color BorderColor
        {
            set
            {
                this.peContent.BorderColor = value;
            }
        }
        /// <summary> 选中颜色 
        /// </summary>
        public Color SelectedColor
        {
            set
            {
                this.selectedColor = value;
            }
            get
            {
                return this.selectedColor;
            }
        }
        /// <summary> 绑定显示名 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary> 绑定值名 
        /// </summary>
        public string ValueName { get; set; }
        /// <summary> 子项目高度 
        /// </summary>
        public int ItemHeight { get; set; }
        #endregion

        #region --绑定下拉框
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ctl">绑定控件</param>
        /// <param name="dt">绑定数据表</param>
        /// <param name="_offSetX">X坐标左偏移</param>
        /// <param name="_offSetY">Y坐标下偏移</param>
        /// <param name="canDelete">子项是否可删除</param>
        public void BindCmb(Control _ctl, DataTable dt, int _offSetX, int _offSetY, bool canDelete)
        {
            this.offSetX = _offSetX;
            this.offSetY = _offSetY;
            this.canDelete = canDelete;
            this.ctl = _ctl;
            if (dt != null)
            {
                this.Show();
                this.AdjustLocation();
                this.Hide();                
                this.LoadItems(dt);

                this.ctl.Click -= new EventHandler(this.ctl_Click);
                this.ctl.Click += new EventHandler(this.ctl_Click);
                this.ctl.KeyDown -= new KeyEventHandler(ctl_KeyDown);
                this.ctl.KeyDown += new KeyEventHandler(ctl_KeyDown);
                this.ctl.Disposed -= new EventHandler(this._Disposed);
                this.ctl.Disposed += new EventHandler(this._Disposed);
            }
        }
        public void RefreshItems(DataTable dt)
        {
            this.peContent.Controls.Clear();
            this.LoadItems(dt);
        }
        #endregion

        #region --加载下拉数据
        /// <summary> 加载子项目 
        /// </summary>
        private void LoadItems(DataTable dt)
        {
            string display = string.Empty;
            string value = string.Empty;
            int y = 2;
            if (this.ItemHeight == 0)
            {
                this.ItemHeight = this.ctl.Height;
            }            
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Contains(this.DisplayName))
                {
                    display = dr[this.DisplayName].ToString();
                }
                if (dt.Columns.Contains(this.ValueName))
                {
                    value = dr[this.ValueName].ToString();
                }
                UCCmbItem cmbItem = new UCCmbItem(display, value);
                cmbItem.CanDelete = this.canDelete;
                cmbItem.SelectedColor = this.selectedColor;
                cmbItem.Width = this.Width - 4;
                cmbItem.SetSize(ctl.Font, this.ItemHeight);
                cmbItem.ItemClicked += new UCCmbItem.ItemClickedHandler(this.After_Selected);
                if (this.canDelete)
                {
                    cmbItem.ItemDeleted += new UCCmbItem.ItemDeletedHandler(this.After_Deleted);
                }
                this.peContent.Controls.Add(cmbItem);
                cmbItem.Location = new Point(2, y);
                y += this.ItemHeight;              

                display = string.Empty;
                value = string.Empty;
            }
            this.Height = y + 2;
        }
        #endregion

        #region --选择用户
        void After_Selected(string display, string value)
        {
            if (this.ItemSelected != null)
            {
                this.ItemSelected(display, value);
            }
            this.Hide();
        }
        #endregion

        #region --删除用户
        void After_Deleted(string value)
        {
            if (this.ItemDeleted != null)
            {
                this.ItemDeleted(value);                
            }
            this.Hide();
        }
        #endregion

        #region --调整窗体大小
        /// <summary> 调整位置和大小 
        /// </summary>
        public void AdjustLocation()
        {
            //屏幕分辨率 大小
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            int resolutionX = rect.Width;
            int resolutionY = rect.Height;

            //实际位置
            int loc_x = 0;
            int loc_y = 0;
            //把控件的当前坐标转换为屏幕坐标
            Point screenPos = this.ctl.Parent.PointToScreen(this.ctl.Location);
            if (screenPos.X < 0)
            {
                loc_x = 0;
            }
            else if (screenPos.X + this.Width > resolutionX)
            {
                loc_x = resolutionX - this.Width;
            }
            else
            {
                loc_x = screenPos.X;
            }


            if (screenPos.Y < 0)
            {
                loc_y = 0;
            }
            else if (screenPos.Y + this.Height + this.ctl.Height > resolutionY)
            {
                loc_y = screenPos.Y - this.Height;
            }
            else
            {
                loc_y = screenPos.Y + this.ctl.Height;
            }

            if (loc_x < 0)
            {
                loc_x = 0;
            }

            if (loc_y < 0)
            {
                loc_y = 0;
            }

            this.Location = new Point(loc_x - this.offSetX - 3, loc_y + this.offSetY + 2);
        }
        #endregion

        #region --窗体初始化
        private void FormCmb_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, -600);
            this.ShowInTaskbar = false;

        }
        #endregion

        #region --Click事件
        void ctl_Click(object sender, EventArgs e)
        {
            if (this != null && this.IsDisposed)
            {
                this.AdjustLocation();
                this.Show();
                //this.BringToFront();
            }
        }
        #endregion

        #region  --目标控件事件
        void ctl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (this.Visible == true)
                {
                    this.Activate();
                    this.peContent.Focus();
                    if (this.peContent.Controls.Count > 0)
                    {
                        this.peContent.Controls[0].Select();
                    }
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region --销毁
        void _Disposed(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region --失去焦点
        private void FormCmb_Deactivate(object sender, EventArgs e)
        {
            if (this == null || this.IsDisposed)
            {
                return;
            }
            if (!this.ctl.Focused)
            {
                this.Hide();
                this.ctl.Parent.Focus();
            }
        }
        #endregion
    }
}
