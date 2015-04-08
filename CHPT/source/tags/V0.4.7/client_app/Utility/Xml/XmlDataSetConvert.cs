using System;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Utility.Xml
{
    public class XmlDataSetConvert
    {
        // Fields
        private string errorMessage;
        private string fullFileName;
        private string str_Path;

        // Methods
        public XmlDataSetConvert()
        {
            this.fullFileName = "";
            this.str_Path = Application.StartupPath + @"\temp.tbl";
            if (!File.Exists(this.str_Path))
            {
                File.Open(this.str_Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write).Close();
            }
        }

        public XmlDataSetConvert(string _fullFileName)
        {
            this.fullFileName = _fullFileName;
            this.str_Path = _fullFileName;
            //this.str_Path = Application.StartupPath + @"\temp.tbl";
            if (!File.Exists(this.str_Path))
            {
                File.Open(this.str_Path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write).Close();
            }
        }

        private bool CheckFileSuffix()
        {
            string[] strArray = this.fullFileName.Split(new char[] { '.' });
            if (strArray.Length <= 0)
            {
                this.errorMessage = "该文件无后缀名";
                return false;
            }
            if ((strArray[strArray.Length - 1].ToUpper() == "CFG") || (strArray[strArray.Length - 1].ToUpper() == "TBL"))
            {
                return true;
            }
            this.errorMessage = "系统不支持该文件格式,请确保文件后缀名为[.CFG] 或者[.TBL]";
            return false;
        }

        public bool DataSetToXML(DataSet _dataSet)
        {
            bool flag;
            if ((_dataSet == null) || (_dataSet.Tables.Count <= 0))
            {
                this.errorMessage = "数据集为空或者无数据";
                return false;
            }
            string[] strArray = this.fullFileName.Split(new char[] { '\\' });
            string path = "";
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                if (i == (strArray.Length - 2))
                {
                    path = path + strArray[i];
                }
                else
                {
                    path = path + strArray[i] + @"\";
                }
            }
            XmlTextWriter writer = null;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(this.fullFileName))
                {
                    File.Delete(this.fullFileName);
                }
                File.Open(this.fullFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Write).Close();
                writer = new XmlTextWriter(this.fullFileName, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                writer.WriteStartDocument();
                writer.WriteStartElement("DS");
                writer.WriteAttributeString("Name", _dataSet.GetHashCode().ToString());
                writer.WriteAttributeString("Num", _dataSet.Tables.Count.ToString());
                foreach (DataTable table in _dataSet.Tables)
                {
                    writer.WriteStartElement("DT");
                    writer.WriteAttributeString("Name", table.TableName);
                    writer.WriteAttributeString("Num", table.Rows.Count.ToString());
                    if (table.Rows.Count == 0)
                    {
                        writer.WriteStartElement("DR");
                        writer.WriteAttributeString("Name", "1");
                        writer.WriteAttributeString("Num", "-1");
                        foreach (DataColumn column in table.Columns)
                        {
                            writer.WriteStartElement("DC");
                            writer.WriteAttributeString("Name", column.ColumnName);
                            writer.WriteAttributeString("Num", "0");
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    else
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            writer.WriteStartElement("DR");
                            writer.WriteAttributeString("Name", row.GetHashCode().ToString());
                            writer.WriteAttributeString("Num", table.Columns.Count.ToString());
                            foreach (DataColumn column in table.Columns)
                            {
                                writer.WriteStartElement("DC");
                                writer.WriteAttributeString("Name", column.ColumnName);
                                writer.WriteAttributeString("Num", "0");
                                writer.WriteString(row[column.ColumnName].ToString().Replace("<", "(!lt!)").Replace(">", "(!gt!)"));
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                flag = true;
            }
            catch (Exception exception)
            {
                this.errorMessage = exception.Message;
                flag = false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
            return flag;
        }

        public string DataSetToXMLString(DataSet _dataSet)
        {
            if (_dataSet == null)
            {
                this.errorMessage = "数据集为空";
                return string.Empty;
            }
            string fullFileName = this.fullFileName;
            this.fullFileName = this.str_Path;
            if (!this.DataSetToXML(_dataSet))
            {
                this.fullFileName = fullFileName;
                return string.Empty;
            }
            this.fullFileName = fullFileName;
            try
            {
                return File.ReadAllText(this.str_Path, Encoding.UTF8).Replace("\r", " ").Replace("\n", " ");
            }
            catch (Exception exception)
            {
                this.errorMessage = exception.Message;
                return string.Empty;
            }
        }

        public DataSet XMLStringToDataSet(string _xmlString)
        {
            string fullFileName = this.fullFileName;
            this.fullFileName = this.str_Path;
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(this.str_Path, false, Encoding.UTF8);
                writer.Write(_xmlString);
                writer.Close();
            }
            catch (Exception exception)
            {
                this.errorMessage = exception.Message;
                this.fullFileName = fullFileName;
                return null;
            }
            DataSet set = this.XMLToDataSet();
            this.fullFileName = fullFileName;
            return set;
        }

        public DataSet XMLToDataSet()
        {
            DataSet set2;
            if (!this.CheckFileSuffix())
            {
                return null;
            }
            DataSet set = null;
            DataTable table = null;
            DataRow row = null;
            DataColumn column = null;
            XmlTextReader reader = null;
            string attribute = "";
            try
            {
                reader = new XmlTextReader(this.fullFileName);
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == "DS"))
                    {
                        set = new DataSet();
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == "DT"))
                    {
                        table = new DataTable
                        {
                            TableName = reader.GetAttribute(0)
                        };
                    }
                    if (((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == "DR")))
                    {
                        attribute = reader.GetAttribute("Num");
                        row = table.NewRow();
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == "DC"))
                    {
                        bool flag = false;
                        column = new DataColumn
                        {
                            ColumnName = reader.GetAttribute(0)
                        };
                        foreach (DataColumn column2 in table.Columns)
                        {
                            if (column2.ColumnName == column.ColumnName)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            table.Columns.Add(column);
                        }
                        if (attribute != "0")
                        {
                            var value = reader.ReadString();
                            value = value.Replace("(!lt!)", "<").Replace("(!gt!)", ">");
                            row[column.ColumnName] = value;
                        }
                    }
                    if (((reader.NodeType == XmlNodeType.EndElement) && (reader.LocalName == "DR")) && (attribute != "-1"))
                    {
                        table.Rows.Add(row);
                    }
                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.LocalName == "DT"))
                    {
                        set.Tables.Add(table);
                    }
                }
                set2 = set;
            }
            catch (Exception exception)
            {
                this.errorMessage = exception.Message;
                set2 = null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return set2;
        }

        public DataTable XMLToDataTable(string _tableName)
        {
            DataTable table2;
            if (!this.CheckFileSuffix())
            {
                return null;
            }
            DataSet ds = this.XMLToDataSet();
            if (ds == null || ds.Tables.Count <= 0)
            {
                table2 = null;
            }
            else
            {
                table2 = ds.Tables[_tableName];
            }

            return table2;
        }
    }
}
