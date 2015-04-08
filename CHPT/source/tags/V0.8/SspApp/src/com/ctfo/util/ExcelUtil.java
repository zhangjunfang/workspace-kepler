package com.ctfo.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;

import jxl.Workbook;
import jxl.format.Alignment;
import jxl.format.VerticalAlignment;
import jxl.write.Label;
import jxl.write.WritableCellFormat;
import jxl.write.WritableSheet;
import jxl.write.WritableWorkbook;
import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.poi2.hssf.usermodel.HSSFCell;
import org.apache.poi2.hssf.usermodel.HSSFRow;
import org.apache.poi2.hssf.usermodel.HSSFSheet;
import org.apache.poi2.hssf.usermodel.HSSFWorkbook;
import org.apache.poi2.hssf.util.Region;

import com.ctfo.context.FrameworkContext;
import com.ctfo.local.exception.CtfoAppException;



public class ExcelUtil {
	private static Log logger = LogFactory.getLog(ExcelUtil.class);
	
	public static void main(String[] arg) throws Exception{
		HSSFWorkbook wb = null;
		wb = new HSSFWorkbook(new FileInputStream("C:/2.xls"));
		HSSFSheet sheet = wb.getSheetAt(0);
		mergedRows(sheet,0);
		mergedRows(sheet,1);
		wb.write(new FileOutputStream("C:/1.xls"));
	}

	/** 判断指定位置是否有合并单元格 */
	public static Region getMergedRegion(HSSFSheet sheet, int row, int column) {
		int sheetMergeCount = sheet.getNumMergedRegions();
		for (int i = 0; i < sheetMergeCount; i++) {
			Region ca = sheet.getMergedRegionAt(i);
			int firstColumn = ca.getColumnFrom();
			int lastColumn = ca.getColumnTo();
			int firstRow = ca.getRowFrom();
			int lastRow = ca.getRowTo();
			if (row >= firstRow && row <= lastRow) {
				if (column >= firstColumn && column <= lastColumn) {
					return ca;
				}
			}
		}
		return null;
	}

	/**
	 * 在Excel文件中的指定列进行遍历，如找到相同内容的单元格，则合并之
	 * @param sheet
	 * @param col
	 */
	public static void mergedRows(HSSFSheet sheet,int col){
		mergedRows(sheet, col, 0, sheet.getLastRowNum(),false,0);
	}
	/**
	 * 在Excel文件中的指定列进行遍历，如找到相同内容的单元格，则合并之
	 * @param sheet
	 * @param col
	 * @param beginRow
	 * @param endRow
	 */
	public static void mergedRows(HSSFSheet sheet,int col,int beginRow,int endRow,boolean isAfterIndex,int relyPreCell){
		if(sheet==null){
			return;
		}
		short scol = (short) col;
		String text = null;
		int begin = 0;
		beginRow = Math.max(beginRow, sheet.getFirstRowNum());
		endRow = Math.min(endRow, sheet.getLastRowNum());
		for(int i=beginRow;i<=endRow;i++){
			HSSFRow row = sheet.getRow(i);
			HSSFCell cell = row==null?null:row.getCell(scol);
			String tmp = cell==null?null:(cell.getCellType()==HSSFCell.CELL_TYPE_STRING?cell.getStringCellValue():""+cell.getNumericCellValue());
			String cellText = getText(sheet,i,scol,relyPreCell);
			boolean merged = getMergedRegion(sheet,i,scol)!=null;
			if(!merged&&text==null&&tmp!=null){
				text = cellText;
				begin = i;
			}
			if(text!=null&&(i==endRow||merged||(!text.equals(cellText)))){
				if(i>begin+1){
					sheet.addMergedRegion(new Region(begin,scol,i-1,scol));
				}
				text = getText(sheet,i,scol,relyPreCell);
				begin = i;
			}
		}
	}
	private static String getText(HSSFSheet sheet,int row,int col,int relyPreCell){
		HSSFRow _row = sheet.getRow(row);
		HSSFCell cell = _row==null?null:_row.getCell((short) col);
		String text = cell==null?"":(cell.getCellType()==HSSFCell.CELL_TYPE_STRING?cell.getStringCellValue():""+cell.getNumericCellValue());
		relyPreCell = Math.min(col, relyPreCell);
		int i=0;
		while(i++<col&&relyPreCell-->0){
			cell = _row==null?null:_row.getCell((short) (col-i));
			String tmp = cell==null?"":(cell.getCellType()==HSSFCell.CELL_TYPE_STRING?cell.getStringCellValue():""+cell.getNumericCellValue());
			text = tmp.replace('_','-')+"_"+text;
		}
		return text;
	}
	
	
	  public static String exportData(String exportDataHeader, String result, String url, String BeanName)
			    throws CtfoAppException
			  {
		          ExcelUtil excelUtil=new ExcelUtil();
			    return excelUtil.creatExcel(exportDataHeader, result, url, BeanName, "");
			  }

