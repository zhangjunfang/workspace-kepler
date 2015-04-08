package com.ctfo.mcc.utils;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


public abstract class TaskAdapter implements Runnable {
	protected Logger log = LoggerFactory.getLogger(getClass());
	/** 任务名称 */
	protected String name = ""; 
	/** 自定义配置文件映射表 */
	protected Map<String, String> conf = null;
	/** 状态	 */
	protected String stats = "";
	
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
		try {
			execute(); //执行任务
		} catch (Exception e) {
			log.error(e.getMessage(), e);
		} 
	}

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public Map<String, String> getConf() {
		return conf;
	}
	public void setConf(Map<String, String> conf) {
		this.conf = conf;
	}
	
}
