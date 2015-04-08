package com.ctfo.filesaveservice.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public abstract class TaskAdapter implements Runnable {
	protected Logger log = LoggerFactory.getLogger(getClass());
	/** 任务名称 */
	protected String name = ""; 
	/** 自定义配置文件映射表 */
	protected Map<String, String> config = null;
	/** 状态	 */
	protected String stats = "";
	
	protected String threadName = "";
	
	public TaskAdapter(){
		
		config = new ConcurrentHashMap<String, String>();
	}
	 
	/**
	 * 初始化任务
	 */
	public abstract void init();
	
	/**
	 * 执行任务
	 */
	public abstract void execute();
	
	/**
	 * 默认执行入口，调用execute方法
	 */
	public void run() {
//		log.info("任务[" + this.getName() +"]开始执行...");
//		long st = System.currentTimeMillis();
		execute(); //执行任务
//		long et = System.currentTimeMillis();
//		log.info("任务[" + this.getName() +"]执行完成，"+stats+"共耗时：" + (et - st) + "ms");
	}

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * 获得自定义配置文件映射表的值
	 * @return the config 自定义配置文件映射表  
	 */
	public Map<String, String> getConfig() {
		return config;
	}

	/**
	 * 设置自定义配置文件映射表的值
	 * @param config 自定义配置文件映射表  
	 */
	public void setConfig(String key, String value) {
		this.config.put(key, value);
	}
	
}
