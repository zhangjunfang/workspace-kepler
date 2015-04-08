using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI.TextBox
{
    public partial class UCAutoContacts : UserControl
    {
        private DataTable dataSource;
        public ListBox lsBoxResult = new ListBox();

        public event EventHandler AfterSearch;
        public DataTable DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
            }
        }
        bool bln = false;
        private System.Drawing.Point poit = new System.Drawing.Point(0, 0);
        public System.Drawing.Point Poit
        {
            get
            {
                return poit;
            }
            set
            {
                poit = value;
                lsBoxResult.BringToFront();
                if (!this.ListBoxParent.Controls.Contains(lsBoxResult) && bln)
                {
                    this.ListBoxParent.Controls.Add(lsBoxResult);
                    //lsBoxResult.Top = this.Top + this.Height;
                    //lsBoxResult.Left = this.Left;
                    lsBoxResult.Top = poit.Y + this.Height;
                    lsBoxResult.Left = poit.X;
                    lsBoxResult.Width = this.Width;
                }
                bln = true;
                //else
                //{
                //    ListBox lb = (ListBox)this.ListBoxParent.Controls.Find(lsBoxResult.Name, false)[0];
                //    lb.BringToFront();
                //    lb.Top = poit.Y + this.Height;
                //    lb.Left = poit.X;
                //    lb.Width = this.Width;
                //}
            }
        }
        public bool ListBoxVisable
        {
            get
            {
                return lsBoxResult.Visible;
            }
            set
            {
                lsBoxResult.Visible = value;
            }
        }

        private Control listBoxParent = null;
        public Control ListBoxParent
        {
            get
            {
                return listBoxParent;
            }
            set
            {
                listBoxParent = value;
                //lsBoxResult.BringToFront();
                //if (!this.ListBoxParent.Controls.Contains(lsBoxResult))
                //{
                //    this.ListBoxParent.Controls.Add(lsBoxResult);
                //    //lsBoxResult.Top = this.Top + this.Height;
                //    //lsBoxResult.Left = this.Left;
                //    lsBoxResult.Top = poit.Y + this.Height;
                //    lsBoxResult.Left = poit.X;
                //    lsBoxResult.Width = this.Width;

                //}
            }
        }
      
        public string ResultWords
        {
            get
            {
                return txtKeyWords.Caption;
            }
            set
            {
                txtKeyWords.Caption = value;
                txtKeyWords.WaterMark = value;
            }
        }

        public bool IsVideoTree { get; set; }
        public UCAutoContacts()
        {
            InitializeComponent();
            lsBoxResult.DrawItem += new DrawItemEventHandler(lsBoxResult_DrawItem);
            lsBoxResult.MouseUp += new MouseEventHandler(lsBoxResult_MouseUp);
            lsBoxResult.MouseMove += new MouseEventHandler(lsBoxResult_MouseMove);
            lsBoxResult.KeyDown += new KeyEventHandler(lsBoxResult_KeyDown);
            lsBoxResult.Visible = false;
            lsBoxResult.DrawMode = DrawMode.OwnerDrawFixed;
            lsBoxResult.ItemHeight = 20;
        }
        void lsBoxResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lsBoxResult.Visible && lsBoxResult.SelectedItems.Count > 0)
            {
                txtKeyWords.Caption = lsBoxResult.SelectedItems[0].ToString();

                lsBoxResult.Visible = false;
                lsBoxResult.Focus();
                txtKeyWords_KeyDown(null, e);
            }
        }

        private int GetItemAt(ListBox listBox, int X, int Y)
        {
            int index = -1;
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                System.Drawing.Rectangle r = listBox.GetItemRectangle(i);
                if (r.Contains(new Point(X, Y)))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        void lsBoxResult_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = new Point(e.X, e.Y);

            int index = GetItemAt(lsBoxResult, e.X, e.Y);
            if (lsBoxResult.SelectedItems.Count > 0 && lsBoxResult.SelectedIndex != index)
                lsBoxResult.SetSelected(lsBoxResult.SelectedIndex, false);

            if (index != -1 && lsBoxResult.SelectedIndex != index)
                lsBoxResult.SetSelected(index, true);
        }

        void lsBoxResult_MouseUp(object sender, MouseEventArgs e)
        {
            if (lsBoxResult.SelectedItems.Count > 0)
            {
                this.txtKeyWords.Caption = lsBoxResult.SelectedItems[0].ToString();
                lsBoxResult.Visible = false;
                lsBoxResult.Focus();
                if (AfterSearch != null)
                {
                    AfterSearch(sender, new EventArgs());
                }
            }
        }

        void lsBoxResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            lsBoxResult.DrawMode = DrawMode.OwnerDrawFixed;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            FontFamily fontFamily = new FontFamily("宋体");
            System.Drawing.Font myFont = new Font(fontFamily, 12);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if (e.Index > -1)
                    e.Graphics.DrawString(lsBoxResult.Items[e.Index].ToString(), myFont, Brushes.White, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                if (e.Index > -1)
                    e.Graphics.DrawString(lsBoxResult.Items[e.Index].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }


        private void txtKeyWords_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down && lsBoxResult.Visible)
                {
                    lsBoxResult.Focus();
                    if (lsBoxResult.SelectedItems.Count > 0)
                        lsBoxResult.SetSelected(lsBoxResult.SelectedIndex, false);

                    if (lsBoxResult.Items.Count > 0)
                        lsBoxResult.SetSelected(0, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lsBoxResult.Visible && lsBoxResult.SelectedItems.Count > 0)
                    {
                        txtKeyWords.Caption = lsBoxResult.SelectedItems[0].ToString();
                        lsBoxResult.Items.Clear();
                        lsBoxResult.Visible = false;
                    }

                    if (AfterSearch != null)
                    {
                        AfterSearch(sender, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                //clsLog.WriteLog(1, "Exe_ctlLocus_text_LicensePlateNumber_KeyDown：" + ex.Message, 1);
            }
        }

        private void txtKeyWords_UserControlValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "") != "")
                {

                    System.Data.DataTable TbTree = new System.Data.DataTable();
                    TbTree = dataSource.Copy();
                    System.Data.DataView dvTree = new System.Data.DataView();
                    dvTree = TbTree.DefaultView;
                    string strFilter = string.Empty;

                    strFilter = "[cont_name] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "")
                                   + "%' OR ([cont_phone] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "") + "%'"
                                   + " OR [cont_tel]  like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "") + "%')";

                    //if (IsVideoTree)
                    //{
                    //    strFilter = "[entName] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "")
                    //               + "%' OR ([vehicleNo] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "") + "%'"
                    //               + " AND [dvrID] IS NOT NULL)";
                    //}
                    //else
                    //{
                    //    strFilter = "[entName] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "")
                    //                + "%' OR [vehicleNo] like '%" + txtKeyWords.Caption.Trim().Replace(txtKeyWords.WaterMark, "") + "%'";
                    //}
                    dvTree.RowFilter = strFilter;
                    BindList(dvTree.ToTable());

                    lsBoxResult.Width = txtKeyWords.Width + 23;
                }
                else
                {
                    lsBoxResult.Items.Clear();
                    lsBoxResult.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void Clear()
        {
            txtKeyWords.Caption = txtKeyWords.WaterMark;
            lsBoxResult.Visible = false;
        }
        #region listBox_Search 绑定数据

        private void BindList(DataTable dt)
        {
            this.lsBoxResult.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["cont_name"].ToString() == "")
                    lsBoxResult.Items.Add(dt.Rows[i]["cont_id"].ToString());
                else
                    lsBoxResult.Items.Add(dt.Rows[i]["cont_name"].ToString());
            }

            if (dt.Rows.Count < 11)
                this.lsBoxResult.Height = 30 * dt.Rows.Count + 30;
            else
                this.lsBoxResult.Height = 300;

            if (lsBoxResult.Items.Count > 0)
            {
                lsBoxResult.BringToFront();
                
                lsBoxResult.Visible = true;
                lsBoxResult.SelectedIndex = 0;
            }
            else
                lsBoxResult.Visible = false;
        }

        #endregion
    }
}
