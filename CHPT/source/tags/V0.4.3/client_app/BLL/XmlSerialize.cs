using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace BLL
{
    public class XmlSerialize
    {
        public const string XML_ROOT = "<?xml version=\"1.0\" encoding=\"utf-8\"?><msg><head transcode=\"104\" partnerid=\"1008602\" version=\"1.0\" time=\"20100101121212\"/><body></body></msg>";
        private XmlDocument xmlDoc = new XmlDocument(); 
        private System.Collections.Generic.IDictionary<string, string> _controlList = new System.Collections.Generic.Dictionary<string, string>();

        public System.Collections.Generic.IDictionary<string, string> ControlList
        {
            get { return _controlList; }
        }

        public string Xml
        {
            get { return xmlDoc.OuterXml; }
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                {
                    xmlDoc.LoadXml(XML_ROOT);
                }
                else
                {
                    xmlDoc.LoadXml(value);
                }
            }
        }

        public XmlSerialize()
        {
            xmlDoc.LoadXml(XML_ROOT);
            SetAttributeValue("head", "partnerid", "1008602");
            SetAttributeValue("head", "time", DateTime.Now.ToShortDateString());
        }

        public XmlSerialize(string Xml)
        {
            if (!string.IsNullOrEmpty(Xml.Trim()))
            {
                xmlDoc.LoadXml(Xml);
            }
            else
            {
                xmlDoc.LoadXml(XML_ROOT);
            }
        }
        //增加节点(格式:<body/>)
        public void AddNode(string pKey, string name)
        {
            XmlNodeList iNodeList = xmlDoc.SelectNodes("//" + pKey);
            iNodeList[iNodeList.Count - 1].AppendChild(xmlDoc.CreateElement(name));
        }
        //增加节点并添加InnerText(格式:<body>1234</body>)
        public void AddNodeAndValue(string pKey, string name, string value)
        {
            XmlElement xe = xmlDoc.CreateElement(name);
            xe.InnerText = value;
            XmlNodeList iNodeList = xmlDoc.SelectNodes("//" + pKey); 
            iNodeList[iNodeList.Count - 1].AppendChild(xe);
        }

        //增加节点属性
        public void AddXmlElement(string pKey, string name)
        {
            //XmlElement xmlElement = xmlDoc.CreateElement(pKey + "//" + name);
            //return xmlElement;
        }

        //XmlNode find=doc.SelectSingleNode( "/student/username[@id= '001 '] "); 
        /// <summary>
        /// 删除xml节点或节点属性；如果删除节点name为空，若删除节点属性name为节点属性字符串，用','隔开
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="name"></param>
        public void DeleteNodeOrAttribute(string pKey, string AttriList)
        {
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(pKey);
            for (int k = 0; k < xmlNodeList.Count; k++)
            {
                XmlNode xmlNode = xmlNodeList[k];
                if (AttriList == "")
                {
                    xmlNode.ParentNode.RemoveChild(xmlNode);
                    //xmlNode.RemoveAll();
                }
                else
                {
                    string[] arr = AttriList.Split(',');
                    for (int i = 0; i < xmlNode.Attributes.Count; i++)
                    {
                        int j = (AttriList + ",").IndexOf(xmlNode.Attributes[i].Name + ",");
                        if ((AttriList + ",").IndexOf(xmlNode.Attributes[i].Name + ",") > -1)
                        {
                            xmlNode.Attributes.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
        }

        //增加节点属性和值(如果要添加的节点有重复的时候用)
        public void AddNodeAndValueOfAttri(XmlNode xmlNode, string name, string value)
        {
            XmlElement xe = xmlDoc.CreateElement(name);
            xe.InnerText = value;
            XmlAttribute xa = xmlDoc.CreateAttribute(name);
            xa.Value = value;
            xmlNode.Attributes.Append(xa);
            //XmlNodeList iNodeList = xmlDoc.SelectNodes("//" + pKey);
            //for (int i = 0; i < iNodeList.Count; i++)
            //{
            //    XmlNode xmlNode = iNodeList[i];
            //    XmlAttribute attr = xmlDoc.CreateAttribute(name);
            //    attr.Value = value;
            //    XmlNode node = xmlNode.Attributes.Append(attr);
            //}
            //XmlAttribute attr = xmlDoc.CreateAttribute(name);
            //attr.Value = value;
            //iNodeList[iNodeList.Count - 1].Attributes.Append(attr);
        }

        //增加节点属性和值
        public void AddNodeAndValueOfAttri(string pKey, string name, string value)
        {
            //XmlElement xe = xmlDoc.CreateElement(name);
            //xe.InnerText = value;
            XmlNodeList iNodeList = xmlDoc.SelectNodes("//" + pKey);
            //for (int i = 0; i < iNodeList.Count; i++)
            //{
            //    XmlNode xmlNode = iNodeList[i];
            //    XmlAttribute attr = xmlDoc.CreateAttribute(name);
            //    attr.Value = value;
            //    XmlNode node = xmlNode.Attributes.Append(attr);
            //}
            XmlAttribute attr = xmlDoc.CreateAttribute(name);
            attr.Value = value;
            iNodeList[iNodeList.Count - 1].Attributes.Append(attr);
        }

        public XmlNodeList GetChildList(string key)
        {
            XmlNode xmlNode = xmlDoc.SelectSingleNode("//" + key);
            XmlNodeList xmlNodelist = xmlDoc.SelectNodes("//" + key);
            return xmlNodelist;
        }

        //获取节点属性值
        public string GetAttributeValue(string key, string name)
        {
            XmlNode iNode = xmlDoc.SelectSingleNode("//" + key);
            if (null != iNode)
            {
                return iNode.Attributes[name].Value;
            }
            return string.Empty;
        }
        //返回节点属性值列表
        public IDictionary<string, string> GetAttributeList(string key)
        {
            IDictionary<string, string> list = new Dictionary<string, string>();
            XmlNode iNode = xmlDoc.SelectSingleNode("//" + key);
            if (null != iNode)
            {
                foreach (XmlAttribute iAttribute in iNode.Attributes)
                {
                    list.Add(iAttribute.Name, iAttribute.Value);
                }
            }
            return list;
        }
        //返回子节点属性值列表
        public DataTable GetAttributesList(string key)
        {
            DataTable dt = new DataTable();
            XmlNode iNode = xmlDoc.SelectSingleNode("//" + key);
            if (null != iNode && iNode.HasChildNodes)
            {
                foreach (XmlNode xn in iNode.ChildNodes)
                {
                    DataRow newRow;
                    newRow = dt.NewRow();
                    foreach (XmlAttribute iAttribute in xn.Attributes)
                    {
                        if (dt.Rows.Count == 0)
                            dt.Columns.Add(iAttribute.Name);
                        newRow[iAttribute.Name] = iAttribute.Value;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        //返回所有子节点属性值列表（用于有很多名称一样的子节点）;
        //public DataTable GetAttributesList(string key)
        //{
        //    DataTable dt = new DataTable();
        //    XmlNode iNode = xmlDoc.SelectSingleNode("//" + key);
        //    if (null != iNode && iNode.HasChildNodes)
        //    {
        //        foreach (XmlNode xn in iNode.ChildNodes)
        //        {
        //            DataRow newRow;
        //            newRow = dt.NewRow();
        //            foreach (XmlAttribute iAttribute in xn.Attributes)
        //            {
        //                if (dt.Rows.Count == 0)
        //                    dt.Columns.Add(iAttribute.Name);
        //                newRow[iAttribute.Name] = iAttribute.Value;
        //            }
        //            dt.Rows.Add(newRow);
        //        }
        //    }
        //    return dt;
        //}

        //增加修改属性值
        public void SetAttributeValue(string key, string name, string value)
        {
            XmlNodeList iNodeList = xmlDoc.SelectNodes("//" + key);
            XmlNode iNode = iNodeList[iNodeList.Count - 1];
            if (null != iNode)
            {
                if (null != iNode.Attributes[name])
                {
                    iNode.Attributes[name].Value = value;
                }
                else
                {
                    XmlAttribute iAttribute = xmlDoc.CreateAttribute(name);
                    iAttribute.Value = value;
                    iNode.Attributes.Append(iAttribute);
                }
            }
        }
        //增加节点并添加属性值列表
        public void SetAttributesValue(string pKey, string key)
        {
            XmlNode iNode = xmlDoc.SelectSingleNode("//" + pKey);
            if (null != iNode)
            {
                iNode.AppendChild(xmlDoc.CreateElement(key));
                XmlNodeList cNodeList = iNode.SelectNodes("//" + key);
                foreach (KeyValuePair<string, string> d in ControlList)
                {
                    XmlAttribute iAttribute = xmlDoc.CreateAttribute(d.Key);
                    iAttribute.Value = d.Value;
                    cNodeList[cNodeList.Count - 1].Attributes.Append(iAttribute);
                }
            }
            ControlList.Clear();
        }
        //获取错误消息体节点属性值
        public string GetErrAttValue()
        {
            XmlNode iNode = xmlDoc.SelectNodes("//msg")[1];
            if (null != iNode)
            {
                return iNode.Attributes[0].Value + ":" + iNode.Attributes[1].Value;
            }
            return string.Empty;
        }
        //格式化xml字符串
        public string FormatXml()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(new System.IO.StringWriter(sb));
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteTo(writer);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
            return sb.ToString();
        }

        public static DataSet GetDSByXml(string xmlPath)
        {
            DataSet ds = new DataSet();          
            ds.ReadXml(xmlPath);
            return ds;
        }
    }
}
