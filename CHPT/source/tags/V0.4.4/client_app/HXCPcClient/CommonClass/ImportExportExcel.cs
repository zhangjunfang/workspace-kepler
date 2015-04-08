using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using System.IO;
using System.Data.OleDb;
using NPOI.XSSF.UserModel;
using HXCPcClient.Chooser.CommonForm;
using System.Drawing;
namespace HXCPcClient.CommonClass
{

    /// <summary>
    /// 创建人：赵学营
    /// 内容摘要：根据选择的单据行创建Excel文件
    /// </summary>
    public class ImportExportExcel
    {
        /// <summary>
        /// 利用NPOI导入Excel文件数据
        /// </summary>
        /// <param name="XlsFilesPathName">获取选定文件的路径名</param>
        /// <returns></returns>
        public static DataTable NPOIImportExcelFile(string XlsFilesPathName)
        {
            try
            {

                DataTable ExcelTable = new DataTable();//创建填充数据表

                if (XlsFilesPathName == string.Empty)
                {
                    MessageBoxEx.Show("请您选择要导入的配件信息文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (Path.GetExtension(XlsFilesPathName) != ".xlsx" && Path.GetExtension(XlsFilesPathName) != ".xls")
                {
                    MessageBoxEx.Show("请您选择Excel格式的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                else
                {
                    IWorkbook WkBk = null;//创建Excel工作簿
                    ISheet sht = null;//获取第一个sheet表
                    IRow HeaderRow = null;//获取Excel标题行
                    FileStream fsxls = new FileStream(XlsFilesPathName, FileMode.Open, FileAccess.Read);//读取Excel文件流
                    if (Path.GetExtension(XlsFilesPathName) == ".xlsx")
                    {
                        WkBk = new XSSFWorkbook(fsxls);//生成支持Office2007以上版本的Excel表格工作簿
                        sht = (XSSFSheet)WkBk.GetSheetAt(0);//获取第一个sheet表数据
                        HeaderRow = (XSSFRow)sht.GetRow(0);//获取Excel标题行
                    }
                    else if (Path.GetExtension(XlsFilesPathName) == ".xls")
                    {
                        WkBk = new HSSFWorkbook(fsxls);//生成支持Office2003及以下版本的Excel表格工作簿
                        sht = (HSSFSheet)WkBk.GetSheetAt(0);//获取第一个sheet表数据
                        HeaderRow = (HSSFRow)sht.GetRow(0);//获取Excel标题行
                    }
                    fsxls.Close();
                    fsxls.Dispose();

                    int FirstCellIndx = HeaderRow.FirstCellNum;//获取Excel标题行首列索引
                    int LastCellIndx = HeaderRow.LastCellNum;//获取Excel标题行尾列索引
                    //创建表列
                    for (int i = FirstCellIndx; i < LastCellIndx; i++)
                    {
                        DataColumn DataCol = new DataColumn();
                        DataCol.ColumnName = HeaderRow.GetCell(i).StringCellValue;//标题列名称
                        ExcelTable.Columns.Add(DataCol);//添加表列名
                    }
                    int FirstRowIndx = sht.FirstRowNum;//获取Excel首行索引
                    int LastRowIndx = sht.LastRowNum;//获取Excel首行索引

                    //创建表行
                    for (int j = FirstRowIndx + 1; j < LastRowIndx; j++)
                    {
                        IRow DatasRow = sht.GetRow(j);
                        DataRow TableRow = ExcelTable.NewRow();//创建表行
                        for (int k = FirstCellIndx; k < LastCellIndx; k++)
                        {
                            //获取Excel表行数据值
                            string XlsCellValue = DatasRow.GetCell(k).ToString() == string.Empty ? "" : DatasRow.GetCell(k).ToString();
                            TableRow[k] = XlsCellValue;

                        }
                        ExcelTable.Rows.Add(TableRow);//添加表行

                    }
                    WkBk = null;
                    sht = null;
                }
                return ExcelTable;//返回数据表

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return null;
            }
        }
        /// <summary>
        /// 利用office组件读取Excel表格中配件信息
        /// </summary>
        /// <returns></returns>
        //public static DataTable ImportExcelFile(string FilesPathName)
        //{
        //    try
        //    {
        //        string ExcelFilePath = string.Empty;//存储Excel文件路径
        //        string ExcelConnStr = string.Empty;//读取Excel表格字符串
        //        DataTable ExcelTable = null;//获取Excel表格中的数据
        //        string SheetName = string.Empty;//Excel中表名
        //        string ExcelSql = string.Empty;//查询Excel表格

        //        ExcelFilePath = FilesPathName;//获取选定文件的路径名
        //        if (Path.GetExtension(ExcelFilePath) != ".xlsx" && Path.GetExtension(ExcelFilePath) != ".xls")
        //        {
        //            MessageBoxEx.Show("请您输入Excel格式的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        }
        //        else if (ExcelFilePath == string.Empty)
        //        {
        //            MessageBoxEx.Show("请您选择要导入的配件信息文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        }
        //        else
        //        {
        //            //HDR=YES代表Excel第一行是标题不作为数据使用，IMEX三种模式：0代表写入模式，1代表读取模式，2代表链接模式（支持同时读取和写入）
        //            ExcelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
        //            OleDbConnection DbCon = new OleDbConnection(ExcelConnStr);//打开数据源连接
        //            DbCon.Open();
        //            ExcelTable = DbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "table" });//获取Excel表格中的数据
        //            SheetName = ExcelTable.Rows[0]["table_name"].ToString();//获取Excel表名称
        //            ExcelSql = "select * from [" + SheetName + "]";
        //            OleDbDataAdapter DbAdapter = new OleDbDataAdapter(ExcelSql, DbCon);//执行数据查询
        //            DataSet ds = new DataSet();
        //            DbAdapter.Fill(ds, SheetName);//填充数据集
        //            DbCon.Close();//关闭数据源连接
        //            ExcelTable = ds.Tables[0];

        //        }


        //        return ExcelTable;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
        //        return null;
        //    }
        //}
        /// <summary>
        /// 利用NPOI创建Excel文件
        /// </summary>
        /// <param name="ExcelTable">excel表格名称</param>
        /// <param name="SheetName">sheet表名</param>
        public static void NPOIExportExcelFile(DataTable NPOIExcelTable, string SheetName)
        {
            try
            {
                string SaveExcelName = string.Empty;//保存的Excel文件名称
                SaveFileDialog SFDialog = new SaveFileDialog();
                SFDialog.DefaultExt = "xls";
                SFDialog.Filter = "Excel文件(*.xls)|*.xls";
                SFDialog.ShowDialog();
                SFDialog.CheckPathExists = true;
                SFDialog.CheckFileExists = true;
                SaveExcelName = SFDialog.FileName;//获取保存的Excel文件名称
                if (File.Exists(SaveExcelName))
                {
                    if (IsExcelOpen(SaveExcelName)) File.Delete(SaveExcelName);//替换同名文件
                    else return;
                }
                if (SaveExcelName.IndexOf(":") < 0) return;//点击取消按钮返回
                HSSFWorkbook WkBk = new HSSFWorkbook();//创建工作表流
                MemoryStream ms = new MemoryStream();//创建支持内存的存储区流
                HSSFSheet sht = (HSSFSheet)WkBk.CreateSheet(SheetName);//创建Excel表格
                sht.CreateFreezePane(0, 1, 0, 1);//设置冻结首行
                HSSFRow HeaderRow = (HSSFRow)sht.CreateRow(0);//创建标题行
                int TotalCount = NPOIExcelTable.Rows.Count;
                int rowRead = 0;//读取行数
                ProgressBarFrm ProgBarFrm = new ProgressBarFrm();
                ProgBarFrm.MaxNum = TotalCount;//获取最大记录行
                ProgBarFrm.ShowDialog();//显示进度条
                for (int i = 0; i < NPOIExcelTable.Columns.Count; i++)
                {
                    HeaderRow.CreateCell(i).SetCellValue(NPOIExcelTable.Columns[i].ColumnName.ToString());//创建标题列
                    SetXlsHeaderStyle(HeaderRow, WkBk, i);//设置标题样式
                    sht.SetColumnWidth(i, 1000 * 5);//设置列宽
                }

                //创建Excel数据行项
                for (int j = 0; j < NPOIExcelTable.Rows.Count; j++)
                {
                    HSSFRow DatasRow = (HSSFRow)sht.CreateRow(j + 1);//创建数据行
                    for (int k = 0; k < NPOIExcelTable.Columns.Count; k++)
                    {//填充Excel表数据
                        DatasRow.CreateCell(k).SetCellValue(NPOIExcelTable.Rows[j][k].ToString());
                    }
                    rowRead++;//自动增长行号
                    ProgressBarFrm.CurrentValue = rowRead;//传递当前值
                    Application.DoEvents();//处理当前在消息队列中所有windows消息
                }

                //写入内存流数据
                WkBk.Write(ms);
                ms.Flush();//清空缓存
                ms.Position = 0;
                sht = null;
                HeaderRow = null;
                WkBk = null;
                FileStream fs = new FileStream(SaveExcelName, FileMode.CreateNew, FileAccess.Write);//创建文件流
                byte[] dataXls = ms.ToArray();//把文件流转换为字节数组
                fs.Write(dataXls, 0, dataXls.Length);
                fs.Flush();//清除缓存
                fs.Close();//关闭文件流
                dataXls = null;
                ms = null;
                fs = null;

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }


        /// <summary>
        /// 利用office组件导出Excel表格文件
        /// </summary> 
        //public static void ExportExcelFile(DataTable ExcelTable)
        //{
        //    try
        //    {

        //        string SaveExcelName = string.Empty;//保存的Excel文件名称
        //        SaveFileDialog SFDialog = new SaveFileDialog();
        //        SFDialog.DefaultExt = "xls";
        //        SFDialog.Filter = "Excel文件(*.xls)|*.xls";
        //        SFDialog.ShowDialog();
        //        SaveExcelName = SFDialog.FileName;//获取保存的Excel文件名称
        //        if (SaveExcelName.IndexOf(":") < 0) return;
        //        Microsoft.Office.Interop.Excel.Application XlsApp = new Microsoft.Office.Interop.Excel.Application();//创建Excel应用程序

        //        object missing = System.Reflection.Missing.Value;
        //        if (XlsApp == null)
        //        {
        //            MessageBoxEx.Show("无法创建Excel表格文件，您的电脑可能未安装Excel软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        else
        //        {

        //            Microsoft.Office.Interop.Excel.Workbooks WkBks = XlsApp.Workbooks;//获取工作簿对像
        //            Microsoft.Office.Interop.Excel.Workbook WkBk = WkBks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);//添加Excel表格模板
        //            Microsoft.Office.Interop.Excel.Worksheet WkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WkBk.Worksheets[1];//获取工作表格1;
        //            Microsoft.Office.Interop.Excel.Range Ran;//声明Excel表格
        //            int TotalCount = ExcelTable.Rows.Count;
        //            //int rowRead = 0;//读取行数
        //            //float PercentRead = 0;//导入百分比
        //            //写入字段名
        //            for (int i = 0; i < ExcelTable.Columns.Count; i++)
        //            {
        //                WkSheet.Cells[1, i + 1] = ExcelTable.Columns[i].ColumnName.ToString();//获取表列名称
        //                Ran = (Microsoft.Office.Interop.Excel.Range)WkSheet.Cells[1, i + 1];//列名称写入单元格
        //                Ran.Interior.ColorIndex = 15;
        //                Ran.Font.Bold = true;//标题加粗

        //            }

        //            //ProgressBarMsg ProgBarMsg = new ProgressBarMsg();
        //            //ProgBarMsg.MaxNum = TotalCount;//获取总记录行项
        //            //ProgBarMsg.ShowDialog();//显示进度条
        //            //写字段值
        //            for (int j = 0; j < ExcelTable.Rows.Count; j++)
        //            {
        //                for (int k = 0; k < ExcelTable.Columns.Count; k++)
        //                {
        //                    WkSheet.Cells[j + 2, k + 1] = ExcelTable.Rows[j][k].ToString();//写表格值
        //                }
        //                //rowRead++;
        //                //PercentRead = ((float)rowRead * 100) / TotalCount;//导入进度百分比
        //                //ProgressBarMsg.PercentMsg = rowRead;
        //                //Thread.Sleep(200);
        //                Application.DoEvents();//处理当前在消息队列中所有windows消息
        //            }

        //            WkSheet.SaveAs(SaveExcelName, missing, missing, missing, missing, missing, missing, missing, missing);
        //            Ran = WkSheet.get_Range((object)WkSheet.Cells[2, 1], (object)WkSheet.Cells[ExcelTable.Rows.Count + 1, ExcelTable.Columns.Count]);//给工作表指定区域

        //            //设置Excel表格边框样式
        //            Ran.BorderAround2(missing, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
        //            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, missing, missing);
        //            Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;//设置区域边框颜色
        //            Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//连续边框
        //            Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;//边框浓度

        //            if (ExcelTable.Columns.Count > 1)
        //            {//设置垂直表格颜色索引
        //                Ran.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
        //            }
        //            WkBk.Save();//保存Excel表数据
        //            //关闭表格对像，并退出应用程序域
        //            WkBk.Close(missing, missing, missing);
        //            XlsApp.Quit();
        //            XlsApp = null;
        //            GC.Collect();//强制关闭
        //            MessageBoxEx.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
        //    }
        //}
        /// <summary>
        /// 设置导出Excel标题样式
        /// </summary>
        /// <param name="HeaderRow">标题行</param>
        /// <param name="WkBk">工作表</param>
        /// <param name="ColNum">列序号</param>
        private static void SetXlsHeaderStyle(HSSFRow HeaderRow, HSSFWorkbook WkBk, int ColNum)
        {
            try
            {
                HSSFCellStyle HeaderStyle = (HSSFCellStyle)WkBk.CreateCellStyle();//创建Excel表格标题样式
                HSSFFont fontStyle = (HSSFFont)WkBk.CreateFont();//创建Excel表格字体样式
                HeaderStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//设置居中
                HeaderStyle.FillForegroundColor = HSSFColor.Lime.Index;//设置背景色
                HeaderStyle.FillPattern = FillPattern.SolidForeground;//设置填充样式
                HeaderStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");//设置数据格式
                fontStyle.FontHeightInPoints = 11;
                fontStyle.Boldweight = 700;
                HeaderStyle.SetFont(fontStyle);
                HeaderRow.GetCell(ColNum).CellStyle = (ICellStyle)HeaderStyle;//设置列样式

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

        }
        /// <summary>
        /// 判断替换的excel文件是否打开
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static bool IsExcelOpen(string FilePath)
        {
            try
            {
                File.Move(FilePath, FilePath);
                return true;
            }
            catch
            {
                MessageBoxEx.Show("请您先关闭要替换的同名文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}
