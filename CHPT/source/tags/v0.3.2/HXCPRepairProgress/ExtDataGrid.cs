using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HXCPRepairProgress
{
    /// <summary>
    /// 自定义DataGrid
    /// </summary>
    public sealed class ExtDataGrid : Border
    {
        #region 构造函数
        public ExtDataGrid()
        {
            if (Grid.Parent == null)
            {
                Child = Grid;
            }
            RowCols = new ObservableCollection<Row>();
            ColumnCols = new ObservableCollection<Column>();
            CellCols = new ObservableCollection<Cell>();
        }
        static ExtDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtDataGrid), new FrameworkPropertyMetadata(typeof(ExtDataGrid)));
        }
        #endregion


        #region Field -- 字段
        private Boolean _columnInit, _rowInit;
        private static readonly Grid Grid = new Grid();
        #endregion

        #region Property -- 属性
        public String Title { get; set; }
        /// <summary>
        /// 单元格单击事件代理
        /// </summary>
        /// <param name="key"></param>
        public delegate void CellClickDelegate(String key);
        /// <summary>
        /// 单元格单击事件
        /// </summary>
        public CellClickDelegate CellClickEvenHandle;
        private ObservableCollection<Row> RowCols { get; set; }
        private ObservableCollection<Column> ColumnCols { get; set; }
        private ObservableCollection<Cell> CellCols { get; set; }

        #region ColMinWidth -- 数据列最小宽度
        /// <summary>
        /// 数据列最小宽度
        /// </summary>
        public static readonly DependencyProperty ColMinWidthProperty = DependencyProperty.Register(
            "ColMinWidth", typeof(Double), typeof(ExtDataGrid), new PropertyMetadata(default(Double)));
        /// <summary>
        /// 数据列最小宽度
        /// </summary>
        public Double ColMinWidth
        {
            get { return (Double)GetValue(ColMinWidthProperty); }
            set { SetValue(ColMinWidthProperty, value); }
        }
        #endregion

        #region RowMinHeight -- 数据行最小高度
        /// <summary>
        /// 数据行最小高度
        /// </summary>
        public static readonly DependencyProperty RowMinHeightProperty = DependencyProperty.Register(
            "RowMinHeight", typeof(Double), typeof(ExtDataGrid), new PropertyMetadata(default(Double)));
        /// <summary>
        /// 数据行最小高度
        /// </summary>
        public Double RowMinHeight
        {
            get { return (Double)GetValue(RowMinHeightProperty); }
            set { SetValue(RowMinHeightProperty, value); }
        }
        #endregion
        #endregion

        #region Method -- 方法
        private UIElement BuildRowColumnContent(String content)
        {
            var label = new Label
            {
                Content = content,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            var border = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0.1),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Child = label
            };
            return border;
        }
        /// <summary>
        /// 生成单元格项
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private UIElement BuildCellItem(String content, String tag)
        {
            var label = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            var run = new Run(content);
            var link = new Hyperlink(run) { Tag = tag };
            link.Click += delegate(object sender, RoutedEventArgs args)
            {
                var cSender = sender as Hyperlink;
                if (cSender == null) return;
                if (CellClickEvenHandle != null)
                {
                    CellClickEvenHandle(cSender.Tag.ToString());
                }
            };
            label.Content = link;
            return label;
        }
        private void BuildGridHeader()
        {
            if (_columnInit || _rowInit) return;
            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowMinHeight / 2) });
            Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowMinHeight / 2) });
            Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(ColMinWidth) });
            var label = BuildRowColumnContent(String.IsNullOrEmpty(Title) ? "" : Title);
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 0);
            Grid.SetRowSpan(label, 2);
            Grid.Children.Add(label);
        }
        /// <summary>
        /// 清空行内容
        /// </summary>
        private void ClearRows()
        {
            ClearContentGrid();
            foreach (var row in RowCols)
            {
                Grid.Children.Remove(row.Element);
                Grid.RowDefinitions.Remove(row.RowDefinition);
            }
            if (RowCols != null) RowCols.Clear();
            _rowInit = false;
        }
        /// <summary>
        /// 生成单元格行
        /// </summary>
        /// <param name="dic">单元格行标识,单元格行名称</param>
        public void BuildGridRows(Dictionary<String, String> dic)
        {
            if (dic == null) return;
            ClearRows();
            BuildGridHeader();
            var i = 0;
            foreach (var item in dic)
            {
                var rowInfo = new Row();
                var row = new RowDefinition { MinHeight = RowMinHeight, Height = GridLength.Auto, Tag = item.Key };
                row.Tag = rowInfo;
                Grid.RowDefinitions.Add(row);
                var element = BuildRowColumnContent(item.Value);
                Grid.SetColumn(element, 0);
                Grid.SetRow(element, i + 2);
                Grid.Children.Add(element);
                rowInfo.Element = element;
                rowInfo.Key = item.Key;
                rowInfo.Name = item.Value;
                rowInfo.RowDefinition = row;
                RowCols.Add(rowInfo);
                i++;
            }
            _rowInit = true;
            BuildContentGrid();
        }
        /// <summary>
        /// 清理列内容
        /// </summary>
        private void ClearColumns()
        {
            ClearContentGrid();
            foreach (var column in ColumnCols)
            {
                Grid.Children.Remove(column.Element);
                foreach (var column2 in column.ColumnDefinitions)
                {
                    Grid.Children.Remove(column2.Element);
                    Grid.ColumnDefinitions.Remove(column2.ColumnDefinition);
                }
            }
            if (ColumnCols != null) ColumnCols.Clear();
            _columnInit = false;
        }
        /// <summary>
        /// 生成单元格列
        /// </summary>
        /// <param name="dic">单元格一级列头集合</param>
        /// <param name="dic2">单元格二级列头集合</param>
        public void BuildGridColumns(Dictionary<String, String> dic, Dictionary<String, String> dic2)
        {
            if (dic == null || dic2 == null) return;
            {
                ClearColumns();
                BuildGridHeader();
                var i = 0;
                foreach (var item in dic)
                {
                    var columnInfo = new Column { Key = item.Key, Name = item.Value };
                    var j = 0;
                    foreach (var item2 in dic2)
                    {

                        var column2Info = new Column2 { Key = item2.Key, Name = item2.Value };
                        var col = new ColumnDefinition { MinWidth = ColMinWidth, Width = GridLength.Auto };
                        var element4ColProperty = BuildRowColumnContent(item2.Value);
                        Grid.ColumnDefinitions.Add(col);
                        Grid.SetColumn(element4ColProperty, (i * dic2.Count) + (j + 1));
                        Grid.SetRow(element4ColProperty, 1);
                        Grid.Children.Add(element4ColProperty);
                        column2Info.ColumnDefinition = col;
                        column2Info.Element = element4ColProperty;
                        columnInfo.ColumnDefinitions.Add(column2Info);
                        j++;
                    }
                    var element4Col = BuildRowColumnContent(item.Value);
                    Grid.SetColumn(element4Col, (i * dic2.Count) + 1);
                    Grid.SetColumnSpan(element4Col, dic2.Count);
                    Grid.SetRow(element4Col, 0);
                    Grid.Children.Add(element4Col);
                    columnInfo.Element = element4Col;
                    ColumnCols.Add(columnInfo);
                    i++;
                }
                _columnInit = true;
                BuildContentGrid();
            }
        }
        private void ClearContentGrid()
        {
            foreach (var cell in CellCols)
            {
                Grid.Children.Remove(cell.Element);
            }
        }
        private void BuildContentGrid()
        {
            if (!_columnInit || !_rowInit) return;
            ClearContentGrid();
            foreach (var row in RowCols)
            {
                foreach (var column in ColumnCols)
                {
                    foreach (var column2 in column.ColumnDefinitions)
                    {
                        var border = new Border() { BorderBrush = Brushes.Black, BorderThickness = new Thickness(0.1) };
                        var sp = new StackPanel() { Orientation = Orientation.Vertical };
                        border.Child = sp;
                        var cell = new Cell
                        {
                            RowKey = row.Key,
                            ColumnKey = column.Key,
                            Column2Key = column2.Key,
                            Element = border
                        };
                        Grid.SetColumn(border, Grid.ColumnDefinitions.IndexOf(column2.ColumnDefinition));
                        Grid.SetRow(border, Grid.RowDefinitions.IndexOf(row.RowDefinition));
                        Grid.Children.Add(border);
                        CellCols.Add(cell);
                    }
                }
            }
        }
        /// <summary>
        /// 生成单元格
        /// </summary>
        /// <param name="rowKey">单元格行标识</param>
        /// <param name="colKey">单元格一级列标识</param>
        /// <param name="col2Key">单元格二级列标识</param>
        /// <param name="contentKey">单元格标识</param>
        /// <param name="content">单元格内容</param>
        public void AddCellContent(String rowKey, String colKey, String col2Key, String contentKey, String content)
        {
            foreach (var col in CellCols)
            {
                if (col.RowKey == rowKey && col.ColumnKey == colKey && col.Column2Key == col2Key)
                {
                    var cell = BuildCellItem(content, contentKey);
                    col.CellItems.Add(cell);
                }
            }
        }
        #endregion

        #region private class -- 内部私有类
        private class Row
        {
            public String Key { get; set; }
            public String Name { get; set; }
            public RowDefinition RowDefinition { get; set; }
            public UIElement Element { get; set; }
        }
        private class Column
        {
            public Column()
            {
                ColumnDefinitions = new ObservableCollection<Column2>();
            }

            public String Key { get; set; }
            public String Name { get; set; }
            public ObservableCollection<Column2> ColumnDefinitions { get; set; }
            public UIElement Element { get; set; }
        }
        private class Column2
        {
            public String Key { get; set; }
            public String Name { get; set; }
            public ColumnDefinition ColumnDefinition { get; set; }
            public UIElement Element { get; set; }
        }
        private class Cell
        {
            public Cell()
            {
                CellItems = new ObservableCollection<UIElement>();
                CellItems.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
                {
                    switch (args.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            if (Element != null)
                            {
                                var element = args.NewItems[0] as UIElement;
                                if (element != null)
                                {
                                    var border = Element as Border;
                                    if (border != null)
                                    {
                                        var stackPanel = border.Child as StackPanel;
                                        if (stackPanel != null)
                                        {
                                            stackPanel.Children.Add(element);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                };
            }
            public String RowKey { get; set; }
            public String ColumnKey { get; set; }
            public String Column2Key { get; set; }
            public UIElement Element { get; set; }
            public ObservableCollection<UIElement> CellItems { get; set; }
        }
        #endregion
    }
}
