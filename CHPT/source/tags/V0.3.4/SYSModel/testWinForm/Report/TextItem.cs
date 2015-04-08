using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;

namespace testWinForm.Report
{
   [Serializable]
   public class TextItem
   {
       private string _text;
       private string _key;
       private string _datamember;
       private Rectangle _rect;
       private StringFormat _align;
       private bool _count;
       private string _format;
       internal decimal _sum;
       private Font _font = System.Windows.Forms.Control.DefaultFont;
       private Font _headerfont = System.Windows.Forms.Control.DefaultFont;

       public TextItem(XmlNode node)
       {
           _key = node.Attributes["key"].Value;
           if(!string.IsNullOrEmpty(node.InnerText))
           {
               _text = node.InnerText;
           }
           else
           {
               ReportConfig.GetAttribute(node, "text", ref _text);
           }
           ReportConfig.GetAttribute(node, "data", ref _datamember);
           ReportConfig.GetAttribute(node, "format", ref _format);
           string font_name = _font.Name;
           string font_bold = "";
           string font_italic = "";
           string font_underline = "";
           float font_size = Font.Size;
           FontStyle fsty = FontStyle.Regular;
           ReportConfig.GetAttribute(node, "font-name", ref font_name);
           ReportConfig.GetAttribute(node, "font-size", ref font_size);
           ReportConfig.GetAttribute(node, "font-bold", ref font_bold);
           ReportConfig.GetAttribute(node, "font-italic", ref font_italic);
           ReportConfig.GetAttribute(node, "font-underline", ref font_underline);
           if (font_bold.Equals("1"))
           {
               fsty |= FontStyle.Bold;
           }
           if (font_italic.Equals("1"))
           {
               fsty |= FontStyle.Italic;
           }
           if (font_underline.Equals("1"))
           {
               fsty |= FontStyle.Underline;
           }
           _font = new Font(font_name, font_size, fsty);

           font_name = _font.Name;
           font_bold = "";
           font_italic = "";
           font_underline = "";
           font_size = Font.Size;
           fsty = FontStyle.Regular;
           ReportConfig.GetAttribute(node, "header-font-name", ref font_name);
           ReportConfig.GetAttribute(node, "header-font-size", ref font_size);
           ReportConfig.GetAttribute(node, "header-font-bold", ref font_bold);
           ReportConfig.GetAttribute(node, "header-font-italic", ref font_italic);
           ReportConfig.GetAttribute(node, "header-font-underline", ref font_underline);
           if (font_bold.Equals("1"))
           {
               fsty |= FontStyle.Bold;
           }
           if (font_italic.Equals("1"))
           {
               fsty |= FontStyle.Italic;
           }
           if (font_underline.Equals("1"))
           {
               fsty |= FontStyle.Underline;
           }
           _headerfont = new Font(font_name, font_size, fsty);

           int left = 0, top = 0, width = 0, height = 0;
           ReportConfig.GetAttribute(node, "left", ref left);
           ReportConfig.GetAttribute(node, "top", ref top);
           ReportConfig.GetAttribute(node, "width", ref width);
           ReportConfig.GetAttribute(node, "height", ref height);
           _rect = new Rectangle(left, top, width, height);

           string align = "left";
           ReportConfig.GetAttribute(node, "align", ref align);
           string valign = "top";
           ReportConfig.GetAttribute(node, "valign", ref valign);
           _align = new StringFormat();
           if (align.Equals("right", StringComparison.OrdinalIgnoreCase))
           {
                _align.Alignment = StringAlignment.Far;
            }
            else if (align.Equals("center", StringComparison.OrdinalIgnoreCase))
            {
                _align.Alignment = StringAlignment.Center;
            }
            if (valign.Equals("bottom", StringComparison.OrdinalIgnoreCase))
            {
                _align.LineAlignment = StringAlignment.Far;
            }
            else if (valign.Equals("middle", StringComparison.OrdinalIgnoreCase))
            {
                _align.LineAlignment = StringAlignment.Center;
            }
 
            string count = "";
            ReportConfig.GetAttribute(node, "count", ref count);
            if(count.Equals("1"))
            {
                _count = true;
            }
        }
 
        public string Key
        {
            get { return _key; }
        }
 
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
 
        public string DataMember
        {
            get { return _datamember; }
        }
 
        public string DataFormat
        {
            get { return _format; }
        }
 
        public StringFormat Align
        {
            get { return _align; }
        }
 
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }
 
        public bool Count
        {
            get { return _count; }
        }
 
        public Font Font
        {
            get { return _font; }
        }
 
        public Font HeaderFont
        {
            get { return _headerfont; }
        }
    }
}
