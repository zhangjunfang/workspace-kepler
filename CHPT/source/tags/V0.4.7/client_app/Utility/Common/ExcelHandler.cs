using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using System.Collections;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using ServiceStationClient.ComponentUI;
using Utility.CommonForm;
using NPOI.SS.Formula.Eval;
using NPOI.XSSF.UserModel;

namespace Utility.Common
{
    /// <summary>Excel操作类
    /// create by 孙亚楠
    /// </summary>
    public class ExcelHandler
    {
        #region 将数据导出到excel
        /// <summary>  DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本，如无则为空</param>
        /// <param name="filePath">保存位置</param>
        /// <param name="percent">委托事件，更新进度条</param>
        public static void ExportDTtoExcel(DataTable dtSource, string strHeaderText, string filePath, Action<int> percent)
        {
            IWorkbook workbook;
            ISheet sheet;
            if (Path.GetExtension(filePath) == ".xlsx")
            {
                workbook = new XSSFWorkbook();
                sheet = workbook.CreateSheet() as XSSFSheet;
            }
            else if (Path.GetExtension(filePath) == ".xls")
            {
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet() as HSSFSheet;
            }
            else
            {
                throw new Exception("文件格式不正确");
            }

            #region 右击文件 属性信息

            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = ""; //填加xls文件作者信息
            //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
            //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
            //    si.Comments = "说明信息"; //填加xls文件作者信息
            //    si.Title = "NPOI测试"; //填加xls文件标题信息
            //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle() as ICellStyle;
            IDataFormat format = workbook.CreateDataFormat() as IDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;

            #region 新建表，填充表头，填充列头，样式

            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet() as ISheet;
                }
                #region 表头及样式
                {
                    if (!string.IsNullOrEmpty(strHeaderText))
                    {
                        IRow headerRow = sheet.CreateRow(rowIndex) as IRow;
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);
                        ICellStyle headStyle = workbook.CreateCellStyle() as ICellStyle;
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont() as IFont;
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                        rowIndex++;
                    }
                }
                #endregion
                #region 列头及样式
                {
                    IRow headerRow = sheet.CreateRow(rowIndex) as IRow;
                    ICellStyle headStyle = workbook.CreateCellStyle() as ICellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont() as IFont;
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    foreach (DataColumn column in dtSource.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                        //设置列宽
                        int colWiddth = (arrColWidth[column.Ordinal] + 1) * 256;
                        if (colWiddth > 255 * 256)
                        {
                            colWiddth = 255 * 256;
                        }
                        sheet.SetColumnWidth(column.Ordinal, colWiddth);
                    }
                    //headerRow.Dispose();
                    rowIndex++;
                }
                #endregion
            }
            #endregion

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex) as IRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal) as ICell;

                    string drValue = row[column].ToString();
                    newCell.SetCellValue(drValue);
                }
                #endregion

                rowIndex++;

                percent(string.IsNullOrEmpty(strHeaderText) ? (rowIndex - 1) : (rowIndex - 2));
            }
            FileStream fs = new FileStream(filePath, FileMode.Create);
            workbook.Write(fs);
            fs.Close();

        }


        /// <summary>  DataTable导出到Excel文件
        /// </summary>
        /// <param name="dgv">源DataGridView</param>
        /// <param name="strHeaderText">表头文本，如无则为空</param>
        /// <param name="filePath">保存位置</param>
        /// <param name="percent">委托事件，更新进度条</param>
        public static void ExportDTtoExcel(DataGridView dgv, string strHeaderText, string filePath, Action<int> percent)
        {
            IWorkbook workbook;
            ISheet sheet;
            if (Path.GetExtension(filePath) == ".xlsx")
            {
                workbook = new XSSFWorkbook();
                sheet = workbook.CreateSheet() as XSSFSheet;
            }
            else if (Path.GetExtension(filePath) == ".xls")
            {
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet() as HSSFSheet;
            }
            else
            {
                throw new Exception("文件格式不正确");
            }

            #region 右击文件 属性信息
            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = ""; //填加xls文件作者信息
            //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
            //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
            //    si.Comments = "说明信息"; //填加xls文件作者信息
            //    si.Title = "NPOI测试"; //填加xls文件标题信息
            //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle() as ICellStyle;
            IDataFormat format = workbook.CreateDataFormat() as IDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            List<string> listcol = new List<string>();
            List<int> listData = new List<int>();
            foreach (DataGridViewColumn vc in dgv.Columns)
            {
                if (!string.IsNullOrEmpty(vc.HeaderText) && vc.Visible && !listcol.Contains(vc.HeaderText))
                {
                    listcol.Add(vc.HeaderText);
                    listData.Add(vc.Index);
                }
            }

            //取得列宽
            int[] arrColWidth = new int[listcol.Count];
            for (int i = 0; i < listcol.Count; i++)
            {
                arrColWidth[i] = Encoding.GetEncoding(936).GetBytes(listcol[i]).Length;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < listcol.Count; j++)
                {
                    object value = dgv.Rows[i].Cells[listData[j]].Value;

                    int intTemp = 0;
                    if (value != null)
                    {
                        intTemp = Encoding.GetEncoding(936).GetBytes(value.ToString()).Length;
                    }
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            #region 新建表，填充表头，填充列头，样式
            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet() as ISheet;
                }
                #region 表头及样式
                {
                    if (!string.IsNullOrEmpty(strHeaderText))
                    {
                        IRow headerRow = sheet.CreateRow(rowIndex) as IRow;
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);
                        ICellStyle headStyle = workbook.CreateCellStyle() as ICellStyle;
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont() as IFont;
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dgv.Columns.Count - 1));
                        //headerRow.Dispose();
                        rowIndex++;
                    }
                }
                #endregion
                #region 列头及样式
                {
                    IRow headerRow = sheet.CreateRow(rowIndex) as IRow;
                    ICellStyle headStyle = workbook.CreateCellStyle() as ICellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont() as IFont;
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    for (int i = 0; i < listcol.Count; i++)
                    {
                        headerRow.CreateCell(i).SetCellValue(listcol[i]);
                        headerRow.GetCell(i).CellStyle = headStyle;
                        //设置列宽
                        int colWiddth = (arrColWidth[i] + 1) * 256;
                        if (colWiddth > 255 * 256)
                        {
                            colWiddth = 255 * 256;
                        }
                        sheet.SetColumnWidth(i, colWiddth);
                    }
                    //headerRow.Dispose();
                    rowIndex++;
                }
                #endregion
            }
            #endregion
            foreach (DataGridViewRow row in dgv.Rows)
            {
                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex) as IRow;
                for (int i = 0; i < listcol.Count; i++)
                {
                    ICell newCell = dataRow.CreateCell(i) as ICell;
                    string drValue = row.Cells[listData[i]].FormattedValue.ToString();
                    newCell.SetCellValue(drValue);
                }
                #endregion
                rowIndex++;
                percent(string.IsNullOrEmpty(strHeaderText) ? (rowIndex - 1) : (rowIndex - 2));
            }

            FileStream fs = new FileStream(filePath, FileMode.Create);
            workbook.Write(fs);
            fs.Close();
        }
        #endregion

        #region 从excel中将数据导出到datatable
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName)
        {
            DataTable dt = new DataTable();

            IWorkbook workBook = null;//创建Excel工作簿
            ISheet sheet = null;//获取第一个sheet表

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(strFileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(file);//生成支持Office2007以上版本的Excel表格工作簿
                    sheet = (XSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else if (Path.GetExtension(strFileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(file);//生成支持Office2003及以下版本的Excel表格工作簿
                    sheet = (HSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else
                {
                    throw new Exception("文件格式不正确");
                }
            }

            dt = ImportDt(sheet, 0, true);
            workBook = null;
            sheet = null;
            return dt;
        }

        /// <summary> 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            IWorkbook workBook = null;//创建Excel工作簿
            ISheet sheet = null;//获取第一个sheet表

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(strFileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(file);//生成支持Office2007以上版本的Excel表格工作簿
                    sheet = (XSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else if (Path.GetExtension(strFileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(file);//生成支持Office2003及以下版本的Excel表格工作簿
                    sheet = (HSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else
                {
                    throw new Exception("文件格式不正确");
                }
            }
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            workBook = null;
            sheet = null;
            return table;
        }

        /// <summary> 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            IWorkbook workBook = null;//创建Excel工作簿
            ISheet sheet = null;//获取第一个sheet表

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(strFileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(file);//生成支持Office2007以上版本的Excel表格工作簿
                    sheet = (XSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else if (Path.GetExtension(strFileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(file);//生成支持Office2003及以下版本的Excel表格工作簿
                    sheet = (HSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else
                {
                    throw new Exception("文件格式不正确");
                }
            }
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            workBook = null;
            sheet = null;
            return table;
        }

        /// <summary> 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
        {
            IWorkbook workBook = null;//创建Excel工作簿
            ISheet sheet = null;//获取第一个sheet表

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(strFileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(file);//生成支持Office2007以上版本的Excel表格工作簿
                    sheet = (XSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else if (Path.GetExtension(strFileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(file);//生成支持Office2003及以下版本的Excel表格工作簿
                    sheet = (HSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else
                {
                    throw new Exception("文件格式不正确");
                }
            }
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            workBook = null;
            sheet = null;
            return table;
        }

        /// <summary> 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
        {
            IWorkbook workBook = null;//创建Excel工作簿
            ISheet sheet = null;//获取第一个sheet表

            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(strFileName) == ".xlsx")
                {
                    workBook = new XSSFWorkbook(file);//生成支持Office2007以上版本的Excel表格工作簿
                    sheet = (XSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else if (Path.GetExtension(strFileName) == ".xls")
                {
                    workBook = new HSSFWorkbook(file);//生成支持Office2003及以下版本的Excel表格工作簿
                    sheet = (HSSFSheet)workBook.GetSheetAt(0);//获取第一个sheet表数据
                }
                else
                {
                    throw new Exception("文件格式不正确");
                }
            }
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            workBook = null;
            sheet = null;
            return table;
        }

        /// <summary> 将制定sheet中的数据导出到datatable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        static DataTable ImportDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            IRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as IRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as IRow;
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i));
                                table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        IRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as IRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as IRow;
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.Error:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }
        #endregion

        /// <summary> 根据DataGridView处理DataTable列名
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="dgv">DataGridView</param>
        /// <returns>改变列名的DataTable</returns>
        public static DataTable HandleDataTableForExcel(DataTable dt, DataGridView dgv)
        {
            DataTable dtNew = dt.Copy();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string propertyName = col.DataPropertyName;
                if (string.IsNullOrEmpty(col.DataPropertyName))
                {
                    continue;
                }
                if (col.Visible && !string.IsNullOrEmpty(col.HeaderText))
                {
                    dic.Add(col.DataPropertyName, col.HeaderText);
                }
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colName = dt.Columns[i].ColumnName;
                if (dic.ContainsKey(colName))
                {
                    dtNew.Columns[colName].ColumnName = dic[colName];
                }
                else
                {
                    dtNew.Columns.Remove(colName);
                }
            }
            return dtNew;
        }

        /// <summary> 导出excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dgv">数据控件DataGridView</param>
        /// <returns>错误信息</returns>
        public static void ExportExcel(string fileName, DataGridView dgv)
        {
            string dir = Application.StartupPath + @"\ExportFile";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = dir;
            sfd.Title = "导出文件";
            sfd.DefaultExt = "xls";
            sfd.Filter = "Microsoft Office Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx|Microsoft Office Excel 文件(*.xls)|*.xls|Microsoft Office Excel 文件(*.xlsx)|*.xlsx";
            sfd.FileName = dir + @"\" + fileName;
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PercentProcessOperator process = new PercentProcessOperator();
                #region 匿名方法，后台线程执行调用
                process.BackgroundWork =
                    delegate(Action<int> percent)
                    {
                        ExcelHandler.ExportDTtoExcel(dgv, "", sfd.FileName, percent);
                    };
                #endregion
                process.MessageInfo = "正在执行中";
                process.Maximum = dgv.RowCount;
                #region 匿名方法，后台线程执行完调用
                process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                        delegate(object osender, BackgroundWorkerEventArgs be)
                        {
                            if (be.BackGroundException == null)
                            {
                                MessageBoxEx.ShowInformation("导出成功！");
                            }
                            else
                            {
                                throw be.BackGroundException;
                            }
                        }
                    );
                #endregion
                process.Start();
            }
        }

        /// <summary> 导出excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dt">数据表</param>
        /// <returns>错误信息</returns>
        public static string ExportExcel(string fileName, DataTable dt)
        {
            string errMsg = string.Empty;
            string dir = Application.StartupPath + @"\ExportFile";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = dir;
            sfd.Title = "导出文件";
            sfd.DefaultExt = "xls";
            sfd.Filter = "Microsoft Office Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx|Microsoft Office Excel 文件(*.xls)|*.xls|Microsoft Office Excel 文件(*.xlsx)|*.xlsx";
            sfd.FileName = dir + @"\" + fileName;
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PercentProcessOperator process = new PercentProcessOperator();
                #region 匿名方法，后台线程执行调用
                process.BackgroundWork =
                    delegate(Action<int> percent)
                    {
                        ExcelHandler.ExportDTtoExcel(dt, "", sfd.FileName, percent);
                    };
                #endregion
                process.MessageInfo = "正在执行中";
                process.Maximum = dt.Rows.Count;
                #region 匿名方法，后台线程执行完调用
                process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                        delegate(object osender, BackgroundWorkerEventArgs be)
                        {
                            if (be.BackGroundException == null)
                            {
                                MessageBoxEx.ShowInformation("导出成功！");
                            }
                            else
                            {
                                throw be.BackGroundException;
                            }
                        }
                    );
                #endregion
                process.Start();
            }
            return errMsg;
        }
    }
}