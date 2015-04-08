package com.ctfo.advice.util;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.dao.MQDataSource;
import com.ctfo.advice.dao.OracleDataSource;
import com.ctfo.advice.thread.MQReviceThread;


/**
 * 文件名：ConfigLoader.java
 * 功能：
 *
 * @author hjc
 * 2014-8-12下午3:59:02
 * 
 */
public class ConfigLoader {
	private static Logger log = LoggerFactory.getLogger(ConfigLoader.class);
	/**	占位符	'${' */
	private static String placeholderPrefix = "${";
	/**	占位符	'}' */
	private static String placeholderSuffix = "}";
	/**	忽略无法解决的占位符	*/
	private static boolean ignoreUnresolvablePlaceholders = false;
	/** 系统配置信息 */
	public static Map<String, String> systemParamMap = new HashMap<String, String>();
	/** oracle数据库配置信息 */
	public static Map<String, String> oracleParamMap = new HashMap<String, String>();
	/** MQ配置信息 */
	public static Map<String, String> mqParamMap = new HashMap<String, String>();
	/** Sql语句映射表 */
	public static Map<String, String> sqlMap = new HashMap<String, String>();
	/** oracle数据库客户端连接(池)管理器 */
	private static OracleDataSource oracle = null; 
	/** MQ连接管理器 */
	private static MQDataSource mq = null; 

	
	/**
	 * 初始化数据
	 * 
	 * @param args
	 * @throws Exception 
	 * @throws DocumentException
	 */
	public static void init(String arg) throws Exception { 
		try {
			String[] args = arg.split(" ");
			Element root = getConfiguration(args);

//			初始化Oracle
			oracleParamMap = getNameMap(root.element("oracle"));
			for(Map.Entry<String, String> sql : oracleParamMap.entrySet()){
				if(sql.getKey().startsWith("sql_")){
					sqlMap.put(sql.getKey(), sql.getValue());
				}
			}
			oracle = new OracleDataSource();
			oracle.setUsername(oracleParamMap.get("username"));
			oracle.setUrl(oracleParamMap.get("url"));
			oracle.setPassword(oracleParamMap.get("password"));
			oracle.setInitialSize(Integer.parseInt(oracleParamMap.get("initialSize")));
			oracle.setMaxActive(Integer.parseInt(oracleParamMap.get("maxActive")));
			oracle.setMinIdle(Integer.parseInt(oracleParamMap.get("minIdle")));
			oracle.init();
			
//			初始化MQ
			mqParamMap = getNameMap(root.element("mq"));
			mq = new MQDataSource();
			mq.setUrl(mqParamMap.get("url"));
			mq.setMqName(mqParamMap.get("mqtopicsName"));
			mq.init();
//			启动任务线程
			MQReviceThread parse = new MQReviceThread();
			parse.start();
			
		}catch (Exception e) {
			log.error("数据库连接初始化错误！"+e.getMessage());
		}
			
	
	}
	/**
	 * 获取配置文件节点信息
	 * @param args
	 * @return
	 * @throws Exception
	 */
	private static Element getConfiguration(String[] args) throws Exception {
		InputStream propsInput = null;
		InputStream in = null;
		try {
			// 加载配置文件
			String path = ConfigLoader.class.getResource("/").getPath().toString();
			log.info("path:" + path);
			// 加载配置文件
			String xml = path + System.getProperty("file.separator") + "adviceService.xml";
			String pro = path + System.getProperty("file.separator") + "system.properties";
			propsInput = new FileInputStream(new File(pro));
			File file = new File(xml);
			String strVal = readToString(file, "utf-8");
			Properties props = new Properties();
			props.load(propsInput);
			String result = parseStringValue(strVal, props);
			in = new ByteArrayInputStream(result.getBytes());
			SAXReader reader = new SAXReader();
			Reader read = new InputStreamReader(in,"UTF-8");
			Document document = reader.read(read);
			return document.getRootElement();
		} catch (Exception e) {
			log.error(e.getMessage(), e);
			System.out.println(e.getMessage()); 
			throw new Exception("读取配置参数异常! getConfiguration Error！" + e.getMessage());
		} finally {
			if (propsInput != null) {
				propsInput.close();
			}
			if (in != null) {
				in.close();
			}
		}
	}

