using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testWinForm.Report
{
    public class TableRdlGenerator
    {
        private Dictionary<string, TextItem> m_fields;

        public Dictionary<string, TextItem> Fields
        {
            get { return m_fields; }
            set { m_fields = value; }
        }

        public Rdl.TableType CreateTable()
        {
            //定义表格
            Rdl.TableType table = new Rdl.TableType();
            table.Name = "Table1";
            table.Items = new object[]
               {
                   CreateTableColumns(),
                   CreateHeader(),
                   CreateDetails(),
               };
            table.ItemsElementName = new Rdl.ItemsChoiceType21[]
               {
                   Rdl.ItemsChoiceType21.TableColumns,
                   Rdl.ItemsChoiceType21.Header,
                   Rdl.ItemsChoiceType21.Details,
               };
            return table;
        }

        private Rdl.HeaderType CreateHeader()
        {
            Rdl.HeaderType header = new Rdl.HeaderType();
            header.Items = new object[]
               {
                   CreateHeaderTableRows(),
                   true,
               };
            header.ItemsElementName = new Rdl.ItemsChoiceType20[]
               {
                   Rdl.ItemsChoiceType20.TableRows,
                   Rdl.ItemsChoiceType20.RepeatOnNewPage,
               };
            return header;
        }

        private Rdl.TableRowsType CreateHeaderTableRows()
        {
            Rdl.TableRowsType headerTableRows = new Rdl.TableRowsType();
            headerTableRows.TableRow = new Rdl.TableRowType[] { CreateHeaderTableRow() };
            return headerTableRows;
        }

        private Rdl.TableRowType CreateHeaderTableRow()
        {
            Rdl.TableRowType headerTableRow = new Rdl.TableRowType();
            headerTableRow.Items = new object[] { CreateHeaderTableCells(), "0.25in" };
            return headerTableRow;
        }

        private Rdl.TableCellsType CreateHeaderTableCells()
        {
            Rdl.TableCellsType headerTableCells = new Rdl.TableCellsType();
            headerTableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            int i = 0;
            foreach (string key in m_fields.Keys)
            {
                headerTableCells.TableCell[i++] = CreateHeaderTableCell(m_fields[key]);
            }
            return headerTableCells;
        }

        private Rdl.TableCellType CreateHeaderTableCell(TextItem item)
        {
            Rdl.TableCellType headerTableCell = new Rdl.TableCellType();
            headerTableCell.Items = new object[] { CreateHeaderTableCellReportItems(item) };
            return headerTableCell;
        }

        private Rdl.ReportItemsType CreateHeaderTableCellReportItems(TextItem item)
        {
            Rdl.ReportItemsType headerTableCellReportItems = new Rdl.ReportItemsType();
            headerTableCellReportItems.Items = new object[] { CreateHeaderTableCellTextbox(item) };
            return headerTableCellReportItems;
        }

        private Rdl.TextboxType CreateHeaderTableCellTextbox(TextItem item)
        {
            Rdl.TextboxType headerTableCellTextbox = new Rdl.TextboxType();
            headerTableCellTextbox.Name = item.Text + "_Header";
            headerTableCellTextbox.Items = new object[] 
               {
                    item.Text,
                    CreateHeaderTableCellTextboxStyle(item),
                    true,
                };
            headerTableCellTextbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                };
            return headerTableCellTextbox;
        }

        private Rdl.StyleType CreateHeaderTableCellTextboxStyle(TextItem item)
        {
            Rdl.StyleType headerTableCellTextboxStyle = new Rdl.StyleType();
            headerTableCellTextboxStyle.Items = new object[]
                {
                    item.HeaderFont.Name,
                    item.HeaderFont.Size + "pt",
                    item.HeaderFont.Bold ? "700" : "100",
                    "Center",
                    "Middle",
                    CreateBorderStyle(),
                };
            headerTableCellTextboxStyle.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.FontFamily,
                    Rdl.ItemsChoiceType5.FontSize,
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.VerticalAlign,
                    Rdl.ItemsChoiceType5.BorderStyle,
                };
            return headerTableCellTextboxStyle;
        }

        private Rdl.DetailsType CreateDetails()
        {
            Rdl.DetailsType details = new Rdl.DetailsType();
            details.Items = new object[] { CreateTableRows() };
            return details;
        }

        private Rdl.TableRowsType CreateTableRows()
        {
            Rdl.TableRowsType tableRows = new Rdl.TableRowsType();
            tableRows.TableRow = new Rdl.TableRowType[] { CreateTableRow() };
            return tableRows;
        }

        private Rdl.TableRowType CreateTableRow()
        {
            Rdl.TableRowType tableRow = new Rdl.TableRowType();
            tableRow.Items = new object[] { CreateTableCells(), "0.25in" };
            return tableRow;
        }

        private Rdl.TableCellsType CreateTableCells()
        {
            Rdl.TableCellsType tableCells = new Rdl.TableCellsType();
            tableCells.TableCell = new Rdl.TableCellType[m_fields.Count];
            int i = 0;
            foreach (string key in m_fields.Keys)
            {
                tableCells.TableCell[i++] = CreateTableCell(m_fields[key]);
            }
            return tableCells;
        }

        private Rdl.TableCellType CreateTableCell(TextItem item)
        {
            Rdl.TableCellType tableCell = new Rdl.TableCellType();
            tableCell.Items = new object[] { CreateTableCellReportItems(item) };
            return tableCell;
        }

        private Rdl.ReportItemsType CreateTableCellReportItems(TextItem item)
        {
            Rdl.ReportItemsType reportItems = new Rdl.ReportItemsType();
            reportItems.Items = new object[] { CreateTableCellTextbox(item) };
            return reportItems;
        }

        private Rdl.TextboxType CreateTableCellTextbox(TextItem item)
        {
            Rdl.TextboxType textbox = new Rdl.TextboxType();
            textbox.Name = item.Key;
            textbox.Items = new object[] 
                {
                    "=Fields!" + item.DataMember + ".Value",
                    CreateTableCellTextboxStyle(item),
                    true,
                };
            textbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
                {
                    Rdl.ItemsChoiceType14.Value,
                    Rdl.ItemsChoiceType14.Style,
                    Rdl.ItemsChoiceType14.CanGrow,
                };
            return textbox;
        }

        private Rdl.StyleType CreateTableCellTextboxStyle(TextItem item)
        {
            Rdl.StyleType style = new Rdl.StyleType();
            style.Items = new object[]
                {
                    item.Font.Name,
                    item.Font.Size + "pt",
                    item.Font.Bold ? "400" : "100",
                    GetAlign(item.Align.Alignment),
                    "Middle",
                    CreateBorderStyle(),
                    "1pt",
                    "1pt",
                    "1pt",
                    "1pt",
                };
            style.ItemsElementName = new Rdl.ItemsChoiceType5[]
                {
                    Rdl.ItemsChoiceType5.FontFamily,
                    Rdl.ItemsChoiceType5.FontSize,
                    Rdl.ItemsChoiceType5.FontWeight,
                    Rdl.ItemsChoiceType5.TextAlign,
                    Rdl.ItemsChoiceType5.VerticalAlign,
                    Rdl.ItemsChoiceType5.BorderStyle,
                    Rdl.ItemsChoiceType5.PaddingLeft,
                    Rdl.ItemsChoiceType5.PaddingTop,
                    Rdl.ItemsChoiceType5.PaddingRight,
                    Rdl.ItemsChoiceType5.PaddingBottom,
                };
            return style;
        }

        private Rdl.TableColumnsType CreateTableColumns()
        {
            Rdl.TableColumnsType tableColumns = new Rdl.TableColumnsType();
            tableColumns.TableColumn = new Rdl.TableColumnType[m_fields.Count];
            int i = 0;
            foreach (string key in m_fields.Keys)
            {
                tableColumns.TableColumn[i++] = CreateTableColumn(m_fields[key]);
            }
            return tableColumns;
        }

        private Rdl.TableColumnType CreateTableColumn(TextItem item)
        {
            Rdl.TableColumnType tableColumn = new Rdl.TableColumnType();
            tableColumn.Items = new object[] { (item.Rect.Width / 96.0) + "in" };
            return tableColumn;
        }

        private Rdl.BorderColorStyleWidthType CreateBorderStyle()
        {
            Rdl.BorderColorStyleWidthType bstyle = new Rdl.BorderColorStyleWidthType();
            bstyle.Items = new object[]
                {
                    "Solid",
                };
            bstyle.ItemsElementName = new Rdl.ItemsChoiceType3[]
                {
                    Rdl.ItemsChoiceType3.Default,
                };
            return bstyle;
        }

        private string GetVAlign(StringAlignment sformat)
        {
            switch (sformat)
            {
                case StringAlignment.Center: return "Middle";
                case StringAlignment.Far: return "Bottom";
                default: return "Top";
            }
        }

        private string GetAlign(StringAlignment sformat)
        {
            switch (sformat)
            {
                case StringAlignment.Center: return "Center";
                case StringAlignment.Far: return "Right";
                default: return "Left";
            }
        }
    }
}
