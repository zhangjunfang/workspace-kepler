package com.ctfo.storage.command.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.dao.HBaseDataSource;
import com.ctfo.storage.command.dao.MySqlDataSource;
import com.ctfo.storage.command.dao.RedisDataSource;
import com.ctfo.storage.command.io.IoClient;
import com.ctfo.storage.command.io.IoHandler;
import com.ctfo.storage.command.parse.ResponseListen;

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
	/** Redis缓存服务配置信息 */
	public static Map<String, String> redisParamMap = new HashMap<String, String>();
	/** MySql数据库配置信息 */
	public static Map<String, String> mySqlParamMap = new HashMap<String, String>();
	/** hbase数据库配置信息 */
	public static Map<String, String> hbaseParamMap = new HashMap<String, String>();
	/** 通讯配置信息 */
	public static Map<String, String> msgParamMap = new HashMap<String, String>();
	/** hbase服务客户端连接(池)管理器 */
	public static HBaseDataSource hds;
	/** mysql缓存服务客户端连接(池)管理器 */
	public static MySqlDataSource mysql;
	/** Redis缓存服务客户端连接(池)管理器 */
	private static RedisDataSource redis = null;
	
	public static ScheduledExecutorService service;
	
	/**
	 * 初始化数据
	 * 
	 * @param args
	 * @throws Exception 
	 * @throws DocumentException
	 */
	public static void init(String arg0) throws Exception { 
		try {
			String[] args = arg0.split(" ");
			Element root = getConfiguration(args);
//			初始化redis
			redisParamMap = getNameMap(root.element("redis"));
			redis = RedisDataSource.getInstance();
			redis.setHost(redisParamMap.get("host"));
			redis.setPort(Integer.parseInt(redisParamMap.get("port")));
			redis.setPass(redisParamMap.get("pass"));
			redis.setMaxActive(Integer.parseInt(redisParamMap.get("maxActive"))); 
			redis.setMaxIdle(Integer.parseInt(redisParamMap.get("maxIdle")));
			redis.setMaxWait(Integer.parseInt(redisParamMap.get("maxWait")));
			redis.setTimeOut(Integer.parseInt(redisParamMap.get("timeOut"))); 
			redis.init();
			//初始化MySQL
			mySqlParamMap = getNameMap(root.element("mysql"));
			mysql = new MySqlDataSource();
			mysql.setUsername(mySqlParamMap.get("username"));
			mysql.setUrl(mySqlParamMap.get("url"));
			mysql.setPassword(mySqlParamMap.get("password"));
			mysql.setInitialSize(Integer.parseInt(mySqlParamMap.get("initialSize")));
			mysql.setMaxActive(Integer.parseInt(mySqlParamMap.get("maxActive")));
			mysql.setMinIdle(Integer.parseInt(mySqlParamMap.get("minIdle")));
			mysql.init();
			
			//初始化hbase
			hbaseParamMap = getNameMap(root.element("hbase"));
			hds = new HBaseDataSource();
			hds.setQuorum(hbaseParamMap.get("quorum"));
			hds.setPort(hbaseParamMap.get("port"));
			hds.init();
					
//			启动任务线程 
			Element tasksElement = root.element("tasks"); 
			List<?> tasks = tasksElement.elements("task");
			service = Executors.newScheduledThreadPool(tasks.size());
			
			for (Object element : tasks) {
				Element taskEm = (Element)element;
				//	任务启用标记
				if (!Boolean.parseBoolean(taskEm.attributeValue("enable"))) {
					continue;
				}
				Class<?> taskClass = Class.forName(taskEm.elementTextTrim("class"));
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setName(taskEm.attributeValue("name"));
				task.setMysql(mysql);
				task.setRedis(redis);
				for (Object obj : taskEm.element("properties").elements("property")){
					String key = ((Element)obj).attributeValue("name");
					String value = ((Element)obj).getTextTrim();
					task.setConfig(key, value); 
				}
				
				Element interval = taskEm.element("interval"); 			// 任务间隔参数
				String unit = interval.attributeValue("unit").toLowerCase();
				long initialDelay = Long.parseLong(interval.attributeValue("delay"));// 延迟执行时间
				long period = Long.parseLong(interval.getTextTrim());	//间隔时间
//				任务启动
				//task.init();
				service.scheduleAtFixedRate(task, initialDelay, period, gerTimeUnit(unit));
			}
			
			
//			启动IO通讯部分
			ResponseListen r = new ResponseListen();
			r.start();
			
			msgParamMap = getNameMap(root.element("receive"));// 加载系统参数配置
			int connections = Integer.parseInt(msgParamMap.get("connections"));
			if(connections > 0){
				for(int i = 0; i < connections; i++){
					IoHandler ioHandler = new IoHandler();
					ioHandler.setUserName(msgParamMap.get("user"));
					ioHandler.setPassword(msgParamMap.get("pass"));
					ioHandler.setSource(Integer.parseInt(msgParamMap.get("source")));
					ioHandler.setDestination(Integer.parseInt(msgParamMap.get("destination")));
					IoClient ioClient = new IoClient();
					ioClient.setHost(msgParamMap.get("host"));
					ioClient.setPort(Integer.parseInt(msgParamMap.get("port")));
					ioClient.setHandler(ioHandler);
					ioClient.connect();
				}
			}else {
				throw new Exception("通讯连接数错误! msg Error！" + connections); 
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
	private static Element getConfiguration(String[] args) throws Exception {
		InputStream propsInput = null;
		try {
			String path = ConfigLoader.class.getResource("/").getPath().toString();
			log.info("path:" + path);
			// 加载配置文件
			String xml = path + System.getProperty("file.separator") + "command.xml";
			String pro = path + System.getProperty("file.separator") + "system.properties";
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
	private static TimeUnit gerTimeUnit(String unit) {
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
