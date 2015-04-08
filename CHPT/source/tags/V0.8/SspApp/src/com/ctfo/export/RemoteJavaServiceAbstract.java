package com.ctfo.export;

import java.io.File;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;

import jxl.Workbook;
import jxl.write.Label;
import jxl.write.WritableCellFormat;
import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.init.CovertTable;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.util.DateUtil;
import com.ctfo.util.JsonUtil;

public class RemoteJavaServiceAbstract implements RemoteJavaService {

	private static Log logger = LogFactory.getLog(RemoteJavaServiceAbstract.class);

	private String exportExcelUrl = (String) CustomizedPropertyPlaceholderConfigurer.getContextProperty("exportExcelUrl");

	private String webappsExcelUrl = (String) CustomizedPropertyPlaceholderConfigurer.getContextProperty("webappsExcelUrl");

	@Autowired
	private CovertTable covertTable;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.business.basic.service.RemoteJavaService#exportData(java.lang .String, java.lang.String)
	 */
	@Override
	public String exportData(String exportDataHeader, String result, String url, String BeanName,String funName) throws CtfoAppException {
		return creatExcel(exportDataHeader, result, url, BeanName, "",funName);
	}

	// 带sheetName参数
	public String exportData(String exportDataHeader, String result, String url, String BeanName, String sheetName,String funName) throws CtfoAppException {
		return creatExcel(exportDataHeader, result, url, BeanName, sheetName,funName);
	}

