using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceStationClient.ComponentUI
{
    public partial class UCTimeSelector : UserControl
    {

        private DataTable dt = null;

        public event DataGridViewCellEventHandler OnTimeValueChanged;
        //显示数据
        private int[] items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        public int[] Items 
        {
            get 
            { 
                return items; 
            }
            set
            { 
                items = value;
                SetDataTable();
            }
        }

        //列数
        //private int intColumns = 6;
        //public int Columns
        //{
        //    get { return intColumns; }
        //    set { intColumns = value; }
        //}

        //private int intRows = 4;
        //public int Rows
        //{
        //    get { return intRows; }
        //    set { intRows = value; }
        //}

        private int intSelectedValue = 0;
        public int SelectedValue
        {
            get 
            { 
                return intSelectedValue; 
            }
            set 
            { 
                intSelectedValue = value;

            }
        }



        public UCTimeSelector()
        {
            InitializeComponent();

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("C1"));
            dt.Columns.Add(new DataColumn("C2"));
            dt.Columns.Add(new DataColumn("C3"));
            dt.Columns.Add(new DataColumn("C4"));
            dt.Columns.Add(new DataColumn("C5"));
            dt.Columns.Add(new DataColumn("C6"));

            //SetDataTable();
        }


        private void SetDataTable() 
        {
            int intRows = Convert.ToInt32(Math.Ceiling((double)items.Length / 6));
            dt.Clear();
            for (int i = 0; i < intRows; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 6; j++)
                {
                    string strColName = string.Format("C{0}", j + 1);

                    dr[strColName] = items[i * 6 + j];
                }
                dt.Rows.Add(dr);
            }
            this.dgvTime.DataSource = dt;
            this.dgvTime.Height = dgvTime.Rows[0].Height * intRows + 1;
            this.Height = this.dgvTime.Height;
            this.dgvTime.ClearSelection();
            this.dgvTime.Invalidate();
        }



        int _selectedRow = -1;
        int _selectedColumn = -1;
        private void dgvTime_SelectionChanged(object sender, EventArgs e)
        {
            switch (dgvTime.SelectedCells.Count)
            {
                case 0:
                    // store no current selection
                    _selectedRow = -1;
                    _selectedColumn = -1;
                    return;
                case 1:
                    // store starting point for multi-select
                    _selectedRow = dgvTime.SelectedCells[0].RowIndex;
                    _selectedColumn = dgvTime.SelectedCells[0].ColumnIndex;
                    return;
            }

            foreach (DataGridViewCell cell in dgvTime.SelectedCells)
            {
                if (cell.RowIndex == _selectedRow && cell.ColumnIndex == _selectedColumn)
                {
                    cell.Selected = true;
                }
                else
                {
                    cell.Selected = false;
                }
            }
        }

        private void dgvTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            intSelectedValue = int.Parse(dt.Rows[e.RowIndex][e.ColumnIndex].ToString());
            if (OnTimeValueChanged != null) 
            {
                OnTimeValueChanged(sender, e);
            }
        }
    }
}
