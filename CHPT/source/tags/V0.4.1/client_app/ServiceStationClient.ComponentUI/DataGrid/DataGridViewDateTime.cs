using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ServiceStationClient.ComponentUI
{
    public partial class DataGridViewDateTimeColumn : DataGridViewColumn
    {
        public DataGridViewDateTimeColumn()
            : base(new DataGridViewDateTimeCell())
        {
            this.MinimumWidth = 190;
            this.DefaultCellStyle.ForeColor = Color.Blue;
        }

        /// <summary>
        /// 过滤文件类型
        /// </summary>
        public string Filter = "All files(*.*)|*.*;";

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewDateTimeCell)))
                {
                    throw new InvalidCastException("单元格类型无效");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewDateTimeCell : DataGridViewTextBoxCell
    {
        public DataGridViewDateTimeCell()
            : base()
        {
        }

        /// <summary>
        /// 重写样式
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            //Point cursorPositon = this.DataGridView.PointToClient(Cursor.Position);
            //if (cellBounds.Contains(cursorPositon))
            //{
            //    Rectangle newRect = new Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width - 4, cellBounds.Height - 4);
            //    graphics.DrawRectangle(Pens.Red, newRect);
            //}
        }

        /// <summary>
        /// 初始化编辑单元格的控件
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="dataGridViewCellStyle"></param>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DataGridViewDateTime_sms ctl =
             DataGridView.EditingControl as DataGridViewDateTime_sms;

            if (this.Value == null)
            {
                ctl.Value = this.DefaultNewRowValue.ToString();
            }
            else
            {
                ctl.Value = this.Value.ToString();
            }  
        }
        protected override object GetValue(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return null;
            }
            return base.GetValue(rowIndex);
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (rowIndex < 0)
            {
                return null;
            }
            if (value == null || value == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDateTime(value).ToShortDateString();
        }
        /// <summary>
        /// 获取单元格的寄宿编辑控件的类型
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewDateTime_sms);
            }
        }

        /// <summary>
        /// 获取或设置单元格中值的数据类型
        /// </summary>
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        /// <summary>
        /// 获取新记录所在行中单元格默认的值
        /// </summary>
        public override object DefaultNewRowValue
        {
            get
            {
                return string.Empty;
            }
        }

        public override object Clone()
        {
            return base.Clone();
        }
    }
}