	// 生成excel
	public String creatExcel(String exportDataHeader, String result, String url, String BeanName, String sheetName,String funName) throws CtfoAppException {

		logger.debug("开始导出");
		long start = System.currentTimeMillis() / 1000;
		long end = System.currentTimeMillis() / 1000;
		SimpleDateFormat df = new SimpleDateFormat("yyyyMMddHHmmss");//设置日期格式
		String convertResult = "";
		boolean bSuccess = true;
		String fileName = funName + "-"+ df.format(new Date()) + "" + (Math.round(Math.random() * 10000)) + ".xls";
		String export_url_path = exportExcelUrl + "/";
		
		try {
			Map<String, Integer> tableHead = new HashMap<String, Integer>();
			logger.debug("开始解析Grid表头 " + (end - start));
			// 创建文件
			File f_path = new File(export_url_path);
			if (!f_path.exists()) {
				logger.debug("导出文件夹：" + export_url_path + "不存在，自动建立文件夹");
				f_path.mkdirs();
			}
			// 可以自动获取
			File f = new File(export_url_path + fileName);

			OutputStream os = new FileOutputStream(f);

			jxl.write.WritableWorkbook wwb = Workbook.createWorkbook(os);
			jxl.write.WritableSheet ws;
			if ("".equals(sheetName) || sheetName == null) {
				ws = wwb.createSheet("page0", 0);
			} else {
				ws = wwb.createSheet(sheetName, 0);
			}

			String[] columnKeyTitle = exportDataHeader.split("&");
			// 是否表头有合并,如果有会设置此值为1，数据行会下移1行
			int addMulHeader = 0;
			// 行的偏移量
			int offset = 0;
			// 用于存储多行表头的合并情况
			List<int[]> cellListForMulHeader = new ArrayList<int[]>();

			// 单元格样式
			WritableCellFormat wcf = new WritableCellFormat();
			wcf.setAlignment(jxl.format.Alignment.CENTRE);
			wcf.setVerticalAlignment(jxl.format.VerticalAlignment.CENTRE);

			for (int q = 0; q < columnKeyTitle.length; q++) {

				if (columnKeyTitle[q].split("=").length > 1) {
					String[] titleArrStr = columnKeyTitle[q].split("=");
					String keyStr = titleArrStr[0];
					String headTitle = titleArrStr[1];
					Label label = new Label(q - offset, 0, headTitle, wcf); // 把标题放到第一行
					ws.addCell(label);
					// 第一个是英文名称 第二个是前面显示的中文名称 现在要把中文名称输出并记录位置
					tableHead.put(keyStr, q - offset);
					// 如果本列没有拆分单元格，那么只做上下两格的合并。
					// 如果整个表格都没有使用合并表头，下面的代码将判断addMulHeader是否为1，如为0不进行ws.mergeCells
					int[] cellArr = { q - offset, 0, q - offset, 1 };
					cellListForMulHeader.add(cellArr);
				} else {
					tableHead.put(columnKeyTitle[q].split("=")[0], q);

					// i 是列号 0 标示行数后面是内容
					Label label = new Label(q, 0, columnKeyTitle[q].split("=")[1], wcf); // 把标题放到第一行
					ws.addCell(label);
				}

			}
			JSONObject json = JSONObject.fromObject(result);
			Object obj = json.get("Rows");
			if (obj != null) {
				JSONArray objArr = JSONArray.fromObject(obj);
				for (int i = 0; i < objArr.size(); i++) {

					JSONObject jsoObje = JSONObject.fromObject(objArr.get(i));
					Set<String> mapSet = tableHead.keySet(); // 获取所有的key值
					Iterator<String> itor = mapSet.iterator();// 获取key的Iterator便利
					while (itor.hasNext()) {// 存在下一个值
						String name = itor.next();// 当前key值
						String rawVal = jsoObje.getString(name);
						/*
						 * 如果当前值是负数，则设为0
						 */
						// 2012年7月10日修改，负值不过滤
						// if (StringUtils.startsWithIgnoreCase(rawVal, "-")) {
						// rawVal = "0";
						// }
						// 配置方案过滤数据
						Integer index = tableHead.get(name);
						if (index != null) {
							convertResult = covertTable.getKeyValue().get(BeanName + "_" + name + "_" + rawVal);
							if (convertResult == null) {
								convertResult = covertTable.getKeyValue().get(BeanName + "_" + name);
								if (convertResult == null) {
									if (rawVal == null) {
										rawVal = " ";
									}
								} else if (convertResult.equals("UTC_TIME")) {
									rawVal = DateUtil.getUTCtime(rawVal); // 转换后的格式为：yyyy-MM-dd hh:MM:ss
								} else if (convertResult.equals("UTC_DATE")) {
									rawVal = DateUtil.getUTCdate(rawVal);// 转换后的格式为：yyyy-MM-dd
								} else if (convertResult.equals("SYS_AREA_INFO")) {
									rawVal = DateUtil.getAreaInfoByCodeToName(rawVal);
									String city = DateUtil.getAreaInfoByCodeToName(jsoObje.getString("city"));
									String county = DateUtil.getAreaInfoByCodeToName(jsoObje.getString("county"));
									if(city!=null && !city.equals("")){
										rawVal += city;
									}
									if(county!=null && !county.equals("")){
										rawVal += county;
									}
								} else if (convertResult.equals("ONLINE_TIME")) {
									long nowUtcTime = DateUtil.dateToUtcTime(new Date());
									rawVal = DateUtil.getHMSColonFormateOfTimePeriodBySeconds((nowUtcTime-Long.valueOf(jsoObje.getString("loadTime")))/1000);
									DateUtil.getUTCdate(rawVal);// 在线时长
								} else if (convertResult.equals("WORKING_HOURS")) {
									String manHourQuantity = jsoObje.getString("manHourQuantity");
									String manHourUnitprice = jsoObje.getString("manHourUnitprice");
									double workingHours = Double.valueOf(manHourQuantity)*Double.valueOf(manHourUnitprice);
									workingHours = Math.round(workingHours*100)/100.0;
									rawVal = String.valueOf(workingHours); //工时费用
								} else if (convertResult.equals("ACCESSORIES_SUM")) {
									String quantity = jsoObje.getString("quantity");
									String unitPrice = jsoObje.getString("unitPrice");
									double accessoriesSum = Double.valueOf(quantity)*Double.valueOf(unitPrice);
									accessoriesSum = Math.round(accessoriesSum*100)/100.0; 
									rawVal = String.valueOf(accessoriesSum);// 配件结算金额
								}else if (convertResult.equals("REMAIN_SPACE")) {
									String cloudSize = jsoObje.getString("cloudSize");
									String usedSpace = jsoObje.getString("usedSpace");
									if(Integer.parseInt(cloudSize)>Integer.parseInt(usedSpace)){
										rawVal = String.valueOf(Integer.parseInt(cloudSize)-Integer.parseInt(usedSpace));
									}else{
										rawVal = null;
									}
								}else if (convertResult.equals("SERVER_TOTEL")) {
									rawVal = "1";
								}else {
									rawVal = convertResult;
								}
							} else {
								if(name.equals("status") && jsoObje.has("registerAuthentication")){     //注册鉴权中只有已授权的情况下才会显示状态
									if(jsoObje.getString("registerAuthentication").equals("1")){
										rawVal = convertResult;
									}else{
										rawVal = null;
									}
								}else{
									rawVal = convertResult;
								}
							}
							if (null == rawVal || "".equals(rawVal) || " ".equals(rawVal) || "null".equals(rawVal)) {
								rawVal = "--";
							}
							Label label = new Label(index, i + 1 + addMulHeader, rawVal.toString()); //
							ws.addCell(label);
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
			logger.debug("系统错误" + e);
			bSuccess = false;
		}
		String resultStr = "";

		if (bSuccess) {
			resultStr = "{\"msg\":\"" + JsonUtil.jsonCharFormat(webappsExcelUrl) + "/" + fileName + "\"}";
		} else {
			resultStr = "{\"msg\":\"error\"}";
		}
		logger.info("返回的结果：" + resultStr);
		end = System.currentTimeMillis() / 1000;
		return resultStr;
	}

	/**************************************************************/

	public String getExportExcelUrl() {
		return exportExcelUrl;
	}

	public void setExportExcelUrl(String exportExcelUrl) {
		this.exportExcelUrl = exportExcelUrl;
	}

	public String getWebappsExcelUrl() {
		return webappsExcelUrl;
	}

	public void setWebappsExcelUrl(String webappsExcelUrl) {
		this.webappsExcelUrl = webappsExcelUrl;
	}

	public CovertTable getCovertTable() {
		return covertTable;
	}

	public void setCovertTable(CovertTable covertTable) {
		this.covertTable = covertTable;
	}

	public static void main(String[] args) {
		 SimpleDateFormat df = new SimpleDateFormat("yyyyMMddHHmmss");//设置日期格式
	     System.out.println(df.format(new Date()));// new Date()为获取当前系统时间
	}
}
