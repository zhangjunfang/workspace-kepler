package com.ctfo.trackservice.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.TimeUnit;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleProperties;
import com.ctfo.trackservice.dao.RedisProperties;
import com.ctfo.trackservice.task.TaskConfiger;



public class ConfigLoader {
	private static Logger log = LoggerFactory.getLogger(ConfigLoader.class);
	/**	占位符	'${' */
	private static String placeholderPrefix = "${";
	/**	占位符	'}' */
	private static String placeholderSuffix = "}";
	/**	忽略无法解决的占位符	*/
	private static boolean ignoreUnresolvablePlaceholders = false;
	/** 参数配置信息 */
	public static Map<String, String> config = new HashMap<String, String>();
	/** 定时任务配置列表 */
	public static List<TaskConfiger> tasks = new ArrayList<TaskConfiger>();
	
	public static RedisProperties redisProperties;
	
	public static OracleProperties oracleProperties;
	/**
	 * 初始化数据
	 * 
	 * @param args
	 * @throws Exception 
	 * @throws DocumentException
	 */
	public static void init(String[] configuration) throws Exception { 
		try {
//			 读取配置文件
			Element root = getConfiguration(configuration);
			
//			应用参数
			config = getNameMap(root.element("config"));
			
//			任务
			Element tasksElement = root.element("tasks"); 
			List<?> taskList = tasksElement.elements("task");
			for (Object element : taskList) {
				Element taskEm = (Element)element;
				//任务属性
				if (!Boolean.parseBoolean(taskEm.attributeValue("enable"))) continue;
				TaskConfiger task = new TaskConfiger();
				task.setThreadNum(Integer.parseInt(taskEm.attributeValue("threadNum")));
				task.setName(taskEm.attributeValue("name"));
				task.setDesc(taskEm.attributeValue("desc"));
				task.setImpClass(taskEm.elementTextTrim("class"));
				//任务执行时间
				/*Element period = taskEm.element("interval");
				task.setUnit(period.attributeValue("unit").toLowerCase());
				task.setDelay(period.attributeValue("delay"));
				task.setPeriod(period.getTextTrim());*/
				task.setDelay(Tools.getDelaySeconds(config.get("startTime"), "yyyy-MM-dd HH:mm:ss")+"");
				//用户自定义属性
				for (Object obj : taskEm.element("properties").elements("property")){
					String key = ((Element)obj).attributeValue("name");
					String value = ((Element)obj).getTextTrim();
					task.setConfig(key, value);
				}
				task.setConfig("oilFilePath", config.get("oilFilePath"));
				task.setConfig("trackFilePath", config.get("trackFilePath"));
				task.setConfig("eventFilePath", config.get("eventFilePath"));
				tasks.add(task); //存入任务列表
			}
		} catch (Exception e1) {
			log.error("初始化数据异常:" + e1.getMessage(), e1);
			throw new Exception("初始化数据异常! Init Error！");
		}
		
	}
	/**
	 * 获取配置文件节点信息
	 * @param args
	 * @return
	 * @throws Exception
	 */
	private static Element getConfiguration(String[] configPath) throws Exception {
		InputStream propsInput = null;
		String xml = null;
		String pro = null;
		String path = null;
		try {
			if(configPath != null && configPath.length >= 3){
				xml = configPath[1] + "/config.xml";
				pro = configPath[1] +"/system.properties";
			} else {
				xml = path + "config.xml";
				pro = path +"system.properties";
			}
			propsInput = new FileInputStream(new File(pro));
			File file = new File(xml);
			String strVal = readToString(file, "utf-8");
			Properties props = new Properties();
			props.load(propsInput);
			String result = parseStringValue(strVal, props);
			Document document = DocumentHelper.parseText(result);
			return document.getRootElement();
		} catch (Exception e) {
			log.error(e.getMessage(), e);
			throw new Exception("读取配置参数异常! getConfiguration Error！" + e.getMessage());
		} finally {
			if (propsInput != null) {
				propsInput.close();
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
	 * 获取时间格式
	 * @param unit
	 * @return
	 */
	public static TimeUnit gerTimeUnit(String unit) {
		if ("day".equals(unit)) {
			return TimeUnit.DAYS;
		} else if ("hour".equals(unit)) {
			return TimeUnit.HOURS;
		} else if ("minute".equals(unit)) {
			return TimeUnit.MINUTES;
		} else if ("second".equals(unit)) {
			return TimeUnit.SECONDS;
		} else {
			return TimeUnit.MINUTES;
		}
	}
}
