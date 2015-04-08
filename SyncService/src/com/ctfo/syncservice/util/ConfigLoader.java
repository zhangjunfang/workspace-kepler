package com.ctfo.syncservice.util;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;

import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ConfigLoader {
	private static Logger log = LoggerFactory.getLogger(ConfigLoader.class); 
	public static Map<String, String> CONFIG_SYS_PARAM = new HashMap<String, String>();
	/** Redis缓存服务配置信息 */
	public static Map<String, String> CONFIG_REDISCACHE = new HashMap<String, String>();
	
	/** Oracle数据库配置信息 */
	public static Map<String, String> CONFIG_ORACLEDB = new HashMap<String, String>();
	
	/** 定时任务配置列表 */
	public static List<TaskConfiger> TASK_LIST = new ArrayList<TaskConfiger>();
	
	/**
	 * 将XML格式的键值映射转换为Properties对象
	 * @param elements
	 * @return
	 */
	public static Properties getProperties(List<?> elements) {
		Properties ppts = new Properties();
		for (Object em : elements) {
			String name = ((Element)em).attributeValue("name");
			String value = ((Element)em).getTextTrim();
			ppts.put(name, value);
		}
		return ppts;
	}
	
	/**
	 * 生成指定节点下的所有节点的name属性和值的映射表
	 * @param element 指定节点
	 * @return name属性和值的映射表
	 */
	public static Map<String, String> getNameMap(Element element) {
		Map<String, String> map = new HashMap<String, String>();
		List<?> elements = element.elements("property");
		for (Object em : elements) {
			String key = ((Element)em).attributeValue("name");
			String value = ((Element)em).getTextTrim();
			map.put(key, value);
		}
		return map;
	}
	
	public static void main(String[] args) {
		try {
			ConfigLoader.load(args[0]);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public static String configXml = ""; //配置文件路径
	
	public static void load(String configXmlPath) throws Exception {
		try {
			SAXReader reader = new SAXReader();
			Document doc = reader.read(new File(configXmlPath));
			Element root = doc.getRootElement(); 
			
			log.info("开始加载配置文件....");
			CONFIG_SYS_PARAM = getNameMap(root.element("systemparam"));//加载系统参数配置
			CONFIG_ORACLEDB = getNameMap(root.element("oracledb"));//加载Oracle数据库客户端配置
			CONFIG_REDISCACHE = getNameMap(root.element("rediscache"));//加载Redis缓存服务客户端配置
			
			//任务配置///////////////////////////////////////
			Element tasksElement = root.element("tasks"); 
			List<?> tasks = tasksElement.elements("task");
			for (Object element : tasks) {
				Element taskEm = (Element)element;
				//任务属性
				if (!Boolean.parseBoolean(taskEm.attributeValue("enable"))) continue;
				TaskConfiger task = new TaskConfiger();
				task.setName(taskEm.attributeValue("name"));
				task.setDesc(taskEm.attributeValue("desc"));
				task.setImpClass(taskEm.elementTextTrim("class"));
				//任务执行时间
				Element period = taskEm.element("period");
				task.setUnit(period.attributeValue("unit").toLowerCase());
				task.setDelay(period.attributeValue("delay"));
				task.setPeriod(period.getTextTrim());
				//用户自定义属性
				for (Object obj : taskEm.element("properties").elements("property")){
					String key = ((Element)obj).attributeValue("name");
					String value = ((Element)obj).getTextTrim();
					task.putProperty(key, value);
				}
				TASK_LIST.add(task); //存入任务列表
			}
			log.info("配置文件加载成功！");
			
		} catch (Exception e) {
			System.out.println("加载配置文件出错，程序退出:" + e.getMessage());  
			log.error("加载配置文件出错，程序退出:" + e.getMessage() , e);
			System.exit(0);
		}
	}
	
	
}
