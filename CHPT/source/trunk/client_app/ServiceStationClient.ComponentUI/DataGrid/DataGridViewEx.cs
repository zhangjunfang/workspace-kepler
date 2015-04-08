using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Text.RegularExpressions;

namespace ServiceStationClient.ComponentUI
{
    public partial class DataGridViewEx : DataGridView
    {
        public delegate void DelegateOnClick();
        [Description("全选改变事件")]
        public event DelegateOnClick HeadCheckChanged;
        private DataGridViewCheckBoxHeaderCell headCheckBox = null;
        private bool checkBoxVisable = true;
        private bool showNum = true;
        private bool PaintFlag = false;
        private bool isCheck = false;//默认选择当前行
        DataGridViewTextBoxEditingControl txtNum;//数字单元格控件
        private Image backImage;
        private int firstColumnIndex = -1;//第一个显示的列索引
        public DataGridViewEx()
        {
            InitializeComponent();
            this.backImage = ServiceStationClient.Skin.Properties.Resources.DataGrid_Bg;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //add by kord -- 强制分配样式重新应用到控件上(不推荐使用,目前临时解决方案)
            Paint += delegate
            {
                if (!PaintFlag)
                {
                    SetDataGridViewStyle(this);
                    PaintFlag = true;
                }
            };
            this.IsCheck = true;
        }
        /// <summary>
        /// 设置数据表格样式,并将最后一列填充其余空白
        /// </summary>
        /// <param name="dgv">数据表格</param>
        /// <param name="column">最后一列</param>
        public static void SetDataGridViewStyle(DataGridViewEx dgv, DataGridViewColumn column)
        {
            if (column != null) column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  //最后一列填充
            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            var dataGridViewCellStyle2 = new DataGridViewCellStyle();
            var dataGridViewCellStyle3 = new DataGridViewCellStyle();
            var dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 241, 243);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            //选中行颜色
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(183, 233, 156);
            dataGridViewCellStyle3.Font = new Font("宋体", 9F, FontStyle.Regular);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(51, 51, 51);
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgv.GridColor = Color.FromArgb(221, 221, 221);

            //序号列样式-行标题背景色 选中色
            dataGridViewCellStyle4.BackColor = Color.FromArgb(224, 236, 255);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(224, 236, 255);
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            //行标题边框样式
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
        /// <summary>
        /// 设置数据表格样式
        /// </summary>
        /// <param name="dgv">数据表格</param>
        public static void SetDataGridViewStyle(DataGridViewEx dgv)
        {
            SetDataGridViewStyle(dgv, null);
        }

        [DefaultValue(true), Description("是否显示行号")]
        public bool ShowNum
        {
            get
            {
                return showNum;
            }
            set
            {
                showNum = value;
                if (value)
                {
                    this.RowHeadersVisible = true;
                }
            }
        }
        [DefaultValue(false), Description("默认选择当前行")]
        public bool IsCheck
        {
            get { return isCheck; }
            set { isCheck = value; }
        }

        /// <summary>
        /// 第一显示的列索引
        /// </summary>
        [Browsable(false)]
        public int FirstColumnIdex
        {
            get
            {
                if (firstColumnIndex < 0)
                {
                    foreach (DataGridViewColumn dgvc in this.Columns)
                    {
                        if (dgvc.Visible && dgvc.Index > 0)
                        {
                            firstColumnIndex = dgvc.Index;
                            break;
                        }
                    }
                }
                return firstColumnIndex;
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            //base.OnDataError(displayErrorDialogIfNoHandler, e);
            DataGridViewCell dgvc = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (dgvc.ValueType == typeof(decimal))
            {
                e.Cancel = false;
            }
        }

        #region --单击事件
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0)
            {
                base.OnCellMouseClick(e);
                return;
            }
            if (!IsCheck)
            {
                base.OnCellMouseClick(e);
                return;
            }
            SelectedCheckBox();
            base.OnCellMouseClick(e);
        }

