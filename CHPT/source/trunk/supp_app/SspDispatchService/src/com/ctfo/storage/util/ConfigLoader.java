package com.ctfo.storage.util;

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
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dao.MongoDataSource;
import com.ctfo.storage.dao.MySqlDataSource;
import com.ctfo.storage.dao.RedisDataSource;
import com.ctfo.storage.io.FileIoHandler;
import com.ctfo.storage.io.FileIoServer;
import com.ctfo.storage.io.IoHandler;
import com.ctfo.storage.io.IoServer;
import com.ctfo.storage.parse.ExceptionDataListenHandler;
import com.ctfo.storage.parse.FileResponseListen;
import com.ctfo.storage.parse.ListListen;
import com.ctfo.storage.parse.ResponseListen;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-14</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class ConfigLoader {

	/** 日志 */
	private static Logger logger = LoggerFactory.getLogger(ConfigLoader.class);

	/** 占位符 '${' */
	private static String placeholderPrefix = "${";

	/** 占位符 '}' */
	private static String placeholderSuffix = "}";

	/** 忽略无法解决的占位符 */
	private static boolean ignoreUnresolvablePlaceholders = false;

	/** mysql表model映射 */
	public static Map<String, String> protocolMap = new HashMap<String, String>();

	/** 批量提交设置 */
	public static Map<String, String> commitParamMap = new HashMap<String, String>();

	/** MySql数据库配置信息 */
	public static Map<String, String> mySqlParamMap = new HashMap<String, String>();

	/** Redis缓存服务配置信息 */
	public static Map<String, String> redisParamMap = new HashMap<String, String>();

	/** Mongo数据库配置信息 */
	public static Map<String, String> mongoParamMap = new HashMap<String, String>();

	/** 通讯配置信息 */
	public static Map<String, String> msgParamMap = new HashMap<String, String>();

	/** Sql语句映射表 */
	public static Map<String, String> sqlMap = new HashMap<String, String>();

	/** MySQL数据库客户端连接(池)管理器 */
	private static MySqlDataSource mysql = null;

	/** Redis缓存服务客户端连接(池)管理器 */
	private static RedisDataSource redis = null;

	/** Mongo缓存服务客户端连接(池)管理器 */
	private static MongoDataSource mongo = null;

	/** 定时任务 */
	public static ScheduledExecutorService service;

	/**
	 * 初始化数据
	 * 
	 * @param arg
	 * @throws Exception
	 */
	public static void init(String[] args) throws Exception {
		try {
			Element root = getConfiguration(args);

			// 消息子类型对应实体
			protocolMap = getNameMap(root.element("protocol"));

			// 批量提交数
			commitParamMap = getNameMap(root.element("commit"));

			// 初始化MySQL
			mySqlParamMap = getNameMap(root.element("mysql"));
			for (Map.Entry<String, String> sql : mySqlParamMap.entrySet()) {
				if (sql.getKey().startsWith("sql_")) {
					sqlMap.put(sql.getKey(), sql.getValue());
				}
			}
			mySqlParamMap = getNameMap(root.element("mysql"));
			mysql = new MySqlDataSource();
			mysql.setUsername(mySqlParamMap.get("username"));
			mysql.setUrl(mySqlParamMap.get("url"));
			mysql.setPassword(mySqlParamMap.get("password"));
			mysql.setInitialSize(Integer.parseInt(mySqlParamMap.get("initialSize")));
			mysql.setMaxActive(Integer.parseInt(mySqlParamMap.get("maxActive")));
			mysql.setMinIdle(Integer.parseInt(mySqlParamMap.get("minIdle")));
			mysql.init();

			// 初始化redis
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

			// 初始化Mongo数据库
			mongoParamMap = getNameMap(root.element("mongo"));
			mongo = MongoDataSource.getInstance();
			mongo.setHost(mongoParamMap.get("host"));
			mongo.setPort(Integer.parseInt(mongoParamMap.get("port")));
			mongo.init();

			// 启动任务线程
			Element tasksElement = root.element("tasks");
			List<?> tasks = tasksElement.elements("task");
			service = Executors.newScheduledThreadPool(tasks.size());
			for (Object element : tasks) {
				Element taskEm = (Element) element;
				// 任务启用标记
				if (!Boolean.parseBoolean(taskEm.attributeValue("enable"))) {
					continue;
				}
				Class<?> taskClass = Class.forName(taskEm.elementTextTrim("class"));
				TaskAdapter task = (TaskAdapter) taskClass.newInstance();
				task.setName(taskEm.attributeValue("name"));
				task.setRedis(redis);

				Element interval = taskEm.element("interval"); // 任务间隔参数
				String unit = interval.attributeValue("unit").toLowerCase();
				long initialDelay = Long.parseLong(interval.attributeValue("delay")); // 延迟执行时间
				long period = Long.parseLong(interval.getTextTrim()); // 间隔时间
				// 任务启动
				service.scheduleAtFixedRate(task, initialDelay, period, gerTimeUnit(unit));
			}

			// 启动应答监听线程
			ResponseListen responseListen = new ResponseListen();
			responseListen.start();
			FileResponseListen fileResponseListen = new FileResponseListen();
			fileResponseListen.start();

			// 数据异常处理
			ExceptionDataListenHandler handler = new ExceptionDataListenHandler();
			handler.autoCommit();

			// 启动IO通讯部分
			msgParamMap = getNameMap(root.element("receive")); // 加载系统参数配置
			IoHandler ioHandler = new IoHandler();
			IoServer ioServer = new IoServer();
			ioServer.setPort(Integer.parseInt(msgParamMap.get("port")));
			ioServer.setHandler(ioHandler);
			ioServer.start();

			// 附件同步
			FileIoHandler fileIoHandler = new FileIoHandler();
			FileIoServer fileIoServer = new FileIoServer();
			fileIoServer.setPort(Integer.parseInt(msgParamMap.get("filePort")));
			fileIoServer.setHandler(fileIoHandler);
			fileIoServer.start();

			ListListen ll = new ListListen();
			ll.start();

		} catch (Exception e) {
			logger.error("初始化数据异常：", e);
		}
	}

	/**
	 * 获取配置文件节点信息
	 * 
	 * @param args
	 * @return
	 * @throws Exception
	 */
	private static Element getConfiguration(String[] args) throws Exception {
		InputStream propsInput = null; // 输入流
		InputStream in = null;
		try {
			// 加载配置文件
			// String path = ConfigLoader.class.getResource("/").getPath().toString();
			String path = args[1].trim();
			String xml = path + System.getProperty("file.separator") + "dispatchservice.xml";
			String pro = path + System.getProperty("file.separator") + "system.properties";

			// xml
			File file = new File(xml);
			String strVal = readToString(file, "utf-8");

			// properties
			propsInput = new FileInputStream(new File(pro));
			Properties props = new Properties();
			props.load(propsInput);
			String result = parseStringValue(strVal, props);
			in = new ByteArrayInputStream(result.getBytes());
			SAXReader reader = new SAXReader();
			Reader read = new InputStreamReader(in, "UTF-8");
			Document document = reader.read(read);
			return document.getRootElement();
		} catch (Exception e) {
			logger.error("异常", e);

			throw new Exception("读取配置参数异常! getConfiguration Error！" + e.getMessage());
		} finally {
			// 关闭输入流
			if (null != propsInput) {
				propsInput.close();
			}
			if (null != in) {
				in.close();
			}
		}
	}

	/**
	 * 根据编码读取文件为字符串
	 * 
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
			logger.error(e.getMessage(), e);
		} catch (IOException e1) {
			logger.error(e1.getMessage(), e1);
		}
		try {
			return new String(filecontent, encoding);
		} catch (UnsupportedEncodingException e) {
			System.err.println("The OS does not support " + encoding);
			logger.error("The OS does not support " + encoding, e);
			return null;
		}
	}

	/**
	 * 参数替换
	 * 
	 * @param strVal
	 * @param props
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
					if (logger.isTraceEnabled()) {
						logger.trace("已经替换占位符 '" + placeholder + "'");
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
	 * 测试给定的字符串是否与给定的子字符串匹配
	 * 
	 * @param str
	 *            给定的字符串
	 * @param index
	 *            开始匹配的位置
	 * @param substring
	 *            子字符串
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
	 * 生成指定节点下的所有节点的name属性和值的映射表
	 * 
	 * @param element
	 *            指定节点
	 * @return name 属性和值的映射表
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
	 * 获取时间格式
	 * 
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
