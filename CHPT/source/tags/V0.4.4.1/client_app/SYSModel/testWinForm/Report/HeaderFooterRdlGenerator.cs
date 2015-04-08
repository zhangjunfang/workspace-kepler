using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace testWinForm.Report
{
    public class HeaderFooterRdlGenerator
    {
        private Dictionary<string, TextItem> m_fields;
        private int m_height;

        public Dictionary<string, TextItem> Fields
        {
            get { return m_fields; }
            set { m_fields = value; }
        }

        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        public Rdl.ReportItemsType CreateItems()
        {
            Rdl.ReportItemsType items = new Rdl.ReportItemsType();
            items.Items = new Rdl.TextboxType[m_fields.Count];
            int i = 0;
            foreach (string key in m_fields.Keys)
            {
                items.Items[i++] = CreateTextBox(m_fields[key]);
            }
            return items;
        }

        private Rdl.TextboxType CreateTextBox(TextItem item)
        {
            Rdl.TextboxType textbox = new Rdl.TextboxType();
            textbox.Name = item.Key;
            textbox.Items = new object[] 
               {
                   item.Text,
                   CreateTextboxStyle(item),
                   true,
                   (item.Rect.Left / 96.0) + "in",
                   (item.Rect.Top / 96.0) + "in",
                   (item.Rect.Width / 96.0) + "in",
                   (item.Rect.Height / 96.0) + "in",
               };
            textbox.ItemsElementName = new Rdl.ItemsChoiceType14[] 
               {
                   Rdl.ItemsChoiceType14.Value,
                   Rdl.ItemsChoiceType14.Style,
                   Rdl.ItemsChoiceType14.CanGrow,
                   Rdl.ItemsChoiceType14.Left,
                   Rdl.ItemsChoiceType14.Top,
                   Rdl.ItemsChoiceType14.Width,
                   Rdl.ItemsChoiceType14.Height,
               };
            return textbox;
        }

        private Rdl.StyleType CreateTextboxStyle(TextItem item)
        {
            Rdl.StyleType style = new Rdl.StyleType();
            style.Items = new object[]
               {
                   GetAlign(item.Align.Alignment),
                   GetVAlign(item.Align.LineAlignment),
                   item.Font.Name,
                   item.Font.Size + "pt",
                   item.Font.Bold ? "700" : "100",
               };
            style.ItemsElementName = new Rdl.ItemsChoiceType5[]
               {
                   Rdl.ItemsChoiceType5.TextAlign,
                   Rdl.ItemsChoiceType5.VerticalAlign,
                   Rdl.ItemsChoiceType5.FontFamily,
                   Rdl.ItemsChoiceType5.FontSize,
                   Rdl.ItemsChoiceType5.FontWeight,
               };
            return style;
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
