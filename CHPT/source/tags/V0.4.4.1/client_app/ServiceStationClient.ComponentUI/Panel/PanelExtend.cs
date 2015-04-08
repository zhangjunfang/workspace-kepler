using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI.Panel
{
    public partial class PanelExtend : UserControl
    {
        #region --属性
        private Color borderColor = Color.FromArgb(187, 210, 255);
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                this.SetBorderColor();
            }
        }
        private bool showBorder = true;
        public bool ShowBorder
        {
            get
            {
                return this.showBorder;
            }
            set
            {
                this.showBorder = value;
                this.SetBorderColor();
            }
        }       
        #endregion


        #region --构造函数
        public PanelExtend()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            this.SetBorderColor();
        }
        #endregion


        #region --成员方法
        private void SetBorderColor()
        {
            if (showBorder)
            {
                this.labelLeft.Visible = true;
                this.labelTop.Visible = true;
                this.labelRight.Visible = true;
                this.labelButtom.Visible = true;

                this.labelLeft.BackColor = this.borderColor;
                this.labelTop.BackColor = this.borderColor;
                this.labelRight.BackColor = this.borderColor;
                this.labelButtom.BackColor = this.borderColor;
            }
            else
            {
                this.labelLeft.Visible = false;
                this.labelTop.Visible = false;
                this.labelRight.Visible = false;
                this.labelButtom.Visible = false;
            }
        }
        public void ClearControls()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control ctl = this.Controls[i];
                if (ctl.Name == this.labelTop.Name
                    || ctl.Name == this.labelLeft.Name
                    || ctl.Name == this.labelRight.Name
                    || ctl.Name == this.labelButtom.Name)
                {
                    continue;
                }
                this.Controls.Remove(ctl);
                i--;
            }
        }
        /// <summary> 设置边线可见性
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="buttom"></param>
        public void SetVisible(bool top, bool left, bool right, bool buttom)
        {
            this.labelTop.Visible = top;
            this.labelLeft.Visible = left;
            this.labelRight.Visible = right;
            this.labelButtom.Visible = buttom;
        }
        #endregion
    }
}