	/**
	 * 生成指定节点下的所有节点的name属性和值的映射表
	 * @param element  	指定节点
	 * @return name		属性和值的映射表
	 */
	public static Map<String, String> getNameMap(Element element) {
		Map<String, String> map = new HashMap<String, String>();
		List<?> elements = element.elements("property");
		for (Object em : elements) {
			String key = ((Element) em).attributeValue("name");
			String value = ((Element) em).getTextTrim();
			map.put(key, value);
		}
		return map;
	}



	/**
	 * 参数替换
	 * 
	 * @param strVal
	 * @param props
	 * @param visitedPlaceholders
	 * @return
	 * @throws Exception
	 */
	public static String parseStringValue(String strVal, Properties props) throws Exception {
		StringBuffer buf = new StringBuffer(strVal);
		int startIndex = strVal.indexOf(placeholderPrefix);
		while (startIndex != -1) {
			int endIndex = findPlaceholderEndIndex(buf, startIndex);
			if (endIndex != -1) {
				String placeholder = buf.substring(startIndex + placeholderPrefix.length(), endIndex);
				placeholder = parseStringValue(placeholder, props);
				String propVal = resolvePlaceholder(placeholder, props);
				if (propVal != null) {
					propVal = parseStringValue(propVal, props);
					buf.replace(startIndex, endIndex + placeholderSuffix.length(), propVal);
					if (log.isTraceEnabled()) {
						log.trace("已经替换占位符 '" + placeholder + "'");
					}
					startIndex = buf.indexOf(placeholderPrefix, startIndex + propVal.length());
				} else if (ignoreUnresolvablePlaceholders) {
					startIndex = buf.indexOf(placeholderPrefix, endIndex + placeholderSuffix.length());
				} else {
					throw new Exception("无法替换占位符 '" + placeholder + "'");
				}
			} else {
				startIndex = -1;
			}
		}
		return buf.toString();
	}

	/**
	 * 查找结束符位置
	 * 
	 * @param buf
	 * @param startIndex
	 * @return
	 */
	private static int findPlaceholderEndIndex(CharSequence buf, int startIndex) {
		int index = startIndex + placeholderPrefix.length();
		int withinNestedPlaceholder = 0;
		while (index < buf.length()) {
			if (substringMatch(buf, index, placeholderSuffix)) {
				if (withinNestedPlaceholder > 0) {
					withinNestedPlaceholder--;
					index = index + placeholderSuffix.length();
				} else {
					return index;
				}
			} else if (substringMatch(buf, index, placeholderPrefix)) {
				withinNestedPlaceholder++;
				index = index + placeholderPrefix.length();
			} else {
				index++;
			}
		}
		return -1;
	}

	/**
	 * 从配置文件中获取参数
	 * 
	 * @param placeholder
	 * @param props
	 * @return
	 */
	protected static String resolvePlaceholder(String placeholder, Properties props) {
		return props.getProperty(placeholder);
	}

	/**
	 * 测试给定的字符串是否与给定的子字符串匹配
	 * 
	 * @param str 给定的字符串
	 * @param index 开始匹配的位置
	 * @param substring 子字符串
	 * @return
	 */
	public static boolean substringMatch(CharSequence str, int index, CharSequence substring) {
		for (int j = 0; j < substring.length(); j++) {
			int i = index + j;
			if (i >= str.length() || str.charAt(i) != substring.charAt(j)) {
				return false;
			}
		}
		return true;
	}
	/**
	 * 根据编码读取文件为字符串
	 * @param file
	 * @param encoding
	 * @return
	 */
	public static String readToString(File file, String encoding) {
		Long filelength = file.length();
		byte[] filecontent = new byte[filelength.intValue()];
		try {
			FileInputStream in = new FileInputStream(file);
			in.read(filecontent);
			in.close();
		} catch (FileNotFoundException e) {
			log.error(e.getMessage(), e); 
		} catch (IOException e1) {
			log.error(e1.getMessage(), e1); 
		}
		try {
			return new String(filecontent, encoding);
		} catch (UnsupportedEncodingException e) {
			System.err.println("The OS does not support " + encoding);
			log.error("The OS does not support " + encoding, e); 
			return null;
		}
	}
	
	/**
	 * 获取配置文件中的sql的Map
	 * @return
	 */
	public static Map<String, String> getSqlMap() {
		return sqlMap;
	}
}
