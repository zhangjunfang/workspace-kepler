using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.Drawing;
using System.Data;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 名    称: DataGridViewTotal
    /// 功能描述：实现DataGridView的合计功能
    /// 作    者：JC
    /// 创建日期：2014.10.11
    /// 备    注：由于使用动态增加的纵向滚动条会出现黑边，所以暂时将原DataGridView的纵向滚动条显示出来，如果以后
    ///           有办法更改这个Bug的话，可以建议继续使用手动添加的纵向滚动条
    /// </summary>
    public  class DataGridViewTotal
    {
        //保存原datagridview
        private DataGridView dgv;
        //保存合计的DataGridView
        private DataGridView dgvSum = new DataGridView();
        //动态增加竖向滚动条
        VScrollBar vScrollBar;
        //取得原数据
        //DataTable dt;
        //获得合计数据
        DataTable dtSum;
        //行号
        private int ROW_HEIGHT;
        //是否显示滚动条
        bool isCroll = true;
        //要合计的列
        private List<string> sumColumns = new List<string>();
        public  DataGridViewTotal(DataGridView dataGridView, string[] totalColumns, bool iscroll)
        {
            //dataGridView.ScrollBars = ScrollBars.Vertical;
            //dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            //return;
            this.dgv = dataGridView;
            isCroll = iscroll;
            sumColumns = new List<string>();
            if (totalColumns != null)
            {
                for (int i = 0; i < totalColumns.Length; i++)
                {
                    sumColumns.Add(totalColumns[i]);
                }
            }
            //合计所以数字的列
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.ValueType == typeof(decimal) || dgvc.ValueType == typeof(int) || dgvc.ValueType == typeof(float))
                {
                    if (!sumColumns.Contains(dgvc.Name))
                    {
                        sumColumns.Add(dgvc.Name);
                    }
                }
            }
            //如果原来已经有dgvSum,则删除
            foreach (Control c in dgv.Parent.Controls)
            {
                if (c is DataGridView && c.Name == "dgvTotal")
                {
                    dgv.Parent.Controls.Remove(c);
                }
                if (c is VScrollBar && c.Name == "vScrollBarTotal")
                {
                    dgv.Parent.Controls.Remove(c);
                }
            }
            //初始化控件基本属性
            this.InitControls();
            //初始化滚动条
            //this.InitScrollBar();
            //初始化合计
            this.InitDataGridViewSum();
            //增加事件
            this.InitEvents();
        }
        #region 初始化
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //初始化行高的值
            ROW_HEIGHT = dgv.RowTemplate.Height;
            //初始化DataGridView

            this.dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.dgv.BorderStyle = BorderStyle.None;
            this.dgv.Height += 35;
            //增加合计DataGridView
            this.dgv.Parent.Controls.Add(dgvSum);
            dgvSum.Name = "dgvTotal";
            dgvSum.Location = new Point(dgv.Location.X, dgv.Location.Y + dgv.Height);
            dgvSum.Width = dgv.Width;
            dgvSum.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            if (isCroll)
            {
                this.dgv.ScrollBars = ScrollBars.Vertical;
                dgvSum.ScrollBars = ScrollBars.Horizontal;
            }
            else
            {
                this.dgv.ScrollBars = ScrollBars.None;
                dgvSum.ScrollBars = ScrollBars.None;

            }
            this.dgvSum.RowsDefaultCellStyle = this.dgv.RowsDefaultCellStyle;
            this.dgvSum.RowHeadersDefaultCellStyle = this.dgv.RowHeadersDefaultCellStyle;
            dgvSum.ColumnHeadersVisible = false;
            dgvSum.AllowUserToAddRows = false;
            dgvSum.AllowUserToDeleteRows = false;
            dgvSum.AllowUserToResizeColumns = false;
            dgvSum.RowTemplate.Height = ROW_HEIGHT;
            dgvSum.Height = ROW_HEIGHT;
            dgvSum.RowHeadersDefaultCellStyle.Padding = new Padding(this.dgvSum.RowHeadersWidth);
            dgvSum.AllowUserToResizeColumns = false;
            dgvSum.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvSum.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvSum.BorderStyle = BorderStyle.None;
            //增加竖向滚动条
            vScrollBar = new VScrollBar();
            //this.dgv.Parent.Controls.Add(vScrollBar);
            //vScrollBar.Name = "vScrollBarTotal";
            //vScrollBar.Top = dgv.Top;
            //vScrollBar.Left = dgv.Left + dgv.Width;
            //vScrollBar.Height = dgv.Height;
            //vScrollBar.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            //vScrollBar.Minimum = 0;
            //vScrollBar.Maximum = (this.dgv.Rows.Count - this.dgv.DisplayedRowCount(false) + 2) * ROW_HEIGHT;
            //vScrollBar.SmallChange = ROW_HEIGHT;
            //vScrollBar.LargeChange = 65;
            //this.dgvSum.Enabled = false;

            this.dgvSum.TabStop = false;


        }
        /// <summary>
        /// 初始化滚动条
        /// </summary>
        private void InitScrollBar()
        {
            if ((this.dgv.Height - this.dgv.ColumnHeadersHeight) / ROW_HEIGHT < this.dgv.RowCount)
                this.vScrollBar.Visible = true;
            else
            {
                this.vScrollBar.Visible = false;
            }

        }
        /// <summary>
        /// 初始化合计的DataGridView
        /// </summary>
        private void InitDataGridViewSum()
        {
            this.dgvSum.DataSource = null;
            DataRow dr;
            dtSum = new DataTable("Total");
            for (int i = 0; i < this.dgv.Columns.Count; i++)
            {
                dtSum.Columns.Add(this.dgv.Columns[i].Name);
            }
            dr = dtSum.NewRow();
            //dr[0] = "合计";
            dtSum.Rows.Add(dr);

            this.dgvSum.DataSource = dtSum;
            //this.dgvSum.Rows[0].DefaultCellStyle.BackColor = Color.Brown;
            this.dgvSum.BackgroundColor = this.dgv.BackgroundColor;

            this.dgvSum.ReadOnly = true;
            this.dgvSum.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //
            for (int i = 0; i < this.dgv.Columns.Count; i++)
            {
                this.dgvSum.Columns[i].Visible = this.dgv.Columns[i].Visible;
                this.dgvSum.Columns[i].Width = this.dgv.Columns[i].Width;
                this.dgvSum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvSum.Columns[i].Frozen = this.dgv.Columns[i].Frozen;
            }
            this.SetSum();
            if (IsShowScroll(this.dgvSum))
            {
                this.dgvSum.Height = ROW_HEIGHT + 20;
            }
            else
            {
                this.dgvSum.Height = ROW_HEIGHT;
            }
        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvents()
        {
            //合计行的横向滚动条滚动的时候
            this.dgvSum.Scroll += dgvSum_Scroll;
            //dataGridView的值发生改变时，重新计算合计
            this.dgv.CellValueChanged += dgv_CellValueChanged;

            //datagridView的列宽发生改变时，改变合计行的列宽
            this.dgv.ColumnWidthChanged += dgv_ColumnWidthChanged;

            //在原datagridView中滚动鼠标的时候
            this.dgv.MouseWheel += dgv_MouseWheel;
            //竖向滚动条改变时
            this.vScrollBar.Scroll += vScrollBar_Scroll;

            //数据发生变化时，重新计算合计
            this.dgv.DataSourceChanged += dgv_DataSourceChanged;
            //在合计行中绘制合计
            this.dgvSum.RowPostPaint += dgvSum_RowPostPaint;
            //当隐藏，显示列时调用,使合计行的对应列页显示出来
            this.dgv.ColumnStateChanged += dgv_ColumnStateChanged;
            //DataGridView宽度改变时，判断是否显示滚动条，调整列宽
            this.dgvSum.SizeChanged += dgvSum_SizeChanged;
            //当dvg的行删除时发生
            this.dgv.RowsRemoved += dgv_RowsRemoved;
            //
            this.dgv.RowsAdded += dgv_RowsAdded;
            //当列的序号发生改变时
            this.dgv.ColumnDisplayIndexChanged += dgv_ColumnDisplayIndexChanged;
            //当dgv的大小改变时, 重新更改合计行的宽度
            this.dgv.SizeChanged += dgv_SizeChanged;
            //合计行无选中效果
            this.dgvSum.SelectionChanged += dgvSum_SelectionChanged;
            //按键
            this.dgv.KeyUp += dgv_KeyUp;

        }

        #endregion

        #region 事件
        /// <summary>
        /// 合计行的横向滚动条滚动时，让原来的GridView也跟随滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvSum_Scroll(object sender, ScrollEventArgs e)
        {
            this.dgv.HorizontalScrollingOffset = e.NewValue;
        }
        /// <summary>
        /// 原dataGridView的合计行发生改变时，重新计算合计行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.dgv.Equals(sender))
                return;
            if (e.ColumnIndex <= 0)
                return;
            if (!sumColumns.Contains(this.dgv.Columns[e.ColumnIndex].Name))
                return;
            this.SetSum();
            //this.dgvSum[e.ColumnIndex, 0].Value = dt.Compute("Sum(" + dt.Columns[e.ColumnIndex].ColumnName + ")", "dt.Columns[e.ColumnIndex].ColumnName != null");
        }
        /// <summary>
        /// 在原DataGridView中滚动鼠标的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!this.vScrollBar.Visible)
                return;
            if (this.vScrollBar.Value - ROW_HEIGHT < 0 && e.Delta > 0)
            {
                this.vScrollBar.Value = this.vScrollBar.Minimum;
                return;
            }
            if (this.vScrollBar.Value + ROW_HEIGHT > this.vScrollBar.Maximum && e.Delta < 0)
            {
                this.vScrollBar.Value = this.vScrollBar.Maximum;
                return;
            }
            try
            {
                int i = e.Delta / Math.Abs(e.Delta) * ROW_HEIGHT;
                this.vScrollBar.Value -= i;
                this.dgv.FirstDisplayedScrollingRowIndex = this.vScrollBar.Value / ROW_HEIGHT;
                return;
                //
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 当改变dataGridView的列宽时，合计的列宽也跟随改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.dgv.HorizontalScrollingOffset = this.dgvSum.HorizontalScrollingOffset;
            this.dgvSum.Columns[e.Column.Index].Width = e.Column.Width;
            if (IsShowScroll(this.dgvSum))
            {
                //if (this.dgvSum.Height != ROW_HEIGHT + 20)
                this.dgvSum.Height = ROW_HEIGHT + 20;
            }
            else
            {
                //if (this.dgvSum.Height != ROW_HEIGHT)
                this.dgvSum.Height = ROW_HEIGHT;
            }
        }
        /// <summary>
        /// 竖向滚动条改变时，重新定位dataGridView开始行的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.dgv.FirstDisplayedScrollingRowIndex = e.NewValue / ROW_HEIGHT;
        }
        /// <summary>
        /// 当绑定数据源改变时，重新计算合集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgvSender = (DataGridView)sender;
            bool b_colChange = false;
            for (int i = 0; i < dgvSender.ColumnCount; i++)
            {
                if (!this.dgvSum.Columns[i].Equals(dgvSender.Columns[i]))
                {
                    b_colChange = true;
                    break;
                }
            }
            if (b_colChange)
            {
                this.InitDataGridViewSum();
            }
            else
            {
                if (dgvSender.Rows.Count == 0)
                    return;
                this.SetSum();
            }


        }
        /// <summary>
        /// 合计行中绘制合计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvSum_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            Color color = gridView.RowHeadersDefaultCellStyle.ForeColor;
            if (gridView.Rows[e.RowIndex].Selected)
                color = gridView.RowHeadersDefaultCellStyle.SelectionForeColor;
            else
                color = gridView.RowHeadersDefaultCellStyle.ForeColor;

            using (SolidBrush b = new SolidBrush(color))
            {
                e.Graphics.DrawString("合计", e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 6);
            }

        }
        /// <summary>
        /// 当显示合计行改变时，使合计行的对应列也作出对应改变，同时判断是否应该显示横向滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Visible)
            {
                dgvSum.Columns[e.Column.Index].Visible = e.Column.Visible;
                if (IsShowScroll(this.dgvSum))
                {
                    //if (this.dgvSum.Height != ROW_HEIGHT + 20)
                    this.dgvSum.Height = ROW_HEIGHT + 20;
                }
                else
                {
                    //if (this.dgvSum.Height != ROW_HEIGHT)
                    this.dgvSum.Height = ROW_HEIGHT;
                }
                if (this.sumColumns.Contains(e.Column.Name))
                {
                    this.SetSum();
                }
            }
        }
        /// <summary>
        /// 当dgvSum的宽度改变时，重新判断是否显示滚动条，来调整dgvSum的高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvSum_SizeChanged(object sender, EventArgs e)
        {
            if (IsShowScroll(this.dgvSum))
            {
                this.dgvSum.Height = this.ROW_HEIGHT + 20;
                //if (this.dgvSum.Height != this.ROW_HEIGHT + 20)
                //{
                //    this.dgvSum.Height = this.ROW_HEIGHT + 20;
                //}

            }
            else
            {
                this.dgvSum.Height = this.ROW_HEIGHT;
                //if (this.dgvSum.Height != this.ROW_HEIGHT)
                //{
                //    this.dgvSum.Height = this.ROW_HEIGHT;
                //}

            }

            this.dgvSum.Top = this.dgv.Parent.Height - this.dgvSum.Height;
        }
        /// <summary>
        /// 当DataGridView的行删除时，包括调用Rows.Clear()事件时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //if (e.RowIndex == 0 && e.RowCount > 0)
            //    return;
            this.SetSum();
        }
        /// <summary>
        /// 当DataGridView的行增加时，包括重新排序时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.dgv.Columns.Count != this.dgvSum.Columns.Count)
                return;
            this.SetSum();
        }
        /// <summary>
        /// 当DataGridView的列顺序改变时，同时改变合计行的列序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.dgv.Columns.Count != dgvSum.Columns.Count)
                return;
            this.dgvSum.Columns[e.Column.Index].DisplayIndex = e.Column.DisplayIndex;
        }
        /// <summary>
        /// 当DataGridView的宽度改变时，重新改变合计行的宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_SizeChanged(object sender, EventArgs e)
        {
            this.dgvSum.Width = this.dgv.Width;
        }

        /// <summary>
        /// 功能:取消选中        
        /// 备注:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgvSum_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvSum.ClearSelection();
        }


        #endregion
        /// <summary>
        /// 重新计算合计行
        /// </summary>
        public void SetSum()
        {
            for (int i = 0; i < this.dgv.ColumnCount; i++)
            {
                if (this.sumColumns.Contains(this.dgv.Columns[i].Name) && this.dgv.Columns[i].Visible)
                {
                    decimal sum = 0;
                    for (int j = 0; j < this.dgv.RowCount; j++)
                    {
                        if (this.dgv[i, j].Value != null && !string.IsNullOrEmpty(this.dgv[i, j].Value.ToString()))
                        {
                            try
                            {
                                sum += decimal.Parse(this.dgv[i, j].Value.ToString());
                            }
                            catch
                            {
                                string message = string.Format("{0}列的第{1}行数据有问题，请核实.", this.dgv.Columns[i].HeaderText, j + 1);
                                MessageBoxEx.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.dgvSum[i, 0].Value = 0;
                                return;
                            }

                        }
                    }
                    this.dgvSum[i, 0].Value = sum;
                }
            }
        }
        /// <summary>
        /// 根据列的宽度是否大于datagridview的宽度来判断是否显示滚动条
        /// </summary>
        /// <returns></returns>
        private bool IsShowScroll(DataGridView dataGridView)
        {
            if (dataGridView.ColumnCount <= 0)
                return false;
            int columnWidth = dataGridView.RowHeadersWidth;
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                if (dataGridView.Columns[i].Visible)
                    columnWidth += dataGridView.Columns[i].Width;
            }
            if (columnWidth >= dataGridView.Width)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 重新设置合计列
        /// </summary>
        public List<string> TotalColumns
        {
            set
            {
                this.sumColumns = value;
            }
        }
        /// <summary>
        /// 返回合计行，用来强行改变合计行的属性
        /// </summary>
        public DataGridView DgvSum
        {
            get
            {
                return this.dgvSum;
            }
        }
        /// <summary>
        /// 功能 左右键 控制滚动条       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
            {
                if (dgvSum.HorizontalScrollingOffset > 0)
                {
                    if (dgvSum.FirstDisplayedScrollingColumnHiddenWidth != 0)
                    {
                        dgvSum.HorizontalScrollingOffset -= dgvSum.FirstDisplayedScrollingColumnHiddenWidth;
                    }
                    else
                    {
                        dgvSum.HorizontalScrollingOffset -= 2;
                        dgvSum.HorizontalScrollingOffset -= dgvSum.FirstDisplayedScrollingColumnHiddenWidth;
                    }
                }

            }
            else if (e.KeyData == Keys.Right)
            {
                if (dgvSum.HorizontalScrollingOffset < GetParentForm(dgv).HorizontalScroll.Maximum)
                {
                    dgvSum.HorizontalScrollingOffset += dgvSum.Columns[dgvSum.FirstDisplayedScrollingColumnIndex].Width - dgvSum.FirstDisplayedScrollingColumnHiddenWidth;
                }

            }

        }
        private static Form GetParentForm(Control control)
        {
            if (control is Form)
            {
                return (Form)control;
            }
            return GetParentForm(control.Parent);
        }
    }   
}
