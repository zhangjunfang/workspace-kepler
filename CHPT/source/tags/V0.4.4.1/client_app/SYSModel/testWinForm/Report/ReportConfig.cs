using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Xml;
using System.Reflection;

namespace testWinForm.Report
{
    /// <summary>
    /// 报表配置
    /// </summary>
    public class ReportConfig
    {
        private string _filename;
        private bool _autosize;
        private PageSettings _pset = new PageSettings();
        private Dictionary<string, TextItem> _header;
        private Dictionary<string, TextItem> _dataitem;
        private Dictionary<string, TextItem> _footer;
        private Dictionary<string, object> _cusmdata;
        private float _headheight;
        private float _footheight;
        private float _width, _height, _lm, _tm, _rm, _bm;
        private string _unit;

        public ReportConfig()
        {
        }

        public ReportConfig(string frpFile)
        {
            _filename = frpFile;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(frpFile);
            XmlNode pnode = xdoc.SelectSingleNode("//configuration/pageset");
            ReportConfig.GetAttribute(pnode, "unit", ref _unit);
            if (string.IsNullOrEmpty(_unit)) //没有单位
            {
                //页面大小
                int w = 0, h = 0;
                ReportConfig.GetAttribute(pnode, "width", ref w);
                ReportConfig.GetAttribute(pnode, "height", ref h);
                _autosize = h == 0;
                _pset.PaperSize = new PaperSize(frpFile, w, h);
                //页边距
                int m_left = 0, m_top = 0, m_right = 0, m_bottom = 0;
                ReportConfig.GetAttribute(pnode, "margin-left", ref m_left);
                ReportConfig.GetAttribute(pnode, "margin-top", ref m_top);
                ReportConfig.GetAttribute(pnode, "margin-right", ref m_right);
                ReportConfig.GetAttribute(pnode, "margin-bottom", ref m_bottom);
                _pset.Margins = new Margins(m_left, m_right, m_top, m_bottom);
            }
            else
            {
                ReportConfig.GetAttribute(pnode, "width", ref _width);
                ReportConfig.GetAttribute(pnode, "height", ref _height);
                ReportConfig.GetAttribute(pnode, "margin-left", ref _lm);
                ReportConfig.GetAttribute(pnode, "margin-top", ref _tm);
                ReportConfig.GetAttribute(pnode, "margin-right", ref _rm);
                ReportConfig.GetAttribute(pnode, "margin-bottom", ref _bm);
            }
            //头
            pnode = xdoc.SelectSingleNode("//configuration/header");
            ReportConfig.GetAttribute(pnode, "height", ref _headheight);
            //字段
            foreach (XmlNode node in xdoc.SelectNodes("//configuration/header/textitem"))
            {
                TextItem item = new TextItem(node);
                if (_header == null)
                {
                    _header = new Dictionary<string, TextItem>();
                }
                _header.Add(item.Key, item);
            }
            //尾
            pnode = xdoc.SelectSingleNode("//configuration/footer");
            ReportConfig.GetAttribute(pnode, "height", ref _footheight);
            //字段
            foreach (XmlNode node in xdoc.SelectNodes("//configuration/footer/textitem"))
            {
                TextItem item = new TextItem(node);
                if (_footer == null)
                {
                    _footer = new Dictionary<string, TextItem>();
                }
                _footer.Add(item.Key, item);
            }
            //数据字段
            foreach (XmlNode node in xdoc.SelectNodes("//configuration/dataitem/textitem"))
            {
                TextItem item = new TextItem(node);
                if (_dataitem == null)
                {
                    _dataitem = new Dictionary<string, TextItem>();
                }
                _dataitem.Add(item.Key, item);
            }
        }

        public Dictionary<string, object> CustomData
        {
            get
            {
                if (_cusmdata == null)
                    _cusmdata = new Dictionary<string, object>();
                return _cusmdata;
            }
        }

        public Dictionary<string, TextItem> Header
        {
            get { return _header; }
        }

        public Dictionary<string, TextItem> DataItem
        {
            get { return _dataitem; }
        }

        public Dictionary<string, TextItem> Footer
        {
            get { return _footer; }
        }

        public PageSettings PageSettings
        {
            get { return _pset; }
        }

        public float HeadHeight
        {
            get { return _headheight; }
        }

        public float FootHeight
        {
            get { return _footheight; }
        }

        public float Width
        {
            get { return _width; }
        }

        public float Height
        {
            get { return _height; }
        }

        public float LeftMargin
        {
            get { return _lm; }
        }

        public float TopMargin
        {
            get { return _tm; }
        }

        public float RightMargin
        {
            get { return _rm; }
        }

        public float BottomMargin
        {
            get { return _bm; }
        }

        public bool AutoSize
        {
            get { return _autosize; }
            set { _autosize = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// 从一个类获取数据
        /// </summary>
        /// <param name="data"></param>
        public void DataFromDataItem(object data)
        {
            Type type = data.GetType();
            foreach (PropertyInfo pinfo in type.GetProperties(BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (CustomData.ContainsKey(pinfo.Name))
                    continue;
                object value = type.InvokeMember(pinfo.Name, BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic, null, data, null);
                if (value != null)
                {
                    CustomData.Add(pinfo.Name, value);
                }
            }
        }

        internal static void GetAttribute(XmlNode node, string key, ref string value)
        {
            if (node.Attributes[key] != null)
            {
                value = node.Attributes[key].Value;
            }
        }

        internal static void GetAttribute(XmlNode node, string key, ref int value)
        {
            if (node.Attributes[key] != null)
            {
                value = int.Parse(node.Attributes[key].Value);
            }
        }

        internal static void GetAttribute(XmlNode node, string key, ref float value)
        {
            if (node.Attributes[key] != null)
            {
                value = float.Parse(node.Attributes[key].Value);
            }
        }
    }
}
