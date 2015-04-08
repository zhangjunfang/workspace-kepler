package com.ctfo.analy.util;

import java.net.MalformedURLException;
import java.util.Iterator;

import org.apache.log4j.Logger;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;

import com.ctfo.gis.query.StreetQuery;

public class Utils {
	private static final Logger logger = Logger.getLogger(Utils.class);
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
				logger.info("根据坐标点获取行政区和街道等具体地址失败！");
				logger.info(e.fillInStackTrace());
			}
		}
		if(district != null && street != null){
			district = district.replace(">", "");
			return district + street;
		}
		return "未知位置";
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
	
}
