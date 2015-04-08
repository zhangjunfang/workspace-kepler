using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HXCPcClient
{
    public partial class UCPageTurning : UserControl
    {
        public event EventHandler OnRowsNumberChange;
        public event EventHandler OnPageChange;
        public event EventHandler OnRefresh;

        private int pageCount = 0;
        private int currentPage = 0;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                txtCurrentPage.Text = currentPage.ToString();
                if (OnPageChange != null)
                {
                    OnPageChange(this, new EventArgs());
                }
                
            }
        }

        /// <summary>
        /// 显示行数设置
        /// </summary>
        public bool ShowRowsNumber
        {
            get 
            {
                return cmbRowsCount.Visible;
            }
            set 
            {
                cmbRowsCount.Visible = value;
                if (!cmbRowsCount.Visible)
                {
                    pnlPageTurning.Left = 0;
                }
                else
                {
                    cmbRowsCount.SelectedIndex = 0;
                    pnlPageTurning.Left = cmbRowsCount.Width;
                }
            }
        }

        public int RowsNumber
        {
            get 
            {
                int rowsNumber=0;
                if (cmbRowsCount.Visible) 
                {
                    int.TryParse(cmbRowsCount.Text, out rowsNumber);
                }
                return rowsNumber;
            }
        }
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = value;

                if (pageCount > 0)
                {
                    lblPageCount.Text = string.Format("/{0}", pageCount);
                }
                else 
                {
                    lblPageCount.Text = string.Empty;
                    lblPageInfo.Text = string.Empty;
                }
                
            }
        }



        public string PageInfo 
        {
            get
            {
                return lblPageInfo.Text;
            }
            set
            {
                lblPageInfo.Text = value;

            }
        }

        public UCPageTurning()
        {
            InitializeComponent();
            picPageFirst.Image = ServiceStationClient.Skin.Properties.Resources.page_first_default;
            picPageUp.Image = ServiceStationClient.Skin.Properties.Resources.page_up_default;
            picPageDown.Image = ServiceStationClient.Skin.Properties.Resources.page_down_default;
            picPageLast.Image = ServiceStationClient.Skin.Properties.Resources.page_last_default;
            picRefresh.Image = ServiceStationClient.Skin.Properties.Resources.page_refresh_default;
            
        }

        private void picPageFirst_Click(object sender, EventArgs e)
        {
            if (PageCount > 0) 
            {
                CurrentPage = 1;
            }
         
        }

        private void picPageUp_Click(object sender, EventArgs e)
        {
            if (currentPage - 1 > 0) 
            {
                CurrentPage--;
            }
            
        }

        private void picPageDown_Click(object sender, EventArgs e)
        {
            if (currentPage + 1 <= PageCount)
            {
                CurrentPage++;
            }
           
        }

        private void picPageLast_Click(object sender, EventArgs e)
        {
            if (PageCount > 0)
            {
                CurrentPage = PageCount;
            }
            
        }

        [Obsolete("过期事件")]
        private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter) 
            {
                int number = 0;
                if (int.TryParse(txtCurrentPage.Text, out number)) 
                {
                    if (number > 0 && number <= PageCount) 
                    {
                        CurrentPage = number;
                    }
                }
               
            }
        }

        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else 
            {
                if (e.KeyChar == (char)13)
                {
                    int number = 0;
                    if (int.TryParse(txtCurrentPage.Text, out number))
                    {
                        if (number > 0 && number <= PageCount)
                        {
                            CurrentPage = number;
                        }
                    }

                }
            }
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            if (OnRefresh != null) 
            {
                OnRefresh(sender, e);
            }
        }

        private void cmbRowsCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnRowsNumberChange != null) 
            {
                OnRowsNumberChange(sender, e);
            }
            
        }
    }
}
