package com.caits.analysisserver.utils;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.Iterator;
import java.util.List;
import java.util.TreeMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;




import com.caits.analysisserver.bean.DataBean;
import com.ctfo.gis.query.StreetQuery;

public class Utils {
	private static final Logger logger = LoggerFactory.getLogger(Utils.class);

	public static boolean isContainAlarm(String str, String alarmCode) {
		if (str.contains(alarmCode)) {
			return true;
		}
		return false;
	}

	/****
	 *  根据经纬度获取具体地理位置
	 * @param lon
	 * @param lat
	 * @return
	 */
	public static String getAddress(Double lon, Double lat) {
		String addressXml = null;
		StringBuffer pt = null;
		String street = null;
		String district = null;
		if (lon != null && lat != null) {
			pt = new StringBuffer("");
			pt.append(lon);
			pt.append(",");
			pt.append(lat);
			try {

				addressXml = StreetQuery.getSrteet(pt.toString());
				// 省市县
				district = getRootElementAttribute(parseWithSAX(addressXml), "Dis", "name");

				// 具体地址 街道
				street = getRootElementAttribute(parseWithSAX(addressXml),"Add", "v");

			} catch (Exception e) {
				logger.error("根据坐标点获取行政区和街道等具体地址失败！" +e.getMessage(), e);
			}
		}
		if(district != null && street != null){
			district = district.replace(">", "");
			return district + street;
		}
		return "";
	}

	/**
	 * 根据xml字符串解析xml对象
	 * 
	 * @param xmlStr
	 * @throws MalformedURLException
	 * @throws DocumentException
	 */
	public static Document parseWithSAX(String xmlStr)
			throws MalformedURLException, DocumentException {

		return DocumentHelper.parseText(xmlStr);
	}

	/**
	 * 得到根节点属性值
	 * 
	 * @param doc
	 *            xml对象
	 * @param elementName
	 *            所取节点名称
	 * @param attribute
	 *            属性名
	 * @return 属性值
	 */
	@SuppressWarnings("rawtypes")
	public static String getRootElementAttribute(Document doc,
			String elementName, String attribute) {

		String attributeText = null;

		Iterator iter = doc.getRootElement().elementIterator();

		while (iter.hasNext()) {

			Element sub = (Element) iter.next();

			if (sub.getName().equals(elementName)) {
				attributeText = sub.attribute(attribute).getText();
				break;
			}
		}

		return attributeText;
	}
	

	
	/****
	 * 过滤重复记录
	 * @param list
	 * @param map
	 */
	public static void clearDuplicateRecord(List<DataBean> list,TreeMap<Long, String> map){
		for(DataBean dataBean : list){
			map.put(dataBean.getGpsTime(), dataBean.getData());
		}// End for
	}

	
	
	/*****
	 * 判断字符串是否是为null或者空，是则返回true，否则为false
	 * @param ck
	 * @return
	 */
	public static boolean checkEmpty(String str){
		if(str == null || "".equals(str) || "null".equals(str)){
			return true;
		}
		return false;
	}
	
	/***
	 *  根据速度来源，获取是GPS速度或VSS速度
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static int getSpeed(String spdFrom,String[] cols){
		int spd = 0;
		if(spdFrom.equals("0")){// 0：来自VSS
			if(cols[19] != null && !cols[19].equals("")){
				spd = Integer.parseInt((null == cols[19] || "".equals(cols[19]))?"0":cols[19]);
			}
		}else{
			if(cols[3] != null && !cols[3].equals("")){
				spd = Integer.parseInt((null == cols[3] || "".equals(cols[3]))?"0":cols[3]); // 1：来自GPS
			}
		}
		return spd;
	}
	
	/**
	 * 用字符"0"前向填充指定字符串为指定长度
	 * @param str  需要填充的字符串
	 * @param maxLength 填充后长度
	 * @return
	 */
	public static String fillString(String str,int maxLength){
		int len = maxLength - str.length();
		if (len>0){
			String fillstr = "";
			for (int i=0;i<len;i++){
				fillstr += "0";
			}
			str =fillstr+str;
		}
		return str;
	}
	
	@SuppressWarnings("unused")
	public static String Post(String path) throws Exception {
		/**
		 * 首先要和URL下的URLConnection对话。 URLConnection可以很容易的从URL得到。比如： // Using
		 * java.net.URL and //java.net.URLConnection
		 */
		// http://218.241.155.27/SE_RGC?st=Rgc&pt=116.42028,39.91845
		URL url = new URL(path);
		URLConnection connection = url.openConnection();
		HttpURLConnection httpUrlConnection = (HttpURLConnection) connection;
		// 设置是否向httpUrlConnection输出，因为这个是post请求，参数要放在 02. 2. //
		// http正文内，因此需要设为true, 默认情况下是false;
		httpUrlConnection.setDoOutput(true);
		// 设置是否从httpUrlConnection读入，默认情况下是true;
		httpUrlConnection.setDoInput(true);
		// Post 请求不能使用缓存
		httpUrlConnection.setUseCaches(false);
		httpUrlConnection.setRequestProperty("Content-type", "text/html");
		httpUrlConnection.setRequestProperty("Accept-Charset", "ISO8859-1");
		httpUrlConnection.setRequestProperty("contentType", "ISO8859-1");

		// 设定传送的内容类型是可序列化的java对象
		// (如果不设此项,在传送序列化对象时,当WEB服务默认的不是这种类型时可能抛java.io.EOFException)
		// httpUrlConnection.setRequestProperty("Content-type",
		// "application/x-java-serialized-object");
		// 设定请求的方法为"POST"，默认是GET 16.16.
		//httpUrlConnection.setRequestMethod("POST");
		// 连接，从上述第2条中url.openConnection()至此的配置必须要在connect之前完成，
		httpUrlConnection.connect();
		OutputStream outStrm = httpUrlConnection.getOutputStream();

		// 一旦发送成功，用以下方法就可以得到服务器的回应：
		String sCurrentLine;
		String sTotalString = "";
		sCurrentLine = "";
		
		InputStream l_urlStream;
		l_urlStream = httpUrlConnection.getInputStream();
		// 传说中的三层包装阿！
		BufferedReader l_reader = new BufferedReader(new InputStreamReader(
				l_urlStream));
		while ((sCurrentLine = l_reader.readLine()) != null) {
			sTotalString += sCurrentLine + "";
		}
		return sTotalString.trim();
	}
	
	/**
	 * 判断二进制某位是否是1或0
	 * @param args
	 */
	public static boolean check(String num, String result) {

		boolean bool = false;
		if (result.matches(".*0\\d{"+ num +"}")) { 
			bool = false;
		}
		if (result.matches(".*1\\d{"+ num +"}")) { 
			bool = true;
		}

		return bool;

	}
}
