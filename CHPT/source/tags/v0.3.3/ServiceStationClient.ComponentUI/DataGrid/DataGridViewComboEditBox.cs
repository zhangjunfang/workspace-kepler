using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ServiceStationClient.ComponentUI.DataGrid
{
    public class DataGridViewComboEditBoxColumn : DataGridViewComboBoxColumn 
    { 
        public DataGridViewComboEditBoxColumn()
        { 
            DataGridViewComboEditBoxCell obj = new DataGridViewComboEditBoxCell(); 
            this.CellTemplate = obj; 
        }

    }
    //要加入的类      
    public class DataGridViewComboEditBoxCell : DataGridViewComboBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            ComboBox comboBox = (ComboBox)DataGridView.EditingControl;
            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.FlatStyle = FlatStyle.Flat;
                comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox.Validated += new EventHandler(comboBox_Validated);
            }
        }

        void comboBox_Validated(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
            if (cbo.Text.Trim() == string.Empty) return;
            DataGridView grid = cbo.EditingControlDataGridView;
            object value = cbo.Text;                // Add value to list if not there             
            if (cbo.Items.IndexOf(value) == -1)
            {
                DataGridViewComboBoxColumn cboCol = (DataGridViewComboBoxColumn)grid.Columns[grid.CurrentCell.ColumnIndex];
                // Must add to both the current combobox as well as the template, to avoid duplicate entries                  
                cbo.Items.Add(value);
                cboCol.Items.Add(value);
                grid.CurrentCell.Value = value;
            }
        }
        public override void DetachEditingControl()
        {
            base.DetachEditingControl();
            ComboBox comboBox = DataGridView.EditingControl as ComboBox;

            comboBox.Validated -= new EventHandler(comboBox_Validated);
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {

            if (value != null && this.DataSource==null)
            {
                if (value.ToString().Trim() != string.Empty)
                {
                    if (Items.IndexOf(value) == -1)
                    {
                        Items.Add(value);
                        DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)OwningColumn;
                        col.Items.Add(value);
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }
    }
}
