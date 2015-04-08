using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace testWinForm.Report
{
     public class RdlGenerator
   {
       private ReportConfig _rpc;

       /// <summary>
       /// 报表配置
       /// </summary>
       public ReportConfig ReportConfig
       {
           get { return _rpc; }
           set { _rpc = value; }
       }

       /// <summary>
       /// 创建报表
       /// </summary>
       /// <returns></returns>
       private Rdl.Report CreateReport()
       {
           Rdl.Report report = new Rdl.Report();
           string w = "", h = "", lm = "", tm = "", rm = "", bm = "";
           //设置报表页面
           if(!string.IsNullOrEmpty(_rpc.Unit))
           {
               w = _rpc.Width + _rpc.Unit;
               h = _rpc.Height + _rpc.Unit;
               lm = _rpc.LeftMargin + _rpc.Unit;
               tm = _rpc.TopMargin + _rpc.Unit;
               rm = _rpc.RightMargin + _rpc.Unit;
               bm = _rpc.BottomMargin + _rpc.Unit;
           }
           else
           {
               w = (_rpc.PageSettings.PaperSize.Width / 96.0) + "in";
               h = (_rpc.PageSettings.PaperSize.Height / 96.0) + "in";
               lm = (_rpc.LeftMargin / 96.0) + "in";
               tm = (_rpc.TopMargin / 96.0) + "in";
               rm = (_rpc.RightMargin / 96.0) + "in";
               bm = (_rpc.BottomMargin / 96.0) + "in";
           }
           report.Items = new object[] 
               {
                   CreateDataSources(),
                   CreateHeader(),
                   CreateBody(),
                   CreateFooter(),
                   CreateDataSets(),
                   w,
                   h,
                   lm,
                   tm,
                   rm,
                   bm,
               };
           report.ItemsElementName = new Rdl.ItemsChoiceType37[]
               { 
                   Rdl.ItemsChoiceType37.DataSources, 
                   Rdl.ItemsChoiceType37.PageHeader,
                   Rdl.ItemsChoiceType37.Body,
                   Rdl.ItemsChoiceType37.PageFooter,
                   Rdl.ItemsChoiceType37.DataSets,
                   Rdl.ItemsChoiceType37.Width,
                   Rdl.ItemsChoiceType37.PageHeight,
                   Rdl.ItemsChoiceType37.LeftMargin,
                   Rdl.ItemsChoiceType37.TopMargin,
                   Rdl.ItemsChoiceType37.RightMargin,
                   Rdl.ItemsChoiceType37.BottomMargin,
               };
           return report;
       }

       #region 数据源
       /// <summary>
       /// 数据源
       /// </summary>
       /// <returns></returns>
       private Rdl.DataSourcesType CreateDataSources()
       {
           Rdl.DataSourcesType dataSources = new Rdl.DataSourcesType();
           dataSources.DataSource = new Rdl.DataSourceType[] { CreateDataSource() };
           return dataSources;
       }

       private Rdl.DataSourceType CreateDataSource()
       {
           Rdl.DataSourceType dataSource = new Rdl.DataSourceType();
           dataSource.Name = "FaibLists";
           dataSource.Items = new object[] { CreateConnectionProperties() };
           return dataSource;
       }

        private Rdl.ConnectionPropertiesType CreateConnectionProperties()
        {
            Rdl.ConnectionPropertiesType connectionProperties = new Rdl.ConnectionPropertiesType();
            connectionProperties.Items = new object[]
                {
                    "",
                    "SQL",
                };
            connectionProperties.ItemsElementName = new Rdl.ItemsChoiceType[]
                {
                    Rdl.ItemsChoiceType.ConnectString,
                    Rdl.ItemsChoiceType.DataProvider,
                };
            return connectionProperties;
        }
        #endregion
 
        #region 主体
        /// <summary>
        /// 报表主体
        /// </summary>
        /// <returns></returns>
        private Rdl.BodyType CreateBody()
        {
            Rdl.BodyType body = new Rdl.BodyType();
            body.Items = new object[]
                {
                    CreateReportItems(),
                    "1in",
                };
            body.ItemsElementName = new Rdl.ItemsChoiceType30[]
                {
                    Rdl.ItemsChoiceType30.ReportItems,
                    Rdl.ItemsChoiceType30.Height,
                };
            return body;
        }
 
        private Rdl.ReportItemsType CreateReportItems()
        {
            Rdl.ReportItemsType reportItems = new Rdl.ReportItemsType();
            TableRdlGenerator tableGen = new TableRdlGenerator();
            tableGen.Fields = _rpc.DataItem;
            reportItems.Items = new object[] { tableGen.CreateTable() };
            return reportItems;
        }
        #endregion
 
        #region 页头页尾
        private Rdl.PageHeaderFooterType CreateHeader()
        {
            Rdl.PageHeaderFooterType header = new Rdl.PageHeaderFooterType();
            HeaderFooterRdlGenerator headerGen = new HeaderFooterRdlGenerator();
            headerGen.Fields = _rpc.Header;
 
            header.Items = new object[]
                {
                    (_rpc.HeadHeight / 96.0) + "in",
                    true,
                    true,
                    headerGen.CreateItems(),
                };
            header.ItemsElementName = new Rdl.ItemsChoiceType34[]
                {
                    Rdl.ItemsChoiceType34.Height,
                    Rdl.ItemsChoiceType34.PrintOnFirstPage,
                    Rdl.ItemsChoiceType34.PrintOnLastPage,
                    Rdl.ItemsChoiceType34.ReportItems
                };
            return header;
        }
 
        private Rdl.PageHeaderFooterType CreateFooter()
        {
            Rdl.PageHeaderFooterType footer = new Rdl.PageHeaderFooterType();
            HeaderFooterRdlGenerator footerGen = new HeaderFooterRdlGenerator();
            footerGen.Fields = _rpc.Footer;
 
            footer.Items = new object[]
                {
                    (_rpc.FootHeight / 96.0) + "in",
                    true,
                    true,
                    footerGen.CreateItems(),
                };
            footer.ItemsElementName = new Rdl.ItemsChoiceType34[]
                {
                    Rdl.ItemsChoiceType34.Height,
                    Rdl.ItemsChoiceType34.PrintOnFirstPage,
                    Rdl.ItemsChoiceType34.PrintOnLastPage,
                    Rdl.ItemsChoiceType34.ReportItems
                };
            return footer;
        }
        #endregion
 
        #region 数据集
        private Rdl.DataSetsType CreateDataSets()
        {
            Rdl.DataSetsType dataSets = new Rdl.DataSetsType();
            dataSets.DataSet = new Rdl.DataSetType[] { CreateDataSet() };
            return dataSets;
        }
 
        private Rdl.DataSetType CreateDataSet()
        {
            Rdl.DataSetType dataSet = new Rdl.DataSetType();
            dataSet.Name = "FaibLists";
            dataSet.Items = new object[] { CreateQuery(), CreateFields() };
            return dataSet;
        }
 
        private Rdl.QueryType CreateQuery()
        {
            Rdl.QueryType query = new Rdl.QueryType();
            query.Items = new object[] 
                {
                    "FaibLists",
                    "",
                };
            query.ItemsElementName = new Rdl.ItemsChoiceType2[]
                {
                    Rdl.ItemsChoiceType2.DataSourceName,
                    Rdl.ItemsChoiceType2.CommandText,
                };
            return query;
        }
 
        private Rdl.FieldsType CreateFields()
        {
            Rdl.FieldsType fields = new Rdl.FieldsType();
            Dictionary<string, TextItem> m_fields = _rpc.DataItem;
            fields.Field = new Rdl.FieldType[m_fields.Count];
            int i = 0;
            foreach (string key in m_fields.Keys)
            {
                fields.Field[i++] = CreateField(m_fields[key]);
            }
 
            return fields;
        }
 
        private Rdl.FieldType CreateField(TextItem item)
        {
            Rdl.FieldType field = new Rdl.FieldType();
            field.Name = item.DataMember;
            field.Items = new object[] { item.DataMember };
            field.ItemsElementName = new Rdl.ItemsChoiceType1[] { Rdl.ItemsChoiceType1.DataField };
            return field;
        }
        #endregion
 
        public void WriteXml(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Rdl.Report));
            serializer.Serialize(stream, CreateReport());
        }
    }
}