        /// <summary>
        /// 单击选择当前行CheckBox
        /// </summary>
        void SelectedCheckBox()
        {
            if (this.CurrentRow == null)
            {
                return;
            }
            if (!IsCheck)
            {
                return;
            }
            if (this.Columns[0] is DataGridViewCheckBoxColumn)
            {
                //清空已选择框
                foreach (DataGridViewRow dgvr in this.Rows)
                {
                    if (dgvr == this.CurrentRow)
                    {
                        continue;
                    }
                    object check = dgvr.Cells[0].EditedFormattedValue;
                    if (check != null && (bool)check)
                    {
                        dgvr.Cells[0].Value = false;
                    }
                }
                //this.CurrentRow.Cells[0].Value = !(bool)this.CurrentRow.Cells[0].EditedFormattedValue;
                this.CurrentRow.Cells[0].Value = true;//选中
            }
            if (HeadCheckChanged != null)
            {
                HeadCheckChanged();
            }
        }

        //右键事件
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                if (isCheck)
                {
                    int rowIndex = this.HitTest(e.X, e.Y).RowIndex;
                    if (rowIndex < 0)
                    {
                        return;
                    }
                    #region 如果已选择多行,右键不再选择当前行
                    int checkCount = 0;//已选择行数
                    if (this.Columns[0] is DataGridViewCheckBoxColumn)
                    {
                        foreach (DataGridViewRow dgvr in this.Rows)
                        {
                            object check = dgvr.Cells[0].EditedFormattedValue;
                            if (check != null && (bool)check)
                            {
                                checkCount++;
                            }
                        }
                    }
                    if (checkCount > 1)
                    {
                        return;
                    }
                    #endregion
                    this.CurrentCell = this.Rows[rowIndex].Cells[FirstColumnIdex];
                    SelectedCheckBox();
                }
            }
        }
        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (this.Columns[0] is DataGridViewCheckBoxColumn)
                {
                    if (this.CurrentRow == null)
                    {
                        return;
                    }
                    this.CurrentRow.Cells[0].Value = !(bool)this.CurrentRow.Cells[0].EditedFormattedValue;
                }
                if (HeadCheckChanged != null)
                {
                    HeadCheckChanged();
                }
            }
            base.OnCellContentClick(e);
        }
        #endregion

        public DataGridViewEx(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.backImage = ServiceStationClient.Skin.Properties.Resources.DataGrid_Bg;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //add by kord -- 强制分配样式重新应用到控件上(不推荐使用,目前临时解决方案)
            Paint += delegate
            {
                if (!PaintFlag)
                {
                    SetDataGridViewStyle(this);
                    PaintFlag = true;
                }
            };
            this.IsCheck = true;
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            base.OnEditingControlShowing(e);
            if (this.CurrentCell == null)
            {
                return;
            }
            if (this.CurrentCell.ValueType != typeof(decimal) && this.CurrentCell.ValueType != typeof(int))
            {
                return;
            }

            #region create:tang  time:2015-02-27 根据列设置的类型控制内容显示的位置。当列为int,decimal,float类型时，内容居右
            if (this.CurrentCell.ValueType == typeof(int) || this.CurrentCell.ValueType == typeof(decimal) || this.CurrentCell.ValueType == typeof(float))
            {
                this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            } 
            #endregion

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                txtNum = (DataGridViewTextBoxEditingControl)e.Control;
                txtNum.KeyPress += new KeyPressEventHandler(txtNum_KeyPress);
            }
        }

        void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl text = (DataGridViewTextBoxEditingControl)sender;
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar.ToString() != "-" && e.KeyChar.ToString() != ".")
            {
                e.Handled = true;
            }
            else
            {
                if (text.Text.ToString().IndexOf("-") == -1)
                {
                    if ((Convert.ToInt32(e.KeyChar) == 46) && (text.Text.Length == 0 || text.Text.ToString().IndexOf(".") != -1))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    if (e.KeyChar.ToString() == "-")
                    {
                        e.Handled = true;
                    }
                    if ((Convert.ToInt32(e.KeyChar) == 46) && (text.Text.Length <= 1 || text.Text.ToString().IndexOf(".") != -1))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (txtNum != null)
            {
                txtNum.KeyPress -= new KeyPressEventHandler(txtNum_KeyPress);
                txtNum = null;
            }
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            if (ShowNum)
            {
                this.RowHeadersVisible = true;
                SolidBrush solidBrush = new SolidBrush(Color.Black);
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, solidBrush, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);
            }
            base.OnRowPostPaint(e);
        }

        [DefaultValue(false), Description("是否显示复选框")]
        public bool ShowCheckBox
        {
            get
            {
                return checkBoxVisable;
            }
            set
            {
                checkBoxVisable = value;
                if (checkBoxVisable)
                {
                    if (this.ColumnCount > 0)
                    {
                        DataGridViewCheckBoxColumn dc = this.Columns[0] as DataGridViewCheckBoxColumn;
                        if (dc != null)
                        {
                            headCheckBox = new DataGridViewCheckBoxHeaderCell();
                            //关联单击事件 
                            headCheckBox.OnCheckBoxClicked += new DataGridViewCheckBoxHeaderEventHander(headCheckBox_OnCheckBoxClicked);
                            dc.HeaderCell = headCheckBox;
                            dc.HeaderText = string.Empty;
                            dc.MinimumWidth = 30;
                            dc.Width = 30;
                            //dc.Width = 18;
                        }
                    }

                }
                else
                {
                    if (this.ColumnCount > 0)
                    {
                        DataGridViewCheckBoxColumn dc = this.Columns[0] as DataGridViewCheckBoxColumn;
                        if (dc != null)
                        {
                            dc.HeaderCell = null;
                            dc.HeaderText = "";
                        }
                    }
                }
            }
        }

        void headCheckBox_OnCheckBoxClicked(object sender, DataGridViewEx.datagridviewCheckboxHeaderEventArgs e)
        {
            //throw new NotImplementedException();             
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < this.Rows.Count; i++)
                {
                    this.Rows[i].Cells[e.ColumnIndex].Value = e.CheckedState;
                }
                if (HeadCheckChanged != null)
                {
                    HeadCheckChanged();
                }

                this.RefreshEdit();
            }
        }

        #region  列头处理(TODO：共通功能，后期优化到共通控件中)
        public delegate void DataGridViewCheckBoxHeaderEventHander(object sender, datagridviewCheckboxHeaderEventArgs e);

        public class datagridviewCheckboxHeaderEventArgs : EventArgs
        {
            private bool checkedState = false;

            public bool CheckedState
            {
                get { return checkedState; }
                set { checkedState = value; }
            }

            private int columnIndex = -1;

            public int ColumnIndex
            {
                get { return columnIndex; }
                set { columnIndex = value; }
            }
        }

        public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            public event DataGridViewCheckBoxHeaderEventHander OnCheckBoxClicked;

            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates dataGridViewElementState,
                object value,
                object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle,
                    advancedBorderStyle, paintParts);

                Point p = new Point();
                Size s = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);

                p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2) - 1;
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);

                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;
                if (_checked)
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.CheckedNormal;
                else
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.UncheckedNormal;

                CheckBoxRenderer.DrawCheckBox
                (graphics, checkBoxLocation, _cbState);
            }

            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
                if (p.X >= checkBoxLocation.X && p.X <=
                    checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <=
                    checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;

                    datagridviewCheckboxHeaderEventArgs ex = new datagridviewCheckboxHeaderEventArgs();
                    ex.CheckedState = _checked;
                    ex.ColumnIndex = e.ColumnIndex;
                    object sender = new object();

                    if (OnCheckBoxClicked != null)
                    {
                        OnCheckBoxClicked(sender, ex);
                        this.DataGridView.InvalidateCell(this);
                    }
                }
                base.OnMouseClick(e);
            }
        }
        #endregion

        #region 公用方法
        /// <summary>添加行,没有分页控件时使用。
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="row">数据行</param>
        public void AddRow(int rowIndex, DataRow row)
        {
            if (rowIndex < 0)
            {
                return;
            }
            DataTable dt = this.DataSource as DataTable;
            if (this.RowCount > rowIndex)
            {
                dt.Rows.InsertAt(row, rowIndex);
            }
            else
            {
                dt.Rows.Add(row);
            }
            this.DataSource = dt;
            this.Refresh();
            this.SetCurrentRow(rowIndex);

        }

        /// <summary> 更新行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">行索引</param>
        /// <param name="row">数据行</param>
        public void UpdateRow(int rowIndex, DataRow row)
        {
            if (rowIndex < 0)
            {
                return;
            }

            if (row == null)
            {
                return;
            }
            DataTable dt = this.DataSource as DataTable;
            dt.Rows.RemoveAt(rowIndex);
            dt.Rows.InsertAt(row, rowIndex);
            this.DataSource = dt;
            this.Refresh();
            this.SetCurrentRow(rowIndex);
        }

        /// <summary>  删除行,没有分页控件时使用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">行索引</param>
        /// <param name="t"></param>
        public void RemoveRow<T>(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return;
            }
            DataTable dt = this.DataSource as DataTable;
            dt.Rows.RemoveAt(rowIndex);
            this.DataSource = dt;
            if (rowIndex > 0)
            {
                this.SetCurrentRow(rowIndex - 1);
            }
        }

        /// <summary>
        /// 设置当前行
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        public void SetCurrentRow(int rowIndex)
        {
            if (rowIndex >= 0 && this.Rows.Count > rowIndex)
            {
                this.Rows[rowIndex].Selected = true;
                if (this.Columns[0].Visible)
                {
                    this.CurrentCell = this.Rows[rowIndex].Cells[0];
                }
            }
        }
        #endregion

        protected override void OnScroll(ScrollEventArgs e)
        {
            this.ReDrawHead();
            base.OnScroll(e);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                #region 不含合并列标题背景
                if (e.RowIndex == -1)
                {
                    if (!this.SpanRows.ContainsKey(e.ColumnIndex))
                    {
                        int X = e.CellBounds.X;
                        int Y = e.CellBounds.Y;
                        int W = e.CellBounds.Width;
                        int H = e.CellBounds.Height;

                        using (Pen blackPen = new Pen(Color.FromArgb(221, 221, 221))) //(163, 192, 232)))//边框颜色
                        {
                            using (TextureBrush tBrush = new TextureBrush(this.backImage))
                            {
                                e.Graphics.FillRectangle(tBrush, new Rectangle(X, Y, W, H));
                            }
                            if (e.ColumnIndex == 0)
                            {
                                if (this.Columns[0] is DataGridViewCheckBoxColumn)
                                {
                                    if (this.Columns[0].Width != 30)
                                    {
                                        this.Columns[0].Width = 30;
                                    }
                                }
                                e.Graphics.DrawRectangle(blackPen, new Rectangle(X, Y + 1, W - 1, H - 2));
                            }
                            else
                            {
                                e.Graphics.DrawRectangle(blackPen, new Rectangle(X - 1, Y + 1, W, H - 2));
                            }
                            e.PaintContent(e.CellBounds);
                            e.Handled = true;
                        }
                    }
                }
                #endregion
                #region 合并列标题背景
                if (((e.RowIndex <= -1) || (e.ColumnIndex <= -1)) && ((e.RowIndex == -1) && this.SpanRows.ContainsKey(e.ColumnIndex)))
                {
                    Graphics graphics = e.Graphics;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.Background);
                    int left = e.CellBounds.Left;
                    int y = e.CellBounds.Top + 2;
                    int right = e.CellBounds.Right;
                    int bottom = e.CellBounds.Bottom;
                    switch (this.SpanRows[e.ColumnIndex].Position)
                    {
                        case 1:
                            left += 2;
                            break;

                        case 3:
                            right -= 2;
                            break;
                    }
                    using (TextureBrush tBrush = new TextureBrush(this.backImage))
                    {
                        graphics.FillRectangle(tBrush, new Rectangle(left, y, right - left, (bottom - y) / 2));
                        graphics.FillRectangle(tBrush, new Rectangle(left, y, right - left, (bottom - y)));
                    }
                    using (Pen blackPen = new Pen(Color.FromArgb(221, 221, 221))) //(163, 192, 232)))//边框颜色
                    {
                        e.Graphics.DrawRectangle(blackPen, new Rectangle(left - 1, y + (bottom - y) / 2, right - left, (bottom - y) / 2));
                    }
                    //graphics.FillRectangle(new SolidBrush(this._mergecolumnheaderbackcolor), left, y, right - left, (bottom - y) / 2);
                    graphics.DrawLine(new Pen(base.GridColor), left, (y + bottom) / 2, right, (y + bottom) / 2);
                    StringFormat format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    //graphics.DrawString("",e.CellStyle.Font,Brushes.Black,
                    graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, new RectangleF(left, (y + bottom) / 2, right - left, (bottom - y) / 2), format);
                    left = base.GetColumnDisplayRectangle(this.SpanRows[e.ColumnIndex].Left, true).Left - 2;
                    if (left < 0)
                    {
                        left = base.GetCellDisplayRectangle(-1, -1, true).Width;
                    }
                    right = base.GetColumnDisplayRectangle(this.SpanRows[e.ColumnIndex].Right, true).Right - 2;
                    if (right < 0)
                    {
                        right = base.Width;
                    }
                    graphics.DrawString(this.SpanRows[e.ColumnIndex].Text, e.CellStyle.Font, Brushes.Black, new Rectangle(left, y, right - left, (bottom - y) / 2), format);

                    e.Handled = true;
                }
                base.OnCellPainting(e);
                #endregion
            }
            catch
            {
            }
        }

        #region 二维表头
        private Color _mergecolumnheaderbackcolor = SystemColors.Control;
        private List<string> _mergecolumnname = new List<string>();
        private Dictionary<int, SpanInfo> SpanRows = new Dictionary<int, SpanInfo>();
        public void AddSpanHeader(int ColIndex, int ColCount, string Text)
        {
            if (ColCount < 2)
            {
                throw new Exception("行宽应大于等于2，合并1列无意义。");
            }
            int right = (ColIndex + ColCount) - 1;
            this.SpanRows[ColIndex] = new SpanInfo(Text, 1, ColIndex, right);
            this.SpanRows[right] = new SpanInfo(Text, 3, ColIndex, right);
            for (int i = ColIndex + 1; i < right; i++)
            {
                this.SpanRows[i] = new SpanInfo(Text, 2, ColIndex, right);
            }
        }

        public void ClearSpanInfo()
        {
            this.SpanRows.Clear();
        }

        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush brush = new SolidBrush(base.GridColor);
            SolidBrush brush2 = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush brush3 = new SolidBrush(e.CellStyle.ForeColor);
            int upRows = 0;
            int downRows = 0;
            int count = 0;
            if (this.MergeColumnNames.Contains(base.Columns[e.ColumnIndex].Name) && (e.RowIndex != -1))
            {
                int width = e.CellBounds.Width;
                Pen pen = new Pen(brush);
                string str = (e.Value == null) ? "" : e.Value.ToString().Trim();
                if (base.CurrentRow.Cells[e.ColumnIndex].Value != null)
                {
                    base.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
                }
                if (!string.IsNullOrEmpty(str))
                {
                    for (int i = e.RowIndex; i < base.Rows.Count; i++)
                    {
                        if (!base.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(str))
                        {
                            break;
                        }
                        downRows++;
                        if (e.RowIndex != i)
                        {
                            width = (width < base.Rows[i].Cells[e.ColumnIndex].Size.Width) ? width : base.Rows[i].Cells[e.ColumnIndex].Size.Width;
                        }
                    }
                    for (int j = e.RowIndex; j >= 0; j--)
                    {
                        if (!base.Rows[j].Cells[e.ColumnIndex].Value.ToString().Equals(str))
                        {
                            break;
                        }
                        upRows++;
                        if (e.RowIndex != j)
                        {
                            width = (width < base.Rows[j].Cells[e.ColumnIndex].Size.Width) ? width : base.Rows[j].Cells[e.ColumnIndex].Size.Width;
                        }
                    }
                    count = (downRows + upRows) - 1;
                    if (count < 2)
                    {
                        return;
                    }
                }
                if (base.Rows[e.RowIndex].Selected)
                {
                    brush2.Color = e.CellStyle.SelectionBackColor;
                    brush3.Color = e.CellStyle.SelectionForeColor;
                }
                e.Graphics.FillRectangle(brush2, e.CellBounds);
                this.PaintingFont(e, width, upRows, downRows, count);
                if (downRows == 1)
                {
                    e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    count = 0;
                }
                e.Graphics.DrawLine(pen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                e.Handled = true;
            }
        }
        private void PaintingFont(DataGridViewCellPaintingEventArgs e, int cellwidth, int UpRows, int DownRows, int count)
        {
            SolidBrush brush = new SolidBrush(e.CellStyle.ForeColor);
            int height = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int width = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int num3 = e.CellBounds.Height;
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)(e.CellBounds.X + ((cellwidth - width) / 2)), (float)((e.CellBounds.Y + (num3 * DownRows)) - height));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomLeft)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)e.CellBounds.X, (float)((e.CellBounds.Y + (num3 * DownRows)) - height));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)((e.CellBounds.X + cellwidth) - width), (float)((e.CellBounds.Y + (num3 * DownRows)) - height));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)(e.CellBounds.X + ((cellwidth - width) / 2)), (float)((e.CellBounds.Y - (num3 * (UpRows - 1))) + (((num3 * count) - height) / 2)));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)e.CellBounds.X, (float)((e.CellBounds.Y - (num3 * (UpRows - 1))) + (((num3 * count) - height) / 2)));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)((e.CellBounds.X + cellwidth) - width), (float)((e.CellBounds.Y - (num3 * (UpRows - 1))) + (((num3 * count) - height) / 2)));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopCenter)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)(e.CellBounds.X + ((cellwidth - width) / 2)), (float)(e.CellBounds.Y - (num3 * (UpRows - 1))));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopLeft)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)e.CellBounds.X, (float)(e.CellBounds.Y - (num3 * (UpRows - 1))));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopRight)
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)((e.CellBounds.X + cellwidth) - width), (float)(e.CellBounds.Y - (num3 * (UpRows - 1))));
            }
            else
            {
                e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, brush, (float)(e.CellBounds.X + ((cellwidth - width) / 2)), (float)((e.CellBounds.Y - (num3 * (UpRows - 1))) + (((num3 * count) - height) / 2)));
            }
        }

        public void ReDrawHead()
        {
            foreach (int num in this.SpanRows.Keys)
            {
                base.Invalidate(base.GetCellDisplayRectangle(num, -1, true));
            }
        }

        [Category("二维表头"), Browsable(true), Description("二维表头的背景颜色")]
        public Color MergeColumnHeaderBackColor
        {
            get
            {
                return this._mergecolumnheaderbackcolor;
            }
            set
            {
                this._mergecolumnheaderbackcolor = value;
            }
        }

        [MergableProperty(false), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Localizable(true), Description("设置或获取合并列的集合"), Browsable(true), Category("单元格合并")]
        public List<string> MergeColumnNames
        {
            get
            {
                return this._mergecolumnname;
            }
            set
            {
                this._mergecolumnname = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SpanInfo
        {
            public string Text;
            public int Position;
            public int Left;
            public int Right;
            public SpanInfo(string Text, int Position, int Left, int Right)
            {
                this.Text = Text;
                this.Position = Position;
                this.Left = Left;
                this.Right = Right;
            }
        }
        #endregion

        #region --扩展方法，可移出
        /// <summary>
        /// 获取选中行主键ID
        /// </summary>
        /// <param name="dgvcbc"></param>
        /// <param name="columnId"></param>
        /// <returns></returns>
        public List<string> GetCheckedIds(DataGridViewCheckBoxColumn dgvcbc, string columnId)
        {
            List<string> list = new List<string>();
            if (this.Columns.Contains(dgvcbc.Name)
                && this.Columns.Contains(columnId))
            {
                object flag = string.Empty;
                foreach (DataGridViewRow dr in this.Rows)
                {
                    flag = dr.Cells[dgvcbc.Name].EditedFormattedValue;
                    if (flag != null && (bool)flag)
                    {
                        list.Add(dr.Cells[columnId].Value.ToString());
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取绑定数据
        /// </summary>        
        /// <returns></returns>
        public DataTable GetBoundData()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn dgvc in this.Columns)
            {
                //if (!dgvc.Visible)
                //{
                //    continue;
                //}
                if (dgvc is DataGridViewTextBoxColumn || dgvc is DataGridViewComboBoxColumn||dgvc is DataGridViewLinkColumn)
                {
                    if (!dt.Columns.Contains(dgvc.DataPropertyName) && dgvc.HeaderText.Length > 0)
                    {
                        dt.Columns.Add(dgvc.DataPropertyName);
                    }
                }
            }

            foreach (DataGridViewRow dgvr in this.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewColumn dgvc in this.Columns)
                {
                    if (dt.Columns.Contains(dgvc.DataPropertyName))
                    {
                        dr[dgvc.DataPropertyName] = dgvr.Cells[dgvc.Name].FormattedValue;
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion
    }
}