	  public static String exportData(String exportDataHeader, String result, String url, String BeanName, String sheetName) throws CtfoAppException
	  {
		  ExcelUtil excelUtil=new ExcelUtil();
	    return excelUtil.creatExcel(exportDataHeader, result, url, BeanName, sheetName);
	  }
	
	public String creatExcel(String exportDataHeader, String result, String url, String BeanName, String sheetName)
		    throws CtfoAppException
		  {
		    long start = System.currentTimeMillis() / 1000L;
		    long end = System.currentTimeMillis() / 1000L;
		    boolean bSuccess = true;
		    String fileName = System.currentTimeMillis() + Math.round(Math.random() * 1000.0D) + ".xls";
		    String expUrlPath =FrameworkContext.getAppPath()+"/temp/"; //this.exportExcelUrl + "/";
		    String webappsExcelUrl=url+"/temp/";
		    try
		    {
		      WritableSheet ws;
		      Label label;
		      Map<String, Integer> tableHead = new HashMap<String, Integer>();
		      logger.debug("开始解析Grid表头 " + (end - start));

		      File f_path = new File(expUrlPath);
		      if (!(f_path.exists())) {
		        logger.debug("导出文件夹：" + expUrlPath + "不存在，自动建立文件夹");
		        f_path.mkdirs();
		      }

		      File f = new File(expUrlPath + fileName);

		      OutputStream os = new FileOutputStream(f);

		      WritableWorkbook wwb = Workbook.createWorkbook(os);

		      if (("".equals(sheetName)) || (sheetName == null))
		        ws = wwb.createSheet("page0", 0);
		      else {
		        ws = wwb.createSheet(sheetName, 0);
		      }

		      String[] columnKeyTitle = exportDataHeader.split("&");

		      int addMulHeader = 0;

		      int offset = 0;

		      List<int[]> cellListForMulHeader = new ArrayList<int[]>();

		      WritableCellFormat wcf = new WritableCellFormat();
		      wcf.setAlignment(Alignment.CENTRE);
		      wcf.setVerticalAlignment(VerticalAlignment.CENTRE);
              //构建表头
		      for (int q = 0; q < columnKeyTitle.length; ++q)
		      {
		        if (columnKeyTitle[q].split("=").length > 1)
		        {
		          String[] titleArrStr = columnKeyTitle[q].split("=");

		          String keyStr = titleArrStr[0];
		          String headTitle = titleArrStr[1];

		          if (keyStr.split("_").length > 1) {
		            int[] cellArr;
		            String[] keyArray = keyStr.split("_");

		            if ("1".equals(keyArray[1])) {
		              label = new Label(q - offset, 0, headTitle, wcf);
		              ws.addCell(label);
		              cellArr =new int[] { q - offset, 0, q - offset - 1 };
		              cellListForMulHeader.add(cellArr);
		              ++offset;
		            } else if ("2".equals(keyArray[1])) {
		              label = new Label(q - offset, 1, headTitle, wcf);
		              ws.addCell(label);
		              tableHead.put(keyArray[0], Integer.valueOf(q - offset));

		              cellArr = (int[])cellListForMulHeader.get(cellListForMulHeader.size() - 1);
		              cellArr[2] += 1;
		            }

		            addMulHeader = 1;
		          } else {
		            label = new Label(q - offset, 0, headTitle, wcf);
		            ws.addCell(label);

		            tableHead.put(keyStr, Integer.valueOf(q - offset));

		            int[] cellArr = { q - offset, 0, q - offset, 1 };
		            cellListForMulHeader.add(cellArr);
		          }
		        } else {
		          tableHead.put(columnKeyTitle[q].split("=")[0], Integer.valueOf(q));

		          label = new Label(q, 0, columnKeyTitle[q].split("=")[1], wcf);
		          ws.addCell(label);
		        }

		      }

		      if (addMulHeader == 1)
		        for (Iterator<int[]> it = cellListForMulHeader.iterator(); it.hasNext(); ) 
		        { int[] cellArr = (int[])
		            it.next();
		          ws.mergeCells(cellArr[0], cellArr[1], cellArr[2], cellArr[3]);
		        }

              //填充数据
		      JSONObject json = JSONObject.fromObject(result);
		      Object obj = json.get("Rows");
		      if (obj != null) {
		        JSONArray objArr = JSONArray.fromObject(obj);
		        for (int i = 0; i < objArr.size(); ++i)
		        {
		          JSONObject jsoObje = JSONObject.fromObject(objArr.get(i));
		          Set<String> mapSet = tableHead.keySet();
		          Iterator<String> itor = mapSet.iterator();
		          while (itor.hasNext()) {
		            String name = (String)itor.next();
		            String rawVal = jsoObje.getString(name);

		            if (rawVal.equals("999999999"))
		              rawVal = "--";

		            Integer index = (Integer)tableHead.get(name);

		            if (index != null) {
		               /* convertResult = (String)this.covertTable.getKeyValue().get(BeanName + "_" + name + "_" + rawVal);
		                rawVal = convertResult;
		              if ((rawVal == null) || ("".equals(rawVal)) || (" ".equals(rawVal)) || ("null".equals(rawVal)))
		                rawVal = "--";

		              String[] timeVal = (String[])null;
		              String coverTimeVal = "";
		              if ((name.equals("overspeedTime")) || (name.equals("fatigueTime")) || (name.equals("gearGlideTime")) || 
		                (name.equals("a001Time")) || (name.equals("a002Timestr")) || (name.equals("a003Time")) || 
		                (name.equals("a004Time")) || (name.equals("a005Time"))) {
		                timeVal = ReportLoadData.changeSecondToTime(Long.valueOf(Long.parseLong(rawVal))).split("：");
		                coverTimeVal = coverTimeVal + ((timeVal[0].equals("0")) ? "" : new StringBuilder(String.valueOf(timeVal[0])).append("时").toString());
		                coverTimeVal = coverTimeVal + ((timeVal[1].equals("0")) ? "" : new StringBuilder(String.valueOf(timeVal[1])).append("分").toString());
		                coverTimeVal = coverTimeVal + ((timeVal[2].equals("0")) ? "" : new StringBuilder(String.valueOf(timeVal[2])).append("秒").toString());
		                rawVal = coverTimeVal;
		                coverTimeVal = "";
		              }*/
		              Label labelLast = new Label(index.intValue(), i + 1 + addMulHeader, rawVal.toString());
		              ws.addCell(labelLast);
		            }
		          }
		        }

		      }

		      wwb.write();
		      wwb.close();
		      os.flush();
		      os.close();
		    } catch (Exception e) {
		      e.printStackTrace();
		      bSuccess = false;
		    }
		    String resultStr = "";

		    if (bSuccess)
		    	resultStr = "{\"msg\":\"" + JsonUtil.jsonCharFormat(webappsExcelUrl) + "/" + fileName + "\"}";
		    else
		      resultStr = "{\"msg\":\"error\"}";

		    end = System.currentTimeMillis() / 1000L;
		    return resultStr;
		  }

}
